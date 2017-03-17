using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Threading;

using MB.WinBase;
using MB.WinClientDefault;

namespace MB.WinClientDefault {
    /// <summary>
    /// 方法后台调用的委托
    /// </summary>
    /// <param name="workThread"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate object  AsyncMethodCallerDelegate(MB.Util.AsynWorkThread workThread, DoWorkEventArgs e);
    ///// <summary>
    ///// 在后台线程中调用的类需要实现的接口。
    ///// </summary>
    //public interface IAsynMethodCaller {
    //    object DoWork(MB.Util.AsynWorkThread workThread, DoWorkEventArgs e);
    //}
    /// <summary>
    /// 带有等待窗口的后台方法调用。
    /// </summary>
    public class BackWorkWithWaitDialog : IDisposable, MB.WinBase.IFace.IWaitDialogFormHoster {
        #region 变量定义...
        private AsyncMethodCallerDelegate _AsynCaller;
        private System.Collections.Generic.SortedList<int, byte[]> _DataList;
        private MB.Util.AsynWorkThread _WorkThread;
        private WaitDialogForm _WaitDialog;
        private MB.WinBase.Common.WorkWaitDialogArgs _WaitProcessState;
        private int _TotalBuffer;
        #endregion 变量定义...

        #region 自定义事件处理相关...
        private System.EventHandler<RunWorkerCompletedEventArgs> _WorkerCompleted;
        public event System.EventHandler<RunWorkerCompletedEventArgs> WorkerCompleted {
            add {
                _WorkerCompleted += value;
            }
            remove {
                _WorkerCompleted -= value;
            }
        }
        private void onWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (_WorkerCompleted != null)
                _WorkerCompleted(sender, e);

        }
        #endregion 自定义事件处理相关...

        #region 构造函数...

        /// <summary>
        /// 在后台线程中异步调用 获取数据的方法。
        /// </summary>
        /// <param name="asynCaller"></param>
        /// <param name="showWaitDialog"></param>
        public BackWorkWithWaitDialog(AsyncMethodCallerDelegate asynCaller) {
            _AsynCaller = asynCaller;

            _WorkThread = new MB.Util.AsynWorkThread();

      
                _WaitDialog = new WaitDialogForm(this);
    
            // _WaitDialog.ClickCanceled += new EventHandler(_WaitDialog_ClickCanceled);
            _WaitProcessState = new MB.WinBase.Common.WorkWaitDialogArgs();

            iniAsynWorkThread();
        }

        void _WaitDialog_ClickCanceled(object sender, EventArgs e) {
            cancelLoad();
        }
        #endregion 构造函数...

        #region public 成员...
        /// <summary>
        /// 
        /// </summary>
        /// <param name="winParent"></param>
        public void Invoke(IWin32Window winParent) {
            _WorkThread.RunWorkerAsync();

            Thread.Sleep(100);

            if (winParent==null && Application.OpenForms.Count > 0)
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
 
        #endregion public 成员...

        #region 内部处理函数...
        //取消数据加载
        private void cancelLoad() {
            _WorkThread.CancelAsync();
            _WaitProcessState.Processed = true;

        }
        //初始化
        private void iniAsynWorkThread() {
            _WorkThread.WorkerReportsProgress = true;
            _WorkThread.WorkerSupportsCancellation = true;
            _WorkThread.DoWork += new DoWorkEventHandler(_WorkThread_DoWork);
            _WorkThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_WorkThread_RunWorkerCompleted);
            _WorkThread.ProgressChanged += new ProgressChangedEventHandler(_WorkThread_ProgressChanged);

        }
        #endregion 内部处理函数...

        #region 对象事件处理相关...
        void _WorkThread_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            //在这里设置进度的百份比
            string state = e.UserState.ToString();
            _WaitProcessState.CurrentProcessContent = state;
        }
        void _WorkThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            onWorkerCompleted(sender, e);

            _WaitProcessState.Processed = true;
            System.Threading.Thread.Sleep(100);

            _WaitDialog.Close();
            _WaitDialog = null;
        }

        void _WorkThread_DoWork(object sender, DoWorkEventArgs e) {
            e.Result = _AsynCaller.Invoke(_WorkThread, e);// _AsynCaller.DoWork(_WorkThread, e);
        }
        #endregion 对象事件处理相关...

        #region IWaitDialogFormHoster 成员
        /// <summary>
        /// 处理状态。
        /// </summary>
        public MB.WinBase.Common.WorkWaitDialogArgs ProcessState {
            get {
                return _WaitProcessState;
            }
        }

        #endregion

        #region IDisposable 成员
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose() {
            try {
                if (_WaitDialog != null)
                    _WaitDialog.Close();

                _WaitDialog = null;
                _WorkThread.Dispose();
            }
            catch { }
        }
        #endregion


    }
}
