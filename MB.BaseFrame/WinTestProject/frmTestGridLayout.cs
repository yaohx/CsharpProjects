using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinTestProject {
    public partial class frmTestGridLayout : Form {
        public frmTestGridLayout() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
           var info = MB.WinBase.LayoutXmlConfigHelper.Instance.GetGridColumnLayoutInfo("testHViewDataConvert", "");
           MessageBox.Show("OK");


           gridControlEx1.BeforeContextMenuClick += new MB.XWinLib.XtraGrid.GridControlExMenuEventHandle(gridControlEx1_BeforeContextMenuClick);
        }
        void gridControlEx1_BeforeContextMenuClick(object sender, MB.XWinLib.XtraGrid.GridControlExMenuEventArg arg) {
            if (arg.MenuType == MB.XWinLib.XtraGrid.XtraContextMenuType.Export) {
                //
                arg.Handled = true;
            }
        }
    }
}
