using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.Util.XmlConfig;
using System.Xml.Serialization;
using System.ComponentModel;
using MB.Util;

namespace MB.WinBase.Common
{
    /// <summary>
    /// 查询过滤配置的
    /// </summary>
    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    [XmlRoot("Element")]
    public class DataFilterElementCfgInfo
    {
        private string _Name;
        private string _EditControlType;
        private bool _LimitColumn;
        private bool _Nullable = true;
        private bool _AllowMultiValue;
        private MB.Util.DataFilterConditions _FilterCondition;
        private string _Formate;

        private List<DataFilterElementLimitInfo> _FilterLimits;

        #region construct function...
        /// <summary>
        /// construct function...
        /// </summary>
        public DataFilterElementCfgInfo()
            : this(string.Empty) {

        }
        /// <summary>
        /// construct function...
        /// </summary>
        /// <param name="columnName"></param>
        public DataFilterElementCfgInfo(string columnName) {
            _Name = columnName;
            _FilterCondition = MB.Util.DataFilterConditions.Include;
            _FilterLimits = new List<DataFilterElementLimitInfo>();
        }
        #endregion construct function...

        #region public 属性...
        /// <summary>
        /// 列的名称。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }
        /// <summary>
        ///  输入条件的控件类型。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string EditControlType {
            get {
                return _EditControlType;
            }
            set {
                _EditControlType = value;
            }
        }
        /// <summary>
        /// 判断是否为限制的列。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        [DefaultValue(false)]
        public bool LimitColumn {
            get {
                return _LimitColumn;
            }
            set {
                _LimitColumn = value;
            }
        }
        /// <summary>
        /// 是否允许为空，默认情况下为 True
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        [DefaultValue(true)]
        public bool Nullable {
            get {
                return _Nullable;
            }
            set {
                _Nullable = value;
            }
        }
        /// <summary>
        /// 判断是否允许输入多个值。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        [DefaultValue(false)]
        public bool AllowMultiValue {
            get {
                return _AllowMultiValue;
            }
            set {
                _AllowMultiValue = value;
            }
        }
        /// <summary>
        /// 过滤条件。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        [DefaultValue(DataFilterConditions.Equal)]
        public MB.Util.DataFilterConditions FilterCondition {
            get {
                return _FilterCondition;
            }
            set {
                _FilterCondition = value;
            }
        }
        /// <summary>
        /// 格式化
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string Formate {
            get;
            set;
        }

        /// <summary>
        /// 查询条件限制列
        /// </summary>
        [PropertyXmlConfig(typeof(DataFilterElementLimitInfo))]
        public List<DataFilterElementLimitInfo> FilterLimits
        {
            get {
                return _FilterLimits;
            }
            set {
                _FilterLimits = value;
            }
        }
        #endregion public 属性...
    }

    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    public class DataFilterElementLimitInfo
    {
        private string _Name;
        private string _SourceName;
        private MB.Util.DataFilterConditions _FilterCondition;
        private bool _Nullable;
        private bool _AllowFilterValue;

        public DataFilterElementLimitInfo():this(null)
        {

        }
        public DataFilterElementLimitInfo(string colName)
        {
            _Name = colName;
            _FilterCondition = Util.DataFilterConditions.Include;
            _Nullable = true;
            _AllowFilterValue = false;
        }

        /// <summary>
        /// 限制列名
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        /// <summary>
        /// 限制列源列名
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string SourceName
        {
            get
            {
                return _SourceName;
            }
            set
            {
                _SourceName = value;
            }
        }
        /// <summary>
        /// 过滤条件
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public MB.Util.DataFilterConditions FilterCondition
        {
            get
            {
                return _FilterCondition;
            }
            set
            {
                _FilterCondition = value;
            }
        }
        /// <summary>
        /// 是否允许为空，默认情况下为 True
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public bool Nullable
        {
            get
            {
                return _Nullable;
            }
            set
            {
                _Nullable = value;
            }
        }

        /// <summary>
        /// 是否允许过滤数据
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public bool AllowFilterValue
        {
            get
            {
                return _AllowFilterValue;
            }
            set
            {
                _AllowFilterValue = value;
            }
        }
    }
}
