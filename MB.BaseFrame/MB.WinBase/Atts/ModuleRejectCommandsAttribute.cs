using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Atts {
    /// <summary>
    /// 模块拒绝操作的菜单项。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModuleRejectCommandsAttribute : System.Attribute {

        private MB.WinBase.Common.UICommandType _RejectCommands;
        /// <summary>
        /// 配置针对该模块不能进行操作的菜单项。
        /// </summary>
        /// <param name="rejectCommands"></param>
        public ModuleRejectCommandsAttribute(MB.WinBase.Common.UICommandType rejectCommands) {
            _RejectCommands = rejectCommands;
        }
        /// <summary>
        /// 配置针对该模块不能进行操作的菜单项。
        /// </summary>
        public MB.WinBase.Common.UICommandType RejectCommands {
            get { return _RejectCommands; }
            set { _RejectCommands = value; }
        }  
    }
}
