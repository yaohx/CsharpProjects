using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinTestProject.Atts {
    /// <summary>
    /// 动态控件创建绑定配置
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EditControlTypeAttribute : Attribute {
        private Type _FormCtlType;
        private Type _GridColumnEditCtlType;

        public EditControlTypeAttribute(Type formCtlType) {
            _FormCtlType = formCtlType;
        }

        /// <summary>
        /// 绑定编辑的控件类型。
        /// </summary>
        public Type FormCtlType {
            get {
                return _FormCtlType;
            }
            set {
                _FormCtlType = value;
            }
        }
        /// <summary>
        /// 网格编辑列对应的控件类型。
        /// </summary>
        public Type GridColumnEditCtlType {
            get {
                return _GridColumnEditCtlType;
            }
            set {
                _GridColumnEditCtlType = value;
            }
        }
    }
}
