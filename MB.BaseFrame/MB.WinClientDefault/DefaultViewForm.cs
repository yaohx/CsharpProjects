//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-17
// Description	:	DefaultViewForm: 对象浏览窗口
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MB.WinBase.IFace;
using MB.WinBase.Atts;
using MB.WinBase.Common;
using MB.WinClientDefault.UICommand;
using MB.Util;
using MB.WinBase;
using MB.WcfServiceLocator;
using MB.XWinLib.XtraGrid;
using MB.XWinLib.Share;
using MB.Util.Model;
using MB.WinClientDefault.DynamicGroup;

namespace MB.WinClientDefault {
    /// <summary>
    /// 对象浏览窗口。
    /// </summary>
    public partial class DefaultViewForm : AbstractListViewForm {

        public static readonly string QUERY_REFRESH_MSG_ID = "1950526C-FF32-4EA9-A85B-3E748F8C72A2";
        public static readonly string QUERY_REFRESH_TOTAL_PAGE_ID = "5A04F5BB-8126-405C-90EA-992AFA04DE0B";
        

        private DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo _HitInfo;
        private MB.WinBase.IFace.IChartViewControl _ChartViewControl;
        private ToolTip _ToolTip;
        private Dictionary<string, object> _CustomFootSummaryCols;//自定义网格脚汇总


        #region 构造函数...
        /// <summary>
        /// 构造函数...
        /// </summary>
        public DefaultViewForm()
            : this(null) {
        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="clientRuleObject"></param>
        public DefaultViewForm(IClientRule clientRuleObject)
            : base(clientRuleObject) {
            InitializeComponent();

            this.Load += new EventHandler(DefaultViewForm_Load);
            grdCtlMain.Dock = DockStyle.Fill;
            pivGrdCtlMain.Dock = DockStyle.Fill;

            _ToolTip = new ToolTip();

            lnkNextPage.Click += new EventHandler(lnkNextPage_Click);
            lnkPreviousPage.Click += new EventHandler(lnkPreviousPage_Click);
        }

        void lnkPreviousPage_Click(object sender, EventArgs e)
        {
            _ClientRuleObject.CurrentQueryBehavior.PageIndex -= 1;
            Refresh();
        }

        void lnkNextPage_Click(object sender, EventArgs e)
        {
            _ClientRuleObject.CurrentQueryBehavior.PageIndex += 1;
            Refresh();
        }

        #endregion 构造函数...

        void DefaultViewForm_Load(object sender, EventArgs e) {
            if (MB.Util.General.IsInDesignMode()) return;

            if (_ClientRuleObject == null)
                throw new MB.Util.APPException("请检查功能模块节点的业务类配置是否正确", MB.Util.APPMessageType.DisplayToUser);


            MB.WinBase.AppMessenger.DefaultMessenger.Subscribe<string>(QUERY_REFRESH_MSG_ID, o => {
                labTitleMsg.Text = o;
            });

            
            //订阅刷新总页数的消息，当返回总页数的时候刷新界面
            MB.WinBase.AppMessenger.DefaultMessenger.Subscribe<string>(QUERY_REFRESH_TOTAL_PAGE_ID, o =>
            {
                labTotalPageNumber.Visible = IsTotalPageDisplayed;
                labCurrentPageNumber.Visible = IsTotalPageDisplayed;
                if (this.IsTotalPageDisplayed)
                {
                    string[] totalPage_currentPage = o.Split(',');

                    labTotalPageNumber.Text = string.Format("共{0}页", totalPage_currentPage[0]);
                    labCurrentPageNumber.Text = string.Format("第{0}页", totalPage_currentPage[1]);

                }

            });




            //获取业务类默认设置的过滤条件参数
            _CurrentQueryParameters = _ClientRuleObject.GetDefaultFilterParams();
            if (this.ClientRuleObject != null && this.ClientRuleObject.OpennedState != null &&
                this.ClientRuleObject.OpennedState.OpennedFrom == ModuleOpennedFrom.Task) {
                    _CurrentQueryParameters = _ClientRuleObject.GetFilterParamsIfOpenFromTask(this.ClientRuleObject.OpennedState.OpenState);
            }

            LoadObjectData(_CurrentQueryParameters);
            grdCtlMain.ValidedDeleteKeyDown = true;
            if (_ClientRuleObject != null) {
                string titleMsg = string.Empty;
                if (string.IsNullOrEmpty(_ClientRuleObject.DefaultFilterMessage))
                    titleMsg = GetDefaultFilterMessage(_ClientRuleObject.ClientLayoutAttribute.DefaultFilter);
                else
                    titleMsg = _ClientRuleObject.DefaultFilterMessage;

                AppMessenger.DefaultMessenger.Publish(QUERY_REFRESH_MSG_ID, titleMsg);
            }

            if ((_ClientRuleObject.ClientLayoutAttribute.DataViewStyle & DataViewStyle.Chart) == 0) {
                tabCtlMain.TabPages.Remove(tPageChart);
            }
            if ((_ClientRuleObject.ClientLayoutAttribute.DataViewStyle & DataViewStyle.ModuleComment) == 0) {
                tabCtlMain.TabPages.Remove(tPageModuleComment);
            }
            else {
                MB.WinClientDefault.Ctls.ucBfModuleComment ucModuleComment = new MB.WinClientDefault.Ctls.ucBfModuleComment(this);
                ucModuleComment.Dock = DockStyle.Fill;
                tPageModuleComment.Controls.Add(ucModuleComment);
            }

            if (_ClientRuleObject.ClientLayoutAttribute == null ||
                !_ClientRuleObject.ClientLayoutAttribute.IsDynamicGroupEnabled)
                tabCtlMain.TabPages.Remove(tPageDynamicGroup);

            

        }

        #region 界面对象事件处理相关...
        private void grdCtlMain_DoubleClick(object sender, EventArgs e) {
            try {
                if (_HitInfo != null && _HitInfo.InRow && _HitInfo.RowHandle >= 0) {
                    Open();
                }
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }

        }
        private void grdCtlMain_MouseMove(object sender, MouseEventArgs e) {
            _HitInfo = gridViewMain.CalcHitInfo(new Point(e.X, e.Y));
        }
        private void tabCtlMain_SelectedIndexChanged(object sender, EventArgs e) {
            using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                try {
                    this._IsDynamicGroupIsActive = false;
                    if (tabCtlMain.SelectedTab.Equals(tPageMultiView)) {
                        if (_BindingSource == null || _BindingSource.DataSource == null || grdCtlMain.DataSource == null) {
                            MB.WinBase.MessageBoxEx.Show("请先查询数据,得到数据后才能进行数据多维分析！");
                            tabCtlMain.SelectedIndex = 0;
                            return;
                        }
                        //以后需要判断是否已经加载，同时还要判断数据是否已经发生变化来决定是否需要转换和重新加载
                        DataSet dsData = MB.WinBase.ShareLib.Instance.ConvertEntityToDataSet((_BindingSource.DataSource as IBindingList),
                                                                    _ClientRuleObject.UIRuleXmlConfigInfo.GetDefaultColumns(), _ClientRuleObject.UIRuleXmlConfigInfo.ColumnsCfgEdit);
                        if (pivGrdCtlMain.DataSource == null) {
                            MB.XWinLib.PivotGrid.ColPivotList pivColList = MB.XWinLib.Share.XmlCfgHelper.Instance.GetPivotGridCfgData(_ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile);

                            MB.XWinLib.PivotGrid.PivotGridHelper.Instance.LoadPivotGridData(pivGrdCtlMain, dsData.Tables[0].DefaultView, _ClientRuleObject, pivColList);
                        }
                        else {
                            pivGrdCtlMain.DataSource = dsData.Tables[0].DefaultView;
                        }
                    }
                    else if (tabCtlMain.SelectedTab.Equals(tPageChart)) {
                        if (_ChartViewControl == null) {
                            _ChartViewControl = MB.WinClientDefault.Common.ChartAnalyzeHelper.Instance.CreateDefaultChartControl();
                            Control charCtl = _ChartViewControl as Control;
                            charCtl.Dock = DockStyle.Fill;
                            tPageChart.Controls.Add(charCtl);
                        }
                        DataSet dsData = MB.WinBase.ShareLib.Instance.ConvertEntityToDataSet((_BindingSource.DataSource as IBindingList),
                                                                   _ClientRuleObject.UIRuleXmlConfigInfo.GetDefaultColumns(), _ClientRuleObject.UIRuleXmlConfigInfo.ColumnsCfgEdit);

                        _ChartViewControl.CreateDataBinding(_ClientRuleObject, dsData);
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

        private void gridViewMain_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e) {
            MB.WinClientDefault.Extender.IViewFormStyleProvider provider = _ClientRuleObject as MB.WinClientDefault.Extender.IViewFormStyleProvider;
            if (provider != null)
                provider.RowCellStyle(this, gridViewMain, e);
            else {
                MB.WinClientDefault.Extender.ViewGridExtenderHelper.CustomDrawRowStyleByDocState(this, gridViewMain, e);
            }
        }

        /// <summary>
        /// 普通视图中自动以网格脚中的内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewMain_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e) {
            if (_ClientRuleObject.ClientLayoutAttribute != null && 
                _ClientRuleObject.ClientLayoutAttribute.IsCustomFooterSummary &&
                _CustomFootSummaryCols != null && _CustomFootSummaryCols.Count > 0 &&  
                _CustomFootSummaryCols.ContainsKey(e.Column.FieldName)) {
                    e.Info.DisplayText = _CustomFootSummaryCols[e.Column.FieldName].ToString();
            }
        }
         
       
        #endregion 界面对象事件处理相关...

        #region 覆盖基类的方法...

        /// <summary>
        /// 除了客户端配置以外加载额外的列，集成的类可以重写该方法
        /// </summary>
        /// <returns>返回需要加载的额外列，这里返回一个空字典</returns>
        protected virtual Dictionary<string, ColumnPropertyInfo> LoadExtraColumns()
        {
            return new Dictionary<string,ColumnPropertyInfo>();
        }

        protected override void LoadObjectData(MB.Util.Model.QueryParameterInfo[] queryParams) {

            if (_ClientRuleObject == null)
                throw new MB.Util.APPException("在加载浏览窗口<DefaultViewForm>时 需要配置对应的ClientRule 类！");

            if (_ClientRuleObject.ClientLayoutAttribute == null)
                throw new MB.Util.APPException(string.Format("对于客户段逻辑类 {0} ,需要配置 RuleClientLayoutAttribute.", _ClientRuleObject.GetType().FullName));

            try {



                //添加右键菜单扩展  add by aifang 2012-07-19 begin
                if (_ClientRuleObject.ReSetContextMenu != null && _ClientRuleObject.ReSetContextMenu.MenuItems.Count > 0)
                {
                    grdCtlMain.ContextMenu = _ClientRuleObject.ReSetContextMenu;
                }
                //end

                gridViewMain.RowHeight = MB.WinBase.LayoutXmlConfigHelper.Instance.GetMainGridViewRowHeight(_ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile);
                using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                    using (MB.Util.MethodTraceWithTime timeTrack = new  MethodTraceWithTime(null)) {

                        if (!tabCtlMain.SelectedTab.Equals(tPageDynamicGroup))
                        {
                            #region 非动态聚组下的查询

                            //特殊说明 2009-02-20 在这里需要增加 RefreshLookupDataSource 以便在加载数据ID 时能得到对应的描述信息。
                            int rowCount = 0;
                            int dbRecordCount = 0;
                            string messageHeaderKey = string.Empty;
                            

                            //添加动态列消息头
                            XtraGridDynamicHelper.Instance.AppendQueryBehaviorColumns(_ClientRuleObject);

                            Dictionary<string, ColumnPropertyInfo> bindingPropertys = XtraGridDynamicHelper.Instance.GetDynamicColumns(_ClientRuleObject);

                            //加载额外的列，通过继承当前窗口有并重写方法LoadExtraColumns的方式去加载
                            Dictionary<string, ColumnPropertyInfo> extraColumns = LoadExtraColumns();
                            if (extraColumns != null && extraColumns.Count > 0)
                            {
                                foreach (KeyValuePair<string, ColumnPropertyInfo> extraCol in extraColumns)
                                {
                                    if (!bindingPropertys.ContainsKey(extraCol.Key))
                                        bindingPropertys.Add(extraCol.Key, extraCol.Value);

                                }
                            }

                            if (_ClientRuleObject.ClientLayoutAttribute.LoadType == ClientDataLoadType.ReLoad)
                            {
                                messageHeaderKey = _ClientRuleObject.ClientLayoutAttribute.MessageHeaderKey;
                            }

                            if (_ClientRuleObject.ClientLayoutAttribute.CommunicationDataType == CommunicationDataType.DataSet)
                            {
                                DataSet dsData = null;
                                try
                                {
                                    using (QueryBehaviorScope scope = new QueryBehaviorScope(_ClientRuleObject.CurrentQueryBehavior, messageHeaderKey))
                                    {
                                        //指定是否需要显示页数
                                        QueryBehaviorScope.CurQueryBehavior.IsTotalPageDisplayed = this.IsTotalPageDisplayed;

                                        dsData = _ClientRuleObject.GetObjectAsDataSet((int)_ClientRuleObject.MainDataTypeInDoc, queryParams);
                                        string queryRefreshTotalPage = getTotalPageAndCurrentpage(dsData.Tables[0].Rows.Count, out dbRecordCount);
                                        AppMessenger.DefaultMessenger.Publish(QUERY_REFRESH_TOTAL_PAGE_ID, queryRefreshTotalPage);
                                        
                                        #region 得到自定义网格页脚汇总列的信息
                                        if (dsData.Tables[0].Rows.Count > 0)
                                            _CustomFootSummaryCols = getCustomSummaryColValues(queryParams);
                                        #endregion
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, " GetObjectAsDataSet 出错！");
                                }
                                rowCount = dsData.Tables[0].Rows.Count;
                                MB.XWinLib.XtraGrid.XtraGridHelper.Instance.BindingToXtraGrid(grdCtlMain, dsData, bindingPropertys,
                                    _ClientRuleObject.UIRuleXmlConfigInfo.ColumnsCfgEdit, _ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile, true);
                            }
                            else
                            {
                                IList lstDatas = null;
                                try
                                {
                                    MB.WinBase.InvokeMethodWithWaitCursor.InvokeWithWait(() =>
                                    {
                                        using (QueryBehaviorScope scope = new QueryBehaviorScope(_ClientRuleObject.CurrentQueryBehavior, messageHeaderKey))
                                        {
                                            //指定是否需要显示页数
                                            QueryBehaviorScope.CurQueryBehavior.IsTotalPageDisplayed = this.IsTotalPageDisplayed;

                                            lstDatas = _ClientRuleObject.GetObjects((int)_ClientRuleObject.MainDataTypeInDoc, queryParams);

                                            string queryRefreshTotalPage = getTotalPageAndCurrentpage(lstDatas.Count, out dbRecordCount);
                                            AppMessenger.DefaultMessenger.Publish(QUERY_REFRESH_TOTAL_PAGE_ID, queryRefreshTotalPage);
                                            #region 得到自定义网格页脚汇总列的信息
                                            if (lstDatas.Count > 0)
                                                _CustomFootSummaryCols = getCustomSummaryColValues(queryParams);
                                            #endregion
                                        }

                                    });
                                }
                                catch (Exception ex)
                                {
                                    throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, " GetObjects 出错！");
                                }
                                rowCount = lstDatas.Count;

                                if (_BindingSource == null || _IsReloadData)
                                {
                                    IBindingList bl = _ClientRuleObject.CreateMainBindList(lstDatas);
                                    _BindingSource = new MB.WinBase.Binding.BindingSourceEx();
                                    _BindingSource.DataSource = bl;

                                    MB.XWinLib.XtraGrid.XtraGridHelper.Instance.BindingToXtraGrid(grdCtlMain, _BindingSource, bindingPropertys,
                                        _ClientRuleObject.UIRuleXmlConfigInfo.ColumnsCfgEdit, _ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile, true);
                                    //每次重新加载完以后，清空动态列的条件，比避免重复加载
                                    this._IsReloadData = false;
                                }
                                else
                                {
                                    MB.XWinLib.XtraGrid.XtraGridHelper.Instance.RefreshDataGrid(grdCtlMain, lstDatas);
                                }
                            }

                            _ClientRuleObject.BindingSource = _BindingSource;

                            var msg = string.Format("查询花费:{0} 毫秒,返回 {1} 记录,查询时间:{2}", timeTrack.GetExecutedTimes(), rowCount, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            AppMessenger.DefaultMessenger.Publish(QUERY_REFRESH_MSG_ID, msg);
                            validatedPageControl(rowCount, dbRecordCount);

                            #endregion
                            
                        }
                        else
                        {
                            var dynamicGroupSetting = _DynamicGroupSettings;
                            if (dynamicGroupSetting == null)
                                throw new APPException("动态聚组查询，聚组条件不能为空");
                            DataSet ds = _ClientRuleObject.GetDynamicGroupQueryData(dynamicGroupSetting, queryParams);
                            ucDynamicGroupResultInQuery.BindDynamicResultQueryResult(ds, _ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile);
                        }
                    }
                }
            }
            catch (MB.Util.APPException aex) {
                throw aex;
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "在浏览窗口Form_Load 时出错！");
            }

            if ((_ClientRuleObject.ClientLayoutAttribute.DataViewStyle & DataViewStyle.Multi) == 0)
                tabCtlMain.TabPages.Remove(tPageMultiView);

        }

        /// <summary>
        /// 从消息头,得到 （总共页，当前页）格式以逗号隔开
        /// </summary>
        /// <param name="recordCount">客户端实际的返回的记录条数</param>
        /// <param name="dbRecordCount">数据库实际有的记录条数，如果与recordCount有差别，则证明中间层做了帅选</param>
        /// <returns></returns>
        private string getTotalPageAndCurrentpage(int recordCount, out int dbRecordCount)
        {
            dbRecordCount = 0;
            int currentPageIndex = this._ClientRuleObject.CurrentQueryBehavior.PageIndex;
            int pageSize = this._ClientRuleObject.CurrentQueryBehavior.PageSize;
            int totalPageCount = 0;
            if (recordCount > 0)
            {
                if (QueryBehaviorScope.ResponseInfo != null)
                {
                    dbRecordCount = QueryBehaviorScope.ResponseInfo.TotalRecordCount;
                    totalPageCount = QueryBehaviorScope.ResponseInfo.TotalRecordCount / pageSize;
                    if (QueryBehaviorScope.ResponseInfo.TotalRecordCount % pageSize != 0)
                    {
                        totalPageCount += 1;
                    }
                }
            }
            string queryRefreshTotalPage = string.Join(",",
                new string[2] { totalPageCount.ToString(), 
                                            (recordCount > 0 ? (currentPageIndex + 1).ToString() : "0")});
            return queryRefreshTotalPage;
        }




        public override MB.WinBase.Common.ClientUIType ActiveUIType {
            get {
                return MB.WinBase.Common.ClientUIType.GridViewForm;
            }
        }
        public override object GetCurrentMainGridView(bool mustEditGrid) {
            if (mustEditGrid || tabCtlMain.SelectedTab.Equals(tPageGeneral))
                return grdCtlMain;
            else
                return pivGrdCtlMain;
        }
        /// <summary>
        /// 继承的子类必须继承的方法
        /// </summary>
        /// <returns></returns>
        protected override MB.WinClientDefault.Common.MainViewDataNavigator GetViewDataNavigator() {
            return new MB.WinClientDefault.Common.MainViewDataNavigator(gridViewMain);

        }
        #endregion 覆盖基类的方法...

        /// <summary>
        /// 验证页面上的控件在刷新数据源以后，是不是需要改变状态
        /// </summary>
        /// <param name="loadCount">客户端实际的返回的记录条数</param>
        /// <param name="dbRecordCount">数据库实际有的记录条数，如果与recordCount有差别，则证明中间层做了帅选</param>
        private void validatedPageControl(int loadCount, int dbRecordCount) {
            #region 对于下一页的控制
            //首先看消息头返回的记录是否大于页总记录数。消息头的记录数是基于数据库的查询记录数
            //有的情况下，中间层还会做额外的过滤导致返回记录数小于，每页记录数，但其实数据库大于每页记录数
            //如果消息头没有返回记录，则根据客户端判断
            if (dbRecordCount > 0)
                lnkNextPage.Enabled = dbRecordCount > _ClientRuleObject.CurrentQueryBehavior.PageSize;
            else
                lnkNextPage.Enabled = loadCount >= _ClientRuleObject.CurrentQueryBehavior.PageSize;
            #endregion

            lnkPreviousPage.Enabled = _ClientRuleObject.CurrentQueryBehavior.PageIndex > 0;
        }

        /// <summary>
        /// 返回需要自定义的汇总列
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> getCustomSummaryColValues(MB.Util.Model.QueryParameterInfo[] queryParams) {
            List<string> colsToGet = new List<string>();
            Dictionary<string, ColumnPropertyInfo> bindingPropertys = LayoutXmlConfigHelper.Instance.GetColumnPropertys(_ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile);
            foreach (ColumnPropertyInfo colInfo in bindingPropertys.Values) {
                if (colInfo.IsCustomFootSummary) {
                    colsToGet.Add(colInfo.Name);
                }
            }
            //如果没有开通自定义网格页脚汇总， 或者没有列需要汇总，则返回空
            if ((_ClientRuleObject.ClientLayoutAttribute != null &&
                _ClientRuleObject.ClientLayoutAttribute.IsCustomFooterSummary) ||
                colsToGet.Count <= 0) {
                Dictionary<string, object> customSummaryCols = new Dictionary<string, object>();
                return customSummaryCols;
            }
            else
                return _ClientRuleObject.GetCustomSummaryColValues(colsToGet.ToArray(), queryParams);
        }
    }
}
