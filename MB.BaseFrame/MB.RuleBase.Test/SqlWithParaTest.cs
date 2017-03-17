using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.Orm.DbSql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using MB.RuleBase.Common;

namespace MB.RuleBase.Test
{
    [TestClass]
    public class SqlWithParaTest
    {
        [TestMethod]
        public void TestSqlWithPara()
        {
            try
            {
                Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();

                //string sqlFilter = "((SF_DGN.DOC_DATE BETWEEN to_Date('2012-11-01','YYYY-MM-DD') AND (to_Date('2012-11-30','YYYY-MM-DD')+ 0.99999)))";
                //string sql = "SELECT SF_DGN.BF_ORG_UNIT_ID,SUM(SF_DGN.TTL_QTY) AS TTL_QTY FROM SF_DGN LEFT JOIN SF_DGN_DTL  ON SF_DGN.ID = SF_DGN_DTL.ID  WHERE :CONDITION GROUP BY SF_DGN.BF_ORG_UNIT_ID";

                string sqlFilter = "2012-11-01";
                string sql = "SELECT SF_DGN.BF_ORG_UNIT_ID,SUM(SF_DGN.TTL_QTY) AS TTL_QTY FROM SF_DGN LEFT JOIN SF_DGN_DTL  ON SF_DGN.ID = SF_DGN_DTL.ID  WHERE ((SF_DGN.DOC_DATE BETWEEN to_Date(:CONDITION,'YYYY-MM-DD') AND (to_Date('2012-11-30','YYYY-MM-DD')+ 0.99999))) GROUP BY SF_DGN.BF_ORG_UNIT_ID";
                SqlParamInfo[] paramArray = new SqlParamInfo[1];
                paramArray[0] = new SqlParamInfo("CONDITION", sqlFilter, System.Data.DbType.String);
                System.Data.Common.DbCommand dbcmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(dbcmd, "CONDITION", DbType.String, sqlFilter);

                DataSet ds = new DatabaseExecuteHelper(null).ExecuteDataSet(db, dbcmd);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

        }
    }
}
