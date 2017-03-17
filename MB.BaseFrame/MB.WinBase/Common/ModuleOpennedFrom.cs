using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Common {
    /// <summary>
    /// 枚举菜单是从哪里被打开，根据不同打开位置，有不同的处理
    /// </summary>
    public enum ModuleOpennedFrom {
        /// <summary>
        /// 从菜单打开，例如从ERP属性菜单，桌面菜单等
        /// </summary>
        Menu,
        /// <summary>
        /// 从任务栏打开，任务栏打开的任务
        /// 这种情况可以在ClientRule中设定默认的查询条件，以便于打开时直接加载数据
        /// </summary>
        Task,
    }
}
