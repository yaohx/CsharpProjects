//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	aifang
// Create date	:	2012-04-11
// Description	:	 Rest调用方法Url配置
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MB.WinBase.Atts
{
    /// <summary>
    /// Rest调用方法Url配置
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface, Inherited = false)]
    public sealed class MappingAttribute : System.Attribute
    {
        /// <summary>
        /// Rest调用方法Url
        /// </summary>
        public string MethodUrl { get; set; }
    }
}
