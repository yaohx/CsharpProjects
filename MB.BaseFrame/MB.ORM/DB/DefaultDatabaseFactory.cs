using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MB.Util;

namespace MB.Orm.DB
{
    /// <summary>
    /// DefaultDatabaseFactory
    /// </summary>
    public class DefaultDatabaseFactory : IDatabaseFactory
    {
        /// <summary>
        /// CreateDatabase
        /// </summary>
        /// <returns></returns>
        public Microsoft.Practices.EnterpriseLibrary.Data.Database CreateDatabase() {
            try {
                if (OperationDatabaseContext.Current == null)
                    return DatabaseFactory.CreateDatabase();

                if (string.IsNullOrEmpty(OperationDatabaseContext.Current.DbName))
                    return DatabaseFactory.CreateDatabase();
                else
                    return DatabaseFactory.CreateDatabase(OperationDatabaseContext.Current.DbName);

            }
            catch (Exception ex) {
                string name = (OperationDatabaseContext.Current == null || string.IsNullOrEmpty(OperationDatabaseContext.Current.DbName)) ?
                              "DefaultDatabase" : OperationDatabaseContext.Current.DbName;
                throw new MB.Util.APPException(string.Format("创建数据库对象出错,请检查配置的 {0} 数据库连接字符窜", name), MB.Util.APPMessageType.SysErrInfo, ex);
            }
        }
        /// <summary>
        /// CreateDatabase
        /// </summary>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public Microsoft.Practices.EnterpriseLibrary.Data.Database CreateDatabase(bool readOnly) {
            return DatabaseFactory.CreateDatabase();
        }
        /// <summary>
        /// CreateDatabase
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public Microsoft.Practices.EnterpriseLibrary.Data.Database CreateDatabase(string dbName) {
            try {
                return DatabaseFactory.CreateDatabase(dbName);
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(string.Format("创建数据库对象出错,请检查配置的 {0} 数据库连接字符窜", dbName), MB.Util.APPMessageType.SysErrInfo, ex);
            }
        }

        /// <summary>
        /// 本地配置文件中暂时不提供该方法的使用
        /// </summary>
        /// <param name="appCode"></param>
        /// <param name="dbName"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public Database CreateDatabase(string appCode, string dbName, bool readOnly) {
            throw new MB.Util.APPException("该方法为云计算拆库使用的方法,目前暂不使用", APPMessageType.SysErrInfo);
        }
        /// <summary>
        /// databaseContext
        /// </summary>
        /// <param name="databaseContext"></param>
        /// <returns></returns>
        public Database CreateDatabase(OperationDatabaseContext databaseContext) {
            return string.IsNullOrEmpty(databaseContext.DbName) ? DatabaseFactory.CreateDatabase() : DatabaseFactory.CreateDatabase(databaseContext.DbName);
        }
    }
}
