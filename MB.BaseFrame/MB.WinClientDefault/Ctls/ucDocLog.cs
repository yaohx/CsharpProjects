using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinClientDefault.Ctls {
    [ToolboxItem(true)]
    public partial class ucDocLog : UserControl {
        public ucDocLog() {
            InitializeComponent();

            this.Load += new EventHandler(ucDocLog_Load);
        }

        void ucDocLog_Load(object sender, EventArgs e) {
            //if (this.Parent != null)
            //    this.BackColor = this.Parent.BackColor; 
        }
        private string _LogText;
        [Description("描述信息")]
        public string LogText {
            get {
                return labMessage.Text;
            }
            set {
                labMessage.Text = value;
            }
        }
        [Description("描述信息字体")]
        public Font LogTextFont {
            get {
                return labMessage.Font; 
            }
            set {
                labMessage.Font = value;
            }
        }
    }
}
