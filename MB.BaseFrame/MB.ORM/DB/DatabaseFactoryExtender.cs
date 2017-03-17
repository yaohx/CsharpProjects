using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MB.Util;

namespace MB.Orm.DB
{
    /// <summary>
    /// Database Factory 扩展方法.
    /// 为实现数据库动态路由表而增加
    /// </summary>
    public static  class DatabaseFactoryExtender
    {
        private static readonly string DEFAULT_DB_NAME = "DBConnectionString";

        /// <summary>
        /// 根据指定的provider 和 connectionString 创建对应的链接数据库，不从本地配置文件中创建.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static Database CreateDatabase(string provider,string connectionString) {
            if (string.IsNullOrEmpty(provider))
                throw new MB.Util.APPException("创建Database 时 provider 不能为空,请检查", Util.APPMessageType.SysErrInfo);

            if (string.IsNullOrEmpty(connectionString))
                throw new MB.Util.APPException("创建Database 时 connectionString 不能为空,请检查", Util.APPMessageType.SysErrInfo);

            try {
                var databaseSettings = new Microsoft.Practices.EnterpriseLibrary.Data.Configuration.DatabaseSettings();
                var connStringSection = new System.Configuration.ConnectionStringsSection();
                var dictDataSource = new Microsoft.Practices.EnterpriseLibrary.Common.Configuration.DictionaryConfigurationSource();
                //针对SqLite provider 的特殊配置
                var dbProvider = new Microsoft.Practices.EnterpriseLibrary.Data.Configuration.DbProviderMapping("System.Data.SQLite", "EntLibContrib.Data.SQLite.SQLiteDatabase, EntLibContrib.Data.SqLite");
                connStringSection.ConnectionStrings.Add(new System.Configuration.ConnectionStringSettings(DEFAULT_DB_NAME, connectionString, provider));
                databaseSettings.ProviderMappings.Add(dbProvider);
                databaseSettings.DefaultDatabase = DEFAULT_DB_NAME;
                dictDataSource.Add(Microsoft.Practices.EnterpriseLibrary.Data.Configuration.DatabaseSettings.SectionName, databaseSettings);
                dictDataSource.Add("connectionStrings", connStringSection);
                var dbFactory = new DatabaseProviderFactory(dictDataSource);
                var database = dbFactory.Create(DEFAULT_DB_NAME);
                return database;
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(string.Format("创建数据库连接出错,请检查Provider {0}, ConnectionString {1} 配置是否正确:" + ex.Message,provider, connectionString), APPMessageType.SysErrInfo);
            }
        }
    }
}
