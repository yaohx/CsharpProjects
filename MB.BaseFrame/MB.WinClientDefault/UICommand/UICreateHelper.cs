//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-03-02
// Description	:	UICreateHelper：客户端界面创建相关处理函数。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;

using MB.WinBase.Binding;
using MB.WinBase.Common;
using MB.WinBase.IFace;
namespace MB.WinClientDefault.UICommand {
    /// <summary>
    /// UICreateHelper：客户端界面创建相关处理函数。
    /// </summary>
    public class UICreateHelper {
        private static readonly string ASSEMBLY_PATH = MB.Util.General.GeApplicationDirectory();  

        #region Instance...
        private static object _Object = new object();
        private static UICreateHelper _Instance;

        protected UICreateHelper() { }

        /// <summary>
        /// Instance
        /// </summary>
        public static UICreateHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Object) {
                        if (_Instance == null)
                            _Instance = new UICreateHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...


        /// <summary>
        /// 创建数据网格浏览界面。
        /// </summary>
        /// <param name="mdiForm"></param>
        /// <param name="nodeInfo"></param>
        public void ShowViewGridForm(MB.WinBase.IFace.IMdiMainForm mdiForm, MB.Util.Model.ModuleTreeNodeInfo nodeInfo) {
            ModuleOpenState openState = new ModuleOpenState();
            openState.OpennedFrom = ModuleOpennedFrom.Menu;
            ShowViewGridForm(mdiForm, nodeInfo, openState);
        }

        /// <summary>
        /// 创建数据网格浏览界面。
        /// </summary>
        /// <param name="mdiForm"></param>
        /// <param name="nodeInfo"></param>
        /// <param name="opennedFrom"></param>
        public void ShowViewGridForm(MB.WinBase.IFace.IMdiMainForm mdiForm, MB.Util.Model.ModuleTreeNodeInfo nodeInfo, ModuleOpenState openState) {
            MB.Util.Model.ModuleCommandInfo commandInfo = nodeInfo.Commands.Find
                                 (o => (UICommandType)Enum.Parse(typeof(UICommandType), o.CommandID) == UICommandType.Open);

            if (commandInfo == null) {
                MB.WinBase.MessageBoxEx.Show(string.Format("模块{0} 的浏览窗口没有配置！", nodeInfo.Name));
                return;
            }

            IForm viewGridForm = null;
            try {

                viewGridForm = CreateWinForm(null, commandInfo) as IForm;

                var uiStyle = viewGridForm.ActiveUIType;
                //viewGridForm.ModuleInfo = nodeInfo;
                if (viewGridForm.ClientRuleObject != null) {
                    viewGridForm.ClientRuleObject.OpennedState = openState;
                    viewGridForm.ClientRuleObject.ModuleTreeNodeInfo = nodeInfo;
                    //验证当前活动窗口的列信息。
                    mdiForm.ValidatedColumns(viewGridForm);
                }



                Form frmMdiChild = viewGridForm as Form;
                frmMdiChild.Text = nodeInfo.Name;
                if (uiStyle == ClientUIType.ShowModelForm) {
                    (viewGridForm as Form).ShowDialog();
                }
                else {
                    frmMdiChild.MdiParent = mdiForm as Form;
                    frmMdiChild.Show();
                }
                //else {
                //    throw new MB.Util.APPException(string.Format("窗口配置的ShowModel类型 {0} 当前还没有进行处理", commandInfo.ViewModel.ToString()), MB.Util.APPMessageType.SysErrInfo);
                //}
            }
            catch (MB.Util.APPException aex) {
                if (viewGridForm != null)
                    (viewGridForm as Form).Dispose();
                throw aex;
            }
            catch (Exception ex) {
                if (viewGridForm != null)
                    (viewGridForm as Form).Dispose();
                //throw new MB.Util.APPException(" 根据ModuleTreeNodeInfo 的信息创建窗口时出错：", MB.Util.APPMessageType.SysErrInfo  , ex);  
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, " 根据ModuleTreeNodeInfo 的信息创建窗口时出错!");
            }
        }

        /// <summary>
        /// 根据指定的模块命令参数 创建Win Form 窗口。
        /// </summary>
        /// <param name="clientRule">指定的客户端业务类，可以为空。</param>
        /// <param name="commandInfo">模块命令参数</param>
        /// <returns></returns>
        public MB.WinBase.IFace.IForm CreateWinForm(MB.WinBase.IFace.IClientRuleQueryBase clientRule, MB.Util.Model.ModuleCommandInfo commandInfo) {
            try {
                string[] clientRuleSetting = null;
                if(!string.IsNullOrEmpty(commandInfo.ClientRule))
                    clientRuleSetting = commandInfo.ClientRule.Split(',');
                string[] formSetting = commandInfo.UIView.Split(',');
                object[] ruleCreatePars = null;
                object[] formCreatePars = null;
                if (!string.IsNullOrEmpty(commandInfo.RuleCreateParams)) {
                    ruleCreatePars = commandInfo.RuleCreateParams.Split(',');
                }
                if (!string.IsNullOrEmpty(commandInfo.UICreateParams))
                    formCreatePars = commandInfo.UICreateParams.Split(',');

                MB.WinBase.IFace.IClientRuleQueryBase rule = clientRule;
                if (!string.IsNullOrEmpty(commandInfo.ClientRule)) {
                    if (clientRule == null || string.Compare(clientRuleSetting[0], clientRule.GetType().FullName, true) != 0) {
                        var temp = MB.Util.DllFactory.Instance.LoadObject(clientRuleSetting[0], ruleCreatePars, clientRuleSetting[1]);
                        rule = temp as MB.WinBase.IFace.IClientRuleQueryBase;
                    }
                    MB.Util.TraceEx.WriteIf(rule != null, string.Format("请检查数据库中该功能模块对应的客户端类{0} 是否设置出错！", commandInfo.ClientRule), MB.Util.APPMessageType.SysErrInfo);
                }
                MB.WinBase.IFace.IForm winForm = MB.Util.DllFactory.Instance.LoadObject(formSetting[0], formCreatePars, formSetting[1]) as MB.WinBase.IFace.IForm;
                MB.Util.TraceEx.WriteIf(winForm != null, string.Format("请检查数据库中该功能模块对应的显示窗口{0},是否设置出错！", commandInfo.UIView), MB.Util.APPMessageType.SysErrInfo);
                winForm.ClientRuleObject = rule;
                return winForm;
            }
            catch (MB.Util.APPException aex) {
                throw aex;
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, MB.Util.APPMessageType.SysErrInfo);  
            }
        }
        /// <summary>
        /// 根据指定的参数创建对象编辑窗口。
        /// </summary>
        /// <param name="commandInfo"></param>
        /// <param name="clientRuleObject"></param>
        /// <param name="editType"></param>
        /// <param name="bindingSource"></param>
        /// <returns></returns>
        public MB.WinBase.IFace.IForm CreateObjectEditForm(MB.Util.Model.ModuleCommandInfo commandInfo,
                        IClientRule clientRuleObject, ObjectEditType editType, BindingSourceEx bindingSource) {

            string[] formSetting = commandInfo.UIView.Split(',');
            object[] formCreatePars = new object[] { clientRuleObject, editType, bindingSource };

            MB.WinBase.IFace.IForm winForm = MB.Util.DllFactory.Instance.LoadObject(formSetting[0], formCreatePars, formSetting[1]) as MB.WinBase.IFace.IForm;
            MB.Util.TraceEx.WriteIf(winForm != null, string.Format("请检查数据库中该功能模块对应的显示窗口{0},是否设置出错！", commandInfo.UIView), MB.Util.APPMessageType.SysErrInfo);
            return winForm;
        }
        /// <summary>
        ///  创建查询过滤处理接口。
        /// </summary>
        /// <param name="commandInfo"></param>
        /// <returns></returns>
        public MB.WinBase.IFace.IQueryFilterForm CreateQueryFilterForm(MB.Util.Model.ModuleCommandInfo commandInfo) {
            string[] formSetting = commandInfo.UIView.Split(',');
            object[] formCreatePars = null;

            Form winForm = MB.Util.DllFactory.Instance.LoadObject(formSetting[0], formCreatePars, formSetting[1]) as Form;
            MB.Util.TraceEx.WriteIf(winForm != null, string.Format("请检查数据库中该功能模块对应的显示窗口{0},是否设置出错！", commandInfo.UIView), MB.Util.APPMessageType.SysErrInfo);
            MB.WinBase.IFace.IQueryFilterForm filterForm = winForm as MB.WinBase.IFace.IQueryFilterForm;
            if (filterForm == null)
                throw new MB.Util.APPException("所有查询过滤的窗口必须实现 MB.WinBase.IFace.IQueryFilterForm 接口！", MB.Util.APPMessageType.SysFileInfo);
            return filterForm;
        }

        /// <summary>
        /// 创建普通的显示窗口。
        /// </summary>
        /// <param name="commandInfo"></param>
        /// <returns></returns>
        public MB.WinBase.IFace.IForm CreateGeneralForm(MB.Util.Model.ModuleCommandInfo commandInfo) {
            string[] formSetting = commandInfo.UIView.Split(',');
            object[] formCreatePars = null;

            Form winForm = MB.Util.DllFactory.Instance.LoadObject(formSetting[0], formCreatePars, formSetting[1]) as Form;
            MB.Util.TraceEx.WriteIf(winForm != null, string.Format("请检查数据库中该功能模块对应的显示窗口{0},是否设置出错！", commandInfo.UIView), MB.Util.APPMessageType.SysErrInfo);
            MB.WinBase.IFace.IForm frm = winForm as MB.WinBase.IFace.IForm;
            if (frm == null)
                throw new MB.Util.APPException("所有弹出窗口必须实现 MB.WinBase.IFace.IForm 接口！", MB.Util.APPMessageType.SysFileInfo);
            return frm;
        }

        
    }
}
