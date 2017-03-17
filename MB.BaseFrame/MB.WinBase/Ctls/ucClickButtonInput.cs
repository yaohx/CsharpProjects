//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-03-26。
// Description	:	Click Button Input 自定义控件。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.WinBase.Common;
using MB.WinBase.IFace;
using MB.Util.Model;
using MB.WinBase.Atts;
using MB.Util;

namespace MB.WinBase.Ctls {
    /// <summary>
    /// 点击查找数据编辑控件。
    /// </summary>
    [ToolboxItem(true)]
    public partial class ucClickButtonInput : UserControl
    {
        private MB.WinBase.Common.ColumnEditCfgInfo _ColumnEditCfgInfo;
        private MB.WinBase.IFace.IQueryObject _QueryObject;
        private bool _MultiSelect;
        private bool _AllowInput;
        private bool _HideFilterPane;
        private bool _ButtonHasLoad;
        private System.Windows.Forms.ErrorProvider _ErrorProvider;

        private MB.WinBase.IFace.IDataAssistant _FrmDataAssistant;

        private IClientRuleQueryBase _ClientRule;

        #region 自定义时间处理相关...
        private System.EventHandler<MB.WinBase.IFace.GetObjectDataAssistantEventArgs> _AfterSelectedData;
        public event System.EventHandler<MB.WinBase.IFace.GetObjectDataAssistantEventArgs> AfterSelectedData
        {
            add
            {
                _AfterSelectedData += value;
            }
            remove
            {
                _AfterSelectedData -= value;
            }
        }
        private void onAfterSelectedData(MB.WinBase.IFace.GetObjectDataAssistantEventArgs arg)
        {
            if (_AfterSelectedData != null)
                _AfterSelectedData(this, arg);
        }
        #endregion 自定义时间处理相关...

        #region 构造函数...
        /// <summary>
        ///  Click Button Input 自定义控件。
        /// </summary>
        public ucClickButtonInput()
        {
            InitializeComponent();
            this.btnEdit.Width = this.Width;

            this.Resize += new EventHandler(ucClickButtonInput_Resize);
            this.btnEdit.Leave += new EventHandler(btnEdit_Leave);

            this.btnEdit.KeyDown += new KeyEventHandler(btnEdit_KeyDown);

            //默认情况下不可输入。
            _AllowInput = true;

            btnEdit.Properties.ReadOnly = false;
            this.Layout += new LayoutEventHandler(ucClickButtonInput_Layout);
            btnEdit.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            btnEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(btnEdit_ButtonClick);
        }

        void btnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Index == 0)
                {
                    if (_FrmDataAssistant == null || _ColumnEditCfgInfo.NeedCreate)
                        createFilterObjectData();

                    //如果创建小助手时，用户取消，则返回的是NULL
                    if (_FrmDataAssistant != null) {
                        _HideFilterPane = _ColumnEditCfgInfo.HideFilterPane;
                        _FrmDataAssistant.HideFilterPane = _HideFilterPane;
                        _FrmDataAssistant.MultiSelect = this.MultiSelect;

                        (_FrmDataAssistant as Form).ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        void btnEdit_Leave(object sender, EventArgs e)
        {
            if (_ButtonHasLoad)
            {
                if (!string.IsNullOrEmpty(this.btnEdit.Text.Trim()))
                {
                    filterObjectsByInput(false);
                }
                else
                    setNullValue();
            }
        }

        void btnEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || btnEdit.Text.Trim().Length == 0) return;

            filterObjectsByInput(true);
        }
        #endregion 构造函数...

        private bool filterObjectsByInput(bool isKeyDown)
        {
            try
            {
                if (!checkCanQueryData()) return false;

                if (_FrmDataAssistant == null || _ColumnEditCfgInfo.NeedCreate)
                    createFilterObjectData();

                if (_ErrorProvider != null)
                    _ErrorProvider.SetError(this, string.Empty);

                int docType = 0;

                bool needValidation=true;//需要验证输入数据

                // 生成查询条件,默认为Equal,该查询仅仅用于验证
                List<MB.Util.Model.QueryParameterInfo> lst = new List<MB.Util.Model.QueryParameterInfo>();
                var queryPara = new QueryParameterInfo(_ColumnEditCfgInfo.TextFieldName, this.btnEdit.Text, DataFilterConditions.Equal);
                lst.Add(queryPara);

                // 在查询窗口或者获取对象助手窗口中，需要根据FilterElementInfo信息进一步处理
                if (this.ParentForm is IQueryFilterForm || this.ParentForm is IGetObjectDataAssistant)
                {
                    if (this.FilterElementInfo != null)
                    {
                        // 多选,进行验证
                        if ((this.FilterElementInfo.FilterCondition == Util.DataFilterConditions.In || 
                            this.FilterElementInfo.FilterCondition == Util.DataFilterConditions.Equal)
                            && this.MultiSelect)
                        {
                            queryPara.Condition = Util.DataFilterConditions.In;
                        }
                        else if (this.FilterElementInfo.FilterCondition == Util.DataFilterConditions.BenginsWith
                            || this.FilterElementInfo.FilterCondition == Util.DataFilterConditions.EndsWith
                            || this.FilterElementInfo.FilterCondition == Util.DataFilterConditions.Like)
                        {
                            // 模糊查询,不进行验证
                            needValidation = false;
                        }
                    }
                }

                // 验证输入的值是否正确
                if (needValidation)
                {
                    var lstData = _FrmDataAssistant.GetFilterObjects(docType, lst);

                    if (lstData == null || lstData.Count == 0)
                    {
                        if (_ErrorProvider == null)
                            _ErrorProvider = new ErrorProvider();

                        if (isKeyDown)
                            _ErrorProvider.SetError(this, "当前输入不是合法的数据,请重新输入！");
                        else
                            btnEdit.Text = string.Empty;
                    }
                    else
                    {
                        setSelectValue(lstData, isKeyDown);

                        if (_ErrorProvider != null) _ErrorProvider = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                return false;
            }
            return true;
        }

        #region 覆盖基类的成员...
        [System.Diagnostics.DebuggerHidden()]
        public override string Text
        {
            get
            {
                //if (btnEdit.ForeColor != Color.Black)
                //    return string.Empty;
                //else
                return btnEdit.Text;
            }
            set
            {
                btnEdit.Text = value;

            }

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _ButtonHasLoad = true;
        }

        #endregion 覆盖基类的成员...

        #region 扩展的public 成员...
        /// <summary>
        /// 设置控件大小写。
        /// </summary>
        public CharacterCasing CharacterCasing
        {
            get
            {
                return btnEdit.Properties.CharacterCasing;
            }
            set
            {
                btnEdit.Properties.CharacterCasing = value;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MB.WinBase.IFace.IQueryObject QueryObject
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
        /// 获取或者设置Click Button 调用的方法描述。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MB.WinBase.Common.ColumnEditCfgInfo ColumnEditCfgInfo
        {
            get
            {
                return _ColumnEditCfgInfo;
            }
            set
            {
                _ColumnEditCfgInfo = value;
            }
        }
        /// <summary>
        /// 获取或者设置控件是否允许编辑。
        /// </summary>
        [Description("获取或者设置控件是否允许编辑。")]
        public bool ReadOnly
        {
            get
            {
                return !btnEdit.Properties.Buttons[0].Enabled;
            }
            set
            {
                btnEdit.Properties.ReadOnly = value || !_AllowInput;
                btnEdit.Properties.Buttons[0].Enabled = !value;
            }
        }

        /// <summary>
        /// 允许多选.在编辑窗口中,根据用户设置来决定;在查询窗口中,由配置文件决定
        /// </summary>
        public bool MultiSelect
        {
            set
            {
                _MultiSelect = value;
            }
            get
            {
                if (FilterElementInfo == null)
                    return _MultiSelect;
                else
                {
                    return FilterElementInfo.AllowMultiValue;
                }
            }
        }

        /// <summary>
        /// 配置文件中,该字段的查询方式的信息.查询条件决定了验证的方式,如果查询条件为Like,BeginWith等,则不验证
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataFilterElementCfgInfo FilterElementInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 判断是否显示过滤带，如果为False 那么只显示数据选择列表项。
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
        /// <summary>
        /// 判断文本框是否允许编辑。
        /// </summary>
        [Description("判断文本框是否允许编辑")]
        public bool AllowInput
        {
            get
            {
                return _AllowInput;
            }
            set
            {
                _AllowInput = value;
                btnEdit.Properties.ReadOnly = !value;
            }
        }
        /// <summary>
        /// 获取点击默认调用的数据查找窗口,由于该窗口目前基本上满足所有数据的查找功能，暂时不提供扩展。
        /// </summary>
        [Description("获取点击默认调用的数据查找窗口,由于该窗口目前基本上满足所有数据的查找功能，暂时不提供扩展。")]
        public string QueryFilterForm
        {
            get
            {
                return "MB.WinClientDefault.QueryFilter.FrmGetObjectDataAssistant,MB.WinClientDefault.Dll";
            }
        }

        public IClientRuleQueryBase ClientRule
        {
            get { return _ClientRule; }
            set { _ClientRule = value; }
        }

        #endregion 扩展的public 成员...

        #region 布局的 时候需要双重设置，否则会出现这样那样一些问题...
        void ucClickButtonInput_Resize(object sender, EventArgs e)
        {
            this.Height = btnEdit.Height;
        }

        void ucClickButtonInput_Layout(object sender, LayoutEventArgs e)
        {
            this.Height = btnEdit.Height;
        }
        #endregion 布局的 时候需要双重设置，否则会出现这样那样一些问题...

        void assistant_AfterGetObjectData(object sender, MB.WinBase.IFace.GetObjectDataAssistantEventArgs arg)
        {

            onAfterSelectedData(arg);
            if (arg.Handed) return;

            if (arg.SelectedRows == null && arg.SelectedRows.Length == 0) return;

            try
            {
                setSelectValue(arg.SelectedRows, false);
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write("获取数据后设置控件值出错，请检查对应的EditCtlDataMappings 配置是否有误！" + ex.Message);
                MB.WinBase.MessageBoxEx.Show("获取数据后设置控件值出错，请检查对应的EditCtlDataMappings 配置是否有误！");
            }
        }

        #region 设置控件值处理相关...
        //创建查询过滤对象
        private void createFilterObjectData()
        {

            System.Windows.Forms.Control parentHoster = MB.WinBase.ShareLib.Instance.GetInvokeDataHosterControl(this.Parent);
            if (parentHoster != null)
            {
                _FrmDataAssistant = MB.WinBase.ObjectDataFilterAssistantHelper.Instance.CreateDataAssistantObject(this, _ColumnEditCfgInfo, parentHoster);
            }
            else
            {
                MB.WinBase.IFace.IBaseDataBindingEdit editForm = getDirectnessBaseEdit(this) as MB.WinBase.IFace.IBaseDataBindingEdit;
                if (editForm == null)
                    editForm = this.ParentForm as MB.WinBase.IFace.IBaseDataBindingEdit;
                object currentEditEntity = null;
                if (editForm != null)
                {
                    currentEditEntity = editForm.CurrentEditEntity;
                }

                if (_ClientRule == null)
                {
                    parentHoster = MB.WinBase.ShareLib.Instance.GetControlParentForm(this.Parent);
                    IForm frm = parentHoster as IForm;
                    if (frm != null) _ClientRule = frm.ClientRuleObject;

                }
                _FrmDataAssistant = MB.WinBase.ObjectDataFilterAssistantHelper.Instance.CreateDataAssistantObject(this, currentEditEntity, _ColumnEditCfgInfo, _ClientRule);
            }
            if (_FrmDataAssistant != null)
            {
                _FrmDataAssistant.MaxSelectRows = this._ColumnEditCfgInfo.MaxSelectRows;
                _FrmDataAssistant.MultiSelect = this.MultiSelect;
                _FrmDataAssistant.QueryObject = _QueryObject;
                _FrmDataAssistant.AfterGetObjectData += new MB.WinBase.IFace.GetObjectDataAssistantEventHandle(assistant_AfterGetObjectData);
            }
        }

        //设置选择行的值
        private void setSelectValue(IList rows, bool isKeyDown)
        {
            //先判断上级控件
            MB.WinBase.IFace.IBaseDataBindingEdit editForm = getDirectnessBaseEdit(this) as MB.WinBase.IFace.IBaseDataBindingEdit;
            if (editForm == null)
                editForm = this.ParentForm as MB.WinBase.IFace.IBaseDataBindingEdit;

            if (editForm != null)
            {
                object mainEntity = editForm.CurrentEditEntity;
                try
                {
                    if (rows != null && rows.Count > 0)
                    {
                        foreach (MB.WinBase.Common.EditCtlDataMappingInfo info in _ColumnEditCfgInfo.EditCtlDataMappings)
                        {
                            object dataObject = rows[0];

                            object val = null;
                            if (!string.IsNullOrEmpty(info.SourceColumnName))
                                val = MB.Util.MyReflection.Instance.InvokePropertyForGet(rows[0], info.SourceColumnName);
                            object valDesc = MB.Util.MyReflection.Instance.InvokePropertyForGet(mainEntity, info.ColumnName);
                            if (val != null)
                            {
                                if ((valDesc == null || string.Compare(val.ToString(), valDesc.ToString(), true) != 0))
                                    MB.Util.MyReflection.Instance.InvokePropertyForSet(mainEntity, info.ColumnName, val.ToString());
                            }
                            else
                            {
                                MB.Util.MyReflection.Instance.InvokePropertyForSet(mainEntity, info.ColumnName, null);
                            }
                        }
                    }
                    else
                    {
                        if (!isKeyDown)
                        {
                            foreach (MB.WinBase.Common.EditCtlDataMappingInfo info in _ColumnEditCfgInfo.EditCtlDataMappings)
                            {
                                MB.Util.MyReflection.Instance.InvokePropertyForSet(mainEntity, info.ColumnName, null);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new MB.Util.APPException("请检查EditCtlDataMappingInfo 的Mapping 信息是否正确 ", MB.Util.APPMessageType.SysErrInfo, ex);
                }
            }
            else
            {
                if (rows != null && rows.Count > 0)
                    this.btnEdit.Text = convertToString(rows, true);
                else
                    if (!isKeyDown)
                        this.btnEdit.Text = string.Empty;
            }
        }
        private void setNullValue()
        {
            //先判断上级控件
            MB.WinBase.IFace.IBaseDataBindingEdit editForm = getDirectnessBaseEdit(this) as MB.WinBase.IFace.IBaseDataBindingEdit;
            if (editForm == null)
                editForm = this.ParentForm as MB.WinBase.IFace.IBaseDataBindingEdit;

            if (editForm != null)
            {
                object mainEntity = editForm.CurrentEditEntity;
                foreach (MB.WinBase.Common.EditCtlDataMappingInfo info in _ColumnEditCfgInfo.EditCtlDataMappings)
                {
                    MB.Util.MyReflection.Instance.InvokePropertyForSet(mainEntity, info.ColumnName, null);
                }
            }
        }

        //判断当前输入的值和已经存在的值是否相等，如果相等那么就没必须再重新加载
        private bool checkCanQueryData()
        {
            if (_ColumnEditCfgInfo == null)
                throw new MB.Util.APPException(string.Format("ClickButtonInput 控件{0} 没有相应的配置项 ColumnEditCfgInfo", this.Name), MB.Util.APPMessageType.SysErrInfo);

            if (btnEdit.Properties.ReadOnly) return false;


            MB.WinBase.IFace.IBaseEditForm editForm = this.ParentForm as MB.WinBase.IFace.IBaseEditForm;
            if (editForm != null)
            {
                object mainEntity = editForm.CurrentEditEntity;
                object valDesc = MB.Util.MyReflection.Instance.InvokePropertyForGet(mainEntity, _ColumnEditCfgInfo.Name);
                if (valDesc != null && string.Compare(this.btnEdit.Text, valDesc.ToString(), true) == 0)
                {
                    return false;
                }
            }
            return true;

        }
        private string convertToString(IList rows, bool allowMultiSelect)
        {
            if (_ColumnEditCfgInfo == null)
                throw new MB.Util.APPException(string.Format("ClickButtonInput 控件{0} 没有相应的配置项 ColumnEditCfgInfo", this.Name), MB.Util.APPMessageType.SysErrInfo);

            string str = string.Empty;
            foreach (object o in rows)
            {
                object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(o, _ColumnEditCfgInfo.TextFieldName);
                str += val.ToString() + ",";
                if (!allowMultiSelect || !this.MultiSelect)
                    break;
            }
            if (str.Length > 0)
            {
                str = str.Remove(str.Length - 1, 1);
            }
            return str;
        }
        #endregion 设置控件值处理相关...

        private MB.WinBase.IFace.IBaseDataBindingEdit getDirectnessBaseEdit(Control ctl)
        {
            if (ctl.Parent == null) return null;
            MB.WinBase.IFace.IBaseDataBindingEdit baseEdit = ctl.Parent as MB.WinBase.IFace.IBaseDataBindingEdit;
            if (baseEdit != null)
                return baseEdit;
            else
                return getDirectnessBaseEdit(ctl.Parent);

        }

        private void tsmiProperty_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.btnEdit.Text)) return;

            MB.WinBase.ObjectDataFilterAssistantHelper.Instance.ShowProperty(_ColumnEditCfgInfo, _ColumnEditCfgInfo.TextFieldName, this.btnEdit.Text);
        }

    }
}
