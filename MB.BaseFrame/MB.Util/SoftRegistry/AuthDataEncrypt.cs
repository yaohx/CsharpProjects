//---------------------------------------------------------------- 
// All rights reserved. 
// Author		:	chendc
// Create date	:	2003-01-04
// Description	:	AuthDataEncrypt
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Text;
using System.Security.Cryptography ;
using System.IO ;
using System.Management;

namespace MB.Aop.SoftRegistry
{
	/// <summary>
	/// AuthDataEncrypt 授权工作业务处理类。
	/// </summary>
	public class AuthDataEncrypt
	{
		#region 变量定义...
		public const int RAND_KEY_LEN = 4;
		private const int MAX_SERIAL_NUMBER_LEN = 16;
		private static string KEY_HD = "ufSoft";
		public const int GET_RAND_MAX = 10000;
		private static string REPLACE_CHAR = "=";
		#endregion 变量定义...


		#region add private construct function...
		/// <summary>
		/// add private construct function to prevent instance.
		/// </summary>
		private AuthDataEncrypt(){
		}
		#endregion add private construct function...

		/// <summary>
		/// 获取加密的随机数。
		/// </summary>
		/// <returns></returns>
		public static string GetRandKey(){
			Random r = new Random();
			int rand = r.Next(0,GET_RAND_MAX);
			
			return rand.ToString();
		}
		#region 硬盘系列号加密解密处理相关...
		/// <summary>
		/// 加密硬盘系列号。
		/// </summary>
		/// <param name="serialNumber"></param>
		/// <returns></returns>
		public static string DescryptHDString(string serialNumber){
			if(serialNumber==null || serialNumber.Length < MAX_SERIAL_NUMBER_LEN){
				//UP.Utils.TraceEx.Write("字符窜:" + serialNumber + "加密有误！");
				return string.Empty;
			}
			string hexRand = serialNumber.Substring(0,RAND_KEY_LEN);
			int rand = ToDec(hexRand);
			string str  =  DecryptString(serialNumber.Substring(RAND_KEY_LEN,serialNumber.Length - RAND_KEY_LEN),KEY_HD + rand.ToString()); 

			return str;
		}
		/// <summary>
		/// 解密硬盘系列号。
		/// </summary>
		/// <param name="serialNumber"></param>
		/// <returns></returns>
		public static string EncryptHDString(string serialNumber){
			string randKey = GetRandKey();
			if(serialNumber==null || serialNumber.Length < MAX_SERIAL_NUMBER_LEN){
				//UP.Utils.TraceEx.Write("字符窜:" + serialNumber + "解密有误！");
				return string.Empty;
			}
			string str = EncryptString(serialNumber,KEY_HD + randKey);
			string hexStr = ToHex(int.Parse(randKey),RAND_KEY_LEN);
			return hexStr + str;

		}

		#endregion 硬盘系列号加密解密处理相关...

		#region 特殊要求的16进制和10 进制之间的转换处理...
		/// <summary>
		/// 十进制转换为固定长度的十六进制，不足地方用特殊字符来代替。
		/// </summary>
		/// <param name="number"></param>
		/// <param name="fixLenth"></param>
		/// <returns></returns>
		public static  string ToHex(int number,int fixLenth){
			string hexStr = BODHConvert(number.ToString(),10,16);
			hexStr = FormatStr(hexStr, fixLenth,char.Parse(REPLACE_CHAR));
			return hexStr;
		}
		/// <summary>
		/// 把特殊的十六进制转换为十进制。
		/// </summary>
		/// <param name="hexStr"></param>
		/// <returns></returns>
		public static int ToDec(string  hexStr){
			string hexRand = hexStr.Replace(REPLACE_CHAR,"") ;
			int rand = int.Parse(BODHConvert(hexRand,16,10));
			return rand;
		}

		#endregion 特殊要求的16进制和10 进制之间的转换处理...

		/// <summary>
		/// 获取硬盘系列号
		/// </summary>
		/// <returns></returns>
		public static  string GetHd() {  //97 bit   after Substring  still have 40 bit
			ManagementObjectSearcher wmiSearcher = new ManagementObjectSearcher();

			wmiSearcher.Query = new SelectQuery(
				"Win32_DiskDrive", //Win32_PhysicalMedia //
				"", 
				new string[]{"PNPDeviceID"}//SerialNumber//
				);
			ManagementObjectCollection myCollection = wmiSearcher.Get();
			ManagementObjectCollection.ManagementObjectEnumerator em = myCollection.GetEnumerator();
			em.MoveNext();
			ManagementBaseObject mo = em.Current;
			string id = mo.Properties["PNPDeviceID"].Value.ToString().Trim();
			int leftSign = id.LastIndexOf(@"\");
 
			string hdStr = id.Substring(leftSign + 1,id.Length - leftSign -1);
			if(hdStr.Length >16){
				hdStr = hdStr.Substring(0,16);
			}
			else{
				if(hdStr!=null && hdStr.Length > 0){
					int llen = 16 - hdStr.Length;
					//如果不足16位就补0
					for(int i =0 ; i < llen; i++){
						hdStr +="0";
					}
				}
			}
			return hdStr;
		}

		#region 内部函数处理...
		//解密字符窜
		private  static string DecryptString(string pDecryptStr,string encr_key) {

			DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
			//产生key
			PasswordDeriveBytes db = new PasswordDeriveBytes(encr_key, null);
			byte[] key = db.GetBytes(8);
			//存储解密后的数据
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms,desc.CreateDecryptor(key, key),CryptoStreamMode.Write);
			//取到加密后的数据的字节流，如果是保存到文件
			try{
				byte[] databytes =  Convert.FromBase64String(pDecryptStr);
				//解密数据
				cs.Write(databytes, 0, databytes.Length);
				cs.FlushFinalBlock();
				byte[] res = ms.ToArray();
				//返回解密后的数据，这里返回的数据应该和参数pwd的值相同。
				return  System.Text.Encoding.UTF8.GetString(res);
			}
			catch{
				//UP.Utils.TraceEx.Write("字符窜" + pDecryptStr + "解密不成功。");   
				return null;
			}
		}
		//加密字符窜
		private static string EncryptString(string pEncryptedStr,string encr_key) {
			//des进行加密
			DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
			//产生key
			PasswordDeriveBytes db = new PasswordDeriveBytes(encr_key, null);
			byte[] key = db.GetBytes(8);
			//存储加密后的数据
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms,desc.CreateEncryptor(key, key),CryptoStreamMode.Write);
			//取到密码的字节流
			try{
				byte[] data = Encoding.UTF8.GetBytes(pEncryptedStr);
				//进行加密
				cs.Write(data, 0, data.Length);
				cs.FlushFinalBlock();
				//取加密后的数据
				byte[] res = ms.ToArray();
				return Convert.ToBase64String(res);
			}
			catch{
				//UP.Utils.TraceEx.Write("字符窜" + pEncryptedStr + "加密不成功。");   
				return null;
			}

		}
		private static string BODHConvert(string valStr, int fromBase, int toBase) {
			int intValue = Convert.ToInt32(valStr,fromBase);

			return Convert.ToString(intValue, toBase);
		}
		public  static string FormatStr(string pStr,int pLen,char pFormatChar){
			string s = pStr==null?"":pStr;
			if(s.Length > pLen){
				return pStr.Substring(0,pLen);
			}
			else if(s==""){
				for(int i =0 ;i <pLen;i++){
					s +=pFormatChar.ToString();
				}
				return s;
			}
			else if(s.Length < pLen){
				int l = pLen - s.Length;
				for(int i =0 ;i < l;i++){
					s +=pFormatChar.ToString();
				}
				return s;
			}
			return s;
		}
		#endregion 内部函数处理...
	}
}
