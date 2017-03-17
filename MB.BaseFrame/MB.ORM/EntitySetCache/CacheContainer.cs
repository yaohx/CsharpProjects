using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using System.Collections;
using MB.Util;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using System.Collections.Concurrent;

namespace MB.Orm.EntitySetCache {

    /// <summary>
    /// 缓存数据的容易，整个容器是一个Microsoft.Practices.EnterpriseLibrary.Caching的一个CacheManager
    /// 其中每一个项是一个CacheData,每一条数据是CacheData的一个项
    /// </summary>
    public class CacheContainer {


        private static object SynLock = new object();
        private static CacheContainer _CacheContainer;

        /// <summary>
        /// 当前缓存容器的实例
        /// </summary>
        public static CacheContainer Current {
            get {
                if (_CacheContainer == null) {
                    lock (SynLock) {
                        if (_CacheContainer == null) {
                            _CacheContainer = new CacheContainer();
                        }
                    }
                }
                return _CacheContainer;
            }
        }

        private ICacheManager _EntitySetCache;
        private List<EntityCfg> _EntitySetCfg;

        /// <summary>
        /// 私有化的构造函数，防止被构造多次
        /// </summary>
        private CacheContainer() {
            try {
                _EntitySetCache = CacheProxy.EntitySetCache;
                if (_EntitySetCache == null) {
                    throw new MB.Util.APPException("EntitySetCache没有做出正确的配置，请检查cachingConfiguration/cacheManagers节点");
                }
                _EntitySetCfg = EntitySetCacheCfgHelper.Instance.GetEntitySetCfg();//得到缓存对象的配置

            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write("EntitySetCache.CacheContainer构造失败:" + ex.ToString());
                throw new MB.Util.APPException("EntitySetCache.CacheContainer构造失败");
            }
        }

        /// <summary>
        /// 通过缓存注册器，加载缓存到缓存容器中
        /// </summary>
        public void LoadCache() {
            foreach (EntityCfg entityCfg in _EntitySetCfg) {
                LoadCache(entityCfg);
            }
        }

        /// <summary>
        /// 加载缓存
        /// </summary>
        /// <param name="entityCfg"></param>
        public void LoadCache(EntityCfg entityCfg) {
            string key = string.Empty;
            try {

                //从继承的CustomerLoader中获取数据
                IList data;
                ICacheLoader cacheLoader;
                if (!string.IsNullOrEmpty(entityCfg.CustomLoaderType)) {
                    string[] cfgContent = entityCfg.CustomLoaderType.Split(',');
                    cacheLoader = MB.Util.DllFactory.Instance.LoadObject(cfgContent[0], cfgContent[1]) as ICacheLoader;
                    data = cacheLoader.LoadCache();
                }
                else
                    throw new MB.Util.APPException("Entity节点中CustomLoaderType不存在");

                if (!data.GetType().IsGenericType) {
                    throw new MB.Util.APPException("加载器返回的IList不是泛型列表"); ;
                }
                key = data.GetType().GetGenericArguments()[0].FullName;
                if (data != null) {
                    CacheData cacheData = fillCacheData(data, entityCfg);//将数据填入CacheData
                    addCacheDataToContainer(key, cacheData, entityCfg);//将CacheData存入EnterpriseLib的CacheManager
                }
            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write("缓存加载器{0}出错，错误信息：{1}", key, ex.ToString());
            }
        }

        /// <summary>
        /// 从继承的CustomerLoader中获取数据
        /// </summary>
        /// <param name="entityCfg"></param>
        /// <returns></returns>
        private IList loadCacheData(EntityCfg entityCfg) {
            IList data;
            if (!string.IsNullOrEmpty(entityCfg.CustomLoaderType)) {
                string[] cfgContent = entityCfg.CustomLoaderType.Split(',');
                ICacheLoader cacheLoader = MB.Util.DllFactory.Instance.LoadObject(cfgContent[0].Trim(), cfgContent[1].Trim()) as ICacheLoader;
                data = cacheLoader.LoadCache();
            }
            else
                throw new MB.Util.APPException("Entity节点中CustomLoaderType不存在");

            return data;
        }

        /// <summary>
        /// 加载CacheData中的项目
        /// </summary>
        private CacheData fillCacheData(IList data, EntityCfg entityCfg) {
            Type genericCacheDataType = typeof(CacheData<>).MakeGenericType(data.GetType().GetGenericArguments()[0]);
            CacheData cacheData = DllFactory.Instance.CreateInstance(genericCacheDataType) as CacheData;
            MB.Util.MyReflection.Instance.InvokeMethodByName(cacheData, "InitCacheData", data, entityCfg);
            return cacheData;
        }

        /// <summary>
        /// 将Cache的数据加到缓存manager中
        /// </summary>
        /// <param name="cacheData"></param>
        /// <param name="entityCfg"></param>
        private void addCacheDataToContainer(string key, CacheData cacheData, EntityCfg entityCfg) {
            if (entityCfg != null) {
                List<ICacheItemExpiration> cacheItemExpiration = new List<ICacheItemExpiration>();
                foreach (var cacheExpireCfg in entityCfg.Expirations) {
                    if (cacheExpireCfg.ExpirationType == CacheExpirationType.AbsoluteTime) {
                        TimeSpan ts = TimeSpan.FromMilliseconds(cacheExpireCfg.ExpireTime);
                        if (ts.TotalMilliseconds > 0) {
                            AbsoluteTime absoluteTime = new AbsoluteTime(ts);
                            cacheItemExpiration.Add(absoluteTime);
                        }
                    }
                    else if (cacheExpireCfg.ExpirationType == CacheExpirationType.SlidingTime) {
                        TimeSpan ts = TimeSpan.FromMilliseconds(cacheExpireCfg.ExpireTime);
                        if (ts.TotalMilliseconds > 0) {
                            SlidingTime slidingTime = new SlidingTime(ts);
                            cacheItemExpiration.Add(slidingTime);
                        }
                    }
                    else if (cacheExpireCfg.ExpirationType == CacheExpirationType.FileDependency) {
                        if (System.IO.File.Exists(cacheExpireCfg.FilePath)) {
                            FileDependency fileDependency = new FileDependency(cacheExpireCfg.FilePath);
                            cacheItemExpiration.Add(fileDependency);
                        }
                    }
                }
                if (entityCfg.IsCacheItemRefreshed)
                    _EntitySetCache.Add(key, cacheData, entityCfg.CacheItemPriority, new CacheItemRefreshAction(entityCfg, key), cacheItemExpiration.ToArray());
                else
                    _EntitySetCache.Add(key, cacheData, entityCfg.CacheItemPriority, null, cacheItemExpiration.ToArray());
            }
            else
                _EntitySetCache.Add(key, cacheData);
        }

        #region 从缓存容器中获取数据
        /// <summary>
        /// 根据类型直接获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetObjects<T>() {
            Type cacheDataGenericArgumentType = typeof(T);
            string key = cacheDataGenericArgumentType.FullName;
            var result = MB.Util.MyReflection.Instance.InvokeMethod(_EntitySetCache[key], "GetObjects") as List<T>;
            return result;
        }


        /// <summary>
        /// 根据过滤条件，返回缓存对象LIST
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filters"></param>
        /// <returns></returns>
        public IList GetObjectsWithFilter<T>(params FilterParameter[] filters) {
            Type cacheDataGenericArgumentType = typeof(T);
            string key = cacheDataGenericArgumentType.FullName;
            Type cacheDataType = _EntitySetCache[key].GetType();
            if (filters == null) {
                return GetObjects<T>();
            }
            else {
                System.Reflection.MethodInfo getObjectsWithFilters = cacheDataType.GetMethod("GetObjectsWithFilters");

                object[] input = null;
                System.Reflection.ParameterInfo[] parameters = getObjectsWithFilters.GetParameters();
                bool hasParams = false;
                if (parameters.Length > 0)
                    hasParams = parameters[parameters.Length - 1].GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0;
                if (hasParams) {
                    int lastParamPosition = parameters.Length - 1;
                    object[] realParams = new object[parameters.Length];
                    for (int i = 0; i < lastParamPosition; i++)
                        realParams[i] = filters[i];
                    Type paramsType = parameters[lastParamPosition].ParameterType.GetElementType();
                    Array extra = Array.CreateInstance(paramsType, filters.Length - lastParamPosition);
                    for (int i = 0; i < extra.Length; i++)
                        extra.SetValue(filters[i + lastParamPosition], i);
                    realParams[lastParamPosition] = extra;
                    input = realParams;
                }

                return getObjectsWithFilters.Invoke(_EntitySetCache[key], input) as IList;
            }
        }

        /// <summary>
        /// 根据键值返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheDataKey"></param>
        /// <returns></returns>
        public T GetObjectByKey<T>(string cacheDataKey) where T : class {
            Type cacheDataGenericArgumentType = typeof(T);
            string key = cacheDataGenericArgumentType.FullName;
            Type cacheDataType = _EntitySetCache[key].GetType();

            System.Reflection.MethodInfo getObjectsWithKey = cacheDataType.GetMethod("GetObjectsWithKey");
            object[] input = new object[1];
            input[0] = cacheDataKey;
            var result = getObjectsWithKey.Invoke(_EntitySetCache[key], input) as T;
            return result;
        }

        #endregion

        /// <summary>
        /// 从缓存管理器中去除缓存
        /// </summary>
        /// <param name="entityType"></param>
        public void RemoveEntity(Type entityType) {
            string key = entityType.FullName;
            if (_EntitySetCache.Contains(key))
                _EntitySetCache.Remove(key);
        }

    }
}
