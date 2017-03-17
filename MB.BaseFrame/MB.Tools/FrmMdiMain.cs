using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.Tools.LogView;

namespace MB.Tools {
    public partial class FrmMdiMain : Form {
        public FrmMdiMain() {
            InitializeComponent();
        }

        private void tsmDatabaseLink_Click(object sender, EventArgs e) {

        }

        private void tsmCodeGenerate_Click(object sender, EventArgs e) {
            frmCodeGenerate frm = new frmCodeGenerate();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tlbCodeGenerate_Click(object sender, EventArgs e) {
            tsmCodeGenerate_Click(null, null);
        }
        
        private void 水平ToolStripMenuItem_Click(object sender, EventArgs e) {
            this.LayoutMdi(MdiLayout.TileVertical);  
        }

        private void tsbLogView_Click(object sender, EventArgs e) {
            MB.Tools.LogView.FrmChoosePath frm = new MB.Tools.LogView.FrmChoosePath(this);
            frm.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            frmLogAnalyze frm = new frmLogAnalyze();
            frm.ShowDialog(); 
        }
    }
}
