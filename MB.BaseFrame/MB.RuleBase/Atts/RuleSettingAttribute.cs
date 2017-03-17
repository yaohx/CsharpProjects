using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.RuleBase.Atts {
    /// <summary>
    /// RuleSettingAttribute 业务规则类的配置处理相关。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RuleSettingAttribute : System.Attribute {
        #region 变量定义...
        private object _BaseDataType;

        private bool _StartPrivilege;
        private bool _IncludeSubmit;
        private bool _CopyAndAdd;
        private bool _DateLimitFilter = true;
        private GenerateKeyModel _GenerateKeyModel = GenerateKeyModel.OnUIEdit;
        #endregion 变量定义...

        #region 构造函数...
        /// <summary>
        /// 构造函数.
        /// </summary>
        public RuleSettingAttribute()
            : this(null,false, false) {

        }
        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="startSwitch">判断是否启动</param>
        public RuleSettingAttribute(bool startPrivilege)
            : this(null,startPrivilege, true) {
        }
        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="startPrivilege"></param>
        /// <param name="includeSubmit"></param>
        public RuleSettingAttribute(object baseDataType,bool startPrivilege, bool includeSubmit) {
            _BaseDataType = baseDataType;
            _StartPrivilege = startPrivilege;
            _IncludeSubmit = includeSubmit;
        }
        #endregion 构造函数...

        #region public 属性...
        /// <summary>
        /// 主表数据在单据数据类型中的类型值。
        /// </summary>
        public object BaseDataType {
            get {
                return _BaseDataType;
            }
            set {
                _BaseDataType = value;
            }
        }
        /// <summary>
        /// 判断是否启动数据权限控制。
        /// </summary>
        public bool StartPrivilege {
            get {
                return _StartPrivilege;
            }
            set {
                _StartPrivilege = value;
            }
        }
        /// <summary>
        /// 判断是否包含提交功能。
        /// </summary>
        public bool IncludeSubmit {
            get {
                return _IncludeSubmit;
            }
            set {
                _IncludeSubmit = value;
            }
        }
        /// <summary>
        /// 判断是否包含复制新增的功能。
        /// </summary>
        public bool CopyAndAdd {
            get {
                return _CopyAndAdd;
            }
            set {
                _CopyAndAdd = value;
            }
        }
        /// <summary>
        /// 初始化打开时，判断是否显示当天的数据。
        /// </summary>
        public bool DateLimitFilter {
            get {
                return _DateLimitFilter;
            }
            set {
                _DateLimitFilter = value;
            }
        }
        /// <summary>
        /// 编辑对象键值产生的方式。
        /// </summary>
        public GenerateKeyModel GenerateKeyModel {
            get {
                return _GenerateKeyModel;
            }
            set {
                _GenerateKeyModel = value;
            }
        }
        #endregion public 属性...
    }
    /// <summary>
    /// 编辑对象键值产生的方式。
    /// </summary>
    public enum GenerateKeyModel {
        /// <summary>
        /// 默认值，在UI层用户创建的时候产生。
        /// </summary>
         OnUIEdit,
        /// <summary>
        /// 在数据保存的时候产生。
        /// </summary>
         OnDataSave
    }
}
