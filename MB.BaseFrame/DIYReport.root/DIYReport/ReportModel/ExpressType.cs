//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-15
// Description	:	 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;

namespace DIYReport.ReportModel
{
	/// <summary>
	/// ExpressType 数据表达的类型
	/// </summary>
	public enum ExpressType
	{
		Field = 0,//字段
		SysParam = 1,//系统参数
		UserParam = 2,//用户参数
		Express = 3,//一般表达式
		Calculate = 4 //表达式计算
		 
	}
}
