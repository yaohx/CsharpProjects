using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;

using MB.Util.XmlConfig;
using MB.WinBase.DesignEditor;
using DevExpress.XtraGrid;
namespace MB.XWinLib.DesignEditor {
    /// <summary>
    /// StyleConditionInfo 网格列样式的条件描述。
    /// </summary>
    public class XtraStyleConditionInfo : MB.WinBase.DesignEditor.BaseEditorObject{
        #region 变量定义...
        private string _Name;
        private string _ColumnName;
        private string _ColumnCaption;
        private bool _ApplyToRow;
        private FormatConditionEnum _Condition;
        private string _StyleName;
        private string _Value;
        private string _Value2;
        private string _Expression;
        private bool _IsByEvent;

        private System.Drawing.Color _BackColor;
        private System.Drawing.Color _ForeColor;
        private System.Drawing.Font _ForeFont;
        private System.Drawing.Image _MarkImage;

        private object _Tag;
        #endregion 变量定义...

        #region 构造函数...
        /// <summary>
        ///  StyleConditionInfo 网格列样式的条件描述。
        /// </summary>
        public XtraStyleConditionInfo() {
        }
        /// <summary>
        ///  StyleConditionInfo 网格列样式的条件描述。
        /// </summary>
        /// <param name="name"></param>
        public XtraStyleConditionInfo(string name) {
            _Name = name;

        }
        #endregion 构造函数...
        /// <summary>
        /// 覆盖基类的方法，返回条件的名称。
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return _Name;
        }


        #region Public 属性...
        [
        Category("数据"),
        ReadOnly(true),
        Description("获取或设置列的名称."),
        EditorDescription("条件样式名称:")
        ]
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }
        /// <summary>
        /// 对应列的名称。
        /// </summary>
        [
         Browsable(false)
        ]
        public string ColumnName {
            get {
                return _ColumnName;
            }
            set {
                _ColumnName = value;
            }
        }
        /// <summary>
        /// 显示Caption 的列的名称。可以为空
        /// </summary>
        [
        CategoryAttribute("数据"),
        ReadOnly(false),
        DescriptionAttribute("获取或设置列的名称."),
        EditorDescription("绑定列名称:"),
        TypeConverter(typeof(ConvertToDropDownList)),
        Editor(typeof(XmlColumnPropertyInfoEditor), typeof(UITypeEditor))
        ]
        public string ColumnCaption {
            get {
                return _ColumnCaption;
            }
            set {
                _ColumnCaption = value;
            }
        }
        /// <summary>
        /// 当前设置的条件是否应用到整行。
        /// </summary>
        [
        Category("数据"),
        ReadOnly(false),
        Description("获取或设置当前设置的条件是否应用到整行."),
        EditorDescription("应用到整行:")
        ]
        public bool ApplyToRow {
            get {
                return _ApplyToRow;
            }
            set {
                _ApplyToRow = value;
            }
        }
        /// <summary>
        /// 数据比较的条件。
        /// </summary>
        [
        Category("数据"),
        ReadOnly(false),
        Description("获取或设置数据比较的条件."),
        EditorDescription("条件:")
        ]
        public FormatConditionEnum Condition
        {
            get {
                return _Condition;
            }
            set {
                _Condition = value;
            }
        }
        /// <summary>
        /// 满足条件时，需要显示的样式的名称。
        /// </summary>
        [Browsable(false)]
        [PropertyXmlConfig]
        public string StyleName {
            get {
                return _StyleName;
            }
            set {
                _StyleName = value;
            }
        }

        /// <summary>
        /// 比较值。
        /// </summary>
        [Browsable(false)]
        public string Value {
            get {
                return _Value;
            }
            set {
                _Value = value;
            }
        }

        string _valueDisplayText;

        /// <summary>
        /// 比较值。
        /// </summary>
        [
        Category("数据"),
        ReadOnly(false),
        Description("获取或设置列的值."),
        EditorDescription("条件值:"),
        Editor(typeof(StyleConditionValueEditor), typeof(UITypeEditor))]
        public string ValueDisplayText 
        {
            get
            {
                return _valueDisplayText;
            }
            set
            {
                _valueDisplayText = value;

                // 下拉框的值格式为Name-Code,把Code赋值给Values
                if (_valueDisplayText.Contains("-"))
                {
                    this.Value = _valueDisplayText.Substring(_valueDisplayText.IndexOf("-") + 1,
                        _valueDisplayText.Length - _valueDisplayText.IndexOf("-") - 1);
                }
                else
                {
                    this.Value = _valueDisplayText;
                }
            }
        }

        string _value2DisplayText;

        /// <summary>
        /// 比较值2。
        /// </summary>
        [
        Category("数据"),
        ReadOnly(false),
        Description("获取或设置列的值."),
        EditorDescription("条件值2:"),
        Editor(typeof(StyleConditionValueEditor), typeof(UITypeEditor))]
        public string Value2DisplayText 
        { 
            get
            {
                return _value2DisplayText;
            }
            set
            {
                _value2DisplayText = value;

                // 下拉框的值格式为Name-Code,把Code赋值给Values
                if (_value2DisplayText.Contains("-"))
                {
                    this.Value2 = _value2DisplayText.Substring(_value2DisplayText.IndexOf("-") + 1, 
                        _value2DisplayText.Length - _value2DisplayText.IndexOf("-") -1);
                }
                else
                {
                    this.Value2 = _value2DisplayText;
                }
            }
        }

        /// <summary>
        /// 比较值2。
        /// </summary>
        [Browsable(false)]
        public string Value2 {
            get {
                return _Value2;
            }
            set {
                _Value2 = value;
            }
        }
        /// <summary>
        /// 是否通过控件事件来控制。
        /// </summary>
        [Browsable(false)]
        public bool IsByEvent {
            get {
                return _IsByEvent;
            }
            set {
                _IsByEvent = value;
            }
        }
        /// <summary>
        /// 表达式
        /// (目前只支持简单的表达式) 例如：Col1 > Col2")
        /// </summary>
        [
        Category("表达式"),
        Description("获取或设置表达式(目前只支持简单的表达式) 例如：Col1 > Col2"),
        EditorDescription("表达式:"),
        Editor(typeof(StyleConditionExpressionEditor), typeof(UITypeEditor))
        ]
        public string Expression {
            get {
                return _Expression;
            }
            set {
                _Expression = value;
            }
        }
        /// <summary>
        /// 背景颜色。
        /// </summary>
        [
        Category("外观"),
        Description("获取或设置背景颜色"),
        EditorDescription("背景颜色:")
        ]
        public System.Drawing.Color BackColor {
            get {
                return _BackColor;
            }
            set {
                _BackColor = value;
            }
        }
        /// <summary>
        /// 字体颜色。
        /// </summary>
        [
        Category("外观"),
        Description("获取或设置字体颜色"),
        EditorDescription("字体颜色:")
        ]
        public System.Drawing.Color ForeColor {
            get {
                return _ForeColor;
            }
            set {
                _ForeColor = value;
            }
        }
        /// <summary>
        /// 字体。
        /// </summary>
        [
        Category("外观"),
        Description("获取或设置字体"),
        EditorDescription("字体:")
        ]
        public System.Drawing.Font ForeFont {
            get {
                return _ForeFont;
            }
            set {
                _ForeFont = value;
            }
        }
        /// <summary>
        /// 图片信息。
        /// </summary>
        [
        Category("外观"),
        Description("获取或设置行标记的图片信息"),
        EditorDescription("标记的图片:")
        ]
        public System.Drawing.Image MarkImage {
            get {
                return _MarkImage;
            }
            set {
                _MarkImage = value;
            }
        }
        /// <summary>
        /// 标签
        /// </summary>
        [Browsable(false)]
        public object Tag {
            get {
                return _Tag;
            }
            set {
                _Tag = value;
            }
        }
        #endregion Public 属性...
    }
}
