//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-19
// Description	:	ClipboardEx 剪贴板数据相关处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace DIYReport.UserDIY {
	/// <summary>
	/// ClipboardEx 剪贴板数据相关处理。
	/// </summary>
	public class ClipboardEx {
		private static readonly string MY_CLIPBOARD_DATA_FORMAT_NAME = "DIYReport.ReportModel.RptSingleObj" ;
		/// <summary>
		/// add private construct function to prevent instance.
		/// </summary>
		private ClipboardEx() {
		
		}
		/// <summary>
		/// 从剪贴板中获取
		/// </summary>
		/// <returns></returns>
		public static IList GetFromClipBoard(){
			IDataObject ido = Clipboard.GetDataObject(); 
			if(ido.GetDataPresent(MyClipbordData.Format.Name)) {
				object obj = ido.GetData(MyClipbordData.Format.Name,true);
				MyClipbordData cbCtrl = obj as MyClipbordData; 
				
				return cbCtrl.DataToIList(); 
			}
			return null;
		}
		/// <summary>
		/// 把数据复制到剪贴板中。
		/// </summary>
		/// <param name="ctls"></param>
		public static void CopyToClipBoard(IList ctls){
			IDataObject ido = new DataObject();
			MyClipbordData cData = new MyClipbordData(ctls);

			ido.SetData(MyClipbordData.Format.Name,true,cData);
			Clipboard.SetDataObject(ido,false);
		}

	}
	/// <summary>
	/// 存储在剪贴板中的数据格式定义。
	/// </summary>
	[Serializable()]
	public class MyClipbordData{
		private static DataFormats.Format _Format; 
		private ArrayList _Ctls;

		#region 构造函数...
		static MyClipbordData(){
			_Format =  DataFormats.GetFormat(typeof(MyClipbordData).FullName);
		}
		public MyClipbordData(){
		}
		public MyClipbordData(IList cData){
			_Ctls = new ArrayList();
			if(cData==null)
				return;
			foreach(object ctl in cData){
				DIYReport.Interface.IRptSingleObj rptObj = ctl as DIYReport.Interface.IRptSingleObj;
				if(rptObj==null)
					continue;
				MySingleRptCtlData ctlData = new MySingleRptCtlData(rptObj);
				_Ctls.Add(ctlData);
			}
		}
		#endregion 构造函数...
		/// <summary>
		/// 转换为Diyreport 设计需要的控件格式。
		/// </summary>
		/// <returns></returns>
		public IList DataToIList(){
			TrackEx.Write("正在加载自定义报表组件:" +  ReportXmlHelper.REPORT_ASSEMBLY);
			System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(DIYReport.ReportXmlHelper.REPORT_ASSEMBLY);
			TrackEx.Write("自定义报表组件加载成功！");

			ArrayList ctlList = new ArrayList();
			foreach(MySingleRptCtlData ctl in _Ctls){
				DIYReport.Interface.IRptSingleObj rptObj = asm.CreateInstance(ctl.CtrlFullName) as DIYReport.Interface.IRptSingleObj;    
				if(rptObj==null)
					continue;
				rptObj.BeginUpdate();
				setRptCtlProperties(rptObj,ctl.PropertyList);
				rptObj.EndUpdate();

				ctlList.Add(rptObj);
			}
			return ctlList;
		}
		//设置控件的属性值。
		private static void setRptCtlProperties(DIYReport.Interface.IRptSingleObj rptObj,Hashtable propertyList) {
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(rptObj);

			foreach (PropertyDescriptor myProperty in properties) {	
				if(!propertyList.Contains(myProperty.Name))
					continue;
				object val = propertyList[myProperty.Name];
				try {
					myProperty.SetValue(rptObj,val);
				}
				catch(Exception ex) {
					DIYReport.TrackEx.Write("在进行剪贴板操作的时候,设置控件的属性值时出错。" + ex.Message);
				}
			}	
		}

		public static DataFormats.Format Format{
			get{
				return _Format;
			}
		}
	}
	/// <summary>
	/// 单个报表设计控件对应的数据。
	/// </summary>
	[Serializable()]
	public class MySingleRptCtlData{
		private string _CtrlFullName;
		private Hashtable _PropertyList;
		
		#region 构造函数...
		public MySingleRptCtlData() {
			
		}

		public MySingleRptCtlData(DIYReport.Interface.IRptSingleObj  ctrl) {		
			_PropertyList = new Hashtable();

			_CtrlFullName = ctrl.GetType().FullName;
			
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(ctrl);
			foreach (PropertyDescriptor myProperty in properties) {
				try {
					if(!myProperty.PropertyType.IsSerializable)
						continue;
					object val = myProperty.GetValue(ctrl);
					if(val!=null){
						Type objType = val.GetType();
						System.Reflection.MethodInfo methInfo = objType.GetMethod("Clone"); 
						if(methInfo!=null){
							val = methInfo.Invoke(val,null);
						}
					}
					_PropertyList.Add(myProperty.Name,val);	
				}
				catch(Exception ex) {
					DIYReport.TrackEx.Write("在进行剪贴板操作的时候,获取属性的值出错。" + ex.Message);
				}
			}

		}	
		#endregion 构造函数...

		public string CtrlFullName {
			get {
				return _CtrlFullName;
			}
			set {
				_CtrlFullName = value;
			}
		}

		public Hashtable PropertyList {
			get {
				return _PropertyList;
			}
			
		}
	}
}
