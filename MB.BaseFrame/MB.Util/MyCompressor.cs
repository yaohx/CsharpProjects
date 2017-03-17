using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace MB.Util {
    /// <summary>
    /// 压缩器，利用.net基础类支持GZip和Deflate算法
    /// </summary>
    public class MyCompressor {

        /// <summary>
        /// 获取GZIP算法的压缩器
        /// </summary>
        public static MyCompressor Instance {
            get {
                return new MyCompressor();
            }
        }

        private CompressionAlgorithm _Algorithm;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MyCompressor()
            : this(CompressionAlgorithm.GZip) {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="algorithm">压缩算法</param>
        private MyCompressor(CompressionAlgorithm algorithm) {
            _Algorithm = algorithm;
        }

        /// <summary>
        /// 压缩原始字节
        /// </summary>
        /// <param name="decompressedData">需要压缩的字节数组</param>
        /// <returns>压缩以后的字节数组</returns>
        public byte[] Compress(byte[] decompressedData) {
            CompressionAlgorithm algorithm = _Algorithm;
            using (MemoryStream stream = new MemoryStream()) {
                if (algorithm == CompressionAlgorithm.Deflate) {
                    GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress, true);
                    stream2.Write(decompressedData, 0, decompressedData.Length);
                    stream2.Close();
                }
                else {
                    DeflateStream stream3 = new DeflateStream(stream, CompressionMode.Compress, true);
                    stream3.Write(decompressedData, 0, decompressedData.Length);
                    stream3.Close();
                }
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 对压缩的数据进行解压
        /// </summary>
        /// <param name="compressedData">需要解压的字节</param>
        /// <returns>解压以后的结果</returns>
        public byte[] Decompress(byte[] compressedData) {
            CompressionAlgorithm algorithm = _Algorithm;
            using (MemoryStream stream = new MemoryStream(compressedData)) {
                if (algorithm == CompressionAlgorithm.GZip) {
                    using (GZipStream stream2 = new GZipStream(stream, CompressionMode.Decompress)) {
                        return loadToBuffer(stream2);
                    }
                }
                else {
                    using (DeflateStream stream3 = new DeflateStream(stream, CompressionMode.Decompress)) {
                        return loadToBuffer(stream3);
                    }
                }
            }
        }

        /// <summary>
        /// 从解压以后的流中，转换到内存流中，并变成byte数组
        /// </summary>
        /// <param name="stream">解压以后的流</param>
        /// <returns>解压以后的字节数组</returns>
        private byte[] loadToBuffer(Stream stream) {
            using (MemoryStream stream2 = new MemoryStream()) {
                int num;
                byte[] buffer = new byte[0x400];
                while ((num = stream.Read(buffer, 0, buffer.Length)) > 0) {
                    stream2.Write(buffer, 0, num);
                }
                return stream2.ToArray();
            }
        }

    }

    /// <summary>
    /// 压缩算法
    /// </summary>
    public enum CompressionAlgorithm {
        GZip,
        Deflate
    }
}
