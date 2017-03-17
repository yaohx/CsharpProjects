using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MB.Util {
    /// <summary>
    /// 基于常量密钥的简单字符加密处理。
    /// </summary>
    public class DESDataEncrypt {
        private static string KEY_ENCRYPT = "c_dc";
        /// <summary>
        /// 解密字符窜
        /// </summary>
        /// <param name="pDecryptStr"></param>
        /// <returns></returns>
        public static string DecryptString(string decryptStr) {

            DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
            //产生key
            PasswordDeriveBytes db = new PasswordDeriveBytes(KEY_ENCRYPT, null);
            byte[] key = db.GetBytes(8);
            //存储解密后的数据
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, desc.CreateDecryptor(key, key), CryptoStreamMode.Write);
            //取到加密后的数据的字节流，如果是保存到文件
            try {
                byte[] databytes = Convert.FromBase64String(decryptStr);
                //解密数据
                cs.Write(databytes, 0, databytes.Length);
                cs.FlushFinalBlock();
                byte[] res = ms.ToArray();
                //返回解密后的数据，这里返回的数据应该和参数pwd的值相同。
                return System.Text.Encoding.UTF8.GetString(res);
            }
            catch {
                TraceEx.Write("字符窜" + decryptStr + "解密不成功。");
                return null;
            }
        }
        /// <summary>
        /// 加密字符窜
        /// </summary>
        /// <param name="pEncryptedStr"></param>
        /// <returns></returns>
        public static string EncryptString(string encryptedStr) {
            //des进行加密
            DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
            //产生key
            PasswordDeriveBytes db = new PasswordDeriveBytes(KEY_ENCRYPT, null);
            byte[] key = db.GetBytes(8);
            //存储加密后的数据
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, desc.CreateEncryptor(key, key), CryptoStreamMode.Write);
            //取到密码的字节流
            try {
                byte[] data = Encoding.UTF8.GetBytes(encryptedStr);
                //进行加密
                cs.Write(data, 0, data.Length);
                cs.FlushFinalBlock();
                //取加密后的数据
                byte[] res = ms.ToArray();
                return Convert.ToBase64String(res);
            }
            catch {
               TraceEx.Write("字符窜" + encryptedStr + "加密不成功。");
                return null;
            }

        }
    }
}
