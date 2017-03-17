//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-09-25
// Description	:	报表模板设计。
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

using MB.WinPrintReport.Model;
namespace MB.WinPrintReport {
    /// <summary>
    /// 报表模板设计
    /// </summary>
    public partial class FrmEditPrintTemplete : Form {

        #region 变量定义...
        private string _ModuleID;
        private MB.WinPrintReport.Model.PrintTempleteContentInfo _PrintTemplete;
        private MB.WinPrintReport.IFace.IReportData _ReportDataHelper;
        private MB.WinBase.Binding.BindingSourceEx _BindingSource;
        private ReportTemplete _ReportTempleteHelper;
        private static readonly string TEMPLETE_NODE_NULL_TEXT = "[[请输入报表模板名称]]";
        #endregion 变量定义...

        #region 构造函数...
        /// <summary>
        /// 报表模板设计。
        /// </summary>
        /// <param name="reportDataHelper"></param>
        /// <param name="moduleID"></param>
        /// <param name="templeteID"></param>
        public FrmEditPrintTemplete(MB.WinPrintReport.IFace.IReportData reportDataHelper, System.Guid templeteID) {
            InitializeComponent();

            _ModuleID = reportDataHelper.ModuleID;
            _ReportDataHelper = reportDataHelper;
            _ReportTempleteHelper = new ReportTemplete(reportDataHelper);

            _PrintTemplete = reportDataHelper.GetPrintTemplete(templeteID);
            if (_PrintTemplete == null) {
                _PrintTemplete = createNewPrintTemplete();
                _PrintTemplete.GID = templeteID;
                _PrintTemplete.DataSource = (reportDataHelper.DataSource as DataSet).Tables[0].TableName;  
            }

            _BindingSource = new MB.WinBase.Binding.BindingSourceEx();
            _BindingSource.DataSource = _PrintTemplete;

        }
        #endregion 构造函数...

        private void FrmEditPrintTemplete_Load(object sender, EventArgs e) {
            iniCreateTempletTree();
            iniDataSourceName();

            dataBinding();

            trvReport.Nodes[0].ExpandAll(); 
        }

        protected override void OnClosing(CancelEventArgs e) {
            if (_ReportDataHelper.PrintTempleteChanged) {
                DialogResult re = MB.WinBase.MessageBoxEx.Question("打印模板已经发生改变,是否放弃保存");
                if (re == DialogResult.No)
                    e.Cancel = true;
            }
            base.OnClosing(e);
        }

        #region 内部函数处理...
        //检查模板输入的数据是否正确。
        private bool checkPrintTempleteInputData() {
            try {
                List<string> addItems = new List<string>();
                bool b = checkDataSourceSetting(addItems, _PrintTemplete);
                return b;
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);     
                return false;
            }
        }
        //检查数据源设置是否正确
        private bool checkDataSourceSetting(List<string> addItems, MB.WinPrintReport.Model.PrintTempleteContentInfo printTemplete) {
            if (addItems.Contains(printTemplete.DataSource))
                throw new MB.Util.APPException( string.Format("打印模板 {0} 的 数据源设置有重复,请检查",printTemplete.DataSource) , MB.Util.APPMessageType.DisplayToUser);
            else if (string.IsNullOrEmpty(printTemplete.Name))
                throw new MB.Util.APPException(string.Format("打印模板ID {0} 的 名称不能为空,请检查", printTemplete.GID), MB.Util.APPMessageType.DisplayToUser);
            else
                addItems.Add(printTemplete.DataSource);

            if (printTemplete.Childs != null && printTemplete.Childs.Count > 0) {
                foreach (var child in printTemplete.Childs) {
                    bool b = checkDataSourceSetting(addItems, child);
                    if (!b)
                        return false;
                }
            }
            return true;            
        }
        //创建新的报表模板
        private PrintTempleteContentInfo createNewPrintTemplete() {
            return new PrintTempleteContentInfo(System.Guid.NewGuid(), TEMPLETE_NODE_NULL_TEXT, string.Empty);
        }
        //初始化绑定数据源
        private void iniDataSourceName() {
            DataSet dsData = _ReportDataHelper.DataSource as DataSet;
            cobDataSource.Items.Clear(); 
            foreach (DataTable dt in dsData.Tables) {
                cobDataSource.Items.Add(dt.TableName);                  
            }
            if (cobDataSource.Items.Count > 0)
                cobDataSource.SelectedIndex = 0;
        }

        //初始化创建报表模板树
        private void iniCreateTempletTree() {
            createTreeNode(_PrintTemplete, trvReport.Nodes); 

        }

        //创建树型结点
        private TreeNode  createTreeNode(MB.WinPrintReport.Model.PrintTempleteContentInfo printTemplete,TreeNodeCollection childNodes) {
            System.Windows.Forms.TreeNode node = new TreeNode();
            node.Text = printTemplete.Name;
            node.Tag = printTemplete;
            childNodes.Add(node);
            node.ImageIndex = 2;
            node.SelectedImageIndex = 1;
            if (printTemplete.Childs == null || printTemplete.Childs.Count == 0)
                return node;

            foreach (var child in printTemplete.Childs) {
                createTreeNode(child, node.Nodes);
            }
            return node;
        }

        //创建数据绑定
        private void dataBinding() {

            this.txtGID.DataBindings.Add(new System.Windows.Forms.Binding("Text", _BindingSource,
                                                          "GID", true, DataSourceUpdateMode.OnPropertyChanged));

            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", _BindingSource,
                                                          "Name", true, DataSourceUpdateMode.OnPropertyChanged));

            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", _BindingSource,
                                                          "Remark", true, DataSourceUpdateMode.OnPropertyChanged));

            this.cobDataSource.DataBindings.Add(new System.Windows.Forms.Binding("Text", _BindingSource,
                                                          "DataSource", true, DataSourceUpdateMode.OnPropertyChanged));

        }

        //创建新的节点。
        private void   createNewNode(TreeNode currentNode) {
            MB.WinPrintReport.Model.PrintTempleteContentInfo newInfo = createNewPrintTemplete();

            TreeNode newNode = createTreeNode(newInfo, currentNode.Nodes);

            (currentNode.Tag as PrintTempleteContentInfo).Childs.Add(newInfo);

            trvReport.SelectedNode = newNode; 
        }

        //删除当前节点。
        private void deleteNode(TreeNode currentNode) {
            if (currentNode.Parent == null)
                throw new MB.Util.APPException("根节点不能进行删除", MB.Util.APPMessageType.DisplayToUser); 
            if(currentNode.Nodes.Count > 0)
                throw new MB.Util.APPException( "存在下级节点,不能进行删除", MB.Util.APPMessageType.DisplayToUser);

            (currentNode.Parent.Tag as PrintTempleteContentInfo).Childs.Remove(currentNode.Tag as PrintTempleteContentInfo);
            currentNode.Parent.Nodes.Remove(currentNode);  
        }

        //打印模板结构存储
        private void printTempleteSave() {
            bool b = checkPrintTempleteInputData();
            if(b)
                _ReportDataHelper.SavePrintTemplete(_PrintTemplete);   
        }
        #endregion 内部函数处理...

        #region 对象事件处理...
        private void trvReport_AfterSelect(object sender, TreeViewEventArgs e) {
            if (e.Node == null) return;

            _BindingSource.DataSource = e.Node.Tag;
        }

        private void trvReport_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                TreeNode node = trvReport.GetNodeAt(e.X, e.Y);
                if (node != null) {
                    menuItemDelete.Visible = node.Parent != null;
                    menuItemAddNew.Visible = node.Parent == null;

                    Point p = trvReport.PointToScreen(new Point(e.X, e.Y));
                    Point cp = this.PointToClient(p); 
                    contextMenuTreeNode.Show(this, cp); 
                }
            }
        }

        private void tButDesign_Click(object sender, EventArgs e) {
            using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                try {
                    _ReportTempleteHelper.ShowDesign(_ModuleID, trvReport.SelectedNode.Tag as MB.WinPrintReport.Model.PrintTempleteContentInfo);
                }
                catch (Exception ex) {
                    MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                }
            }
        }

        private void tButExit_Click(object sender, EventArgs e) {
          
            this.Close();
        }

        private void tButSave_Click(object sender, EventArgs e) {
            try {
                printTempleteSave();
                _ReportDataHelper.PrintTempleteChanged = false;
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);   
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e) {
            if(trvReport.SelectedNode!=null)
                trvReport.SelectedNode.Text = txtName.Text;
            _ReportDataHelper.PrintTempleteChanged = true;
        }

        private void trvReport_AfterLabelEdit(object sender, NodeLabelEditEventArgs e) {
            txtName.Text = e.Label;
            _ReportDataHelper.PrintTempleteChanged = true;
        }

        private void menuItemAddNew_Click(object sender, EventArgs e) {
            try {
                DataSet dsData = _ReportDataHelper.DataSource;
                if (dsData.Tables.Count == 1)
                    throw new MB.Util.APPException("目前的数据源结构不支持子报表模板", MB.Util.APPMessageType.DisplayToUser);

                if (trvReport.SelectedNode.Parent != null)
                    throw new MB.Util.APPException("目前的数据源结构只能支持2级的报表子模板", MB.Util.APPMessageType.DisplayToUser);
  
                createNewNode(trvReport.SelectedNode);
                _ReportDataHelper.PrintTempleteChanged = true;
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        private void menuItemDelete_Click(object sender, EventArgs e) {
            try {
                deleteNode(trvReport.SelectedNode);
                _ReportDataHelper.PrintTempleteChanged = true;
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }
        private void tButPreview_Click(object sender, EventArgs e) {
            using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                _ReportTempleteHelper.ShowPreview(_ModuleID, _PrintTemplete);
            }
        }

        private void menuItemPrintPreview_Click(object sender, EventArgs e) {
            tButPreview_Click(sender, e);
        }

        private void menuItemButPrint_Click(object sender, EventArgs e) {
            tButDesign_Click(sender, e);
        }

        #endregion 对象事件处理...

   
    }
}
