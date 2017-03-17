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
using System.Diagnostics ;
using System.Drawing ;
using System.Xml ;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace DIYReport.ReportModel
{
	/// <summary>
	/// RptSingleObj 报表浏览和打印的单个对象
	/// </summary>
	public class RptSingleObj : DIYReport.Interface.IRptSingleObj {
		#region 变量定义...
		private string _Name;
		private RptObjType  _Type;
		private Point _Location;
		private Size _Size;
		private Rectangle _Rect;
 
		private int _Left;
		private int _Top;
		private int _Right;
		private int _Bottom;

		private RptSection  _Section;
		 
		private Color _ForeColor;
		private Color _BackgroundColor;
	 
		private int _LinePound = 1;//线的宽度
		private System.Drawing.Drawing2D.DashStyle _LineStyle;//线的样式

		//设置画该对象的时候决定是否产生事件 
		private bool _RaiseEvent =false;
		private bool _IsEndUpdate = true;
		private bool _IsTranspControl = false;//透明的控件




		#endregion 变量定义...


		#region 自定义事件...
		private RptEventHandler _AfterValueChanged;
		public event RptEventHandler AfterValueChanged{
			add{
				_AfterValueChanged +=value;
			}
			remove{
				_AfterValueChanged -=value;
			}
		}
		protected void OnAfterValueChanged(RptEventArgs arg){
			if(_AfterValueChanged!=null){
				DIYReport.UserDIY.DesignEnviroment.DesignHasChanged = true;
				_AfterValueChanged(this,arg);
			}

		}
		#endregion 自定义事件...
		
		#region 构造函数...
		public RptSingleObj(){
		}
		public RptSingleObj(string pName,DIYReport.ReportModel.RptObjType pType ){
			_Name = pName;
			_Type =  pType;
			_ForeColor = Color.Black ;
			_BackgroundColor = Color.White ;
			 
			_Location = new Point(0,0);
			Size = new Size(100,25);
		}
		#endregion 构造函数...

		#region 扩展的Public 方法...
		public void BeginUpdate(){
			_IsEndUpdate = false;
		}
		public void EndUpdate(){
			_IsEndUpdate = true;
			OnAfterValueChanged(new RptEventArgs());
		}
		#endregion 扩展的Public 方法...

		#region 内部处理函数...
		//得到画文本的开始点

		#endregion 内部处理函数...

		#region Public 属性...
		[Description("得到打印控件对象的Left。"),Category("布局")]
		public int Left{
			get{
				return _Left;
			}
		}
		[Description("得到打印控件对象的Top。"),Category("布局")]
		public int Top{
			get{
				return _Top;
			}
		}
		[Description("得到打印控件对象的Right。"),Category("布局")]
		public int Right{
			get{
				return _Right;
			}
		}
		[Description("得到打印控件对象的Bottom。"),Category("布局")]
		public int Bottom{
			get{
				return _Bottom;
			}
		}

		[Browsable(false)]
		public bool IsTranspControl{
			get{
				return _IsTranspControl;
			}
			set{
				_IsTranspControl = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到边框线条的样式。"),Category("外观")]
		public  System.Drawing.Drawing2D.DashStyle LineStyle{
			get{
				return _LineStyle;
			}
			set{
				if(value!=System.Drawing.Drawing2D.DashStyle.Custom){
					_LineStyle = value;
					if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
				}
			}
		}
		[Description("设置或者得到边框线条的宽度。"),Category("外观")]
		public int LinePound{
			get{
				return _LinePound;
			}
			set{
				_LinePound = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Browsable(false)]
		public RptSection Section {
			get {
				return _Section;
			}
			set {
				_Section = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Browsable(false),Description("设置或者得到打印控件数据的数据来源类型。"),Category("行为")]
		public RptObjType Type {
			get {
				return _Type;
			}
			set {
				_Type = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}

		[Description("设置或者得到打印控件对象的位置。"),Category("布局")]
		public Point Location{
			get{
				return _Location;
			}
			set{
				_Location = value; 
				_Left = _Location.X;
				_Top = _Location.Y ;
				_Right = _Location.X + _Size.Width ;
				_Bottom = _Location.Y + _Size.Height ;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到打印控件对象的大小。"),Category("布局")]
		public Size Size{
			get{
				return _Size;
			}
			set{
				_Size = value;
				_Right = _Location.X + _Size.Width ;
				_Bottom = _Location.Y + _Size.Height ;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Browsable(false)]
		public Rectangle Rect {
			get {
				return new Rectangle(_Location,_Size);
			}
			set {
				_Rect = value;
				_Location = _Rect.Location ;
				_Size = _Rect.Size ;
			}
		}
		[Browsable(false)]
		public Rectangle InnerRect{
			get{
				return new Rectangle(0,0,_Size.Width - 1,_Size.Height - 1);
			}
		}
//		[Description("设置或者得到打印控件对象的样式描述。"),Category("外观"),
//		Editor(typeof(DIYReport.Design.RptCssClassAttributesEditor), typeof(UITypeEditor))]
//		public RptCssClass CssClass {
//			get {
//				return _CssClass;
//			}
//			set {
//				_CssClass = value;
//			}
//		}
		 
		[Description("设置或者得到样式的名称。"),Category("设计")]
		public string Name {
			get {
				return _Name;
			}
			set {
				_Name = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到样式的字体颜色。"),Category("外观")]
		public Color ForeColor {
			get {
				return _ForeColor;
			}
			set {
				_ForeColor = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到打印对象的背景颜色。"),Category("外观")]
		public Color BackgroundColor {
			get {
				return _BackgroundColor;
			}
			set {
				_BackgroundColor = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
 
 
		[Description("设置或者得到在打印或者浏览的时候是否产生一个内部的事件"),Category("行为"),DefaultValue(false)]
		public bool RaiseEvent {
			get {
				return _RaiseEvent;
			}
			set {
				_RaiseEvent=value;
			}
		}
		[Browsable(false)]
		public bool IsEndUpdate{
			get{
				return _IsEndUpdate;
			}
		}
		#endregion Public 属性...

		#region ICloneable Members
		
		public virtual object WiseClone(){
			object info = this.MemberwiseClone() as object ;
			return info;
		}
		public  virtual object Clone() {
			object info = this.MemberwiseClone() as object ;
			return info;
		}
		object ICloneable.Clone() {
			return Clone();
		}

		#endregion ICloneable Members
	}
}
