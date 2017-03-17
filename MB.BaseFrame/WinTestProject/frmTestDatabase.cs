using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MB.RuleBase.Common;
using MB.WinClientDefault.UICommand;
using MB.WinBase.Common;
using System.Transactions;
using System.Threading;

namespace WinTestProject {
    
    public partial class frmTestDatabase : Form {
        public frmTestDatabase() {
            InitializeComponent();
            
        }

        private void frmTestDatabase_Load(object sender, EventArgs e) {
                      
        }

        private void button1_Click(object sender, EventArgs e) {
            string sql = "INSERT INTO MYTable(ID,Name) VALUES ({0},'{1}')";
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
          
            
            DateTime begin = System.DateTime.Now;
            try {
                using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required)) {
                        int index = 1;
                        while (true) {
                            DateTime end = System.DateTime.Now;
                            TimeSpan span = end.Subtract(begin);
                            //if (span.TotalSeconds > 5) {
                            //    MB.Util.TraceEx.Write("正在执行长时间操作...");
                            //}
                            if (span.TotalSeconds > 100) break;

                            string newsql = string.Format(sql, index++, "ABC" + index);
                            var cmd = db.GetSqlStringCommand(newsql);
                            db.ExecuteNonQuery(cmd);
                        }
                        scope.Complete();
                    }
                }
                MessageBox.Show("OK"); 
            }
            catch (Exception ex) {
                DateTime end = System.DateTime.Now;
                TimeSpan span = end.Subtract(begin);
                MessageBox.Show("出错 总共花费:" + span.TotalMilliseconds);
                MB.Util.TraceEx.Write(ex.Message);
            }
           

            //string str = MB.Orm.Persistence.DatabaseHelper.GetConnectionString();
            //MB.Orm.Persistence.DatabaseHelper.SaveConnectionString("12412412414214");

        
            //Microsoft.Practices.EnterpriseLibrary.Data.Database db2 = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();

            //MB.Util.Compression.Instance.Zip();   
            //MB.Util.Compression.Instance.UnZip();
  
        }
 
        private void button2_Click(object sender, EventArgs e) {
            //Oracle.DataAccess.Client.OracleConnection cn = new Oracle.DataAccess.Client.OracleConnection();
            //Oracle.DataAccess.Client.OracleTransaction tran = cn.BeginTransaction();
            //try {
            //    MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteNonQuery(db, dbCmds, tran);
            //    tran.Commit();
            //}
            //catch (Exception ex) {
            //    tran.Rollback();
            //    throw ex;
            //}
            //finally {
            //    if(cn.State == ConnectionState.Open)
            //        cn.Close();
            //}
            //var items = MB.WinClientDefault.XtraRibbonMdiMainForm.Active_Mdi_Form.AllCreateMenuItems.Keys;
            //foreach (DevExpress.XtraBars.BarButtonItem but in items) {
            //    if (items[but] == MB.WinClientDefault.UICommand.UICommands.AddNew) {
                     
            //    }
            //}

            //string sql = "INSERT INTO TABLE(ID,NAME,CREATE_DATE) VALUES(@ID,@NAME,@CREATE_DATE)";
            //sql = MB.Orm.DbSql.SqlShareHelper.Instance.ReplaceCanExecSqlStringEx(sql, 1, null, System.DateTime.Now);

            //string str = System.DateTime.Now.ToString("YYYY-MM-DD HH24:MI:SS");

            //MB.Util.Serializer.LightweightTextSerializer ser = new MB.Util.Serializer.LightweightTextSerializer();
            //ser.Serializer();
            //ser.DeSerializer()

            //int re =MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery("MbfsFuc", "AddToMyTable", 1, "ABCDSS");
            //MessageBox.Show(re.ToString()); 

            string str = "bjmSL69kNb91/oXX7DKHUjnSrpDwsxDaij3zuX3IpBqHIPIEXFwa9I0F+Oa2hmwJF+A3IR8r/pGXVqjjJgxfh2BgYGbk8yj7jFdcCchYn+JCbLKDZLX+zYSZQnKBHJg0wrCpoFx0euxssmj8klPtDUPz+Ao4EHINAZ0xsIf0C0q7wmf+vO43ZDcKf8DMRNuIXDzL+ud0D8O7klO9olaWvjLBbtX0DLjuwi+WIa/QVyVlPvWa3DFOfaX5XsIcBUkURfFSNUPgrWlRlUdOGzUDz+SovUb8XyGl9PRhFCNAK5fJlq2RE9Dxuwo0QEAk0EkZ46RQaals3JlDnyjKG+NTL9I9J/5vLEtUa4UElsmwzncmFwlEcQd1JlDtNwPhEO3kGzsEq9ayncZ1QD7R1xKZfQ==";
            var t = MB.Util.DESDataEncrypt.DecryptString(str);
            string t1 = t;              
        }
 
        private void ribbonControl1_Click(object sender, EventArgs e) {
            
        }
 
        private void button3_Click(object sender, EventArgs e) {

            ////OracleClient 10.2 或者 11G 可以这样使用
            //using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required)) {
            //    using (var dbBulk = MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateDbBulkExecute()) {
            //        dbBulk.WriteToServer("xml文件名称", "SQL 名称", "实体集合类");
            //    }
            //    scope.Complete();
            //}

            ////或

            ////也可以显示调用Oracle DataAccess 事务
            //Oracle.DataAccess.Client.OracleConnection cn = new Oracle.DataAccess.Client.OracleConnection();
            //Oracle.DataAccess.Client.OracleTransaction tran = cn.BeginTransaction();
            //try {
            //    using (var dbBulk = MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateDbBulkExecute(tran)) {
            //        dbBulk.WriteToServer("xml文件名称", "SQL 名称", "实体集合类");
            //    }
            //    tran.Commit();
            //}
            //catch (Exception ex) {
            //    tran.Rollback();
            //    throw ex;
            //}
            //finally {
            //    if (cn.State == ConnectionState.Open)
            //        cn.Close();
            //}
           //var lst = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<MbfsFucInfo>("MbfsFuc", "SelectObject");
           // MessageBox.Show(lst.Count.ToString());  

            //string sql = "SELECT * FROM MyTable where Name = :Name";
            //Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            //var cmd = db.GetSqlStringCommand(sql);
            //db.AddInParameter(cmd, "ID", DbType.AnsiString);
            //cmd.Parameters[0].Value = "ABCDSS";

            //DataSet ds = db.ExecuteDataSet(cmd);

            //getOjbects();
            for (int i = 0; i < 1000; i++)
            {
                ThreadStart s = new ThreadStart(getOjbects);
                System.Threading.Thread thread = new System.Threading.Thread(s);
                thread.Start();
            }
            MessageBox.Show("test over");
            //MessageBox.Show(ds.Tables[0].Rows.Count.ToString());   
             
        }

        private static void getOjbects()
        {
            var list = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<string>("TestMySqlDB", "SelectObjectProduct");
            MB.Util.TraceEx.Write(string.Join(",",list.ToArray()));
        }

        private void button4_Click(object sender, EventArgs e) {
            //(参数:ID=>'20',CODE=>'PKN_MISSION',NAME=>'分拣任务单6')
            try {
                string sql = "UPDATE BF_DOC_TYPE SET CODE = :CODE ,NAME = :NAME WHERE ID = :ID ";
                Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
                var cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "ID", DbType.Int32);
                db.AddInParameter(cmd, "CODE", DbType.AnsiString);
                db.AddInParameter(cmd, "NAME", DbType.AnsiString);
            
                object val = 20;
                cmd.Parameters[0].Value = val;
                cmd.Parameters[1].Value = "PKN_MISSION";
                cmd.Parameters[2].Value = "分拣任务单6";
                (cmd as Oracle.DataAccess.Client.OracleCommand).BindByName = true;

                db.ExecuteNonQuery(cmd);

                string n = cmd.GetType().FullName;
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        private void button5_Click(object sender, EventArgs e) {
            SimulatedOracleHelper helper = new SimulatedOracleHelper();
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();    
            string sql = "INSERT INTO MY_GDN(ID,CODE,NAME) VALUES(:ID,:CODE,:NAME)";
            System.Data.IDbCommand cmd = helper.CreateOracleCommand(sql);
           var p1 = helper.CreateOracleParameter("ID", Oracle.DataAccess.Client.OracleDbType.Int32);
           var p2 = helper.CreateOracleParameter("CODE", Oracle.DataAccess.Client.OracleDbType.Varchar2);
           var p3 = helper.CreateOracleParameter("NAME", Oracle.DataAccess.Client.OracleDbType.Varchar2);

            p1.Value = 2;
            p2.Value = "BB";
            p3.Value = "BB";
            IDbConnection cn = helper.CreateOracleConnection(db.ConnectionString);
            cn.Open();
            helper.SetCommandConnection(cn,cmd); 
            helper.ExecuteNonQuery(cmd);
            MessageBox.Show("OK"); 
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            MessageBox.Show("ok");
        }

        private void button7_Click(object sender, EventArgs e) {
            MB.Util.Common.QueryParameterHelper helper = new MB.Util.Common.QueryParameterHelper();
            helper.AddParameterInfo("1", "1", MB.Util.DataFilterConditions.Special);
            helper.AddParameterInfo("", "ID in(2,2,3,3)", MB.Util.DataFilterConditions.SqlAppend);
            
            string sql = MB.Orm.DbSql.SqlShareHelper.Instance.QueryParametersToSqlString(null,helper.ToArray());
            MessageBox.Show(sql);

        }

        
    }

    /// <summary>
    /// 临时解决方案，换成Oracle11 G 以后，该方法不在使用。
    /// </summary>
    public class SimulatedOracleHelper{
        public static readonly string ORACLE_ASSEMBLY = @"Oracle.DataAccess.dll";
        public static readonly string ORACLE_CONNECTION = "Oracle.DataAccess.Client.OracleConnection";
        public static readonly string ORACLE_COMMAND = "Oracle.DataAccess.Client.OracleCommand";
        public static readonly string ORACLE_PARAMTER = "Oracle.DataAccess.Client.OracleParameter";



        /// <summary>
        /// CreateOracleConnection
        /// </summary>
        /// <param name="cn"></param>
        /// <returns></returns>
        public System.Data.IDbConnection CreateOracleConnection(string cn) {
            object instance = MB.Util.DllFactory.Instance.LoadObject(ORACLE_CONNECTION, new object[]{ cn }, ORACLE_ASSEMBLY);

            return instance as System.Data.IDbConnection;
        }
        /// <summary>
        ///  SetCommandConnection
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="oracleCommand"></param>
        public void SetCommandConnection(System.Data.IDbConnection cn, System.Data.IDbCommand oracleCommand) {
            MB.Util.MyReflection.Instance.InvokePropertyForSet(oracleCommand, "Connection", cn); 
        }
        /// <summary>
        /// CreateOracleCommand
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public System.Data.IDbCommand CreateOracleCommand(string sql) {
            object instance = MB.Util.DllFactory.Instance.LoadObject(ORACLE_COMMAND,new object[]{ sql }, ORACLE_ASSEMBLY);
            if (instance == null)
                throw new MB.Util.APPException(string.Format("根据类型{0},{1} 创建实例出错",ORACLE_COMMAND,ORACLE_ASSEMBLY), MB.Util.APPMessageType.SysErrInfo);

            return instance as System.Data.IDbCommand;
        }
        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="oracleCommand"></param>
        /// <returns></returns>
        public void ExecuteNonQuery(System.Data.IDbCommand oracleCommand) {
            MB.Util.MyReflection.Instance.InvokeMethodByName(oracleCommand, "ExecuteNonQuery");
        }

        /// <summary>
        /// AddOracleParamter
        /// </summary>
        /// <param name="oracleCommand"></param>
        /// <param name="paramter"></param>
        public void AddOracleParamter(System.Data.IDbCommand oracleCommand,IDbDataParameter paramter) {
            object paramters = oracleCommand.GetType().GetProperty("Parameters");
            MB.Util.MyReflection.Instance.InvokeMethodByName(paramters, "Add", paramter);

        }
        /// <summary>
        /// SetCommandArrayBindCount
        /// </summary>
        /// <param name="oracleCommand"></param>
        /// <param name="arrayBindCount"></param>
        public void SetCommandArrayBindCount(System.Data.IDbCommand oracleCommand, int arrayBindCount) {
            MB.Util.MyReflection.Instance.InvokePropertyForSet(oracleCommand, "ArrayBindCount", arrayBindCount);
        }
        #region 参数相关...
        /// <summary>
        /// CreateOracleParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="oracleDataType"></param>
        /// <returns></returns>
        public IDbDataParameter CreateOracleParameter(string name, object oracleDataType) {
            object instance = MB.Util.DllFactory.Instance.LoadObject(ORACLE_PARAMTER, new object[] { name, oracleDataType }, ORACLE_ASSEMBLY);
            return instance as IDbDataParameter;

        }
        ///// <summary>
        /////  SetParamterDirection
        ///// </summary>
        ///// <param name="paramter"></param>
        ///// <param name="direction"></param>
        //public void SetParamterDirection(IDbDataParameter paramter, ParameterDirection direction) {
        //    MB.Util.MyReflection.Instance.InvokePropertyForSet(paramter, "Direction", direction);
        //}
        ///// <summary>
        ///// SetParamterValue
        ///// </summary>
        ///// <param name="paramter"></param>
        ///// <param name="value"></param>
        //public void SetParamterValue(IDbDataParameter paramter,object value) {
        //    MB.Util.MyReflection.Instance.InvokePropertyForSet(paramter, "Value", value);
        //}
        #endregion 参数相关...
    }

    
}

