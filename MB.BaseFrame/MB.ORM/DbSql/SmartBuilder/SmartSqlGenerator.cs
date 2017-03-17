//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-08-02
// Description	:	 
// Modify date	:			By:					Why: 
//----------------------------------10------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MB.Orm.DbSql;
using MB.Orm.Enums;
using MB.Orm.Persistence;

namespace MB.Orm.DbSql.SmartBuilder
{
    /// <summary>
    /// IFluentSqlGenerator
    /// </summary>
    internal interface ISmartSqlGenerator
    {
        SqlString Create(Database db);
    }
    /// <summary>
    /// BaseFluentSqlGenerator
    /// </summary>
    internal abstract class BaseSmartSqlGenerator : ISmartSqlGenerator
    {
        private BuilderData _data;
        public BaseSmartSqlGenerator(BuilderData data) {
            _data = data;
        }
        protected BuilderData BuilderData {
            get {
                return _data;
            }
        }

        public virtual SqlString Create(Database db) {
            throw new NotImplementedException();
        }
    }
    internal class SmartDeleteSqlGenerator : BaseSmartSqlGenerator
    {
        public SmartDeleteSqlGenerator(BuilderData data)
            : base(data) {
        }
        public override SqlString Create(Database db) {
            var parameterPrefix = PersistenceManagerHelper.GetSqlParamsPrefix(db);
            var whereSql = string.Empty;
            foreach (var column in BuilderData.Columns) {
                if (whereSql.Length > 0)
                    whereSql += " and ";

                whereSql += string.Format("{0} = {1}{2}",
                                                column.ColumnName,
                                                parameterPrefix,
                                                column.ParameterName);
            }
            var sql = string.Format("delete from {0} where {1}", BuilderData.EntityMapping.MapTable, whereSql);

            SqlString sqlInfo = new SqlString(sql, BuilderData.Parameters);
            return sqlInfo;
        }
    }
    internal class SmartInsertSqlGenerator : BaseSmartSqlGenerator
    {
        public SmartInsertSqlGenerator(BuilderData data)
            : base(data) {
        }
        public override SqlString Create(Database db) {
            var parameterPrefix = PersistenceManagerHelper.GetSqlParamsPrefix(db);
            var insertSql = string.Empty;
            var valuesSql = string.Empty;
            foreach (var column in BuilderData.Columns) {
                if (insertSql.Length > 0) {
                    insertSql += ",";
                    valuesSql += ",";
                }

                insertSql += column.ColumnName;
                valuesSql += parameterPrefix + column.ParameterName;
            }

            var sql = string.Format("insert into {0}({1}) values({2})",
                                        BuilderData.EntityMapping.MapTable,
                                        insertSql,
                                        valuesSql);
            SqlString sqlInfo = new SqlString(sql, BuilderData.Parameters);
            return sqlInfo;
        }
    }
    internal class SmartUpdateSqlGenerator : BaseSmartSqlGenerator
    {
        public SmartUpdateSqlGenerator(BuilderData data)
            : base(data) {
        }
        public override SqlString Create(Database db) {
            var parameterPrefix = PersistenceManagerHelper.GetSqlParamsPrefix(db);
            var setSql = string.Empty;
            foreach (var column in BuilderData.Columns) {
                if (setSql.Length > 0)
                    setSql += ", ";

                setSql += string.Format("{0} = {1}{2}",
                                    column.ColumnName,
                                    parameterPrefix,
                                    column.ParameterName);
            }

            var whereSql = string.Empty;
            foreach (var column in BuilderData.Where) {
                if (whereSql.Length > 0)
                    whereSql += " and ";

                whereSql += string.Format("{0} = {1}{2}",
                                    column.ColumnName,
                                    parameterPrefix,
                                    column.ParameterName);
            }

            var sql = string.Format("update {0} set {1} where {2}",
                                        BuilderData.EntityMapping.MapTable,
                                        setSql,
                                        whereSql);

            SqlString sqlInfo = new SqlString(sql, BuilderData.Parameters);
            return sqlInfo;
        }
    }
    internal class SmartQuerySqlGenerator : BaseSmartSqlGenerator
    {
        public SmartQuerySqlGenerator(BuilderData data)
            : base(data) {
        }
        public override SqlString Create(Database db) {
            var parameterPrefix = PersistenceManagerHelper.GetSqlParamsPrefix(db);
            var querySql = string.Empty;
            foreach (var column in BuilderData.Columns) {
                if (querySql.Length > 0)
                    querySql += ", ";

                querySql += column.ColumnName;
            }

            var whereSql = string.Empty;
            foreach (var column in BuilderData.Where) {
                if (whereSql.Length > 0)
                    whereSql += " and ";

                whereSql += string.Format("{0} = {1}{2}",
                                    column.ColumnName,
                                    parameterPrefix,
                                    column.ParameterName);
            }
            var sql = string.Format("select {0} from {1} where {2}",
                                        querySql,
                                        BuilderData.EntityMapping.MapTable,
                                        whereSql);

            SqlString sqlInfo = new SqlString(sql, BuilderData.Parameters);
            return sqlInfo;
        }
    }
    internal class SmartExistsSqlGenerator : BaseSmartSqlGenerator
    {
        public SmartExistsSqlGenerator(BuilderData data)
            : base(data) {
        }
        public override SqlString Create(Database db) {
            var parameterPrefix = PersistenceManagerHelper.GetSqlParamsPrefix(db);
            var querySql = string.Empty;

            var whereSql = string.Empty;
            foreach (var column in BuilderData.Where) {
                if (whereSql.Length > 0)
                    whereSql += " and ";

                whereSql += string.Format("{0} = {1}{2}",
                                    column.ColumnName,
                                    parameterPrefix,
                                    column.ParameterName);
            }

            var databaseType = MB.Orm.Persistence.DatabaseConfigurationScope.GetDatabaseType(db);
            if (databaseType == DatabaseType.MSSQLServer)
                querySql = "top 1 1";
            else if (databaseType == DatabaseType.Oracle) {
                querySql = "1";
                if (whereSql.Length > 0)
                    whereSql += " and ";
                whereSql += " rownum <=1";
            }
            else
                throw new MB.Util.APPException(string.Format("在进行SmartDAL Exists 时目前还不支持 {0}", databaseType.ToString()), Util.APPMessageType.SysErrInfo);


            var sql = string.Format("select {0} from {1} where {2}",
                                        querySql,
                                        BuilderData.EntityMapping.MapTable,
                                        whereSql);

            SqlString sqlInfo = new SqlString(sql, BuilderData.Parameters);
            return sqlInfo;
        }
    }
}
