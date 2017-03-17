//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-04-17
// Description	:	在打印或者浏览的时候绘制报表所需要的对象。 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------

using System.Drawing;
using DIYReport.ReportModel.RptObj ; 
using DIYReport.ReportModel;
using System.Collections;
using System.Diagnostics ;
using DIYReport.Barcode;
using System.Drawing.Imaging;

namespace DIYReport.Extend.Print
{
	/// <summary>
	/// PrintDrawHelper 在打印或者浏览的时候绘制报表所需要的对象。 
	/// </summary>
	public class PrintDrawHelper
	{
		public PrintDrawHelper()
		{
		}
	}

	public	class DrawCodeBarCode {
		private static DrawCodeBarCode _DrawObj;
		public static DrawCodeBarCode Instance(){
			if(_DrawObj==null)
				_DrawObj = new DrawCodeBarCode();
			return _DrawObj;
		}
			//绘制标准 条形码。
			public  void DrawBarCode(DevExpress.XtraPrinting.BrickGraphics  g,RptBarCode codeInfo,string barCode,Rectangle realDrawRect){
				if(barCode==null || barCode.Length ==0)
					return;				
				DevExpress.XtraPrinting.BorderSide borSide = DevExpress.XtraPrinting.BorderSide.None;
				Barcode.IBarcode barCodeObj = null;
				if(codeInfo.CodeType==BarCodeType.Code128)   
					barCodeObj = new DIYReport.Barcode.Code128(barCode);
				else
					barCodeObj = new DIYReport.Barcode.Code39(barCode);


				string encodedString = barCodeObj.Encoded_Value ;
				if(encodedString==null || encodedString.Length ==0)
					g.DrawString("INVALID BAR CODE TEXT", Color.Red,new RectangleF(realDrawRect.X,realDrawRect.Y,realDrawRect.Width,realDrawRect.Height),borSide);


				int encodedStringLength = encodedString.Length;
				float widthOfBarCodeString = 0f;
                //float wideToNarrowRatio = 1.67f;
                //float wid = codeInfo.WID * wideToNarrowRatio;
                float pound = codeInfo.LinePound;
			
				if (codeInfo.VertAlign != AlignType.Left) {
					for ( int i=0;i<encodedStringLength; i++) {
						//if ( encodedString[i]=='1' ) 
                        widthOfBarCodeString += pound; 
                      
					}
				}

				float xLeft = 0f;
				int yTop = 0;
				g.Font = codeInfo.HeaderFont;
				SizeF hSize = g.MeasureString(codeInfo.HeaderText);
				g.Font = codeInfo.FooterFont ;
				SizeF fSize = g.MeasureString(barCode);

				int headerX = 0;
				int footerX = 0;

				if (codeInfo.VertAlign == AlignType.Left) {
					xLeft = realDrawRect.X + codeInfo.LeftMargin;
					headerX = realDrawRect.X + codeInfo.LeftMargin;
					footerX = realDrawRect.X + codeInfo.LeftMargin;
				}
				else if (codeInfo.VertAlign == AlignType.Center) {
                   // xLeft = realDrawRect.X + (realDrawRect.Width - widthOfBarCodeString * pound) / 2;
                    xLeft = realDrawRect.X + (realDrawRect.Width - widthOfBarCodeString ) / 2;
					headerX = realDrawRect.X + (realDrawRect.Width - (int)hSize.Width) / 2;
					footerX = realDrawRect.X + (realDrawRect.Width - (int)fSize.Width) / 2;
				}
				else {
                    xLeft = realDrawRect.X + realDrawRect.Width - widthOfBarCodeString * pound - codeInfo.LeftMargin;
					headerX = realDrawRect.X + realDrawRect.Width - (int)hSize.Width - codeInfo.LeftMargin;
					footerX = realDrawRect.X + realDrawRect.Width - (int)fSize.Width - codeInfo.LeftMargin;
				}

				if (codeInfo.ShowHeader) {
					yTop = (int)hSize.Height + codeInfo.TopMargin;
					g.Font = codeInfo.HeaderFont;
					g.DrawString(codeInfo.HeaderText,Color.Black,new RectangleF(headerX,realDrawRect.Y + codeInfo.TopMargin,realDrawRect.Width,hSize.Height-4),borSide);
				}
				else {
					yTop = codeInfo.TopMargin;
				}
                float px = xLeft;// +realDrawRect.X; 
              
				for ( int i = 0 ;i< encodedStringLength; i++) {
 
                    Color c = encodedString[i] == '1' ? Color.Black : Color.White;

                    px += pound;
                    var brick = g.DrawLine(new PointF(px, yTop + realDrawRect.Y),
                               new PointF(px, yTop + realDrawRect.Y + codeInfo.BarCodeHeight), c, pound);

                    brick.Sides = DevExpress.XtraPrinting.BorderSide.None; 
					//x++; 
                    

				}

				yTop += codeInfo.BarCodeHeight;

				if (codeInfo.ShowFooter){
					//g.BackColor = codeInfo.BackgroundColor ;
					g.Font = codeInfo.FooterFont ;
					//g.DrawString(codeInfo.BarCode, codeInfo.FooterFont, Brushes.Black, footerX, yTop);
					var rec = g.DrawString(barCode,codeInfo.ForeColor,new RectangleF(footerX,realDrawRect.Y +  yTop,realDrawRect.Width,hSize.Height - 4), borSide);
                    //rec.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
				}
					
			}

            public void DrawBarCodeWithLib(DevExpress.XtraPrinting.BrickGraphics g, RptBarCode codeInfo, string barCode, Rectangle realDrawRect)
            {
                BarcodeLib.Barcode bar = new BarcodeLib.Barcode();
                //bar.BackColor = Color.White;\
                bar.ImageFormat = ImageFormat.Gif;
                bar.IncludeLabel = codeInfo.ShowFooter;
                try {
                    var rect = new RectangleF( realDrawRect.X,realDrawRect.Y + 1,realDrawRect.Width,realDrawRect.Height - 1);
                    g.DrawImage(bar.Encode(ShareLib.Convert(codeInfo.CodeType), barCode, (int)rect.Width, (int)rect.Height),
                                 rect, DevExpress.XtraPrinting.BorderSide.None,Color.White);
                }
                catch { }
            }
		}
 
}
