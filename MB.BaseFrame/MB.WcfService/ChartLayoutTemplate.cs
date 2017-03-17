using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ServiceModel;
using MB.WcfService.IFace;
using MB.Util.Model.Chart;

namespace MB.WcfService
{
    [MB.Aop.InjectionManager]
    [MB.WcfService.WcfFaultBehavior(typeof(MB.WcfService.WcfFaultExceptionHandler))]
    public class ChartLayoutTemplate : IChartLayoutTemplate
    {
        public List<ChartLayoutTemplateInfo> GetObjects(string xmlFilterParams)
        {
            string sql = MB.WcfService.Properties.Resources.ChartLayoutTemplate_Sql_SelectObject;
            List<ChartLayoutTemplateInfo> lstData = new List<ChartLayoutTemplateInfo>();
            var pars = MB.Util.Serializer.QueryParameterXmlSerializer.DefaultInstance.DeSerializer(xmlFilterParams);
            string sqlFilter = MB.Orm.DbSql.SqlShareHelper.Instance.QueryParametersToSqlString(null, pars);
            sql = string.Format(sql, sqlFilter);
            using (IDataReader reader = MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    ChartLayoutTemplateInfo newInfo = new ChartLayoutTemplateInfo();
                    newInfo.ID = int.Parse(reader["ID"].ToString());
                    newInfo.NAME = reader["NAME"].ToString();
                    newInfo.TEMPLATE_FILE = (byte[])reader["TEMPLATE_FILE"];
                    newInfo.TEMPLATE_TYPE = reader["TEMPLATE_TYPE"].ToString();
                    newInfo.CREATE_USER = reader["CREATE_USER"].ToString();
                    newInfo.CREATE_DATE = (DateTime)reader["CREATE_DATE"];

                    lstData.Add(newInfo);
                }
            }
            return lstData; 
        }

        public List<ChartLayoutItemInfo> GetObjectDetail(int id)
        {
            string sql = MB.WcfService.Properties.Resources.ChartLayoutItem_Sql_SelectObject;
            List<ChartLayoutItemInfo> lstData = new List<ChartLayoutItemInfo>(); 
            List<MB.Orm.DbSql.SqlParamInfo> pars = new List<MB.Orm.DbSql.SqlParamInfo>();
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("ID", id, DbType.Int32));
            return MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.GetObjects<ChartLayoutItemInfo>(null, typeof(ChartLayoutItemInfo), sql, pars);
        }


        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public int AddObjectImmediate(ChartLayoutTemplateInfo entity, List<ChartLayoutItemInfo> items)
        {
            if (entity.ID > 0) DeletedImmediate(entity.ID);

            string sql = MB.WcfService.Properties.Resources.ChartLayoutTemplate_Sql_AddObject;
            List<MB.Orm.DbSql.SqlParamInfo> pars = new List<MB.Orm.DbSql.SqlParamInfo>();
            entity.ID = MB.Orm.Persistence.EntityIdentityHelper.NewInstance.GetEntityIdentity("CHART_LAYOUT_TEMPLATE");
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("ID", entity.ID, DbType.Int32));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("NAME", entity.NAME, DbType.String));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("TEMPLATE_TYPE", entity.TEMPLATE_TYPE, DbType.String));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("TEMPLATE_FILE", entity.TEMPLATE_FILE, DbType.Binary));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("CREATE_USER", entity.CREATE_USER, DbType.String));

            int iRet = MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteNonQuery(sql, pars);

            foreach (ChartLayoutItemInfo item in items)
            {
                sql = MB.WcfService.Properties.Resources.ChartLayoutItem_Sql_AddObject;
                pars.Clear();
                
                item.ID = MB.Orm.Persistence.EntityIdentityHelper.NewInstance.GetEntityIdentity("CHART_LAYOUT_ITEM");
                pars.Add(new MB.Orm.DbSql.SqlParamInfo("ID", item.ID, DbType.Int32));
                pars.Add(new MB.Orm.DbSql.SqlParamInfo("NAME", item.NAME, DbType.String));
                pars.Add(new MB.Orm.DbSql.SqlParamInfo("LT_ID", entity.ID, DbType.String));
                pars.Add(new MB.Orm.DbSql.SqlParamInfo("CT_ID", item.CT_ID, DbType.String));
                pars.Add(new MB.Orm.DbSql.SqlParamInfo("ITEM_TYPE", item.ITEM_TYPE, DbType.String));
                pars.Add(new MB.Orm.DbSql.SqlParamInfo("TEXT", item.TEXT, DbType.String));
                pars.Add(new MB.Orm.DbSql.SqlParamInfo("FORM_TEXT", item.FORM_TEXT, DbType.String));

                iRet += MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteNonQuery(sql, pars);
            }
            return iRet;
        }

        public List<ChartLayoutTemplateInfo> GetObjectByUserCode(string userCode)
        {
            List<MB.Util.Model.QueryParameterInfo> list = new List<Util.Model.QueryParameterInfo>();
            MB.Util.Model.QueryParameterInfo p = new Util.Model.QueryParameterInfo(Util.Model.QueryGroupLinkType.OR);
            p.Childs.Add(new Util.Model.QueryParameterInfo("TEMPLATE_TYPE", "PUBLIC", Util.DataFilterConditions.Equal));
            p.Childs.Add(new Util.Model.QueryParameterInfo("CREATE_USER", userCode, Util.DataFilterConditions.Equal));
            list.Add(p);

            string xmlFilterParam = MB.Util.Serializer.QueryParameterXmlSerializer.DefaultInstance.Serializer(list.ToArray());
            return GetObjects(xmlFilterParam);
        }

        public int DeletedImmediate(int id)
        {
            string sql = MB.WcfService.Properties.Resources.ChartLayoutItem_Sql_DeleteObject;
            List<MB.Orm.DbSql.SqlParamInfo> pars = new List<MB.Orm.DbSql.SqlParamInfo>();
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("ID", id, DbType.Int32));
            int ret = MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteNonQuery(sql, pars);

            sql = MB.WcfService.Properties.Resources.ChartLayoutTempalte_Sql_DeleteObject;
            ret += MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteNonQuery(sql, pars);

            return ret;
        }
    }
}
