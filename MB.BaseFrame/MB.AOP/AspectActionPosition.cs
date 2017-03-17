//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	Aspect拦截的位置，包括在方法执行前、执行后和全部
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
namespace MB.Aop {

    /// <summary>
    /// Aspect拦截的位置，包括在方法执行前、执行后和全部
    /// </summary>
    public enum AspectActionPosition { 
        /// <summary>
        /// 在方法的开始位置拦截。
        /// </summary>
        Before, 
        /// <summary>
        /// 在方法调用完成后拦截。
        /// </summary>
        After, 
        /// <summary>
        /// 在开始和结束的位置都需要拦截。
        /// </summary>
        Both 
    }
}

