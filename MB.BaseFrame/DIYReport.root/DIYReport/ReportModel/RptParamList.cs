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
using System.Data;
using System.Collections;
using System.Xml ;

using DIYReport;
namespace DIYReport.ReportModel
{
	/// <summary>
	/// RptParamList报表用户描述集合
	/// </summary>
	public class RptParamList : Hashlist {
		public RptParamList() {	
		}
		public new RptParam this[string key] {
			get {
				if(base[key]!=null)
					return  base[key] as RptParam;
				else
					return null;
			}
		}
		/// <summary>
		/// 把定单标题中相关的字段转换成报表用户参数的方式 
		/// </summary>
		/// <returns></returns>
		public static DIYReport.ReportModel.RptParamList RowValueToParamList(DataRow drData){
			if(drData==null || drData.Table ==null)
				return null;
			DataTable dt = drData.Table ;
			DIYReport .ReportModel.RptParamList paramList = new DIYReport.ReportModel.RptParamList();
			paramList.Clear();   
			foreach(DataColumn dc in dt.Columns){
				object objValue =  drData[dc.ColumnName];
				paramList.Add(new DIYReport.ReportModel.RptParam(dc.ColumnName,objValue));
			}
			return paramList;
		}
		#region Add...
		/// <summary>
		///  add
		/// </summary>
		/// <param name="pName"></param>
		/// <param name="PValue"></param>
		/// <returns></returns>
		public RptParam Add(string pName,object PValue){
			return this.Add(new RptParam(pName,PValue));
		}
		/// <summary>
		/// add
		/// </summary>
		/// <param name="pParam"></param>
		/// <returns></returns>
		public RptParam Add(RptParam pParam) {
			base.Add(pParam.ParamName,pParam);
			return pParam;
		}
		#endregion Add...
	}
}
