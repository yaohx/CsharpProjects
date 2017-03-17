using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using System.Collections;

namespace MB.Orm.EntitySetCache {

    /// <summary>
    /// 刷新CacheItem
    /// </summary>
    [Serializable]
    public class CacheItemRefreshAction : ICacheItemRefreshAction {
        private EntityCfg _EntityCfg;
        private string _Key;

        public CacheItemRefreshAction(EntityCfg entityCfg, string key) {
            _EntityCfg = entityCfg;
            _Key = key;
        }

        #region ICacheItemRefreshAction Members

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="removedKey"></param>
        /// <param name="expiredValue"></param>
        /// <param name="removalReason"></param>
        public void Refresh(string removedKey, object expiredValue, CacheItemRemovedReason removalReason) {
            if (removedKey.CompareTo(_Key) == 0) {
                if (removalReason != CacheItemRemovedReason.Removed && _EntityCfg.IsCacheItemRefreshed) {
                    CacheData cacheValue = expiredValue as CacheData;
                    CacheContainer.Current.LoadCache(_EntityCfg);
                }

            }

        }

        #endregion
    }
}
