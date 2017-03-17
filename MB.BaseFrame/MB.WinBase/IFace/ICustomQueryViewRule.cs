using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MB.WinBase.Common;

namespace MB.WinBase.IFace {
    /// <summary>
    /// 对于想要自动以查询结果的窗口，需要实现的rule
    /// </summary>
    public interface ICustomQueryViewRule {
        /// <summary>
        /// 创建自定义的控件，返回的控件需要从System.Windows.Forms.Control继承而来
        /// </summary>
        /// <returns></returns>
        CustomViewType CustomViewType { get; }
    }

}
