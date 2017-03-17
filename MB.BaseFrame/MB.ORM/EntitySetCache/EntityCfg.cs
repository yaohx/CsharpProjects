using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.EntitySetCache {
    /// <summary>
    /// 缓存加载配置
    /// </summary>
    public class EntityCfg {

        private string _Name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _RuleType;
        public string RuleType {
            get { return _RuleType; }
            set { _RuleType = value; }
        }


        private string _CustomLoaderType;
        /// <summary>
        /// 自动以缓存加载类型
        /// 如果同时配置了RuleType，以CustomLoaderType优先
        /// </summary>
        public string CustomLoaderType {
            get { return _CustomLoaderType; }
            set { _CustomLoaderType = value; }
        }

        private Microsoft.Practices.EnterpriseLibrary.Caching.CacheItemPriority _CacheItemPriority;
        /// <summary>
        /// 缓存项的优先级 None = 0, Low = 1, Normal = 2, High = 3, NotRemovable = 4,
        /// 默认值是Normal
        /// </summary>
        public Microsoft.Practices.EnterpriseLibrary.Caching.CacheItemPriority CacheItemPriority {
            get { return _CacheItemPriority; }
            set { _CacheItemPriority = value; }
        }

        private List<CacheExpirationCfg> _Expirations;
        /// <summary>
        /// 过期策略
        /// </summary>
        public List<CacheExpirationCfg> Expirations {
            get { return _Expirations; }
            set { _Expirations = value; }
        }

        private bool _IsCacheItemRefreshed;
        /// <summary>
        /// 过期以后是否刷新
        /// </summary>
        public bool IsCacheItemRefreshed {
            get { return _IsCacheItemRefreshed; }
            set { _IsCacheItemRefreshed = value; }
        }

    }

    /// <summary>
    /// 过期策略配置
    /// </summary>
    public class CacheExpirationCfg {
        private CacheExpirationType _ExpirationType;
        
        public CacheExpirationType ExpirationType {
            get { return _ExpirationType; }
            set { _ExpirationType = value; }
        }

        private string _FilePath;
        /// <summary>
        /// 如果是FileDependency的策略，需要提供FilePath
        /// </summary>
        public string FilePath {
            get { return _FilePath; }
            set { _FilePath = value; }
        }

        private int _ExpireTime;
        /// <summary>
        /// 如果是SlidingTime，AbsoluteTime过期策略，需要提供过期时间
        /// </summary>
        public int ExpireTime {
            get { return _ExpireTime; }
            set { _ExpireTime = value; }
        }

        


    }

    /// <summary>
    /// 过期策略枚举
    /// </summary>
    public enum CacheExpirationType {
        /// <summary>
        /// 从不
        /// </summary>
        Never = 0,
        /// <summary>
        /// 根据文件
        /// </summary>
        FileDependency = 1,
        /// <summary>
        /// 相对时间
        /// </summary>
        SlidingTime = 2,
        /// <summary>
        /// 绝对时间
        /// </summary>
        AbsoluteTime = 3,
    }
}
