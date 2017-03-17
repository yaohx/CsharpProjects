//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2010-01-26
// Description	:	单据通用操作菜单项。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MB.WinBase.Common {
    /// <summary>
    /// 单据通用操作菜单项。
    /// </summary>
    [Flags]
    public enum GeneralOperateMenus {
        /// <summary>
        /// None
        /// </summary>
        [Description("None")]
        None = 0x00000,
        /// <summary>
        /// 所有
        /// </summary>
        [Description("所有")]
        All = 0x00001,
        /// <summary>
        /// 审核
        /// </summary>
        [Description("审核")]
        Approved = 0x00002,
        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Completed = 0x00004,
        /// <summary>
        /// 撤消
        /// </summary>
        [Description("撤消")]
        Withdraw = 0x00008,
        /// <summary>
        /// 挂起
        /// </summary>
        [Description("挂起")]
        Suspended = 0x00010,
         /// <summary>
        /// 取消挂起
        /// </summary>
        [Description("取消挂起")]
        CancelSuspended = 0x00020
    }
}
