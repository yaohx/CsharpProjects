//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-03-13
// Description	:	决定连接哪个配置数据库的连接处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MB.Orm.Enums;
using MB.Orm.DB; 
namespace MB.Orm.Persistence {
    /// <summary>
    /// 决定连接哪个配置数据库的连接处理。
    /// </summary>
    [Obsolete("该类已过期,请使用MB.Orm.DB 中的 OperationDatabaseScope 类代替")]
    public class DatabaseConfigurationScope : System.IDisposable
    {
        private OperationDatabaseContext _OldConfigurationDatabase;
        /// <summary>
        /// 当前范围内调用的数据库连接配置。
        /// 请不要直接对该字段赋值。
        /// </summary>
        //[ThreadStatic]
        //public static string ConfigurationDatabase;

        /// <summary>
        /// 决定连接哪个配置数据库的连接处理。
        /// </summary>
        /// <param name="configurationDatabase"></param>
        public DatabaseConfigurationScope(string configurationDatabase) {
            _OldConfigurationDatabase = OperationDatabaseContext.Current;

            OperationDatabaseContext.Current = new OperationDatabaseContext(configurationDatabase);
        }
        /// <summary>
        /// 创建数据库配置对应的数据类型。
        /// 请 使用 DatabaseHelper.CreateDatabaseType();代替
        /// </summary>
        /// <returns></returns>
        public static MB.Orm.Enums.DatabaseType CreateDatabaseType() {
            return DatabaseHelper.CreateDatabaseType();
        }
        /// <summary>
        /// 根据database 获取对应的数据库类型。
        /// 请 使用 DatabaseHelper.GetDatabaseType(db);代替
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static MB.Orm.Enums.DatabaseType GetDatabaseType(Database db) {
            return DatabaseHelper.GetDatabaseType(db);
        }
        #region IDisposable...
        private bool disposed = false;

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose() {
            Dispose(true);
            //
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    OperationDatabaseContext.Current = _OldConfigurationDatabase;

                }
                disposed = true;
            }
        }

        ~DatabaseConfigurationScope() {

            Dispose(false);
        }

        #endregion IDisposable...
    }
}
