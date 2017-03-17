using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.Diagnostics;
using System.Threading;

namespace MB.WcfServiceLocator.ClientChannel {
    /// <summary>
    /// 服务调用环境范围
    /// </summary>
    public sealed class WcfServiceInvokeScope : IDisposable {
        [ThreadStatic]
        private static WcfServiceInvoke _Current;

        /// <summary>
        /// 当前服务调用有效的环境变量信息。
        /// </summary>
        public static WcfServiceInvoke Current {
            get {
                return _Current;
            }
            set {
                _Current = value;
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        public WcfServiceInvokeScope(): this(null) {

        }

        /// <summary>
        /// constructor
        /// </summary>
        public WcfServiceInvokeScope(List<IClientMessageInspector> clientMessageInspector) {
            if (_Current == null)
                _Current = new WcfServiceInvoke();

            _Current.AddClientMessageInspectors(clientMessageInspector);
            _Current.AddRef();
        }

        /// <summary>
        /// 重新进行服务调用性能计数
        /// </summary>
        public static void Reset() {
            if (_Current != null) {
                _Current.RuningWatch.Reset();
            }
        }

        #region IDisposable...

        public void Dispose() {
            if (_Current != null)
                _Current.Dispose();
        }
        #endregion
    }

    /// <summary>
    /// 服务调用环境
    /// </summary>
    public class WcfServiceInvoke : IDisposable {
        private List<IClientMessageInspector> _clientMessageInspector;
        private Stopwatch _RuningWatch;
        private int refCount;

        /// <summary>
        /// ServiceInvokeEnvironment
        /// </summary>
        public WcfServiceInvoke() {
            _clientMessageInspector = new List<IClientMessageInspector>();
            _RuningWatch = Stopwatch.StartNew();
        }
        /// <summary>
        /// 初始化调用的时间
        /// </summary>
        public Stopwatch RuningWatch {
            get {
                return _RuningWatch;
            }
            set {
                _RuningWatch = value;
            }
        }

 
        /// <summary>
        /// ClientMessageInspector
        /// </summary>
        public List<IClientMessageInspector> ClientMessageInspector {
            get {
                return _clientMessageInspector;
            }
        }

        internal bool IsDisposed {
            get {
                return refCount == 0;
            }
        }


        /// <summary>
        /// 增加Scope深度的计数器，每增加一层ServiceInvokeScope,计数器+1
        /// </summary>
        /// <returns></returns>
        public WcfServiceInvoke AddRef() {
            Interlocked.Increment(ref refCount);
            return this;
        }

        /// <summary>
        /// 增加IClientMessageInspector
        /// </summary>
        /// <param name="clientMessageInspectors">client message inspector的实现集合</param>
        public void AddClientMessageInspectors(List<IClientMessageInspector> clientMessageInspectors) {
            if (clientMessageInspectors != null && clientMessageInspectors.Count > 0)
                _clientMessageInspector.AddRange(clientMessageInspectors);
        }

        #region IDisposable...

        /// <summary>
        /// Decrement the reference count and, if refcount is 0, Dispose all
        /// </summary>
        public void Dispose() {
            Dispose(true);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                int count = Interlocked.Decrement(ref refCount);
                if (count == 0) {
                    WcfServiceInvokeScope.Current = null;
                    GC.SuppressFinalize(this);
                }
            }
        }
        #endregion
    }

}
