//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-08
// Description	:	绘制报表所需要的对象。 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------

using System.Drawing;
using DIYReport.ReportModel.RptObj ; 
using DIYReport.ReportModel;
using System.Collections;
using System.Diagnostics ;
using DIYReport.Barcode;
 

namespace DIYReport.Drawing
{
	/// <summary>
	/// DrawHelper 绘制报表所需要的对象。 
	/// </summary>
	public class RptDrawHelper
	{
		public static readonly string CHECK_BOX_FILE = "DIYReport.Images.check.gif";
		public static readonly string UN_CHECK_BOX_FILE = "DIYReport.Images.uncheck.gif";
		public static readonly string SUB_REPORT_FILE = "DIYReport.Images.Subreport.gif";

		public static readonly string NO_BING_TAG = "[未绑定]";

		private static Hashtable _DrawObjLib = new Hashtable();
		private static Hashtable _DrawPreviewObjLib = new Hashtable();
		/// <summary>
		/// add private to prevent instance.
		/// </summary>
		private RptDrawHelper()
		{
		}
		/// <summary>
		/// 获取绘制报表对象的操作对象。(主要用在设计状态下的报表绘制)
		/// </summary>
		/// <param name="dataObj"></param>
		/// <returns></returns>
		public static DrawingRptObject GetDrawDesignObject(DIYReport.Interface.IRptSingleObj  dataObj){
			DIYReport.ReportModel.RptObjType objType = dataObj.Type ;
			DrawingRptObject dObj = _DrawObjLib[objType] as DrawingRptObject;
			if(dObj!=null)
				return dObj;
			switch(objType){
				case DIYReport.ReportModel.RptObjType.Line:
					dObj = new DrawRptLine(); 
					break;
				case DIYReport.ReportModel.RptObjType.Text:
					dObj = new DrawRptLable();
					break;
				case DIYReport.ReportModel.RptObjType.Express:
					dObj = new  DrawDesignRptExpressBox();
					break;
				case DIYReport.ReportModel.RptObjType.Rect:
					dObj = new DrawRptFrame(); 
					break;
				case DIYReport.ReportModel.RptObjType.FieldImage:
					dObj = new DrawBingFieldImage();
					break;
				case DIYReport.ReportModel.RptObjType.Image:
					dObj = new DrawRptPicrureBox();
					break;
				case DIYReport.ReportModel.RptObjType.CheckBox:
					dObj = new DrawRptCheckBox(); 
					break;
				case DIYReport.ReportModel.RptObjType.BarCode:
					dObj = new DrawRptBarCode(); 
					break;
				case DIYReport.ReportModel.RptObjType.SubReport:
					dObj = new DrawRptSubReport(); 
					break;
				case DIYReport.ReportModel.RptObjType.FieldTextBox:
					dObj = new  DrawDesignRptFieldTextBox();
					break;
				case DIYReport.ReportModel.RptObjType.HViewSpecField:
					dObj = new  DrawDesignRptHViewSpecFieldBox();
					break;
				case DIYReport.ReportModel.RptObjType.RichTextBox:
					dObj = new  DrawDesignRptRichTextBox();
					break;
				default:
					Debug.Assert(false,"该类型的绘制还没有处理。");
					break;
			}
			_DrawObjLib[objType] = dObj;
			return dObj;
		}
		/// <summary>
		/// 获取绘制报表对象的操作对象。(主要用在报表预浏和打印状态下的报表绘制)
		/// </summary>
		/// <param name="dataObj"></param>
		/// <returns></returns>
		public static DrawingRptObject GetPreviewDrawObject(DIYReport.Interface.IRptSingleObj  dataObj){
			DIYReport.ReportModel.RptObjType objType = dataObj.Type ;
			DrawingRptObject dObj = _DrawPreviewObjLib[objType] as DrawingRptObject;
			if(dObj!=null)
				return dObj;
			switch(objType){
//				case DIYReport.ReportModel.RptObjType.Line:
//					dObj = new DrawRptLine(); 
//					break;
//				case DIYReport.ReportModel.RptObjType.Text:
//					dObj = new DrawRptLable();
//					break;
//				case DIYReport.ReportModel.RptObjType.Express:
//					dObj = new  DrawDesignRptFieldTextBox();
//					break;
//				case DIYReport.ReportModel.RptObjType.Rect:
//					dObj = new DrawRptFrame(); 
//					break;
//				case DIYReport.ReportModel.RptObjType.FieldImage:
//					dObj = new DrawBingFieldImage();
//					break;
//				case DIYReport.ReportModel.RptObjType.Image:
//					dObj = new DrawRptPicrureBox();
//					break;
//				case DIYReport.ReportModel.RptObjType.CheckBox:
//					dObj = new DrawRptCheckBox(); 
//					break;
				case DIYReport.ReportModel.RptObjType.BarCode:
					dObj = new DrawRptBarCode(); 
					break;
//				case DIYReport.ReportModel.RptObjType.SubReport:
//					dObj = new DrawRptSubReport(); 
//					break;
				default:
					Debug.Assert(false,"该类型的绘制还没有处理。");
					break;
			}
			_DrawPreviewObjLib[objType] = dObj;
			return dObj;
		}
		/// <summary>
		/// 从资源中创建一个位图。
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static Bitmap CreateBitmapFromResources(string name){
			System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
			System.IO.Stream stream = asm.GetManifestResourceStream(name);
			Bitmap image = (Bitmap)Bitmap.FromStream(stream);
			return image;
		}
	}

	public abstract class DrawingRptObject{
		private bool _IsDesign;
		public abstract void Draw(Graphics g,DIYReport.Interface.IRptSingleObj  dataObj);

		public bool IsDesign{
			get{
				return _IsDesign;
			}
			set{
				_IsDesign = value;
			}
		}
		/// <summary>
		/// 绘制绑定到非文本对象的字段信息。
		/// </summary>
		/// <param name="g"></param>
		/// <param name="dataObj"></param>
		/// <param name="fieldName"></param>
		public  void DrawBingFieldName(Graphics g, DIYReport.Interface.IRptSingleObj dataObj,string fieldName) {
			StringFormat strFormat = new StringFormat();
			strFormat.FormatFlags = StringFormatFlags.LineLimit;
			SolidBrush brush = new SolidBrush(Color.Black);
			Font f = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			SizeF fontSize = g.MeasureString("A",f);
			float fontFirstY =(dataObj.Size.Height - fontSize.Height)/2; 

			g.DrawString(fieldName,f,brush ,new RectangleF(new PointF(0,fontFirstY),  dataObj.Size),strFormat);
 
			Pen pen = new Pen(brush,dataObj.LinePound); 
			pen.DashStyle = dataObj.LineStyle ;
			g.DrawRectangle(pen,dataObj.InnerRect);   
 
		}
		/// <summary>
		/// 绘制Unscaled图象
		/// </summary>
		/// <param name="g"></param>
		/// <param name="dataObj"></param>
		/// <param name="image"></param>
		public void DrawImageUnscaled(Graphics g, DIYReport.Interface.IRptSingleObj dataObj,Image image){
			SolidBrush brush = new SolidBrush(dataObj.ForeColor);
			System.Drawing.Rectangle drawRect = dataObj.InnerRect;
			if(image!=null){
				System.Drawing.Size  iSize = image.Size;
				System.Drawing.Point drawPoint = new  Point((dataObj.InnerRect.Width - iSize.Width)/2,(dataObj.InnerRect.Height - iSize.Height)/2);
				g.DrawImageUnscaled(image,drawPoint); 
			}

	 
			g.DrawRectangle(new Pen(brush,dataObj.LinePound),dataObj.InnerRect);
		 
		}
	}

	public class DrawRptLine : DrawingRptObject{
		public override void Draw(Graphics g,DIYReport.Interface.IRptSingleObj  dataObj) {
			SolidBrush brush = new SolidBrush(dataObj.ForeColor);
			DIYReport.ReportModel.RptObj.RptLine obj = dataObj as  DIYReport.ReportModel.RptObj.RptLine;
			if(obj==null)
				return;
			Point p1 = new Point(0,0);
			Point p2 = new Point(dataObj.Size.Width,dataObj.Size.Height);
			switch(obj.LineType){
				case DIYReport.ReportModel.LineType.Horizon://水平线
					p2 = new Point(dataObj.Size.Width,0);
					break;
				case DIYReport.ReportModel.LineType.Vertical ://垂直线
					p2 = new Point(0,dataObj.Size.Height);
					break;
				case DIYReport.ReportModel.LineType.Bias:// 斜线
					p2 = new Point(dataObj.Size.Width,dataObj.Size.Height);
					break;
				case DIYReport.ReportModel.LineType.Backlash://反 斜线
					p1= new Point(0,dataObj.Size.Height);
					p2 =  new Point(dataObj.Size.Width,0);
					break;
				default:
					Debug.Assert(false,"该类型还没有设置！","");
					break;
			}
			Pen pen = new Pen(brush,dataObj.LinePound); 
			pen.DashStyle = dataObj.LineStyle ;
			g.DrawLine(pen,p1,p2); 
		}

	}

	public class DrawRptFrame : DrawingRptObject{
		public override void Draw(Graphics g, DIYReport.Interface.IRptSingleObj dataObj) {
			SolidBrush brush = new SolidBrush(dataObj.ForeColor);
			Pen pen = new Pen(brush,dataObj.LinePound); 
			pen.DashStyle = dataObj.LineStyle ;
			g.DrawRectangle(pen,dataObj.InnerRect);
		}
	}
	#region draw 文本相关...
	public class DrawRptText :DrawingRptObject{
		public override void Draw(Graphics g, DIYReport.Interface.IRptSingleObj dataObj) {
			StringFormat strFormat = new StringFormat();
			DIYReport.Interface.IRptTextObj obj = dataObj as    DIYReport.Interface.IRptTextObj;
			if(obj==null)
				return;

			SolidBrush brush = new SolidBrush(dataObj.ForeColor);
			 
			if(obj.WordWrap==false) {
				strFormat.Trimming = StringTrimming.EllipsisCharacter;
				strFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit;
			}
			else {
				strFormat.FormatFlags = StringFormatFlags.LineLimit;
			}
			strFormat.Alignment = obj.Alignment;
  
			string txt = GetObjectText(dataObj);
			if(txt.Length > 0){
				SizeF fontSize = g.MeasureString(txt,obj.Font,dataObj.Size.Width,strFormat);
				float fontFirstY =(dataObj.Size.Height - fontSize.Height)/2; 
				g.DrawString(txt,obj.Font,brush ,new RectangleF(new PointF(0,fontFirstY),  dataObj.Size),strFormat);
			}
			if(obj.ShowFrame){
				Pen pen = new Pen(brush,dataObj.LinePound); 
				pen.DashStyle = dataObj.LineStyle ;
				g.DrawRectangle(pen,dataObj.InnerRect);   
			}
		}
		public virtual string GetObjectText(DIYReport.Interface.IRptSingleObj dataObj){
			return string.Empty;
		}

	}
	public class DrawRptLable : DrawRptText{
		public override string GetObjectText(DIYReport.Interface.IRptSingleObj dataObj) {
			RptLable obj = dataObj as RptLable;
			if(obj!=null)
			return obj.Text;
			else
				return string.Empty;
		}

	}
	public class DrawDesignRptFieldTextBox : DrawRptText{
		public override string GetObjectText(DIYReport.Interface.IRptSingleObj dataObj) {
			string txt = string.Empty;
			RptFieldTextBox exObj = dataObj as  RptFieldTextBox;
			if(exObj!=null){
				txt = "@" +  exObj.FieldName ;  
			}
			return txt;
		}
	}
	public class DrawDesignRptRichTextBox : DrawingRptObject{
		public override void Draw(Graphics g, DIYReport.Interface.IRptSingleObj dataObj) {
			RptRichTextBox obj = dataObj as RptRichTextBox ;
			if(obj==null)
				return;

			this.DrawBingFieldName(g,obj,"@" + obj.FieldName); 
		}
	}
	public class DrawDesignRptHViewSpecFieldBox : DrawingRptObject{
		public override void Draw(Graphics g, DIYReport.Interface.IRptSingleObj dataObj) {
			RptHViewSpecFieldBox exObj = dataObj as   RptHViewSpecFieldBox;
			int cellWidth = exObj.CellWidth;
			int width = exObj.Size.Width;
			int count = width / cellWidth;

			SolidBrush brush = new SolidBrush(dataObj.ForeColor);
			Pen pen = new Pen(brush,dataObj.LinePound); 
			pen.DashStyle = dataObj.LineStyle ;
			g.DrawRectangle(pen,dataObj.InnerRect); 
			Rectangle rect = exObj.InnerRect;
			for(int i = 1 ; i < count; i++){
				g.DrawLine(pen,rect.X + cellWidth * i,rect.Y,rect.X + cellWidth * i,rect.Y + rect.Height);
			}
		}
	}
	public class DrawDesignRptExpressBox : DrawRptText{
		public override string GetObjectText(DIYReport.Interface.IRptSingleObj dataObj) {
			string txt = string.Empty;
			RptExpressBox exObj = dataObj as RptExpressBox;
			if(exObj!=null){
				if( exObj.ExpressType!= ExpressType.Express){ 
					txt = "@" + exObj.DataSource; 
				}
				else{
					txt = "@" + exObj.DataSource +"(" + exObj.FieldName + ")"; 
				}
			}
			return txt;
		}
	}
	#endregion draw 文本相关...
		
	#region 绘制图象相关...
	public class DrawRptPicrureBox : DrawingRptObject {
		public override void Draw(Graphics g, DIYReport.Interface.IRptSingleObj dataObj) {
			RptPictureBox imageObj = dataObj as  RptPictureBox ;  
			if(imageObj==null )
				return;
			 
 
//				switch(image.DrawSizeModel){
//					case System.Windows.Forms.PictureBoxSizeMode.AutoSize:
//						g.DrawImage(
//						 
//						break;
//					default:
//						break;
//				}
			 
			this.DrawImageUnscaled(g, imageObj,imageObj.Image);
		}

	}
	public class DrawBingFieldImage : DrawingRptObject{
		public override void Draw(Graphics g, DIYReport.Interface.IRptSingleObj dataObj) {
			RptDBPictureBox picData = dataObj as RptDBPictureBox;
			if(picData==null)
				return;

			this.DrawBingFieldName(g,dataObj,"@" + picData.FieldName); 
		}

	}

	#endregion 绘制图象相关...

	#region 绘制check Box 相关...
	public class DrawRptCheckBox : DrawingRptObject{
		public override void Draw(Graphics g, DIYReport.Interface.IRptSingleObj dataObj) {
			RptCheckBox checkBox = dataObj as RptCheckBox;
			if(checkBox==null)
				return;
			if(checkBox.BingField){
				string fieleName = checkBox.Checked?RptDrawHelper.CHECK_BOX_FILE : RptDrawHelper.UN_CHECK_BOX_FILE; 
				Image checkImage = RptDrawHelper.CreateBitmapFromResources(fieleName); 

				 base.DrawImageUnscaled(g,dataObj,checkImage);
 
				try{
					checkImage.Dispose();
				}
				catch{}
			}
			else{
				this.DrawBingFieldName(g,checkBox,checkBox.FieldName); 
			}
		}

	}
	#endregion 绘制check Box 相关...

	#region 条形码绘制相关...
	
	public class DrawRptBarCode : DrawingRptObject{
		private Hashtable _DrawBarCodeObject;

		public DrawRptBarCode(){
			_DrawBarCodeObject = new Hashtable();
		}

		public override void Draw(Graphics g, DIYReport.Interface.IRptSingleObj dataObj) {
			RptBarCode codeInfo = dataObj as RptBarCode;
			if(codeInfo==null)
				return;

			IDrawBarCode drawLib = _DrawBarCodeObject[typeof(DrawCodeBarCode)] as IDrawBarCode ;
			if(drawLib == null){
				drawLib = new DrawCodeBarCode();
				
				_DrawBarCodeObject[typeof(DrawCodeBarCode)]  = drawLib;
			}
			drawLib.DrawBarCode(g,codeInfo);
		}

		
		interface IDrawBarCode{
			void DrawBarCode(Graphics g,RptBarCode codeInfo);
		}
		class DrawCodeBarCode : IDrawBarCode{
			//绘制标准条形码。
			public  void DrawBarCode_old(Graphics g,RptBarCode codeInfo){
				float wid= codeInfo.WID;
				
				Barcode.IBarcode barCode = null;
				if(codeInfo.CodeType==BarCodeType.Code128)   
					barCode = new DIYReport.Barcode.Code128(codeInfo.BarCode);
				else
					barCode = new DIYReport.Barcode.Code39(codeInfo.BarCode);
				string encodedString = barCode.Encoded_Value ;

				if(encodedString==null || encodedString.Length ==0){
					g.DrawString("INVALID BAR CODE TEXT", codeInfo.HeaderFont, Brushes.Red, 10, 10);
					return ;
				}
				int encodedStringLength = encodedString.Length;
				int widthOfBarCodeString = 0;
				double wideToNarrowRatio = 1.67;
			
			
				if (codeInfo.VertAlign != AlignType.Left) {
					for ( int i=0;i<encodedStringLength; i++) {
						if ( encodedString[i]=='1' ) 
							widthOfBarCodeString +=(int)(wideToNarrowRatio*(int)codeInfo.Weight);
						else 
							widthOfBarCodeString +=(int)codeInfo.Weight;
					}
					//widthOfBarCodeString = System.Convert.ToInt32(encodedStringLength * 2) ;
				}

				float x = 0f;
				//int wid=0;
				int yTop = 0;
				SizeF hSize = g.MeasureString(codeInfo.HeaderText, codeInfo.HeaderFont);
				SizeF fSize = g.MeasureString(codeInfo.BarCode, codeInfo.FooterFont);

				float headerX = 0f;
				float footerX = 0f;

				if (codeInfo.VertAlign == AlignType.Left) {
					x = codeInfo.LeftMargin;
					headerX = codeInfo.LeftMargin;
					footerX = codeInfo.LeftMargin;
				}
				else if (codeInfo.VertAlign == AlignType.Center) {
					x = (codeInfo.InnerRect.Width  - widthOfBarCodeString  * wid) / 2;
					headerX = (codeInfo.InnerRect.Width - hSize.Width) / 2;
					footerX = (codeInfo.InnerRect.Width - fSize.Width ) / 2;
				}
				else {
					x = codeInfo.InnerRect.Width - widthOfBarCodeString * wid - codeInfo.LeftMargin;
					headerX = codeInfo.InnerRect.Width - hSize.Width - codeInfo.LeftMargin;
					footerX = codeInfo.InnerRect.Width - fSize.Width  - codeInfo.LeftMargin;
				}

				if (codeInfo.ShowHeader) {
					yTop = (int)hSize.Height + codeInfo.TopMargin;
					g.DrawString(codeInfo.HeaderText, codeInfo.HeaderFont, Brushes.Black, headerX, codeInfo.TopMargin);
				}
				else {
					yTop = codeInfo.TopMargin;
				}

				for ( int i=0;i<encodedStringLength; i++) {
					Color c = encodedString[i]=='1'? Color.Black : Color.White;

					g.DrawLine(new Pen(c, (float)wid), new PointF(x + wid, 0), new PointF(x + wid,codeInfo.BarCodeHeight));
					
					x +=wid;
				}

				yTop += codeInfo.BarCodeHeight;

				if (codeInfo.ShowFooter)
					g.DrawString(codeInfo.BarCode, codeInfo.FooterFont, Brushes.Black, footerX, yTop);
			}

            /// <summary>
            /// 
            /// </summary>
            /// <param name="g"></param>
            /// <param name="codeInfo"></param>
            public void DrawBarCode(Graphics g, RptBarCode codeInfo)
            {
                BarcodeLib.Barcode bar = new BarcodeLib.Barcode();
                bar.IncludeLabel = codeInfo.ShowFooter;
                
                try {
                    var barCode = bar.Encode(ShareLib.Convert(codeInfo.CodeType), codeInfo.BarCode, (int)codeInfo.Rect.Width, codeInfo.InnerRect.Height - 2);
                    g.DrawImage(barCode, new Point((int)codeInfo.LeftMargin - 3, (int)codeInfo.TopMargin - 1));
                }
                catch { }
            }
		}
		
		#region ....
//		class DrawCode39BarCode : IDrawBarCode{
//			private string alphabet39="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*";
//
//			private string [] coded39Char = {
//												/* 0 */ "000110100", 
//												/* 1 */ "100100001", 
//												/* 2 */ "001100001", 
//												/* 3 */ "101100000",
//												/* 4 */ "000110001", 
//												/* 5 */ "100110000", 
//												/* 6 */ "001110000", 
//												/* 7 */ "000100101",
//												/* 8 */ "100100100", 
//												/* 9 */ "001100100", 
//												/* A */ "100001001", 
//												/* B */ "001001001",
//												/* C */ "101001000", 
//												/* D */ "000011001", 
//												/* E */ "100011000", 
//												/* F */ "001011000",
//												/* G */ "000001101", 
//												/* H */ "100001100", 
//												/* I */ "001001100", 
//												/* J */ "000011100",
//												/* K */ "100000011", 
//												/* L */ "001000011", 
//												/* M */ "101000010", 
//												/* N */ "000010011",
//												/* O */ "100010010", 
//												/* P */ "001010010", 
//												/* Q */ "000000111", 
//												/* R */ "100000110",
//												/* S */ "001000110", 
//												/* T */ "000010110", 
//												/* U */ "110000001", 
//												/* V */ "011000001",
//												/* W */ "111000000", 
//												/* X */ "010010001", 
//												/* Y */ "110010000", 
//												/* Z */ "011010000",
//												/* - */ "010000101", 
//												/* . */ "110000100", 
//												/*' '*/ "011000100",
//												/* $ */ "010101000",
//												/* / */ "010100010", 
//												/* + */ "010001010", 
//												/* % */ "000101010", 
//												/* * */ "010010100" 
//											};
//			//绘制标准code39 条形码。
//			public  void DrawBarCode(Graphics g,RptBarCode codeInfo){
//				string intercharacterGap="0";
//				string str = '*' + codeInfo.BarCode.ToUpper() + '*';
//				int strLength = str.Length;
//
//				for ( int i=0;i<codeInfo.BarCode.Length ;i++ ) {
//					if (alphabet39.IndexOf(codeInfo.BarCode[i])==-1 || codeInfo.BarCode[i] == '*') {
//						g.DrawString("INVALID BAR CODE TEXT", codeInfo.HeaderFont, Brushes.Red, 10, 10);
//						return;
//					}
//				}
//
//				string encodedString="";
//
//				for ( int i=0;i<strLength;i++ ) {
//					if (i>0) 
//						encodedString+=intercharacterGap;
//					
//					encodedString+=coded39Char[alphabet39.IndexOf(str[i])];
//				}
//
//				int encodedStringLength = encodedString.Length;
//				int widthOfBarCodeString = 0;
//				double wideToNarrowRatio=3;
//			
//			
//				if (codeInfo.VertAlign != AlignType.Left) {
//					for ( int i=0;i<encodedStringLength; i++) {
//						if ( encodedString[i]=='1' ) 
//							widthOfBarCodeString+=(int)(wideToNarrowRatio*(int)codeInfo.Weight);
//						else 
//							widthOfBarCodeString+=(int)codeInfo.Weight;
//					}
//				}
//
//				int x = 0;
//				int wid=0;
//				int yTop = 0;
//				SizeF hSize = g.MeasureString(codeInfo.HeaderText, codeInfo.HeaderFont);
//				SizeF fSize = g.MeasureString(codeInfo.BarCode, codeInfo.FooterFont);
//
//				int headerX = 0;
//				int footerX = 0;
//
//				if (codeInfo.VertAlign == AlignType.Left) {
//					x = codeInfo.LeftMargin;
//					headerX = codeInfo.LeftMargin;
//					footerX = codeInfo.LeftMargin;
//				}
//				else if (codeInfo.VertAlign == AlignType.Center) {
//					x = (codeInfo.InnerRect.Width  - widthOfBarCodeString) / 2;
//					headerX = (codeInfo.InnerRect.Width - (int)hSize.Width) / 2;
//					footerX = (codeInfo.InnerRect.Width - (int)fSize.Width) / 2;
//				}
//				else {
//					x = codeInfo.InnerRect.Width - widthOfBarCodeString - codeInfo.LeftMargin;
//					headerX = codeInfo.InnerRect.Width - (int)hSize.Width - codeInfo.LeftMargin;
//					footerX = codeInfo.InnerRect.Width - (int)fSize.Width - codeInfo.LeftMargin;
//				}
//
//				if (codeInfo.ShowHeader) {
//					yTop = (int)hSize.Height + codeInfo.TopMargin;
//					g.DrawString(codeInfo.HeaderText, codeInfo.HeaderFont, Brushes.Black, headerX, codeInfo.TopMargin);
//				}
//				else {
//					yTop = codeInfo.TopMargin;
//				}
//
//				for ( int i=0;i<encodedStringLength; i++) {
//					if ( encodedString[i]=='1' ) 
//						wid=(int)(wideToNarrowRatio*(int)codeInfo.Weight);
//					else 
//						wid=(int)codeInfo.Weight;
//
//					g.FillRectangle(i%2==0? Brushes.Black : Brushes.White, x,yTop, wid, codeInfo.BarCodeHeight);
//					
//					x+=wid;
//				}
//
//				yTop += codeInfo.BarCodeHeight;
//
//				if (codeInfo.ShowFooter)
//					g.DrawString(codeInfo.BarCode, codeInfo.FooterFont, Brushes.Black, footerX, yTop);
//			}
//		}

		#endregion ....
	}
	#endregion 条形码绘制相关...

	#region 绘制子报表...
	public class DrawRptSubReport : DrawingRptObject{
		public override void Draw(Graphics g, DIYReport.Interface.IRptSingleObj dataObj) {
			RptSubReport subreport = dataObj as  RptSubReport;
			if(subreport==null)
				return;
		 
			string fieleName = RptDrawHelper.SUB_REPORT_FILE; 
			Image reportImage = RptDrawHelper.CreateBitmapFromResources(fieleName); 
			this.DrawImageUnscaled(g,subreport,reportImage);
			try{
				reportImage.Dispose();
			}
			catch{}
		}

	}
	#endregion 绘制子报表...

}
