//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-18
// Description	:	 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Drawing ;
namespace DIYReport.Interface {
	/// <summary>
	/// IRptTextObj 的摘要说明。
	/// </summary>
	public interface IRptTextObj : IRptSingleObj {
		Font Font{get;set;}
	bool WordWrap{get;set;}
		bool ShowFrame{get;set;}
		StringAlignment Alignment{get;set;}
		string FormatStyle{get;set;}	//格式化样式

}
}
