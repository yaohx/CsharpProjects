using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MB.WinBase.IFace;
using System.Collections;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;

namespace MB.WinClientDefault.TreeViewList {

    public partial class TreeViewNodesSortingForm : DevExpress.XtraEditors.XtraForm {

        //private const string MSG_REQUIRE_REFRESH_BUTTONS = "78EB0107-8BB2-438C-B515-09CA0431E195";

        private IClientRule _ClientRuleObject;//构造函数初始化
        private ITreeListViewHoster _TreeListHoster;//构造函数初始化
        private DevExpress.XtraTreeList.Nodes.TreeListNode _SortingNode;//构造函数初始化,需要被排序的父节点
        private ImageList _ImageList;//构造函数初始化
        private MB.WinBase.Binding.BindingSourceEx _BindingSource;//TreeViewNodesSortingForm_Load中初始化

        private bool _IsMainTreeNeedRefreshed; //表示打开排序的主树是不是需要重新刷新

        public bool IsMainTreeNeedRefreshed { get { return _IsMainTreeNeedRefreshed; } }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parentSortingNode"></param>
        /// <param name="treeListHoster"></param>
        public TreeViewNodesSortingForm(DevExpress.XtraTreeList.Nodes.TreeListNode parentSortingNode,
            ITreeListViewHoster treeListHoster) {
            InitializeComponent();

            _SortingNode = parentSortingNode;
            _TreeListHoster = treeListHoster;
            _ClientRuleObject = treeListHoster as IClientRule;

            trvLstMain.OptionsBehavior.DragNodes = true;
            trvLstMain.OptionsView.ShowIndicator = false;
            _ImageList = MB.WinClientDefault.Images.ImageListHelper.Instance.GetDefaultTreeNodeImage();
            trvLstMain.StateImageList = _ImageList;
            trvLstMain.Dock = DockStyle.Fill;
            _IsMainTreeNeedRefreshed = false;

            if (_TreeListHoster == null || _TreeListHoster.TreeViewCfg == null 
                || string.IsNullOrEmpty(_TreeListHoster.TreeViewCfg.OrderFieldName)) {
                    throw new MB.Util.APPException("没有配置排序字段，不能打开排序窗口");
            }

        }

        private void TreeViewNodesSortingForm_Load(object sender, EventArgs e) {
            ITreeListViewHoster treeListHoster = _TreeListHoster;
            try {
                using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                    LoadDataObject();
                }
            }
            catch (MB.Util.APPException aex) {
                throw aex;
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "在浏览窗口TreeViewNodesSortingForm_Load 时出错！");
            }
        }

        private void barManager_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if (e.Item.Equals(btnSaveItem)) {
                Save();
                LoadDataObject();
            }
            else if (e.Item.Equals(btnQuit)) {
                this.Close();
                return;
            }
        }

        #region tree view list 事件
        private int _NodeImageIndex = 0;

        private void trvLstMain_DragDrop(object sender, DragEventArgs e) {
            if (_NodeImageIndex <= 0)
                e.Effect = DragDropEffects.None;
            else {
                TreeListNode dragNode, targetNode;
                TreeList tl = sender as TreeList;
                Point p = tl.PointToClient(new Point(e.X, e.Y));
                dragNode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
                targetNode = tl.CalcHitInfo(p).Node;
                tl.SetNodeIndex(dragNode, tl.GetNodeIndex(targetNode));
                e.Effect = DragDropEffects.None;
            }
        }

        private void trvLstMain_CalcNodeDragImageIndex(object sender, DevExpress.XtraTreeList.CalcNodeDragImageIndexEventArgs e) {
            _NodeImageIndex = e.ImageIndex;
            if (e.ImageIndex == 0) //表示DragNode become a child of the underlying node
                e.ImageIndex = -1;
        }
        #endregion

        #region 内部处理方法

        private void LoadDataObject() {
            ITreeListViewHoster treeListHoster = _TreeListHoster;
            IList lstDatas = null;
            try {
                int id = (int)_SortingNode.GetValue(_TreeListHoster.TreeViewCfg.KeyFieldName);
                MB.Util.Model.QueryParameterInfo[] queryParams = new Util.Model.QueryParameterInfo[1];
                queryParams[0] = new Util.Model.QueryParameterInfo(_TreeListHoster.TreeViewCfg.ParentFieldName, id, Util.DataFilterConditions.Equal);
                lstDatas = _ClientRuleObject.GetObjects((int)_ClientRuleObject.MainDataTypeInDoc, queryParams);
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "DefaultTreeListViewForm.GetObjects 出错！");
            }

            if (_BindingSource == null) {
                IBindingList bl = _ClientRuleObject.CreateMainBindList(lstDatas);
                _BindingSource = new MB.WinBase.Binding.BindingSourceEx();
                _BindingSource.DataSource = bl;
                treeListHoster.CreateTreeListViewDataBinding(trvLstMain, _BindingSource);

                if (trvLstMain.Nodes.Count > 0) {
                    trvLstMain.Nodes[0].Expanded = true;
                    trvLstMain.FocusedNode = trvLstMain.Nodes[0];
                }
            }
            else {
                treeListHoster.RefreshTreeListData(trvLstMain, lstDatas);
            }
        }

        private int Save() {
            int re = 0;
            System.ServiceModel.ICommunicationObject commObject = _ClientRuleObject.CreateServerCommunicationObject();
            try {
                int index = 0;
                foreach (TreeListNode node in this.trvLstMain.Nodes) {
                    int id = (int)node.GetValue(_TreeListHoster.TreeViewCfg.KeyFieldName);
                    string name = (string)node.GetValue(_TreeListHoster.TreeViewCfg.DisplayFieldName);//调试用

                    IList dataSource = _BindingSource.DataSource as IList;
                    foreach (object data in dataSource) {
                        int dataId = (int)MB.Util.MyReflection.Instance.InvokePropertyForGet(data, _TreeListHoster.TreeViewCfg.KeyFieldName);
                        if (id == dataId) {
                            MB.Util.Model.EntityState entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(data);
                            if (entityState == MB.Util.Model.EntityState.Persistent) {
                                MB.WinBase.UIDataEditHelper.Instance.SetEntityState(data, MB.Util.Model.EntityState.Modified);
                            }

                            MB.Util.MyReflection.Instance.InvokePropertyForSet(data, _TreeListHoster.TreeViewCfg.OrderFieldName, index);
                            _ClientRuleObject.AddToCache(commObject, (int)_ClientRuleObject.MainDataTypeInDoc, data, false, (string[])null);
                            index++;
                            break;
                        }
                    }
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
                    _IsMainTreeNeedRefreshed = true;
                    MB.WinBase.MessageBoxEx.Show("数据保存成功");
                }
                catch (Exception ex) {
                    MB.Util.TraceEx.Write(ex.Message);
                    throw new MB.Util.APPException("数据保存成功,但本地数据更新有误,请关闭窗口手动完成刷新", MB.Util.APPMessageType.DisplayToUser);
                }
            }
            return re;
        }

        #endregion
    }
}