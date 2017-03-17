using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Globalization;

namespace MB.Orm.Persistence
{
    /// <summary>
    /// 提供一个可以获取分页数据的处理类。
    /// </summary>
    public static class DatabaseExecuteExtender
    {
        private static readonly string TABLE_NAME = "Table";
        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        /// <param name="db"></param>
        /// <param name="command"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(this Database db, DbCommand command, DbTransaction transaction, int startRecord, int maxRecord) {
            var dataSet = new DataSet();
            dataSet.Locale = CultureInfo.InvariantCulture;

            if (transaction != null) {
                command.Transaction = transaction;
                command.Connection = transaction.Connection;
                doLoadDataSet(db, command, startRecord, maxRecord, dataSet);
            }
            else {
                using (var wraper = db.GetOpenConnection()) {
                    //try {
                       // dbConnection.Open();
                        command.Connection = wraper.Connection;

                        doLoadDataSet(db, command, startRecord, maxRecord, dataSet);
                    //}
                    //finally {
                    //    dbConnection.Close();
                    //}
                }
            }
            return dataSet;
        }

        private static void doLoadDataSet(Database db, DbCommand command, int startRecord, int maxRecord, DataSet dataSet) {
            using (DbDataAdapter adapter = db.GetDataAdapter()) {
                ((IDbDataAdapter)adapter).SelectCommand = command;

                adapter.TableMappings.Add(TABLE_NAME, TABLE_NAME);

                adapter.Fill(dataSet, startRecord, maxRecord, TABLE_NAME);

            }
        }

    }
}
