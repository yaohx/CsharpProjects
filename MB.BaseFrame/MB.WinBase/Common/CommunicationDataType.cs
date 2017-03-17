//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-17
// Description	:	层之间数据传递的类型。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Common {
    /// <summary>
    /// 层之间数据传递的类型。
    /// </summary>
    [Flags] 
    public enum CommunicationDataType : int  {
        /// <summary>
        /// 通过DataSet 来进行传递。
        /// </summary>
        DataSet = 0x0001,
        /// <summary>
        /// 通过数据实体来进行传递。
        /// </summary>
        ModelEntity = 0x0002,
        /// <summary>
        /// 在数据传递中是否需要压缩。
        /// </summary>
        DataCompression = 0x0004
    }
}
