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
using System.Drawing ;
using System.Windows.Forms;
using System.Data ;

namespace DIYReport.Print {
	/// <summary>
	/// SwPrintView打印预览和控制。
	/// </summary>
	public class SwPrintView : IDisposable  {

		#region  变量定义...
		private System.Windows.Forms.PrintPreviewDialog _PreviewDialog;
		private System.Drawing.Printing.PrintDocument _PrintDocument;
		private DIYReport.Interface.IDrawReport  _DrawObj;
		#endregion  变量定义...
		
		#region 构造函数...
		/// <summary>
		///  打印预览和控制 
		/// </summary>
		/// <param name="pDs"></param>
		/// <param name="pParams"></param>
		/// <param name="pReportName"></param>
		public SwPrintView(object pDs,DIYReport.ReportModel.RptReport pReport) {
			_DrawObj = new DrawReport(pDs,pReport);
			_PrintDocument = pReport.PrintDocument;
			iniForm();
		}
		public SwPrintView(DIYReport.Interface.IDrawReport  pDrawObj) {
			_DrawObj = pDrawObj;
			_PrintDocument = _DrawObj.DataReport.PrintDocument;
			iniForm();
		}
		#endregion 构造函数...

		#region 内部处理 (画打印的数据)...
		private void iniForm() {
			this._PreviewDialog = new System.Windows.Forms.PrintPreviewDialog();

			this._PreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this._PreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this._PreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
			this._PreviewDialog.Enabled = true;
			
			this._PreviewDialog.Location = new System.Drawing.Point(44, 58);
			this._PreviewDialog.MinimumSize = new System.Drawing.Size(400, 300);
			this._PreviewDialog.Name = "printPreviewDialog1";
			this._PreviewDialog.TransparencyKey = System.Drawing.Color.Empty;
			this._PreviewDialog.Visible = false;

			_PreviewDialog.Document = _PrintDocument;
			_PrintDocument.BeginPrint +=new System.Drawing.Printing.PrintEventHandler(_PrintDocument_BeginPrint);
			_PrintDocument.PrintPage +=new System.Drawing.Printing.PrintPageEventHandler(_PrintDocument_PrintPage);
		}
		//在开始打印之前所处理的数据
		private void _PrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
			//DIYReport.Print.MyPrinterSettings.ChangeSetting(_PrintDocument,"Microsoft Office Document Image Writer",System.Convert.ToInt16(400),System.Convert.ToInt16(600));
			_DrawObj.BeginPrint();

		}
		//浏览或者打印页时响应的事件 
		private void _PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			Graphics	g = e.Graphics;
			//nick 2006-01-02 如果希望每页只绘制一次的话，那么需要在这里进行判断，以决定在那里进行绘制。
			_DrawObj.DrawReportSection(g,DIYReport.SectionType.ReportTitle);  
			_DrawObj.DrawReportSection(g,DIYReport.SectionType.PageHead);
			bool more = _DrawObj.DrawReportSection(g,DIYReport.SectionType.Detail);
			if (more == true) {
				_DrawObj.DrawReportSection(g,DIYReport.SectionType.PageFooter);
				e.HasMorePages = true;
				_DrawObj.PageNumber++;
			}
			else {
				_DrawObj.DrawReportSection(g,DIYReport.SectionType.PageFooter);
				_DrawObj.DrawReportSection(g,DIYReport.SectionType.ReportBottom); 
			}
		}
		#endregion 内部处理...

		#region Public ...
		/// <summary>
		/// 打印 啊！  
		/// </summary>
		public void Printer() {
			//mDReport.PageNumber++;
			_DrawObj.PageNumber = 1;
			_DrawObj.HasDrawRowCount = 0;
			try{
//				if (_PreviewDialog.ShowDialog() == DialogResult.OK) {
//					_PrintDocument.Print();
//				}
				//PrintDialog dlg = new PrintDialog() ;
				//dlg.Document = _PrintDocument;
				//DialogResult result = dlg.ShowDialog();
				//if (result == DialogResult.OK) {
					_PrintDocument.Print();
				//}
			}
			catch{
				MessageBox.Show("打印出错，请检查本地或者网络打印机是否正确安装！","打印提示",MessageBoxButtons.OK ,MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// 显示打印预览
		/// </summary>
		public void ShowPreview() {
			try{
				if (_PreviewDialog.ShowDialog() == DialogResult.OK) {
				}
			}
			catch{
				MessageBox.Show("打印出错，请检查本地或者网络打印机是否正确安装！","打印提示",MessageBoxButtons.OK ,MessageBoxIcon.Information);
    
			}
		}

		/// <summary>
		/// 设置工作区域
		/// </summary>
		/// <param name="pRct"></param>
		public System.Drawing.Rectangle PreviewDialogBounds {
			set {
				_PreviewDialog.Bounds = value;
			}
		}
				
		#endregion Public ...

		#region IDisposable 成员

		public void Dispose() {
			_PrintDocument.BeginPrint -=new System.Drawing.Printing.PrintEventHandler(_PrintDocument_BeginPrint);
			_PrintDocument.PrintPage -=new System.Drawing.Printing.PrintPageEventHandler(_PrintDocument_PrintPage);

		}

		#endregion
	}
}
