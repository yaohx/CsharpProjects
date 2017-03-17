//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	可进行方面注入的对象需要继承的基类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Aop {

    /// <summary>
    /// 可进行方面注入的对象需要继承的基类。
    /// 从扩展方面的考虑。
    /// </summary>
    public class AspectableObject : ContextBoundObject  {
    }
}
