//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-12-06
// Description	:	DefaultTreeListViewForm: 树型浏览主界面
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MB.WinBase.IFace; 
using MB.WinBase.Common;
using MB.WinClientDefault.TreeViewList;
using DevExpress.XtraTreeList.Nodes;
namespace MB.WinClientDefault {
    /// <summary>
    /// 树型浏览主界面
    /// </summary>
    public partial class DefaultTreeListViewForm : AbstractListViewForm {
        private ITreeListViewHoster _TreeListHoster;
        private List<object> _UnSaveEditEntitys;
        private bool _CurrentIsDropNode;
        private ImageList _ImageList;
        private ToolTip _ToolTip;

        private bool _RecreateTreeAfterSorting;//当排序以后重新生成树
        /// <summary>
        /// 树型浏览主界面
        /// </summary>
        public DefaultTreeListViewForm() {
            InitializeComponent();

            trvLstMain.Dock = DockStyle.Fill;
            trvLstMain.BeforeContextMenuClick += new MB.XWinLib.XtraTreeList.TreeListExMenuEventHandle(trvLstMain_BeforeContextMenuClick);
            this.Load += new EventHandler(DefaultTreeListViewForm_Load);

            //默认情况下设置为多选和可编辑,以后需要修改为根据权限来和业务规则来进行约束
            trvLstMain.OptionsBehavior.DragNodes = true;
           // trvLstMain.OptionsSelection.MultiSelect = true;
            trvLstMain.OptionsView.ShowIndicator = false;

             _ImageList = MB.WinClientDefault.Images.ImageListHelper.Instance.GetDefaultTreeNodeImage();
             trvLstMain.StateImageList = _ImageList;

            _UnSaveEditEntitys = new List<object>();
            _ToolTip = new ToolTip();
            _RecreateTreeAfterSorting = false;
        }

        void trvLstMain_BeforeContextMenuClick(object sender, MB.XWinLib.XtraTreeList.TreeListExMenuEventArg arg) {
            switch (arg.MenuType) {
                case MB.XWinLib.XtraGrid.XtraContextMenuType.SaveGridState:
                    MB.XWinLib.XtraTreeList.TreeListHelper.SaveXtraGridState(trvLstMain);   
                    break;
                case MB.XWinLib.XtraGrid.XtraContextMenuType.SortChildNode:
                    if (_TargetNodeAfterMouseRightClick != null) {

                        try {

                            TreeViewNodesSortingForm frm = new TreeViewNodesSortingForm(_TargetNodeAfterMouseRightClick, _TreeListHoster);
                            frm.ShowDialog();
                            if (frm.IsMainTreeNeedRefreshed) {
                                _RecreateTreeAfterSorting = true;
                                this.Refresh();
                            }
                        }
                        catch (Exception ex) {
                            MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        void DefaultTreeListViewForm_Load(object sender, EventArgs e) {
            if (MB.Util.General.IsInDesignMode()) return;

            if (_ClientRuleObject == null)
                throw new MB.Util.APPException("请检查功能模块节点的业务类配置是否正确", MB.Util.APPMessageType.DisplayToUser);
            //获取业务类默认设置的过滤条件参数
            _CurrentQueryParameters = _ClientRuleObject.GetDefaultFilterParams();
            _TreeListHoster = _ClientRuleObject as ITreeListViewHoster;
            if (_TreeListHoster == null)
                throw new MB.Util.APPException(string.Format("请检查{0} 是否已经实现了 ITreeListViewHoster接口", _ClientRuleObject.GetType().FullName), MB.Util.APPMessageType.SysErrInfo);

            LoadObjectData(_CurrentQueryParameters);


            if (_ClientRuleObject != null) {
                string titleMsg = string.Empty;
                if (string.IsNullOrEmpty(_ClientRuleObject.DefaultFilterMessage))
                    titleMsg = GetDefaultFilterMessage(_ClientRuleObject.ClientLayoutAttribute.DefaultFilter);
                else
                    titleMsg = _ClientRuleObject.DefaultFilterMessage;

                panTitle.Visible = true;
                _ToolTip.SetToolTip(panTitle, titleMsg);  
            }

             
        }

        #region 覆盖基类的方法...
        public override int Delete() {
            if (trvLstMain.FocusedNode == null) return 0;
            if (trvLstMain.FocusedNode.Nodes.Count > 0) {
                DialogResult re = MB.WinBase.MessageBoxEx.Question("存在下级节点,删除当前节点将把下级节点一起删除,是否继续");
                if (re != DialogResult.Yes) return 0;
                //throw new MB.Util.APPException("存在下级节点,请先删除下级节点", MB.Util.APPMessageType.DisplayToUser);
            }
            return base.Delete();
        }

        protected override void LoadObjectData(MB.Util.Model.QueryParameterInfo[] queryParams) {

            if (_ClientRuleObject == null)
                throw new MB.Util.APPException("在加载浏览窗口<DefaultViewForm>时 需要配置对应的ClientRule 类！");

            if (_ClientRuleObject.ClientLayoutAttribute == null)
                throw new MB.Util.APPException(string.Format("对于客户段逻辑类 {0} ,需要配置 RuleClientLayoutAttribute.", _ClientRuleObject.GetType().FullName));

            ITreeListViewHoster treeListHoster = _TreeListHoster;
            try {
                using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                    
                    if (_ClientRuleObject.ClientLayoutAttribute.CommunicationDataType == CommunicationDataType.DataSet) {
                        throw new MB.Util.APPException( "树型浏览主界面暂时不支持DataSet 的绑定" , MB.Util.APPMessageType.SysErrInfo);
                    }
                    else {

                        IList lstDatas = null;
                        try {
                            lstDatas = _ClientRuleObject.GetObjects((int)_ClientRuleObject.MainDataTypeInDoc, queryParams);
                        }
                        catch (Exception ex) {
                            throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "DefaultTreeListViewForm.GetObjects 出错！");
                        }

                        if (_BindingSource == null || _RecreateTreeAfterSorting) {
                            IBindingList bl = _ClientRuleObject.CreateMainBindList(lstDatas);
                            _BindingSource = new MB.WinBase.Binding.BindingSourceEx();
                            _BindingSource.ListChanged += new ListChangedEventHandler(_BindingSource_ListChanged);
                            _BindingSource.DataSource = bl;
                            treeListHoster.CreateTreeListViewDataBinding(trvLstMain, _BindingSource);

                            if (trvLstMain.Nodes.Count > 0) {
                                trvLstMain.Nodes[0].Expanded = true;
                                trvLstMain.FocusedNode = trvLstMain.Nodes[0];
                            }
                            _RecreateTreeAfterSorting = false;
                            
                        }
                        else {
                            treeListHoster.RefreshTreeListData(trvLstMain, lstDatas); 
                        }
                   
                    }
                    panTitle.Visible = false;
                }
            }
            catch (MB.Util.APPException aex) {
                throw aex;
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "在浏览窗口DefaultTreeListViewForm_Load 时出错！");
            }

        }
       
        protected override void ShowObjectEditForm(ObjectEditType editType) {
            checkExistsUnSaveData(true);
            
                base.ShowObjectEditForm(editType);
         
        }
        public override int Save() {
            if (!this.ExistsUnSaveData()) return 0;

            int re = 0;
            System.ServiceModel.ICommunicationObject commObject = _ClientRuleObject.CreateServerCommunicationObject();
            try {
                foreach (object editEntity in _UnSaveEditEntitys) {
                    //判断并追加登录用户的相关信息( 实体数据的登录用户操作信息一般只在主表中存在 )
                    MB.WinBase.UIDataEditHelper.Instance.AppendLoginUserInfo(editEntity);

                    //增加主表实体对象
                    _ClientRuleObject.AddToCache(commObject, (int)_ClientRuleObject.MainDataTypeInDoc, editEntity, false, (string[])null);
                }
                re = _ClientRuleObject.Flush(commObject);
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
            finally {
                try {
                    commObject.Close();
                }
                catch { }
            }
            if (re > 0) {
                try {
                    foreach (object entity in _UnSaveEditEntitys)
                        _ClientRuleObject.RefreshEntity((int)_ClientRuleObject.MainDataTypeInDoc, entity);
                    _UnSaveEditEntitys.Clear();
                    MB.WinBase.AppMessenger.DefaultMessenger.Publish(XtraRibbonMdiMainForm.MSG_REQUIRE_REFRESH_BUTTONS);

                    MB.WinBase.MessageBoxEx.Show("数据保存成功");
                }
                catch (Exception ex) {
                    MB.Util.TraceEx.Write(ex.Message);
                    throw new MB.Util.APPException("数据保存成功,但本地数据更新有误,请关闭窗口手动完成刷新", MB.Util.APPMessageType.DisplayToUser); 
                }

                //this.Refresh(); 
            }
            return re;
        }
        void _BindingSource_ListChanged(object sender, ListChangedEventArgs e) {
            if (!_CurrentIsDropNode) return;

            //判断如果是在 _EditEntitys 的修改 明显禁止的事件就不做处理。
            if (e.ListChangedType == ListChangedType.Reset) return;

            if (e.ListChangedType != ListChangedType.ItemChanged) {
                return;
            }

            object currentEntity = _BindingSource[e.NewIndex];

            MB.Util.Model.EntityState entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(currentEntity);
            if (entityState == MB.Util.Model.EntityState.Persistent) {
                MB.WinBase.UIDataEditHelper.Instance.SetEntityState(currentEntity, MB.Util.Model.EntityState.Modified);
                _UnSaveEditEntitys.Add(currentEntity);
                MB.WinBase.AppMessenger.DefaultMessenger.Publish(XtraRibbonMdiMainForm.MSG_REQUIRE_REFRESH_BUTTONS);
            }
            else if (entityState == MB.Util.Model.EntityState.Modified) {
                //保证最后加入的未存储容器的对象 Index 都在最后，以保证在树型数据保存中结构存储的正确性。
                if(_UnSaveEditEntitys.Contains(currentEntity)) 
                    _UnSaveEditEntitys.Remove(currentEntity);

                _UnSaveEditEntitys.Add(currentEntity);
                MB.WinBase.AppMessenger.DefaultMessenger.Publish(XtraRibbonMdiMainForm.MSG_REQUIRE_REFRESH_BUTTONS);
            }
        }
        /// <summary>
        /// 当前活动窗口的类型。
        /// </summary>
        public override MB.WinBase.Common.ClientUIType ActiveUIType {
            get {
                return MB.WinBase.Common.ClientUIType.GridViewForm;
            }
        }
        /// <summary>
        /// 当前数据主浏览控件。
        /// </summary>
        /// <param name="mustEditGrid"></param>
        /// <returns></returns>
        public override object GetCurrentMainGridView(bool mustEditGrid) {
            return trvLstMain;

        }
        /// <summary>
        /// 继承的子类必须继承的方法。
        /// </summary>
        /// <returns></returns>
        protected override MB.WinClientDefault.Common.MainViewDataNavigator GetViewDataNavigator() {
            return new MB.WinClientDefault.Common.MainViewDataNavigator(trvLstMain);
        }
        [System.Diagnostics.DebuggerStepThrough()]
        public override bool ExistsUnSaveData() {
 
                return _UnSaveEditEntitys.Count > 0;
        }
        #endregion 覆盖基类的方法...

        private void trvLstMain_DoubleClick(object sender, EventArgs e) {
            try {
                if (trvLstMain.FocusedNode != null) {
                    checkExistsUnSaveData(true);

                    Open();
                }
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }
        private void trvLstMain_CustomDrawNodeImages(object sender, DevExpress.XtraTreeList.CustomDrawNodeImagesEventArgs e) {
            //bool existsIco = false;
            //if (_TreeListHoster.TreeViewCfg != null && !string.IsNullOrEmpty(_TreeListHoster.TreeViewCfg.IcoFieldName)) {
            //    object val = e.Node.GetValue(_TreeListHoster.TreeViewCfg.IcoFieldName);
            //    if (val != null) {
            //        byte[] datas = (byte[])val;
            //        e.Graphics.DrawImage(MB.Util.MyConvert.Instance.ByteToImage(datas), e.StateRect.Location);
            //        existsIco = true;
            //    }
            //}
            //if (!existsIco) {
            _TreeListHoster.FormatTreeListNode(trvLstMain, e);

            //if (_ImageList.Images.Count > 0) {
            //    if (e.Node.HasChildren)
            //        e.Graphics.DrawImage(_ImageList.Images[0], e.StateRect.Location);
            //    else {
            //        e.Graphics.DrawImage(_ImageList.Images[2], e.StateRect.Location);
            //    }
            //}
            //}
        }

        private bool checkExistsUnSaveData(bool throwException) {
            if (_UnSaveEditEntitys.Count > 0) {
                if (throwException) {
                    throw new MB.Util.APPException("当前界面存在未保存的数据,请先保存", MB.Util.APPMessageType.DisplayToUser);  
                }
                return true;
            }
            return false;
        }

        protected override void OnClosing(CancelEventArgs e) {
            if (checkExistsUnSaveData(false)) {
                DialogResult re = MB.WinBase.MessageBoxEx.Question("当前界面存在未保存的数据,是否需要继续关闭");
                e.Cancel = re != DialogResult.Yes ;
                if (e.Cancel) {
                    this.Activate(); 
                }
  
            }
            base.OnClosing(e);
        }

        private void trvLstMain_BeforeDragNode(object sender, DevExpress.XtraTreeList.BeforeDragNodeEventArgs e) {
            _CurrentIsDropNode = true;
        }

        private void trvLstMain_AfterDragNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e) {
            _CurrentIsDropNode = false;
        }

        private TreeListNode _TargetNodeAfterMouseRightClick;//右键点击以后的节点

        private void trvLstMain_MouseDown(object sender, MouseEventArgs e) {
            DevExpress.XtraTreeList.TreeList tree = sender as DevExpress.XtraTreeList.TreeList;
            DevExpress.XtraTreeList.TreeListHitInfo hitInfo = tree.CalcHitInfo(e.Location);
            if (e.Button == MouseButtons.Right && hitInfo.HitInfoType ==
                DevExpress.XtraTreeList.HitInfoType.Cell) {
                    _TargetNodeAfterMouseRightClick = hitInfo.Node;
            }
        } 
    }
}                                              
