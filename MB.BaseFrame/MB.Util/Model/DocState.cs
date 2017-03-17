using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel; 

namespace MB.Util.Model {
    /// <summary>
    /// 单据编辑状态。
    /// </summary>
    [Flags]
    public enum DocState : int {
        /// <summary>
        /// 录入中。
        /// </summary>
        [Description("录入中")] 
        Progress = 0x000000,
        /// <summary>
        /// 已确认。
        /// </summary>
        [Description("已确认")]
        Validated = 0x000001,
        /// <summary>
        /// 已审核通过。
        /// </summary>
        [Description("已审核")]
        Approved = 0x000002,
        /// <summary>
        ///已完成
        /// </summary>
        [Description("已完成")]
        Completed = 0x000004,
        /// <summary>
        /// 已撤消
        /// </summary>
        [Description("已撤消")]
        Withdraw = 0x00008,
        /// <summary>
        /// 已挂起
        /// </summary>
        [Description("已挂起")]
        Suspended =0x000010

    }
}
