//---------------------------------------------------------------- 
// Author		:	Nick
// Create date	:	2009-02-13
// Description	:	ColPivotInfo 字段进行轴分组拖动的参数信息设置。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;

namespace MB.XWinLib.PivotGrid
{
	/// <summary>
	/// ColPivotInfo 字段进行轴分组拖动的参数信息设置。
	/// </summary>
	public class ColPivotInfo
	{
		#region 变量定义...
		private string _FieldName;//帮定对应的字段名称。
		//该列允许拖动的区域(RowArea,DataArea,ColumnArea,FilterArea。）多列之间以分号割开。
		private DevExpress.XtraPivotGrid.PivotGridAllowedAreas _AllowedAreas;
		//初始化时该列所RowArea 或者 DataArea 或者 ColumnArea 或者 FilterArea；
		//默认情况下该列属于FilterArea；
		private DevExpress.XtraPivotGrid.PivotArea _IniArea;
		private int  _AreaIndex;
		//字段分组统计分组的间隔设置。如：Alphabetical,Date,DateDay,DateDayOfWeek,DateDayOfYear,DateMonth
		//DateQuarter,DateWeekOfMonth,DateWeekOfYear,DateYear,Default,Numeric
		private DevExpress.XtraPivotGrid.PivotGroupInterval  _GroupInterval;
		private int _TopValueCount;
		private string _Description;
		//下面为Pivot 拖动分组绑定相关
		private string _DragGroupName;//拖动分组的名称
		private int _DragGroupIndex;//在拖动分组中的顺序
		
		//值中统计汇总的数据
		private string _SummaryItemType;
		
		//格式化信息
		private DevExpress.Utils.FormatInfo _CellFormat;
		private DevExpress.Utils.FormatInfo _ValueFormat;


        //创建表达式UnboundExpression,用来推断某一个列的值
        private string _Expression;
        //UnboundExpression所依赖的列
        private string _ExpressionSourceColumns;

        


		#endregion 变量定义...

		#region 构造函数...
		/// <summary>
		/// 构造函数。
		/// </summary>
		public ColPivotInfo(string fieldName)
		{
			_FieldName = fieldName;
			
			_CellFormat = DevExpress.Utils.FormatInfo.Empty; 
			_ValueFormat = DevExpress.Utils.FormatInfo.Empty; 
		}
		#endregion 构造函数...

		#region Public 属性...
		public string FieldName{
			get{
				return _FieldName;
			}
			set{
				_FieldName = value;
			}
		}
		public string Description{
			get{
				return _Description;
			}
			set{
				_Description = value;
			}
		}
		public DevExpress.XtraPivotGrid.PivotGridAllowedAreas AllowedAreas{
			get{
				return _AllowedAreas;
			}
			set{
				_AllowedAreas = value;
			}
		}
		public DevExpress.XtraPivotGrid.PivotArea IniArea{
			get{
				return _IniArea;
			}
			set{
				_IniArea = value;
			}
		}
		public int AreaIndex{
			get{
				return _AreaIndex;
			}
			set{
				_AreaIndex = value;
			}
		}
		public DevExpress.XtraPivotGrid.PivotGroupInterval GroupInterval{
			get{
				return _GroupInterval;
			}
			set{
				_GroupInterval = value;
			}
		}
		public int TopValueCount{
			get{
				return _TopValueCount;
			}
			set{
				_TopValueCount = value;
			}
		}
		public string DragGroupName{
			get{
				return _DragGroupName;
			}
			set{
				_DragGroupName = value;
			}
		}
		public int DragGroupIndex{
			get{
				return _DragGroupIndex;
			}
			set{
				_DragGroupIndex = value;
			}
		}
		public DevExpress.Utils.FormatInfo CellFormat{
			get{
				return _CellFormat;
			}
			set{
				_CellFormat = value;
			}
		}
		public DevExpress.Utils.FormatInfo ValueFormat{
			get{
				return _ValueFormat;
			}
			set{
				_ValueFormat = value;
			}
		}
		public string SummaryItemType{
			get{
				return _SummaryItemType;
			}
			set{
				_SummaryItemType = value;
			}
		}

        public string Expression
        {
            get { return _Expression; }
            set { _Expression = value; }
        }

        public string ExpressionSourceColumns
        {
            get { return _ExpressionSourceColumns; }
            set { _ExpressionSourceColumns = value; }
        }
		#endregion Public 属性...


	}
}
