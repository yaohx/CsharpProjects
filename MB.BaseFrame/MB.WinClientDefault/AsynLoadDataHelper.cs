//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2009-06-08
// Description	:	数据分析处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.ServiceModel.Channels;
using MB.XWinLib.XtraGrid;
using MB.WinBase.Common;
using MB.WcfServiceLocator;
using MB.WinBase; 

namespace MB.WinClientDefault {

    /// <summary>
    /// 在后台线程中异步调用 获取数据的方法。
    /// 结合后台线程的取消优点和异步调用的合理性能优点。
    /// 测试证明调用Microsoft 的异步能得到合理的性能。
    /// 报表查询分析默认的解决方案，主要针对返回值是DataSet 的数据源。
    /// </summary>
    public class AsynLoadDataHelper : IDisposable,MB.WinBase.IFace.IWaitDialogFormHoster {
        #region 变量定义...
        private MB.WinBase.IFace.IAsynClientQueryRule _AsynQueryRule;
        private System.Collections.Generic.SortedList<int, byte[]> _DataList;
        private MB.Util.AsynWorkThread _WorkThread;
        private WaitDialogForm _WaitDialog;
        private MB.WinBase.Common.WorkWaitDialogArgs _WaitProcessState;
        private int _TotalBuffer;
        private int _TotalRecordCount; //执行wcf后call以后，通过消息头返回的所有记录数



        
        private bool _IsTotalPageDisplayed; //是否需要显示最大页数
        private bool _IsQueryAll; //是否查询出所有记录数
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
        ///  在后台线程中异步调用 获取数据的方法。
        /// </summary>
        public AsynLoadDataHelper(MB.WinBase.IFace.IAsynClientQueryRule asynRule, bool isTotalPageDisplayed) : 
            this(asynRule, isTotalPageDisplayed, false) {
        }

        /// <summary>
        ///  在后台线程中异步调用 获取数据的方法。
        /// </summary>
        public AsynLoadDataHelper(MB.WinBase.IFace.IAsynClientQueryRule asynRule, bool isTotalPageDisplayed, bool isQueryAll) {
            _IsTotalPageDisplayed = isTotalPageDisplayed;
            _IsQueryAll = isQueryAll;
            _DataList = new SortedList<int, byte[]>();

            _WorkThread = new MB.Util.AsynWorkThread();
            _AsynQueryRule = asynRule;
            
            _AsynQueryRule.GetBufferByIndexCompleted += new EventHandler<MB.WinBase.Common.GetBufferByIndexCompletedEventArgs>(_AsynQueryRule_GetBufferByIndexCompleted);

            _WaitDialog = new WaitDialogForm(this);
            _WaitDialog.ClickCanceled += new EventHandler(_WaitDialog_ClickCanceled);

            _WaitProcessState = new MB.WinBase.Common.WorkWaitDialogArgs();

            iniAsynWorkThread();
        }

        void _WaitDialog_ClickCanceled(object sender, EventArgs e) {
            cancelLoad();
        }
        #endregion 构造函数...

        #region public 成员...
        /// <summary>
        /// 执行wcf后call以后，通过消息头返回的所有记录数
        /// </summary>
        public int TotalRecordCount {
            get { return _TotalRecordCount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RunLoad(IWin32Window parentControl, MB.Util.Model.QueryParameterInfo[] filterParams) {
            _WaitDialog.ShowWaitForm(parentControl);

            _WorkThread.RunWorkerAsync(filterParams);
        }
        //取消数据加载
        private void cancelLoad() {
            _WorkThread.CancelAsync();
            _AsynQueryRule.WorkerCompleted();
            _WaitProcessState.Processed = true;
        }
        #endregion public 成员...

        #region 对象事件处理相关...
        void _AsynQueryRule_GetBufferByIndexCompleted(object sender, MB.WinBase.Common.GetBufferByIndexCompletedEventArgs e) {
            int index = (int)e.UserState;
            _DataList.Add(index, e.Result);
        }

        private void iniAsynWorkThread() {
            _WorkThread.WorkerReportsProgress = true;
            _WorkThread.WorkerSupportsCancellation = true;
            _WorkThread.DoWork += new DoWorkEventHandler(_WorkThread_DoWork);
            _WorkThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_WorkThread_RunWorkerCompleted);
            _WorkThread.ProgressChanged += new ProgressChangedEventHandler(_WorkThread_ProgressChanged);


        }

        void _WorkThread_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            LoadDataProcessState state = (LoadDataProcessState)e.UserState;

            switch (state) {
                case LoadDataProcessState.BeginInvoke:
                    _WaitProcessState.CurrentProcessContent = "初始化执行...";
                    break;
                case LoadDataProcessState.HasCount:
                    _TotalBuffer = e.ProgressPercentage;
                    _WaitProcessState.CurrentProcessContent = string.Format("需要下载{0}个数据块", _TotalBuffer);
                    break;
                case LoadDataProcessState.HasSingleData:
                    _WaitProcessState.CurrentProcessContent = string.Format("已成功下载{0} of {1}个数据块", e.ProgressPercentage, _TotalBuffer);
                    break;
                case LoadDataProcessState.BeginUnZip:
                    _WaitProcessState.CurrentProcessContent = "正在解压...";
                    break;
                case LoadDataProcessState.BeginDeserialize :
                    _WaitProcessState.CurrentProcessContent = "正在组装数据...";
                    break;
                default:
                    break;
            }

        }
        void _WorkThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            onWorkerCompleted(this, e);
            
            _AsynQueryRule.WorkerCompleted();

            
            _WaitProcessState.Processed = true;

            string queryRefreshTotalPage = GetTotalPageAndCurrentpage(_TotalRecordCount);
            AppMessenger.DefaultMessenger.Publish(DefaultAsynQueryViewForm.QUERY_ASYN_REFRESH_TOTAL_PAGE_ID, queryRefreshTotalPage);

            System.Threading.Thread.Sleep(100);
        }

        void _WorkThread_DoWork(object sender, DoWorkEventArgs e) {
            e.Result = getDataObjects(e);

        }
        #endregion 对象事件处理相关...

        #region IWaitDialogFormHoster 成员

        public MB.WinBase.Common.WorkWaitDialogArgs ProcessState {
            get { return _WaitProcessState; }
        }

        #endregion

        #region 内部处理函数...
        private DataSet getDataObjects(DoWorkEventArgs e) {
            MemoryStream stream = new MemoryStream();
            int count = 0;

            _WorkThread.ReportProgress(0, LoadDataProcessState.BeginInvoke);

            //添加动态列消息头
            XtraGridDynamicHelper.Instance.AppendQueryBehaviorColumns(_AsynQueryRule);

            try {
                //添加动态列消息头信息
                string messageHeaderKey = string.Empty;
                if (_AsynQueryRule.ClientLayoutAttribute.LoadType == ClientDataLoadType.ReLoad)
                {
                    messageHeaderKey = _AsynQueryRule.ClientLayoutAttribute.MessageHeaderKey;
                }
                using (QueryBehaviorScope scope = new QueryBehaviorScope(_AsynQueryRule.CurrentQueryBehavior, messageHeaderKey))
                {
                    QueryBehaviorScope.CurQueryBehavior.IsTotalPageDisplayed = _IsTotalPageDisplayed;
                    QueryBehaviorScope.CurQueryBehavior.IsQueryAll = _IsQueryAll;
                    _AsynQueryRule.BeginRunWorker(e.Argument as MB.Util.Model.QueryParameterInfo[]);
                    if (QueryBehaviorScope.ResponseInfo != null)
                        _TotalRecordCount = QueryBehaviorScope.ResponseInfo.TotalRecordCount;

                }
            }
            catch (Exception ex) {
              //  System.Threading.Thread.Sleep(100);
                throw new MB.Util.APPException("数据库初始化查询条件出错,可能超时引起,请尽可能输入过滤条件以免引起大量数据 (关闭连接通道 可能需要一会时间,请耐心等待)", MB.Util.APPMessageType.DisplayToUser, ex); 
            }
          
           

            count = _AsynQueryRule.GetBufferCount();

            if (count == 0) return null;

            _WorkThread.ReportProgress(count, LoadDataProcessState.HasCount);

            IAsyncResult[] res = new IAsyncResult[count];
            for (int i = 0; i < count; i++) {
                if (e.Cancel) return null;

                res[i] = _AsynQueryRule.BeginGetBufferByIndex(i, null, i);
            }
            for (int i = 0; i < count; i++) {
                if (e.Cancel) return null;

                if (res[i].IsCompleted) {
                    byte[] bts = _AsynQueryRule.EndGetBufferByIndex(res[i]);
                    _DataList.Add(i, bts);

                    _WorkThread.ReportProgress(i, LoadDataProcessState.HasSingleData);
                }
                else {
                    System.Threading.Thread.Sleep(100);
                    //通过这种变态的方式 等待直到接收到数据为止。
                    i--;
                }
            }

            _WorkThread.ReportProgress(0, LoadDataProcessState.BeginUnZip);

            byte[] buffer;
            for (int i = 0; i < count; i++) {
                if (e.Cancel) return null;

                stream.Write(_DataList[i], 0, _DataList[i].Length);
            }

            stream.Position = 0;
            buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();
            //解压压缩流 edit by cdc 2011-01-04 由于统一在通道上进行解压缩处理，这里不需要
            byte[] bytes = buffer;// MB.Util.Compression.Instance.UnZip(buffer);// Compression(buffer, CompressionMode.Decompress);

            _WorkThread.ReportProgress(0, LoadDataProcessState.BeginDeserialize);

            stream = new MemoryStream(bytes);
            IFormatter formatter = new BinaryFormatter();
            //反序列化
            DataSet ds = (DataSet)formatter.Deserialize(stream);
            stream.Close();
            return ds;
        }

        /// <summary>
        /// 得到 （总共页，当前页）格式以逗号隔开
        /// </summary>
        /// <returns></returns>
        private string GetTotalPageAndCurrentpage(int recordCount)
        {
            int currentPageIndex = _AsynQueryRule.CurrentQueryBehavior.PageIndex;
            int pageSize = _AsynQueryRule.CurrentQueryBehavior.PageSize;
            int totalPageCount = 0;
            if (recordCount > 0)
            {
                totalPageCount = _TotalRecordCount / pageSize;
                if (_TotalRecordCount % pageSize != 0)
                {
                    totalPageCount += 1;
                }
            }
            string queryRefreshTotalPage = string.Join(",",
                new string[2] { totalPageCount.ToString(), 
                                            (recordCount > 0 ? (currentPageIndex + 1).ToString() : "0")});
            return queryRefreshTotalPage;
        }

        #endregion 内部处理函数...

        #region IDisposable 成员

        public void Dispose() {
            try {
                _WaitDialog = null;
                _WorkThread.Dispose();
                _AsynQueryRule.GetBufferByIndexCompleted -= new EventHandler<MB.WinBase.Common.GetBufferByIndexCompletedEventArgs>(_AsynQueryRule_GetBufferByIndexCompleted);
            }
            catch { }
        }

        #endregion

        /// <summary>
        /// 加载数据处理状态。
        /// </summary>
        enum LoadDataProcessState {
            BeginInvoke,
            HasCount,
            HasSingleData,
            BeginUnZip,
            BeginDeserialize,
            Exception
        }
    }
}
