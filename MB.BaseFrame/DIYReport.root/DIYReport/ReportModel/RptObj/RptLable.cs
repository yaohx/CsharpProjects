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
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace DIYReport.ReportModel.RptObj
{
	/// <summary>
	/// RptLable 般文本描述。
	/// </summary>
	public class RptLable : DIYReport.ReportModel.RptTextObj
	{
		private string _Text;

		#region 构造函数...
		public RptLable() : this(null){
		}
		public RptLable(string pName):this(pName,null){
		}
		public RptLable(string pName,string pText) : base(pName,DIYReport.ReportModel.RptObjType.Text)
		{
			if(pText==null){
				_Text = "[请输入]";
			}
			else{
				_Text = pText;
			}
		}
		#endregion 构造函数...

		#region Public 属性...
		[Description("设置或者得到打印控件的文本描述。"),Category("数据")]
		public string Text {
			get {
				return _Text;
				
			}
			set {
				_Text = value;
				if(base.IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		#endregion Public 属性...

		#region ICloneable Members

		public  override object Clone() {
			object info = this.MemberwiseClone() as object ;
			return info;
		}
 
		#endregion ICloneable Members
	}
}
