//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2005-04-29
// Description	:	绘制Report Detail 时，当前要存储的处理信息
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;


using DIYReport.GroupAndSort; 
using DIYReport.Print;
namespace DIYReport.Print
{
	#region 绘制报表时的相关信息...
	public struct ReportDrawInfo{
		private int _TitleHeight;
		private int _PageHeadHeight;
		private int _DetailHeight;
		private int _PageFooterHeight;
		private int _BottomHeight;

		public ReportDrawInfo(DIYReport.ReportModel.RptReport  _Report){
			DIYReport.ReportModel.RptSectionList sectionList = _Report.SectionList;
			_TitleHeight = sectionList.GetSectionHeightByType( DIYReport.SectionType.ReportTitle); 
			_PageHeadHeight = sectionList.GetSectionHeightByType( DIYReport.SectionType.PageHead); 
			_DetailHeight = sectionList.GetSectionHeightByType( DIYReport.SectionType.Detail); 
			_PageFooterHeight = sectionList.GetSectionHeightByType( DIYReport.SectionType.PageFooter); 
			_BottomHeight = sectionList.GetSectionHeightByType( DIYReport.SectionType.ReportBottom); 
		}
		#region Public 属性...
		public int TitleHeight{
			get{
				return _TitleHeight;
			}
			set{
				_TitleHeight = value;
			}
		}
		public int PageHeadHeight{
			get{
				return _PageHeadHeight;
			}
			set{
				_PageHeadHeight = value;
			}
		}
		public int DetailHeight{
			get{
				return _DetailHeight;
			}
			set{
				_DetailHeight = value;
			}
		}
		public int PageFooterHeight{
			get{
				return _PageFooterHeight;
			}
			set{
				_PageFooterHeight = value;
			}
		}
		public int BottomHeight{
			get{
				return _BottomHeight;
			}
			set{
				_BottomHeight = value;
			}
		}

		#endregion Public 属性...
	}
	#endregion 绘制报表时的相关信息...

	#region 在画分组统计的时候，存储的组字段格式...
	public class DrawGroupField{
		#region 内部变量定义...
		//分析该组的第一个行Index  ； -1 为没有开始进行分析该分组的字段
		private int _FirstRowIndex;
		private int _PrevFirstRowIndex = 0;
		//分组的字段名称
		private string _GroupFieldName;
		//当前组分析对应的值
		private object _CurrGroupValue;
		//判断是否已经绘制Group Section 的头
		private bool _HasDrawGroupHead;
		#endregion 内部变量定义...
		
		#region 构造函数...
		/// <summary>
		///  构造函数
		/// </summary>
		/// <param name="pFirstRowIndex"></param>
		/// <param name="pGroupFieldName"></param>
		public DrawGroupField(int pFirstRowIndex , string pGroupFieldName){
			_FirstRowIndex = pFirstRowIndex;
			_CurrGroupValue = null;
			_GroupFieldName = pGroupFieldName;
			_HasDrawGroupHead = false;
		}
		#endregion 构造函数...

		#region Public 方法...
		public int FirstRowIndex{
			get{
				return _FirstRowIndex;
			}
			set{
				_FirstRowIndex = value;
			}
		}
		public int PrevFirstRowIndex{
			get{
				return _PrevFirstRowIndex;
			}
			set{
				_PrevFirstRowIndex = value;
			}
		}
		public string GroupFieldName{
			get{
				return _GroupFieldName;
			}
			set{
				_GroupFieldName = value;
			}
		}
		public object CurrGroupValue{
			get{
				return _CurrGroupValue;
			}
			set{
				_CurrGroupValue = value;
			}
		}
		public bool HasDrawGroupHead{
			get{
				return _HasDrawGroupHead;
			}
			set{
				_HasDrawGroupHead = value;
			}
		}
		#endregion Public 方法...


	}
	#endregion 在画分组统计的时候，存储的组字段格式...

	#region DrawDetailInfo...
	/// <summary>
	/// DrawDetailInfo 绘制Report Detail 时，当前要存储的处理信息。
	/// </summary>
	public class DrawDetailInfo
	{
		#region 变量定义...
		//当前页的第一个行的Index 
		private int _CurrPageFirstRowIndex;
		//当前正在绘制的行的Index 
		private int _CurrDrawRowIndex;
		//当前页的顺序号
		private int _CurrPageNumber;
		//分组的字段信息
		private IList  _GroupFields;
		#endregion 变量定义...
		
		#region 构造函数...
		/// <summary>
		///  根据DesignEnviroment中的DesignField构造出一个绘制Detail临时存储的信息
		/// </summary>
		/// <param name="pDataReport"></param>
		public DrawDetailInfo(DIYReport.ReportModel.RptReport report )
		{
			//-1 表示当前页还没有开始绘制
			_CurrPageFirstRowIndex = -1;
			_CurrPageNumber = -1;

			IList fieldList = report.DesignField;
			//对分组的字段进行排序，以获取分组字段的顺序
			(fieldList as ArrayList).Sort(new FieldSortComparer());  
			_GroupFields = new ArrayList();
			foreach(DIYReport.GroupAndSort.RptFieldInfo field in fieldList){
				if(field.IsGroup){
					DrawGroupField drwafield = new DrawGroupField(-1,field.FieldName); 
					_GroupFields.Add(drwafield); 
				}
			}  
		}
		#endregion 构造函数...

		#region Public 方法...
		/// <summary>
		/// 得到绘制分组对应字段处理信息 
		/// </summary>
		/// <param name="pFieldName">分组的字段名称</param>
		/// <returns></returns>
		public DrawGroupField GetGroupFieldByName(string pFieldName){
			int count = _GroupFields.Count ;
			for(int i = 0 ; i < count;i++){
				DrawGroupField field = _GroupFields[i] as DrawGroupField;
				if(field.GroupFieldName == pFieldName){
					return field;
				}
			}
			return (DrawGroupField)null;
		}
		#endregion Public 方法...

		#region Public 属性...
		/// <summary>
		/// 当前页的Index
		/// </summary>
		public int CurrPageFirstRowIndex{
			get{
				return _CurrPageFirstRowIndex;
			}
			set{
				_CurrPageFirstRowIndex = value;
			}
		} 
		/// <summary>
		/// 当前绘制页的number 
		/// </summary>
		public int CurrPageNumber{
			get{
				return _CurrPageNumber;
			}
			set{
				_CurrPageNumber = value;
			}
		}
		/// <summary>
		/// 分组的字段 
		/// </summary>
		public IList GroupFields{
			get{
				return _GroupFields;
			}
			set{
				_GroupFields = value;
			}
		}
		/// <summary>
		/// 当前已经绘制到的行的index 
		/// </summary>
		public int CurrDrawRowIndex{
			get{
				return _CurrDrawRowIndex;
			}
			set{
				_CurrDrawRowIndex = value;
			}
		}
		#endregion Public 属性...
	}

	#endregion DrawDetailInfo...
}
