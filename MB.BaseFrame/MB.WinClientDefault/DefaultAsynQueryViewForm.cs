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

using MB.WinBase.IFace;
using MB.WinBase.Atts;
using MB.WinBase.Common;
using MB.WinClientDefault.UICommand;
using MB.Util;
using MB.WinBase;
using MB.XWinLib.XtraGrid;
using MB.WcfServiceLocator;
using MB.WinClientDefault.Extender;
using MB.WinBase.Binding;
using MB.WinClientDefault.DynamicGroup;
namespace MB.WinClientDefault {
    /// <summary>
    /// 查询异步调用分析默认缺省浏览窗口。
    /// 目前只支持 数据类型是DataSet 的数据分析格式。
    /// </summary>
    public partial class DefaultAsynQueryViewForm : AbstractBaseForm, IViewGridForm, IViewGridFormWithGreatData, IViewDynamicGroupGridForm {
        private MB.WinBase.IFace.IAsynClientQueryRule _AsynQueryRule;
        private MB.Util.Model.QueryParameterInfo[] _CurrentQueryParameters;
        private DataSet _CurrentQueryData;
        protected BindingSourceEx _BindingSource;
        private MB.WinBase.IFace.IChartViewControl _ChartViewControl;
        private DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo _HitInfo;
        public static readonly string QUERY_REFRESH_MSG_ID = "B8EDC6C8-67F3-416C-A927-C40CC9CDCDFE";
        public static readonly string QUERY_VIEW_CONTENT_MSG_ID = "24CE9FFF-9B55-487F-ACCD-B5269C365230";
        public static readonly string QUERY_ASYN_REFRESH_TOTAL_PAGE_ID = "74E09995-4F19-41BD-9DC0-C62E22893E55";
        protected bool _IsReloadData;
        private double _ExcuteTime;
        private Dictionary<string, object> _CustomFootSummaryCols; //自定义网格脚汇总

        //动态聚组设置
        protected bool _IsDynamicGroupIsActive = false; //判断是否是动图聚组的view
        protected Util.Model.DynamicGroupSetting _DynamicGroupSettings; //动态聚组的设置，用做查询条件用
        protected FrmDynamicGroupSetting _DynamicGroupSettingUI; //动态聚组设定UI

        //查询全部数据的设置
        private bool _IsQueryAll = false;

        #region 构造函数...
        /// <summary>
        /// DefaultAsynQueryViewForm
        /// </summary>
        public DefaultAsynQueryViewForm()
            : this(null) {
        }
        /// <summary>
        /// DefaultAsynQueryViewForm
        /// </summary>
        /// <param name="asynQueryRule"></param>
        public DefaultAsynQueryViewForm(MB.WinBase.IFace.IAsynClientQueryRule asynQueryRule) {
            InitializeComponent();

            grdCtlMain.Dock = DockStyle.Fill;
            pivGrdCtlMain.Dock = DockStyle.Fill;

            this.labQueryContent.Text = string.Empty;
            this.Load += new EventHandler(DefaultAsynQueryViewForm_Load);
            _AsynQueryRule = asynQueryRule;
            lnkNextPage.Click += new EventHandler(lnkNextPage_Click);
            lnkPreviousPage.Click += new EventHandler(lnkPreviousPage_Click);

            this.dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }

        void lnkPreviousPage_Click(object sender, EventArgs e) {
            _AsynQueryRule.CurrentQueryBehavior.PageIndex -= 1;
            this.Refresh();
        }

        void lnkNextPage_Click(object sender, EventArgs e) {
            _AsynQueryRule.CurrentQueryBehavior.PageIndex += 1;
            this.Refresh();
        }
        #endregion 构造函数...

        void DefaultAsynQueryViewForm_Load(object sender, EventArgs e) {
            if (MB.Util.General.IsInDesignMode()) return;

            if (_AsynQueryRule == null)
                throw new MB.Util.APPException("请检查功能模块节点的业务类配置是否正确", MB.Util.APPMessageType.DisplayToUser);

            MB.WinBase.AppMessenger.DefaultMessenger.Subscribe<string>(QUERY_REFRESH_MSG_ID, o => {
                labTitleMsg.Text = o;
            });
            MB.WinBase.AppMessenger.DefaultMessenger.Subscribe<string>(QUERY_VIEW_CONTENT_MSG_ID, o => {
                labQueryContent.Text = o;
            });
            MB.WinBase.AppMessenger.DefaultMessenger.Subscribe<string>(QUERY_ASYN_REFRESH_TOTAL_PAGE_ID, o =>
            {


                labTotalPageNumber.Visible = this.IsTotalPageDisplayed;
                labCurrentPageNumber.Visible = this.IsTotalPageDisplayed;
                if (this.IsTotalPageDisplayed)
                {
                    string[] totalPage_currentPage = o.Split(',');
                    labTotalPageNumber.Text = string.Format("共{0}页", totalPage_currentPage[0]);
                    labCurrentPageNumber.Text = string.Format("第{0}页", totalPage_currentPage[1]);
                }

            });

            if ((_AsynQueryRule.ClientLayoutAttribute.DataViewStyle & DataViewStyle.AdvBandGrid) != 0) {
                grdCtlMain.ViewCollection.Clear();
                DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView bView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();

                grdCtlMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { bView });
                grdCtlMain.MainView = bView;
                bView.GridControl = grdCtlMain;
                bView.OptionsView.ShowGroupPanel = false;
                bView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridViewMain_RowCellStyle);
            }
            else {
                gridViewMain.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridViewMain_RowCellStyle);
            }
            //获取业务类默认设置的过滤条件参数
            _CurrentQueryParameters = _AsynQueryRule.GetDefaultFilterParams();
            if (this.ClientRuleObject != null && this.ClientRuleObject.OpennedState != null &&
                this.ClientRuleObject.OpennedState.OpennedFrom == ModuleOpennedFrom.Task) {
                    _CurrentQueryParameters = _AsynQueryRule.GetFilterParamsIfOpenFromTask(this.ClientRuleObject.OpennedState.OpenState);
            }

            loadObjectData(_CurrentQueryParameters);

            if ((_AsynQueryRule.ClientLayoutAttribute.DataViewStyle & DataViewStyle.Chart) == 0) {
                tabCtlMain.TabPages.Remove(tPageQAChart);
            }
            if ((_AsynQueryRule.ClientLayoutAttribute.DataViewStyle & DataViewStyle.ModuleComment) == 0) {
                tabCtlMain.TabPages.Remove(tPageModuleComment);
            }
            else {
                MB.WinClientDefault.Ctls.ucBfModuleComment ucModuleComment = new MB.WinClientDefault.Ctls.ucBfModuleComment(this);
                ucModuleComment.Dock = DockStyle.Fill;
                tPageModuleComment.Controls.Add(ucModuleComment);
            }

            if (_AsynQueryRule.ClientLayoutAttribute == null ||
                !_AsynQueryRule.ClientLayoutAttribute.IsDynamicGroupEnabled)
                tabCtlMain.TabPages.Remove(tPageDynamicGroup);
        }

        void gridViewMain_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e) {
            if (e.RowHandle < 0) return;
            MB.WinClientDefault.Extender.IViewFormStyleProvider provider = _AsynQueryRule as MB.WinClientDefault.Extender.IViewFormStyleProvider;
            if (provider != null)
                provider.RowCellStyle(this, grdCtlMain.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView , e);

            var gridView = grdCtlMain.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
            if ((e.RowHandle % 2) != 0 && gridView.FocusedRowHandle != e.RowHandle)
                e.Appearance.BackColor = MB.XWinLib.XtraGrid.GridControlEx.ODD_ROW_BACK_COLOR  ;   
        }

        //加载对象数据
        private void loadObjectData(MB.Util.Model.QueryParameterInfo[] queryParams) {
            if (_AsynQueryRule == null)
                throw new MB.Util.APPException("在加载浏览窗口<DefaultViewForm>时 需要配置对应的ClientRule 类！");

            if (!tabCtlMain.SelectedTab.Equals(tPageDynamicGroup)) {
                //在非动态聚组查询的条件下 

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
            else {

                var dynamicGroupSetting = _DynamicGroupSettings;
                if (dynamicGroupSetting == null)
                    throw new APPException("动态聚组查询，聚组条件不能为空");
                DataSet ds = _AsynQueryRule.GetDynamicGroupQueryData(dynamicGroupSetting, queryParams);
                ucDynamicGroupResultInQuery.BindDynamicResultQueryResult(ds, _AsynQueryRule.ClientLayoutAttribute.UIXmlConfigFile);
            }



        }

        void asynCall_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
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

            _BindingSource = new MB.WinBase.Binding.BindingSourceEx();
            _BindingSource.DataSource = _CurrentQueryData;
            _BindingSource.DataMember = "Table1";
            bool isSame = checkTableStructIsSame(grdCtlMain.DataSource, _BindingSource);

            AbstractClientRuleQuery clientRuleQuery = _AsynQueryRule as AbstractClientRuleQuery;
            if (clientRuleQuery != null &&
                clientRuleQuery.ReSetContextMenu != null &&
                clientRuleQuery.ReSetContextMenu.MenuItems.Count > 0)
                grdCtlMain.ContextMenu = clientRuleQuery.ReSetContextMenu;


            if (grdCtlMain.DataSource == null || !isSame || _IsReloadData)
            {
                Dictionary<string, ColumnPropertyInfo> bindingPropertys = XtraGridDynamicHelper.Instance.GetDynamicColumns(_AsynQueryRule);

                MB.XWinLib.XtraGrid.XtraGridHelper.Instance.BindingToXtraGrid(grdCtlMain, _BindingSource,
                    bindingPropertys,
                    _AsynQueryRule.UIRuleXmlConfigInfo.ColumnsCfgEdit,
                     _AsynQueryRule.ClientLayoutAttribute.UIXmlConfigFile, true);

                var gridView = grdCtlMain.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
                gridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

                //每次重新加载完以后，清空动态列的条件，比避免重复加载
                _IsReloadData = false;

                //加载导出全部的右键菜单
                this.grdCtlMain.ContextMenu.MenuItems.Add("导出全部", new EventHandler(exportAllMenuItem_Click));


            }
            else
                grdCtlMain.DataSource = _BindingSource;

            var count = _CurrentQueryData == null ? 0 : _CurrentQueryData.Tables[0].Rows.Count;
            var msg = string.Format("查询花费:{0} 毫秒，返回 {1} 记录，查询时间：{2}", _ExcuteTime, count, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            AppMessenger.DefaultMessenger.Publish(QUERY_REFRESH_MSG_ID, msg);


            validatedPageControl(count);

            #region  对下一页重新控制
            //首先看消息头返回的记录是否大于页总记录数。消息头的记录数是基于数据库的查询记录数
            //有的情况下，中间层还会做额外的过滤导致返回记录数小于，每页记录数，但其实数据库大于每页记录数
            //如果消息头没有返回记录，则根据客户端判断
            AsynLoadDataHelper asynLoader = sender as AsynLoadDataHelper;
            if (asynLoader != null && !_IsQueryAll) {
                if (asynLoader.TotalRecordCount > 0) {
                    lnkNextPage.Enabled = asynLoader.TotalRecordCount > _AsynQueryRule.CurrentQueryBehavior.PageSize;
                }
            }
            #endregion



            //如果返回的记录数为0，则清空自定义网格汇总信息
            if (count > 0 && 
                _AsynQueryRule.ClientLayoutAttribute != null &&
                _AsynQueryRule.ClientLayoutAttribute.IsCustomFooterSummary)
                //绑定自定义网格脚汇总
                _CustomFootSummaryCols = getCustomSummaryColValues(_QueryParamsFromQueryFilterForm);
        }

        
        //判断两个表结构是否相同
        private bool checkTableStructIsSame(object oldSource,object newSource) {
            
            if (oldSource == null || newSource == null) return false;

            MB.WinBase.Binding.BindingSourceEx oldBinding = grdCtlMain.DataSource as MB.WinBase.Binding.BindingSourceEx;
            MB.WinBase.Binding.BindingSourceEx newBinding = newSource as MB.WinBase.Binding.BindingSourceEx;
            if (oldBinding == null || newBinding == null) return false;

            DataTable oldDt = MB.Util.MyConvert.Instance.ToDataTable(oldBinding.DataSource, string.Empty);
            DataTable newDt = MB.Util.MyConvert.Instance.ToDataTable(newBinding.DataSource, string.Empty);
            if (oldDt == null || newDt == null) return false;
            foreach (DataColumn dc in oldDt.Columns) {
                if (!newDt.Columns.Contains(dc.ColumnName)) return false; 
            }
            foreach (DataColumn dc in newDt.Columns) {
                if (!oldDt.Columns.Contains(dc.ColumnName)) return false; 
            }
            return true;
        }
        //显示对象数据查询窗口。
        private MB.WinBase.IFace.IQueryFilterForm _QueryFilterForm;
        private MB.Util.Model.QueryParameterInfo[] _QueryParamsFromQueryFilterForm;
        private void showQueryParamterInput() {
            MB.Util.Model.ModuleCommandInfo commandInfo = this._AsynQueryRule.ModuleTreeNodeInfo.Commands.Find
                               (o => string.Compare(o.CommandID, MB.BaseFrame.SOD.MODULE_COMMAND_QUERY, true) == 0);

            Form frm = null;
            
            if (_QueryFilterForm == null) {
                if (commandInfo == null)
                    _QueryFilterForm = new MB.WinClientDefault.QueryFilter.FrmQueryFilterInput();
                else {
                    _QueryFilterForm = MB.WinClientDefault.UICommand.UICreateHelper.Instance.CreateQueryFilterForm(commandInfo);
                }
                _QueryFilterForm.ClientRuleObject = _AsynQueryRule;
                _QueryFilterForm.ViewGridForm = this;
                _QueryFilterForm.IniCreateFilterElements();
                _QueryFilterForm.AfterInputQueryParameter += new QueryFilterInputEventHandle(filterForm_AfterInputQueryParameter);
            }

            #region 如果是动态聚组的查询,先要判断是否设置了动态聚组条件，如果没有，强制设置
            if (this._IsDynamicGroupIsActive) {
                if (_DynamicGroupSettings == null) {
                    if (_DynamicGroupSettingUI == null)
                        _DynamicGroupSettingUI = new FrmDynamicGroupSetting(this, _AsynQueryRule);
                    _DynamicGroupSettings = _DynamicGroupSettingUI.GetDynamicGroupSetting();
                }
                Util.Model.DynamicGroupSetting dySetting = _DynamicGroupSettings;
                if (dySetting == null || dySetting.DataAreaFields == null || dySetting.DataAreaFields.Count <= 0) {
                    MB.WinBase.MessageBoxEx.Show("没有设置聚组列和条件，请设置");
                    if (_DynamicGroupSettingUI == null)
                        _DynamicGroupSettingUI = new FrmDynamicGroupSetting(this, _AsynQueryRule, _QueryFilterForm);
                    if (_DynamicGroupSettingUI.ShowDialog() != System.Windows.Forms.DialogResult.OK) {
                        return;
                    }
                }
            }
            #endregion

            frm = _QueryFilterForm as Form;
            frm.Text = this.Text;
            frm.ShowDialog(this);
        }
        void filterForm_AfterInputQueryParameter(object sender, QueryFilterInputEventArgs arg) {
            if (arg.QueryParamters != null && arg.QueryParamters.Length > 0) {
                _QueryParamsFromQueryFilterForm = arg.QueryParamters;
                _CurrentQueryParameters = arg.QueryParamters;
                _AsynQueryRule.CurrentQueryBehavior.PageIndex = 0;
                loadObjectData(arg.QueryParamters);

                _AsynQueryRule.CurrentFilterParams = arg.QueryParamters;
            }
        }
        private void tabCtlMain_SelectedIndexChanged(object sender, EventArgs e) {
            using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                try {
                    this._IsDynamicGroupIsActive = false;
                    
                    if (tabCtlMain.SelectedTab.Equals(tPageMultiView)) {
                        if (_CurrentQueryData == null) {
                            MB.WinBase.MessageBoxEx.Show("请先查询数据,得到数据后才能进行数据多维分析！");
                            tabCtlMain.SelectedIndex = 0;
                            return;
                        }
                        //以后需要判断是否已经加载，同时还要判断数据是否已经发生变化来决定是否需要转换和重新加载
                        Dictionary<string, ColumnPropertyInfo> bindingPropertys = XtraGridDynamicHelper.Instance.GetDynamicColumns(_AsynQueryRule);

                        DataSet dsData = MB.WinBase.ShareLib.Instance.ConvertDataSetToQaDataSet(_CurrentQueryData.Tables[0].DefaultView,
                                                                    bindingPropertys, _AsynQueryRule.UIRuleXmlConfigInfo.ColumnsCfgEdit);
                        
                        if (pivGrdCtlMain.DataSource == null) {
                            MB.XWinLib.PivotGrid.ColPivotList pivColList = MB.XWinLib.Share.XmlCfgHelper.Instance.GetPivotGridCfgData(_AsynQueryRule.ClientLayoutAttribute.UIXmlConfigFile);
                            MB.XWinLib.PivotGrid.PivotGridHelper.Instance.LoadPivotGridData(pivGrdCtlMain, dsData.Tables[0].DefaultView, _AsynQueryRule, pivColList);
                        }
                        else {
                            pivGrdCtlMain.DataSource = dsData.Tables[0].DefaultView;
                        }
                    }
                    else if (tabCtlMain.SelectedTab.Equals(tPageQAChart)) {
                        if (_ChartViewControl == null) {
                            _ChartViewControl = MB.WinClientDefault.Common.ChartAnalyzeHelper.Instance.CreateDefaultChartControl();
                            Control charCtl = _ChartViewControl as Control;
                            charCtl.Dock = DockStyle.Fill;
                            tPageQAChart.Controls.Add(charCtl);
                        }

                        _ChartViewControl.CreateDataBinding(_AsynQueryRule, _CurrentQueryData);
                        _ChartViewControl.RefreshLayout();
                    }
                    else if (tabCtlMain.SelectedTab.Equals(tPageDynamicGroup)) {
                        this._IsDynamicGroupIsActive = true;
                    }

                    MB.WinBase.AppMessenger.DefaultMessenger.Publish(DynamicGroupUIHelper.DYNAMIC_GROUP_ACTIVE_MSG_ID);
                }
                catch (Exception ex) {
                    MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                }
            }
        }


        /// <summary>
        /// 普通视图中自动以网格脚中的内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewMain_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e) {
            if (_AsynQueryRule.ClientLayoutAttribute != null &&
                _AsynQueryRule.ClientLayoutAttribute.IsCustomFooterSummary &&
                _CustomFootSummaryCols != null &&
                _CustomFootSummaryCols.ContainsKey(e.Column.FieldName)) {
                e.Info.DisplayText = _CustomFootSummaryCols[e.Column.FieldName].ToString();
            }
        }

        /// <summary>
        /// 返回需要自定义的汇总列
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> getCustomSummaryColValues(MB.Util.Model.QueryParameterInfo[] queryParams) {
            List<string> colsToGet = new List<string>();
            Dictionary<string, object> customSummaryCols = new Dictionary<string, object>();
            Dictionary<string, ColumnPropertyInfo> bindingPropertys = LayoutXmlConfigHelper.Instance.GetColumnPropertys(_AsynQueryRule.ClientLayoutAttribute.UIXmlConfigFile);
            foreach (ColumnPropertyInfo colInfo in bindingPropertys.Values) {
                if (colInfo.IsCustomFootSummary) {
                    colsToGet.Add(colInfo.Name);
                }
            }

            AbstractClientRuleQuery clientRule = _AsynQueryRule as AbstractClientRuleQuery;
            if (clientRule != null)
                return clientRule.GetCustomSummaryColValues(colsToGet.ToArray(), queryParams);
            else
                return null;
        }

        #region IForm 成员

        public IClientRuleQueryBase ClientRuleObject {
            get {
                return _AsynQueryRule;
            }
            set {
                _AsynQueryRule = value as IAsynClientQueryRule;
            }
        }

        public ClientUIType ActiveUIType {
            get { return ClientUIType.AsynViewForm; }
        }

        #endregion

        #region IViewGridForm 成员

        public bool IsTotalPageDisplayed { get; set; }

        public int AddNew() {
            throw new MB.WinBase.Exceptions.NotAllowImplementedException();
        }

        public int CopyAsNew() {
            throw new MB.WinBase.Exceptions.NotAllowImplementedException();
        }

        public int Open() {
            throw new MB.WinBase.Exceptions.NotAllowImplementedException();
        }

        public int Delete() {
            throw new MB.WinBase.Exceptions.NotAllowImplementedException();
        }

        public virtual int Query() {
            try {
                showQueryParamterInput();
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                return -1;
            }
            return 1;
        }
        public virtual int Refresh() {
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

        public virtual int ReloadData()
        {
            _IsReloadData = true;
            return 0;
        }

        /// <summary>
        /// 数据导入处理。
        /// </summary>
        /// <returns></returns>
        public virtual int DataImport() {
            throw new MB.WinBase.Exceptions.NotAllowImplementedException();  
        }
        /// <summary>
        /// 数据导出处理。
        /// </summary>
        /// <returns></returns>
        public virtual int DataExport() {
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mustEditGrid"></param>
        /// <returns></returns>
        public object GetCurrentMainGridView(bool mustEditGrid) {
            if (mustEditGrid || tabCtlMain.SelectedTab.Equals(tPageGeneral))
                return grdCtlMain;
            else
                return pivGrdCtlMain;
        }

        public int Save() {
            return 0;
        }

        public bool ExistsUnSaveData() {
            return false;
        }
        #endregion


        private void validatedPageControl(int loadCount) {
            if (_IsQueryAll) {
                lnkNextPage.Enabled = false;
                lnkPreviousPage.Enabled = false;
            }
            else {
                lnkNextPage.Enabled = loadCount >= _AsynQueryRule.CurrentQueryBehavior.PageSize;
                lnkPreviousPage.Enabled = _AsynQueryRule.CurrentQueryBehavior.PageIndex > 0;
            }
        }

        private void exportAllMenuItem_Click(object sender, EventArgs e) {
            _AsynQueryRule.CurrentQueryBehavior.PageIndex = 0;
            _AsynQueryRule.CurrentQueryBehavior.PageSize = Int32.MaxValue;

            using (MB.Util.MethodTraceWithTime timeTrack = new MethodTraceWithTime(null)) {
                using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                    using (AsynLoadDataHelper asynCall = new AsynLoadDataHelper(_AsynQueryRule, this.IsTotalPageDisplayed, _IsQueryAll)) {
                        asynCall.WorkerCompleted += new EventHandler<RunWorkerCompletedEventArgs>(asynCall_WorkerCompleted_ExportAll);
                        asynCall.RunLoad(this, _QueryParamsFromQueryFilterForm);

                    }
                }
                double excuteTime = timeTrack.GetExecutedTimes();
                MB.Util.TraceEx.Write("导出全部使用的时间是 (毫秒)： {0}",   excuteTime.ToString());

            }
        }

        void asynCall_WorkerCompleted_ExportAll(object sender, RunWorkerCompletedEventArgs e) {
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



            System.Windows.Forms.SaveFileDialog fileDialog = new System.Windows.Forms.SaveFileDialog();
            fileDialog.Filter = "Excel文件t(.xls)|";
            System.Windows.Forms.DialogResult dResult = fileDialog.ShowDialog();
            if (dResult == System.Windows.Forms.DialogResult.OK) {
                string path = fileDialog.FileName;
                if (!path.EndsWith(".xls")) {
                    path += ".xls";
                }

                MB.WinEIDrive.Export.ExportXls export = new WinEIDrive.Export.ExportXls(path, true);
                export.DataSource = _CurrentQueryData.Tables[0];
                export.Commit();
            }
        }

        #region 双击GRID显示详细信息
        /// <summary>
        /// 双击Grid显示详细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdCtlMain_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (_HitInfo != null && _HitInfo.InRow && _HitInfo.RowHandle >= 0)
                {
                    ShowDetailForm();
                }
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        private void grdCtlMain_MouseMove(object sender, MouseEventArgs e)
        {
            _HitInfo = grdCtlMain.MainView.CalcHitInfo(new Point(e.X, e.Y)) as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo;
        }


        /// <summary>
        /// 双击grid中的信息，打开详细界面
        /// </summary>
        protected virtual void ShowDetailForm()
        {
            //如果异步窗口的UIRule不是从AbstractClientRule继承而来的，则不能双击打开
            if (!(this._AsynQueryRule is IClientRule))
            {
                return;
            }
            MB.Util.Model.ModuleCommandInfo commandInfo = this._AsynQueryRule.ModuleTreeNodeInfo.Commands.Find
                                (o => string.Compare(o.CommandID, MB.BaseFrame.SOD.MODULE_COMMAND_ADD, true) == 0);

            var cInfo = this._AsynQueryRule.ModuleTreeNodeInfo.Commands.Find
                                (o => string.Compare(o.CommandID, MB.BaseFrame.SOD.MODULE_COMMAND_EDIT, true) == 0);
            //如果不存在就去新增加的默认配置
            if (cInfo != null)
                commandInfo = cInfo;

            if (commandInfo == null)
            {
                MB.WinBase.MessageBoxEx.Show(string.Format("模块{0} 的编辑窗口没有配置！", this._AsynQueryRule.ModuleTreeNodeInfo.Name));
                return;
            }

            IExtenderEditForm baseEditForm = null;

            IClientRule clientRule = _AsynQueryRule as IClientRule;
            if (clientRule == null)
            {
                throw new APPException("异步模块如果要打开详细信息，UI Rule必需继承自AbstractClientRule", APPMessageType.DisplayToUser);
            }

            try
            {
                MB.WinBase.IFace.IForm editForm = MB.WinClientDefault.UICommand.UICreateHelper.Instance.CreateObjectEditForm(
                                                            commandInfo, ClientRuleObject as IClientRule, ObjectEditType.OpenReadOnly, _BindingSource);
                Form frm = editForm as Form;

                baseEditForm = editForm as IExtenderEditForm;
                if (baseEditForm != null)
                    baseEditForm.MainBindingGridView = new MB.WinClientDefault.Common.MainViewDataNavigator(gridViewMain);

                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (baseEditForm != null)
                {
                    baseEditForm.DisposeBindingEvent();
                }
            }
        }
        #endregion


        #region IViewDynamicGroupGridForm Members

        public bool IsDynamicGroupIsActive {
            get {
                return _IsDynamicGroupIsActive;
            }
            set {
               _IsDynamicGroupIsActive = value;
            }
        }

        public Util.Model.DynamicGroupSetting DynamicGroupSettingForQuery {
            get {
                return _DynamicGroupSettings;
            }
            set {
                _DynamicGroupSettings = value;
            }
        }

        public void OpenDynamicSettingForm() {
            if (_DynamicGroupSettingUI == null)
                _DynamicGroupSettingUI = new FrmDynamicGroupSetting(this, _AsynQueryRule);
            _DynamicGroupSettingUI.ShowDialog();
        }
        #endregion


        #region IViewGridFormWithGreatData Members

        public bool IsQueryAll {
            get {
                return _IsQueryAll;
            }
            set {
                _IsQueryAll = value;
            }
        }

        #endregion
    }

}
