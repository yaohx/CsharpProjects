//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2010-03-21
// Description	:	数据库批量处理的基础类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Orm.Persistence;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MB.RuleBase.BulkCopy {
    /// <summary>
    /// 数据库批量处理的基础类。
    /// </summary>
    public class AbstractBaseBulk : IDbBulkExecute {
        private const int DEFAULT_BATCH_SIZE = 5000;
        private int _BatchSize;
        private int _BulkCopyTimeout;
        private int _NotifyAfter;
        private System.Data.IDbTransaction _DbTransaction;
        private string[] _CommandTextFormatValues;

        #region 自定义事件相关...
        private DbBulkExecuteEventHandle _SqlRowsCopied;
        /// <summary>
        /// 
        /// </summary>
        public event DbBulkExecuteEventHandle SqlRowsCopied {
            add {
                _SqlRowsCopied += value; 
            }
            remove {
                _SqlRowsCopied -= value;
            }
        }
        protected virtual void onSqlRowsCopied(DbBulkExecuteEventArgs arg) {
            if (_SqlRowsCopied != null)
                _SqlRowsCopied(this, arg);
        }
        #endregion 自定义事件相关...
        /// <summary>
        /// 
        /// </summary>
        public AbstractBaseBulk() : this(null,new string[]{}){
        }

        /// <summary>
        /// 
        /// </summary>
        public AbstractBaseBulk(System.Data.IDbTransaction dbTransaction, string[] commandTextFormatValues) {
            _BatchSize = DEFAULT_BATCH_SIZE;
            _BulkCopyTimeout = 60 * 1000;//一分钟
            _NotifyAfter = DEFAULT_BATCH_SIZE;

            _DbTransaction = dbTransaction;

            _CommandTextFormatValues = commandTextFormatValues;
        }
        /// <summary>
        /// 继承的子类必须要实现的接口。
        /// 把集合中的数据存储到数据库中。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="sqlName"></param>
        /// <param name="lstData">实体集合类或者DataRow[] 数组</param>
        public virtual void WriteToServer(string xmlFileName, string sqlName, System.Collections.IList lstData) {
            throw new NotImplementedException();
        }
 
        /// <summary>
        /// 获取配置的SQL 语句。
        /// </summary>
        /// <param name="db"></param>
        /// <param name="xmlFileName"></param>
        /// <param name="sqlName"></param>
        /// <returns></returns>
        protected MB.Orm.DbSql.SqlString GetXmlSqlString(Database db, string xmlFileName, string sqlName) {
            var dbType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            MB.Orm.DbSql.SqlString[] sqlStrsArray = MB.Orm.Mapping.Xml.SqlConfigHelper.Instance.GetSqlString(xmlFileName, sqlName);
            if (sqlStrsArray == null || sqlStrsArray.Length == 0)
                throw new MB.Orm.Exceptions.XmlSqlConfigNotExistsException(xmlFileName, sqlName);

            if (sqlStrsArray.Length > 1)
                throw new MB.Util.APPException(string.Format("批量处理中XML文件:{0}中SQL:{1} 的配置语句只能有一个", xmlFileName, sqlName), MB.Util.APPMessageType.SysErrInfo);

            if (sqlStrsArray[0].ParamFields == null || sqlStrsArray[0].ParamFields.Count == 0)
                throw new MB.Util.APPException(string.Format("批量处理中XML文件:{0}中SQL:{1} 的参数至少有一个", xmlFileName, sqlName), MB.Util.APPMessageType.SysErrInfo);

            if (_CommandTextFormatValues != null && _CommandTextFormatValues.Length > 0) {
                try {
                    foreach (var sql in sqlStrsArray) {
                        if (sql.SqlStr.IndexOf("{0}") > 0)
                            sql.SqlStr = string.Format(sql.SqlStr, _CommandTextFormatValues);
                    }
                }
                catch (Exception ex) {
                    throw new MB.Util.APPException(string.Format("批量处理中格式化CommandText XML文件:{0}中SQL:{1} 的时出错,请检查! {2}", xmlFileName, sqlName,ex.Message), MB.Util.APPMessageType.SysErrInfo);
                }
            }
            return sqlStrsArray[0];
        }

        #region public 属性...
        /// <summary>
        /// 每次提交的数据块大小。
        /// </summary>
        public int BatchSize {
            get {
                return _BatchSize;
            }
            set {
                _BatchSize = value;
            }
        }
        /// <summary>
        /// 批量处理Timeout时间。
        /// </summary>
        public int BulkCopyTimeout {
            get {
                return _BulkCopyTimeout;
            }
            set {
                _BulkCopyTimeout = value;
            }
        }
        /// <summary>
        /// 处理多少后通知。
        /// </summary>
        public int NotifyAfter {
            get {
                return _NotifyAfter;
            }
            set {
                _NotifyAfter = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Data.IDbTransaction DbTransaction {
            get { return _DbTransaction; }
        }
        #endregion public 属性...

        #region IDisposable 成员

        public virtual void Dispose() {
             
        }

        #endregion
 
    }
}
