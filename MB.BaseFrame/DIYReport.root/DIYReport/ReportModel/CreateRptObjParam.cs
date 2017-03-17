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
	/// CreateRptObjParam 的摘要说明。
	/// </summary>
	public class CreateRptObjParam
	{
		private RptCssClassList  _CssClasss;
		private RptSection _Section;
		public CreateRptObjParam(RptCssClassList pCssClasss,RptSection  pSection) {
			_CssClasss = pCssClasss;
			_Section = pSection;
		}
		#region Public 属性...
		public RptCssClassList CssClasss {
			get {
				return _CssClasss;
			}
		}
		public RptSection Section {
			get {
				return _Section;
			}
		}
		#endregion Public 属性...
	}
}
