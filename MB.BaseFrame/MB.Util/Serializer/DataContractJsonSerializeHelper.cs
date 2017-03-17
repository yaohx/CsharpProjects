using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace MB.Util.Serializer {
    public class DataContractJsonSerializeHelper {

        /// <summary>
        /// Json序列化
        /// </summary>
        /// <param name="data">需要序列化的数据</param>
        public static string Serializer<T>(T data) {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream()) {
                serializer.WriteObject(ms, data);
                StringBuilder sb = new StringBuilder();
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                return sb.ToString();
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonString) {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            T jsonObject = default(T);
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString))) {
                jsonObject = (T)serializer.ReadObject(ms);
            }

            return jsonObject;

        }
    }
}
