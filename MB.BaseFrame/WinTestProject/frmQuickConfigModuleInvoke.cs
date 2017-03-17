using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinTestProject {
    public partial class frmQuickConfigModuleInvoke : Form {
        public frmQuickConfigModuleInvoke() {
            InitializeComponent();
        }

        private void butSelectDll_Click(object sender, EventArgs e) {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog(); ;
            fileDialog.Filter = "应用组件 (*.dll)|*.dll";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                cobDllFileName.Text = fileDialog.FileName;
                loadDllClassType(fileDialog.FileName); 
            }

        }
        //加载配件中的所有类型
        private void loadDllClassType(string dllFullName) {
            trvClassType.Nodes.Clear();
            TreeNode rootNode = new TreeNode(dllFullName);
            rootNode.ImageIndex = 1;
            rootNode.SelectedImageIndex = 0;
            trvClassType.Nodes.Add(rootNode); 
            System.Reflection.Assembly dll = System.Reflection.Assembly.LoadFile(dllFullName);
            Type[] tps = dll.GetTypes();
            foreach (Type t in tps) {
                if (t.GetInterface(typeof(MB.WinBase.IFace.IClientRuleConfig).Name)!=null) {
                    TreeNode childNode = new TreeNode(t.Name);
                    childNode.ImageIndex = 0;
                    childNode.SelectedImageIndex = 0;
                    rootNode.Nodes.Add(childNode);
                }
            }
            rootNode.Expand(); 
        }
    }
}
