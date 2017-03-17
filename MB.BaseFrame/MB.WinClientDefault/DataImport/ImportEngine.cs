//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2009-06-08
// Description	:	数据导入处理引擎。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MB.WinClientDefault {
    /// <summary>
    ///  数据导入处理引擎。
    ///  特别说明：增加这个 引擎 而不直接使用  MB.WinEIDrive.Import.XlsImport  是为了提高功能的扩展性，
    ///  为以后能导入 .txt 等文件格式做准备。
    /// </summary>
    public class ImportEngine {
        #region 变量定义...
 
        private MB.WinEIDrive.Import.IImportProvider _ImportProvider;
        private static readonly string COL_DEFAULT_KEY = "ID"; //缺省的键名称，对于键值不做处理。
        private MB.XWinLib.XtraGrid.IGridExDataIOControl _DataIOControl;
        private DevExpress.XtraGrid.Views.Grid.GridView _GridView;
        private Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> _EditColumns;
        private Dictionary<string, Dictionary<string, object>> _LookUpData;
        #endregion 变量定义...

        #region 构造函数...
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="buiObj"></param>
        /// <param name="xtraGrid"></param>
        /// <param name="importProvider"></param>
        /// <param name="ignoreObjectKey"></param>
        public ImportEngine(Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editColumns, DevExpress.XtraGrid.GridControl xtraGrid,
                                            MB.WinEIDrive.Import.IImportProvider importProvider) {
         
            _EditColumns = editColumns;
            _LookUpData = convertTableToHasTable(_EditColumns);

            _GridView = xtraGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (_GridView == null)
                throw new MB.Util.APPException("目前只支持对GridView 控件的导入处理。", MB.Util.APPMessageType.DisplayToUser);

            _ImportProvider = importProvider;
            _ImportProvider.DataProgress += new MB.WinEIDrive.Import.ImportProgressEventHandler(_ImportProvider_DataProgress);

            MB.XWinLib.XtraGrid.GridControlEx gEx = xtraGrid as MB.XWinLib.XtraGrid.GridControlEx;
            if (gEx != null) {
                _DataIOControl = gEx.DataIOControl;
            }
            if (_DataIOControl == null)
                _DataIOControl = new MB.XWinLib.XtraGrid.GridExDataIOControl(true);
        }
        #endregion 构造函数...



        #region Public 函数...

        /// <summary>
        /// 提交数据导出操作。
        /// </summary>
        public void Commit() {
            if (_ImportProvider != null) {
                try {
                    bool b = _DataIOControl.BeforeIOData();
                    if (!b) return;
                    _ImportProvider.Commit();

                    _DataIOControl.AfterIOData();
                }
                catch (Exception e) {
                   throw new MB.Util.APPException("数据导入有误，请检查需要导入的文件是否在打开状态。", MB.Util.APPMessageType.DisplayToUser,e);
                }
            }
        }
        #endregion Public 函数...

        #region 导入事件处理...
        private void _ImportProvider_DataProgress(object sender, MB.WinEIDrive.Import.ImportProgressEventArgs e) {
            if (_DataIOControl.IgnoreKeyOnDataImport && string.Compare(e.FieldName, COL_DEFAULT_KEY, true) == 0) {
                e.NewValue = System.DBNull.Value;
                return;
            }
            //if (_GridView.Columns[e.FieldName] == null) { //过滤掉隐藏的字段，避免导入如： ID,DocTreeID 等内部处理字段。
            //    e.NewValue = System.DBNull.Value;
            //    return;
            //}
            e.NewValue = findIDByText(e.DataValue, e.FieldName);

        }
        #endregion 导入事件处理...

        #region 内部函数处理...
        //根据描述获取对应的ID值。
        private object findIDByText(object key, string fieldName) {
            if (key == null || key == System.DBNull.Value)
                return key;
            if (_EditColumns == null || _EditColumns.Count == 0)
                return key;

            if (!_LookUpData.ContainsKey(fieldName)) return key;

            var temp = _LookUpData[fieldName];
            if (temp.ContainsKey(key.ToString()))
                return temp[key.ToString()];
            else
                return key;


            //var colEdit = _EditColumns.Values.FirstOrDefault(o=>string.Compare(o.Name,fieldName,true)==0 );
            //if (colEdit==null || colEdit.DataSource == null)
            //    return key;

            //DataTable dtData = MB.Util.MyConvert.Instance.ToDataTable(colEdit.DataSource,string.Empty);
            //if (dtData == null)
            //    return key;
            //DataRow[] drs = dtData.Select(colEdit.TextFieldName + " = '" + key.ToString().Trim() + "'");
            //if (drs.Length == 0)
            //    return System.DBNull.Value;
            //else
            //    return drs[0][colEdit.ValueFieldName]; 

        }
        //把dataTable 的值转换为哈希表存储的值；
        private Dictionary<string, Dictionary<string, object>> convertTableToHasTable(Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editColumns) {
            Dictionary<string, Dictionary<string, object>> datas = new Dictionary<string, Dictionary<string, object>>();
            if (editColumns == null) return datas;
            foreach (MB.WinBase.Common.ColumnEditCfgInfo info in editColumns.Values) {
                if (info.DataSource == null) continue;
                  DataTable dtData = MB.Util.MyConvert.Instance.ToDataTable(info.DataSource,string.Empty);
                  if (dtData == null) continue;

                  DataRow[] drs = dtData.Select();
                  if (drs.Length == 0) continue;
                  if (!drs[0].Table.Columns.Contains(info.TextFieldName))
                      throw new MB.Util.APPException(string.Format("列 {0} 的下拉框配置中描述字段{1} 不属于对应的数据源", info.Name, info.TextFieldName));

                  if (!drs[0].Table.Columns.Contains(info.ValueFieldName))
                      throw new MB.Util.APPException(string.Format("列 {0} 的下拉框配置中值字段{1} 不属于对应的数据源", info.Name, info.ValueFieldName));
 

                  Dictionary<string, object> hasTable = new Dictionary<string, object>();
                  foreach (DataRow dr in drs) {
                      if (dr[info.TextFieldName] == System.DBNull.Value) continue;

                      string key = dr[info.TextFieldName].ToString();
                      if (hasTable.ContainsKey(key)) continue;
 
                      hasTable.Add(key,dr[info.ValueFieldName]); 
                  }
                  datas.Add(info.Name, hasTable);   
            }
            return datas;
        }
        #endregion 内部函数处理...

        #region IDisposable 成员
        /// <summary>
        /// Dispose...
        /// </summary>
        public void Dispose() {
            if (_ImportProvider != null) {
                try {
                    _ImportProvider.DataProgress -= new MB.WinEIDrive.Import.ImportProgressEventHandler(_ImportProvider_DataProgress);
                }
                catch {
                }
            }
        }
        #endregion
    }

}
