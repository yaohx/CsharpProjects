//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	General  系统通过函数。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util.Emit {
    /// <summary>
    ///  动态属性获取异常。
    /// </summary>
    public class DynamicPropertyAccessorException : APPException {
        /// <summary>
        /// 动态属性获取异常。
        /// </summary>
        /// <param name="message"></param>
        public DynamicPropertyAccessorException(string message)
            : base(message) {
        }
    }
}
