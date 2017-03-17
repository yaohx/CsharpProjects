using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MB.Util.Serializer {
    /// <summary>
    /// 二进制系列化并压缩
    /// </summary>
    public class BinarySerializerAndZip {
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="objectInstance"></param>
        /// <returns></returns>
        public byte[] Serialize(object objectInstance) {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream tstream = new MemoryStream()) {
                formatter.Serialize(tstream, objectInstance);
                byte[] bytes_c = MB.Util.Compression.Instance.Zip(tstream.ToArray());
                return bytes_c;
            }
        }
        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public object Deserialize(byte[] data) {
            byte[] bytes_c = MB.Util.Compression.Instance.UnZip(data);
              BinaryFormatter formatter = new BinaryFormatter();
              using (MemoryStream tstream = new MemoryStream()) {
                  tstream.Write(bytes_c, 0, bytes_c.Length);
                  return formatter.Deserialize(tstream);
              }

        }
    }
}
