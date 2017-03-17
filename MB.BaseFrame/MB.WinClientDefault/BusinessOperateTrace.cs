using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.WinBase.Common;
using MB.BaseFrame;
using System.Drawing;

namespace MB.WinClientDefault {
    /// <summary>
    /// 单据操作状态改变记录跟踪。
    /// </summary>
   public  class BusinessOperateTrace {
       private static readonly string VIEW_DOC_TRACE_CFG = "ViewDocOperateTrace";
       private static readonly string INVOKE_METHOD_NAME = "BusinessFlowSubmit";
        private ContextMenuStrip _ContextMenuStrip;
        private Dictionary<MB.WinBase.Common.GeneralOperateMenus, ToolStripMenuItem> _MenuBinding;
        private Dictionary<ToolStripMenuItem, MB.Util.Model.DocOperateType> _OperateTypes; 
        private MB.WinBase.IFace.IClientRuleConfig _ClientRule;
        private object _CurrentEditEntity;

        #region 自定义事件处理相关...
        private System.EventHandler<MB.WinClientDefault.Common.EditDocStateTraceEventArgs> _CommandItemClick;
       /// <summary>
       /// 在点击操作菜单项后响应的事件。
       /// </summary>
        public event System.EventHandler<MB.WinClientDefault.Common.EditDocStateTraceEventArgs> CommandItemClick {
            add {
                _CommandItemClick += value; 
            }
            remove {
                _CommandItemClick -= value; 
            }
        }
        private void onCommandItemClick(MB.WinClientDefault.Common.EditDocStateTraceEventArgs arg) {
            if (_CommandItemClick != null)
                _CommandItemClick(this, arg);
        }
        #endregion 自定义事件处理相关...
        /// <summary>
        /// 单据操作状态改变记录跟踪。
        /// </summary>
        /// <param name="clientRule"></param>
        public BusinessOperateTrace(MB.WinBase.IFace.IClientRuleConfig clientRule) {
            _ClientRule = clientRule;
            var att = MB.WinBase.Atts.AttributeConfigHelper.Instance.GetClientRuleSettingAtt(clientRule);
            _ContextMenuStrip = new ContextMenuStrip();
            _MenuBinding = new Dictionary<GeneralOperateMenus, ToolStripMenuItem>();
            _OperateTypes = new Dictionary<ToolStripMenuItem, MB.Util.Model.DocOperateType>();

            createMenuItems(att.GeneralMenus);
        }

        /// <summary>
        /// 重新设置实体数据并刷新相应的控制处理。
        /// </summary>
        /// <param name="entityInfo"></param>
        public void ResetDocEntity(object entityInfo) {
            bool exists = MB.WinBase.UIDataEditHelper.Instance.CheckExistsDocState(entityInfo);
            if (exists) {
                MB.Util.Model.DocState docState = MB.WinBase.UIDataEditHelper.Instance.GetEntityDocState(entityInfo);
                controlMenuItem(docState);

                _CurrentEditEntity = entityInfo;
            }
        }
       /// <summary>
       /// 根据业务类创建的功能菜单项。
       /// </summary>
        public ContextMenuStrip CommandMenus {
            get {
                return _ContextMenuStrip; 
            }
        }
        /// <summary>
        /// 当前业务操作对象的公共业务操作菜单项.
        /// </summary>
        public Dictionary<MB.WinBase.Common.GeneralOperateMenus, ToolStripMenuItem> BusinessMenuItemsBindings {
            get {
                return _MenuBinding;
            }
        }
        #region 内部处理函数...
        //根据当前单据对象的状态控制可操作菜单项
        private void controlMenuItem(MB.Util.Model.DocState docState) {
            if (_MenuBinding.ContainsKey(GeneralOperateMenus.Approved)) 
                _MenuBinding[GeneralOperateMenus.Approved].Enabled = docState == MB.Util.Model.DocState.Validated;
            if (_MenuBinding.ContainsKey(GeneralOperateMenus.Completed)) 
                _MenuBinding[GeneralOperateMenus.Completed].Enabled = docState == MB.Util.Model.DocState.Approved;
            if (_MenuBinding.ContainsKey(GeneralOperateMenus.Withdraw)) 
                _MenuBinding[GeneralOperateMenus.Withdraw].Enabled = docState == MB.Util.Model.DocState.Approved;
            if (_MenuBinding.ContainsKey(GeneralOperateMenus.Suspended)) 
                _MenuBinding[GeneralOperateMenus.Suspended].Enabled = docState != MB.Util.Model.DocState.Completed &&
                                                                      docState != MB.Util.Model.DocState.Withdraw &&
                                                                      docState != MB.Util.Model.DocState.Suspended;
            if (_MenuBinding.ContainsKey(GeneralOperateMenus.CancelSuspended)) 
             _MenuBinding[GeneralOperateMenus.CancelSuspended].Enabled = docState == MB.Util.Model.DocState.Suspended;

        }
        //创建所有设置的菜单项
        private void createMenuItems(MB.WinBase.Common.GeneralOperateMenus generalMenus) {
            if (generalMenus == GeneralOperateMenus.None) return;
            if ((generalMenus == GeneralOperateMenus.All) || (generalMenus & GeneralOperateMenus.Approved) != 0) {
                _OperateTypes.Add(createMenuByType(GeneralOperateMenus.Approved), MB.Util.Model.DocOperateType.Approved);
            }
            if ((generalMenus == GeneralOperateMenus.All) || (generalMenus & GeneralOperateMenus.Completed) != 0) {
                _OperateTypes.Add(createMenuByType(GeneralOperateMenus.Completed), MB.Util.Model.DocOperateType.Completed);
            }
            if ((generalMenus == GeneralOperateMenus.All) || (generalMenus & GeneralOperateMenus.Withdraw) != 0) {
                if (_ContextMenuStrip.Items.Count > 0)
                    _ContextMenuStrip.Items.Add("-");

                _OperateTypes.Add(createMenuByType(GeneralOperateMenus.Withdraw), MB.Util.Model.DocOperateType.Withdraw);
            }
            if ((generalMenus == GeneralOperateMenus.All) || (generalMenus & GeneralOperateMenus.Suspended) != 0) {
                _OperateTypes.Add(createMenuByType(GeneralOperateMenus.Suspended), MB.Util.Model.DocOperateType.Suspended);
            }
            if ((generalMenus == GeneralOperateMenus.All) || (generalMenus & GeneralOperateMenus.CancelSuspended) != 0) {
                _OperateTypes.Add(createMenuByType(GeneralOperateMenus.CancelSuspended), MB.Util.Model.DocOperateType.CancelSuspended);

            }

        }
        //根据类型创建菜单项
        private ToolStripMenuItem createMenuByType(GeneralOperateMenus menuType) {
            Type enumType = typeof(GeneralOperateMenus);
            string str = MB.Util.MyCustomAttributeLib.Instance.GetFieldDesc(enumType, menuType.ToString(), false);
            str = CLL.Convert(str);
            ToolStripMenuItem menu = new ToolStripMenuItem(str, (Image)null, new System.EventHandler(menuItemClick));
            _MenuBinding.Add(menuType, menu);

            _ContextMenuStrip.Items.Add(menu);
            return menu;
        }
        //菜单项对应操作事件。
        private void menuItemClick(object sender, System.EventArgs e) {
            try {
                //检验业务对象是否已经实现了
                System.Reflection.MemberInfo[] mths = _ClientRule.GetType().GetMember(INVOKE_METHOD_NAME);
                if (mths == null || mths.Length == 0)
                    throw new MB.Util.APPException(string.Format("在单据业务状态操作改变中对应的业务类 {0} 必须实现方法{1}",_ClientRule.GetType().FullName,INVOKE_METHOD_NAME), MB.Util.APPMessageType.SysErrInfo);

                if(!_OperateTypes.ContainsKey((ToolStripMenuItem)sender))
                    throw new MB.Util.APPException("该菜单还没有进行相应的处理", MB.Util.APPMessageType.DisplayToUser); 

                MB.Util.Model.DocOperateType operateType = _OperateTypes[(ToolStripMenuItem)sender];
             
                //operateType = MB.Util.Model.DocOperateType.Approved;
                MB.WinClientDefault.Common.frmEditDocStateTrace editDocState = new MB.WinClientDefault.Common.frmEditDocStateTrace(operateType);
                editDocState.AfterEditSure += new EventHandler<MB.WinClientDefault.Common.EditDocStateTraceEventArgs>(editDocState_AfterEditSure);
                editDocState.ShowDialog(); 

            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }
        /// <summary>
        /// 显示单据操作记录
        /// </summary>
        /// <param name="docType"></param>
        /// <param name="docID"></param>
        public void ShowDocOperateTrace(string docType,int docID) {
            string cfgSetting = System.Configuration.ConfigurationManager.AppSettings[VIEW_DOC_TRACE_CFG];
            if (string.IsNullOrEmpty(cfgSetting))
                throw new MB.Util.APPException(string.Format("请确保Config 文件中是否已经配置了{0}",VIEW_DOC_TRACE_CFG), MB.Util.APPMessageType.SysErrInfo);

            try {
                string[] cfgs = cfgSetting.Split(',');
                object[] pars = new object[] { docType, docID };
 
                object frmView = MB.Util.DllFactory.Instance.LoadObject(cfgs[0],pars, cfgs[1]);
                Form view = frmView as Form;
                view.ShowDialog(); 
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(string.Format("请检查配置文件{0}是否正确,创建单据浏览窗口出错！" + ex.Message,VIEW_DOC_TRACE_CFG), MB.Util.APPMessageType.SysErrInfo);
            }

 
        }
        #endregion 内部处理函数...

        void editDocState_AfterEditSure(object sender, MB.WinClientDefault.Common.EditDocStateTraceEventArgs e) {
            //object[] pars = new object[] { _CurrentEditEntity, e.DocOperateType, e.Remark };

            //object val = MB.Util.MyReflection.Instance.InvokeMethod(_ClientRule, INVOKE_METHOD_NAME, pars);
            onCommandItemClick(e);
        }


    }
}
