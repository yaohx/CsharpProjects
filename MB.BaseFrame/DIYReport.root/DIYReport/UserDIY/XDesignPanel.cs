//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-03
// Description	:	 XDesignPanel 报表设计操作界面控件。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace DIYReport.UserDIY {
 
	/// <summary>
	/// XDesignPanel 报表设计操作界面控件。
	/// </summary>
	public class XDesignPanel : System.Windows.Forms.UserControl,DIYReport.Interface.IDesignPanel{ 
		#region 内部自动生成代码...

		private System.Windows.Forms.Panel panMain;
		private DIYReport.UserDIY.DesignRuler rulerLeft;
		private DIYReport.UserDIY.DesignRuler rulerTop;
		private System.Windows.Forms.Panel panDesign;
		private System.Windows.Forms.VScrollBar vscrBar;
		private System.Windows.Forms.HScrollBar hscrBar;
		private System.Windows.Forms.Panel panRightBottom;
		private System.Windows.Forms.Panel panLeftTop;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ContextMenu  contextMenu1;
		private XCMenuItem mItemBringToFront;
		private XCMenuItem mItemSendToBack;
		private XCMenuItem menuItem3;
		private XCMenuItem mItemCut;
		private XCMenuItem mItemCopy;
		private XCMenuItem mItemPast;
		private XCMenuItem menuItem7;
		private XCMenuItem mItemDelete;
		private XCMenuItem menuItem9;
		private XCMenuItem mItemInsertTopMargin;
		private XCMenuItem mItemInsertReportHeader;
		private XCMenuItem mItemInsertPageHeader;
		private XCMenuItem mItemInsertGroupHeader;
		private XCMenuItem mItemInsertDataDetail;
		private XCMenuItem mItemInsertGroupFooter;
		private XCMenuItem mItemPageFooter;
		private XCMenuItem mItemReportFooter;
		private XCMenuItem mItemBottomMargin;
		private XCMenuItem menuItem19;
		private System.ComponentModel.IContainer components;
		#region 清理所有正在使用的资源...
		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion 清理所有正在使用的资源...

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(XDesignPanel));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panMain = new System.Windows.Forms.Panel();
			this.panRightBottom = new System.Windows.Forms.Panel();
			this.panDesign = new System.Windows.Forms.Panel();
			this.vscrBar = new System.Windows.Forms.VScrollBar();
			this.hscrBar = new System.Windows.Forms.HScrollBar();
			this.rulerLeft = new DIYReport.UserDIY.DesignRuler();
			this.rulerTop = new DIYReport.UserDIY.DesignRuler();
			this.panLeftTop = new System.Windows.Forms.Panel();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.mItemBringToFront = new DIYReport.UserDIY.XCMenuItem();
			this.mItemSendToBack = new DIYReport.UserDIY.XCMenuItem();
			this.menuItem3 = new DIYReport.UserDIY.XCMenuItem();
			this.mItemCut = new DIYReport.UserDIY.XCMenuItem();
			this.mItemCopy = new DIYReport.UserDIY.XCMenuItem();
			this.mItemPast = new DIYReport.UserDIY.XCMenuItem();
			this.menuItem7 = new DIYReport.UserDIY.XCMenuItem();
			this.mItemDelete = new DIYReport.UserDIY.XCMenuItem();
			this.menuItem19 = new DIYReport.UserDIY.XCMenuItem();
			this.menuItem9 = new DIYReport.UserDIY.XCMenuItem();
			this.mItemInsertTopMargin = new DIYReport.UserDIY.XCMenuItem();
			this.mItemInsertReportHeader = new DIYReport.UserDIY.XCMenuItem();
			this.mItemInsertPageHeader = new DIYReport.UserDIY.XCMenuItem();
			this.mItemInsertGroupHeader = new DIYReport.UserDIY.XCMenuItem();
			this.mItemInsertDataDetail = new DIYReport.UserDIY.XCMenuItem();
			this.mItemInsertGroupFooter = new DIYReport.UserDIY.XCMenuItem();
			this.mItemPageFooter = new DIYReport.UserDIY.XCMenuItem();
			this.mItemReportFooter = new DIYReport.UserDIY.XCMenuItem();
			this.mItemBottomMargin = new DIYReport.UserDIY.XCMenuItem();
			this.panMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// panMain
			// 
			this.panMain.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panMain.Controls.Add(this.panRightBottom);
			this.panMain.Controls.Add(this.panDesign);
			this.panMain.Controls.Add(this.vscrBar);
			this.panMain.Controls.Add(this.hscrBar);
			this.panMain.Controls.Add(this.rulerLeft);
			this.panMain.Controls.Add(this.rulerTop);
			this.panMain.Controls.Add(this.panLeftTop);
			this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panMain.Location = new System.Drawing.Point(0, 0);
			this.panMain.Name = "panMain";
			this.panMain.Size = new System.Drawing.Size(520, 320);
			this.panMain.TabIndex = 2;
			// 
			// panRightBottom
			// 
			this.panRightBottom.BackColor = System.Drawing.SystemColors.Control;
			this.panRightBottom.Location = new System.Drawing.Point(496, 296);
			this.panRightBottom.Name = "panRightBottom";
			this.panRightBottom.Size = new System.Drawing.Size(16, 16);
			this.panRightBottom.TabIndex = 5;
			// 
			// panDesign
			// 
			this.panDesign.BackColor = System.Drawing.Color.White;
			this.panDesign.Location = new System.Drawing.Point(24, 24);
			this.panDesign.Name = "panDesign";
			this.panDesign.Size = new System.Drawing.Size(448, 264);
			this.panDesign.TabIndex = 4;
			this.panDesign.Resize += new System.EventHandler(this.panDesign_Resize);
			// 
			// vscrBar
			// 
			this.vscrBar.Location = new System.Drawing.Point(496, 24);
			this.vscrBar.Name = "vscrBar";
			this.vscrBar.Size = new System.Drawing.Size(17, 277);
			this.vscrBar.TabIndex = 3;
			this.vscrBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vscrBar_Scroll);
			// 
			// hscrBar
			// 
			this.hscrBar.Location = new System.Drawing.Point(24, 296);
			this.hscrBar.Name = "hscrBar";
			this.hscrBar.Size = new System.Drawing.Size(472, 17);
			this.hscrBar.TabIndex = 2;
			this.hscrBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hscrBar_Scroll);
			// 
			// rulerLeft
			// 
			this.rulerLeft.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.rulerLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rulerLeft.BackgroundImage")));
			this.rulerLeft.BeginDrawPoint = 16;
			this.rulerLeft.IsHorizontal = false;
			this.rulerLeft.Location = new System.Drawing.Point(0, 24);
			this.rulerLeft.Name = "rulerLeft";
			this.rulerLeft.Size = new System.Drawing.Size(24, 294);
			this.rulerLeft.TabIndex = 1;
			// 
			// rulerTop
			// 
			this.rulerTop.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.rulerTop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rulerTop.BackgroundImage")));
			this.rulerTop.BeginDrawPoint = 0;
			this.rulerTop.IsHorizontal = true;
			this.rulerTop.Location = new System.Drawing.Point(24, 0);
			this.rulerTop.Name = "rulerTop";
			this.rulerTop.Size = new System.Drawing.Size(496, 24);
			this.rulerTop.TabIndex = 0;
			// 
			// panLeftTop
			// 
			this.panLeftTop.BackColor = System.Drawing.SystemColors.Control;
			this.panLeftTop.Location = new System.Drawing.Point(0, 0);
			this.panLeftTop.Name = "panLeftTop";
			this.panLeftTop.Size = new System.Drawing.Size(24, 24);
			this.panLeftTop.TabIndex = 0;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mItemBringToFront,
																						 this.mItemSendToBack,
																						 this.menuItem3,
																						 this.mItemCut,
																						 this.mItemCopy,
																						 this.mItemPast,
																						 this.menuItem7,
																						 this.mItemDelete,
																						 this.menuItem19,
																						 this.menuItem9});
			// 
			// mItemBringToFront
			// 
			this.mItemBringToFront.Index = 0;
			this.mItemBringToFront.Tag = null;
			this.mItemBringToFront.Text = "置于最前端";
			// 
			// mItemSendToBack
			// 
			this.mItemSendToBack.Index = 1;
			this.mItemSendToBack.Tag = null;
			this.mItemSendToBack.Text = "置于最底端";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Tag = null;
			this.menuItem3.Text = "-";
			// 
			// mItemCut
			// 
			this.mItemCut.Index = 3;
			this.mItemCut.Tag = null;
			this.mItemCut.Text = "剪切(&T)";
			// 
			// mItemCopy
			// 
			this.mItemCopy.Index = 4;
			this.mItemCopy.Tag = null;
			this.mItemCopy.Text = "复制(&C)";
			// 
			// mItemPast
			// 
			this.mItemPast.Index = 5;
			this.mItemPast.Tag = null;
			this.mItemPast.Text = "粘贴(&P)";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 6;
			this.menuItem7.Tag = null;
			this.menuItem7.Text = "-";
			// 
			// mItemDelete
			// 
			this.mItemDelete.Index = 7;
			this.mItemDelete.Tag = null;
			this.mItemDelete.Text = "删除(&D)";
			// 
			// menuItem19
			// 
			this.menuItem19.Index = 8;
			this.menuItem19.Tag = null;
			this.menuItem19.Text = "-";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 9;
			this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mItemInsertTopMargin,
																					  this.mItemInsertReportHeader,
																					  this.mItemInsertPageHeader,
																					  this.mItemInsertGroupHeader,
																					  this.mItemInsertDataDetail,
																					  this.mItemInsertGroupFooter,
																					  this.mItemPageFooter,
																					  this.mItemReportFooter,
																					  this.mItemBottomMargin});
			this.menuItem9.Tag = null;
			this.menuItem9.Text = "插入 Section";
			// 
			// mItemInsertTopMargin
			// 
			this.mItemInsertTopMargin.Index = 0;
			this.mItemInsertTopMargin.Tag = null;
			this.mItemInsertTopMargin.Text = "上边距";
			// 
			// mItemInsertReportHeader
			// 
			this.mItemInsertReportHeader.Index = 1;
			this.mItemInsertReportHeader.Tag = null;
			this.mItemInsertReportHeader.Text = "报表 Header";
			// 
			// mItemInsertPageHeader
			// 
			this.mItemInsertPageHeader.Index = 2;
			this.mItemInsertPageHeader.Tag = null;
			this.mItemInsertPageHeader.Text = "页 Header";
			// 
			// mItemInsertGroupHeader
			// 
			this.mItemInsertGroupHeader.Index = 3;
			this.mItemInsertGroupHeader.Tag = null;
			this.mItemInsertGroupHeader.Text = "分组 Header";
			// 
			// mItemInsertDataDetail
			// 
			this.mItemInsertDataDetail.Index = 4;
			this.mItemInsertDataDetail.Tag = null;
			this.mItemInsertDataDetail.Text = "数据区";
			// 
			// mItemInsertGroupFooter
			// 
			this.mItemInsertGroupFooter.Index = 5;
			this.mItemInsertGroupFooter.Tag = null;
			this.mItemInsertGroupFooter.Text = "分组 Footer";
			// 
			// mItemPageFooter
			// 
			this.mItemPageFooter.Index = 6;
			this.mItemPageFooter.Tag = null;
			this.mItemPageFooter.Text = "页 Footer";
			// 
			// mItemReportFooter
			// 
			this.mItemReportFooter.Index = 7;
			this.mItemReportFooter.Tag = null;
			this.mItemReportFooter.Text = "报表 Footer";
			// 
			// mItemBottomMargin
			// 
			this.mItemBottomMargin.Index = 8;
			this.mItemBottomMargin.Tag = null;
			this.mItemBottomMargin.Text = "下边距";
			// 
			// XDesignPanel
			// 
			this.ContextMenu = this.contextMenu1;
			this.Controls.Add(this.panMain);
			this.Name = "XDesignPanel";
			this.Size = new System.Drawing.Size(520, 320);
			this.Resize += new System.EventHandler(this.ReportDesign_Resize);
			this.Load += new System.EventHandler(this.ReportDesign_Load);
			this.panMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#endregion 内部自动生成代码...

		#region 变量定义...
		private object _CurrentObj;
		private DesignSectionList _SectionList;
		private DIYReport.ReportModel.RptReport _DataObj;

		//业务处理主要变量
		//private DataSet _DsFields = null;
		private DIYReport.Interface.IReportDataIO  _ReportIO;

		private DIYReport.UndoManager.UndoMgr _UndoMgr; 
		#endregion 变量定义...

		#region 自定义事件...
		private System.EventHandler _UndoMgrChanged;
		public event System.EventHandler UndoMgrChanged{
			add{
				_UndoMgrChanged +=value;
			}
			remove{
				_UndoMgrChanged -=value;
			}
		}
		private void onUndoMgrCHanged(System.EventArgs arg){
			if(_UndoMgrChanged!=null){
				_UndoMgrChanged(this,arg);
			}
		}
		#endregion 自定义事件...

		#region 构造函数...
		private Hashtable _CMenuCommands;
		private RptDesignCommandsExecutor _RptDesignExec;
		public XDesignPanel() {
			InitializeComponent();
			//panMain.Visible = false;
			_UndoMgr = new DIYReport.UndoManager.UndoMgr();
			_UndoMgr.UndoMgrChanged +=new EventHandler(_UndoMgr_UndoMgrChanged);

			_CMenuCommands = new Hashtable();
			_RptDesignExec = new RptDesignCommandsExecutor(this);
			
			contextMenu1.Popup +=new EventHandler(contextMenu1_Popup);
			//contextMenu1.Images = imageList1;
			//foreach(System.Drawing.Image image in imageList1
			//contextMenu1.i
			XCMenuItem.Images = imageList1.Images;
			iniContextMenu();

			rulerLeft.DesignPanel = this;
			rulerTop.DesignPanel = this;
		}
		#endregion 构造函数...

		#region 处理Context Menu 相关...
		private void iniContextMenu(){
			addMenuCommand(StandardCommands.BringToFront ,mItemBringToFront,0,false);
			addMenuCommand(StandardCommands.SendToBack,mItemSendToBack,1,false);
			addMenuCommand(StandardCommands.Cut,mItemCut,2,false);
			addMenuCommand(StandardCommands.Copy,mItemCopy,3,false);
			addMenuCommand(StandardCommands.Paste,mItemPast,4,false);
			addMenuCommand(StandardCommands.Delete,mItemDelete,5,true);

			addMenuCommand(RptDesignCommands.InsertTopMarginBand,mItemInsertTopMargin,-1,true);
			addMenuCommand(RptDesignCommands.InsertReportHeaderBand,mItemInsertReportHeader,-1,true);
			addMenuCommand(RptDesignCommands.InsertPageHeaderBand,mItemInsertPageHeader,-1,true);
			addMenuCommand(RptDesignCommands.InsertGroupFooterBand,mItemInsertGroupHeader,-1,false);
			addMenuCommand(RptDesignCommands.InsertDetailBand,mItemInsertDataDetail,-1,true);
			addMenuCommand(RptDesignCommands.InsertGroupFooterBand,mItemInsertGroupFooter,-1,false);
			addMenuCommand(RptDesignCommands.InsertPageFooterBand,mItemPageFooter,-1,true);
			addMenuCommand(RptDesignCommands.InsertReportFooterBand,mItemReportFooter,-1,true);
			addMenuCommand(RptDesignCommands.InsertBottomMarginBand,mItemBottomMargin,-1,true);
		}
		private void addMenuCommand(CommandID cmdID,XCMenuItem menuItem,int imageIndex,bool enabled){
			menuItem.Enabled = enabled;
			menuItem.Tag = cmdID;
			menuItem.ImageIndex = imageIndex;
			_CMenuCommands[cmdID] = menuItem;
			menuItem.Click +=new EventHandler(menuItem_Click);

		}
		private void menuItem_Click(object sender, EventArgs e) {
			XCMenuItem menuItem = sender as XCMenuItem;
			if(menuItem!=null){
				_RptDesignExec.ExecCommand(menuItem.Tag as CommandID); 
			}
		}
		
		private void contextMenu1_Popup(object sender, EventArgs e) {
			bool b = this.SectionList.GetDataSectionList().Count > 0;
			mItemCut.Enabled = b;
			mItemCopy.Enabled = b;
			mItemPast.Enabled = b;
		}
		#endregion 处理Context Menu 相关...
		
		#region public 方法...
		/// <summary>
		/// 创建一个新的报表
		/// </summary>
		/// <returns></returns>
		[Obsolete("请直接在调用的中创建一个新的报表然后用OpenReport的方式打开。")] 
		public DIYReport.ReportModel.RptReport CreateNewReport(){
			_DataObj = ReportIO.NewReport(); 
			DIYReport.UserDIY.DesignEnviroment.CurrentReport = _DataObj;
 
			iniNewReportDesign();

			DesignEnviroment.DesignHasChanged = false;
			return _DataObj;
		}
		/// <summary>
		/// 打开已经存在的报表
		/// </summary>
		/// <param name="pReport"></param>
		/// <returns></returns>
		public DIYReport.ReportModel.RptReport OpenReport(DIYReport.ReportModel.RptReport pReport){
			DIYReport.UserDIY.DesignEnviroment.CurrentReport = pReport;

			if(pReport!=null ){				 
				_DataObj = pReport;
				//判断是否有分组的Section，初始化分组设计的字段，然后当前设计的字段引用到分组的Section 中
				//iniGroupFieldOnOpen(_DataObj);
				PublicFun.IniGroupFieldOnOpen(_DataObj); 

				iniNewReportDesign();
			}
			DesignEnviroment.DesignHasChanged = false; 
			return _DataObj;

		}
        /// <summary>
        /// 删除当前选中的报表控件
        /// </summary>
        public void DeleteSelectedControls() {
            DeleteSelectedControls(false);
        }
		/// <summary>
		/// 删除当前选中的报表控件
		/// </summary>
		public void DeleteSelectedControls(bool delByKey){
			object obj = DesignEnviroment.CurrentRptObj ;
			DesignSection section  = _SectionList.GetActiveSection();
			if(obj==null || section==null)
				return;
			if(string.Compare(obj.GetType().Name,"RptSection",true)==0){
                if (delByKey) {
                    if (section.DataObj.BackgroundImage != null)
                        section.DataObj.BackgroundImage = null;
                } else {
                    if (section.DesignControls.Count > 0) {
                        DialogResult re = MessageBox.Show("删除Section ,Section  上包含的控件也一起被删除。是否继续?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (re == DialogResult.No)
                            return;
                    }
                    this.SectionList.DataObj.RemoveSection(section.DataObj);
                }
			}
			else{
				section.DesignControls.RemoveSelectedControl();
			}
		}
		#region 剪贴、复制、粘贴操作处理相关...
		/// <summary>
		/// 剪贴操作
		/// </summary>
		public void Cut(){
			copyCtl(false);
		}
		/// <summary>
		/// 复制操作
		/// </summary>
		public void Copy(){
			copyCtl(true);
		}
		public void Past(){
			DesignSection section  = _SectionList.GetActiveSection();
			if(section==null)
				return;
			IList dataLst = DIYReport.UserDIY.ClipboardEx.GetFromClipBoard(); 
			section.DesignControls.Add(dataLst); 
		}
		private void copyCtl(bool isCopy){
			DesignSection section  = _SectionList.GetActiveSection();
			if(section==null)
				return;

			IList dataLst = section.DesignControls.GetSelectedCtlsData();
			DIYReport.UserDIY.ClipboardEx.CopyToClipBoard(dataLst);
			if(!isCopy){
				System.Threading.Thread.CurrentThread.Join(200);
				section.DesignControls.RemoveSelectedControl();
			}
		}
		#endregion 剪贴、复制、粘贴操作处理相关...

		#endregion public 方法...

		#region 扩展的Public 属性...
		/// <summary>
		/// 撤消和恢复的管理处理对象
		/// </summary>
		public DIYReport.UndoManager.UndoMgr UndoMgr{
			get{
				return _UndoMgr;
			}
			set{
				_UndoMgr = value;
			}
		}
		/// <summary>
		///  报表操作的IO对象
		/// </summary>
		[Obsolete("过期的属性，请直接使用RptObjectHelper 代替。")] 
		public  DIYReport.Interface.IReportDataIO  ReportIO{
			get{
				if(_ReportIO==null){
					_ReportIO = new DIYReport.ReportDataIO(); 
				}
				return _ReportIO;
			}
			set{
				_ReportIO = value;
			}
		}
		/// <summary>
		/// 该报表甚至对象中所有的Design Section
		/// </summary>
		public DesignSectionList SectionList{
			get{
				return _SectionList;
			}
			set{
				_SectionList = value;
			}
		}
		/// <summary>
		/// 报表的数据
		/// </summary>
		public DIYReport.ReportModel.RptReport DataObj{
			get{
				return _DataObj;
			}
			set{
				_DataObj = value;
			}
		}
		/// <summary>
		/// 当前选择的是什么对象
		/// </summary>
		public object CurrentObj{
			get{
				return _CurrentObj;
			}
			set{
				_CurrentObj = value;
			}
		}
		public Panel DesignBack{
			get{
				return panDesign ;
			}
		}
		#endregion 扩展的Public 属性...

		#region 创建Section...
		/// <summary>
		/// 把所有的Design Section 都显示给用户设计
		/// </summary>
		public void CreateDesignSection(){
			
			//DesignSection lastSection = null;
			foreach(DesignSection section in _SectionList){
				if(section.IsDisplay){
					//section.Visible = true;
					//section.CaptionCtl.Location = new Point(0,height);
					//section.Location = new Point(0,height +  SectionCaption.CAPTION_HEIGHT );
					panDesign.Controls.Add(section); 
					//section.BringToFront();
					panDesign.Controls.Add(section.CaptionCtl); 
					//height +=section.Height +  SectionCaption.CAPTION_HEIGHT  ;
					//section.IsDispBottomSection = false;
					//
				}//
			}

		}
		#endregion 创建Section...

		#region 界面事件处理...
		private void hscrBar_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e) {
			rulerTop.Left = panLeftTop.Width  - e.NewValue ; 
			panDesign.Left = rulerLeft.Width - e.NewValue ; 
			rulerTop.Width =  panMain.Width - panLeftTop.Width - rulerTop.Left + 20 ;
			
			 
		}
		private void vscrBar_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e) {
			rulerLeft.Top = panLeftTop.Height  -e.NewValue ; 
			panDesign.Top = rulerTop.Height - e.NewValue ; 
			rulerLeft.Height  = panMain.Height - panLeftTop.Height   - rulerLeft.Top + 20 ;
		}


		private void ReportDesign_Resize(object sender, System.EventArgs e) {
			iniResizeDesignForm();
		}

		private void ReportDesign_Load(object sender, System.EventArgs e) {
			this.ParentForm.KeyPreview = true;
			this.ParentForm.Closed +=new EventHandler(ParentForm_Closed); 
			this.ParentForm.KeyDown +=new KeyEventHandler(ParentForm_KeyDown);
			this.ParentForm.KeyUp +=new KeyEventHandler(ParentForm_KeyUp);

			panLeftTop.Location = new Point(0,0);
			panLeftTop.Size = new Size(rulerLeft.Width ,rulerTop.Height);

			rulerTop.Location = new Point(panLeftTop.Width,0);
			rulerLeft.Location = new Point(0,rulerTop.Height);
			panDesign.Location = new Point( rulerLeft.Width , rulerTop.Height );

			panRightBottom.Location = new Point(panMain.Width - panRightBottom.Width ,panMain.Height - panRightBottom.Height);

			hscrBar.Location = new Point(rulerLeft.Width ,panMain.Height    -  hscrBar.Height);
			hscrBar.Size = new Size(panMain.Width - rulerLeft.Width - panRightBottom.Width  , hscrBar.Height); 

			vscrBar.Location = new Point(panMain.Width - vscrBar.Width, rulerTop.Height);
			vscrBar.Size = new Size(vscrBar.Width ,panMain.Height -  rulerTop.Height  - panRightBottom.Height);
			rulerLeft.Size = new Size(rulerLeft.Width ,panMain.Height - rulerLeft.Top  ); 


			rulerLeft.BringToFront();
			rulerTop.BringToFront();
			hscrBar.BringToFront();
			vscrBar.BringToFront();
			panLeftTop.BringToFront(); 
		}

		private void panDesign_Resize(object sender, System.EventArgs
			e) {
			//rulerLeft.DesignPanel = this;
			rulerLeft.DrawRuler(); 
			rulerTop.DrawRuler(); 

			reCalculateScrollValue();
		}

		private void tBarTools_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
//			foreach(ToolBarButton but in tBarTools.Buttons){
//				but.Pushed = false;
//			}
//			e.Button.Pushed = true;
//			DesignEnviroment.DrawControlType = (DIYReport.ReportModel.RptObjType)int.Parse(e.Button.Tag.ToString());
//			DesignEnviroment.IsCreateControl = DesignEnviroment.DrawControlType!= DIYReport.ReportModel.RptObjType.None;
		}

		private void tlbTopBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
//			//e.Button.Tag 的0-5为存储格式控件操作样式的 
//			this.Cursor = Cursors.WaitCursor ;
//			if(e.Button.Tag!=null){
//				int index = int.Parse(e.Button.Tag.ToString());  
//				if(index<6){
//					DesignSection section = _SectionList.GetActiveSection();  
//					section.DesignControls.FormatCtl((DIYReport.UserDIY.FormatCtlType)index);  
//				}
//				switch(index){
//					case 6://向左边靠齐
//						_SectionList.GetActiveSection().DesignControls.DockToLeft();
//						break;
//					case 7://向右边靠齐
//						_SectionList.GetActiveSection().DesignControls.DockToTop() ;
//						break;
//					case 8://显示方向控制盘
//						FrmArrowOperate.ShowArrowForm( _SectionList,this.ParentForm );  
//						break;
//					case 16:
//						//显示属性窗口
//						DesignEnviroment.ShowPropertyForm(this.ParentForm,true); 
//						break;
//					case 17:
//						//删除控件
//						DeleteSelectedControls();
//						break;
//					case 18://打印
//						using(DIYReport.Print.SwPrintView print = new DIYReport.Print.SwPrintView( DIYReport.UserDIY.DesignEnviroment.DataSource,_DataObj)){
//							print.Printer(); 
//						}
//						break;
//					case 19://打印预览
//						using(DIYReport.Print.SwPrintView printView = new DIYReport.Print.SwPrintView( DIYReport.UserDIY.DesignEnviroment.DataSource,_DataObj)){
//							printView.ShowPreview();
//						}
//						break;
//					case 20://打印页面设置
//						DIYReport.Print.RptPageSetting.ShowPageSetupDialog(_DataObj);   
//						break;
//					case 21://保存报表
//						_ReportIO.SaveReport(_DataObj,null); 
//						DIYReport.UserDIY.DesignEnviroment.DesignHasChanged = false;
//						break;
//					case 22://打开报表 
//						DIYReport.ReportModel.RptReport report =  _ReportIO.Open(); 
//						if(report!=null){
//							OpenReport( report );
//						}
//						break;
//					case 23://新增报表 
//						//CreateNewReport();
//						//MessageBox.Show("当前不支持从这里新建一张报表摸板.","操作提示"); 
//						//暂时把它修改为打印摸板的导出功能。
//						_ReportIO.Save(_DataObj);
//						break;
//					case 24 ://Undo
//						_UndoMgr.Undo();
//						break;
//					case 25 ://Redo 
//						_UndoMgr.Redo();
//						break;
//					case 26 : //显示分组和排序
//						IList fieldsList = DIYReport.UserDIY.DesignEnviroment.DesignField; 
//						DIYReport.GroupAndSort.frmSortAndGroup frm = new DIYReport.GroupAndSort.frmSortAndGroup(fieldsList); 
//						frm.AfterSortAndGroup +=new DIYReport.GroupAndSort.SortAndGroupEventHandler(frm_AfterSortAndGroup);
//						frm.ShowDialog(); 
//						break;
//					default:
//						break;
//				}
//			}
//			this.Cursor = Cursors.Arrow;
		}

		#endregion 界面事件处理...

		#region 内部函数处理...
		private void iniResizeDesignForm(){
			//设计的尺寸

			rulerTop.Size = new Size(panMain.Width - rulerTop.Left ,rulerTop.Height );
			rulerLeft.Size = new Size(rulerLeft.Width ,panMain.Height - rulerLeft.Top  ); 
			//右下角下的小贴片位置
			panRightBottom.Location = new Point(panMain.Width - panRightBottom.Width ,panMain.Height - panRightBottom.Height);
			//滚动条
			 
			hscrBar.Location = new Point(rulerLeft.Width ,panMain.Height    -  hscrBar.Height);
			hscrBar.Size = new Size(panMain.Width - rulerLeft.Width - panRightBottom.Width  , hscrBar.Height); 

			vscrBar.Location = new Point(panMain.Width - vscrBar.Width, rulerTop.Height);
			vscrBar.Size = new Size(vscrBar.Width ,panMain.Height -  rulerTop.Height  - panRightBottom.Height);

			//section 设计的区域
			//int width = vscrBar.Visible?vscrBar.Width : 0;
			//int height = hscrBar.Visible ? hscrBar.Height : 0 ;
			//panDesign.Size = new Size(panMain.Width - rulerLeft.Width - width ,panMain.Height - rulerTop.Height - height);
			
			reCalculateScrollValue();
		}
		private void reCalculateScrollValue(){
			int maxWidth = panDesign.Width + rulerLeft.Width + panRightBottom.Width - panMain.Width + 50;
			hscrBar.Maximum = maxWidth > 0? maxWidth : 0;
			int maxHeight  = panDesign.Height + rulerTop.Height + panRightBottom.Height - panMain.Height  + 50 ;
			vscrBar.Maximum = maxHeight > 0?maxHeight:0;
		}
		//重新布置和创建一个新的报表设计
		private void iniNewReportDesign(){
			if(_DataObj!=null){
				if(_SectionList!=null){
					foreach(DesignSection section in _SectionList){
						panDesign.Controls.Remove(section.CaptionCtl); 
						panDesign.Controls.Remove(section); 
					}
				}
				_DataObj.AfterValueChanged +=new DIYReport.ReportModel.RptEventHandler(_DataObj_AfterValueChanged);
				_SectionList = new DesignSectionList(this);
				_SectionList.BeforeRemoveSection +=new DesignSectionEventHandler(_SectionList_BeforeRemoveSection);
				_SectionList.AfterInsertSection +=new DesignSectionEventHandler(_SectionList_AfterInsertSection);
				_SectionList.AfterRefreshLayout +=new DesignSectionEventHandler(_SectionList_AfterRefreshLayout);
				_SectionList.CreateNewSectionList();
 
				CreateDesignSection();
				DIYReport.UserDIY.DesignEnviroment.CurrentReport  = _DataObj;
			}
		}
		//DIYReport.ReportModel.RptReport pReport

		#endregion 内部函数处理...

		#region DataObj_AfterValue...

		private void _DataObj_AfterValueChanged(object sender, DIYReport.ReportModel.RptEventArgs e) {
			//_DataObj.SectionList.
			int paperWidth = _DataObj.IsLandscape?_DataObj.PaperSize.Height:_DataObj.PaperSize.Width ;
			int width = paperWidth - _DataObj.Margins.Left - _DataObj.Margins.Right ;
			panDesign.Size = new Size(width ,panDesign.Height); 
			_DataObj.SectionList.ReSizeByPaperSize(width); 
			reCalculateScrollValue();

			rulerTop.DrawRuler();
		}

		#endregion DataObj_AfterValue...

		#region 接受Parent Form 的事件消息...

		private void ParentForm_Closed(object sender, EventArgs e) {
			//清理所有正在使用的资源...
			try{
				this.ParentForm.Closed -=new EventHandler(ParentForm_Closed); 
				this.ParentForm.KeyDown -=new KeyEventHandler(ParentForm_KeyDown);
				this.ParentForm.KeyUp -=new KeyEventHandler(ParentForm_KeyUp);
				disposeRptImage();
			}
			catch{}
		}
		private void disposeRptImage(){
			if(SectionList==null || SectionList.Count == 0)
				return;
			foreach(DIYReport.UserDIY.DesignSection section in this.SectionList){
				foreach(DIYReport.UserDIY.DesignControl ctl in section.DesignControls){
					DIYReport.Interface.IRptSingleObj   obj = ctl.DataObj ;
					if(obj.Type != DIYReport.ReportModel.RptObjType.Image)
						continue;
					System.Drawing.Image img = (obj as DIYReport.ReportModel.RptObj.RptPictureBox).Image ;
					if(img!=null){
						try{
							img.Dispose();
						}
						catch{
						}
					}
				}
			}
		}
		private void ParentForm_KeyDown(object sender, KeyEventArgs e) {
			DIYReport.UserDIY.DesignEnviroment.PressShiftKey = e.KeyValue == 16;
			DIYReport.UserDIY.DesignEnviroment.PressCtrlKey  = e.KeyValue == 17;
			//判断用户是否按下 Delete 键
			if(e.KeyValue ==46){
				DeleteSelectedControls(true);
			}
			//处理方向键
			DIYReport.UserDIY.DesignSection sSection = _SectionList.GetActiveSection();
			if(sSection!=null)
                sSection.DesignControls.ProcessArrowKeyDown(e.KeyValue, DIYReport.UserDIY.DesignEnviroment.PressCtrlKey);  
		}

		private void ParentForm_KeyUp(object sender, KeyEventArgs e) {
			DIYReport.UserDIY.DesignEnviroment.PressShiftKey = false;
			DIYReport.UserDIY.DesignEnviroment.PressCtrlKey  = false;
		}

		#endregion 接受Parent Form 的事件消息...
		//在分组操作确定之后重新排版报表的设计界面
		private void frm_AfterSortAndGroup(object sender, EventArgs e) {
			_SectionList.DataObj.CreateGroupSection();
			//_SectionList.Refresh();
		}

		private void _SectionList_BeforeRemoveSection(object sender, DesignSectionEventArgs e) {
			panDesign.Controls.Remove(e.Section.CaptionCtl); 
			panDesign.Controls.Remove(e.Section); 
		}

		private void _SectionList_AfterInsertSection(object sender, DesignSectionEventArgs e) {
			//当插入一个新的Design Section 后重新布局用户的报
			panDesign.Controls.Add(e.Section.CaptionCtl); 
			panDesign.Controls.Add(e.Section); 

		}

		private void _SectionList_AfterRefreshLayout(object sender, DesignSectionEventArgs e) {
			//重新调整 panDesign 的显示高度
			int height = _SectionList.GetDesignHeight();
			int paperWidth = _DataObj.IsLandscape?_DataObj.PaperSize.Height:_DataObj.PaperSize.Width ;
			int width = paperWidth- _DataObj.Margins.Left - _DataObj.Margins.Right ;
			panDesign.Size = new Size(width ,height); 
			//panDesign.ResumeLayout(false);
		}

		private void _UndoMgr_UndoMgrChanged(object sender, EventArgs e) {
			onUndoMgrCHanged(null);
		}
	}

	public class XCMenuItem : System.Windows.Forms.MenuItem{
		private static IList _Images;	
		private object _Tag;
		private int _ImageIndex;

		public XCMenuItem(){
			base.OwnerDraw = true;
			_ImageIndex = -1;
		}
		public static IList Images{
			get{
				return _Images;
			}
			set{
				_Images = value;
			}
		}
		public int ImageIndex{
			get{
				return _ImageIndex;
			}
			set{
				_ImageIndex = value;
			}
		}
		public object Tag{
			get{
				return _Tag;
			}
			set{
				_Tag = value;
			}
		}
		protected override void OnMeasureItem(MeasureItemEventArgs e) {
			if(string.Compare(this.Text , "-",true)==0)
				e.ItemHeight = Font.Height /2;
			else
				e.ItemHeight = Font.Height + 6;
			e.ItemWidth = 100;
			base.OnMeasureItem (e);
		}

		protected override void OnDrawItem(DrawItemEventArgs e) {
			Rectangle menuTextRect = new Rectangle(e.Bounds.X + 24, e.Bounds.Y,  e.Bounds.Width , e.Bounds.Height);
			if(string.Compare(this.Text , "-",true)==0){
				e.Graphics.DrawLine(new Pen(SystemBrushes.ControlText),e.Bounds.X + 24,e.Bounds.Y,e.Bounds.X + 24 +  e.Bounds.Width , e.Bounds.Y );
				return;
			}
			bool selected = (e.State & DrawItemState.Selected) > 0;
			if(selected) {
				e.DrawBackground();
			} else {
				e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds);
			}
			if(_ImageIndex > -1){
				Rectangle rectImage = new Rectangle(e.Bounds.X, e.Bounds.Y,16,16);
				e.Graphics.DrawImage((Image)Images[_ImageIndex],rectImage);
				//e.Graphics.DrawRectangle(new Pen(SystemBrushes.ControlText), rectImage);
			}
			StringFormat sf = new StringFormat();
			sf.LineAlignment = StringAlignment.Center;
			Color strColor = e.ForeColor;
			if(!this.Enabled){
				strColor = SystemColors.Control;  
			}
			e.Graphics.DrawString(((MenuItem)this).Text,Font, new SolidBrush(strColor), menuTextRect, sf);

			base.OnDrawItem (e);
		}

		
		private Font Font { get { return System.Windows.Forms.Control.DefaultFont; }
		}

	}
}
