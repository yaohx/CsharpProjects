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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace DIYReport.UserDIY {
 
	/// <summary>
	/// ReportDesign 报表设计操作界面。
	/// </summary>
	[ToolboxItem(false)]
	public class ReportDesign : System.Windows.Forms.UserControl,DIYReport.Interface.IDesignPanel { 
		#region 内部自动生成代码...

		private System.Windows.Forms.Panel panMain;
		private DIYReport.UserDIY.DesignRuler rulerLeft;
		private DIYReport.UserDIY.DesignRuler rulerTop;
		private System.Windows.Forms.Panel panDesign;
		private System.Windows.Forms.VScrollBar vscrBar;
		private System.Windows.Forms.HScrollBar hscrBar;
		private System.Windows.Forms.Panel panRightBottom;
		private System.Windows.Forms.Panel panLeftTop;
		private System.Windows.Forms.ToolBarButton toolBarButton6;
		private System.Windows.Forms.ToolBarButton tlbButProperty;
		private System.Windows.Forms.ToolBarButton tlbButFormatLeft;
		private System.Windows.Forms.ToolBarButton tlbButFormatRight;
		private System.Windows.Forms.ToolBarButton tlbButFormatTop;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton tlbButFormatBottom;
		private System.Windows.Forms.ToolBarButton tlbButFormatWidth;
		private System.Windows.Forms.ToolBarButton tlbButFormatHeight;
		private System.Windows.Forms.ToolBarButton tlbButSelect;
		private System.Windows.Forms.ToolBarButton tlbButCreateLable;
		private System.Windows.Forms.ToolBarButton tlbButCreateText;
		private System.Windows.Forms.ToolBar tBarTools;
		private System.Windows.Forms.ToolBarButton tlbButCreatePic;
		private System.Windows.Forms.ToolBarButton tblButPageSet;
		private System.Windows.Forms.ToolBarButton tlbButPreview;
		private System.Windows.Forms.ToolBarButton tlbButPrint;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBar tlbTopBar;
		private System.Windows.Forms.ToolBarButton tlbButAddNew;
		private System.Windows.Forms.ToolBarButton tlbButOpen;
		private System.Windows.Forms.ToolBarButton tlbButSave;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton tlbButLine;
		private System.Windows.Forms.ToolBarButton tlbButRect;
		private System.Windows.Forms.ToolBarButton tlbButDeleteCtl;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton tlbButArrow;
		private System.Windows.Forms.ToolBarButton tlbButDockLeft;
		private System.Windows.Forms.ToolBarButton tlbButDockTop;
		private System.Windows.Forms.ToolBarButton toolBarButton8;
		private System.Windows.Forms.ToolBarButton tlbButRedo;
		private System.Windows.Forms.ToolBarButton tlbButUndo;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ToolBarButton tlbGroupAndSort;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ReportDesign));
			this.tlbTopBar = new System.Windows.Forms.ToolBar();
			this.tlbButAddNew = new System.Windows.Forms.ToolBarButton();
			this.tlbButOpen = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
			this.tlbButSave = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
			this.tblButPageSet = new System.Windows.Forms.ToolBarButton();
			this.tlbButPreview = new System.Windows.Forms.ToolBarButton();
			this.tlbButPrint = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
			this.tlbButDeleteCtl = new System.Windows.Forms.ToolBarButton();
			this.tlbButUndo = new System.Windows.Forms.ToolBarButton();
			this.tlbButRedo = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.tlbGroupAndSort = new System.Windows.Forms.ToolBarButton();
			this.tlbButProperty = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
			this.tlbButFormatLeft = new System.Windows.Forms.ToolBarButton();
			this.tlbButFormatRight = new System.Windows.Forms.ToolBarButton();
			this.tlbButFormatTop = new System.Windows.Forms.ToolBarButton();
			this.tlbButFormatBottom = new System.Windows.Forms.ToolBarButton();
			this.tlbButFormatWidth = new System.Windows.Forms.ToolBarButton();
			this.tlbButFormatHeight = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.tlbButDockLeft = new System.Windows.Forms.ToolBarButton();
			this.tlbButDockTop = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton8 = new System.Windows.Forms.ToolBarButton();
			this.tlbButArrow = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.tBarTools = new System.Windows.Forms.ToolBar();
			this.tlbButSelect = new System.Windows.Forms.ToolBarButton();
			this.tlbButCreateLable = new System.Windows.Forms.ToolBarButton();
			this.tlbButCreateText = new System.Windows.Forms.ToolBarButton();
			this.tlbButCreatePic = new System.Windows.Forms.ToolBarButton();
			this.tlbButLine = new System.Windows.Forms.ToolBarButton();
			this.tlbButRect = new System.Windows.Forms.ToolBarButton();
			this.panMain = new System.Windows.Forms.Panel();
			this.panRightBottom = new System.Windows.Forms.Panel();
			this.panDesign = new System.Windows.Forms.Panel();
			this.vscrBar = new System.Windows.Forms.VScrollBar();
			this.hscrBar = new System.Windows.Forms.HScrollBar();
			this.rulerLeft = new DIYReport.UserDIY.DesignRuler();
			this.rulerTop = new DIYReport.UserDIY.DesignRuler();
			this.panLeftTop = new System.Windows.Forms.Panel();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlbTopBar
			// 
			this.tlbTopBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						 this.tlbButAddNew,
																						 this.tlbButOpen,
																						 this.toolBarButton3,
																						 this.tlbButSave,
																						 this.toolBarButton5,
																						 this.tblButPageSet,
																						 this.tlbButPreview,
																						 this.tlbButPrint,
																						 this.toolBarButton4,
																						 this.tlbButDeleteCtl,
																						 this.tlbButUndo,
																						 this.tlbButRedo,
																						 this.toolBarButton2,
																						 this.tlbGroupAndSort,
																						 this.tlbButProperty,
																						 this.toolBarButton6,
																						 this.tlbButFormatLeft,
																						 this.tlbButFormatRight,
																						 this.tlbButFormatTop,
																						 this.tlbButFormatBottom,
																						 this.tlbButFormatWidth,
																						 this.tlbButFormatHeight,
																						 this.toolBarButton1,
																						 this.tlbButDockLeft,
																						 this.tlbButDockTop,
																						 this.toolBarButton8,
																						 this.tlbButArrow});
			this.tlbTopBar.DropDownArrows = true;
			this.tlbTopBar.ImageList = this.imageList1;
			this.tlbTopBar.Location = new System.Drawing.Point(0, 0);
			this.tlbTopBar.Name = "tlbTopBar";
			this.tlbTopBar.ShowToolTips = true;
			this.tlbTopBar.Size = new System.Drawing.Size(552, 28);
			this.tlbTopBar.TabIndex = 0;
			this.tlbTopBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tlbTopBar_ButtonClick);
			// 
			// tlbButAddNew
			// 
			this.tlbButAddNew.ImageIndex = 15;
			this.tlbButAddNew.Tag = "23";
			this.tlbButAddNew.ToolTipText = "导出报表摸板";
			// 
			// tlbButOpen
			// 
			this.tlbButOpen.ImageIndex = 16;
			this.tlbButOpen.Tag = "22";
			this.tlbButOpen.ToolTipText = "导入报表";
			// 
			// toolBarButton3
			// 
			this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tlbButSave
			// 
			this.tlbButSave.ImageIndex = 17;
			this.tlbButSave.Tag = "21";
			this.tlbButSave.ToolTipText = "报表保存";
			// 
			// toolBarButton5
			// 
			this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tblButPageSet
			// 
			this.tblButPageSet.ImageIndex = 12;
			this.tblButPageSet.Tag = "20";
			this.tblButPageSet.ToolTipText = "页面设置";
			// 
			// tlbButPreview
			// 
			this.tlbButPreview.ImageIndex = 10;
			this.tlbButPreview.Tag = "19";
			this.tlbButPreview.ToolTipText = "打印预览";
			// 
			// tlbButPrint
			// 
			this.tlbButPrint.ImageIndex = 11;
			this.tlbButPrint.Tag = "18";
			this.tlbButPrint.ToolTipText = "打印";
			// 
			// toolBarButton4
			// 
			this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tlbButDeleteCtl
			// 
			this.tlbButDeleteCtl.ImageIndex = 19;
			this.tlbButDeleteCtl.Tag = "17";
			this.tlbButDeleteCtl.ToolTipText = "删除当前选择的控件";
			// 
			// tlbButUndo
			// 
			this.tlbButUndo.Enabled = false;
			this.tlbButUndo.ImageIndex = 23;
			this.tlbButUndo.Tag = "24";
			// 
			// tlbButRedo
			// 
			this.tlbButRedo.Enabled = false;
			this.tlbButRedo.ImageIndex = 24;
			this.tlbButRedo.Tag = "25";
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tlbGroupAndSort
			// 
			this.tlbGroupAndSort.ImageIndex = 25;
			this.tlbGroupAndSort.Tag = "26";
			// 
			// tlbButProperty
			// 
			this.tlbButProperty.ImageIndex = 0;
			this.tlbButProperty.Tag = "16";
			this.tlbButProperty.ToolTipText = "属性";
			// 
			// toolBarButton6
			// 
			this.toolBarButton6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tlbButFormatLeft
			// 
			this.tlbButFormatLeft.ImageIndex = 1;
			this.tlbButFormatLeft.Tag = "0";
			this.tlbButFormatLeft.ToolTipText = "左对齐";
			// 
			// tlbButFormatRight
			// 
			this.tlbButFormatRight.ImageIndex = 2;
			this.tlbButFormatRight.Tag = "2";
			this.tlbButFormatRight.ToolTipText = "右对齐";
			// 
			// tlbButFormatTop
			// 
			this.tlbButFormatTop.ImageIndex = 3;
			this.tlbButFormatTop.Tag = "1";
			this.tlbButFormatTop.ToolTipText = "顶端对齐";
			// 
			// tlbButFormatBottom
			// 
			this.tlbButFormatBottom.ImageIndex = 4;
			this.tlbButFormatBottom.Tag = "3";
			this.tlbButFormatBottom.ToolTipText = "底端对齐";
			// 
			// tlbButFormatWidth
			// 
			this.tlbButFormatWidth.ImageIndex = 6;
			this.tlbButFormatWidth.Tag = "4";
			this.tlbButFormatWidth.ToolTipText = "相同宽度";
			// 
			// tlbButFormatHeight
			// 
			this.tlbButFormatHeight.ImageIndex = 5;
			this.tlbButFormatHeight.Tag = "5";
			this.tlbButFormatHeight.ToolTipText = "相同高度";
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tlbButDockLeft
			// 
			this.tlbButDockLeft.ImageIndex = 21;
			this.tlbButDockLeft.Tag = "6";
			this.tlbButDockLeft.ToolTipText = "水平紧靠";
			// 
			// tlbButDockTop
			// 
			this.tlbButDockTop.ImageIndex = 22;
			this.tlbButDockTop.Tag = "7";
			this.tlbButDockTop.ToolTipText = "垂直紧靠";
			// 
			// toolBarButton8
			// 
			this.toolBarButton8.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tlbButArrow
			// 
			this.tlbButArrow.ImageIndex = 20;
			this.tlbButArrow.Tag = "8";
			this.tlbButArrow.ToolTipText = "移动控件";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tBarTools
			// 
			this.tBarTools.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						 this.tlbButSelect,
																						 this.tlbButCreateLable,
																						 this.tlbButCreateText,
																						 this.tlbButCreatePic,
																						 this.tlbButLine,
																						 this.tlbButRect});
			this.tBarTools.Dock = System.Windows.Forms.DockStyle.Left;
			this.tBarTools.DropDownArrows = true;
			this.tBarTools.ImageList = this.imageList1;
			this.tBarTools.Location = new System.Drawing.Point(0, 28);
			this.tBarTools.Name = "tBarTools";
			this.tBarTools.ShowToolTips = true;
			this.tBarTools.Size = new System.Drawing.Size(24, 332);
			this.tBarTools.TabIndex = 1;
			this.tBarTools.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.tBarTools.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tBarTools_ButtonClick);
			// 
			// tlbButSelect
			// 
			this.tlbButSelect.ImageIndex = 7;
			this.tlbButSelect.Pushed = true;
			this.tlbButSelect.Tag = "0";
			this.tlbButSelect.ToolTipText = "选择";
			// 
			// tlbButCreateLable
			// 
			this.tlbButCreateLable.ImageIndex = 8;
			this.tlbButCreateLable.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.tlbButCreateLable.Tag = "1";
			this.tlbButCreateLable.ToolTipText = "文本";
			// 
			// tlbButCreateText
			// 
			this.tlbButCreateText.ImageIndex = 9;
			this.tlbButCreateText.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.tlbButCreateText.Tag = "2";
			this.tlbButCreateText.ToolTipText = "数据框";
			// 
			// tlbButCreatePic
			// 
			this.tlbButCreatePic.ImageIndex = 13;
			this.tlbButCreatePic.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.tlbButCreatePic.Tag = "5";
			this.tlbButCreatePic.ToolTipText = "图像";
			// 
			// tlbButLine
			// 
			this.tlbButLine.ImageIndex = 14;
			this.tlbButLine.Tag = "3";
			this.tlbButLine.ToolTipText = "画线";
			// 
			// tlbButRect
			// 
			this.tlbButRect.ImageIndex = 18;
			this.tlbButRect.Tag = "4";
			this.tlbButRect.ToolTipText = "画边框";
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
			this.panMain.Location = new System.Drawing.Point(24, 28);
			this.panMain.Name = "panMain";
			this.panMain.Size = new System.Drawing.Size(528, 332);
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
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 500;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// ReportDesign
			// 
			this.Controls.Add(this.panMain);
			this.Controls.Add(this.tBarTools);
			this.Controls.Add(this.tlbTopBar);
			this.Name = "ReportDesign";
			this.Size = new System.Drawing.Size(552, 360);
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

		#region 构造函数...
		public ReportDesign() {
			InitializeComponent();
			//panMain.Visible = false;
			_UndoMgr = new DIYReport.UndoManager.UndoMgr();
			
		}
		#endregion 构造函数...
		
		#region public 方法...
		/// <summary>
		/// 创建一个新的报表
		/// </summary>
		/// <returns></returns>
		public DIYReport.ReportModel.RptReport CreateNewReport(){
			_DataObj = _ReportIO.NewReport(); 
 
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
			if(pReport!=null ){				 
				_DataObj = pReport;
				//判断是否有分组的Section，初始化分组设计的字段，然后当前设计的字段引用到分组的Section 中
				iniGroupFieldOnOpen(_DataObj);

				iniNewReportDesign();
			}
			DesignEnviroment.DesignHasChanged = false; 
			return _DataObj;

		}
		/// <summary>
		/// 删除当前选中的报表控件
		/// </summary>
		public void DeleteSelectedControls(){
			DesignSection section  = _SectionList.GetActiveSection();
			if(section!=null){
				section.DesignControls.RemoveSelectedControl();
			}
 
		}
		#region 剪贴、复制、粘贴操作处理相关...
		/// <summary>
		/// 剪贴操作
		/// </summary>
		public void Cut(){
			Copy();
			DeleteSelectedControls();
		}
		/// <summary>
		/// 复制操作
		/// </summary>
		public void Copy(){
			DesignSection section  = _SectionList.GetActiveSection();
			if(section==null)
				return;

			IList dataLst = section.DesignControls.GetSelectedCtlsData();
			DIYReport.UserDIY.ClipboardEx.CopyToClipBoard(dataLst);
		}
		public void Past(){
			DesignSection section  = _SectionList.GetActiveSection();
			if(section==null)
				return;
			IList dataLst = DIYReport.UserDIY.ClipboardEx.GetFromClipBoard(); 
			section.DesignControls.Add(dataLst); 
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
			rulerTop.Left = panLeftTop.Width  -e.NewValue ; 
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
			rulerLeft.SectionList = _SectionList.GetDataSectionList();  

			reCalculateScrollValue();
		}

		private void tBarTools_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			foreach(ToolBarButton but in tBarTools.Buttons){
				but.Pushed = false;
			}
			e.Button.Pushed = true;
			DesignEnviroment.DrawControlType = (DIYReport.ReportModel.RptObjType)int.Parse(e.Button.Tag.ToString());
			DesignEnviroment.IsCreateControl = DesignEnviroment.DrawControlType!= DIYReport.ReportModel.RptObjType.None;
		}

		private void tlbTopBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			//e.Button.Tag 的0-5为存储格式控件操作样式的 
			this.Cursor = Cursors.WaitCursor ;
			if(e.Button.Tag!=null){
				int index = int.Parse(e.Button.Tag.ToString());  
				if(index<6){
					DesignSection section = _SectionList.GetActiveSection();  
					section.DesignControls.FormatCtl((DIYReport.UserDIY.FormatCtlType)index);  
				}
				switch(index){
					case 6://向左边靠齐
						_SectionList.GetActiveSection().DesignControls.DockToLeft();
						break;
					case 7://向右边靠齐
						_SectionList.GetActiveSection().DesignControls.DockToTop() ;
						break;
					case 8://显示方向控制盘
						FrmArrowOperate.ShowArrowForm( _SectionList,this.ParentForm );  
						break;
					case 16:
						//显示属性窗口
						DesignEnviroment.ShowPropertyForm(this.ParentForm,true); 
						break;
					case 17:
						//删除控件
						DeleteSelectedControls();
						break;
					case 18://打印
						using(DIYReport.Print.SwPrintView print = new DIYReport.Print.SwPrintView( DIYReport.UserDIY.DesignEnviroment.DataSource,_DataObj)){
							print.Printer(); 
						}
						break;
					case 19://打印预览
						using(DIYReport.Print.SwPrintView printView = new DIYReport.Print.SwPrintView( DIYReport.UserDIY.DesignEnviroment.DataSource,_DataObj)){
							printView.ShowPreview();
						}
						break;
					case 20://打印页面设置
						DIYReport.Print.RptPageSetting.ShowPageSetupDialog(_DataObj);   
						break;
					case 21://保存报表
						_ReportIO.SaveReport(_DataObj,null); 
						DIYReport.UserDIY.DesignEnviroment.DesignHasChanged = false;
						break;
					case 22://打开报表 
						DIYReport.ReportModel.RptReport report =  _ReportIO.Open(); 
						if(report!=null){
							OpenReport( report );
						}
						break;
					case 23://新增报表 
						//CreateNewReport();
						//MessageBox.Show("当前不支持从这里新建一张报表摸板.","操作提示"); 
						//暂时把它修改为打印摸板的导出功能。
						_ReportIO.Save(_DataObj);
						break;
					case 24 ://Undo
						_UndoMgr.Undo();
						break;
					case 25 ://Redo 
						_UndoMgr.Redo();
						break;
					case 26 : //显示分组和排序
						IList fieldsList = DIYReport.UserDIY.DesignEnviroment.CurrentReport.DesignField; 
						
						DIYReport.GroupAndSort.frmSortAndGroup frm = new DIYReport.GroupAndSort.frmSortAndGroup(fieldsList); 
						frm.AfterSortAndGroup +=new DIYReport.GroupAndSort.SortAndGroupEventHandler(frm_AfterSortAndGroup);
						frm.ShowDialog(); 
						break;
					default:
						break;
				}
			}
			this.Cursor = Cursors.Arrow;
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
		//初始化分组设计的字段，然后当前设计的字段引用到分组的Section 中
		private void iniGroupFieldOnOpen(DIYReport.ReportModel.RptReport pReport){
			//判断是否有分组的Section
			DIYReport.ReportModel.RptSectionList sectionList = pReport.SectionList;
			foreach(DIYReport.ReportModel.RptSection section in sectionList){
				if(section.SectionType == SectionType.GroupHead || section.SectionType == SectionType.GroupFooter){
					DIYReport.GroupAndSort.RptFieldInfo groupField =  section.GroupField;
					if(groupField == null){Debug.Assert(false,"在获取报表的分组Section 时，得到字段的信息为空。"); }
					IList fieldList = DIYReport.UserDIY.DesignEnviroment.CurrentReport.DesignField;
					if(fieldList == null){Debug.Assert(false,"没有初始化设计的字段。"); }
					int count = fieldList.Count ;
					for(int i =0; i <count;i++){
						DIYReport.GroupAndSort.RptFieldInfo designField = fieldList[i] as DIYReport.GroupAndSort.RptFieldInfo;
						if(designField.FieldName.Trim().ToUpper() == groupField.FieldName.Trim().ToUpper()){
							designField.IsGroup = true;
							designField.IsAscending = groupField.IsAscending ;
							designField.OrderIndex = groupField.OrderIndex;
							designField.SetSort = groupField.SetSort;
							designField.DivideName = groupField.DivideName;
							section.GroupField = designField;
							break;
						}
					}
				}
			}
		}
		#endregion 内部函数处理...

		#region DataObj_AfterValue...

		private void _DataObj_AfterValueChanged(object sender, DIYReport.ReportModel.RptEventArgs e) {
			//_DataObj.SectionList.
			int paperWidth = _DataObj.IsLandscape?_DataObj.PaperSize.Height:_DataObj.PaperSize.Width ;
			int width = paperWidth - _DataObj.Margins.Left - _DataObj.Margins.Right ;
			panDesign.Size = new Size(width ,panDesign.Height); 
			_DataObj.SectionList.ReSizeByPaperSize(width); 
			reCalculateScrollValue();
		}

		#endregion DataObj_AfterValue...

		#region 接受Parent Form 的事件消息...

		private void ParentForm_Closed(object sender, EventArgs e) {
			//清理所有正在使用的资源...
			this.ParentForm.Closed -=new EventHandler(ParentForm_Closed); 
			this.ParentForm.KeyDown -=new KeyEventHandler(ParentForm_KeyDown);
			this.ParentForm.KeyUp -=new KeyEventHandler(ParentForm_KeyUp);
			disposeRptImage();
		}
		private void disposeRptImage(){
			foreach(DIYReport.UserDIY.DesignSection section in this.SectionList){
				foreach(DIYReport.UserDIY.DesignControl ctl in section.DesignControls){
					DIYReport.Interface.IRptSingleObj   obj = ctl.DataObj ;
					if(obj.Type == DIYReport.ReportModel.RptObjType.Image){
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
		}
		private void ParentForm_KeyDown(object sender, KeyEventArgs e) {
			DIYReport.UserDIY.DesignEnviroment.PressShiftKey = e.KeyValue == 16;
			DIYReport.UserDIY.DesignEnviroment.PressCtrlKey  = e.KeyValue == 17;
			//判断用户是否按下 Delete 键
			if(e.KeyValue ==46){
				DeleteSelectedControls();
			}
			//处理方向键
            bool isSiae = DIYReport.UserDIY.DesignEnviroment.PressCtrlKey;
            _SectionList.GetActiveSection().DesignControls.ProcessArrowKeyDown(e.KeyValue, isSiae);  
		}

		private void ParentForm_KeyUp(object sender, KeyEventArgs e) {
			DIYReport.UserDIY.DesignEnviroment.PressShiftKey = false;
			DIYReport.UserDIY.DesignEnviroment.PressCtrlKey  = false;
		}

		#endregion 接受Parent Form 的事件消息...
		
		private void timer1_Tick(object sender, System.EventArgs e) {
			tlbButRedo.Enabled = _UndoMgr.CanRedo ;
			tlbButUndo.Enabled = _UndoMgr.CanUndo ;
		}
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
	}
}
