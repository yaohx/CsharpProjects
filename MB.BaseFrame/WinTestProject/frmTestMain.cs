using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Practices.EnterpriseLibrary.Data;
using MB.Util.Model;
using MB.WinClientDefault.QueryFilter;
using System.Runtime.Serialization;
namespace WinTestProject {
    public partial class frmTestMain : Form {
        public frmTestMain() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            //MB.Orm.DbSql.SqlString    sql = new MB.Orm.DbSql.SqlString();
            //MB.Orm.CacheProxy.CacheSql("AA",sql); 
            //if (MB.Orm.CacheProxy.ContainsSql("AA"))
            //    MessageBox.Show("OK"); 
        }

        private void button2_Click(object sender, EventArgs e) {
            //System.Data.OracleClient.OracleConnection cn = new System.Data.OracleClient.OracleConnection();
            //cn.ConnectionString = "Data Source=ORCL;Persist Security Info=True;User ID=system;Password=123456;Unicode=True";
            //cn.Open();
            //System.Data.OracleClient.OracleCommand cmd = new System.Data.OracleClient.OracleCommand();
            //cmd.CommandText = "SELECT ID,Name,Address,CreateDate,CreateUser FROM TestRule   WHERE  0=0;";
            //cmd.Connection = cn;
            //var ds = cmd.ExecuteReader(); 

            

            Database db2 = MB.Orm.Persistence.DatabaseHelper.CreateDatabase(); 

            System.Data.Common.DbCommand   cmd2 = db2.GetSqlStringCommand("SELECT ID,Name,Address,CreateDate,CreateUser FROM TestRule   WHERE  0=0");
            var ds2= db2.ExecuteReader(cmd2); 
        }

        private void button3_Click(object sender, EventArgs e) {
            frmTestTreeViewDataBinding frm = new frmTestTreeViewDataBinding();
            frm.ShowDialog();  
        }

        private void button4_Click(object sender, EventArgs e) {
            testXtraGrid frm = new testXtraGrid();
            frm.ShowDialog(); 
        }

        private void button5_Click(object sender, EventArgs e) {
            //MB.WinClientDefault.RibbonMdiMainForm frm = new MB.WinClientDefault.RibbonMdiMainForm();
            //frm.Show();
        }

        private void button6_Click(object sender, EventArgs e) {
            using (MB.Util.MethodTraceWithTime trace = new MB.Util.MethodTraceWithTime("PPP")) {
                testHViewDataConvert frm = new testHViewDataConvert();
                frm.ShowDialog();
            }

          
        }

        private void button7_Click(object sender, EventArgs e) {
            frmTestXmlSql frm = new frmTestXmlSql();
            frm.ShowDialog(); 
        }

        private void button8_Click(object sender, EventArgs e) {
            DataTable dt = new DataTable();
            Type ty = System.Type.GetType("System.DateTime?");
            dt.Columns.Add("",ty) ;

            
        }

        private void button9_Click(object sender, EventArgs e) {
            frmTestDiyReportPrint frm = new frmTestDiyReportPrint();
            frm.ShowDialog(); 
        }

        private void button10_Click(object sender, EventArgs e) {
            frmTestDataSerializer frm = new frmTestDataSerializer();
            frm.ShowDialog();  
        }

        private void button11_Click(object sender, EventArgs e) {
            frmTestOracleODP frm = new frmTestOracleODP();
            frm.ShowDialog(); 
        }

        private void button12_Click(object sender, EventArgs e) {
            frmTestAsynCall frm = new frmTestAsynCall();
            frm.ShowDialog(); 
        }

        private void button13_Click(object sender, EventArgs e) {
            frmtesthastable frm = new frmtesthastable();
            frm.ShowDialog(); 
        }

        private void button14_Click(object sender, EventArgs e) {
            frmTestImageIcoEdit frm = new frmTestImageIcoEdit();
            frm.ShowDialog(); 
        }

        private void button15_Click(object sender, EventArgs e) {
            frmTestReflectionEmit frm = new frmTestReflectionEmit();
            frm.ShowDialog(); 
        }

        private void button16_Click(object sender, EventArgs e) {
            frmQuickConfigModuleInvoke frm = new frmQuickConfigModuleInvoke();
            frm.ShowDialog();  
        }

        private void button18_Click(object sender, EventArgs e) {
            frmTestDatabase frm = new frmTestDatabase();
            frm.ShowDialog(); 
        }

        private void button19_Click(object sender, EventArgs e) {
            XtraForm1 frm = new XtraForm1();
            frm.ShowDialog();  
        }

        private void button20_Click(object sender, EventArgs e) {
            frmTestEditColor frm = new frmTestEditColor();
            frm.ShowDialog(); 
        }

        private void button21_Click(object sender, EventArgs e) {
            testEmit frm = new testEmit();
            frm.ShowDialog(); 
        }

        private void button22_Click(object sender, EventArgs e) {
            frmTestBulkInsert frm = new frmTestBulkInsert();
            frm.ShowDialog(); 
        }

        private void button23_Click(object sender, EventArgs e) {
            frmTestOracleScalar frm = new frmTestOracleScalar();
            frm.ShowDialog(); 
        }

        private void button24_Click(object sender, EventArgs e) {
            frmTestImageFiles frm = new frmTestImageFiles();
            frm.ShowDialog(); 
        }

        private void button25_Click(object sender, EventArgs e) {
            frmTestXtraGridGroup frm = new frmTestXtraGridGroup();
            frm.ShowDialog(); 
        }

        private void button26_Click(object sender, EventArgs e) {
            frmTestGridLayout frm = new frmTestGridLayout();
            frm.ShowDialog();
        }

        private void button27_Click(object sender, EventArgs e) {
            frmTestDbPictureBox frm = new frmTestDbPictureBox();
            frm.ShowDialog();
        }

        private void button28_Click(object sender, EventArgs e) {
            frmTestSqlite frm = new frmTestSqlite();
            frm.ShowDialog();
        }

        private void button29_Click(object sender, EventArgs e) {
            frmTestMySql frm = new frmTestMySql();
            frm.ShowDialog();
        }

        private void button30_Click(object sender, EventArgs e) {
            frmTestOracleTransaction frm = new frmTestOracleTransaction();
            frm.ShowDialog();
        }

        private void button31_Click(object sender, EventArgs e) {
            frmTestMultiThreadInvoke frm = new frmTestMultiThreadInvoke();
            frm.ShowDialog();
        }

        private void button32_Click(object sender, EventArgs e) {
            frmTestExcelImport frm = new frmTestExcelImport();
            frm.ShowDialog();
        }

        private void button33_Click(object sender, EventArgs e) {
            frmtestWcfInvoke frm = new frmtestWcfInvoke();
            frm.ShowDialog();
        }

        private void button34_Click(object sender, EventArgs e) {
            frmTestDateFilter frm = new frmTestDateFilter();
            frm.ShowDialog();
        }

        private void button17_Click(object sender, EventArgs e) {
            var gridViewLayoutInfo = MB.WinBase.LayoutXmlConfigHelper.Instance.GetGridColumnLayoutInfo("TrOnwayFeedbackDtl", string.Empty);
            MessageBox.Show(gridViewLayoutInfo.Name);
        }

        private void button35_Click(object sender, EventArgs e) {
            frmTestReadFetchcs frm = new frmTestReadFetchcs();
            frm.ShowDialog();
        }

        private void button36_Click(object sender, EventArgs e) {
            frmTestAopInvoke frm = new frmTestAopInvoke();
            frm.ShowDialog();
        }

        private void btnDynamicGroupSetting_Click(object sender, EventArgs e) {
            //List<DataAreaField> dataAreaList = new List<DataAreaField>();
            //DataAreaField dataArea = new DataAreaField();
            //dataArea.Name = "Condition1";
            //dataArea.Description = "条件1";
            //dataAreaList.Add(dataArea);
            //DynamicGroupSetting setting = new DynamicGroupSetting();
            //setting.DataAreaFields.Add("Entity1", dataAreaList);

            //DynamicGroupConditionSetting settingFrm = new DynamicGroupConditionSetting(setting);
            //settingFrm.Show();

            //FrmQueryFilterInput frm = new FrmQueryFilterInput();
            //frm.Show();
        }

        private void btnTestSummaryWithRule_Click(object sender, EventArgs e) {
            List<TestFrmSummary> sources = new List<TestFrmSummary>();
            TestFrmSummary source11 = new TestFrmSummary() { ID = 1, CODE = "C1234567", DOC_STATE = 1, NUMBER = 5, PRICE = 10 };
            TestFrmSummary source11Ext = new TestFrmSummary() { ID = 6, CODE = "C123456", DOC_STATE = 1, NUMBER = 100, PRICE = 100 };
            TestFrmSummary source12 = new TestFrmSummary() { ID = 2, CODE = "C1", DOC_STATE = 2, NUMBER = 6, PRICE = 20 };
            TestFrmSummary source13 = new TestFrmSummary() { ID = 3, CODE = "C1", DOC_STATE = 3, NUMBER = 7, PRICE = 30 };
            TestFrmSummary source21 = new TestFrmSummary() { ID = 4, CODE = "C2", DOC_STATE = 1, NUMBER = 5, PRICE = 10 };
            TestFrmSummary source22 = new TestFrmSummary() { ID = 5, CODE = "C2", DOC_STATE = 2, NUMBER = 6, PRICE = 20 };
            sources.Add(source11); sources.Add(source12); sources.Add(source13);
            sources.Add(source21); sources.Add(source22);
            sources.Add(source11Ext);
            TestRule rule = new TestRule();
            MB.WinClientDefault.Common.FrmSummay frm = new MB.WinClientDefault.Common.FrmSummay(rule, sources);
            //MB.WinClientDefault.Common.FrmSummay frm = new MB.WinClientDefault.Common.FrmSummay("FrmSummaryTest", sources);
            frm.ShowDialog();
        }

        private void button37_Click(object sender, EventArgs e) {
            testBandGridView frm = new testBandGridView();
            frm.ShowDialog();
        }

        private void btnExcelView_Click(object sender, EventArgs e) {
            frmExcelViewTest frm = new frmExcelViewTest();
            frm.ShowDialog();
        }

    }

    [DataContract]
    public class TestFrmSummary {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string CODE { get; set; }
        [DataMember]
        public int DOC_STATE { get; set; }
        [DataMember]
        public int PRICE { get; set; }
        [DataMember]
        public int NUMBER { get; set; }
    }

    [MB.WinBase.Atts.RuleClientLayout("FrmSummaryTest")]
    public class TestRule : MB.WinBase.AbstractClientRuleQuery {
    }


    public class MySpinEdit : DevExpress.XtraEditors.SpinEdit {
      
        protected override void OnSpin(DevExpress.XtraEditors.Controls.SpinEventArgs e) {
           // e.Handled = true;
            base.OnSpin(e);
        }

    }
}
