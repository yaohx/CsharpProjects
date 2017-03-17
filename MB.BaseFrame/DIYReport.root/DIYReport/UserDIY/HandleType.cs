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

namespace DIYReport.UserDIY
{
	#region 焦点的类型，表示在那个方向...
	public enum HandleType : int{
		LeftTop = 0,//左上
		MiddleTop = 1, //上中
		RightTop = 2,//右上
		RightMiddle = 3,//右中
		RightBottom = 4,//右下
		BottomMiddle = 5,//下中
		LeftBottom = 6,//左下
		LeftMiddle = 7//左中
	}
	#endregion 焦点的类型，表示在那个方向...
}
