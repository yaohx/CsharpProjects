using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.Tools.LogView {
    public partial class frmViewIpAndDate : Form {
        public frmViewIpAndDate(List<UserDateInfo> datas) {
            InitializeComponent();

            gridControl1.DataSource = datas; 
        }

        private void butExport_Click(object sender, EventArgs e) {
           
        }
    }
}
