using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Atts {
    /// <summary>
    /// 模块调用配置。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = true )]
    public class ModuleInvokeConfigAttribute : Attribute {
        private MB.WinBase.Common.UICommandType _OptType;
        private Type _UIViewType;
        private string _CreatePars;

        /// <summary>
        /// 模块操作配置
        /// </summary>
        /// <param name="optType">操作类型</param>
        /// <param name="uiViewType">窗口浏览类型</param>
        public ModuleInvokeConfigAttribute(MB.WinBase.Common.UICommandType optType, Type uiViewType) {
            _OptType = optType;
            _UIViewType = uiViewType;
            
        }

        /// <summary>
        /// 模块调用操作类型。
        /// </summary>
        public MB.WinBase.Common.UICommandType OptType {
            get { return _OptType; }
            set { _OptType = value; }
        }

        /// <summary>
        /// UI 调用的浏览窗口类型。
        /// </summary>
        public Type UIViewType {
            get { return _UIViewType; }
            set { _UIViewType = value; }
        }

        /// <summary>
        /// 创建参数。
        /// </summary>
        public string CreatePars {
            get { return _CreatePars; }
            set { _CreatePars = value; }
        }

    }
}
