//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-05-18
// Description	:	直接执行数据库SQL 语句操作处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using MB.Orm.Common;
using MB.Orm.Enums;
using MB.Orm.Persistence;
using MB.Orm.Persistence;
using MB.RuleBase.Atts;
using MB.RuleBase.IFace;
using MB.Util.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using MB.WcfService;
using System.Text.RegularExpressions;

namespace MB.RuleBase.Common
{
    /// <summary>
    /// 直接执行数据库SQL 语句操作处理相关。
    /// </summary>
    public class DatabaseExecuteHelper
    {
        private MB.Orm.Persistence.PersistenceManagerHelper _PersistenceHelper;
        private QueryBehavior _QueryBehavior;

        /// <summary>
        /// DatabaseExecuteHelper
        /// </summary>
        public DatabaseExecuteHelper()
        {
            _PersistenceHelper = new PersistenceManagerHelper();
        }
        /// <summary>
        /// DatabaseExecuteHelper
        /// </summary>
        /// <param name="queryBehavior"></param>
        public DatabaseExecuteHelper(QueryBehavior queryBehavior):this ()
        {
            _QueryBehavior = queryBehavior;
        }
        /// <summary>
        /// 返回新的对象实例。
        /// </summary>
        public static DatabaseExecuteHelper NewInstance
        {
            get
            {
                return new DatabaseExecuteHelper();
            }
        }
        #region ExecByStoreProcedure...
        /// <summary>
        /// 通过存储过程执行数据库操作。
        /// </summary>
        /// <param name="storeProcedureName">指定的存储过程文件名称</param>
        /// <param name="sqlParInfos">MB.Orm.DbSql.SqlParamInfo 数组格式的参数</param>
        /// <returns>大于或者等0表示成功，-1表示不成功 </returns>
        public int ExecByStoreProcedure(string storeProcedureName, MB.Orm.DbSql.SqlParamInfo[] sqlParInfos)
        {
            return ExecByStoreProcedure((System.Data.Common.DbTransaction)null, storeProcedureName, sqlParInfos);
        }
        /// <summary>
        /// 通过存储过程执行数据库操作。
        /// </summary>
        /// <param name="dbTran"></param>
        /// <param name="storeProcedureName"></param>
        /// <param name="sqlParInfos"></param>
        /// <returns></returns>
        public int ExecByStoreProcedure(System.Data.Common.DbTransaction dbTran, string storeProcedureName, MB.Orm.DbSql.SqlParamInfo[] sqlParInfos)
        {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();

            int re = 0;
            DbCommand dbCmd = db.GetStoredProcCommand(storeProcedureName);
            if (sqlParInfos != null && sqlParInfos.Length > 0)
            {
                foreach (var par in sqlParInfos)
                {
                    if (par.Direction == ParameterDirection.Input)
                    {
                        _PersistenceHelper.AddParamInfoToDbCommand(db, dbCmd, par);
                        //db.AddInParameter(dbCmd, par.Name, par.DbType, par.Value);
                    }
                    else if (par.Direction == ParameterDirection.Output)
                    {
                        _PersistenceHelper.AddParamInfoToDbCommand(db, dbCmd, par);
                        // db.AddOutParameter(dbCmd, par.Name, par.DbType, par.Length);
                    }
                }
            }
            try
            {
                string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, dbCmd);
                MB.Util.TraceEx.Write("正在执行:" + cmdMsg);

                using (new Util.MethodTraceWithTime(true, cmdMsg))
                {
                    re = dbTran == null ? db.ExecuteNonQuery(dbCmd) : db.ExecuteNonQuery(dbCmd, dbTran);
                }

                if (sqlParInfos != null && sqlParInfos.Length > 0)
                {
                    foreach (var par in sqlParInfos)
                    {
                        if (par.Direction == ParameterDirection.Output)
                        {
                            par.Value = db.GetParameterValue(dbCmd, par.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new MB.RuleBase.Exceptions.DatabaseExecuteException("ExecByStoreProcedure 出错！", ex);
            }
            finally
            {
                dbCmd.Dispose();
            }
            return re;
        }
        #endregion ExecByStoreProcedure...

        #region ExecuteNonQuery...
        /// <summary>
        /// 执行数据库操作，返回受影响的行数。
        /// </summary>
        /// <param name="db">操作数据库对象</param>
        /// <param name="dbCmd">数据操作的dbCommand</param>
        /// <returns>返回受影响的行数,-1 表示不成功</returns>
        public int ExecuteNonQuery(Database db, DbCommand dbCmd)
        {
            return ExecuteNonQuery(db, new DbCommand[] { dbCmd });
        }
        /// <summary>
        /// 根据DbCommand 执行相应的数据库操作。
        ///特殊说明：  加入到 DbCommand[] 内的所有Command 将在同一个事务中进行处理。
        ///默认使用的是  System.Transactions.TransactionScopeOption.Required
        /// </summary>
        /// <param name="db">操作数据库对象</param>
        /// <param name="dbCmds">数据操作的dbCommand</param>
        /// <returns>返回受影响的行数,-1 表示不成功</returns>
        public int ExecuteNonQuery(Database db, DbCommand[] dbCmds)
        {
            return ExecuteNonQuery(db, dbCmds, System.Transactions.TransactionScopeOption.Required);
        }
        /// <summary>
        /// 根据DbCommand 执行相应的数据库操作。
        ///特殊说明：  加入到 DbCommand[] 内的所有Command 将在同一个事务中进行处理。
        /// </summary>
        /// <param name="db">操作数据库对象</param>
        /// <param name="dbCmds">数据操作的dbCommand</param>
        /// <param name="transactionScope">当前的所有操作事务处理的类型</param>
        /// <returns>返回受影响的行数,-1 表示不成功</returns>
        public int ExecuteNonQuery(Database db, DbCommand[] dbCmds, System.Transactions.TransactionScopeOption transactionScope)
        {
            int count = 0;
            var dbType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(transactionScope))
            {
                try
                {

                    foreach (DbCommand cmd in dbCmds)
                    {
                        try
                        {
                            if (dbType == DatabaseType.Oracle)
                            {
                                cmd.CommandText = _PersistenceHelper.RemoveSqlStringLastSemicolon(db, cmd.CommandText);
                            }
                            string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmd);
                            MB.Util.TraceEx.Write("正在执行:" + cmdMsg);
                            using (new Util.MethodTraceWithTime(true, cmdMsg))
                            {
                                count += db.ExecuteNonQuery(cmd);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new MB.RuleBase.Exceptions.DatabaseExecuteException(" 数据执行SQL 语句 出错！", ex);
                        }
                    }
                    scope.Complete();
                }
                catch (MB.RuleBase.Exceptions.DatabaseExecuteException dx)
                {
                    throw dx;
                }
                catch (Exception exx)
                {
                    throw exx;
                }
                finally
                {

                    try
                    {
                        foreach (DbCommand cmd in dbCmds)
                            cmd.Dispose();
                    }
                    catch { }
                }
                return count;
            }

        }
        /// <summary>
        /// 根据DbCommand 执行相应的数据库操作。
        /// </summary>
        /// <param name="db">操作数据库对象</param>
        /// <param name="dbCmds">数据操作的dbCommand</param>
        /// <param name="transaction">数据库操作事务(特别说明：除非特殊需要，否则尽量不要传入一个指定的事务对象)</param>
        /// <returns></returns>
        public int ExecuteNonQuery(Database db, DbCommand[] dbCmds, DbTransaction transaction)
        {
            int count = 0;
            var dbType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);

            try
            {

                foreach (DbCommand cmd in dbCmds)
                {
                    try
                    {
                        if (dbType == DatabaseType.Oracle)
                        {
                            cmd.CommandText = _PersistenceHelper.RemoveSqlStringLastSemicolon(db, cmd.CommandText);
                        }

                        string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmd);
                        MB.Util.TraceEx.Write("正在执行:" + cmdMsg);
                        using (new Util.MethodTraceWithTime(true, cmdMsg))
                        {
                            count += transaction == null ? db.ExecuteNonQuery(cmd) : db.ExecuteNonQuery(cmd, transaction);

                        }
                    }
                    catch (Exception ex)
                    {
                        throw new MB.RuleBase.Exceptions.DatabaseExecuteException(" 数据执行SQL 语句 出错！", ex);
                    }
                }
            }
            catch (MB.RuleBase.Exceptions.DatabaseExecuteException dx)
            {
                throw dx;
            }
            catch (Exception exx)
            {
                throw exx;
            }
            finally
            {

                try
                {
                    foreach (DbCommand cmd in dbCmds)
                        cmd.Dispose();
                }
                catch { }
            }
            return count;

        }
        /// <summary>
        ///  根据SQl 字符窜 执行相应的数据库操作。
        /// </summary>
        /// <param name="sqlString">需要进行操作的SQL 拼接字符窜</param>
        /// <param name="sqlParams">MB.Orm.DbSql.SqlParamInfo 集合类型的参数 </param>
        /// <returns>返回受影响的行数,-1 表示不成功</returns>
        public int ExecuteNonQuery(string sqlString, List<MB.Orm.DbSql.SqlParamInfo> sqlParams)
        {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();

            var dbCmd = db.GetSqlStringCommand(sqlString);
            dbCmd.CommandText = _PersistenceHelper.ReplaceSqlParamsByDatabaseType(db, dbCmd.CommandText);

            if (sqlParams != null && sqlParams.Count > 0)
            {
                foreach (var parInfo in sqlParams)
                {
                    _PersistenceHelper.AddParamInfoToDbCommand(db, dbCmd, parInfo);
                }
            }
            try
            {
                string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, dbCmd);
                MB.Util.TraceEx.Write("正在执行:" + cmdMsg);
                using (new Util.MethodTraceWithTime(true, cmdMsg))
                {
                    return db.ExecuteNonQuery(dbCmd);
                }
            }
            catch (Exception ex)
            {
                throw new MB.RuleBase.Exceptions.DatabaseExecuteException(" 数据执行SQL 语句 出错！", ex);
            }
            finally
            {
                dbCmd.Dispose();
            }
        }
        #endregion ExecuteNonQuery...

        /// <summary>
        /// 根据对象类型获取值
        /// </summary>
        /// <typeparam name="T">获取得到返回值的数据类型</typeparam>
        /// <param name="transaction"></param>
        /// <param name="cfgEntityType"></param>
        /// <param name="sqlString"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public List<T> GetObjects<T>(DbTransaction transaction, Type cfgEntityType, string sqlString, List<MB.Orm.DbSql.SqlParamInfo> sqlParams)
        {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();

            if (_PersistenceHelper == null)
                _PersistenceHelper = new PersistenceManagerHelper();

            var dbCmd = db.GetSqlStringCommand(sqlString);
            dbCmd.CommandText = _PersistenceHelper.ReplaceSqlParamsByDatabaseType(db, dbCmd.CommandText);

            if (sqlParams != null && sqlParams.Count > 0)
            {
                foreach (var parInfo in sqlParams)
                {
                    _PersistenceHelper.AddParamInfoToDbCommand(db, dbCmd, parInfo);
                }
            }
            System.Data.Common.DbCommand cmd = dbCmd;

            Type entityType = cfgEntityType;

            return GetObjects<T>(cfgEntityType, transaction, db, cmd);
        }
        /// <summary>
        /// 根据对象类型获取值
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="entityType">泛型对象的类型，为了兼容以前的接口才这么做</param>
        /// <param name="transaction">事务</param>
        /// <param name="db">数据库对象</param>
        /// <param name="cmd">数据库命令</param>
        /// <returns>对象集合</returns>
        public List<T> GetObjects<T>(Type entityType, DbTransaction transaction, Database db, DbCommand cmd)
        {
            if (entityType.IsValueType || entityType.Equals(typeof(string)))
                return GetValueTypeObjects<T>(entityType, transaction, db, cmd);

            bool isBaseModelSub = entityType.IsSubclassOf(typeof(BaseModel));
            var attMapping = MB.Orm.Mapping.AttMappingManager.Instance;
            var lstMapping = attMapping.CheckExistsModelMapping(entityType) ?
                attMapping.GetModelMappingInfo(entityType).FieldPropertys.Values.ToList() : attMapping.GetEntityMappingPropertys(entityType);

            List<T> ic = new List<T>();
            var qh = _QueryBehavior;
            if (qh == null) qh = MessageHeaderHelper.GetQueryBehavior();
            resetDynamicColumns(cmd, qh);
            string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmd);
            MB.Util.TraceEx.Write("正在执行:" + cmdMsg);
            using (new Util.MethodTraceWithTime(true, cmdMsg))
            {
                using (IDataReader reader = transaction == null ? db.ExecuteReader(cmd) : db.ExecuteReader(cmd, transaction))
                {
                    try
                    {
                        DataTable dtReader = reader.GetSchemaTable();
                        Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> existsFields = new Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor>();
                        foreach (MB.Orm.Mapping.FieldPropertyInfo proInfo in lstMapping)
                        {
                            if (dtReader.Select(string.Format("ColumnName='{0}'", proInfo.FieldName)).Length <= 0)
                                continue;
                            System.Reflection.PropertyInfo propertyInfo = entityType.GetProperty(proInfo.PropertyName);
                            if (propertyInfo == null) continue;

                            MB.Util.Emit.DynamicPropertyAccessor propertyAccess = new MB.Util.Emit.DynamicPropertyAccessor(entityType, propertyInfo);
                            existsFields.Add(proInfo.FieldName, propertyAccess);
                        }
                        if (existsFields.Count > 0)
                        {
                            int rows = -1;
                            int firstIndex = qh != null ? qh.PageIndex * qh.PageSize : -1;

                            while (reader.Read())
                            {
                                rows += 1;
                                //限制读取的最大行数
                                if (qh != null)
                                {
                                    if (rows < firstIndex) continue;
                                    if ((rows - firstIndex) >= qh.PageSize)
                                    {
                                        int currentRowsCount = rows - firstIndex;
                                        //如果读取的记录数大于当前页规定的记录数，如果要返回全部记录，则游标继续，如果不要返回，则推出循环
                                        if (qh.IsTotalPageDisplayed)
                                            continue;
                                        else if (currentRowsCount == qh.PageSize) //在服务器这边多读取一条，以通知客户端是否需要分页
                                            continue;
                                        else
                                            break;

                                    }
                                }

                                T entity = (T)MB.Util.DllFactory.Instance.CreateInstance(entityType);

                                foreach (var proInfo in existsFields)
                                {
                                    var val = reader[proInfo.Key];
                                    if (val != null && val != System.DBNull.Value)
                                    {
                                        existsFields[proInfo.Key].Set(entity, val);
                                    }
                                }
                                if (isBaseModelSub)
                                {
                                    MB.Orm.Common.BaseModel temp = entity as MB.Orm.Common.BaseModel;
                                    temp.EntityState = EntityState.Persistent;
                                }
                                ic.Add(entity);
                            }

                            if (qh != null && qh.PageSize < Int32.MaxValue) {
                                //如果有些语句不想分页的，要么不设置QueryBehavior或者把PageSize设置为MaxValue，则不传分页信息到客户端
                                MessageHeaderHelper.AppendMessageHeaderResponse(new ResponseHeaderInfo(rows + 1), true);
                                DataBaseQueryResult.Instance.SetTotalRows(rows + 1);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new MB.RuleBase.Exceptions.DatabaseExecuteException("执行GetObjectsByXml<" + entityType.FullName + "> 出错！", ex);
                    }
                    finally
                    {
                        cmd.Dispose();
                    }
                }
            }
            return ic;
        }

        #region 动态列相关功能

        /// <summary>
        /// 为SQL语句设定动态列
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="qh"></param>
        private void resetDynamicColumns(DbCommand cmd, QueryBehavior qh)
        {
            if (qh != null && qh.Columns != null && qh.Columns.Length > 0)
            {
                string[] cols = qh.Columns.Split(',');
                string strSql = cmd.CommandText;
                cmd.CommandText = "SELECT " + qh.Columns + " FROM (" + strSql + ") ";
                MB.Util.TraceEx.Write("发现动态列: " + cmd.CommandText);
            }
        }

        /// <summary>
        /// 截取order by前的SQL语句，如果没有order by,则返回原SQL语句
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private string DumpMatch(string inputString)
        {
            string result = string.Empty;

            var lastMatchIndex = Regex.Replace(inputString, @"\s", " ").ToUpper().LastIndexOf("ORDER BY");

            if (lastMatchIndex > 0)
                result = inputString.Substring(0, lastMatchIndex);
            else
                result = inputString;
            return result;
        }

        private string[] getOrderCols(string inputString)
        {
            string[] cols = inputString.Split(',');

            string[] orderCols = new string[cols.Length];
            for (int i = 0; i < cols.Length; i++)
            {
                orderCols[i] = cols[i].Substring(cols[i].LastIndexOf('.')+1);
            }

            return orderCols;
        }

        #endregion

        /// <summary>
        /// 获取值类型的数据集合
        /// 备注：在这里字符窜也当做值类型进行处理。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityType"></param>
        /// <param name="transaction"></param>
        /// <param name="db"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public List<T> GetValueTypeObjects<T>(Type entityType, DbTransaction transaction, Database db, DbCommand cmd)
        {
            List<T> ic = new List<T>();
            string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmd);
            MB.Util.TraceEx.Write("正在执行:" + cmdMsg);
            using (new Util.MethodTraceWithTime(true, cmdMsg))
            {
                using (IDataReader reader = transaction == null ? db.ExecuteReader(cmd) : db.ExecuteReader(cmd, transaction))
                {
                    try
                    {
                        while (reader.Read())
                        {
                            var val = reader[0];
                            if (val != null && val != System.DBNull.Value)
                                ic.Add((T)MB.Util.MyConvert.Instance.ChangeType(val, typeof(T)));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new MB.RuleBase.Exceptions.DatabaseExecuteException("执行GetObjectsByXml<" + entityType.FullName + "> 出错！", ex);
                    }
                    finally
                    {
                        cmd.Dispose();
                    }
                }
            }
            return ic;
        }

        #region ExecuteReader...
        /// <summary>
        /// 以连接式的方式直接执行SQL 语句并返回读取器 
        /// </summary>
        /// <param name="sqlString">需要进行操作的SQL 拼接字符窜</param>
        /// <returns>IDataReader 数据读取器</returns>
        public IDataReader ExecuteReader(string sqlString)
        {
            return ExecuteReader(sqlString, (DbTransaction)null);
        }
        /// <summary>
        /// 以连接式的方式直接执行SQL 语句并返回读取器 
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="sqlString">需要进行操作的SQL 拼接字符窜</param>
        /// <returns>IDataReader 数据读取器</returns>
        public IDataReader ExecuteReader(string sqlString, DbTransaction transaction)
        {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();

            var dbCmd = db.GetSqlStringCommand(sqlString);
            dbCmd.CommandText = _PersistenceHelper.ReplaceSqlParamsByDatabaseType(db, dbCmd.CommandText);

            try
            {
                string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, dbCmd);
                MB.Util.TraceEx.Write("正在执行:" + cmdMsg);
                using (new Util.MethodTraceWithTime(true, cmdMsg))
                {
                    return db.ExecuteReader(dbCmd);
                }
            }
            catch (Exception ex)
            {
                throw new MB.RuleBase.Exceptions.DatabaseExecuteException(" 数据执行SQL 语句 出错！", ex);
            }
            finally
            {
                dbCmd.Dispose();
            }
        }

        /// <summary>
        /// ExecuteReader.
        /// 返回DataReader  读取器，使用后别忘了关闭掉。
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCmd"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(Database db, DbCommand dbCmd)
        {
            IDataReader val = null;
            try
            {
                string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, dbCmd);
                MB.Util.TraceEx.Write("正在执行:" + cmdMsg);
                using (new Util.MethodTraceWithTime(true, cmdMsg))
                {
                    val = db.ExecuteReader(dbCmd);
                }
            }
            catch (Exception ex)
            {
                throw new MB.RuleBase.Exceptions.DatabaseExecuteException(" 数据执行SQL 语句 出错！", ex);
            }
            finally
            {
                dbCmd.Dispose();
            }
            return val;
        }
        #endregion ExecuteReader...

        /// <summary>
        /// ExecuteScalar
        /// 执行返回单个值。
        /// </summary>
        /// <param name="db">操作数据库对象</param>
        /// <param name="dbCmd">数据操作的dbCommand</param>
        /// <returns>object 类型的数值</returns>
        public object ExecuteScalar(Database db, DbCommand dbCmd)
        {
            return ExecuteScalar(db, dbCmd, (DbTransaction)null);

        }
        /// <summary>
        /// ExecuteScalar
        /// 执行返回单个值。
        /// </summary>
        /// <param name="db">操作数据库对象</param>
        /// <param name="dbCmd">数据操作的dbCommand</param>
        /// <param name="transaction"></param>
        /// <returns>object 类型的数值</returns>
        public object ExecuteScalar(Database db, DbCommand dbCmd, DbTransaction transaction)
        {
            object val = null;
            try
            {
                string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, dbCmd);
                MB.Util.TraceEx.Write("正在执行:" + cmdMsg);
                using (new Util.MethodTraceWithTime(true, cmdMsg))
                {
                    val = transaction == null ? db.ExecuteScalar(dbCmd) : db.ExecuteScalar(dbCmd, transaction);
                }
            }
            catch (Exception ex)
            {
                throw new MB.RuleBase.Exceptions.DatabaseExecuteException(" 数据执行SQL 语句 出错！", ex);
            }
            finally
            {
                dbCmd.Dispose();
            }
            return val;
        }
        /// <summary>
        ///  执行返回一个DataSet集合。
        /// </summary>
        /// <param name="db">操作数据库对象</param>
        /// <param name="dbCmd">数据操作的dbCommand</param>
        /// <returns>DataSet 数据格式的 数据集</returns>
        public DataSet ExecuteDataSet(Database db, DbCommand dbCmd)
        {
            return ExecuteDataSet(db, dbCmd, (DbTransaction)null);
        }
        /// <summary>
        ///  执行返回一个DataSet集合。
        /// </summary>
        /// <param name="db">操作数据库对象</param>
        /// <param name="dbCmd">数据操作的dbCommand</param>
        /// <param name="transaction"></param>
        /// <returns>>DataSet 数据格式的 数据集</returns>
        public DataSet ExecuteDataSet(Database db, DbCommand dbCmd, DbTransaction transaction)
        {
            DataSet ds = new DataSet();
            int rowCounts = -1;
            try
            {
                string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, dbCmd);
                MB.Util.TraceEx.Write("正在执行:" + cmdMsg);

                //设定查询行为
                var qh = _QueryBehavior;
                if (qh == null) qh = MessageHeaderHelper.GetQueryBehavior();
                if (qh != null && qh.IsQueryAll) qh = null;  //如果允许查询全部,则给出全部值
                


                //增加动态列 2012-08-21 add by aifang begin
                resetDynamicColumns(dbCmd, qh);
                //增加动态列 2012-08-21 add by aifang end

                using (new Util.MethodTraceWithTime(true, cmdMsg))
                {
                    bool isTotolPageDisplayed = (qh == null ? false : qh.IsTotalPageDisplayed);

                    if (isTotolPageDisplayed)
                    {
                        DataSet dsTemp = new DataSet();

                        dsTemp = transaction == null ? db.ExecuteDataSet(dbCmd) : db.ExecuteDataSet(dbCmd, transaction);
                        if (dsTemp.Tables.Count > 0) {
                            //在这种情况下，返回的是全部记录数的数量
                            rowCounts = dsTemp.Tables[0].Rows.Count;
                        }

                        if (qh != null)
                        {
                            ds = dsTemp.Clone();
                            int start = qh.PageIndex * qh.PageSize;
                            for (int i = 0; i < rowCounts; i++)
                            {
                                //小于起始位置的行
                                if (i < start) continue;
                                
                                //大于当前页的最大记录
                                if (i >= (start + qh.PageSize)) break;


                                DataRow dr = ds.Tables[0].NewRow();
                                for (int colIndex = 0; colIndex < ds.Tables[0].Columns.Count; colIndex++)
                                {
                                    dr[colIndex] = dsTemp.Tables[0].Rows[i][colIndex];
                                }

                                ds.Tables[0].Rows.Add(dr);
                            }
                        }
                        else
                            ds = dsTemp;
                    }
                    else
                    {
                        if (qh != null)
                        {
                            int start = qh.PageIndex * qh.PageSize;
                            int returnedCount = 0;
                            if (qh.PageSize < Int32.MaxValue) {
                                //这里判断一下，是否是INT32的最大值，以防止整数的溢出
                                returnedCount = qh.PageSize + 1;
                                ds = db.ExecuteDataSet(dbCmd, transaction, start, returnedCount);
                            }
                            else
                                ds = db.ExecuteDataSet(dbCmd, transaction, start, qh.PageSize);
                                

                            //在这里需要多返回一条记录，客户端用来判断是否有下一页
                            if (ds.Tables.Count > 0) {
                                rowCounts = ds.Tables[0].Rows.Count;
                                //清除多返回的记录
                                if (returnedCount > 0 && rowCounts == returnedCount) {
                                    ds.Tables[0].Rows.RemoveAt(rowCounts - 1);
                                }
                            }
                            
                        }
                        else
                        {
                            ds = transaction == null ? db.ExecuteDataSet(dbCmd) : db.ExecuteDataSet(dbCmd, transaction);
                            if (ds.Tables.Count > 0)
                                rowCounts = ds.Tables[0].Rows.Count; //这里返回的也是全部记录
                        }
                    }
                }

                if (qh != null && qh.PageSize != Int32.MaxValue) {
                    //如果有些语句不想分页的，要么不设置QueryBehavior或者把PageSize设置为MaxValue，则不传分页信息到客户端
                    MessageHeaderHelper.AppendMessageHeaderResponse(new ResponseHeaderInfo(rowCounts), true);
                    DataBaseQueryResult.Instance.SetTotalRows(rowCounts);
                }
            }
            catch (Exception ex)
            {
                throw new MB.RuleBase.Exceptions.DatabaseExecuteException(" 数据执行SQL 语句 出错！", ex);
            }
            finally
            {
                dbCmd.Dispose();
            }
            return ds;
        }
        /// <summary>
        ///  根据SQl 字符窜 执行相应的数据库操作。
        /// </summary>
        /// <param name="sqlString">需要进行操作的SQL 拼接字符窜</param>
        /// <param name="sqlParams">MB.Orm.DbSql.SqlParamInfo 集合类型的参数 </param>
        /// <returns>DataSet 数据格式的 数据集</returns>
        public DataSet ExecuteDataSet(string sqlString, List<MB.Orm.DbSql.SqlParamInfo> sqlParams)
        {
            return ExecuteDataSet(sqlString, sqlParams, (DbTransaction)null);
        }
        /// <summary>
        ///  根据SQl 字符窜 执行相应的数据库操作。
        /// </summary>
        /// <param name="sqlString">需要进行操作的SQL 拼接字符窜</param>
        /// <param name="sqlParams">MB.Orm.DbSql.SqlParamInfo 集合类型的参数 </param>
        /// <param name="transaction"></param>
        /// <returns>DataSet 数据格式的 数据集</returns>
        public DataSet ExecuteDataSet(string sqlString, List<MB.Orm.DbSql.SqlParamInfo> sqlParams, DbTransaction transaction)
        {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();

            var dbCmd = db.GetSqlStringCommand(sqlString);

            dbCmd.CommandText = _PersistenceHelper.ReplaceSqlParamsByDatabaseType(db, dbCmd.CommandText);

            if (sqlParams != null && sqlParams.Count > 0)
            {
                foreach (var parInfo in sqlParams)
                {
                    _PersistenceHelper.AddParamInfoToDbCommand(db, dbCmd, parInfo);
                }
            }
            return ExecuteDataSet(db, dbCmd, transaction);
        }

        /// <summary>
        /// 基于DbTransaction事务中的数据库操作
        /// add by aifang
        /// </summary>
        /// <param name="invokeMethod"></param>
        public void ExecuteWithTransaction(Action<System.Data.Common.DbTransaction> invokeMethod)
        {
            try
            {
                var db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
                using (var con = db.CreateConnection())
                {
                    con.Open();
                    using (var tran = con.BeginTransaction())
                    {
                        var error = false;
                        try
                        {
                            invokeMethod(tran);
                        }
                        catch (Exception ex)
                        {
                            error = true;
                            MB.Util.TraceEx.Write(ex.Message);
                            throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, string.Empty);
                        }
                        finally
                        {
                            try
                            {
                                if (error)
                                {
                                    tran.Rollback();
                                    con.Close();
                                    con.Dispose();
                                }
                                else
                                    tran.Commit();
                            }
                            catch { }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write(ex.Message);
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, string.Empty);
            }
        }
    }
}
