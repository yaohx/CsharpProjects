//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-06-05
// Description	:	基于Wcf 服务的版本自动更新。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace MB.WcfService.IFace {
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IApplicationVersion {
        /// <summary>
        /// 获取当前正在活动的版本号。
        /// </summary>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))]
        [OperationContract]
        double CurrentActiveVersion();
        /// <summary>
        /// 当前版本包含的文件名称。
        /// </summary>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))]
        [OperationContract]
        string CurrentVersionFileNames(double versionNumber);

        /// <summary>
        /// 获取本次需要下载的数据块总数。
        /// </summary>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))]
        [OperationContract]
        byte[] GetFileBuffer(double versionNumber, string fileName, long position);
    }
}
