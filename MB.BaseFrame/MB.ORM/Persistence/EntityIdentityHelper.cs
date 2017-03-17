//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-02
// Description	:	。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.EnterpriseServices;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MB.Orm.DbSql;
using MB.Orm.Mapping;
using MB.Orm.Mapping.Att;
namespace MB.Orm.Persistence {
    /// <summary>
    /// EntityIdentityHelper： 统一处理实体自增加列解决方案。
    /// 
    /// </summary>
    public sealed class EntityIdentityHelper {
        ///// <summary>
        ///// 多线程安全的单实例模式。
        ///// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        ///// </summary>
        public static EntityIdentityHelper NewInstance {
            get {
                return new EntityIdentityHelper();
            }
        }
        private static readonly string SYS_IDENTITY_TABLE_NAME = "SYS_OBJECT_IDENTITY";
        private static readonly string PARAM_TABLE_NAME = "TableName"; //对应的表名
        private static readonly string PARAM_IDENTITY_SEED = "IdentitySeed"; //标识种子
        private static readonly string PARAM_IDENTITY_INCREMENT = "IdentityIncrement";//标识增量
        private static readonly string PARAM_CURRENT_VALUE = "CurrentValue"; //当前值
        //private static readonly object IDENTITY_CREATE_SYN_LOCK = new object();

        /// <summary>
        /// 设置实体的键值。
        /// </summary>
        /// <param name="entity"></param>
        public void FillEntityIdentity(object entity) {
            Type entityType = entity.GetType(); 
            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);
            if (string.IsNullOrEmpty(mappingInfo.MapTable))
                throw new MB.Util.APPException(string.Format("数据实体{0} 配置有误，没有指定映射到的表。", entityType.FullName), MB.Util.APPMessageType.SysErrInfo);

            if (mappingInfo.PrimaryKeys == null || mappingInfo.PrimaryKeys.Count != 1)
                throw new MB.Util.APPException(string.Format("数据实体{0} 配置有误，没有指定主键或者不是单一主键配置。", entityType.FullName), MB.Util.APPMessageType.SysErrInfo);

            FieldPropertyInfo keyInfo = mappingInfo.PrimaryKeys.Values.First<FieldPropertyInfo>();
            //判断是否存在主键
            bool exists = MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, keyInfo.FieldName);

            if (!exists) return;
            object oldKey = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, keyInfo.FieldName);
            //判断是否已经存在键值。
            if (oldKey != null && (int)oldKey > 0)
                return;
            
            int id = GetEntityIdentity(mappingInfo.MapTable);

            MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, keyInfo.FieldName, id);
        }
        /// <summary>
        /// 获取实体对象的自增列值。
        /// 备注：业务系统需要提供存储过程的方式来实现以便性能上得到提高。
        /// 把int 类型作为自增加列类型，基于以下理论： Int32 的最大值为：2147483647 ，每天都对该表产生一百万个请求的话可以使用 6年（一年360天）。
        /// 6年的数据 基本上都需要整理，对一般表来说，每天都有一百万个请求基本上不可能的。
        /// 对于有可能每天都产生一百万条记录的表，可以考虑使用Int64 类型，只要针对该表单独进行处理就可以。（方法如：Oracel 的话用 create sequence ,sql server 用自带的自增列）
        /// 在Oracle 中需要创建 FU_GET_NEXT_IDENTITY SQL 函数
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int GetEntityIdentity(string tableName) {
            return GetEntityIdentity(tableName, 1);
        }
        /// <summary>
        /// 获取实体对象的自增列值。
        /// 备注：业务系统需要提供存储过程的方式来实现以便性能上得到提高。
        /// 把int 类型作为自增加列类型，基于以下理论： Int32 的最大值为：2147483647 ，每天都对该表产生一百万个请求的话可以使用 6年（一年360天）。
        /// 6年的数据 基本上都需要整理，对一般表来说，每天都有一百万个请求基本上不可能的。
        /// 对于有可能每天都产生一百万条记录的表，可以考虑使用Int64 类型，只要针对该表单独进行处理就可以。（方法如：Oracel 的话用 create sequence ,sql server 用自带的自增列）
        /// 在Oracle 中需要创建 FU_GET_NEXT_IDENTITY SQL 函数
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int GetEntityIdentity(string tableName,int count) {
            string connName = System.Configuration.ConfigurationManager.AppSettings["NewIDCoreDBConnStr"];
            Database db;
            if (string.IsNullOrEmpty(connName))
                db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            else
                db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase(connName);

            var databaseType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);

            if(databaseType == MB.Orm.Enums.DatabaseType.MSSQLServer ||
                databaseType == Enums.DatabaseType.Sqlite || databaseType == Enums.DatabaseType.MySql) {
                return createEntityIdentity(db,tableName, count);
            }
            //对于Oracle 需要通过Oracle 函数来进行实现。
            //为了减少对公共表的锁定时间, 这里每次都要创建一个新事务，完成了就释放。
            string qrySql = "SELECT FU_GET_NEXT_IDENTITY(:pTABLE_NAME,:pCOUNT) FROM DUAL";
            DbCommand cmdSelect = db.GetSqlStringCommand(qrySql);
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.RequiresNew)) {
                try {
                    db.AddInParameter(cmdSelect, "pTABLE_NAME", DbType.AnsiString, tableName);
                    db.AddInParameter(cmdSelect, "pCOUNT", DbType.Int32, count);
                    object val = db.ExecuteScalar(cmdSelect);

                    //如果有异常抛出 该方法不会执行，那么将自动执行回滚
                    scope.Complete();
                  
                    return MB.Util.MyConvert.Instance.ToInt(val);
                }
                catch (Exception ex) {
                    throw new MB.Util.APPException("执行 GetEntityIdentity 出错！", MB.Util.APPMessageType.SysDatabaseInfo, ex);
                }
                finally {
                    try {
                        cmdSelect.Dispose();
                    }
                    catch { }
                }
           }
        }
        //针对sql server 获取自增列
        private int createEntityIdentity(Database db, string tableName, int count) {

            int lastID = 1;
            //为了减少对公共表的锁定时间, 这里每次都要创建一个新事务，完成了就释放。
            // using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.RequiresNew)) {
            //考虑到这里是在独立的事务中执行的，加上如果应用在sql server 中有些时候可能存在不支持DTC的情况，这里就不使用  TransactionScope

            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Suppress)) {

                using (DbConnection connection = db.CreateConnection()) {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try {
                        var databaseType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
                        string parPrefix = databaseType == MB.Orm.Enums.DatabaseType.Oracle ? SqlShareHelper.ORACLE_PARAM_PREFIX : SqlShareHelper.SQL_SERVER_PARAM_PREFIX;

                        string qrySql = "SELECT CURRENT_VALUE FROM SYS_OBJECT_IDENTITY WHERE TABLE_NAME=:TableName".Replace(":", parPrefix);
                        DbCommand cmdSelect = db.GetSqlStringCommand(qrySql);
                        db.AddInParameter(cmdSelect, parPrefix + PARAM_TABLE_NAME, DbType.String, tableName);

                        string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmdSelect);
                        MB.Util.TraceEx.Write("正在执行GetEntityIdentity 中的 Select:" + cmdMsg);

                        object val = null;
                        val = db.ExecuteScalar(cmdSelect, transaction);
                        //using (new Util.MethodTraceWithTime(true, cmdMsg)) {

                        //}

                        cmdSelect.Dispose();

                        if (val == null || val == System.DBNull.Value) {
                            string insert = "INSERT INTO SYS_OBJECT_IDENTITY(TABLE_NAME,CURRENT_VALUE) VALUES(:TableName,:CurrentValue)".Replace(":", parPrefix);
                            DbCommand cmdInsert = db.GetSqlStringCommand(insert);
                            db.AddInParameter(cmdInsert, parPrefix + PARAM_TABLE_NAME, DbType.String, tableName);
                            db.AddInParameter(cmdInsert, parPrefix + PARAM_CURRENT_VALUE, DbType.String, lastID + count - 1);
                            cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmdInsert);
                            MB.Util.TraceEx.Write("正在执行GetEntityIdentity 中的 Insert:" + cmdMsg);
                            db.ExecuteNonQuery(cmdInsert, transaction);
                            //using (new Util.MethodTraceWithTime(true, cmdMsg)) {

                            //}

                            cmdInsert.Dispose();
                        }
                        else {
                            lastID = MB.Util.MyConvert.Instance.ToInt(val) + 1;

                            string update = "UPDATE SYS_OBJECT_IDENTITY SET CURRENT_VALUE=:CurrentValue WHERE TABLE_NAME=:TableName".Replace(":", parPrefix);
                            DbCommand cmdUpdate = db.GetSqlStringCommand(update);
                            db.AddInParameter(cmdUpdate, parPrefix + PARAM_TABLE_NAME, DbType.String, tableName);
                            db.AddInParameter(cmdUpdate, parPrefix + PARAM_CURRENT_VALUE, DbType.String, lastID + count - 1);
                            cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmdUpdate);
                            MB.Util.TraceEx.Write("正在执行GetEntityIdentity 总的 Update:" + cmdMsg);
                            db.ExecuteNonQuery(cmdUpdate, transaction);
                            //using (new Util.MethodTraceWithTime(true, cmdMsg)) {

                            //}

                            cmdUpdate.Dispose();
                        }

                        //如果有异常抛出 该方法不会执行，那么将自动执行回滚
                        // scope.Complete();
                        transaction.Commit();
                    }
                    catch (Exception ex) {
                        transaction.Rollback();
                        throw new MB.Util.APPException("执行 GetEntityIdentity 出错！", MB.Util.APPMessageType.SysDatabaseInfo, ex);
                    }
                    finally {
                        try {
                            transaction.Dispose();
                            if (connection.State == System.Data.ConnectionState.Open)
                                connection.Close();
                        }
                        catch { }

                    }
                }
                scope.Complete();
            }


            //using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Suppress)) {


            //    scope.Complete();
            //}
            //  }
            return lastID;
        }
        
    }
}
