using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.RuleBase.Common; 
namespace MB.RuleBase.Atts {
    /// <summary>
    /// ParentProviderAttribute 配置当前对象引用上级对象的配置关系。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ParentProviderAttribute : System.Attribute {
        private string _ProviderTableName;
        private string _ProviderKeyName;
        private string _ForeingKeyField;
        private ObjectRelationType _ObjectRelationType;
        private bool _AutoOneToOneCheck = true;

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="providerTableName">当前对象引用上级对象表的名称</param>
        public ParentProviderAttribute(string providerTableName)
            : this(providerTableName, "ID", ObjectRelationType.OneToOne) {

        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="providerTableName">当前对象引用上级对象表的名称.</param>
        /// <param name="keyFieldName">当前对象引用上级对象表主键.</param>
        /// <param name="objectRelationType"></param>
        public ParentProviderAttribute(string providerTableName, string keyFieldName, ObjectRelationType objectRelationType)
            : this(providerTableName, keyFieldName, null, objectRelationType) {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="providerTableName">当前对象引用上级对象表的名称.</param>
        /// <param name="keyFieldName">当前对象引用上级对象表主键.</param>
        /// <param name="foreingKeyField">该对象引用的外键名称。</param>
        /// <param name="objectRelationType"></param>
        public ParentProviderAttribute(string providerTableName, string providerKeyName, string foreingKeyField, ObjectRelationType objectRelationType) {
            _ProviderTableName = providerTableName;
            _ProviderKeyName = providerKeyName;
            _ForeingKeyField = foreingKeyField;
            _ObjectRelationType = objectRelationType;
        }
        /// <summary>
        /// 当前对象引用上级对象表的名称.
        /// </summary>
        public string ProviderTableName {
            get {
                return _ProviderTableName;
            }
        }
        /// <summary>
        /// 当前对象引用上级对象表主键.
        /// </summary>
        public string ProviderKeyName {
            get {
                return _ProviderKeyName;
            }
        }
        /// <summary>
        /// 所引用的该对象的外键。
        /// </summary>
        public string ForeingKeyField {
            get {
                return _ForeingKeyField;
            }
            set {
                _ForeingKeyField = value;
            }
        }

        /// <summary>
        /// 对象之间的关联类型。
        /// </summary>
        public ObjectRelationType ObjectRelationType {
            get {
                return _ObjectRelationType;
            }
        }
        /// <summary>
        /// 判断和父级对象在一对一的情况下是否自动进行一对一的判断。
        /// </summary>
        public bool AutoOneToOneCheck {
            get {
                return _AutoOneToOneCheck;
            }
            set {
                _AutoOneToOneCheck = value;
            }
        }
    }
}
