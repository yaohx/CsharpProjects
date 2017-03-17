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
using System.ComponentModel.Design;
namespace DIYReport.ReportModel
{
	/// <summary>
	///  事件委托申明
	/// </summary>
	public delegate void RptEventHandler(object sender, RptEventArgs e);

	/// <summary>
	/// RptEventArgs 报表操作对象事件参数。
	/// </summary>
	public class RptEventArgs: EventArgs {
	}

	#region 报表IO 操作处理的自定义事件声明...
	public delegate void XReportIOEventHandler(object sender,XReportIOEventArgs e);
	
	public class XReportIOEventArgs : EventArgs{
		private RptReport _DataReport;
		private bool _HasProcessed;
		private CommandID  _CommandID;
		public XReportIOEventArgs(RptReport dataReport,CommandID  commandID){
			_DataReport = dataReport;
			_CommandID = commandID;
		}

		#region public 属性...
		public RptReport DataReport{
			get{
				return _DataReport;
			}
			set{
				_DataReport = value;
			}
		}
		public bool HasProcessed{
			get{
				return _HasProcessed;
			}
			set{
				_HasProcessed = value;
			}
		}
		public CommandID CommandID{
			get{
				return _CommandID;
			}
			set{
				_CommandID = value;
			}
		}
		#endregion public 属性...

	}
	#endregion 报表IO 操作处理的自定义事件声明...

//	public enum XReportIOHandlerType{
//		NewReport,
//		Open,
//		Save,
//		SaveAs,
//	}
}
