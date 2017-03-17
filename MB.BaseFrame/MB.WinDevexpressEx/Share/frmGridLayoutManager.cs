using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.WinBase;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using MB.XWinLib.PivotGrid;

namespace MB.XWinLib.Share
{
    public partial class frmGridLayoutManager : Form
    {
        private Control _XtraGrid;
        private static readonly string GRID_LAYOUT_FILE_PATH = MB.Util.General.GeApplicationDirectory() + @"UserSetting\";
        private static readonly string GRID_LAYOUT_FILE_SETTING_FULLNAME = GRID_LAYOUT_FILE_PATH + "GridLayoutSetting.xml";
        private List<GridLayoutMainInfo> _GridLayoutMainList;
        private List<GridLayoutInfo> _GridLayoutList;
        private string _GridLayoutSectionName;
        private GridLayoutMainInfo _CurGridLayoutMain;
        private MB.WinBase.IFace.IForm _ContainerForm;

        public frmGridLayoutManager(DevExpress.XtraGrid.GridControl xtraGrid)
        {
            InitializeComponent();

            _XtraGrid = xtraGrid;

            _GridLayoutSectionName = GridLayoutManager.GetXtraGridLayoutSectionName(xtraGrid);

            this.Load += new EventHandler(frmGridLayoutManager_Load);
            this.txtLayoutName.KeyDown += new KeyEventHandler(txtLayoutName_KeyDown);

            lstLayoutNames.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(lstLayoutNames_ItemSelectionChanged);
        }

        public frmGridLayoutManager(MB.WinBase.IFace.IForm containerForm,PivotGridEx xtraGrid)
        {
            InitializeComponent();

            _ContainerForm = containerForm;
            _XtraGrid = xtraGrid;

            _GridLayoutSectionName = PivotGridHelper.Instance.getLayoutXmlSessionName(containerForm.ClientRuleObject, xtraGrid);

            this.Load += new EventHandler(frmGridLayoutManager_Load);
            this.txtLayoutName.KeyDown += new KeyEventHandler(txtLayoutName_KeyDown);

            lstLayoutNames.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(lstLayoutNames_ItemSelectionChanged);
        }

        void lstLayoutNames_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected) e.Item.BackColor = Color.CornflowerBlue;
            else e.Item.BackColor = Color.White;
        }

        void frmGridLayoutManager_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(GRID_LAYOUT_FILE_SETTING_FULLNAME))
            {
                _GridLayoutMainList = new MB.Util.Serializer.DataContractFileSerializer<List<GridLayoutMainInfo>>(GRID_LAYOUT_FILE_SETTING_FULLNAME).Read();
            }
            if (_GridLayoutMainList == null) _GridLayoutMainList = new List<GridLayoutMainInfo>();

            _CurGridLayoutMain = _GridLayoutMainList.Find(o => o.GridSectionName.Equals(_GridLayoutSectionName));
            if (_CurGridLayoutMain != null) _GridLayoutList = _CurGridLayoutMain.GridLayoutList;
            else{
                _CurGridLayoutMain = new GridLayoutMainInfo();
                _CurGridLayoutMain.GridSectionName = _GridLayoutSectionName;
                _GridLayoutMainList.Add(_CurGridLayoutMain);
            }

            if (_GridLayoutList == null) _GridLayoutList = new List<GridLayoutInfo>();

            //将数据加载到控件显示
            lstLayoutNames.Items.Clear();
            _GridLayoutList = _GridLayoutList.OrderByDescending(o => o.CreateTime).ToList<GridLayoutInfo>();
            foreach (var layoutInfo in _GridLayoutList)
            {
                lstLayoutNames.Items.Add(new ListViewItem(layoutInfo.Name));
            }
            setDefaultLayout();
        }
        //设置当前应用布局
        private void setDefaultLayout()
        {
            if (lstLayoutNames.Items.Count > 0)
            {
                lstLayoutNames.Items[0].Selected = true;
            }
        }

        private void lnkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (string.IsNullOrEmpty(txtLayoutName.Text)) {
                MessageBoxEx.Show("网格布局名称不能为空,请输入");
                txtLayoutName.Focus();
                return;
            }

            if (_GridLayoutList != null && _GridLayoutList.Exists(o => o.Name.Equals(txtLayoutName.Text)))
            {
                MessageBoxEx.Show("网格布局名称已存在,请重新输入");
                txtLayoutName.Focus();
                return;
            }

            //保存信息
            GridLayoutInfo gridLayoutInfo = new GridLayoutInfo();
            gridLayoutInfo.Name = txtLayoutName.Text;
            gridLayoutInfo.CreateTime = DateTime.Now;
            _GridLayoutList.Add(gridLayoutInfo);

            _CurGridLayoutMain._GridLayoutList = _GridLayoutList;
            try
            {
                saveLayoutInfo();
                DevExpress.XtraGrid.GridControl grid = _XtraGrid as DevExpress.XtraGrid.GridControl;
                if (grid != null)
                    GridLayoutManager.SaveXtraGridState(grid, gridLayoutInfo);
                else {
                    PivotGridEx pGrid = _XtraGrid as PivotGridEx;
                    PivotGridHelper.Instance.SavePivotGridLayout(_ContainerForm, pGrid);
                }

                lstLayoutNames.Items.Insert(0, new ListViewItem(txtLayoutName.Text));
                setDefaultLayout();

                txtLayoutName.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        private void lnkDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (lstLayoutNames.SelectedItems.Count == 0) {
                MessageBox.Show("请选择需要删除的项");
                return;
            }

            List<GridLayoutInfo> deleteList = new List<GridLayoutInfo>();
            foreach (ListViewItem item in lstLayoutNames.SelectedItems)
            {
                var gridLayoutInfo = _GridLayoutList.Find(o => o.Name.Equals(item.Text));
                if (gridLayoutInfo == null) continue;

                deleteList.Add(gridLayoutInfo);
                _GridLayoutList.Remove(gridLayoutInfo);
            }

            _CurGridLayoutMain._GridLayoutList = _GridLayoutList;
            try
            {
                saveLayoutInfo();

                foreach (var data in deleteList) {
                    DevExpress.XtraGrid.GridControl grid = _XtraGrid as DevExpress.XtraGrid.GridControl;
                    if (grid != null)
                        GridLayoutManager.DeleteXtraGridState(grid, data);
                    else
                    {
                        PivotGridEx pGrid = _XtraGrid as PivotGridEx;
                        PivotGridHelper.Instance.DeletePivotGridLayout(_ContainerForm, pGrid,data);
                    }
                }
                foreach (ListViewItem item in lstLayoutNames.SelectedItems){
                    lstLayoutNames.Items.Remove(item);
                }
                setDefaultLayout();

                //是否需要设置成第一项值？
                //if (lstLayoutNames.Items.Count > 0)
                //{
                //    var gridLayoutInfo = _GridLayoutList.Find(o => o.Name.Equals(lstLayoutNames.Items[0].Text));
                //    GridLayoutManager.RestoreXtraGridState(_XtraGrid, gridLayoutInfo);
                //}
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write(string.Format("删除布局失败，错误信息为：{0}", ex.Message));
                throw new MB.Util.APPException(string.Format("删除布局失败，错误信息为：{0}", ex.Message), Util.APPMessageType.DisplayToUser);
            }
        }

        private void butQuit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void butSave_Click(object sender, EventArgs e) {
            if (lstLayoutNames.SelectedItems.Count == 0 || lstLayoutNames.SelectedItems.Count > 1)
            {
                MessageBox.Show("请选择一个当前应用的项");
                return;
            }
            var gridLayoutInfo = _GridLayoutList.Find(o => o.Name.Equals(lstLayoutNames.SelectedItems[0].Text));
            if (gridLayoutInfo == null) return;

            try
            {
                gridLayoutInfo.CreateTime = DateTime.Now;
                saveLayoutInfo();
                DevExpress.XtraGrid.GridControl grid = _XtraGrid as DevExpress.XtraGrid.GridControl;
                if (grid != null)
                    GridLayoutManager.RestoreXtraGridState(grid, gridLayoutInfo);
                else
                {
                    PivotGridEx pGrid = _XtraGrid as PivotGridEx;
                    PivotGridHelper.Instance.RestoreLayout(pGrid,_ContainerForm.ClientRuleObject);
                }

            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write(string.Format("应用布局失败，错误信息为：{0}", ex.Message));
                throw new MB.Util.APPException(string.Format("应用布局失败，错误信息为：{0}", ex.Message), Util.APPMessageType.DisplayToUser);
            }
            
            this.Close();
        }

        private void lstLayoutNames_DoubleClick(object sender, EventArgs e) {
            this.butSave_Click(null, null);
        }

        //响应回车事件
        void txtLayoutName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                this.lnkAdd_LinkClicked(null, null);
            }
        }

        private void saveLayoutInfo()
        {
            try
            {
                var file = new MB.Util.Serializer.DataContractFileSerializer<List<GridLayoutMainInfo>>(GRID_LAYOUT_FILE_SETTING_FULLNAME);
                file.Write(_GridLayoutMainList);
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write(string.Format("保存布局失败，错误信息为：{0}", ex.Message));
                throw new MB.Util.APPException(string.Format("保存布局失败，错误信息为：{0}", ex.Message), Util.APPMessageType.DisplayToUser);
            }
        }
    }
}
