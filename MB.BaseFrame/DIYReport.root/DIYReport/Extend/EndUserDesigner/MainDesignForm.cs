//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-07.
// Description	:	MainDesignForm 报表DIY 设计主窗口。
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Drawing;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

using DIYReport.UserDIY; 
namespace DIYReport.Extend.EndUserDesigner
{
	/// <summary>
	/// MainDesignForm 报表DIY 设计主窗口。
	/// </summary>
	public class MainDesignForm : System.Windows.Forms.Form  //DevExpress.XtraBars.Ribbon.RibbonForm
	{
		#region 内部自动生成代码...
		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.Docking.DockManager dockManager1;
		private DevExpress.XtraBars.BarSubItem itemFile;
		private DevExpress.XtraBars.BarSubItem itemEdit;
		private DevExpress.XtraBars.BarButtonItem itemFile_Open;
		private DevExpress.XtraBars.BarButtonItem itemEdit_UnDo;
		private DevExpress.XtraBars.BarButtonItem itemEdit_ReDo;
		private DevExpress.XtraBars.BarButtonItem itemEdit_Cut;
		private DevExpress.XtraBars.BarButtonItem itemEdit_Copy;
		private DevExpress.XtraBars.BarButtonItem itemEdit_Past;
		private DevExpress.XtraBars.BarButtonItem itemEdit_Delete;
		private DevExpress.XtraBars.BarButtonItem itemEdit_SelectAll;
		private DevExpress.XtraBars.BarButtonItem itemFile_Save;
		private DevExpress.XtraBars.BarButtonItem itemFile_PageSetting;
		private DevExpress.XtraBars.BarButtonItem itemFile_ReportPreview;
		private DevExpress.XtraBars.BarButtonItem itemFile_Print;
		private DevExpress.XtraBars.BarButtonItem itemFile_Quit;
		private DevExpress.XtraBars.BarSubItem itemFormat;
		private DevExpress.XtraBars.BarSubItem itemFormatAlign;
		private DevExpress.XtraBars.BarSubItem itemFormatSize;
		private DevExpress.XtraBars.BarSubItem itemFormatSpacing;
		private DevExpress.XtraBars.BarButtonItem itemFormatControl;
		private DevExpress.XtraBars.BarButtonItem itemFormatAlignLeft;
		private DevExpress.XtraBars.BarButtonItem itemFormatAlignRight;
		private DevExpress.XtraBars.BarButtonItem itemFormatAlignTop;
		private DevExpress.XtraBars.BarButtonItem itemFormatAlignBottom;
		private DevExpress.XtraBars.BarButtonItem itemFormatSizeWidth;
		private DevExpress.XtraBars.BarButtonItem itemFormatSpacingH;
		private DevExpress.XtraBars.BarButtonItem itemFormatSpacingV;
		private DevExpress.XtraBars.BarButtonItem itemFormat_BackColor;
		private DevExpress.XtraBars.BarButtonItem itemFormatForeColor;
		private DevExpress.XtraBars.BarSubItem itemFormatFont;
		private DevExpress.XtraBars.BarButtonItem itemFormatFontBold;
		private DevExpress.XtraBars.BarButtonItem itemFormatFontItalic;
		private DevExpress.XtraBars.BarButtonItem itemFormatUnderline;
		private DevExpress.XtraBars.BarSubItem barSubItem1;
		private DevExpress.XtraBars.BarSubItem barSubItem2;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
		private DevExpress.XtraBars.Bar barMainMenu;
		private DevExpress.XtraBars.Bar barFile;
		private DevExpress.XtraBars.Bar barFont;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
		private System.Windows.Forms.ImageList imageMainList;
		private DevExpress.XtraBars.BarButtonItem itemFile_SortGroup;
		private DevExpress.XtraBars.BarButtonItem itemFormatSizeHeight;
		private DevExpress.XtraBars.BarButtonItem itemFile_New;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
		private DIYReport.UserDIY.XDesignPanel xDesignPanel1;
		private System.Windows.Forms.Panel paneDesign;
		private DevExpress.XtraNavBar.NavBarControl navBarControl1;
		private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
		private DevExpress.XtraNavBar.NavBarItem navItemPoint;
		private DevExpress.XtraNavBar.NavBarItem navItemLable;
		private DevExpress.XtraNavBar.NavBarItem navItemFieldBox;
		private DevExpress.XtraNavBar.NavBarItem navItemFieldImage;
		private DevExpress.XtraNavBar.NavBarItem navItemCheckBox;
		private DevExpress.XtraNavBar.NavBarItem navItemSubReport;
		private DevExpress.XtraNavBar.NavBarItem navItemBarCode;
		private DevExpress.XtraNavBar.NavBarItem navItemPictureBox;
		private DevExpress.XtraNavBar.NavBarItem navitemOleObject;
		private DevExpress.XtraNavBar.NavBarItem navItemLine;
		private DevExpress.XtraNavBar.NavBarItem navItemFrame;
		private DIYReport.UserDIY.EditRptObjAttribute editRptObjAttribute1;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel3;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel3_Container;
		private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
		private DevExpress.XtraBars.BarButtonItem itemFormatFontLeft;
		private DevExpress.XtraBars.BarButtonItem itemFormatFontRight;
		private DevExpress.XtraBars.BarButtonItem itemFormatFontCenter;
		private DevExpress.XtraBars.Docking.DockPanel dockPanObjProperty;
		private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rItemCobFontStyle;
		private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rItemCobFontSize;
		private DevExpress.XtraBars.BarButtonItem itemFile_SaveAs;
		private DevExpress.XtraBars.BarSubItem itemFormatOrder;
		private DevExpress.XtraBars.BarButtonItem itemFormatOrderFront;
		private DevExpress.XtraBars.BarButtonItem itemFormatOrderGround;
		private DevExpress.XtraBars.BarButtonItem itemEditProperty;
		private DevExpress.XtraNavBar.NavBarItem navItemExpressBox;
		private DevExpress.XtraNavBar.NavBarItem navItemHViewFieldBox;
		private System.Windows.Forms.StatusBar statusBar1;
		private DevExpress.XtraBars.BarEditItem barEditItemFontStyle;
		private DevExpress.XtraBars.BarEditItem barEditItemFontSize;
		private DevExpress.XtraNavBar.NavBarItem navItemRichTextBox;
		private DevExpress.XtraBars.BarSubItem barSubItem3;
		private DevExpress.XtraBars.BarButtonItem itemTools_Output;
        private DevExpress.XtraBars.BarButtonItem itemTools_Import;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDesignForm));
            DIYReport.ReportDataIO reportDataIO1 = new DIYReport.ReportDataIO();
            DIYReport.UndoManager.UndoMgr undoMgr1 = new DIYReport.UndoManager.UndoMgr();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barMainMenu = new DevExpress.XtraBars.Bar();
            this.itemFile = new DevExpress.XtraBars.BarSubItem();
            this.itemFile_New = new DevExpress.XtraBars.BarButtonItem();
            this.itemFile_Open = new DevExpress.XtraBars.BarButtonItem();
            this.itemFile_Save = new DevExpress.XtraBars.BarButtonItem();
            this.itemFile_SaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.itemFile_PageSetting = new DevExpress.XtraBars.BarButtonItem();
            this.itemFile_ReportPreview = new DevExpress.XtraBars.BarButtonItem();
            this.itemFile_Print = new DevExpress.XtraBars.BarButtonItem();
            this.itemFile_SortGroup = new DevExpress.XtraBars.BarButtonItem();
            this.itemFile_Quit = new DevExpress.XtraBars.BarButtonItem();
            this.itemEdit = new DevExpress.XtraBars.BarSubItem();
            this.itemEdit_UnDo = new DevExpress.XtraBars.BarButtonItem();
            this.itemEdit_ReDo = new DevExpress.XtraBars.BarButtonItem();
            this.itemEdit_Cut = new DevExpress.XtraBars.BarButtonItem();
            this.itemEdit_Copy = new DevExpress.XtraBars.BarButtonItem();
            this.itemEdit_Past = new DevExpress.XtraBars.BarButtonItem();
            this.itemEdit_Delete = new DevExpress.XtraBars.BarButtonItem();
            this.itemEdit_SelectAll = new DevExpress.XtraBars.BarButtonItem();
            this.itemEditProperty = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormat = new DevExpress.XtraBars.BarSubItem();
            this.itemFormat_BackColor = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatForeColor = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatFont = new DevExpress.XtraBars.BarSubItem();
            this.itemFormatFontBold = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatFontItalic = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatUnderline = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatFontLeft = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatFontRight = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatFontCenter = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatAlign = new DevExpress.XtraBars.BarSubItem();
            this.itemFormatAlignLeft = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatAlignRight = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatAlignTop = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatAlignBottom = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatSize = new DevExpress.XtraBars.BarSubItem();
            this.itemFormatSizeWidth = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatSizeHeight = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatSpacing = new DevExpress.XtraBars.BarSubItem();
            this.itemFormatSpacingH = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatSpacingV = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatControl = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatOrder = new DevExpress.XtraBars.BarSubItem();
            this.itemFormatOrderFront = new DevExpress.XtraBars.BarButtonItem();
            this.itemFormatOrderGround = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
            this.itemTools_Output = new DevExpress.XtraBars.BarButtonItem();
            this.itemTools_Import = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.barFile = new DevExpress.XtraBars.Bar();
            this.barFont = new DevExpress.XtraBars.Bar();
            this.barEditItemFontStyle = new DevExpress.XtraBars.BarEditItem();
            this.rItemCobFontStyle = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barEditItemFontSize = new DevExpress.XtraBars.BarEditItem();
            this.rItemCobFontSize = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanObjProperty = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.editRptObjAttribute1 = new DIYReport.UserDIY.EditRptObjAttribute();
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navItemPoint = new DevExpress.XtraNavBar.NavBarItem();
            this.navItemLable = new DevExpress.XtraNavBar.NavBarItem();
            this.navItemFieldBox = new DevExpress.XtraNavBar.NavBarItem();
            this.navItemExpressBox = new DevExpress.XtraNavBar.NavBarItem();
            this.navItemFieldImage = new DevExpress.XtraNavBar.NavBarItem();
            this.navItemRichTextBox = new DevExpress.XtraNavBar.NavBarItem();
            this.navItemHViewFieldBox = new DevExpress.XtraNavBar.NavBarItem();
            this.navItemCheckBox = new DevExpress.XtraNavBar.NavBarItem();
            this.navItemSubReport = new DevExpress.XtraNavBar.NavBarItem();
            this.navItemBarCode = new DevExpress.XtraNavBar.NavBarItem();
            this.navItemPictureBox = new DevExpress.XtraNavBar.NavBarItem();
            this.navitemOleObject = new DevExpress.XtraNavBar.NavBarItem();
            this.navItemLine = new DevExpress.XtraNavBar.NavBarItem();
            this.navItemFrame = new DevExpress.XtraNavBar.NavBarItem();
            this.imageMainList = new System.Windows.Forms.ImageList(this.components);
            this.dockPanel3 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.paneDesign = new System.Windows.Forms.Panel();
            this.xDesignPanel1 = new DIYReport.UserDIY.XDesignPanel();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rItemCobFontStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rItemCobFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanObjProperty.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            this.panelContainer1.SuspendLayout();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.dockPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barMainMenu,
            this.barFile,
            this.barFont});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageMainList;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemFile,
            this.itemEdit,
            this.itemFormat,
            this.itemFile_Open,
            this.itemFile_Save,
            this.itemFile_PageSetting,
            this.itemFile_ReportPreview,
            this.itemFile_Print,
            this.itemFile_Quit,
            this.itemEdit_UnDo,
            this.itemEdit_ReDo,
            this.itemEdit_Cut,
            this.itemEdit_Copy,
            this.itemEdit_Past,
            this.itemEdit_Delete,
            this.itemEdit_SelectAll,
            this.itemFormatAlign,
            this.itemFormatSize,
            this.itemFormatSpacing,
            this.itemFormatControl,
            this.itemFormatAlignLeft,
            this.itemFormatAlignRight,
            this.itemFormatAlignTop,
            this.itemFormatAlignBottom,
            this.itemFormatSizeWidth,
            this.itemFormatSizeHeight,
            this.itemFormatSpacingH,
            this.itemFormatSpacingV,
            this.itemFormat_BackColor,
            this.itemFormatForeColor,
            this.itemFormatFont,
            this.itemFormatFontBold,
            this.itemFormatFontItalic,
            this.itemFormatUnderline,
            this.barSubItem1,
            this.barSubItem2,
            this.barEditItemFontStyle,
            this.barEditItemFontSize,
            this.itemFile_SortGroup,
            this.itemFile_New,
            this.itemFormatFontLeft,
            this.itemFormatFontRight,
            this.itemFormatFontCenter,
            this.itemFile_SaveAs,
            this.itemFormatOrder,
            this.itemFormatOrderFront,
            this.itemFormatOrderGround,
            this.itemEditProperty,
            this.barSubItem3,
            this.itemTools_Output,
            this.itemTools_Import});
            this.barManager1.MainMenu = this.barMainMenu;
            this.barManager1.MaxItemId = 71;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rItemCobFontStyle,
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemTextEdit3,
            this.rItemCobFontSize,
            this.repositoryItemButtonEdit1});
            // 
            // barMainMenu
            // 
            this.barMainMenu.BarName = "barMainMenu";
            this.barMainMenu.DockCol = 0;
            this.barMainMenu.DockRow = 0;
            this.barMainMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barMainMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormat),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1)});
            this.barMainMenu.OptionsBar.MultiLine = true;
            this.barMainMenu.OptionsBar.UseWholeRow = true;
            this.barMainMenu.Text = "barMainMenu";
            this.barMainMenu.Visible = false;
            // 
            // itemFile
            // 
            this.itemFile.Caption = "文件(F)";
            this.itemFile.Id = 0;
            this.itemFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_New),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_Open),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_Save),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_SaveAs),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_PageSetting, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_ReportPreview),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_Print),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_SortGroup, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_Quit, true)});
            this.itemFile.Name = "itemFile";
            // 
            // itemFile_New
            // 
            this.itemFile_New.Caption = "新建(&N)";
            this.itemFile_New.Hint = "新建报表";
            this.itemFile_New.Id = 46;
            this.itemFile_New.ImageIndex = 0;
            this.itemFile_New.Name = "itemFile_New";
            // 
            // itemFile_Open
            // 
            this.itemFile_Open.Caption = "打开(&O)";
            this.itemFile_Open.Hint = "打开报表";
            this.itemFile_Open.Id = 3;
            this.itemFile_Open.ImageIndex = 1;
            this.itemFile_Open.Name = "itemFile_Open";
            // 
            // itemFile_Save
            // 
            this.itemFile_Save.Caption = "保存(&S)";
            this.itemFile_Save.Hint = "保存";
            this.itemFile_Save.Id = 4;
            this.itemFile_Save.ImageIndex = 2;
            this.itemFile_Save.Name = "itemFile_Save";
            // 
            // itemFile_SaveAs
            // 
            this.itemFile_SaveAs.Caption = "另存为(&A)";
            this.itemFile_SaveAs.Id = 51;
            this.itemFile_SaveAs.ImageIndex = 47;
            this.itemFile_SaveAs.Name = "itemFile_SaveAs";
            // 
            // itemFile_PageSetting
            // 
            this.itemFile_PageSetting.Caption = "页面设置(&U)";
            this.itemFile_PageSetting.Id = 6;
            this.itemFile_PageSetting.ImageIndex = 3;
            this.itemFile_PageSetting.Name = "itemFile_PageSetting";
            // 
            // itemFile_ReportPreview
            // 
            this.itemFile_ReportPreview.Caption = "打印预览(&V)";
            this.itemFile_ReportPreview.Id = 7;
            this.itemFile_ReportPreview.ImageIndex = 4;
            this.itemFile_ReportPreview.Name = "itemFile_ReportPreview";
            // 
            // itemFile_Print
            // 
            this.itemFile_Print.Caption = "打印(&P)";
            this.itemFile_Print.Id = 8;
            this.itemFile_Print.ImageIndex = 5;
            this.itemFile_Print.Name = "itemFile_Print";
            // 
            // itemFile_SortGroup
            // 
            this.itemFile_SortGroup.Caption = "分组排序";
            this.itemFile_SortGroup.Id = 45;
            this.itemFile_SortGroup.ImageIndex = 7;
            this.itemFile_SortGroup.Name = "itemFile_SortGroup";
            // 
            // itemFile_Quit
            // 
            this.itemFile_Quit.Caption = "退出(&X)";
            this.itemFile_Quit.Id = 9;
            this.itemFile_Quit.Name = "itemFile_Quit";
            // 
            // itemEdit
            // 
            this.itemEdit.Caption = "编辑(&E)";
            this.itemEdit.Id = 1;
            this.itemEdit.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemEdit_UnDo),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemEdit_ReDo),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemEdit_Cut, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemEdit_Copy),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemEdit_Past),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemEdit_Delete, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemEdit_SelectAll, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemEditProperty, true)});
            this.itemEdit.Name = "itemEdit";
            // 
            // itemEdit_UnDo
            // 
            this.itemEdit_UnDo.Caption = "撤消(&V)";
            this.itemEdit_UnDo.Id = 10;
            this.itemEdit_UnDo.ImageIndex = 8;
            this.itemEdit_UnDo.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z));
            this.itemEdit_UnDo.Name = "itemEdit_UnDo";
            // 
            // itemEdit_ReDo
            // 
            this.itemEdit_ReDo.Caption = "重复(&R)";
            this.itemEdit_ReDo.Id = 11;
            this.itemEdit_ReDo.ImageIndex = 9;
            this.itemEdit_ReDo.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y));
            this.itemEdit_ReDo.Name = "itemEdit_ReDo";
            // 
            // itemEdit_Cut
            // 
            this.itemEdit_Cut.Caption = "剪切(&T)";
            this.itemEdit_Cut.Id = 12;
            this.itemEdit_Cut.ImageIndex = 10;
            this.itemEdit_Cut.Name = "itemEdit_Cut";
            // 
            // itemEdit_Copy
            // 
            this.itemEdit_Copy.Caption = "复制(&C)";
            this.itemEdit_Copy.Id = 13;
            this.itemEdit_Copy.ImageIndex = 11;
            this.itemEdit_Copy.Name = "itemEdit_Copy";
            // 
            // itemEdit_Past
            // 
            this.itemEdit_Past.Caption = "粘贴(&P)";
            this.itemEdit_Past.Id = 14;
            this.itemEdit_Past.ImageIndex = 12;
            this.itemEdit_Past.Name = "itemEdit_Past";
            // 
            // itemEdit_Delete
            // 
            this.itemEdit_Delete.Caption = "删除(&D)";
            this.itemEdit_Delete.Id = 15;
            this.itemEdit_Delete.ImageIndex = 13;
            this.itemEdit_Delete.Name = "itemEdit_Delete";
            // 
            // itemEdit_SelectAll
            // 
            this.itemEdit_SelectAll.Caption = "全选(&A)";
            this.itemEdit_SelectAll.Id = 16;
            this.itemEdit_SelectAll.Name = "itemEdit_SelectAll";
            // 
            // itemEditProperty
            // 
            this.itemEditProperty.Caption = "属性(&P)";
            this.itemEditProperty.Id = 57;
            this.itemEditProperty.Name = "itemEditProperty";
            // 
            // itemFormat
            // 
            this.itemFormat.Caption = "格式(&O)";
            this.itemFormat.Id = 2;
            this.itemFormat.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormat_BackColor, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatForeColor),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatFont, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatAlign, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatSize),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatSpacing),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatControl),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatOrder)});
            this.itemFormat.Name = "itemFormat";
            // 
            // itemFormat_BackColor
            // 
            this.itemFormat_BackColor.Caption = "背景颜色";
            this.itemFormat_BackColor.Id = 29;
            this.itemFormat_BackColor.ImageIndex = 16;
            this.itemFormat_BackColor.Name = "itemFormat_BackColor";
            // 
            // itemFormatForeColor
            // 
            this.itemFormatForeColor.Caption = "字体颜色";
            this.itemFormatForeColor.Id = 30;
            this.itemFormatForeColor.ImageIndex = 17;
            this.itemFormatForeColor.Name = "itemFormatForeColor";
            // 
            // itemFormatFont
            // 
            this.itemFormatFont.Caption = "字体";
            this.itemFormatFont.Id = 31;
            this.itemFormatFont.ImageIndex = 18;
            this.itemFormatFont.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatFontBold),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatFontItalic),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatUnderline),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatFontLeft, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatFontRight),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatFontCenter)});
            this.itemFormatFont.Name = "itemFormatFont";
            // 
            // itemFormatFontBold
            // 
            this.itemFormatFontBold.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.itemFormatFontBold.Caption = "加粗";
            this.itemFormatFontBold.Description = "加粗";
            this.itemFormatFontBold.Id = 32;
            this.itemFormatFontBold.ImageIndex = 19;
            this.itemFormatFontBold.Name = "itemFormatFontBold";
            // 
            // itemFormatFontItalic
            // 
            this.itemFormatFontItalic.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.itemFormatFontItalic.Caption = "倾斜";
            this.itemFormatFontItalic.Id = 33;
            this.itemFormatFontItalic.ImageIndex = 20;
            this.itemFormatFontItalic.Name = "itemFormatFontItalic";
            // 
            // itemFormatUnderline
            // 
            this.itemFormatUnderline.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.itemFormatUnderline.Caption = "加下划线";
            this.itemFormatUnderline.Id = 34;
            this.itemFormatUnderline.ImageIndex = 21;
            this.itemFormatUnderline.Name = "itemFormatUnderline";
            // 
            // itemFormatFontLeft
            // 
            this.itemFormatFontLeft.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.itemFormatFontLeft.Caption = "左对齐";
            this.itemFormatFontLeft.Id = 47;
            this.itemFormatFontLeft.ImageIndex = 22;
            this.itemFormatFontLeft.Name = "itemFormatFontLeft";
            // 
            // itemFormatFontRight
            // 
            this.itemFormatFontRight.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.itemFormatFontRight.Caption = "右对齐";
            this.itemFormatFontRight.Id = 48;
            this.itemFormatFontRight.ImageIndex = 24;
            this.itemFormatFontRight.Name = "itemFormatFontRight";
            // 
            // itemFormatFontCenter
            // 
            this.itemFormatFontCenter.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.itemFormatFontCenter.Caption = "中间对齐";
            this.itemFormatFontCenter.Id = 49;
            this.itemFormatFontCenter.ImageIndex = 23;
            this.itemFormatFontCenter.Name = "itemFormatFontCenter";
            // 
            // itemFormatAlign
            // 
            this.itemFormatAlign.Caption = "对齐";
            this.itemFormatAlign.Id = 17;
            this.itemFormatAlign.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatAlignLeft),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatAlignRight),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatAlignTop),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatAlignBottom)});
            this.itemFormatAlign.Name = "itemFormatAlign";
            // 
            // itemFormatAlignLeft
            // 
            this.itemFormatAlignLeft.Caption = "左对齐";
            this.itemFormatAlignLeft.Id = 21;
            this.itemFormatAlignLeft.ImageIndex = 25;
            this.itemFormatAlignLeft.Name = "itemFormatAlignLeft";
            // 
            // itemFormatAlignRight
            // 
            this.itemFormatAlignRight.Caption = "右对齐";
            this.itemFormatAlignRight.Id = 22;
            this.itemFormatAlignRight.ImageIndex = 26;
            this.itemFormatAlignRight.Name = "itemFormatAlignRight";
            // 
            // itemFormatAlignTop
            // 
            this.itemFormatAlignTop.Caption = "顶端对齐";
            this.itemFormatAlignTop.Id = 23;
            this.itemFormatAlignTop.ImageIndex = 27;
            this.itemFormatAlignTop.Name = "itemFormatAlignTop";
            // 
            // itemFormatAlignBottom
            // 
            this.itemFormatAlignBottom.Caption = "底端对齐";
            this.itemFormatAlignBottom.Id = 24;
            this.itemFormatAlignBottom.ImageIndex = 28;
            this.itemFormatAlignBottom.Name = "itemFormatAlignBottom";
            // 
            // itemFormatSize
            // 
            this.itemFormatSize.Caption = "大小相同(&M)";
            this.itemFormatSize.Id = 18;
            this.itemFormatSize.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatSizeWidth),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatSizeHeight)});
            this.itemFormatSize.Name = "itemFormatSize";
            // 
            // itemFormatSizeWidth
            // 
            this.itemFormatSizeWidth.Caption = "宽度";
            this.itemFormatSizeWidth.Id = 25;
            this.itemFormatSizeWidth.ImageIndex = 30;
            this.itemFormatSizeWidth.Name = "itemFormatSizeWidth";
            // 
            // itemFormatSizeHeight
            // 
            this.itemFormatSizeHeight.Caption = "高度";
            this.itemFormatSizeHeight.Id = 26;
            this.itemFormatSizeHeight.ImageIndex = 29;
            this.itemFormatSizeHeight.Name = "itemFormatSizeHeight";
            // 
            // itemFormatSpacing
            // 
            this.itemFormatSpacing.Caption = "紧靠";
            this.itemFormatSpacing.Id = 19;
            this.itemFormatSpacing.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatSpacingH),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatSpacingV)});
            this.itemFormatSpacing.Name = "itemFormatSpacing";
            // 
            // itemFormatSpacingH
            // 
            this.itemFormatSpacingH.Caption = "左右";
            this.itemFormatSpacingH.Id = 27;
            this.itemFormatSpacingH.ImageIndex = 32;
            this.itemFormatSpacingH.Name = "itemFormatSpacingH";
            // 
            // itemFormatSpacingV
            // 
            this.itemFormatSpacingV.Caption = "上下";
            this.itemFormatSpacingV.Id = 28;
            this.itemFormatSpacingV.ImageIndex = 31;
            this.itemFormatSpacingV.Name = "itemFormatSpacingV";
            // 
            // itemFormatControl
            // 
            this.itemFormatControl.Caption = "微调器";
            this.itemFormatControl.Id = 20;
            this.itemFormatControl.ImageIndex = 33;
            this.itemFormatControl.Name = "itemFormatControl";
            // 
            // itemFormatOrder
            // 
            this.itemFormatOrder.Caption = "顺序";
            this.itemFormatOrder.Id = 54;
            this.itemFormatOrder.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatOrderFront),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatOrderGround)});
            this.itemFormatOrder.Name = "itemFormatOrder";
            // 
            // itemFormatOrderFront
            // 
            this.itemFormatOrderFront.Caption = "置于顶层(&B)";
            this.itemFormatOrderFront.Id = 55;
            this.itemFormatOrderFront.ImageIndex = 14;
            this.itemFormatOrderFront.Name = "itemFormatOrderFront";
            // 
            // itemFormatOrderGround
            // 
            this.itemFormatOrderGround.Caption = "置于底层(&S)";
            this.itemFormatOrderGround.Id = 56;
            this.itemFormatOrderGround.ImageIndex = 15;
            this.itemFormatOrderGround.Name = "itemFormatOrderGround";
            // 
            // barSubItem3
            // 
            this.barSubItem3.Caption = "工具(&T)";
            this.barSubItem3.Id = 68;
            this.barSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTools_Output),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTools_Import)});
            this.barSubItem3.Name = "barSubItem3";
            // 
            // itemTools_Output
            // 
            this.itemTools_Output.Caption = "导出(&O)";
            this.itemTools_Output.Id = 69;
            this.itemTools_Output.Name = "itemTools_Output";
            // 
            // itemTools_Import
            // 
            this.itemTools_Import.Caption = "导入(&I)";
            this.itemTools_Import.Id = 70;
            this.itemTools_Import.Name = "itemTools_Import";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "帮助(&H)";
            this.barSubItem1.Id = 35;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem2)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "关于(&A)";
            this.barSubItem2.Id = 36;
            this.barSubItem2.Name = "barSubItem2";
            // 
            // barFile
            // 
            this.barFile.BarName = "barFile";
            this.barFile.DockCol = 0;
            this.barFile.DockRow = 1;
            this.barFile.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barFile.Text = "barFile";
            // 
            // barFont
            // 
            this.barFont.BarName = "barFont";
            this.barFont.DockCol = 0;
            this.barFont.DockRow = 2;
            this.barFont.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barFont.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.barEditItemFontStyle, "", false, true, true, 100),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.barEditItemFontSize, "", false, true, true, 50)});
            this.barFont.Text = "barFont";
            // 
            // barEditItemFontStyle
            // 
            this.barEditItemFontStyle.Caption = "Test";
            this.barEditItemFontStyle.Edit = this.rItemCobFontStyle;
            this.barEditItemFontStyle.Id = 42;
            this.barEditItemFontStyle.Name = "barEditItemFontStyle";
            this.barEditItemFontStyle.Width = 120;
            // 
            // rItemCobFontStyle
            // 
            this.rItemCobFontStyle.AutoHeight = false;
            this.rItemCobFontStyle.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rItemCobFontStyle.Name = "rItemCobFontStyle";
            // 
            // barEditItemFontSize
            // 
            this.barEditItemFontSize.Edit = this.rItemCobFontSize;
            this.barEditItemFontSize.Id = 43;
            this.barEditItemFontSize.ImageIndex = 18;
            this.barEditItemFontSize.Name = "barEditItemFontSize";
            this.barEditItemFontSize.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // rItemCobFontSize
            // 
            this.rItemCobFontSize.AutoHeight = false;
            this.rItemCobFontSize.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rItemCobFontSize.Name = "rItemCobFontSize";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(776, 78);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 566);
            this.barDockControlBottom.Size = new System.Drawing.Size(776, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 78);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 488);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(776, 78);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 488);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanObjProperty,
            this.panelContainer1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar"});
            // 
            // dockPanObjProperty
            // 
            this.dockPanObjProperty.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.dockPanObjProperty.Appearance.Options.UseBackColor = true;
            this.dockPanObjProperty.Controls.Add(this.dockPanel2_Container);
            this.dockPanObjProperty.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanObjProperty.FloatVertical = true;
            this.dockPanObjProperty.ID = new System.Guid("5d231b74-3816-4662-91c7-84b75c53e6b7");
            this.dockPanObjProperty.Location = new System.Drawing.Point(576, 78);
            this.dockPanObjProperty.Name = "dockPanObjProperty";
            this.dockPanObjProperty.OriginalSize = new System.Drawing.Size(200, 234);
            this.dockPanObjProperty.Size = new System.Drawing.Size(200, 467);
            this.dockPanObjProperty.Text = "对象属性";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.editRptObjAttribute1);
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 29);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(194, 435);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // editRptObjAttribute1
            // 
            this.editRptObjAttribute1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editRptObjAttribute1.Location = new System.Drawing.Point(0, 0);
            this.editRptObjAttribute1.Name = "editRptObjAttribute1";
            this.editRptObjAttribute1.Size = new System.Drawing.Size(194, 435);
            this.editRptObjAttribute1.TabIndex = 0;
            // 
            // panelContainer1
            // 
            this.panelContainer1.ActiveChild = this.dockPanel1;
            this.panelContainer1.Controls.Add(this.dockPanel3);
            this.panelContainer1.Controls.Add(this.dockPanel1);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.panelContainer1.ID = new System.Guid("1af171a4-fca3-4edd-98da-82a7d3953207");
            this.panelContainer1.Location = new System.Drawing.Point(0, 78);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(200, 200);
            this.panelContainer1.Size = new System.Drawing.Size(200, 467);
            this.panelContainer1.Tabbed = true;
            this.panelContainer1.Text = "panelContainer1";
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel1.ID = new System.Guid("8f791a3e-1a13-4c92-b8b8-f795d37a74ec");
            this.dockPanel1.Location = new System.Drawing.Point(3, 29);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(194, 416);
            this.dockPanel1.Size = new System.Drawing.Size(194, 413);
            this.dockPanel1.Text = "工具箱";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.navBarControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(194, 413);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.AllowSelectedLink = true;
            this.navBarControl1.Appearance.ItemPressed.BackColor = System.Drawing.SystemColors.ControlLight;
            this.navBarControl1.Appearance.ItemPressed.Options.UseBackColor = true;
            this.navBarControl1.ContentButtonHint = null;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.navBarControl1.HotTrackedItemCursor = System.Windows.Forms.Cursors.Arrow;
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navItemPoint,
            this.navItemLable,
            this.navItemFieldBox,
            this.navItemFieldImage,
            this.navItemPictureBox,
            this.navItemLine,
            this.navItemFrame,
            this.navItemSubReport,
            this.navItemBarCode,
            this.navItemCheckBox,
            this.navitemOleObject,
            this.navItemExpressBox,
            this.navItemHViewFieldBox,
            this.navItemRichTextBox});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 112;
            this.navBarControl1.Size = new System.Drawing.Size(194, 413);
            this.navBarControl1.SmallImages = this.imageMainList;
            this.navBarControl1.TabIndex = 7;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.View = new DevExpress.XtraNavBar.ViewInfo.VSToolBoxViewInfoRegistrator();
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "报表对象";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.SmallIconsText;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navItemPoint),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navItemLable),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navItemFieldBox),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navItemExpressBox),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navItemFieldImage),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navItemRichTextBox),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navItemHViewFieldBox),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navItemCheckBox),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navItemSubReport),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navItemBarCode),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navItemPictureBox),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navitemOleObject),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navItemLine),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navItemFrame)});
            this.navBarGroup1.Name = "navBarGroup1";
            this.navBarGroup1.SelectedLinkIndex = 0;
            // 
            // navItemPoint
            // 
            this.navItemPoint.Caption = "光标";
            this.navItemPoint.Name = "navItemPoint";
            this.navItemPoint.SmallImageIndex = 34;
            // 
            // navItemLable
            // 
            this.navItemLable.Caption = "标签";
            this.navItemLable.Name = "navItemLable";
            this.navItemLable.SmallImageIndex = 35;
            // 
            // navItemFieldBox
            // 
            this.navItemFieldBox.Caption = "文本字段";
            this.navItemFieldBox.Name = "navItemFieldBox";
            this.navItemFieldBox.SmallImageIndex = 36;
            // 
            // navItemExpressBox
            // 
            this.navItemExpressBox.Caption = "表达式";
            this.navItemExpressBox.Name = "navItemExpressBox";
            this.navItemExpressBox.SmallImageIndex = 48;
            // 
            // navItemFieldImage
            // 
            this.navItemFieldImage.Caption = "图象字段";
            this.navItemFieldImage.Name = "navItemFieldImage";
            this.navItemFieldImage.SmallImageIndex = 37;
            // 
            // navItemRichTextBox
            // 
            this.navItemRichTextBox.Caption = "RichTextBox";
            this.navItemRichTextBox.Name = "navItemRichTextBox";
            this.navItemRichTextBox.SmallImageIndex = 50;
            // 
            // navItemHViewFieldBox
            // 
            this.navItemHViewFieldBox.Caption = "横向显示字段";
            this.navItemHViewFieldBox.Name = "navItemHViewFieldBox";
            this.navItemHViewFieldBox.SmallImageIndex = 49;
            // 
            // navItemCheckBox
            // 
            this.navItemCheckBox.Caption = "选择框";
            this.navItemCheckBox.Name = "navItemCheckBox";
            this.navItemCheckBox.SmallImageIndex = 45;
            // 
            // navItemSubReport
            // 
            this.navItemSubReport.Caption = "子报表";
            this.navItemSubReport.Name = "navItemSubReport";
            this.navItemSubReport.SmallImageIndex = 44;
            // 
            // navItemBarCode
            // 
            this.navItemBarCode.Caption = "条形码";
            this.navItemBarCode.Name = "navItemBarCode";
            this.navItemBarCode.SmallImageIndex = 43;
            // 
            // navItemPictureBox
            // 
            this.navItemPictureBox.Caption = "图象";
            this.navItemPictureBox.Name = "navItemPictureBox";
            this.navItemPictureBox.SmallImageIndex = 38;
            // 
            // navitemOleObject
            // 
            this.navitemOleObject.Caption = "OLE 对象";
            this.navitemOleObject.Name = "navitemOleObject";
            this.navitemOleObject.SmallImageIndex = 46;
            // 
            // navItemLine
            // 
            this.navItemLine.Caption = "线条";
            this.navItemLine.Name = "navItemLine";
            this.navItemLine.SmallImageIndex = 39;
            // 
            // navItemFrame
            // 
            this.navItemFrame.Caption = "边框";
            this.navItemFrame.Name = "navItemFrame";
            this.navItemFrame.SmallImageIndex = 40;
            // 
            // imageMainList
            // 
            this.imageMainList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageMainList.ImageStream")));
            this.imageMainList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageMainList.Images.SetKeyName(0, "");
            this.imageMainList.Images.SetKeyName(1, "");
            this.imageMainList.Images.SetKeyName(2, "");
            this.imageMainList.Images.SetKeyName(3, "");
            this.imageMainList.Images.SetKeyName(4, "");
            this.imageMainList.Images.SetKeyName(5, "");
            this.imageMainList.Images.SetKeyName(6, "");
            this.imageMainList.Images.SetKeyName(7, "");
            this.imageMainList.Images.SetKeyName(8, "");
            this.imageMainList.Images.SetKeyName(9, "");
            this.imageMainList.Images.SetKeyName(10, "");
            this.imageMainList.Images.SetKeyName(11, "");
            this.imageMainList.Images.SetKeyName(12, "");
            this.imageMainList.Images.SetKeyName(13, "");
            this.imageMainList.Images.SetKeyName(14, "");
            this.imageMainList.Images.SetKeyName(15, "");
            this.imageMainList.Images.SetKeyName(16, "");
            this.imageMainList.Images.SetKeyName(17, "");
            this.imageMainList.Images.SetKeyName(18, "");
            this.imageMainList.Images.SetKeyName(19, "");
            this.imageMainList.Images.SetKeyName(20, "");
            this.imageMainList.Images.SetKeyName(21, "");
            this.imageMainList.Images.SetKeyName(22, "");
            this.imageMainList.Images.SetKeyName(23, "");
            this.imageMainList.Images.SetKeyName(24, "");
            this.imageMainList.Images.SetKeyName(25, "");
            this.imageMainList.Images.SetKeyName(26, "");
            this.imageMainList.Images.SetKeyName(27, "");
            this.imageMainList.Images.SetKeyName(28, "");
            this.imageMainList.Images.SetKeyName(29, "");
            this.imageMainList.Images.SetKeyName(30, "");
            this.imageMainList.Images.SetKeyName(31, "");
            this.imageMainList.Images.SetKeyName(32, "");
            this.imageMainList.Images.SetKeyName(33, "");
            this.imageMainList.Images.SetKeyName(34, "");
            this.imageMainList.Images.SetKeyName(35, "");
            this.imageMainList.Images.SetKeyName(36, "");
            this.imageMainList.Images.SetKeyName(37, "");
            this.imageMainList.Images.SetKeyName(38, "");
            this.imageMainList.Images.SetKeyName(39, "");
            this.imageMainList.Images.SetKeyName(40, "");
            this.imageMainList.Images.SetKeyName(41, "");
            this.imageMainList.Images.SetKeyName(42, "");
            this.imageMainList.Images.SetKeyName(43, "");
            this.imageMainList.Images.SetKeyName(44, "");
            this.imageMainList.Images.SetKeyName(45, "");
            this.imageMainList.Images.SetKeyName(46, "");
            this.imageMainList.Images.SetKeyName(47, "");
            this.imageMainList.Images.SetKeyName(48, "");
            this.imageMainList.Images.SetKeyName(49, "");
            this.imageMainList.Images.SetKeyName(50, "");
            // 
            // dockPanel3
            // 
            this.dockPanel3.Controls.Add(this.dockPanel3_Container);
            this.dockPanel3.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel3.ID = new System.Guid("93acf743-c798-4e21-ad53-85230bcde97a");
            this.dockPanel3.Location = new System.Drawing.Point(3, 29);
            this.dockPanel3.Name = "dockPanel3";
            this.dockPanel3.OriginalSize = new System.Drawing.Size(194, 416);
            this.dockPanel3.Size = new System.Drawing.Size(194, 413);
            this.dockPanel3.Text = "数据源";
            // 
            // dockPanel3_Container
            // 
            this.dockPanel3_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel3_Container.Name = "dockPanel3_Container";
            this.dockPanel3_Container.Size = new System.Drawing.Size(194, 413);
            this.dockPanel3_Container.TabIndex = 0;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // paneDesign
            // 
            this.paneDesign.BackColor = System.Drawing.Color.White;
            this.paneDesign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paneDesign.Location = new System.Drawing.Point(200, 78);
            this.paneDesign.Name = "paneDesign";
            this.paneDesign.Size = new System.Drawing.Size(376, 467);
            this.paneDesign.TabIndex = 5;
            // 
            // xDesignPanel1
            // 
            this.xDesignPanel1.CurrentObj = null;
            this.xDesignPanel1.DataObj = null;
            this.xDesignPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xDesignPanel1.Location = new System.Drawing.Point(0, 0);
            this.xDesignPanel1.Name = "xDesignPanel1";
            this.xDesignPanel1.ReportIO = reportDataIO1;
            this.xDesignPanel1.SectionList = null;
            this.xDesignPanel1.Size = new System.Drawing.Size(459, 496);
            this.xDesignPanel1.TabIndex = 0;
            this.xDesignPanel1.UndoMgr = undoMgr1;
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 545);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(776, 21);
            this.statusBar1.TabIndex = 6;
            this.statusBar1.Text = "欢迎使用 DIY 报表设计器  ";
            // 
            // MainDesignForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(776, 566);
            this.Controls.Add(this.paneDesign);
            this.Controls.Add(this.panelContainer1);
            this.Controls.Add(this.dockPanObjProperty);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainDesignForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DIY 报表设计器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rItemCobFontStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rItemCobFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanObjProperty.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            this.panelContainer1.ResumeLayout(false);
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.dockPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#endregion 内部自动生成代码...

		#region 自定义事件声明...
		private DIYReport.ReportModel.XReportIOEventHandler _BeginXReportIOProcess;
		public event DIYReport.ReportModel.XReportIOEventHandler BeginXReportIOProcess{
			add{ _BeginXReportIOProcess +=value;}
			remove{_BeginXReportIOProcess -=value;}
		}
		protected void OnBeginXReportIOProcess(DIYReport.ReportModel.XReportIOEventArgs arg){
			if(_BeginXReportIOProcess!=null){
				_BeginXReportIOProcess(this,arg);
			}
		}
		private DIYReport.ReportModel.XReportIOEventHandler _EndXReportIOProcess;
		public event DIYReport.ReportModel.XReportIOEventHandler EndXReportIOProcess{
			add{ _EndXReportIOProcess +=value;}
			remove{_EndXReportIOProcess -=value;}
		}
		protected void OnEndXReportIOProcess(DIYReport.ReportModel.XReportIOEventArgs arg){
			if(_EndXReportIOProcess!=null){
				_EndXReportIOProcess(this,arg);
			}
		}
		#endregion 自定义事件声明...

		private DIYReport.Extend.Print.XPrintingSystem _PrintSystem;
		#region 构造函数...
		/// <summary>
		/// 构造函数....
		/// </summary>
		public MainDesignForm() : this(null,null,null){

		}
		/// <summary>
		/// 构造函数....
		/// </summary>
		/// <param name="dataReport"></param>
		public MainDesignForm(DIYReport.ReportModel.RptReport dataReport) : this(dataReport.DataSource,dataReport.UserParamList,dataReport) {
		}
		/// <summary>
		/// 构造函数....
		/// </summary>
		public MainDesignForm(object dsData,DIYReport.ReportModel.RptParamList userParamList,DIYReport.ReportModel.RptReport dataReport) {

			InitializeComponent();

			
			iniMainMenuItem();
			
			iniToolBar();

			registerEvent();

			paneDesign.Controls.Add(xDesignPanel1);

			if(dataReport!=null){
				xDesignPanel1.OpenReport(dataReport);
			}
			else{
				xDesignPanel1.CreateNewReport();
			}
//			if(xDesignPanel1.DataObj!=null){
//				xDesignPanel1.DataObj.UserParamList = userParamList;
//				xDesignPanel1.DataObj.DataSource = dsData;
//			}

			DIYReport.UserDIY.DesignEnviroment.DataSource = dsData;
			DIYReport.UserDIY.DesignEnviroment.CurrentReport = dataReport;
 
			xDesignPanel1.Dock = System.Windows.Forms.DockStyle.Fill ;
			xDesignPanel1.UndoMgrChanged +=new EventHandler(xDesignPanel1_UndoMgrChanged); 
			itemEdit_ReDo.Enabled = false;
			itemEdit_UnDo.Enabled = false ;

			_PrintSystem = new DIYReport.Extend.Print.XPrintingSystem();

			DIYReport.UserDIY.DesignEnviroment.IsUserDesign = true;
			DIYReport.UserDIY.DesignEnviroment.DrawControlType = DIYReport.ReportModel.RptObjType.None;  

		}
		#endregion 构造函数...

		#region 覆盖基类的方法...
		protected override void OnClosing(CancelEventArgs e) {
			DIYReport.UserDIY.DesignEnviroment.IsUserDesign = false;

			base.OnClosing (e);
		}

		#endregion 覆盖基类的方法...
		

		#region 初始化主窗口菜单...
	

		private Hashtable menuCommands;
		private Hashtable _AlignCommands;
		private Hashtable _FormattingCommands;
		private Hashtable _ToolBoxCommands;

		private DIYReport.UserDIY.UICommandExecutor _CommandExecutor; 
		private DIYReport.UserDIY.AlignExecutor _AlignExecutor;
		private DIYReport.UserDIY.FormattingExecutor _FormatExecutor;

		private void iniMainMenuItem(){
			_CommandExecutor = new DIYReport.UserDIY.UICommandExecutor(xDesignPanel1);
			_AlignExecutor = new AlignExecutor(xDesignPanel1);
			_FormatExecutor = new FormattingExecutor(xDesignPanel1);

			_CommandExecutor.SetObjProperty +=new EventHandler(_CommandExecutor_SetObjProperty);

			
			DesignEnviroment.UICmidExecutor =  _CommandExecutor;

			menuCommands = new  Hashtable();
			_ToolBoxCommands = new Hashtable();
			_AlignCommands = new Hashtable();
			_FormattingCommands = new Hashtable();
			//初试化字体相关。
			iniFontFormat();

			//file 相关
			addCommand(itemFile_New,UICommands.NewReport);
			addCommand(itemFile_Open,UICommands.OpenFile);
            this.itemFile_Open.Enabled = false;
			addCommand(itemFile_Save ,UICommands.SaveFile);
			addCommand(itemFile_SaveAs ,UICommands.SaveFileAs);
            this.itemFile_SaveAs.Enabled = false;
			addCommand(itemFile_PageSetting,UICommands.PageSetup);
			addCommand(itemFile_ReportPreview,UICommands.Preview);
			addCommand(itemFile_Print ,UICommands.Print);
			addCommand(itemFile_SortGroup ,UICommands.SortAndGroup);
			addCommand(itemFile_Quit,UICommands.Exit);
			//编辑相关
			addCommand(itemEdit_UnDo,StandardCommands.Undo );
			addCommand(itemEdit_ReDo ,StandardCommands.Redo);
			addCommand(itemEdit_Copy,StandardCommands.Copy);
			addCommand(itemEdit_Cut,StandardCommands.Cut);
			addCommand(itemEdit_Past,StandardCommands.Paste);
			addCommand(itemEdit_Delete ,StandardCommands.Delete);
			addCommand(itemEdit_SelectAll,StandardCommands.SelectAll);
			addCommand(itemEditProperty ,UICommands.ShowProperty);

			
			addCommand(itemFormatSpacingH,StandardCommands.VertSpaceConcatenate);
			addCommand(itemFormatSpacingV,StandardCommands.HorizSpaceConcatenate);
			addCommand(itemFormatControl,UICommands.ControlHandle );
			
			//工具菜单
			addCommand(itemTools_Output,UICommands.Output);
			addCommand(itemTools_Import,UICommands.Import);

			//格式化设置相关
			addFormattingCommand(itemFormat_BackColor,FormattingCommands.BackColor);
			addFormattingCommand(itemFormatForeColor,FormattingCommands.ForeColor);
			addFormattingCommand(itemFormatFontBold,FormattingCommands.Bold);
			addFormattingCommand(itemFormatFontItalic,FormattingCommands.Italic);
			addFormattingCommand(itemFormatUnderline,FormattingCommands.Underline);
			addFormattingCommand(itemFormatFontLeft,FormattingCommands.JustifyLeft);
			addFormattingCommand(itemFormatFontRight,FormattingCommands.JustifyRight);
			addFormattingCommand(itemFormatFontCenter,FormattingCommands.JustifyCenter);

			addFormattingCommand(itemFormatOrderFront,StandardCommands.BringToFront);
			addFormattingCommand(itemFormatOrderGround,StandardCommands.SendToBack);


			addAlignCommand(itemFormatAlignLeft,StandardCommands.AlignLeft);
			addAlignCommand(itemFormatAlignRight,StandardCommands.AlignRight);
			addAlignCommand(itemFormatAlignTop,StandardCommands.AlignTop);
			addAlignCommand(itemFormatAlignBottom,StandardCommands.AlignBottom);
			addAlignCommand(itemFormatSizeHeight,StandardCommands.SizeToControlHeight);
			addAlignCommand(itemFormatSizeWidth,StandardCommands.SizeToControlWidth);


			//处理增加相应控件相关
			addNavItemRptCommands(navItemPoint,RptDesignCommands.RptNone);
			addNavItemRptCommands(navItemLable ,RptDesignCommands.RptLabel);
			addNavItemRptCommands(navItemFieldBox,RptDesignCommands.RptFieldText);
			addNavItemRptCommands(navItemFieldImage,RptDesignCommands.RptFieldImage);
			addNavItemRptCommands(navItemCheckBox,RptDesignCommands.RptCheckBox);
			addNavItemRptCommands(navItemPictureBox,RptDesignCommands.RptPictureBox);
			addNavItemRptCommands(navItemSubReport,RptDesignCommands.RptSubReport);
			addNavItemRptCommands(navItemBarCode,RptDesignCommands.RptBarCode);
			addNavItemRptCommands(navitemOleObject,RptDesignCommands.RptOleObject);
			addNavItemRptCommands(navItemLine,RptDesignCommands.RptLine);
			addNavItemRptCommands(navItemFrame ,RptDesignCommands.RptFrame);

			addNavItemRptCommands(navItemHViewFieldBox ,RptDesignCommands.RptHViewSpecFieldBox);
			addNavItemRptCommands(navItemExpressBox ,RptDesignCommands.RptExpressBox); //
			addNavItemRptCommands(navItemRichTextBox ,RptDesignCommands.RptRichTextBox); 
			
		}
		//初始化字体相关
		private void iniFontFormat(){
			System.Drawing.Text.InstalledFontCollection c = new System.Drawing.Text.InstalledFontCollection();
			foreach(System.Drawing.FontFamily font in c.Families){
				rItemCobFontStyle.Items.Add(font.GetName(System.Globalization.CultureInfo.CurrentCulture.LCID));
			}
			rItemCobFontStyle.NullText = "宋体";
			foreach(byte size in FormattingCommands.FontSizeSet){
				rItemCobFontSize.Items.Add(size.ToString()); 
			}
			rItemCobFontSize.NullText = "9";
			//rItemCobFontStyle.Items.Add()
		}
		private void addCommand(DevExpress.XtraBars.BarButtonItem item,System.ComponentModel.Design.CommandID commandID){
			menuCommands[commandID] = item;
			item.Tag = commandID;
		}
		private void addAlignCommand(DevExpress.XtraBars.BarButtonItem item,System.ComponentModel.Design.CommandID commandID){
			_AlignCommands[commandID] = item;
			item.Tag = commandID;
		}
		private void addFormattingCommand(DevExpress.XtraBars.BarButtonItem item,System.ComponentModel.Design.CommandID commandID){
			_FormattingCommands[commandID] = item;
			item.Tag = commandID;
		}
		//增加报表对象操作的命令
		private void addNavItemRptCommands(DevExpress.XtraNavBar.NavBarItem item,System.ComponentModel.Design.CommandID commandID){
			_ToolBoxCommands[commandID] = item;
			item.Tag = commandID;
		}
		//初始化工具条操作相关。
		private void iniToolBar(){
			//格式化文件主要操作相关
			this.barMainMenu.OptionsBar.DisableCustomization = true;
			this.barMainMenu.OptionsBar.AllowQuickCustomization = false;
			this.barMainMenu.OptionsBar.MultiLine = false;

			this.barFile.OptionsBar.AllowQuickCustomization = false;
			this.barFile.OptionsBar.DisableCustomization = true;
			this.barFile.OptionsBar.MultiLine = false;
			this.barFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_New),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_Open),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_Save),
																								  new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_SaveAs),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_PageSetting,true),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_ReportPreview),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_Print),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFile_SortGroup,true),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemEdit_UnDo,true),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemEdit_ReDo),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemEdit_Cut,true),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemEdit_Copy),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemEdit_Past),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemEdit_Delete,true) ,
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatAlignLeft,true),  
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatAlignRight),  
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatAlignTop),  
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatAlignBottom),  
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatSizeHeight,true),  
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatSizeWidth),  
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatSpacingV,true),  
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatSpacingH),  
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatControl,true)});
			this.barFont.OptionsBar.DisableCustomization = true;
			this.barFont.OptionsBar.AllowQuickCustomization = false;
			this.barFont.OptionsBar.MultiLine = false;
			this.barFont.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatFontBold,true),  
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatFontItalic),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatUnderline),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormat_BackColor,true),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatForeColor),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatFontLeft,true ),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatFontCenter),
																								 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatFontRight ),
				 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatOrderFront,true ),
				 new DevExpress.XtraBars.LinkPersistInfo(this.itemFormatOrderGround )
																								 });


																								 
			}

		//登记菜单的事件。
		private void registerEvent(){
			foreach(DevExpress.XtraBars.BarButtonItem item in menuCommands.Values){
				item.ItemClick +=new DevExpress.XtraBars.ItemClickEventHandler(barMenuItem_ItemClick);
				setCommandHint(item);
			}
			foreach(DevExpress.XtraBars.BarButtonItem alignItem in _AlignCommands.Values){
				alignItem.ItemClick +=new DevExpress.XtraBars.ItemClickEventHandler(alignItem_ItemClick);
				setCommandHint(alignItem);
			}
			foreach(DevExpress.XtraBars.BarButtonItem formatItem in _FormattingCommands.Values){
				formatItem.ItemClick +=new DevExpress.XtraBars.ItemClickEventHandler(formatItem_ItemClick);
				setCommandHint(formatItem);
			}

			foreach(DevExpress.XtraNavBar.NavBarItem navItem in _ToolBoxCommands.Values){
				navItem.LinkClicked +=new DevExpress.XtraNavBar.NavBarLinkEventHandler(navItem_LinkClicked);
			}
		}
		private void setCommandHint(DevExpress.XtraBars.BarButtonItem item){
			int index = item.Caption.IndexOf('(');
			if(index > 0)
				item.Hint = item.Caption.Substring(0,index); 
			else
				item.Hint = item.Caption;
		}
		#endregion 初始化主窗口菜单...

		private void barMenuItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
			DIYReport.UserDIY.DesignEnviroment.IsUserDesign = false;
			System.ComponentModel.Design.CommandID cmdID = e.Item.Tag  as System.ComponentModel.Design.CommandID;
	
			DIYReport.ReportModel.XReportIOEventArgs ioEventArgs = new DIYReport.ReportModel.XReportIOEventArgs(xDesignPanel1.DataObj,cmdID);
			OnBeginXReportIOProcess(ioEventArgs);
			if(ioEventArgs.HasProcessed){
				if(cmdID.Equals(UICommands.NewReport) || cmdID.Equals(UICommands.OpenFile))
					xDesignPanel1.OpenReport(ioEventArgs.DataReport);
			}
			else{
				if(cmdID.Equals(UICommands.ShowProperty)){
					dockPanObjProperty.Show();
					editRptObjAttribute1.SetPropertryObject(xDesignPanel1.DataObj,DesignEnviroment.CurrentRptObj);  
				}
				else if(cmdID.Equals(UICommands.Print)){
					_PrintSystem.Print(xDesignPanel1.DataObj); 
				}
				else if(cmdID.Equals(UICommands.Preview) ){
					_PrintSystem.PrintPreview(xDesignPanel1.DataObj);
				}
				else if(cmdID.Equals(UICommands.PageSetup)){
					_PrintSystem.PageSetup(xDesignPanel1.DataObj);
				}
				else{
					_CommandExecutor.ExecCommand(cmdID);    
				}
			}
			OnEndXReportIOProcess(ioEventArgs); 

			DIYReport.UserDIY.DesignEnviroment.IsUserDesign = true;
		}
		private void navItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e) {
			_CommandExecutor.ExecRptCtlType( e.Link.Item.Tag  as System.ComponentModel.Design.CommandID);    
		}
		private void alignItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
			_AlignExecutor.ExecCommand(e.Item.Tag  as System.ComponentModel.Design.CommandID); 
		}
		private void formatItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
			_FormatExecutor.ExecCommand(e.Item.Tag  as System.ComponentModel.Design.CommandID); 

			//在这里判断和处理文本报表对象格式化的信息
			formatRptTextObj(e.Item.Tag  as System.ComponentModel.Design.CommandID);
		}
		private void _CommandExecutor_SetObjProperty(object sender, EventArgs e) {
			if(dockPanObjProperty.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible){  
				editRptObjAttribute1.SetPropertryObject(xDesignPanel1.DataObj,DesignEnviroment.CurrentRptObj);
				
				 DIYReport.ReportModel.RptTextObj rptObj = DesignEnviroment.CurrentRptObj as DIYReport.ReportModel.RptTextObj;
				if(rptObj!=null){
					setbarItemEnabled(true);
					barEditItemFontSize.EditValue = rptObj.Font.Size; 
					barEditItemFontStyle.EditValue = rptObj.Font.FontFamily.Name;   
					itemFormatUnderline.Down = rptObj.Font.Underline;
					itemFormatFontBold.Down = rptObj.Font.Bold ;
					itemFormatFontItalic.Down = rptObj.Font.Italic;

					itemFormatFontLeft.Down = rptObj.Alignment == System.Drawing.StringAlignment.Near;  
					 itemFormatFontCenter.Down = rptObj.Alignment == System.Drawing.StringAlignment.Center;  
					itemFormatFontRight.Down = rptObj.Alignment == System.Drawing.StringAlignment.Far;

				}
				else{
					setbarItemEnabled(false);
				}
			}
		}
		private void setbarItemEnabled(bool enabled){
			this.barEditItemFontSize.Enabled = enabled;
			this.barEditItemFontStyle.Enabled = enabled;
			 this.itemFormatFontBold.Enabled = enabled;
			 this.itemFormatFontItalic.Enabled = enabled;
			 this.itemFormatUnderline.Enabled = enabled;
			 this.itemFormat_BackColor.Enabled = enabled;
			 this.itemFormatForeColor.Enabled = enabled;
			 this.itemFormatFontLeft.Enabled = enabled;
			 this.itemFormatFontCenter.Enabled = enabled;
			this.itemFormatFontRight.Enabled = enabled;
		}
		private void formatRptTextObj(System.ComponentModel.Design.CommandID cmdID){
			DIYReport.ReportModel.RptTextObj rptObj = DesignEnviroment.CurrentRptObj as DIYReport.ReportModel.RptTextObj;
			if(rptObj==null){
				return;
			}
			//先设置字体
			float size = DIYReport.PublicFun.ToFloat(barEditItemFontSize.EditValue);
			string fontName = barEditItemFontStyle.EditValue.ToString();
			Font f = new Font(fontName,size);
			FontStyle fontType = FontStyle.Regular;
			rptObj.BeginUpdate();
			if(itemFormatUnderline.Down)
				fontType = fontType | FontStyle.Underline;
			if(itemFormatFontBold.Down)
				fontType = fontType | FontStyle.Bold;
			if(itemFormatFontItalic.Down)
				fontType = fontType | FontStyle.Italic;
			Font newFont = new Font(f,fontType);
			rptObj.Font = (Font)newFont.Clone();
			if(cmdID.Equals(FormattingCommands.JustifyLeft)){
				if(itemFormatFontLeft.Down)
					rptObj.Alignment = System.Drawing.StringAlignment.Near;
			}
			if(cmdID.Equals(FormattingCommands.JustifyCenter)){
				if(itemFormatFontCenter.Down)
					rptObj.Alignment = System.Drawing.StringAlignment.Center;
			}
			if(cmdID.Equals(FormattingCommands.JustifyRight)){
				if(itemFormatFontRight.Down)
					rptObj.Alignment = System.Drawing.StringAlignment.Far;
			}

			rptObj.EndUpdate();
			
		}

		private void xDesignPanel1_UndoMgrChanged(object sender, EventArgs e) {
			itemEdit_ReDo.Enabled = xDesignPanel1.UndoMgr.CanRedo ;
			itemEdit_UnDo.Enabled = xDesignPanel1.UndoMgr.CanUndo ;
		}
	}								
}									
									
									
									
