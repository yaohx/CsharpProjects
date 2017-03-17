//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-07
// Description	:	RptBarCode 绘制条形码。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace DIYReport.ReportModel.RptObj
{
	public enum BarCodeType{
		Code39,//标准39码
		EAN13,//码（EAN-13国际商品条码）、EAN、UPC码（商品条码，用于在世界范围内唯一标识一种商品。我们在超市中最常见的就是这种条码
		EAN8,//码（EAN-8国际商品条码）、
		Code128,
        Code128A,
        Code128B,
        Code128C,
	}
	public enum AlignType {
		Left, Center, Right
	}

	public enum BarCodeWeight {
		Small = 1, Medium, Large
	}

	/// <summary>
	/// RptBarCode 绘制条形码。
	/// </summary>
	public class RptBarCode : DIYReport.ReportModel.RptSingleObj {
		private string _FieldName;
		private string _BingDBFieldName;//内部处理字段，fieldName 一般对应的是描述，只有BingDBFieldName 才真正绑定数据库对应的字段。

		private AlignType _Align ;
		private String _Code ;
		private int _LeftMargin ;
		private int _TopMargin;
		private int _Height;
		private bool _ShowHeader;
		private bool _ShowFooter;
		private String _HeaderText ;
		private BarCodeWeight _Weight ;
		private Font _HeaderFont;
		private Font _FooterFont;
		private bool _DrawByBarCode;
		private BarCodeType _CodeType;
		private bool _ShowFrame;
		private float _WID;

		public RptBarCode():this(null){
		}
		/// <summary>
		/// 构造函数。
		/// </summary>
		/// <param name="pName"></param>
		public RptBarCode(string pName) : base(pName,DIYReport.ReportModel.RptObjType.BarCode) {
			_Align = AlignType.Center;
			_Code = "1234567890";
			_LeftMargin = 5;
			_TopMargin = 5;
			_Height = 30;
			_ShowHeader = false;
			_ShowFooter = true;
			_HeaderText = "BarCode";
			_Weight = BarCodeWeight.Small;
			_HeaderFont = new Font("Microsoft Sans Serif",12F);
			_FooterFont = new Font("Microsoft Sans Serif", 8F);
			_CodeType = BarCodeType.Code39;
			_WID = 1f;
			_DrawByBarCode = true;
		}
		#region ICloneable Members

		public  override object Clone() {
			object info = this.MemberwiseClone() as object ;
			return info;
		}
 
		#endregion ICloneable Members

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
		[Description("设置或者得到是否通过barCode 条码类型。"),Category("数据")]
		public BarCodeType CodeType{
			get{
				return _CodeType;
			}
			set{
				_CodeType = value;
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到是否通过barCode 还是绑定的数据源来绘制条码。"),Category("行为")]
		public bool DrawByBarCode{
			get{
				return _DrawByBarCode;
			}
			set{
				_DrawByBarCode = value;
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到条码的对齐方式。"),Category("外观")]
		public AlignType VertAlign {
			get { return _Align; }
			set {
				_Align = value; 
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到条码的编码。"),Category("数据")]
		public String BarCode {
			get { return _Code; }
			set { _Code = value.ToUpper();
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到条码的高度。"),Category("外观")]
		public int BarCodeHeight {
			get { return _Height; }
			set { _Height = value;
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到条码的左边距。"),Category("外观")]
		public int LeftMargin {
			get { return _LeftMargin; }
			set { _LeftMargin = value;
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到条码的上边距。"),Category("外观")]
		public int TopMargin {
			get { return _TopMargin; }
			set { _TopMargin = value;
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到是否显示条码的标题。"),Category("外观")]
		public bool ShowHeader {
			get { return _ShowHeader; }
			set { _ShowHeader = value; 
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到是否显示脚注。"),Category("外观")]
		public bool ShowFooter {
			get { return _ShowFooter; }
			set { _ShowFooter = value; 
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到条码的标题描述。"),Category("外观")]
		public String HeaderText {
			get { return _HeaderText; }
			set { _HeaderText = value; 
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到绘制条码线的粗细。"),Category("外观")]
		public BarCodeWeight Weight {
			get { return _Weight; }
			set { _Weight = value; 
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到条码标题的字体。"),Category("外观")]
		public Font HeaderFont {
			get { return _HeaderFont; }
			set { _HeaderFont = value;
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("设置或者得到条码脚注字体。"),Category("外观")]
		public Font FooterFont {
			get { return _FooterFont; }
			set { _FooterFont = value;
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
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
		[Description("设置或者获取条码打印的每个像素宽度。"),Category("外观")]
		public float WID{
			get{
				return _WID;
			}
			set{
				_WID = value;
				if(base.IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
	}
}
