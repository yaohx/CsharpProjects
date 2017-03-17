using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MB.Tools.LogView {
    public partial class FrmLogView : Form {
        private string _Current_LogFile_Path = string.Empty;
        private string _LogFile_Full_Name = string.Empty;

        public FrmLogView() {
            InitializeComponent();

            gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            rxtLogDetail.Dock = System.Windows.Forms.DockStyle.Fill;



            fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(fileSystemWatcher1_Changed);
            fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(fileSystemWatcher1_Created);
        }



        /// <summary>
        /// 加载左边的日子文件树
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadLogFileTree(string filePath) {
            if (string.IsNullOrEmpty(filePath)) return;

            _Current_LogFile_Path = filePath;
            this.Cursor = Cursors.WaitCursor; 
            _LogFile_Full_Name = filePath;
            string[] files = System.IO.Directory.GetFiles(filePath);
            trvLogFiles.Nodes.Clear();
            TreeNode rootNode = trvLogFiles.Nodes.Add("应用程序日志");
            rootNode.ImageIndex = 0;
            System.Collections.Generic.SortedList<DateTime, string> sList = new SortedList<DateTime, string>(); 
            foreach (string f in files) {
                System.IO.FileInfo fInfo = new FileInfo(f);

                string name = System.IO.Path.GetFileNameWithoutExtension(f);
                if (sList.ContainsKey(fInfo.CreationTime)) continue; 
                sList.Add(fInfo.CreationTime, name); 
                
            }
            TreeNode dateNode = null;
            TreeNode selectNode = null;

            string sDate = string.Empty;
            foreach (DateTime key in sList.Keys) {
                string name = sList[key];
                string date = key.ToString("yyyy-MM-dd");
                if (string.Compare(sDate, date, true) != 0) {
                    dateNode = rootNode.Nodes.Add(date); //增加日期节点
                    dateNode.ImageIndex = 0;
                    dateNode.ImageIndex = 0;
                    sDate = date;
                }
                int flag = name.IndexOf(')');
                string text = name.Substring(flag + 1, name.Length - flag - 1);
                TreeNode childNode = dateNode.Nodes.Add(text);
                childNode.Tag = name;
                childNode.ImageIndex = 0;
                childNode.SelectedImageIndex = 1;
                if (null == selectNode)
                {
                    selectNode = childNode;
                }
            }
            rootNode.Expand();
            trvLogFiles.SelectedNode = selectNode;
            this.Cursor = Cursors.Default; 
        }

        private void menu_Open_Click(object sender, EventArgs e) {
            DialogResult re = folderBrowserDialog1.ShowDialog();
            if (re != DialogResult.OK && re != DialogResult.Yes) {
                return;
            }
            _Current_LogFile_Path = folderBrowserDialog1.SelectedPath;
            LoadLogFileTree(_Current_LogFile_Path);
        }

        private void menu_Exit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void trvLogFiles_AfterSelect(object sender, TreeViewEventArgs e) {
            showTrackLogs();
        }

        private void trvLogFiles_AfterCheck(object sender, TreeViewEventArgs e)
        {
            checkControl(e);
        }

        private void FrmMain_Load(object sender, EventArgs e) {

            iniLoadMsgType();
        }

        private void butFilter_Click(object sender, EventArgs e) {
            if (gridControl1.DataSource == null) return;

            string filter = getFilter();
            DataView dv = gridControl1.DataSource as DataView;
            dv.RowFilter = filter;
        }

        private void loadDataToGrid(List<TreeNode> nodes) {
            this.Cursor = Cursors.WaitCursor;
            try {
                LogDataLoadInfo loadInfo = new LogDataLoadInfo();
                List<string> logFileNames = new List<string>();
                foreach (TreeNode node in nodes) {
                    if (node == null || node.Parent == null || node.Tag == null) continue;

                    string fullFileName = _Current_LogFile_Path + @"\" + node.Tag.ToString() + ".txt";
                    logFileNames.Add(fullFileName); 
                }
                if (logFileNames.Count == 0) {
                    this.Cursor = Cursors.Default;
                    return;
                }

                LogFileHelper.LoadDataToCtl(gridControl1, logFileNames.ToArray() , loadInfo);

                labExecuteTime.Text = "1)本次服务器记录时间从：" + loadInfo.BeginDate.ToString() + " 到：" + loadInfo.EndDate.ToString();
                labLoadInfo.Text = "2)发现错误的消息有：" + loadInfo.ErrCount + " 个,超过1秒执行的方法有：" + loadInfo.OverTimeCount.ToString() + "个。";
            }
            catch(Exception ex) {
                MessageBox.Show("加载日记有误,请检查是否有该目录的访问权限？" + ex.Message); 
            } 
            this.Cursor = Cursors.Default; 
        }

        private string getFilter() {
            return string.Empty;
        }

        private void dGridLogData_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {

        }
        private bool goToPosition(Control ctl, string fieldName,int index) {
            return false;
        }
        private void iniLoadMsgType() {

        }
        private void showTrackLogs() {
            List<TreeNode> checkedNodes = new List<TreeNode>();
            getCheckedNodes(trvLogFiles.Nodes, checkedNodes);
            if (checkedNodes.Count == 0)
                checkedNodes.Add(trvLogFiles.SelectedNode);

            loadDataToGrid(checkedNodes);
        }
        private void butRefreshTree_Click(object sender, EventArgs e) {
            _Current_LogFile_Path = @"u:\\";
            LoadLogFileTree(_Current_LogFile_Path);
        }

        private void butRefreshLogFile_Click(object sender, EventArgs e) {
            showTrackLogs();
        }
        private void getCheckedNodes(TreeNodeCollection nodes, List<TreeNode> checkedNodes) {
            foreach (TreeNode node in nodes) {
                if (node.Checked)
                    checkedNodes.Add(node);

                if (node.Nodes.Count > 0)
                    getCheckedNodes(node.Nodes, checkedNodes);
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            _Current_LogFile_Path = @"v:\\";
            LoadLogFileTree(_Current_LogFile_Path);
        }

        private void chkReslTimeRefresh_CheckedChanged(object sender, EventArgs e) {

        }

        void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e) {

        }
        void fileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e) {

        }

        private void addLeaveData() {

        }

        private void dGridLogData_CurrentCellChanged(object sender, EventArgs e) {

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
            if (e.FocusedRowHandle < 0) return;

            var drData = gridView1.GetDataRow(e.FocusedRowHandle);
            rxtLogDetail.Clear();
            if(drData["Detail"]!=System.DBNull.Value)
                rxtLogDetail.AppendText(drData["Detail"].ToString());
        }

        private void gridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e) {

        }

        private void checkControl(TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node != null && !Convert.IsDBNull(e.Node))
                {
                    checkParentNode(e.Node);
                    if (e.Node.Nodes.Count > 0)
                    {
                        checkAllChildNodes(e.Node, e.Node.Checked);
                    }
                }
            }
        }

        private void checkAllChildNodes(TreeNode pn, bool IsChecked)
        {
            foreach (TreeNode tn in pn.Nodes)
            {
                tn.Checked = IsChecked;

                if (tn.Nodes.Count > 0)
                {
                    checkAllChildNodes(tn, IsChecked);
                }
            }
        }

        private void checkParentNode(TreeNode curNode)
        {
            bool bChecked = false;

            if (curNode.Parent != null)
            {
                foreach (TreeNode node in curNode.Parent.Nodes)
                {
                    if (node.Checked)
                    {
                        bChecked = true;
                        break;
                    }
                }

                if (bChecked)
                {
                    curNode.Parent.Checked = true;
                    checkParentNode(curNode.Parent);
                }
                else
                {
                    curNode.Parent.Checked = false;
                    checkParentNode(curNode.Parent);
                }
            }
        }
    }
}