//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-30
// Description	:	ISubReportCommand 子报表相关处理事项接口。 
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data;
using System.Collections;

namespace DIYReport.Interface
{
	/// <summary>
	/// ISubReportCommand 子报表相关处理事项接口。
	/// </summary>
	public interface ISubReportCommand
	{
		/// <summary>
		/// 根据报表的名称获取相应的子报表。
		/// </summary>
		/// <param name="reportName"></param>
		/// <returns></returns>
		DIYReport.ReportModel.RptReport GetReportContent(string reportName);
		/// <summary>
		/// 根据名称获取对应数据源。
		/// </summary>
		/// <param name="reportName"></param>
		/// <returns></returns>
        object GetReportDataSource(DataRow parentRow, string relationMember, string reportName);
		
		/// <summary>
		/// 子报表的名称。
		/// </summary>
		IList  SubReportName{get;}
		
	}
}
