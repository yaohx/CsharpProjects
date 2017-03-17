//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-07
// Description	:	Cache 管理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

using MB.Orm.DbSql;
using MB.Orm.Mapping;

namespace MB.Orm {
    /// <summary>
    /// 统一把ORM 中涉及到的Cache 都集中放在这里是为了 对Cache 更好的管理，
    /// Cache 是非常珍贵的资源，使用要节约。同时计划和策略也非常关键。
    /// 
    /// CacheProxy 
    /// </summary>
    public class CacheProxy {
        private static ICacheItemExpiration[] _EntityCacheSetting;
        private static ICacheManager _EntityCache = null;//CacheFactory.GetCacheManager("MB.Orm.EntityCache");
        private static ICacheManager _SqlCache = null;//CacheFactory.GetCacheManager("MB.Orm.SqlCache");
        private static ICacheManager _MappingCache = null;//CacheFactory.GetCacheManager("MB.Orm.MappingCache");
        private static ICacheManager _XmlCfgFileCache = null;//CacheFactory.GetCacheManager("MB.Orm.XmlCfgFileCache");
        private static ICacheManager _EntitySetCache = null;
        private static readonly string ENTITY_CACHE_NAME = "MB.Orm.EntityCache";
        private static readonly string SQL_CACHE_NAME = "MB.Orm.SqlCache";
        private static readonly string MAPPING_CACHE_NAME = "MB.Orm.MappingCache";
        private static readonly string XML_CONFIG_CACHE_NAME = "MB.Orm.XmlCfgFileCache";
        private static readonly string ENTITY_SET_CACHE_NAME = "MB.Orm.EntitySetCache";

        private const int ENTITY_CACHE_EXPIRATION_CHECK_INTERVAL = 60;
        
        private static Object _Obj = new object();

        static CacheProxy() {
            
            try {
                lock (_Obj) {
                    string entityCachName = System.Configuration.ConfigurationManager.AppSettings["EntityCachName"];
                    if (string.IsNullOrEmpty(entityCachName))
                        entityCachName = ENTITY_CACHE_NAME;

                    string sqlCachName = System.Configuration.ConfigurationManager.AppSettings["SqlCachName"];
                    if (string.IsNullOrEmpty(sqlCachName))
                        sqlCachName = SQL_CACHE_NAME;

                    string mappingCachName = System.Configuration.ConfigurationManager.AppSettings["MappingCachName"];
                    if (string.IsNullOrEmpty(mappingCachName))
                        mappingCachName = MAPPING_CACHE_NAME;

                    string xmlCachName = System.Configuration.ConfigurationManager.AppSettings["XmlCachName"];
                    if (string.IsNullOrEmpty(xmlCachName))
                        xmlCachName = XML_CONFIG_CACHE_NAME;

                    string entitySetCacheName = System.Configuration.ConfigurationManager.AppSettings["EntitySetCache"];
                    if (string.IsNullOrEmpty(entitySetCacheName))
                        entitySetCacheName = ENTITY_SET_CACHE_NAME;

                    _EntityCache = CacheFactory.GetCacheManager(entityCachName);
                    _SqlCache = CacheFactory.GetCacheManager(sqlCachName);
                    _MappingCache = CacheFactory.GetCacheManager(mappingCachName);
                    _XmlCfgFileCache = CacheFactory.GetCacheManager(xmlCachName);
                    //_EntitySetCache = CacheFactory.GetCacheManager(ENTITY_SET_CACHE_NAME);
                }
            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write("MB.CacheProxy 缓存没有进行相应的配置！" + ex.Message);
            }
        }

        //获取过期策略配置信息。
        internal static ICacheItemExpiration[] GetExpirationPolicy() {
            if (Object.Equals(_EntityCacheSetting, null)) {
                _EntityCacheSetting = (ICacheItemExpiration[])ConfigurationSettings.GetConfig("MbOrmExpirationPolicy");
            }
            return _EntityCacheSetting;
        }

        #region CacheEntity 处理相关(目前还没有启动)...
        /// <summary>
        /// CacheEntity.
        /// </summary>
        /// <param name="entity"></param>
        public static void CacheEntity(object entity) {
            
            if (_EntityCache == null) return;

            string key = buildKey(entity);
            _EntityCache.Add(key, entity);
        }
        /// <summary>
        /// CacheEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="key"></param>
        public static void CacheEntity(object entity, string key) {
            if (_EntityCache == null) return;

            _EntityCache.Add(key, entity, CacheItemPriority.Normal, null, GetExpirationPolicy());
        }
        /// <summary>
        /// GetCachedEntity
        /// </summary>
        /// <param name="t"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static object GetCachedEntity(Type t, params object[] keys) {
            if (_EntityCache == null) return null;

            return _EntityCache.GetData(buildKey(t, keys));
        }
        /// <summary>
        /// ContainsEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool ContainsEntity(object entity) {
            if (_EntityCache == null) return false;

            string key = buildKey(entity);
            return _EntityCache.Contains(key);
        }
        /// <summary>
        /// ContainsEntity
        /// </summary>
        /// <param name="t"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool ContainsEntity(Type t, params object[] keys) {
            if (_EntityCache == null) return false;

            string key = buildKey(t, keys);
            return _EntityCache.Contains(key);
        }
        /// <summary>
        /// RemoveCachedEntity
        /// </summary>
        /// <param name="entity"></param>
        public static void RemoveCachedEntity(object entity) {
            if (_EntityCache == null) return;

            string key = buildKey(entity);
            _EntityCache.Remove(key);
        }
        /// <summary>
        /// ClearEntityCache
        /// </summary>
        public static void ClearEntityCache() {
            if(_EntityCache!=null)
                _EntityCache.Flush();
        }
        private static string buildKey(object entity) {

            Type t = entity.GetType();
            var keys = AttMappingManager.Instance.GetModelMappingInfo(t).PrimaryKeys;
            object[] keyalues = new object[keys.Count];
            int i = 0;
            foreach (string fd in keys.Keys) {
                keyalues[i] = t.GetProperty(fd).GetValue(entity, null);
                i++;
            }

            return buildKey(t, keyalues);
        }
        private static string buildKey(Type t, object[] keys) {
            StringBuilder s = new StringBuilder(t.FullName).Append("@");
            for (int i = 0; i < keys.Length; i++) {
                s.Append(keys[i].ToString()).Append("#");
            }
            return s.ToString();
        }
        #endregion CacheEntity 处理相关...

        #region CacheSql 处理相关...
        private static object _CSql = new object();
        /// <summary>
        /// CacheSql
        /// </summary>
        /// <param name="key"></param>
        /// <param name="sql"></param>
        public static void CacheSql(string key, SqlString[] sql) {
            lock (_CSql) {
                if (_SqlCache != null && !_SqlCache.Contains(key))
                    _SqlCache.Add(key, sql);
            }
        }
        /// <summary>
        /// GetCachedSql
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static SqlString[] GetCachedSql(string key) {
            if (_SqlCache == null) return null;

            return _SqlCache.GetData(key) as SqlString[];
        }
        /// <summary>
        /// ContainsSql
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ContainsSql(string key) {
            if (_SqlCache == null) return false;

            return _SqlCache.Contains(key);
        }
        #endregion CacheSql 处理相关...

        #region MappingCache 处理相关(目前还没有启动)...
        /// <summary>
        /// CacheMapping
        /// </summary>
        /// <param name="key"></param>
        /// <param name="sql"></param>
        public static void CacheMapping(string key, object mapping) {
            if(_MappingCache!=null)
                _MappingCache.Add(key, mapping);
        }
        /// <summary>
        /// GetCacheMapping
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetCacheMapping<T>(string key) {
            return (T)_MappingCache.GetData(key);
        }
        /// <summary>
        /// ContainsSql
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ContainsCacheMapping(string key) {
            if (_MappingCache == null) return false;

            return _MappingCache.Contains(key);
        }
        #endregion MappingCache 处理相关...

        #region XmlCfgFileCache 处理相关...
        private static object _CFGObj = new object();
        /// <summary>
        /// CacheXmlCfgFile
        /// </summary>
        /// <param name="key"></param>
        /// <param name="sql"></param>
        public static void CacheXmlCfgFile(string key, object mapping) {
            lock (_CFGObj) {
                if (_XmlCfgFileCache != null && !_XmlCfgFileCache.Contains(key))
                    _XmlCfgFileCache.Add(key, mapping);
            }
        }
        /// <summary>
        /// GetCacheXmlCfgFile
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetCacheXmlCfgFile<T>(string key) {
            return (T)_XmlCfgFileCache.GetData(key);
        }
        /// <summary>
        /// ContainsXmlCfgFile
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ContainsXmlCfgFile(string key) {
            if (_XmlCfgFileCache == null) return false;

            return _XmlCfgFileCache.Contains(key);
        }
        #endregion XmlCfgFileCache 处理相关...

        #region EntitySetCache 处理相关,EntitySet的相关处理都在 MB.Orm.EntitySetCache.CacheContainer中(目前还没有启动)...
        /// <summary>
        /// EntitySetCache的容器
        /// </summary>
        internal static ICacheManager EntitySetCache { get { return _EntitySetCache; } }

        #endregion
    }
}
