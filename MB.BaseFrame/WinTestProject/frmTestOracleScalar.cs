using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.Orm.Persistence;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MB.RuleBase.Common;

namespace WinTestProject {
    public partial class frmTestOracleScalar : Form {
        public frmTestOracleScalar() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            string m_objName = "MbfsFuc";

            Database db2 = DatabaseHelper.CreateDatabase();
            var perstIns1 = PersistenceManagerHelper.NewInstance;
            var dbExcIns1 = DatabaseExecuteHelper.NewInstance;
            var cmd1 = perstIns1.CreateDbCommandByXml(db2, m_objName, "Import_CheckSalesNoExists", new object[] { "00063866", "A00024S001" })[0];
          
            object reSalesNoExists0 = dbExcIns1.ExecuteScalar(db2, cmd1);

            var cmd2 = perstIns1.CreateDbCommandByXml(db2, m_objName, "Import_CheckSalesNoExists", new object[] { "99999999", "A00024S001" })[0];
            object reSalesNoExists1 = dbExcIns1.ExecuteScalar(db2, cmd2);


            IDataReader reader = db2.ExecuteReader(cmd2);

            DataTable dt = reader.GetSchemaTable();

            DataRow[] drs = dt.Select();

        }
    }
}
