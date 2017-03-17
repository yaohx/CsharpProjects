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
    public partial class frmTestMySql : Form
    {
        private readonly string XML_FILE_NAME = "TestMySqlDB";
        public frmTestMySql() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            var datas = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.GetDataSetByXml(XML_FILE_NAME, "SelectObject");
            dataGridView1.DataSource = datas.Tables[0].DefaultView;

        

        }

        private void butAddNew_Click(object sender, EventArgs e) {
          //  int id = MB.Orm.Persistence.EntityIdentityHelper.NewInstance.GetEntityIdentity("myTable");
            object[] vals = new object[] { "AAA8888888888", "BBB888888888", DateTime.Now };

            int re = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery(XML_FILE_NAME, "AddObject", vals);
            MessageBox.Show(string.Format("数据增加成功,影响的行数有：{0}", re.ToString()));
        }

        private void butUpdate_Click(object sender, EventArgs e) {

            object[] vals = new object[] { 3, "修改的数据00000", "修改的数据00000", DateTime.Now };
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope( System.Transactions.TransactionScopeOption.RequiresNew)) {
                int re = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery(XML_FILE_NAME, "UpdateObject", vals);
               // MessageBox.Show(string.Format("数据增加成功,影响的行数有：{0}", re.ToString()));
               //scope.Complete();
              throw new Exception();
            }
        }

        private void butDelete_Click(object sender, EventArgs e) {
            int id = 5;
            int re = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery(XML_FILE_NAME, "DeleteObject", id);
            MessageBox.Show(string.Format("数据删除成功,影响的行数有：{0}", re.ToString()));
        }
    }
}
