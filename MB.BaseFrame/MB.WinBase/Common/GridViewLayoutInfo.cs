//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-06-12
// Description	:	网格列布局的描述信息。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MB.Util.XmlConfig; 

namespace MB.WinBase.Common {
    /// <summary>
    ///  网格列布局的描述信息。
    /// </summary>
    [Serializable]
    [ModelXmlConfig(ByXmlNodeAttribute = true)] 
    public class GridViewLayoutInfo {
        private string _Name;
        private GridViewType _GridViewType;
        private bool _ReadOnly;
        private DataTable _DataSource;
        private List<GridColumnLayoutInfo> _GridLayoutColumns;


        public GridViewLayoutInfo() {

            _GridLayoutColumns = new List<GridColumnLayoutInfo>();
        }
        /// <summary>
        ///  网格布局的名称。
        /// </summary>
        [PropertyXmlConfig]
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }
        /// <summary>
        ///  网格类型。
        /// </summary>
        [PropertyXmlConfig]
        public GridViewType GridViewType {
            get {
                return _GridViewType;
            }
            set {
                _GridViewType = value;
            }
        }
        /// <summary>
        /// 列的描述信息
        /// </summary>
        [PropertyXmlConfig(typeof(GridColumnLayoutInfo), MappingName = "Column", NotExistsGroupNode = true)]
        public List<GridColumnLayoutInfo> GridLayoutColumns {
            get {
                return _GridLayoutColumns;
            }
            set {
                _GridLayoutColumns = value;
            }
        }
        /// <summary>
        /// 判断当前创建的网格是否为只读。
        /// </summary>
        [PropertyXmlConfig]
        public bool ReadOnly {
            get {
                return _ReadOnly;
            }
            set {
                _ReadOnly = value;
            }

        }
        /// <summary>
        /// 需要绑定的数据源。
        /// </summary>
        public DataTable DataSource {
            get {
                return _DataSource;
            }
            set {
                _DataSource = value;
            }
        }
    }


    /// <summary>
    /// 网格列布局的描述信息。
    /// </summary>
    [Serializable]
    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    public class GridColumnLayoutInfo : System.ICloneable  {
        private string _Name;
        private string _Text;
        private string _Type;
        private string _ForeColor;
        private string _BackColor;
        private float _ForeFontSize;
        private int _VisibleWidth;
        private int _Index;
        private FixedStyle _Fixed;
        private DisplayFormat _DisplayFormat; // 格式化显示在网格中的
        private bool _DynamicChild;//判断子对象是否为动态列
        private List<GridColumnLayoutInfo> _Childs;
        private string _ColumnXmlCfgName; //

        public GridColumnLayoutInfo() {
            _Childs = new List<GridColumnLayoutInfo>();
            _Fixed = FixedStyle.None;
        }
        #region ICloneable 成员
        /// <summary>
        /// 创建当前不带Childs 的副表。
        /// </summary>
        /// <returns></returns>
        public GridColumnLayoutInfo CloneWithoutChilds() {
            GridColumnLayoutInfo clone = base.MemberwiseClone() as GridColumnLayoutInfo;
            return clone;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone() {
            GridColumnLayoutInfo clone = base.MemberwiseClone() as GridColumnLayoutInfo;
            List<GridColumnLayoutInfo> cloneChilds = new List<GridColumnLayoutInfo>();
            foreach (var child in _Childs)
                cloneChilds.Add((GridColumnLayoutInfo)child.Clone());

            clone.Childs = cloneChilds; 
            return clone;
        }

        #endregion

        /// <summary>
        /// 布局列名称
        /// </summary>
        [PropertyXmlConfig]
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        /// <summary>
        /// 文本描述
        /// </summary>
        [PropertyXmlConfig]
        public string Text {
            get { return _Text; }
            set { _Text = value; }
        }

        /// <summary>
        /// 创建列的类型
        /// </summary>
        [PropertyXmlConfig]
        public string Type {
            get { return _Type; }
            set { _Type = value; }
        }
        /// <summary>
        /// 字体颜色
        /// </summary>
        [PropertyXmlConfig]
        public string ForeColor {
            get { return _ForeColor; }
            set { _ForeColor = value; }
        }
        /// <summary>
        /// 背景颜色。
        /// </summary>
        [PropertyXmlConfig]
        public string BackColor {
            get { return _BackColor; }
            set { _BackColor = value; }
        }
        /// <summary>
        /// 字体大小。
        /// </summary>
        [PropertyXmlConfig]
        public float ForeFontSize {
            get { return _ForeFontSize; }
            set { _ForeFontSize = value; }
        }
        /// <summary>
        /// 显示宽度
        /// </summary>
        [PropertyXmlConfig]
        public int VisibleWidth {
            get { return _VisibleWidth; }
            set { _VisibleWidth = value; }
        }
        /// <summary>
        /// 位置。
        /// </summary>
        [PropertyXmlConfig]
        public int Index {
            get { return _Index; }
            set { _Index = value; }
        }
        /// <summary>
        /// 字段值显示的格式。
        /// </summary>
        [PropertyXmlConfig]
        public DisplayFormat DisplayFormat {
            get {
                return _DisplayFormat;
            }
            set {
                _DisplayFormat = value;
            }
        }
        /// <summary>
        /// 是否以该列作为
        /// </summary>
        [PropertyXmlConfig]
        public FixedStyle Fixed {
            get {
                return _Fixed;
            }
            set {
                _Fixed = value;
            }
        }
        /// <summary>
        /// 判断子对象是否为动态列。
        /// </summary>
        [PropertyXmlConfig]
        public bool DynamicChild {
            get {
                return _DynamicChild;
            }
            set {
                _DynamicChild = value;
            }
        }
        /// <summary>
        /// 对应XML列配置的信息。
        /// </summary>
        [PropertyXmlConfig]
        public string ColumnXmlCfgName {
            get {
                if (string.IsNullOrEmpty(_ColumnXmlCfgName))
                    return _Name;
                return _ColumnXmlCfgName;
            }
            set {
                _ColumnXmlCfgName = value;
            }
        }
        /// <summary>
        /// 子列信息设置。
        /// </summary>
        [PropertyXmlConfig(typeof(GridColumnLayoutInfo), MappingName = "Column",NotExistsGroupNode = true )]
        public List<GridColumnLayoutInfo> Childs {
            get {
                return _Childs;
            }
            set {
                _Childs = value;
            }
        }


    }
    /// <summary>
    /// DisplayFormat 格式化显示数据.
    /// </summary>
    [Serializable]
    [ModelXmlConfig(ByXmlNodeAttribute = false)]
    public class DisplayFormat {
        private string _FormatString;
        private FormatType _FormatType;
        /// <summary>
        /// 构造函数...
        /// </summary>
        public DisplayFormat() {
        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="formatString"></param>
        /// <param name="formatType"></param>
        public DisplayFormat(string formatString, FormatType formatType) {
            _FormatString = formatString;
            _FormatType = formatType;
        }
        [PropertyXmlConfig]
        public string FormatString {
            get {
                return _FormatString;
            }
            set {
                _FormatString = value;
            }
        }
        [PropertyXmlConfig]
        public FormatType FormatType {
            get {
                return _FormatType;
            }
            set {
                _FormatType = value;
            }
        }
    }
    /// <summary>
    /// FormatType 字符窜格式化显示的类型。
    /// </summary>
    public enum FormatType : int {
        None = 0,
        Numeric,
        DateTime,
        Custom
    }
    /// <summary>
    /// 网格视图类型。
    /// </summary>
    public enum GridViewType : int {
        GridView,
        AdvBandedGridView
    }
    /// <summary>
    /// Fixed 样式。
    /// </summary>
    public enum FixedStyle : int {
        None,
        Left,
        Right
    }

}
