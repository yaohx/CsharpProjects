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
/*
 我用一个思路：从System.ComponentModel.PropertyDescriptor继承自己的类，然后
 从在DisplayName，在里面实现一个查表功能，使对应的英文名返回中文。最后要把PropertyGrid.SelectObject
  所选的对象从ICustomTypeDescriptor继承，并在其中的GetProperties中构造自己的PropertyDescriptorCollection用于返回。
*/
using System;
using System.Data ;
using System.Reflection ;
using System.Collections ;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using DIYReport.Common;

namespace DIYReport.Design {
	/// <summary>
	/// BaseDesignListEditor  ListBox xia  。
	/// </summary>
	public class BaseDesignListEditor : UITypeEditor {
		protected IServiceProvider _Provider;
		public BaseDesignListEditor() {
		}
		public override object EditValue(ITypeDescriptorContext context, 
			IServiceProvider provider, object value) {
			_Provider = provider;
			//Create the listbox for display
			ListBox    lstFields = new ListBox();
			lstFields.SelectedIndexChanged +=new EventHandler(lstFields_SelectedIndexChanged);

			AddDataToList(lstFields);

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
		protected virtual void AddDataToList(ListBox pLst){
			
		}
 
		#endregion 内部处理相关...
		private void lstFields_SelectedIndexChanged(object sender, EventArgs e) {
			((IWindowsFormsEditorService)_Provider.GetService(
				typeof(IWindowsFormsEditorService))).CloseDropDown();
		}
	}
	/// <summary>
	/// 选择显示报表数据源的字段
	/// </summary>
	public class RptFieldAttributesEditor : BaseDesignListEditor{
		protected override void AddDataToList(ListBox pLst) {
			pLst.Items.Clear(); 
			IList fieldList = DIYReport.UserDIY.DesignEnviroment.CurrentReport.DesignField;
			if(fieldList!=null && fieldList.Count > 0 ){
				foreach(object dc in fieldList){
					if(dc.GetType().Name == "RptFieldInfo"){
						DIYReport.GroupAndSort.RptFieldInfo field = dc as DIYReport.GroupAndSort.RptFieldInfo;
						if(string.Compare(field.DataType, "Byte[]",true)!=0){ //byte[] 数组类型不需要进行分组
							if(field.Description==null || field.Description.Trim().Length ==0) continue;

							pLst.Items.Add(field.Description);  
						}
					}
					else{
						pLst.Items.Add(dc.ToString());
					}
				}
			}
		}

	}
	/// <summary>
	///  显示子报表选择的编辑框
	/// </summary>
	public class RptSubReportAttributesEditor : BaseDesignListEditor{
		protected override void AddDataToList(ListBox pLst) {
			pLst.Items.Clear(); 
			if(DIYReport.UserDIY.DesignEnviroment.CurrentReport.SubReportCommand!=null){
				IList names = DIYReport.UserDIY.DesignEnviroment.CurrentReport.SubReportCommand.SubReportName;
				if(names==null || names.Count== 0)
					return;
				foreach(object name in names){
					pLst.Items.Add(name.ToString());
				}
			}
			else{
				Hashtable  fieldList = DIYReport.UserDIY.DesignEnviroment.CurrentReport.SubReports;
				if(fieldList==null || fieldList.Count == 0 )
					return;
				foreach(object key in fieldList.Keys){
					pLst.Items.Add(key.ToString()); 
				}
			}
		}
	}
	/// <summary>
	///  显示子报表选择的编辑框
	/// </summary>
	public class RelationMemberAttributesEditor : BaseDesignListEditor{
		protected override void AddDataToList(ListBox pLst) {
			pLst.Items.Clear(); 
			DataSet ds = DIYReport.PublicFun.GetDataSetByObject(DIYReport.UserDIY.DesignEnviroment.CurrentReport.DataSource);
			if(ds==null || ds.Relations.Count ==0 )
				return;
			foreach(System.Data.DataRelation  relation in ds.Relations){
				pLst.Items.Add(relation.RelationName); 
			}
		}
	}
    /// <summary>
    /// 显示本地的打印机
    /// </summary>
    public class EnumPrinterListEditor : BaseDesignListEditor
    {
        protected override void AddDataToList(ListBox pLst) {
            pLst.Items.Clear();
            var printers = DIYReport.Common.EnumPrintersHelperEx.GetLocalPrinters();//.EnumPrinters(PrinterEnumFlags.PRINTER_ENUM_LOCAL | PrinterEnumFlags.PRINTER_ENUM_NETWORK);
            if (printers == null || printers.Count == 0)
                return;
            foreach (var p in printers) {
                pLst.Items.Add(p);
            }
        }
    }
}
