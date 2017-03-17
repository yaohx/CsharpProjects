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
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace DIYReport.ReportModel
{
	/// <summary>
	/// CssClass 打印对象样式的设置。
	/// </summary>
	public class RptCssClass
	{
		private string _Key;
		private string _Name;
		private Font _Font;
		private Color _FontColor;
		private Color _BackgroundColor;
		private bool _WordWrap;
		private bool _ShowFrame;
		private StringAlignment _Alignment;
		private string _FormatStyle;	//格式化样式
		//设置画该对象的时候决定是否产生事件 //
		private bool _RaiseEvent =false;
		public RptCssClass(string pKey){
			_Key = pKey;
			_Name = pKey;
			_Font = new Font("Tahoma",9);
			_FontColor = Color.Black ;
			_BackgroundColor  = Color.White ;

		}
		public RptCssClass(XmlNode pNode) {
			try {
				int fontSize = int.Parse(pNode.Attributes["Font-Size"].Value);
				_Font = new Font( "Tahoma",fontSize);
				//_Font.Bold = bool.Parse(pNode.Attributes["Font-Bold"].Value );
				_Name = pNode.Attributes["Name"].Value ;
				//把XML文件中设置的字体颜色转换成系统的颜色 
				_FontColor = System.Drawing.ColorTranslator.FromHtml(pNode.Attributes["Font-Color"].Value );
				_BackgroundColor = System.Drawing.ColorTranslator.FromHtml(pNode.Attributes["Background-Color"].Value );

				_WordWrap = bool.Parse(pNode.Attributes["WordWrap"].Value ); 
				_ShowFrame = bool.Parse(pNode.Attributes["ShowFrame"].Value ); 
				string align = pNode.Attributes["Alignment"].Value;
				switch(align.ToUpper()) {
					case "LEFT":
						_Alignment = StringAlignment.Near ; 
						break;
					case "RIGHT":
						_Alignment = StringAlignment.Far;
						break;
					case "MIDDLE" :
						_Alignment = StringAlignment.Center;
						break;
					default:
						_Alignment = StringAlignment.Near ; 
						break;
				}
				// 
				if (pNode.Attributes["FormatStyle"]!=null) {
					_FormatStyle=pNode.Attributes["FormatStyle"].Value;
				}
				if (pNode.Attributes["RaiseEvent"]!=null) {
					_RaiseEvent=bool.Parse(pNode.Attributes["RaiseEvent"].Value);
				}

			}
			catch(Exception e) {
				Debug.Assert(false,"得到对象的CssClass有错!",e.ToString());
			}
		}

		#region Public 属性...
		[Browsable(false)]
		public string Key{
			get{
				return _Key;
			}
			set{
				_Key = value;
			}
		}
		[Description("设置或者得到样式的名称。"),Category("描述")]
		public string Name {
			get {
				return _Name;
			}
			set {
				_Name = value;
			}
		}
		[Description("设置或者得到样式的字体。"),Category("描述")]
		public Font Font {
			get {
				return _Font;
			}
			set {
				_Font = value;
			}
		}
		[Description("设置或者得到样式的字体颜色。"),Category("描述")]
		public Color FontColor {
			get {
				return _FontColor;
			}
			set {
				_FontColor = value;
			}
		}
		[Description("设置或者得到打印对象的背景颜色。"),Category("描述")]
		public Color BackgroundColor {
			get {
				return _BackgroundColor;
			}
			set {
				_BackgroundColor = value;
			}
		}
		[Description("设置或者得到打引对象是否自动换行字体。"),Category("描述")]
		public bool WordWrap {
			get {
				return _WordWrap;
			}
			set {
				_WordWrap = value;
			}
		}
		[Description("设置或者得到打引对象是否显示边框。"),Category("描述")]
		public bool ShowFrame {
			get {
				return _ShowFrame;
			}
			set {
				_ShowFrame = value;
			}
		}
		[Description("设置或者得到对象的字符窜的对齐方式。"),Category("描述")]
		public StringAlignment Alignment {
			get {
				return _Alignment;
			}
			set {
				_Alignment = value;
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
		[Description("设置或者得到在打印或者浏览的时候是否产生一个内部的事件"),Category("描述"),DefaultValue(false)]
		public bool RaiseEvent {
			get {
				return _RaiseEvent;
			}
			set {
				_RaiseEvent=value;
			}
		}
		#endregion Public 属性...

		#region 覆盖基类的方法...
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return _Name;
		}

		#endregion 覆盖基类的方法...

		//格式化字符串  
		public string FormatString(object pObj) {
			string strRet="";
			if (this.FormatStyle!=null) {
				switch (pObj.GetType().Name.ToLower()) {
					case "datetime":
						strRet=Convert.ToDateTime(pObj).ToString(this.FormatStyle);
						break;
					case "int16":
					case "int32":
					case "int64":
					case "uint16":
					case "uint32":
					case "uint64":
					case "single":
					case "double":
					case "decimal":
						strRet=Convert.ToDecimal(pObj).ToString(this.FormatStyle);
						break;
					default:
						strRet=pObj.ToString();
						break;
				}
			}
			else {
				strRet=pObj.ToString();
			}
			return strRet;
		}
	}
}
