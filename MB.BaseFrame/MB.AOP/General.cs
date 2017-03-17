//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	General  系统通过函数。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;

namespace MB.Util {
    /// <summary>
    /// General  系统通过函数。
    /// </summary>
    internal class General {
        /// <summary>
        /// private constract function to prevent instance.
        /// </summary>
        private General() {
        }
        /// <summary>
        /// 获取应用程序的当前目录。
        /// </summary>
        /// <returns></returns>
        public static string GeApplicationDirectory() {
            string rootPath = AppDomain.CurrentDomain.BaseDirectory;//
            string cfgPath = System.Configuration.ConfigurationManager.AppSettings["RelativeSearchPath"];
            if (string.IsNullOrEmpty(cfgPath)) {
                cfgPath = AppDomain.CurrentDomain.RelativeSearchPath;
            }
            if (string.IsNullOrEmpty(cfgPath)) {
                //判断是否在DEBUG 环境下
                if (System.Environment.CurrentDirectory.LastIndexOf(@"\IDE") != System.Environment.CurrentDirectory.Length - 4) {
                    if (System.Environment.CurrentDirectory + @"\" != AppDomain.CurrentDomain.BaseDirectory) {//Windows应用程序则相等{
                        //C:\Windows\system32 windows services
                        if (System.Environment.CurrentDirectory.LastIndexOf(@"\system32") != System.Environment.CurrentDirectory.Length - 9)
                            rootPath += @"Bin\";
                    }
                }
            } else {
                rootPath = cfgPath;
            }

            if (rootPath.LastIndexOf(@"\") < rootPath.Length - 1)
                rootPath += @"\";

            return rootPath;
        }
    }
}
