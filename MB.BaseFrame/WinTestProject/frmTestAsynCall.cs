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
namespace WinTestProject {
    public partial class frmTestAsynCall : DevExpress.XtraEditors.XtraForm {
        public frmTestAsynCall() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            
            using (BackWorkWithWaitDialog worker = new BackWorkWithWaitDialog(new AsyncMethodCallerDelegate( DoWork))) {
                    worker.WorkerCompleted += new EventHandler<RunWorkerCompletedEventArgs>(worker_WorkerCompleted);
                    worker.Invoke(this);

            //while(!isDoing){
            //     Thread.Sleep(100);
            //}
           }
 
        }

        public object DoWork(MB.Util.AsynWorkThread workThread, DoWorkEventArgs e) {
            int sum = 0;
            for (int i = 0; i < 100; i++) {
                sum += i;

                workThread.ReportProgress(0, string.Format("Main thread {0} does some work.",
                Thread.CurrentThread.ManagedThreadId));
                Thread.Sleep(200);
            }
            return sum;
        }
        private bool isDoing;
        void worker_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            var ee = e;
            bool b = e.Cancelled;
        }
        //class myTes : IAsynMethodCaller {
        //    #region IAsynMethodCaller 成员

          

        //    #endregion
        //}

        class MyAstncCall {//: IAsynMethodCaller {
            MB.WinClientDefault.WaitDialogForm _wait;
 
            #region IAsynMethodCaller 成员

            public object DoWork(MB.Util.AsynWorkThread workThread, DoWorkEventArgs e) {
                // The asynchronous method puts the thread id here.

                int threadId;

                // Create an instance of the test class.
                AsyncDemo ad = new AsyncDemo();
                                                                               
                // Create the delegate.
                AsyncMethodCaller caller = new AsyncMethodCaller(ad.TestMethod);
            
                // Initiate the asychronous call.
                IAsyncResult result = caller.BeginInvoke(3000,
                    out threadId, null, null);

                Thread.Sleep(0);
               workThread.ReportProgress(0, string.Format("Main thread {0} does some work.",
                Thread.CurrentThread.ManagedThreadId));

                // Call EndInvoke to wait for the asynchronous call to complete,
                // and to retrieve the results.
                string returnValue = caller.EndInvoke(out threadId, result);

                workThread.ReportProgress(1, string.Format("The call executed on thread {0}, with return value \"{1}\".",
                                          threadId, returnValue));

                e.Result = returnValue;
                 return returnValue; 
            }

            #endregion
        }
    }




    public class AsyncDemo {
        // The method to be executed asynchronously.
        public string TestMethod(int callDuration, out int threadId) {
           // Console.WriteLine("Test method begins.");
            Thread.Sleep(callDuration);
            threadId = Thread.CurrentThread.ManagedThreadId;
            return String.Format("My call time was {0}.", callDuration.ToString());
        }
    }
    // The delegate must have the same signature as the method
    // it will call asynchronously.
    public delegate string AsyncMethodCaller(int callDuration, out int threadId);

    public delegate string AsyncMethodCallerEventhand();

 
}
