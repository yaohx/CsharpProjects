using System;

namespace MB.WinEIDrive.Export
{
	/// <summary>
	/// IExportProvider 数据导出的接口说明。
	/// </summary>
	public interface IExportProvider
	{
		object DataSource{get;set;}
		void Commit();

		event ExportProgressEventHandler DataProgress;
		event ExportColumnsEventHandler ColumnProgress;
	}
}
