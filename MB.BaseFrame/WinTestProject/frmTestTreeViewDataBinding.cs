using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinTestProject {
    public partial class frmTestTreeViewDataBinding : Form {
        public frmTestTreeViewDataBinding() {
            InitializeComponent();
            this.Load += new EventHandler(frmTestTreeViewDataBinding_Load);
        }

        void frmTestTreeViewDataBinding_Load(object sender, EventArgs e) {
            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e) {
            List<MyTreeNodeData> lstData = new List<MyTreeNodeData>();
            lstData.Add(new MyTreeNodeData("A1", "美邦测试", null));
            lstData.Add(new MyTreeNodeData("A2", "美邦测试1", null));
            lstData.Add(new MyTreeNodeData("A3", "美邦测试2", "A1"));
            lstData.Add(new MyTreeNodeData("A4", "美邦测试2-2", "A1"));
            lstData.Add(new MyTreeNodeData("A5", "美邦测试3", "A2"));
            lstData.Add(new MyTreeNodeData("A6", "美邦测试4", "A5"));
            lstData.Add(new MyTreeNodeData("A7", "美邦测试5", "A6"));

            MB.WinBase.Ctls.TreeViewDataBinding<MyTreeNodeData> dataBinding = new MB.WinBase.Ctls.TreeViewDataBinding<MyTreeNodeData>(treeView1, "Text", "ID", "PrevID");
            dataBinding.CurrentFormate = new myTreeNodeFormate();
            //dataBinding.RootNodePrevKeyValue = "-1";
            dataBinding.RefreshDataSource(lstData); 

           

        }

        private void button1_Click_1(object sender, EventArgs e) {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Rows.Add(new object[] { "1", "A" });
            dt.Rows.Add(new object[] { "2", "B" });
            dt.Rows.Add(new object[] { "3", "AA" });
            dt.Rows.Add(new object[] { "4", "C" });
            dt.Rows.Add(new object[] { "5", "CC" });

            ucCheckedListCombox1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e) {
            MyTreeNodeData m = new MyTreeNodeData();
            m.ID = "qwrqwrqw";
            var xml = new MB.Util.Serializer.EntityXmlSerializer<MyTreeNodeData>();
            string str = xml.SingleSerializer(m, "RootNode");
            var mmm = xml.SingleDeSerializer(str, "RootNode"); 

        }

        private void frmTestTreeViewDataBinding_Load_1(object sender, EventArgs e) {

        }
        private MB.WinBase.Binding.BindingSourceEx bindingSource = new MB.WinBase.Binding.BindingSourceEx();  
        private void button3_Click(object sender, EventArgs e) {
            xtraTreeList1.KeyFieldName = "ID";
            xtraTreeList1.ParentFieldName = "PrevID";
            xtraTreeList1.PreviewFieldName = "Text";

            List<MyTreeNodeData> lstData = new List<MyTreeNodeData>();
            lstData.Add(new MyTreeNodeData("A1", "美邦测试", null));
            lstData.Add(new MyTreeNodeData("A2", "美邦测试1", null));
            lstData.Add(new MyTreeNodeData("A3", "美邦测试2", "A1"));
            lstData.Add(new MyTreeNodeData("A4", "美邦测试2-2", "A1"));
            lstData.Add(new MyTreeNodeData("A5", "美邦测试3", "A2"));
            lstData.Add(new MyTreeNodeData("A6", "美邦测试4", "A5"));
            lstData.Add(new MyTreeNodeData("A7", "美邦测试5", "A6"));

            bindingSource.DataSource = lstData;

            MB.XWinLib.XtraTreeList.TreeListHelper<MyTreeNodeData> binding = new MB.XWinLib.XtraTreeList.TreeListHelper<MyTreeNodeData>();
            binding.CreateDataBinding(xtraTreeList1, bindingSource, "TreeListViewBinding",false);

            xtraTreeList1.OptionsBehavior.DragNodes = true;
            xtraTreeList1.OptionsView.ShowIndicator = false;
            xtraTreeList1.OptionsSelection.MultiSelect = true;

          //  xtraTreeList1.OptionsView.ShowRowFooterSummary = true; 

            txtID.DataBindings.Add(new Binding("Text", bindingSource, "ID"));
            txtName.DataBindings.Add(new Binding("Text", bindingSource, "Text"));
            txtCode.DataBindings.Add(new Binding("Text", bindingSource, "PrevID"));
        }

        private void button4_Click(object sender, EventArgs e) {
            MyTreeNodeData data = new MyTreeNodeData();
            data.ID = Guid.NewGuid().ToString();

            data.PrevID = xtraTreeList1.FocusedNode.GetValue("ID").ToString();
 
            bindingSource.Add(data);
        }

        private void button5_Click(object sender, EventArgs e) {

        }

        private void button7_Click(object sender, EventArgs e) {
            xtraTreeList1.ExportToXls("ppppp.xls");
        }

        private void button8_Click(object sender, EventArgs e) {
             

            string name =  "@Name" ;
            string name2 = "@Name" ;

            using (MB.Util.MethodTraceWithTime trace = new MB.Util.MethodTraceWithTime("")) {
                Microsoft.Practices.EnterpriseLibrary.Data.Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
                if (db is Microsoft.Practices.EnterpriseLibrary.Data.Oracle.OracleDatabase) {
                    name = ":" + name.Substring(1, name.Length - 1);
                }

                double d1 = trace.GetExecutedTimes(); 
            }
            using (MB.Util.MethodTraceWithTime trace = new MB.Util.MethodTraceWithTime("")) {
                using (MB.Orm.Persistence.DatabaseConfigurationScope scop = new MB.Orm.Persistence.DatabaseConfigurationScope("SQL SERVER")) {
                    Microsoft.Practices.EnterpriseLibrary.Data.Database db2 = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
                    if (db2 is Microsoft.Practices.EnterpriseLibrary.Data.Oracle.OracleDatabase) {
                        name = ":" + name.Substring(1, name.Length - 1);
                    }
                    else if (db2 is Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase) {
                        name = "@" + name.Substring(1, name.Length - 1);
                    }
                    else {
                    }
                }
                double d2 = trace.GetExecutedTimes(); 
            }
        }

        private void button8_Click_1(object sender, EventArgs e) {
            xtraTreeList1.MoveFirst(); 
        }

        private void button9_Click(object sender, EventArgs e) {
            xtraTreeList1.MoveNext(); 
        }

        private void button10_Click(object sender, EventArgs e) {
            xtraTreeList1.MovePrevVisible(); 
        }

        private void button11_Click(object sender, EventArgs e) {
            xtraTreeList1.MoveLastVisible(); 
        }

        private void button12_Click(object sender, EventArgs e) {

        }

        private void xtraTreeList1_CustomDrawNodeImages(object sender, DevExpress.XtraTreeList.CustomDrawNodeImagesEventArgs e) {
            e.Graphics.DrawImage(imageList1.Images[0], e.StateRect.Location);
          
               
            
        }

        private void xtraTreeList1_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e) {
           // e.Node.SelectImageIndex = 1; 
        }

        private void xtraTreeList1_GetSelectImage(object sender, DevExpress.XtraTreeList.GetSelectImageEventArgs e) {
             // e.NodeImageIndex
        }

        private void xtraTreeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e) {
            //if (e.Node.Selected)
            //    e.NodeImageIndex = 2;
            //else
            //    e.NodeImageIndex = 1;
        }

        private void xtraTreeList1_DragObjectStart(object sender, DevExpress.XtraTreeList.DragObjectStartEventArgs e) {

        }

        private void xtraTreeList1_DragObjectOver(object sender, DevExpress.XtraTreeList.DragObjectOverEventArgs e) {
               
        }

        private void xtraTreeList1_DragDrop(object sender, DragEventArgs e) {
            object node = e.Data.GetData(typeof(DevExpress.XtraTreeList.Columns.TreeListColumn));    
        }

        private void xtraTreeList1_AfterDragNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e) {
           // e.Node;
        }

        private void xtraTreeList1_DragOver(object sender, DragEventArgs e) {

        }

        private void xtraTreeList1_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e) {
            MyTreeNodeData data = xtraTreeList1.GetDataRecordByNode(e.Node) as MyTreeNodeData;  
            if(data.ID == "A5" && e.Column.FieldName == "ID")
                e.Appearance.BackColor = Color.WhiteSmoke; 

               
        }
    }


    public class myTreeNodeFormate : MB.WinBase.Ctls.ITreeViewDataBindingFormate<MyTreeNodeData> {

        #region ITreeViewDataBindingFormate<MyTreeNodeData> 成员

        public void Format(TreeNode tNode, MyTreeNodeData createNodeEntity) {
            if (createNodeEntity.PrevID == null) {
                tNode.ImageIndex = 2;
                tNode.StateImageIndex = 3;
            }
            else {
                tNode.ImageIndex = 1;
                tNode.StateImageIndex = 3;
            }
        }

        #endregion
    }
    /// <summary>
    /// 树型节点描述。
    /// </summary>
    [MB.Util.XmlConfig.ModelXmlConfig]     
   public class MyTreeNodeData {
        private string _ID;
        private string _Text;
        private string _PrevID;
        public MyTreeNodeData() {
        }
        public MyTreeNodeData(string id,string text,string prevID) {
            _ID = id;
            _Text = text;
            _PrevID = prevID;
        }
        [System.Runtime.Serialization.DataMember]
        public string ID {
            get {
                return _ID;
            }
            set {
                _ID = value;
            }
        }
        [System.Runtime.Serialization.DataMember]
        public string Text {
            get {
                return _Text;
            }
            set {
                _Text = value;
            }
        }
        [System.Runtime.Serialization.DataMember]
        public string PrevID {
            get {
                return _PrevID;
            }
            set {
                _PrevID = value;
            }
        }
    }
}
