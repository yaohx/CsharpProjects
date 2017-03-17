using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MB.Util.Serializer
{
    /// <summary>
    /// 数据系列化到文件的处理类。
    /// </summary>
    public class DataContractFileSerializer<T>
    {
        private static readonly string MUTEX_KEY = "A218E930-E61D-4BF4-AAFE-D20FC93B979C";

        private string _MutexKey;
        private string _SerializerFileName;
        private static object _lockObj = new object();
        /// <summary>
        /// 实例化数据系列化操作类
        /// </summary>
        /// <param name="fileFullName">需要系列化得文件名称</param>
        public DataContractFileSerializer(string fileFullName)
            : this(fileFullName, Guid.NewGuid().ToString()) {

        }
        /// <summary>
        /// 实例化数据系列化操作类.
        /// </summary>
        /// <param name="fileFullName">需要系列化得文件名称</param>
        /// <param name="synKey">在多线程访问中进行同步控制的键值</param>
        public DataContractFileSerializer(string fileFullName, string synKey) {
            _MutexKey = string.IsNullOrEmpty(synKey) ? MUTEX_KEY : synKey;
            _SerializerFileName = fileFullName;
        }

        #region ...
        /// <summary>
        /// 系列数据对象到指定的XML 文件.
        /// </summary>
        /// <param name="data"></param>
        public void Write(T data) {
            lock (_lockObj) {
                System.Threading.Mutex m = new System.Threading.Mutex(false, _MutexKey);

                try {
                    m.WaitOne(new TimeSpan(0, 0, 10));

                    using (FileStream file = new FileStream(_SerializerFileName, FileMode.Create)) {
                        System.Runtime.Serialization.DataContractSerializer serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
                        serializer.WriteObject(file, data);
                    }
                }
                catch (Exception ex) {
                    throw ex;
                }
                finally {
                    m.ReleaseMutex();
                }
            }
        }
        /// <summary>
        /// 从指定的XML文件中反系列化数据对象.
        /// </summary>
        /// <returns></returns>
        public T Read() {
            if (!File.Exists(_SerializerFileName)) return default(T);
            lock (_lockObj) {
                T val = default(T);
                System.Threading.Mutex m = new System.Threading.Mutex(true, _MutexKey);

                try {
                    m.WaitOne(new TimeSpan(0, 0, 10));

                    using (FileStream file = new FileStream(_SerializerFileName, FileMode.Open)) {
                        System.Runtime.Serialization.DataContractSerializer serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
                        val = (T)serializer.ReadObject(file);
                    }
                }
                catch (Exception ex) {
                    throw ex;
                }
                finally {
                    m.ReleaseMutex();
                }
                return val;
            }
        }
        #endregion
    }
}
