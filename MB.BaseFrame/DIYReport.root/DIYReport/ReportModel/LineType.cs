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
	/// LineType 直线类型。
	/// </summary>
	public enum LineType : int
	{
		Horizon = 0,//水平线
		Vertical = 1,//垂直线
		Bias = 2,// 斜线
		Backlash = 3 //反斜线
	}
}
