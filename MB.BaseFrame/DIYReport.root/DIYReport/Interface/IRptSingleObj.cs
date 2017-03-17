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
namespace DIYReport.Interface
{
	/// <summary>
	/// IRptSingleObj 的摘要说明。
	/// </summary>
	public interface IRptSingleObj : ICloneable 
	{
		string Name{get;set;}
		DIYReport.ReportModel.RptObjType   Type{get;set;}
		Point Location{get;set;}
		Size Size{get;set;}
		Rectangle Rect{get;set;}

		System.Drawing.Drawing2D.DashStyle LineStyle{get;set;} //线条的样式
		int LinePound{get;set;}//线的宽度

		bool RaiseEvent{get;set;}
		Color ForeColor{get;set;}
		Color BackgroundColor{get;set;}
		DIYReport.ReportModel.RptSection  Section{get;set;}
		bool IsTranspControl{get;set;}
		bool IsEndUpdate{get;}
		void BeginUpdate();
		void EndUpdate();
		Rectangle InnerRect{get;}

		int Left{get;}
		int Top{get;}
		int Right{get;}
		int Bottom{get;}

		object WiseClone();
	}
}
