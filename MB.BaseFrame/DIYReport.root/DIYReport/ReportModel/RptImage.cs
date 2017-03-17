//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-08
// Description	:	 报表Image 图象。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Drawing ;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace DIYReport.ReportModel
{
	/// <summary>
	/// RptImage 报表Image 图象。
	/// </summary>
	public class RptImage : DIYReport.ReportModel.RptSingleObj 
	{ 
		private System.Windows.Forms.PictureBoxSizeMode _DrawSizeModel;
		private bool _DrawFrame;
		public RptImage(string pName,DIYReport.ReportModel.RptObjType type) : base(pName,type) {
		}
		[Description("绘制图象在指定框中的位置."),Category("行为")]
		public System.Windows.Forms.PictureBoxSizeMode DrawSizeModel{
			get{
				return _DrawSizeModel;
			}
			set{
				_DrawSizeModel = value;
			}
		}
		[Description("获取或者设置是否绘制图象的边框."),Category("行为")]
		public bool DrawFrame{
			get{
				return _DrawFrame;
			}
			set{
				_DrawFrame = value;
			}
		}
	}
}
