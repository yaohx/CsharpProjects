//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2010-07-12
// Description	:	添加默认的查询默认返回值最大命中数。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Persistence
{
    /// <summary>
    /// 添加默认的查询默认返回值最大命中数。
    /// </summary>
    public class DbQueryTargetRowCountScope : IDisposable
    {
        private static string TARGET_ROW_COUNT_CFG = "DBQueryTargetRowCount";
        [ThreadStatic] 
        private static int _OldTargetRowCount;

        /// <summary>
        /// 最大命中数。
        /// </summary>
        [ThreadStatic] 
        public static int TargetRowCount;

        /// <summary>
        /// 
        /// </summary>
        static DbQueryTargetRowCountScope() {
            string val = System.Configuration.ConfigurationManager.AppSettings[TARGET_ROW_COUNT_CFG];
            if (!string.IsNullOrEmpty(val))
                TargetRowCount = MB.Util.MyConvert.Instance.ToInt(val);
            else
                TargetRowCount = 0;
        }
        /// <summary>
        /// 设置默认返回的最大命中数。
        /// </summary>
        /// <param name="rowCount">最大命中数。</param>
        public DbQueryTargetRowCountScope(int rowCount) {
            _OldTargetRowCount = TargetRowCount;
            TargetRowCount = rowCount;
        }

        /// <summary>
        /// 获取配置的最大命中数SQL 过滤条件。
        /// </summary>
        /// <param name="dbaseType"></param>
        /// <returns></returns>
        public static string GetTargetRowCountSqlFilter(MB.Orm.Enums.DatabaseType dbaseType) {
            if (TargetRowCount > 0 && dbaseType == Enums.DatabaseType.Oracle) {
                return string.Format("ROWNUM <= {0}", TargetRowCount);
            }
            else {
                return string.Empty;
            }
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
                    TargetRowCount = _OldTargetRowCount;
         
                }
                disposed = true;
            }
        }

        ~DbQueryTargetRowCountScope() {

          Dispose(false);
        }

        #endregion IDisposable...
    }
}
