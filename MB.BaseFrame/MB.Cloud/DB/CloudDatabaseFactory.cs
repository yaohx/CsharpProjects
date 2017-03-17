using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Orm.DB;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MB.Util;
using MB.Cloud.DB;
using MB.Cloud.Configuration;

namespace MB.Cloud.DB
{
    /// <summary>
    /// 云计算平台多库动态路由数据库创建默认处理工厂.
    /// </summary>
    public class CloudDatabaseFactory : IDatabaseFactory
    {
        //云计算数据库连接字符窜的模板
        private static  CloudConfigInfo _CloudConfigInfo;
        //[ThreadStatic]
        private static Yoyosys.ConnectionManager _ConnectionManager;
        //云数据库管理配置信息
        private static string CLOUD_DB_CFG = "CloudDbManagerCfg";

        private static object _Lock = new object();
        /// <summary>
        /// static 静态构造函数
        /// </summary>
        static CloudDatabaseFactory() {
            try {
                iniConnectionManager();

                
            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write("加载云计算配置信息由误:" + ex.Message, APPMessageType.SysErrInfo);
            }
           
        }
        /// <summary>
        /// 关闭连接池
        /// </summary>
        public static void CloseCloudConnectionManager()
        {
            try
            {
                if (_ConnectionManager != null)
                {
                    _ConnectionManager.Dispose();
                }
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write("关闭连接池出错" + ex.Message);
            }
        }
        #region Create
        static void iniConnectionManager(){
            _CloudConfigInfo = CloudConfigManager.GetConfigInfo();
                if (_CloudConfigInfo == null)
                    throw new MB.Util.APPException("MBCloudConfig 还没有进行配置,请先配置", APPMessageType.SysErrInfo);

                var cfg = _CloudConfigInfo.CloudDbManager;
                if (_ConnectionManager == null) {
                    lock (_Lock) {
                        if (_ConnectionManager == null) {
                           _ConnectionManager = new Yoyosys.ConnectionManager(cfg.Agent, cfg.DatabaseService,cfg.MonitorService, cfg.GroupService, cfg.GroupName);
                            System.Threading.Thread.Sleep(2000);
                        }
                    }
                }
        }
        #endregion
        /// <summary>
        /// 从动态路由表中创建默认连接的数据(为可写的核心数据库)
        /// </summary>
        /// <returns></returns>
        public Microsoft.Practices.EnterpriseLibrary.Data.Database CreateDatabase() {
            bool readOnly = OperationDatabaseContext.Current != null && OperationDatabaseContext.Current.ReadOnly ;
            var info = getCloudDatabaseSettingInfo(readOnly);
            return DatabaseFactoryExtender.CreateDatabase(info.Provider, info.ConnectionString);
        }
        /// <summary>
        /// 从动态路由表中创建数据库 
        /// </summary>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public Microsoft.Practices.EnterpriseLibrary.Data.Database CreateDatabase(bool readOnly) {
            var info = getCloudDatabaseSettingInfo(readOnly);
            return DatabaseFactoryExtender.CreateDatabase(info.Provider, info.ConnectionString);
        }

        /// <summary>
        /// CreateDatabase 该方法目前未启用.
        /// </summary>
        /// <param name="appCode"></param>
        /// <param name="dbName"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public Database CreateDatabase(string appCode, string dbName, bool readOnly) {
            throw new MB.Util.APPException("该方法为云计算拆库使用的方法,目前暂不使用", APPMessageType.SysErrInfo);
        }
        /// <summary>
        /// CreateDatabase 根据配置的数据库操作上下文创建连接数据库.
        /// </summary>
        /// <param name="databaseContext"></param>
        /// <returns></returns>
        public Database CreateDatabase(OperationDatabaseContext databaseContext) {
            if (databaseContext.CreateFromLocalAppConfiguration) {
                if (string.IsNullOrEmpty(databaseContext.DbName))
                    return DatabaseFactory.CreateDatabase();
                else
                    return DatabaseFactory.CreateDatabase(databaseContext.DbName);
            }
            else {
                return CreateDatabase(databaseContext.ReadOnly);
            }
        }
        /// <summary>
        /// 根据配置的名称从本地配置文件中创建数据库连接.
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

        #region 内部处理函数...
        //根据传入的参数从动态路由表中获取配置的数据库信息.
        private DynamicDatabaseSettingInfo getCloudDatabaseSettingInfo(bool readOnly) {
            try {
                //edit by chendc 2011-12-06 确保在同一个上线文种返回同样的数据库连接
                if (OperationDatabaseContext.Current != null && OperationDatabaseContext.Current.DatabaseSettingInfo != null) {
                    MB.Util.TraceEx.Write("从相同上下文种获取");
                    return OperationDatabaseContext.Current.DatabaseSettingInfo;
                }

                if (_ConnectionManager == null)
                    iniConnectionManager();

                MB.Util.TraceEx.Write(string.Format("从动态路由表中获取 {0},{1}", _CloudConfigInfo.CloudDbCode, readOnly));
                var cfgInfo = _ConnectionManager.getConnectionConfig(_CloudConfigInfo.CloudDbCode, readOnly);

                var cn = _CloudConfigInfo.CloudDBTemplates.FirstOrDefault(o => o.Provider == cfgInfo.provider);
                if (cn == null)
                    throw new MB.Util.APPException(string.Format("请检查Provider {0} 的数据库连接字符窜模板是否存在",cfgInfo.provider), APPMessageType.SysErrInfo);

                var cnStr = string.Format(cn.ConnectionTemplate, cfgInfo.ip, cfgInfo.port, cfgInfo.database);
                var setting = new DynamicDatabaseSettingInfo(cfgInfo.provider, cnStr);
                if (readOnly && OperationDatabaseContext.Current != null)
                    OperationDatabaseContext.Current.DatabaseSettingInfo = setting;

                return setting;
            }
            catch (Exception ex) {
                throw new MB.Util.APPException("请检查云计算平台数据库连接配置项 CloudDbManager " + ex.Message, APPMessageType.SysErrInfo);
            }
        }
        #endregion
    }

 
}
    