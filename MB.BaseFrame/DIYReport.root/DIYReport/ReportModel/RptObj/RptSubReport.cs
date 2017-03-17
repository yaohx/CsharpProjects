//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-03
// Description	:   RptSubReport在套打模型下处理子报表的情况。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace DIYReport.ReportModel.RptObj
{
	/// <summary>
	/// RptSubReport在套打模型下处理子报表的情况。
	/// </summary>
	public class RptSubReport: DIYReport.ReportModel.RptSingleObj {
		private string _ReportFileName;
		private string _RelationMember;//目前在设计的状态下，_RelationMember的名称就是_ReportFileName
		private int _PreviewRowCount;//显示的最大记录数
		private bool _FixedWidth;//判断是否按照所指定的子报表控件宽度来绘制
		private bool _FixedHeight;//判断是否按照所指定的子报表控件高度来绘制


		public RptSubReport() : this(null){
		}
		/// <summary>
		/// RptOleObject 在套打模型下处理子报表的情况。
		/// </summary>
		/// <param name="pName"></param>
		public RptSubReport(string pName) : base(pName,DIYReport.ReportModel.RptObjType.SubReport) {
			_PreviewRowCount = 100;
		}

		#region ICloneable Members

		public  override object Clone() {
			object info = this.MemberwiseClone() as object ;
			return info;
		}
 
		#endregion ICloneable Members

		#region public 属性...
		[Description("设置或者得到要打印的子报表文件名称。"),Category("数据"),Editor(typeof(DIYReport.Design.RptSubReportAttributesEditor), typeof(UITypeEditor))]
		public string ReportFileName{
			get{
				return _ReportFileName;
			}
			set{
				_ReportFileName = value;
				_RelationMember = _ReportFileName;
				if(IsEndUpdate){base.OnAfterValueChanged(new DIYReport.ReportModel.RptEventArgs());} 
			}
		}
		[Description("在数据源中指数据的名称。"),Category("数据"),Editor(typeof(DIYReport.Design.RelationMemberAttributesEditor), typeof(UITypeEditor))]
		public string RelationMember{
			get{
				return _RelationMember;
			}
			set{
				_RelationMember = value;
			}
		}
		[Description("设置或者得到要打印的子报表的记录数。"),Category("数据")]
		public int PreviewRowCount{
			get{
				return _PreviewRowCount;
			}
			set{
				_PreviewRowCount = value;
			}
		}
		[Description("判断是否按照所指定的子报表控件宽度来绘制。"),Category("外观")]
		public bool FixedWidth{
			get{
				return _FixedWidth;
			}
			set{
				_FixedWidth = value;
			}
		}
		[Description("判断是否按照所指定的子报表控件高度来绘制。"),Category("外观")]
		public bool FixedHeight{
			get{
				return _FixedHeight;
			}
			set{
				_FixedHeight = value;
			}
		}
 
		#endregion public 属性...

	}
}
