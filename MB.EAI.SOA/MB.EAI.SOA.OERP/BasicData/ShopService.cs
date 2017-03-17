using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.EAI.SOA.OERP.Entities;
using MB.EAI.SOA.COMMON.CommonMSG;

namespace MB.EAI.SOA.OERP.BasicData
{
    public class ShopService
    {
        public static ShopService Instance=new ShopService();
        private const string XMLFILE = "Organization";
        public GetResponse<List<ShopInfo>> GetShopsInfo()
        {
            GetResponse<List<ShopInfo>> response = new GetResponse<List<ShopInfo>>();
            using (var dbScrop = new MB.Orm.DB.OperationDatabaseScope(new Orm.DB.OperationDatabaseContext("MB.OldERP")))
            {

                List<ShopInfo> lsShops = new List<ShopInfo>();
                lsShops = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<ShopInfo>(XMLFILE, "GetShopInfo");
                if (lsShops != null && lsShops.Count > 0)
                {
                    response.HeaderFlag = "1";
                    response.HeaderMSG = "Success";
                }
                response.Responseparams = lsShops;

                return response;
            }
        }
    }
}
