//---------------------------------------------------------------- 
// Copyright (C) 2008-2008 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	TraceEx 程序代码跟踪
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;

namespace MB.Aop {
    /// <summary>
    /// 定义一个需要注入的接口
    /// </summary>
    public interface  IInjection {
        /// <summary>
        /// 在被拦截的方法执行前执行的方法
        /// </summary>
        /// <param name="msg">IMessage，包含有关方法调用的信息。</param>
        void BeginProcess(IMessage msg);

        /// <summary>
        /// 在被拦截的方法执行后执行的方法
        /// </summary>
        /// <param name="msg">IMessage，包含有关方法调用的信息。</param>
        void EndProcess(DateTime beginExecute, IMessage msg);
    }
}
