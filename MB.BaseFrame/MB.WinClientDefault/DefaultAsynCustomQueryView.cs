using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.WinBase.IFace;
using MB.Util;
using MB.WinBase;
using MB.WinBase.Common;
using MB.WinClientDefault.CustomUserControl;

namespace MB.WinClientDefault {
    /// <summary>
    /// 异步查询窗口，在ERP主窗体中加载
    /// </summary>
    public partial class DefaultAsynCustomQueryView : AbstractBaseForm, IViewGridForm, IViewGridFormWithGreatData {

        public static readonly string QUERY_REFRESH_MSG_ID = "7C29B91A-1658-4428-AABF-925229A5EA47";
        private ICustomViewControl _CustomViewControl; //自定义呈现数据的控件
        private IAsynClientQueryRule _AsynQueryRule;
        private ICustomQueryViewRule _CustomQueryViewRule; //自定义控件窗口需要对应的业务类需要实现的接口
        private MB.Util.Model.QueryParameterInfo[] _CurrentQueryParameters;
        private DataSet _CurrentQueryData;
        private double _ExcuteTime;

        //查询全部数据的设置
        private bool _IsQueryAll = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DefaultAsynCustomQueryView() {
            InitializeComponent();
        }


        private void DefaultAsynCustomQueryView_Load(object sender, EventArgs e) {

            MB.WinBase.AppMessenger.DefaultMessenger.Subscribe<string>(QUERY_REFRESH_MSG_ID, o => {
                labTitleMsg.Text = o;
            });
            //获取业务类默认设置的过滤条件参数
            _CurrentQueryParameters = _AsynQueryRule.GetDefaultFilterParams();
            loadObjectData(_CurrentQueryParameters);
        }

        #region IViewGridForm Members

        public bool IsTotalPageDisplayed {
            get;
            set;
        }

        public int Save() {
            throw new NotImplementedException();
        }

        public int AddNew() {
            throw new NotImplementedException();
        }

        public int CopyAsNew() {
            throw new NotImplementedException();
        }

        public int Open() {
            throw new NotImplementedException();
        }

        public int Delete() {
            throw new NotImplementedException();
        }

        public int Query() {
            try {
                showQueryParamterInput();
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                return -1;
            }
            return 1;
        }

        public new int Refresh() {
            using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                try {
                    if (_CurrentQueryParameters != null && _CurrentQueryParameters.Length > 0)
                        loadObjectData(_CurrentQueryParameters);
                }
                catch (Exception ex) {
                    MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                    return -1;
                }
                return 1;
            }
        }

        public int DataImport() {
            throw new NotImplementedException();
        }

        public object GetCurrentMainGridView(bool mustEditGrid) {
            return null;
        }

        public bool ExistsUnSaveData() {
            return false;
        }

        public int DataExport() {
             using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                 try {
                     if (_CustomViewControl != null) {
                         _CustomViewControl.Export();
                     }
                     else
                         return 0;
                 }
                 catch (Exception ex) {
                     MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                     return -1;
                 }
                return 1;
            }
        }

        public int ReloadData() {
            return 0;
        }

        #endregion

        #region IForm Members

        public IClientRuleQueryBase ClientRuleObject {
            get {
                return _AsynQueryRule;
            }
            set {
                _AsynQueryRule = value as IAsynClientQueryRule;
                _CustomQueryViewRule = value as ICustomQueryViewRule;
                if (_CustomQueryViewRule == null) {
                    throw new APPException("DefaultAsynCustomQueryView对应的业务类没有实现接口ICustomQueryViewRule");
                }

            }
        }

        public WinBase.Common.ClientUIType ActiveUIType {
            get { return ClientUIType.AsynViewForm; }
        }

        #endregion

        #region IViewGridFormWithGreatData Members

        public bool IsQueryAll {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region 内部处理函数

        //加载对象数据
        private void loadObjectData(MB.Util.Model.QueryParameterInfo[] queryParams) {
            if (_AsynQueryRule == null)
                throw new MB.Util.APPException("在加载浏览窗口<DefaultAsynCustomQueryView>时 需要配置对应的ClientRule 类！");

            //特殊说明 2009-02-20 在这里需要增加 RefreshLookupDataSource 以便在加载数据ID 时能得到对应的描述信息。
            using (MB.Util.MethodTraceWithTime timeTrack = new MethodTraceWithTime(null)) {
                using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                    using (AsynLoadDataHelper asynCall = new AsynLoadDataHelper(_AsynQueryRule, this.IsTotalPageDisplayed, _IsQueryAll)) {
                        asynCall.WorkerCompleted += new EventHandler<RunWorkerCompletedEventArgs>(asynCall_WorkerCompleted);
                        asynCall.RunLoad(this, queryParams);

                    }
                }
                _ExcuteTime = timeTrack.GetExecutedTimes();
            }
        }

        private void asynCall_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) return;
            if (e.Error != null) {
                System.Threading.Thread.Sleep(100);
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(e.Error);
                System.Threading.Thread.Sleep(100);
                return;
            }
            if (e.Result == null) {
                MB.WinBase.MessageBoxEx.Show("获取数据有误,请重试！");
                return;
            }
            _CurrentQueryData = e.Result as DataSet;
            _CurrentQueryData.Tables[0].TableName = "Table1";
            AbstractClientRuleQuery clientRuleQuery = _AsynQueryRule as AbstractClientRuleQuery;
            var count = _CurrentQueryData == null ? 0 : _CurrentQueryData.Tables[0].Rows.Count;
            var msg = string.Format("查询花费:{0} 毫秒，返回 {1} 记录，查询时间：{2}", _ExcuteTime, count, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            AppMessenger.DefaultMessenger.Publish(QUERY_REFRESH_MSG_ID, msg);
            loadCustomViewControl();
        }

        private void loadCustomViewControl() {
            if (_CustomViewControl == null) {
                if (_CustomQueryViewRule.CustomViewType == CustomViewType.Chart) {
                    _CustomViewControl = new ucChartView();
                }

                Control cusCrl = _CustomViewControl as Control;
                cusCrl.Dock = DockStyle.Fill;
                tPageGeneral.Controls.Add(cusCrl);
            }

            _CustomViewControl.CreateDataBinding(_AsynQueryRule, _CurrentQueryData);
        }


        //************显示查询过滤条件窗口**********************
        private MB.WinBase.IFace.IQueryFilterForm _QueryFilterForm;
        private MB.Util.Model.QueryParameterInfo[] _QueryParamsFromQueryFilterForm;
        private void showQueryParamterInput() {
            MB.Util.Model.ModuleCommandInfo commandInfo = this._AsynQueryRule.ModuleTreeNodeInfo.Commands.Find
                               (o => string.Compare(o.CommandID, MB.BaseFrame.SOD.MODULE_COMMAND_QUERY, true) == 0);

            Form frm = null;

            if (_QueryFilterForm == null) {
                if (commandInfo == null)
                    _QueryFilterForm = new MB.WinClientDefault.QueryFilter.FrmQueryFilterInput(false);
                else {
                    _QueryFilterForm = MB.WinClientDefault.UICommand.UICreateHelper.Instance.CreateQueryFilterForm(commandInfo);
                }
                _QueryFilterForm.ClientRuleObject = _AsynQueryRule;
                _QueryFilterForm.ViewGridForm = this;
                _QueryFilterForm.IniCreateFilterElements();
                _QueryFilterForm.AfterInputQueryParameter += new QueryFilterInputEventHandle(filterForm_AfterInputQueryParameter);
            }


            frm = _QueryFilterForm as Form;
            frm.Text = this.Text;
            frm.ShowDialog(this);
        }
        private void filterForm_AfterInputQueryParameter(object sender, QueryFilterInputEventArgs arg) {
            if (arg.QueryParamters != null && arg.QueryParamters.Length > 0) {
                _QueryParamsFromQueryFilterForm = arg.QueryParamters;
                _CurrentQueryParameters = arg.QueryParamters;
                _AsynQueryRule.CurrentQueryBehavior.PageIndex = 0;
                loadObjectData(arg.QueryParamters);
                _AsynQueryRule.CurrentFilterParams = arg.QueryParamters;
            }
        }

        #endregion


    }
}
