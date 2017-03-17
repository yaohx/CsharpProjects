//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-03
// Description	:	XCommands 用户UI 操作的命令集合描述。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;


namespace DIYReport.UserDIY
{
	/// <summary>
	/// 用户UI 操作的基类。
	/// </summary>
	public abstract class CommandExecutorBase {	
		protected DIYReport.Interface.IDesignPanel  DesignerHost;

		protected object[] parameters = null;
		
		public CommandExecutorBase(DIYReport.Interface.IDesignPanel designerHost) {
			DesignerHost = designerHost;

		}
		public void ExecCommand(CommandID cmdID, object[] parameters) {
			this.parameters = parameters;
			ExecCommand(cmdID);
		}
		abstract public void ExecCommand(CommandID cmdID);

		protected ArrayList GetSelectedComponents() {
			return null;
		}
	}
	/// <summary>
	/// 字体和报表设计对象格式化操作相关。
	/// </summary>
	public class FormattingExecutor : CommandExecutorBase{
		public FormattingExecutor(XDesignPanel designerHost) : base(designerHost){

		}

		/// <summary>
		/// 格式化选择的控件
		/// </summary>
		/// <param name="pCurrentCtl"></param>
		/// <param name="pType"></param>
		public override void ExecCommand(CommandID cmdID) {
			 DIYReport.ReportModel.RptSingleObj rptObj = DesignEnviroment.CurrentRptObj as DIYReport.ReportModel.RptSingleObj;
			if(rptObj==null)
				return;
			if(cmdID.Equals(FormattingCommands.BackColor)){
				System.Windows.Forms.ColorDialog backColor = new ColorDialog();
				backColor.Color = rptObj.BackgroundColor;
				if(backColor.ShowDialog()==System.Windows.Forms.DialogResult.OK ){
					 rptObj.BackgroundColor = backColor.Color;
				}
			}
			else if(cmdID.Equals(FormattingCommands.ForeColor)){
				System.Windows.Forms.ColorDialog foreColor = new ColorDialog();
				foreColor.Color = rptObj.ForeColor;
				if(foreColor.ShowDialog()==System.Windows.Forms.DialogResult.OK ){
					rptObj.ForeColor = foreColor.Color;
				}
			}
			else{
				//Debug.Assert(false,"该cmdID" + cmdID.ID.ToString()  +"目前还没有处理。");
			}
		}

	}
	/// <summary>
	/// 格式化执行操作对象。
	/// </summary>
	public class AlignExecutor : CommandExecutorBase{
		public AlignExecutor(XDesignPanel designerHost) : base(designerHost){
		}

		/// <summary>
		/// 格式化选择的控件
		/// </summary>
		/// <param name="pCurrentCtl"></param>
		/// <param name="pType"></param>
		public override void ExecCommand(CommandID cmdID) {
			FormatCtl(cmdID);
		}

		#region 内部函数处理...
		//格式化选择的控件
		private void FormatCtl(CommandID cmdID){
			DesignControl ctl = DesignerHost.SectionList.GetActiveSection().DesignControls.GetMainSelectedCtl();
			if(ctl!=null){
				FormatCtl(new Rectangle(ctl.Location,ctl.Size) ,cmdID);
			}
		}
		//格式化选择的控件
		private void FormatCtl(Rectangle pRect,CommandID cmdID){
			DesignControlList ctlList = DesignerHost.SectionList.GetActiveSection().DesignControls;
			foreach(DesignControl ctl in ctlList){
				if(!ctl.IsSelected)
					continue;

				DIYReport.Interface.IRptSingleObj  dataObj = ctl.DataObj;
				if(cmdID.Equals(StandardCommands.AlignLeft)){
					dataObj.Location = new Point(pRect.Left,ctl.Top)  ;
				}
				else if(cmdID.Equals(StandardCommands.AlignTop)){
					dataObj.Location = new Point(ctl.Left, pRect.Top );
				}
				else if(cmdID.Equals(StandardCommands.AlignRight)){
					dataObj.Location = new Point(pRect.Right - ctl.Width ,ctl.Top);
				}
				else if(cmdID.Equals(StandardCommands.AlignBottom)){
					dataObj.Location = new Point(ctl.Left,pRect.Top + pRect.Height  - ctl.Height);
					//ctl.Top = pRect.Top + pRect.Height  - ctl.Height  ;
				}
				else if(cmdID.Equals(StandardCommands.SizeToControlWidth)){
					dataObj.Size = new Size( pRect.Width,ctl.Height);
					//ctl.Width  = pRect.Width  ;
				}
				else if(cmdID.Equals(StandardCommands.SizeToControlHeight)){
					dataObj.Size = new Size(ctl.Width , pRect.Height);
					//ctl.Height  = pRect.Height  ;
				}
				else {
					Debug.Assert(false,"Command" + cmdID.ID.ToString() + "没有处理。"); 
				}
			}
			DesignerHost.SectionList.GetActiveSection().DesignControls.ShowFocusHandle(true); 
		}
		#endregion 内部函数处理...
	}
	
	/// <summary>
	/// 执行UI command 相关
	/// </summary>
	public class UICommandExecutor : CommandExecutorBase{
		
		#region 构造函数...
		/// <summary>
		/// 构造函数...
		/// </summary>
		/// <param name="designerHost"></param>
		public UICommandExecutor(XDesignPanel designerHost) : base(designerHost){
		}
		#endregion 构造函数...

		#region 自定义事件相关...
		private System.EventHandler _SetObjProperty;
		public event System.EventHandler SetObjProperty{
			add{
				_SetObjProperty +=value;
			}
			remove{
				_SetObjProperty -=value;
			}
		}
		protected virtual void OnSetObjProperty(){
			if(_SetObjProperty!=null){
				_SetObjProperty(this,null);
			}
		}
		#endregion 自定义事件相关...
		
		#region public execute command...
		/// <summary>
		/// 执行命令
		/// </summary>
		/// <param name="cmdID"></param>
		public override void ExecCommand(CommandID cmdID) {
			DesignSection designSe = this.DesignerHost.SectionList.GetActiveSection();

			//this.DesignerHost.Cursor = Cursors.WaitCursor ;
				//switch(cmdID){
			if(cmdID.Equals(StandardCommands.HorizSpaceConcatenate)){//向左边靠齐
				designSe.DesignControls.DockToLeft();
			}
			else if(cmdID.Equals(StandardCommands.VertSpaceConcatenate)){ 
				//向右边靠齐
				designSe.DesignControls.DockToTop() ;
			}
			else if(cmdID.Equals(UICommands.ControlHandle)){//显示方向控制盘
				FrmArrowOperate.ShowArrowForm( this.DesignerHost.SectionList,this.DesignerHost.ParentForm );  
			}
			else if(cmdID.Equals(UICommands.SetObjProperty)){
				//显示属性窗口
				OnSetObjProperty();
			}
			else if(cmdID.Equals(UICommands.ShowProperty)){
				//显示属性窗口
				//DesignEnviroment.ShowPropertyForm(this.DesignerHost.ParentForm,true); 
				//Debug.Assert(false,"ShowProperty ");
			}
			else if(cmdID.Equals(StandardCommands.Delete)){
				//删除控件
				this.DesignerHost.DeleteSelectedControls();
			}
			else if(cmdID.Equals(UICommands.Print)){//打印
				using(DIYReport.Print.SwPrintView print = new DIYReport.Print.SwPrintView( this.DesignerHost.DataObj.DataSource ,this.DesignerHost.DataObj)){
					print.Printer(); 
				}
			}
			else if(cmdID.Equals(UICommands.Preview)){//打印预览
				using(DIYReport.Print.SwPrintView printView = new DIYReport.Print.SwPrintView( this.DesignerHost.DataObj.DataSource,this.DesignerHost.DataObj)){
					printView.ShowPreview();
				}
			}
			else if(cmdID.Equals(UICommands.PageSetup)){//打印页面设置
				DIYReport.Print.RptPageSetting.ShowPageSetupDialog(this.DesignerHost.DataObj);   
			}
			else if(cmdID.Equals(UICommands.SaveFile)){//保存报表
				DIYReport.ReportXmlHelper.Save(this.DesignerHost.DataObj);
				//this.DesignerHost.ReportIO.SaveReport(,this.DesignerHost.DataObj.RptFilePath); 
				DIYReport.UserDIY.DesignEnviroment.DesignHasChanged = false;
			}
			else if(cmdID.Equals(UICommands.OpenFile)){//打开报表 
				DIYReport.ReportModel.RptReport report =   DIYReport.ReportXmlHelper.Open() ; 
				if(report!=null){
					this.DesignerHost.OpenReport( report );
				}
			}
			else if(cmdID.Equals(UICommands.Output)){ //导出
				bool b = DIYReport.ReportXmlHelper.Save(this.DesignerHost.DataObj);
                if(b)
				    MessageBox.Show("打印模板导出成功！","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);    
			}
			else if(cmdID.Equals(UICommands.Import)){ //导入
				DialogResult re = MessageBox.Show("导入打印模板将会清空当前模板,是否继续?","操作提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
				DIYReport.ReportModel.RptReport oldCurrentReport = DIYReport.UserDIY.DesignEnviroment.CurrentReport;
				DIYReport.ReportModel.RptReport report =   DIYReport.ReportXmlHelper.Open() ; 
				if(report!=null){
					report.IDEX = System.Guid.NewGuid();
					
					if(oldCurrentReport!=null){
						if(oldCurrentReport.DataSource!=null){
							report.Tag = oldCurrentReport.Tag;
							report.UserParamList = oldCurrentReport.UserParamList;
							report.DataSource = oldCurrentReport.DataSource;
							report.DesignField = oldCurrentReport.DesignField;
						}
						else{
							report.DataSource = DIYReport.UserDIY.DesignEnviroment.DataSource;
							report.DesignField = DIYReport.UserDIY.DesignEnviroment.DesignField;
						}
						report.SubReportCommand = oldCurrentReport.SubReportCommand;

						foreach(DIYReport.ReportModel.RptReport subReport in oldCurrentReport.SubReports.Values){
							report.SubReports.Add(subReport.Name,subReport);
						}
					}
					this.DesignerHost.OpenReport( report );
				}
			}
			else if(cmdID.Equals(UICommands.NewReport)){//新增报表 
				DIYReport.ReportModel.RptReport report = DIYReport.ReportModel.RptReport.NewReport();
				this.DesignerHost.OpenReport( report );
			}
			else if(cmdID.Equals(StandardCommands.Undo)){//Undo
				this.DesignerHost.UndoMgr.Undo();
			}
			else if(cmdID.Equals(StandardCommands.Redo)){//Redo 
				this.DesignerHost.UndoMgr.Redo();
			}
			else if(cmdID.Equals(StandardCommands.Cut)){
				this.DesignerHost.Cut();
			}
			else if(cmdID.Equals(StandardCommands.Copy)){//Redo 
				this.DesignerHost.Copy();
			}
			else if(cmdID.Equals(StandardCommands.Paste)){//Redo 
				this.DesignerHost.Past();
			}
			else if(cmdID.Equals(UICommands.SortAndGroup)){ //显示分组和排序
				IList fieldsList = this.DesignerHost.DataObj.DesignField; 
				DIYReport.GroupAndSort.frmSortAndGroup frm = new DIYReport.GroupAndSort.frmSortAndGroup(fieldsList); 
				frm.AfterSortAndGroup +=new DIYReport.GroupAndSort.SortAndGroupEventHandler(frm_AfterSortAndGroup);
				frm.ShowDialog(); 
			}

			else{
				Debug.Assert(false,"Command" + cmdID.ID.ToString() + "没有处理。"); 
			} 
			//this.DesignerHost.Cursor = Cursors.Arrow;
		}
		/// <summary>
		/// 绘制控件选择控件类型相关。
		/// </summary>
		/// <param name="cmdID"></param>
		public void ExecRptCtlType(CommandID cmdID){
			DIYReport.ReportModel.RptObjType type = DIYReport.ReportModel.RptObjType.None;
			if(cmdID.Equals(RptDesignCommands.RptNone))
				type = DIYReport.ReportModel.RptObjType.None;
			else if(cmdID.Equals(RptDesignCommands.RptLabel))
				type = DIYReport.ReportModel.RptObjType.Text;
			else if(cmdID.Equals(RptDesignCommands.RptFieldText))
				type = DIYReport.ReportModel.RptObjType.FieldTextBox;
			else if(cmdID.Equals(RptDesignCommands.RptFieldImage))
				type = DIYReport.ReportModel.RptObjType.FieldImage;
			else if(cmdID.Equals(RptDesignCommands.RptPictureBox))
				type = DIYReport.ReportModel.RptObjType.Image;
			else if(cmdID.Equals(RptDesignCommands.RptCheckBox))
				type = DIYReport.ReportModel.RptObjType.CheckBox;
			else if(cmdID.Equals(RptDesignCommands.RptSubReport))
				type = DIYReport.ReportModel.RptObjType.SubReport;
			else if(cmdID.Equals(RptDesignCommands.RptBarCode))
				type = DIYReport.ReportModel.RptObjType.BarCode;
			else if(cmdID.Equals(RptDesignCommands.RptOleObject))
				type = DIYReport.ReportModel.RptObjType.OleObject;
			else if(cmdID.Equals(RptDesignCommands.RptLine))
				type = DIYReport.ReportModel.RptObjType.Line;
			else if(cmdID.Equals(RptDesignCommands.RptFrame ))
				type = DIYReport.ReportModel.RptObjType.Rect;
			else if(cmdID.Equals(RptDesignCommands.RptHViewSpecFieldBox ))
				type = DIYReport.ReportModel.RptObjType.HViewSpecField;
			else if(cmdID.Equals(RptDesignCommands.RptExpressBox ))
				type = DIYReport.ReportModel.RptObjType.Express;
			else if(cmdID.Equals(RptDesignCommands.RptRichTextBox))
				type = DIYReport.ReportModel.RptObjType.RichTextBox;
			else{
				 Debug.Assert(false,"Command" + cmdID.ID.ToString() + "没有处理。"); 
			}
			DesignEnviroment.DrawControlType = type;
			DesignEnviroment.IsCreateControl = type!= DIYReport.ReportModel.RptObjType.None;
		}
		#endregion public execute command...
		
		#region 内部函数处理...
		//格式化选择的控件
		private void FormatCtl(CommandID cmdID){
			DesignControl ctl = DesignerHost.SectionList.GetActiveSection().DesignControls.GetMainSelectedCtl();
			if(ctl!=null){
				FormatCtl(new Rectangle(ctl.Location,ctl.Size) ,cmdID);
			}
		}
		//格式化选择的控件
		private void FormatCtl(Rectangle pRect,CommandID cmdID){
			DesignControlList ctlList = DesignerHost.SectionList.GetActiveSection().DesignControls;
			foreach(DesignControl ctl in ctlList){
				if(!ctl.IsSelected)
					continue;

				DIYReport.Interface.IRptSingleObj  dataObj = ctl.DataObj;
				if(cmdID.Equals(StandardCommands.AlignLeft)){
					dataObj.Location = new Point(pRect.Left,ctl.Top)  ;
				}
				else if(cmdID.Equals(StandardCommands.AlignTop)){
					dataObj.Location = new Point(ctl.Left, pRect.Top );
				}
				else if(cmdID.Equals(StandardCommands.AlignRight)){
					dataObj.Location = new Point(pRect.Right - ctl.Width ,ctl.Top);
				}
				else if(cmdID.Equals(StandardCommands.AlignBottom)){
					dataObj.Location = new Point(ctl.Left,pRect.Top + pRect.Height  - ctl.Height);
					//ctl.Top = pRect.Top + pRect.Height  - ctl.Height  ;
				}
				else if(cmdID.Equals(StandardCommands.SizeToControlWidth)){
					dataObj.Size = new Size( pRect.Width,ctl.Height);
					//ctl.Width  = pRect.Width  ;
				}
				else if(cmdID.Equals(StandardCommands.SizeToControlHeight)){
					dataObj.Size = new Size(ctl.Width , pRect.Height);
					//ctl.Height  = pRect.Height  ;
				}
				else {
					Debug.Assert(false,"Command" + cmdID.ID.ToString() + "没有处理。"); 
					return;
				}
			}
			DesignerHost.SectionList.GetActiveSection().DesignControls.ShowFocusHandle(true); 

		}

		//在分组操作确定之后重新排版报表的设计界面
		private void frm_AfterSortAndGroup(object sender, EventArgs e) {
			this.DesignerHost.SectionList.DataObj.CreateGroupSection();
			this.DesignerHost.SectionList.RefreshDesignLayout();
		}
		#endregion 内部函数处理...
	}
	/// <summary>
	/// 报表设计相关
	/// </summary>
	public class RptDesignCommandsExecutor  : CommandExecutorBase{
		#region 构造函数...
		/// <summary>
		/// 构造函数...
		/// </summary>
		/// <param name="designerHost"></param>
		public RptDesignCommandsExecutor(XDesignPanel designerHost) : base(designerHost){

		}
		#endregion 构造函数...

		public override void ExecCommand(CommandID cmdID) {
			DesignSection designSe = this.DesignerHost.SectionList.GetActiveSection();

			if(cmdID.Equals(RptDesignCommands.InsertTopMarginBand)){
				this.DesignerHost.SectionList.DataObj.AddbySectionType(DIYReport.SectionType.TopMargin);   
			}
			else if(cmdID.Equals(RptDesignCommands.InsertReportHeaderBand)){
				this.DesignerHost.SectionList.DataObj.AddbySectionType(DIYReport.SectionType.ReportTitle);
			}
			else if(cmdID.Equals(RptDesignCommands.InsertPageHeaderBand)){
				this.DesignerHost.SectionList.DataObj.AddbySectionType(DIYReport.SectionType.PageHead);
			}
			else if(cmdID.Equals(RptDesignCommands.InsertGroupHeaderBand)){
				this.DesignerHost.SectionList.DataObj.AddbySectionType(DIYReport.SectionType.GroupHead);
			}
			else if(cmdID.Equals(RptDesignCommands.InsertDetailBand)){
				this.DesignerHost.SectionList.DataObj.AddbySectionType(DIYReport.SectionType.Detail);
			}
			else if(cmdID.Equals(RptDesignCommands.InsertGroupFooterBand)){
				this.DesignerHost.SectionList.DataObj.AddbySectionType(DIYReport.SectionType.GroupFooter);
			}
			else if(cmdID.Equals(RptDesignCommands.InsertPageFooterBand)){
				this.DesignerHost.SectionList.DataObj.AddbySectionType(DIYReport.SectionType.PageFooter);
			}
			else if(cmdID.Equals(RptDesignCommands.InsertReportFooterBand)){
				this.DesignerHost.SectionList.DataObj.AddbySectionType(DIYReport.SectionType.ReportBottom);
			}
			else if(cmdID.Equals(RptDesignCommands.InsertBottomMarginBand)){
				this.DesignerHost.SectionList.DataObj.AddbySectionType(DIYReport.SectionType.BottomMargin);
			}
			else if(cmdID.Equals(StandardCommands.Delete)){
				this.DesignerHost.DeleteSelectedControls();
			}
			else if(cmdID.Equals(StandardCommands.Cut)){
				this.DesignerHost.Cut();
			}
			else if(cmdID.Equals(StandardCommands.Copy)){//Redo 
				this.DesignerHost.Copy();
			}
			else if(cmdID.Equals(StandardCommands.Paste)){//Redo 
				this.DesignerHost.Past();
			}
			else{
				Debug.Assert(false,"该命令" + cmdID.ToString() + "目前还没有进行处理。");
			}
		}
	}

}
