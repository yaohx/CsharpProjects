using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace MB.WcfServiceLocator.ClientChannel {

    /// <summary>
    /// 返回WcfClient
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WcfClientProxyScope<T> : IDisposable {

        private T _TChannnel;

        /// <summary>
        /// construct function...
        /// </summary>
        /// <param name="tChannel">客户端通道</param>
        public WcfClientProxyScope(T tChannel) {
            _TChannnel = tChannel;
            
        }

        /// <summary>
        /// 当前客户端代理
        /// </summary>
        public T ClientProxy {
            get {
                return _TChannnel;
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
                    try {
                        if (ClientProxy != null)
                            ((IClientChannel)ClientProxy).Close();
                    }
                    catch {
                        if (ClientProxy != null)
                            ((IClientChannel)ClientProxy).Abort();
                    }
                }
                disposed = true;
            }
        }

        ~WcfClientProxyScope() {

            Dispose(false);
        }

        #endregion IDisposable...
    }

}
