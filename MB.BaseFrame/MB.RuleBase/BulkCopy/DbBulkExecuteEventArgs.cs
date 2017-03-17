using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.RuleBase.BulkCopy {
    /// <summary>
    /// 批量处理执行过程中的事件。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="arg"></param>
   public delegate void DbBulkExecuteEventHandle(object sender, DbBulkExecuteEventArgs arg);
    /// <summary>
    /// 批量处理执行过程中的参数。
    /// </summary>
    public class DbBulkExecuteEventArgs {
        private long _RowsExecuted;
        private bool _Abort;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowsExecuted"></param>
        public DbBulkExecuteEventArgs(long rowsExecuted) {
            RowsExecuted = rowsExecuted;
        }
        /// <summary>
        /// 判断是否终止。
        /// </summary>
        public bool Abort {
            get {
                return _Abort;
            }
            set {
                _Abort = value;
            }
        }
        /// <summary>
        /// 已经
        /// </summary>
        public long RowsExecuted { 
            get{
                return _RowsExecuted;
            }
            set {
                _RowsExecuted = value;
            }
        }
    }
}
