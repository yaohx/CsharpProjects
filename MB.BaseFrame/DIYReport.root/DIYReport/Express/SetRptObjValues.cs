////---------------------------------------------------------------- 
//// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
//// All rights reserved. 
//// 
//// Author		:	Nick
//// Create date	:	2004-12-21
//// Description	:	 
//// 备注描述：  
//// Modify date	:			By:					Why: 
////----------------------------------------------------------------
//using System;
//using System.Data ;
//
//namespace DIYReport.Express
//{
//	/// <summary>
//	/// SetRptValues 的摘要说明。
//	/// </summary>
//	public class SetRptObjValues
//	{
//		/// <summary>
//		/// 在开始打印之前设置每一个打印对象的值
//		/// </summary>
//		/// <param name="_DataReport"></param>
//		/// <param name="pDataSource"></param>
//		public static void SetIniValues(DIYReport.ReportModel.RptReport _DataReport,DataSet pDataSource){
//			DIYReport.ReportModel.RptSectionList sectionList = _DataReport.SectionList ;
//			foreach(DIYReport.ReportModel.RptSection section in sectionList){
//				DIYReport.ReportModel.RptSingleObjList objList = section.RptObjList ;
//				foreach(DIYReport.ReportModel.RptSingleObj obj in objList){
//					if(obj.Type== DIYReport.ReportModel.RptObjType.Express){
//						DIYReport.ReportModel.RptObj.RptExpressBox box = obj as DIYReport.ReportModel.RptObj.RptExpressBox;
//						switch(box.ExpressType){
//							case DIYReport.ReportModel.ExpressType.Express:
//
//								break;
//							case DIYReport.ReportModel.ExpressType.Field:
//								break;
//							case DIYReport.ReportModel.ExpressType.Param:
//								break;
//							default:
//								break;
//						}
//					}
//				}
//			}
//		}
//	}
//}
