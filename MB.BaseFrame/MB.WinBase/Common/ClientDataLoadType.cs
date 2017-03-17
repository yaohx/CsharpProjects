//---------------------------------------------------------------- 
// Copyright (C) 2008-2012 www.metersbonwe.com
// All rights reserved. 
// Author		:	aifang
// Create date	:	2012-08-01
// Description	:	客户端数据加载方式。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MB.WinBase.Common
{
    public enum ClientDataLoadType:int
    {
        /// <summary>
        /// 默认值：缓存加载
        /// </summary>
        [Description("默认值")]
        Default = 0x000001,

        /// <summary>
        /// 本地缓存加载方式
        /// </summary>
        [Description("缓存加载")]
        Cache = 0x000002,

        /// <summary>
        /// 实时读取数据加载方式
        /// </summary>
        [Description("实时加载")]
        ReLoad = 0x000003
    }
}
