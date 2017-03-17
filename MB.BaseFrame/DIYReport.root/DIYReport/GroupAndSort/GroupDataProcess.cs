//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2005-04-19
// Description	:	处理分组的数据。
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics; 

namespace DIYReport.GroupAndSort
{
	/// <summary>
	/// GroupDataProcess 处理分组的数据。
	/// </summary>
	public class GroupDataProcess
	{
		#region public 方法...
		/// <summary>
		/// 把报表打印的数据按照统计分组的方式进行排序；
		/// </summary>
		/// <param name="pDs"></param>
		/// <param name="pDataReport"></param>
		/// <returns></returns>
		public static DataRow[] SortData(object pDs,DIYReport.ReportModel.RptReport pDataReport){
			if(pDs==null)
				return new DataRow[0];
			DataView dv = PublicFun.GetDataViewByObject(pDs);

			//构造排序的字符窜
			string sortStr = getSortStr(dv.Table,pDataReport);
			return dv.Table.Select(dv.RowFilter,sortStr);
		}

		public static bool ValueInTheGroup(DIYReport.ReportModel.RptSection pGroupSection,object pGroupValue , object pValue){
			if(pGroupValue==null){
				return true;
			}
			//先处理简单的相等分组
			if(pGroupValue == System.DBNull.Value){
				if( pValue == System.DBNull.Value){
					return true;
				}
			}
			if (pValue == System.DBNull.Value) {
				return false;
			}
			return pGroupValue.ToString() == pValue.ToString();

		}
		#endregion public 方法...

		#region 内部处理函数...
		//构造排序的字符窜
		private static string getSortStr(DataTable pDt, DIYReport.ReportModel.RptReport pDataReport){
			DIYReport.ReportModel.RptSectionList sectionList =   pDataReport.SectionList; 
			string filterStr = "";
			foreach(DIYReport.ReportModel.RptSection section in sectionList){
				if(section.SectionType == DIYReport.SectionType.GroupHead){
					string asc = section.GroupField.IsAscending ? " ASC ," :" DESC ,";
					string sname = section.GroupField.FieldName;
					if(pDt.Columns.Contains(sname)){
						filterStr += section.GroupField.FieldName  + asc;
					}
				}
			}
			if( filterStr!=""){
				filterStr = filterStr.Remove(filterStr.Length - 1,1);
			}
			return filterStr;
		}
		#endregion 内部处理函数...
	}
}
