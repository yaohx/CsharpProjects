//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-13
// Description	:	RptFieldTextBox 绑定字段的文本框。   
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;


namespace DIYReport.ReportModel.RptObj {
	/// <summary>
	/// RptFieldTextBox 绑定字段的文本框。
	/// </summary>
	public class RptFieldTextBox : DIYReport.ReportModel.RptTextObj {
		private string _FieldName;

		private string _BingDBFieldName;//内部处理字段，fieldName 一般对应的是描述，只有BingDBFieldName 才真正绑定数据库对应的字段。
		private bool _IncludeMultiField;

		#region 构造函数...
		/// <summary>
		/// 
		/// </summary>
		public RptFieldTextBox() : this(null){
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		public RptFieldTextBox(string name) : base(name,DIYReport.ReportModel.RptObjType.FieldTextBox) {
			_FieldName = DIYReport.Drawing.RptDrawHelper.NO_BING_TAG ; 
			
		}
		#endregion 构造函数...

		#region ICloneable Members

		public  override object Clone() {
			object info = this.MemberwiseClone() as object ;
			return info;
		}
 
		#endregion ICloneable Members

		#region public 属性...
		[Description("绑定数据源的字段名称。"),Category("数据") ,Editor(typeof(DIYReport.Design.RptFieldAttributesEditor), typeof(UITypeEditor))]
		public string FieldName{
			get{
				return _FieldName;
			}
			set{
				_FieldName = value;
				
				if(this.Section!=null && this.Section.Report!=null){ 
					string fName = DIYReport.PublicFun.GetFieldNameByDesc(_FieldName,this.Section.Report.DesignField); 
					_BingDBFieldName = fName;
				}
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("内部绑定字段,不可以编辑由绑定FieldName时自动查询并获取。") ] 
        [Browsable(false)]
		public string BingDBFieldName{
			get{
				return _BingDBFieldName;
			}
			set{
				_BingDBFieldName = value;
			}
		}
		[Description("当前字段绑定是否包含多个字段,如果是字段之间要用分号分开。"),Category("数据")]
		public bool IncludeMultiField{
			get{
				return _IncludeMultiField;
			}
			set{
				_IncludeMultiField = value;
			}
		}
		#endregion public 属性...
	}
}
