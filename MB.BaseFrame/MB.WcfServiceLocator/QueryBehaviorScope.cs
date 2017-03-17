using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Util.Model;

namespace MB.WcfServiceLocator
{
    public class QueryBehaviorScope:IDisposable
    {
        [ThreadStatic]
        public static QueryBehavior CurQueryBehavior;

        [ThreadStatic]
        public static ResponseHeaderInfo ResponseInfo;

        [ThreadStatic]
        private static QueryBehavior _Old;

        [ThreadStatic]
        public static string MessageHeaderKey;
        [ThreadStatic]
        private static string _OldMessageHeaderKey;

        public QueryBehaviorScope(QueryBehavior queryBehavior)
        {
            _Old = CurQueryBehavior;
            CurQueryBehavior = queryBehavior;
        }

        public QueryBehaviorScope(QueryBehavior queryBehavior,string messageHeaderKey)
        {
            _Old = CurQueryBehavior;
            CurQueryBehavior = queryBehavior;

            _OldMessageHeaderKey = MessageHeaderKey;
            MessageHeaderKey = messageHeaderKey;
        }

        #region IDisposable...
        private bool disposed = false;

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    CurQueryBehavior = _Old;
                    MessageHeaderKey = _OldMessageHeaderKey;
                    ResponseInfo = null;
                }
                disposed = true;
            }
        }

        ~QueryBehaviorScope()
        {

            Dispose(false);
        }

        #endregion IDisposable...
    }
}
