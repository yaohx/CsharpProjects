using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace MB.WinEIDrive.Import
{
    /// <summary>
    /// TxtImport : 从文件文件中导入数据
    /// </summary>
    public class TxtImport : ImportBase, IImportProvider 
    {
        #region 变量定义...
        private string[] _TxtFile;
        private DataTable _DtData;
        private Hashtable _ColumnAndIndex;
        private bool _DataTableIsAutoCreate;
        #endregion 变量定义...

        public TxtImport(object dtData, string fullFileName)
        {
            if (dtData != null)
            {
                DataView dv = MB.WinEIDrive.DataHelpers.ToDataView(dtData);
                if (dv != null)
                    _DtData = dv.Table;
            }

            _TxtFile = System.IO.File.ReadAllLines(fullFileName, Encoding.Default);

            _ColumnAndIndex = new Hashtable();
            if (dtData == null)
            {
                _DtData = createDataTable();
                _DataTableIsAutoCreate = true;
            }
        }

        public virtual void Commit()
        {
            try
            {
                if (!_DataTableIsAutoCreate)
                {
                    bool b = setColumnAndIndex(_DtData);
                    if (!b)
                        return;
                }
                //表示不存在任何记录。
                if (_TxtFile.Length == 1)
                    return;
                int rowCount = _TxtFile.Length;
                string[] heads = _TxtFile[0].Split('\t');
                int colCount = heads.Length;
                if (colCount == 0)
                    return;
                for (int i = 1; i < rowCount; i++)
                {

                    string dataRow = _TxtFile[i];
                    string[] rows = dataRow.Split('\t');
                    DataRow newDr = _DtData.NewRow();
                    for (int j = 0; j < colCount; j++)
                    {
                        if (!_ColumnAndIndex.ContainsKey(j))
                            continue;
                        DataColumn dc = (DataColumn)_ColumnAndIndex[j];
                        object val = rows[j];
                        OnProviderProgress(dc.ColumnName, ref val);
                        setRowValue(newDr, val, dc.ColumnName, dc.DataType);
                    }
                    _DtData.Rows.Add(newDr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }

        /// <summary>
        /// 获取正在导入的数据。
        /// </summary>
        public DataTable ImportData
        {
            get
            {
                return _DtData;
            }
        }

        #region 内部函数处理...
        //创建一个空的表结构
        private DataTable createDataTable()
        {            
            if (_TxtFile.Length == 0)
                return null;
            string headRow = _TxtFile[0];
            string[] heads = headRow.Split('\t');
            int aCount = heads.Length;
            DataTable newDt = new DataTable();

            for (int i = 0; i < aCount; i++)
            {
                if (heads[i] == null)
                    continue;
                DataColumn dc = new DataColumn(heads[i].Trim());
                dc.Caption = heads[i].Trim();
                _ColumnAndIndex.Add(i, dc);
                newDt.Columns.Add(dc);
            }
            return newDt;
        }
        //根据Caption 找到datatable中的列,并把和Excel 中对应列的Index 一起存储在hashTable中作为
        //获取行导入时对应的列。
        private bool setColumnAndIndex(DataTable dt)
        {
            string headRow = _TxtFile[0];
            string[] heads = headRow.Split('\t');
            int aCount = heads.Length;
            for (int i = 0; i < aCount; i++)
            {               
                if (heads[i] == null)
                    continue;
                foreach (DataColumn dc in dt.Columns)
                {
                    if (string.Compare(dc.Caption, heads[i].Trim(), true) != 0)
                        continue;
                    _ColumnAndIndex.Add(i, dc);
                    break;
                }
            }
            return _ColumnAndIndex.Count > 0;
        }
        //赋值操作. 
        private void setRowValue(DataRow drData, object val, string fieldName, Type dataType)
        {
            if (val == null || val == System.DBNull.Value) return;

            try
            {
                if (dataType != null && string.Compare(dataType.Name, "Boolean", true) == 0)
                    drData[fieldName] = getBooleanPropertyValue(val);
                else
                    drData[fieldName] = System.Convert.ChangeType(val, dataType);
            }
            catch
            {
            }
        }
        //设置Boolean 值。
        private bool getBooleanPropertyValue(object val)
        {
            bool bVal = false;
            if (val != null && val != System.DBNull.Value)
            {
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
