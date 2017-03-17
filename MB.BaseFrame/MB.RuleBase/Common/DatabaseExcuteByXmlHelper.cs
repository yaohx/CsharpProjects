//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-04-10
// Description	:	通过XML 配置文件执行相应的数据库操作。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using MB.Orm.Common;
using MB.Orm.DbSql;
using MB.Orm.Enums;
using MB.Orm.Mapping;
using MB.Orm.Persistence;
using MB.RuleBase.Atts;
using MB.RuleBase.IFace;
using MB.Util.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Transactions;
using System.ComponentModel;

namespace MB.RuleBase.Common
{
    /// <summary>
    /// 通过XML 配置文件执行相应的数据库操作。
    /// [Transaction(TransactionOption.Required)]  
    /// </summary>
    public class DatabaseExcuteByXmlHelper
    {
        private MB.Orm.Persistence.PersistenceManagerHelper _PersistenceManager;
        private MB.RuleBase.Common.DatabaseExecuteHelper _DatabaseExecte;
        private QueryBehavior _QueryBehavior;

        private string[] _CommandTextFormatValues;
        /// <summary>
        /// 兼容老版本而增加的
        /// </summary>
        public DatabaseExcuteByXmlHelper() {
        }
        /// <summary>
        ///  通过XML 配置文件执行相应的数据库操作。
        /// </summary>
        public DatabaseExcuteByXmlHelper(string[] commandTextFormatValues) {
            _CommandTextFormatValues = commandTextFormatValues;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryBehavior"></param>
        public DatabaseExcuteByXmlHelper(QueryBehavior queryBehavior)
        {
            _QueryBehavior = queryBehavior;
        }
        /// <summary>
        /// 返回新的对象实例。
        /// </summary>
        public static DatabaseExcuteByXmlHelper NewInstance {
            get {
                return new DatabaseExcuteByXmlHelper(new string[]{});
            }
        }

        #region List<T> GetObjectsByXml<T>...

        /// <summary>
        /// 根据XML 文件名称 和SQL 名称以及指定的过滤参数获取对应的。
        /// </summary>
        /// <typeparam name="T">获取得到返回值的数据类型</typeparam>
        /// <param name="parsMapping">参数Mapping 的信息</param>
        /// <param name="xmlFileName">SQL 语句所在的Xml 文件名称</param>
        /// <param name="sqlName">SQL 语句定义的名称</param>
        /// <param name="parInfos">QueryParameterInfo 数组格式的参数</param>
        /// <param name="parValues">SQL 参数值</param>
        /// <returns>指定类型的集合类</returns>
        public List<T> GetObjectsByXml<T>(QueryParameterMappings parsMapping, string xmlFileName, string sqlName, 
                                                                        QueryParameterInfo[] parInfos, params object[] parValues) {
            try {
                QueryParameterMappings queryMappings = parsMapping;
                if (queryMappings == null)
                    queryMappings = MB.Orm.Mapping.Xml.SqlConfigHelper.Instance.GetSqlQueryParamMappings(xmlFileName, sqlName);

                string sqlFilter = MB.Orm.DbSql.SqlShareHelper.Instance.QueryParametersToSqlString(queryMappings, parInfos);

                List<object> pars = new List<object>();
                if (parValues != null && parValues.Length > 0) {
                    foreach (object p in parValues)
                        pars.Add(p);
                }
                pars.Add(sqlFilter);

                return GetObjectsByXml<T>(xmlFileName, sqlName, pars.ToArray());
            }
            catch (Exception ex) {
                throw new MB.Util.APPException("执行GetObjectsByXml<T> 出错", MB.Util.APPMessageType.SysDatabaseInfo, ex);
            }
        }
        /// <summary>
        /// 根据XML 文件名称 和SQL 名称以及指定的过滤参数获取对应的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlFileName"></param>
        /// <param name="sqlName"></param>
        /// <param name="parValues"></param>
        /// <returns></returns>
        public List<T> GetObjectsByXml<T>(string xmlFileName, string sqlName, params object[] parValues) {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            return GetObjectsByXml<T>(db,xmlFileName, sqlName, parValues);
        }
        /// <summary>
        ///  根据XML 文件名称 和SQL 名称以及指定的过滤参数获取对应的
        /// </summary>
        /// <typeparam name="T">获取得到返回值的数据类型</typeparam>
        /// <param name="xmlFileName">SQL 语句所在的Xml 文件名称</param>
        /// <param name="sqlName">SQL 语句定义的名称</param>
        /// <param name="parValues">SQL 参数值</param>
        /// <returns>指定类型的集合类</returns>
        public List<T> GetObjectsByXml<T>(Database db, string xmlFileName, string sqlName, params object[] parValues) {
            try {
                Type entityType = typeof(T);
                return GetObjectsByXml<T>(db,(DbTransaction)null, entityType, xmlFileName, sqlName, parValues);
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(ex);
            }
        }
        /// <summary>
        /// 根据XML 文件名称 和SQL 名称以及指定的过滤参数获取对应的
        /// </summary>
        /// <typeparam name="T">获取得到返回值的数据类型</typeparam>
        /// <param name="xmlFileName">SQL 语句所在的Xml 文件名称</param>
        /// <param name="sqlName">>SQL 语句定义的名称</param>
        /// <param name="parValues">SQL 参数值</param>
        /// <returns>指定类型的集合类</returns>
        public List<T> GetObjectsByXml<T>(Type cfgEntityType, string xmlFileName, string sqlName, params object[] parValues) {
            return GetObjectsByXml<T>((DbTransaction)null, cfgEntityType, xmlFileName, sqlName, parValues);
        }
        /// <summary>
        /// 根据XML 文件名称 和SQL 名称以及指定的过滤参数获取对应的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transaction"></param>
        /// <param name="cfgEntityType"></param>
        /// <param name="xmlFileName"></param>
        /// <param name="sqlName"></param>
        /// <param name="parValues"></param>
        /// <returns></returns>
        public List<T> GetObjectsByXml<T>(DbTransaction transaction, Type cfgEntityType, string xmlFileName, string sqlName, params object[] parValues) {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            return GetObjectsByXml<T>(db, transaction, cfgEntityType, xmlFileName, sqlName, parValues);
        }
        /// <summary>
        /// 根据XML 文件名称 和SQL 名称以及指定的过滤参数获取对应的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transaction"></param>
        /// <param name="cfgEntityType"></param>
        /// <param name="xmlFileName"></param>
        /// <param name="sqlName"></param>
        /// <param name="parValues"></param>
        /// <returns></returns>
        public List<T> GetObjectsByXml<T>(Database db,DbTransaction transaction, Type cfgEntityType, string xmlFileName, string sqlName, params object[] parValues) {
           

            if (_PersistenceManager == null)
                _PersistenceManager = new PersistenceManagerHelper(_CommandTextFormatValues);

            System.Data.Common.DbCommand[] cmds = _PersistenceManager.CreateDbCommandByXml(db,
                                                  xmlFileName, sqlName, parValues);

            if (cmds.Length != 1)
                throw new MB.RuleBase.Exceptions.SelectSqlXmlConfigException(xmlFileName, sqlName);

            System.Data.Common.DbCommand cmd = cmds[0];

            Type entityType = cfgEntityType;
           
            return new DatabaseExecuteHelper(_QueryBehavior).GetObjects<T>(cfgEntityType,transaction,db,cmd);
        }

        #endregion List<T> GetObjectsByXml<T>...

        #region GetDataSetByXml...
        /// <summary>
        ///  通过Xml 配置的语句获取数据集。
        /// </summary>
        /// <param name="xmlFileName">SQL 语句所在的Xml 文件名称</param>
        /// <param name="sqlName">SQL 语句定义的名称</param>
        /// <param name="parInfos">QueryParameterInfo 数组格式的参数</param>
        /// <returns>指定类型的集合类</returns>
        public DataSet GetDataSetByXmlParams(string xmlFileName, string sqlName, QueryParameterInfo[] parInfos) {
            return GetDataSetByXmlParams((DbTransaction)null, xmlFileName, sqlName, parInfos);
        }
        /// <summary>
        ///  通过Xml 配置的语句获取数据集。
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="xmlFileName">SQL 语句所在的Xml 文件名称</param>
        /// <param name="sqlName">SQL 语句定义的名称</param>
        /// <param name="parInfos">QueryParameterInfo 数组格式的参数</param>
        /// <returns>指定类型的集合类</returns>
        public DataSet GetDataSetByXmlParams(DbTransaction transaction, string xmlFileName, string sqlName, QueryParameterInfo[] parInfos) {
            QueryParameterMappings queryMappings = MB.Orm.Mapping.Xml.SqlConfigHelper.Instance.GetSqlQueryParamMappings(xmlFileName, sqlName);
            string sqlFilter = MB.Orm.DbSql.SqlShareHelper.Instance.QueryParametersToSqlString(queryMappings, parInfos);
            return GetDataSetByXml(transaction, xmlFileName, sqlName, sqlFilter);
        }
        /// <summary>
        /// 通过Xml 配置的语句获取数据集。
        /// </summary>
        /// <param name="xmlFileName">SQL 语句所在的Xml 文件名称</param>
        /// <param name="sqlName">SQL 语句定义的名称</param>
        /// <param name="parValues">SQL 参数值</param>
        /// <returns>指定类型的集合类</returns>
        public DataSet GetDataSetByXml(string xmlFileName, string sqlName, params object[] parValues) {
            return GetDataSetByXml((DbTransaction)null, xmlFileName, sqlName, parValues);
        }
        /// <summary>
        /// 通过Xml 配置的语句获取数据集
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="xmlFileName"></param>
        /// <param name="sqlName"></param>
        /// <param name="parValues"></param>
        /// <returns></returns>
        public DataSet GetDataSetByXml(DbTransaction transaction, string xmlFileName, string sqlName, params object[] parValues) {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            return GetDataSetByXml(db, transaction, xmlFileName, sqlName, parValues);
        }
        /// <summary>
        /// 通过Xml 配置的语句获取数据集。
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="xmlFileName">SQL 语句所在的Xml 文件名称</param>
        /// <param name="sqlName">SQL 语句定义的名称</param>
        /// <param name="parValues">SQL 参数值</param>
        /// <returns>指定类型的集合类</returns>
        public DataSet GetDataSetByXml(Database db, DbTransaction transaction, string xmlFileName, string sqlName, params object[] parValues) {
           

            MB.Util.TraceEx.Write("开始执行  MB.Orm.Persistence.PersistenceManagerHelper.NewInstance.CreateDbCommandByXml ");

            System.Data.Common.DbCommand[] cmds = null;
            try {
                if (_PersistenceManager == null)
                    _PersistenceManager = new PersistenceManagerHelper(_CommandTextFormatValues);

                cmds = _PersistenceManager.CreateDbCommandByXml(db, xmlFileName, sqlName, parValues);
            }
            catch (Exception exx) {
                throw new MB.Util.APPException(exx);
            }
            if (cmds.Length != 1)
                throw new MB.RuleBase.Exceptions.SelectSqlXmlConfigException(xmlFileName, sqlName);

            System.Data.Common.DbCommand cmd = cmds[0];

            string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmd);
            MB.Util.TraceEx.Write("正在执行:" + cmdMsg);
            DataSet ds = new DataSet();
            try {
                using (new Util.MethodTraceWithTime(true, cmdMsg))
                {
                    ds = new DatabaseExecuteHelper(_QueryBehavior).ExecuteDataSet(db, cmd, transaction);
                }

            }
            catch (Exception ex) {
                throw new MB.RuleBase.Exceptions.DatabaseExecuteException("执行GetDataSetByXml 出错！", ex);
            }
            finally {
                foreach (DbCommand td in cmds)
                    td.Dispose();
            }
            return ds;
        }

        #endregion GetDataSetByXml...

        #region ExecuteScalar...
        /// <summary>
        /// ExecuteScalar
        /// 执行返回单个值，如果需要返回集合类或者DataSet 请调用 GetObjects 或者  GetDataSet 。
        /// </summary>
        /// <param name="xmlFileName">SQL 语句所在的Xml 文件名称</param>
        /// <param name="sqlName">SQL 语句定义的名称</param>
        /// <param name="parValues">SQL 参数值</param>
        /// <returns>object 类型的返回值</returns>
        public object ExecuteScalar(string xmlFileName, string sqlName, params object[] parValues) {
            return ExecuteScalar((DbTransaction)null, xmlFileName, sqlName, parValues);
        }
        /// <summary>
        /// 执行返回单个值，如果需要返回集合类或者DataSet 请调用 GetObjects 或者  GetDataSet 。
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="xmlFileName">SQL 语句所在的Xml 文件名称</param>
        /// <param name="sqlName">SQL 语句定义的名称</param>
        /// <param name="parValues">SQL 参数值</param>
        /// <returns>object 类型的返回值</returns>
        public object ExecuteScalar(DbTransaction transaction, string xmlFileName, string sqlName, params object[] parValues) {
            return ExecuteScalar<object>(transaction, xmlFileName, sqlName, parValues);
        }
        /// <summary>
        ///  ExecuteScalar
        ///  返回指定类型的单个值，如果需要返回集合类或者DataSet 请调用 GetObjects 或者  GetDataSet 。
        /// </summary>
        /// <param name="xmlFileName">SQL 语句所在的Xml 文件名称</param>
        /// <param name="sqlName">SQL 语句定义的名称</param>
        /// <param name="parValues">SQL 参数值</param>
        /// <returns>指定类型的返回值</returns>
        public T ExecuteScalar<T>(string xmlFileName, string sqlName, params object[] parValues) {
            return ExecuteScalar<T>(null, xmlFileName, sqlName, parValues);
        }
        /// <summary>
        /// 返回指定类型的单个值，如果需要返回集合类或者DataSet 请调用 GetObjects 或者  GetDataSet 。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transaction"></param>
        /// <param name="xmlFileName"></param>
        /// <param name="sqlName"></param>
        /// <param name="parValues"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(DbTransaction transaction, string xmlFileName, string sqlName, params object[] parValues) {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            return ExecuteScalar<T>(db, transaction, xmlFileName, sqlName, parValues);
        }
        /// <summary>
        /// 返回指定类型的单个值，如果需要返回集合类或者DataSet 请调用 GetObjects 或者  GetDataSet 。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transaction"></param>
        /// <param name="xmlFileName">SQL 语句所在的Xml 文件名称</param>
        /// <param name="sqlName">SQL 语句定义的名称</param>
        /// <param name="parValues">SQL 参数值</param>
        /// <returns>指定类型的返回值</returns>
        public T ExecuteScalar<T>(Database db, DbTransaction transaction, string xmlFileName, string sqlName, params object[] parValues) {
           

            if (_PersistenceManager == null)
                _PersistenceManager = new PersistenceManagerHelper(_CommandTextFormatValues);

            System.Data.Common.DbCommand[] cmds = _PersistenceManager.CreateDbCommandByXml(db,
                                                  xmlFileName, sqlName, parValues);

            if (cmds.Length != 1)
                throw new MB.RuleBase.Exceptions.SelectSqlXmlConfigException(xmlFileName, sqlName);
            System.Data.Common.DbCommand cmd = cmds[0];
            string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmd);
            MB.Util.TraceEx.Write("正在执行:" + cmdMsg);
            object re = null;
            try {
                using (new Util.MethodTraceWithTime(true, cmdMsg))
                {
                    re = transaction == null ? db.ExecuteScalar(cmd) : db.ExecuteScalar(cmd, transaction);
                }
            }
            catch (Exception ex) {
                throw new MB.RuleBase.Exceptions.DatabaseExecuteException("执行ExecuteScalar 出错！", ex);
            }
            finally {
                foreach (DbCommand td in cmds)
                    td.Dispose();
            }
            if (re == null || re == System.DBNull.Value)
                return default(T);
            else {
                return (T)System.Convert.ChangeType(re, typeof(T));
            }
        }
        #endregion ExecuteScalar...

        #region ExecuteNonQuery...
        /// <summary>
        /// 直接通过实体来执行数据库的存储操作。
        /// 特别说明：如果需要根据EntityState 的状态来进行相应的操作请使用ObjectEditHelper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmFileName"></param>
        /// <param name="sqlName"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int ExecuteNonQueryByEntity<T>(string xmFileName, string sqlName, T entity) where T : MB.Orm.Common.BaseModel {
            List<T> lst = new List<T>();
            lst.Add(entity);
            return ExecuteNonQueryByEntity<T>(null, xmFileName, sqlName, lst);
        }
        /// <summary>
        /// 直接通过实体来执行数据库的存储操作。
        /// 特别说明：如果需要根据EntityState 的状态来进行相应的操作请使用ObjectEditHelper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transaction"></param>
        /// <param name="xmFileName"></param>
        /// <param name="sqlName"></param>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public int ExecuteNonQueryByEntity<T>(DbTransaction transaction, string xmFileName, string sqlName, IList<T> entitys)
             where T : MB.Orm.Common.BaseModel {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            return ExecuteNonQueryByEntity<T>(db, transaction, xmFileName, sqlName, entitys);
        }
        /// <summary>
        /// 直接通过实体来执行数据库的存储操作。
        /// 特别说明：如果需要根据EntityState 的状态来进行相应的操作请使用ObjectEditHelper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transaction">可以为空，为空将使用TransactionScope</param>
        /// <param name="xmFileName"></param>
        /// <param name="sqlName"></param>
        /// <param name="entitys">集合里面只能存在一种操作状态的实体</param>
        /// <returns></returns>
        public int ExecuteNonQueryByEntity<T>(Database db, DbTransaction transaction, string xmFileName, string sqlName, IList<T> entitys) 
                                                                                        where T : MB.Orm.Common.BaseModel {
             
              List<DbCommand> dbCmds = new List<DbCommand>();
              foreach (T et in entitys) {
                  if (_PersistenceManager == null)
                      _PersistenceManager = new PersistenceManagerHelper(_CommandTextFormatValues);

                  var cmds = _PersistenceManager.GetDbCommand(db, et as MB.Orm.Common.BaseModel, xmFileName, sqlName);
                  dbCmds.AddRange(cmds);
              }

              int re = 0;
              try {
                  if (_DatabaseExecte == null)
                      _DatabaseExecte = new DatabaseExecuteHelper();

                  re = transaction == null ? _DatabaseExecte.ExecuteNonQuery(db, dbCmds.ToArray()) : _DatabaseExecte.ExecuteNonQuery(db, dbCmds.ToArray(), transaction);
              }
              catch (Exception ex) {
                  throw new MB.RuleBase.Exceptions.DatabaseExecuteException("执行ExecuteNonQueryByEntity<T> 出错！", ex);
              }
              finally {
                  foreach (DbCommand td in dbCmds)
                      td.Dispose();
              }

              return re;
        }
        /// <summary>
        /// 执行数据操作,返回受影响的行数。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="sqlName"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public int ExecuteNonQueryBySqlParams(string xmlFileName, string sqlName, List<SqlParamInfo> sqlParams) {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();

            return ExecuteNonQueryBySqlParams(db, xmlFileName, sqlName, sqlParams);
        }
        /// <summary>
        /// 执行数据操作,返回受影响的行数。
        /// </summary>
        /// <param name="xmlFileName">SQL 语句所在的Xml 文件名称</param>
        /// <param name="sqlName">SQL 语句定义的名称</param>
        /// <param name="sqlParams">SQL 参数</param>
        /// <returns>返回最后执行语句受影响的行数</returns>
        public int ExecuteNonQueryBySqlParams(Database db, string xmlFileName, string sqlName, List<SqlParamInfo> sqlParams) {
            
            if (_PersistenceManager == null)
                _PersistenceManager = new PersistenceManagerHelper(_CommandTextFormatValues);

            System.Data.Common.DbCommand[] cmds = _PersistenceManager.CreateDbCommandBySqlParams(db,
                                                  xmlFileName, sqlName, sqlParams);

            int re = 0;
            try {
                if (_DatabaseExecte == null)
                    _DatabaseExecte = new DatabaseExecuteHelper();

                re = _DatabaseExecte.ExecuteNonQuery(db, cmds);
            }
            catch (Exception ex) {
                throw new MB.RuleBase.Exceptions.DatabaseExecuteException("执行ExecuteNonQueryBySqlParams 出错！", ex);
            }
            finally {
                foreach (DbCommand td in cmds)
                    td.Dispose();
            }

            return re;
        }
        /// <summary>
        /// ExecuteNonQuery
        /// 执行数据操作,返回受影响的行数。
        /// </summary>
        /// <param name="xmlFileName">SQL 语句所在的Xml 文件名称<</param>
        /// <param name="sqlName">SQL 语句定义的名称</param>
        /// <param name="parValues">SQL 参数值</param>
        /// <returns>返回最后执行语句受影响的行数</returns>
        public int ExecuteNonQuery(string xmlFileName, string sqlName, params object[] parValues) {
            return ExecuteNonQuery((DbTransaction)null, xmlFileName, sqlName, parValues);
        }
        /// <summary>
        /// 执行数据操作,返回受影响的行数。
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="xmlFileName"></param>
        /// <param name="sqlName"></param>
        /// <param name="parValues"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(DbTransaction transaction, string xmlFileName, string sqlName, params object[] parValues) {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            return ExecuteNonQuery(db, transaction, xmlFileName, sqlName, parValues);
        }
        /// <summary>
        /// 执行数据操作,返回受影响的行数。
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="xmlFileName">SQL 语句所在的Xml 文件名称<</param>
        /// <param name="sqlName">SQL 语句定义的名称</param>
        /// <param name="parValues">SQL 参数值</param>
        /// <returns>返回最后执行语句受影响的行数</returns>
        public int ExecuteNonQuery(Database db, DbTransaction transaction, string xmlFileName, string sqlName, params object[] parValues) {

            if (_PersistenceManager == null)
                _PersistenceManager = new PersistenceManagerHelper(_CommandTextFormatValues);

            System.Data.Common.DbCommand[] cmds = _PersistenceManager.CreateDbCommandByXml(db,
                                                  xmlFileName, sqlName, parValues);

            int re = 0;
            try {
                if (_DatabaseExecte == null)
                    _DatabaseExecte = new DatabaseExecuteHelper();

                re = transaction == null ? _DatabaseExecte.ExecuteNonQuery(db, cmds) : _DatabaseExecte.ExecuteNonQuery(db, cmds, transaction);
            }
            catch (Exception ex) {
                throw new MB.RuleBase.Exceptions.DatabaseExecuteException("执行ExecuteNonQuery 出错！", ex);
            }
            finally {
                foreach (DbCommand td in cmds)
                    td.Dispose();
            }

            return re;
        }
        #endregion ExecuteNonQuery...


        /// <summary>
        /// 模拟ORACEL的Merge into数据库操作
        /// 将ORACLE的Merge into分为三个步骤
        /// 1. 得到需要merge到最终表的数据源，需要与最终表做一个外链接，并在数据集中取出最终表的KEY值
        /// 2. 将得到的数据源根据最终表的ID是否有值来区分出insert与update的数据集
        /// 3. 批量执行insert和update
        /// 使用该方法的人需要提供select(得到数据源)，insert和update的SQL语句，并指定最终表
        /// </summary>
        /// <param name="mergeIntoPara">执行merge into操作需要的参数</param>
        /// <param name="paras">在SQL中的实际参数</param>
        public void MergeInto<T>(MergrIntoParameter mergeIntoPara, params Object[] paras)
        {
            if (string.IsNullOrEmpty(mergeIntoPara.KeyColumn))
                mergeIntoPara.KeyColumn = "ID";
            List<T> sources = this.GetObjectsByXml<T>(
                mergeIntoPara.SqlFileName, mergeIntoPara.SelectSourceSqlName, paras);

            //如果得到的需要被mergeinto的数据源是空的，则结束mergeInto操作
            if (sources == null || sources.Count <= 0)
                return;

            List<T> insertedSources = new List<T>();
            List<T> updatedSources = new List<T>();
            foreach (T source in sources)
            {
                int id = MB.Util.MyReflection.Instance.InvokePropertyForGet<int>(source, mergeIntoPara.KeyColumn);
                if (id > 0)
                    updatedSources.Add(source);
                else
                    insertedSources.Add(source);
            }

            using (TransactionScope scope = new TransactionScope())
            {
                using (MB.RuleBase.BulkCopy.IDbBulkExecute bulk = MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateDbBulkExecute())
                {
                    if (insertedSources.Count > 0)
                    {
                        int identityStartValue = MB.Orm.Persistence.EntityIdentityHelper.NewInstance.GetEntityIdentity(mergeIntoPara.TargetTableName, insertedSources.Count);
                        insertedSources.ForEach(t =>
                        {
                            MB.Util.MyReflection.Instance.InvokePropertyForSet(t, mergeIntoPara.KeyColumn, identityStartValue++);
                        });
                        bulk.WriteToServer(mergeIntoPara.SqlFileName, mergeIntoPara.InsertSqlName, insertedSources);

                    }
                    if (updatedSources.Count > 0)
                        bulk.WriteToServer(mergeIntoPara.SqlFileName, mergeIntoPara.UpdateSqlName, updatedSources);

                }
                
                scope.Complete();
            }
        }

        /// <summary>
        /// 显示传入transaction的 mergeInto, 功能与transactionscope的merge into相一致
        /// </summary>
        public void MergeInto<T>(DbTransaction transaction, MergrIntoParameter mergeIntoPara, params Object[] paras)
        {
            MergeInto<T>(null, transaction, mergeIntoPara, paras);
        }

        /// <summary>
        /// 显示传入transaction的 mergeInto, 功能与transactionscope的merge into相一致
        /// </summary>
        public void MergeInto<T>(Database db, DbTransaction transaction, MergrIntoParameter mergeIntoPara, params Object[] paras)
        {
            if (string.IsNullOrEmpty(mergeIntoPara.KeyColumn))
                mergeIntoPara.KeyColumn = "ID";
            List<T> sources = null;

            if (db == null)
                sources = this.GetObjectsByXml<T>(transaction, typeof(T), mergeIntoPara.SqlFileName, mergeIntoPara.SelectSourceSqlName, paras);
            else
                sources = this.GetObjectsByXml<T>(db, transaction, typeof(T), mergeIntoPara.SqlFileName, mergeIntoPara.SelectSourceSqlName, paras);

            //如果得到的需要被mergeinto的数据源是空的，则结束mergeInto操作
            if (sources == null || sources.Count <= 0)
                return;

            List<T> insertedSources = new List<T>();
            List<T> updatedSources = new List<T>();
            foreach (T source in sources)
            {
                int id = MB.Util.MyReflection.Instance.InvokePropertyForGet<int>(source, mergeIntoPara.KeyColumn);
                if (id > 0)
                    updatedSources.Add(source);
                else
                    insertedSources.Add(source);
            }

            MB.RuleBase.BulkCopy.IDbBulkExecute bulk = null;
            using (bulk = MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateDbBulkExecute(transaction))
            {
                try
                {
                    if (insertedSources.Count > 0)
                    {
                        int identityStartValue = MB.Orm.Persistence.EntityIdentityHelper.NewInstance.GetEntityIdentity(mergeIntoPara.TargetTableName, insertedSources.Count);
                        insertedSources.ForEach(t =>
                        {
                            MB.Util.MyReflection.Instance.InvokePropertyForSet(t, mergeIntoPara.KeyColumn, identityStartValue++);
                        });
                        bulk.WriteToServer(mergeIntoPara.SqlFileName, mergeIntoPara.InsertSqlName, insertedSources);

                    }
                    if (updatedSources.Count > 0)
                        bulk.WriteToServer(mergeIntoPara.SqlFileName, mergeIntoPara.UpdateSqlName, updatedSources);
                }
                catch (Exception ex)
                {
                    throw new MB.RuleBase.Exceptions.DatabaseExecuteException("执行MergeInto 出错！", ex);
                }
            }


        }

       
    }

    /// <summary>
    /// 模拟Merge Into所需要的参数
    /// </summary>
    public class MergrIntoParameter
    {
        /// <summary>
        /// Merge Into的目标表
        /// </summary>
        public string TargetTableName { get; set; }
        /// <summary>
        /// SQL语句文件名
        /// </summary>
        public string SqlFileName { get; set; }
        /// <summary>
        /// 步骤一查询语句的SQL名
        /// </summary>
        public string SelectSourceSqlName { get; set; }
        /// <summary>
        /// 步骤二插入语句的SQL名
        /// </summary>
        public string InsertSqlName { get; set; }
        /// <summary>
        /// 步骤三更新语句的SQL名
        /// </summary>
        public string UpdateSqlName { get; set; }
        /// <summary>
        /// 主键名称，可以不填，默认不填的时候为"ID"
        /// </summary>
        [DefaultValue("ID")]
        public string KeyColumn { get; set; }
    }
}
