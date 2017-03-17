//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-18
// Description	:	StyleConditionInfo  网格列样式的条件描述。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.ComponentModel;

using MB.Util.XmlConfig;
namespace MB.WinBase.Common {
    /// <summary>
    /// StyleConditionInfo 网格列样式的条件描述。
    /// </summary>
    [ModelXmlConfig(ByXmlNodeAttribute=false)]   
    public class StyleConditionInfo {
        #region 变量定义...
        private string _Name;
        private string _ColumnName;
        private string _DispColName;
        private bool _ApplyToRow;
        private MB.Util.DataFilterConditions _Condition;
        private string _StyleName;
        private string _Value;
        private string _Value2;
        private string _Expression;
        private bool _IsByEvent;

        private System.Drawing.Color _BackColor;
        private System.Drawing.Color _ForeColor;
        private System.Drawing.Font _ForeFont;
        private System.Drawing.Image _MarkImage;
 
        #endregion 变量定义...

        #region 构造函数...
        /// <summary>
        ///  StyleConditionInfo 网格列样式的条件描述。
        /// </summary>
        public StyleConditionInfo() {
        }
        /// <summary>
        ///  StyleConditionInfo 网格列样式的条件描述。
        /// </summary>
        /// <param name="name"></param>
        public StyleConditionInfo(string name) {
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
        /// <summary>
        /// 条件ID ,可以为空。
        /// </summary>
        [ReadOnly(true), Category("数据")]
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
        /// 对应列的名称。
        /// </summary>
        [Description("获取或设置列的名称"), Category("数据")]
        [PropertyXmlConfig]  
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
        [Browsable(false)]
        [PropertyXmlConfig]  
        public string DispColName {
            get {
                return _DispColName;
            }
            set {
                _DispColName = value;
            }
        }
        /// <summary>
        /// 当前设置的条件是否应用到整行。
        /// </summary>
        [Description("获取或设置当前设置的条件是否应用到整行。"), Category("数据")]
        [PropertyXmlConfig]  
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
        [Description("获取或设置数据比较的条件。"), Category("数据")]
        [PropertyXmlConfig]  
        public MB.Util.DataFilterConditions Condition {
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
        [Description("获取或设置列的值"), Category("数据")]
        [PropertyXmlConfig]  
        public string Value {
            get {
                return _Value;
            }
            set {
                _Value = value;
            }
        }
        /// <summary>
        /// 比较值2。
        /// </summary>
        [Description("获取或设置列的值2"), Category("数据")]
        [PropertyXmlConfig]  
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
        [PropertyXmlConfig]  
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
        [Description("获取或设置表达式(目前只支持简单的表达式) 例如：Col1 > Col2"), Category("表达式")]
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
        [Description("获取或设置背景颜色"), Category("外观")]
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
        [Description("获取或设置字体颜色。"), Category("外观")]
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
        [Description("获取或设置字体。"), Category("外观")]
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
        [Description("获取或设置行标记的图片信息。"), Category("外观")]
        public System.Drawing.Image MarkImage {
            get {
                return _MarkImage;
            }
            set {
                _MarkImage = value;
            }
        }
 
        #endregion Public 属性...
    }
}
