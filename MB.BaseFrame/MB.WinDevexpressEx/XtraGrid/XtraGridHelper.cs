//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-18
// Description	:	XtraGridHelper  封装对XtraGrid的操作处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Text;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

using DevExpress.XtraGrid;
using DevExpress.XtraExport;
using DevExpress.XtraGrid.Export;
using System.ComponentModel;
using DevExpress.XtraGrid.Views.Base;
using MB.XWinLib.Share;
using DevExpress.XtraPrinting;
using MB.WinBase.IFace;
using System.Drawing;
using MB.WinBase.Common.DynamicGroup;

namespace MB.XWinLib.XtraGrid
{
    /// <summary>
    /// XtraGridHelper  封装对XtraGrid的操作处理。
    /// </summary>
    public class XtraGridHelper
    {

        #region 变量定义...
        //private static readonly string KEY_NAME = "ID";
        //private static readonly string SAVE_EDIT_GRID_MSG = "已成功处理了{0} 行,发现以下错误{1}。系统自动停止处理。";
        //private static readonly string DELETE_EDIT_GRID_MSG = "当前没有选择的行，请选择！！";
        //private static int ExportNum = 0;
        //private static readonly string BUSINESS_RULE_DLL_NAME = "UP4.Rule.DLL";
        private static readonly string GRID_VIEW_LAYOUT_NAME = "DefaultGridView";
        #endregion 变量定义...

        #region Instance...
        private static Object _Obj = new object();
        private static XtraGridHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected XtraGridHelper() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static XtraGridHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new XtraGridHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...


        #region 一般Grid View绑定处理...
        /// <summary>
        /// 刷新已经绑定的网格数据。
        /// </summary>
        /// <param name="xtraGrid"></param>
        /// <param name="dataSource"></param>
        public void RefreshDataGrid(DevExpress.XtraGrid.GridControl xtraGrid, object dataSource) {
            if (xtraGrid.DataSource == null)
                throw new MB.Util.APPException("当前网格绑定的数据源为空,不能使用方法 RefreshDataGrid", MB.Util.APPMessageType.SysErrInfo);

            IList oldList = xtraGrid.DataSource as IList;
            if (oldList != null) {
                IList lstData = dataSource as IList;
                oldList.Clear();
                foreach (object newItem in lstData)
                    oldList.Add(newItem);

                xtraGrid.RefreshDataSource();
                return;
            }
            DataView dv = xtraGrid.DataSource as DataView;
            if (dv != null) {
                DataTable dtData = MB.Util.MyConvert.Instance.ToDataTable(dataSource, "");
                if (dtData != null) {
                    DataView newDv = dtData.DefaultView;
                    newDv.RowFilter = dv.RowFilter;
                    xtraGrid.DataSource = newDv;
                }
                return;
            }

            throw new MB.Util.APPException(string.Format("当前网格绑定的数据源不支持 类型{0} ", xtraGrid.DataSource.GetType().FullName), MB.Util.APPMessageType.SysErrInfo);
        }

        /// <summary>
        /// 把数据绑定到动态聚组结果展示的xtraGrid中
        /// </summary>
        /// <param name="xtraGrid"></param>
        /// <param name="dataSource">数据源</param>
        /// <param name="xmlFileName">动态聚组的客户端配置文件</param>
        /// <returns></returns>
        public bool BindingToXtraGridForDynamicGroup(DevExpress.XtraGrid.GridControl xtraGrid, object dataSource, string xmlFileName)
        {
            DynamicGroupCfgHelper cfgHelper = new DynamicGroupCfgHelper(xmlFileName);
            var cols = cfgHelper.GetResultColPropertys();
            return BindingToXtraGrid(xtraGrid, dataSource, cols, null, string.Empty, false);
        }

        /// <summary>
        ///  把数据绑定到XtraGrid 控件中。
        /// </summary>
        /// <param name="xtraGrid"></param>
        /// <param name="dataSource"></param>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public bool BindingToXtraGrid(DevExpress.XtraGrid.GridControl xtraGrid, object dataSource, string xmlFileName) {
            var cols = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnPropertys(xmlFileName);
            var editCols = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnEdits(cols, xmlFileName);

            return BindingToXtraGrid(xtraGrid, dataSource, cols, editCols, xmlFileName, false);
        }
        /// <summary>
        /// 把数据绑定到XtraGrid 控件中。
        /// </summary>
        /// <param name="xtraGrid"></param>
        /// <param name="dataSource"></param>
        /// <param name="colPropertys"></param>
        /// <param name="imageDockRow"></param>
        /// <param name="editCols"></param>
        /// <returns></returns>
        public bool BindingToXtraGrid(DevExpress.XtraGrid.GridControl xtraGrid, object dataSource,
                                     Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys,
                                     Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols, string xmlFileName,
                                     bool imageDockRow) {
            using (MB.Util.MethodTraceWithTime trace = new MB.Util.MethodTraceWithTime("MB.XWinLib.XtraGrid.XtraGridHelper.BindingToXtraGrid", xmlFileName)) {
                try {
                    MB.WinBase.Common.GridViewLayoutInfo gridViewLayoutInfo = null;
                    #region 根据GridViewLayoutInfo的配置来决定GridView的类型，甚至动态创建GridView
                    if (!string.IsNullOrEmpty(xmlFileName)) {
                        gridViewLayoutInfo =
                            MB.WinBase.LayoutXmlConfigHelper.Instance.GetGridColumnLayoutInfo(xmlFileName, GRID_VIEW_LAYOUT_NAME);
                        //首先看是否配置为DefaultGridView,如果是，则不做任何事情
                        if (gridViewLayoutInfo == null || gridViewLayoutInfo.GridLayoutColumns == null) {
                            //如果没有配置DefaultGridView，则看是否有配置其他的GridView
                            gridViewLayoutInfo = MB.WinBase.LayoutXmlConfigHelper.Instance.GetGridColumnLayoutInfo(xmlFileName, string.Empty);
                            if (gridViewLayoutInfo != null && gridViewLayoutInfo.GridLayoutColumns != null) {
                                if (gridViewLayoutInfo.GridViewType == WinBase.Common.GridViewType.AdvBandedGridView) {
                                    //动态创建AdvBandedGridView
                                    DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView(xtraGrid);
                                    xtraGrid.MainView = advBandedView;
                                }
                            }
                        }
                    }

                    #endregion
                    string viewTypeName = xtraGrid.MainView.GetType().Name;

                    if (viewTypeName == "GridView") {
                        return createGridView(xtraGrid, dataSource, colPropertys, imageDockRow, editCols, gridViewLayoutInfo);
                    }
                    else {
                        XtraGridEditHelper.Instance.CreateEditBandXtraGrid(new GridDataBindingParam(xtraGrid, dataSource), colPropertys, editCols, gridViewLayoutInfo);
                        return true;
                    }
                }
                catch (Exception ex) {
                    throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, string.Format("通过XML文件{0}绑定XtraGrid 出错", xmlFileName));
                }
            }
        }
        //绑定到GridView 数据源。
        private bool createGridView(DevExpress.XtraGrid.GridControl xtraGrid, object dataSource, Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys,
            bool imageDockRow, Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols, MB.WinBase.Common.GridViewLayoutInfo gridViewLayoutInfo) {
            object pictureEdit;

            MB.XWinLib.XtraGrid.GridControlEx grdEx = xtraGrid as MB.XWinLib.XtraGrid.GridControlEx;
            if (grdEx != null) {
                grdEx.ShowOptionMenu = true;
                if(grdEx.ContextMenu == null)
                    grdEx.ReSetContextMenu(XtraContextMenuType.SaveGridState | XtraContextMenuType.Export | XtraContextMenuType.ColumnsAllowSort | XtraContextMenuType.Copy | XtraContextMenuType.Chart);
            }
            if (xtraGrid.RepositoryItems.Count > 0) {
                if (imageDockRow) {
                    pictureEdit = xtraGrid.RepositoryItems[0] as DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit;
                }
                else {
                    pictureEdit = xtraGrid.RepositoryItems[0] as DevExpress.XtraEditors.Repository.RepositoryItemImageEdit;
                }
            }
            else {
                if (imageDockRow) {
                    pictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
                    (pictureEdit as DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit).SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
                }
                else {
                    pictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
                }
                if (imageDockRow) {
                    xtraGrid.RepositoryItems.Add(pictureEdit as DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit);
                }
                else {
                    xtraGrid.RepositoryItems.Add(pictureEdit as DevExpress.XtraEditors.Repository.RepositoryItemImageEdit);
                }

            }
            DevExpress.XtraGrid.Views.Grid.GridView gridView = xtraGrid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;

            XtraGridViewHelper.Instance.SetGridView(gridView, colPropertys);

            XtraGridViewHelper.Instance.CreateViewColumns(xtraGrid, dataSource, colPropertys, pictureEdit, editCols, gridViewLayoutInfo);


            //if (colPropertys != null) {
            //    XtraGridViewHelper.Instance.SetGroupSummary(gridView, colPropertys);
            //}

            //增加保存Grid UI 操作状态而增加的。
            XtraGridViewHelper.Instance.RestoreXtraGridState(xtraGrid);
            DataView dv = MB.Util.MyConvert.Instance.ToDataView(dataSource, null);
            if (dv != null)
                xtraGrid.DataSource = dv;
            else
                xtraGrid.DataSource = dataSource;
            return true;
        }

        #endregion 一般Grid View绑定处理...


        /// <summary>
        /// 操作当用户点击XtraGrid ContextMenu 时处理的事项。
        /// </summary>
        /// <param name="containerForm"></param>
        /// <param name="xtraGrid"></param>
        /// <param name="menuType"></param>
        public void HandleClickXtraContextMenu(MB.WinBase.IFace.IForm containerForm,
                                DevExpress.XtraGrid.GridControl xtraGrid, XtraContextMenuType menuType) {
            //if(containerForm==null)
            //    return;
            switch (menuType) {
                case XtraContextMenuType.Add:
                case XtraContextMenuType.Print:
                case XtraContextMenuType.Aggregation:
                    MB.WinBase.MessageBoxEx.Show("目前暂时先不提供右键操作的功能，请直接从工具栏上进行选择");
                    break;
                case XtraContextMenuType.Delete:
                    //每个网格的数据 删除涉及到具体的操作，需要手工进行处理。
                    // MB.XWinLib.XtraGrid.XtraGridEditHelper.Instance.DeleteFocusedRow(xtraGrid);    
                    break;
                case XtraContextMenuType.Export:
                    ExportToExcelAndShow(xtraGrid);
                    break;
                case XtraContextMenuType.Copy:
                    dataCopy(xtraGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView);
                    break;
                case XtraContextMenuType.SaveGridState:
                    showFrmGridLayoutManager(xtraGrid);
                    break;
                case XtraContextMenuType.ViewControl:
                case XtraContextMenuType.ExportAsTemplet:
                case XtraContextMenuType.Past:
                case XtraContextMenuType.DataImport:
                case XtraContextMenuType.ExcelEdit:
                default:
                    MB.WinBase.MessageBoxEx.Show("该菜单项对于当前对象操作无效,有可能是该对象不能进行该菜单项操作或者开发人员还没有实现该菜单项");
                    break;
            }
        }

        //显示Grid状态设置窗体
        private void showFrmGridLayoutManager(DevExpress.XtraGrid.GridControl xtraGrid)
        {
            frmGridLayoutManager frm = new frmGridLayoutManager(xtraGrid);
            frm.ShowDialog();
        }

        /// <summary>
        /// 把XtraGrid中的数据倒出到Excel 中
        /// </summary>
        /// <param name="pGridView"></param>
        /// <param name="pExportFullName"></param>
        public void ExportToExcel(DevExpress.XtraGrid.GridControl pDg, string pExportFullName) {
            if (pDg.MainView != null) {
                try {
                    ExportXlsProvider ex = new ExportXlsProvider(pExportFullName);
                    BaseExportLink link = pDg.MainView.CreateExportLink(ex);
                    ((GridViewExportLink)link).ExpandAll = false;
                    link.ExportTo(true);
                }
                catch (Exception e) {
                    throw e;
                }
            }
        }

        /// <summary>
        /// 把XtraGrid中的数据倒出到Txt 中
        /// </summary>
        /// <param name="pDg"></param>
        /// <param name="pExportFullName"></param>
        public void ExportToText(DevExpress.XtraGrid.GridControl pDg, string pExportFullName) {
            if (pDg.MainView != null)
            {
                try
                {
                    //ExportTxtProvider ex = new ExportTxtProvider(pExportFullName);
                    //ExportTo(pDg, ex);

                    TextExportOptions option = new TextExportOptions();
                    //option.Encoding = Encoding.UTF8;
                    //转成中文格式,方便EXCEL直接打开
                    option.Encoding = System.Text.Encoding.GetEncoding("GB2312");
                    option.TextExportMode = TextExportMode.Value;
                    option.Separator = "\t";
                    option.QuoteStringsWithSeparators = false;
                    
                    pDg.MainView.ExportToText(pExportFullName,option);               
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        private void ExportTo(DevExpress.XtraGrid.GridControl pDg, IExportProvider provider)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            BaseExportLink link = pDg.MainView.CreateExportLink(provider);
            (link as GridViewExportLink).ExpandAll = false;
            link.ExportTo(true);
            provider.Dispose();

            Cursor.Current = currentCursor;
        }

        /// <summary>
        /// 把XtraGrid中的数据倒出到Excel 中 并打开Excel 进行显示
        /// edit cdc 2012-01-09 修改为导出后不再直接打开
        /// </summary>
        /// <param name="pDg"></param>
        public void ExportToExcelAndShow(DevExpress.XtraGrid.GridControl pDg) {
            //string temPath = MB.Util.General.GeApplicationDirectory() + @"Temp\";
            //bool b = System.IO.Directory.Exists(temPath);
            //if (!b) {
            //    System.IO.Directory.CreateDirectory(temPath);
            //}
            //string fullPath = temPath + System.Guid.NewGuid().ToString() + ".xls";

            var fileDialog = new System.Windows.Forms.SaveFileDialog();
            fileDialog.Filter = "文本文件|.txt|Excel 文件|.xls";
            fileDialog.ShowDialog();
            var fileFullName = fileDialog.FileName;
            if (string.IsNullOrEmpty(fileFullName)) return;
            //var b = System.IO.File.Exists(fullPath);
            try {

                //MB.WinBase.InvokeMethodWithWaitCursor.InvokeWithWait(() => ExportToExcel(pDg, fileFullName));
                MessageBox.Show(fileFullName);

                if (fileFullName.EndsWith(".txt"))
                {
                    ExportToText(pDg, fileFullName);
                }
                else
                {
                    ExportToExcel(pDg, fileFullName);
                }

                var b = System.IO.File.Exists(fileFullName);
                //判断该文件是否导出成功，如果是那么直接打开该文件
                if (b) {
                    var dre = MB.WinBase.MessageBoxEx.Question("文件导出成功，是否需要打开文件所在的目录");
                    if (dre == DialogResult.Yes) {
                        System.Diagnostics.ProcessStartInfo explore = new System.Diagnostics.ProcessStartInfo();
                        explore.FileName = "explorer.exe";
                        explore.Arguments = System.IO.Path.GetDirectoryName(fileFullName);
                        System.Diagnostics.Process.Start(explore);
                    }
                }
            }
            catch (Exception e) {
                MB.Util.TraceEx.Write("导出Excel 文件出错！" + e.Message, MB.Util.APPMessageType.SysErrInfo);
                MB.WinBase.MessageBoxEx.Show("导出Excel 文件出错！");
            }
        }

        

        //复制数据到剪贴板中
        private void dataCopy(DevExpress.XtraGrid.Views.Grid.GridView gridViewMain) {
            if (gridViewMain == null) return;
            string ret = string.Empty;
            int rowIndex = -1;
            var rows = gridViewMain.GetSelectedRows();
            var  focusCol = gridViewMain.FocusedColumn;
            if (focusCol == null || rows.Length == 0) {
                Clipboard.SetDataObject(string.Empty);
            }

            foreach (var index in rows) {
                if (rowIndex != index) {
                    if (!string.IsNullOrEmpty(ret))
                        ret += "\r\n";
                } else {
                    if (!string.IsNullOrEmpty(ret))
                        ret += "\t";
                }

                ret += gridViewMain.GetRowCellDisplayText(index, focusCol);
                rowIndex = index;
            }

            Clipboard.SetDataObject(ret);
        }
    }
}
