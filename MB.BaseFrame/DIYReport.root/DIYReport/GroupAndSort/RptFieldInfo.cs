//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2005-04-19
// Description	:	报表设计字段的描述
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;

namespace DIYReport.GroupAndSort
{
	/// <summary>
	/// FieldInfo 报表设计字段的描述
	/// </summary>
	public class RptFieldInfo : ICloneable	 
	{
		#region 变量定义...
		private string _FieldName;
		private string _Description;
		private string _DataType;
		private int _OrderIndex;
		//判断是否选择作为分组的字段
		private bool _IsGroup;
		//在多字段分组中的顺序(从0开始)
		//private int _OrderIndex;
		//分组的间隔名称
		private string _DivideName;
		//该字段是否为升级排序 
		private bool _IsAscending;
		//判断是否设置排序的控制
		private bool _SetSort;
		#endregion 变量定义...

		#region 构造函数...
		public RptFieldInfo(){
		}
		public RptFieldInfo(string pFieldName){
			_FieldName = pFieldName;
			_Description = pFieldName;
			_DataType = "String";
		}
		public RptFieldInfo(string pFieldName,string pDescription ,string pDataType,int pOrderIndex){
			_FieldName = pFieldName;
			_Description = pDescription;
			_DataType = pDataType;
			_OrderIndex = pOrderIndex;
		}
		#endregion 构造函数...

		#region 覆盖ToString方法...
		public override string ToString() {
			return _Description;
		}

		#endregion 覆盖ToString方法...

		public object Clone(){
			return this.MemberwiseClone();
		}

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
		public string DataType{
			get{
				return _DataType;
			}
			set{
				_DataType = value;
			}
		}
		public int OrderIndex{
			get{
				return _OrderIndex;
			}
			set{
				_OrderIndex = value;
			}
		}
		public bool IsGroup{
			get{
				return _IsGroup;
			}
			set{
				_IsGroup = value;
			}
		}
		public string  DivideName{
			get{
				return _DivideName;
			}
			set{
				_DivideName = value;
			}
		} //
		public bool IsAscending{
			get{
				return _IsAscending;
			}
			set{
				_IsAscending = value;
			}
		}
		public bool SetSort{
			get{
				return _SetSort;
			}
			set{
				_SetSort = value;
			}
		}
		#endregion Public 属性...
	}
}
