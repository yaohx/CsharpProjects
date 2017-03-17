using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Common {
    /// <summary>
    /// 数据浏览窗口显示的样式
    /// </summary>
    [Flags]
    public enum  DataViewStyle : int {
        /// <summary>
        /// 普通
        /// </summary>
        General = 0x0001,
        /// <summary>
        /// 多表头
        /// </summary>
        AdvBandGrid = 0x0002,
        /// <summary>
        /// 多维
        /// </summary>
        Multi = 0x0004,
        /// <summary>
        /// 图表分析
        /// </summary>
        Chart = 0x0008,
        /// <summary>
        /// 模块评语
        /// </summary>
        ModuleComment = 0x0010
    }
}
