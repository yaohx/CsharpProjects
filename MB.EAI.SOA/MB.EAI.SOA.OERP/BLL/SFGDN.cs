using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MB.EAI.SOA.OERP.Entities;
using MB.EAI.SOA.COMMON.CommonMSG;

namespace MB.EAI.SOA.OERP.BLL
{
    public class SFGDN
    {
        private const string XMLFILE = "Gdn";
        private static List<DistroLineCount> ProcessingNote = new List<DistroLineCount>();
        private static object SyncState = new object();

        #region wms
        public GetResponse<List<DistroGDNInfo>> GetDistroDeliveredNote()
        {
            GetResponse<List<DistroGDNInfo>> response = new GetResponse<List<DistroGDNInfo>>();
            using (var dbScrop = new MB.Orm.DB.OperationDatabaseScope(new Orm.DB.OperationDatabaseContext("MB.OldERP")))
            {
                List<DistroHeaderHelper> distroHeaderHelpers = new List<DistroHeaderHelper>();
                distroHeaderHelpers = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<DistroHeaderHelper>(XMLFILE, "GetDistroDeliveredNoteHeader");
                if (distroHeaderHelpers == null || distroHeaderHelpers.Count <= 0)
                {
                    return response;
                }
                GetHeaderParams(distroHeaderHelpers);
            }
            return response;
        }

        private void GetHeaderParams(List<DistroHeaderHelper> lsDistroHeader)
        {

            var lsDocType = lsDistroHeader.GroupBy(o => new { o.Src_Doc_Type }).Select(o => o.Key).ToList();
            if (lsDocType.FindIndex(o => o.Src_Doc_Type == "AAD") >= 0 || lsDocType.FindIndex(o => o.Src_Doc_Type == "SAD") >= 0)
                GetHeaderParamsByAGOR(lsDistroHeader);
            if (lsDocType.FindIndex(o => o.Src_Doc_Type == "FON") >= 0)
                GetHeaderParams(lsDistroHeader, "FON", "GetDistroDeliveredNoteHeaderByFON");
            if (lsDocType.FindIndex(o => o.Src_Doc_Type == "TBN") >= 0)
                GetHeaderParams(lsDistroHeader, "TBN", "GetDistroDeliveredNoteHeaderByTBN");
            if (lsDocType.FindIndex(o => o.Src_Doc_Type == "TFO") >= 0)
                GetHeaderParams(lsDistroHeader, "TFO", "GetDistroDeliveredNoteHeaderByTFO");
            if (lsDocType.FindIndex(o => o.Src_Doc_Type == "BBN") >= 0)
                GetHeaderParams(lsDistroHeader, "BBN", "GetDistroDeliveredNoteHeaderByBBN");
        }
        private void GetHeaderParamsByAGOR(List<DistroHeaderHelper> lsDistroHeader)
        {
            DataSet ds = new DataSet();
            List<DistroHeaderHelper> lsparam = lsDistroHeader.Where(o => o.Src_Doc_Type == "AAD" || o.Src_Doc_Type == "SAD").ToList();
            ds = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.GetDataSetByXml(XMLFILE, "", lsparam);
            DataTable dt = ds.Tables[0];

            dt.AsEnumerable().ToList().ForEach(o => lsDistroHeader
                        .Where(a => (a.Src_Doc_Type == o.Field<string>("Src_Doc_Type")
                                     && a.Src_Doc_Unit == o.Field<string>("Src_Doc_Unit")
                                     && a.Src_Doc_Num == o.Field<string>("Src_Doc_Num"))).ToList()
                                .ForEach(b =>
                                {
                                    b.PO_NBR = o.Field<string>("PO_NBR");
                                    b.STORE_NBR = o.Field<string>("STORE_NBR");
                                    b.SHPMT_NBR = o.Field<string>("SHPMT_NBR");
                                    b.IN_STORE_DATE = o.Field<string>("IN_STORE_DATE");
                                    b.START_SHIP_DATE = o.Field<string>("START_SHIP_DATE");
                                    b.DISTRO_TYPE = o.Field<string>("DISTRO_TYPE");
                                    b.IDT_NUM = o.Field<string>("IDT_NUM");
                                }));
        }
        private void GetHeaderParams(List<DistroHeaderHelper> lsDistroHeader, string srcDocType, string sqlStr)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<DistroHeaderHelper> lsparam = lsDistroHeader.Where(o => o.Src_Doc_Type == srcDocType).ToList();
       
            dt = MB.EAI.SOA.COMMON.DBHelper.OracleDBHelper.GetDataSetByXml<DistroHeaderHelper>(XMLFILE, sqlStr, lsparam);

            dt.AsEnumerable().ToList().ForEach(o => lsDistroHeader
                        .Where(a => (a.Src_Doc_Type == o.Field<string>("Src_Doc_Type")
                                     && a.Src_Doc_Unit == o.Field<string>("Src_Doc_Unit")
                                     && a.Src_Doc_Num == o.Field<string>("Src_Doc_Num"))).ToList()
                                .ForEach(b =>
                                {
                                    b.PO_NBR = o.Field<string>("PO_NBR");
                                    b.SHPMT_NBR = o.Field<string>("SHPMT_NBR");
                                    b.IN_STORE_DATE = o["IN_STORE_DATE"]==null? "":o["IN_STORE_DATE"].ToString();
                                    b.START_SHIP_DATE = o["START_SHIP_DATE"] == null ? "" : o["START_SHIP_DATE"].ToString();
                                    b.DISTRO_TYPE = o.Field<string>("DISTRO_TYPE");
                                    b.IDT_NUM = o.Field<string>("IDT_NUM");
                                }));
        }
        #endregion

        public const int MaxLineCount = 2000;
        #region SAP 
        public List<object> GetGdnDistro()
        {
            //1、获取可读取的出库单号+行数
            //2、根据时间 行数排序
            //3、累计行数 将超过指定行数的倒序去除
            //4、收集出库单head信息（移动类型、供应商、工厂、库存地等）
            //5、收集出库单明细信息
            //6、拼接格式 返回。
            List<DistroLineCount> originalOrderHeader = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<DistroLineCount>(XMLFILE, "GetDistroDeliveredNoteSAP", null);
           
            lock (SyncState)
            {
                originalOrderHeader.ForEach(o =>
                {
                    if (!ProcessingNote.Exists(p => p.Unit_id == o.Unit_id && p.Gdn_Num == o.Gdn_Num))
                    {
                        ProcessingNote.Add(o);
                    }
                });
            }

            int currentLineCount = 0;
            int index = 0;
            for (; index < originalOrderHeader.Count; index++)
            {
                currentLineCount += originalOrderHeader[index].LineCount;
                if (currentLineCount > MaxLineCount)
                    break;
            }
            //4、获取相关主表属性
            List<DistroLineCount> waitforDipose = new List<DistroLineCount>();
            if (index > 0)
            {
                waitforDipose.AddRange(originalOrderHeader.GetRange(0,index));
            }


            
            
            //originalOrderHeader.DefaultView.Sort="";
            //originalOrderHeader = originalOrderHeader.DefaultView.ToTable();

            //var t = from o in originalOrderHeader.AsEnumerable()
            //        select o.
            
            return null;
        }
        #endregion
    }

    public class DistroLineCount{
        public string Unit_id{get;set;}
        public string Gdn_Num{get;set;}
        public int LineCount{get;set;}

    }

    public class DistroSapInfo
    {
        public string Unit_ID { get; set; }
        public string  Gdn_Num { get; set; }
        public DateTime Doc_Date { get; set; }
        public string Wareh_ID { get; set; }
        public string Rcv_Wareh_ID { get; set; }
        



        public string InnerOrder { get; set; }
        public string OuterOrder { get; set; }

        public string MoveType { get; set; }

        public string FactoryCode { get; set; }
        public string Wareh_Code { get; set; }
        public string EnterpriseCode { get; set; }

        public string Rcv_FactoryCode { get; set; }
        public string Rcv_Wareh_Code { get; set; }
        public string Rcv_EnterpriseCode { get; set; }

        public string CustomNo { get; set; }
        public string VenderNO { get; set; }

        

    }

    public class DistroSapDtlInfo
    {
        public string Prod_ID { get; set; }
        public int Qty { get; set; }

        public string ItemNo { get; set; }
        public string MaterialNO { get; set; }
        public string Grid { get; set; }

        public string Meins { get; set; }
    }
}
