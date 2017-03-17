using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MB.Util.Model {
    /// <summary>
    /// 单据操作类型。
    /// 在数据库中存储对应的int 数值
    /// </summary>
    public enum DocOperateType : int {
        /// <summary>
        /// 录入保存
        /// </summary>
        [Description("录入保存")]
        Save = 0x0000,
        /// <summary>
        /// 确认
        /// </summary>
        [Description("确认")]
        Validated = 0x0001,
        /// <summary>
        /// 审核
        /// </summary>
        [Description("审核")]
        Approved = 0x0002,
        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Completed = 0x0004,
        /// <summary>
        /// 撤消
        /// </summary>
        [Description("撤消")]
        Withdraw = 0x0008,
        /// <summary>
        /// 挂起
        /// </summary>
        [Description("挂起")]
        Suspended = 0x0010,
         /// <summary>
        /// 取消挂起
        /// </summary>
        [Description("取消挂起")]
        CancelSuspended = 0x0020,
        /// <summary>
        /// 表示已进入扩展操作状态。
        /// </summary>
        OverDocState = 0x010000 
    }
}
