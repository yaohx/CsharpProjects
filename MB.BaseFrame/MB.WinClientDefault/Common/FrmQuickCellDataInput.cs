//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-11-16
// Description	:	可编辑网格快速输入
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;

using MB.XWinLib.XtraGrid;
using MB.WinClientDefault.DataImport;
namespace MB.WinClientDefault.Common {
    /// <summary>
    /// 把多维AdvBandGridView 转换为一维列表满足快速输入和数据导入导出Excel 的需求。
    /// </summary>
    public partial class FrmQuickCellDataInput : AbstractBaseForm {
        private DevExpress.XtraGrid.GridControl _GrdCtlSrc;
        private DevExpress.XtraGrid.Views.Grid.GridView _GridViewSrc;
        private DataTable _DtSource;
        private List<MB.WinBase.Data.DynamicColumnInfo> _DynamicCols;
        private Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> _ColPropertys;
        private Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> _EditCols;
        private MB.WinBase.Common.DataImportCfgInfo _ImportCfgInfo;

        #region 构造函数...
        /// <summary>
        /// 提供设计阶段的默认构造函数。
        /// </summary>
        protected FrmQuickCellDataInput()
            : this(null,null,null, null) {
        }
        /// <summary>
        /// 把多维AdvBandGridView 转换为一维列表满足快速输入和数据导入导出Excel 的需求。
        /// </summary>
        /// <param name="grdCtlSrc"></param>
        /// <param name="dsSource"></param>
        /// <param name="dynamicCols"></param>
        /// <param name="xmlFileName"></param>
        public FrmQuickCellDataInput(DevExpress.XtraGrid.GridControl grdCtlSrc, DataTable dsSource,
                                  List<MB.WinBase.Data.DynamicColumnInfo> dynamicCols,string xmlFileName)
            : this(grdCtlSrc, dsSource, dynamicCols,null,null,null) {

            if (!string.IsNullOrEmpty(xmlFileName)) {
                _ColPropertys = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnPropertys(xmlFileName);
                _EditCols = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnEdits(_ColPropertys, xmlFileName);
                _ImportCfgInfo = MB.WinBase.LayoutXmlConfigHelper.Instance.GetDataImportCfgInfo(xmlFileName, null);
            }

        }
        /// <summary>
        /// 把多维AdvBandGridView 转换为一维列表满足快速输入和数据导入导出Excel 的需求。
        /// </summary>
        public FrmQuickCellDataInput(DevExpress.XtraGrid.GridControl grdCtlSrc, DataTable dsSource, 
                                     List<MB.WinBase.Data.DynamicColumnInfo> dynamicCols,
                                     Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys,
                                     Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols,
                                     MB.WinBase.Common.DataImportCfgInfo importCfgInfo) {

            InitializeComponent();

            _GrdCtlSrc = grdCtlSrc;
            _GridViewSrc = grdCtlSrc.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
            _ColPropertys = colPropertys;
            _EditCols = editCols;

            _DtSource = dsSource;
            _DynamicCols = dynamicCols;
            this.Load += new EventHandler(FrmQuickCellDataInput_Load);

        }
        #endregion 构造函数...

        #region 自定义事件处理相关...
        private System.EventHandler<QuickInputDataImportEventArgs> _BeforeDataImport;
        public event System.EventHandler<QuickInputDataImportEventArgs> BeforeDataImport {
            add {
                _BeforeDataImport += value; 
            }
            remove {
                _BeforeDataImport -= value;
            }
        }
        protected virtual void OnBeforeDataImport(QuickInputDataImportEventArgs arg) {
            if (_BeforeDataImport != null)
                _BeforeDataImport(this, arg);
        }
        private System.EventHandler<GridRowCellEditEventArgs> _RowCellEditForEditing;
        public event System.EventHandler<GridRowCellEditEventArgs> RowCellEditForEditing {
            add {
                _RowCellEditForEditing += value;
            }
            remove {
                _RowCellEditForEditing -= value;
            }
        }
        protected virtual void OnRowCellEditForEditing(GridRowCellEditEventArgs arg) {
            if (_RowCellEditForEditing != null)
                _RowCellEditForEditing(this, arg);
        }
        #endregion 自定义事件处理相关...

        #region 界面事件处理相关...
        void FrmQuickCellDataInput_Load(object sender, EventArgs e) {
            if (MB.Util.General.IsInDesignMode()) return;
 
            gridViewMain.OptionsSelection.MultiSelect = true;
            gridViewMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;

            grdCtlMain.BeforeContextMenuClick += new MB.XWinLib.XtraGrid.GridControlExMenuEventHandle(grdCtlMain_BeforeContextMenuClick);
            gridViewMain.CustomDrawCell += new RowCellCustomDrawEventHandler(gridViewMain_CustomDrawCell);
            gridViewMain.CustomRowCellEditForEditing += new CustomRowCellEditEventHandler(gridViewMain_CustomRowCellEditForEditing);

            grdCtlMain.DataSource = _DtSource.DefaultView;
            iniFormatGridColumnByOtherColumn();


            grdCtlMain.ReSetContextMenu(XtraContextMenuType.Copy | XtraContextMenuType.Past | XtraContextMenuType.QuickInput 
                                        | XtraContextMenuType.Export | XtraContextMenuType.DataImport | XtraContextMenuType.SaveGridState);

            
            
            resetXtraGridColumnCaption();
        }

        void gridViewMain_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e) {
            DataRow dr = gridViewMain.GetDataRow(e.RowHandle);
            GridRowCellEditEventArgs arg = new GridRowCellEditEventArgs(dr,e.Column);
            arg.AllowEdit = e.Column.OptionsColumn.AllowEdit;
            OnRowCellEditForEditing(arg);

            bool canEdit = arg.AllowEdit;
            e.RepositoryItem.ReadOnly = !canEdit;
            e.RepositoryItem.AllowFocused = canEdit;
            e.RepositoryItem.AppearanceReadOnly.BackColor = canEdit ? Color.White : Color.WhiteSmoke;
            e.RepositoryItem.AppearanceReadOnly.BorderColor = canEdit ? Color.White : Color.WhiteSmoke;
            if (!canEdit) {
                SendKeys.Send("{TAB}");
            }

        }

        void gridViewMain_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e) {
            DataRow dr = gridViewMain.GetDataRow(e.RowHandle);
            GridRowCellEditEventArgs arg = new GridRowCellEditEventArgs(dr, e.Column);
            arg.AllowEdit = e.Column.OptionsColumn.AllowEdit;
            OnRowCellEditForEditing(arg);
            if (!arg.AllowEdit) {
                e.Appearance.BackColor = Color.WhiteSmoke; 
            }

            if (gridViewMain.IsCellSelected(e.RowHandle,e.Column) ) {
                e.Appearance.BackColor = Color.FromArgb(0,88,176) ;
                e.Appearance.ForeColor = Color.White; 
            }
        }

        void grdCtlMain_BeforeContextMenuClick(object sender, MB.XWinLib.XtraGrid.GridControlExMenuEventArg arg) {
            if (arg.MenuType == MB.XWinLib.XtraGrid.XtraContextMenuType.Copy) {
                arg.Handled = true;

                dataCopy();
            }
            else if (arg.MenuType == MB.XWinLib.XtraGrid.XtraContextMenuType.Past) {
                arg.Handled = true;

                IDataObject data = Clipboard.GetDataObject();

                object ss = data.GetData(typeof(string));
                dataPast(ss.ToString());

            }
            else if (arg.MenuType == XtraContextMenuType.DataImport) {
                arg.Handled = true;
                dataImport();
            }
            else if (arg.MenuType == XtraContextMenuType.QuickInput) {
                arg.Handled = true;

                if (arg.Column != null) {
                    DevExpress.XtraGrid.Views.Grid.GridView gridView = gridViewMain;
                    if (arg.Column.OptionsColumn.AllowEdit && gridView.FocusedRowHandle >= 0) {
                        var dre = MB.WinBase.MessageBoxEx.Question("是否决定以当前选择列的值进行快速填充");
                        if (dre != DialogResult.Yes) return;

                        DataRow entity = gridView.GetDataRow(gridView.FocusedRowHandle);
                        object val = entity[arg.Column.FieldName];
                        if (val != null) {
                            int count = gridView.RowCount;
                            for (int i = 0; i < count; i++) {
                                DataRow dr = gridView.GetDataRow(i);
                                GridRowCellEditEventArgs cellarg = new GridRowCellEditEventArgs(dr, arg.Column);
                                cellarg.AllowEdit = arg.Column.OptionsColumn.AllowEdit ;
                                OnRowCellEditForEditing(cellarg);
                                if (cellarg.AllowEdit)
                                    gridView.SetRowCellValue(i, arg.Column.FieldName, val); 
                            }
                        }
                    }
                }
            }
            
        }
        private void tButQuit_Click(object sender, EventArgs e) {
            this.Close();
        }
        #endregion 界面事件处理相关...

        #region 数据导入处理相关...
        //数据导入处理相关...
        private void dataImport() {
            string file = MB.WinBase.ShareLib.Instance.SelectedFile("Excel 文件 (*.xls)|*.xls");
            if (string.IsNullOrEmpty(file)) return ;

            DataSet dsData = new DataSet();
            dsData.Tables.Add(_DtSource.Clone());
            foreach (DataColumn dc in dsData.Tables[0].Columns) {
                var eCol = getGridColumnByFieldName(gridViewMain,dc.ColumnName);
                if (eCol == null) continue;
                dc.Caption = eCol.Caption;
            }
            MB.WinEIDrive.Import.XlsImport xlsImport = new MB.WinEIDrive.Import.XlsImport(dsData, file);
            ImportEngine helper = new ImportEngine(_EditCols, grdCtlMain, xlsImport);
            helper.Commit();
            //移除空行数据
            MB.Util.DataValidated.Instance.RemoveNULLRowData(dsData);

            var arg = new QuickInputDataImportEventArgs(_DtSource, dsData);
            OnBeforeDataImport(arg);
            if (!arg.Handled)
                dataImportByOveride(dsData);

        }
        //以覆盖的方式进行数据导入
        private void dataImportByOveride(DataSet dsData) {
            DataRow[] drs = dsData.Tables[0].Select();
             MB.WinBase.Common.DataImportCfgInfo importCfgInfo = _ImportCfgInfo;
            string[] keys = importCfgInfo.OverideKeys.Split(',');
            string[] oFields = importCfgInfo.OverideFields.Split(',');
            if (keys == null || keys.Length == 0)
                throw new MB.Util.APPException("在数据覆盖导入是对DataImportCfgInfo 没有 配置 OverideKeys");
            if (oFields == null || oFields.Length == 0)
                throw new MB.Util.APPException("在数据覆盖导入是对DataImportCfgInfo 没有 配置 OverideFields");
            List<DataRow> notExists = new List<DataRow>();
            DataRow[] editDatas = _DtSource.Select() ;
            foreach (DataRow dr in drs) {
                DataRow  findDataRow = null;
                string[] vals = MB.Util.DataHelper.Instance.GetMultiFieldValue(dr, keys);

                bool exits = MB.Util.DataValidated.Instance.CheckExistsDataRow(editDatas, keys, vals.ToArray(), out findDataRow);
                if (!exits) {
                    continue;
                }

                foreach (string field in oFields) {
                    if (!findDataRow.Table.Columns.Contains(field))
                        throw new MB.Util.APPException(string.Format("导入配置的属性{0}不属于需要导入的表", field));
                    if (!dr.Table.Columns.Contains(field))
                        continue;
                    findDataRow[field] = dr[field];
                }
            }
        }

        #endregion 数据导入处理相关...

        #region 网格初始化处理相关...

        //通过其它列格式化当前编辑的网格列
        private void iniFormatGridColumnByOtherColumn() {
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridViewMain.Columns) {
                if (_ColPropertys != null && _ColPropertys.ContainsKey(col.FieldName)) {
                    col.Visible = _ColPropertys[col.FieldName].Visibled;
                    col.Width = _ColPropertys[col.FieldName].VisibleWidth;
                }

                DevExpress.XtraGrid.Columns.GridColumn srcCol = getGridColumnByFieldName(_GridViewSrc,col.FieldName);
                if (srcCol == null) continue;

                col.Caption = srcCol.Caption;

                col.OptionsColumn.ReadOnly = srcCol.OptionsColumn.ReadOnly;
                col.OptionsColumn.AllowEdit = srcCol.OptionsColumn.AllowEdit;


                col.AppearanceCell.BackColor = srcCol.AppearanceCell.BackColor;
                col.AppearanceCell.ForeColor = srcCol.AppearanceCell.ForeColor;
                
                
                col.Width = srcCol.Width;
                if (col.AppearanceCell.BackColor != Color.White) {
                   // col.AppearanceCell.BackColor2 = Color.Gray;
                    
                }
                if (srcCol.VisibleIndex < 0)
                    col.VisibleIndex = -1;


                col.ColumnEdit = srcCol.ColumnEdit.Clone() as DevExpress.XtraEditors.Repository.RepositoryItem; 
            }

        }
        //通过字段名称获取对应的网格列
        private DevExpress.XtraGrid.Columns.GridColumn getGridColumnByFieldName(DevExpress.XtraGrid.Views.Grid.GridView gridView,string fieldName) {
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridView.Columns) {
                if (string.Compare(col.FieldName, fieldName, true) == 0)
                    return col;
            }
            return null;
        }
        //重新设置网格列的Caption
        private void resetXtraGridColumnCaption() {
            foreach (DevExpress.XtraGrid.Columns.GridColumn gCol in gridViewMain.Columns) {
                string fieldName = gCol.FieldName;
                int index = MB.WinBase.Data.HViewDataConvert.GetIndexByColumnFieldName(fieldName);
                if (index < 0) continue;
                string mappingCol = MB.WinBase.Data.HViewDataConvert.GetMappingFieldName(gCol.FieldName);
                if (_ColPropertys.ContainsKey(mappingCol))
                    mappingCol = _ColPropertys[mappingCol].Description;

                if (index < _DynamicCols.Count)
                    gCol.Caption = mappingCol + "-" + _DynamicCols[index].Caption;
                else
                    gCol.Caption = mappingCol;
            }
        }
        #endregion 网格初始化处理相关...

        #region 复制、粘贴处理相关...
        //复制数据到剪贴板中
        private void dataCopy() {
            string ret = string.Empty;
            int rowIndex = -1;
            foreach (GridCell cell in gridViewMain.GetSelectedCells()) {
                if (rowIndex != cell.RowHandle) {
                    if (!string.IsNullOrEmpty(ret))
                        ret += "\r\n";
                }
                else {
                    if (!string.IsNullOrEmpty(ret))
                        ret += "\t";
                }
                
                ret += gridViewMain.GetRowCellDisplayText(cell.RowHandle, cell.Column);
                rowIndex = cell.RowHandle;
            }

            Clipboard.SetDataObject(ret); 
        }
        //数据粘贴
        private void dataPast(string clipboardData) {
            if (string.IsNullOrEmpty(clipboardData)) return;

            string[] rows = System.Text.RegularExpressions.Regex.Split(clipboardData, "\r\n");

            if (rows == null || rows.Length == 0) return;
            List<string[]> copyDatas = new List<string[]>();
            int colCount = 0;
            //分析并获取需要复制的数据
            foreach (string line in rows) {
                if (string.IsNullOrEmpty(line)) continue;

                string[] cols = System.Text.RegularExpressions.Regex.Split(line, "\t");
                if (colCount < cols.Length)
                    colCount = cols.Length;

                copyDatas.Add(cols);
            }
            checkAllowPast(copyDatas.Count, colCount);
            GridCell[] cells = gridViewMain.GetSelectedCells();
            int rowIndex = -1;
            int colIndex = -1;
            int oldHandle = -1;
            foreach (GridCell cell in cells) {
                if (oldHandle != cell.RowHandle) {
                    colIndex = 0;
                    rowIndex ++;

                    oldHandle = cell.RowHandle;
                }
                else {
                    colIndex++;
                }
                //判断对应的列是否允许编辑
                if (cell.Column.OptionsColumn.ReadOnly) continue;

                if (copyDatas[rowIndex].Length < colIndex) continue;

                string temp = copyDatas[rowIndex][colIndex];
                try {
                    DataRow dr = gridViewMain.GetDataRow(cell.RowHandle);
                    GridRowCellEditEventArgs cellarg = new GridRowCellEditEventArgs(dr, cell.Column);
                    cellarg.AllowEdit = cell.Column.OptionsColumn.AllowEdit;
                    OnRowCellEditForEditing(cellarg);
                    if (cellarg.AllowEdit) {
                        object val = MB.Util.MyReflection.Instance.ConvertValueType( cell.Column.ColumnType,temp);
                        gridViewMain.SetRowCellValue(cell.RowHandle, cell.Column, val);
                    }
                }
                catch (MB.Util.APPException appex) {
                    throw appex;
                }
                catch (Exception ex) {
                    throw new MB.Util.APPException(ex.Message, MB.Util.APPMessageType.DisplayToUser);
                }
            }


        }
        ////把值转换为列需要的数据类型。
        //private object convertValueType(string val, Type dataType) {
        //    if (string.IsNullOrEmpty(val)) {
        //        if (dataType.IsValueType)
        //            return getValueTypeDefaultValue(dataType);
        //        else
        //            return null;
        //    }
        //    return MB.Util.MyReflection.Instance.ConvertValueType(dataType,val); 

        //}
     
        //检查 粘贴 的数据 和选择的 列是否一致
        private bool checkAllowPast(int srcRowCount,int srcColCount) {
            int colCount = 0;
            int rowCount = getSelectedRowCount(out colCount);
            if (srcRowCount != rowCount) {
                throw new MB.Util.APPException(string.Format("选择行数{0} 和需要复制的行数{1} 不一致,请重新选择",rowCount,srcRowCount), MB.Util.APPMessageType.DisplayToUser);  
            }
            if (srcColCount != colCount) {
                throw new MB.Util.APPException(string.Format("选择列数{0} 和需要复制的列数{1} 不一致,请重新选择", colCount, srcColCount), MB.Util.APPMessageType.DisplayToUser);
            }
            return true;
        }
        //获取选择的行和列树
        private int getSelectedRowCount(out int colCount) {
            GridCell[] cells = gridViewMain.GetSelectedCells();
            int oldHandle = -1;
            int rowCount = 0;
            int cCount = 0;
            foreach (GridCell cell in cells) {
                if (oldHandle != cell.RowHandle) {
                    rowCount++;
                    oldHandle = cell.RowHandle;
                }
                if (rowCount == 1) {
                    cCount++;
                }
            }
            colCount = cCount;

            return rowCount;
        }
        #endregion 复制、粘贴处理相关...
    }

    /// <summary>
    /// 快速网格编辑数据导入事件。
    /// </summary>
    public class QuickInputDataImportEventArgs : System.EventArgs {
        private DataTable _EditGridData;
        private DataSet _ImportData;
        private bool _Handled;

        public QuickInputDataImportEventArgs(DataTable editGridData, DataSet importData) {
            _EditGridData = editGridData;
            _ImportData = importData;
        }

        /// <summary>
        /// 当前编辑的网格
        /// </summary>
        public DataTable EditGridData {
            get {
                return _EditGridData;
            }
            set {
                _EditGridData = value;
            }
        }
        /// <summary>
        /// 当前导入的数据。
        /// </summary>
        public DataSet ImportData {
            get {
                return _ImportData;
            }
            set {
                _ImportData = value;
            }
        }
        /// <summary>
        /// 判断相应的事件是否已经处理。
        /// </summary>
        public bool Handled {
            get {
                return _Handled;
            }
            set {
                _Handled = value;
            }
        }
    }

    /// <summary>
    /// 判断当前列是否允许编辑。
    /// </summary>
    public class GridRowCellEditEventArgs : System.EventArgs {
        private DataRow _QuickEditDataRow;
        private DevExpress.XtraGrid.Columns.GridColumn   _ColumnName;

        private bool _AllowEdit;
        public GridRowCellEditEventArgs(DataRow quickEditDataRow, DevExpress.XtraGrid.Columns.GridColumn columnName) {
            _QuickEditDataRow = quickEditDataRow;
            _ColumnName = columnName; 
        }
        /// <summary>
        /// 判断对应的列是否允许编辑
        /// </summary>
        public bool AllowEdit {
            get {
                return _AllowEdit;
            }
            set {
                _AllowEdit = value;
            }
        }
        /// <summary>
        /// 当前快速编辑行。
        /// </summary>
        public DataRow QuickEditDataRow {
            get {
                return _QuickEditDataRow;
            }
            set {
                _QuickEditDataRow = value;
            }
        }
        /// <summary>
        /// 当前编辑行对应的列。
        /// </summary>
        public DevExpress.XtraGrid.Columns.GridColumn ColumnName {
            get {
                return _ColumnName;
            }
            set {
                _ColumnName = value; 
            }
        }
    }
}
