using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace WinTestProject {
    public partial class frmTestOracleODP : Form {
        public frmTestOracleODP() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            using (Oracle.DataAccess.Client.OracleConnection cn = new Oracle.DataAccess.Client.OracleConnection(
                                                        "Data Source=MBDEVDB;User ID=DHHUSER;Password=DHHUSER123")) {

                Oracle.DataAccess.Client.OracleCommand cmd = new Oracle.DataAccess.Client.OracleCommand();
                
                cmd.Connection = cn;
                cmd.CommandText = "INSERT INTO MBFS_SUBJECT(ID,CODE) VALU0ES(99999,'test transaction')";
                cmd.CommandType = CommandType.Text;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
