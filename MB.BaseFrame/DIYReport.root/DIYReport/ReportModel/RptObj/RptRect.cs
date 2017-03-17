//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-15
// Description	:	 
// ±¸×¢ÃèÊö£º  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;

namespace DIYReport.ReportModel.RptObj
{
	/// <summary>
	/// RptRect »­Ö±Ïß¡£
	/// </summary>
	public class RptRect : DIYReport.ReportModel.RptSingleObj   {
		public RptRect() : this(null){
		}
		public RptRect(string pName) : base(pName,DIYReport.ReportModel.RptObjType.Rect) {
			base.IsTranspControl = true;
		}

		#region ICloneable Members

		public  override object Clone() {
			object info = this.MemberwiseClone() as object ;
			return info;
		}
 
		#endregion ICloneable Members
	}
}
