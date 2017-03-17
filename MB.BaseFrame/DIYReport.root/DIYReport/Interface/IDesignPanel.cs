using System;

namespace DIYReport.Interface
{
	/// <summary>
	/// IDesignPanel 的摘要说明。
	/// </summary>
	public interface IDesignPanel : IDisposable
	{
		object CurrentObj{get;}
		DIYReport.UserDIY.DesignSectionList SectionList{get;set;}
		DIYReport.ReportModel.RptReport DataObj{get;set;}
		DIYReport.Interface.IReportDataIO  ReportIO{get;set;}
		DIYReport.UndoManager.UndoMgr UndoMgr{get;set;}

		DIYReport.ReportModel.RptReport OpenReport(DIYReport.ReportModel.RptReport report);
		DIYReport.ReportModel.RptReport CreateNewReport();

		void DeleteSelectedControls();

		System.Windows.Forms.Form ParentForm{get;}

		void Cut();
		void Copy();
		void Past();




	}
}
