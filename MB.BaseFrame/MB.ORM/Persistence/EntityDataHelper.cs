using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.Orm.Mapping;
using MB.Orm.Mapping.Att;

namespace MB.Orm.Persistence {
    /// <summary>
    /// 实体数据处理相关。
    /// </summary>
    public class EntityDataHelper {

        #region Instance...
        private static Object _Obj = new object();
        private static EntityDataHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected EntityDataHelper() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static EntityDataHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new EntityDataHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...
        /// <summary>
        ///  获取数据实体的键值。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public object GetEntityKeyValue(object entity) {
            string re = string.Empty;
            return  GetEntityKeyValue(entity,out re);
        }
        /// <summary>
        /// 获取数据实体的键值。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public object GetEntityKeyValue(object entity,out string keyPropertyName) {
            Type entityType = entity.GetType();
            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);
            if (string.IsNullOrEmpty(mappingInfo.MapTable))
                throw new MB.Util.APPException(string.Format("数据实体{0} 配置有误，没有指定映射到的表。", entityType.FullName), MB.Util.APPMessageType.SysErrInfo);
            if (mappingInfo.PrimaryKeys == null || mappingInfo.PrimaryKeys.Count != 1)
                throw new MB.Util.APPException(string.Format("数据实体{0} 配置有误，没有指定主键或者不是单一主键配置。", entityType.FullName), MB.Util.APPMessageType.SysErrInfo);
            FieldPropertyInfo keyInfo = mappingInfo.PrimaryKeys.Values.First<FieldPropertyInfo>();

            object key = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, keyInfo.PropertyName);
            keyPropertyName = keyInfo.PropertyName; 
            return key;
        }
    }
}
