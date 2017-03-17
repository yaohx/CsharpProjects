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
using System.Diagnostics ;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DIYReport.ReportModel.RptObj ; 
using DIYReport.ReportModel;
namespace DIYReport.UserDIY
{
	#region 获取窗口的参数信息...

	#endregion 获取窗口的参数信息...
	/// <summary>
	/// DesignControl 用户DIY操作的主要控件。
	/// </summary>
	[ToolboxItem(false)]
	public class DesignControl : System.Windows.Forms.UserControl
	{
		#region 内部自动生成代码...

		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

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

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// DesignControl
			// 
			this.Name = "DesignControl";
			this.Size = new System.Drawing.Size(224, 72);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labBack_MouseUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.DesignControl_Paint);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DesignControl_KeyDown);
			this.DoubleClick += new System.EventHandler(this.DesignControl_DoubleClick);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labBack_MouseMove);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labBack_MouseDown);

		}
		#endregion

		#endregion 内部自动生成代码...
		
		#region 变量定义...
		private static int FUDGER = GDIHelper.FUDGER ;
		private FocusHandleList _FocusList;


		private bool _IsSelected;
		private bool _IsMainSelected;

		private DIYReport.UserDIY.DesignControlList _DeControlList;

		private DIYReport.Interface.IRptSingleObj  _DataObj;  
		#endregion 变量定义...

		#region 构造函数...
		
		public DesignControl(DIYReport.Interface.IRptSingleObj   pDataObj) {
			_IsSelected = false;
			_DataObj = pDataObj;
			(_DataObj as RptSingleObj).AfterValueChanged +=new DIYReport.ReportModel.RptEventHandler(_DataObj_AfterValueChanged);

			InitializeComponent();
			_FocusList = new FocusHandleList(this);
			updateByDataObj();
		}
		#endregion 构造函数...

		#region 覆盖基类的方法...
		protected override void OnLocationChanged(EventArgs e) {
			if(_FocusList!=null){
				_FocusList.DockFocusToCtl(false);
			}
			base.OnLocationChanged (e);
		}
		protected override void OnSizeChanged(EventArgs e) {
			if(_FocusList!=null){
				_FocusList.DockFocusToCtl(false);
			}
			base.OnSizeChanged (e);
		}
		#endregion 覆盖基类的方法...

		#region Public 属性...
		public DIYReport.Interface.IRptSingleObj  DataObj{
			get{
				return _DataObj;
			}
		}

		public bool IsSelected{
			get{
				return _IsSelected;
			}
			set{
				_IsSelected = value;				
			}
		}
		public bool IsMainSelected{
			get{
				return _IsMainSelected;
			}
			set{
				_IsMainSelected = value;
				_FocusList.SetMainSelected(_IsMainSelected);
			}
		}
		public FocusHandleList FocusList{
			get{
				return _FocusList;
			}
//			set{
//				_FocusList = value;
//			}
		}
		public DIYReport.UserDIY.DesignControlList DeControlList{
			get{
				return _DeControlList;
			}
			set{
				_DeControlList = value;
			}
		}
		#endregion Public 属性...

		#region Public 方法...
		public Rectangle GetOutFocusRect(){
			int SEP = 0;
			Rectangle rect = new Rectangle(this.Left  - SEP,this.Top - SEP ,this.Width + SEP,this.Height + SEP);
			Rectangle focusFrame = new Rectangle(rect.X - FUDGER ,rect.Y -FUDGER  ,rect.Width + 2*FUDGER  ,rect.Height + 2*FUDGER ); 
			return focusFrame;
		}
		/// <summary>
		/// 判断鼠标当前的点是否在该控件上面
		/// </summary>
		/// <param name="pPoint"></param>
		/// <returns></returns>
		public bool MouseIsOver(Point pPoint){
			int X = pPoint.X ,Y = pPoint.Y ;
			return X > this.Left & X <this.Right & Y > this.Top & Y< this.Bottom ;
		}
		/// <summary>
		/// 判断鼠标是否移动在控件所在的焦点上
		/// </summary>
		/// <param name="pPoint"></param>
		/// <returns></returns>
		public bool MouseIsOverFocus(Point pPoint){
			 
			Rectangle rect = GetOutFocusRect();
			bool b = MouseIsOver( pPoint );
			b = !b && rect.Contains(pPoint);
			return b;
		}
		#endregion Public 方法...
		
		private bool isdown=false;
		private bool _isMove;
		private Point p1 = Point.Empty;
		private Point p2 = Point.Empty;

		private void labBack_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			DesignEnviroment.CurrentRptObj = _DataObj ; 
			DesignEnviroment.UICmidExecutor.ExecCommand(UICommands.SetObjProperty) ; 

			DesignSection section = this.DeControlList.Section;
			if(section.IsActive==false){
				section.SectionList.SetOtherSectionAllNotSelected( section);
				section.SectionList.SetActiveSection( section);
			}
			_DeControlList.SetMainSelected(this);
			
			if(e.Button == MouseButtons.Left && e.Button != MouseButtons.Right){  
				isdown=true;
				this.Capture = true;
				p1 =  new Point(this.Left + e.X , this.Top + e.Y) ;
			}
			//判断用户是否按下shift 或者ctrl 键
			if(DesignEnviroment.PressCtrlKey!=true && DesignEnviroment.PressShiftKey!=true && this.IsSelected ==false){ 
				_DeControlList.SetAllNotSelected(); 
			}
			this.IsSelected = true;
			_isMove = false;
		}

		private void labBack_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(isdown && e.Button != MouseButtons.Right) {
				//_FocusList.ShowFocusHandle(false);
				_isMove = true; 
				int x =this.Left +  e.X,y = this.Top + e.Y;
				Point p =  new Point(x,y) ;

				if(p2 != Point.Empty ) {
					_DeControlList.DrawReversibleFrame(p1,p2);
				}
				p2 = p;
				_DeControlList.DrawReversibleFrame(p1,p2);
			}
		}
		private void labBack_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			int x =this.Left +  e.X,y = this.Top + e.Y;
			
			p2 =  new Point(x, y) ;
			 
			if(e.Button == MouseButtons.Left && e.Button != MouseButtons.Right && _isMove){ 
				//if(p1.X!= p2.X || p1.Y != p2.Y ){
				_DeControlList.DrawReversibleFrame(p1,p2);
				_DeControlList.MoveToPoint(p1,p2);
				//}
			}
			isdown=false;
			this.Capture = false;
			p2 = Point.Empty;
			_DeControlList.ShowFocusHandle(true);

            //if(_DataObj.Type!=DIYReport.ReportModel.RptObjType.Line && _DataObj.Type!=DIYReport.ReportModel.RptObjType.Rect){
            //    this.BringToFront();
            //}
			//this.Parent.Invalidate();
			//dList.DEControls.Invalidate(); 
            DesignEnviroment.UICmidExecutor.ExecCommand(UICommands.SetObjProperty); 
		}
		protected override void OnInvalidated(InvalidateEventArgs e) {
			base.OnInvalidated (e);
			//
		}

		#region 内部处理函数...
		 
		//
//		private void drawFocus(){
//			Graphics g = Graphics.FromImage(this.Parent.BackgroundImage); 
//			Rectangle  sc = Screen.GetWorkingArea(this)  ;  
//			g.Clear( Color.LightGray);  
//			foreach(FocusHandle focus in this.FocusList ){
//				g.DrawRectangle(new Pen(Brushes.Black),focus.Rect  );
//			}
//			this.Parent.Invalidate(); 
//		}
		#endregion 内部处理函数...

		private void _DataObj_AfterValueChanged(object sender, DIYReport.ReportModel.RptEventArgs e) {
			updateByDataObj();

			//DesignEnviroment.UICmidExecutor.ExecCommand(UICommands.SetObjProperty) ; 
		}
		//

        private void updateByDataObj() {
            this.Location = _DataObj.Location;
            this.Size = _DataObj.Size;
            //this.Font = _DataObj;
            this.ForeColor = _DataObj.ForeColor;
            this.BackColor = _DataObj.BackgroundColor;
            //判断如果是
            this.Invalidate(); 
        }

		private void DesignControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
			//drawSingleObj(e.Graphics );
			DIYReport.Drawing.RptDrawHelper.GetDrawDesignObject(_DataObj).Draw(e.Graphics,_DataObj);  
		}
		private void drawSingleObj(Graphics g){
//			switch(_DataObj.Type){
//				case DIYReport.ReportModel.RptObjType.Line :
//					drawLine(g);
//					break;
//				case DIYReport.ReportModel.RptObjType.Rect  :
//					drawFrame(g);
//					break;
//				case DIYReport.ReportModel.RptObjType.Text  :
//					drawLableAndTextBox(g,true);
//					break;
//				case DIYReport.ReportModel.RptObjType.Express  :
//					drawLableAndTextBox(g,false);
//					break;
//				case DIYReport.ReportModel.RptObjType.Image  :
//					drawImage(g);
//					break;
//				default:
//					Debug.Assert(false,"该控件类型还没有设置！","");
//					break;
//			}
		}

		#region 绘制相应的控件...
//		//画线
//		private void drawLine(Graphics g){
//			SolidBrush brush = new SolidBrush(_DataObj.ForeColor);
//			DIYReport.ReportModel.RptObj.RptLine obj = _DataObj as  DIYReport.ReportModel.RptObj.RptLine;
//			Point p1 = new Point(0,0);
//			Point p2 = new Point(_DataObj.Size.Width,_DataObj.Size.Height);
//			switch(obj.LineType){
//				case DIYReport.ReportModel.LineType.Horizon://水平线
//					p2 = new Point(_DataObj.Size.Width,0);
//					break;
//				case DIYReport.ReportModel.LineType.Vertical ://垂直线
//					p2 = new Point(0,_DataObj.Size.Height);
//					break;
//				case DIYReport.ReportModel.LineType.Bias:// 斜线
//					p2 = new Point(_DataObj.Size.Width,_DataObj.Size.Height);
//					break;
//				case DIYReport.ReportModel.LineType.Backlash://反 斜线
//					p1= new Point(0,_DataObj.Size.Height);
//					p2 =  new Point(_DataObj.Size.Width,0);
//					break;
//				default:
//					Debug.Assert(false,"该类型还没有设置！","");
//					break;
//			}
//			Pen pen = new Pen(brush,_DataObj.LinePound); 
//			pen.DashStyle = _DataObj.LineStyle ;
//			g.DrawLine(pen,p1,p2); 
//		}
//		//画边框
//		private void drawFrame(Graphics g){
//			SolidBrush brush = new SolidBrush(_DataObj.ForeColor);
//			Pen pen = new Pen(brush,_DataObj.LinePound); 
//			pen.DashStyle = _DataObj.LineStyle ;
//			g.DrawRectangle(pen,_DataObj.InnerRect);
//		}
//		//画Lable和数据
//		private void drawLableAndTextBox(Graphics g,bool pIsLable){
//			StringFormat strFormat = new StringFormat();
//			DIYReport.Interface.IRptTextObj obj = _DataObj as    DIYReport.Interface.IRptTextObj;
//			SolidBrush brush = new SolidBrush(_DataObj.ForeColor);
//			 
//			if(obj.WordWrap==false) {
//				strFormat.Trimming = StringTrimming.EllipsisCharacter;
//				strFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit;
//			}
//			else {
//				strFormat.FormatFlags = StringFormatFlags.LineLimit;
//			}
//			strFormat.Alignment = obj.Alignment;  
//			string txt ="";
//			if( pIsLable){
//				txt = (_DataObj as RptLable).Text;
//			}
//			else{
//				RptExpressBox exObj = _DataObj as RptExpressBox;
//				if( exObj.ExpressType!= ExpressType.Express){ 
//					txt = "@" + exObj.DataSource; 
//				}
//				else{
//					txt = "@" + exObj.DataSource +"(" + exObj.FieldName + ")"; 
//				}
//			}
//			SizeF fontSize = g.MeasureString("A",obj.Font);
//			float fontFirstY =(_DataObj.Size.Height - fontSize.Height)/2; 
//			g.DrawString(txt,obj.Font,brush ,new RectangleF(new PointF(0,fontFirstY),  _DataObj.Size),strFormat);
//			if(obj.ShowFrame){
//				Pen pen = new Pen(brush,_DataObj.LinePound); 
//				pen.DashStyle = _DataObj.LineStyle ;
//				g.DrawRectangle(pen,_DataObj.InnerRect);   
//			}
//		}
//		//画Image 图像
//		private void drawImage(Graphics g) {
//		 DIYReport.ReportModel.RptObj.RptPictureBox pic = _DataObj as DIYReport.ReportModel.RptObj.RptPictureBox;    
//			SolidBrush brush = new SolidBrush(_DataObj.ForeColor);
//			if(pic.Image!=null){
//				g.DrawImageUnscaled(pic.Image ,0,0); 
////				try{
////					pic.Image.Dispose();
////				}
////				catch{}
//			}
//			g.DrawRectangle(new Pen(brush,_DataObj.LinePound),_DataObj.InnerRect);
//		}
		#endregion 绘制相应的控件...

		private void DesignControl_DoubleClick(object sender, System.EventArgs e) {
			DesignEnviroment.ShowPropertyForm(this.ParentForm,true); 
		}

		private void DesignControl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			
		}
//		protected override void OnPaintBackground(PaintEventArgs pevent) {
//			base.OnPaintBackground (pevent);
//		}
        //protected override CreateParams CreateParams {
        //    get {
        //        CreateParams cp = base.CreateParams;
        //        // 增加一个透明的样式到控件窗口上
        //        if(_DataObj!=null && _DataObj.IsTranspControl){
        //            cp.ExStyle |= 0x20;
        //        }
        //        return cp;
        //    }
        //}
	}

}
