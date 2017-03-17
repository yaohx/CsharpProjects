using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.RuleBase.Atts {
    /// <summary>
    /// NextOwnAttribute 配置当前对象下级对象的引用关系。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class NextOwnAttribute : System.Attribute {
        private string _OwnTableName;
        private string _OwnFieldName;
        private string _OwnFilter;
        private string _OwnDescription;
        private string _CfgXmlSqlName;
        private LinkRestrictOption _RestrictOption;

        #region 构造函数...
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="cfgXmlSqlName">判断该对象是否被引用在XML中配置的SQL语句名称。</param>
        /// <param name="description"></param>
        public NextOwnAttribute(string cfgXmlSqlName, string description) {
            _CfgXmlSqlName = cfgXmlSqlName;
            _OwnDescription = description;
            _RestrictOption = LinkRestrictOption.CancelSubmit;
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="ownSetting">表和键值用逗号分开，多个不同对象引用用分号分开. 如：OrderDoc,UID;Material,ID</param>
        public NextOwnAttribute(string ownTableName, string ownFieldName, string description) {
            _OwnTableName = ownTableName;
            _OwnFieldName = ownFieldName;
            _OwnDescription = description;
            _RestrictOption = LinkRestrictOption.CancelSubmit;
        }
        /// <summary>
        /// 检查下级单据 并需要其它查询条件时
        /// </summary>
        /// <param name="ownTableName"></param>
        /// <param name="ownFieldName"></param>
        /// <param name="description"></param>
        public NextOwnAttribute(string ownTableName, string ownFieldName, string filter, string description) {
            _OwnTableName = ownTableName;
            _OwnFieldName = ownFieldName;
            _OwnFilter = filter;
            _OwnDescription = description;
            _RestrictOption = LinkRestrictOption.CancelSubmit;
        }
        #endregion 构造函数...

        #region public 成员...
        /// <summary>
        /// 当前对象下级对象的表名称。
        /// </summary>
        public string OwnTableName {
            get {
                return _OwnTableName;
            }
        }
        /// <summary>
        /// 当前对象下级对象的字段名称名称。
        /// </summary>
        public string OwnFieldName {
            get {
                return _OwnFieldName;
            }
        }

        /// <summary>
        /// 过滤条件
        /// </summary>
        public string OwnFilter {
            get {
                return _OwnFilter;
            }
        }
        /// <summary>
        /// 引用对象描述
        /// </summary>
        public string OwnDescription {
            get {
                return _OwnDescription;
            }
        }
        /// <summary>
        /// 判断该对象是否被引用在XML中配置的SQL语句名称。
        /// </summary>
        public string CfgXmlSqlName {
            get {
                return _CfgXmlSqlName;
            }
        }
        /// <summary>
        ///  下级数据引用后上级数约束配置。
        /// </summary>
        public LinkRestrictOption RestrictOption {
            get {
                return _RestrictOption;
            }
            set {
                _RestrictOption = value;
            }
        }
        #endregion public 成员...
    }

    /// <summary>
    /// 下级数据引用后上级数约束配置。    
    /// </summary>
    public enum LinkRestrictOption {
        /// <summary>
        /// 提交后阻止再重做，如果需要重做需要把下级单据删除掉。
        /// </summary>
        CancelSubmit,
        /// <summary>
        /// 提交后如果存在下级单据，可以撤消提交，但不可以删除。
        /// 对于这部分的处理，如果下级涉及到数量、金额、单价、日期、商品等信息的引用，
        /// 要进行特殊的处理，否则会引起数据的不一致。
        /// 注意： 要详细理解后再选择这种模式。
        /// </summary>
        Delete
    }
}
