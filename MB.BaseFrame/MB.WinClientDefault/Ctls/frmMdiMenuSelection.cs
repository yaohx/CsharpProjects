using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinClientDefault.Ctls {
    public partial class frmMdiMenuSelection : Form {
        private Dictionary<string, string> _MATCHED_MENUS;
        private string _SELECT_MODULE_CODE;

        /// <summary>
        /// 最终选择的模块编码
        /// </summary>
        public string SELECT_MODULE_CODE {
            get { return _SELECT_MODULE_CODE; }
        }
        public frmMdiMenuSelection(Dictionary<string, string> matchedMenus) {
            InitializeComponent();
            _MATCHED_MENUS = matchedMenus;
            flowLayoutPanel.AutoScroll = true;
        }

        private void frmMdiMenuSelection_Load(object sender, EventArgs e) {
            this.flowLayoutPanel.Controls.Clear();
            if (_MATCHED_MENUS != null) {
                foreach (string key in _MATCHED_MENUS.Keys) {
                    LinkLabel lnkMenu = new LinkLabel();
                    lnkMenu.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
                    lnkMenu.TextAlign = ContentAlignment.MiddleLeft;
                    lnkMenu.Width = 105;

                    lnkMenu.Name = "lnkRegion" + key;
                    lnkMenu.Text = _MATCHED_MENUS[key];
                    lnkMenu.Tag = key;

                    lnkMenu.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkMenu_LinkClicked);
                    this.flowLayoutPanel.Controls.Add(lnkMenu);

                }

            }
        }

        void lnkMenu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            LinkLabel lnk = (LinkLabel)sender;
            _SELECT_MODULE_CODE = lnk.Tag as string;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            
        }
    }
}
