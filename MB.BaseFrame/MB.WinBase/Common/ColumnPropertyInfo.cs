//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-18
// Description	:	FieldPropertyInfo  描述数据库表字段的属性描述
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;

using MB.Util.XmlConfig;
using System.Xml.Serialization;
using System.ComponentModel;
namespace MB.WinBase.Common
{
    /// <summary>
    /// FieldPropertyInfo  描述数据库表字段的属性描述
    /// </summary>
    [Serializable]
    [ModelXmlConfig(ByXmlNodeAttribute=true)]
    [XmlRoot("Column")]
    public class ColumnPropertyInfo : ICloneable {
        #region 变量定义...
        private string _Name; //字段的名称
        private string _Description; //字段的描述信息
        private string _DataType; //字段的类型
        private int _MaxLength = -1; //字段的长度
        private bool _IsNull = true;//该字段是否可以为空
        private bool _IsKey = false; //是否为Key值
        private bool _Visibled = true; //在构造Grid的时候是否要显示出来
        private int _VisibleWidth = 100; //在构造Grid的时候显示的宽度
        private int _FitLength = -1;//该字段只能输入的长度 ，不能短也不能长
        private string _BandName; //列联合的band的名称
        private bool _CanEdit = false; //表示该列是否可以编辑
        private int _OrderIndex; //在集合中的顺序号，通过它来构造列的显示顺序
        private bool _CanGroup = true; //判断是否可以进行分组拖的处理
        private bool _CanSort = true; //判断该列是否可以进行单击排列
        private bool _SummaryItem = false;//是否显示汇总的信息
        private SummaryItemType _SummaryItemType;//汇总的类型设置。
        //private bool _CreateChart = false;//标志该数据列是否可以用来创建图表
        //private string _ChartText;//该列构造图形的中文描述
        //private bool _PrintShow = true;//在对应的报表打印中，该列是否显示来设计
        //private bool _IsQueryField = false;//判断是否为查询字段
        private string _AllowChartAxes; //在图表显示中允许创建的轴
        private string _ChartDataType; //图表 数据类型

        private double _MinValue = double.MinValue; //最大值
        private double _MaxValue = double.MaxValue; //最小值
        private string _AllowEditStates;//允许编辑的实体对象状态。
        private int _MaxDecimalPlaces = -1; //小数点的最大位数
        private bool _AdvanceFilter = true; //判断该字段是否允许进行高级查询

        private bool _IsDynamic = true; //判断该字段是否为动态加载列
        private bool _IsPivotExpressionSourceColumn = false; //判断该字段是否为多维视图推算列的数据源

        private bool _IsFormatControl;  //是否格式控制
        private bool _ThousandsSeparator;  //是否显示千分位
        private bool _IsCustomFootSummary;//是否是自定义网格脚汇总列



        #endregion 变量定义...

        #region 构造函数...
        public ColumnPropertyInfo() {

        }
        /// <summary>
        /// 构造函数。。。
        /// </summary>
        /// <param name="fieldCaption"></param>
        /// <param name="isNull"></param>
        /// <param name="length"></param>
        public ColumnPropertyInfo(string fieldCaption, bool isNull, int length)
            : this(fieldCaption, isNull, "System.String", length) {
        }

        /// <summary>
        /// 构造函数。。。
        /// </summary>
        /// <param name="fieldCaption"></param>
        /// <param name="isNull"></param>
        /// <param name="typeName"></param>
        /// <param name="length"></param>
        public ColumnPropertyInfo(string fieldCaption, bool isNull, string typeName, int length) {
            _Description = fieldCaption;
            _IsNull = isNull;
            _DataType = typeName;
            _MaxLength = length;
        }
        public ColumnPropertyInfo(string name, bool canEdit, int visibleWidth, int orderIndex) {
            _Name = name;
            _Description = name;
            _CanEdit = canEdit;
            _VisibleWidth = visibleWidth;
            _OrderIndex = orderIndex;
        }
        //public FieldPropertyInfo(
        public ColumnPropertyInfo(string name, string description, string dataType,
            int length, bool isNull, bool isKey, bool visibled, int visibleWidth, int fitLength) {
            _Name = name;
            _Description = description;
            _DataType = dataType;
            _MaxLength = length;
            _IsNull = isNull;
            _IsKey = isKey;
            _Visibled = visibled;
            _VisibleWidth = visibleWidth;
            _FitLength = fitLength;
        }
        #endregion 构造函数...

        public override string ToString() {
            return _Description;
        }
        #region ICloneable Members

        public ColumnPropertyInfo Clone() {
            ColumnPropertyInfo info = this.MemberwiseClone() as ColumnPropertyInfo;
            return info;
        }

        object ICloneable.Clone() {
            return Clone();
        }

        #endregion ICloneable Members

        #region Public 属性...
        /// <summary>
        /// 字段名称。
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
        /// 描述。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string Description {
            get {
                return _Description;
            }
            set {
                _Description = value;
            }
        }
        /// <summary>
        /// 字段类型。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string DataType {
            get {
                if (_DataType == null || _DataType.Length == 0)
                    return "System.String";
                else
                    return _DataType;
            }
            set {
                _DataType = value;
            }
        }
        /// <summary>
        /// 字段存储在数据库中的最大长度。，
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public int MaxLength {
            get {
                return _MaxLength;
            }
            set {
                _MaxLength = value;
            }
        }
        /// <summary>
        /// 判断是否可以为空。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public bool IsNull {
            get {
                return _IsNull;
            }
            set {
                _IsNull = value;
            }
        }
        /// <summary>
        /// 判断是否为键值。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public bool IsKey {
            get {
                return _IsKey;
            }
            set {
                _IsKey = value;
            }
        }
        /// <summary>
        /// 判断是否显示。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public bool Visibled {
            get {
                return _Visibled;
            }
            set {
                _Visibled = value;
            }
        }
        /// <summary>
        /// 在默认网格显示中的宽度。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public int VisibleWidth {
            get {
                return _VisibleWidth;
            }
            set {
                _VisibleWidth = value;
            }
        }
        /// <summary>
        /// 字段定长的设置（设置后该字段的输入值只能是固定的长度。）
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public int FitLength {
            get {
                return _FitLength;
            }
            set {
                _FitLength = value;
            }
        }
        /// <summary>
        /// 在网格编辑中判断该列是否参与编辑。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public bool CanEdit {
            get {
                return _CanEdit;
            }
            set {
                _CanEdit = value;
            }
        }
        /// <summary>
        /// 在默认创建时该列在Grid 控件中的顺序。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public int OrderIndex {
            get {
                return _OrderIndex;
            }
            set {
                _OrderIndex = value;
            }
        }
        /// <summary>
        /// 判断该列是否可以分组。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public bool CanGroup {
            get {
                return _CanGroup;
            }
            set {
                _CanGroup = value;
            }
        }
        /// <summary>
        /// 判断该列是否可以排序。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public bool CanSort {
            get {
                return _CanSort;
            }
            set {
                _CanSort = value;
            }
        }
        /// <summary>
        /// 判断是否为汇总列。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public bool SummaryItem {
            get {
                return _SummaryItem;
            }
            set {
                _SummaryItem = value;
            }
        }
        /// <summary>
        /// 设置用该字段进行分组汇总的信息。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public SummaryItemType SummaryItemType {
            get {
                return _SummaryItemType;
            }
            set {
                _SummaryItemType = value;
            }
        }
        /// <summary>
        /// 字段所属的Band 名称。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string BandName {
            get {
                return _BandName;
            }
            set {
                _BandName = value;
            }
        }

        /// <summary>
        /// 保存为默认值
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public bool SaveDefaultValue
        {
            get;
            set;
        }

        ///// <summary>
        ///// 判断该字段是否可以创建图形分析的纬度。
        ///// </summary>
        //[PropertyXmlConfig]  
        //public bool CreateChart {
        //    get {
        //        return _CreateChart;
        //    }
        //    set {
        //        _CreateChart = value;
        //    }
        //}
        ///// <summary>
        ///// 在图形分析中字段显示的描述。
        ///// </summary>
        //[PropertyXmlConfig]  
        //public string ChartText {
        //    get {
        //        return _ChartText;
        //    }
        //    set {
        //        _ChartText = value;
        //    }
        //}
        ///// <summary>
        ///// 判断打印时是否需要显示。
        ///// </summary>
        //[PropertyXmlConfig]  
        //public bool PrintShow {
        //    get {
        //        return _PrintShow;
        //    }
        //    set {
        //        _PrintShow = value;
        //    }
        //}
        ///// <summary>
        ///// 判断是否为查询需要的字段。
        ///// </summary>
        //[PropertyXmlConfig]  
        //public bool IsQueryField {
        //    get {
        //        return _IsQueryField;
        //    }
        //    set {
        //        _IsQueryField = value;
        //    }
        //}
        /// <summary>
        /// 最小值。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public double MinValue {
            get {
                return _MinValue;
            }
            set {
                _MinValue = value;
            }
        }
        /// <summary>
        /// 最大值。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public double MaxValue {
            get {
                return _MaxValue;
            }
            set {
                _MaxValue = value;
            }
        }
        /// <summary>
        /// 允许编辑的实体对象状态。
        /// 例如：New,Modified,
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string AllowEditStates {
            get {
                return _AllowEditStates;
            }
            set {
                _AllowEditStates = value;
            }
        }
        /// <summary>
        /// 小数点最大位数
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public int MaxDecimalPlaces {
            get {
                return _MaxDecimalPlaces;
            }
            set {
                _MaxDecimalPlaces = value;
            }
        }
        /// <summary>
        /// 判断该字段是否允许作为高级查询字段。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public bool AdvanceFilter {
            get {
                return _AdvanceFilter;
            }
            set {
                _AdvanceFilter = value;
            }
        }
        /// <summary>
        ///  允许创建的图表区域
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string AllowChartAxes {
            get {
                return _AllowChartAxes;
            }
            set {
                _AllowChartAxes = value;
            }
        }
        /// <summary>
        /// 列对应的图表数据类型
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string ChartDataType {
            get {
                return _ChartDataType;
            }
            set {
                _ChartDataType = value;
            }
        }

        /// <summary>
        /// Having条件.当为True时,该字段只用于查询条件
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        [DefaultValue(false)]
        public bool IsHavingCondition { get; set; }

        /// <summary>
        /// 判断该字段是否为动态加载列
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public bool IsDynamic {
            get { return _IsDynamic; }
            set { _IsDynamic = value; }
        }


        /// <summary>
        /// 判断该字段是否为多维视图推算列的数据源
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public bool IsPivotExpressionSourceColumn
        {
            get { return _IsPivotExpressionSourceColumn; }
            set { _IsPivotExpressionSourceColumn = value; }
        }

        //add by aifang 2012-10-25 增加数字千分位显示   begin
        [PropertyXmlConfig]
        [XmlAttribute]
        [DefaultValue(false)]
        public bool IsFormatControl {
            get { return _IsFormatControl; }
            set { _IsFormatControl = value; }
        }
        [PropertyXmlConfig]
        [XmlAttribute]
        [DefaultValue(false)]
        public bool ThousandsSeperator
        {
            get { return _ThousandsSeparator; }
            set { _ThousandsSeparator = value; }
        }
        //add by aifang 2012-10-25 增加数字千分位显示   end

        [PropertyXmlConfig]
        [XmlAttribute]
        [DefaultValue(false)]
        public bool IsCustomFootSummary {
            get { return _IsCustomFootSummary; }
            set { _IsCustomFootSummary = value; }
        }
        #endregion Public 属性...
    }

  
    /// <summary>
    /// 列汇总的方式。
    /// </summary>
    public enum SummaryItemType {
        None,
        Average,
        Count,
        Max,
        Min,
        Sum
    }
  
}

