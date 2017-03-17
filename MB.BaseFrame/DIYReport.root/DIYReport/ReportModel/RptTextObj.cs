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
using System.Drawing ;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
namespace DIYReport.ReportModel
{
	/// <summary>
	/// RptTextObj 基本的文本对象。
	/// </summary>
	public class RptTextObj : RptSingleObj, DIYReport.Interface.IRptTextObj 
	{
		private Font _Font;
		private bool _WordWrap;
		private bool _ShowFrame;
		private StringAlignment _Alignment;
		private string _FormatStyle;	//格式化样式
		private bool _ToUpperMoney; //转换为大写的金额
		private bool _ToUpperEnglish;

		public RptTextObj(string pName ,DIYReport.ReportModel.RptObjType pType) : base(pName,pType){
			_Font = new Font("Tahoma",9);
			_Alignment = StringAlignment.Near ;
			_ShowFrame = true;
		}
		#region Public 属性...

		[Description("设置或者得到样式的字体。"),Category("外观")]
		public Font Font {
			get {
				return _Font;
			}
			set {
				_Font = value;
				if(base.IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到打引对象是否自动换行字体。"),Category("外观")]
		public bool WordWrap {
			get {
				return _WordWrap;
			}
			set {
				_WordWrap = value;
				if(base.IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
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
		[Description("设置或者得到对象的字符窜的对齐方式。"),Category("外观")]
		public StringAlignment Alignment {
			get {
				return _Alignment;
			}
			set {
				_Alignment = value;

				if(base.IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		public string FormatStyle {
			get {
				return _FormatStyle;
			}
			set {
				_FormatStyle=value;
			}
		}
		[Description("设置或者是否把金额转换为大写的金额。"),Category("外观")]
		public bool ToUpperMoney{
			get{
				return _ToUpperMoney;
			}
			set{
				_ToUpperMoney = value;
			}
		}
		[Description("设置或者是否把金额转换为英文大写的金额。"),Category("外观")]
		public bool ToUpperEnglish{
			get{
				return _ToUpperEnglish;
			}
			set{
				_ToUpperEnglish = value;
			}
		}
		#endregion Public 属性...
	}
}
