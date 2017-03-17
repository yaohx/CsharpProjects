using System;

namespace DIYReport.Interface
{
	/// <summary>
	/// IReportDataIO   
	/// </summary>
	public interface IReportDataIO
	{
		 
		 DIYReport.ReportModel.RptReport NewReport();
		 DIYReport.ReportModel.RptReport Open();
		 DIYReport.ReportModel.RptReport OpenReport(string pFullPath);
		 bool Save(DIYReport.ReportModel.RptReport pReport);
		 bool SaveReport(DIYReport.ReportModel.RptReport pReport,string pFullPath);
		 bool SaveReportAs(DIYReport.ReportModel.RptReport pReport,string pFullPath);


	}
}
