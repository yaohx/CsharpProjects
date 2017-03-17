using System;
using System.Data;

using MB.WinEIDrive;
using MB.WinEIDrive.Excel; 
namespace MB.WinEIDrive.Export
{
	/// <summary>
	/// ExportXls 导出Excel 操作。
	/// </summary>
	public class ExportXls : ExportBase, IExportProvider 
	{
		#region 变量定义...
		private string _FileName;
		private bool _OpenExportFile;

		private static readonly string SHEET_NAME = "Crea.D Export";
		private const int DEFAULT_WIDTH = 3000;
		#endregion 变量定义...

		#region 构造函数...
		/// <summary>
		/// 提供把相应的数据源导出到Excel 文件中。
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="openExportFile"></param>
		public ExportXls(string fileName,bool openExportFile): this(null,fileName,openExportFile){
		}
		/// <summary>
		/// 把DataSet 导出到指定的Excel 文件中。
		/// </summary>
		/// <param name="dsData"></param>
		/// <param name="fullFileName"></param>
		/// <param name="openExportFile"></param>
		public ExportXls(object data,string fileName,bool openExportFile)
		{
			base.DataSource = data;
			_FileName = fileName;
			_OpenExportFile = openExportFile;
		}
		#endregion 构造函数...

		#region 实现接口 IExportProvider 的方法...
		/// <summary>
		/// 提交数据导出的操作。
		/// </summary>
		public virtual void Commit(){
			DataView dv = MB.WinEIDrive.DataHelpers.ToDataView(base.DataSource);
			if(dv == null)
				return;
			ExcelFile excelFile = new ExcelFile();
			ExcelWorksheet ws = excelFile.Worksheets.Add(SHEET_NAME);
			for(int i = 0 ; i <dv.Table.Columns.Count;i++){
				ExportColumnsEventArgs arg = new ExportColumnsEventArgs(dv.Table.Columns[i].Caption,dv.Table.Columns[i].ColumnName,DEFAULT_WIDTH); 
				OnColumnProgress(arg);
				ws.Columns[i].Hidden = arg.IsHide;
				ws.Columns[i].Width = arg.ColumnWidth;
				ws.Cells[0,i].Value  = arg.Caption;  
			}
			int colCount = dv.Table.Columns.Count; 
			for(int j = 0 ; j < dv.Count; j++){ 
				for(int k = 0 ; k < colCount;k++){
					object val = dv[j][k];

					OnProviderProgress(dv.Table.Columns[k].ColumnName,j,ref val);

					if(val!=null && val !=System.DBNull.Value) 
						ws.Cells[j+1,k].Value = val;
				}
			}
			excelFile.SaveXls(_FileName);
			if(_OpenExportFile)
				MB.WinEIDrive.FileLib.TryToOpenFile(_FileName);
		}
		#endregion 实现接口 IExportProvider 的方法...
		// 
	}
}
