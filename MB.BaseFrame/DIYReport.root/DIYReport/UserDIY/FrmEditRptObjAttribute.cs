////---------------------------------------------------------------- 
//// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
//// All rights reserved. 
//// 
//// Author		:	Nick
//// Create date	:	2004-12-15
//// Description	:	 
//// 备注描述：  
//// Modify date	:			By:					Why: 
////----------------------------------------------------------------
//using System;
//using System.Drawing;
//using System.Collections;
//using System.ComponentModel;
//using System.Windows.Forms;
//using System.Runtime.InteropServices;
//
//namespace DIYReport.UserDIY
//{
//	/// <summary>
//	/// FrmEditRptObjAttribute 打印对象属性编辑。
//	/// </summary>
//	public class FrmEditRptObjAttribute : System.Windows.Forms.Form
//	{
//		#region 内部自动生成代码...
//
// 
//		private System.Windows.Forms.PropertyGrid pgridMain;
//		private System.Windows.Forms.ComboBox cobControls;
//	 	private System.ComponentModel.IContainer components = null;
//
//		/// <summary>
//		/// 清理所有正在使用的资源。
//		/// </summary>
//		protected override void Dispose( bool disposing )
//		{
//			if( disposing )
//			{
//				if(components != null)
//				{
//					components.Dispose();
//				}
//			}
//			base.Dispose( disposing );
//		}
//
//		#region Windows 窗体设计器生成的代码
//		/// <summary>
//		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
//		/// 此方法的内容。
//		/// </summary>
//		private void InitializeComponent()
//		{
//			this.cobControls = new System.Windows.Forms.ComboBox();
//			this.pgridMain = new System.Windows.Forms.PropertyGrid();
//			this.SuspendLayout();
//			// 
//			// cobControls
//			// 
//			this.cobControls.Dock = System.Windows.Forms.DockStyle.Top;
//			this.cobControls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//			this.cobControls.Location = new System.Drawing.Point(0, 0);
//			this.cobControls.Name = "cobControls";
//			this.cobControls.Size = new System.Drawing.Size(200, 20);
//			this.cobControls.TabIndex = 0;
//			this.cobControls.SelectedIndexChanged += new System.EventHandler(this.cobControls_SelectedIndexChanged);
//			// 
//			// pgridMain
//			// 
//			this.pgridMain.CommandsVisibleIfAvailable = true;
//			this.pgridMain.Dock = System.Windows.Forms.DockStyle.Fill;
//			this.pgridMain.LargeButtons = false;
//			this.pgridMain.LineColor = System.Drawing.SystemColors.ScrollBar;
//			this.pgridMain.Location = new System.Drawing.Point(0, 20);
//			this.pgridMain.Name = "pgridMain";
//			this.pgridMain.Size = new System.Drawing.Size(200, 386);
//			this.pgridMain.TabIndex = 2;
//			this.pgridMain.Text = "propertyGrid1";
//			this.pgridMain.ViewBackColor = System.Drawing.SystemColors.Window;
//			this.pgridMain.ViewForeColor = System.Drawing.SystemColors.WindowText;
//			// 
//			// FrmEditRptObjAttribute
//			// 
//			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
//			this.ClientSize = new System.Drawing.Size(200, 406);
//			this.Controls.Add(this.pgridMain);
//			this.Controls.Add(this.cobControls);
//			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
//			this.MaximizeBox = false;
//			this.Name = "FrmEditRptObjAttribute";
//			this.ShowInTaskbar = false;
//			this.Text = "属性";
//			this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmEditRptObjAttribute_Closing);
//			this.ResumeLayout(false);
//
//		}
//		#endregion
//
//		#endregion 内部自动生成代码...
//
//		private DIYReport.ReportModel.RptReport    _Report;
//
//		#region 构造函数...
//		public FrmEditRptObjAttribute() {
//			InitializeComponent();
//			_Report = DesignEnviroment.CurrentReport ;
//			pgridMain.SelectedObject =  DesignEnviroment.CurrentRptObj ;
//
//			addCtlToList();
//			//让窗口在最顶层
//			DIYReport.PublicFun.SetWindowPos(this.Handle,-1,0,0,0,0,3);
//		 
//			 this.Location = this.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - this.Width ,this.Top ); 
//		}
//		#endregion 构造函数...
//
////		private void timer1_Tick(object sender, System.EventArgs e) {
////			//刷新控件的属性
////			object obj = _Report.CurrentObj;
////			if(pgridMain.SelectedObject==null || pgridMain.SelectedObject.Equals(obj)!=false){
////				pgridMain.SelectedObject =  _Report.CurrentObj ;
////			}
////		}
//
////		[DllImport("User32.dll")]
////		private static extern bool SetWindowPos(IntPtr hwnd,int hWndInsertAfter,int x,int y,int cx,int cy,int wFlagslong);
//		#region Public 方法...
//		public void SetPropertryObject(object pObject){
//			pgridMain.SelectedObject = pObject;
//			if(pObject!=null){
//				addCtlToList();
//				displaySelectedNode(pObject);
//			}
//		}
//		
//		#endregion Public 方法...
//
//		#region 内部处理函数...
//
//		private void displaySelectedNode(object pObject){
//			DataStrtuct da = findNode(pObject);
//			cobControls.SelectedItem   = da;
//		}
//		private DataStrtuct findNode(object pObject){
//			foreach(DataStrtuct obj in  cobControls.Items){
//				if(pObject.Equals(obj.Data )){
//					return obj;
//				}
//			}
//			return null;
//		}
//
//		//增加报表节点 
//		private void addCtlToList(){
//			cobControls.Items.Clear();
//			if(_Report!=null){
//				cobControls.Items.Add(new  DataStrtuct(_Report.Name , _Report));
//
//				addSections( _Report.SectionList);
//			}
//		}
//		//增加Section
//		private void addSections(DIYReport.ReportModel.RptSectionList pSections ){
//			foreach(DIYReport.ReportModel.RptSection section in pSections){
//			 
//				string txt = section.Name + ":" + Enum.GetName(section.SectionType.GetType() , section.SectionType) ;
//				cobControls.Items.Add(new  DataStrtuct(txt , section));
//				addRptObjList( section.RptObjList); 
//			}
//		}
//
//		private void addRptObjList(DIYReport.ReportModel.RptSingleObjList  pControls){
//			foreach(DIYReport.ReportModel.RptSingleObj obj in pControls ){
//				string txt = obj.Name + ":" + Enum.GetName(obj.Type.GetType() ,obj.Type);
//				cobControls.Items.Add(new  DataStrtuct(txt , obj));
//			}
//		}
//		#endregion 内部处理函数...
//
//
//		private void FrmEditRptObjAttribute_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
////			e.Cancel = true;
////			this.Hide();
//		}
//
//		private void cobControls_SelectedIndexChanged(object sender, System.EventArgs e) {
//			DataStrtuct da = cobControls.SelectedItem as DataStrtuct ;
//			pgridMain.SelectedObject = da.Data ;
//		}
//	}
//	class DataStrtuct{
//		private string _Name;
//		private object _Data;
//		public DataStrtuct(string pName,object pData){
//			_Name = pName;
//			_Data = pData;
//		}
//		#region 覆盖基类的方法...
//		public override string ToString() {
//			return _Name == null?"":_Name;
//		}
//
//		#endregion 覆盖基类的方法...
//
//		#region Public 属性...
//		public string Name{
//			get{
//				return _Name;
//			}
//			set{
//				_Name = value;
//			}
//		}
//		public object Data{
//			get{
//				return _Data;
//			}
//			set{
//				_Data = value;
//			}
//		}
//		#endregion Public 属性...
//	}
//}
