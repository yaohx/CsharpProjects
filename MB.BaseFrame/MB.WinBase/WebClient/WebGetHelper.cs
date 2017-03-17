//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	aifang
// Create date	:	2012-04-11
// Description	:	Rest服务客户端调用封装
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.WinBase.Atts;
using System.Reflection;

namespace MB.WinBase.WebClient
{
    /// <summary>
    /// Rest服务客户端调用封装
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WebGetHelper<T>
    {
        private static string _ServiceName = "";
        private static WebGetHelper<T> _NewInstance = new WebGetHelper<T>();
        /// <summary>
        /// 实例化对象
        /// </summary>
        public static WebGetHelper<T> NewInstance
        {
            get
            {
                if (_NewInstance == null) _NewInstance = new WebGetHelper<T>();

                if (string.IsNullOrEmpty(_ServiceName))
                {
                    System.Reflection.MemberInfo info = typeof(T);
                    ServiceNameAttribute s = (ServiceNameAttribute)Attribute.GetCustomAttribute(info, typeof(ServiceNameAttribute));
                    _ServiceName = s.Name;
                }

                return _NewInstance;
            }
        }

        /// <summary>
        /// Rest服务不带TInput参数的Invoke方法
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="method"></param>
        /// <param name="objParams"></param>
        /// <returns></returns>
        public TResult InvokeWebMethod<TResult>(string method, params object[] objParams)
        {
            try
            {
                using (ClientChannel scope = new ClientChannel(_ServiceName))
                {
                    try
                    {
                        string methodUrl = GetMethodUrl(method, objParams);

                        return (TResult)scope.Get<TResult>(methodUrl);
                    }
                    catch (Exception ex)
                    {
                        MB.Util.TraceEx.Write(string.Format("调用服务{0} 方法出错,请查阅服务端日志 " + ex.Message, _ServiceName), MB.Util.APPMessageType.SysErrInfo);
                        throw new MB.Util.APPException(ex.Message, MB.Util.APPMessageType.SysErrInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write(string.Format("调用服务{0} 方法出错,请查阅服务端日志 " + ex.Message, _ServiceName), MB.Util.APPMessageType.SysErrInfo);
                throw new MB.Util.APPException(ex.Message, MB.Util.APPMessageType.SysErrInfo);
            }
        }

        /// <summary>
        /// Rest服务带TInput参数的Invoke方法
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="method"></param>
        /// <param name="objParams"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public TResult InvokeWebMethod<TInput, TResult>(string method, TInput data,params object[] objParams)
        {
            try
            {
                using (ClientChannel scope = new ClientChannel(_ServiceName))
                {
                    try
                    {
                        string methodUrl = GetMethodUrl(method, objParams);

                        return (TResult)scope.Post<TInput, TResult>(data, methodUrl);
                    }
                    catch (Exception ex)
                    {
                        MB.Util.TraceEx.Write(string.Format("调用服务{0} 方法出错,请查阅服务端日志 " + ex.Message, _ServiceName), MB.Util.APPMessageType.SysErrInfo);
                        throw new MB.Util.APPException(ex.Message, MB.Util.APPMessageType.SysErrInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write(string.Format("调用服务{0} 方法出错,请查阅服务端日志 " + ex.Message, _ServiceName), MB.Util.APPMessageType.SysErrInfo);
                throw new MB.Util.APPException(ex.Message, MB.Util.APPMessageType.SysErrInfo);
            }
        }

        private string GetMethodUrl(string method, object[] objParams)
        {
            string methodUrl = "";
            MemberInfo[] arr = typeof(T).GetMethods();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Name.Equals(method))
                {
                    MappingAttribute mapUrl = (MappingAttribute)arr[i].GetCustomAttributes(typeof(MappingAttribute), false)[0];
                    methodUrl = mapUrl.MethodUrl;
                    break;
                }
            }

            for (int i = 0; i < objParams.Length; i++)
            {
                methodUrl = methodUrl.Replace("{" + i + "}", objParams[i].ToString());
            }
            return methodUrl;
        }


    }
}
