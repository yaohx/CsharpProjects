using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Orm.Persistence;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MB.Orm.DbSql.DynamicGroupBuilder
{
    /// <summary>
    /// 支持oracle数据库的动态聚组查询语句构造类
    /// </summary>
    public class DynamicGroupOracleBuilder : DynamicGroupBuilder
    {

        public DynamicGroupOracleBuilder(MB.Util.Model.DynamicGroupSetting setting) : base(setting)
        {
 
        }

        public override string BuildDynamicQuery(string sqlFilter)
        {
            //构造查询区域
            string querySql = GetQueryFields();
            querySql = querySql.TrimEnd(',');

            //构造主从表
            string fromSql = this.GetFromSql();

            //构造条件
            string whereSql = sqlFilter;

            //构造分组
            string groupSql = this.GetGroupFields();

            //构造聚组条件
            string aggConditionSql = this.GetAggConditionSql();

            string sql = string.Empty;
            if (!string.IsNullOrEmpty(aggConditionSql))
                 sql = string.Format("SELECT {0} FROM {1} WHERE {2} GROUP BY {3} HAVING {4}",
                                        querySql, fromSql, whereSql, groupSql, aggConditionSql);
            else
                sql = string.Format("SELECT {0} FROM {1} WHERE {2} GROUP BY {3} ",
                                        querySql, fromSql, whereSql, groupSql);
            return sql;
        }

        
    }
}
