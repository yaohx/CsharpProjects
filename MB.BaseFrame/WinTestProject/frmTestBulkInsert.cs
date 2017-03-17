using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Reflection;
using System.Transactions;

namespace WinTestProject {
    public partial class frmTestBulkInsert : Form {
        public frmTestBulkInsert() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            int count = int.Parse(textBox1.Text);
            using (MB.Util.MethodTraceWithTime t = new MB.Util.MethodTraceWithTime("BLUCK", count)) {
                int idC = MB.Orm.Persistence.EntityIdentityHelper.NewInstance.GetEntityIdentity("MBFS_FUC_DTL", count);
                IList lstData = null;
                if (chkDataTable.Checked) {
                    DataTable dt = getDataTable(count, idC);
                    lstData = dt.Select();
                }
                else
                    lstData = getListData(count, idC);

                using (MB.RuleBase.BulkCopy.IDbBulkExecute bulk = MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateDbBulkExecute()) {
                    bulk.WriteToServer("MbfsFucDtl", "AddObject", lstData);
                }

                MessageBox.Show(string.Format("总共执行的时间有{0} 毫秒", t.GetExecutedTimes()));
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            int count = int.Parse(textBox1.Text);
            using (MB.Orm.Persistence.DatabaseConfigurationScope scope = new MB.Orm.Persistence.DatabaseConfigurationScope("SQL SERVER")) {
                using (MB.Util.MethodTraceWithTime t = new MB.Util.MethodTraceWithTime("BLUCK", count)) {
                    IList lstData = null;
                    if (chkDataTable.Checked) {
                        DataTable dt = getDataTable(count, 1);
                        lstData = dt.Select();
                    }
                    else
                        lstData = getListData(count, 1);

                    using (MB.RuleBase.BulkCopy.IDbBulkExecute bulk = MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateDbBulkExecute()) {
                        bulk.WriteToServer("MbfsFucDtl", "AddObject", lstData);
                    }

                    MessageBox.Show(string.Format("总共执行的时间有{0} 毫秒", t.GetExecutedTimes()));
                }
            }

        }

        private void button4_Click(object sender, EventArgs e) {
            int count = int.Parse(textBox1.Text);
            using (MB.Orm.Persistence.DatabaseConfigurationScope scope = new MB.Orm.Persistence.DatabaseConfigurationScope("SQL SERVER")) {
                Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
                using (MB.Util.MethodTraceWithTime t = new MB.Util.MethodTraceWithTime("BLUCK", count)) {
                    using (SqlConnection cn = new SqlConnection(db.ConnectionString)) {
                        cn.Open();
                        using (SqlTransaction tran = cn.BeginTransaction()) {
                            try {

                                IList lstData = null;
                                if (chkDataTable.Checked) {
                                    DataTable dt = getDataTable(count, 1);
                                    lstData = dt.Select();
                                }
                                else
                                    lstData = getListData(count, 1);
                                using (MB.RuleBase.BulkCopy.IDbBulkExecute bulk = MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateDbBulkExecute(tran)) {
                                    bulk.WriteToServer("MbfsFucDtl", "AddObject", lstData);
                                }
                                tran.Commit();
                                // throw new Exception();
                            }
                            catch {
                                tran.Rollback();
                            }
                        }
                    }
                    MessageBox.Show(string.Format("总共执行的时间有{0} 毫秒", t.GetExecutedTimes()));
                }
            }
        }
        private DataTable getDataTable(int count, int index) {
            DataTable dt = new DataTable();
            for (int i = 0; i < count; i++) {
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("MBFS_FUC_ID", typeof(int));
                dt.Columns.Add("SHOP_ID", typeof(string));
                dt.Columns.Add("BATCH_DATE", typeof(DateTime));
                dt.Columns.Add("PROD_ID", typeof(string));
                dt.Columns.Add("DISC_RATE", typeof(decimal));
                dt.Columns.Add("UNIT_PRICE", typeof(decimal));
                dt.Columns.Add("CNTT_QTY", typeof(decimal));
                dt.Columns.Add("VALID_CNTT_QTY", typeof(decimal));
                dt.Columns.Add("REMARK", typeof(string));

                dt.Rows.Add(new object[] { index++, 982, "A01339S100", System.DateTime.Now, "22114444144", 100, 129, 2, 2, null });
            }
            return dt;
        }

        private List<MbfsFucDtlInfo> getListData(int count, int index) {
            List<MbfsFucDtlInfo> lstData = new List<MbfsFucDtlInfo>();
            for (int i = 0; i < count; i++) {
                MbfsFucDtlInfo info = new MbfsFucDtlInfo();
                info.ID = index++;
                info.MBFS_FUC_ID = 982;
                info.PROD_ID = "22114444144";
                info.SHOP_ID = "A01339S100";
                info.BATCH_DATE = DateTime.Parse(System.DateTime.Now.ToString("yyyy-MM-dd"));
                info.UNIT_PRICE = 129;
                info.DISC_RATE = 100;
                info.CNTT_QTY = 2;
                lstData.Add(info);
            }
            return lstData;
        }

        private void frmTestBulkInsert_Load(object sender, EventArgs e) {

        }

        private void button3_Click(object sender, EventArgs e) {
            using (MB.Orm.Persistence.DatabaseConfigurationScope scope = new MB.Orm.Persistence.DatabaseConfigurationScope("SQL SERVER")) {
                for (int i = 0; i < 10000; i++) {
                    Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
                    using (SqlConnection cn = new SqlConnection(db.ConnectionString)) {
                        cn.Open();
                    }

                }
            }
        }

        private void button5_Click(object sender, EventArgs e) {
            Assembly DLL = Assembly.LoadWithPartialName("Oracle.DataAccess");
            var dbType = DLL.GetType("Oracle.DataAccess.Client.OracleDbType");
            var field = dbType.GetField("Decimal");
            var val = field.GetValue(null);

           Oracle.DataAccess.Client.OracleDbType dv = (Oracle.DataAccess.Client.OracleDbType)val;

            MessageBox.Show("OK");
        }

        private void button6_Click(object sender, EventArgs e) {
            int count = int.Parse(textBox1.Text);
            List<MY_TABLE_MAINInfo> myEntitys = new List<MY_TABLE_MAINInfo>();
            int beginID = 1;
            for (int i = 0; i < 1000; i++) {
                myEntitys.Add(new MY_TABLE_MAINInfo() { ID = beginID++, NAME = "CCCC", CODE = "BB", ADDRESS = "PPP" });
            }
            using (MB.Orm.Persistence.DatabaseConfigurationScope scope = new MB.Orm.Persistence.DatabaseConfigurationScope("MB.MBFS")) {
                Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
                using (MB.Util.MethodTraceWithTime t = new MB.Util.MethodTraceWithTime("BLUCK", count)) {
                    //using (SqlConnection cn = new SqlConnection(db.ConnectionString)) {
                    //    cn.Open();
                    //    using (SqlTransaction tran = cn.BeginTransaction()) {
                    //        try {
                    using (TransactionScope scopeTran = new TransactionScope()) {
                        using (MB.RuleBase.BulkCopy.IDbBulkExecute bulk = MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateDbBulkExecute()) {
                            bulk.WriteToServer("MY_TABLE_MAIN", "UpdateObject", myEntitys);
                        }
                       // string data = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteScalar<string>("MY_TABLE_MAIN", "SelectObject", 10);

                        scopeTran.Complete();
                       // throw new Exception("ERROR");

                    }
                        //        tran.Commit();
                        //        // throw new Exception();
                        //    }
                        //    catch {
                        //        tran.Rollback();
                        //    }
                        //}
                                MessageBox.Show(string.Format("总共执行的时间有{0} 毫秒", t.GetExecutedTimes()));
                    }
                  
                }
          }

        private void button7_Click(object sender, EventArgs e) {
            //AddFucwTmpTable
            MB.RuleBase.BulkCopy.OracleBulkExecute o = new MB.RuleBase.BulkCopy.OracleBulkExecute();
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            List<myTestInfo> lst = new List<myTestInfo>();
            myTestInfo info = new myTestInfo();
            info.TableName = "MyTestTable";
            info.VendeeID = "";
            info.ProdID = "";
            info.BatchDate = System.DateTime.Now;
            info.WareDate = System.DateTime.Now;
            info.WareID = "MyID";
            info.WareQty = 10;
            info.FucLevQty = 20;
            info.PrLevQty = 30;
            lst.Add(info);
            var cmd = o.createDbCommandByXml(db, "Fuc", "AddFucwTmpTable", lst);
            MessageBox.Show(cmd.CommandText);
            
        }

        private void button8_Click(object sender, EventArgs e) {
            List<MyTable> lst = new List<MyTable>();
            lst.Add(new MyTable() { ID = 1, NAME = "mmmm", CODE = "PP" });
            lst.Add(new MyTable() { ID = 2, NAME = "mmmmmmmm", CODE = "PP2" });
            using (var bulk = MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateDbBulkExecute()) {
                bulk.WriteToServer("TestOracleTransaction", "UpdateObject", lst);
            }
        }
 

    }
    public class MyTable
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
    }
    public class myTestInfo
    {
        public string TableName { get; set; }
        public string VendeeID { get; set; }
        public string ProdID { get; set; }
        public DateTime BatchDate { get; set; }
        public DateTime WareDate { get; set; }
        public string WareID { get; set; }
        public int WareQty { get; set; }
        public int FucLevQty { get; set; }
        public int PrLevQty { get; set; }


    }
    // 需要引用的命名空间  using System.Runtime.Serialization;  

    /// <summary> 
    /// 文件生成时间 2009-09-25 11:03 
    /// </summary> 
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("MBFS_FUC_DTL", "MbfsFucDtl", new string[] { "ID" })]
    [KnownType(typeof(MbfsFucDtlInfo))]
    public class MbfsFucDtlInfo : MB.Orm.Common.BaseModel {

        public MbfsFucDtlInfo() {

        }
        private int _ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ID", System.Data.DbType.Int32)]
        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }
        private int _MBFS_FUC_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MBFS_FUC_ID", System.Data.DbType.Int32)]
        public int MBFS_FUC_ID {
            get { return _MBFS_FUC_ID; }
            set { _MBFS_FUC_ID = value; }
        }
        private DateTime _BATCH_DATE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BATCH_DATE", System.Data.DbType.DateTime)]
        public DateTime BATCH_DATE {
            get { return _BATCH_DATE; }
            set { _BATCH_DATE = value; }
        }
        private string _PROD_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROD_ID", System.Data.DbType.String)]
        public string PROD_ID {
            get { return _PROD_ID; }
            set { _PROD_ID = value; }
        }
        private decimal _UNIT_PRICE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UNIT_PRICE", System.Data.DbType.Decimal)]
        public decimal UNIT_PRICE {
            get { return _UNIT_PRICE; }
            set { _UNIT_PRICE = value; }
        }
        private decimal _DISC_RATE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DISC_RATE", System.Data.DbType.Decimal)]
        public decimal DISC_RATE {
            get { return _DISC_RATE; }
            set { _DISC_RATE = value; }
        }
        private decimal _CNTT_QTY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CNTT_QTY", System.Data.DbType.Decimal)]
        public decimal CNTT_QTY {
            get { return _CNTT_QTY; }
            set { _CNTT_QTY = value; }
        }
        private decimal _VALID_CNTT_QTY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VALID_CNTT_QTY", System.Data.DbType.Decimal)]
        public decimal VALID_CNTT_QTY {
            get { return _VALID_CNTT_QTY; }
            set { _VALID_CNTT_QTY = value; }
        }
        private string _REMARK;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("REMARK", System.Data.DbType.String)]
        public string REMARK {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        private string _SHOP_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SHOP_ID", System.Data.DbType.String)]
        public string SHOP_ID {
            get { return _SHOP_ID; }
            set { _SHOP_ID = value; }
        }
        private string _SHOP_NAME;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SHOP_NAME", System.Data.DbType.String)]
        public string SHOP_NAME {
            get { return _SHOP_NAME; }
            set { _SHOP_NAME = value; }
        }
        private string _STYLE_NAME;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STYLE_NAME", System.Data.DbType.String)]
        public string STYLE_NAME {
            get { return _STYLE_NAME; }
            set { _STYLE_NAME = value; }
        }
        private string _PROD_PROP;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROD_PROP", System.Data.DbType.String)]
        public string PROD_PROP {
            get { return _PROD_PROP; }
            set { _PROD_PROP = value; }
        }
        private string _PROD_SORT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROD_SORT", System.Data.DbType.String)]
        public string PROD_SORT {
            get { return _PROD_SORT; }
            set { _PROD_SORT = value; }
        }
        private string _PROD_STYLE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROD_STYLE", System.Data.DbType.String)]
        public string PROD_STYLE {
            get { return _PROD_STYLE; }
            set { _PROD_STYLE = value; }
        }
        private string _COLOR_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("COLOR_CODE", System.Data.DbType.String)]
        public string COLOR_CODE {
            get { return _COLOR_CODE; }
            set { _COLOR_CODE = value; }
        }
        private string _COLOR_DESC;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("COLOR_DESC", System.Data.DbType.String)]
        public string COLOR_DESC {
            get { return _COLOR_DESC; }
            set { _COLOR_DESC = value; }
        }
        private string _EDITION_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("EDITION_CODE", System.Data.DbType.String)]
        public string EDITION_CODE {
            get { return _EDITION_CODE; }
            set { _EDITION_CODE = value; }
        }
        private string _EDITION_DESC;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("EDITION_DESC", System.Data.DbType.String)]
        public string EDITION_DESC {
            get { return _EDITION_DESC; }
            set { _EDITION_DESC = value; }
        }
        private string _SPEC_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SPEC_CODE", System.Data.DbType.String)]
        public string SPEC_CODE {
            get { return _SPEC_CODE; }
            set { _SPEC_CODE = value; }
        }
        private string _SPEC_DESC;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SPEC_DESC", System.Data.DbType.String)]
        public string SPEC_DESC {
            get { return _SPEC_DESC; }
            set { _SPEC_DESC = value; }
        }
    }


    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("MY_TABLE_MAIN", "MY_TABLE_MAIN", new string[] { "ID" })]
    [KnownType(typeof(MbfsFucDtlInfo))]
    public class MY_TABLE_MAINInfo : MB.Orm.Common.BaseModel
    {
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ID", System.Data.DbType.Int32)]
        public int ID { get; set; }
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("NAME", System.Data.DbType.String)]
        public string NAME { get; set; }
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CODE", System.Data.DbType.String)]
        public string CODE { get; set; }
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ADDRESS", System.Data.DbType.String)]
        public string ADDRESS { get; set; }
    }
}
