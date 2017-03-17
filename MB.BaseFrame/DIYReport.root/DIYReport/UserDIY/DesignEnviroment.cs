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
using System.Data ;
using System.Collections ;
using System.Windows.Forms ;
using System.Drawing.Printing;
namespace DIYReport.UserDIY {
	/// <summary>
	/// DesignEnviroment 用户DIY报表设计的外部参数信息。
	/// </summary>
	public class DesignEnviroment{
		private static bool _PressShiftKey;
		private static bool _PressCtrlKey;
		private static DIYReport.ReportModel.RptObjType  _DrawControlType;
		private static bool _IsCreateControl;
		private static PageSettings _PageSettings;
		//当前正在设计的报表
		private static DIYReport.ReportModel.RptReport _CurrentReport;
		//当前正在获取焦点的报表对象
		private static object _CurrentRptObj;
		//报表对应的数据
		private static object  _DataSource ;
		//报表设计时的字段
		private static IList _DesignField;

		//private static DIYReport.ReportModel.RptParamList _UserParamList ;  
		//判断当前报表的设计类型
		private static bool _IsUserDesign = false;
		//判断当前报表设计是否已经发生改变，通过它来让用户决定是否存储
		private static bool _DesignHasChanged = false;

		//nick 2006-04-07 增加
		private static UICommandExecutor _UICmidExecutor;

		#region Public 方法...
		//		/// <summary>
		//		/// 用户创建报表的参数信息
		//		/// </summary>
		//		public static DIYReport.ReportModel.RptParamList UserParamList{
		//			get{
		//				if(_UserParamList==null){
		//					_UserParamList = new DIYReport.ReportModel.RptParamList();
		//				}
		//				return _UserParamList;
		//			}
		//			set{
		//				_UserParamList = value;
		//			}
		//		}
		public static object  DataSource{
			get{
				return _DataSource;
			}
			set{
				_DataSource = value;
			}
		}
		/// <summary>
		/// 显示属性窗口
		/// </summary>
		/// <param name="pParentForm"> 显示该窗口的父窗口</param>
		/// <param name="pMustShow">判断是否必须显示</param>
		public static void ShowPropertyForm(Form pParentForm,bool pMustShow){			
			//			bool b = false;
			//			foreach(Form frm in pParentForm.OwnedForms){
			//				if(frm.Name == "FrmEditRptObjAttribute"){
			//					(frm as FrmEditRptObjAttribute).SetPropertryObject( _CurrentRptObj);
			//					b = true;
			//					break;
			//				}
			//			}
			//			if(!b && pMustShow == true){
			//				FrmEditRptObjAttribute frm =  new FrmEditRptObjAttribute();
			//				frm.SetPropertryObject( _CurrentRptObj);
			//				pParentForm.AddOwnedForm(frm);
			//				frm.Show();
			//			}
		}
		//		/// <summary>
		//		/// 如果属性窗口已经显示，当点击控件的时候，显示相应的属性
		//		/// </summary>
		//		public static void DispProperty(){
		//			if(_PropertyForm!=null && _PropertyForm.Visible){
		//				_PropertyForm.SetPropertryObject( _CurrentRptObj);
		//			}
		//		}
		/// <summary>
		/// 根据字段的描述得到字段的名称
		/// </summary>
		/// <param name="pFieldDesc"></param>
		/// <returns></returns>
		public static string GetFieldNameByDesc(string pFieldDesc){
			if(pFieldDesc==null || pFieldDesc=="" || _CurrentReport == null || _CurrentReport.DesignField ==null)
				return pFieldDesc;
			foreach(DIYReport.GroupAndSort.RptFieldInfo info in _CurrentReport.DesignField){
				if(info.Description.ToUpper() ==  pFieldDesc.ToUpper()){
					return info.FieldName ;
				}
			}
			return pFieldDesc;
		}
		#endregion Public 方法...

		#region Public 属性...
		public static bool IsUserDesign{
			get{
				return _IsUserDesign;
			}
			set{
				_IsUserDesign = value;
			}
		}
		public static object CurrentRptObj{
			get{
				return _CurrentRptObj;
			}
			set{
				_CurrentRptObj = value;
				//DispProperty();
			}
		}
		public static DIYReport.ReportModel.RptReport CurrentReport{
			get{
				return _CurrentReport;
			}
			set{
				_CurrentReport = value;
			}
		}
		public static PageSettings PageSettings{
			get{
				if(_PageSettings==null){
					_PageSettings = new PageSettings();
				}
				return _PageSettings;
			}
			set{
				_PageSettings = value;
			}
		}
		public static bool IsCreateControl{
			get{
				return _IsCreateControl;
			}
			set{
				_IsCreateControl = value;
			}
		}
		/// <summary>
		/// 创建控件的类型 //
		/// </summary>
		public static DIYReport.ReportModel.RptObjType DrawControlType{
			get{
				return _DrawControlType;
			}
			set{
				_DrawControlType = value;
			}
		}
		public static bool PressShiftKey{
			get{
				return _PressShiftKey;
			}
			set{
				_PressShiftKey = value;
			}
		}
		public static bool PressCtrlKey{
			get{
				return _PressCtrlKey;
			}
			set{
				_PressCtrlKey = value;
			}
		}
		public static bool DesignHasChanged{
			get{
				return _DesignHasChanged;
			}
			set{
				_DesignHasChanged = value;
			}
		}
		public static IList DesignField{
			get{
				if(_DesignField==null || _DesignField.Count==0){
					_DesignField = new ArrayList(); 
					if(_DataSource!=null  ){
						DataView  dv = PublicFun.GetDataViewByObject(_DataSource);  
						DataTable dt = dv.Table ;
						int i = 0;
						foreach(DataColumn dc in dt.Columns){
							//限制ID的显示作为报表的设计
							if( string.Compare(dc.ColumnName,"ID",true)==0) continue;

							DIYReport.GroupAndSort.RptFieldInfo info = new DIYReport.GroupAndSort.RptFieldInfo(dc.ColumnName,dc.Caption
																			,dc.DataType.Name,i++);
							_DesignField.Add( info );
							

						}
					}
				}
				return _DesignField;
			}
			set{
				IList temp = value;
				IList newList = null ;
				if(temp!=null){
					if(temp.Count >0){
						if(temp[0].GetType().Name!= "RptFieldInfo"){
							newList = new ArrayList();
							for(int i = 0 ; i < temp.Count;i++){
								object field = temp[i];
								if(string.Compare(field.GetType().Name,"String",true)==0) {
									DIYReport.GroupAndSort.RptFieldInfo info = new DIYReport.GroupAndSort.RptFieldInfo(temp[i].ToString());
									newList.Add(info);
								}
								else{
									newList.Add(field);
								}
							}
						}
						else{
							newList = value;
						}
					}
				}
				_DesignField = newList;
			}
		}
		public static UICommandExecutor UICmidExecutor{
			get{
				return _UICmidExecutor;
			}
			set{
				_UICmidExecutor = value;
			}
		}
		#endregion Public 属性...
	}
}
