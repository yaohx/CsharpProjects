using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace WinTestProject {
    public partial class XtraForm1 : MB.WinClientDefault.XtraAbstractEditBaseForm  {
        public XtraForm1() {
            InitializeComponent();

            this.SetExtendToolStripButtonMenu(contextMenuStrip2); 
             
        }

        private void ¿ªÊ¼·Ö¼ðToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("OK"); 
        }
    }
}