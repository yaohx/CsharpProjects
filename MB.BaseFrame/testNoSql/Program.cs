using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Orm.DB;
 

namespace testNoSql
{
    class Program
    {
        static void Main(string[] args) {
          //  DateTime beg = DateTime.Now;
          // // for (int i = 0; i < 100; i++) {
          //      testMongoDB t = new testMongoDB();
          //      t.testSaveData();
          //   //   t.testGetData();
          ////  }
          //  TimeSpan span = DateTime.Now.Subtract(beg);
           // Console.WriteLine("总共花费:" + span.TotalMilliseconds.ToString());
            //testMongoDB t = new testMongoDB();
            //t.testClear();

            //for (int i = 0; i < 10; i++)
            //{
            //    MB.Util.TraceEx.Write("ok" + i.ToString());
            //}
           
            //    Console.WriteLine(t);
            //var db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            //Console.WriteLine(db.ConnectionString);
            ////using (var d = new OperationDatabaseScope(new OperationDatabaseContext("MB.MBFS1"))) {
            ////    db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            ////    Console.WriteLine(db.ConnectionString);
            ////}
            //using (var d = new MB.Orm.Persistence.DatabaseConfigurationScope("MB.MBFS1")) {
            //    db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            //    Console.WriteLine(db.ConnectionString);
            //}
            var db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            var cmd = db.GetSqlStringCommand("select * from bf_product");
            var data = db.ExecuteDataSet(cmd);

            Console.WriteLine(data.Tables[0].Rows.Count.ToString());

            Console.ReadLine();
        }

    }
}
