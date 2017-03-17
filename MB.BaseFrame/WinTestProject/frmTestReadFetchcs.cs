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
    public partial class frmTestReadFetchcs : Form
    {
        public frmTestReadFetchcs() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            string sql = "select ID,NAME,CODE,REMARK,CREATEDATE from MYTable";
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(System.Int32));
            dt.Columns.Add("NAME", typeof(System.String));
            dt.Columns.Add("CODE", typeof(System.String));
            dt.Columns.Add("REMARK", typeof(System.String));
            dt.Columns.Add("CREATEDATE", typeof(System.DateTime));
            using (var reader = MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteReader(sql)) {
                while (reader.Read()) {
                    var newdr = dt.NewRow();
                    newdr["ID"] = MB.Util.MyConvert.Instance.ToInt(reader["ID"]);
                    newdr["NAME"] =  reader["NAME"].ToString();
                    newdr["CODE"] =  reader["CODE"].ToString();
                    newdr["REMARK"] = reader["REMARK"].ToString();
                    //newdr["CREATEDATE"] = (DateTime)reader["CREATEDATE"];
                    dt.Rows.Add(newdr);
                }
            }

            dataGridView1.DataSource = dt.DefaultView;
        }

        private void button2_Click(object sender, EventArgs e) {
            //
            for (int i = 110; i < 10000; i++) {
                MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery("TestOracleTransaction", "AddObject", i + 4, "AAA" + i, "CODE" + i);
            }
        }
    }
}
