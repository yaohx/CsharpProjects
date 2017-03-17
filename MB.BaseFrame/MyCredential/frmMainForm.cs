using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyCredential {
    public partial class frmMainForm : Form {
        public frmMainForm() {
            InitializeComponent();
        }

        private void butCreate_Click(object sender, EventArgs e) {
            CreateCredential frm = new CreateCredential();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
