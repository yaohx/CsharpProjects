//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-03-13
// Description	:	数据库操作执行跟踪处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MB.Orm.Persistence {
    /// <summary>
    /// 数据库操作执行跟踪处理相关。
    /// </summary>
   public  class DbCommandExecuteTrack {
       private static readonly string RUN_INFO_RECORD_DB_NAME = "RunInfoRecordDbName";

        #region Instance...
        private static Object _Obj = new object();
        private static DbCommandExecuteTrack _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected DbCommandExecuteTrack() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static DbCommandExecuteTrack Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new DbCommandExecuteTrack();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        /// <summary>
        /// 把DbCommand 转换为可阅的消息文本格式。
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCmd"></param>
        /// <returns></returns>
        public string CommandToTrackMessage(Database db, System.Data.Common.DbCommand dbCmd) {
            string msg = " CommandText:";
            if (db != null && !string.IsNullOrEmpty(db.ConnectionStringWithoutCredentials)) {
                string logDbName = System.Configuration.ConfigurationManager.AppSettings[RUN_INFO_RECORD_DB_NAME];
                if(!string.IsNullOrEmpty(logDbName) && string.Compare(logDbName,"True",true)==0)
                    msg = db.ConnectionStringWithoutCredentials.Split(';')[0] + msg;
            }

            msg += dbCmd.CommandText;
            msg += toCommandParameters(dbCmd);
            return msg;
        }

        #region private function ...
        //转换Command 为可阅的文本格式。
        private string toCommandParameters(System.Data.Common.DbCommand dbCmd) {
            if (dbCmd.Parameters == null || dbCmd.Parameters.Count == 0)
                return "(没有参数)";
            string msg = "(参数:";
            foreach (System.Data.Common.DbParameter par in dbCmd.Parameters) {
                msg += par.ParameterName + "=>";
                if (par.Value == null)
                    msg += "NULL";
                else
                    msg += "'" + par.Value.ToString() + "'";

                if (par.Direction != ParameterDirection.Input)
                    msg += " " + par.Direction.ToString();
                msg += ",";
            }
            msg = msg.Remove(msg.Length - 1, 1);
            msg += ")";
            return msg;
        }
        //把参数拼接为可执行的SQL 格式。
        private string toCommandSqlString(System.Data.Common.DbCommand dbCmd) {
            if (dbCmd.Parameters.Count == 0)
                return dbCmd.CommandText;
            string msg = dbCmd.CommandText;
            foreach (System.Data.Common.DbParameter par in dbCmd.Parameters) {
                string val = par.Value == null ? "NULL" : "'" + par.Value.ToString() + "'";
                msg = msg.Replace(":" + par.ParameterName, val);
            }
            return msg;
        }
       
        #endregion private function ...
 
    }
}
