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
	/// RptSection 报表的section描述。
	/// </summary>
	public class RptSection
	{
		#region 变量定义...
		private string _Name;
		private SectionType _SectionType;
		private Point  _Location;
		private int _Height;
		private int _Width;
		private bool _Visibled = true;
		private bool _IsEndUpdate = true;

		private RptSingleObjList _RptObjList;

		private RptReport _Report;

		//分组处理情况下字段的名称，（指针对页眉和页脚情况下设置的）
		private int _Index; //Section 在集合中的顺序位置
		private DIYReport.GroupAndSort.RptFieldInfo   _GroupField;

		//判断该数据Section 是否已经创建对应的Design Section 
		private bool _HasCreateViewDesign; 
		
		//在绘制报表时候真正的开始位置
		private int _DrawLocationY = 0;
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
				_AfterValueChanged(this,arg);
			}

		}
		#endregion 自定义事件...

		#region 构造函数...
		public RptSection() : this(DIYReport.SectionType.Detail){
		}
		/// <summary>
		///  根据类型创建一个新的Section 
		/// </summary>
		/// <param name="pType">Section 的类型</param>
		public RptSection(DIYReport.SectionType pType) : this(pType,null){
		}
		public RptSection(DIYReport.SectionType pType,DIYReport.GroupAndSort.RptFieldInfo  pGroupField){
			_SectionType = pType;
			_GroupField = pGroupField;
			_RptObjList = new RptSingleObjList(this);
			_Height = RptReport.DEFAULT_SELCTION_HEIGHT ;
			_Visibled = true;
			if(_GroupField!=null)
				_Name = DIYReport.PublicFun.GetTextBySectionType( pType) + "GROUP BY " + _GroupField.FieldName;
			else
				_Name = DIYReport.PublicFun.GetTextBySectionType( pType);

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
		
		#region public 属性...
		[Browsable(false)]
		public int Index{
			get{
				return _Index;
			}
			set{
				_Index = value;
			}
		}

		[Browsable(false)]
		public DIYReport.GroupAndSort.RptFieldInfo GroupField{
			get{
				return _GroupField;
			}
			set{
				_GroupField = value;
			}
		}
		[Browsable(false)]
		public RptReport Report{
			get{
				return _Report;
			}
			set{
				_Report = value;
			}
		}
		[Browsable(false)]
		public RptSingleObjList RptObjList{
			get{
				return _RptObjList;
			}
			set{
				_RptObjList = value;
			}
		}
		[ReadOnly(true),Description("设置或者得到Section 的宽度"),Category("外观")]
		public int Width{
			get{
				return _Width;
			}
			set{
				_Width = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
        private Image _BackgroundImage; 
        [Description("设置或者得到要打印的图片。"), Category("数据")]
        public Image BackgroundImage {
            get {
                return _BackgroundImage;
            }
            set {
                
                if (_BackgroundImage != null) {
                    _BackgroundImage.Dispose();
                }
                if (value != null) {
                    Bitmap bit = new Bitmap(value);

                    System.Drawing.Image img = System.Drawing.Image.FromHbitmap(bit.GetHbitmap());
                    _BackgroundImage = img;
                    value.Dispose();
                } else {
                    _BackgroundImage = null;
                }
                if (_IsEndUpdate) { OnAfterValueChanged(new DIYReport.ReportModel.RptEventArgs()); }
            }
        }
		[Description("设置或者得到Section 的高度"),Category("外观")]
		public int Height{
			get{
				return _Height;
			}
			set{
				_Height = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Browsable(false),Description("是否显示该Section"),Category("行为")]
		public bool Visibled{
			get{
				return _Visibled;
			}
			set{
				_Visibled = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[ReadOnly(true),Description("该Section 的类型"),Category("行为")]
		public SectionType SectionType{
			get{
				return _SectionType;
			}
			set{
				_SectionType = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Browsable(false)]
		public Point Location{
			get{
				return _Location;
			}
			set{
				_Location = value;
			}
		}
		[Browsable(false),Description("设置或者得到该Section的名称"),Category("描述")]
		public string Name{
			get{
				return _Name;
			}
			set{
				_Name = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Browsable(false)]
		public bool HasCreateViewDesign{
			get{
				return _HasCreateViewDesign;
			}
			set{
				_HasCreateViewDesign = value;
			}
		}//
		[Browsable(false)]
		public int DrawLocationY{
			get{
				return _DrawLocationY;
			}
			set{
				_DrawLocationY = value;
			}
		}
		#endregion public 属性...
	}
}
