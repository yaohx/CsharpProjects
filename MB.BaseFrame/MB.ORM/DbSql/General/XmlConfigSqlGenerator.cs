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

namespace MB.Orm.DbSql
{
    /// <summary>
    /// 获取配置的XML文件处理相关。
    /// </summary>
    public class XmlConfigSqlGenerator : BaseSqlGenerator
    {

        #region 覆盖基类的方法...
        /// <summary>
        /// GenerateSelectSql
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public override SqlString[] GenerateSimpleSelectSql(Type entityType, string[] properties) {
            return getSqlString(entityType, OperationType.Select);
        }
        /// <summary>
        /// GenerateFindByKeySql
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public override SqlString[] GenerateFindByKeySql(Type entityType, string[] properties) {
            return getSqlString(entityType, OperationType.SelectByKey);
        }
        /// <summary>
        /// GenerateInsertSql
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public override SqlString[] GenerateInsertSql(Type entityType, string[] properties) {
            return getSqlString(entityType, OperationType.Insert);
        }
        /// <summary>
        /// GenerateUpdateSql
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public override SqlString[] GenerateUpdateSql(Type entityType, string[] properties) {
            return getSqlString(entityType, OperationType.Update);
        }
        /// <summary>
        /// GenerateDeleteSql
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public override SqlString[] GenerateDeleteSql(Type entityType) {
            return getSqlString(entityType, OperationType.Delete);
        }

        #endregion 覆盖基类的方法...

        #region 内部函数处理...
        //获取对应的SQL 语句
        private SqlString[] getSqlString(Type entityType, OperationType operationType) {
            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);
            string xmlFileName = mappingInfo.XmlConfigFileName;
            string sqlName = getSqlName(operationType);
            MB.Orm.Mapping.Xml.SqlConfigHelper xmlConfig = MB.Orm.Mapping.Xml.SqlConfigHelper.Instance;

            SqlString[] sqlStrs = xmlConfig.GetSqlString(xmlFileName, sqlName);
            string cacheKey = GenerateCacheKey(entityType, operationType);

            CacheProxy.CacheSql(cacheKey, sqlStrs);
            return sqlStrs;
        }
        //根据操作类型获取对应的名称。
        private string getSqlName(OperationType operationType) {
            switch (operationType) {
                case OperationType.Select:
                    return MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_SELECT_OBJECT;
                case OperationType.SelectByKey:
                    return MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_SELECT_BY_KEY;
                case OperationType.Insert:
                    return MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_ADD_OBJECT;
                case OperationType.Update:
                    return MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_UPDATE_OBJECT;
                case OperationType.Delete:
                    return MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_DELETE_OBJECT;
                case OperationType.DeleteNotIn:
                    return MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_DELETE_NOT_IN_IDS;
                default:
                    throw new MB.Util.APPException("操作类型:" + operationType.ToString() + " 还没有配置相应SQL 操作名称！");
            }
        }
        #endregion 内部函数处理...

    }
}
