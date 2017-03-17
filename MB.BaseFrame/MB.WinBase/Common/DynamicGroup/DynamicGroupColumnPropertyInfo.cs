using System;
using MB.Util.XmlConfig;
using System.Xml.Serialization;
using System.ComponentModel;

namespace MB.WinBase.Common.DynamicGroup
{
    /// <summary>
    /// 动态聚组列设定
    /// </summary>
    [Serializable]
    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    [XmlRoot("Columns")]
    public class DynamicGroupColumnPropertyInfo
    {
        #region 变量定义...
        private string _Name;//字段名称。
        private string _Description;//字段描述
        private string _DataType;//字段类型
        private DynamicGroupColArea _ColArea; //列的作用，是分组还是聚合
        private string _SummaryItemType;//值中统计汇总的数据


        ////格式化信息 
        //private string _FormatType;
        //private string _FormatString;

        ////创建表达式UnboundExpression,用来推断某一个列的值
        //private string _Expression;
        ////UnboundExpression所依赖的列
        //private string _ExpressionSourceColumns;
        


        #endregion 变量定义...

        #region Public 属性...
        /// <summary>
        /// 字段名称, 需要与ColumnPropertyInfo中的NAME一致
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
        /// 字段描述
        /// 如果该COLUMN已经在ColumnPropertyInfo定义，忽略该属性
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        /// <summary>
        /// 字段类型
        /// 如果该COLUMN已经在ColumnPropertyInfo定义，忽略该属性
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string DataType {
            get { return _DataType; }
            set { _DataType = value; }
        }

        /// <summary>
        /// 列的作用域，必须配置
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public DynamicGroupColArea ColArea {
            get { return _ColArea; }
            set { _ColArea = value; }
        }

        ///// <summary>
        ///// 字段格式化类型
        ///// </summary>
        //[PropertyXmlConfig]
        //[XmlAttribute]
        //public string FormatType {
        //    get {
        //        return _FormatType;
        //    }
        //    set {
        //        _FormatType = value;
        //    }
        //}

        ///// <summary>
        ///// 字段格式化字符串
        ///// </summary>
        //[PropertyXmlConfig]
        //[XmlAttribute]
        //public string FormatString {
        //    get {
        //        return _FormatString;
        //    }
        //    set {
        //        _FormatString = value;
        //    }
        //}

        /// <summary>
        /// 如果字段是聚合列，则需要填写聚合类型,如果是分组列，则是空
        /// 聚合类型是Sum, Avg, Max, Min, Count
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string SummaryItemType
        {
            get
            {
                return _SummaryItemType;
            }
            set
            {
                _SummaryItemType = value;
            }
        }

        ///// <summary>
        ///// 如果这个列是从某些列推算出来的，可以填写表达式
        ///// 表达式中涉及的数据源列用占位符{0}表示
        ///// </summary>
        //[PropertyXmlConfig]
        //[XmlAttribute]
        //public string Expression
        //{
        //    get { return _Expression; }
        //    set { _Expression = value; }
        //}
        ///// <summary>
        ///// 以逗号隔开的表达式中涉及的源列
        ///// </summary>
        //[PropertyXmlConfig]
        //[XmlAttribute]
        //public string ExpressionSourceColumns
        //{
        //    get { return _ExpressionSourceColumns; }
        //    set { _ExpressionSourceColumns = value; }
        //}
        #endregion Public 属性...
    }

    /// <summary>
    /// 动态聚组列的领域定义
    /// 聚合或者分组
    /// </summary>
    public enum DynamicGroupColArea {
        [Description("None")]
        None,
        /// <summary>
        /// 分组列
        /// </summary>
        [Description("Group")]
        Group,
        /// <summary>
        /// 聚组列
        /// </summary>
        [Description("Aggregation")]
        Aggregation,

    }

}
