using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinBase.Binding {
    /// <summary>
    /// 数据源绑定是否需要响应对应事件的处理范围。
    /// </summary>
    public class BindingSourceRaiseEventsScope : System.IDisposable {
        private BindingSource _BindingSource;
        private bool _OldRaiseEvent;
        /// <summary>
        /// 数据源绑定是否需要响应对应事件的处理范围。
        /// </summary>
        /// <param name="bindingSource">数据源绑定</param>
        /// <param name="raiseEvents">是否需要响应事件</param>
        public BindingSourceRaiseEventsScope(BindingSource bindingSource, bool raiseEvents) {
            _BindingSource = bindingSource;
            _OldRaiseEvent = bindingSource.RaiseListChangedEvents;
            bindingSource.RaiseListChangedEvents = raiseEvents;
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
                    _BindingSource.RaiseListChangedEvents = _OldRaiseEvent;

                }
                disposed = true;
            }
        }

        ~BindingSourceRaiseEventsScope() {

            Dispose(false);
        }

        #endregion IDisposable...
    }
}
