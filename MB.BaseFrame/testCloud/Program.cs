using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using testCloud.Model;
using System.Data.Common;

namespace testCloud
{

    class Program
    {
        static void Main(string[] args) {

            //using (MB.Orm.DB.OperationDatabaseScope scope = new MB.Orm.DB.OperationDatabaseScope("SQL SERVER"))
            //{
            //    ///
            //    ///
            //    //var db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            //    //db
            //}

            //}
            //var db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            //var dbReadOnly = MB.Orm.Persistence.DatabaseHelper.CreateDatabase(new MB.Orm.DB.OperationDatabaseContext(true));
           // using (MB.Orm.DB.OperationDatabaseScope scope = new MB.Orm.DB.OperationDatabaseScope(true)) {
          //  List<DbConnection> lst = new List<DbConnection>();
          //  for (int i = 0; i < 100; i++) {
          //      var ocn = new MB.RuleBase.BulkCopy.SimulatedOracleHelper().CreateOracleConnection();
          //      ocn.Open();
          //      lst.Add(ocn);
                
          //  }
          //  foreach (var cn in lst) {
          //      cn.Close();
          //      cn.Dispose();
          //  }
          //  lst.Clear();
          //      //using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope()) {
          //      //    var datas = MB.RuleBase.Common.DatabaseExcuteByXmlHelper.NewInstance.ExecuteScalar<int>("TestCloudDb", "SelectObject", null);
          //      //    Console.WriteLine("OK" + datas);
          //      //    scope.Complete();
          //      //}
          ////  }
          //  //using(var dbScope = new MB.Orm.Persistence.DatabaseConfigurationScope()){
          //  //    using (MB.Orm.DB.OperationDatabaseScope dbscope = new MB.Orm.DB.OperationDatabaseScope(true))
          //  //    {
          //  //        //
          //  //    }
                
          //  //}
          //  using (var dbCn = new MB.RuleBase.BulkCopy.SimulatedOracleHelper().CreateOracleConnection()) {
          //      dbCn.Open();
          //      var tran = dbCn.BeginTransaction();

          //      var db = new MB.RuleBase.Common.DatabaseExcuteByXmlHelper();
          //      //var c = db.GetObjectsByXml<MyDataInfo>()
          //  }

          //  registryDB();
            testOracleDb.testGetObjects();

            Console.WriteLine("OK");
            Console.ReadLine();
        }
        class MyDataInfo
        {
            public string Name { get; set; }
        }
        private static void registryDB() {
            try {
                // 启动时建立ConnectionManager,需要正确配置Bitsflow参数
                Yoyosys.ConnectionManager manager = new Yoyosys.ConnectionManager("192.168.204.35:23245","5678","4567","5888","ConnectionManager");

                System.Console.WriteLine("1");
                // 构造数据，需要根据实际情况修改IP和connectionString
                Yoyosys.DatabaseConfig databaseConfig = new Yoyosys.DatabaseConfig();
                databaseConfig.name = "test";
                databaseConfig.master = new Yoyosys.ConnectionConfig("192.168.204.73", "MTSBW", "NEWTEST", "oracle", "Oracle.DataAccess.Client");// "Provider=MSDAORA.1;User ID=MTSBW;Password=mtsbw123;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.204.73)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = NEWTEST)))");
                List<Yoyosys.ConnectionConfig> slaveList = new List<Yoyosys.ConnectionConfig>();
                slaveList.Add(new Yoyosys.ConnectionConfig("192.168.204.74", "MTSBW", "NEWTEST", "oracle", "Oracle.DataAccess.Client"));//"192.168.204.74","MTSBW", "Provider=MSDAORA.1;User ID=MTSBW;Password=mtsbw123;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.204.74)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = NEWTEST)))"));
                slaveList.Add(new Yoyosys.ConnectionConfig("192.168.204.75", "MTSBW", "NEWTEST", "oracle", "Oracle.DataAccess.Client"));//"192.168.204.75", "MTSBW", "Provider=MSDAORA.1;User ID=MTSBW;Password=mtsbw123;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.204.75)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = NEWTEST)))"));
                databaseConfig.slaves = slaveList;
                manager.updateDatabaseConfig("core", databaseConfig);
                Thread.Sleep(500);
                System.Console.WriteLine("2");
                // DAO层获取数据库连接
                bool readOnly = false;
                Yoyosys.ConnectionConfig connectionConfig = manager.getConnectionConfig("core", readOnly);
                System.Console.WriteLine("3");
                // String connectionString = connectionConfig.connectionString;
                System.Console.WriteLine(connectionConfig.ip);
                manager.Dispose();
                connectionConfig = manager.getConnectionConfig("core", readOnly);
                //
                // Console.WriteLine("enter any key to end");
                // Console.ReadKey();
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }
    }

}
