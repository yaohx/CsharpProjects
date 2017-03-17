using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MB.Util.Monitors
{
    /// <summary>
    /// 与WCF有关的性能观测
    /// </summary>
    public class WcfPerformanceMonitorScope : IDisposable
    {
        #region 变量定义...
        [ThreadStatic]
        private static WcfPerformanceMonitorScope _CurrentScope;

        public static WcfPerformanceMonitorScope CurrentScope
        {
            get { return WcfPerformanceMonitorScope._CurrentScope; }
            set { WcfPerformanceMonitorScope._CurrentScope = value; }
        }



        private bool _Disposed;
        private readonly Thread _Thread = Thread.CurrentThread;
        private readonly WcfPerformaceMonitorContext _OriginalContext = WcfPerformaceMonitorContext.Current;
        private readonly WcfPerformanceMonitorScope _OriginalScope = WcfPerformanceMonitorScope.CurrentScope;

       

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public WcfPerformanceMonitorScope()
        {
            WcfPerformaceMonitorContext context;
            if (_OriginalContext == null)
                context = new WcfPerformaceMonitorContext();
            else
                context = _OriginalContext;

            pushContext(context);
        }


        #region privater function ...
        private void pushContext(WcfPerformaceMonitorContext context)
        {
            WcfPerformanceMonitorScope._CurrentScope = this;
            WcfPerformaceMonitorContext.Current = context;
        }

        private void popContext()
        {
            if (this._Thread != Thread.CurrentThread)
                throw new MB.Util.APPException("在使用性能监测时,返回出现线程不一致", Util.APPMessageType.SysErrInfo);

            if (_OriginalScope == null && _OriginalContext == null)
            {
                if (WcfPerformaceMonitorContext.Current != null)
                {
                    MB.Util.TraceEx.Write(WcfPerformaceMonitorContext.Current.ToString(), APPMessageType.CodeRunInfo);
                }
            }

            WcfPerformanceMonitorScope._CurrentScope = _OriginalScope;
            WcfPerformaceMonitorContext.Current = _OriginalContext;
        }
        #endregion

        #region Dispose...
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (!this._Disposed)
            {
                this._Disposed = true;
                this.popContext();
            }
        }
        #endregion
    }
}
