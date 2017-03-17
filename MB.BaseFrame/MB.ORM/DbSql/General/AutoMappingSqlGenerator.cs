//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-08-02
// Description	:	 
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Orm.Enums;
using MB.Orm.Mapping;
using MB.Orm.Common;

namespace MB.Orm.DbSql
{
    #region BaseAutoSqlGenerator(根据对象属性配置自动生成SQL 语句处理相关)...
    /// <summary>
    /// 通过配置属性自动生成SQL语句。
    /// </summary>
    public class AutoMappingSqlGenerator : BaseSqlGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoMappingSqlGenerator() {

        }

        #region GenerateSelectSql...
        /// <summary>
        /// 根据配置的属性信息生成选择的Select 语句。
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public override SqlString[] GenerateSimpleSelectSql(Type entityType, string[] properties) {
            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);
            string tableName = mappingInfo.MapTable;
            List<string> columns = prepareSelectSql(mappingInfo, properties);

            string sql = new StringBuilder("SELECT ").Append(string.Join(",", columns.ToArray())).Append(" FROM ").Append(tableName).ToString();

            SqlString ss = new SqlString(sql, null);
            string cacheKey = GenerateCacheKey(entityType, OperationType.Select, properties);
            SqlString[] asql = new SqlString[] { ss };
            CacheProxy.CacheSql(cacheKey, asql);
            return asql;
        }

        #endregion GenerateSelectSql...

        #region GenerateFindByKeySql...

        /// <summary>
        /// 通过键值自动获取某一条记录的SQL 语句。
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public override SqlString[] GenerateFindByKeySql(Type entityType, string[] properties) {
            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);
            string tableName = mappingInfo.MapTable;
            List<string> whereString = new List<string>();
            List<SqlParamInfo> sqlParams = new List<SqlParamInfo>();
            List<string> selectString = prepareUpdateSql(mappingInfo, properties, ref whereString, ref sqlParams);
            SqlString ss = new SqlString(new StringBuilder("SELECT ").Append(string.Join(",", selectString.ToArray()))
                .Append(" FROM ").Append(tableName).Append(" WHERE ").Append(string.Join(" AND ", whereString.ToArray())).ToString(), sqlParams);

            string cacheKey = GenerateCacheKey(entityType, OperationType.SelectByKey, properties);
            SqlString[] asql = new SqlString[] { ss };
            CacheProxy.CacheSql(cacheKey, asql);
            return asql;
        }
        #endregion GenerateFindByKeySql...

        #region GenerateInsertSql...
        /// <summary>
        /// 根据配置的信息自动产生Insert 的SQL 语句。
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public override SqlString[] GenerateInsertSql(Type entityType, string[] properties) {
            SqlString sqlStr = null;
            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);

            string tableName = mappingInfo.MapTable;

            List<string> parameters = new List<string>();
            List<SqlParamInfo> sqlParams = new List<SqlParamInfo>();
            List<string> fields = prepareInsertSql(mappingInfo, properties, ref parameters, ref sqlParams);

            StringBuilder strSqlBuilder = new StringBuilder("INSERT INTO ").
                Append(tableName).Append("(").Append(string.Join(",", fields.ToArray())).
                Append(") VALUES(").Append(string.Join(",", parameters.ToArray())).Append(") ");

            sqlStr = new SqlString(strSqlBuilder.ToString(), sqlParams);

            string cacheKey = GenerateCacheKey(entityType, OperationType.Insert);
            SqlString[] asql = new SqlString[] { sqlStr };
            CacheProxy.CacheSql(cacheKey, asql);
            return asql;
        }
        #endregion GenerateInsertSql...

        #region GenerateUpdateSql...
        /// <summary>
        /// 根据对象的配置信息自动生成Update SQL 语句。
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public override SqlString[] GenerateUpdateSql(Type entityType, string[] properties) {
            SqlString sqlStr = null;
            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);

            string tableName = mappingInfo.MapTable;
            List<string> whereString = new List<string>();
            List<SqlParamInfo> sqlParams = new List<SqlParamInfo>();
            List<string> setString = prepareUpdateSql(mappingInfo, properties, ref whereString, ref sqlParams);

            StringBuilder strSqlBuilder = new StringBuilder();
            strSqlBuilder.Append(" UPDATE ").Append(tableName).Append(" SET ").Append(string.Join(",", setString.ToArray()))
                      .Append(" WHERE ").Append(string.Join(" AND ", whereString.ToArray())).Append(" ;");

            sqlStr = new SqlString(strSqlBuilder.ToString(), sqlParams);

            string cacheKey = GenerateCacheKey(entityType, OperationType.Update);
            SqlString[] asql = new SqlString[] { sqlStr };
            CacheProxy.CacheSql(cacheKey, asql);
            return asql;
        }
        #endregion GenerateUpdateSql...

        #region GenerateDeleteSql...
        /// <summary>
        /// 根据对象配置信息获取自动生成的Delete SQL 语句。
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public override SqlString[] GenerateDeleteSql(Type entityType) {
            SqlString sqlStr = null;
            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);

            string tableName = mappingInfo.MapTable;
            List<SqlParamInfo> sqlParams = new List<SqlParamInfo>();
            List<string> whereString = prepareDeleteSql(mappingInfo, ref sqlParams);

            StringBuilder strSqlBuilder = new StringBuilder();
            strSqlBuilder.Append(" DELETE FROM ").Append(tableName).Append(" WHERE ").Append(string.Join(" AND ", whereString.ToArray()));

            sqlStr = new SqlString(strSqlBuilder.ToString(), sqlParams);

            string cacheKey = GenerateCacheKey(entityType, OperationType.Delete);
            SqlString[] asql = new SqlString[] { sqlStr };
            CacheProxy.CacheSql(cacheKey, asql);
            return asql;
        }
        #endregion GenerateDeleteSql...

        #region 内部函数处理...
        //准备生成SQL Insert  语句处理。
        private List<string> prepareInsertSql(ModelMappingInfo mappingInfo, string[] properties, ref List<string> parameters, ref List<SqlParamInfo> sqlParams) {
            parameters.Clear();
            sqlParams.Clear();

            List<string> fields = new List<string>();
            bool existsPros = properties != null && properties.Length > 0;
            foreach (FieldPropertyInfo info in mappingInfo.FieldPropertys.Values) {
                if (existsPros && Array.IndexOf<string>(properties, info.FieldName) < 0) {
                    continue;
                }
                fields.Add(info.FieldName);
                parameters.Add(ParameterPrefix + info.FieldName);
                sqlParams.Add(new SqlParamInfo(info.FieldName, DbShare.Instance.SystemTypeNameToDbType(info.DeclaringType.FullName)));
            }
            return fields;
        }
        //准备生成SQL Update 语句处理。
        private List<String> prepareUpdateSql(ModelMappingInfo mappingInfo, string[] properties, ref List<string> whereString, ref List<SqlParamInfo> sqlParams) {
            List<string> setString = new List<string>();
            whereString.Clear();
            sqlParams.Clear();

            bool existsPros = properties != null && properties.Length > 0;
            foreach (FieldPropertyInfo info in mappingInfo.FieldPropertys.Values) {
                if (mappingInfo.PrimaryKeys.ContainsKey(info.FieldName)) {
                    whereString.Add(info.FieldName + "=" + ParameterPrefix + info.FieldName);
                }
                else {
                    if (!existsPros || Array.IndexOf<string>(properties, info.FieldName) >= 0) {
                        setString.Add(info.FieldName + "=" + ParameterPrefix + info.FieldName);
                    }
                }
                if (mappingInfo.PrimaryKeys.ContainsKey(info.FieldName) || (!existsPros || Array.IndexOf<string>(properties, info.FieldName) >= 0)) {
                    sqlParams.Add(new SqlParamInfo(info.FieldName, info.DbType));
                }
            }
            return setString;
        }
        //准备生成SQL Delete 语句处理。
        private List<string> prepareDeleteSql(ModelMappingInfo mappingInfo, ref List<SqlParamInfo> sqlParams) {
            List<string> whereString = new List<string>();
            sqlParams.Clear();

            foreach (FieldPropertyInfo info in mappingInfo.FieldPropertys.Values) {

                if (mappingInfo.PrimaryKeys.ContainsKey(info.FieldName)) {
                    whereString.Add(info.FieldName + "=" + ParameterPrefix + info.FieldName);

                    sqlParams.Add(new SqlParamInfo(info.FieldName, DbShare.Instance.SystemTypeNameToDbType(info.DeclaringType.FullName)));
                }

            }
            return whereString;
        }
        //选择
        private List<string> prepareSelectSql(ModelMappingInfo mappingInfo, string[] properties) {
            List<string> selectString = new List<string>();
            bool existsPros = properties != null && properties.Length > 0;
            foreach (FieldPropertyInfo info in mappingInfo.FieldPropertys.Values) {
                if (existsPros && Array.IndexOf<string>(properties, info.FieldName) < 0) {
                    continue;
                }
                selectString.Add(info.FieldName);

            }
            return selectString;
        }
        private List<String> prepareFindByKeySql(ModelMappingInfo mappingInfo, string[] properties, ref List<string> whereString, ref List<SqlParamInfo> sqlParams) {
            List<string> selectString = new List<string>();
            whereString.Clear();
            sqlParams.Clear();

            bool existsPros = properties != null && properties.Length > 0;
            foreach (FieldPropertyInfo info in mappingInfo.FieldPropertys.Values) {
                if (mappingInfo.PrimaryKeys.ContainsKey(info.FieldName)) {
                    whereString.Add(info.FieldName + "=" + ParameterPrefix + info.FieldName);
                    sqlParams.Add(new SqlParamInfo(info.FieldName, DbShare.Instance.SystemTypeNameToDbType(info.DeclaringType.FullName)));
                    continue;
                }
                if (existsPros && Array.IndexOf<string>(properties, info.FieldName) < 0) {
                    continue;
                }
                selectString.Add(info.FieldName);

            }
            return selectString;
        }
        #endregion 内部函数处理...

    }

    #endregion BaseAutoSqlGenerator(根据对象属性配置自动生成SQL 语句处理相关)...
}
