//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	aifang
// Create date	:	2012-04-11
// Description	:	 Rest服务名称
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MB.WinBase.Atts
{
    /// <summary>
    /// Rest服务名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, Inherited = false)]
    public sealed class ServiceNameAttribute : System.Attribute
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; set; }
    }
}
