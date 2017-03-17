//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-16
// Description	:	创建数据绑定的类型。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Common {
    /// <summary>
    /// 创建数据绑定的类型。
    /// </summary>
    public enum DataBindingType {
        /// <summary>
        /// 没有设置数据绑定，或者用户直接通过代码来进行赋值。
        /// </summary>
        None,
        /// <summary>
        /// 通过控件名称自动创建数据绑定。
        /// 默认情况下为这种模式。
        /// </summary>
        AutoByCtlName,
        /// <summary>
        /// 通过XML 配置来创建自动绑定。
        /// </summary>
        FromXmlCfg,
        /// <summary>
        /// 通过用户UI 设计手工来绑定。
        /// </summary>
        ByUserUIDesign,
        /// <summary>
        /// 其它设置方法。
        /// </summary>
        Other
    }
}
