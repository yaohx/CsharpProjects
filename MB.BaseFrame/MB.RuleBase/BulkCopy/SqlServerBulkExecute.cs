//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2010-03-21
// Description	:	针对SqlServer 的数据批量处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MB.Orm.DbSql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MB.RuleBase.BulkCopy {
    
    /// <summary>
    /// 针对SqlServer 的数据批量处理.
    /// 使用 System.Data.SqlClient.SqlBulkCopy 进处理。
    /// 目前 只支持在独立事务中执行，需要的话在添加。
    /// </summary>
    internal class SqlServerBulkExecute : AbstractBaseBulk {
        private static readonly string TEMP_TABLE_PREX = "tempdb..";
        
        /// <summary>
        ///针对SqlServer 的数据批量处理.
        ///目前 只支持在独立事务中执行，需要的话在添加。
        /// </summary>
        public SqlServerBulkExecute(params string[] commandTextFormatValues)
            : this(null, commandTextFormatValues) {
    
        }
        /// <summary>
        /// 针对SqlServer 的数据批量处理.
        /// 目前 只支持在独立事务中执行，需要的话在添加。
        /// </summary>
        /// <param name="dbTransaction"></param>
        public SqlServerBulkExecute(System.Data.IDbTransaction dbTransaction, params string[] commandTextFormatValues)
            : base(dbTransaction, commandTextFormatValues) {
        }

        /// <summary>
        /// 调用 System.Data.SqlClient.SqlBulkCopy  进行数据批量处理。
        /// XML 文件中 参数 MappingName 必须和表中字段的名称一致。
        /// 特殊说明：目前只支持INSERT 的情况。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="sqlName"></param>
        /// <param name="lstData"> 实体集合类或者DataRow[]数组</param>
        public override void WriteToServer(string xmlFileName, string sqlName, IList lstData) {
            if (lstData == null || lstData.Count == 0) return;
            if (this.DbTransaction != null) {
                writeToServer(xmlFileName, sqlName, lstData, this.DbTransaction);
                return;
            }
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            // using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.RequiresNew)) {
            //考虑到这里是在独立的事务中执行的，加上如果应用在sql server 中有些时候可能存在不支持DTC的情况，这里就不使用  TransactionScope
            //未来需要使用  TransactionScope.
            //   using (SqlConnection cn = new SqlConnection(db.ConnectionString)) {
            using (var tcn =db.CreateConnection()) {
                SqlConnection cn = tcn as SqlConnection;
                if (cn == null)
                    throw new MB.Util.APPException("请确定当前连接的数据库是否为 SQL SERVER", Util.APPMessageType.SysErrInfo);
                cn.Open();
                using (SqlTransaction tran = cn.BeginTransaction()) {
                    try {
                        writeToServer(xmlFileName, sqlName, lstData, tran);

                        tran.Commit();
                    }
                    catch (Exception ex) {
                        try {
                            tran.Rollback();
                        }
                        catch(Exception x) {
                            MB.Util.TraceEx.Write("SQL SERVER Bulk COPY 操作失败回滚时出错!" + x.Message, MB.Util.APPMessageType.SysDatabaseInfo);
                        }
                        throw new MB.Util.APPException(string.Format("执行XML{0} 中的{1} 的 SQL Server BulkCopy 操作出错! " + ex.Message,xmlFileName,sqlName), MB.Util.APPMessageType.SysDatabaseInfo);
                    }
                }
            }
        }
        //调用 System.Data.SqlClient.SqlBulkCopy  进行数据批量处理。
        // ML 文件中 参数 MappingName 必须和表中字段的名称一致。
        //特殊说明：目前只支持INSERT 的情况。
        private void writeToServer(string xmlFileName, string sqlName, IList lstData, IDbTransaction transaction) {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            if ((transaction.Connection as SqlConnection) == null)
                throw new MB.Util.APPException(string.Format("SqlServer批量处理中提供的{0}不是有效的SqlConnection",transaction.Connection.GetType().FullName), MB.Util.APPMessageType.SysErrInfo );
            if((transaction as SqlTransaction)==null)
                throw new MB.Util.APPException(string.Format("SqlServer批量处理中提供的{0}不是有效的SqlTransaction", transaction.GetType().FullName), MB.Util.APPMessageType.SysErrInfo);

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(transaction.Connection as SqlConnection, SqlBulkCopyOptions.Default, transaction as SqlTransaction)) {
                bulkCopy.BatchSize = this.BatchSize;
                bulkCopy.BulkCopyTimeout = this.BulkCopyTimeout;
                bulkCopy.NotifyAfter = this.NotifyAfter;
                bulkCopy.SqlRowsCopied += new SqlRowsCopiedEventHandler(bulkCopy_SqlRowsCopied);

                MB.Orm.DbSql.SqlString sqlStr = this.GetXmlSqlString(db, xmlFileName, sqlName);
                string tableName = getTableNameFromSql(sqlStr.SqlStr);
                bulkCopy.DestinationTableName = tableName.IndexOf('#') == 0 ? TEMP_TABLE_PREX + tableName : tableName;
             
                DataTable data = convertToDataTable(db, tableName, sqlStr, lstData);
                bulkCopy.WriteToServer(data);
            }
        }

        void bulkCopy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e) {
            DbBulkExecuteEventArgs arg = new DbBulkExecuteEventArgs(e.RowsCopied);
            onSqlRowsCopied(arg);
            e.Abort = arg.Abort;
        }

        #region 内部函数处理...
        //把ListData 中的所有实体对象 转换为 可执行的DataTable 格式
        private DataTable convertToDataTable(Database db, string tableName,MB.Orm.DbSql.SqlString sqlStr, IList lstData) {
            IList<SqlParamInfo> tPars = sqlStr.ParamFields;
            DataTable dTable = createTable(tableName);
            Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> dynamicAcc = new Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor>();
            object entityData = lstData[0];
            Type objType = entityData.GetType();
            //edit by cdc 2011-3-13
            if (objType.IsValueType || string.Compare(objType.Name, "String", true) == 0) {
                if (tPars.Count != 1)
                    throw new MB.Util.APPException(string.Format("批量处理中,值只有一个而参数有{0}个", tPars.Count), Util.APPMessageType.SysErrInfo);
           
                var sp = tPars[0];
                foreach (var d in lstData) {
                    DataRow newDr = dTable.NewRow();
                    newDr[sp.MappingName] = d;
                    dTable.Rows.Add(newDr);
                }
               
            }
            else if ((entityData as DataRow) != null) {
                foreach (DataRow dr in lstData) {
                    DataRow newDr = dTable.NewRow();
                    foreach (SqlParamInfo info in tPars) {
                        newDr[info.MappingName] = dr[info.MappingName]; 
                    }
                    dTable.Rows.Add(newDr);
                }
            }
            else {
                foreach (SqlParamInfo info in tPars) {
                    MB.Util.Emit.DynamicPropertyAccessor acc = new MB.Util.Emit.DynamicPropertyAccessor(entityData.GetType(), info.MappingName);
                    dynamicAcc.Add(info.MappingName, acc);
                }
                foreach (object entity in lstData) {
                    DataRow newDr = dTable.NewRow();
                    foreach (string fieldName in dynamicAcc.Keys) {
                        //MB.Util.MyReflection.Instance.ConvertValueType(
                        newDr[fieldName] = dynamicAcc[fieldName].Get(entity);

                    }
                    dTable.Rows.Add(newDr);
                }
            }
            return dTable;
        }
        private DataTable createTable(string tableName) {
            bool isTempTable = tableName.IndexOf('#') == 0;
            string sql = @"SELECT c.[Name],t.[Name] FROM tempdb..SysColumns c 
                           INNER JOIN tempdb..SysObjects o ON o.id=c.id 
                           LEFT JOIN tempdb..SysTypes t ON c.xType = t.xType AND t.Status<>'1'
                           WHERE c.ID = object_id('tempdb..{0}')  AND o.xtype='U'";

            sql = string.Format(sql, tableName);
            if (!isTempTable)
                sql = sql.Replace(TEMP_TABLE_PREX, "");

           string tName = tableName.IndexOf('#') == 0 ? TEMP_TABLE_PREX + tableName : tableName;
           DataTable table = new DataTable(tName);
            using (IDataReader reader = MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteReader(sql)) {
                while (reader.Read()) {
                    string colName = reader[0].ToString();
                    Type dataType = convertToSystemType(reader[1].ToString());
                    table.Columns.Add(colName, dataType);
                }
            }
            return table;
        }
        //从SQL 语句中获取表名称。
        private string getTableNameFromSql(string sqlString) {
            string[] sqlSplit = sqlString.Split(' ');
            List<string> sqlWords = new List<string>();
            for (int i = 0; i < sqlSplit.Length; i++) {
                if (string.IsNullOrEmpty(sqlSplit[i].Trim())) continue;
                sqlWords.Add(sqlSplit[i]);
            }
            for (int i = 0; i < sqlWords.Count; i++) {
                if (string.Compare(sqlWords[i].Trim(), "INTO", true) != 0) continue;
                if (i + 1 >= sqlWords.Count) continue;
                string temp = sqlWords[i + 1];
                int index = temp.IndexOf('(');

                string tableName = index < 0 ? temp : temp.Substring(0, index);
                return tableName.Trim();
            }
            return string.Empty;
        }
        private System.Type convertToSystemType(string  sqlDbTypeName) {
            string caseName = sqlDbTypeName.ToUpper();
            switch (caseName) {
                case "CHAR":
                case "VARCHAR":
                case "TEXT":
                case "NCHAR":
                case "NVARCHAR":
                case "NTEXT":
                    return typeof(System.String);
                case "TINYINT":
                case "DECIMAL":
                case "NUMERIC":
                    return typeof(System.Decimal);
                case "SMALLINT":
                    return typeof(System.Int16); 
                case "INT":
                    return typeof(System.Int32); 
                case "BIGINT":
                    return typeof(System.Int64); 
                case "UNIQUEIDENTIFIER":
                    return typeof(System.Guid); 
                case "FLOAT":
                case "MONEY":
                case "SMALLMONEY":
                    return typeof(System.Double);
                case "DATETIME":
                case "TIMESTAMP":
                case "SMALLDATETIME":
                    return typeof(System.DateTime);
                case "BIT":
                    return typeof(System.Boolean);
                case "IMAGE":     //在 Microsoft SQL Server 的未来版本中将删除 ntext、text 和 image 数据类型。请避免在新开发工作中使用这些数据类型.
                case "BINARY":
                case "VARBINARY":
                    return typeof(System.Byte[]); 
                default:
                    throw new MB.Util.APPException(string.Format("把DbType{0} 转换为System.Type时出错,请检查相应的数据类型是否已经处理",
                                                    sqlDbTypeName), MB.Util.APPMessageType.SysDatabaseInfo);
            }
        }
        #endregion 内部函数处理...

        #region IDisposable 成员

        private bool _Disposed = false;
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing) {
            if (!this._Disposed) {
                _Disposed = true;

            }
        }
        ~SqlServerBulkExecute()
        {
      
            Dispose(false);
        }

        #endregion
    }
}
