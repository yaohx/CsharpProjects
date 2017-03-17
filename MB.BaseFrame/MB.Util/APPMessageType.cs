//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	APPMessageType 出错提示信息的等级
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MB.Util {
    /// <summary>
    /// 出错提示信息的等级
    /// </summary>
    public enum APPMessageType {
        [Description("系统异常")]
        SysErrInfo, //
        [Description("数据库操作异常")]
        SysDatabaseInfo,//
        [Description("硬盘文件操作异常")]
        SysFileInfo, //
        [Description("显示给用户的异常信息")]
        DisplayToUser, //显示给用户的异常信息
        [Description("代码运行的信息")]
        CodeRunInfo, // 代码运行的信息 供开发人员做调试用
        [Description("系统警告")]
        SysWarning, //系统警告
        [Description("其它系统异常")]
        OtherSysInfo, //其它系统异常
        [Description("数据验证")]
        DataInvalid
    }
}
