//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-22
// Description	:	RptRichTextBox 绑定字段的richtextbox 对象。
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;


namespace DIYReport.ReportModel.RptObj {
	/// <summary>
	/// RptRichTextBox 绑定字段的richtextbox 对象。
	/// </summary>
	public class RptRichTextBox : DIYReport.ReportModel.RptSingleObj {

		private string _FieldName;
		private string _BingDBFieldName;//内部处理字段，fieldName 一般对应的是描述，只有BingDBFieldName 才真正绑定数据库对应的字段。
		private bool _ShowFrame;
		private System.Windows.Forms.PictureBoxSizeMode _SizeModel;
		private System.Drawing.Font _Font;
		#region 构造函数...
		public RptRichTextBox() : this(null){
		}
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pName"></param>
		/// <param name="pText"></param>
		public RptRichTextBox(string pName) : this(pName,null) {

		}
		public RptRichTextBox(string pName,string dataSource) : base(pName,DIYReport.ReportModel.RptObjType.RichTextBox) {
			if(dataSource==null){
				_FieldName =  DIYReport.Drawing.RptDrawHelper.NO_BING_TAG; 
			}
			else{
				_FieldName = dataSource;
			}
		}
		#endregion 构造函数...

		#region ICloneable Members

		public  override object Clone() {
			object info = this.MemberwiseClone() as object ;
			return info;
		}
 
		#endregion ICloneable Members

		#region public 属性...
		[Description("设置或者得到图象的数据库字段。"),Category("数据"),Editor(typeof(DIYReport.Design.RptFieldAttributesEditor), typeof(UITypeEditor))]
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
		[Description("设置或者得到对象是否显示边框。"),Category("外观")]
		public bool ShowFrame {
			get {
				return _ShowFrame;
			}
			set {
				_ShowFrame = value;
				if(base.IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到绘制richTextBox 数据的方式。"),Category("外观")]
		public System.Windows.Forms.PictureBoxSizeMode DrawSizeModel{
			get{
				return _SizeModel;
			}
			set{
				_SizeModel = value;
			}

		}
		[Description("设置或者得到样式的字体。"),Category("外观")]
		public System.Drawing.Font Font {
			get {
				return _Font;
			}
			set {
				_Font = value;
				if(base.IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		#endregion public 属性...
	}
}
