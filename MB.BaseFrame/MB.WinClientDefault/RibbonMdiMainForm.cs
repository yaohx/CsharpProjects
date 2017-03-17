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
namespace MB.WinClientDefault {
    /// <summary>
    ///  Ribbon 样式的系统操作主窗口。
    /// </summary>
    public partial class RibbonMdiMainForm : DevExpress.XtraBars.Ribbon.RibbonForm, MB.WinBase.IFace.IMdiMainForm {
        private MB.Util.Model.ModuleTreeNodeInfo[] _ModuleTreeNodes;
        private static string _SystemName = "MBMS";
        private UICommandExecutor _CommandExecutor;
        private string _MdiLayoutFileName = MB.Util.General.GeApplicationDirectory() + _SystemName + ".Xml";
        private string _CurrentUserName;
        private Dictionary<DevExpress.XtraBars.BarButtonItem, System.ComponentModel.Design.CommandID> _AllCreateMenuItems;
        private Dictionary<DevExpress.XtraBars.BarButtonItem, bool> _RejectCommandItems;  

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
        /// 
        /// </summary>
        public RibbonMdiMainForm(string systemName, MB.Util.Model.ModuleTreeNodeInfo[] treeNodes) {
            InitializeComponent();

            _SystemName = systemName;
            _ModuleTreeNodes = treeNodes;

            _CommandExecutor = new UICommandExecutor(this);
            _RejectCommandItems = new Dictionary<DevExpress.XtraBars.BarButtonItem, bool>();  

            _AllCreateMenuItems = new Dictionary<DevExpress.XtraBars.BarButtonItem,System.ComponentModel.Design.CommandID>();
        }

        private void RibbonMdiMainForm_Load(object sender, EventArgs e) {
            if (MB.Util.General.IsInDesignMode()) return;

            initSkinGallery();

            try {
                iniCreateModuleTree();

                if (System.IO.File.Exists(_MdiLayoutFileName)) {
                    dockManagerMain.RestoreLayoutFromXml(_MdiLayoutFileName);

                  //  this.barManagerMain.DockManager = this.dockManagerMain;

                }
                this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
            _BeginDate = System.DateTime.Now;
            timer1.Enabled = true;
            double versionNum = MB.WinClientDefault.VersionAutoUpdate.VersionAutoUpdateHelper.GetClientVersionNumber();
            if (versionNum <= 0)
                versionNum = 1.0;

            _CurrentUserName = "Administrator";
            if (MB.WinBase.AppEnvironmentSetting.Instance.CurrentLoginUserInfo != null)
                _CurrentUserName = MB.WinBase.AppEnvironmentSetting.Instance.CurrentLoginUserInfo.DISP_NAME;
            bStaticItemUserName.Caption = string.Format("系统版本号: {0}     登录用户: {1}", versionNum, _CurrentUserName);

            iniCreateMenuItem();
        }
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
        }

        private DateTime _BeginDate;
        private void timer1_Tick(object sender, EventArgs e) {
            var sp = System.DateTime.Now.Subtract(_BeginDate);
            int totalHouse = System.Convert.ToInt32(sp.TotalHours);
            TimeSpan nsp = new TimeSpan(totalHouse, sp.Minutes, sp.Seconds);

            barStaticItemTime.Caption = string.Format("累计在线时间: {0}", nsp.ToString());

           if (this.ActiveMdiChild != null) {
                setAllButtonEnabled(true);
                MB.WinBase.IFace.IViewGridForm gridViewForm = this.ActiveMdiChild as MB.WinBase.IFace.IViewGridForm;
                if (gridViewForm != null) {
                    bool existsUnSave = gridViewForm.ExistsUnSaveData();
                    bButSave.Enabled = existsUnSave && checkExistsRejectCommand(bButSave);
                    bButAddNew.Enabled = !existsUnSave && checkExistsRejectCommand(bButAddNew);
                    bButDelete.Enabled = !existsUnSave && checkExistsRejectCommand(bButDelete);
                    bButFilter.Enabled = !existsUnSave && checkExistsRejectCommand(bButFilter);
                    bButAdvanceFilter.Enabled = !existsUnSave && checkExistsRejectCommand(bButAdvanceFilter);
                    bButRefresh.Enabled = !existsUnSave && checkExistsRejectCommand(bButRefresh);
                    bButOpen.Enabled = !existsUnSave && checkExistsRejectCommand(bButOpen);
                    bButImport.Enabled = !existsUnSave && checkExistsRejectCommand(bButImport);
                }
            }
            else {
                setAllButtonEnabled(false);
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

        #region 初始化功能模块树相关...
        private void trvMainFunction_DoubleClick(object sender, EventArgs e) {
            try {
                using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                    TreeViewHitTestInfo hitInfo = trvMainFunction.HitTest(_CurrentMovePoint);
                    if (hitInfo == null || hitInfo.Node == null) return;

                    trvMainFunction.SelectedNode = hitInfo.Node;

                    ModuleTreeNodeInfo nodeInfo = hitInfo.Node.Tag as ModuleTreeNodeInfo;
                    OnBeforeDoubleClickTreeNode(new MdiMainFunctionTreeEventArgs(trvMainFunction.SelectedNode, nodeInfo));

                    if (nodeInfo == null || nodeInfo.Commands == null || nodeInfo.Commands.Count == 0)
                        return;

                    //判断是否已经打开
                    foreach (Form f in this.MdiChildren) {
                        MB.WinBase.IFace.IViewGridForm iView = f as MB.WinBase.IFace.IViewGridForm;
                        if (iView == null) continue;
                        if (iView.ClientRuleObject.ModuleTreeNodeInfo.Equals(nodeInfo)) {
                            (iView as Form).BringToFront();
                            return;
                        }
                    }

                    UICreateHelper.Instance.ShowViewGridForm(this, nodeInfo);

                    // _MenuManager.RefreshToolsButtonItem(nodeInfo);  
                }
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }

        }
        private Point _CurrentMovePoint;
        private void trvMainFunction_MouseMove(object sender, MouseEventArgs e) {
            _CurrentMovePoint = new Point(e.X, e.Y);
        }
        [Browsable(false)]
        protected ImageList ModuleTreeImages {
            get {
                return trvMainFunction.ImageList;
            }
            set {
                trvMainFunction.ImageList = value;
            }
        }
        private void trvMainFunction_AfterSelect(object sender, TreeViewEventArgs e) {

        }
        //初始化创建功能模块树
        private void iniCreateModuleTree() {
            IOrderedEnumerable<ModuleTreeNodeInfo> tnodes = _ModuleTreeNodes.OrderBy<ModuleTreeNodeInfo, int>(o => o.Index);
            IOrderedEnumerable<ModuleTreeNodeInfo> nodes = tnodes.OrderBy<ModuleTreeNodeInfo, int>(o => o.LevelNum);
            TreeNode rootNode = trvMainFunction.Nodes.Add(_SystemName);
            createModuleTreeNode(rootNode, nodes.ToArray<ModuleTreeNodeInfo>(), nodes.ToArray<ModuleTreeNodeInfo>());

            rootNode.Expand();
        }
        //创建功能模块树节点。
        private List<MB.Util.Model.ModuleTreeNodeInfo> _HasCreateModule = new List<ModuleTreeNodeInfo>();
        private void createModuleTreeNode(TreeNode rootNode, MB.Util.Model.ModuleTreeNodeInfo[] allNodes, MB.Util.Model.ModuleTreeNodeInfo[] treeNodes) {
            // IOrderedEnumerable<ModuleTreeNodeInfo> nodes = treeNodes.OrderBy<ModuleTreeNodeInfo,int>(o=>o.Index);
            foreach (ModuleTreeNodeInfo node in treeNodes) {
                if (_HasCreateModule.Contains(node)) continue;

                _HasCreateModule.Add(node);

                TreeNode treeNode = rootNode.Nodes.Add(node.ID.ToString(), node.Name);

                treeNode.Tag = node;

                OnAfterCreateModuleNode(new MdiMainFunctionTreeEventArgs(treeNode, node));

                IEnumerable<ModuleTreeNodeInfo> childs = allNodes.Where<ModuleTreeNodeInfo>(o => o.PrevID == node.ID);
                if (childs.Count<ModuleTreeNodeInfo>() > 0)
                    createModuleTreeNode(treeNode, allNodes, childs.ToArray<ModuleTreeNodeInfo>());
            }


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
        #endregion 功能菜单项...

        private void RibbonMdiMainForm_MdiChildActivate(object sender, EventArgs e) {
            if (trvMainFunction.SelectedNode == null || trvMainFunction.SelectedNode.Tag == null) return;
            ModuleTreeNodeInfo nodeInfo = trvMainFunction.SelectedNode.Tag as ModuleTreeNodeInfo;
            var afrm = this.GetActiveMdiChildForm();
            if (afrm != null && afrm.ClientRuleObject != null)
                nodeInfo = afrm.ClientRuleObject.ModuleTreeNodeInfo;
            refreshToolsButtonItem(nodeInfo);  
        }
        //根据当前活动的子窗口 格式化操作的菜单项
        private void refreshToolsButtonItem(MB.Util.Model.ModuleTreeNodeInfo moduleInfo) {
            _RejectCommandItems.Clear();
            foreach (DevExpress.XtraBars.BarButtonItem butItem in _AllCreateMenuItems.Keys) {
                butItem.Enabled = true;
            }
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
            if (activeForm == null) return;

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




        #region IMdiMainForm 成员

        /// <summary>
        /// 
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
        #endregion
    }
}
