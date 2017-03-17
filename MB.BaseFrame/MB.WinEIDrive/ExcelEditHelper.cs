using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.WinEIDrive.Excel;
using System.Drawing;
using System.Data;
using System.Collections;
using MB.WinEIDrive.Import;

namespace MB.WinEIDrive {
    /// <summary>
    /// 为Excel编辑服务的帮助类
    /// </summary>
    public class ExcelEditHelper : ImportBase {

        private static readonly string SHEET_NAME = "WorkingSheet";
        private static readonly string COL_DEFAULT_KEY = "ID"; //缺省的键名称
        private string _FilePath;

        /// <summary>
        /// 构造excel编辑帮助类
        /// </summary>
        /// <param name="filePath">excel文件的路径</param>
        public ExcelEditHelper(string filePath) {
            _FilePath = filePath;
        }

        /// <summary>
        /// 把数据源加载进excel
        /// </summary>
        public void LoadDataAsExcel(string[] mainKeys, DataTable dataSource) {
            ExcelFile excelFile = new ExcelFile();
            // Check the file exists
            bool isNewFile = !System.IO.File.Exists(_FilePath);
            if (isNewFile) {
                excelFile.Worksheets.Add(SHEET_NAME);
            }
            else {
                excelFile.LoadXls(_FilePath);
            }
            ExcelWorksheet ws = clearLastDataInExcel(excelFile);
            fillDataInExcel(_FilePath, mainKeys, excelFile, ws, dataSource);
        }

        /// <summary>
        /// 提交excel中的数据到界面中
        /// </summary>
        /// <param name="dataSource">要提交的数据源</param>
        public void CommitExcel(object dataSource) {
            try {
                string fullFileName = _FilePath;
                DataTable dtData = null;
                if (dataSource != null) {
                    DataView dv = MB.WinEIDrive.DataHelpers.ToDataView(dataSource);
                    if (dv != null)
                        dtData = dv.Table;
                }
                ExcelFile excelObj = new ExcelFile();
                excelObj.LoadXls(fullFileName);
                Hashtable columnAndIndex = new Hashtable();
                bool b = setColumnAndIndex(dtData, excelObj, columnAndIndex);
                if (!b)
                    return;
                ExcelWorksheet sheet = excelObj.Worksheets.ActiveWorksheet;
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
                    DataRow newDr = dtData.NewRow();
                    for (int j = 0; j < colCount; j++) {
                        if (!columnAndIndex.ContainsKey(j))
                            continue;
                        DataColumn dc = (DataColumn)columnAndIndex[j];
                        ExcelCell cell = dataRow.AllocatedCells[j];
                        object val = null;
                        if (cell.FormulaInternal != null) 
                            val = cell.FormulaInternal.DecodeValue();
                        else
                            val = cell.Value;
                        if (val != null && !string.IsNullOrEmpty(val.ToString().TrimEnd()))
                            isEmptyRow = false;
                        OnProviderProgress(dc.ColumnName, ref val);
                        setRowValue(newDr, val, dc.ColumnName, dc.DataType);
                    }
                    //如果是空行的话，则跳出循环
                    if (!isEmptyRow)
                        dtData.Rows.Add(newDr);
                    else
                        break;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return;

        }
        #region 内部函数

        /// <summary>
        /// 清除前一次打开excel时残留的数据
        /// </summary>
        /// <param name="excelFile">Excel File编辑对象</param>
        /// <returns></returns>
        private ExcelWorksheet clearLastDataInExcel(ExcelFile excelFile) {
            ExcelWorksheet ws = excelFile.Worksheets[SHEET_NAME];
            object tmpValue = null;
            int colIndex = 0;
            tmpValue = ws.Cells[0, colIndex].Value;
            while (tmpValue != null && !string.IsNullOrEmpty(tmpValue.ToString().TrimEnd())) {
                //计算出有多少连续的列
                tmpValue = ws.Cells[0, ++colIndex].Value;
            }
            
            //通过读取CELL的值来确定那些行和列中有值
            for (int i = 0; i < ws.Rows.Count; i++) {
                bool emptyRow = true;
                for (int j = 0; j < colIndex; j++) {
                    tmpValue = ws.Cells[i, j].Value;
                    if (tmpValue != null && !string.IsNullOrEmpty(tmpValue.ToString().TrimEnd())) {
                        ws.Rows.DeleteInternal(i);
                        emptyRow = false;
                        i--; //一旦删除了一条，就要回到上一条
                        break;
                    }
                }
                if (emptyRow) break;
            }
            return ws;
        }


        /// <summary>
        /// 在当前的excel中填充数据
        /// 填充头和数据体
        /// </summary>
        /// <param name="filePath">excel路径</param>
        /// <param name="mainKeys">要编辑的对象的主键</param>
        /// <param name="excelFile">excel访问对象</param>
        /// <param name="ws">当前的工作薄</param>
        /// <param name="dt">需要填充的数据源</param>
        private void fillDataInExcel(string filePath, string[] mainKeys, ExcelFile excelFile, ExcelWorksheet ws, System.Data.DataTable dt) {
            //填充头
            ws.Rows[0].InsertEmpty(1);
            for (int i = 0; i < dt.Columns.Count; i++) {
                if (mainKeys.Contains(dt.Columns[i].ColumnName) ||
                    dt.Columns[i].ColumnName.CompareTo(COL_DEFAULT_KEY) == 0) {
                    ws.Columns[i].Hidden = true;
                    ws.Cells[0, i].Style.Font.Color = Color.Red;
                }
                else {
                    ws.Columns[i].Hidden = false;
                }
                ws.Cells[0, i].Value = dt.Columns[i].Caption;
                ws.Cells[0, i].Style.FillPattern.SetSolid(Color.LightGray);

                //foreach (IndividualBorder ib in Enum.GetValues(typeof(IndividualBorder))) {
                //    if (ib != IndividualBorder.DiagonalDown && ib != IndividualBorder.DiagonalUp) {
                //        ws.Cells[0, i].Style.Borders[ib].LineColor = Color.Black;
                //        ws.Cells[0, i].Style.Borders[ib].LineStyle = LineStyle.Thin;
                //    }
                //}


            }
            //填充数据
            int colCount = dt.Columns.Count;
            ws.Rows[1].InsertEmpty(dt.Rows.Count);
            for (int i = 0; i < dt.Rows.Count; i++) {
                for (int j = 0; j < colCount; j++) {
                    object val = dt.Rows[i][j];
                    if (val != null && val != System.DBNull.Value)
                        ws.Cells[i + 1, j].Value = val;
                }
            }
            excelFile.SaveXls(filePath);


        }

        //根据Caption 找到datatable中的列,并把和Excel 中对应列的Index 一起存储在hashTable中作为
        //获取行导入时对应的列。
        private bool setColumnAndIndex(DataTable dt, ExcelFile excelObj, Hashtable columnAndIndex) {
            ExcelWorksheet sheet = excelObj.Worksheets.ActiveWorksheet;
            if (sheet.Rows.Count == 0)
                return false;
            ExcelRow headRow = sheet.Rows[0];
            int aCount = headRow.AllocatedCells.Count;
            for (int i = 0; i < aCount; i++) {
                ExcelCell cell = headRow.AllocatedCells[i];
                if (cell.Value == null)
                    continue;
                foreach (DataColumn dc in dt.Columns) {
                    if (string.Compare(dc.Caption, cell.Value.ToString(), true) != 0)
                        continue;
                    columnAndIndex.Add(i, dc);
                    break;
                }
            }
            return columnAndIndex.Count > 0;
        }


        //赋值操作. 
        private void setRowValue(DataRow drData, object val, string fieldName, Type dataType) {
            if (val == null || val == System.DBNull.Value) return;

            try {
                if (dataType != null && string.Compare(dataType.Name, "Boolean", true) == 0)
                    drData[fieldName] = getBooleanPropertyValue(val);
                else
                    drData[fieldName] = System.Convert.ChangeType(val, dataType);
            }
            catch {
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
        #endregion

    }


}
