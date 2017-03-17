using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.NoSql.Mongo;

namespace testNoSql
{
    public class testMongoDB
    {
        public void testSaveData() {

            using (var mongo = new MongoDBHelper<MyDataInfo>()) {
                for (int i = 0; i < 10000; i++) {
                    mongo.Save(new MyDataInfo() { ID = 1, Name = "我的测试" });
                }
            }
            //Console.WriteLine("测试成功！");
        }
        public void testGetData() {
            using (var mongo = new MongoDBHelper<MyDataInfo>()) {
                for (int i = 0; i < 1000; i++) {
                    mongo.GetDataByID(1);
                }
            }
        }
        public void testClear() {
            using (var mongo = new MongoDBHelper<MyDataInfo>()) {
                mongo.Clear();
            }
        }

    }

    public class MyDataInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
