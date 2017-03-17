//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	aifang
// Create date	:	2012-04-11
// Description	:	Rest服务客户端调用通道
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using MB.Util;
using MB.Json;

namespace MB.WinBase.WebClient
{
    /// <summary>
    /// Rest服务客户端调用通道
    /// </summary>
    public class ClientChannel:IDisposable
    {
        private static string WEB_URL_REST_SERVICE_CFG = "RestWebServiceAddress";
        private string _BaseUri;

        /// <summary>
        /// 
        /// </summary>
        public ClientChannel(string serviceName)
        {
            string webUrl = System.Configuration.ConfigurationManager.AppSettings[WEB_URL_REST_SERVICE_CFG];
            _BaseUri = string.Format(ClientChannelFactory.WEB_URL_ROOT_PATH,webUrl, serviceName);
        }


        #region Get...
        /// <summary>
        /// Get调用
        /// </summary>
        /// <param name="methodUrl"></param>
        /// <returns></returns>
        public string Get(string methodUrl)
        {
            //Web访问对象
            string serviceUrl = string.Format("{0}/{1}", _BaseUri, methodUrl);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);

            string returnJson = string.Empty;
            
            // 获得接口返回值
            using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
            {
                using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
                {
                    returnJson = HttpUtility.UrlDecode(reader.ReadToEnd());
                }
            }
            return returnJson;
        }
        /// <summary>
        ///  Get调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodUrl"></param>
        /// <returns></returns>
        public T Get<T>(string methodUrl)
        {
            string re = Get(methodUrl);
            if(string.IsNullOrEmpty(re)) return default(T);

            return Converter.Deserialize<T>(re);
        }
        #endregion ...

        #region post ...
        /// <summary>
        /// Post调用
        /// </summary>
        /// <param name="jsonData"></param>
        /// <param name="methodUrl"></param>
        /// <returns></returns>
        public string Post(string jsonData, string methodUrl)
        {
            //Web访问对象
            string serviceUrl = string.Format("{0}/{1}", _BaseUri, methodUrl);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);

            //转成网络流
            byte[] buf = UnicodeEncoding.UTF8.GetBytes(jsonData);

            //设置
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            myRequest.ContentType = "application/json; charset=utf-8";//"application/x-www-form-urlencoded";//"text/html";

            // 发送请求
            using (Stream newStream = myRequest.GetRequestStream())
            {
                newStream.Write(buf, 0, buf.Length);
            }
            string returnJson = string.Empty;
            // 获得接口返回值
            using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
            {
                using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
                {
                    returnJson = HttpUtility.HtmlDecode(reader.ReadToEnd());
                }
            }
            return returnJson;
        }
        /// <summary>
        /// Post调用
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="data"></param>
        /// <param name="methodUrl"></param>
        /// <returns></returns>
        public TResult Post<TInput, TResult>(TInput data, string methodUrl)
        {
            string jsonData = Converter.Serialize(data);
            string re = Post(jsonData, methodUrl);
            return Converter.Deserialize<TResult>(re);
        }
        #endregion ...

        #region IDisposable 成员

        #region IDisposable...
        private bool disposed = false;

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            //
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {


                }
                disposed = true;
            }
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~ClientChannel()
        {

            Dispose(false);
        }

        #endregion IDisposable...

        #endregion
    }
}
