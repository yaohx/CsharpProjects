using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace MB.Util.Serializer {
    /// <summary>
    /// oapSerializer 提供 SoapSerializer 的方法系列化操作对象。
    /// </summary>
    [MB.Aop.InjectionManager]  
    public class SoapSerializer : System.ContextBoundObject {
        #region construct function...
        /// <summary>
        /// add private construct function to prevent instance.
        /// </summary>
        public SoapSerializer() {
           // System.Runtime.Serialization.Formatters.Binary.BinaryFormatter 

        }
        #endregion construct function...
        /// <summary>
        /// 默认的实例。
        /// </summary>
        public static SoapSerializer DefaultInstance {
            get {
                return SingletonProvider<SoapSerializer>.Instance;
            }
        }

        #region 系列化byte 数组相关...
        /// <summary>
        /// 系列化到Stream.
        /// </summary>
        /// <param name="objectInstance">需要进行Soap 系列化的对象</param>
        /// <returns></returns>
        public MemoryStream SerializerToStream(object objectInstance) {
            MemoryStream ms = new MemoryStream();
            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            SoapFormatter formatter = new SoapFormatter();
            try {
                formatter.Serialize(ms, objectInstance);
                return ms;
            }
            catch (SerializationException e) {
                //Log.Write("ServiceNode:Exception","Failed to serialize. Reason: " + e.Message);
                throw e;
            }
         
        }
        /// <summary>
        /// 把byte数组 反系列化为对应的object 对象。
        /// </summary>
        /// <param name="msData">需要反系列化的对象Stream</param>
        /// <returns></returns>
        public object Deserialize(MemoryStream msData) {
            // Open the file containing the data that you want to deserialize.
            SoapFormatter formatter;
            try {
                formatter = new SoapFormatter();
                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                object o = formatter.Deserialize(msData);
                return o;
            }
            catch (SerializationException e) {
                throw e;
            }
        
        }

        /// <summary>
        /// 把对象系列化为byte数组的格式。
        /// </summary>
        /// <param name="objectInstance"></param>
        /// <returns></returns>
        public byte[] SerializerToByte(object objectInstance) {
            // To serialize the hashtable and its key/value pairs,  
            // you must first open a stream for writing. 
            // In this case, use a file stream.
            //FileStream fs = new FileStream("DataFile.xml", FileMode.Create);
            Stream ms = new MemoryStream();
            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            SoapFormatter formatter = new SoapFormatter();
            try {
                formatter.Serialize(ms, objectInstance);

                byte[] b = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(b, 0, b.Length);
                return b;
            }
            catch (SerializationException e) {
                //Log.Write("ServiceNode:Exception","Failed to serialize. Reason: " + e.Message);
                throw e;
            }
            finally {
                ms.Close();
            }
        }
        /// <summary>
        /// 把byte数组 反系列化为对应的object 对象。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public object DeserializeByByte(byte[] data) {
            // Open the file containing the data that you want to deserialize.
            SoapFormatter formatter;
            MemoryStream ms = null;
            try {
                formatter = new SoapFormatter();

                ms = new MemoryStream(data);

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                object o = formatter.Deserialize(ms);
                return o;
            }
            catch (SerializationException e) {
                throw e;
            }
            finally {
                ms.Close();
            }
        }
        #endregion 系列化byte 数组相关...

        #region 系列化字符窜相关...
        /// <summary>
        /// Soap格式的序列化
        /// </summary>
        /// <param name="objectInstance"></param>
        /// <returns></returns>
        public string Serializer(object objectInstance) {
            try {
                byte[] b = SerializerToByte(objectInstance);
                string s = Convert.ToBase64String(b);
                return s;
            }
            catch (SerializationException e) {
                throw e;
            }
        }
        /// <summary>
        /// Soap格式的反序列化
        /// </summary>
        /// <param name="returnString"></param>
        /// <returns></returns>
        public object Deserialize(string objectSerializeString) {
            // Open the file containing the data that you want to deserialize.
            SoapFormatter formatter;
            MemoryStream ms = null;
            try {
                formatter = new SoapFormatter();

                byte[] b = Convert.FromBase64String(objectSerializeString);

                ms = new MemoryStream(b);

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                object o = formatter.Deserialize(ms);
                return o;
            }
            catch (SerializationException e) {
                throw e;
            }
            finally {
                ms.Close();
            }
        }
        #endregion 系列化字符窜相关...
    }
}
