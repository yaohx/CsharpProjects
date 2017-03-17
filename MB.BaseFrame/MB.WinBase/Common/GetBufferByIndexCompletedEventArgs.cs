using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Common {
    /// <summary>
    ///  异步调用查询分析调用完成响应的参数。
    /// </summary>
    public class GetBufferByIndexCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {

        private byte[] _Results;
        /// <summary>
        /// 异步调用查询分析调用完成响应的参数。
        /// </summary>
        /// <param name="results"></param>
        /// <param name="exception"></param>
        /// <param name="cancelled"></param>
        /// <param name="userState"></param>
        public GetBufferByIndexCompletedEventArgs(byte[] results, System.Exception exception, bool cancelled, object userState)
            : base(exception, cancelled, userState) {
            this._Results = results;
        }
        /// <summary>
        /// 获取的结果
        /// </summary>
        public byte[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return _Results;
            }
        }
    }
}
