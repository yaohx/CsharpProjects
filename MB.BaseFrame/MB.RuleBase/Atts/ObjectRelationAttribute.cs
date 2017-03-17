using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.RuleBase.Atts {
    /// <summary>
    /// ObjectRelationAttribute 对象之间关联存储的属性定义。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
    public class ObjectRelationAttribute : System.Attribute {
        private int _SaveOrderLevel;
        private string[] _RelationSetting;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ObjectRelationAttribute(int saveOrderLevel) {
            _SaveOrderLevel = saveOrderLevel;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="saveOrderLevel">对象存储的先后顺序</param>
        /// <param name="relation">存储关联对象的关系信息(就主表通过什么字段关联外表的数据字段和表之间用逗号分开。如果存在多个就用分号隔开。)例如："MainTableID,MainTable"</param>
        public ObjectRelationAttribute(int saveOrderLevel, params string[] relation) {
            _SaveOrderLevel = saveOrderLevel;
            _RelationSetting = relation;
        }

        #region public 属性...
        /// <summary>
        /// 对象数据存储的顺序号
        /// </summary>
        public int SaveOrderLevel {
            get {
                return _SaveOrderLevel;
            }
            set {
                _SaveOrderLevel = value;
            }
        }
        /// <summary>
        /// 存储关联对象的关系信息
        /// </summary>
        public string[] RelationSetting {
            get {
                return _RelationSetting;
            }
            set {
                _RelationSetting = value;
            }
        }
        #endregion public 属性...
    }
}
