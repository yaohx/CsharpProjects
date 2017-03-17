//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-05
// Description	:	AutoIncreaseAttribute指示该属性是自动增长的。。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Mapping.Att {
    /// <summary>
    ///AutoIncreaseAttribute指示该属性是自动增长的。
    ///自动增长默认种子为1
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoIncreaseAttribute : Attribute  {
        private int step = 1;

        public AutoIncreaseAttribute() { }

        public AutoIncreaseAttribute(int step) {
            this.step = step;
        }

        public int Step {
            get { return step; }
            set { step = value; }
        }
    }
}
