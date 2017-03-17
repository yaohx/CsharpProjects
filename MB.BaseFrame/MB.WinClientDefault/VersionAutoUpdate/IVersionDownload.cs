//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2009-06-08
// Description	:	版本更新处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinClientDefault.VersionAutoUpdate {
    /// <summary>
    /// UI 客户端版本更新需要实现的接口。
    /// </summary>
    public interface IVersionDownload {
        /// <summary>
        /// 获取当前版本号。
        /// </summary>
        /// <returns></returns>
        double GetServerPackClientVN();
        /// <summary>
        /// 获取需要更新的文件名称。
        /// </summary>
        /// <param name="versionNumber"></param>
        /// <returns></returns>
        string GetUpdateFileNames(double versionNumber);
        /// <summary>
        /// 获取需要更新的文件 Byte.
        /// </summary>
        /// <param name="versionNumber"></param>
        /// <param name="fileName"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        byte[] GetFileBuffer(double versionNumber, string fileName, long position);

    }
}
