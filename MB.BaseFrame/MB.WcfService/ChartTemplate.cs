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
    public class ChartTemplate:IChartTemplate
    {
        public List<ChartTemplateInfo> GetObjects(string xmlFilterParams)
        {
            string sql = MB.WcfService.Properties.Resources.ChartTemplate_Sql_SelectObject;
            List<ChartTemplateInfo> lstData = new List<ChartTemplateInfo>();
            var pars = MB.Util.Serializer.QueryParameterXmlSerializer.DefaultInstance.DeSerializer(xmlFilterParams);
            string sqlFilter = MB.Orm.DbSql.SqlShareHelper.Instance.QueryParametersToSqlString(null, pars);
            sql = string.Format(sql, sqlFilter);
            using (IDataReader reader = MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    ChartTemplateInfo newInfo = new ChartTemplateInfo();
                    newInfo.ID = int.Parse(reader["ID"].ToString());
                    newInfo.NAME = reader["NAME"].ToString();
                    newInfo.RULE_PATH = reader["RULE_PATH"].ToString();
                    newInfo.FILTER_STRING = reader["FILTER_STRING"].ToString();
                    newInfo.TEMPLATE_FILE = (byte[])reader["TEMPLATE_FILE"];
                    newInfo.TEMPLATE_TYPE = reader["TEMPLATE_TYPE"].ToString();
                    newInfo.CREATE_USER = reader["CREATE_USER"].ToString();
                    newInfo.CREATE_DATE = (DateTime)reader["CREATE_DATE"];
                    newInfo.GRID_NAME = reader["GRID_NAME"].ToString();

                    lstData.Add(newInfo);
                }
            }
            return lstData; 
        }

        public List<ChartTemplateInfo> GetObjectByUser(string userCode)
        {
            List<MB.Util.Model.QueryParameterInfo> list = new List<Util.Model.QueryParameterInfo>();
            MB.Util.Model.QueryParameterInfo p = new Util.Model.QueryParameterInfo(Util.Model.QueryGroupLinkType.OR);
            p.Childs.Add(new Util.Model.QueryParameterInfo("TEMPLATE_TYPE", "PUBLIC", Util.DataFilterConditions.Equal));
            p.Childs.Add(new Util.Model.QueryParameterInfo("CREATE_USER", userCode, Util.DataFilterConditions.Equal));
            list.Add(p);

            string xmlFilterParam = MB.Util.Serializer.QueryParameterXmlSerializer.DefaultInstance.Serializer(list.ToArray());
            return GetObjects(xmlFilterParam);
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public int AddObjectImmediate(ChartTemplateInfo entity)
        {
            if (entity.ID > 0) DeletedImmediate(entity.ID);

            string sql = MB.WcfService.Properties.Resources.ChartTemplate_Sql_AddObject;
            List<MB.Orm.DbSql.SqlParamInfo> pars = new List<MB.Orm.DbSql.SqlParamInfo>();
            entity.ID = MB.Orm.Persistence.EntityIdentityHelper.NewInstance.GetEntityIdentity("CHART_TEMPLATE");
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("ID", entity.ID, DbType.Int32));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("NAME", entity.NAME, DbType.String));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("RULE_PATH", entity.RULE_PATH, DbType.String));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("GRID_NAME", entity.GRID_NAME, DbType.String));
            pars.Add(new Orm.DbSql.SqlParamInfo("FILTER_STRING", entity.FILTER_STRING, DbType.String));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("TEMPLATE_TYPE", entity.TEMPLATE_TYPE, DbType.String));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("TEMPLATE_FILE", entity.TEMPLATE_FILE, DbType.Binary));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("CREATE_USER", entity.CREATE_USER, DbType.String));
            return MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteNonQuery(sql, pars); 
        }

        public int DeletedImmediate(int id)
        {
            string sql = MB.WcfService.Properties.Resources.ChartTemplate_Sql_DeleteObject;
            List<MB.Orm.DbSql.SqlParamInfo> pars = new List<MB.Orm.DbSql.SqlParamInfo>();
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("ID", id, DbType.Int32));
            return MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteNonQuery(sql, pars); 
        }
    }
}
