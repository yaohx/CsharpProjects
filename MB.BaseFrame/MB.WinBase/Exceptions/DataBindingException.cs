//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-16
// Description	:	数据自动绑定异常。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Exceptions {
    /// <summary>
    /// 数据自动绑定异常。
    /// </summary>
    public class DataBindingException : MB.Util.APPException {
        public DataBindingException()
            : base("数据自动绑定出错，请检查是否没有设置数据源!", MB.Util.APPMessageType.SysErrInfo) {
        }
        public DataBindingException(string message)
            : base(message, MB.Util.APPMessageType.SysErrInfo) {
        }
    }
}
