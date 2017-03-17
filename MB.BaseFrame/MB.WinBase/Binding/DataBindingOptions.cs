//---------------------------------------------------------------- 
// 
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-20
// Description	:	控件数据绑定的选项设置。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Binding {
    /// <summary>
    /// 控件数据绑定的选项设置。
    /// </summary>
    [Flags] 
    public enum  DataBindingOptions {
        /// <summary>
        /// 没有设置任何选项。
        /// </summary>
        None = 0x0000,
        /// <summary>
        /// 判断是否为只读的方式进行绑定。
        /// </summary>
        ReadOnly = 0x0001,
        /// <summary>
        /// 当前的绑定可以进行编辑。
        /// </summary>
        Edit = 0x0002
    }
}
