//---------------------------------------------------------------- 
// Author		:	Nick
// Create date	:	2009-02-13
// Description	:	ColPivotList 字段进行轴分组拖动的参数集合类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace MB.XWinLib.PivotGrid
{
	/// <summary>
	/// ColPivotList 字段进行轴分组拖动的参数集合类。
	/// </summary>
	public class ColPivotList 
	{
		private IList<ColPivotInfo> _Columns;
		//在不配置的情况下是否创建表对应的字段项。
		private bool _AutoCreatedGridField;
		//判断字段名称相同的pivotefield 是否联合在一起
		private bool _SameFieldGroup;
		//字段帮定分组的情况 不同分组之间以分号割开，同一组的不同字段之间以逗号分开。
		private  string _FieldGroups;
		//private Il
		#region 构造函数...
		/// <summary>
		/// 构造函数...
		/// </summary>
		public ColPivotList() 
		{
			_Columns = new List<ColPivotInfo>() ;
			_SameFieldGroup = true;
			_AutoCreatedGridField = true;
		}
		#endregion 构造函数...

 
		
		#region Public 方法...
		/// <summary>
		/// 根据字段的名称获取配置对应的信息。有可能存在一个字段对应多个Pivot分组的信息。
		/// </summary>
		/// <param name="fieldName"></param>
		/// <returns></returns>
		public IList<ColPivotInfo> GetColPivotInfos(string fieldName){
			IList<ColPivotInfo> pivots = new List<ColPivotInfo>();
			foreach(ColPivotInfo info in this.Columns){
				if(string.Compare(info.FieldName,fieldName,true)!=0)
					continue;
				pivots.Add(info);
			}
			return pivots;
		}

		/// <summary>
		/// Public 方法。
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public ColPivotInfo Add(ColPivotInfo info){
			_Columns.Add(info);
			return info;
		}
		#endregion Public 方法...

		#region 扩展的Public 属性
		/// <summary>
		/// 所有列的配置信息。
		/// </summary>
        public IList<ColPivotInfo> Columns {
			get{
				return _Columns;
			}
		}

		public bool AutoCreatedGridField{
			get{
				return _AutoCreatedGridField;
			}
			set{
				_AutoCreatedGridField = value;
			}
		}
		public bool SameFieldGroup{
			get{
				return _SameFieldGroup;
			}
			set{
				_SameFieldGroup = value;
			}
		}
		public string FieldGroups{
			get{
				return _FieldGroups;
			}
			set{
				_FieldGroups = value;
			}
		}
		#endregion 扩展的Public 属性
	}
}
