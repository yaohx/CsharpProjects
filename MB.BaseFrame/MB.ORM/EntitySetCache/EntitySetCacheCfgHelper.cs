using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace MB.Orm.EntitySetCache {
    /// <summary>
    /// 数据缓存配置的帮助类
    /// </summary>
    public class EntitySetCacheCfgHelper {

        private static object _Synobject = new object();
        private static EntitySetCacheCfgHelper _Instance;

        /// <summary>
        /// 缓存路径
        /// </summary>
        public static string CachePath {
            get {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
                baseDirectory = baseDirectory + @"\ConfigFile\";
                return baseDirectory;
            }
        }
        /// <summary>
        /// 缓存文件详细信息列表
        /// </summary>
        public static string CacheFileInfoName { get { return "EntitySetCacheCfg.xml"; } }

        

        public static EntitySetCacheCfgHelper Instance {
            get { return _Instance; }
        }

        /// <summary>
        /// 静态构造函数，构造单例
        /// </summary>
        static EntitySetCacheCfgHelper() {
            if (_Instance == null) {
                lock (_Synobject) {
                    if (_Instance == null)
                        _Instance = new EntitySetCacheCfgHelper();
                }
            }
        }


        /// <summary>
        /// 获取缓存加载器
        /// </summary>
        /// <returns></returns>
        public List<EntityCfg> GetEntitySetCfg() {

            string cacheConfig = CachePath + CacheFileInfoName;
            if (!File.Exists(cacheConfig))
                throw new MB.Util.APPException(string.Format("缓存需要的配置文件 {0} 不存在", cacheConfig), Util.APPMessageType.SysErrInfo);
            //获取需要载入的文件列表
            XElement root = XElement.Load(cacheConfig);
            var entitySet = from c in root.Elements("EntitySet").Elements()
                            select c;

            List<EntityCfg> cfgs = new List<EntityCfg>();
            foreach (var entity in entitySet) {
                EntityCfg cfg = new EntityCfg();
                string name = entity.Attribute("Name") == null ? string.Empty : entity.Attribute("Name").Value;
                string ruleType = entity.Attribute("RuleType") == null ? string.Empty : entity.Attribute("RuleType").Value;
                string customLoaderType = entity.Attribute("CustomLoaderType") == null ? string.Empty : entity.Attribute("CustomLoaderType").Value;
                string cacheItempriority = entity.Attribute("CacheItemPriority") == null ? "NotRemovable" : entity.Attribute("CacheItemPriority").Value;
                string isRefreshed = entity.Attribute("IsCacheItemRefreshed") == null ? "false" : entity.Attribute("IsCacheItemRefreshed").Value;
                cfg.Name = name;
                cfg.RuleType = ruleType;
                cfg.CustomLoaderType = customLoaderType;
                Microsoft.Practices.EnterpriseLibrary.Caching.CacheItemPriority priority;
                if (Enum.TryParse<Microsoft.Practices.EnterpriseLibrary.Caching.CacheItemPriority>(cacheItempriority, out priority))
                    cfg.CacheItemPriority = priority;
                else {
                    cfg.CacheItemPriority = Microsoft.Practices.EnterpriseLibrary.Caching.CacheItemPriority.NotRemovable;
                    MB.Util.TraceEx.Write(string.Format("Entity中的名称为({0})的CacheItemPriority的值{1}配置的不正确", name, cacheItempriority));
                }

                bool tempIsRefreshed;
                if (bool.TryParse(isRefreshed, out tempIsRefreshed))
                    cfg.IsCacheItemRefreshed = tempIsRefreshed;
                else
                    cfg.IsCacheItemRefreshed = false;

                List<CacheExpirationCfg> expireCfgs = new List<CacheExpirationCfg>();

                var expirations = from e in entity.Descendants("Expiration")
                                 select e;
                foreach (var expiration in expirations) {
                    CacheExpirationCfg expCfg = new CacheExpirationCfg();
                    string expirationType = expiration.Attribute("ExpirationType") == null ? string.Empty : expiration.Attribute("ExpirationType").Value;
                    string filePath = expiration.Attribute("FilePath") == null ? string.Empty : expiration.Attribute("FilePath").Value;
                    string expireTime = expiration.Attribute("ExpireTime") == null ? "0" : expiration.Attribute("ExpireTime").Value;
                    

                    CacheExpirationType expireType;
                    if (Enum.TryParse<CacheExpirationType>(expirationType, out expireType))
                        expCfg.ExpirationType = expireType;
                    else {
                        expCfg.ExpirationType = CacheExpirationType.Never;
                        MB.Util.TraceEx.Write(string.Format("Expiration中的名称为({0})的ExpirationType的值{1}配置的不正确", name, expirationType));
                    }
                    expCfg.FilePath = filePath;

                    int tempExpireTime;
                    if (int.TryParse(expireTime, out tempExpireTime))
                        expCfg.ExpireTime = tempExpireTime;
                    else
                        expCfg.ExpireTime = 0;

                    
                    expireCfgs.Add(expCfg);
                }

                cfg.Expirations = expireCfgs;
                cfgs.Add(cfg);
            }



            return cfgs;
        }
    }
}
