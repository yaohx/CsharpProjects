using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace MB.Util.Serializer
{
    /// <summary>
    /// WCF序列化帮助类,包含泛型方法与非泛型方法
    /// </summary>
    public class DataContractSerializeHelper
    {
        /// <summary>
        /// 非泛型方法序列化某对象.该方法通常在基类中使用
        /// </summary>
        /// <param name="t">对象类型</param>
        /// <param name="value">对象实例</param>
        /// <returns>序列化结果</returns>
        public static string Serialize(Type t, object value)
        {
            MemoryStream msTemp = null;

            try
            {
                msTemp = new MemoryStream();

                DataContractSerializer dcsSerializer = new DataContractSerializer(t);

                dcsSerializer.WriteObject(msTemp, value);

                byte[] array = msTemp.ToArray();

                string strValue = Encoding.UTF8.GetString(array, 0, array.Length);

                return strValue;
            }
            catch
            {
                throw;
            }
            finally
            {
                msTemp.Close();
                msTemp.Dispose();
            }
        }

        /// <summary>
        /// 泛型方法序列化某对象.
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">对象实例</param>
        /// <returns>序列化结果</returns>
        public static string Serialize<T>(object value)
        {
            return Serialize(typeof(T), value);
        }

        /// <summary>
        /// 泛型方法反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">序列化字符串</param>
        /// <returns>对象实例</returns>
        public static T Deserialize<T>(string value)
        {
            return (T)Deserialize(typeof(T), value);
        }

        /// <summary>
        /// 非泛型方法反序列化
        /// </summary>
        /// <param name="t">对象类型</param>
        /// <param name="value">序列化字符串</param>
        /// <returns>对象实例</returns>
        public static object Deserialize(Type t, string value)
        {
            MemoryStream msTemp = null;

            try
            {
                DataContractSerializer dcsSerializer = new DataContractSerializer(t);

                msTemp = new MemoryStream(Encoding.UTF8.GetBytes(value));

                return dcsSerializer.ReadObject(msTemp);
            }
            catch
            {
                throw;
            }
            finally
            {
                msTemp.Close();
                msTemp.Dispose();
            }
        }
    }
}
