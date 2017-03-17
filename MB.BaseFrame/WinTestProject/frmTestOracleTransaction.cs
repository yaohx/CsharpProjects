using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace WinTestProject
{
    public partial class frmTestOracleTransaction : Form
    {
        private string XML_NAME = "TestOracleTransaction";

        public frmTestOracleTransaction() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            var data = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.GetDataSetByXml(XML_NAME, "SelectObject");
            dataGridView1.DataSource = data.Tables[0].DefaultView;

            //MB.RuleBase.BulkCopy.SimulatedOracleHelper h = new MB.RuleBase.BulkCopy.SimulatedOracleHelper();
            //var cn = h.CreateOracleConnection("CN").BeginTransaction();

            

        }

        private void button2_Click(object sender, EventArgs e) {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            var cn = new MB.RuleBase.BulkCopy.SimulatedOracleHelper().CreateOracleConnection(MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateOracleCnString(db.ConnectionString));
            cn.Open();
            var tran = cn.BeginTransaction();
            //using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope()) {
                string sqlName = "AddObject";
                object[] parValues = new object[] { 91, "名称", "编码测试" };
                MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery(tran,XML_NAME,sqlName, parValues);

                object[] parValues2 = new object[] { 92, "名称", "编码测试" };
                MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery(tran,XML_NAME, sqlName, parValues2);

              //  scope.Complete();
                tran.Commit();
                cn.Close();
          //  }

    
        }

        private void button3_Click(object sender, EventArgs e) {
            string cn = @"Data Source=(DESCRIPTION =
    (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.149.114)(PORT = 1521))
    (CONNECT_DATA =
      (SERVER = DEDICATED)
      (SERVICE_NAME = MBTESTDB)
    )
  );Persist Security Info=True;User ID=MTSBWNET;Password=MTSBWTST;";
            try {
                Oracle.DataAccess.Client.OracleConnection ocn = new Oracle.DataAccess.Client.OracleConnection(cn);
                //
               // System.Data.OracleClient.OracleConnection ocn = new System.Data.OracleClient.OracleConnection(cn);
                ocn.Open();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void button4_Click(object sender, EventArgs e) {
           //string ORACLE_FACTORY_PROVIDER = "Oracle.DataAccess.Client.OracleClientFactory";
           //Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
           //if (db.DbProviderFactory.GetType().FullName == "Oracle.DataAccess.Client.OracleClientFactory")
           //    MessageBox.Show("OK");

            List<string> vals = new List<string>();
            vals.Add("AAAAAAAAAAAA");
            vals.Add("BBBBBBBBBBBB");
            vals.Add("CCCCCCCCCCCC");
            vals.Add("DDDDDDDDDDDD");
           // using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope()) {
                
                using (var dbConnection = new MB.RuleBase.BulkCopy.SimulatedOracleHelper().CreateOracleConnection()) {
                    dbConnection.Open();
                    using (var tran = dbConnection.BeginTransaction()) {
                        using (MB.RuleBase.BulkCopy.IDbBulkExecute bulk = MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateDbBulkExecute(tran)) {
                            bulk.WriteToServer("TestOracleTransaction", "InsertGlobalTemporaryl", vals);
                            // MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery("TestOracleTransaction", "InsertGlobalTemporaryl");
                            var data = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.GetDataSetByXml(tran, "TestOracleTransaction", "GetFromGlobalTemporaryl");
                            MessageBox.Show(data.Tables[0].Rows.Count.ToString());
                        }
                        tran.Commit();
                    }
                }
               
          //  }
        }

        private void button5_Click(object sender, EventArgs e) {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope()) {
                string code = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteScalar<string>("TestOracleTransaction", "SelectDataForUpdate", 534);

                MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery("TestOracleTransaction", "UpdateMyTableCode", 534, "MytestData");
                //scope.Complete();
            }
        }
    }
}
