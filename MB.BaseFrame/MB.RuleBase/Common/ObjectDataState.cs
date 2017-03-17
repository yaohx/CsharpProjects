//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-03
// Description	:	ObjectDataState 标记数据在集合中的状态。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.RuleBase.Common {
    /// <summary>
    /// ObjectDataState 标记数据在集合中的状态。
    /// </summary>
    public enum ObjectDataState {
        /// <summary>
        /// 表示该数据是刚增加的。
        /// </summary>
        Added, // 
        /// <summary>
        /// 存储在数据库中但已经被删除了。
        /// </summary>
        Deleted,//
        /// <summary>
        /// 不存储在数据库中，在编辑的过程中新增后被删除的。
        /// </summary>
        Detached,//
        /// <summary>
        /// 存储在数据库中但已经被修改了。
        /// </summary>
        Modified,//
        /// <summary>
        /// 没发现任何变化 （从数据库中读取出来的默认值）。
        /// </summary>
        Unchanged//
    }
}
