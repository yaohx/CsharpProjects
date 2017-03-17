//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-05
// Description	:	
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Mapping.Att {
  
    /// <summary>
    /// ExclusiveAttribute指示相关的属性不需要映射到数据库
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExclusiveAttribute : Attribute { }
}
