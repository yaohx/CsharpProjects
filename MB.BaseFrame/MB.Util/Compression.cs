//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	提供内部使用压缩字流的方法。 
// Modify date	:			By:					Why: 
//---------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace MB.Util {
    /// <summary>
    /// 提供内部使用压缩字流的方法.
    /// </summary>
    public sealed class Compression : System.ContextBoundObject {
        private const int ZIP_MAX_LENGTH = 1024;

        #region Instance...
        private static Object _Obj = new object();
        private static Compression _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected Compression() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static Compression Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new Compression();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        /// <summary>
        /// 压缩。
        /// 提示：使用无损压缩和解压缩文件的行业标准算法. 此类不能用于压缩大于 4 GB 的文件 。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Zip(byte[] data) {
            return process(data, CompressionMode.Compress);
        }
        /// <summary>
        /// 解压缩。
        /// 提示：使用无损压缩和解压缩文件的行业标准算法. 此类不能用于压缩大于 4 GB 的文件 。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] UnZip(byte[] data) {
            return process(data, CompressionMode.Decompress);
        }

        #region private function...
        private byte[] process(byte[] data, CompressionMode mode) {
            DeflateStream zip = null;//和GZipStream 相似 此类表示 GZip 数据格式，它使用无损压缩和解压缩文件的行业标准算法. 此类不能用于压缩大于 4 GB 的文件
            try {
                if (mode == CompressionMode.Compress) {
                    MemoryStream ms = new MemoryStream();
                    zip = new DeflateStream(ms, mode, true);
                    zip.Write(data, 0, data.Length);
                    zip.Close();
                    return ms.ToArray();
                }
                else {
                    MemoryStream ms = new MemoryStream();
                    ms.Write(data, 0, data.Length);
                    ms.Flush();
                    ms.Position = 0;
                    zip = new DeflateStream(ms, mode, true);
                    MemoryStream os = new MemoryStream();
                    int SIZE = ZIP_MAX_LENGTH;
                    byte[] buf = new byte[SIZE];
                    int l = 0;
                    do {
                        l = zip.Read(buf, 0, SIZE);
                        if (l == 0) l = zip.Read(buf, 0, SIZE);
                        os.Write(buf, 0, l);
                    } while (l != 0);
                    zip.Close();
                    return os.ToArray();
                }
            }
            catch (Exception ex) {
                if (zip != null) zip.Close();
                return null;
            }
            finally {
                if (zip != null) zip.Close();
            }
        }
        #endregion private function...
    }
}
