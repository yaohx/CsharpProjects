//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-22
// Description	:	 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Drawing;
using System.Diagnostics ;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace DIYReport.Print
{
	/// <summary>
	/// RptPageSetting 打印页面设置
	/// </summary>
	public class RptPageSetting
	{
		//1 inch 英寸=25.4 millimetres 毫米
		private static double SEP_MINI = System.Convert.ToDouble(65 / 25.4f); // System.Convert.ToDecimal(3.938)  ;// 
		/// <summary>
		/// 调用页面设置的窗口
		/// </summary>
		/// <param name="printDocument"></param>
		/// <returns></returns>
		public  static void ShowPageSetupDialog(DIYReport.ReportModel.RptReport pReport) {
			PageSettings ps = new PageSettings();
 
			try {
				PageSetupDialog psDlg = new PageSetupDialog();
				psDlg.Document = pReport.PrintDocument;
				
				//pReport.PrintDocument.DefaultPageSettings.Margins = pReport.Margins;
				pReport.PrintDocument.DefaultPageSettings.Margins  = new System.Drawing.Printing.Margins( pReport.Margins.Left,
					pReport.Margins.Right,
					pReport.Margins.Top ,
					pReport.Margins.Bottom);

				psDlg.PageSettings = pReport.PrintDocument.DefaultPageSettings ;
				//psDlg.AllowPaper = true;
				DialogResult result = psDlg.ShowDialog();
				if (result == DialogResult.OK) {
					ps = psDlg.PageSettings;
					pReport.PrintDocument.DefaultPageSettings = psDlg.PageSettings;
					pReport.Margins =   convertTomini(psDlg.PageSettings.Margins) ;
					pReport.PrintDocument.DefaultPageSettings.Margins =  pReport.Margins ;
					pReport.PaperSize = psDlg.PageSettings.PaperSize ;
					pReport.IsLandscape = psDlg.PageSettings.Landscape ;
				}
			}
			catch(System.Drawing.Printing.InvalidPrinterException e) {
				MessageBox.Show("未安装打印机，请进入系统控制面版添加打印机！" ,"打印",MessageBoxButtons.OK,MessageBoxIcon.Information);
				Debug.Assert(false,e.Message ,"打印");
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message,"打印",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}

		//	return ps;
//
//			FrmPageSetting frm = new FrmPageSetting(pReport);
//			frm.ShowDialog(); 
		}
		/// <summary>
		/// 根据名称得到PaperSize (该方法除了已经停止使用的FrmPageSetting外，基本上已经不再使用)
		/// </summary>
		/// <param name="pDoc"></param>
		/// <param name="pPaperName"></param>
		/// <returns></returns>
		public static System.Drawing.Printing.PaperSize GetPaperSizeByName(System.Drawing.Printing.PrintDocument pDoc,
											string printName,string pPaperName){
			System.Drawing.Printing.PrintDocument doc =pDoc;
			if(doc==null){
				doc = new PrintDocument();
			}
			if(printName!=null && printName.Length >0)
				doc.PrinterSettings.PrinterName = printName;
 
			for(int i  = 0; i < doc.PrinterSettings.PaperSizes.Count;i ++){
				string paName = doc.PrinterSettings.PaperSizes[i].PaperName ;
				if(paName == pPaperName){
					return doc.PrinterSettings.PaperSizes[i];
				}
			}
			
			//找不到的话返回一个A4的格式纸张
			return new PaperSize(pPaperName,210,297); 
		}
		/// <summary>
		/// 通过完整的信息获取paperSize.
		/// </summary>
		/// <param name="pDoc"></param>
		/// <param name="printerName"></param>
		/// <param name="paperName"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static System.Drawing.Printing.PaperSize GetSizeByFullInfo(System.Drawing.Printing.PrintDocument pDoc,
				string printerName,string paperName,int width,int height){
			/*
			//根据指定的打印机和打印纸张名称获取，
			//如果获取不到再根据打印纸张名称来获取，
			//如果再获取不到再根据 width 和height 来获取，
			//如果再获取不到再根据 width 和height获取相近的纸张。
			*/
			System.Drawing.Printing.PrintDocument doc = new System.Drawing.Printing.PrintDocument();
            int count = 0;
            try {
                count = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count;
            }
            catch (Exception ex) {
                throw new Exception("获取安装的打印机信息有误 " + ex.Message);

            }
			int SEP_WIDTH = 2;
			bool existsPrinter = false;
            if (!string.IsNullOrEmpty(printerName)) {
                for (int i = 0; i < count; i++) {
                    string print = System.Drawing.Printing.PrinterSettings.InstalledPrinters[i];
                    if (string.Compare(print, printerName, true) == 0) {
                        existsPrinter = true;
                        break;
                    }
                }
            }
			if(existsPrinter){
				doc.PrinterSettings.PrinterName = printerName;
				foreach(System.Drawing.Printing.PaperSize size in doc.PrinterSettings.PaperSizes){
					if(string.Compare(size.PaperName,paperName,true)==0){
						return size;
					}
				}
			}
            if (!string.IsNullOrEmpty(paperName)) {
                //如果上面的代码获取不到，那么获取纸张名称相同并且大小相同的
                for (int i = 0; i < count; i++) {
                    string print = System.Drawing.Printing.PrinterSettings.InstalledPrinters[i];
                    doc.PrinterSettings.PrinterName = print;
                    foreach (System.Drawing.Printing.PaperSize size in doc.PrinterSettings.PaperSizes) {
                        if (string.Compare(size.PaperName, paperName, true) == 0 && (size.Width == width || width == 0) && (size.Height == height || height == 0)) {
                            return size;
                        }
                    }
                }
            }
			//如果再获取不到再根据 width 和height 来获取，
			for(int i = 0 ;i <count; i++){
				string print = System.Drawing.Printing.PrinterSettings.InstalledPrinters[i];
				doc.PrinterSettings.PrinterName = print;
				foreach(System.Drawing.Printing.PaperSize size in doc.PrinterSettings.PaperSizes){
					if(size.Width == width && size.Height == height ){
						return size;
					}
				}
			}
			//如果再获取不到再根据 width 和height获取相近的纸张。
			for(int i = 0 ;i <count; i++){
				string print = System.Drawing.Printing.PrinterSettings.InstalledPrinters[i];
				doc.PrinterSettings.PrinterName = print;
				foreach(System.Drawing.Printing.PaperSize size in doc.PrinterSettings.PaperSizes){
					if((size.Width - SEP_WIDTH < width && width < size.Width + SEP_WIDTH)  && 
						size.Height - SEP_WIDTH < height && height < size.Height + SEP_WIDTH ){
						return size;
					}
				}
			}
			//找不到的话返回一个A4的格式纸张。
			MessageBox.Show("在该计算机中找不到设计的打印机或者纸张类型，请重新绑定设计。");
			return new PaperSize(paperName,210,297); 
		}
		#region 内部自动处理...
		//根据

		//娘的，这他妈的肯定是microsoft 的一个 bug!
		//怎么换算都不符合实际显示的数字！只能先把它当做一个bug留在代码中
		private static  Margins convertTomini(Margins pMar){
			Margins  m = new Margins();
			m.Left = System.Convert.ToInt32(pMar.Left * SEP_MINI);
			m.Right = System.Convert.ToInt32(pMar.Right * SEP_MINI);
			m.Top  = System.Convert.ToInt32(pMar.Top * SEP_MINI);
			m.Bottom = System.Convert.ToInt32(pMar.Bottom * SEP_MINI);

			return m;
		}
		#endregion 内部自动处理...
	}
}
