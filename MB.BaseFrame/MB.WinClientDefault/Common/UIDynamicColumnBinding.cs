//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-17
// Description	:	编辑界面数据和编辑网格数据绑定。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;

using MB.WinBase.Common;
using MB.XWinLib.XtraGrid;
namespace MB.WinClientDefault.Common {
    /// <summary>
    /// 动态列的绑定处理。
    /// 同一个窗口，重新绑定时，注意必须要调用 Dispose
    /// </summary>
    public class UIDynamicColumnBinding<T> : IDisposable where T : class {

        #region 变量定义...
        private Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> _ColPropertys;

        private Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> _EditCols;
        private MB.XWinLib.XtraGrid.GridControlEx _XtraGrid;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView _AdvBandGridView;
        private string _CurrentXmlFileName;
        private MB.WinBase.Data.HViewDataConvert<T> _HViewConvertObject;
        private Dictionary<string, DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn> _DynamicBandColumns;

        private DataTable _CurrentEditTable;
        private static readonly string DYNAMIC_COL_LEFT_NAME = MB.BaseFrame.SOD.DYNAMIC_COL_LEFT_NAME;//   "~Active_";
        private List<T> _CurrentEditEntitys;
        private bool _ReadOnly;
        private Timer _Timer;
        #endregion 变量定义...

        #region 自定义事件处理相关...
        private System.EventHandler _AfterDataConvert;
        public event System.EventHandler AfterDataConvert {
            add {
                _AfterDataConvert += value; 
            }
            remove {
                _AfterDataConvert -= value;
            }
        }
        private void onAfterDataConvert() {
            if (_AfterDataConvert != null)
                _AfterDataConvert(this, new EventArgs());
        }
        #endregion 自定义事件处理相关...

        public bool ReadOnly {
            get {
                return _ReadOnly;
            }
            set {
                _ReadOnly = value;
            }
        }
        /// <summary>
        /// 绑定动态列处理相关。
        /// </summary>
        /// <param name="activeColumnIsBand"></param>
        /// <param name="activeColumnNames"></param>
        public UIDynamicColumnBinding(MB.XWinLib.XtraGrid.GridControlEx gridControl, string xmlFileName) {
            try {
                _XtraGrid = gridControl;
                _XtraGrid.BeforeContextMenuClick += new MB.XWinLib.XtraGrid.GridControlExMenuEventHandle(_XtraGrid_BeforeContextMenuClick);
                _AdvBandGridView = _XtraGrid.DefaultView as DevExpress.XtraGrid.Views.BandedGrid.BandedGridView;
                if(_AdvBandGridView==null)
                    throw new MB.Util.APPException("UIDynamicColumnBinding 绑定时出错,DefaultView 需要修改为BandedGridView ", Util.APPMessageType.SysErrInfo);
                //_AdvBandGridView.OptionsSelection.MultiSelect = true;
                //_AdvBandGridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect; 


                _AdvBandGridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(_AdvBandGridView_FocusedRowChanged);
                _AdvBandGridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(_AdvBandGridView_CustomDrawCell);
                _AdvBandGridView.CustomRowCellEditForEditing += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(_AdvBandGridView_CustomRowCellEditForEditing);
                _AdvBandGridView.CustomRowFilter += new RowFilterEventHandler(_AdvBandGridView_CustomRowFilter);

                _AdvBandGridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(_AdvBandGridView_CellValueChanged);

                _CurrentXmlFileName = xmlFileName;
                _ColPropertys = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnPropertys(xmlFileName);
                _EditCols = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnEdits(_ColPropertys, xmlFileName);

                _Timer = new Timer();
                _Timer.Enabled = false;
                _Timer.Tick += new EventHandler(_Timer_Tick);
                _Timer.Interval = 100;
            }
            catch (Exception ex) {
                throw new MB.Util.APPException("UIDynamicColumnBinding 绑定时出错:" + ex.Message, Util.APPMessageType.SysErrInfo);
            }
        }

        void _XtraGrid_BeforeContextMenuClick(object sender, MB.XWinLib.XtraGrid.GridControlExMenuEventArg arg) {
            if (arg.MenuType == XtraContextMenuType.QuickInput) {
                arg.Handled = true;

                if (arg.Column == null) return;

                DevExpress.XtraGrid.Views.Grid.GridView gridView = _AdvBandGridView;
                if (!arg.Column.OptionsColumn.AllowEdit || gridView.FocusedRowHandle < 0) return;

                if (gridView.SortedColumns.Contains(arg.Column)) {
                    MB.WinBase.MessageBoxEx.Show("当前列正在排序状态,不能进行快速填充.请先取消排序");
                    return;
                }

                var dre = MB.WinBase.MessageBoxEx.Question("是否决定以当前选择列的值进行快速填充");
                if (dre != DialogResult.Yes) return;

                DataRow entity = gridView.GetDataRow(gridView.FocusedRowHandle);
                object val = entity[arg.Column.FieldName];

                if (val == null) return;

                int count = gridView.RowCount;
                for (int i = 0; i < count; i++) {
                    //判断当前列是否可以编辑
                    if (!checCanEdit(i, arg.Column.FieldName)) continue;

                    gridView.SetRowCellValue(i, arg.Column.FieldName, val);
                }
            }
        }

        #region 复制、粘贴处理相关...

        ////复制数据到剪贴板中
        //private void dataCopy() {
        //    string ret = string.Empty;
        //    int rowIndex = -1;
        //    foreach (GridCell cell in _AdvBandGridView.GetSelectedCells()) {
        //        if (rowIndex != cell.RowHandle) {
        //            if (!string.IsNullOrEmpty(ret))
        //                ret += "\r\n";
        //        }
        //        else {
        //            if (!string.IsNullOrEmpty(ret))
        //                ret += "\t";
        //        }

        //        ret += _AdvBandGridView.GetRowCellDisplayText(cell.RowHandle, cell.Column);
        //        rowIndex = cell.RowHandle;
        //    }

        //    Clipboard.SetDataObject(ret);
        //}
        ////数据粘贴
        //private void dataPast(string clipboardData) {
        //    if (string.IsNullOrEmpty(clipboardData)) return;

        //    string[] rows = System.Text.RegularExpressions.Regex.Split(clipboardData, "\r\n");

        //    if (rows == null || rows.Length == 0) return;
        //    List<string[]> copyDatas = new List<string[]>();
        //    int colCount = 0;
        //    //分析并获取需要复制的数据
        //    foreach (string line in rows) {
        //        if (string.IsNullOrEmpty(line)) continue;

        //        string[] cols = System.Text.RegularExpressions.Regex.Split(line, "\t");
        //        if (colCount < cols.Length)
        //            colCount = cols.Length;

        //        copyDatas.Add(cols);
        //    }
        //    checkAllowPast(copyDatas.Count, colCount);
        //    GridCell[] cells = _AdvBandGridView.GetSelectedCells();
        //    int rowIndex = -1;
        //    int colIndex = -1;
        //    int oldHandle = -1;
        //    foreach (GridCell cell in cells) {
        //        if (oldHandle != cell.RowHandle) {
        //            colIndex = 0;
        //            rowIndex++;

        //            oldHandle = cell.RowHandle;
        //        }
        //        else {
        //            colIndex++;
        //        }
        //        //判断对应的列是否允许编辑
        //        if (cell.Column.OptionsColumn.ReadOnly) continue;

        //        if (copyDatas[rowIndex].Length < colIndex) continue;

        //        string temp = copyDatas[rowIndex][colIndex];
        //        try {
        //            object val = convertValueType(temp, cell.Column.ColumnType);
        //            //判断当前列是否可以编辑
        //            if (!checCanEdit(rowIndex, cell.Column.FieldName)) continue;

        //            _AdvBandGridView.SetRowCellValue(cell.RowHandle, cell.Column, val);
        //        }
        //        catch (MB.Util.APPException appex) {
        //            throw appex;
        //        }
        //        catch (Exception ex) {
        //            throw new MB.Util.APPException(ex.Message, MB.Util.APPMessageType.DisplayToUser);
        //        }
        //    }


        //}
        ////把值转换为列需要的数据类型。
        //private object convertValueType(string val, Type dataType) {
        //    if (string.IsNullOrEmpty(val)) {
        //        if (dataType.IsValueType)
        //            return getValueTypeDefaultValue(dataType);
        //        else
        //            return null;
        //    }
        //    return Convert.ChangeType(val, dataType);

        //}
        ////获取值类型默认的值   (自定义的结构目前先不考虑)
        //private object getValueTypeDefaultValue(Type dataType) {
        //    string name = dataType.Name;
        //    switch (name) {
        //        case "DateTime":
        //            return default(DateTime);
        //        default:
        //            return default(int);

        //    }

        //}
        ////检查 粘贴 的数据 和选择的 列是否一致
        //private bool checkAllowPast(int srcRowCount, int srcColCount) {
        //    int colCount = 0;
        //    int rowCount = getSelectedRowCount(out colCount);
        //    if (srcRowCount != rowCount) {
        //        throw new MB.Util.APPException(string.Format("选择行数{0} 和需要复制的行数{1} 不一致,请重新选择", rowCount, srcRowCount), MB.Util.APPMessageType.DisplayToUser);
        //    }
        //    if (srcColCount != colCount) {
        //        throw new MB.Util.APPException(string.Format("选择列数{0} 和需要复制的列数{1} 不一致,请重新选择", colCount, srcColCount), MB.Util.APPMessageType.DisplayToUser);
        //    }
        //    return true;
        //}
        ////获取选择的行和列树
        //private int getSelectedRowCount(out int colCount) {
        //    GridCell[] cells = _AdvBandGridView.GetSelectedCells();
        //    int oldHandle = -1;
        //    int rowCount = 0;
        //    int cCount = 0;
        //    foreach (GridCell cell in cells) {
        //        if (oldHandle != cell.RowHandle) {
        //            rowCount++;
        //            oldHandle = cell.RowHandle;
        //        }
        //        if (rowCount == 1) {
        //            cCount++;
        //        }
        //    }
        //    colCount = cCount;

        //    return rowCount;
        //}

        #endregion 复制、粘贴处理相关...

        /// <summary>
        /// 当前绑定XML 的列信息
        /// </summary>
        public Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> ColumnPropertys {
            get { return _ColPropertys; }
        }
        /// <summary>
        /// 列的编辑配置信息。
        /// </summary>
        public Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> ColumnEditCfgInfos {
            get {

                return _EditCols;
            }
        }

        #region IDisposable 成员

        public void Dispose() {
            try {
                _XtraGrid.BeforeContextMenuClick -= new MB.XWinLib.XtraGrid.GridControlExMenuEventHandle(_XtraGrid_BeforeContextMenuClick);
                _AdvBandGridView.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(_AdvBandGridView_FocusedRowChanged);
                _AdvBandGridView.CustomDrawCell -= new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(_AdvBandGridView_CustomDrawCell);
                _AdvBandGridView.CustomRowCellEditForEditing -= new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(_AdvBandGridView_CustomRowCellEditForEditing);
                _AdvBandGridView.CustomRowFilter -= new RowFilterEventHandler(_AdvBandGridView_CustomRowFilter);
                _AdvBandGridView.CellValueChanged -= new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(_AdvBandGridView_CellValueChanged);
            }
            catch { }
        }

        #endregion

        #region public 成员...
        /// <summary>
        ///  动态列绑定处理相关。
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="gridControl"></param>
        /// <param name="xmlFileName"></param>
        public void CreateDataBinding(MB.WinBase.Data.HViewDataConvert<T> convertObject, List<T> lstEntitys,string gridLayoutCfgName) {
            using (MB.Util.MethodTraceWithTime trace = new MB.Util.MethodTraceWithTime("MB.WinClientDefault.Common.UIDynamicColumnBinding<" + typeof(T).FullName + ">.CreateDataBinding")) {
                _HViewConvertObject = convertObject;
                _CurrentEditEntitys = lstEntitys;


                var gridViewLayoutInfo = MB.WinBase.LayoutXmlConfigHelper.Instance.GetGridColumnLayoutInfo(_CurrentXmlFileName, gridLayoutCfgName);
                MB.XWinLib.XtraGrid.DynamicColumnBandGridHelper<T> dynamicBandHelper = new MB.XWinLib.XtraGrid.DynamicColumnBandGridHelper<T>();


                _CurrentEditTable = convertObject.Convert(lstEntitys, _ColPropertys);

                onAfterDataConvert();

                if (convertObject.ConvertCfgParam.DynamicColumnCaption)
                    gridViewLayoutInfo = dynamicBandHelper.ResetDynamicCaptionColumnViewLayout(convertObject, gridViewLayoutInfo);
                else
                    gridViewLayoutInfo = dynamicBandHelper.ResetDynamicColumnViewLayout(convertObject, gridViewLayoutInfo);

                var detailBindingParams = new MB.XWinLib.GridDataBindingParam(_XtraGrid, _CurrentEditTable, false);


                MB.XWinLib.XtraGrid.XtraGridEditHelper.Instance.CreateEditXtraGrid(detailBindingParams, _ColPropertys, _EditCols, gridViewLayoutInfo);
                //if (convertObject.ConvertCfgParam.DynamicColumnCaption) {
                iniDynamicBandColumns();
                // }
                MB.XWinLib.XtraGrid.XtraGridViewHelper.Instance.SetGridViewNewItem(_XtraGrid, false);
                _XtraGrid.ReSetContextMenu(XtraContextMenuType.QuickInput
                                         | XtraContextMenuType.Export | XtraContextMenuType.SaveGridState);


                setDynamicColumnOnFocusedRowChanged(_AdvBandGridView.FocusedRowHandle);
            }
        }
        /// <summary>
        /// 当前进行数据转换处理的对象。
        /// </summary>
        public MB.WinBase.Data.HViewDataConvert<T> HViewConvertObject {
            get {
                return _HViewConvertObject;
            }
        }
        /// <summary>
        /// 当前编辑的表结构。
        /// </summary>
        public DataTable CurrentEditTable {
            get {
                return _CurrentEditTable;
            }
        }
        /// <summary>
        /// 以相同的状态改变数据实体
        /// </summary>
        public void AcceptChanges() {
            if (_CurrentEditEntitys == null) return;

            foreach (T info in _CurrentEditEntitys) {
                MB.Util.Model.EntityState entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(info);
                if (entityState == MB.Util.Model.EntityState.New || entityState == MB.Util.Model.EntityState.Modified) {
                    MB.WinBase.UIDataEditHelper.Instance.SetEntityState(info, MB.Util.Model.EntityState.Persistent);
                }
            }
        }
        /// <summary>
        /// 检查编辑的实体是否已经发生改变。
        /// </summary>
        /// <returns></returns>
        public bool CheckEntityExistsModified() {
            if (_CurrentEditEntitys == null) return false;
            foreach (T info in _CurrentEditEntitys) {
                MB.Util.Model.EntityState entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(info);
                if (entityState == MB.Util.Model.EntityState.New || entityState == MB.Util.Model.EntityState.Modified) {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据 RowHandle 获取当前行对应的所有编辑明细。
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        public T[] GetDetailEntitysByDataRow(int rowHandle) {
            DataRow dr = _AdvBandGridView.GetDataRow(rowHandle);
            if (dr == null) return default(T[]);

            var lstData = _HViewConvertObject.DynamicColumnValueMappings[dr];
            List<T> details = new List<T>();
            foreach (var data in lstData) {
                details.Add(data.DetailEntity);  
            }
            return details.ToArray(); 
        }
        /// <summary>
        /// 根据RowHandle 和列名称获取列对应的明细项。
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="columnFieldName"></param>
        /// <returns></returns>
        public T GetDetailEntityByDataRowAndColumn(int rowHandle,string columnFieldName) {
            DataRow dr = _AdvBandGridView.GetDataRow(rowHandle);
            if (dr == null) return default(T);

            var lstData = _HViewConvertObject.DynamicColumnValueMappings[dr];
            List<T> details = new List<T>();
            int s = MB.WinBase.Data.HViewDataConvert.GetIndexByColumnFieldName(columnFieldName);
            if (s < 0) return default(T);
            foreach (var data in lstData) {
                if (data.Index == s)
                    return data.DetailEntity; 
            }
            return default(T);
        }
        /// <summary>
        /// 检查行对应的列是否允许编辑。
        /// </summary>
        /// <param name="drData"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool CheckRowCellAllowEdit(DataRow drData, string columnName) {
            if (!_HViewConvertObject.DynamicColumnValueMappings.ContainsKey(drData)) {
                return false;
            }
            var lstColumn = _HViewConvertObject.DynamicColumnValueMappings[drData];
            int index = getDynamicIndexByName(columnName);
            return lstColumn.Exists(o => o.Index == index);
        }
        #endregion public 成员...

        #region 绑定的对象事件处理...
        void _AdvBandGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e) {
            //处理动态Caption 列的情况
            
            DataRow dr = _AdvBandGridView.GetDataRow(e.RowHandle);
            if (dr == null) return;

            var lstColumn = _HViewConvertObject.DynamicColumnValueMappings[dr];
            int colIndex = getDynamicIndexByName(e.Column.FieldName);
            if (colIndex >= 0 ) {
                MB.WinBase.Data.DynamicColumnValueMappingInfo<T> colInfo = lstColumn.FirstOrDefault(o=>o.Index == colIndex);
                if (colInfo != null) {
                    foreach (string mappingColName in _HViewConvertObject.ConvertCfgParam.ColumnAreaCfgInfo.MappingColumnName) {
                        string colName = MB.WinBase.Data.HViewDataConvert.CreateDynamicColumnFieldName(mappingColName, colInfo.Index);
                        if (string.Compare(e.Column.FieldName, colName, true) != 0) continue;
                        MB.Util.MyReflection.Instance.InvokePropertyForSet(colInfo.DetailEntity, mappingColName, e.Value);
                    }

                    if (MB.WinBase.UIDataEditHelper.Instance.CheckExistsEntityState(colInfo.DetailEntity)) {
                        var state = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(colInfo.DetailEntity);
                        if (state == MB.Util.Model.EntityState.Persistent)
                            MB.WinBase.UIDataEditHelper.Instance.SetEntityState(colInfo.DetailEntity, MB.Util.Model.EntityState.Modified);
                    }
                }
            }
           
            _AdvBandGridView.UpdateCurrentRow();
        }

        void _AdvBandGridView_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e) {
            if (_HViewConvertObject == null || _DynamicBandColumns == null || _DynamicBandColumns.Count == 0) return;
           // if (!_HViewConvertObject.ConvertCfgParam.DynamicColumnCaption) return;

            if (e.Column.FieldName.IndexOf(DYNAMIC_COL_LEFT_NAME) >= 0) {
                if (_ReadOnly) {
                    e.RepositoryItem.ReadOnly = true;
                    e.RepositoryItem.AllowFocused = false;
                }
                else {
                    bool canEdit = checCanEdit(e.RowHandle, e.Column.FieldName);
                    e.RepositoryItem.ReadOnly = !canEdit;
                    e.RepositoryItem.AllowFocused = canEdit;
                    e.RepositoryItem.AppearanceReadOnly.BackColor = canEdit ? Color.White : Color.WhiteSmoke;
                    e.RepositoryItem.AppearanceReadOnly.BorderColor = canEdit ? Color.White : Color.WhiteSmoke;
                    if (!canEdit) {
                        SendKeys.Send("{TAB}");
                    }
                }
            }
        }

        void _AdvBandGridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e) {
           // if (!_HViewConvertObject.ConvertCfgParam.DynamicColumnCaption) return;
            if (_HViewConvertObject == null) return;

            gridViewCustomDrawCell(e);
        }

        void _AdvBandGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
            if (_HViewConvertObject==null || !_HViewConvertObject.ConvertCfgParam.DynamicColumnCaption) return;

            setDynamicColumnOnFocusedRowChanged(e.FocusedRowHandle);
        }
        private DateTime _BeTime = System.DateTime.Now;
     
        void _AdvBandGridView_CustomRowFilter(object sender, RowFilterEventArgs e) {
            if (!_HViewConvertObject.ConvertCfgParam.DynamicColumnCaption) return;

            if (_AdvBandGridView.FocusedRowHandle >= 0) {
                //主要是为了解决GridView.GetFocuseRow() BUG 而采取的变态处理方法，这里看不懂不要紧。
                TimeSpan span = System.DateTime.Now.Subtract(_BeTime);
                if (span.TotalMilliseconds > 200) {
                    _Timer.Enabled = true;
                }
                _BeTime = System.DateTime.Now;
            }
               
        }

        void _Timer_Tick(object sender, EventArgs e) {
            _Timer.Enabled = false;

            _AdvBandGridView.RefreshData();
            if (_HViewConvertObject == null || !_HViewConvertObject.ConvertCfgParam.DynamicColumnCaption) return;

            setDynamicColumnOnFocusedRowChanged(_AdvBandGridView.FocusedRowHandle);
            _BeTime = System.DateTime.Now;
        }
        #endregion 绑定的对象事件处理...

        #region 内部函数处理...
        private void gridViewCustomDrawCell(DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e) {
            if (_HViewConvertObject == null || _DynamicBandColumns == null || _DynamicBandColumns.Count == 0 || _ReadOnly) return;
            if (e.Column.FieldName.IndexOf(DYNAMIC_COL_LEFT_NAME) >= 0) {
                if (!checCanEdit(e.RowHandle, e.Column.FieldName))
                    e.Appearance.BackColor = Color.WhiteSmoke;

            }
        }
        //根据行的不同设置动态列的Caption
        private void setDynamicColumnOnFocusedRowChanged(int rowHandle) {
            if (rowHandle < 0 || _HViewConvertObject == null ||
                _DynamicBandColumns == null || _DynamicBandColumns.Count == 0) return;

            if (!_HViewConvertObject.ConvertCfgParam.DynamicColumnCaption) return;
   
            DataRow dr = _AdvBandGridView.GetDataRow(rowHandle);

            if (dr == null) return;

            if (!_HViewConvertObject.DynamicColumnValueMappings.ContainsKey(dr)) return; 

            var lstColumn = _HViewConvertObject.DynamicColumnValueMappings[dr];
            foreach (DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bdc in _DynamicBandColumns.Values) {
                bdc.Caption = " ";
            }
            foreach (MB.WinBase.Data.DynamicColumnValueMappingInfo<T> activeCol in lstColumn) {
              //  string sName = MB activeCol.Index + " " + _HViewConvertObject.ConvertCfgParam.ColumnAreaCfgInfo.MappingColumnName[0];
                string colName = MB.WinBase.Data.HViewDataConvert.CreateDynamicColumnFieldName(_HViewConvertObject.ConvertCfgParam.ColumnAreaCfgInfo.MappingColumnName[0], activeCol.Index);
                if (!_DynamicBandColumns.ContainsKey(colName))
                    throw new MB.Util.APPException(string.Format("动态创建的列 {0} 在动态列集合中不存在,请检查MappingColumnName 和绑定列的配置是否正确", colName), MB.Util.APPMessageType.SysErrInfo);   

                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn adc = _DynamicBandColumns[colName];
                adc.Caption = activeCol.Caption;

            }
        }
        //初始化动态Caption 创建的列
        private void iniDynamicBandColumns() {
            if (_DynamicBandColumns == null)
                _DynamicBandColumns = new Dictionary<string, DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn>();
            else
                _DynamicBandColumns.Clear();

            foreach (DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandCol in _AdvBandGridView.Columns) {
                if (bandCol.FieldName.IndexOf(DYNAMIC_COL_LEFT_NAME) < 0) continue;

                int index = getDynamicIndexByName(bandCol.FieldName);
                _DynamicBandColumns.Add(bandCol.FieldName, bandCol);

            }
        }

        //检查当前CELL 是否可以编辑
        private bool checCanEdit(int rowHandle, string columnName) {
            if (rowHandle < 0) return false;
            DataRow dr = _AdvBandGridView.GetDataRow(rowHandle);
            return CheckRowCellAllowEdit(dr, columnName);
            //return index < lstColumn.Count;
        }
        //获取动态列的Index
        private int getDynamicIndexByName(string columnName) {
            return MB.WinBase.Data.HViewDataConvert.GetIndexByColumnFieldName(columnName);   
            //try {
            //    int lastS = columnName.LastIndexOf('_');
            //    int index = int.Parse(columnName.Substring(lastS + 1, columnName.Length - lastS - 1));

            //    return index;
            //}
            //catch (Exception ex) {
            //    throw new MB.Util.APPException(string.Format("执行  MB.WinClientDefault.Common.UIDynamicColumnBinding<T>.getDynamicIndexByName 时出错,可能是动态列名{0} 出错", columnName) + ex.Message);
            //}
        }
         

        #endregion 内部函数处理...

    }
    /// <summary>
    /// 
    /// </summary>
    public class DynamicColumnBindingEventAegs : System.EventArgs {

    }
}
