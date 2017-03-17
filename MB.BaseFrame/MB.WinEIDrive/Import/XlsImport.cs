using System;
using System.Data;
using System.Collections;

using MB.WinEIDrive.Excel;
//using GemBox.ExcelLite; 
namespace MB.WinEIDrive.Import
{
	/// <summary>
	/// XlsImport 从Excel 中导入数据。
	/// </summary>
	public class XlsImport : ImportBase,IImportProvider 
	{
		#region 变量定义...
		private ExcelFile _ExcelObj;
		private DataTable _DtData;
		private Hashtable _ColumnAndIndex;
        private bool _DataTableIsAutoCreate;
		#endregion 变量定义...

		#region 构造函数...
		/// <summary>
		/// 构造函数...
		/// </summary>
		/// <param name="dtData"></param>
		/// <param name="fullFileName"></param>
		public XlsImport(object dtData,string fullFileName){
            if (dtData != null) {
                DataView dv = MB.WinEIDrive.DataHelpers.ToDataView(dtData);
                if (dv != null)
                    _DtData = dv.Table;
            }
			_ExcelObj = new ExcelFile();
 
			_ExcelObj.LoadXls( fullFileName );
			_ColumnAndIndex = new Hashtable();
            if (dtData == null) {
                _DtData = createDataTable();
                _DataTableIsAutoCreate = true;
            }
		}
		#endregion 构造函数...

		#region Public 函数...
		/// <summary>
		/// 执行数据导入的操作。
		/// </summary>
		/// <returns></returns>
		public virtual void Commit(){
            try {
                if (!_DataTableIsAutoCreate) {
                    bool b = setColumnAndIndex(_DtData);
                    if (!b)
                        return;
                }
                ExcelWorksheet sheet = _ExcelObj.Worksheets.ActiveWorksheet;
                //表示不存在任何记录。
                if (sheet.Rows.Count == 1)
                    return;
                int rowCount = sheet.Rows.Count;
                int colCount = sheet.Rows[0].AllocatedCells.Count;
                if (colCount == 0)
                    return;
                for (int i = 1; i < rowCount; i++) {
                    bool isEmptyRow = true;
                    ExcelRow dataRow = sheet.Rows[i];
                    DataRow newDr = _DtData.NewRow();
                    for (int j = 0; j < colCount; j++) {
                        if (!_ColumnAndIndex.ContainsKey(j))
                            continue;
                        DataColumn dc = (DataColumn)_ColumnAndIndex[j];
                        ExcelCell cell = dataRow.AllocatedCells[j];
                        object val = cell.Value;
                        if (val != null && !string.IsNullOrEmpty(val.ToString().TrimEnd()))
                            isEmptyRow = false;
                        OnProviderProgress(dc.ColumnName, ref val);
                        setRowValue(newDr, val, dc.ColumnName, dc.DataType);
                    }
                    //如果是空行的话，不加到返回的DataTable中
                    if (!isEmptyRow)
                        _DtData.Rows.Add(newDr);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
			return;
		}
		#endregion Public 函数...

        /// <summary>
        /// 获取正在导入的数据。
        /// </summary>
        public DataTable ImportData {
            get {
                return _DtData;
            }
        }
		
		#region 内部函数处理...
        //创建一个空的表结构
        private DataTable createDataTable() {
            ExcelWorksheet sheet = _ExcelObj.Worksheets.ActiveWorksheet;
            if (sheet.Rows.Count == 0)
                return null;
            ExcelRow headRow = sheet.Rows[0];
            DataTable newDt = new DataTable();
            int aCount = headRow.AllocatedCells.Count;
            for (int i = 0; i < aCount; i++) {
                ExcelCell cell = headRow.AllocatedCells[i];
                if (cell.Value == null)
                    continue;
                DataColumn dc = new DataColumn(cell.Value.ToString().Trim());
                dc.Caption = cell.Value.ToString().Trim();
                _ColumnAndIndex.Add(i, dc);
                newDt.Columns.Add(dc);
            }
            return newDt;
        }
		//根据Caption 找到datatable中的列,并把和Excel 中对应列的Index 一起存储在hashTable中作为
		//获取行导入时对应的列。
		private bool setColumnAndIndex(DataTable dt){
			ExcelWorksheet sheet = _ExcelObj.Worksheets.ActiveWorksheet;
			if(sheet.Rows.Count == 0)
				return false;
			ExcelRow headRow = sheet.Rows[0];
			int aCount = headRow.AllocatedCells.Count;
			for(int i=0; i < aCount; i++){
				ExcelCell cell = headRow.AllocatedCells[i];
				if(cell.Value==null)
					continue;
				foreach(DataColumn dc in dt.Columns){
					if(string.Compare(dc.Caption,cell.Value.ToString() ,true)!=0)
						continue;
					_ColumnAndIndex.Add(i,dc);
					break;
				}
			}
			return _ColumnAndIndex.Count > 0; 
		}
		//赋值操作. 
		private void setRowValue(DataRow drData,object val,string fieldName,Type dataType){
            if (val == null || val == System.DBNull.Value) return;
 
			try{
                if (dataType != null && string.Compare(dataType.Name, "Boolean", true) == 0)
                    drData[fieldName] = getBooleanPropertyValue(val);
                else
				    drData[fieldName] = System.Convert.ChangeType( val,dataType );
			}
			catch{
			}
		}
        //设置Boolean 值。
        private bool getBooleanPropertyValue(object val) {
            bool bVal = false;
            if (val != null && val != System.DBNull.Value) {
                bVal = string.Compare(val.ToString(), "1") == 0 || 
                       string.Compare(val.ToString(), "TRUE", true) == 0 ||
                       string.Compare(val.ToString(), "是", true) == 0 ||
                       string.Compare(val.ToString(), "选择", true) == 0 ||
                       string.Compare(val.ToString(), "Checked", true) == 0;
            }
            return bVal;
        }
		#endregion 内部函数处理...
		
	}
}

