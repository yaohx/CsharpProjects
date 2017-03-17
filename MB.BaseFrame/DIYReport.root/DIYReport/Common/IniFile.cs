/**///<summary>--------------------------------------------------- 
/// Copyright (C)2004-2004 nick chen (nickchen77@gmail.com)
/// All rights reerved. 
/// 
/// Author		:	Nick
/// Create date	:	2004-07-17
/// Description	:	Ini 文件操作类。
/// Modify date	:			By:					Why: 
///</summary>-----------------------------------------------------
using System; 
using System.IO; 
using System.Runtime.InteropServices; 
using System.Text;

namespace DIYReport{
	/// <summary>
	/// Ini 文件操作类。
	/// </summary>
　　public class IniFile {

	  //写INI文件
	  [ DllImport("kernel32") ]
	  private static extern bool WritePrivateProfileString ( string section ,string key , string val , string filePath );
　　//读ini文件（字符
	  [ DllImport("kernel32") ]
	  private static extern int GetPrivateProfileString ( string section ,string key , string def , StringBuilder retVal ,int size , string filePath ); 
　　//读ini文件（数字
	  [ DllImport("kernel32") ]
	  private static extern int GetPrivateProfileInt( string section ,string key , int def , string filePath );

	  //默认设置
	  public static  int MAX_STR_LENGTH = 1024 * 4;
	  #region 构造函数...
	  /// <summary>
	  /// private construct function to prevent instance.
	  /// </summary>
	  private  IniFile(){
		  
	  } 
	  #endregion 构造函数...

	  #region Public static 方法...
	  /// <summary>
	  /// 读取数据。
	  /// </summary>
	  /// <param name="section"></param>
	  /// <param name="key"></param>
	  /// <param name="def">缺省的值</param>
	  /// <returns></returns>
	  public static string ReadString(string section,string key,string defaultVal,string fullFileName){
		  StringBuilder temp = new StringBuilder(MAX_STR_LENGTH); 
		  GetPrivateProfileString(section,key,defaultVal,temp,MAX_STR_LENGTH,fullFileName); 
		  return temp.ToString().Trim(); 
	  } 
	  /// <summary>
	  /// 写数据
	  /// </summary>
	  /// <param name="section"></param>
	  /// <param name="key"></param>
	  /// <param name="strVal"></param>
	  public static void WriteString(string section,string key,string strVal,string fullFileName){
		  WritePrivateProfileString(section,key,strVal,fullFileName); 
	  } 
	  /// <summary>
	  /// 删除键值。
	  /// </summary>
	  /// <param name="section"></param>
	  /// <param name="key"></param>
	  public static void DelKey(string section,string key,string fullFileName) { 
		  WritePrivateProfileString(section,key,null,fullFileName); 
	  }
	  /// <summary>
	  /// 删除Section。
	  /// </summary>
	  /// <param name="section"></param>
	  public static void DelSection(string section,string fullFileName) { 
		  WritePrivateProfileString(section,null,null,fullFileName); 
	  }
	  #endregion Public static 方法...
  }
 
}
