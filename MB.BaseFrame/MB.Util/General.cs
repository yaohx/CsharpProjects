//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	General  系统通过函数。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Windows.Forms;
using System.Management;
using System.Runtime.InteropServices;
using System.Diagnostics;
using MB.Platform.Common;
using MB.Platform.Logging;

namespace MB.Util {
    /// <summary>
    /// General  系统通过函数。
    /// </summary>
    [System.Diagnostics.DebuggerStepThrough()]
    public sealed  class General {
        private static string _LastGetClientIp;
        /// <summary>
        /// private constract function to prevent instance.
        /// </summary>
        private General() {
            
        }

        static General()
        {
            s_logger = LoggerManager.GetLoggerForType(typeof(General));
        }

        private static ILogger s_logger = null;

        /// <summary>
        /// 判断当前界面是否在设计状态。
        /// 备注： Control.DesigeMode 有 BUG
        /// </summary>
        /// <returns></returns>
        public static bool IsInDesignMode() {
            string exePath = Application.ExecutablePath;
            exePath = exePath.ToLower();

            if (Application.ExecutablePath.ToLower().IndexOf("devenv.exe") > -1) {
                return true;
            }
            else {
                return false;
            }

        }
        #region CreateSystemType...
        /// <summary>
        /// 创建系统类型。
        /// 默认情况下不支持Nullable Type
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        public static Type CreateSystemType(string typeFullName) {
            return CreateSystemType(typeFullName, false);
        }
        /// <summary>
        /// 创建系统类型。
        /// 默认情况下不支持NullAbleTalbe
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <param name="supportNullable">判断是否支持Nullable 类型 </param>
        /// <returns></returns>
        public static Type CreateSystemType(string typeFullName, bool supportNullable) {
            if (string.IsNullOrEmpty(typeFullName)) return null;

            if (typeFullName.IndexOf("?") == typeFullName.Length - 1) {
                string name = typeFullName.Replace("?", "");
                if (supportNullable) {
                    name = string.Format(MB.BaseFrame.SOD.NULLABLE_VALUE_TYPE, name);
                }
                return System.Type.GetType(name);
            }
            else
                return System.Type.GetType(typeFullName);
        }
        #endregion CreateSystemType...
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
        /// 获取路径2相对于路径1的相对路径
        /// </summary>
        /// <param name="strPath1">路径1</param>
        /// <param name="strPath2">路径2</param>
        /// <returns>返回路径2相对于路径1的路径</returns>
        /// <example>
        /// string strPath = GetRelativePath(@"C:\WINDOWS\system32", @"C:\WINDOWS\system\*.*" );
        /// //strPath == @"..\system\*.*"
        /// </example>
        public static string GetRelativePath(string strPath1, string strPath2) {
            if (!strPath1.EndsWith("\\")) strPath1 += "\\";    //如果不是以"\"结尾的加上"\"
            int intIndex = -1, intPos = strPath1.IndexOf('\\');
            ///以"\"为分界比较从开始处到第一个"\"处对两个地址进行比较,如果相同则扩展到
            ///下一个"\"处;直到比较出不同或第一个地址的结尾.
            while (intPos >= 0) {
                intPos++;
                if (string.Compare(strPath1, 0, strPath2, 0, intPos, true) != 0) break;
                intIndex = intPos;
                intPos = strPath1.IndexOf('\\', intPos);
            }

            ///如果从不是第一个"\"处开始有不同,则从最后一个发现有不同的"\"处开始将strPath2
            ///的后面部分付值给自己,在strPath1的同一个位置开始望后计算每有一个"\"则在strPath2
            ///的前面加上一个"..\"(经过转义后就是"..\\").
            if (intIndex >= 0) {
                strPath2 = strPath2.Substring(intIndex);
                intPos = strPath1.IndexOf("\\", intIndex);
                while (intPos >= 0) {
                    strPath2 = "..\\" + strPath2;
                    intPos = strPath1.IndexOf("\\", intPos + 1);
                }
            }
            //否则直接返回strPath2
            return strPath2;
        }
        #region 获取计算机硬件信息...
        /// <summary>
        /// 该方法只能在服务端使用。
        /// 获取请求的服务IP;
        /// </summary>
        /// <returns></returns>
        public static string GetRequestIP() {
            if (System.ServiceModel.OperationContext.Current == null)
            {
                int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

                s_logger.Info("GetRequestIP() return [localhost]");

                return "localhost" + threadId.ToString();
            }

            try {
                var msgPropertys = System.ServiceModel.OperationContext.Current.IncomingMessageProperties;
                if (msgPropertys == null || !msgPropertys.ContainsKey(System.ServiceModel.Channels.RemoteEndpointMessageProperty.Name))
                {
                    s_logger.Info("GetRequestIP() return [localhost]");
                    return "localhost";
                }

                string ipAddr = string.Empty;
                string ipInfo = string.Empty;
                ipAddr = GetClientIPAddress(out ipInfo);
                s_logger.Info(ipInfo);
                s_logger.InfoFormat("GetClientIPAddress(out ipInfo) return [{0}].", new object[] { ipAddr });

                if (!string.IsNullOrEmpty(ipAddr))
                {                   
                    _LastGetClientIp = ipAddr;
                    return _LastGetClientIp;
                }
                                
                System.ServiceModel.Channels.RemoteEndpointMessageProperty endPoint =
                        msgPropertys[System.ServiceModel.Channels.RemoteEndpointMessageProperty.Name] as System.ServiceModel.Channels.RemoteEndpointMessageProperty;

                _LastGetClientIp = endPoint.Address;
                return _LastGetClientIp;

                //只获取IPV4 格式 （影响性能，需要设计一个HashTable来缓存才可以）
                //if (remoteIP.IndexOf(":") < -1) {
                //    IPAddress[] addresses = Dns.GetHostAddresses(remoteIP);
                //    for (int i = 0; i < addresses.Length; i++) {
                //        if (addresses[i].ToString().IndexOf(".") > -1)
                //            return addresses[i].ToString();
                //    }
                //    return remoteIP;
                //} else {
                //    return remoteIP;
                //}
            }
            catch {
                return  "*" + _LastGetClientIp + "*";
            }
        }

        private static string GetClientIPAddress(out string ipInfo)
        {
            string ipAddr = string.Empty;
            string temp = string.Empty;

            ipInfo = string.Empty;

            temp = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(temp))
            {
                ipAddr = temp;
                ipInfo = string.Format("HTTP_X_FORWARDED_FOR:{0}", temp);                
                return ipInfo;
            }

            temp = System.Web.HttpContext.Current.Request.Headers["X-Forwarded-For"];
            if (!string.IsNullOrEmpty(temp))
            {
                ipAddr = temp;
                ipInfo = string.Format("X-Forwarded-For:{0}", temp);
                return ipInfo;
            }

            temp = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (!string.IsNullOrEmpty(temp))
            {
                ipAddr = temp;
                ipInfo = string.Format("REMOTE_ADDR:{0}", temp);
                return ipInfo;
            }

            temp = System.Web.HttpContext.Current.Request.UserHostAddress;
            if (!string.IsNullOrEmpty(temp))
            {
                ipAddr = temp;
                ipInfo = string.Format("UserHostAddress:{0}", temp);
                return ipInfo;
            }

            ipInfo = "Get IP Address failed!";

            return ipAddr;
        }

        /// <summary>
        /// 获取当前计算机IP地址。
        /// </summary>
        /// <returns></returns>
        public static  string LocalhostIPAddress() {
            try {
                string st = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc) {
                    if ((bool)mo["IPEnabled"] == true) {
                        System.Array ar = (System.Array)(mo.Properties["IpAddress"].Value);
                        st = ar.GetValue(0).ToString();
                        break;
                    }
                }
                return st;
            }
            catch {
                return "unknow";
            }
            finally {
            }
        }
        #endregion 获取计算机硬件信息...
 


    }

    
}
