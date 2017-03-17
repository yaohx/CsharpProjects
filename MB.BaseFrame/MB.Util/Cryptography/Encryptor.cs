using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace MB.Util.Cryptography {
    public class Encryptor {
        #region Instance...
        private static Object _Obj = new object();
        private static Encryptor _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected Encryptor() {

        }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static Encryptor Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new Encryptor();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...
        /// <summary>
        /// 对传入的字符串进行MD5 HASH计算，并将最终结果每一个字节都转化成2位16进制
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string MD5Hash(string text) {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(Encoding.Default.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++) {
                //change it into 2 hexadecimal digits for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        /// <summary>
        /// 对传入的字符串进行SHA1 HASH计算，并将最终结果每一个字节都转化成2位16进制
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string SHA1Hash(string text) {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            sha1.ComputeHash(Encoding.Default.GetBytes(text));
            //get hash result after compute it
            byte[] result = sha1.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++) {
                //change it into 2 hexadecimal digits for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

    }
}
