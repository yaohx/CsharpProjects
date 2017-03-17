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
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace DIYReport.ReportModel.RptObj
{
	/// <summary>
	/// RptHViewSpecFieldBox 需要进行横向排序显示的数据框。
	/// </summary>
	public class RptHViewSpecFieldBox : DIYReport.ReportModel.RptTextObj
	{
		public static readonly string SIZE_FIRST_PREX = "Size_";
		public static readonly string COLOR_FIRST_PREX = "Color_";
		public static readonly string SHIP_PORT_PREX = "ShipPort_";
		public static readonly string ACTIVE_GROUP_FIELD_IDENTITY = "_{0}@@";

		private string _FieldName;
		private string _GroupDisplayField;
		private string _ActiveOrderField;//

		private string _SubTotalFields;
		private string _ForLetHViewKeysFields;
		private string _MapHViewColDataField;
		private ActiveViewArea _DataViewArea;
		private ActiveDataType _ActiveDataType;
		private int _CellWidth;
		private bool _AutoSize;
		

		#region 构造函数...
		/// <summary>
		/// 
		/// </summary>
		public RptHViewSpecFieldBox() : this(null){
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		public RptHViewSpecFieldBox(string name) : base(name,DIYReport.ReportModel.RptObjType.HViewSpecField)
		{
			_FieldName = DIYReport.Drawing.RptDrawHelper.NO_BING_TAG ; 

			_CellWidth = 26;
		}
		#endregion 构造函数...

		#region ICloneable Members

		public  override object Clone() {
			object info = this.MemberwiseClone() as object ;
			return info;
		}
 
		#endregion ICloneable Members

		#region public 属性...
		[Description("动态显示字段类型。"),Category("数据")]
		public ActiveDataType ActiveDataType{
			get{
				return _ActiveDataType;
			}
			set{
				_ActiveDataType = value;
			}
		}
		[Description("设置数据显示的区域。"),Category("数据")]
		public ActiveViewArea DataViewArea{
			get{
				return _DataViewArea;
			}
			set{
				_DataViewArea = value;
			}
		}
		[Description("默认单元格显示宽度。"),Category("数据")]
		public int CellWidth{
			get{
				return _CellWidth;
			}
			set{
				_CellWidth = value;
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Description("判断是否设置为自动适应宽度。"),Category("数据")]
		public bool AutoSize{
			get{
				return _AutoSize;
			}
			set{
				_AutoSize = value;
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Browsable(false),Description("需要进行横向转换的字段名称。"),Category("数据"),Editor(typeof(DIYReport.Design.RptFieldAttributesEditor), typeof(UITypeEditor))]
		public string FieldName{
			get{
				return _FieldName;
			}
			set{
				_FieldName = value;
				if(IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}

		[Browsable(false),Description("设置或者获取在横向转换中需要进行汇总的字段名称,多个字段之间用分号割开。"),Category("数据")]
		public string SubTotalFields{
			get{
				return _SubTotalFields;
			}
			set{
				_SubTotalFields = value;
			}
		}
		[Browsable(false),Description("设置或者获取在横向转换中需要设置为主键的字段名称,多个字段之间用分号割开。"),Category("数据")]
		public string ForLetHViewKeysFields{
			get{
				return _ForLetHViewKeysFields;
			}
			set{
				_ForLetHViewKeysFields = value;
			}
		}
		[Browsable(false),Description("设置或者获取在横向转换中映射到转换列下面的字段。"),Category("数据")]
		public string MapHViewColDataField{
			get{
				return _MapHViewColDataField;
			}
			set{
				_MapHViewColDataField = value;
			}
		}
		[Description("动态尺码分组显示字段名称。"),Category("显示"),Editor(typeof(DIYReport.Design.RptFieldAttributesEditor), typeof(UITypeEditor))]
		public string GroupDisplayField{
			get{
				return _GroupDisplayField;
			}
			set{
				_GroupDisplayField = value;
			}
		}
		[Description("动态尺码排序的字段名称。"),Category("显示"),Editor(typeof(DIYReport.Design.RptFieldAttributesEditor), typeof(UITypeEditor))]
		public string ActiveOrderField{
			get{
				return _ActiveOrderField;
			}
			set{
				_ActiveOrderField = value;
			}
		}

		#endregion public 属性...
	}
	/// <summary>
	/// 动态显示数据类型。（系统特殊内置处理方式）
	/// </summary>
	public enum ActiveDataType{
		Size,
		Color,
		ShipPort
	}

	public enum ActiveViewArea{
		CaptionArea, //标题区域
		DetailArea, //字段明细区域
		SumArea     //汇总区域
	}
}
