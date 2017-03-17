using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace MB.Orm.EntitySetCache {
    /// <summary>
    /// 缓存加载器
    /// </summary>
    public abstract class AbstractCacheLoader : ICacheLoader, ICacheItemRefreshAction {

        #region ICacheLoader Members

        public abstract System.Collections.IList LoadCache();

        public System.Collections.IList LoadIncrementalCache() {
            throw new NotImplementedException();
        }

        #endregion

        #region ICacheItemRefreshAction Members

        public void Refresh(string removedKey, object expiredValue, CacheItemRemovedReason removalReason) {
            
        }

        #endregion
    }
}
