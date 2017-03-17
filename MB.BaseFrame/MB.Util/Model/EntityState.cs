//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-05
// Description	:	数据库类型。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util.Model {
    /// <summary>
    /// 永久性操作中的实体对象状态。
    /// </summary>
    public enum EntityState { 
        /// <summary>
        ///  瞬时的。
        /// </summary>
        Transient, 
        /// <summary>
        /// 新建。
        /// </summary>
        New, 
        /// <summary>
        /// 已永久存储并已经发生修改。
        /// </summary>
        Modified,
        /// <summary>
        /// 已永久存储。
        /// </summary>
        Persistent, 
        /// <summary>
        /// 已删除。
        /// </summary>
        Deleted 
    } 
}
