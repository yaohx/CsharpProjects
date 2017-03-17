using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MB.Orm.DB
{
    /// <summary>
    /// cloud database 操作范围...
    /// </summary>
    public class OperationDatabaseScope : IDisposable
    {
        #region 变量定义...
        [ThreadStatic]
        private static OperationDatabaseScope _CurrentScope;

        private OperationDatabaseContext _CurrentContext;
        private bool _Disposed;
        private readonly OperationDatabaseContext _OriginalContext = OperationDatabaseContext.Current;
        private readonly OperationDatabaseScope _OriginalScope = OperationDatabaseScope._CurrentScope;
        private readonly Thread _Thread = Thread.CurrentThread;
        //判断是否从
        private bool _RefrenceFromParent;
        #endregion

        #region constructer...
        /// <summary>
        /// constructor.
        /// </summary>
        public OperationDatabaseScope() :
            this(new OperationDatabaseContext(false)) {
        }
        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="readOnly"></param>
        public OperationDatabaseScope(bool readOnly) :
            this(new OperationDatabaseContext(readOnly)) {
        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="dbName"></param>
        public OperationDatabaseScope(string dbName) :
            this(new OperationDatabaseContext(dbName)) {
        }
        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="context"></param>
        public OperationDatabaseScope(OperationDatabaseContext context) {
            if (OperationDatabaseContext.Current != null && context!=null)
            {
                //由于存在事务临时表，确保相同的多次读在同一个库中进行，
                if (OperationDatabaseContext.Current.ReadOnly && context.ReadOnly)
                {
                    _RefrenceFromParent = true;
                }
            }
            if (!_RefrenceFromParent)
                this.pushContext(context);
        }
        #endregion

        #region Dispose...
        /// <summary>
        /// 
        /// </summary>
        public void Dispose() {
            if (!this._Disposed) {
                this._Disposed = true;
                if (!_RefrenceFromParent)
                    this.popContext();
            }
        }
        #endregion

        #region privater function ...
        private void pushContext(OperationDatabaseContext context) {
            this._CurrentContext = context;
            OperationDatabaseScope._CurrentScope = this;
            OperationDatabaseContext.Current = _CurrentContext;
        }
        private void popContext() {
            if (this._Thread != Thread.CurrentThread)
                throw new MB.Util.APPException("在使用数据库范围时,返回出现线程不一致", Util.APPMessageType.SysErrInfo);
            
            if (OperationDatabaseScope._CurrentScope != this)
                throw new MB.Util.APPException("在使用数据库范围时,返回出现数据库范围不一致", Util.APPMessageType.SysErrInfo);
            
            if (OperationDatabaseContext.Current != this._CurrentContext)
                throw new MB.Util.APPException("在使用数据库范围时,返回出现数据库配置不一致", Util.APPMessageType.SysErrInfo);

            OperationDatabaseScope._CurrentScope = _OriginalScope;
            OperationDatabaseContext.Current = _OriginalContext;

        }
        #endregion
    }
}
