using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MB.Orm.Persistence;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace MB.Orm.Test
{
    [TestClass]
    public class EntityCodeHelperTester
    {

        [TestMethod]
        public void TestGetEntityCode()
        {
            string connName = MB.Util.AppConfigSetting.GetKeyValue("NewIDCoreDBConnStr");

            Database db;
            if (string.IsNullOrEmpty(connName))
                db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            else
                db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase(connName);

            var databaseType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);

            string qrySql = "SELECT DCD.LAST_ASSIGNED_NUM FROM BF_DOC_CODE_DETAIL DCD INNER JOIN BF_DOC_CODE DC ON DCD.BF_DOC_CODE_ID = DC.ID WHERE DCD.SECT_CODE = :pSectCode AND DC.DOC_TYPE=:p_DOC_TYPE_NAME";
            DbCommand cmdSelect = db.GetSqlStringCommand(qrySql);

            db.AddInParameter(cmdSelect, "p_DOC_TYPE_NAME", DbType.AnsiString, "DLV");
            db.AddInParameter(cmdSelect, "pSectCode", DbType.AnsiString, "HQ01");
            object val1 = db.ExecuteScalar(cmdSelect);
            int firstResultInt = Convert.ToInt32(val1);
            

            string fisrtResult = EntityCodeHelper.NewInstance.GetEntityCode("DLV", "HQ01");
            object val2 = db.ExecuteScalar(cmdSelect);
            int secondResultInt = Convert.ToInt32(val2);
            Assert.AreEqual(firstResultInt + 1, secondResultInt);

            cmdSelect.Parameters.Clear();
            db.AddInParameter(cmdSelect, "p_DOC_TYPE_NAME", DbType.AnsiString, "DLV");
            db.AddInParameter(cmdSelect, "pSectCode", DbType.AnsiString, "*");

            val1 = db.ExecuteScalar(cmdSelect);
            firstResultInt = Convert.ToInt32(val1);

            fisrtResult = EntityCodeHelper.NewInstance.GetEntityCode("DLV");
            val2 = db.ExecuteScalar(cmdSelect);
            secondResultInt = Convert.ToInt32(val2);
            Assert.AreEqual(firstResultInt + 1, secondResultInt);
           
            
        }
    }
}
