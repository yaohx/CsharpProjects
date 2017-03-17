//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2008-01-05
// Description	:	主要针对文本硬盘操作的简单读写。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util {
    /// <summary>
    /// 主要针对文本硬盘操作的简单读写。
    /// </summary>
    public class MyFileStream {
        #region Instance...
        private static Object _Obj = new object();
        private static MyFileStream _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected MyFileStream() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static MyFileStream Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new MyFileStream();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...
        /// <summary>
        ///  以UTF8 的格式从文本文件中读取字符窜。
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public string StreamReader(string fileFullName) {
            if (!System.IO.File.Exists(fileFullName)) {
                return string.Empty;
            }
            using(System.IO.StreamReader reader = new System.IO.StreamReader(fileFullName, Encoding.UTF8))
            {
                try {
                    string str = reader.ReadToEnd();
                    return str;
                }
                catch (Exception ex) {
                    throw new MB.Util.APPException(string.Format("打开文件{0} 有误 ",fileFullName) + ex.Message, APPMessageType.SysErrInfo); 
                }
                finally {
                    try {
                        reader.Close();
                    }
                    catch { }
                }
            }


        }
        /// <summary>
        /// 把字符窜文本以覆盖的方式写到本地硬盘中。
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <param name="strText"></param>
        public void StreamWriter(string fileFullName, string strText) {
            using (System.IO.StreamWriter write = new System.IO.StreamWriter(fileFullName, false)) {
                try {
                    write.Write(strText);
                }
                catch (Exception ex) {
                    throw new MB.Util.APPException(string.Format("写文件{0} 有误 ", fileFullName) + ex.Message, APPMessageType.SysErrInfo); 
                }
                finally {
                    try {
                        write.Close();
                    }
                    catch { }
                }
               
            }
        }

    }
}
