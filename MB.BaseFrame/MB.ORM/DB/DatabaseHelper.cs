//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-03-13
// Description	:	包含常用的工厂方法创建一个数据库。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MB.Util;
using MB.Orm.DB;
using MB.Orm.Enums;
namespace MB.Orm.Persistence {
    /// <summary>
    /// 包含常用的工厂方法创建一个数据库。
    /// </summary>
    public class DatabaseHelper {
        private static string[] MY_SQL_DB_PROVIDER = new string[] { "MySql.Data.MySqlClient.MySqlClientFactory" };
        private static string CONNECTION_NODES_PATH = "/configuration/connectionStrings/add";
        private static string CLOUD_DATABASE_FACTORY_CFG = "CloudDatabaseFactory";

        private static IDatabaseFactory _DatabaseFactory;

        /// <summary>
        /// 
        /// </summary>
        static DatabaseHelper() {
            //
            if (_DatabaseFactory == null) {
                try {
                    string cfg = System.Configuration.ConfigurationManager.AppSettings[CLOUD_DATABASE_FACTORY_CFG];
                    if (string.IsNullOrEmpty(cfg))
                        _DatabaseFactory = new DefaultDatabaseFactory();
                    else {
                        string[] cfgs = cfg.Split(',');
                        _DatabaseFactory =  Activator.CreateInstance(cfgs[1], cfgs[0]).Unwrap() as IDatabaseFactory;
                         
                    }
                }
                catch (Exception ex) {
                    MB.Util.TraceEx.Write("创建DatabaseFactory有误:" + ex.Message, APPMessageType.SysErrInfo);
                }
            }
           
        }

        /// <summary>
        /// 反射调用创建一个数据库对象,.
        /// 如果调用的不是默认配置的数据库，需要调用 using(MB.Orm.Persistence.DatabaseConfigurationScope)
        /// 来进行处理。
        /// </summary>
        /// <returns></returns>
        public static Database CreateDatabase() {

            InitDBProcessMonitor();

            return _DatabaseFactory.CreateDatabase();
        }
        /// <summary>
        /// 根据配置的名称创建数据库对象,如果指定的数据库配置节点名称，那么DatabaseConfigurationScope对它不起作用。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Database CreateDatabase(string name) {
            InitDBProcessMonitor();
            return _DatabaseFactory.CreateDatabase(name);
        }
        public static Database CreateDatabase(OperationDatabaseContext databaseContext) {
            InitDBProcessMonitor();
            return _DatabaseFactory.CreateDatabase(databaseContext);
        }

        /// <summary>
        /// 对于数据操作性能指标的监视对象的创建
        /// 再每次创建数据库连接的时候新增对象DBProcessMonitorInfo，到LIST中
        /// </summary>
        private static void InitDBProcessMonitor()
        {
            if (MB.Util.Monitors.WcfPerformaceMonitorContext.Current != null &&
                MB.Util.Monitors.WcfPerformaceMonitorContext.Current.CurrentWcfProcessMonitorInfo != null)
            {
                //由于MB.Util.Monitors.WcfPerformaceMonitorContext.Current是线程变量，所以线程安全
                MB.Util.Monitors.WcfPerformaceMonitorContext.Current.CurrentWcfProcessMonitorInfo.DBRequestCount++;
                MB.Util.Monitors.WcfPerformaceMonitorContext.Current.CurrentWcfProcessMonitorInfo.DBProcessMonitorInfos.Add(new Util.Monitors.DBProcessMonitorInfo());
            }
        }

        /// <summary>
        /// 创建数据库配置对应的数据类型。
        /// </summary>
        /// <returns></returns>
        public static MB.Orm.Enums.DatabaseType CreateDatabaseType() {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db = DatabaseHelper.CreateDatabase();
            return GetDatabaseType(db);
        }
        /// <summary>
        /// 根据database 获取对应的数据库类型。
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static MB.Orm.Enums.DatabaseType GetDatabaseType(Database db) {
            if (db is Microsoft.Practices.EnterpriseLibrary.Data.Oracle.OracleDatabase) {
                return DatabaseType.Oracle;
            }
            else if (db is Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase) {
                return DatabaseType.MSSQLServer;
            }
            else if (db is Microsoft.Practices.EnterpriseLibrary.Data.GenericDatabase) {
                string dbProviderName = db.DbProviderFactory.ToString();
                //目前先根据mysql 公司提供的Provider Name 来判断，如果有其他链接mysql 的provider ,需要再增加
                if (Array.IndexOf<string>(MY_SQL_DB_PROVIDER, dbProviderName) >= 0)
                    return DatabaseType.MySql;
                else
                    return DatabaseType.Oracle; //以后需要修改根据ProviderName 来判断是否为Oracle.
            }
            else {
                if (System.Text.RegularExpressions.Regex.IsMatch(db.ConnectionString, ".db3", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    return DatabaseType.Sqlite;
                else
                    throw new MB.Util.APPException(string.Format("当前数据库类型{0} 目前还不支持,先要实现对应的类", db.ToString()), MB.Util.APPMessageType.SysDatabaseInfo);
            }
        }

        #region 设置数据库连接字符窜处理相关...
        /// <summary>
        /// 设置当前数据库连接字符窜。
        /// </summary>
        /// <param name="connectionString"></param>
        public static void SaveConnectionString(string connectionString) {
            string dbcfgName = getDatabaseCfgName();
            setKeyValue(dbcfgName, connectionString);
        }
        /// <summary>
        /// 获取当前配置的数据库连接字符窜。
        /// </summary>
        public static string GetConnectionString() {
            string dbcfgName = getDatabaseCfgName();
            return getKeyValue(dbcfgName);
        }
        //设置当前数据库连接字符窜
        private static void setKeyValue(string keyName, string keyValue) {
            string nodePath = CONNECTION_NODES_PATH;
          
            XmlDocument xmlDoc;
            xmlDoc = new XmlDocument();
            string pathName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            try {
                xmlDoc.Load(pathName);
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(string.Format("加载配置文件{0}出错", pathName) + ex.Message, APPMessageType.SysErrInfo);
            }

            XmlNodeList nodes = xmlDoc.SelectNodes(nodePath);
            bool findKey = false;
            foreach (XmlNode node in nodes) {
                if (string.Compare(node.Attributes["name"].Value, keyName, true) == 0) {
                    node.Attributes["connectionString"].Value = keyValue;
                    findKey = true;
                    break;
                }
            }
            if (!findKey) {
                MB.Util.TraceEx.Write("没有配置键值，请以这样的方式(add name=XX providerName=XX connectionString=XX)在config文件中配置。", APPMessageType.SysErrInfo);
            }
            xmlDoc.Save(pathName);
        }
        //获取当前数据库连接字符窜
        private static string getKeyValue(string keyName) {
            string nodePath = CONNECTION_NODES_PATH;
            XmlDocument xmlDoc;
            xmlDoc = new XmlDocument();
            string pathName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            try {
                xmlDoc.Load(pathName);
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(string.Format("加载配置文件{0}出错", pathName) + ex.Message, APPMessageType.SysErrInfo);
            }

            XmlNodeList nodes = xmlDoc.SelectNodes(nodePath);
            foreach (XmlNode node in nodes) {
                if (string.Compare(node.Attributes["name"].Value, keyName, true) == 0) {
                   return  node.Attributes["connectionString"].Value;

                }
            }
            return string.Empty;
        }
        
        //获取当前配置的数据库节点名称
        private static string getDatabaseCfgName() {
            if (OperationDatabaseContext.Current != null && !string.IsNullOrEmpty(OperationDatabaseContext.Current.DbName)) {
                return OperationDatabaseContext.Current.DbName;
            }
            string nodePath = "/configuration/dataConfiguration";
        
            XmlDocument xmlDoc;
            xmlDoc = new XmlDocument();
            string pathName =  AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
         
            xmlDoc.Load(pathName);
            XmlNode node = xmlDoc.SelectSingleNode(nodePath);
             if (node != null)
                return node.Attributes["defaultDatabase"].Value;
            else
                throw new MB.Util.APPException(string.Format("请检查配置文件{0} 是否已经配置dataConfiguration/defaultDatabase", pathName), APPMessageType.SysErrInfo); 
        }
        #endregion 设置数据库连接字符窜处理相关...
    }
}
