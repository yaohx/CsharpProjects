//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-07
// Description	:	 RptOleObject ole 对象。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;

namespace DIYReport.ReportModel.RptObj
{
	/// <summary>
	/// RptOleObject ole 对象。
	/// </summary>
	public class RptOleObject : DIYReport.ReportModel.RptSingleObj 
	{
		public RptOleObject(): this(null){
		}
		/// <summary>
		/// RptOleObject ole 对象。
		/// </summary>
		/// <param name="pName"></param>
		public RptOleObject(string pName) : base(pName,DIYReport.ReportModel.RptObjType.OleObject)  
		{

		}
	}
}
