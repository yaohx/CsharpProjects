using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinTestProject {
    public partial class frmTestXmlSql : Form {
        public frmTestXmlSql() {
            InitializeComponent();
        }

        private void frmTestXmlSql_Load(object sender, EventArgs e) {
         
        }

        private void button1_Click(object sender, EventArgs e) {
            DataSet dsData = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.GetDataSetByXml("AgentFlowDataProvider",
                                                                        "GetMbfsRestrictData22", 5, "A00030", "A00030S001", "25600248");


            // "GetMbfsRestrictData", 5, "A00030", 5, "A00030", 5, "A00030", "A00030S001", "25600248");


        }
    }
}
