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
	/// RptObjType 报表对象描述。(原先的报表类型设计中是通过index 的处理的，所以现在只能通过它来设置)
	/// </summary>
	public enum RptObjType : int
	{
		 None = 0,
		 Text = 1,//描述 打印或者浏览的时候直接做为字符输出
		 Express = 2,//字段，参数\表达试 需要替换数据源中对应的数据
		 Line = 3,//需要画直线
		 Rect = 4,
		 Image = 5, //图片
		 //2006-04-07 增加
		 FieldTextBox = 6,//绑定字段的文本框
		 CheckBox = 7,
		 FieldImage = 8,//绑定字段的图象控件
		 SubReport = 9,//子报表对象
		 OleObject = 10,// ole 绑定对象
		 BarCode = 11,//条形码
		 HViewSpecField = 12, //设置横向转换的字段框
		 RichTextBox = 13 //

		 
	}
}
