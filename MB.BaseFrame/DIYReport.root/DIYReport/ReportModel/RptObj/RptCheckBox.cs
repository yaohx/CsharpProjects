//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-07
// Description	:	RptCheckBox True 或 False 选择框。
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
	/// RptCheckBox True 或 False 选择框。
	/// </summary>
	public class RptCheckBox : DIYReport.ReportModel.RptSingleObj {
		private string _FieldName;
		private string _BingDBFieldName;//内部处理字段，fieldName 一般对应的是描述，只有BingDBFieldName 才真正绑定数据库对应的字段。
		private bool _Checked;
		private bool _BingField;
		private bool _ShowFrame;
		#region 构造函数...
		public RptCheckBox() : this(null){
		}
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pName"></param>
		/// <param name="pText"></param>
		public RptCheckBox(string pName) : this(pName,null) {

		}
		public RptCheckBox(string pName,string dataSource) : base(pName,DIYReport.ReportModel.RptObjType.CheckBox) {
			if(dataSource==null){
				_FieldName =  DIYReport.Drawing.RptDrawHelper.NO_BING_TAG; 
			}
			else{
				_FieldName = dataSource;
				_BingField = true;
			}
		}
		#endregion 构造函数...

		#region ICloneable Members

		public  override object Clone() {
			object info = this.MemberwiseClone() as object ;
			return info;
		}
 
		#endregion ICloneable Members

		[Description("获取或者设置True 或者 False."),Category("行为")]
		public bool Checked{
			get{
				return _Checked;
			}
			set{
				_Checked = value;
				if(IsEndUpdate){base.OnAfterValueChanged(new DIYReport.ReportModel.RptEventArgs());} 
			}
		}
		[Description("获取或者设置是否需要绑定数据源."),Category("数据")]
		public bool BingField{
			get{
				return _BingField;
			}
			set{
				_BingField = value;
				if(IsEndUpdate){base.OnAfterValueChanged(new DIYReport.ReportModel.RptEventArgs());} 
			}
		}
		[Description("设置或者得到图象的数据库字段。"),Category("数据"),Editor(typeof(DIYReport.Design.RptFieldAttributesEditor), typeof(UITypeEditor))]
		public string FieldName{
			get{
				return _FieldName;
			}
			set{
				_FieldName = value;
				_BingField = true;
				if(this.Section!=null && this.Section.Report!=null){ 
					string fName = DIYReport.PublicFun.GetFieldNameByDesc(_FieldName,this.Section.Report.DesignField); 
					_BingDBFieldName = fName;
				}
				if(IsEndUpdate){base.OnAfterValueChanged(new DIYReport.ReportModel.RptEventArgs());} 
			}
		}
		[Browsable(false)] 
		public string BingDBFieldName{
			get{
				return _BingDBFieldName;
			}
			set{
				_BingDBFieldName = value;
			}
		}
		[Description("设置或者得到打引对象是否显示边框。"),Category("外观")]
		public bool ShowFrame {
			get {
				return _ShowFrame;
			}
			set {
				_ShowFrame = value;
				if(base.IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
	}
}
