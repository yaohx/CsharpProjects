//---------------------------------------------------------------- 
// Author		:	Nick
// Create date	:	2009-02-13
// Description	:	XPrintParam XPrint 需要的参数设置信息。
//                  主要是设置报表的标题 和公司的Loger
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;

namespace MB.XWinLib.Print
{
	/// <summary>
	/// XPrintParam XPrint 需要的参数设置信息。
	/// </summary>
	public class XPrintParam
	{
		private string _ReportHeaderTitle;
		private string _PageHeaderTitle;
		private bool _Landscape;
		
		/// <summary>
		/// 构造函数
		/// </summary>
		public XPrintParam(string reportTitle,string pageHeader)
		{
			_ReportHeaderTitle = reportTitle;
			_PageHeaderTitle = pageHeader;
			_Landscape = false;
		}

		#region Public 属性...
		public string ReportHeaderTitle{
			get{
				return _ReportHeaderTitle;
			}
			set{
				_ReportHeaderTitle = value;
			}
		}
		public string PageHeaderTitle{
			get{
				return _PageHeaderTitle;
			}
			set{
				_PageHeaderTitle = value;
			}
		}
		public bool Landscape{
			get{
				return _Landscape;
			}
			set{
				_Landscape = value;
			}
		}
		#endregion Public 属性...
	}
}
