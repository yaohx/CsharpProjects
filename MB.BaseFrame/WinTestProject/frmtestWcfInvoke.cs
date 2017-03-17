using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinTestProject
{
    public partial class frmtestWcfInvoke : Form
    {
        public frmtestWcfInvoke() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {

            MB.Util.MyNetworkCredential.CurrentSelectedServerInfo = new MB.Util.Model.ServerConfigInfo("");
            var t = MB.WcfClient.WcfClientFactory.CreateWcfClient<frmtestWcfInvoke>("", new MB.Util.Model.ServerConfigInfo("", "mboos.credential"));
        }
    }
}
