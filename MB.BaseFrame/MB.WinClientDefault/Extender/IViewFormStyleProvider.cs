//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2009-06-08
// Description	:	网格浏览界面扩展处理接口。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.WinBase.IFace;
namespace MB.WinClientDefault.Extender {
    /// <summary>
    /// 网格浏览界面扩展处理接口。
    /// </summary>
    public interface IViewFormStyleProvider {
        /// <summary>
        /// 浏览界面中浏览网格控件的个性化处理。
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="rowData"></param>
        /// <param name="e"></param>
        void RowCellStyle(IViewGridForm parentForm, DevExpress.XtraGrid.Views.Grid.GridView gridView,DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e);
    }
}
