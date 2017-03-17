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

using DIYReport.ReportModel ;
namespace DIYReport.UserDIY
{
	/// <summary>
	/// DesignSection 报表设计的Section。
	/// </summary>
	[ToolboxItem(false)]
	public class DesignSection : System.Windows.Forms.UserControl {

		#region 内部自动生成代码...

		private System.Windows.Forms.Panel panBottomMove;
		private System.Windows.Forms.PictureBox picMain;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
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

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent() {
			this.panBottomMove = new System.Windows.Forms.Panel();
			this.picMain = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// panBottomMove
			// 
			this.panBottomMove.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.panBottomMove.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.panBottomMove.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panBottomMove.Location = new System.Drawing.Point(0, 167);
			this.panBottomMove.Name = "panBottomMove";
			this.panBottomMove.Size = new System.Drawing.Size(424, 1);
			this.panBottomMove.TabIndex = 2;
			this.panBottomMove.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panBottomMove_MouseUp);
			this.panBottomMove.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panBottomMove_MouseMove);
			this.panBottomMove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panBottomMove_MouseDown);
			// 
			// picMain
			// 
			this.picMain.BackColor = System.Drawing.Color.White;
			this.picMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.picMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picMain.Location = new System.Drawing.Point(0, 0);
			this.picMain.Name = "picMain";
			this.picMain.Size = new System.Drawing.Size(424, 167);
			this.picMain.TabIndex = 3;
			this.picMain.TabStop = false;
			this.picMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picMain_MouseUp);
			this.picMain.DoubleClick += new System.EventHandler(this.picMain_DoubleClick);
			this.picMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMain_MouseMove);
			this.picMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMain_MouseDown);
			// 
			// DesignSection
			// 
			this.Controls.Add(this.picMain);
			this.Controls.Add(this.panBottomMove);
			this.Name = "DesignSection";
			this.Size = new System.Drawing.Size(424, 168);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DesignSection_KeyDown);
			this.ResumeLayout(false);

		}
		#endregion

		#endregion 内部自动生成代码...

		#region 变量定义...
		private DIYReport.SectionType _SectionType;
		private DIYReport.UserDIY.SectionCaption _CaptionCtl; 

		private DesignControlList _DesignControls;
		private DesignSectionList _SectionList;
 
//		private int INI_SECTION_HEIGHT = 108;
//		private int INI_SECTION_WIDTH = 580;
		//操作的动作
		private bool _IsActive = false;
		private bool _IsDisplay = true;
		private bool _IsDispTopSection = false;
		private bool _IsDispBottomSection = false;

		private const int MOVE_PAN_HEIGHT = 1;

		private DIYReport.ReportModel.RptSection _DataObj; 
		#endregion 变量定义...

		#region 构造函数...
		public DesignSection() : this(null) {
		}
	
		public DesignSection( DIYReport.ReportModel.RptSection pDataObj) {
			InitializeComponent();
			picMain.Paint +=new PaintEventHandler(picMain_Paint);
			this.Width = pDataObj.Width ;
			this.Height = pDataObj.Height;
			_DataObj = pDataObj;

			updateByDataObj();

			_DataObj.AfterValueChanged +=new RptEventHandler(_DataObj_AfterValueChanged);
	
			_DesignControls = new DesignControlList(this);
//			if(_SectionType == DIYReport.SectionType.Detail){
//				//在刚开始设计的时候只有Detail Section 是默认显示的
//				_IsDisplay = true;
//				panBottomMove.Visible = true;
//				//panTopMove.Visible = false; 
//			}
			_IsDisplay = pDataObj.Visibled;
			panBottomMove.Visible = _IsDisplay;

			pDataObj.HasCreateViewDesign = true;
			
		}

		#endregion 构造函数...
		
		#region 用户设计的鼠标事件...
		private void picMain_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			DesignEnviroment.CurrentRptObj = _DataObj ;
			DesignEnviroment.UICmidExecutor.ExecCommand(UICommands.SetObjProperty) ; 

			_SectionList.SetActiveSection(this);
			isdown = true;
			picMain.Capture = true;
			p1 = picMain.PointToScreen( new Point(  e.X ,  e.Y) );
			if(DesignEnviroment.PressCtrlKey!=true && DesignEnviroment.PressShiftKey!=true){
				_DesignControls.SetAllNotSelected(); 
			}
		}

		private void picMain_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(isdown){
				Point p = picMain.PointToScreen( new Point( e.X  , e.Y))  ;
				if(p2 != Point.Empty) {
					GDIHelper.DrawMouseSelected(p1,p2);
				}
				p2 = p;
				GDIHelper.DrawMouseSelected(p1,p2);
			}
		}

		private void picMain_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(isdown){
				p2 =   picMain.PointToScreen( new Point( e.X  , e.Y))   ;
				if(p1.X!= p2.X || p1.Y != p2.Y ){
					GDIHelper.DrawMouseSelected(p1,p2);
				}
				if(DesignEnviroment.DrawControlType != DIYReport.ReportModel.RptObjType.None){
					_DesignControls.CreateControl(DesignEnviroment.DrawControlType, p1,p2);
				}
				else{
					_DesignControls.SelectCtlByMouseRect(p1,p2);
				}
				_DesignControls.ShowFocusHandle(true); 

				DesignEnviroment.UICmidExecutor.ExecCommand(UICommands.SetObjProperty);     
				
			}
			isdown=false;
			p2 = Point.Empty;
			picMain.Capture = false;
		//	picMain.Invalidate(); 
		}
		#endregion 用户设计的鼠标事件...

		#region Public 方法...

 
		/// <summary>
		/// 通过报表的Data section 设置Design Section 的数据信息
		/// </summary>
		/// <param name="pDataSection"></param>
		public void SetDataSection(DIYReport.ReportModel.RptSection pDataSection){
			this.IsDisplay = pDataSection.Visibled ;
			//this.Location = new Point(0 ,  SectionCaption.CAPTION_HEIGHT + pDataSection.Location.Y) ;
			this.Height = pDataSection.Height ;
		}
		public DIYReport.ReportModel.RptSection GetDataSection(){
			return null;
		}
		#endregion Public 方法...

		private bool isdown=false;
		private Point p1 = Point.Empty;
		private Point p2 = Point.Empty;

		private int maxCtlBottom = 0;
		private void panBottomMove_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(e.Button == MouseButtons.Left){  
				isdown=true;
				panBottomMove.Capture = true;
				p1 = this.PointToScreen( new Point(this.Left ,this.Height  + e.Y)) ; 
			}
			_SectionList.SetActiveSection(this);
			maxCtlBottom = _DataObj.RptObjList.GetMaxCtlBottom()  ;
			 
		}

		private void panBottomMove_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(isdown) {
				int top = this.Height   + e.Y;
				top = top < maxCtlBottom + MOVE_PAN_HEIGHT?maxCtlBottom + MOVE_PAN_HEIGHT:top;
				 

				Point p = this.PointToScreen( new Point(this.Left ,top)) ; 
				if(p2 != Point.Empty) {
					//dList.DEControls.DrawReversibleFrame(p1,p2);
					ControlPaint.DrawReversibleFrame(new Rectangle(p1.X ,p2.Y ,this.Width ,1) ,Color.Black,FrameStyle.Thick );
				}
				p2 = p;
				ControlPaint.DrawReversibleFrame(new Rectangle(p1.X ,p2.Y ,this.Width,1) ,Color.Black,FrameStyle.Thick );
			}
		}

		private void panBottomMove_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			int top = this.Height   + e.Y;
			top = top < maxCtlBottom + MOVE_PAN_HEIGHT?maxCtlBottom + MOVE_PAN_HEIGHT:top;
		 

			p2 = this.PointToScreen( new Point(this.Left , top)) ; 
			if(e.Button == MouseButtons.Left){ 
				if(p1.X!= p2.X || p1.Y != p2.Y ){

					ControlPaint.DrawReversibleFrame(new Rectangle(p1.X,p2.Y ,this.Width,1) ,Color.Black,FrameStyle.Thick  );
				}
			}
			isdown=false;
			panBottomMove.Capture = false;
		
			Point  r1 = this.PointToClient(p1), r2 = this.PointToClient(p2); 
			int sepHeight = r2.Y - r1.Y ;
			this.DataObj.Height = sepHeight + this.Height - MOVE_PAN_HEIGHT ;
			this.Parent.Height =  this.Parent.Height + sepHeight;

			_SectionList.RefreshDesignLayout(); 
			p2 = Point.Empty;
		}

		#region Public 属性...
		public DIYReport.ReportModel.RptSection DataObj{
			get{
				return _DataObj ;
			}
		}
		public DesignControlList DesignControls{
			get{
				return _DesignControls;
			}
		}
		public DIYReport.UserDIY.SectionCaption CaptionCtl{
			get{
				return _CaptionCtl ;
			}
			set{
				_CaptionCtl = value;
				_CaptionCtl.Caption = this.DataObj.Name ;// DIYReport.PublicFun.GetTextBySectionType( _SectionType);  
				_CaptionCtl.Section = this;
				_CaptionCtl.Size = new Size(this.Width,_CaptionCtl.Height);
			}
		}
		public DesignSectionList SectionList{
			get{
				return _SectionList;
			}
			set{
				_SectionList = value;
			}
		}
		public bool IsActive{
			get{
				return _IsActive;
			}
			set{
				_IsActive = value;
				this.CaptionCtl.SetActive( _IsActive );
			}
		}
		public bool IsDisplay{
			get{
				return _IsDisplay;
			}
			set{
				_IsDisplay = value;
			}
		}
		public bool IsDispBottomSection{
			get{
				return _IsDispBottomSection;
			}
			set{
				_IsDispBottomSection = value;
			}
		}
		public bool IsDispTopSection{
			get{
				return _IsDispTopSection;
			}
			set{
				_IsDispTopSection = value;
				//panTopMove.Visible =  !_IsDispTopSection;
			}
		}
		public DIYReport.SectionType SectionType{
			get{
				return _SectionType;
			}
			set{
				_SectionType = value;
				if(this.CaptionCtl!=null){
					//this.CaptionCtl.Caption  = DIYReport.PublicFun.GetTextBySectionType( _SectionType);
				}
			}
		}
		#endregion Public 属性...

		 
		public  IList DeControls{
			get{
				return this.picMain.Controls ;
			}
		}

		#region 内部处理函数...
 
		#endregion 内部处理函数...

		private void panCaption_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			_SectionList.SetActiveSection(this); 
		}

		private void labCaption_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			_SectionList.SetActiveSection(this);
		}

		private void _DataObj_AfterValueChanged(object sender, RptEventArgs e) {
			updateByDataObj();
		}
		private void updateByDataObj(){
			this.Height = _DataObj.Height + MOVE_PAN_HEIGHT ;
			this.Width = _DataObj.Width ;
			if(this.CaptionCtl!=null){
				this.CaptionCtl.Width = _DataObj.Width ;
			}
			this.SectionType = _DataObj.SectionType ;
			this.IsDisplay = _DataObj.Visibled ;
			if(_SectionList!=null){
				_SectionList.RefreshDesignLayout(); 
			}
			picMain.Invalidate();
		}

		private void picMain_DoubleClick(object sender, System.EventArgs e) {
			DesignEnviroment.ShowPropertyForm(this.ParentForm,true); 
		}

		private void DesignSection_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
		
		}

		private void picMain_Paint(object sender, PaintEventArgs e) {
            if (this.DataObj.BackgroundImage != null)
                drawBackgroundImage(e);
            else
                drawGridBackground(e);
		}

		//画背景
        private void drawGridBackground(PaintEventArgs e) {
            int width = this.Width;
            int height = this.Height;
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    e.Graphics.FillRectangle(Brushes.Black, i, j, 1, 1);
                    j += 8;
                }
                i += 8;
            }
            if (_DataObj.Report != null && _DataObj.Report.ReportDataWidth > 0) {
                Pen p = new Pen(Brushes.Red, 1);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                e.Graphics.DrawLine(p, _DataObj.Report.ReportDataWidth, 0, _DataObj.Report.ReportDataWidth, this.Height);
            }
        }
        private void drawBackgroundImage(PaintEventArgs e) {
            e.Graphics.DrawImageUnscaled(this.DataObj.BackgroundImage, this.DisplayRectangle);
        }
	}
}
