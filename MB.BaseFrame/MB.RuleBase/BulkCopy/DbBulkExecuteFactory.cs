//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2010-03-21
// Description	:	数据库批量处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MB.RuleBase.BulkCopy {
    /// <summary>
    ///  数据库批量处理。
    /// </summary>
    public class DbBulkExecuteFactory {

        /// <summary>
        /// 兼容老版本而增。
        /// </summary>
        /// <returns></returns>
        public static IDbBulkExecute CreateDbBulkExecute() {
            return CreateDbBulkExecute(null, new string[]{});
        }
        /// <summary>
        /// 根据当前数据库类型创建批量处理业务类。
        /// 目前只支持Oracle 和 SqlServer。
        /// </summary>
        /// <returns></returns>
        public static IDbBulkExecute CreateDbBulkExecute(string[] commandTextFormatValues) {
            return CreateDbBulkExecute(null, commandTextFormatValues);
        }
        /// <summary>
        /// 兼容老版本而增加
        /// </summary>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        public static IDbBulkExecute CreateDbBulkExecute(System.Data.IDbTransaction dbTransaction) {
            return CreateDbBulkExecute(dbTransaction, new string[] { });
        }
        /// <summary>
        /// 根据当前数据库类型创建批量处理业务类。
        /// 目前只支持Oracle 和 SqlServer。
        /// </summary>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        public static IDbBulkExecute CreateDbBulkExecute(System.Data.IDbTransaction dbTransaction, string[] commandTextFormatValues) {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            var dbType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            if (dbType == MB.Orm.Enums.DatabaseType.Oracle) {
                return new OracleBulkExecute(dbTransaction, commandTextFormatValues);
            }
            else if (dbType == MB.Orm.Enums.DatabaseType.MSSQLServer) {
                return new SqlServerBulkExecute(dbTransaction, commandTextFormatValues);
            }
            else {
                throw new MB.Util.APPException(string.Format("当前数据库类型{0} 还没有支持批量处理", dbType.ToString()), MB.Util.APPMessageType.SysErrInfo);
            }
        }
        /// <summary>
        /// 通过db.ConnectionString 返回Oracle 的数据库配置字符窜。
        /// </summary>
        /// <param name="cnStr"></param>
        /// <returns></returns>
        public static string CreateOracleCnString(string cnStr) {

            return (new OracleBulkExecute()).CreateOracleCnString(cnStr);
        }
    }
}
