//---------------------------------------------------------------- 
// Author		:	Nick
// Create date	:	2009-02-13
// Description	:	XPrint XWin 打印对象。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Drawing;

using DevExpress.XtraPrinting;
namespace MB.XWinLib.Print
{
	/// <summary>
	/// XPrint XWin 打印对象。
	/// </summary>
	public class XPrint
	{
		#region 变量定义...
		private DevExpress.XtraPrinting.PrintingSystem _PS;
		private DevExpress.XtraPrinting.PrintableComponentLink _PrintableLink;
		private XPrintParam _PrintParam;

		private static XPrint _PrintObj;

		private const int REPORT_HEADER_HEIGHT = 50;
		#endregion 变量定义...
		
		#region 构造函数...

		#endregion 构造函数...

       #region Instance...
        private static Object _Obj = new object();
        private static XPrint _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        /// <summary>
        /// 构造函数...
        /// </summary>
        protected XPrint() {
            _PS = new DevExpress.XtraPrinting.PrintingSystem();
            _PrintableLink = new DevExpress.XtraPrinting.PrintableComponentLink(_PS);
           
        }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static XPrint Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new XPrint();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

		#region public 方法...
		/// <summary>
		/// 显示预览窗口。
		/// </summary>
		/// <param name="printable"></param>
		public void ShowPreview(DevExpress.XtraPrinting.IPrintable printable,XPrintParam printParam){
			_PrintParam = printParam;
			CreateLink(printable);
			_PrintableLink.ShowPreview();
		}
		#endregion public 方法...
		
		#region protected virtual 函数...
		protected virtual object CreateLink(DevExpress.XtraPrinting.IPrintable printable) {
			if(_PrintParam!=null)
				_PrintableLink.PrintingSystem.PageSettings.Landscape = _PrintParam.Landscape;
			if(_PrintableLink == null) return null;
			_PrintableLink.Component = printable;
			_PrintableLink.CreateDocument();
			return _PrintableLink;
		}
		#endregion protected virtual 函数...
		
		#region 对象事件...
		private void _PrintableLink_CreatePageHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e) {

			if(_PrintParam!=null && _PrintParam.PageHeaderTitle!=null && _PrintParam.PageHeaderTitle.Length > 0){
				RectangleF r = new RectangleF(0, 0,e.Graph.ClientPageSize.Width, e.Graph.Font.Height);
				TextBrick brick = e.Graph.DrawString(_PrintParam.PageHeaderTitle, Color.Blue, r, BorderSide.None);
				brick.StringFormat = new BrickStringFormat(StringAlignment.Far);
				 
			}
		}

		private void _PrintableLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e) {
			if(_PrintParam!=null && _PrintParam.ReportHeaderTitle!=null && _PrintParam.ReportHeaderTitle.Length > 0){

				RectangleF r = new RectangleF(0, 0,e.Graph.ClientPageSize.Width, REPORT_HEADER_HEIGHT);
				TextBrick brick = e.Graph.DrawString(_PrintParam.ReportHeaderTitle, Color.Black, r, BorderSide.None);
				brick.StringFormat = new BrickStringFormat(StringAlignment.Center);
				brick.Font = getDefaultFont(12F,FontStyle.Bold);

			}
		}

		private void _PrintableLink_CreatePageFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e) {
			string format = "当前页:{0}  总页数 {1}";
			e.Graph.Font = e.Graph.DefaultFont;
			e.Graph.BackColor = Color.Transparent;

			RectangleF r = new RectangleF(0, 0, 0, e.Graph.Font.Height);

			PageInfoBrick brick = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, format, Color.Black, r, BorderSide.None);
			brick.Alignment = BrickAlignment.Far;
			brick.AutoWidth = true;

			brick = e.Graph.DrawPageInfo(PageInfo.DateTime, "", Color.Black, r, BorderSide.None);
			brick.Alignment = BrickAlignment.Near;
			brick.AutoWidth = true;
		}
		#endregion 对象事件...

		#region 内部函数处理...
		//获取默认设置的字体
		private Font getDefaultFont(float size,System.Drawing.FontStyle style){
			return new Font("Microsoft Sans Serif",size,style, System.Drawing.GraphicsUnit.Point);
		}
		
		#endregion 内部函数处理...
	}


}
