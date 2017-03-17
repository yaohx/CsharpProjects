//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-16
// Description	:	客户端UI 类型。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Common {
    /// <summary>
    /// 客户端UI 类型。
    /// </summary>
    public enum ClientUIType {
        /// <summary>
        /// 通用网格浏览的操作界面。
        /// </summary>
        GridViewForm,
        /// <summary>
        /// 窗口编辑的操作界面。
        /// </summary>
        ObjectEditForm, 
        /// <summary>
        /// 查询编辑界面。
        /// </summary>
        QueryFilter,
        /// <summary>
        /// 独立模式对话窗口。
        /// </summary>
        ShowModelForm,
        /// <summary>
        /// Mdi 编辑主窗口。
        /// </summary>
        MdiMainForm, 
        /// <summary>
        /// 批量数据导入处理窗口
        /// </summary>
        BatchDataImport,
        /// <summary>
        /// 异步查询分析浏览窗口。
        /// </summary>
        AsynViewForm,
        /// <summary>
        /// 树型浏览窗口。
        /// </summary>
        TreeListViewForm,
        /// <summary>
        /// 基于MDI 子窗口的网格浏览编辑窗口。
        /// </summary>
        GridViewEditForm,
        /// <summary>
        /// 其它类型窗口。
        /// </summary>
        Other
    }
}
