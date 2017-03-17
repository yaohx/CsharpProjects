//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-03-11。
// Description	:	针对单据编辑的状态处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Common {
    /// <summary>
    /// 针对单据编辑的状态处理。
    /// </summary>
    [Flags]
    public enum ObjectState : int {
        /// <summary>
        /// None.
        /// </summary>
        None = 0x0000,
        /// <summary>
        /// 新创建。
        /// </summary>
        New = 0x0001,
        /// <summary>
        /// 保存但已经发生修改。
        /// </summary>
        Modified = 0x0002,
        /// <summary>
        /// 已删除。
        /// </summary>
        Deleted = 0x0004,
        /// <summary>
        /// 已提交。
        /// </summary>
        Validated = 0x0008,
        /// <summary>
        /// 已审核通过。
        /// </summary>
        Approved = 0x0010,
        /// <summary>
        /// 完成
        /// </summary>
        Completed = 0x0020,
        /// <summary>
        /// 撤消
        /// </summary>
        Withdraw = 0x0040,
        /// <summary>
        /// 挂起
        /// </summary>
        Suspended = 0x0080,
        /// <summary>
        /// 已保存 并没有发生过改变。
        /// </summary>
        Unchanged = 0x0100,
        /// <summary>
        /// 表示已进入扩展操作状态。
        /// </summary>
        OverDocState = 0x010000 


    }
}
