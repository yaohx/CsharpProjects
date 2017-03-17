using System;

namespace MB.WinEIDrive.Import
{
	/// <summary>
	/// IImportProvider 数据导入接口说明。
	/// </summary>
	public interface IImportProvider
	{
		void Commit();

		event ImportProgressEventHandler DataProgress;
	}
}
