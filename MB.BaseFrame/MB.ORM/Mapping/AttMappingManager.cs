//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-05
// Description	:	通过Attribute 属性配置的映射操作类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

using MB.Orm.Mapping.Att;
using System.Collections.Concurrent;

namespace MB.Orm.Mapping {
    /// <summary>
    /// 通过Attribute 属性配置的映射操作类。
    /// 通过Instance 访问 不需要直接实例化该类。
    /// </summary>
    public sealed class AttMappingManager {
        private ConcurrentDictionary<Type, ModelMappingInfo> metaDataCache = new ConcurrentDictionary<Type, ModelMappingInfo>();

        #region Instance...
        private static Object _Obj = new object();
        private static AttMappingManager _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected AttMappingManager() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static AttMappingManager Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new AttMappingManager();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...
        /// <summary>
        /// 获取实体对象的简单映射。
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<FieldPropertyInfo> GetEntityMappingPropertys(Type t) {
            PropertyInfo[] pinfos = t.GetProperties();
            List<FieldPropertyInfo> lstPro = new List<FieldPropertyInfo>();
            foreach (var pro in pinfos) {
                if (pro.IsSpecialName) continue;

                lstPro.Add(new FieldPropertyInfo() { FieldName = pro.Name, PropertyName = pro.Name });
            }
            return lstPro;
        }
        /// <summary>
        /// 判断该类型是否存在实体映射的配置信息。
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool CheckExistsModelMapping(Type t) {
             ModelMapAttribute tattr = Attribute.GetCustomAttribute(t, typeof(ModelMapAttribute)) as ModelMapAttribute;
             return tattr != null;
        }
        /// <summary>
        /// 获取对象的映射信息。
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public ModelMappingInfo GetModelMappingInfo(Type t) {
            if (metaDataCache.ContainsKey(t)) {
                return metaDataCache[t];
            }
            else {
                metaDataCache[t] = parseObjectMappingInfo(t);
                return metaDataCache[t];
            }
        }
     

       /// <summary>
        ///  获取实体对象的配置主键。
       /// </summary>
       /// <param name="entity"></param>
       /// <param name="entityMappingInfo"></param>
       /// <returns></returns>
        public string GetPrimaryKey(object entity, ref  MB.Orm.Mapping.ModelMappingInfo entityMappingInfo) {
            Type entityType = entity.GetType();
            MB.Orm.Mapping.ModelMappingInfo mappingInfo = MB.Orm.Mapping.AttMappingManager.Instance.GetModelMappingInfo(entityType);

            if (string.IsNullOrEmpty(mappingInfo.MapTable))
                throw new MB.Util.APPException(string.Format("数据实体{0} 配置有误，没有指定映射到的表。", entityType.FullName), MB.Util.APPMessageType.SysErrInfo);

            if (mappingInfo.PrimaryKeys == null || mappingInfo.PrimaryKeys.Count != 1)
                throw new MB.Util.APPException(string.Format("数据实体{0} 配置有误，没有指定主键或者不是单一主键配置。", entityType.FullName), MB.Util.APPMessageType.SysErrInfo);

            MB.Orm.Mapping.FieldPropertyInfo keyInfo = mappingInfo.PrimaryKeys.Values.FirstOrDefault<MB.Orm.Mapping.FieldPropertyInfo>();
            if (keyInfo==null || !MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, keyInfo.FieldName))
                throw new MB.Util.APPException(string.Format("在检查下级对象引用时,对象主键{0}在对象{1}中不存在。请检查配置信息！", keyInfo.FieldName, entity.GetType().FullName), MB.Util.APPMessageType.DisplayToUser);

            entityMappingInfo = mappingInfo;
            return keyInfo.FieldName;
        }
        //根据类型获取对应的对象配置信息。
        private ModelMappingInfo parseObjectMappingInfo(Type t) {
            ModelMappingInfo m = new ModelMappingInfo();
            m.ClassName = t.Name;                   //类名
            m.EntityType = t;                       //实体类的类型
            parseModelMapping(t, m);     //实体类映射的表
            parseMetaData(t, m);
            return m;
        }
        //获取配置的列信息。
        private ModelMappingInfo parseMetaData(Type t, ModelMappingInfo m) {
            #region 字段信息
            PropertyInfo[] pinfos = t.GetProperties();                              //所有的属性
            m.FieldPropertys = new Dictionary<string, FieldPropertyInfo>(pinfos.Length);

            foreach (PropertyInfo pinfo in pinfos) {
                ExclusiveAttribute eattr = Attribute.GetCustomAttribute(pinfo, typeof(ExclusiveAttribute)) as ExclusiveAttribute;
                if (!Object.Equals(eattr, null)) {
                    continue;
                }
                ColumnMapAttribute cattr = Attribute.GetCustomAttribute(pinfo, typeof(ColumnMapAttribute)) as ColumnMapAttribute;
                if (cattr == null)
                    continue;

                FieldPropertyInfo fd = new FieldPropertyInfo();
                fd.PropertyInfo = pinfo;
                fd.PropertyName = pinfo.Name;
                fd.DeclaringType = pinfo.DeclaringType;


                fd.FieldName = cattr.ColumnName;
                fd.DbType = cattr.DbType;
                fd.DefaultValue = cattr.DefaultValue;
                //}
                //else {
                //    fd.FieldName = fd.PropertyName;
                //    fd.DbType = DbType.Object;
                //    fd.DefaultValue = String.Empty;
                //}

                AutoIncreaseAttribute aattr = Attribute.GetCustomAttribute(pinfo, typeof(AutoIncreaseAttribute)) as AutoIncreaseAttribute;
                if (!Object.Equals(null, aattr)) {
                    fd.AutoIncrease = m.HasAutoIncreasePorperty = true;
                    fd.Step = aattr.Step;
                    m.AutoIncreasePorperty = fd.PropertyName;
                }
                m.FieldPropertys.Add(fd.PropertyName, fd);
            }

            m.PrimaryKeys = getKeyColumns(t, m);
            if (m.PrimaryKeys.Count > 1)
                m.IsMultiPrimaryKey = true;
            else
                m.IsMultiPrimaryKey = false;

            #endregion
            return m;
        }
        #region 支持方法
        //获取实体对象的所有列
        private List<string> getDbColumns(Type entityType) {
            PropertyInfo[] pinfos = entityType.GetProperties();
            List<string> columns = new List<string>();
            foreach (PropertyInfo pinfo in pinfos) {
                ColumnMapAttribute attr = Attribute.GetCustomAttribute(pinfo, typeof(ColumnMapAttribute)) as ColumnMapAttribute;
                if (attr == null) continue;
                columns.Add(attr.ColumnName);
            }
            return columns;
        }
        //获取键值列。
        private Dictionary<string, FieldPropertyInfo> getKeyColumns(Type entityType, ModelMappingInfo m) {
            ModelMapAttribute tattr = Attribute.GetCustomAttribute(entityType, typeof(ModelMapAttribute)) as ModelMapAttribute;
            if (tattr == null)
                throw new MB.Util.APPException(string.Format("在获取实体列时 类型{0} 没有配置 ModelMapAttribute 特性,请先配置",entityType.FullName)); 

            string[] keys = tattr.PrimaryKeys;
            Dictionary<string, FieldPropertyInfo> pkeys = new Dictionary<string, FieldPropertyInfo>(keys.Length);
            for (int i = 0; i < keys.Length; i++) {
                string keyName = keys[i];
                if (m.FieldPropertys.ContainsKey(keyName))
                    pkeys.Add(keyName, m.FieldPropertys[keyName]);
                else
                    throw new MB.Util.APPException(string.Format("指定的键值{0} 在实体类型{1}中不存在,或者存在但没有配置属性的MB.Orm.Mapping.Att.ColumnMap 特殊", keyName, entityType.FullName)); 
                    //MB.Util.TraceEx.Write(string.Format("指定的键值{0} 在实体类型{1}中不存在！",keyName,entityType.FullName), MB.Util.APPMessageType.SysErrInfo);
 
            }
            return pkeys;
        }
        //获取除了主键外的列。
        private List<string> getColumnsExceptKey(Type entityType) {
            Dictionary<string, FieldPropertyInfo> keyColumns = GetModelMappingInfo(entityType).PrimaryKeys;

            PropertyInfo[] pinfos = entityType.GetProperties();
            List<string> columns = getDbColumns(entityType);

            foreach (FieldPropertyInfo fd in keyColumns.Values) {
                columns.Remove(fd.FieldName);
            }
            return columns;
        }

        //获取映射的表名。
        private ModelMappingInfo parseModelMapping(Type entityType, ModelMappingInfo m) {
            ModelMapAttribute tattr = Attribute.GetCustomAttribute(entityType, typeof(ModelMapAttribute)) as ModelMapAttribute;
            if (tattr != null) {
                m.MapTable = tattr.TableName;
                m.XmlConfigFileName = tattr.XmlFileName;
                m.ConfigOptions = tattr.ConfigOptions; 
            }
            else {
                m.MapTable = entityType.Name;
                m.XmlConfigFileName = entityType.Name;
                m.ConfigOptions = MB.Orm.Enums.ModelConfigOptions.ColumnCfgByAttribute | MB.Orm.Enums.ModelConfigOptions.CreateSqlByXmlCfg;
            }
            return m;
        }

        #endregion

    }
}
