using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.RuleBase.Common;
using System.Data;

namespace MB.RuleBase.Test
{
    [TestClass]
    public class DatabaseExecuteHelperTester
    {
        [TestMethod]
        public void TestGetSystemTimeStamp()
        {
            // using (IDataReader reader = DatabaseExecuteHelper.NewInstance.ExecuteReader("SELECT systimestamp FROM dba_users where rownum = 1");
            //{
            //    OracleDataReader  
            //}

            DataSet ds = DatabaseExecuteHelper.NewInstance.ExecuteDataSet("SELECT systimestamp FROM dba_users where rownum = 1", null);
            DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0][0]);
            string timeString = dt.ToString("MM/dd/yyyy hh:mm:ss.fff tt");

        }
        [TestMethod]
        public void TestProcedure()
        {
            List<MB.Orm.DbSql.SqlParamInfo> ls = new List<Orm.DbSql.SqlParamInfo>();

            DatabaseExecuteHelper.NewInstance.ExecByStoreProcedure("", null);
        }
    }
}
