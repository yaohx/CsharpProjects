//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-03-17
// Description	:	列的绑定信息。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Binding {
    /// <summary>
    /// 列的绑定信息。
    /// </summary>
    [Serializable]
    public class ColumnBindingInfo {
        private string _ColumnName;
        private MB.WinBase.Common.ColumnPropertyInfo _ColumnPropertyInfo;
        private System.Windows.Forms.Control _BindingControl;

        #region construct function...
        /// <summary>
        ///  construct function...
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="columnPropertyInfo"></param>
        /// <param name="bindingControl"></param>
        public ColumnBindingInfo(string columnName, MB.WinBase.Common.ColumnPropertyInfo columnPropertyInfo, System.Windows.Forms.Control bindingControl) {
            _ColumnName = columnName;
            _ColumnPropertyInfo = columnPropertyInfo;
            _BindingControl = bindingControl;
            
        }
        #endregion construct function...

        public override string ToString() {
            if (_ColumnPropertyInfo != null)
                return _ColumnPropertyInfo.Description;
            else
                return string.Empty;
        }

        #region public 属性...
        /// <summary>
        /// 对应列的名称，如果绑定的是集合那么对应的是Entity 的属性名称。
        /// </summary>
        public string ColumnName {
            get {
                return _ColumnName;
            }
            set {
                _ColumnName = value;
            }
        }
        /// <summary>
        /// 列的配置描述信息。
        /// </summary>
        public MB.WinBase.Common.ColumnPropertyInfo ColumnPropertyInfo {
            get {
                return _ColumnPropertyInfo;
            }
            set {
                _ColumnPropertyInfo = value;
            }
        }
        /// <summary>
        /// 对应绑定的控件。
        /// </summary>
        public System.Windows.Forms.Control BindingControl {
            get {
                return _BindingControl;
            }
            set {
                _BindingControl = value;
            }
        }
        #endregion public 属性...
    }
}
