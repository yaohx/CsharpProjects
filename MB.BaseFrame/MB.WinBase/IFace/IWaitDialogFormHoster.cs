using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.WinBase.Common;
namespace MB.WinBase.IFace {
    /// <summary>
    /// 等待窗口调用的需要实现的接口
    /// </summary>
    public interface IWaitDialogFormHoster {
        /// <summary>
        /// 异步调用等待处理窗口需要接收的状态参数。
        /// </summary>
        WorkWaitDialogArgs ProcessState { get; }
    }
}
