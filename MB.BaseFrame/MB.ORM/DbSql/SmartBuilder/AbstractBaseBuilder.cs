//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-08-02
// Description	:	 
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MB.Orm.DB;
using MB.Orm.Persistence;

namespace MB.Orm.DbSql.SmartBuilder
{
    /// <summary>
    /// AbstractBaseBuilder
    /// </summary>
    internal abstract class AbstractBaseBuilder
    {
        protected BuilderData BuilderData {
            get;
            set;
        }
        protected ActionsHandler Actions {
            get;
            set;
        }

        public AbstractBaseBuilder(Type entityType) {
            BuilderData = new BuilderData(entityType);
            Actions = new ActionsHandler(BuilderData);
        }

        public int Execute() {
            return ExecuteReturnValue<int>((db, cmd) => db.ExecuteNonQuery(cmd));
        }

        protected TReturn ExecuteReturnValue<TReturn>(Func<Database, DbCommand, TReturn> executeFunc) {
            try {
                var sqlGenerator = CreateSqlGenerator();
                var db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
                var dbType = MB.Orm.Persistence.DatabaseConfigurationScope.GetDatabaseType(db);
                var persistence = new PersistenceManagerHelper();
                var sqlStr = sqlGenerator.Create(db);
                var dbCmd = persistence.GetSqlStringCommand(db, sqlStr.SqlStr);
                foreach (var par in sqlStr.ParamFields) {
                    var val = persistence.ConvertToRealDbValue(db, par.Value, par.DbType);
                    PersistenceManagerHelper.NewInstance.AddParamInfoToDbCommand(db, dbCmd, par, par.Value);
                }

                string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, dbCmd);
                MB.Util.TraceEx.Write("正在执行:" + cmdMsg);

                //if (DatabaseConnectionContext.Current != null && DatabaseConnectionContext.Current.Connection != null)
                //    dbCmd.Connection = DatabaseConnectionContext.Current.Connection;
                using (new Util.MethodTraceWithTime(true, cmdMsg))
                {
                    return executeFunc(db, dbCmd);
                }
            }
            catch (MB.Util.APPException appEx) {
                throw appEx;
            }
            catch (Exception ex) {
                throw new MB.Util.APPException("通过SmartBuilder 执行数据库操作有误,请检查语法是否正确" + ex.Message, Util.APPMessageType.SysDatabaseInfo);
            }
        }
        protected virtual ISmartSqlGenerator CreateSqlGenerator() {
            throw new MB.Util.APPException("CreateSqlGenerator 没有实现", MB.Util.APPMessageType.SysErrInfo);
        }
    }
}
