//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-21
// Description	:	ExSpecial 报表打印的特殊字段(属于系统参数的描述信息，如当前页、总页、当前时间、日期等)
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Drawing.Design;
namespace DIYReport.Express
{
	/// <summary>
	/// ExSpecial 报表打印的特殊字段(属于系统参数的描述信息，如当前页、总页、当前时间、日期等)
	/// </summary>
	public class ExSpecial
	{
		//这两个变量的值在每次预览或者打印的过程中都需要更新
		public static int _Page = 0;
		public static int _PageCount = 0;
		public static int _RowOrderNO = 0;
		#region 静态的方法...
		[Description("页码")]
		public static int Page(){
			return _Page;
		}
		[Description("页总数")]
		public static int PageCount(){
			return _PageCount;
		}
		[Description("页面信息")]
		public static int PageInfo(){
			return 0;
		}
		[Description("打印日期")]
		public static string PrintDate(){
			return System.DateTime.Now.ToShortDateString()  ;
		}
		[Description("打印时间")]
		public static string PrintTime(){
			return System.DateTime.Now.ToShortTimeString() ;
		}
		[Description("行数的顺序号")]
		public static int RowOrderNO(){
			return _RowOrderNO ;
		}
		#endregion 静态的方法...
	}
}
