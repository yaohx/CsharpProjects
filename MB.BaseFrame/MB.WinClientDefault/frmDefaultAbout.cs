using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinClientDefault {
    /// <summary>
    /// 应用系统关于窗口。
    /// </summary>
    public partial class frmDefaultAbout : Form {
        private static readonly string SYSTEM_VERSION = "ApplicationVersion";
        private static readonly string APPLICATION_NAME = "ApplicationName";

        public frmDefaultAbout() {
            InitializeComponent();

            this.Load += new EventHandler(frmDefaultAbout_Load);
        }

        void frmDefaultAbout_Load(object sender, EventArgs e) {
            getIniConfigData();
        }

        private void butSure_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void getIniConfigData() {
            double version = MB.WinClientDefault.VersionAutoUpdate.VersionAutoUpdateHelper.GetClientVersionNumber();
            string name = System.Configuration.ConfigurationManager.AppSettings[APPLICATION_NAME];

            labVersionNo.Text =  version.ToString();
            labApplicationName.Text = name;
            this.Text = "关于 " + name;
        }
    }
}
