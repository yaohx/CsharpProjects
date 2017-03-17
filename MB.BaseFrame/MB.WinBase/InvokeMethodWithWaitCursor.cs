using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using MB.WinBase.Ctls;

namespace MB.WinBase
{
    /// <summary>
    /// 
    /// </summary>
    public class InvokeMethodWithWaitCursor : IDisposable
    {
        private WaitCursorDialog _WaitDialog;
        private System.ComponentModel.BackgroundWorker _WorkThread;
        private Action _Exectute;

        /// <summary>
        /// InvokeMethodWithWaitCursor
        /// </summary>
        /// <param name="exectute">需要被执行的操作</param>
        public InvokeMethodWithWaitCursor(Action exectute) : this(exectute, string.Empty)
        {
        }

        /// <summary>
        /// InvokeMethodWithWaitCursor
        /// </summary>
        /// <param name="exectute">需要被执行的操作</param>
        /// <param name="waitingMessage">等待操作过程中显示在客户端的消息</param>
        public InvokeMethodWithWaitCursor(Action exectute, string waitingMessage) {
            _Exectute = exectute;

            _WaitDialog = new WaitCursorDialog(waitingMessage);

            iniAsynWorkThread();
        }

        #region InvokeWithWait...
        /// <summary>
        /// InvokeWithWait.
        /// </summary>
        /// <param name="exectute"></param>
        public static void InvokeWithWait(Action exectute)
        {
            InvokeWithWait(exectute,null, string.Empty);
        }

        /// <summary>
        /// InvokeWithWait.
        /// </summary>
        /// <param name="exectute"></param>
        public static void InvokeWithWait(Action exectute, string waitingMessage) {
            InvokeWithWait(exectute, null, waitingMessage);
        }

        /// <summary>
        /// InvokeWithWait.
        /// </summary>
        /// <param name="exectute"></param>
        /// <param name="winParent"></param>
        public static void InvokeWithWait(Action exectute, IWin32Window winParent) {
            InvokeWithWait(exectute, winParent, string.Empty);
        }

        /// <summary>
        /// InvokeWithWait.
        /// </summary>
        /// <param name="exectute"></param>
        /// <param name="winParent"></param>
        public static void InvokeWithWait(Action exectute,IWin32Window winParent, string waitingMessage)
        {
            using (InvokeMethodWithWaitCursor work = new InvokeMethodWithWaitCursor(exectute, waitingMessage))
            {
                work.Invoke(winParent);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="winParent"></param>
        public void Invoke(IWin32Window winParent)
        {
            _WorkThread.RunWorkerAsync();

            Thread.Sleep(100);

            if (winParent == null && Application.OpenForms.Count > 0)
            {
                var parent = Application.OpenForms[Application.OpenForms.Count - 1];
                if (!parent.Equals(_WaitDialog))
                    winParent = parent;
            }
            if (winParent != null)
            {
                _WaitDialog.ShowDialog(winParent);
            }
            else
            {
                _WaitDialog.ShowDialog();
            }
        }

        #region 内部处理函数...
        //初始化
        private void iniAsynWorkThread()
        {
            _WorkThread = new BackgroundWorker();

            _WorkThread.WorkerReportsProgress = true;
            _WorkThread.WorkerSupportsCancellation = true;
            _WorkThread.DoWork += new DoWorkEventHandler(_WorkThread_DoWork);
            _WorkThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_WorkThread_RunWorkerCompleted);
            _WorkThread.ProgressChanged += new ProgressChangedEventHandler(_WorkThread_ProgressChanged);

        }

        void _WorkThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //在这里设置进度的百份比
        }

        void _WorkThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Threading.Thread.Sleep(100);

            _WaitDialog.Close();
            _WaitDialog = null;
        }

        void _WorkThread_DoWork(object sender, DoWorkEventArgs e)
        {
            _Exectute();
        }
        #endregion

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
                    try
                    {
                        if (_WaitDialog != null)
                            _WaitDialog.Close();

                        _WaitDialog = null;
                        _WorkThread.Dispose();
                    }
                    catch { }
         
                }
                disposed = true;
            }
        }

        ~InvokeMethodWithWaitCursor()
        {

          Dispose(false);
        }

        #endregion IDisposable...
    }
}
