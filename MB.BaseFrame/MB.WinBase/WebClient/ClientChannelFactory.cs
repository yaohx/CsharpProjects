//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	aifang
// Create date	:	2012-04-11
// Description	:	Web服务客户端调用通道
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.WebClient
{
    /// <summary>
    /// Web服务客户端调用通道
    /// </summary>
    public class ClientChannelFactory
    {
        private static Dictionary<Type, string> _WcfInvokeContainer = new Dictionary<Type, string>();
        /// <summary>
        /// Web Url地址
        /// </summary>
        public static string WEB_URL_ROOT_PATH = "http://{0}/{1}.svc";

        /// <summary>
        /// WCF 服务调用注册.
        /// </summary>
        /// <param name="iServiceType"></param>
        /// <param name="relativeUrlPath"></param>
        public static void RegisterTypeIfMissing(Type iServiceType, string relativeUrlPath)
        {
            if (!_WcfInvokeContainer.ContainsKey(iServiceType))
            {
                _WcfInvokeContainer.Add(iServiceType, relativeUrlPath);
            }
        }

        /// <summary>
        /// 反射调用方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="t"></param>
        /// <param name="execute"></param>
        /// <returns></returns>
        public static TResult InvokeWebMethod<T, TResult>(T t,Func<T, TResult> execute)
        {
            return execute(t); ;
        }
    }
}
