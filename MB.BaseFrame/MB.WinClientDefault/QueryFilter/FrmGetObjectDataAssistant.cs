//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved 
// Author		:	chendc
// Create date	:	2009-04-03.
// Description	:	获取单据数据。 
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
using MB.WinBase.Common;
using MB.WcfServiceLocator;
using MB.Util.Model;
namespace MB.WinClientDefault.QueryFilter
{
    /// <summary>
    /// 获取数据助手
    /// </summary>
    public partial class FrmGetObjectDataAssistant : AbstractBaseForm, IGetObjectDataAssistant
    {

        #region 变量定义..
        public static readonly string QUERY_REFRESH_MSG_ID = "BE2C58AD-BEF8-4E8B-BBB6-E82C31A18453";

        private static readonly string[] HELP_MESSAGE = new string[] { "请至少输入一个查询条件！", "请选择数据！" };
        private Dictionary<PaneViewType, Control> _StepShowControlPane;
        private MB.WinBase.IFace.IClientRuleQueryBase _ClientRule;
        private MB.WinBase.Common.ColumnEditCfgInfo _ClumnEditCfgInfo;
        private bool _MultiSelect;
        private bool _HideFilterPane;
        private List<MB.Util.Model.QueryParameterInfo> _FilterParametersIfNoFiterPanel;
        private MB.WinBase.Common.InvokeDataSourceDescInfo _InvokeDataSourceDesc;
        private IInvokeDataAssistantHoster _InvokeFilterParentFormHoster;
        private IQueryObject _QueryObject;
        private object _InvokeParentControl;
        private ToolTip _ToolTip;
        private IDataAssistantListControl _ListView;

        //最大行数据限制
        private int _MaxSelectRows = DEFAULT_MAX_SELECT_ROWS;
        private object _CurrentEditObject;
        private const int MAX_SELECT_ROWS = 10000;
        private const int DEFAULT_MAX_SELECT_ROWS = 100;
        private IDictionary<int, IDictionary<int, object>> SelectedRows = new Dictionary<int, IDictionary<int, object>>();
        private int _MAX_SHOW_ROWS;

        /// <summary>
        /// 小助手最大显示的行数
        /// </summary>
        public int MAX_SHOW_ROWS {
            get { return _MAX_SHOW_ROWS; }
            set { _MAX_SHOW_ROWS = value; }
        }

        #endregion 变量定义..

        #region 自定义事件处理相关...
        /// <summary>
        ///  数据选择后产生的事件。
        /// </summary>
        private GetObjectDataAssistantEventHandle _AfterGetObjectData;
        public event GetObjectDataAssistantEventHandle AfterGetObjectData
        {
            add
            {
                _AfterGetObjectData += value;
            }
            remove
            {
                _AfterGetObjectData -= value;
            }
        }
        protected virtual void OnAfterGetObjectData(GetObjectDataAssistantEventArgs arg)
        {
            if (_AfterGetObjectData != null)
                _AfterGetObjectData(this, arg);
        }
        #endregion 自定义事件处理相关...

        #region 构造函数...
        /// <summary>
        /// 
        /// </summary>
        public FrmGetObjectDataAssistant()
            : this(null)
        {
        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        public FrmGetObjectDataAssistant(MB.WinBase.IFace.IClientRule clientRule)
        {
            InitializeComponent();

            _ToolTip = new ToolTip();
            panMain.Dock = DockStyle.Fill;

            _ClientRule = clientRule;

            lnkChecked.Visible = false;

            this.Load += new EventHandler(FrmGetObjectDataAssistant_Load);

        }

        void lnkPreviousPage_Click(object sender, EventArgs e)
        {
            ChangePageIndex(-1);
        }

        void lnkNextPage_Click(object sender, EventArgs e)
        {
            ChangePageIndex(1);
        }

        private void validatedPageControl(int loadCount)
        {
            if (_ClientRule.CurrentQueryBehavior.PageIndex < 1 && loadCount < _ClientRule.CurrentQueryBehavior.PageSize)
            {
                this.splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            }
            else
            {
                lnkNextPage.Enabled = loadCount >= _ClientRule.CurrentQueryBehavior.PageSize;
                lnkPreviousPage.Enabled = _ClientRule.CurrentQueryBehavior.PageIndex > 0;
            }
        }
        /// <summary>
        /// 获取数据助手
        /// </summary>
        /// <param name="invokeType">包含类和配件信息。如：MB.ERP.BaseLibrary.CWarehouse.UIRule.BfOrgWsUIRule,MB.ERP.BaseLibrary.CWarehouse.dll</param>
        /// <param name="method">获取数据的方法</param>
        /// <param name="conpar">实例化查询业务类的构造函数。</param>
        public FrmGetObjectDataAssistant(string invokeType, string method, string conpar)
            : this(null)
        {
            _InvokeDataSourceDesc = new MB.WinBase.Common.InvokeDataSourceDescInfo(invokeType, method, conpar);
        }

        #endregion 构造函数...


        #region IGetObjectDataAssistant 成员
        /// <summary>
        /// 获取过滤数据的对象。
        /// </summary>
        public IQueryObject QueryObject
        {
            get
            {
                return _QueryObject;
            }
            set
            {
                _QueryObject = value;
            }
        }

        /// <summary>
        /// 可户业务端控制类。
        /// </summary>
        public MB.WinBase.IFace.IClientRuleQueryBase FilterClientRule
        {
            get
            {
                return _ClientRule;
            }
            set
            {
                _ClientRule = value;
            }
        }

        public MB.WinBase.Common.ColumnEditCfgInfo ClumnEditCfgInfo
        {
            get
            {
                if (_ClumnEditCfgInfo == null)
                    _ClumnEditCfgInfo = new MB.WinBase.Common.ColumnEditCfgInfo();
                return _ClumnEditCfgInfo;
            }
            set
            {
                _ClumnEditCfgInfo = value;
            }
        }
        public bool MultiSelect
        {
            get
            {
                return _MultiSelect;
            }
            set
            {
                _MultiSelect = value;
            }
        }
        public MB.WinBase.Common.InvokeDataSourceDescInfo InvokeDataSourceDesc
        {
            get
            {
                return _InvokeDataSourceDesc;
            }
            set
            {
                _InvokeDataSourceDesc = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaxSelectRows
        {
            get
            {
                return _MaxSelectRows;
            }
            set
            {
                if (value <= 0) _MaxSelectRows = DEFAULT_MAX_SELECT_ROWS;
                else _MaxSelectRows = value > MAX_SELECT_ROWS ? MAX_SELECT_ROWS : value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object CurrentEditObject
        {
            get
            {
                return _CurrentEditObject;
            }
            set
            {
                _CurrentEditObject = value;
            }
        }

        /// <summary>
        /// 根据查询条件获取数据。
        /// </summary>
        /// <param name="dataInDocType"></param>
        /// <param name="filterParameters"></param>
        /// <returns></returns>
        public System.Collections.IList GetFilterObjects(int dataInDocType, List<MB.Util.Model.QueryParameterInfo> filterParameters)
        {
            if (_InvokeFilterParentFormHoster != null)
            {
                InvokeDataAssistantHosterEventArgs arg = new InvokeDataAssistantHosterEventArgs(_ClientRule, _ClumnEditCfgInfo, _CurrentEditObject, filterParameters);
                _InvokeFilterParentFormHoster.BeforeGetFilterData(_InvokeParentControl, arg);
                if (arg.Cancel)
                    return null;
            }
            try
            {

                if (_QueryObject != null)
                {

                    return _QueryObject.GetFilterObjects(filterParameters.ToArray());
                }
                if (_InvokeDataSourceDesc == null)
                {
                    IList lstDatas = null;
                    try
                    {
                        using (MB.Util.MethodTraceWithTime timeTrack = new MB.Util.MethodTraceWithTime(null))
                        {

                            lstDatas = _ClientRule.GetObjects((int)_ClientRule.MainDataTypeInDoc, filterParameters.ToArray());

                            var msg = string.Format("查询花费:{0} 毫秒,返回 {1} 记录", timeTrack.GetExecutedTimes(), lstDatas.Count);
                            MB.WinBase.AppMessenger.DefaultMessenger.Publish(QUERY_REFRESH_MSG_ID, msg);
                        };
                    }
                    catch (Exception ex)
                    {
                        throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, " GetObjects 出错！");
                    }
                    return lstDatas;
                }

                string[] desc = _InvokeDataSourceDesc.Method.Split(',');
                ArrayList pars = new ArrayList();
                if (desc.Length > 1)
                {
                    string[] ss = desc[1].Split(';');
                    foreach (var s in ss)
                        //判断是否为
                        pars.Add(MB.WinBase.AppEnvironmentSetting.Instance.ConvertSystemParamValue(s));
                }
                if (filterParameters != null && filterParameters.Count > 0)
                {
                    pars.Add(filterParameters.ToArray());
                }
                else
                {
                    if (_InvokeDataSourceDesc.ExistsFilterParams)
                    {
                        //判断调用的方法是否存在过滤的参数,如果存在需要进行特殊的处理（默认情况下存在）
                        List<MB.Util.Model.QueryParameterInfo> filterParam = new List<MB.Util.Model.QueryParameterInfo>();
                        filterParam.Add(new MB.Util.Model.QueryParameterInfo("1", "1", MB.Util.DataFilterConditions.Special));
                        pars.Add(filterParam.ToArray());
                    }
                }
                //object val = MB.Util.MyReflection.Instance.InvokeMethod(_ClientRule, desc[0], pars.ToArray());
                // object val = MB.Util.MyReflection.Instance.InvokeMethodByName(_ClientRule, desc[0], pars.ToArray());
                object val = null;
                try
                {
                    using (MB.Util.MethodTraceWithTime timeTrack = new MB.Util.MethodTraceWithTime(null))
                    {
                        val = MB.Util.MyReflection.Instance.InvokeMethod(_ClientRule, desc[0], pars.ToArray());

                        var msg = string.Format("查询花费:{0} 毫秒,返回 {1} 记录", timeTrack.GetExecutedTimes(), (val as IList).Count);
                        MB.WinBase.AppMessenger.DefaultMessenger.Publish(QUERY_REFRESH_MSG_ID, msg);
                    };
                }
                catch (Exception ex)
                {
                    throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, desc[0] + " 出错！");
                }

                return val as IList;
            }
            catch (Exception ex)
            {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "调用方法 MB.WinClientDefault.QueryFilter.FrmGetObjectDataAssistant::GetFilterObjects  获取数据有误");
            }
            return null;
        }
        #endregion

        #region protected virtual 提供的继承的子类调用的方法...
        /// <summary>
        /// 创建过滤查询的窗口。
        /// </summary>
        /// <param name="clientRule"></param>
        /// <param name="filterCfgName"></param>
        /// <returns></returns>
        protected virtual Control CreateFilterControl(IClientRuleQueryBase clientRule, string filterCfgName)
        {
            return new ucFilterCondition(clientRule, filterCfgName);
        }
        #endregion protected virtual 提供的继承的子类调用的方法...

        void FrmGetObjectDataAssistant_Load(object sender, EventArgs e)
        {
            if (MB.Util.General.IsInDesignMode()) return;

            if (_ClientRule == null)
            {
                string[] tps = this.InvokeDataSourceDesc.Type.Split(',');
                if (string.IsNullOrEmpty(this.InvokeDataSourceDesc.TypeConstructParams))
                {
                    _ClientRule = MB.Util.DllFactory.Instance.LoadObject(tps[0], tps[1]) as MB.WinBase.IFace.IClientRuleQueryBase;
                }
                else
                {
                    var conPars = this.InvokeDataSourceDesc.TypeConstructParams.Split(',');
                    _ClientRule = MB.Util.DllFactory.Instance.LoadObject(tps[0], conPars, tps[1]) as MB.WinBase.IFace.IClientRuleQueryBase;
                }
            }
            if (_ClientRule == null)
                throw new MB.Util.APPException("没有得到客户端业务控制类！", MB.Util.APPMessageType.SysErrInfo);

            //增加分页  add by aifang 2012-07-26 begin
            MB.WinBase.AppMessenger.DefaultMessenger.Subscribe<string>(QUERY_REFRESH_MSG_ID, o =>
            {
                labTitleMsg.Text = o;
            });
            if (_QueryObject == null && pnlQry.Visible)
            {
                lnkNextPage.Click += new EventHandler(lnkNextPage_Click);
                lnkPreviousPage.Click += new EventHandler(lnkPreviousPage_Click);

                lnkPreviousPage.Enabled = false;
                lnkNextPage.Enabled = false;


            }
            //增加分页  add by aifang 2012-07-26 end

            if (_StepShowControlPane == null)
            {
                _StepShowControlPane = new Dictionary<PaneViewType, Control>();
                _StepShowControlPane.Add(PaneViewType.FilterPane, CreateFilterControl(_ClientRule, ClumnEditCfgInfo.FilterCfgName));

                ITreeListViewHoster treeViewRule = _ClientRule as ITreeListViewHoster;
                if (treeViewRule == null)
                    _ListView = new ucDataCheckListView();
                else
                    _ListView = new ucDataTreeListView();

                _ListView.AfterSelectData += new GetObjectDataAssistantEventHandle(listView_AfterSelectData);
                _StepShowControlPane.Add(PaneViewType.DataSelect, _ListView as Control);

                foreach (Control ctl in _StepShowControlPane.Values)
                {
                    ctl.Dock = DockStyle.Fill;
                    panMain.Controls.Add(ctl);
                }
            }

            if (_HideFilterPane)
            {
                _StepShowControlPane[PaneViewType.DataSelect].BringToFront();
                _ListView.MultiSelect = _MultiSelect;
                _ListView.ColumnEditCfgInfo = _ClumnEditCfgInfo;
                lnkChecked.Visible = _MultiSelect;

                try
                {
                    //获得动态列的MessageKey
                    string messageHeaderKey = string.Empty;
                    if (_ClientRule.ClientLayoutAttribute.LoadType == ClientDataLoadType.ReLoad)
                        messageHeaderKey = _ClientRule.ClientLayoutAttribute.MessageHeaderKey;

                    //添加动态列消息头
                    MB.XWinLib.XtraGrid.XtraGridDynamicHelper.Instance.AppendQueryBehaviorColumns(_ClientRule);
                    //增加分页信息
                    if (_MAX_SHOW_ROWS <= 0) {
                        _MAX_SHOW_ROWS = MB.Util.MyConvert.Instance.ToInt(nubMaxShotCount.Value);
                    }
                    _ClientRule.CurrentQueryBehavior.PageSize = _MAX_SHOW_ROWS;
                    _ClientRule.CurrentQueryBehavior.PageIndex = 0;

                    using (QueryBehaviorScope scope = new QueryBehaviorScope(_ClientRule.CurrentQueryBehavior, messageHeaderKey))
                    {
                        List<MB.Util.Model.QueryParameterInfo> filterParas;
                        if (_FilterParametersIfNoFiterPanel != null)
                            filterParas = _FilterParametersIfNoFiterPanel;
                        else
                            filterParas = new List<MB.Util.Model.QueryParameterInfo>();

                        var lstDatas = this.GetFilterObjects(0, filterParas);
                        _ListView.SetDataSource(_ClientRule, lstDatas);

                        validateButton(PaneViewType.DataSelect);
                        butNext.Enabled = false;
                        butPreviouss.Enabled = false;
                        validatedPageControl((lstDatas as IList).Count);
                    }
                }
                catch (Exception ex)
                {
                    MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                }
            }
            else
            {
                _StepShowControlPane[PaneViewType.FilterPane].BringToFront();
                validateButton(PaneViewType.FilterPane);
            }
            ucFilterCondition filterCtl = _StepShowControlPane[PaneViewType.FilterPane] as ucFilterCondition;
            if (filterCtl != null)
            {
                //panBottom.BackColor = filterCtl.AllowEmptyFilter ? System.Drawing.Color.FromArgb(212, 228, 248) : Color.White;
                if (filterCtl.AllowEmptyFilter)
                    _ToolTip.SetToolTip(panBottom, "查询绿色通道,允许查询所有数据");
            }
        }

        private void validateButton(PaneViewType currentViewType)
        {
            bool isFirst = currentViewType == PaneViewType.FilterPane;
            butPreviouss.Enabled = !isFirst;
            butNext.Enabled = isFirst;
            butSure.Enabled = !isFirst;
            labMessage.Text = isFirst ? HELP_MESSAGE[0] : HELP_MESSAGE[1];

            if (isFirst) this.splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            else this.splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;

            mpanFilterTop.Visible = isFirst;
            if (isFirst) mpanFilterTop.BringToFront();
            else mpanFilterTop.SendToBack();
        }

        #region 控制项 Pane 类型...
        /// <summary>
        /// 控制项 Pane 类型。
        /// </summary>
        internal enum PaneViewType
        {
            FilterPane,
            DataSelect
        }
        #endregion 控制项 Pane 类型...

        private void butQuit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void butNext_Click(object sender, EventArgs e)
        {
            _MAX_SHOW_ROWS = MB.Util.MyConvert.Instance.ToInt(nubMaxShotCount.Value);
            _ClientRule.CurrentQueryBehavior.PageSize = _MAX_SHOW_ROWS;
            _ClientRule.CurrentQueryBehavior.PageIndex = 0;

            queryFilterData();
        }

        private void queryFilterData()
        {
            try
            {
                ucFilterCondition filterCtl = _StepShowControlPane[PaneViewType.FilterPane] as ucFilterCondition;
                MB.Util.Model.QueryParameterInfo[] filters = filterCtl.GetQueryParameters();

                List<QueryParameterInfo> tempFilters = new List<QueryParameterInfo>(filters);

                //判断如果是自定义的条件，在查询的时候，需要再加上这个条件
                if (_FilterParametersIfNoFiterPanel != null && _FilterParametersIfNoFiterPanel.Count > 0) {
                    tempFilters.AddRange(_FilterParametersIfNoFiterPanel);
                    filters = tempFilters.ToArray();
                }


                if (filters == null || filters.Length == 0)
                {
                    if (!filterCtl.AllowEmptyFilter)
                    {
                        MB.WinBase.MessageBoxEx.Show("请至少输入一个数据过滤的条件！");
                        return;
                    }
                    else
                    {
                        MB.Util.Model.QueryParameterInfo allFilter = new MB.Util.Model.QueryParameterInfo("0", "0", MB.Util.DataFilterConditions.Special);
                        filters = new MB.Util.Model.QueryParameterInfo[] { allFilter };
                    }
                }
                //modify by aifang 2012-04-17 支持树型控件数据助手选择
                //ucDataCheckListView listView = _StepShowControlPane[PaneViewType.DataSelect] as ucDataCheckListView;
                IDataAssistantListControl listView = _StepShowControlPane[PaneViewType.DataSelect] as IDataAssistantListControl;
                //modify by aifang 2012-04-17 支持树型控件数据助手选择

                listView.MultiSelect = _MultiSelect;
                listView.ColumnEditCfgInfo = _ClumnEditCfgInfo;
                lnkChecked.Visible = _MultiSelect;

                using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this))
                {
                    int mainType = 0;
                    if (_ClientRule.MainDataTypeInDoc != null)
                        mainType = (int)_ClientRule.MainDataTypeInDoc;

                    //获得动态列的MessageKey
                    string messageHeaderKey = string.Empty;
                    if (_ClientRule.ClientLayoutAttribute.LoadType == ClientDataLoadType.ReLoad)
                        messageHeaderKey = _ClientRule.ClientLayoutAttribute.MessageHeaderKey;

                    //添加动态列消息头
                    MB.XWinLib.XtraGrid.XtraGridDynamicHelper.Instance.AppendQueryBehaviorColumns(_ClientRule);

                    using (QueryBehaviorScope scope = new QueryBehaviorScope(_ClientRule.CurrentQueryBehavior, messageHeaderKey))
                    {
                        var lstDatas = this.GetFilterObjects(mainType, new List<MB.Util.Model.QueryParameterInfo>(filters));
                        if (lstDatas != null && lstDatas.Count > 0)
                        {
                            listView.SetDataSource(_ClientRule, lstDatas);
                            _StepShowControlPane[PaneViewType.DataSelect].BringToFront();
                            validateButton(PaneViewType.DataSelect);
                            validatedPageControl((lstDatas as IList).Count);
                        }
                        else
                        {
                            MB.WinBase.MessageBoxEx.Show("根据该条件查找不到数据，请重试！");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        private void butPreviouss_Click(object sender, EventArgs e)
        {
            try
            {
                using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this))
                {
                    _StepShowControlPane[PaneViewType.FilterPane].BringToFront();
                    validateButton(PaneViewType.FilterPane);
                    lnkChecked.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        private void butSure_Click(object sender, EventArgs e)
        {
            
            try
            {
                using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this))
                {
                    //IDataAssistantListControl listView = _StepShowControlPane[PaneViewType.DataSelect] as IDataAssistantListControl;
                    SaveSelectedCurrentPageRows();
                    object[] rows = GetSelectedRows();
                    if (rows == null || rows.Length == 0)
                    {
                        MB.WinBase.MessageBoxEx.Show("请选择数据！");
                        return;
                    }
                    if (rows.Length > _MaxSelectRows)
                    {
                        MB.WinBase.MessageBoxEx.Show(string.Format("选择数据超过最大设置行数{0}！", _MaxSelectRows));
                        return;
                    }
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    OnAfterGetObjectData(new GetObjectDataAssistantEventArgs(rows));
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }
        void listView_AfterSelectData(object sender, GetObjectDataAssistantEventArgs arg)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            OnAfterGetObjectData(new GetObjectDataAssistantEventArgs(arg.SelectedRows));
            this.Close();
        }

        #region IGetObjectDataAssistant 成员

        /// <summary>
        /// 
        /// </summary>
        public IInvokeDataAssistantHoster InvokeFilterParentFormHoster
        {
            get
            {
                return _InvokeFilterParentFormHoster;
            }
            set
            {
                _InvokeFilterParentFormHoster = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public object InvokeParentControl
        {
            get
            {
                return _InvokeParentControl;
            }
            set
            {
                _InvokeParentControl = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool HideFilterPane
        {
            get
            {
                return _HideFilterPane;
            }
            set
            {
                _HideFilterPane = value;
            }
        }

        public List<MB.Util.Model.QueryParameterInfo> FilterParametersIfNoFiterPanel {
            get {
                return _FilterParametersIfNoFiterPanel;
            }
            set {
                _FilterParametersIfNoFiterPanel = value;
            }
        }

        #endregion

        private void lnkChecked_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            contextMenuStrip1.Show(lnkChecked, new System.Drawing.Point(0, lnkChecked.Height));
        }

        private void menuCheckAll_Click(object sender, EventArgs e)
        {
            _ListView.CheckListViewItem(true);
        }

        private void menuUnCheckAll_Click(object sender, EventArgs e)
        {
            _ListView.CheckListViewItem(false);
        }

        private void SaveSelectedCurrentPageRows()
        {
            IDataAssistantListControl listView = _StepShowControlPane[PaneViewType.DataSelect] as IDataAssistantListControl;
            IDictionary<int, object> currentPageSelectedItems = listView.GetSelectRowsWithIndex();
            if (currentPageSelectedItems != null && currentPageSelectedItems.Any())
            {
                if (!_MultiSelect)
                {
                    SelectedRows.Clear();
                    SelectedRows.Add(_ClientRule.CurrentQueryBehavior.PageIndex, currentPageSelectedItems);
                }
                if (SelectedRows.ContainsKey(_ClientRule.CurrentQueryBehavior.PageIndex))
                {
                    SelectedRows[_ClientRule.CurrentQueryBehavior.PageIndex] = currentPageSelectedItems;
                }
                else
                {
                    SelectedRows.Add(_ClientRule.CurrentQueryBehavior.PageIndex, currentPageSelectedItems);
                }
            }


        }
        private void CheckListViewItems()
        {
            IDictionary<int, object> currentPageSelectedItems;
            if (SelectedRows.TryGetValue(_ClientRule.CurrentQueryBehavior.PageIndex, out currentPageSelectedItems))
            {
                IDataAssistantListControl listView = _StepShowControlPane[PaneViewType.DataSelect] as IDataAssistantListControl;
                listView.CheckListViewItems(currentPageSelectedItems.Keys);
            }
        }
        private void ChangePageIndex(int offsetIndex)
        {
            SaveSelectedCurrentPageRows();
            _ClientRule.CurrentQueryBehavior.PageIndex += offsetIndex;
            queryFilterData();
            CheckListViewItems();
        }
        private object[] GetSelectedRows()
        {
            var list = new List<object>();
            foreach (var item in SelectedRows.Values)
            {
                list.AddRange(item.Values);
            }
            return list.ToArray();
        }

    }
}
