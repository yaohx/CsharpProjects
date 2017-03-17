//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-04-04
// Description	:	对象数据查询窗口。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
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
using MB.BaseFrame;
using MB.WinBase.Atts;
using MB.WinBase.Common;
using MB.XWinLib.XtraGrid;
using DevExpress.XtraGrid;
using MB.Util.Model;
namespace MB.WinClientDefault.QueryFilter {
    /// <summary>
    /// 对象数据查询窗口。
    /// </summary>
    public partial class FrmQueryFilterInput : AbstractBaseForm, IQueryFilterForm, IInvokeDataAssistantHoster
    {
        private ucFilterCondition _SimpleFilterEdit;
        private ucAdvanceFilterControl _AdvanceFilter;
        private MB.WinBase.IFace.IClientRuleQueryBase _ClientRuleObject;
        private string _DataFilterElementsName;
        private IViewGridForm _ViewGridForm;
        private ToolTip _ToolTip;

        private Control _SimpleFilterControl;
        private IFilterConditionCtl _FilterConditionCtl;
        private FilterElementCfgs _DataElementCfgs;
        Dictionary<string, ColumnPropertyInfo> _Columns;
        private static readonly string DATA_FILTER_MAIN_CFG_NAME = "MainDataFilter";

        private bool _IsPageSettingPanelVisiable;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientRule"></param>
        public FrmQueryFilterInput()
            : this(true) {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientRule"></param>
        public FrmQueryFilterInput(bool showPageSettingPanel) {
            InitializeComponent();
            _ToolTip = new ToolTip();
            _IsPageSettingPanelVisiable = showPageSettingPanel;
        }


        #region 自定义事件处理相关...
        /// <summary>
        ///  数据选择后产生的事件。
        /// </summary>
        private QueryFilterInputEventHandle _AfterInputQueryParameter;
        public event QueryFilterInputEventHandle AfterInputQueryParameter {
            add {
                _AfterInputQueryParameter += value;
            }
            remove {
                _AfterInputQueryParameter -= value;
            }
        }
        protected virtual void OnAfterGetObjectData(QueryFilterInputEventArgs arg) {
            if (_AfterInputQueryParameter != null)
                _AfterInputQueryParameter(this, arg);
        }
        #endregion 自定义事件处理相关...

        private void FrmQueryFilterInput_Load(object sender, EventArgs e) {
            if (!_IsPageSettingPanelVisiable) {
                mpanFilterTop.Visible = false;
            }

            //add by aifang
            if (string.IsNullOrEmpty(_DataFilterElementsName)) _DataFilterElementsName = DATA_FILTER_MAIN_CFG_NAME;
            _DataElementCfgs = MB.WinBase.LayoutXmlConfigHelper.Instance.GetDataFilterCfgElements(_ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile, _DataFilterElementsName);
            if (_DataElementCfgs == null || _DataElementCfgs.Count == 0)
                throw new MB.Util.APPException("该对象在对应的XML配置文件中没有配置查询项！", MB.Util.APPMessageType.DisplayToUser);

            _Columns = _ClientRuleObject.UIRuleXmlConfigInfo.GetDefaultColumns();

            #region 设置过滤框的长度与宽度
            if (_DataElementCfgs.Width > 0)
                this.Width = _DataElementCfgs.Width;
            if (_DataElementCfgs.Height > 0)
                this.Height = _DataElementCfgs.Height;
            #endregion

            //增加判断,如果主窗口中的不是GridView,则不加载高级查询
            var gridControl = _ViewGridForm.GetCurrentMainGridView(true) as DevExpress.XtraGrid.GridControl;
            if (gridControl != null) {
                tPageAdvanceFilter.PageVisible = true;
                showAdvanceFilter();
                
            }
            else {
                tPageAdvanceFilter.PageVisible = false;
            }
            
            //判断大数据查询是否被激活
            IViewGridFormWithGreatData viewWithGreatData = _ViewGridForm as IViewGridFormWithGreatData;
            this.cbQueryAll.Visible = (viewWithGreatData != null && _DataElementCfgs.AllowQueryAll);
            this.cbQueryAll.Checked = false;


            bool isDynamicGroupEnabled = false;
            IViewDynamicGroupGridForm dynamicGroupView = _ViewGridForm as IViewDynamicGroupGridForm;
            if (dynamicGroupView != null) isDynamicGroupEnabled = dynamicGroupView.IsDynamicGroupIsActive;

            this.lnkDynamaicSetting.Visible = (_ClientRuleObject.ClientLayoutAttribute.LoadType == ClientDataLoadType.ReLoad && !isDynamicGroupEnabled);
            this.cbShowTotalPage.Visible = !isDynamicGroupEnabled;
            this.cbQueryAll.Visible = !isDynamicGroupEnabled;
            
        }

       
        void gridcontrolEx_DefaultViewColumnsCleared(object sender, EventArgs e)
        {
            _AdvanceFilter = null;
        }

        protected override void OnClosing(CancelEventArgs e) {
            e.Cancel = true;
            base.OnClosing(e);
            this.Hide();

            if (this.Owner != null) {
                this.Owner.Activate();
                this.Owner.Focus();
                this.Owner.BringToFront();
            }
        }
        private void butQuit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void butSure_Click(object sender, EventArgs e) {
            executeQuery();
        }

        private MB.Util.Model.QueryParameterInfo[] GetAdvanceQueryParameters()
        {
            MB.Util.Model.QueryParameterInfo[]  queryParamters = _AdvanceFilter.GetQueryParameters();

            List<MB.Util.Model.QueryParameterInfo> filterList = new List<Util.Model.QueryParameterInfo>();
            filterList.AddRange(queryParamters.ToList());

            filterList.AddRange(buildQueryParams(queryParamters));

            return filterList.ToArray();
        }

        private List<QueryParameterInfo> buildQueryParams(MB.Util.Model.QueryParameterInfo[] queryParamters)
        {
            List<MB.Util.Model.QueryParameterInfo> filterList = new List<QueryParameterInfo>();
            foreach (var queryParam in queryParamters)
            {
                if (string.IsNullOrEmpty(queryParam.PropertyName))
                {
                    if (queryParam.Childs != null && queryParam.Childs.Count > 0)
                    {
                        filterList.AddRange(buildQueryParams(queryParam.Childs.ToArray()));
                        continue;
                    }
                }
                if (_ClientRuleObject.UIRuleXmlConfigInfo.ColumnsCfgEdit == null || !_ClientRuleObject.UIRuleXmlConfigInfo.ColumnsCfgEdit.ContainsKey(queryParam.PropertyName)) continue;

                var cfgEditInfo = _ClientRuleObject.UIRuleXmlConfigInfo.ColumnsCfgEdit[queryParam.PropertyName];
                MB.WinBase.Common.EditControlType controlType = (EditControlType)Enum.Parse(typeof(EditControlType), cfgEditInfo.EditControlType);
                if (controlType == EditControlType.PopupRegionEdit)
                {
                    var filter = queryParamters.Where(o => o.PropertyName.Equals(cfgEditInfo.Name)).FirstOrDefault();
                    if (filter != null)
                    {
                        string val = filter.Value.ToString();
                        string[] vals = val.Split(' ');

                        if (vals.Length > 0)
                        {
                            var mappingInfo = cfgEditInfo.EditCtlDataMappings.Find(o => o.SourceColumnName.Equals("Country"));
                            if (mappingInfo != null) filterList.Add(new MB.Util.Model.QueryParameterInfo(mappingInfo.ColumnName, vals[0], filter.Condition));
                        }
                        if (vals.Length > 1)
                        {
                            var mappingInfo = cfgEditInfo.EditCtlDataMappings.Find(o => o.SourceColumnName.Equals("Province"));
                            if (mappingInfo != null) filterList.Add(new MB.Util.Model.QueryParameterInfo(mappingInfo.ColumnName, vals[1], filter.Condition));
                        }
                        if (vals.Length > 2)
                        {
                            var mappingInfo = cfgEditInfo.EditCtlDataMappings.Find(o => o.SourceColumnName.Equals("City"));
                            if (mappingInfo != null) filterList.Add(new MB.Util.Model.QueryParameterInfo(mappingInfo.ColumnName, vals[2], filter.Condition));
                        }
                        if (vals.Length > 3)
                        {
                            var mappingInfo = cfgEditInfo.EditCtlDataMappings.Find(o => o.SourceColumnName.Equals("District"));
                            if (mappingInfo != null) filterList.Add(new MB.Util.Model.QueryParameterInfo(mappingInfo.ColumnName, vals[3], filter.Condition));
                        }

                        filterList.Remove(filter);
                    }
                }
            }
            return filterList;
        }

        private void executeQuery() {
            try {
              
                MB.Util.Model.QueryParameterInfo[] queryParamters = null;
                if (tabCtlFilterMain.SelectedTabPage.Equals(tPageAdvanceFilter)) {
                    queryParamters = GetAdvanceQueryParameters();//modify by aifang 2012-07-30 queryParamters = _AdvanceFilter.GetQueryParameters();
                }
                else
                    queryParamters = _FilterConditionCtl.GetQueryParameters();  // _SimpleFilterEdit.GetQueryParameters();

                if (queryParamters == null || queryParamters.Length == 0) {
                    if (!_FilterConditionCtl.AllowEmptyFilter)
                    {
                        MB.WinBase.MessageBoxEx.Show("请至少输入一个数据过滤的条件！");
                        return;
                    }
                    else {
                        MB.Util.Model.QueryParameterInfo allFilter = new MB.Util.Model.QueryParameterInfo("0", "0", MB.Util.DataFilterConditions.Special);
                        queryParamters = new MB.Util.Model.QueryParameterInfo[] { allFilter };
                    }
                }
                _ViewGridForm.ClientRuleObject.CurrentQueryBehavior.PageSize = MyConvert.Instance.ToInt(nubMaxShotCount.Value);
                _ViewGridForm.ClientRuleObject.CurrentQueryBehavior.IsTotalPageDisplayed = this.cbShowTotalPage.Checked;
                OnAfterGetObjectData(new QueryFilterInputEventArgs(queryParamters));
                this.Close();
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        #region IQueryFilterForm 成员

        public string DataFilterElementsName {
            get {
                return _DataFilterElementsName;
            }
            set {
                _DataFilterElementsName = value;
            }
        }

        #endregion

        #region IForm 成员

        public IClientRuleQueryBase ClientRuleObject {
            get {
                return _ClientRuleObject;
            }
            set {
                _ClientRuleObject = value;
            }
        }

        public MB.WinBase.Common.ClientUIType ActiveUIType {
            get { return MB.WinBase.Common.ClientUIType.QueryFilter; }
        }

        #endregion

        #region IQueryFilterForm 成员


        public IViewGridForm ViewGridForm {
            get {
                return _ViewGridForm;
            }
            set {

                // 确保事件只注册一次
                if (_ViewGridForm == null || _ViewGridForm!=value)
                 {
                     var gridcontrol = value.GetCurrentMainGridView(true) as GridControl;

                    if (gridcontrol is GridControlEx)
                    {
                        var gridcontrolEx = gridcontrol as GridControlEx;
                        gridcontrolEx.DefaultViewColumnsCleared += new EventHandler(gridcontrolEx_DefaultViewColumnsCleared);
                    }   
                 }
                _ViewGridForm = value;
            }
        }

        #endregion

        private void showAdvanceFilter() {
            try {
                if (_AdvanceFilter == null) {
                    _AdvanceFilter = new ucAdvanceFilterControl();
                    _AdvanceFilter.IniLoadFilterControl(_ViewGridForm);
                    _AdvanceFilter.Dock = DockStyle.Fill;
                    panAdvanceFilter.Controls.Add(_AdvanceFilter);
                }
                _AdvanceFilter.BringToFront();

            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        #region IQueryFilterForm 成员

        public void IniCreateFilterElements() {
            if (_ClientRuleObject != null) {

                SimpleDataFilterControlAttribute ctlAtt = MB.WinBase.Atts.AttributeConfigHelper.Instance.GetSimpleDataFilterControl(_ClientRuleObject.GetType());
                if(ctlAtt != null)
                    _SimpleFilterControl = ctlAtt.FormControlType.Assembly.CreateInstance(ctlAtt.FormControlType.FullName, true) as Control;
                if (_SimpleFilterControl == null)
                {
                    _SimpleFilterEdit = new ucFilterCondition(_ViewGridForm, _ClientRuleObject, _DataFilterElementsName);
                    _SimpleFilterControl = _SimpleFilterEdit;
                }

                _FilterConditionCtl = _SimpleFilterControl as IFilterConditionCtl;
                if (_FilterConditionCtl == null)
                    throw new MB.Util.APPException(string.Format("条件过滤窗口 {0} 未继承接口 IFilterConditionCtl ，请检查!", ctlAtt.FormControlType.FullName), APPMessageType.DisplayToUser);

                panQuickFilter.Controls.Add(_SimpleFilterControl);
                _SimpleFilterControl.Dock = DockStyle.Fill;

                //panFilterTop.Height = 2;
                //panFilterTop.BorderStyle = BorderStyle.FixedSingle; 
               // panBottom.BackColor = _SimpleFilterEdit.AllowEmptyFilter ? System.Drawing.Color.FromArgb(212, 228, 248) : Color.White;
                if (_FilterConditionCtl.AllowEmptyFilter)
                {
                    _ToolTip.SetToolTip(panBottom, "查询绿色通道,允许查询所有数据");
                }
            }
        }

        #endregion

        private void tabCtlFilterMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e) {
            if (_AdvanceFilter == null) {
                showAdvanceFilter();
            }
        }

        private void FrmQueryFilterInput_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                executeQuery();
            }
        }

        public void BeforeShowDataAssistant(object sender, InvokeDataAssistantHosterEventArgs args)
        {
            IInvokeDataAssistantHoster hoster = this.ClientRuleObject as IInvokeDataAssistantHoster;
            if (hoster != null)
            {
                if (tabCtlFilterMain.SelectedTabPage.Equals(tPageAdvanceFilter))
                {
                    args.PreFilterParameters = GetAdvanceQueryParameters(); //modify by aifang 2012-07-30 args.PreFilterParameters = _AdvanceFilter.GetQueryParameters();
                }
                else
                {
                    IPreFilterConditionCtl preFilter = _SimpleFilterControl as IPreFilterConditionCtl;
                    args.PreFilterParameters = preFilter.GetPreQueryParameters();
                }

                //校验当前列的限制列是否已有值
                var param = _DataElementCfgs[args.ClumnEditCfgInfo.Name].FilterLimits;
                if (param != null && param.Count > 0)
                {
                    foreach (var filter in param)
                    {
                        if (filter.Nullable) continue;
                        if (args.PreFilterParameters != null && args.PreFilterParameters.Length > 0)
                        {
                            var limit = args.PreFilterParameters.Where(o => o.PropertyName.Equals(filter.Name)).ToList();
                            if (limit != null && limit.Count > 0) continue;
                        }

                        string description = _Columns[filter.Name].Description;
                        throw new MB.Util.APPException(string.Format("请先选择{0}", description), APPMessageType.DisplayToUser);
                    }
                }
                
                hoster.BeforeShowDataAssistant(sender, args);
            }
        }

        public void BeforeGetFilterData(object sender, InvokeDataAssistantHosterEventArgs args)
        {
            IInvokeDataAssistantHoster hoster = this.ClientRuleObject as IInvokeDataAssistantHoster;
            if (hoster != null)
            {
                if (tabCtlFilterMain.SelectedTabPage.Equals(tPageAdvanceFilter))
                {
                    args.PreFilterParameters = GetAdvanceQueryParameters(); //modify by aifang 2012-07-30 args.PreFilterParameters = _AdvanceFilter.GetQueryParameters();
                }
                else {
                    IPreFilterConditionCtl preFilter = _SimpleFilterControl as IPreFilterConditionCtl;
                    args.PreFilterParameters = preFilter.GetPreQueryParameters();
                }

                //校验当前列的限制列是否已有值
                var param = _DataElementCfgs[args.ClumnEditCfgInfo.Name].FilterLimits;
                if (param != null && param.Count > 0)
                {
                    foreach (var filter in param)
                    {
                        if (!filter.AllowFilterValue) continue;
                        if (args.PreFilterParameters != null && args.PreFilterParameters.Length > 0)
                        {
                            var limit = args.PreFilterParameters.Where(o => o.PropertyName.Equals(filter.Name)).ToList();
                            if (limit != null && limit.Count > 0) {
                                args.FilterParameters.Add(new Util.Model.QueryParameterInfo(filter.SourceName, limit[0].Value, filter.FilterCondition));
                            }
                        }
                    }
                }

                hoster.BeforeGetFilterData(sender, args);
            }
        }

        private void lnkDynamaicSetting_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                MB.XWinLib.Share.FrmDynamicColumnSetting frm = new XWinLib.Share.FrmDynamicColumnSetting(_ClientRuleObject);
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    //需要重新加载数据列
                    _ViewGridForm.ReloadData();
                }
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        /// <summary>
        /// 是否显示页数的控制器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbShowTotalPage_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowTotalPage.Checked)
                _ViewGridForm.IsTotalPageDisplayed = true;
            else
                _ViewGridForm.IsTotalPageDisplayed = false;
        }

        /// <summary>
        /// 大数据查询激活勾选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbQueryAll_CheckedChanged(object sender, EventArgs e) {
            if (cbQueryAll.Checked) {
                cbShowTotalPage.Checked = false;
            }
            cbShowTotalPage.Enabled = !cbQueryAll.Checked;
            nubMaxShotCount.Enabled = !cbQueryAll.Checked;
            IViewGridFormWithGreatData viewWithGreatData = _ViewGridForm as IViewGridFormWithGreatData;
            if (viewWithGreatData != null) {
                viewWithGreatData.IsQueryAll = cbQueryAll.Checked;
            }

        }

        


    } 
}

