using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Reflection;

namespace MB.WinEIDrive.Import
{
    public class OfficeXlsImport : ImportBase, IImportProvider 
    {
        #region 变量定义...
        private System.Data.DataTable _DtData;
        private Hashtable _ColumnAndIndex;
        private bool _DataTableIsAutoCreate;
        private DataTable _ExcelData;

		#endregion 变量定义...

		#region 构造函数...
		/// <summary>
		/// 构造函数...
		/// </summary>
		/// <param name="dtData"></param>
		/// <param name="fullFileName"></param>
        public OfficeXlsImport(object dtData, string fullFileName)
        {
            if (dtData != null) {
                DataView dv = MB.WinEIDrive.DataHelpers.ToDataView(dtData);
                if (dv != null)
                    _DtData = dv.Table;
            }

            LoadExcelToDataTable(fullFileName);

            _ColumnAndIndex = new Hashtable();
            if (dtData == null)
            {
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
                if (!_DataTableIsAutoCreate)
                {
                    bool b = setColumnAndIndex(_DtData);
                    if (!b)
                        return;
                }
                //表示不存在任何记录。
                if (_ExcelData.Rows.Count == 0)
                    return;
                int rowCount = _ExcelData.Rows.Count;
                int colCount = _ExcelData.Columns.Count;
                if (colCount == 0)
                    return;
                for (int i = 0; i < rowCount; i++)
                {
                    bool isEmptyRow = true;
                    DataRow newDr = _DtData.NewRow();
                    for (int j = 0; j < colCount; j++)
                    {
                        if (!_ColumnAndIndex.ContainsKey(j))
                            continue;
                        DataColumn dc = (DataColumn)_ColumnAndIndex[j];
                        object val = _ExcelData.Rows[i][j];
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
        /// <summary>  
        /// 加载Excel表到DataTable，跟原始Excel表形式一样，需要筛选自己有用的数据  
        /// </summary>  
        /// <param name="filename">需要读取的Excel文件路径</param>  
        /// <param name="sheetname">工作表名称</param>  
        /// <returns>DataTable</returns>  
        private void LoadExcelToDataTable(string filename)
        {
            //改成反射的方式调用
            object objAppClass = null, objWorkbooks = null, objWorkbook = null, objSheets = null;
            try
            {
                objAppClass = MB.Util.DllFactory.Instance.LoadObject("Microsoft.Office.Interop.Excel.ApplicationClass", "Microsoft.Office.Interop.Excel.dll");
                if (objAppClass == null) throw new MB.Util.APPException("加载程序集 [Microsoft.Office.Interop.Excel.dll] 失败!", Util.APPMessageType.DisplayToUser);

                objWorkbooks = MB.Util.MyReflection.Instance.InvokePropertyForGet(objAppClass, "Workbooks");
                object missing = System.Reflection.Missing.Value;
                object[] objs = { filename, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing };
                objWorkbook = objWorkbooks.GetType().InvokeMember("Open", BindingFlags.Default | BindingFlags.InvokeMethod, null, objWorkbooks, objs);

                objSheets = objWorkbook.GetType().InvokeMember("Sheets", BindingFlags.GetProperty, null, objWorkbook, null);

                Assembly assembly = Assembly.LoadFrom("Microsoft.Office.Interop.Excel.dll");
                Type type = assembly.GetType("Microsoft.Office.Interop.Excel.Sheets");

                int iCount = (int)objSheets.GetType().InvokeMember("Count", BindingFlags.GetProperty, null, objSheets, null);
                if (iCount <= 0) throw new MB.Util.APPException("Excel数据表为空，请检查导入模板!", Util.APPMessageType.DisplayToUser);

                object[] objParams = { 1 };

                object objSheet = type.InvokeMember("Item", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic, null, objSheets, objParams);
                string sheetName = (string)objSheet.GetType().InvokeMember("Name", BindingFlags.GetProperty, null, objSheet, null);

                //连接字符串  
                String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filename + ";" + "Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                OleDbConnection myConn = new OleDbConnection(sConnectionString);
                string strCom = " SELECT * FROM [" + sheetName + "$]";
                myConn.Open();

                _ExcelData = new DataTable();
                OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);
                myCommand.Fill(_ExcelData);
                myConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objSheets != null) objSheets = null;
                if(objWorkbook != null) objWorkbook.GetType().InvokeMember("Close", BindingFlags.Default | BindingFlags.InvokeMethod, null, objWorkbook, null);
                if (objWorkbooks != null) objWorkbooks.GetType().InvokeMember("Close", BindingFlags.Default | BindingFlags.InvokeMethod, null, objWorkbooks, null);
                if(objAppClass != null) objAppClass.GetType().InvokeMember("Quit", BindingFlags.Default | BindingFlags.InvokeMethod, null, objAppClass, null);
            }
            
        }
       #endregion Public 函数...

        /// <summary>
        /// 获取正在导入的数据。
        /// </summary>
        public System.Data.DataTable ImportData {
            get {
                return _DtData;
            }
        }
		
		#region 内部函数处理...
        //创建一个空的表结构
        private System.Data.DataTable createDataTable() {

            if (_ExcelData.Rows.Count == 0)
                return null;
            System.Data.DataTable newDt = new System.Data.DataTable();

            int aCount = _ExcelData.Columns.Count;
            for (int i = 0; i < aCount; i++) {
                object obj = _ExcelData.Columns[i].ColumnName;
                if (obj == null)
                    continue;
                DataColumn dc = new DataColumn(obj.ToString().Trim());
                dc.Caption = obj.ToString().Trim();
                _ColumnAndIndex.Add(i, dc);
                newDt.Columns.Add(dc);
            }
            return newDt;
        }
		//根据Caption 找到datatable中的列,并把和Excel 中对应列的Index 一起存储在hashTable中作为
		//获取行导入时对应的列。
		private bool setColumnAndIndex(System.Data.DataTable dt){
            if (_ExcelData.Rows.Count == 0)
				return false;
            int aCount = _ExcelData.Columns.Count;
			for(int i=0; i < aCount; i++){
                object obj = _ExcelData.Columns[i].ColumnName;
                if (obj == null)
					continue;
				foreach(DataColumn dc in dt.Columns){
					if(string.Compare(dc.Caption,obj.ToString() ,true)!=0)
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
