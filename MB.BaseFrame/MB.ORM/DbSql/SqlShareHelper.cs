//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-08
// Description	:	SqlShareHelper 提供创建SQL 语句的静态方法。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using MB.Orm.Common;
using MB.Orm.Mapping;
namespace MB.Orm.DbSql {
    /// <summary>
    /// SqlShareHelper 提供创建SQL 语句的静态方法。
    /// </summary>
    public sealed class SqlShareHelper {
        /// <summary>
        /// Oracle 参数前缀。
        /// </summary>
        public static readonly string ORACLE_PARAM_PREFIX = ":";
        /// <summary>
        /// SQL SERVER 参数前缀。
        /// </summary>
        public static readonly string SQL_SERVER_PARAM_PREFIX = "@";
        //拼接SQL 语句的最大长度。
        private const int SQL_MAX_LENGTH = 7600;
        /// <summary>
        /// 获取当前数据库对应参数前缀。
        /// XML 文件中配置的参数 @
        /// </summary>
        public static string SQL_XML_CFG_PARAM_PREFIX = SQL_SERVER_PARAM_PREFIX;//DbShare.Instance.GetPramPrefixByDatabaseType(DbShare.Instance.GetCurrentDatabaseType);
        /// <summary>
        /// XML SQL 配置特殊参数。
        /// </summary>
        public static string[] SQL_SPEC_STRING = new string[] { SQL_XML_CFG_PARAM_PREFIX + "WHERE", SQL_XML_CFG_PARAM_PREFIX + "ORDERBY", SQL_XML_CFG_PARAM_PREFIX + "GROUPBY" };


        private static readonly string SQL_LEFT_BRACKET = "(";
        private static readonly string SQL_RIGHT_BRACKET = ")";
        private static readonly string SQL_AND = " AND ";
        private static readonly string SQL_AND_NOT = " AND NOT ";
        private static readonly string SQL_OR = " OR ";
        private static readonly string SQL_OR_NOT = " OR NOT ";

       private static MB.Util.MyHashtable<string, string> _HS_SQL_SPEC_STRING;
       public static MB.Util.MyHashtable<string, string> HS_SQL_SPEC_STRING {
           get {
               return _HS_SQL_SPEC_STRING;
           }
       }
        /// <summary>
        /// 
        /// </summary>
       static SqlShareHelper() {
           _HS_SQL_SPEC_STRING = new MB.Util.MyHashtable<string, string>();
            foreach (string p in SQL_SPEC_STRING) {
                _HS_SQL_SPEC_STRING.Add(p, p);
            }
        }

        #region Instance...
        private static Object _Obj = new object();
        private static SqlShareHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected SqlShareHelper() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static SqlShareHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new SqlShareHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        #region BuildQueryInSql...
        /// <summary>
        /// 根据数组创建查询语句中的In查询语句.
        /// 如果有可能会产生超过8000个字符，请使用 SplitInSqlStringBySqlMaxLength 代替
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public string BuildQueryInSql<T>(IList<T> ids) {
            List<string> hasAdd = new List<string>();
            foreach (T id in ids) {
                string s = id.ToString();
                if (string.IsNullOrEmpty(s)) continue;


                //特殊处理以兼容老的应用 (判断用户是否已经对多个值加了单引号)
                if (s.IndexOf("'") != 0 || s.LastIndexOf("'") != s.Length - 1) {
                    s = "'" + s.Replace("'", "''") + "'";
                }
                if (hasAdd.Contains(s)) continue;

                hasAdd.Add(s);
            }
            if (hasAdd.Count == 0)
                hasAdd.Add("'-1'");

            return string.Join(",", hasAdd.ToArray());
        }
        /// <summary>
        /// 根据数组创建查询语句中的In查询语句.
        /// 如果有可能会产生超过8000个字符，请使用 SplitInSqlStringBySqlMaxLength 代替
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public string BuildQueryInSql<T>(IList<T> ids, string fieldName) {
            //MB.Util.TraceEx.Assert(ids.Count > 0 && fieldName != null && fieldName.Length > 0, "在BuildQueryInSql函数中,传入的参数不正确.");
            string sql = fieldName + " IN(";
            List<string> hasAdd = new List<string>();
            foreach (T id in ids) {
                string s = "'" + id.ToString().Replace("'", "''") + "'";
                if (hasAdd.Contains(s)) continue;

                hasAdd.Add(s);


            }
            if (hasAdd.Count == 0)
                hasAdd.Add("'-1'");

            sql += string.Join(",", hasAdd.ToArray());

            sql += ")";
            return sql;
        }
        		
        /// <summary>
        /// 根据数组创建查询语句中的In查询语句.
        /// 如果有可能会产生超过8000个字符，请使用 SplitInSqlStringBySqlMaxLength 代替
        /// </summary>
        /// <param name="drsData"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public string BuildQueryInSql(DataRow[] drsData, string fieldName, bool isString) {
           // MB.Util.TraceEx.Assert(drsData.Length > 0 && fieldName != null && fieldName.Length > 0, "在BuildQueryInSql函数中,传入的参数不正确.");
            if (drsData.Length == 0) {
                return string.Format("{0} IN ('1')", fieldName);
            }
            System.Text.StringBuilder sqlSB = new StringBuilder();
            sqlSB.Append(fieldName);
            sqlSB.Append(" IN(");
            List<string> hasAdd = new List<string>();
            
            foreach (DataRow dr in drsData) {
                string keyVal = dr[fieldName].ToString();

                if (hasAdd.Contains(keyVal)) continue;

                hasAdd.Add(keyVal);

                if (isString)
                    sqlSB.Append("'").Append(keyVal).Append("',");
                else
                    sqlSB.Append(keyVal).Append(",");
            }
            sqlSB.Remove(sqlSB.Length - 1, 1);
            sqlSB.Append(")");
            return sqlSB.ToString();
        }
        //end of added
        /// <summary>
        /// 根据数组创建查询语句中的In查询语句.
        /// 如果有可能会产生超过8000个字符，请使用 SplitInSqlStringBySqlMaxLength 代替
        /// </summary>
        /// <param name="drsData"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public string BuildQueryInSql(DataRow[] drsData, string fieldName) {
            //MB.Util.TraceEx.Assert(drsData.Length > 0 && fieldName != null && fieldName.Length > 0, "在BuildQueryInSql函数中,传入的参数不正确.");
            if (drsData.Length == 0) {
                return string.Format("{0} IN ('1')", fieldName);
            }
            string sql = fieldName + " IN(";
            List<string> hasAdd = new List<string>();
            foreach (DataRow dr in drsData) {
                string keyVal = dr[fieldName].ToString();

                if (hasAdd.Contains(keyVal)) continue;

                hasAdd.Add(keyVal);

                sql += keyVal + ",";
            }
            sql = sql.Remove(sql.Length - 1, 1);
            sql += ")";
            return sql;
        }
        #endregion BuildQueryInSql...
        /// <summary>
        /// 拆分创建符合 IN 查询语句的SQL 语句
        /// 拆分的条件是拼接的字符窜长度不能超过允许的最大长度（8000 字节）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vals"></param>
        /// <returns></returns>
        public string[] SplitInSqlStringBySqlMaxLength<T>(IList<T> vals) {
            List<string> sqls = new List<string>();
            List<string> lstData = new List<string>();
            int singleLength = 0;
            if (vals.Count == 0) {
                return new string[]{"'-1'"} ;
            }

            foreach (T v in vals) {
                singleLength += v.ToString().Length;

                if (singleLength < SQL_MAX_LENGTH) {
                    lstData.Add("'" + v.ToString().Replace("'", "''") + "'"); //可以把 v 中特殊字符替换掉
                }
                else {
                    sqls.Add(string.Join(",", lstData.ToArray()));
                    lstData.Clear();
                    singleLength = v.ToString().Length;
                    lstData.Add("'" + v.ToString().Replace("'", "''") + "'"); //可以把 v 中特殊字符替换掉
                }
            }
            if (lstData.Count > 0) {
                sqls.Add(string.Join(",", lstData.ToArray()));
            }
            return sqls.ToArray();
        }
        /// <summary>
        /// 通过值的个数进行拆分
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vals"></param>
        /// <param name="splitValueCcount"></param>
        /// <returns></returns>
        public string[] SplitInSqlStringByValueCount<T>(IList<T> vals,int splitValueCcount) {
            List<string> sqls = new List<string>();
            List<string> lstData = new List<string>();
            if (vals.Count == 0) {
                return new string[] { "'-1'" };
            }
            foreach (T v in vals) {
     
                if (lstData.Count < splitValueCcount ) {
                    lstData.Add("'" + v.ToString().Replace("'", "''") + "'"); //可以把 v 中特殊字符替换掉
                }
                else {
                    sqls.Add(string.Join(",", lstData.ToArray()));
                    lstData.Clear();
                    lstData.Add("'" + v.ToString().Replace("'", "''") + "'"); //可以把 v 中特殊字符替换掉
                }
            }
            if (lstData.Count > 0) {
                sqls.Add(string.Join(",", lstData.ToArray()));
            }
            return sqls.ToArray();
        }

        ///// <summary>
        ///// 根据指定设置在XML 文件中SQL 语句和DataRow 获取需要的参数。
        ///// </summary>
        ///// <param name="xmlFile"></param>
        ///// <param name="sqlName"></param>
        ///// <param name="drData"></param>
        ///// <returns></returns>
        //[Obsolete("如果需要使用需要详细考虑一下。")]
        //public IList<SqlParamInfo> CreateSqlParams(string xmlFile, string sqlName, DataRow drData) {
        //    SqlString[] cfgSql = MB.Orm.Mapping.Xml.SqlConfigHelper.Instance.GetMultiSqlString(xmlFile, sqlName);
        //    foreach (SqlString sqlInfo in cfgSql) {
        //        IList<SqlParamInfo> pars = sqlInfo.ParamFields;
        //        DataColumnCollection drs = drData.Table.Columns;
        //        foreach (SqlParamInfo par in pars) {
        //            MB.Util.TraceEx.Assert(drs.Contains(par.MappingName), "SQL语句中的参数" + par + "在数据集中不存在.");
        //            par.Value = drData[par.MappingName];
        //        }
        //    }
        //    return pars;

        //}

        /// <summary>
        /// 以字符窜的形式获取指定字段的值，并拼接成字符窜。
        /// 这里的ID 字段只能是Int 类型。
        /// </summary>
        /// <param name="drsData"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public string GetKeyValuesToString(DataRow[] drsData, string keyName) {
            StringBuilder strB = new StringBuilder();
            foreach (DataRow dr in drsData) {
                object val = dr[keyName];
                if (val == null || MB.Util.MyConvert.Instance.ToInt(val) <= 0)
                    continue;
                strB.Append("'" + val.ToString() + "',");
            }
            if (strB.Length > 0)
                strB = strB.Remove(strB.Length - 1, 1);
            else
                strB.Append(Int16.MinValue.ToString());

            return strB.ToString();
        }
        /// <summary>
        /// 获取字符窜中参数的名称.
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public IList<string> GetSqlStringParamsName(string sqlStr) {
            string filter = ",;) ";
            IList<string> fs = new List<string>();
            int first = 0;
            for (int i = 0; i < sqlStr.Length; i++) {
                string c = sqlStr.Substring(i, 1);
                if (c.Equals(SQL_XML_CFG_PARAM_PREFIX)) {
                    first = i;
                    continue;
                }
                else if (first > 0 && filter.IndexOf(c) >= 0) {
                    string par = sqlStr.Substring(first + 1, i - first - 1);
                    if (!fs.Contains(par))
                        fs.Add(par);
                    first = -1;
                    continue;
                }
            }
            return fs;
        }
        /// <summary>
        /// 把行中的值替换SQL语句中的参数满足执行的要求
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="paramsName"></param>
        /// <param name="drData"></param>
        /// <returns></returns>
        public string ReplaceCanExecSqlString(string sqlStr, IList<string> paramsName, DataRow drData) {
            MB.Orm.Enums.DatabaseType dbaseType = MB.Orm.Persistence.DatabaseHelper.CreateDatabaseType();  
            string sql = sqlStr;
            DataColumnCollection cols = drData.Table.Columns;
            foreach (string par in paramsName) {
                MB.Util.TraceEx.Assert(cols.Contains(par), "SQL语句中的参数" + par + "在数据集中不存在.");

                string val = valueToSQL(dbaseType,cols[par].DataType.Name, drData[par], MB.Util.DataFilterConditions.Equal);

                sql = sql.Replace(SQL_XML_CFG_PARAM_PREFIX + par + ",", val + ",");
                sql = sql.Replace(SQL_XML_CFG_PARAM_PREFIX + par + " ", val + " ");
                sql = sql.Replace(SQL_XML_CFG_PARAM_PREFIX + par + ";", val + "; ");
                sql = sql.Replace(SQL_XML_CFG_PARAM_PREFIX + par + ")", val + ")");
            }
            return sql;
        }
        /// <summary>
        /// 把行中的值替换SQL语句中的参数满足执行的要求
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="parsValue"></param>
        /// <returns></returns>
        public string ReplaceCanExecSqlStringEx(string sqlStr, params object[] parsValue) {
            MB.Orm.Enums.DatabaseType dbaseType = MB.Orm.Persistence.DatabaseHelper.CreateDatabaseType();
            IList<string> parsName = GetSqlStringParamsName(sqlStr);
            string sql = sqlStr;
            if(parsName.Count != parsValue.Length)
                throw new MB.Util.APPException("在执行SQL语句" + sqlStr + "时获取的参数和传入的参数不一致，请检查。", MB.Util.APPMessageType.SysErrInfo);
            for (int i = 0; i < parsName.Count; i++) {
 
                object val = parsValue[i];

                if (val!=null && string.Compare(parsName[i].ToString(), "WHERE", true) != 0) {
                   
                   val = valueToSQL(dbaseType, val.GetType().Name , val, MB.Util.DataFilterConditions.Equal);

                }
                else if (val == null) {
                    val = "NULL";
                }
                sql = sql.Replace(SQL_XML_CFG_PARAM_PREFIX + parsName[i].ToString() + " ", val.ToString() + " ");
                sql = sql.Replace(SQL_XML_CFG_PARAM_PREFIX + parsName[i].ToString() + ",", val.ToString() + ",");
                sql = sql.Replace(SQL_XML_CFG_PARAM_PREFIX + parsName[i].ToString() + ";", val.ToString() + ";");
                sql = sql.Replace(SQL_XML_CFG_PARAM_PREFIX + parsName[i].ToString() + ")", val.ToString() + ")");
            }
            return sql;
        }
        /// <summary>
        /// 根据查询的参数数组转换为可以进行查询的SQL 字符窜。
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public string QueryParametersToSqlString(MB.Orm.Mapping.QueryParameterMappings parsMapping, MB.Util.Model.QueryParameterInfo[] queryParams) {
            MB.Orm.Enums.DatabaseType dbaseType = MB.Orm.Persistence.DatabaseHelper.CreateDatabaseType();     
            StringBuilder sqlFilter = new StringBuilder();
            string targetRowCount = MB.Orm.Persistence.DbQueryTargetRowCountScope.GetTargetRowCountSqlFilter(dbaseType);
            if (queryParams == null || queryParams.Length == 0) {
                if (string.IsNullOrEmpty(targetRowCount))
                    return "0=0";
                else
                    return targetRowCount;
            }
            var pars = Array.FindAll<MB.Util.Model.QueryParameterInfo>(queryParams, o => o.Limited != true);
            if (pars.Length == 0) {
                if (string.IsNullOrEmpty(targetRowCount))
                    return "0=0";
                else
                    return targetRowCount;
            }

            builderSqlStringByParameters(dbaseType,sqlFilter, MB.Util.Model.QueryGroupLinkType.AND, parsMapping, pars);

            string sql = "(" + sqlFilter.ToString() + ")";
            if (!string.IsNullOrEmpty(targetRowCount))
                sql += " AND " + targetRowCount;

            return sql;
        }

        /// <summary>
        /// 根据查询的条件转换为SQL 查询的字符窜。 
        /// </summary>
        /// <param name="queryCondition"></param>
        /// <returns></returns>
        public string ConvertConditionToSqlStr(MB.Util.DataFilterConditions queryCondition) {
            switch (queryCondition) {
                case MB.Util.DataFilterConditions.Special:
                case MB.Util.DataFilterConditions.Equal:
                    return " = ";
                case MB.Util.DataFilterConditions.NotEqual:
                    return " <> ";
                case MB.Util.DataFilterConditions.GreaterOrEqual:
                    return " >= ";
                case MB.Util.DataFilterConditions.Greater:
                    return " > ";
                case MB.Util.DataFilterConditions.EndsWith:
                case MB.Util.DataFilterConditions.BenginsWith:
                case MB.Util.DataFilterConditions.Include:
                case MB.Util.DataFilterConditions.Like:
                    return " LIKE ";
                case MB.Util.DataFilterConditions.NotInclude:
                case MB.Util.DataFilterConditions.NotLike:
                    return " NOT LIKE ";
                case MB.Util.DataFilterConditions.LessOrEqual:
                    return " <= ";
                case MB.Util.DataFilterConditions.Less:
                    return " < ";
                case MB.Util.DataFilterConditions.Between:
                    return " BETWEEN ";
                case MB.Util.DataFilterConditions.NotBetween:
                    return " NOT BETWEEN ";
                case MB.Util.DataFilterConditions.IsNull:
                    return " IS NULL ";
                case MB.Util.DataFilterConditions.IsNotNull:
                    return " IS NOT NULL ";
                case MB.Util.DataFilterConditions.In:
                    return " IN ";
                case MB.Util.DataFilterConditions.NotIn:
                    return " NOT IN ";

                default:
                    return string.Empty;
            }
        }

        #region 内部函数处理...
        //通过参数 创建SQL 查询语句
        private void builderSqlStringByParameters(MB.Orm.Enums.DatabaseType dbaseType, StringBuilder sqlFilter, MB.Util.Model.QueryGroupLinkType linkType, MB.Orm.Mapping.QueryParameterMappings parsMapping, MB.Util.Model.QueryParameterInfo[] queryParams) {

            foreach (MB.Util.Model.QueryParameterInfo parInfo in queryParams) {
                List<MB.Util.Model.QueryParameterInfo> childParams = null;
                if (parInfo.IsGroupNode) {
                    if (parInfo.Childs == null || parInfo.Childs.Count == 0) continue;

                    childParams = parInfo.Childs.FindAll(o => o.Limited != true);
                    if (childParams.Count == 0) continue;

                }
                string sql = sqlFilter.ToString().Trim();
                if (sql.Length > 0 && sql.LastIndexOf(SQL_LEFT_BRACKET) != sql.Length - 1) {
                    if (linkType == MB.Util.Model.QueryGroupLinkType.AND)

                        sqlFilter.Append(SQL_AND);
                    else if (linkType == MB.Util.Model.QueryGroupLinkType.AndNot)
                        sqlFilter.Append(SQL_AND_NOT);
                    else if (linkType == MB.Util.Model.QueryGroupLinkType.OrNot)
                        sqlFilter.Append(SQL_OR_NOT);
                    else
                        sqlFilter.Append(SQL_OR);
                }
                sqlFilter.Append(SQL_LEFT_BRACKET);

                if (parInfo.IsGroupNode) {
                    builderSqlStringByParameters(dbaseType, sqlFilter, parInfo.GroupNodeLinkType, parsMapping, childParams.ToArray());
                }
                else {
                    //edit chendc 2010-07-09 增加对特殊条件的处理
                    string singleSql = string.Empty;
                    if (parInfo.Condition == Util.DataFilterConditions.SqlAppend) {
                        if (parInfo.Value == null)
                            throw new MB.Util.APPException(string.Format("参数{0} 的DataFilterConditions 设置为 sqlAppend,value 不能为空 ", parInfo.PropertyName), Util.APPMessageType.SysErrInfo);

                        singleSql = parInfo.Value.ToString();
                    }
                    else {
                        singleSql = singleParameterToSql(dbaseType, parsMapping, parInfo);
                    }

                    sqlFilter.Append(singleSql);
                }
                sqlFilter.Append(SQL_RIGHT_BRACKET);

            }

        }

        //单个参数转换为SQL 语句
        private string singleParameterToSql(MB.Orm.Enums.DatabaseType dbaseType, MB.Orm.Mapping.QueryParameterMappings parsMapping, MB.Util.Model.QueryParameterInfo parInfo) {
            StringBuilder sqlFilter = new StringBuilder();
            string defaultTableAlias = (parsMapping == null || string.IsNullOrEmpty(parsMapping.DefaultTableAlias)) ? "" : parsMapping.DefaultTableAlias + ".";
            // 
            QueryParameterMappingInfo mappingInfo = (parsMapping != null && parsMapping.ContainsKey(parInfo.PropertyName)) ? parsMapping[parInfo.PropertyName] : null;
            string dbFieldName = mappingInfo == null ? (defaultTableAlias + parInfo.PropertyName) : mappingInfo.FieldName;
            if (parInfo.Condition == MB.Util.DataFilterConditions.Special)
                dbFieldName = parInfo.PropertyName;

            //判断是否为多个值的输入形式
            if (parInfo.MultiValue && (parInfo.Condition== Util.DataFilterConditions.In
                || parInfo.Condition== Util.DataFilterConditions.Equal)) {
                string inStr = BuildQueryInSql<string>(parInfo.Value.ToString().Split(','));
                if (inStr.Length > MB.Orm.DbSql.SqlShareHelper.SQL_MAX_LENGTH)
                    throw new MB.Util.APPException(string.Format("构造字段{0} 的IN 查询语句时 超长",dbFieldName));
                return string.Format("{0} IN ({1})", dbFieldName, inStr);
            }

            if (string.Compare(parInfo.DataType, "DateTime", true) == 0) {
                sqlFilter.Append(string.Format("{0}", dbFieldName));
            }
            else
                sqlFilter.Append(dbFieldName);

            sqlFilter.Append(ConvertConditionToSqlStr(parInfo.Condition));

            if (parInfo.Condition != MB.Util.DataFilterConditions.IsNotNull &&
                parInfo.Condition != MB.Util.DataFilterConditions.IsNull) {
                if (parInfo.Condition == MB.Util.DataFilterConditions.Between) {
                    sqlFilter.Append(valueToSQL(dbaseType,parInfo.DataType, parInfo.Value, parInfo.Condition));

                    sqlFilter.Append(SQL_AND);

                    sqlFilter.Append(valueToSQL(dbaseType,parInfo.DataType, parInfo.Value2, parInfo.Condition, true));
                }
                else {

                    sqlFilter.Append(valueToSQL(dbaseType,parInfo.DataType, parInfo.Value, parInfo.Condition));
                }
            }
            return sqlFilter.ToString();
        }

        //把值转换为SQL 存储的格式
        private string valueToSQL(MB.Orm.Enums.DatabaseType dbaseType, string typeName, object dataValue, MB.Util.DataFilterConditions queryCondition) {
            return valueToSQL(dbaseType,typeName, dataValue, queryCondition, false);
        }
        private string valueToSQL(MB.Orm.Enums.DatabaseType dbaseType,string typeName, object dataValue, MB.Util.DataFilterConditions queryCondition,bool isBetweenEnd) {
            if (dataValue == null || dataValue == System.DBNull.Value || dataValue.ToString().Length == 0) {
                return "NULL";
            }
            if(string.IsNullOrEmpty(typeName))
                return stringValueToSqlString(dataValue, queryCondition);

            //判断是否为FullName   如： System.Int32
            int index = typeName.IndexOf('.');
            if (index > 0) {
                typeName = typeName.Substring(index + 1, typeName.Length - index - 1);
            }
            //判断如果是In 查询的话要特殊处理
            if (queryCondition == MB.Util.DataFilterConditions.In 
                || queryCondition == MB.Util.DataFilterConditions.NotIn 
                || queryCondition  == MB.Util.DataFilterConditions.Special) {

                return stringValueToSqlString(dataValue, queryCondition);
            }
            switch (typeName) {
                case "Int16":
                case "Int32":
                case "Decimal":
                    decimal dre = 0;
                    bool b = decimal.TryParse(dataValue.ToString(), out dre);
                    return dre.ToString() ;
                case "Boolean":
                    if (dataValue != null && (string.Compare(dataValue.ToString(), "TRUE",true)==0 || dataValue.ToString() == "1")) {
                        return "1";
                    }
                    else {
                        return "0";
                    }
                case "String":
                    return stringValueToSqlString(dataValue, queryCondition);
                case "DateTime":
                case "DateTime?":
                    if (string.Compare(typeName, "DateTime?", true) == 0) {
                        if (dataValue == null) {
                            throw new MB.Util.APPException("valueToSQL 传入的时间类型DateTime?的条件是空", Util.APPMessageType.DisplayToUser);
                        }
                    }
                    //临时解决时间查询的问题， 所有时间的查询都把小时、分秒去掉，只保留日期。
                    
                    //modify by aifang 增加按照配置日期函数查询，支持时分秒 begin 2012-4-6
                    DateTime tempDate = DateTime.Parse(Convert.ToDateTime(dataValue).ToString());//DateTime tempDate = DateTime.Parse(Convert.ToDateTime(dataValue).ToShortDateString());//;//Convert.ToDateTime(dataValue);//
                    //modify by aifang 增加按照配置日期函数查询，支持时分秒 end

                    //需要完善 //性能影响
                    var dbType = dbaseType;
                    bool withTimeFormate = (tempDate.Hour > 0 || tempDate.Minute > 0 || tempDate.Second > 0);
                    string sysFormate = withTimeFormate ? MB.BaseFrame.SOD.DATE_TIME_FORMATE : MB.BaseFrame.SOD.DATE_WITHOUT_TIME_FORMATE;
                    string dbFormate = withTimeFormate ? MB.BaseFrame.SOD.DATABASE_DATE_TIME_FORMATE : MB.BaseFrame.SOD.DATABASE_WITHOUT_DATE_TIME_FORMATE;

                    if (dbType == MB.Orm.Enums.DatabaseType.Oracle) {

                        if (isBetweenEnd) {
                            string f = "(to_Date('" + tempDate.ToString(sysFormate) + "','" + dbFormate + "'){0})";
                            f = withTimeFormate ? string.Format(f, "") : string.Format(f, "+ 0.99999");  //变态的方法解决时间格式的问题 (到选择日期的11点59分)
                            return f;
                        }
                        else {
                            return "to_Date('" + tempDate.ToString(sysFormate) + "','" + dbFormate + "')";
                        }

                    }
                    else {
                        return "'" + tempDate.ToString(sysFormate) + "'";
                    }
                default:
                    return stringValueToSqlString(dataValue, queryCondition);
            }

        }
        private string stringValueToSqlString(object dataValue, MB.Util.DataFilterConditions queryCondition) {
            if (queryCondition == MB.Util.DataFilterConditions.Special)
                return dataValue.ToString();
           
            //有必要在这里增加一些处理SQL特殊情况的代码，避免SQL漏洞而产生错误
            if (queryCondition == MB.Util.DataFilterConditions.Include || queryCondition == MB.Util.DataFilterConditions.NotInclude)
                return string.Format("'%{0}%'", formatString(dataValue.ToString()));
            else if (queryCondition == MB.Util.DataFilterConditions.In || queryCondition == MB.Util.DataFilterConditions.NotIn) {
                string inStr = BuildQueryInSql<string>(dataValue.ToString().Split(','));
                return "(" + inStr + ")";
            }
            else if (queryCondition == MB.Util.DataFilterConditions.BenginsWith)
            {
                return string.Format("'{0}%'", formatString(dataValue.ToString()));
            }
            else if (queryCondition == MB.Util.DataFilterConditions.EndsWith)
            {
                return string.Format("'%{0}'", formatString(dataValue.ToString()));
            }
            else
                return string.Format("'{0}'", formatString(dataValue.ToString()));
        }

        private string formatString(string value)
        {
            return value.ToString().Trim().Replace("'", "''");
        }
        #endregion 内部函数处理...
    }
}
