//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-17
// Description	:	用户UI 操作的基类。。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------

using System;
using System.Collections;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

using MB.WinBase.IFace;
using MB.WinBase.Common;
namespace MB.WinClientDefault.UICommand {

    /// <summary>
    /// 用户UI 操作的基类。
    /// </summary>
    public abstract class CommandExecutorBase : System.ContextBoundObject {

        #region 变量定义...
        private IMdiMainForm _HostMdiMainForm;
        private object[] _CurrentExecParameters = null;
        #endregion 变量定义...

        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="hostMdiMainForm"></param>
        public CommandExecutorBase(IMdiMainForm hostMdiMainForm) {
            _HostMdiMainForm = hostMdiMainForm;

        }

        public void ExecCommand(CommandID cmdID, object[] execParameters) {
            _CurrentExecParameters = execParameters;
            ExecCommand(cmdID);
        }

        #region 继承的子类需要覆盖实现的方法...

        abstract public void ExecCommand(CommandID cmdID);

        #endregion 继承的子类需要覆盖实现的方法...


        /// <summary>
        /// 命令集所在的主窗口。
        /// </summary>
        public IMdiMainForm HostMdiMainForm {
            get {
                return _HostMdiMainForm;
            }
            set {
                _HostMdiMainForm = value;
            }
        }
        #region protected 成员...
        /// <summary>
        /// 当前命令执行的参数。
        /// </summary>
        protected object[] CurrentExecParameters {
            get {
                return _CurrentExecParameters;
            }
            set {
                _CurrentExecParameters = value;
            }
        }
        #endregion protected 成员...
    }

    /// <summary>
    /// 执行UI command 相关
    /// </summary>
    [MB.Aop.InjectionManager]
    public class UICommandExecutor : CommandExecutorBase {
        private static readonly string APP_HELPER_FILE_NAME = "HelperFileName";

        #region 构造函数...
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="designerHost"></param>
        public UICommandExecutor(IMdiMainForm hostMdiMainForm)
            : base(hostMdiMainForm) {
        }
        #endregion 构造函数...

        #region public execute command...
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmdID"></param>
        public override void ExecCommand(CommandID cmdID) {
            try {
                IForm activeForm = this.HostMdiMainForm.GetActiveMdiChildForm();


                if (object.Equals(cmdID, UICommand.UICommands.Exit)) {
                    this.HostMdiMainForm.Exit();
                }
                else if (object.Equals(cmdID, UICommand.UICommands.MdiSaveLayout)) {
                    this.HostMdiMainForm.SaveMdiLayput();
                }
                else if (object.Equals(cmdID, UICommand.UICommands.About)) {
                    frmDefaultAbout frm = new frmDefaultAbout();
                    frm.Show();
                }
                else if (object.Equals(cmdID, UICommand.UICommands.Calculator)) {
                    openTools(ToolsType.Calc);
                }
                else if (object.Equals(cmdID, UICommand.UICommands.HelpList)) {
                    openHelpFile();
                }
                else if (object.Equals(cmdID, UICommand.UICommands.FunctionTree)) {
                    this.HostMdiMainForm.ShowFunctionTree();
                }
                else if (object.Equals(cmdID, UICommand.UICommands.OnlineMessage)) {
                    this.HostMdiMainForm.ShowOnlineMessage();
                }
                else if (object.Equals(cmdID, UICommand.UICommands.Individuality)) {
                    this.HostMdiMainForm.ShowUserSetting();
                }
                else if (object.Equals(cmdID, UICommand.UICommands.SysSetting)) {
                    this.HostMdiMainForm.ShowApplicationSetting();
                }
                else if (activeForm != null && activeForm.ActiveUIType == ClientUIType.ObjectEditForm) {
                    execEditCommand(activeForm as IBaseEditForm, cmdID);
                }
                else if (activeForm != null && (activeForm.ActiveUIType == ClientUIType.GridViewForm || 
                                                activeForm.ActiveUIType == ClientUIType.AsynViewForm ||
                                                activeForm.ActiveUIType == ClientUIType.TreeListViewForm ||
                                                activeForm.ActiveUIType == ClientUIType.GridViewEditForm)) {

                    execViewFormCommand(activeForm as IViewGridForm, cmdID);
                }
                else {
                    MB.Util.TraceEx.Assert(false, string.Format("对CommandID:{0} 还没有实现相应的代码！", cmdID.ToString()));
                }
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);    
            }

        }
        //从对象浏览窗口的角度执行相应的Command.
        private int execViewFormCommand(IViewGridForm activeForm, CommandID cmdID) {
            if (activeForm == null) return 0;

            if (object.Equals(cmdID, UICommand.UICommands.AddNew)) {
                return activeForm.AddNew();
            }
            if (object.Equals(cmdID, UICommand.UICommands.Save)) {
                return activeForm.Save();
            }
            else if (object.Equals(cmdID, UICommand.UICommands.Open)) {
                return activeForm.Open();
            }
            else if (object.Equals(cmdID, UICommand.UICommands.Copy)) {
                return activeForm.CopyAsNew();
            }
            else if (object.Equals(cmdID, UICommand.UICommands.Delete)) {
                return activeForm.Delete();
            }
            else if (object.Equals(cmdID, UICommand.UICommands.Query)) {
                return activeForm.Query();
            }
            else if (object.Equals(cmdID, UICommand.UICommands.Refresh)) {
                return activeForm.Refresh();
            }
            else if (object.Equals(cmdID, UICommand.UICommands.DataExport)) {
                outputToExcel(activeForm);
                return 1;
            }
            else if (object.Equals(cmdID, UICommand.UICommands.DataImport)) {
                return activeForm.DataImport();
            }
            else if (object.Equals(cmdID, UICommand.UICommands.PrintPreview)) {
                showPrintPreview(activeForm);
                return 1;
            }
            else {
                MB.Util.TraceEx.Assert(false, string.Format("对CommandID:{0} 还没有实现相应的代码！", cmdID.ToString()));
            }
            return 0;
        }
        //从对象浏览窗口的角度执行相应的Command.
        private int execEditCommand(IBaseEditForm activeForm, CommandID cmdID) {
            if (activeForm == null) return 0;

            if (object.Equals(cmdID, UICommand.UICommands.AddNew)) {
                return activeForm.AddNew();
            }
            else if (object.Equals(cmdID, UICommand.UICommands.Save)) {
                return activeForm.Save();
            }
            else if (object.Equals(cmdID, UICommand.UICommands.Delete)) {
                return activeForm.Delete();
            }
            else if (object.Equals(cmdID, UICommand.UICommands.Exit)) {
                activeForm.Close();
            }
            else {
                MB.Util.TraceEx.Assert(false, string.Format("对CommandID:{0} 还没有实现相应的代码！", cmdID.ToString()));
            }
            return 0;
        }

        private void outputToExcel(IViewGridForm activeForm) {
            int re = activeForm.DataExport();

            if (re != 0) return;

            object mainGrid = activeForm.GetCurrentMainGridView(false);
            DevExpress.XtraGrid.GridControl grdCtl = mainGrid as DevExpress.XtraGrid.GridControl;
            if (grdCtl != null) {
                MB.XWinLib.XtraGrid.XtraGridHelper.Instance.ExportToExcelAndShow(grdCtl);
            }
            else {
                DevExpress.XtraPivotGrid.PivotGridControl pivGrid = mainGrid as DevExpress.XtraPivotGrid.PivotGridControl;
                if (pivGrid != null)
                    MB.XWinLib.PivotGrid.PivotGridHelper.Instance.ExportToExcelAndShow(pivGrid);
            }
        }
        private void showPrintPreview(IViewGridForm activeForm) {
            object mainGrid = activeForm.GetCurrentMainGridView(false);
            DevExpress.XtraGrid.GridControl grdCtl = mainGrid as DevExpress.XtraGrid.GridControl;
            if (grdCtl != null) {
                grdCtl.ShowPrintPreview();
            }
            else {
                DevExpress.XtraPivotGrid.PivotGridControl pivGrid = mainGrid as DevExpress.XtraPivotGrid.PivotGridControl;
                if (pivGrid != null)
                    pivGrid.ShowPrintPreview();
            }
        }

        #endregion public execute command...

       private  void openTools(ToolsType pType) {
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();

            switch (pType) {
                case ToolsType.Notepad:
                    Info.FileName = "notepad.exe";
                    break;
                case ToolsType.Calc:
                    Info.FileName = "calc.exe";
                    break;
                default:
                   MB.Util.TraceEx.Write( "目前还没有增加这种类型的工具处理！");
                    break;
            }
            //声明一个程序类
            System.Diagnostics.Process Proc;
            try {
                //启动外部程序
                Proc = System.Diagnostics.Process.Start(Info);
            }
            catch (System.ComponentModel.Win32Exception e) {
                MB.Util.TraceEx.Write("系统找不到指定的程序文件:" + Info.FileName + e.ToString());
                return;
            }
        }
       //打开帮助文件 APP_HELPER_FILE_NAME
       private  static void openHelpFile() {
           string appUri = AppDomain.CurrentDomain.BaseDirectory;
           string helperFileName = System.Configuration.ConfigurationManager.AppSettings[APP_HELPER_FILE_NAME];
           if (string.IsNullOrEmpty(helperFileName)) return;
 
           string destFUri = appUri + helperFileName;
           if (!System.IO.File.Exists(destFUri)) return;

           System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
           startInfo.FileName = destFUri;//FILE_PATH;
           //声明一个程序类
           System.Diagnostics.Process proc;
           try {
               //启动外部程序
               proc = System.Diagnostics.Process.Start(startInfo);
           }
           catch (Exception  e) {
               throw new MB.Util.APPException(string.Format("帮助文件{0} 不存在",helperFileName),MB.Util.APPMessageType.DisplayToUser); 
           }
       }

        enum ToolsType {
            Notepad,
            Calc
        }
    }
    
}
