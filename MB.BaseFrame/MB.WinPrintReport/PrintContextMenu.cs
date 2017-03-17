//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 Crea.D Soft (www.Crea.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2009-09-18。
// Description	:	PrintContextMenu 提供弹出式打印需要的菜单。
//---------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinPrintReport {
    /// <summary>
    /// PrintContextMenu 提供弹出式打印需要的菜单。
    /// </summary>
    public class PrintContextMenu : System.Windows.Forms.ContextMenu {
        #region 变量定义...
        private Dictionary<System.Windows.Forms.MenuItem, MB.WinPrintReport.Model.PrintTempleteContentInfo> _MenuItems;
        private static readonly string TXT_PRINT_PREVIEW = "打印预览";
        private static readonly string TXT_PRINT = "直接打印";
        private static readonly string TXT_PRINT_DESIGN = "模板设计";

        private ReportTemplete _ReportTemplete;
        private MB.WinPrintReport.IFace.IReportData _ReportDataHelper;  

        #endregion 变量定义...

        #region 构造函数...
        /// <summary>
        /// 构造函数...
        /// 外部不能直接实例。
        /// </summary>
        protected PrintContextMenu(MB.WinPrintReport.IFace.IReportData reportDataHelper) {
            _MenuItems = new Dictionary<System.Windows.Forms.MenuItem, MB.WinPrintReport.Model.PrintTempleteContentInfo>();

            _ReportDataHelper = reportDataHelper;
            _ReportTemplete = new ReportTemplete(_ReportDataHelper);

            createMenuItem(reportDataHelper);
        }
        #endregion 构造函数...

        #region 扩展的静态方法...
        /// <summary>
        /// 显示打印模板相关的操作菜单。
        /// </summary>
        /// <param name="iWinCtl"></param>
        /// <param name="moduleID"></param>
        /// <param name="templeteID"></param>
        /// <param name="pars"></param>
        public static void ShowMenu(System.Windows.Forms.Control iWinCtl, MB.WinPrintReport.IFace.IReportData reportDataHelper) {

            PrintContextMenu menu = new PrintContextMenu(reportDataHelper);
            menu.Show(iWinCtl, new System.Drawing.Point(0, iWinCtl.Height));
        }
        /// <summary>
        /// 显示打印模板相关的操作菜单
        /// </summary>
        /// <param name="iWinCtl"></param>
        /// <param name="moduleID"></param>
        /// <param name="menuPoint"></param>
        /// <param name="pars"></param>
        public static void ShowMenu(System.Windows.Forms.Control iWinCtl, System.Drawing.Point menuPoint, MB.WinPrintReport.IFace.IReportData reportDataHelper) {

            PrintContextMenu menu = new PrintContextMenu(reportDataHelper);
            menu.Show(iWinCtl, menuPoint);
        }
        #endregion 扩展的静态方法...

        #region 内部函数处理...
        //创建menu item
        private void createMenuItem(MB.WinPrintReport.IFace.IReportData reportDataHelper) {
            var templetes = reportDataHelper.GetModulePrintTempletes(reportDataHelper.ModuleID);
            if (templetes == null) return;
            foreach (var info in templetes) {
                System.Windows.Forms.MenuItem item = new System.Windows.Forms.MenuItem(info.Name);

                createDefaultItem(item);

                _MenuItems[item] = info;
                this.MenuItems.Add(item);
            }
        }
        //创建固定的菜单项
        private void createDefaultItem(System.Windows.Forms.MenuItem parentItem) {
            parentItem.MenuItems.Add(TXT_PRINT_PREVIEW, new System.EventHandler(menuItem_Click)).Tag = parentItem;
            System.Windows.Forms.MenuItem item = new System.Windows.Forms.MenuItem(TXT_PRINT, new System.EventHandler(menuItem_Click));
            item.Tag = parentItem;
            //item.Enabled = false;//直接打印先暂时注释
            parentItem.MenuItems.Add(item);
            parentItem.MenuItems.Add("-");
            parentItem.MenuItems.Add(TXT_PRINT_DESIGN, new System.EventHandler(menuItem_Click)).Tag = parentItem;
        }
        //对象事件...
        private void menuItem_Click(object sender, EventArgs e) {
            using (MB.WinBase.WaitCursor cursor = new WinBase.WaitCursor()) {
                try {
                    // System.Windows.Forms.Form.ActiveForm.Cursor = Cursors.WaitCursor;
                    System.Windows.Forms.MenuItem item = sender as System.Windows.Forms.MenuItem;
                    if (item.Tag == null) return;
                    string txt = item.Text;
                    var templeteInfo = _MenuItems[item.Tag as System.Windows.Forms.MenuItem];//获取打印模板的ID
                    if (txt == TXT_PRINT_PREVIEW) {//打印预览
                       
                        _ReportTemplete.ShowPreview(_ReportDataHelper.ModuleID, templeteInfo);
                    }
                    else if (txt == TXT_PRINT) { //直接打印
                        _ReportTemplete.Print(_ReportDataHelper.ModuleID, templeteInfo);
                    }
                    else if (txt == TXT_PRINT_DESIGN) {//模板设计
                        MB.WinPrintReport.FrmEditPrintTemplete frm = new MB.WinPrintReport.FrmEditPrintTemplete(_ReportDataHelper, _MenuItems[item.Tag as System.Windows.Forms.MenuItem].GID);
                        frm.ShowDialog();
                    }
                    else {
                        throw new MB.Util.APPException("该文件类型还没有进行处理 ", MB.Util.APPMessageType.SysWarning);
                    }
                }
                catch (Exception ex) {
                    throw new MB.Util.APPException("DIY 报表操作有误,可能格报表格式已经遭到损坏 " + ex.Message, MB.Util.APPMessageType.DisplayToUser);
                }
                finally {
                    //  System.Windows.Forms.Form.ActiveForm.Cursor = Cursors.Default;
                }
            }
        }
        #endregion 内部函数处理...

    }

}
