//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-08-25
// Description	:	模块发表评论处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MB.WinClientDefault.Common {
    /// <summary>
    /// 模块发表评论处理相关。
    /// </summary>
    public class BfModuleCommentHelper {
        private static readonly string APP_CONFIG_KEY_NAME = "BfModuleComment";

        /// <summary>
        /// 客户端模块评语实现客户端。
        /// </summary>
        /// <returns></returns>
        public MB.WinBase.IFace.IBfModuleCommentClient CreateCommentClient() {
            string cfgSetting = System.Configuration.ConfigurationManager.AppSettings[APP_CONFIG_KEY_NAME];
            if (string.IsNullOrEmpty(cfgSetting)) return null;
            string[] cfgs = cfgSetting.Split(',');
            object instance = MB.Util.DllFactory.Instance.LoadObject(cfgs[0], cfgs[1]);
            if (instance == null)
                throw new MB.Util.APPException(string.Format("创建{0} , {1} 有误,请检查",cfgs[0],cfgs[1]));

            MB.WinBase.IFace.IBfModuleCommentClient commentClient = instance as MB.WinBase.IFace.IBfModuleCommentClient;
            if (commentClient == null)
                throw new MB.Util.APPException("客户端模块评语实现客户端需要实现 MB.WinBase.IFace.IBfModuleCommentClient 接口");

            return commentClient;
        }
    }
}
