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
	/// RptParam 的摘要说明。
	/// </summary>
	public class RptParam
	{
		private string _ParamName;
		private object _Value;
		/// <summary>
		/// 打印报表需要的参数信息
		/// </summary>
		/// <param name="pNode"></param>
		public RptParam(string  pName,object pValue) {
			_ParamName = pName;
			_Value =  pValue;
		}

		public string ParamName {
			get {
				return _ParamName;
			}
			set {
				_ParamName=value;
			}
		}
		public object Value {
			get {
				return _Value;
			}
			set {
				_Value=value;
			}
		}
	}
}
