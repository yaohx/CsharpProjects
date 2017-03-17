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

namespace DIYReport 
{
	/// <summary>
	/// SectionType 报表Section 的类型。
	/// </summary>
	public enum SectionType : int
	{
		TopMargin = 0,
		ReportTitle = 1, //报表标题
		PageHead = 2, //页头描述
		GroupHead = 3, //分组的组标题
		Detail = 4, //页面的Detail 信息
		GroupFooter = 5, //分组的组脚
		PageFooter = 6, //页脚的描述信息
		ReportBottom = 7, //报表的尾部信息
		BottomMargin = 8

		
	}
}
