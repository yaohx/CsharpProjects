//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-04-01
// Description	:	自定义条件过滤编辑控件。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MB.WinBase.Common;
using MB.WinBase.Ctls;
using MB.Util.Model;
using MB.WinBase.IFace;

namespace MB.WinClientDefault.QueryFilter {
    [ToolboxItem(true)]
    public partial class ucFilterCondition : UserControl, MB.WinBase.IFace.IFilterConditionCtl,MB.WinBase.IFace.IPreFilterConditionCtl {
        private static readonly string MSG_NOT_ALLOW_NULL_ABLE = "不能为空,请输入";
        private static readonly string DATA_FILTER_MAIN_CFG_NAME = "MainDataFilter";
        private const int DEAFULT_ROW_HEIGHT = 28;
        private const float MARGE_WIDTH = 2f;
        private const int EDIT_CTL_WIDTH = 214;
        //最少行数，如果少于指定的行数将出现滚动条。
        private int _MinRows = 0;

        private Dictionary<string,MB.WinBase.Binding.ColumnBindingInfo> _EditColumnCtlBinding;
        private FilterElementCfgs _DataElementCfgs;
        private MB.WinBase.IFace.IClientRuleQueryBase _ClientRule;
        private string _FilterCfgName;
        private System.Windows.Forms.ErrorProvider _ErrorProvider;
        private MB.WinBase.IFace.IViewGridForm _ViewGridForm;

        public ucFilterCondition() {
            InitializeComponent();
        }

        public ucFilterCondition(MB.WinBase.IFace.IClientRuleQueryBase clientRule, string filterCfgName) : this(null, clientRule, filterCfgName) {
        }

        /// <summary>
        /// 构造函数...
        /// </summary>
        public ucFilterCondition(MB.WinBase.IFace.IViewGridForm viewGridForm,  MB.WinBase.IFace.IClientRuleQueryBase clientRule, string filterCfgName) {
            InitializeComponent();

            _ViewGridForm = viewGridForm;
            _ClientRule = clientRule;
            if (!string.IsNullOrEmpty(filterCfgName))
                _FilterCfgName = filterCfgName;
            else
                _FilterCfgName = DATA_FILTER_MAIN_CFG_NAME;
            this.Load += new EventHandler(ucFilterCondition_Load);

            _ErrorProvider = new ErrorProvider();
            _ErrorProvider.ContainerControl = this;
        }

        void ucFilterCondition_Load(object sender, EventArgs e) {
            if (MB.Util.General.IsInDesignMode()) return;

            if (_EditColumnCtlBinding != null) return;

            _EditColumnCtlBinding = new Dictionary<string, MB.WinBase.Binding.ColumnBindingInfo>();
            iniLayout(panFilterPan);
          

            if(_ClientRule!=null)
                loadFilterControl();

            
        }

        #region public 成员...
        /// <summary>
        /// 获取是否可以允许空查询
        /// </summary>
        public bool AllowEmptyFilter {
            get {
                if (_DataElementCfgs == null)
                    return false;
                else
                    return _DataElementCfgs.AllowEmptyFilter;  
            }
        }

        /// <summary>
        /// 获取已选择的条件。
        /// </summary>
        /// <returns></returns>
        public MB.Util.Model.QueryParameterInfo[] GetPreQueryParameters()
        {
            List<MB.Util.Model.QueryParameterInfo> parameters = new List<MB.Util.Model.QueryParameterInfo>();

            foreach (MB.WinBase.Binding.ColumnBindingInfo bindingInfo in _EditColumnCtlBinding.Values)
            {
                if (!_DataElementCfgs.ContainsKey(bindingInfo.ColumnName))
                    throw new MB.Util.APPException(string.Format("DataFilterElements配置中不包含列 {0}", bindingInfo.ColumnName), MB.Util.APPMessageType.SysErrInfo);

                DataFilterElementCfgInfo filterCfgInfo = _DataElementCfgs[bindingInfo.ColumnName];
                MB.WinBase.Common.EditControlType controlType = (EditControlType)Enum.Parse(typeof(EditControlType), filterCfgInfo.EditControlType);
                Control ctl = bindingInfo.BindingControl;
                //先情况错误的消息描述。
                _ErrorProvider.SetError(ctl, string.Empty);
                switch (controlType)
                {
                    case EditControlType.TextBox:
                    case EditControlType.Combox_DropDown:
                    case EditControlType.Combox_DropDownList:
                    case EditControlType.ClickButtonInput:
                        if (!string.IsNullOrEmpty(ctl.Text.Trim()))
                        {
                            MB.Util.Model.QueryParameterInfo parInfo = new MB.Util.Model.QueryParameterInfo(bindingInfo.ColumnName, ctl.Text, filterCfgInfo.FilterCondition, filterCfgInfo.LimitColumn);
                            if (controlType == EditControlType.TextBox)
                            {
                                parInfo.MultiValue = filterCfgInfo.AllowMultiValue;
                            }
                            //add by aifang 2012-04-10 支持多选查询 begin
                            else if (controlType == EditControlType.ClickButtonInput)
                            {
                                parInfo.MultiValue = filterCfgInfo.AllowMultiValue;
                            }
                            //end
                            parameters.Add(parInfo);
                        }
                        break;
                    case EditControlType.CheckBox:
                        CheckBox cBox = ctl as CheckBox;
                        MB.Util.Model.QueryParameterInfo bparInfo = new MB.Util.Model.QueryParameterInfo(bindingInfo.ColumnName, cBox.Checked, filterCfgInfo.FilterCondition, filterCfgInfo.LimitColumn);
                        bparInfo.DataType = "System.Boolean";
                        parameters.Add(bparInfo);

                        break;
                    case EditControlType.ComboCheckedListBox:
                        MB.WinBase.Ctls.ucComboCheckedListBox comboListBox = ctl as MB.WinBase.Ctls.ucComboCheckedListBox;
                        string str = string.Join(",", comboListBox.GetAllCheckedItemsKey());
                        if (!string.IsNullOrEmpty(str))
                        {
                            MB.Util.Model.QueryParameterInfo parInfo = new MB.Util.Model.QueryParameterInfo(bindingInfo.ColumnName, str, filterCfgInfo.FilterCondition, filterCfgInfo.LimitColumn);
                            parInfo.MultiValue = true; //如果设置为该控件，那么就必须允许输入多个值。
                            parameters.Add(parInfo);
                        }

                        break;
                    case EditControlType.LookUpEdit:
                        ComboBox box = ctl as ComboBox;
                        if (box.SelectedIndex >= 0 && !string.IsNullOrEmpty(box.Text.Trim()))
                            parameters.Add(new MB.Util.Model.QueryParameterInfo(bindingInfo.ColumnName, box.SelectedValue, filterCfgInfo.FilterCondition, filterCfgInfo.LimitColumn));

                        break;
                    case EditControlType.DateFilterCtl:
                        MB.WinBase.Ctls.ucEditDateFilter dateFilter = ctl as MB.WinBase.Ctls.ucEditDateFilter;
                        if (dateFilter.DateFilterType != MB.WinBase.Ctls.ucEditDateFilter.DateFilterEditType.None)
                        {
                            MB.Util.Model.DateFilterStruct dateFilterValue = dateFilter.CurrentSettingValue;
                            MB.Util.Model.QueryParameterInfo dateInfo = new MB.Util.Model.QueryParameterInfo(bindingInfo.ColumnName,
                                                                            dateFilterValue.BeginDate, MB.Util.DataFilterConditions.Between, filterCfgInfo.LimitColumn);
                            dateInfo.DataType = "DateTime";
                            dateInfo.Value2 = dateFilterValue.EndDate;

                            //服务端不在过滤时间的问题
                            if (dateInfo.Value != null)
                                dateInfo.Value = DateTime.Parse(((DateTime)dateInfo.Value).ToString(filterCfgInfo.Formate));
                            if (dateInfo.Value2 != null)
                                dateInfo.Value2 = DateTime.Parse(((DateTime)dateInfo.Value2).ToString(filterCfgInfo.Formate));

                            parameters.Add(dateInfo);
                        }
                        break;

                    case EditControlType.PopupRegionEdit:
                        MB.WinBase.Ctls.ucPopupRegionEdit regionEdit = ctl as MB.WinBase.Ctls.ucPopupRegionEdit;
                        if (_ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit != null && _ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit.ContainsKey(filterCfgInfo.Name))
                        {
                            regionEdit.ColumnEditCfgInfo = _ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit[filterCfgInfo.Name];

                            var curRegion = regionEdit.CurRegionEdit;
                            foreach (var data in regionEdit.ColumnEditCfgInfo.EditCtlDataMappings)
                            {
                                object obj = MB.Util.MyReflection.Instance.InvokePropertyForGet(curRegion, data.SourceColumnName);
                                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                                {
                                    MB.Util.Model.QueryParameterInfo param = new QueryParameterInfo(data.ColumnName, obj, MB.Util.DataFilterConditions.Equal);
                                    parameters.Add(param);
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return parameters.ToArray();
        }

        /// <summary>
        /// 根据输入的条件获取查询的参数。
        /// </summary>
        /// <returns></returns>
        public MB.Util.Model.QueryParameterInfo[] GetQueryParameters() {
            List<MB.Util.Model.QueryParameterInfo> parameters = new List<MB.Util.Model.QueryParameterInfo>();
            bool validated = true;

            foreach (MB.WinBase.Binding.ColumnBindingInfo bindingInfo in _EditColumnCtlBinding.Values) {
                if (!_DataElementCfgs.ContainsKey(bindingInfo.ColumnName))
                    throw new MB.Util.APPException(string.Format("DataFilterElements配置中不包含列 {0}",bindingInfo.ColumnName), MB.Util.APPMessageType.SysErrInfo);
  
                DataFilterElementCfgInfo filterCfgInfo = _DataElementCfgs[bindingInfo.ColumnName];
                MB.WinBase.Common.EditControlType controlType = (EditControlType)Enum.Parse(typeof(EditControlType), filterCfgInfo.EditControlType);
                Control ctl = bindingInfo.BindingControl;
                //先情况错误的消息描述。
                _ErrorProvider.SetError(ctl, string.Empty);
                switch (controlType) {
                    case EditControlType.TextBox:
                    case EditControlType.Combox_DropDown:
                    case EditControlType.Combox_DropDownList:
                    case EditControlType.ClickButtonInput:
                        if (!string.IsNullOrEmpty(ctl.Text.Trim())) {
                            MB.Util.Model.QueryParameterInfo parInfo = new MB.Util.Model.QueryParameterInfo(bindingInfo.ColumnName, ctl.Text, filterCfgInfo.FilterCondition, filterCfgInfo.LimitColumn);
                            
                            if (controlType == EditControlType.TextBox || 
                                controlType == EditControlType.ClickButtonInput) {

                                parInfo.MultiValue = filterCfgInfo.AllowMultiValue;

                                // 生成子查询条件支持多值模糊查询.XiaoMin
                                createChildQueryParameterForMultiValue(parInfo);
                            }
                            //end
                            parameters.Add(parInfo);
                        }
                        else {
                            if (!filterCfgInfo.Nullable) {
                                _ErrorProvider.SetError(ctl, MSG_NOT_ALLOW_NULL_ABLE);
                                validated = false;
                            }
                        }
                        break;
                    case EditControlType.CheckBox:
                        CheckBox cBox = ctl as CheckBox;
                        MB.Util.Model.QueryParameterInfo bparInfo = new MB.Util.Model.QueryParameterInfo(bindingInfo.ColumnName, cBox.Checked, filterCfgInfo.FilterCondition, filterCfgInfo.LimitColumn);
                        bparInfo.DataType = "System.Boolean";
                        parameters.Add(bparInfo);
                        
                        break;
                    case EditControlType.ComboCheckedListBox:
                        MB.WinBase.Ctls.ucComboCheckedListBox comboListBox = ctl as MB.WinBase.Ctls.ucComboCheckedListBox;
                        string str = string.Join(",", comboListBox.GetAllCheckedItemsKey());
                        if (!string.IsNullOrEmpty(str)) {
                            MB.Util.Model.QueryParameterInfo parInfo = new MB.Util.Model.QueryParameterInfo(bindingInfo.ColumnName, str, filterCfgInfo.FilterCondition, filterCfgInfo.LimitColumn);
                            parInfo.MultiValue = true; //如果设置为该控件，那么就必须允许输入多个值。
                            parameters.Add(parInfo);
                        }
                        else {
                            if (!filterCfgInfo.Nullable) {
                                _ErrorProvider.SetError(ctl, MSG_NOT_ALLOW_NULL_ABLE);
                                validated = false;
                            }
                        }
                        break;
                    case EditControlType.LookUpEdit:
                        ComboBox box = ctl as ComboBox;
                        if (box.SelectedIndex >= 0 && !string.IsNullOrEmpty(box.Text.Trim()))
                            parameters.Add(new MB.Util.Model.QueryParameterInfo(bindingInfo.ColumnName, box.SelectedValue, filterCfgInfo.FilterCondition, filterCfgInfo.LimitColumn));
                        else {
                            if (!filterCfgInfo.Nullable) {
                                _ErrorProvider.SetError(ctl, MSG_NOT_ALLOW_NULL_ABLE);
                                validated = false;
                            }
                        }
                        break;
                    case EditControlType.DateFilterCtl:
                        MB.WinBase.Ctls.ucEditDateFilter dateFilter = ctl as MB.WinBase.Ctls.ucEditDateFilter;
                        if (dateFilter.DateFilterType != MB.WinBase.Ctls.ucEditDateFilter.DateFilterEditType.None) {
                            MB.Util.Model.DateFilterStruct dateFilterValue = dateFilter.CurrentSettingValue;
                            MB.Util.Model.QueryParameterInfo dateInfo = new MB.Util.Model.QueryParameterInfo(bindingInfo.ColumnName,
                                                                            dateFilterValue.BeginDate, MB.Util.DataFilterConditions.Between, filterCfgInfo.LimitColumn);
                            dateInfo.DataType = "DateTime";
                            dateInfo.Value2 = dateFilterValue.EndDate;

                            //服务端不在过滤时间的问题
                            if (dateInfo.Value != null)
                                dateInfo.Value = DateTime.Parse(((DateTime)dateInfo.Value).ToString(filterCfgInfo.Formate));
                            if (dateInfo.Value2 != null)
                                dateInfo.Value2 = DateTime.Parse(((DateTime)dateInfo.Value2).ToString(filterCfgInfo.Formate));

                            parameters.Add(dateInfo);
                        }
                        else {
                            if (!filterCfgInfo.Nullable) {
                                _ErrorProvider.SetError(ctl, MSG_NOT_ALLOW_NULL_ABLE);
                                validated = false;
                            }
                        }
                        break;
                    case EditControlType.PopupRegionEdit:
                        MB.WinBase.Ctls.ucPopupRegionEdit regionEdit = ctl as MB.WinBase.Ctls.ucPopupRegionEdit;
                        if (_ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit != null && _ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit.ContainsKey(filterCfgInfo.Name))
                        {
                            regionEdit.ColumnEditCfgInfo = _ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit[filterCfgInfo.Name];

                            var curRegion = regionEdit.CurRegionEdit;
                            foreach (var data in regionEdit.ColumnEditCfgInfo.EditCtlDataMappings)
                            {
                                object obj = MB.Util.MyReflection.Instance.InvokePropertyForGet(curRegion, data.SourceColumnName);
                                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                                {
                                    MB.Util.Model.QueryParameterInfo param = new QueryParameterInfo(data.ColumnName, obj, MB.Util.DataFilterConditions.Equal);
                                    parameters.Add(param);
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            if (!validated)
                throw new MB.Util.APPException("查询条件输入有误,请检查！", MB.Util.APPMessageType.DisplayToUser);
            return parameters.ToArray();
        }

        /// <summary>
        /// 为逗号分割的多个值生成子查询参数,以OR连接.用于模糊查询.
        /// </summary>
        /// <param name="parInfo">原始的查询参数</param>
        private void createChildQueryParameterForMultiValue(QueryParameterInfo parInfo)
        {
            if (parInfo.Condition == Util.DataFilterConditions.BenginsWith
                || parInfo.Condition == Util.DataFilterConditions.EndsWith
                || parInfo.Condition == Util.DataFilterConditions.Like)
            {
                // 多值模糊查询
                if (parInfo.MultiValue && parInfo.Value != null)
                {
                    string multipleValue = parInfo.Value.ToString();

                    // 逗号分割
                    if (multipleValue.Contains(","))
                    {
                        parInfo.IsGroupNode = true;
                        parInfo.GroupNodeLinkType = QueryGroupLinkType.OR;
                        var values = multipleValue.Split(new char[] { ',' });

                        foreach (var value in values)
                        {
                            // 忽略空字符串
                            if (!string.IsNullOrEmpty(value.Trim()))
                            {
                                var childQueryParameter = new QueryParameterInfo(parInfo.PropertyName, value, parInfo.Condition, parInfo.Limited);
                                                          
                                if (parInfo.Childs == null)
                                {
                                    parInfo.Childs = new List<QueryParameterInfo>();
                                }

                                parInfo.Childs.Add(childQueryParameter);
                            }
                        }
                    }
                }
            }
        }

        #endregion public 成员...

        #region 查询控件初始化...
        private void loadFilterControl() {
            _DataElementCfgs = MB.WinBase.LayoutXmlConfigHelper.Instance.GetDataFilterCfgElements(_ClientRule.ClientLayoutAttribute.UIXmlConfigFile, _FilterCfgName);

            if (_DataElementCfgs == null || _DataElementCfgs.Count == 0) {
                throw new MB.Util.APPException("该对象在对应的XML配置文件中没有配置查询项！",MB.Util.APPMessageType.DisplayToUser); 
            }
            this.MinVisibleRows = _DataElementCfgs.Count;

            Dictionary<string, ColumnPropertyInfo> columns = _ClientRule.UIRuleXmlConfigInfo.GetDefaultColumns();
            int row = 0;
            foreach (DataFilterElementCfgInfo elementInfo in _DataElementCfgs.Values) {
                Label newLab = createLabel(elementInfo);
                panFilterPan.Controls.Add(newLab, 0, row);

                Panel pnl = new Panel();
                pnl.Size = new System.Drawing.Size(EDIT_CTL_WIDTH, Convert.ToInt32(panFilterPan.RowStyles[0].Height-MARGE_WIDTH));
                panFilterPan.Controls.Add(pnl, 1, row);

                Control newEdit = createEditCtl(elementInfo);
                //newEdit.Width = EDIT_CTL_WIDTH;
                newEdit.Dock = DockStyle.Fill;
                pnl.Controls.Add(newEdit);
                pnl.BringToFront();

                _EditColumnCtlBinding.Add(elementInfo.Name,new MB.WinBase.Binding.ColumnBindingInfo(elementInfo.Name, columns[elementInfo.Name], newEdit));
                row += 1;
            }
        }
        #endregion 查询控件初始化...
        
        //动态创建Label
        private Label createLabel(DataFilterElementCfgInfo filterCfgInfo) {
            Dictionary<string, ColumnPropertyInfo> columns = _ClientRule.UIRuleXmlConfigInfo.GetDefaultColumns();
            Label lab = new Label();
            lab.Text = columns[filterCfgInfo.Name].Description + "：";
            lab.Size = new System.Drawing.Size(Convert.ToInt32(panFilterPan.ColumnStyles[0].Width), Convert.ToInt32(panFilterPan.RowStyles[0].Height - MARGE_WIDTH));
            lab.TextAlign = ContentAlignment.MiddleLeft;
            
            if (!filterCfgInfo.Nullable) {
                lab.ForeColor = Color.Red;
            }
            return lab;
        }
        //创建编辑控件。
        private Control createEditCtl(DataFilterElementCfgInfo filterCfgInfo) {
            MB.WinBase.IFace.IDataFilterElementHoster hoster = _ClientRule as MB.WinBase.IFace.IDataFilterElementHoster;

            Dictionary<string, ColumnPropertyInfo> columns = _ClientRule.UIRuleXmlConfigInfo.GetDefaultColumns();
            MB.WinBase.Common.EditControlType controlType = MB.WinBase.Common.EditControlType.None;
            try {

                string editCtlType = filterCfgInfo.EditControlType;
                //如果在查询带没有配置那么根据名称在 编辑带查找
                if (string.IsNullOrEmpty(editCtlType)) {
                    if (_ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit!=null && _ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit.ContainsKey(filterCfgInfo.Name))
                        editCtlType = _ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit[filterCfgInfo.Name].EditControlType;
                    //如果在编辑带还没有找到就根据数据类型创建
                    if (string.IsNullOrEmpty(editCtlType)) {
                        editCtlType = getEditControlTypeByDayaType(columns[filterCfgInfo.Name].DataType).ToString();
                    }
                }
                controlType = (EditControlType)Enum.Parse(typeof(EditControlType), editCtlType);
                filterCfgInfo.EditControlType = controlType.ToString();
            }
            catch {
                throw new MB.Util.APPException(string.Format("控件类型{0} 配置有误，请检查！", filterCfgInfo.EditControlType), MB.Util.APPMessageType.SysErrInfo); 
            }
            MB.WinBase.Atts.EditControlTypeAttribute ctlAtt = MB.WinBase.Atts.AttributeConfigHelper.Instance.GetEditControlTypeAtt(controlType);
            Control newCtl = ctlAtt.FormCtlType.Assembly.CreateInstance(ctlAtt.FormCtlType.FullName,true) as Control;
            switch (controlType) {
                case EditControlType.Combox_DropDown:
                    (newCtl as ComboBox).DropDownStyle = ComboBoxStyle.DropDown;
                    MB.WinBase.Binding.BindingSourceHelper.Instance.FillCombox(newCtl, filterCfgInfo.Name, _ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit, false);
                    break;
                case EditControlType.Combox_DropDownList:
                    (newCtl as ComboBox).DropDownStyle = ComboBoxStyle.DropDownList;
                    MB.WinBase.Binding.BindingSourceHelper.Instance.FillCombox(newCtl, filterCfgInfo.Name, _ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit, true);
                    break;
                case EditControlType.LookUpEdit:
                    (newCtl as ComboBox).DropDownStyle = ComboBoxStyle.DropDownList;
                     MB.WinBase.Binding.BindingSourceHelper.Instance.FillComboxLookUp(newCtl, filterCfgInfo.Name,_ClientRule.UIRuleXmlConfigInfo.GetDefaultColumns() , _ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit, true);
                    break;
                case EditControlType.CheckBox:
                    (newCtl as CheckBox).Text = columns[filterCfgInfo.Name].Description;
                    break;
                case EditControlType.ComboCheckedListBox:
                    MB.WinBase.Binding.BindingSourceHelper.Instance.FillComboCheckListBox(newCtl, filterCfgInfo.Name,_ClientRule.UIRuleXmlConfigInfo.GetDefaultColumns() , _ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit);
                    break;
                case EditControlType.ClickButtonInput:
                    MB.WinBase.Ctls.ucClickButtonInput clickButton = newCtl as MB.WinBase.Ctls.ucClickButtonInput;
                    
                    if (_ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit != null && _ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit.ContainsKey(filterCfgInfo.Name)) {
                        clickButton.ColumnEditCfgInfo = _ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit[filterCfgInfo.Name];
                        clickButton.FilterElementInfo = filterCfgInfo;
                        clickButton.ClientRule = this._ClientRule;
                    }
                    break;
                //add by aifang 2012-03-28 begin 根据xml配置设置日期显示的格式
                case EditControlType.DateFilterCtl:
                    MB.WinBase.Ctls.ucEditDateFilter dateFilter = newCtl as MB.WinBase.Ctls.ucEditDateFilter;
                    dateFilter.DateFilterType = WinBase.Ctls.ucEditDateFilter.DateFilterEditType.Today;  //所有查询字段默认为当天 edit by aifang
                    if (string.IsNullOrEmpty(filterCfgInfo.Formate)) filterCfgInfo.Formate = "yyyy-MM-dd";
                    dateFilter.Formate = filterCfgInfo.Formate;
                    break;
                //add by aifang 2012-03-28 end

                case EditControlType.PopupRegionEdit:
                    MB.WinBase.Ctls.ucPopupRegionEdit regionEdit = newCtl as MB.WinBase.Ctls.ucPopupRegionEdit;
                    if (_ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit != null && _ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit.ContainsKey(filterCfgInfo.Name)) {
                        regionEdit.ColumnEditCfgInfo = _ClientRule.UIRuleXmlConfigInfo.ColumnsCfgEdit[filterCfgInfo.Name];
                    }
                    break;
                default:
                    //
                    break;

            }
            if (hoster != null)
                hoster.AfterCreateFilterCtl(newCtl, filterCfgInfo);
 
            return newCtl;
        }

        //根据列的类型返回需要创建的控件类型。
        private EditControlType getEditControlTypeByDayaType(string columnDataTypeName) {
            switch (columnDataTypeName) {
                case "System.Boolean":
                case "System.Boolean?":
                    return EditControlType.CheckBox;
                case "System.DateTime":
                case "System.DateTime?":
                    return EditControlType.DateFilterCtl;
                default:
                    return EditControlType.TextBox;
            }
        }

        #region 扩展public 属性...
        /// <summary>
        /// 最少显示的行数。
        /// </summary>
        [Description("获取或者设置至少显示的行数。")]
        public int MinVisibleRows {
            get {
                return _MinRows;
            }
            set {
                _MinRows = value;
                iniLayout(panFilterPan);
 
            }
        }
        #endregion 扩展public 属性...

        #region 布局...
        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
        }

        private void iniLayout(TableLayoutPanel tlPanel) {

            tlPanel.Height = _MinRows * DEAFULT_ROW_HEIGHT + System.Convert.ToInt32(_MinRows * MARGE_WIDTH);
            int rowCount = _MinRows;
            tlPanel.RowStyles.Clear();
            tlPanel.RowCount = rowCount ;
            for (int i = 0; i < rowCount ; i++) {
                tlPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, DEAFULT_ROW_HEIGHT));
            }
        }
        #endregion 布局...
    }
}
