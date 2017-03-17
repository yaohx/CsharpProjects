//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-21
// Description	:	 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections ;
using System.Text.RegularExpressions;
using System.Reflection ;

namespace DIYReport.Express
{
	/// <summary>
	/// ExpressValueList 表达试值的集合
	/// </summary>
	public class ExpressValueList : Hashlist   
	{
		/// <summary>
		///  表达式
		/// </summary>
		/// <param name="_DataReport"></param>
		/// <returns></returns>
		
		public static ExpressValueList GetBottomExpress(DIYReport.ReportModel.RptReport pDataReport){
			DIYReport.ReportModel.RptSection section =  pDataReport.SectionList.GetSectionByType(DIYReport.SectionType.ReportBottom);   
			return new ExpressValueList(section); 
		}
		public static ExpressValueList GetFooterExpress(DIYReport.ReportModel.RptReport pDataReport){
			DIYReport.ReportModel.RptSection section =  pDataReport.SectionList.GetSectionByType(DIYReport.SectionType.PageFooter);   
			return new ExpressValueList(section); 
		}

		//把对象集合中需要统计计算的对象加入到表达的集合中
		//把需要统计的字段加上表达试做为主键

		#region 构造函数...
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pObjects"></param>
		public ExpressValueList(DIYReport.ReportModel.RptSection   pSection) {
			if(pSection!=null) {
				DIYReport.ReportModel.RptSingleObjList objList = pSection.RptObjList; 
				foreach(DIYReport.ReportModel.RptSingleObj    obj in objList) {
					if(obj.Type== DIYReport.ReportModel.RptObjType.Express){
						DIYReport.ReportModel.RptObj.RptExpressBox box = obj as DIYReport.ReportModel.RptObj.RptExpressBox;
						if(box.ExpressType == DIYReport.ReportModel.ExpressType.Express){
							this.Add(new ExpressValue(obj));
						}//end if
					}//end if
				}//end foreach
			}//end if
		}
		#endregion 构造函数...

		#region Public 方法...
		/// <summary>
		/// 通过字段的名称得到相应的统计对象
		/// </summary>
		/// <param name="pName"></param>
		/// <returns></returns>
		public ExpressValue GetExpressObjByFieldName(string pName){
			foreach( ExpressValue val in this.Values ) {
				if(val.FieldName.Trim() == pName.Trim()){
					return val;
				}
			}
			return null;
		}

		/// <summary>
		/// 清空所有的值
		/// </summary>
		public void ClearValues() {
			foreach( ExpressValue val in this.Values ) {
				val.Value= "0" ;
			}
		}
		#region this...
		public new ExpressValue this[string pKey] {
			get {
				if(base[pKey]!=null) {
					ExpressValue val = base[pKey] as ExpressValue;
					return val; 
				}
				else
					return null;

			}
		}
		#endregion this...
		public ExpressValue Add(ExpressValue pExpress) {
			this.Add(pExpress.Name ,pExpress);
			return pExpress;
		}
		#endregion Public 方法...
	}
}
