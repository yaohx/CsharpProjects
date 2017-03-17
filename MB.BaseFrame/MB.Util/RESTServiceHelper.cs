using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using MB.Util;

namespace MB.Util
{
    /// <summary>
    /// REST Service访问帮助类
    /// </summary>
    public class RESTServiceHelper
    {

        public RESTServiceHelper()
        {
        }

        /// <summary>
        /// 通过HTTP GET的方式访问RESTFUL服务并获取Response
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public TResponse Get<TResponse>(string serviceUrl)
        {
            TResponse result = default(TResponse);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);

            // 获得接口返回值
            HttpWebResponse myResponse = null;

            myResponse = (HttpWebResponse)myRequest.GetResponse();
            using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
            {
                string responseString = reader.ReadToEnd();
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(TResponse));
                result = jsonSerializer.Deserialize<TResponse>(responseString);
            }
           

            return result;
        }

        public TResponse PostAsPlainText<TResponse>(string serviceUrl, byte[] buf)
        {
            return this.Post<TResponse>(serviceUrl, buf, "application/text");
        }

        public TResponse PostDataAsJson<TRequest, TResponse>(string serviceUrl, TRequest data)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(TRequest));
            byte[] buf = UnicodeEncoding.UTF8.GetBytes(jsonSerializer.Serializer<TRequest>(data));
            return Post<TResponse>(serviceUrl, buf, "application/json");
        }

        /// <summary>
        /// 通过HTTP POST的方式访问RESTFUL服务
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="data"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public TResponse Post<TResponse>(string serviceUrl, byte[] buf, string contentType)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //设置
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            myRequest.ContentType = contentType;

            // 发送请求
            using (Stream newStream = myRequest.GetRequestStream())
            {
                newStream.Write(buf, 0, buf.Length);
            }

            TResponse result;

            try
            {
                // 获得接口返回值
                using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(TResponse));
                        string responseString = reader.ReadToEnd();
                        jsonSerializer = new DataContractJsonSerializer(typeof(TResponse));
                        result = (TResponse)jsonSerializer.Deserialize<TResponse>(responseString);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write(string.Format("服务{0}访问失败,{1}", serviceUrl, ex.Message));
                throw new MB.Util.APPException(string.Format("服务{0}访问失败,{1}",serviceUrl, ex.Message));
            }
        }



        

        
    }


    /// <summary>
    /// 该类是对DataContractJsonSerializer一个扩展
    /// </summary>
    public static class DataContractJsonSerializerExternsion
    {
        /// <summary>
        /// Json序列化
        /// </summary>
        public static string Serializer<T>(this DataContractJsonSerializer serializer, T data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, data);
                StringBuilder sb = new StringBuilder();
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                return sb.ToString();
            }
        }

        /// <summary>
        /// Json反序列化
        /// </summary>
        public static T Deserialize<T>(this DataContractJsonSerializer serializer, string jsonString)
        {
            T jsonObject = default(T); 
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                jsonObject = (T)serializer.ReadObject(ms);
            }

            return jsonObject;

        }
    }
}
