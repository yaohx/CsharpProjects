using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Threading.Tasks;

namespace MB.Orm.Test {
    [TestClass]
    public class DataAccessTest {

        
        [TestMethod]
        public void GetEntityLastModifiedDateTest() {

            DateTime dtNow = DateTime.Now;
            DateTime dtMin = DateTime.MinValue;
            bool reNowPkMin = DateTime.Compare(dtNow, dtMin) >= 0;

            string sqlStr = string.Format("SELECT LAST_MODIFIED_DATE FROM MTSBW.SF_RVD WHERE CODE='02725512'");

            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            DbCommand cmdSelect = db.GetSqlStringCommand(sqlStr);
            object val = db.ExecuteScalar(cmdSelect);
            cmdSelect.Dispose();

            DateTime dtDB = System.Convert.ToDateTime(val);
            bool reNowPkDB = DateTime.Compare(dtNow, dtDB) >= 0;

        }

        private int i;
        [TestMethod]
        public void DealWithDataReaderInMutipleThread() {
            i = 0;
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            System.Data.Common.DbCommand dbCmd = db.GetSqlStringCommand("SELECT * FROM MTSBW.BF_USER");
            using (IDataReader reader = db.ExecuteReader(dbCmd)) {

                try {
                    //System.Threading.Thread t1 = new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                    //        while (reader.Read()) {
                    //            System.Threading.Interlocked.Add(ref i, 1);
                    //        }
                    //    }));


                    //System.Threading.Thread t2 = new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                    //    try {
                    //        while (reader.Read()) {
                    //            System.Threading.Interlocked.Add(ref i, 1);
                    //        }
                    //    }
                    //    catch (Exception ex) {
                    //        string msg = ex.ToString();
                    //    }
                    //}));

                    

                    double timeElapsed = 0;
                    using (MB.Util.MethodTraceWithTime trace = new Util.MethodTraceWithTime("test")) {

                        Task[] tasks = new Task[3] {
                        Task.Factory.StartNew(() => consumeDataReader(reader, ref i)),
                        Task.Factory.StartNew(() => consumeDataReader(reader, ref i)),
                        Task.Factory.StartNew(() => consumeDataReader(reader, ref i))
                        };

                        Task.WaitAll(tasks);


                        timeElapsed = trace.GetExecutedTimes();
                    }

                    int result = i;

                }


                catch (Exception ex) {

                }
                finally {
                    dbCmd.Dispose();
                }
            }
        }

        private void consumeDataReader(IDataReader reader, ref int count) {
            while (reader.Read()) {
                System.Threading.Interlocked.Add(ref i, 1);
            }
        }
    }
}
