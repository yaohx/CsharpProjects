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
    public partial class frmTestSqlite : Form
    {
        private readonly string XML_FILE_NAME = "TestSqliteDb";

        public frmTestSqlite() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            var datas = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.GetDataSetByXml(XML_FILE_NAME, "SelectObject");
            dataGridView1.DataSource = datas.Tables[0].DefaultView;

        }

        private void button2_Click(object sender, EventArgs e) {
            //string sql = "INSERT INTO myTable(ID,Name,Code,CreateDate) VALUES(:ID,:Name,:Code,:CreateDate)";t
            int id = MB.Orm.Persistence.EntityIdentityHelper.NewInstance.GetEntityIdentity("myTable");
            object[] vals = new object[]{id,"AAA","BBB",DateTime.Now};

            int re = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery(XML_FILE_NAME, "AddObject", vals);
            MessageBox.Show(string.Format("数据增加成功,影响的行数有：{0}", re.ToString()));
        }

        private void button3_Click(object sender, EventArgs e) {
            int id = 2;
            int re = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery(XML_FILE_NAME, "DeleteObject", id);
            MessageBox.Show(string.Format("数据删除成功,影响的行数有：{0}", re.ToString()));
        }

        private void button4_Click(object sender, EventArgs e) {
          
            object[] vals = new object[] { 1, "修改的数据", "修改的数据BBB", DateTime.Now };

            int re = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery(XML_FILE_NAME, "UpdateObject", vals);
            MessageBox.Show(string.Format("数据增加成功,影响的行数有：{0}", re.ToString()));


            Oracle.DataAccess.Client.OracleConnection cn = new Oracle.DataAccess.Client.OracleConnection();
            //ocm.Parameters.Add("", Oracle.DataAccess.Client.OracleDbType.RefCursor,12,val, ParameterDirection.)

        }
    }
}
