//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-24
// Description	:	默认的主窗口编辑界面。
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

using MB.WinClientDefault.Menu;
using MB.WinClientDefault.UICommand;
using MB.Util.Model;
namespace MB.WinClientDefault {
    /// <summary>
    /// 默认的主窗口编辑界面。
    /// </summary>
    public partial class DefaultMdiMainForm : AbstractBaseForm, MB.WinBase.IFace.IMdiMainForm {
        private XtraMenuManager _MenuManager;
        private UICommandExecutor _CommandExecutor;
        private MB.Util.Model.ModuleTreeNodeInfo[] _ModuleTreeNodes;
        private static string _SystemName = "MBMS";
        private string _MdiLayoutFileName = MB.Util.General.GeApplicationDirectory() + _SystemName + ".Xml";

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
        protected virtual void OnAfterCreateModuleNode(MdiMainFunctionTreeEventArgs arg){
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

        #region 构造函数...
      /// <summary>
        /// 调用默认的主窗口
      /// </summary>
      /// <param name="systemName"></param>
      /// <param name="treeNodes"></param>
        public DefaultMdiMainForm(string systemName,MB.Util.Model.ModuleTreeNodeInfo[] treeNodes)
            : this(systemName,null, treeNodes) {
        }

        /// 调用默认的主窗口
        /// </summary>
        /// <param name="systemName">命令执行控制器</param>
         /// <param name="commandExecutor"></param>
        /// <param name="treeNodes">功能模块树节点</param>
        public DefaultMdiMainForm(string systemName, UICommandExecutor commandExecutor, MB.Util.Model.ModuleTreeNodeInfo[] treeNodes) {

            InitializeComponent();

            _ModuleTreeNodes = treeNodes;
            _SystemName = systemName;

  

            if (commandExecutor == null)
                _CommandExecutor = new UICommandExecutor(this);
            else
                _CommandExecutor = commandExecutor;

            _MenuManager = new XtraMenuManager(_CommandExecutor, barManagerMain, barMainMenu, barTools);
            trvMainFunction.Dock = DockStyle.Fill;
            rxtMessage.Dock = DockStyle.Fill;

            //测试代码
            rxtMessage.SelectionColor = Color.Red;
            rxtMessage.SelectionFont = new Font(this.Font, FontStyle.Bold);
            double  versionNum = MB.WinClientDefault.VersionAutoUpdate.VersionAutoUpdateHelper.GetClientVersionNumber();
            if (versionNum <= 0)
                versionNum = 1.0;
           // rxtMessage.AppendText(string.Format("欢迎使用{0}... 当前版本号  {1} \n",systemName,versionNum));
           // rxtMessage.SelectionColor = Color.Black;
           //  rxtMessage.AppendText("系统公告：");

            //rxtMessage.SelectionColor = Color.Blue;
            //Font old = rxtMessage.SelectionFont;
           
            //rxtMessage.SelectionFont = new Font(this.Font, FontStyle.Underline);
            //rxtMessage.AppendText("在使用本系统之前请详细阅读美邦国际信息安全条款！");
            //rxtMessage.SelectionColor = Color.Black;
            //rxtMessage.SelectionFont = old;

            this.Text = systemName;

           

            _CurrentUserName = "Administrator";
            if (MB.WinBase.AppEnvironmentSetting.Instance.CurrentLoginUserInfo != null)
                _CurrentUserName = MB.WinBase.AppEnvironmentSetting.Instance.CurrentLoginUserInfo.DISP_NAME;

            barStaticItemUserName.Caption = string.Format("系统版本号: {0}     登录用户: {1}", versionNum,_CurrentUserName);
            _BeginDate = System.DateTime.Now;
            timer1.Enabled = true;
            this.SizeChanged += new EventHandler(DefaultMdiMainForm_SizeChanged);

            dockPanelOnlineMsg.Hide(); 
        }

        void DefaultMdiMainForm_SizeChanged(object sender, EventArgs e) {
            barStaticItemUserName.Width = this.Width - 120;
        }
        #endregion 构造函数...
        private DateTime _BeginDate;
        private string _CurrentUserName;
        private void timer1_Tick(object sender, EventArgs e) {
            var sp = System.DateTime.Now.Subtract(_BeginDate);
            int totalHouse = System.Convert.ToInt32(sp.TotalHours);
            TimeSpan nsp = new TimeSpan(totalHouse, sp.Minutes, sp.Seconds);
          
            barStaticItemTime.Caption = string.Format("累计在线时间: {0}", nsp.ToString());
        }
        
        private void DefaultMdiMainForm_Load(object sender, EventArgs e) {
            try {
                if (MB.Util.General.IsInDesignMode()) return;
                _MenuManager.CreateDefaultMenu();

                IOrderedEnumerable<ModuleTreeNodeInfo> tnodes = _ModuleTreeNodes.OrderBy<ModuleTreeNodeInfo, int>(o => o.Index);
                IOrderedEnumerable<ModuleTreeNodeInfo> nodes = tnodes.OrderBy<ModuleTreeNodeInfo, int>(o => o.LevelNum);
                TreeNode rootNode = trvMainFunction.Nodes.Add(_SystemName);
                createModuleTreeNode(rootNode, nodes.ToArray<ModuleTreeNodeInfo>(),nodes.ToArray<ModuleTreeNodeInfo>());

                rootNode.Expand();

                if (System.IO.File.Exists(_MdiLayoutFileName)) {
                    dockManagerMain.RestoreLayoutFromXml(_MdiLayoutFileName);

                    this.barManagerMain.DockManager = this.dockManagerMain;

                }
                this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);    
            }
        }

        //创建功能模块树节点。
        private List<MB.Util.Model.ModuleTreeNodeInfo> _HasCreateModule = new List<ModuleTreeNodeInfo>();
        private void createModuleTreeNode(TreeNode rootNode,MB.Util.Model.ModuleTreeNodeInfo[] allNodes,MB.Util.Model.ModuleTreeNodeInfo[] treeNodes) {
           // IOrderedEnumerable<ModuleTreeNodeInfo> nodes = treeNodes.OrderBy<ModuleTreeNodeInfo,int>(o=>o.Index);
            foreach (ModuleTreeNodeInfo node in treeNodes) {
                if (_HasCreateModule.Contains(node)) continue;

                _HasCreateModule.Add(node);

                TreeNode treeNode = rootNode.Nodes.Add(node.ID.ToString(),node.Name);
                
                treeNode.Tag = node; 

                OnAfterCreateModuleNode(new MdiMainFunctionTreeEventArgs(treeNode,node));

                IEnumerable<ModuleTreeNodeInfo> childs = allNodes.Where<ModuleTreeNodeInfo>(o => o.PrevID == node.ID);
                if(childs.Count<ModuleTreeNodeInfo>() > 0)
                    createModuleTreeNode(treeNode, allNodes,childs.ToArray<ModuleTreeNodeInfo>());
            }

          
        }

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
                        MB.WinBase.IFace.IViewGridForm IView = f as MB.WinBase.IFace.IViewGridForm;
                        if (IView != null) {
                            if (IView.ClientRuleObject.ModuleTreeNodeInfo.Equals(nodeInfo)) {
                                (IView as Form).BringToFront();
                                return;
                            }
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

        private void DefaultMdiMainForm_MdiChildActivate(object sender, EventArgs e) {
            if (trvMainFunction.SelectedNode == null || trvMainFunction.SelectedNode.Tag == null) return;
            ModuleTreeNodeInfo nodeInfo = trvMainFunction.SelectedNode.Tag as ModuleTreeNodeInfo;
            var afrm = this.GetActiveMdiChildForm();
            if (afrm != null && afrm.ClientRuleObject!=null)
                nodeInfo = afrm.ClientRuleObject.ModuleTreeNodeInfo;
            _MenuManager.RefreshToolsButtonItem(nodeInfo);  
        }

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
            dockPanelOnlineMsg.Show();
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

    #region 自定义事件原型...
    /// <summary>
    /// 主窗口功能模块自定义事件类型定义。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="arg"></param>
    public delegate void MdiMainFunctionTreeEventHandle(object sender,MdiMainFunctionTreeEventArgs arg);
    /// <summary>
    /// 主窗口功能模块自定义事件类型参数定义。
    /// </summary>
    public class MdiMainFunctionTreeEventArgs{
        private TreeNode _Node;
        private ModuleTreeNodeInfo _ModuleNodeInfo;
        public MdiMainFunctionTreeEventArgs(TreeNode node, ModuleTreeNodeInfo moduleNodeInfo) {
            _Node = node;
            _ModuleNodeInfo = moduleNodeInfo;
        }
        /// <summary>
        /// 当前正在创建的节点。
        /// </summary>
        public TreeNode Node {
            get {
                return _Node;
            }
            set {
                _Node = value;
            }
        }
        /// <summary>
        /// 当前模块的节点信息。
        /// </summary>
        public ModuleTreeNodeInfo ModuleNodeInfo {
            get {
                return _ModuleNodeInfo;
            }
            set {
                _ModuleNodeInfo = value;
            }
        }
    }
    #endregion 自定义事件原型...
}
