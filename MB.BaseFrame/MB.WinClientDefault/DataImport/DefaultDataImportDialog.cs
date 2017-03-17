//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2009-06-08
// Description	:	数据导入处理相关。
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

using MB.WinBase.Common;
using Microsoft.Win32;
using System.IO;
using System.Reflection;
using DevExpress.XtraGrid.Views.Grid;
using MB.XWinLib.XtraGrid;
using MB.XWinLib.DesignEditor;  
namespace MB.WinClientDefault.DataImport {
    /// <summary>
    /// 数据导入处理相关。
    /// </summary>
    public partial class DefaultDataImportDialog : AbstractBaseForm, MB.WinBase.IFace.IForm {
        private const string _BIZ_CHECKING_COLUMN_NAME = "BIZ_CHECKING_FLAG";

        private Dictionary<string,MB.WinBase.Common.ColumnPropertyInfo> _ColPropertys;
        private Dictionary<string,MB.WinBase.Common.ColumnEditCfgInfo> _EditCols;
        private string _XmlFileName;
        private DataSet _CurrentImportData;
        private DataImportInfo _CurrentImportInfo;
        private static readonly string MESSAGE_DATA_COUNT = "当前需要导入的数据有{0} 条,请过滤掉不需要的数据 ";
        private static readonly string MESSAGE_IMPORT_WARMING = " 数据量太大,建议分批导入";
        private const int WARMING_MAX_COUNT = 1000;
        private const int MAX_IMPORT_COUNT = 0x100000;
        private bool _ImportImmediate;
        private MB.WinBase.IFace.IClientRuleQueryBase _ClientRuleObject;
        #region 构造函数...
        /// <summary>
        /// 构造函数...
        /// </summary>
        public DefaultDataImportDialog(bool importImmediate) {
            InitializeComponent();

            grdCtlMain.Dock = DockStyle.Fill;

            //_CurrentImportData = dsData;
            //_ColPropertys = colPropertys;
            //_EditCols = editCols;
            //_XmlFileName = xmlFileName;
            grdCtlMain.ValidedDeleteKeyDown = true;
            this.Load += new EventHandler(DefaultDataImportDialog_Load);

            _ImportImmediate = importImmediate;

            butSure.Text = importImmediate ? "直接导入(&I)" : "整理完成(&S)"; 
        }
        #endregion 构造函数...

        #region  public 成员  ShowDataImport ...
        public static DataImportInfo ShowDataImport(IWin32Window parent, MB.WinBase.IFace.IClientRule buiObj, string xmlFileName, bool importImmediate) {
            return ShowDataImport(parent, buiObj, xmlFileName, importImmediate, null);
        }

        /// <summary>
        ///  显示数据导入处理窗口。
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="buiObj"></param>
        /// <param name="xmlFileName"></param>
        /// <param name="importImmediate">判断是否为直接导入</param>
        /// <returns></returns>
        public static DataImportInfo ShowDataImport(IWin32Window parent, MB.WinBase.IFace.IClientRule buiObj, string xmlFileName, bool importImmediate, Func<DataSet, DataSet> businessCheckAfterLoadImportData) {
            string file = MB.WinBase.ShareLib.Instance.SelectedFile("Excel 文件 (*.xls)|*.xls|文本文件（*.txt）|*.txt");
            if (string.IsNullOrEmpty(file)) return null;
            if (!file.EndsWith(".xls") && !file.EndsWith(".txt"))
                throw new MB.Util.APPException(string.Format("文件 {0} 对应的文件类型暂不支持!", file), MB.Util.APPMessageType.DisplayToUser);

            try {
                DefaultDataImportDialog dialog = new DefaultDataImportDialog(importImmediate);
                DataSet dsData = dialog.LoadImportData(parent, buiObj, xmlFileName, file);
                
                //新增插入业务检查,检查的方法从外部嵌入
                if (businessCheckAfterLoadImportData != null) {
                    dsData = dialog.BusinessValidate(businessCheckAfterLoadImportData, dsData);
                }
                bool exists = dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0;
                if (exists) {
                    DialogResult importSure = dialog.ShowDialog();
                    if (importSure == DialogResult.OK) {
                        return dialog.CurrentImportInfo;
                    }
                }
                else {
                    DialogResult re = MB.WinBase.MessageBoxEx.Question("没有匹配到任何需要导入的数据,是否需要重新选择");
                    if (re == DialogResult.Yes)
                        return ShowDataImport(parent, buiObj, xmlFileName, importImmediate);
                }
                return null;
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(string.Format("加载需要导入的文件{0} 有误！", file), MB.Util.APPMessageType.DisplayToUser,ex);
            }
        }
        /// <summary>
        /// 当前整理完成，准备导入的数据。
        /// </summary>
        public DataImportInfo CurrentImportInfo {
            get {
                return _CurrentImportInfo;
            }
        }

        /// <summary>
        ///加载需要导入的数据。/
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="buiObj"></param>
        /// <param name="xmlFileName"></param>
        /// <param name="importFile"></param>
        /// <returns></returns>
        public DataSet  LoadImportData(IWin32Window parent, MB.WinBase.IFace.IClientRule buiObj, string xmlFileName, string importFile) {
            try {
                MB.WinBase.IFace.IForm iForm = parent as MB.WinBase.IFace.IForm;
                if (iForm != null)
                    _ClientRuleObject = iForm.ClientRuleObject;

                var colPropertys = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnPropertys(xmlFileName);
                var editCols = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnEdits(colPropertys, xmlFileName);
                DataSet dsData = createNULLDataByFieldPropertys(colPropertys);

                if (importFile.EndsWith(".txt"))
                {
                    MB.WinEIDrive.Import.TxtImport txtImport = new MB.WinEIDrive.Import.TxtImport(dsData, importFile);
                    ImportEngine helper = new ImportEngine(editCols, grdCtlMain, txtImport);
                    helper.Commit();
                }
                else
                {
                    if (ExistsRegedit() > 0)
                    {
                        MB.WinEIDrive.Import.OfficeXlsImport xlsImport = new WinEIDrive.Import.OfficeXlsImport(dsData, importFile);
                        ImportEngine helper = new ImportEngine(editCols, grdCtlMain, xlsImport);
                        helper.Commit();
                    }
                    else
                    {
                        MB.WinEIDrive.Import.XlsImport xlsImport = new MB.WinEIDrive.Import.XlsImport(dsData, importFile);
                        ImportEngine helper = new ImportEngine(editCols, grdCtlMain, xlsImport);
                        helper.Commit();
                    }
                }
                //移除空行数据
                MB.Util.DataValidated.Instance.RemoveNULLRowData(dsData);

                _ColPropertys = colPropertys;
                _EditCols = editCols;
                _CurrentImportData = dsData;
                _XmlFileName = xmlFileName;
                return _CurrentImportData;
            }
            catch (MB.Util.APPException aEx) {
                throw aEx;
            }
            catch (Exception ex) {
                throw new MB.Util.APPException("数据导入处理有误。", MB.Util.APPMessageType.SysFileInfo, ex);
            }
        }

        /// <summary>
        /// 插入业务检查，把检查的结果返回
        /// </summary>
        /// <param name="businessCheckAfterLoadImportData">插入的业务查询的方法</param>
        /// <param name="dsData"></param>
        /// <returns></returns>
        public DataSet BusinessValidate(Func<DataSet, DataSet> businessCheckAfterLoadImportData, DataSet dsData) {
            //新增插入业务检查,检查的方法从外部嵌入
            if (businessCheckAfterLoadImportData != null) {
                dsData = businessCheckAfterLoadImportData(dsData);
                bool hasData = dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0;
                //对业务处理过的对象进行排序
                if (hasData) {
                    DataView dv = dsData.Tables[0].DefaultView;
                    dv.Sort = string.Format("{0} ASC", _BIZ_CHECKING_COLUMN_NAME);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dv.ToTable());
                    _CurrentImportData = ds;
                }
            }
            return _CurrentImportData;

        }

        #endregion  public 成员  ShowDataImport ...

        public int ExistsRegedit()
        {
            int ifused = 0;

            try
            {
                Assembly assembly = Assembly.LoadFrom("Microsoft.Office.Interop.Excel.dll");
                if (assembly == null) return 0;
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write(ex.Message);
                return 0;
            }

            RegistryKey rk = Registry.LocalMachine;
            RegistryKey akey = rk.OpenSubKey(@"SOFTWARE\Microsoft\Office\11.0\Excel\InstallRoot\");//查询2003

            RegistryKey akey07 = rk.OpenSubKey(@"SOFTWARE\Microsoft\Office\12.0\Excel\InstallRoot\");//查询2007
            RegistryKey akey10 = rk.OpenSubKey(@"SOFTWARE\Microsoft\Office\14.0\Excel\InstallRoot\");//查询2010
            //检查本机是否安装Office2003
            if (akey != null)
            {
                string file03 = akey.GetValue("Path").ToString();
                if (File.Exists(file03 + "Excel.exe"))
                {
                    ifused += 1;
                }
            }

            //检查本机是否安装Office2007

            if (akey07 != null)
            {
                string file07 = akey07.GetValue("Path").ToString();
                if (File.Exists(file07 + "Excel.exe"))
                {
                    ifused += 2;
                }
            }
            //检查本机是否安装wps
            if (akey10 != null)
            {
                string file10 = akey10.GetValue("Path").ToString();
                if (File.Exists(file10 + "Excel.exe"))
                {
                    ifused += 4;
                }
            }
            return ifused;
        }


        private void displayMessage() {
            int count = gridViewMain.RowCount - 1;
            labTitleMessage.Text = string.Format(MESSAGE_DATA_COUNT, count);
            if (count >= WARMING_MAX_COUNT)
                labTitleMessage.Text += MESSAGE_IMPORT_WARMING;

        }

        #region 界面处理函数...
        void DefaultDataImportDialog_Load(object sender, EventArgs e) {
            MB.WinBase.Common.GridViewLayoutInfo gridViewLayoutInfo = MB.WinBase.LayoutXmlConfigHelper.Instance.GetGridColumnLayoutInfo(_XmlFileName, string.Empty);
            var detailBindingParams = new MB.XWinLib.GridDataBindingParam(grdCtlMain, _CurrentImportData, false);
            MB.XWinLib.XtraGrid.XtraGridEditHelper.Instance.CreateEditXtraGrid(detailBindingParams, _ColPropertys, _EditCols, gridViewLayoutInfo);
           // MB.XWinLib.XtraGrid.XtraGridHelper.Instance.BindingToXtraGrid(grdCtlMain, _CurrentImportData, _ColPropertys, _EditCols, _XmlFileName, true);
            
            displayMessage();

            grdCtlMain.ReSetContextMenu(MB.XWinLib.XtraGrid.XtraContextMenuType.SaveGridState |
                                        MB.XWinLib.XtraGrid.XtraContextMenuType.Delete);

            grdCtlMain.BeforeContextMenuClick += new XWinLib.XtraGrid.GridControlExMenuEventHandle(grdCtlMain_BeforeContextMenuClick);
            
            DevExpress.XtraGrid.Views.Grid.GridView view =  grdCtlMain.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null) {
                view.RowStyle += new RowStyleEventHandler(view_RowStyle);
            }
            
        }

        void view_RowStyle(object sender, RowStyleEventArgs e) {
            GridView view = sender as GridView;
            string bizCheckingColumnName = _BIZ_CHECKING_COLUMN_NAME;
            if (e.RowHandle >= 0 && _CurrentImportData.Tables[0].Columns.Contains(bizCheckingColumnName)) {
                object bizCheckingResult = _CurrentImportData.Tables[0].Rows[e.RowHandle][bizCheckingColumnName];
                //object bizCheckingResult = view.GetRowCellValue(e.RowHandle, bizCheckingColumnName);
                if (bizCheckingResult != null && !(bizCheckingResult is DBNull)) {
                    bool bizCheckFlag = Convert.ToBoolean(bizCheckingResult);
                    if (!bizCheckFlag)
                        e.Appearance.ForeColor = Color.Red;
                }
            }
        }


        void grdCtlMain_BeforeContextMenuClick(object sender, XWinLib.XtraGrid.GridControlExMenuEventArg arg)
        {
            if (arg.MenuType == XWinLib.XtraGrid.XtraContextMenuType.Delete)
            {
                gridViewMain.DeleteSelectedRows(); 
            }
        }
        private void butCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void butSure_Click(object sender, EventArgs e) {

            int count = gridViewMain.RowCount;
            if (count == 0) {
                MB.WinBase.MessageBoxEx.Show("没有发现需要导入的数据,请重新选择");
                return;
            }
            if (count > MAX_IMPORT_COUNT) {
                MB.WinBase.MessageBoxEx.Show(string.Format("一次性导入的数据不能超过{0},请分批导入",MAX_IMPORT_COUNT));
                return;
            }

            string bizCheckingColumnName = _BIZ_CHECKING_COLUMN_NAME;
            if (_CurrentImportData.Tables[0].Columns.Contains(bizCheckingColumnName)) {
                string filter = string.Format("{0} = false", bizCheckingColumnName);
                DataRow[] drs = _CurrentImportData.Tables[0].Select(filter);
                count = drs.Length;
                if (count > 0) {
                    MB.WinBase.MessageBoxEx.Show("字体为红色的数据由于验证不通过不能导入，请删除");
                    return;
                }
            }

            try
            {
                bool dataValidated = MB.WinBase.UIDataInputValidated.DefaultInstance.DetailGridDataValidated(_XmlFileName, _CurrentImportData, string.Empty);

                if (!dataValidated) return;
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                return;
            }

            if (_ImportImmediate) {
                DialogResult re = MB.WinBase.MessageBoxEx.Question("确定将直接进行数据库保存操作,是否继续?");
                if (re != DialogResult.Yes)
                    return;
            }
            this.DialogResult = DialogResult.OK;
            _CurrentImportInfo = new DataImportInfo(_CurrentImportData);
            this.Close();
        }

        private void toolsMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            //if (e.ClickedItem.Equals(tButCancel)) {
            //    this.Close();
            //}
            //else if (e.ClickedItem.Equals(tButSure)) {

            //}
            //else if (e.ClickedItem.Equals(tButDelete)) {
            //    gridViewMain.DeleteSelectedRows();
            //}
            //else {
            //    MB.Util.TraceEx.Write("该Button 还没有添加相应的处理！");
            //}
        }
        #endregion 界面处理函数...

        #region 内部处理函数...


        //根据配置的XML文件得到一个空的DataSet
        private static  DataSet createNULLDataByFieldPropertys(Dictionary<string,ColumnPropertyInfo> colPropertys) {

            return MB.WinBase.LayoutXmlConfigHelper.Instance.CreateNULLDataByFieldPropertys(colPropertys);
        }
        #endregion 内部处理函数...

        private void gridViewMain_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e) {
            displayMessage();
        }


        #region IForm 成员

        public MB.WinBase.IFace.IClientRuleQueryBase ClientRuleObject {
            get {
                return _ClientRuleObject;
            }
            set {
                _ClientRuleObject = value ;
            }
        }

        public ClientUIType ActiveUIType {
            get { return ClientUIType.Other; }
        }

        #endregion

        private void grdCtlMain_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) {
                gridViewMain.DeleteSelectedRows(); 
            }
        }
    }

}
