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
using System.Collections.Specialized;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;

using MB.Orm.Enums;
using MB.Orm.Mapping;
using MB.Orm.Exceptions;
namespace MB.Orm.DbSql
{

    /// <summary>
    /// 数据对象SQL 自动生成基类。
    /// </summary>
    public abstract class BaseSqlGenerator
    {


        private MB.Orm.Enums.DatabaseType _DatabaseType;


        private string _ParameterPrefix;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public BaseSqlGenerator() {
            //默认的数据库类型为Oracle 数据库.
            _DatabaseType = MB.Orm.Persistence.DatabaseConfigurationScope.CreateDatabaseType();

            _ParameterPrefix = MB.Orm.DbSql.SqlShareHelper.SQL_XML_CFG_PARAM_PREFIX;
        }
        #region protected 成员...
        /// <summary>
        /// 数据库操作参数前缀。
        /// </summary>
        protected string ParameterPrefix {
            get {
                return _ParameterPrefix;
            }
        }
        #endregion protected 成员...


        /// <summary>
        /// 设置对应需要操作的数据库类型。
        /// 可以从应用程序的Config 文件中获取。
        /// </summary>
        /// <param name="databaseType"></param>
        public void SetDatabaseType(MB.Orm.Enums.DatabaseType databaseType) {
            _DatabaseType = databaseType;
            _ParameterPrefix = MB.Orm.DbSql.SqlShareHelper.SQL_XML_CFG_PARAM_PREFIX;// MB.Orm.Common.DbShare.Instance.GetPramPrefixByDatabaseType(databaseType);
        }

        /// <summary>
        /// 获取配置的SQL 字符窜信息。
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="OperationType"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public virtual SqlString[] GenerateSql(Type entityType, OperationType operationType, string[] properties) {
            string cacheKey = this.GenerateCacheKey(entityType, operationType, properties);
            if (CacheProxy.ContainsSql(cacheKey)) {
                return CacheProxy.GetCachedSql(cacheKey);
            }

            SqlString[] ss = null;
            switch (operationType) {
                case (OperationType.Insert):
                    ss = GenerateInsertSql(entityType, properties);
                    break;
                case (OperationType.Delete):
                    ss = GenerateDeleteSql(entityType);
                    break;
                case (OperationType.Update):
                    ss = GenerateUpdateSql(entityType, properties);
                    break;
                case OperationType.Select:
                    ss = GenerateSimpleSelectSql(entityType, properties);
                    break;
                default:
                    throw new MB.Util.APPException("在自动根据配置信息获取对象的操作SQL 语句时,操作类型:" + operationType.ToString() + " 还没有进行定义！");
            }
            return ss;
        }

        #region 基类需要实现的方法...
        /// <summary>
        /// GenerateSelectSql
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public virtual SqlString[] GenerateSimpleSelectSql(Type entityType, string[] properties) {
            throw new SubClassMustOverrideException("GenerateSelectSql");
        }
        /// <summary>
        /// GenerateFindByKeySql
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public virtual SqlString[] GenerateFindByKeySql(Type entityType, string[] properties) {
            throw new SubClassMustOverrideException("SubClassMustOverrideException");
        }
        /// <summary>
        /// GenerateInsertSql
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public virtual SqlString[] GenerateInsertSql(Type entityType, string[] properties) {
            throw new SubClassMustOverrideException("GenerateInsertSql");
        }
        /// <summary>
        /// GenerateUpdateSql
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public virtual SqlString[] GenerateUpdateSql(Type entityType, string[] properties) {
            throw new SubClassMustOverrideException("GenerateUpdateSql");
        }
        /// <summary>
        /// GenerateDeleteSql
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public virtual SqlString[] GenerateDeleteSql(Type entityType) {
            throw new SubClassMustOverrideException("GenerateDeleteSql");
        }

        #endregion 基类需要实现的方法...
        // 
        /// <summary>
        /// 产生实体SQL 语句缓存需要的键值。
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="OperationType"></param>
        /// <returns></returns>
        public string GenerateCacheKey(Type entityType, OperationType operationType) {
            return GenerateCacheKey(entityType, operationType, null);
        }
        /// <summary>
        /// 产生实体SQL 语句缓存需要的键值。
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="operationType"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public string GenerateCacheKey(Type entityType, OperationType operationType, string[] properties) {
            if (properties == null || properties.Length == 0)
                return string.Format("{0}_{1}", entityType.FullName, operationType.ToString());
            else
                return string.Format("{0}_{1}_{2}", entityType.FullName, operationType.ToString(), string.Join("-", properties));
        }
        /// <summary>
        /// 获取自增列的ID 值。
        /// </summary>
        /// <param name="mappingInfo"></param>
        /// <returns></returns>
        public int GetAutoIncreaseID(ModelMappingInfo mappingInfo) {
            throw new MB.Util.APPException("需要配置数据库自增列的处理方式！");
        }
        /// <summary>
        /// 转换值为拼接SQL 需要的语句。
        /// </summary>
        /// <param name="sVal"></param>
        /// <returns></returns>
        public string ToSqlVal(string val) {
            string sVal = val;
            if (string.IsNullOrEmpty(sVal)) {
                if (_DatabaseType == MB.Orm.Enums.DatabaseType.Oracle)
                    return "''";
                else
                    return "NULL";
            }
            sVal = sVal.Replace("'", "''");
            return "'" + sVal + "'";
        }
    }





}
