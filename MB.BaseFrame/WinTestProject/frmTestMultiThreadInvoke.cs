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
    public partial class frmTestMultiThreadInvoke : Form
    {
        private string XML_NAME = "TestOracleTransaction";
        public frmTestMultiThreadInvoke() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            for (int i = 0; i < 1000; i++) {
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(getData));
                t.Start();
            }
        }
        private void getData() {
            var d = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.GetDataSetByXml(XML_NAME, "SelectObject");

            richTextBox1.AppendText("OK \n");
            
            System.Threading.Thread.Sleep(100);
        }
    }
}
