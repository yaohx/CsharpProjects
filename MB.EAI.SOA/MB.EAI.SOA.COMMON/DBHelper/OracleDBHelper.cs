using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;

using Microsoft.Practices.EnterpriseLibrary.Data;
using MB.Orm.Persistence;
using System.Data.Common;

namespace MB.EAI.SOA.COMMON.DBHelper
{
    public class OracleDBHelper
    {
       public static  DbConnection _dbNewConn = DatabaseHelper.CreateDatabase().CreateConnection();
        public static DataTable GetDataSetByXml<T>(OracleConnection cn,string xmlFileName, string sqlName, List<T> entities) where T:class
        {
            DataTable dt = new DataTable();
            OracleCommandHelper cmdHelper=new OracleCommandHelper();
            OracleCommand[] arrayCommands = cmdHelper.CreateDbCommandByXml<T>(xmlFileName, sqlName, entities);
            foreach(var one in arrayCommands)
            {
                DataTable dtTemp = new DataTable();
                one.Connection = cn;
                OracleDataAdapter da = new OracleDataAdapter(one);
                da.Fill(dtTemp);
                dt.Merge(dtTemp);
            }
            return dt;
        }
        public static DataTable GetDataSetByXml<T>(string xmlFileName, string sqlName, List<T> entities) where T : class
        {
            Database db = DatabaseHelper.CreateDatabase();
            OracleConnection con = new OracleConnection(db.ConnectionString);
            return GetDataSetByXml<T>(con, xmlFileName, sqlName, entities);
        }

    }
}
