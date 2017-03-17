//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-22
// Description	:	 DrawReport 根据XML和记录集画报表(nick 2006-04-10 描述： 为了处理兼容up2而存在)
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
//' 设置度量单位为 毫米
//e.Graphics.PageUnit = GraphicsUnit.Millimeter
using System;
using System.Drawing;
using System.Diagnostics ;
using System.Drawing.Printing;
using System.Data;
using System.Reflection ;
using System.Collections;

using DIYReport.ReportModel;
using DIYReport.Express ; 
namespace DIYReport.Print {
	/// <summary>
	/// DrawReport 根据XML和记录集画报表()
	/// </summary>
	public class DrawReport : DIYReport.Interface.IDrawReport {

		#region 变量定义...
		//得到页脚需要计算的字段
		private DIYReport.Express.ExpressValueList    _FooterExpress;
		//得到报表脚需要的统计计算的字段
		private DIYReport.Express.ExpressValueList _BottomExpress;
		//需要绘画的报表数据
		private object _RptData=null; 
		private int _RowPoint = 0;
		private int _HasDrawRowCount = 0;
		private int _PageNumber = 1;
		//为了画报表的脚的而设置的当前数据行
		//private int _BottomRow = 0;
		//判断是否为最后一页
		private bool _IsLastPage = false;
		private System.Drawing.Printing.PaperSize _DocSize;
		private System.Drawing.Printing.Margins _DocMargin ;

		private DIYReport.ReportModel.RptReport _DataReport;
		private ReportDrawInfo _RptInfo;
		private DrawDetailInfo _DrawDetailInfo;

		private DataRow[] _Rows;
		//在当前页中已经绘制的高度
		private int _HasDrawDetailHeight = 0;
//		//在多字段分组的情况下已经绘制的组的最大的index
//		private int _HasDrawGroupHeadMaxIndex = -1;
		//准备需要绘制的Group Section 的 head队列（需要先进的先绘制）
		private System.Collections.Queue _GroupHeads;
		//准备需要绘制的Group Section footer的栈（需要先进的后绘制）
		private Stack _GroupFoots;
		//详细部分的最大高度
		private readonly int REAL_DETAIL_HEIGHT ;
		//可用部分页的最大宽度
		private readonly int REAL_PAGE_WIDTH ;

		#endregion 变量定义...
		public DIYReport.ReportModel.RptReport DataReport{
			get{
				return _DataReport;
			}
		}
		#region Public 属性...

		#endregion Public 属性...

		//		#region 事件处理相关...
		//		private DrawObjEventHandler _BeforDrawObject;
		//		public event DrawObjEventHandler BeforDrawObject {
		//			add { 
		//				_BeforDrawObject += value; 
		//			} 
		//			remove { 
		//				_BeforDrawObject -= value; 
		//			}
		//
		//		}
		//		private void FireBeforDrawObject(DrawObjEventArgs e) {
		//			if (mBeforDrawObject != null) {
		//				// 调用相应的委托代理
		//				mBeforDrawObject(this, e);
		//			} 
		//		}
		//		#endregion 事件处理相关...

		public DrawReport(object pDs,DIYReport.ReportModel.RptReport pDataReport) {
			_RptData = pDs;
			DIYReport.UserDIY.DesignEnviroment.DataSource =  pDs;
 
			_DataReport = pDataReport;
			_Rows = DIYReport.GroupAndSort.GroupDataProcess.SortData(pDs,pDataReport);   

			_RptInfo = new ReportDrawInfo(pDataReport);
			 
			_FooterExpress = DIYReport.Express.ExpressValueList.GetFooterExpress(pDataReport);
			_BottomExpress = DIYReport.Express.ExpressValueList.GetBottomExpress(pDataReport) ;
			
			_DocSize = _DataReport.PaperSize;

			_DocMargin = _DataReport.Margins ;

			//初始化页页数
			DIYReport.Express.ExSpecial._Page = 1;   
			DIYReport.Express.ExSpecial._PageCount = 1;

			_DrawDetailInfo = new DrawDetailInfo(pDataReport);
			_GroupFoots = new Stack();
			_GroupHeads = new Queue();

			float mergeHeight = _DataReport.SectionList.GetExceptDetailHeight() ;
			int rHeight = _DataReport.IsLandscape? _DocSize.Width : _DocSize.Height;
			REAL_DETAIL_HEIGHT = rHeight  - Convert.ToInt32(mergeHeight) - _DocMargin.Top - _DocMargin.Bottom ;
			int rWidth = _DataReport.IsLandscape? _DocSize.Height : _DocSize.Width;
			REAL_PAGE_WIDTH = rWidth - _DocMargin.Left - _DocMargin.Right;
			DIYReport.Express.ExSpecial._RowOrderNO = 0;
		}
		#region IDDispose...
		private bool disposed = false;
		private ArrayList _ImageList;
		/// <summary>
		/// 主动清理垃圾。
		/// </summary>
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(bool disposing) {
			// Check to see if Dispose has already been called.
			if(!disposed) {
				// If disposing equals true, dispose all managed 
				// and unmanaged resources.
				if(disposing) {
					// Dispose managed resources.
				}
				if(_ImageList!=null && _ImageList.Count>0){
					foreach(Image img in _ImageList)
						img.Dispose();
				}
			}
			disposed = true;         
		}
		~DrawReport() {
			// Do not re-create Dispose clean-up code here.
			// Calling Dispose(false) is optimal in terms of
			// readability and maintainability.
			Dispose(false);
		}
		#endregion IDDispose...
		/// <summary>
		/// 打印之前需要初始化的数据
		/// </summary>
		public void BeginPrint(){
			HasDrawRowCount=0;
			PageNumber = 1;
			DIYReport.Express.ExSpecial._Page = 1;   
			DIYReport.Express.ExSpecial._PageCount = 1;
			DIYReport.Express.ExSpecial._RowOrderNO = 0;
			_GroupFoots.Clear();
			_GroupHeads.Clear();
			foreach(object field in _DrawDetailInfo.GroupFields){
				DrawGroupField groupField = field as DrawGroupField;
				groupField.CurrGroupValue = null; 
			}
		}
		
		/// <summary>
		/// 绘制报表的Section 
		/// </summary>
		/// <param name="g"></param>
		/// <param name="pSectionType"></param>
		public bool DrawReportSection(object graphicsObject,DIYReport.SectionType pSectionType){
			Graphics g = graphicsObject as Graphics;

			DIYReport.ReportModel.RptSection section = _DataReport.SectionList.GetSectionByType( pSectionType);  
			if(section.Visibled){
				//分组的Section 在画 Detail 的时候处理
				if(section!=null  && pSectionType!=DIYReport.SectionType.GroupHead && pSectionType!=DIYReport.SectionType.GroupFooter ) {
					if(pSectionType==DIYReport.SectionType.Detail ){
						return DrawDetail(g,section);
					}
					else{
						foreach(DIYReport.ReportModel.RptSingleObj   obj in section.RptObjList) {
							DrawRptSimpleObj(g,obj);
						}
					}
				}
			}
			return true;
		}
		#region 画报表的详细信息...
		/// <summary>
		/// 画详细信息 
		/// </summary>
		/// <param name="g"></param>
		/// <returns>如果还有下一页，那么返回True,否则 False</returns>
		private bool DrawDetail(Graphics g,DIYReport.ReportModel.RptSection pSection) {
			if(_RptData==null || pSection==null || pSection.Height == 0 || pSection.RptObjList.Count == 0 ) {
				_IsLastPage = true;
				return false;
			}
			int initialRow = this.HasDrawRowCount ;
			_RowPoint = 0;
			_HasDrawDetailHeight = 0;
			//_BottomRow = 0;
			_IsLastPage = false;
			bool reB = false;
			//判断是否存在剩余的组头或者组脚并绘制它
			drawLeaveGroupInfo(g);
			if(_Rows.Length > 0 && initialRow==0){
				analyseGroupSection(0);
				reB = drawGroupHead(g);
				if(reB){
					return true;
				}
			}
			for (int i = initialRow; i < _Rows.Length; i++) {
				DIYReport.Express.ExSpecial._RowOrderNO ++;
				foreach(RptSingleObj  obj in pSection.RptObjList) {
					DrawRptSimpleObj(g,obj,_Rows[i]);
				}
				_HasDrawDetailHeight += pSection.Height;
				_RowPoint++;
				_HasDrawRowCount ++;
				
				//分析下一个分组行的信息，并决定如何来绘制当前行的信息，如果存在那么就绘制
				analyseGroupSection(i + 1);
				reB = drawGroupFoot(g);
				if(reB){//判断是否绘制完毕
					return true;
				}
				reB = drawGroupHead(g);
				if(reB){
					return true;
				}
				//已经画到的行的高度 
				int temHeight = _HasDrawDetailHeight + pSection.Height  ;
				if( temHeight > REAL_DETAIL_HEIGHT ) {
					return true;
				}
			}
			int temH = _HasDrawDetailHeight  + _RptInfo.BottomHeight;
			if( temH > REAL_DETAIL_HEIGHT ) {
				return true;
			}
			//_BottomRow = _RowPoint;
			int mergeCount = REAL_DETAIL_HEIGHT / pSection.Height;
			mergeCount -= _RowPoint;
			mergeCount +=1;
			//判断是否有剩余的空间，画空行 
			for(int j =0; j< mergeCount ;j++) {
				if(_DataReport.FillNULLRow){
					foreach(RptSingleObj  obj in pSection.RptObjList) {
						drawRect(g,obj);
					}
				} 
				_RowPoint++;
			} // end for(int j =0; j< mergeCount ;j++) 
			_IsLastPage = true;
			return false;
		}
		#endregion 画报表的详细信息...

		#region 绘制Group Section 的头和脚信息...
		//分析字段的Group Section信息，并存储在对应的队列和栈中
		private void analyseGroupSection(int pRowIndex){
			int rowCount = _Rows.Length;
			IList fields =  _DrawDetailInfo.GroupFields ;
			bool isEnd = false;
			if(fields!=null && fields.Count >0){
				int count = fields.Count;
				int drawFoot = -1;
				int iniGroupIndex = 0;
				if(pRowIndex < rowCount){
					DataRow dRow = _Rows[pRowIndex];
					bool mustDrawHead = false;
					for(int i = iniGroupIndex  ; i <count; i++){
						DrawGroupField groupField = fields[i] as DrawGroupField;
						string fieldName = groupField.GroupFieldName;
						object val = dRow[fieldName];
						DIYReport.ReportModel.RptSection section = _DataReport.SectionList.GetByGroupField(fieldName,true); 
						//判断行的值和已经绘制的grou p section head 中正在分析的字段的值是否相同
						bool inTheGroup = !mustDrawHead && DIYReport.GroupAndSort.GroupDataProcess.ValueInTheGroup(section, groupField.CurrGroupValue,val);  
						groupField.CurrGroupValue = val;
						if(!inTheGroup && !mustDrawHead){
							drawFoot = i;
							mustDrawHead = true;
						}
						if(!groupField.HasDrawGroupHead || mustDrawHead){
							//把需要绘制的group section 的head 添加到队列的结尾
							groupField.PrevFirstRowIndex   = groupField.FirstRowIndex==-1?0:groupField.FirstRowIndex;
							groupField.FirstRowIndex   = _HasDrawRowCount;
							_GroupHeads.Enqueue( section);
							
						}
					}
				}
				else{
					drawFoot = 0;
					isEnd = true;
				}
				if(drawFoot > -1 ){
					for(int i = drawFoot; i <count;i++){
						DrawGroupField groupField = fields[i] as DrawGroupField;
						if(isEnd){
							groupField.PrevFirstRowIndex = groupField.FirstRowIndex; 
						}
						//groupField.FirstRowIndex = groupField.PrevFirstRowIndex; 
						string fieldName = groupField.GroupFieldName;
						DIYReport.ReportModel.RptSection section = _DataReport.SectionList.GetByGroupField(fieldName,false);   
						//把需要绘制的group section 的footer 压到栈中
						_GroupFoots.Push(section);  
						//groupField.HasDrawGroupHead = false;
					} // end for(int i = drawFoot; i <count;i++){
				} //end drawFoot > -1
			} // end fields!=null && fields.Count >0
		}
		//绘制字段分组的头
		//返回 true 表示还没有结束
		private bool drawGroupHead(Graphics g){

			if(_GroupHeads.Count > 0){
				int count = _GroupHeads.Count;
				//_HasDrawDetailHeight
				for(int i = 0; i < count; i ++){
					DIYReport.ReportModel.RptSection section = _GroupHeads.Dequeue() as DIYReport.ReportModel.RptSection;
					if(section.Visibled){
						int tempHeight = _HasDrawDetailHeight + section.Height ;
						if(tempHeight > REAL_DETAIL_HEIGHT){
							return true;
						}
						DrawGroupField groupField = _DrawDetailInfo.GetGroupFieldByName(section.GroupField.FieldName);
   
						groupField.HasDrawGroupHead = true;
						foreach(RptSingleObj  obj in section.RptObjList) {
							DrawRptSimpleObj(g,obj,_Rows[_HasDrawRowCount]);
						}
						_HasDrawDetailHeight +=section.Height;

						if(count > 0 && i == count -1){
							DIYReport.Express.ExSpecial._RowOrderNO = 0 ;  
						}
					}
				}
			}
			return false;
		}
		//绘制字段分组的脚
		//返回 true 表示还没有结束
		private bool drawGroupFoot(Graphics g){
			if(_GroupFoots.Count > 0){
				int count = _GroupFoots.Count;
				//_HasDrawDetailHeight
				for(int i = 0; i < count; i ++){
					DIYReport.ReportModel.RptSection section = _GroupFoots.Peek() as DIYReport.ReportModel.RptSection;
					if(section.Visibled){
						int tempHeight = _HasDrawDetailHeight + section.Height ;
						if(tempHeight > REAL_DETAIL_HEIGHT){
							return true;
						}
						section = _GroupFoots.Pop() as DIYReport.ReportModel.RptSection;
						DrawGroupField groupField = _DrawDetailInfo.GetGroupFieldByName(section.GroupField.FieldName);
   
						groupField.HasDrawGroupHead = false;
						DIYReport.Express.ExSpecial._RowOrderNO = 0;
						foreach(RptSingleObj  obj in section.RptObjList) {
							DrawRptSimpleObj(g,obj,_Rows[_HasDrawRowCount - 1]);
						}
						_HasDrawDetailHeight +=section.Height;
					}
				}
			}
			return false;
		}
		//绘制上页剩下的分组头或者脚
		private bool drawLeaveGroupInfo(Graphics g){
			bool b = drawGroupHead(g);
			if(b){
				return true;
			}
			b = drawGroupFoot(g);
			return b;
		}
		#endregion 绘制Group Section 的头和脚信息...

		#region 内部处理 得到所画对象真正的位置 和画具体的对象...
		//画具体每一个对象
		private void DrawRptSimpleObj(Graphics g,RptSingleObj  pObj) {
			DrawRptSimpleObj(g,pObj,null);
		}
		private void DrawRptSimpleObj(Graphics g,DIYReport.Interface.IRptSingleObj  pObj,DataRow pDRow) {
			switch(pObj.Type) {
				case RptObjType.Text :
				case RptObjType.Express :
					drawTextObj(g,pObj,pDRow);
					break;
				case RptObjType.Line :
					drawLine(g,pObj);
					break;
				case RptObjType.Rect :
					drawRect(g,pObj);
					break;
				case RptObjType.Image :
					drawImage(g,pObj); 
					break;
				default:
					Debug.Assert(false,"请确认报表的XML文件配制是否正确",
						"在标题这一项，对象" + pObj.Name + " 的类型不能设置成:" + pObj.Type.ToString() +"类型");
					break;
			}
		}

		//画文本对象
		private void drawTextObj(Graphics g,DIYReport.Interface.IRptSingleObj  pObj,DataRow pDRow){
			DIYReport.Interface.IRptTextObj textObj = pObj as DIYReport.Interface.IRptTextObj;
			StringFormat strFormat = new StringFormat();
			SolidBrush foreBrush = new SolidBrush(pObj.ForeColor);
			if(textObj.WordWrap==false) {
				strFormat.Trimming = StringTrimming.EllipsisCharacter;
				strFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit;
			}
			else {
				strFormat.FormatFlags = StringFormatFlags.LineLimit;
			}
			strFormat.Alignment = textObj.Alignment;  
			RectangleF strRect = RealRectangle(textObj);
			SizeF fontSize = g.MeasureString("A",textObj.Font);
			float fontFirstY = strRect.Y  +  (textObj.Size.Height - fontSize.Height)/2; 
			strRect.Y = fontFirstY;
			if(textObj.Type == DIYReport.ReportModel.RptObjType.Text){
				DIYReport.ReportModel.RptObj.RptLable lab = pObj as DIYReport.ReportModel.RptObj.RptLable;
				if(lab.Text!=null){
					g.DrawString(lab.Text,lab.Font,foreBrush,strRect,strFormat);   
				}
			}
			else{
				//如果绑定的不是文本而是字段、表达式或者系统参数等，那么就需要对它进行解释并绘制它
				string val = "";
				DIYReport.ReportModel.RptObj.RptExpressBox box = pObj as DIYReport.ReportModel.RptObj.RptExpressBox;
				if(box.DataSource!=null && box.DataSource!="" && box.DataSource!="[未绑定]"){
					switch(box.ExpressType ){
						case ReportModel.ExpressType.Express :
							int beginIndex= 0,endIndex = 0;
							if(box.Section.SectionType == DIYReport.SectionType.ReportBottom){ //统计的字段在报表脚，那么是统计所有的行
								beginIndex = 0;
								endIndex = _Rows.Length;
							}
							else if(box.Section.SectionType == DIYReport.SectionType.PageFooter){//统计的字段在页脚，那么是统计当前页所有的行
								beginIndex = _HasDrawRowCount - _RowPoint;
								endIndex =  _HasDrawRowCount;
							}
							else if(box.Section.SectionType == DIYReport.SectionType.GroupFooter){//统计的字段在分组的脚，那么统计分组涉及到的行
								DIYReport.Print.DrawGroupField groupField = _DrawDetailInfo.GetGroupFieldByName(box.Section.GroupField.FieldName); 
								beginIndex = groupField.PrevFirstRowIndex;
								endIndex =  _HasDrawRowCount;
							}
							else{
								//g.DrawString("不支持的表达式",box.Font,foreBrush,strRect,strFormat); 
								g.DrawString(string.Empty,box.Font,foreBrush,strRect,strFormat); 
								break;
							}
							val = DIYReport.Express.ExStatistical.GetStatisticalValue(beginIndex,endIndex -1,box.DataSource,box.BingDBFieldName,_Rows );
							if(val==null)
								val = " ";
							double dval = DIYReport.PublicFun.ToDouble(val); 
							if(box.FormatStyle!=null && box.FormatStyle!=""){
								try{
									//val = String.Format(box.FormatStyle,dval);
									val = DIYReport.PublicFun.FormatString(box.FormatStyle.Trim(),dval);
								}
								catch{}
							}
							g.DrawString(val,box.Font,foreBrush,strRect,strFormat); 
							break;
						case ReportModel.ExpressType.SysParam  : //系统参数
							Type clsType = System.Type.GetType("DIYReport.Express.ExSpecial");
							MethodInfo meth = clsType.GetMethod(box.DataSource);
							if(meth!=null){
								object speTxt =meth.Invoke(clsType,null);
								val = speTxt.ToString();
							}
							g.DrawString(val,box.Font ,foreBrush,strRect ,strFormat);  
							break;
						case ReportModel.ExpressType.UserParam ://用户外部参数
							DIYReport.ReportModel.RptParam param = DIYReport.UserDIY.DesignEnviroment.CurrentReport.UserParamList[ box.DataSource];
							if(param!=null){
								g.DrawString(param.Value.ToString() ,box.Font ,foreBrush,strRect,strFormat);  
							}
							break;
						case ReportModel.ExpressType.Field ://用户绑定字段
//							string str = "";
//							if(pDRow!=null)   
//								str = pDRow[box.DataSource].ToString();
//							g.DrawString(str,box.Font ,foreBrush,RealRectangle(box) ,strFormat);  
							DrawField(g,pDRow,box,foreBrush,strRect,strFormat);
							break;
						default:
							break;
					}
				}
			}
			if(textObj.ShowFrame == true)
				drawRect(g,pObj);
		}
		#region Protectd virtual 方法...
		/// <summary>
		/// 画用户绑定的字段
		/// </summary>
		/// <param name="g"></param>
		/// <param name="pDRow"></param>
		/// <param name="pBox"></param>
		/// <param name="pBrush"></param>
		/// <param name="pStrFormat"></param>
		protected virtual void DrawField(Graphics g,DataRow pDRow,DIYReport.ReportModel.RptObj.RptExpressBox pBox,
										SolidBrush pBrush, RectangleF pStrRect,StringFormat pStrFormat){
			string str = "";
			if(pDRow!=null){
				if(pDRow.Table.Columns.Contains(pBox.BingDBFieldName) && pDRow[pBox.BingDBFieldName]!= System.DBNull.Value ){
					if(pBox.FormatStyle!=null && pBox.FormatStyle.Trim()!=""){
						try{
							//str = String.Format(pBox.FormatStyle.Trim(),pDRow[pBox.BingDBFieldName]); 
							str = DIYReport.PublicFun.FormatString(pBox.FormatStyle.Trim(),pDRow[pBox.BingDBFieldName]);
						}
						catch{}
					}
					else{
						str = pDRow[pBox.BingDBFieldName].ToString();
					}
					
				}
			}
			g.DrawString(str,pBox.Font ,pBrush,pStrRect,pStrFormat);  	
		}
		#endregion Protectd virtual 方法...
		//画Image 图像
		private void drawImage(Graphics g,DIYReport.Interface.IRptSingleObj pObj) {
			DIYReport.ReportModel.RptObj.RptPictureBox pic = pObj as DIYReport.ReportModel.RptObj.RptPictureBox;
			RectangleF rct = RealRectangle(pObj);
			if(pic.Image!=null)
				g.DrawImage(pic.Image,rct.Left,rct.Top,rct.Width,rct.Height); 
		}

		//画线条
		private void drawLine(Graphics g,DIYReport.Interface.IRptSingleObj pObj) {
			RectangleF rct = RealRectangle(pObj);
			SolidBrush brush = new SolidBrush(pObj.ForeColor);
			Pen pen = new Pen(brush,pObj.LinePound); 
			pen.DashStyle = pObj.LineStyle ;
			
			DIYReport.ReportModel.RptObj.RptLine obj = pObj as  DIYReport.ReportModel.RptObj.RptLine;

			PointF p1 = new PointF(rct.Left ,rct.Top);
			PointF p2 = new PointF(pObj.Size.Width,pObj.Size.Height);

			switch(obj.LineType){
				case DIYReport.ReportModel.LineType.Horizon://水平线
					p2 = new PointF(rct.Left + obj.Size.Width,rct.Top);
					break;
				case DIYReport.ReportModel.LineType.Vertical ://垂直线
					p2 = new PointF(rct.Left,obj.Size.Height + rct.Top);
					break;
				case DIYReport.ReportModel.LineType.Bias:// 斜线
					p2 = new PointF(rct.Left + obj.Size.Width,rct.Top + obj.Size.Height);
					break;
				case DIYReport.ReportModel.LineType.Backlash://反 斜线
					p1= new PointF(rct.Left,rct.Top + obj.Size.Height);
					p2 =  new PointF(rct.Left + obj.Size.Width,rct.Top);
					break;
				default:
					Debug.Assert(false,"该类型还没有设置！","");
					break;
			}
			g.DrawLine(pen,p1.X,p1.Y  ,p2.X ,p2.Y ); 
		}
		//画边框
		private void drawRect(Graphics g,DIYReport.Interface.IRptSingleObj pObj) {
			RectangleF rct = RealRectangle(pObj);
			SolidBrush brush = new SolidBrush(pObj.ForeColor);
			Pen pen = new Pen(brush,pObj.LinePound); 
			pen.DashStyle = pObj.LineStyle ;
			g.DrawRectangle(pen,rct.Left-1,rct.Top-3,rct.Width,rct.Height); 
		}
		//得到绘制文件在页面上真正的位置 XML配制的Y坐标 + Section 的 Height
		protected virtual RectangleF RealRectangle(DIYReport.Interface.IRptSingleObj  pRptObj) {
			RectangleF drawRct;
			RectangleF oldRct = pRptObj.Rect;
			int marginLeft = _DocMargin.Left ,marginTop = _DocMargin.Top ;
			//在这里如果存在每张报表只绘制一个title 的事情detail 的高度要做个计算才可以。
			switch(pRptObj.Section.SectionType  ) {
				case DIYReport.SectionType.ReportTitle:

					drawRct = new RectangleF( marginLeft + oldRct.X,marginTop + oldRct.Y ,oldRct.Width,oldRct.Height); 
					return drawRct;
				case DIYReport.SectionType.PageHead:
					 
					drawRct = new RectangleF(marginLeft + oldRct.X , marginTop + oldRct.Y + _RptInfo.TitleHeight,
						oldRct.Width,oldRct.Height);
					return drawRct;
				case DIYReport.SectionType.GroupHead:
				case DIYReport.SectionType.Detail:
				case DIYReport.SectionType.GroupFooter:
					drawRct = new RectangleF(marginLeft + oldRct.X ,marginTop + oldRct.Y + _RptInfo.TitleHeight +
						_RptInfo.PageHeadHeight + _HasDrawDetailHeight , 
						oldRct.Width,oldRct.Height);
					return drawRct;
				case DIYReport.SectionType.PageFooter:
					drawRct = new RectangleF(marginLeft + oldRct.X ,marginTop + oldRct.Y + _RptInfo.TitleHeight +
						_RptInfo.PageHeadHeight  + REAL_DETAIL_HEIGHT  ,
						oldRct.Width,oldRct.Height);
					return drawRct;
				case DIYReport.SectionType.ReportBottom :
					float rHeight;
					if(_IsLastPage && _RowPoint ==0)
						rHeight =  _RptInfo.BottomHeight;
					else
						rHeight = _HasDrawDetailHeight;
					drawRct = new RectangleF(marginLeft + oldRct.X  ,marginTop + oldRct.Y + _RptInfo.TitleHeight +
						_RptInfo.PageHeadHeight  + rHeight,
						oldRct.Width,oldRct.Height);
					return drawRct;
			}
			return oldRct;
		}
		// 
		#endregion 内部处理 得到所画对象真正的位置 和画具体的对象...
		
		#region Public 属性...
		public int HasDrawRowCount {
			get {
				return _HasDrawRowCount;
			}
			set {
				_HasDrawRowCount = value;
			}
		}
		public int PageNumber {
			get {
				return _PageNumber;
			}
			set {
				_PageNumber = value;
				DIYReport.Express.ExSpecial._Page = _PageNumber;   
				DIYReport.Express.ExSpecial._PageCount = _PageNumber;
			}
		}
		#endregion Public 属性...
		

	}
}
