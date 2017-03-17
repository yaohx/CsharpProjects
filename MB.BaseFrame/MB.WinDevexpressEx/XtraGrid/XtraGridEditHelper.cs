//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-18
// Description	:	XtraGridEditHelper 操作可编辑的XtraGrid 。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
using MB.WinBase.Common;

namespace MB.XWinLib.XtraGrid {
    /// <summary>
    /// XtraGridEditHelper 操作可编辑的XtraGrid 。
    /// 备注： XtraGrid的tag 中存储的是，该网格控件对应绑定数据的业务类。
    /// </summary>
    public class XtraGridEditHelper {
        public static readonly string COL_SELECTED_NAME = "Selected";
        public static readonly string COL_UNKNOW_DATE_TYPE_FOR_CREATING = "Unknow";

        #region Instance...
        private static Object _Obj = new object();
        private static XtraGridEditHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected XtraGridEditHelper() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static XtraGridEditHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new XtraGridEditHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        #region Public static function...
        /// <summary>
        /// 创建可编辑的XtraGrid 控件。
        /// </summary>
        /// <param name="bindParam"></param>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public bool CreateEditXtraGrid(GridDataBindingParam bindParam,
                                    string xmlFileName) {
            var colPropertys = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnPropertys(xmlFileName);
            var editCols = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnEdits(colPropertys, xmlFileName);
            var gridViewLayoutInfo = MB.WinBase.LayoutXmlConfigHelper.Instance.GetGridColumnLayoutInfo(xmlFileName, string.Empty);
            return CreateEditXtraGrid(bindParam, colPropertys, editCols, gridViewLayoutInfo);
        }
        /// <summary>
        ///  创建可编辑的XtraGrid 控件。
        /// </summary>
        /// <param name="bindParam"></param>
        /// <param name="configXmlFile"></param>
        /// <returns></returns>
        public bool CreateEditXtraGrid(GridDataBindingParam bindParam,
                                       Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys,
                                       Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols,
                                       MB.WinBase.Common.GridViewLayoutInfo gridViewLayoutInfo) {

                                          

            string viewTypeName = bindParam.XtraGrid.MainView.GetType().Name;
            if (viewTypeName == "GridView") {
                editViewGrid(bindParam, colPropertys, editCols, gridViewLayoutInfo);
            }
            else if (viewTypeName == "AdvBandedGridView" || viewTypeName == "BandedGridView") {
                if (gridViewLayoutInfo == null)
                    throw new MB.Util.APPException("创建 AdvBandedGridView 网格视图必须在相应的 XML 文件中配置 GridViewLayoutInfo");
                CreateEditBandXtraGrid(bindParam, colPropertys, editCols, gridViewLayoutInfo);
            }
            else {
                throw new MB.Util.APPException("目前编辑的XtrGrid 只支持AdvBandedGridView 和 GridView的格式。请检查你设置的XtrGrid View是否为这种类型。");
            }
            return true;
        }
        /// <summary>
        /// 创建可编辑的卡片网格编辑控件。
        /// </summary>
        /// <param name="xtraGrid"></param>
        /// <param name="dataSource"></param>
        /// <param name="buiObj"></param>
        /// <param name="editCols"></param>
        /// <returns></returns>
        public bool CreateCardEdit(MB.XWinLib.XtraGrid.GridControlEx xtraGrid,
            object dataSource, MB.WinBase.IFace.IClientRule buiObj, Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols) {

            string viewTypeName = xtraGrid.MainView.GetType().Name;
            if (viewTypeName != "CardView") {
                xtraGrid.MainView = new DevExpress.XtraGrid.Views.Card.CardView();
            }
            editCardGrid(xtraGrid, dataSource, buiObj, editCols);
            return true;
        }
        #endregion Public static function...

        #region 内部处理 初始化编辑相关...

        #region GridView...
        //编辑一般ViewGrid 格式
        private bool editViewGrid(GridDataBindingParam bindParam,
                        Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> objPropertys,
                        Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols,
            MB.WinBase.Common.GridViewLayoutInfo gridViewLayoutInfo) {

                if (gridViewLayoutInfo != null)
                    MB.Util.TraceEx.Write("OK");

            DevExpress.XtraGrid.GridControl xtraGCtl = bindParam.XtraGrid;

            MB.XWinLib.XtraGrid.XtraGridViewHelper.Instance.SetEditGridAutoInfo(xtraGCtl);

            XtraGridViewHelper.Instance.SetXtraGridSkin(xtraGCtl, XtraGridSkin.Edit);



            DevExpress.XtraGrid.Views.Grid.GridView gridView = xtraGCtl.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (gridView == null) {
                throw new MB.Util.APPException("目前编辑的XtrGrid 只支持DevExpress.XtraGrid.Views.Grid.GridView的格式。请检查你设置的XtrGrid View是否为这种类型。");
            }
            if (objPropertys == null || objPropertys.Count == 0) {
                throw new MB.Util.APPException("业务对象没有配置对应的XML文件Columns 信息。");
            }
            //	设置控件的显示样式
            XtraGridViewHelper.Instance.SetGridView(gridView, objPropertys);

            if (gridView.Columns.Count > 0) {
                gridView.Columns.Clear();
                xtraGCtl.RepositoryItems.Clear();

            }

            //根据业务处理对象得到对应的 UI 编辑设置信息。
            int i = 0;
            foreach (MB.WinBase.Common.ColumnPropertyInfo fInfo in objPropertys.Values) {
                if (!fInfo.Visibled) continue;
                DevExpress.XtraGrid.Columns.GridColumn bdc = new DevExpress.XtraGrid.Columns.GridColumn();
                //判断该列是否可以进行编辑
                DevExpress.XtraEditors.Repository.RepositoryItem rEdit = null;
                if (editCols != null && editCols.ContainsKey(fInfo.Name))
                    rEdit = CreateEditItemByEditInfo(editCols[fInfo.Name], fInfo.CanEdit, fInfo.DataType);//根据用户XML配置的信息获取一个编辑的列。
                else
                    rEdit = CreateEditItemByCol(fInfo, false);

                rEdit.Name = fInfo.Name;

                bdc.ColumnEdit = rEdit;
                xtraGCtl.RepositoryItems.Add(rEdit);

                gridView.Columns.Add(bdc);

                GridColumnLayoutInfo layoutInfo = null;
                if (gridViewLayoutInfo != null && gridViewLayoutInfo.GridLayoutColumns.Count > 0)
                   layoutInfo = gridViewLayoutInfo.GridLayoutColumns.FirstOrDefault<MB.WinBase.Common.GridColumnLayoutInfo>(o => o.Name == fInfo.Name);

                SetEditColumn(bdc, fInfo, layoutInfo, gridViewLayoutInfo);

                i++;
            }
            //不管什么时候都恢复保存的状态
            XtraGridViewHelper.Instance.RestoreXtraGridState(bindParam.XtraGrid);
            if (objPropertys != null) {
                XtraGridViewHelper.Instance.SetGroupSummary(gridView, objPropertys);
            }

            xtraGCtl.DataSource = MB.Util.MyConvert.Instance.ToGridViewSource(bindParam.DataSource);

            setAllowFocusColumn(gridView);

            XtraContextMenuType viewPopuMenus = XtraContextMenuType.SaveGridState | XtraContextMenuType.Export
                                                | XtraContextMenuType.Delete
                                                | XtraContextMenuType.DataImport
                                                | XtraContextMenuType.ColumnsAllowSort
                                                | XtraContextMenuType.BatchAdd
                                                | XtraContextMenuType.QuickInput
                                                | XtraContextMenuType.ExcelEdit;
            //bool autoNewItem = gridViewLayoutInfo == null || !gridViewLayoutInfo.ReadOnly;
            XtraGridViewHelper.Instance.SetGridViewNewItem(xtraGCtl, true, viewPopuMenus);
            return true;
        }
        #endregion GridView...

        #region BandXtraGrid...
        /// <summary>
        /// 创建 BandXtraGrid 网格编辑或者浏览数据。
        /// </summary>
        /// <param name="bindParam"></param>
        /// <param name="colPropertys"></param>
        /// <param name="editCols"></param>
        /// <returns></returns>
        public bool CreateEditBandXtraGrid(GridDataBindingParam bindParam,
                    Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys,
                    Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols,
                     MB.WinBase.Common.GridViewLayoutInfo gridViewLayoutInfo) {

            DevExpress.XtraGrid.GridControl xtraGCtl = bindParam.XtraGrid;
            // object dataSource = bindParam.DataSource;

            DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandGridView = xtraGCtl.MainView as
                                                DevExpress.XtraGrid.Views.BandedGrid.BandedGridView;

            if (bandGridView == null) {
                throw new MB.Util.APPException("目前编辑的XtrGrid 只支持AdvBandedGridView的格式。请检查你设置的XtrGrid View是否为这种类型。");
            }
            if (colPropertys == null || colPropertys.Count == 0) {
                throw new MB.Util.APPException("ColumnPropertyInfo 的信息 不能为空。");
            }
            //	设置控件的显示样式
            //viewStye.SetStyles(xtraGCtl);
            XtraGridViewHelper.Instance.SetGridView(bandGridView, colPropertys);

            if (bandGridView.Columns.Count > 0) {
                bandGridView.Columns.Clear();
            }

            foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand band in bandGridView.Bands) {
                band.Columns.Clear();
            }
            bandGridView.Bands.Clear();

            if (gridViewLayoutInfo == null) {
                throw new MB.Util.APPException("在绑定多维表头时,可以没有在对应的XML 文件中配置 对应 GridViews/GridViewLayout", MB.Util.APPMessageType.SysErrInfo ); 
            }

            //根据业务处理对象得到对应的 UI 编辑设置信息。
            foreach (var columnLayoutInfo in gridViewLayoutInfo.GridLayoutColumns)
                CreateBandedGridViewColumns(bandGridView, null, colPropertys, editCols, gridViewLayoutInfo, columnLayoutInfo);

            //不管什么时候都恢复保存的状态
            XtraGridViewHelper.Instance.RestoreXtraGridState(bindParam.XtraGrid);

            bool isReadonly = gridViewLayoutInfo == null && gridViewLayoutInfo.ReadOnly;
            XtraContextMenuType viewPopuMenus = isReadonly ? XtraContextMenuType.SaveGridState | XtraContextMenuType.Export | XtraContextMenuType.Chart : XtraContextMenuType.SaveGridState | XtraContextMenuType.Export
                                              | XtraContextMenuType.DataImport
                                              | XtraContextMenuType.ColumnsAllowSort
                                              | XtraContextMenuType.BatchAdd
                                              | XtraContextMenuType.QuickInput;

            XtraGridViewHelper.Instance.SetGridViewNewItem(xtraGCtl, true, viewPopuMenus);
            if (isReadonly)
                bandGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

         
            if (colPropertys != null) {
                XtraGridViewHelper.Instance.SetGroupSummary(bandGridView, colPropertys);
            }
            xtraGCtl.DataSource = MB.Util.MyConvert.Instance.ToGridViewSource(bindParam.DataSource);
            return true;
        }

        /// <summary>
        /// 通过XML布局的配置信息创建布局列.
        /// </summary>
        /// <param name="bandGridView"></param>
        /// <param name="gridBand"></param>
        /// <param name="colPropertys"></param>
        /// <param name="editCols"></param>
        /// <param name="gridViewLayoutInfo"></param>
        /// <param name="columnLayoutInfo"></param>
        public void CreateBandedGridViewColumns(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandGridView,
                                                 DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand,
                                                 Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys,
                                                 Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols,
                                                 MB.WinBase.Common.GridViewLayoutInfo gridViewLayoutInfo,
                                                 MB.WinBase.Common.GridColumnLayoutInfo columnLayoutInfo) {


            if (string.Compare(columnLayoutInfo.Type, XtraGridViewHelper.BAND_TYPE_NAME, true) == 0) {
                createGridBand(bandGridView, gridBand, colPropertys, editCols, gridViewLayoutInfo, columnLayoutInfo);
            }
            else {
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();

                if (!colPropertys.ContainsKey(columnLayoutInfo.ColumnXmlCfgName)) return;

                createBandColumn(bandGridView, gridBand, colPropertys, editCols, gridViewLayoutInfo, columnLayoutInfo, bColumn);

            }

        }
        //创建网格的GridBand。
        private void createGridBand(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandGridView,
                                    DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand,
                                    Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys,
                                    Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols,
                                    MB.WinBase.Common.GridViewLayoutInfo gridViewLayoutInfo,
                                    MB.WinBase.Common.GridColumnLayoutInfo columnLayoutInfo) {

            DevExpress.XtraGrid.Views.BandedGrid.GridBand bdc = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            if (!string.IsNullOrEmpty(columnLayoutInfo.Name))
                bdc.Name = columnLayoutInfo.Name;

            if (gridBand == null)
                bandGridView.Bands.Add(bdc);
            else
                gridBand.Children.Add(bdc);

            bdc.Width = columnLayoutInfo.VisibleWidth;
            bdc.Caption = columnLayoutInfo.Text;
            bdc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            // bdc.Index  = columnLayoutInfo.Index;
            //背景颜色处理
            if (!string.IsNullOrEmpty(columnLayoutInfo.BackColor)) {
                Color bc = MB.Util.MyConvert.Instance.ToColor(columnLayoutInfo.BackColor);
                if (bc != Color.Empty) {
                    bdc.AppearanceHeader.BackColor2 = bc;
                }
            }
            //字体颜色
            if (!string.IsNullOrEmpty(columnLayoutInfo.ForeColor)) {
                Color fc = MB.Util.MyConvert.Instance.ToColor(columnLayoutInfo.ForeColor);
                if (fc != Color.Empty) {
                    bdc.AppearanceHeader.ForeColor = fc;
                }
            }
            //字体颜色
            if (columnLayoutInfo.ForeFontSize > 1) {
                bdc.AppearanceHeader.Font = new Font(bdc.AppearanceHeader.Font.FontFamily, columnLayoutInfo.ForeFontSize);
            }

            if (columnLayoutInfo.Childs != null && columnLayoutInfo.Childs.Count > 0) {
                {
                    foreach (var childLayoutInfo in columnLayoutInfo.Childs)
                        CreateBandedGridViewColumns(bandGridView, bdc, colPropertys, editCols, gridViewLayoutInfo, childLayoutInfo);
                }
            }
            if (columnLayoutInfo.Fixed != MB.WinBase.Common.FixedStyle.None) {
                if (columnLayoutInfo.Fixed == MB.WinBase.Common.FixedStyle.Left)
                    bdc.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                else
                    bdc.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            }
        }
        //创建BandColumn
        private void createBandColumn(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandGridView, DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand, Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys, Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols, MB.WinBase.Common.GridViewLayoutInfo gridViewLayoutInfo, MB.WinBase.Common.GridColumnLayoutInfo columnLayoutInfo, DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bColumn) {
            MB.WinBase.Common.ColumnPropertyInfo fInfo = colPropertys[columnLayoutInfo.ColumnXmlCfgName];
            //判断该列是否可以进行编辑
            DevExpress.XtraEditors.Repository.RepositoryItem rEdit = null;
            if (editCols != null && editCols.ContainsKey(columnLayoutInfo.ColumnXmlCfgName))
                rEdit = CreateEditItemByEditInfo(editCols[columnLayoutInfo.ColumnXmlCfgName], fInfo.CanEdit, fInfo.DataType);//根据用户XML配置的信息获取一个编辑的列。
            else
                rEdit = CreateEditItemByCol(fInfo, false);

            rEdit.Name = fInfo.Name;
            bColumn.ColumnEdit = rEdit;
            bColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //  bColumn.DisplayFormat = new DevExpress.Utils.FormatInfo 

            bandGridView.GridControl.RepositoryItems.Add(rEdit);

            //这句代码必须添加上，否则顺序的调整将会出现问题。
            bandGridView.Columns.Add(bColumn);
            if (gridBand != null)
                gridBand.Columns.Add(bColumn);

            SetEditColumn(bColumn, fInfo, columnLayoutInfo, gridViewLayoutInfo);

        }

        #endregion BandXtraGrid...


        #region Edit CardView...
        //卡片编辑
        private bool editCardGrid(MB.XWinLib.XtraGrid.GridControlEx xtraGCtl,
            object dataSource, MB.WinBase.IFace.IClientRule buiObj, Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols) {
            MB.WinBase.Atts.RuleClientLayoutAttribute layoutAtt = MB.WinBase.Atts.AttributeConfigHelper.Instance.GetClientRuleSettingAtt(buiObj);
            Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnPropertys(layoutAtt.UIXmlConfigFile);
            MB.XWinLib.XtraGrid.XtraGridViewHelper.Instance.SetEditGridAutoInfo(xtraGCtl);

            DevExpress.XtraGrid.Views.Card.CardView cardView = xtraGCtl.MainView as DevExpress.XtraGrid.Views.Card.CardView;

            if (colPropertys == null || colPropertys.Count == 0) {
                throw new MB.Util.APPException(string.Format("业务对象 {0} 没有配置对应的XML文件Columns 信息。", layoutAtt.UIXmlConfigFile));
            }
            //	设置控件的显示样式
            XtraGridViewHelper.Instance.SetCardView(xtraGCtl);

            if (cardView.Columns.Count > 0) {
                cardView.Columns.Clear();
            }
            //根据业务处理对象得到对应的 UI 编辑设置信息。
            int i = 0;
            foreach (MB.WinBase.Common.ColumnPropertyInfo fInfo in colPropertys.Values) {
                DevExpress.XtraGrid.Columns.GridColumn bdc = new DevExpress.XtraGrid.Columns.GridColumn();
                //判断该列是否可以进行编辑
                DevExpress.XtraEditors.Repository.RepositoryItem rEdit = null;
                if (editCols != null && editCols.ContainsKey(fInfo.Name))
                    rEdit = CreateEditItemByEditInfo(editCols[fInfo.Name], fInfo.CanEdit, fInfo.DataType);//根据用户XML配置的信息获取一个编辑的列。
                else
                    rEdit = CreateEditItemByCol(fInfo, true);

                bdc.ColumnEdit = rEdit;
                xtraGCtl.RepositoryItems.Add(rEdit);

                SetEditColumn(bdc, fInfo, null);

                cardView.Columns.Add(bdc);
                i++;
            }
            xtraGCtl.DataSource = dataSource;
            return true;

        }
        #endregion Edit CardView...

        #endregion 内部处理 初始化编辑相关...

        /// <summary>
        /// 设置网格列为只读状态。
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="fieldName"></param>
        public void SetGridColumnReadonly(DevExpress.XtraGrid.Views.Grid.GridView gridView, params string[] fieldName) {
            foreach (DevExpress.XtraGrid.Columns.GridColumn dc in gridView.Columns) {
                if (fieldName.Length > 0 && Array.IndexOf(fieldName, dc.FieldName) < 0) continue;
                dc.OptionsColumn.ReadOnly = true;
                dc.OptionsColumn.AllowFocus = false;
                dc.AppearanceCell.Font = new Font(dc.AppearanceCell.Font, System.Drawing.FontStyle.Bold);
                dc.AppearanceHeader.ForeColor = Color.Black;
                dc.AppearanceCell.BackColor = Color.WhiteSmoke;
            }
        }

        /// <summary>
        /// 增加选择到指定的XtraGrid 控件中。
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="defaultValue"></param>
        public void SetSelectedColStyle(DevExpress.XtraGrid.Views.Grid.GridView gridView) {
            DevExpress.XtraGrid.Columns.GridColumn gCol = gridView.Columns[COL_SELECTED_NAME];
            gCol.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
        }

        #region private function ...
        //设置可编辑列的FocusColumn
        private void setAllowFocusColumn(DevExpress.XtraGrid.Views.Grid.GridView gridView) {
            foreach (DevExpress.XtraGrid.Columns.GridColumn dc in gridView.Columns) {
                if (dc.OptionsColumn.AllowFocus) {
                    gridView.FocusedColumn = dc;
                    return;
                }
            }
            gridView.FocusedColumn = null;
        }
        //根据DataColumn 的信息配置编辑列
        private void setEditColumn(DevExpress.XtraGrid.Columns.GridColumn gridColumn, DataColumn dc) {
            gridColumn.Caption = dc.Caption;
            gridColumn.FieldName = dc.ColumnName;
            gridColumn.VisibleIndex = gridColumn.View.Columns.Count + 1;
            gridColumn.Width = 100;
            gridColumn.Tag = dc.DataType.FullName;

            if (string.Compare(gridColumn.FieldName, "TO_HOR_SUM_COL", true) == 0) { //表示当前是否为汇总列
                gridColumn.OptionsColumn.ReadOnly = true;
                gridColumn.OptionsColumn.AllowFocus = false;
                gridColumn.AppearanceCell.Font = new Font(gridColumn.AppearanceCell.Font, System.Drawing.FontStyle.Bold);
                gridColumn.AppearanceCell.BackColor = Color.WhiteSmoke;
            }
            else {

                gridColumn.AppearanceHeader.Font = new Font(gridColumn.AppearanceHeader.Font, System.Drawing.FontStyle.Bold);
                gridColumn.AppearanceHeader.ForeColor = Color.Blue; //蓝色为可编辑项
            }
        }
        /// <summary>
        /// 根据配置的XML文件设置列的信息。
        /// </summary>
        /// <param name="gridColumn"></param>
        /// <param name="columnPropertyInfo"></param>
        /// <param name="columnLayoutInfo"></param>
        /// <param name="gridViewLayoutInfo"></param>
        public void SetEditColumn(DevExpress.XtraGrid.Columns.GridColumn gridColumn, MB.WinBase.Common.ColumnPropertyInfo columnPropertyInfo,
                            MB.WinBase.Common.GridColumnLayoutInfo columnLayoutInfo) {

            SetEditColumn(gridColumn, columnPropertyInfo, columnLayoutInfo, null);
        }
        /// <summary>
        /// 根据配置的XML文件设置列的信息。
        /// </summary>
        /// <param name="pColumn"></param>
        /// <param name="pFieldInfo"></param>
        public void SetEditColumn(DevExpress.XtraGrid.Columns.GridColumn gridColumn, MB.WinBase.Common.ColumnPropertyInfo columnPropertyInfo,
                                  MB.WinBase.Common.GridColumnLayoutInfo columnLayoutInfo,
                                  MB.WinBase.Common.GridViewLayoutInfo gridViewLayoutInfo) {
            gridColumn.Caption = columnPropertyInfo.Description;
            if (columnLayoutInfo != null)
                gridColumn.FieldName = columnLayoutInfo.Name;
            else
                gridColumn.FieldName = columnPropertyInfo.Name;

            gridColumn.Name = "XtCol" + columnPropertyInfo.Name;
            gridColumn.Width = columnPropertyInfo.VisibleWidth;

            if (columnPropertyInfo.Visibled && columnPropertyInfo.VisibleWidth > 0) {
                gridColumn.VisibleIndex = columnPropertyInfo.OrderIndex;
            }
            else {
                gridColumn.VisibleIndex = -1;
            }
            if (columnPropertyInfo.DataType == "Systen.Byte[]") { //目前先假设System.Byte[] 类型都是 System.Image

            }
            else {
                gridColumn.OptionsColumn.AllowSort = columnPropertyInfo.CanSort ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;

                gridColumn.OptionsColumn.AllowGroup = columnPropertyInfo.CanGroup ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;

            }
            if (gridViewLayoutInfo != null && gridViewLayoutInfo.ReadOnly) {
                XtraGridViewHelper.Instance.SetColumn(gridColumn, columnPropertyInfo, columnLayoutInfo.Name);
            }
            else {
                gridColumn.OptionsColumn.ReadOnly = !columnPropertyInfo.CanEdit;
                if (columnPropertyInfo.CanEdit) {
                    if (columnPropertyInfo.IsKey) {
                        gridColumn.AppearanceHeader.Font = new Font(gridColumn.AppearanceHeader.Font, System.Drawing.FontStyle.Bold);
                        gridColumn.AppearanceHeader.ForeColor = Color.Red;  //不可重复项目为红色而且是粗体。
                    }
                    else if (!columnPropertyInfo.IsNull) {
                        gridColumn.AppearanceHeader.ForeColor = Color.Red;  //红色为必填项
                    }
                    else {
                        gridColumn.AppearanceHeader.ForeColor = Color.Blue;  //蓝色为可编辑项
                    }
                }
                else {
                    //gridColumn.OptionsColumn.AllowFocus = true;
                    gridColumn.OptionsColumn.AllowEdit = false;
                    gridColumn.AppearanceCell.Font = new Font(gridColumn.AppearanceCell.Font, System.Drawing.FontStyle.Bold);
                    gridColumn.AppearanceCell.BackColor = Color.WhiteSmoke;

                }
            }
            if (columnLayoutInfo != null) {
                XtraGridViewHelper.Instance.SetColumnDisplayFormat(gridColumn,columnPropertyInfo, columnLayoutInfo);

       
            }

        }

        //创建一个编辑的edit item 列
        /// <summary>
        /// 根据字段的类型名称获取一个XtrGrid 的编辑控件。
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="isCard">如果是卡片编辑的话图片的显示使用PictureEdit的方式。</param>
        /// <returns></returns>
        public DevExpress.XtraEditors.Repository.RepositoryItem CreateEditItemByCol(MB.WinBase.Common.ColumnPropertyInfo colInfo, bool isCard) {
            return createEditItemByCol(colInfo.DataType, isCard, colInfo);
        }
        /// <summary>
        /// 根据字段的类型名称获取一个XtrGrid 的编辑控件。
        /// </summary>
        /// <param name="fullTypeName"></param>
        /// <param name="isCard"> 如果是卡片编辑的话图片的显示使用PictureEdit的方式。</param>
        /// <returns></returns>
        public DevExpress.XtraEditors.Repository.RepositoryItem createEditItemByCol(string fullTypeName, bool isCard, MB.WinBase.Common.ColumnPropertyInfo propertyInfo) {
            DevExpress.XtraEditors.Repository.RepositoryItem item = null;
            switch (fullTypeName) {
                case "System.Int16":
                    DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit int16Item = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
                    int16Item.IsFloatValue = false;
                    int16Item.EditMask = "N00";
                    int16Item.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    item = int16Item;
                    if (propertyInfo != null) {
                        if (propertyInfo.MinValue > Int32.MinValue)
                            int16Item.MinValue = System.Convert.ToInt16(propertyInfo.MinValue);
                        if (propertyInfo.MaxValue < Int32.MaxValue)
                            int16Item.MaxValue = System.Convert.ToInt16(propertyInfo.MaxValue);
                    }
                    break;
                case "System.Int32":
                    item = CreateEditItemInt32(propertyInfo);
                    break;
                case "System.Decimal":
                    DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit deciItem = new XtraRepositoryItemCalcEditEx();
                    deciItem.MaxLength = 12;//初不设置的一个数，需要修改；
                    item = deciItem;
                    break;
                case "System.DateTime":
                    DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateItem = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
                    dateItem.NullDate = DateTime.MinValue;
                    dateItem.NullText = string.Empty;
                    dateItem.AutoHeight = false;
                    item = dateItem;
                    break;
                case "System.Boolean": //目前Boolean 先用CheckedBox 来表达，如果需要再修改为Group Radio
                    DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit boolItem = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                    item = boolItem;
                    break;
                case "System.Byte[]":
                    if (!isCard) {
                        DevExpress.XtraEditors.Repository.RepositoryItemImageEdit imageItem = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
                        //imageItem.NullText = "";
                        item = imageItem;
                    }
                    else {
                        DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit picItem = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
                        item = picItem;
                    }
                    break;
                default:
                    DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtItem = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
                    item = txtItem;
                    break;
            }
            return item;//new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit(); 
        }
        public DevExpress.XtraEditors.Repository.RepositoryItem CreateEditItemInt32() {
            return CreateEditItemInt32(null);
        }
        /// <summary>
        /// 创建Int32的编辑项
        /// </summary>
        /// <returns></returns>
        public DevExpress.XtraEditors.Repository.RepositoryItem CreateEditItemInt32(MB.WinBase.Common.ColumnPropertyInfo propertyInfo) {
            DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit int32Item = new MB.XWinLib.XtraGrid.XtraRepositoryItemNumberEdit();
            int32Item.IsFloatValue = false;
            int32Item.EditMask = "N00";
            int32Item.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            int32Item.ValidateOnEnterKey = true;
            int32Item.Buttons[0].Visible = false;

            if (propertyInfo != null) {
                if (propertyInfo.MinValue > Int32.MinValue)
                    int32Item.MinValue = System.Convert.ToInt32(propertyInfo.MinValue);
                if (propertyInfo.MaxValue < Int32.MaxValue)
                    int32Item.MaxValue = System.Convert.ToInt32(propertyInfo.MaxValue);
            }
            return int32Item;
        }
        /// <summary>
        /// 在指定的网格中追加选择列。
        /// 注意：需要追加选择列的实体对象必须包含Selected 属性。
        /// </summary>
        /// <param name="grdCtl">需要加选择列的网格</param>
        /// <returns></returns>
        public DevExpress.XtraGrid.Columns.GridColumn AppendEditSelectedColumn(DevExpress.XtraGrid.GridControl grdCtl) {
            return AppendEditSelectedColumn(grdCtl, string.Empty, string.Empty);
        }
        /// <summary>
        /// 在指定的网格中追加选择列。
        /// </summary>
        /// <param name="grdCtl">需要加选择列的网格</param>
        /// <param name="columnCaption">列的标题</param>
        /// <param name="columnFieldName">列的绑定字段名</param>
        public DevExpress.XtraGrid.Columns.GridColumn AppendEditSelectedColumn(DevExpress.XtraGrid.GridControl grdCtl, string columnCaption, string columnFieldName) {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = grdCtl.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;

            DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit boolItem = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            col.ColumnEdit = boolItem;
            grdCtl.RepositoryItems.Add(boolItem);
            gridView.Columns.Insert(0, col);

            if (string.IsNullOrEmpty(columnCaption))
                col.Caption = "选择";
            else
                col.Caption = columnCaption;

            if (string.IsNullOrEmpty(columnFieldName))
                col.FieldName = "Selected";
            else
                col.FieldName = columnFieldName;
            
            col.OptionsColumn.AllowEdit = true;
            col.OptionsFilter.AllowFilter = false;
            col.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            col.OptionsColumn.AllowMove = false;
            col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;

            col.AppearanceHeader.Font = new Font(col.AppearanceHeader.Font, System.Drawing.FontStyle.Bold);
            col.AppearanceHeader.ForeColor = Color.Blue;

            col.VisibleIndex = 0;
            col.Width = 60;

            return col;
        }

        /// <summary>
        /// 通过设置的Edit 列信息得到一个编辑列的控件。向下兼容以前版本
        /// </summary>
        /// <param name="editInfo"></param>
        /// <returns></returns>
        public DevExpress.XtraEditors.Repository.RepositoryItem CreateEditItemByEditInfo(MB.WinBase.Common.ColumnEditCfgInfo editInfo)
        {
            return CreateEditItemByEditInfo(editInfo, true, COL_UNKNOW_DATE_TYPE_FOR_CREATING);
        }

        /// <summary>
        /// 通过设置的Edit 列信息得到一个编辑列的控件。向下兼容以前版本
        /// </summary>
        /// <param name="editInfo"></param>
        /// <param name="allowEdit"></param>
        /// <returns></returns>
        public DevExpress.XtraEditors.Repository.RepositoryItem CreateEditItemByEditInfo(MB.WinBase.Common.ColumnEditCfgInfo editInfo, bool allowEdit)
        {
            return CreateEditItemByEditInfo(editInfo, allowEdit, COL_UNKNOW_DATE_TYPE_FOR_CREATING);
        }

        /// <summary>
        /// 通过设置的Edit 列信息得到一个编辑列的控件。
        /// </summary>
        /// <param name="editInfo"></param>
        /// <returns></returns>
        public DevExpress.XtraEditors.Repository.RepositoryItem CreateEditItemByEditInfo(MB.WinBase.Common.ColumnEditCfgInfo editInfo, string colDataType)
        {
            return CreateEditItemByEditInfo(editInfo, true, colDataType);
        }
        /// <summary>
        /// 通过设置的Edit 列信息得到一个编辑列的控件。
        /// </summary>
        /// <param name="editInfo"></param>
        /// <param name="allowEdit"></param>
        /// <returns></returns>
        public DevExpress.XtraEditors.Repository.RepositoryItem CreateEditItemByEditInfo(MB.WinBase.Common.ColumnEditCfgInfo editInfo, bool allowEdit, string colDataType) {

            DevExpress.XtraEditors.Repository.RepositoryItem item = null;
            MB.WinBase.Common.EditControlType controlType = (MB.WinBase.Common.EditControlType)Enum.Parse(typeof(MB.WinBase.Common.EditControlType), editInfo.EditControlType);

            switch (controlType) {
                case MB.WinBase.Common.EditControlType.Combox_DropDownList:
                case MB.WinBase.Common.EditControlType.Combox_DropDown:
                    DevExpress.XtraEditors.Repository.RepositoryItemComboBox cobItem = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
                    //cobItem.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton());     
                    cobItem.AutoComplete = true;
                    setRComboxItems(cobItem, editInfo);
                    item = cobItem;
                    break;
                case MB.WinBase.Common.EditControlType.ComboCheckedListBox://如果设置为
                case MB.WinBase.Common.EditControlType.LookUpEdit:
                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUp = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();

                    XtraRepositoryItemLookUpEditHelper.Instance.CreateLookUpEditItems(lookUp, editInfo, colDataType);
                    //暂时把所有的LookUp 的格式 转换为PopupCOntainer 的格式，正确的处理方法应该是区别对待。
                    // DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit lookUp = new XtraRepositoryItemPopupContainerEdit(editInfo, allowEdit);
                    // lookUp.Name = editInfo.Name;
                    item = lookUp;
                    break;
                case MB.WinBase.Common.EditControlType.ImageIcoEdit:
                    var imageEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
                    item = imageEdit;
                    break;
                case MB.WinBase.Common.EditControlType.ColorEdit:
                    var colorEdit = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
                    colorEdit.ColorText = DevExpress.XtraEditors.Controls.ColorText.Integer;
                    colorEdit.StoreColorAsInteger = true;
                    item = colorEdit;
                    break;
                //
                //case "ImageCombox": //可以当做存储键/值列并且是只能选择的Combox 控件来使用。
                //    DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imgCob = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
                //    imgCob.AllowFocused = false;
                //    imgCob.AutoHeight = false;
                //    setImageCobItems(imgCob, editInfo);
                //    item = imgCob;
                //    break;
                //case "PopupContainer":
                //    DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit popuCon = new XtraRepositoryItemPopupContainerEdit(editInfo, allowEdit);
                //    item = popuCon;
                //    break;
                //case "Ellipsis":
                //    //DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit butEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
                //    //butEdit.ButtonClick +=new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(butEdit_ButtonClick);
                //    //butEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                //    //																		   new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)});
                //    //					RepositoryItemMyButtonEdit butEdit = new RepositoryItemMyButtonEdit();
                //    //
                //    //					item = butEdit;
                //    break;
                case MB.WinBase.Common.EditControlType.ClickButtonInput:
                    XtraRepositoryItemClickButtonEdit butEdit = new XtraRepositoryItemClickButtonEdit(editInfo);
                    // (butEdit.Buttons[0] as MyButtonEdit).ColumnEditCfgInfo = editInfo; 
                    item = butEdit;

                    break;
                case MB.WinBase.Common.EditControlType.PopupRegionEdit:
                    XtraRepositoryItemRegionEdit regionEdit = new XtraRepositoryItemRegionEdit(editInfo);
                    item = regionEdit;
                    break;
                default:
                    throw new MB.Util.APPException("该EditCols 类型" + editInfo.EditControlType + "还没有进行处理。请确定配置的信息是否正确?");
                //break;
            }
            return item;
        }

        //设置combobox 的items 值。
        private void setRComboxItems(DevExpress.XtraEditors.Repository.RepositoryItemComboBox cobItem, MB.WinBase.Common.ColumnEditCfgInfo editInfo) {
            if (editInfo.DataSource == null)
                return;

            string txtName = editInfo.TextFieldName;
            cobItem.Items.Clear();

            IList lstDatas = editInfo.DataSource as IList;
            if (lstDatas != null) {
                foreach (object entity in lstDatas) {
                    object txtValue = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, txtName);
                    cobItem.Items.Add(txtValue);
                }
            }
            else {
                DataTable dtData = MB.Util.MyConvert.Instance.ToDataTable(editInfo.DataSource, string.Empty);
                foreach (DataRow dr in dtData.Rows) {
                    cobItem.Items.Add(dr[txtName]);
                }
            }
        }

        //设置 ImageComboBox 的值。
        private void setImageCobItems(DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cobItem, MB.WinBase.Common.ColumnEditCfgInfo editInfo) {
            if (editInfo.DataSource == null)
                return;

            string txtName = editInfo.TextFieldName;
            cobItem.Items.Clear();

            IList lstDatas = editInfo.DataSource as IList;
            if (lstDatas != null) {
                foreach (object entity in lstDatas) {
                    object value = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, txtName);
                    cobItem.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(value));
                }
            }
            else {
                DataTable dtData = MB.Util.MyConvert.Instance.ToDataTable(editInfo.DataSource, string.Empty);
                foreach (DataRow dr in dtData.Rows) {
                    cobItem.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dr[txtName]));
                }
            }
        }
        #endregion private function ...


        #region public 方法...
        /// <summary>
        /// 设置整形编辑的最小值。
        /// </summary>
        /// <param name="ctlGrid"></param>
        public void SetInt32EditColMinValue(DevExpress.XtraGrid.GridControl xtraGrid, int val) {
            foreach (DevExpress.XtraEditors.Repository.RepositoryItem editItem in xtraGrid.RepositoryItems) {
                if (string.Compare(editItem.GetType().Name, "RepositoryItemSpinEdit", true) == 0) {
                    DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit int32Item = editItem as DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit;
                    int32Item.MinValue = val;
                }
            }
        }

        /// <summary>
        /// 获取当前正在编辑的行，如果存在多行的选择状态，那么就获取集合中的第一行。
        /// </summary>
        /// <returns></returns>
        public DataRow GetCurrentEditRow(DevExpress.XtraGrid.GridControl xtraGrid) {
            Debug.Assert(xtraGrid.DataSource != null, "XtraGrid控件的数据源没有设置！", "");
            DevExpress.XtraGrid.Views.Grid.GridView gridView = xtraGrid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (gridView.RowCount > 0) {
                int[] cRows = gridView.GetSelectedRows();
                if (cRows == null)
                    return null;
                DataRow cRow = null;
                if (cRows.Length > 0) {
                    cRow = gridView.GetDataRow(cRows[0]);
                }
                else {
                    cRow = gridView.GetDataRow(0);
                }
                return cRow;
            }
            return null;
        }
        /// <summary>
        /// 删除指定控件中有焦点的数据行。
        /// </summary>
        /// <param name="ctrGrid"></param>
        public void DeleteFocusedRow(DevExpress.XtraGrid.GridControl xtraGrid) {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = xtraGrid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (gridView.FocusedRowHandle < 0) return;

            gridView.DeleteRow(gridView.FocusedRowHandle);

            xtraGrid.RefreshDataSource();
        }
        #endregion public 方法...

    }
}
