//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-22
// Description	:	 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data ;
using System.Collections ;
using System.Reflection ;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace DIYReport.Design {
	/// <summary>
	/// RptFieldAttributesEditor 字段属性编辑。
	/// </summary>
	public class RptDataSourceEditor : UITypeEditor {
		private IServiceProvider _Provider;
		public RptDataSourceEditor() {
		}
		public override object EditValue(ITypeDescriptorContext context, 
			IServiceProvider provider, object value) {
		

			_Provider = provider;
			//Create the listbox for display
			ListBox    lstFields = new ListBox();
			lstFields.SelectedIndexChanged +=new EventHandler(lstFields_SelectedIndexChanged);

			addFieldList(lstFields);

			// Display the combolist
			((IWindowsFormsEditorService)provider.GetService(
				typeof(IWindowsFormsEditorService))).DropDownControl(lstFields);
			if(lstFields.SelectedItem!=null){
				string str = lstFields.SelectedItem.ToString(); 
				return str; 
			}
			else{
				return value;
			}
		
		}  

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
			
			// Should we return the style for our listbox?
			if ((context != null) && (context.Instance != null))
				return UITypeEditorEditStyle.DropDown;
			
			// Return the default edit style.
			return base.GetEditStyle(context);
			//return UITypeEditorEditStyle.DropDown ;
		} // End GetEditStyle()

		#region 内部处理相关...
		//
		//增加数据库绑定字段
		private void addFieldList(ListBox pLst){
			pLst.Items.Clear(); 
			IList fieldList = DIYReport.UserDIY.DesignEnviroment.CurrentReport.DesignField;

			DIYReport.ReportModel.RptObj.RptExpressBox obj = DIYReport.UserDIY.DesignEnviroment.CurrentRptObj as DIYReport.ReportModel.RptObj.RptExpressBox ;    
			
			switch(obj.ExpressType){
				case DIYReport.ReportModel.ExpressType.Field :
					if(fieldList!=null && fieldList.Count > 0 ){
						foreach(object dc in fieldList){
							if(dc.GetType().Name == "RptFieldInfo"){
								 DIYReport.GroupAndSort.RptFieldInfo dcInfo = dc as DIYReport.GroupAndSort.RptFieldInfo;
								if(dcInfo.Description==null || dcInfo.Description.Trim().Length ==0) continue;
								pLst.Items.Add(dcInfo.Description);  
							}
							else{
								pLst.Items.Add(dc.ToString());
							}
						}
					}
					break;
				case DIYReport.ReportModel.ExpressType.SysParam:
					Type clsType = System.Type.GetType("DIYReport.Express.ExSpecial");
					MethodInfo[] infos =   clsType.GetMethods(); 
					foreach(MethodInfo info in infos){
						if(info.IsPublic && info.IsStatic ){ 
							bool hasExist = hasAddStr(pLst,info.Name);
							if(!hasExist){
								pLst.Items.Add(info.Name);
							}
						}
					}
					break;
				case DIYReport.ReportModel.ExpressType.UserParam ://用户外部参数
					DIYReport.ReportModel.RptParamList paramList = DIYReport.UserDIY.DesignEnviroment.CurrentReport.UserParamList;
					if(paramList!=null){
						foreach(DIYReport.ReportModel.RptParam param in paramList.Values ){
							pLst.Items.Add(param.ParamName);
						}
					}
					break;
				case DIYReport.ReportModel.ExpressType.Express :
					Type exclsType = System.Type.GetType("DIYReport.Express.ExStatistical");
					MethodInfo[] exinfos =   exclsType.GetMethods(); 
					foreach(MethodInfo info in exinfos){
						if(info.IsPublic && info.IsStatic ){ 
							bool hasExist = hasAddStr(pLst,info.Name);
							if(!hasExist){
								if(info.Name!="GetStatisticalValue"){
									pLst.Items.Add(info.Name);
								}
							}
						}
					}
					break;
				default:
					break;
			}
		}
 
		#endregion 内部处理相关...
		private bool hasAddStr(ListBox pLst,string pName){
			int count = pLst.Items.Count ;
			for(int i = 0;i <count;i++){
				if(pLst.Items[i].ToString() == pName){
					return true;
				}
			}
			return false;
			//foreach( lstFields.Items
		}

		private void lstFields_SelectedIndexChanged(object sender, EventArgs e) {
			((IWindowsFormsEditorService)_Provider.GetService(
				typeof(IWindowsFormsEditorService))).CloseDropDown();
		}
	}
}
