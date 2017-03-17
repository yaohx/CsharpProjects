//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-24
// Description	:	默认的主窗口编辑界面。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraEditors;
using DevExpress.Skins;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Gallery;
using DevExpress.Utils.Drawing;
using DevExpress.Utils;

using MB.WinClientDefault.Menu;
using MB.WinClientDefault.UICommand;
using MB.Util.Model;
using MB.WinBase.Common;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.ViewInfo;
using MB.WinBase.IFace;
using MB.WinClientDefault.DynamicGroup;
using System.Reflection;
using System.Runtime.Serialization;
using MB.WinClientDefault.Ctls; 
namespace MB.WinClientDefault {
    /// <summary>
    ///  Ribbon 样式的系统操作主窗口。
    /// </summary>
    public partial class XtraRibbonMdiMainForm : DevExpress.XtraBars.Ribbon.RibbonForm, MB.WinBase.IFace.IMdiMainForm {
        #region 变量定义...
        public static readonly string MSG_REQUIRE_REFRESH_BUTTONS = "MdiMeessengerCode";
        private static readonly string APPLICATION_ICON = "ApplicationIcon";
        /// <summary>
        /// 当前活动的MDI 主窗口
        /// </summary>
        public static XtraRibbonMdiMainForm Active_Mdi_Form;
        /// <summary>
        /// 当前连接的服务器。
        /// </summary>
        public static string ACTIVE_LINK_SERVER;
        //private MB.Util.Model.ModuleTreeNodeInfo[] _ModuleTreeNodes;
        private static string _SystemName = "MBMS";
        private UICommandExecutor _CommandExecutor;
        private string _MdiLayoutFileName = MB.Util.General.GeApplicationDirectory() + _SystemName + ".Xml";
        private string _CurrentUserName;
        private Dictionary<DevExpress.XtraBars.BarButtonItem, System.ComponentModel.Design.CommandID> _AllCreateMenuItems;
        private Dictionary<DevExpress.XtraBars.BarButtonItem, bool> _RejectCommandItems;
        private ToolTip _ToopTip;
        private static readonly string MODULE_TOOLTIP_MSG = "请输入模块编码后";

        private MB.Util.MyDataCache<MB.WinBase.IFace.IViewGridForm, Dictionary<DevExpress.XtraBars.BarButtonItem,bool>> _ButtonValidatedCatch;
       // private System.Windows.Forms.Timer _Timer;
        #endregion 变量定义...

        #region 自定义事件处理相关...
        private MdiMainFunctionTreeEventHandle _AfterCreateModuleNode;
        /// <summary>
        /// 创建功能模块节点后产生的事件。
        /// </summary>
        public event MdiMainFunctionTreeEventHandle AfterCreateModuleNode {
            add {
                _AfterCreateModuleNode += value;
            }
            remove {
                _AfterCreateModuleNode -= value;
            }
        }
        protected virtual void OnAfterCreateModuleNode(MdiMainFunctionTreeEventArgs arg) {
            if (_AfterCreateModuleNode != null)
                _AfterCreateModuleNode(this, arg);
        }
        private MdiMainFunctionTreeEventHandle _BeforeDoubleClickTreeNode;
        public event MdiMainFunctionTreeEventHandle BeforeDoubleClickTreeNode {
            add {
                _BeforeDoubleClickTreeNode += value;
            }
            remove {
                _BeforeDoubleClickTreeNode -= value;
            }
        }
        protected virtual void OnBeforeDoubleClickTreeNode(MdiMainFunctionTreeEventArgs arg) {
            if (_BeforeDoubleClickTreeNode != null)
                _BeforeDoubleClickTreeNode(this, arg);
        }
        #endregion 自定义事件处理相关...


        /// <summary>
        ///  Ribbon 样式的系统操作主窗口。
        /// </summary>
        public XtraRibbonMdiMainForm() {
            InitializeComponent();

            if (MB.Util.General.IsInDesignMode()) return;

            _CommandExecutor = new UICommandExecutor(this);
            _RejectCommandItems = new Dictionary<DevExpress.XtraBars.BarButtonItem, bool>();  

            _AllCreateMenuItems = new Dictionary<DevExpress.XtraBars.BarButtonItem,System.ComponentModel.Design.CommandID>();
            _ToopTip = new ToolTip();
            _ToopTip.SetToolTip(butOpenModule, MODULE_TOOLTIP_MSG);

            Active_Mdi_Form = this;

            _ButtonValidatedCatch = new Util.MyDataCache<WinBase.IFace.IViewGridForm, Dictionary<DevExpress.XtraBars.BarButtonItem, bool>>();

            MB.WinBase.AppMessenger.DefaultMessenger.Subscribe(MSG_REQUIRE_REFRESH_BUTTONS, refreshToolsButton);
            MB.WinBase.AppMessenger.DefaultMessenger.Subscribe(DynamicGroupUIHelper.DYNAMIC_GROUP_ACTIVE_MSG_ID, refreshToolsButton);

            string icon = System.Configuration.ConfigurationManager.AppSettings[APPLICATION_ICON];
            if (!string.IsNullOrEmpty(icon))
            {
                bool isShow = true;
                bool isValue = bool.TryParse(icon, out isShow);
                if (isValue && !isShow) this.ribbonControl1.ApplicationIcon = null;
            }

            if (PersonalFavoritesEnable) dockPanelPersonalFavorites.Show();


            //else dockPanelPersonalFavorites.Close();
        }
        void refreshToolsButton() {
            using (MB.Util.MethodTraceWithTime track = new Util.MethodTraceWithTime("XtraRibbonMdiMainForm::refreshToolsButton")) {
                try {
                    if (this.ActiveMdiChild != null) {
                        MB.WinBase.IFace.IViewGridForm gridViewForm = this.ActiveMdiChild as MB.WinBase.IFace.IViewGridForm;
                        if (gridViewForm != null) {
                            bool existsUnSave = gridViewForm.ExistsUnSaveData();
                            //if (_LastSetGridForm != null && gridViewForm.Equals(_LastSetGridForm) && _LastSetState == existsUnSave) {
                            //    //setAllButtonEnabled(false);
                            //    return;
                            //}
                            setAllButtonEnabled(true);
                            //_LastSetState = existsUnSave;
                            //_LastSetGridForm = gridViewForm;
                            // MB.Util.TraceEx.Write("开始执行 existsUnSave...");


                            bButSave.Enabled = existsUnSave && chenckButtonEnable(gridViewForm, bButSave);// checkExistsRejectCommand(bButSave) && ValidatedCommandButton(bButSave);
                            bButAddNew.Enabled = !existsUnSave && chenckButtonEnable(gridViewForm, bButAddNew); //checkExistsRejectCommand(bButAddNew) && ValidatedCommandButton(bButAddNew);
                            bButDelete.Enabled = !existsUnSave && chenckButtonEnable(gridViewForm, bButDelete); //checkExistsRejectCommand(bButDelete) && ValidatedCommandButton(bButDelete);
                            bButFilter.Enabled = !existsUnSave && chenckButtonEnable(gridViewForm, bButFilter);// checkExistsRejectCommand(bButFilter) && ValidatedCommandButton(bButFilter);
                            bButAdvanceFilter.Enabled = !existsUnSave && chenckButtonEnable(gridViewForm, bButAdvanceFilter); //checkExistsRejectCommand(bButAdvanceFilter) && ValidatedCommandButton(bButAdvanceFilter);
                            bButRefresh.Enabled = !existsUnSave && chenckButtonEnable(gridViewForm, bButRefresh); //checkExistsRejectCommand(bButRefresh) && ValidatedCommandButton(bButRefresh);
                            bButOpen.Enabled = !existsUnSave && chenckButtonEnable(gridViewForm, bButOpen); //checkExistsRejectCommand(bButOpen) && ValidatedCommandButton(bButOpen);
                            bButImport.Enabled = !existsUnSave && chenckButtonEnable(gridViewForm, bButImport); //checkExistsRejectCommand(bButImport) && ValidatedCommandButton(bButImport);

                            //判断动态聚组设定按钮是否需要可用
                            IViewDynamicGroupGridForm dynamicViewForm = gridViewForm as IViewDynamicGroupGridForm;
                            bButDynamicGroupSetting.Enabled = (dynamicViewForm != null) && dynamicViewForm.IsDynamicGroupIsActive;
                            // MB.Util.TraceEx.Write("结束执行 existsUnSave...");
                        }
                        else
                            setAllButtonEnabled(false);
                            
                    }
                    else {
                        setAllButtonEnabled(false);
                    }
                }
                catch (Exception ex) {
                    throw new MB.Util.APPException("自动刷新主界面功能菜单项出错" + ex.Message, Util.APPMessageType.SysErrInfo);
                }
            }
        }

        /// <summary>
        /// 记录用户当前操作的模块信息
        /// </summary>
        /// <param name="nodeInfo"></param>
        void recordUserActivity(ModuleTreeNodeInfo nodeInfo) {
            MB.WinBase.Auditing.UserActivity activity = new WinBase.Auditing.UserActivity(nodeInfo.Name, nodeInfo.Code);
            MB.WinBase.Auditing.UserActivityTracker.Instance.AddActivityToList(activity);

        }
        #region 窗口事件相关...
        private void RibbonMdiMainForm_Load(object sender, EventArgs e) {
            if (MB.Util.General.IsInDesignMode()) return;

            initSkinGallery();
            //_Timer = new Timer();
            //_Timer.Enabled = false;
            //_Timer.Tick += new EventHandler(timer1_Tick);
            //_Timer.Interval = 5000;

            try {
                IniCreateModuleTree();

                if (System.IO.File.Exists(_MdiLayoutFileName)) {
                    dockManagerMain.RestoreLayoutFromXml(_MdiLayoutFileName);
                }
                this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
           // _BeginDate = System.DateTime.Now;
           
            double versionNum = MB.WinClientDefault.VersionAutoUpdate.VersionAutoUpdateHelper.GetClientVersionNumber();
            if (versionNum <= 0)
                versionNum = 1.0;

            _CurrentUserName = "Administrator";
            if (MB.WinBase.AppEnvironmentSetting.Instance.CurrentLoginUserInfo != null)
                _CurrentUserName = MB.WinBase.AppEnvironmentSetting.Instance.CurrentLoginUserInfo.USER_NAME;
            bStaticItemUserName.Caption = string.Format("系统版本号: {0}  服务器:{1}    登录用户: {2}", versionNum, ACTIVE_LINK_SERVER, _CurrentUserName);

            iniCreateMenuItem();

            trvListMain.OptionsView.EnableAppearanceEvenRow = false;

            MB.WinBase.Ctls.ComboxExtenderHelper.ReadFromFile(cobModuleFilter);

            this.ShowWorkFlow();

            
        }
        private void RibbonMdiMainForm_MdiChildActivate(object sender, EventArgs e) {
            using (MB.Util.MethodTraceWithTime track = new Util.MethodTraceWithTime("XtraRibbonMdiMainForm::MdiChildActivate")) {
                var moduleInfo = GetFocusedNodeInfo(trvListMain.FocusedNode);
                if (moduleInfo == null) return;
                ModuleTreeNodeInfo nodeInfo = moduleInfo;
                var afrm = this.GetActiveMdiChildForm();
                if (afrm != null && afrm.ClientRuleObject != null)
                    nodeInfo = afrm.ClientRuleObject.ModuleTreeNodeInfo;

                refreshToolsButtonItem(nodeInfo);

                refreshToolsButton();
            }
        }

        #endregion 窗口事件相关...

        #region 界面刷新处理相关...
        //private DateTime _BeginDate;
        //private MB.WinBase.IFace.IViewGridForm _LastSetGridForm;
        //private bool _LastSetState;
        //[System.Diagnostics.DebuggerStepThrough()]
        //private void timer1_Tick(object sender, EventArgs e) {
        //        var sp = System.DateTime.Now.Subtract(_BeginDate);
        //        int house = sp.Hours;
        //        TimeSpan nsp = new TimeSpan(house, sp.Minutes, sp.Seconds);

        //        barStaticItemTime.Caption = string.Format("累计在线时间: {0}", nsp.ToString());
        //}
        //控件权限检验。
        private bool chenckButtonEnable( MB.WinBase.IFace.IViewGridForm gridViewForm ,DevExpress.XtraBars.BarButtonItem item)
        {
            if (_ButtonValidatedCatch.ContainsKey(gridViewForm))
            {
                var items = _ButtonValidatedCatch[gridViewForm];
                if (items.ContainsKey(item)) 
                    return items[item];
                else
                {
                    bool v = checkExistsRejectCommand(item) && ValidatedCommandButton(item);
                    items.Add(item, v);
                    return v;
                }
            }
            else
            {
                bool v = checkExistsRejectCommand(item) && ValidatedCommandButton(item);
                var keys = new Dictionary<DevExpress.XtraBars.BarButtonItem, bool>();
                keys.Add(item, v);
                _ButtonValidatedCatch.Add(gridViewForm, keys);
                return v;
            }
       
        }
        private bool checkExistsRejectCommand(DevExpress.XtraBars.BarButtonItem item) {
            if (_RejectCommandItems.ContainsKey(item))
                return _RejectCommandItems[item];
            else
                return true;
        }
        [System.Diagnostics.DebuggerStepThrough()]
        private void setAllButtonEnabled(bool enabled) {
            foreach (var but in _AllCreateMenuItems.Keys) {
                but.Enabled = enabled && checkExistsRejectCommand(but);
            }
        }
        /// <summary>
        /// 权限控制处理相关.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual bool ValidatedCommandButton(DevExpress.XtraBars.BarButtonItem item) {
            return true;
        }
        /// <summary>
        /// 验证对象网格浏览列表中的列信息。
        /// </summary>
        /// <param name="activeForm"></param>
        public virtual void ValidatedColumns(MB.WinBase.IFace.IForm activeForm) {

        }
        /// <summary>
        /// 验证当前活动的编辑窗口。
        /// </summary>
        /// <param name="activeEditForm"></param>
        public virtual void ValidatedEditForm(MB.WinBase.IFace.IForm activeEditForm) {

        }
        #endregion 界面刷新处理相关...

        #region 初始化功能模块树相关...
        protected DevExpress.XtraTreeList.TreeList MainTreeList {
            get {
                return trvListMain;
            }
        }
        /// <summary>
        /// 初始化功能模块树。
        /// </summary>
        protected virtual void IniCreateModuleTree(){
            throw new MB.Util.APPException("继承的窗口必须实现 IniCreateModuleTree", MB.Util.APPMessageType.SysErrInfo);  
        }
        protected virtual ModuleTreeNodeInfo GetFocusedNodeInfo(DevExpress.XtraTreeList.Nodes.TreeListNode treeNode) {
            throw new MB.Util.APPException("继承的窗口必须实现 GetFocusedNodeInfo", MB.Util.APPMessageType.SysErrInfo);  
        }

        private void trvListMain_DoubleClick(object sender, EventArgs e) {
            OpenModuleByTreeNode(trvListMain.FocusedNode);
        }
        #endregion 初始化功能模块树相关...

        #region SkinGallery
        private void initSkinGallery() {
            SimpleButton imageButton = new SimpleButton();
            foreach (SkinContainer cnt in SkinManager.Default.Skins) {
                imageButton.LookAndFeel.SetSkinStyle(cnt.SkinName);
                GalleryItem gItem = new GalleryItem();
                int groupIndex = 0;
                if (cnt.SkinName.IndexOf("Office") > -1) 
                    groupIndex = 1;
                rgbiSkins.Gallery.Groups[groupIndex].Items.Add(gItem);
                gItem.Caption = cnt.SkinName;

                gItem.Image = getSkinImage(imageButton, 32, 17, 2);
                gItem.HoverImage = getSkinImage(imageButton, 70, 36, 5);
                gItem.Caption = cnt.SkinName;
                gItem.Hint = cnt.SkinName;
                rgbiSkins.Gallery.Groups[1].Visible = false;
            }
        }
       private  Bitmap getSkinImage(SimpleButton button, int width, int height, int indent) {
            Bitmap image = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(image)) {
                StyleObjectInfoArgs info = new StyleObjectInfoArgs(new GraphicsCache(g));
                info.Bounds = new Rectangle(0, 0, width, height);
                button.LookAndFeel.Painter.GroupPanel.DrawObject(info);
                button.LookAndFeel.Painter.Border.DrawObject(info);
                info.Bounds = new Rectangle(indent, indent, width - indent * 2, height - indent * 2);
                button.LookAndFeel.Painter.Button.DrawObject(info);
            }
            return image;
        }
        private void rgbiSkins_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e) {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(e.Item.Caption);
            dockPanelFunctionTree.Refresh();
            xtraTabbedMdiManager1.Invalidate();
            ribbonControl1.Refresh(); 
        }

        private void rgbiSkins_Gallery_InitDropDownGallery(object sender, DevExpress.XtraBars.Ribbon.InplaceGalleryEventArgs e) {
            e.PopupGallery.CreateFrom(rgbiSkins.Gallery);
            e.PopupGallery.AllowFilter = false;
            e.PopupGallery.ShowItemText = true;
            e.PopupGallery.ShowGroupCaption = true;
            e.PopupGallery.AllowHoverImages = false;
            foreach (GalleryItemGroup galleryGroup in e.PopupGallery.Groups)
                foreach (GalleryItem item in galleryGroup.Items)
                    item.Image = item.HoverImage;

            e.PopupGallery.ColumnCount = 2;
            e.PopupGallery.ImageSize = new Size(70, 36);
        }
        #endregion

        #region IMdiMainForm 成员
        /// <summary>
        /// 获取当前活动的窗口。
        /// </summary>
        /// <returns></returns>
        public MB.WinBase.IFace.IForm GetActiveMdiChildForm() {
            return this.ActiveMdiChild as MB.WinBase.IFace.IForm;
        }


        public void ShowFunctionTree() {
            dockPanelFunctionTree.Show();
        }

        public void ShowOnlineMessage() {
           // dockPanelOnlineMsg.Show();
        }
        /// <summary>
        /// 显示用户设置。
        /// </summary>
        public virtual void ShowUserSetting() {

        }
        /// <summary>
        ///  显示系统设置。
        /// </summary>
        public virtual void ShowApplicationSetting() {

        }

        /// <summary>
        /// 
        /// </summary>
        public void Exit() {
            var re = MB.WinBase.MessageBoxEx.Question("关闭应用程序,是否继续");
            if (re == DialogResult.Yes)
                Application.Exit();
        }
        /// <summary>
        /// 保存MDI 布局
        /// </summary>
        public void SaveMdiLayput() {
           dockManagerMain.SaveLayoutToXml(_MdiLayoutFileName);
        }
        #endregion

        #region 功能菜单项...
        private void bButSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.Save);  
        }
        private void bButAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.AddNew);  
        }

        private void bButFilter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.Query);
        }

        private void bButOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.Open);
        }

        private void bButDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.Delete);
        }

        private void bButAdvanceFilter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.Query);
        }

        private void bButRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.Refresh);
        }

        private void bButImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.DataImport);
        }

        private void bButExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.DataExport);
        }

        private void bButPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.PrintPreview);
        }

        private void bButCalc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.Calculator);
        }

        private void bButOnlineMessage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.OnlineMessage);
        }

        private void bButFunctionTree_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.FunctionTree);
        }

        private void bButExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.Exit);
        }

        private void bButAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.About);
        }
 
        private void bButLayout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            _CommandExecutor.ExecCommand(UICommands.MdiSaveLayout);
        }
        protected virtual void bButModuleTree_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            dockPanelFunctionTree.Show();
        }
        private void cobModuleFilter_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (Char)Keys.Enter)
                OpenFunctionModule(cobModuleFilter.Text);
            else
                e.KeyChar = Char.ToUpper(e.KeyChar);
        }

 

        private void butFlowDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.ShowWorkFlowDesign();
        }

        private void butFlowRuning_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.ShowWorkFlow();
        }

        private void bButDynamicGroupSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            IViewDynamicGroupGridForm dynamicViewForm = GetActiveMdiChildForm() as IViewDynamicGroupGridForm;
            if (dynamicViewForm != null) {
                dynamicViewForm.OpenDynamicSettingForm();
            }
        }
        #endregion 功能菜单项...

        #region 内部处理函数...

        //初始化已经创建的菜单项
        private void iniCreateMenuItem() {
            _AllCreateMenuItems.Add(this.bButSave, UICommands.Save);
            _AllCreateMenuItems.Add(this.bButAddNew, UICommands.AddNew);
            _AllCreateMenuItems.Add(this.bButFilter, UICommands.Query);
            _AllCreateMenuItems.Add(this.bButOpen, UICommands.Open);
            _AllCreateMenuItems.Add(this.bButDelete, UICommands.Delete);
            _AllCreateMenuItems.Add(this.bButAdvanceFilter, UICommands.Query);
            _AllCreateMenuItems.Add(this.bButRefresh, UICommands.Refresh);
            _AllCreateMenuItems.Add(this.bButImport, UICommands.DataImport);
            _AllCreateMenuItems.Add(this.bButExport, UICommands.DataExport);
            _AllCreateMenuItems.Add(this.bButPrintPreview, UICommands.PrintPreview);
            _AllCreateMenuItems.Add(this.bButDynamicGroupSetting, UICommands.DynamicAggregation);
        }
        //根据当前活动的子窗口 格式化操作的菜单项
        private void refreshToolsButtonItem(MB.Util.Model.ModuleTreeNodeInfo moduleInfo) {
            using (MB.Util.MethodTraceWithTime track = new Util.MethodTraceWithTime("XtraRibbonMdiMainForm::refreshToolsButtonItem")) {
                _RejectCommandItems.Clear();

                foreach (DevExpress.XtraBars.BarButtonItem butItem in _AllCreateMenuItems.Keys) {
                    butItem.Enabled = true;
                }
                //2009-21-29主要从兼容的角度来处理，以后需要去掉 (新订货会系统中有需要)
                if (!string.IsNullOrEmpty(moduleInfo.RejectCommands)) {
                    string[] rejects = moduleInfo.RejectCommands.Split(',');
                    foreach (DevExpress.XtraBars.BarButtonItem butItem in _AllCreateMenuItems.Keys) {
                        System.ComponentModel.Design.CommandID cmdID = _AllCreateMenuItems[butItem];
                        UICommandType cType = (UICommandType)cmdID.ID;
                        if (Array.IndexOf(rejects, cType.ToString()) >= 0) {
                            butItem.Enabled = false;
                            if (!_RejectCommandItems.ContainsKey(butItem))
                                _RejectCommandItems.Add(butItem, false);
                            else
                                _RejectCommandItems[butItem] = false;
                        }
                    }
                }
                MB.WinBase.IFace.IForm activeForm = this.GetActiveMdiChildForm();
                if (activeForm == null || activeForm.ClientRuleObject == null) {
                    foreach (DevExpress.XtraBars.BarButtonItem butItem in _AllCreateMenuItems.Keys) {
                        butItem.Enabled = false;
                    }
                    return;
                }

                //根据业务类上的配置项来进行约束操作菜单项。
                var rejectCfg = MB.WinBase.Atts.AttributeConfigHelper.Instance.GetModuleRejectCommands(activeForm.ClientRuleObject.GetType());
                if (rejectCfg != null) {
                    foreach (DevExpress.XtraBars.BarButtonItem butItem in _AllCreateMenuItems.Keys) {
                        System.ComponentModel.Design.CommandID cmdID = _AllCreateMenuItems[butItem];
                        UICommandType cType = (UICommandType)cmdID.ID;
                        if ((rejectCfg.RejectCommands & cType) != 0) {
                            butItem.Enabled = false;
                            if (!_RejectCommandItems.ContainsKey(butItem))
                                _RejectCommandItems.Add(butItem, false);
                            else
                                _RejectCommandItems[butItem] = false;
                        }
                    }
                }

                //如果是分析界面的话要屏蔽掉操作功能菜单项。
                MB.WinBase.IFace.IClientRule editRule = activeForm.ClientRuleObject as MB.WinBase.IFace.IClientRule;
                if (editRule != null) return;

                foreach (DevExpress.XtraBars.BarButtonItem butItem in _AllCreateMenuItems.Keys) {
                    System.ComponentModel.Design.CommandID cmdID = _AllCreateMenuItems[butItem];
                    var info = CommandGroups.EditCommands.FirstOrDefault(o => o.CommandID.Equals(cmdID));
                    if (info != null) {
                        butItem.Enabled = false;
                        if (!_RejectCommandItems.ContainsKey(butItem))
                            _RejectCommandItems.Add(butItem, false);
                        else
                            _RejectCommandItems[butItem] = false;

                        continue;
                    }
                    if (cmdID.Equals(UICommands.DataImport)) {
                        butItem.Enabled = false;
                        if (!_RejectCommandItems.ContainsKey(butItem))
                            _RejectCommandItems.Add(butItem, false);
                        else
                            _RejectCommandItems[butItem] = false;

                        continue;
                    }
                }
            }
        }
        //打开焦点模块
        protected  void OpenModuleByTreeNode(DevExpress.XtraTreeList.Nodes.TreeListNode treeNode) {
            ModuleOpenState openState = new ModuleOpenState();
            openState.OpennedFrom = ModuleOpennedFrom.Menu;
            OpenModuleByTreeNode(treeNode, openState);
        }

        //打开焦点模块
        protected void OpenModuleByTreeNode(DevExpress.XtraTreeList.Nodes.TreeListNode treeNode, ModuleOpenState openState) {
            try {
                using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                    ModuleTreeNodeInfo nodeInfo = GetFocusedNodeInfo(treeNode);
                    OnBeforeDoubleClickTreeNode(new MdiMainFunctionTreeEventArgs(null, nodeInfo));

                    if (nodeInfo == null || nodeInfo.Commands == null || nodeInfo.Commands.Count == 0)
                        return;

                    //判断是否已经打开
                    foreach (Form f in this.MdiChildren) {
                        MB.WinBase.IFace.IForm iView = f as MB.WinBase.IFace.IForm;
                        if (iView == null) continue;
                        if (iView.ClientRuleObject.ModuleTreeNodeInfo.ID == nodeInfo.ID) {
                            (iView as Form).BringToFront();
                            return;
                        }
                    }

                    UICreateHelper.Instance.ShowViewGridForm(this, nodeInfo, openState);

                    recordUserActivity(nodeInfo);
                }
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        private void butOpenModule_Click(object sender, EventArgs e) {
            string moduleCode = cobModuleFilter.Text.TrimEnd();
            OpenFunctionModule(moduleCode);
        }
        #endregion 内部处理函数...

        #region 子类可以调用和覆盖的成员...
        /// <summary>
        /// 子类可以通过传入的模块查询文字moduleSearchText，去做任何扩展，最后选定某一个模块
        /// </summary>
        /// <param name="moduleSearchText"></param>
        /// <returns>返回模块编码CODE</returns>
        protected virtual string SelectModule(string moduleSearchText) {
            object[] modules = trvListMain.DataSource as object[];
            if (modules != null && modules.Length > 0) {
                Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> dAccs = new Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor>();
                Type entityType = modules[0].GetType();
                PropertyInfo[] infos = entityType.GetProperties();
                foreach (PropertyInfo info in infos) {
                    object[] atts = info.GetCustomAttributes(typeof(DataMemberAttribute), true);
                    if (atts == null || atts.Length == 0) continue;

                    dAccs.Add(info.Name, new MB.Util.Emit.DynamicPropertyAccessor(entityType, info));
                }

                Dictionary<string, string> matchedModules = new Dictionary<string, string>();
                foreach (object module in modules) {
                    string moduleName = dAccs["NAME"].Get(module) as string;
                    string moduleCode = dAccs["CODE"].Get(module) as string;

                    if (moduleCode.Contains(moduleSearchText) || moduleName.Contains(moduleSearchText)) {
                        DevExpress.XtraTreeList.Nodes.TreeListNode treeNode = trvListMain.FindNodeByFieldValue("CODE", moduleCode);
                        if (treeNode != null && treeNode.Nodes.Count <= 0) {
                            if (!matchedModules.ContainsKey(moduleCode)) {
                                matchedModules.Add(moduleCode, moduleName);
                            }
                        }
                    }
                }

                if (matchedModules.Count > 0) {
                    Point p = cobModuleFilter.FindForm().Location;
                    int x = p.X;
                    int y = p.Y;
                    p = cobModuleFilter.PointToScreen(p);
                    Point pNew = new Point(p.X - x, p.Y + 30 - y);
                    frmMdiMenuSelection frm = new Ctls.frmMdiMenuSelection(matchedModules);
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.Location = pNew;
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                        return frm.SELECT_MODULE_CODE;
                    }
                    
                }
            }

            return moduleSearchText;
        }

        /// <summary>
        /// 根据输入的模块编码打开功能模块。
        /// </summary>
        /// <param name="moduleCode"></param>
        protected virtual void OpenFunctionModule(string moduleCode) {
            if (string.IsNullOrEmpty(moduleCode)) return;
 
            //这里临时采取硬编码的方式,是否模块对应的编码字段为CODE,需要对应的应用系统来决定所以最好的外部传进来。
             DevExpress.XtraTreeList.Nodes.TreeListNode treeNode = trvListMain.FindNodeByFieldValue("CODE", moduleCode);
             if (treeNode == null || treeNode.Nodes.Count > 0) {
                 string reSelectModuleCode = SelectModule(moduleCode);
                 treeNode = trvListMain.FindNodeByFieldValue("CODE", reSelectModuleCode);
             }

             if (treeNode == null) {
                 MB.WinBase.MessageBoxEx.Show("模块编码输入无效"); 
                 return;
             }
             if (treeNode.Nodes.Count > 0) {
                 MB.WinBase.MessageBoxEx.Show("不是有效的功能模块节点编码"); 
                 return;
             }
             OpenModuleByTreeNode(treeNode);

             MB.WinBase.Ctls.ComboxExtenderHelper.SaveToFile(cobModuleFilter, moduleCode);
             if (cobModuleFilter.Items.IndexOf(moduleCode) < 0)
                 cobModuleFilter.Items.Add(moduleCode);

        }



        /// <summary>
        /// 显示功能流程图设计窗口。
        /// </summary>
        protected virtual void ShowWorkFlowDesign() {

        }
        /// <summary>
        /// 显示功能流程图。
        /// </summary>
        protected virtual void ShowWorkFlow() {

        }
        #endregion 子类可以调用和覆盖的成员...

        #region 扩展的所有Public 函数...
        /// <summary>
        /// 通过模块ID打开模块
        /// </summary>
        /// <param name="id"></param>
        public void OpenFunctionModuleById(int id) {
            ModuleOpenState openState = new ModuleOpenState();
            openState.OpennedFrom = ModuleOpennedFrom.Menu;
            OpenFunctionModuleById(id, openState);
        }
        /// <summary>
        /// 通过模块ID打开模块
        /// </summary>
        /// <param name="id"></param>
        /// <param name="opennedFrom"></param>
        /// <param name="openState">调用者自定义的参数</param>
        public void OpenFunctionModuleById(int id, ModuleOpenState moduleOpenState) {
            DevExpress.XtraTreeList.Nodes.TreeListNode treeNode = trvListMain.FindNodeByFieldValue("ID", id);
            if (treeNode == null) {
                MB.WinBase.MessageBoxEx.Show("模块ID输入无效");
                return;
            }
            if (treeNode.Nodes.Count > 0) {
                MB.WinBase.MessageBoxEx.Show("不是有效的功能模块节点ID");
                return;
            }
            OpenModuleByTreeNode(treeNode, moduleOpenState);
        }

        /// <summary>
        /// 当前活动主窗口创建的所有菜单项.
        /// </summary>
        public Dictionary<DevExpress.XtraBars.BarButtonItem, System.ComponentModel.Design.CommandID> AllCreateMenuItems {
            get {
                return _AllCreateMenuItems;
            }
        }
        #endregion 扩展的所有Public 函数...

        #region add by aifang 2012-07-11 增加ToolTip显示备注信息...
        private void defaultToolTipController1_DefaultController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl is DevExpress.XtraTreeList.TreeList)
            {

                TreeList tree = (TreeList)e.SelectedControl;

                TreeListHitInfo hit = tree.CalcHitInfo(e.ControlMousePosition);

                if (hit.HitInfoType == HitInfoType.Cell)
                {

                    object cellInfo = new TreeListCellToolTipInfo(hit.Node, hit.Column, null);

                    string toolTip = string.Empty;
                    object remark = hit.Node.GetValue("CODE");
                    object name = hit.Node.GetValue("NAME");
                    if (remark != null) toolTip = remark.ToString();
                    else if (name != null) toolTip = name.ToString();

                    //string toolTip = remark == null ? hit.Node.GetValue("NAME").ToString() : remark.ToString();

                    e.Info = new DevExpress.Utils.ToolTipControlInfo(cellInfo, toolTip);
                    
                }

            }
        }
        #endregion add by aifang 2012-07-11 增加ToolTip显示备注信息...

        //增加个人收藏夹功能

        /// <summary>
        /// 整理收藏夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void barButItemFavoritesSet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        #region 初始化个人收藏夹相关...
        protected DevExpress.XtraTreeList.TreeList FavoritesTreeList
        {
            get
            {
                return trvFavorites;
            }
        }
        /// <summary>
        /// 初始化功能模块树。
        /// </summary>
        protected virtual void IniCreateFavoritesTree()
        {
            throw new MB.Util.APPException("继承的窗口必须实现 IniCreateFavoritesTree", MB.Util.APPMessageType.SysErrInfo);
        }
        protected virtual ModuleTreeNodeInfo GetFocusedFavoritesModuleId(DevExpress.XtraTreeList.Nodes.TreeListNode treeNode)
        {
            throw new MB.Util.APPException("继承的窗口必须实现 GetFocusedFavoritesModuleId", MB.Util.APPMessageType.SysErrInfo);
        }

        //打开焦点模块
        protected void OpenFavoritesModuleByTreeNode(DevExpress.XtraTreeList.Nodes.TreeListNode treeNode)
        {
            try
            {
                using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this))
                {
                    if (treeNode.Nodes.Count > 0) return;

                    ModuleTreeNodeInfo nodeInfo = GetFocusedFavoritesModuleId(treeNode);

                    //判断是否已经打开
                    foreach (Form f in this.MdiChildren)
                    {
                        MB.WinBase.IFace.IViewGridForm iView = f as MB.WinBase.IFace.IViewGridForm;
                        if (iView == null) continue;
                        if (iView.ClientRuleObject.ModuleTreeNodeInfo.ID == nodeInfo.ID)
                        {
                            (iView as Form).BringToFront();
                            return;
                        }
                    }

                    UICreateHelper.Instance.ShowViewGridForm(this, nodeInfo);

                }
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        protected bool PersonalFavoritesEnable
        {
            get {
                return subItemPersonalFavorites.Enabled;
            }
            set {
                subItemPersonalFavorites.Enabled = value;
            }
        }
        #endregion 初始化个人收藏夹相关...

        private void trvFavorites_DoubleClick(object sender, EventArgs e)
        {
            OpenFavoritesModuleByTreeNode(trvFavorites.FocusedNode);
        }

        private void barButItemViewFavorites_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IniCreateFavoritesTree();
            dockPanelPersonalFavorites.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }



        
    }


}
