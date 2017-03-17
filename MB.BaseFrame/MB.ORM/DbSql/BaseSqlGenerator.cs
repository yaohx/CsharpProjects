////---------------------------------------------------------------- 
//// Copyright (C) 2008-2009 www.metersbonwe.com
//// All rights reserved. 
//// Author		:	chendc
//// Create date	:	2009-01-08
//// Description	:	数据对象SQL 自动生成基类。
//// Modify date	:			By:					Why: 
////----------------------------------------------------------------
//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Text;
//using System.Reflection;
//using System.Text.RegularExpressions;

//using MB.Orm.Enums;
//using MB.Orm.Mapping;
//using MB.Orm.Exceptions;
//namespace MB.Orm.DbSql {
//    /// <summary>
//    /// 数据对象SQL 自动生成基类。
//    /// </summary>
//    public abstract class BaseSqlGenerator {
//        private static BaseXmlSqlConfig _XmlSqlConfigObject;
//        private static BaseAutoSqlGenerator _AutoSqlConfigObject;

//        private MB.Orm.Enums.DatabaseType _DatabaseType;


//        private string _ParameterPrefix;
//        /// <summary>
//        /// 构造函数。
//        /// </summary>
//        public BaseSqlGenerator() {
//            //默认的数据库类型为Oracle 数据库.
            
//            _DatabaseType = MB.Orm.Persistence.DatabaseHelper.CreateDatabaseType();

//            _ParameterPrefix = MB.Orm.DbSql.SqlShareHelper.SQL_XML_CFG_PARAM_PREFIX;// MB.Orm.Common.DbShare.Instance.GetPramPrefixByDatabaseType(_DatabaseType);
//        }
//        #region protected 成员...
//        /// <summary>
//        /// 数据库操作参数前缀。
//        /// </summary>
//        protected string ParameterPrefix {
//            get {
//                return _ParameterPrefix;
//            }
//        }
//        #endregion protected 成员...

//        /// <summary>
//        /// 获取生成SQL 的操作对象。
//        /// </summary>
//        /// <param name="cfgOptions"></param>
//        /// <returns></returns>
//        public static BaseSqlGenerator GetSqlGeneratorObject(ModelConfigOptions cfgOptions, string[] properties) {
//            BaseSqlGenerator sqlGenerator = null;
//            if ((cfgOptions & ModelConfigOptions.CreateSqlByXmlCfg) != 0) {
//                if(_XmlSqlConfigObject==null)
//                    _XmlSqlConfigObject = new BaseXmlSqlConfig();
//                sqlGenerator = _XmlSqlConfigObject;
//            }
//            else {
//                if (_AutoSqlConfigObject == null)
//                    _AutoSqlConfigObject = new BaseAutoSqlGenerator();
//                sqlGenerator = _AutoSqlConfigObject;
//            }
//            //如果存在个性化的属性，那么只能通过自动生成SQL 的方式。
//            if (properties != null && properties.Length > 0) {
//                if (_AutoSqlConfigObject == null)
//                    _AutoSqlConfigObject = new BaseAutoSqlGenerator();

//                return _AutoSqlConfigObject;
//            }
//            else
//                return sqlGenerator;
//        }
//        /// <summary>
//        /// 设置对应需要操作的数据库类型。
//        /// 可以从应用程序的Config 文件中获取。
//        /// </summary>
//        /// <param name="databaseType"></param>
//        public void SetDatabaseType(MB.Orm.Enums.DatabaseType databaseType) {
//            _DatabaseType = databaseType;
//            _ParameterPrefix = MB.Orm.DbSql.SqlShareHelper.SQL_XML_CFG_PARAM_PREFIX;// MB.Orm.Common.DbShare.Instance.GetPramPrefixByDatabaseType(databaseType);
//        }

//        /// <summary>
//        /// 获取配置的SQL 字符窜信息。
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <param name="OperationType"></param>
//        /// <param name="properties"></param>
//        /// <returns></returns>
//        public virtual SqlString[] GenerateSql(Type entityType, OperationType operationType, string[] properties) {
//            string cacheKey = entityType.FullName + "_" + operationType.ToString();
//            if (CacheProxy.ContainsSql(cacheKey)) {
//                return CacheProxy.GetCachedSql(cacheKey);
//            }

//            SqlString[] ss = null;
//            switch (operationType) {
//                case (OperationType.Insert):
//                    ss = GenerateInsertSql(entityType, properties);
//                    break;
//                case (OperationType.Delete):
//                    ss = GenerateDeleteSql(entityType);
//                    break;
//                case (OperationType.Update):
//                    ss = GenerateUpdateSql(entityType, properties);
//                    break;
//                case OperationType.Select:
//                    ss = GenerateSimpleSelectSql(entityType, properties);
//                    break;
//                default:
//                    throw new MB.Util.APPException("在自动根据配置信息获取对象的操作SQL 语句时,操作类型:" + operationType.ToString() + " 还没有进行定义！");
//            }
//            return ss;
//        }

//        #region 基类需要实现的方法...
//        /// <summary>
//        /// GenerateSelectSql
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <param name="properties"></param>
//        /// <returns></returns>
//        public virtual SqlString[] GenerateSimpleSelectSql(Type entityType, string[] properties) {
//            throw new SubClassMustOverrideException("GenerateSelectSql");
//        }
//        /// <summary>
//        /// GenerateFindByKeySql
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <param name="properties"></param>
//        /// <returns></returns>
//        public virtual SqlString[] GenerateFindByKeySql(Type entityType, string[] properties) {
//            throw new SubClassMustOverrideException("SubClassMustOverrideException");
//        }
//        /// <summary>
//        /// GenerateInsertSql
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <param name="properties"></param>
//        /// <returns></returns>
//        public virtual SqlString[] GenerateInsertSql(Type entityType, string[] properties) {
//            throw new SubClassMustOverrideException("GenerateInsertSql");
//        }
//        /// <summary>
//        /// GenerateUpdateSql
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <param name="properties"></param>
//        /// <returns></returns>
//        public virtual SqlString[] GenerateUpdateSql(Type entityType, string[] properties) {
//            throw new SubClassMustOverrideException("GenerateUpdateSql");
//        }
//        /// <summary>
//        /// GenerateDeleteSql
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <returns></returns>
//        public virtual SqlString[] GenerateDeleteSql(Type entityType) {
//            throw new SubClassMustOverrideException("GenerateDeleteSql");
//        }

//        #endregion 基类需要实现的方法...

//        /// <summary>
//        /// 产生实体SQL 语句缓存需要的键值。
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <param name="OperationType"></param>
//        /// <returns></returns>
//        public string GenerateCacheKey(Type entityType, OperationType operationType) {
//            return entityType.FullName + "_" + operationType.ToString();
//        }
//        /// <summary>
//        /// 获取自增列的ID 值。
//        /// </summary>
//        /// <param name="mappingInfo"></param>
//        /// <returns></returns>
//        public int GetAutoIncreaseID(ModelMappingInfo mappingInfo) {
//            throw new MB.Util.APPException("需要配置数据库自增列的处理方式！");
//        }
//        /// <summary>
//        /// 转换值为拼接SQL 需要的语句。
//        /// </summary>
//        /// <param name="sVal"></param>
//        /// <returns></returns>
//        public string ToSqlVal(string val) {
//            string sVal = val;
//            if (string.IsNullOrEmpty(sVal)) {
//                if (_DatabaseType == MB.Orm.Enums.DatabaseType.Oracle)
//                    return "''";
//                else
//                    return "NULL";
//            }
//            sVal = sVal.Replace("'", "''");
//            return "'" + sVal + "'";
//        }
//    }
   
//    #region BaseAutoSqlGenerator(根据对象属性配置自动生成SQL 语句处理相关)...
//    /// <summary>
//    /// 通过配置属性自动生成SQL语句。
//    /// </summary>
//    public class BaseAutoSqlGenerator : BaseSqlGenerator {
//        public BaseAutoSqlGenerator() {
         
//        }
//        #region GenerateSelectSql...
//        /// <summary>
//        /// 根据配置的属性信息生成选择的Select 语句。
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <param name="properties"></param>
//        /// <returns></returns>
//        public override SqlString[] GenerateSimpleSelectSql(Type entityType, string[] properties) {
//            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);
//            string tableName = mappingInfo.MapTable;
//            List<string> columns = prepareSelectSql(mappingInfo, properties);
            
//            string sql = new StringBuilder("SELECT ").Append(string.Join(",", columns.ToArray())).Append(" FROM ").Append(tableName).ToString();

//            SqlString ss = new SqlString(sql, null);
//            string cacheKey = GenerateCacheKey(entityType, OperationType.Select);
//            SqlString[] asql = new SqlString[] { ss };
//            CacheProxy.CacheSql(cacheKey, asql);
//            return asql;
//        }

//        #endregion GenerateSelectSql...

//        #region GenerateFindByKeySql...
//        /// <summary>
//        /// 通过键值自动获取某一条记录的SQL 语句。
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <param name="properties"></param>
//        /// <returns></returns>
//        public override SqlString[] GenerateFindByKeySql(Type entityType, string[] properties) {
//            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);
//            string tableName = mappingInfo.MapTable;
//            List<string> whereString = new List<string>();
//            List<SqlParamInfo> sqlParams = new List<SqlParamInfo>();
//            List<string> selectString = prepareUpdateSql(mappingInfo, properties, ref whereString, ref sqlParams);
//            SqlString ss = new SqlString(new StringBuilder("SELECT ").Append(string.Join(",", selectString.ToArray()))
//                .Append(" FROM ").Append(tableName).Append(" WHERE ").Append(string.Join(" AND ", whereString.ToArray())).ToString(), sqlParams);

//            string cacheKey = GenerateCacheKey(entityType, OperationType.SelectByKey);
//            SqlString[] asql = new SqlString[] { ss };
//            CacheProxy.CacheSql(cacheKey, asql);
//            return asql;
//        }
//        #endregion GenerateFindByKeySql...

//        #region GenerateInsertSql...
//        /// <summary>
//        /// 根据配置的信息自动产生Insert 的SQL 语句。
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <param name="properties"></param>
//        /// <returns></returns>
//        public override SqlString[] GenerateInsertSql(Type entityType, string[] properties) {
//            SqlString sqlStr = null;
//            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);

//            string tableName = mappingInfo.MapTable;

//            List<string> parameters = new List<string>();
//            List<SqlParamInfo> sqlParams = new List<SqlParamInfo>();
//            List<string> fields = prepareInsertSql(mappingInfo, properties, ref parameters, ref sqlParams);

//            StringBuilder strSqlBuilder = new StringBuilder("INSERT INTO ").
//                Append(tableName).Append("(").Append(string.Join(",", fields.ToArray())).
//                Append(") VALUES(").Append(string.Join(",", parameters.ToArray())).Append(") ");

//            sqlStr = new SqlString(strSqlBuilder.ToString(), sqlParams);

//            string cacheKey = GenerateCacheKey(entityType, OperationType.Insert);
//            SqlString[] asql = new SqlString[] { sqlStr };
//            CacheProxy.CacheSql(cacheKey, asql);
//            return asql;
//        }
//        #endregion GenerateInsertSql...

//        #region GenerateUpdateSql...
//        /// <summary>
//        /// 根据对象的配置信息自动生成Update SQL 语句。
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <param name="properties"></param>
//        /// <returns></returns>
//        public override SqlString[] GenerateUpdateSql(Type entityType, string[] properties) {
//            SqlString sqlStr = null;
//            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);

//            string tableName = mappingInfo.MapTable;
//            List<string> whereString = new List<string>();
//            List<SqlParamInfo> sqlParams = new List<SqlParamInfo>();
//            List<string> setString = prepareUpdateSql(mappingInfo,properties, ref whereString, ref sqlParams);

//            StringBuilder strSqlBuilder = new StringBuilder();
//            strSqlBuilder.Append(" UPDATE ").Append(tableName).Append(" SET ").Append(string.Join(",", setString.ToArray()))
//                      .Append(" WHERE ").Append( string.Join(" AND ",whereString.ToArray())).Append(" ;");

//            sqlStr = new SqlString(strSqlBuilder.ToString(), sqlParams);

//            string cacheKey = GenerateCacheKey(entityType, OperationType.Update);
//            SqlString[] asql = new SqlString[] { sqlStr };
//            CacheProxy.CacheSql(cacheKey, asql);
//            return asql;
//        }
//        #endregion GenerateUpdateSql...

//        #region GenerateDeleteSql...
//        /// <summary>
//        /// 根据对象配置信息获取自动生成的Delete SQL 语句。
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <returns></returns>
//        public override SqlString[] GenerateDeleteSql(Type entityType) {
//            SqlString sqlStr = null;
//            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);

//            string tableName = mappingInfo.MapTable;
//            List<SqlParamInfo> sqlParams = new List<SqlParamInfo>();
//            List<string> whereString = prepareDeleteSql(mappingInfo, ref sqlParams);

//            StringBuilder strSqlBuilder = new StringBuilder();
//            strSqlBuilder.Append(" DELETE FROM ").Append(tableName).Append(" WHERE ").Append(string.Join(" AND ", whereString.ToArray()));

//            sqlStr = new SqlString(strSqlBuilder.ToString(), sqlParams);

//            string cacheKey = GenerateCacheKey(entityType, OperationType.Delete);
//            SqlString[] asql = new SqlString[] { sqlStr };
//            CacheProxy.CacheSql(cacheKey, asql);
//            return asql;
//        }
//        #endregion GenerateDeleteSql...

//        #region 内部函数处理...
//        //准备生成SQL Insert  语句处理。
//        private List<string> prepareInsertSql(ModelMappingInfo mappingInfo, string[] properties, ref List<string> parameters, ref List<SqlParamInfo> sqlParams) {
//            parameters.Clear();
//            sqlParams.Clear();

//            List<string> fields = new List<string>();
//            bool existsPros = properties!=null && properties.Length >0;
//            foreach (FieldPropertyInfo info in mappingInfo.FieldPropertys.Values) {
//                if (existsPros && Array.IndexOf<string>(properties, info.FieldName) < 0) {
//                    continue;
//                }
//                fields.Add(info.FieldName);
//                parameters.Add(ParameterPrefix + info.FieldName);
//                sqlParams.Add(new SqlParamInfo(info.FieldName, MB.Orm.Common.DbShare.Instance.SystemTypeNameToDbType(info.DeclaringType.FullName)));
//            }
//            return fields;
//        }
//        //准备生成SQL Update 语句处理。
//        private List<String> prepareUpdateSql(ModelMappingInfo mappingInfo, string[] properties, ref List<string> whereString, ref List<SqlParamInfo> sqlParams) {
//            List<string> setString = new List<string>();
//            whereString.Clear();
//            sqlParams.Clear();

//            bool existsPros = properties != null && properties.Length > 0;
//            foreach (FieldPropertyInfo info in mappingInfo.FieldPropertys.Values) {
//                if (mappingInfo.PrimaryKeys.ContainsKey(info.FieldName)) {
//                    whereString.Add(info.FieldName + "=" + ParameterPrefix + info.FieldName);
//                }
//                else {
//                    if (!existsPros || Array.IndexOf<string>(properties, info.FieldName) >=0) {
//                        setString.Add(info.FieldName + "=" + ParameterPrefix + info.FieldName);
//                    }
//                }
//                if (mappingInfo.PrimaryKeys.ContainsKey(info.FieldName) || (!existsPros || Array.IndexOf<string>(properties, info.FieldName) >=0)) {
//                    sqlParams.Add(new SqlParamInfo(info.FieldName, MB.Orm.Common.DbShare.Instance.SystemTypeNameToDbType(info.DeclaringType.FullName)));
//                }
//            }
//            return setString;
//        }
//        //准备生成SQL Delete 语句处理。
//        private List<string> prepareDeleteSql(ModelMappingInfo mappingInfo, ref List<SqlParamInfo> sqlParams) {
//            List<string> whereString = new List<string>();
//            sqlParams.Clear();

//            foreach (FieldPropertyInfo info in mappingInfo.FieldPropertys.Values) {

//                if (mappingInfo.PrimaryKeys.ContainsKey(info.FieldName)) {
//                    whereString.Add(info.FieldName + "=" + ParameterPrefix + info.FieldName);

//                    sqlParams.Add(new SqlParamInfo(info.FieldName, MB.Orm.Common.DbShare.Instance.SystemTypeNameToDbType(info.DeclaringType.FullName)));
//                }
           
//            }
//            return whereString;
//        }
//        //选择
//        private List<string> prepareSelectSql(ModelMappingInfo mappingInfo, string[] properties) {
//            List<string> selectString = new List<string>();
//             bool existsPros = properties != null && properties.Length > 0;
//             foreach (FieldPropertyInfo info in mappingInfo.FieldPropertys.Values) {
//                 if (existsPros && Array.IndexOf<string>(properties, info.FieldName) < 0) {
//                     continue;
//                 }
//                 selectString.Add(info.FieldName);

//             }
//             return selectString;
//        }
//        private List<String> prepareFindByKeySql(ModelMappingInfo mappingInfo, string[] properties, ref List<string> whereString, ref List<SqlParamInfo> sqlParams) {
//            List<string> selectString = new List<string>();
//            whereString.Clear();
//            sqlParams.Clear();

//            bool existsPros = properties != null && properties.Length > 0;
//            foreach (FieldPropertyInfo info in mappingInfo.FieldPropertys.Values) {
//                if (mappingInfo.PrimaryKeys.ContainsKey(info.FieldName)) {
//                    whereString.Add(info.FieldName + "=" + ParameterPrefix + info.FieldName);
//                    sqlParams.Add(new SqlParamInfo(info.FieldName, MB.Orm.Common.DbShare.Instance.SystemTypeNameToDbType(info.DeclaringType.FullName)));
//                    continue;
//                }
//                if (existsPros && Array.IndexOf<string>(properties, info.FieldName) < 0) {
//                    continue;
//                }
//                selectString.Add(info.FieldName);
               
//            }
//            return selectString;
//        }
//        #endregion 内部函数处理...

//    }

//    #endregion BaseAutoSqlGenerator(根据对象属性配置自动生成SQL 语句处理相关)...
//    /// <summary>
//    /// 获取配置的XML文件处理相关。
//    /// </summary>
//    public class BaseXmlSqlConfig : BaseSqlGenerator {

//        #region 覆盖基类的方法...
//        /// <summary>
//        /// GenerateSelectSql
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <param name="properties"></param>
//        /// <returns></returns>
//        public override SqlString[] GenerateSimpleSelectSql(Type entityType, string[] properties) {
//            return getSqlString(entityType, OperationType.Select);
//        }
//        /// <summary>
//        /// GenerateFindByKeySql
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <param name="properties"></param>
//        /// <returns></returns>
//        public override SqlString[] GenerateFindByKeySql(Type entityType, string[] properties) {
//            return getSqlString(entityType, OperationType.SelectByKey);
//        }
//        /// <summary>
//        /// GenerateInsertSql
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <param name="properties"></param>
//        /// <returns></returns>
//        public override SqlString[] GenerateInsertSql(Type entityType, string[] properties) {
//            return getSqlString(entityType, OperationType.Insert);
//        }
//        /// <summary>
//        /// GenerateUpdateSql
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <param name="properties"></param>
//        /// <returns></returns>
//        public override SqlString[] GenerateUpdateSql(Type entityType, string[] properties) {
//            return getSqlString(entityType, OperationType.Update);
//        }
//        /// <summary>
//        /// GenerateDeleteSql
//        /// </summary>
//        /// <param name="entityType"></param>
//        /// <returns></returns>
//        public override SqlString[] GenerateDeleteSql(Type entityType) {
//            return getSqlString(entityType, OperationType.Delete);
//        }

//        #endregion 覆盖基类的方法...

//        #region 内部函数处理...
//        //获取对应的SQL 语句
//        private SqlString[] getSqlString(Type entityType, OperationType operationType) {
//            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);
//            string xmlFileName = mappingInfo.XmlConfigFileName;
//            string sqlName = getSqlName(operationType);
//            MB.Orm.Mapping.Xml.SqlConfigHelper xmlConfig = MB.Orm.Mapping.Xml.SqlConfigHelper.Instance;

//            SqlString[] sqlStrs = xmlConfig.GetSqlString(xmlFileName, sqlName);
//            string cacheKey = GenerateCacheKey(entityType, operationType);
            
//            CacheProxy.CacheSql(cacheKey, sqlStrs);
//            return sqlStrs;
//        }
//        //根据操作类型获取对应的名称。
//        private string getSqlName(OperationType operationType) {
//            switch (operationType) {
//                case OperationType.Select:
//                    return MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_SELECT_OBJECT;
//                case OperationType.SelectByKey:
//                    return MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_SELECT_BY_KEY;
//                case OperationType.Insert:
//                    return MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_ADD_OBJECT;
//                case OperationType.Update:
//                    return MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_UPDATE_OBJECT;
//                case OperationType.Delete:
//                    return MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_DELETE_OBJECT;
//                case OperationType.DeleteNotIn:
//                    return MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_DELETE_NOT_IN_IDS;
//                default:
//                    throw new MB.Util.APPException("操作类型:" + operationType.ToString() + " 还没有配置相应SQL 操作名称！");
//            }
//        }
//        #endregion 内部函数处理...

//    }


//}
