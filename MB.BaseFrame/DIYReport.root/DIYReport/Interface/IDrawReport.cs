//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-30
// Description	:	 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Drawing ;

namespace DIYReport.Interface
{
	/// <summary>
	/// IDrawReport 绘制报表内容的操作对象接口
	/// </summary>
	public interface IDrawReport : System.IDisposable
	{
		DIYReport.ReportModel.RptReport DataReport{get;}
		int HasDrawRowCount{get;set;}
		int PageNumber{get;set;}

		bool DrawReportSection(object graphicsObject,DIYReport.SectionType pSectionType);
		void BeginPrint();
	}
}
