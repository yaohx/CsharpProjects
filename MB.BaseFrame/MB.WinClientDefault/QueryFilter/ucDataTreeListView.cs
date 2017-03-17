using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.WinBase.IFace;
using System.Collections;
using MB.WinBase.Common;
using MB.XWinLib.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace MB.WinClientDefault.QueryFilter {
    /// <summary>
    /// 树型浏览列表的数据控件。
    /// </summary>
    public partial class ucDataTreeListView : UserControl, IDataAssistantListControl {
        private ITreeListViewHoster _TreeListHoster;
        public ucDataTreeListView() {
            InitializeComponent();

            trvLstMain.Dock = DockStyle.Fill;
            trvLstMain.CustomDrawNodeImages += new DevExpress.XtraTreeList.CustomDrawNodeImagesEventHandler(trvLstMain_CustomDrawNodeImages);

        }

        void trvLstMain_CustomDrawNodeImages(object sender, DevExpress.XtraTreeList.CustomDrawNodeImagesEventArgs e)
        {
            if (_TreeListHoster != null) _TreeListHoster.FormatTreeListNode((TreeListEx)sender, e);
        }

        #region IDataAssistantListControl 成员
        private GetObjectDataAssistantEventHandle _AfterSelectData;
        /// <summary>
        /// 双击数据后产生的事件。
        /// </summary>
        public event GetObjectDataAssistantEventHandle AfterSelectData {
            add {
                _AfterSelectData += value;
            }
            remove {
                _AfterSelectData -= value;
            }
        }
        private void onAfterSelectData(GetObjectDataAssistantEventArgs arg) {
            if (_AfterSelectData != null)
                _AfterSelectData(this, arg);
        }

                /// <summary>
        ///  获取当前选择的行。
        /// </summary>
        /// <returns></returns>
        public object[] GetSelectRows() {
            if (trvLstMain.FocusedNode == null)
                return null;

            object record = trvLstMain.GetDataRecordByNode(trvLstMain.FocusedNode);
            return new object[] { record }; 
        }
        /// <summary>
        /// 设置控件数据源。
        /// </summary>
        /// <param name="clientRule"></param>
        /// <param name="dataSource"></param>
        public void SetDataSource(IClientRuleQueryBase clientRule, object dataSource) {
            IList lstDatas = dataSource as IList;
            if (lstDatas == null)
                throw new MB.Util.APPException("目前树型浏览列表只支持IList 数据类型", MB.Util.APPMessageType.SysErrInfo);

            _TreeListHoster = clientRule as ITreeListViewHoster;
            if (_TreeListHoster == null)
                throw new MB.Util.APPException("获取树型列表数据的业务对象必须继承 ITreeListViewHoster 接口。", MB.Util.APPMessageType.SysErrInfo);


            MB.WinBase.Binding.BindingSourceEx bindingSource = new MB.WinBase.Binding.BindingSourceEx();
            
            bindingSource.DataSource = dataSource;
            _TreeListHoster.CreateTreeListViewDataBinding(trvLstMain, bindingSource);
            trvLstMain.DataSource = lstDatas; //add by aifang 2012-04-17 数据绑定无效，重新对控件数据源赋值
            trvLstMain.RefreshDataSource();

            if (trvLstMain.Nodes.Count > 0) {
                trvLstMain.Nodes[0].Expanded = true;
                trvLstMain.FocusedNode = trvLstMain.Nodes[0];
            }

        }



        private bool _MultiSelect;
        public bool MultiSelect {
            get {
                return _MultiSelect;
            }
            set {
                _MultiSelect = value;
            }
        }
        private ColumnEditCfgInfo _ColumnEditCfgInfo;
        /// <summary>
        /// XML配置信息
        /// </summary>
        public ColumnEditCfgInfo ColumnEditCfgInfo
        {
            get
            {
                return _ColumnEditCfgInfo;
            }
            set
            {
                _ColumnEditCfgInfo = value;
            }
        }
        public void CheckListViewItem(bool checkAll) {
             
        }

        public IDictionary<int, object> GetSelectRowsWithIndex()
        {
            IDictionary<int, object> rows = new Dictionary<int, object>();
            object record = trvLstMain.GetDataRecordByNode(trvLstMain.FocusedNode);
            rows.Add(0, record);
            return rows;
        }
        public void CheckListViewItems(IEnumerable<int> selectedIds)
        {
            //throw new NotSupportedException();
        }
        #endregion

        private void trvLstMain_DoubleClick(object sender, EventArgs e) {
            var re = GetSelectRows();
            if (re != null) {
                onAfterSelectData(new GetObjectDataAssistantEventArgs(re));
            }
        }

        private void trvLstMain_BeforeFocusNode(object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e) {
            if (_TreeListHoster != null && _TreeListHoster.TreeViewCfg != null && 
                _TreeListHoster.TreeViewCfg.OnlyLeafNodeSelectable) {
                if (e.Node.HasChildren)
                    e.CanFocus = false;
            }
        }
    }
}
