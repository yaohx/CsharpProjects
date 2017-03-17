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
using MB.WinClientDefault.Extender;
using MB.WinClientDefault.DataImport;
using MB.WinClientDefault.DynamicGroup;

namespace MB.WinClientDefault {
    /// <summary>
    /// 对象浏览窗口。
    /// </summary>
    public partial class AbstractListViewForm : AbstractBaseForm, IViewGridForm, IViewDynamicGroupGridForm {

        protected  IClientRule _ClientRuleObject;
        protected MB.WinBase.Binding.BindingSourceEx _BindingSource;
        protected MB.Util.Model.QueryParameterInfo[] _CurrentQueryParameters;
        protected bool _IsReloadData;
        //动态聚组设置
        protected bool _IsDynamicGroupIsActive = false; //判断是否是动图聚组的view
        protected Util.Model.DynamicGroupSetting _DynamicGroupSettings; //动态聚组的设置，用做查询条件用
        protected FrmDynamicGroupSetting _DynamicGroupSettingUI; //动态聚组设定UI
        #region 构造函数...
        /// <summary>
        /// 构造函数...
        /// </summary>
        public AbstractListViewForm()
            : this(null) {
        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="clientRuleObject"></param>
        public AbstractListViewForm(IClientRule clientRuleObject) {
            InitializeComponent();
            _ClientRuleObject = clientRuleObject;
        }
        #endregion 构造函数...

        protected virtual string GetDefaultFilterMessage(MB.WinBase.Atts.DefaultDataFilter filterType) {
            string msg = "友情提示： 默认情况下 {0}";
            string desc = MB.Util.MyCustomAttributeLib.Instance.GetFieldDesc(typeof(MB.WinBase.Atts.DefaultDataFilter), filterType.ToString(), true);

            if (filterType == DefaultDataFilter.None) {
                msg += " 请通过查询条件找到你需要关注的数据";
            }
            else {
                msg += " (只包含有权限查看的数据)";
            }
            return string.Format(msg, desc);
        }
 

        #region IViewGridForm 成员

        public virtual bool IsTotalPageDisplayed { get; set; }

        public virtual int AddNew() {

            ShowObjectEditForm(ObjectEditType.AddNew);

            return 1;
        }
        public virtual int Open() {
            ShowObjectEditForm(ObjectEditType.OpenEdit);

            return 1;
        }

        public virtual int Delete() {
            if (_BindingSource.Current == null) return 0;
            if (this.GetViewDataNavigator().FocusedRowHandle < 0) return 0;

            if (MB.WinBase.UIDataEditHelper.Instance.CheckExistsDocState(_BindingSource.Current)) {
                var docState = MB.WinBase.UIDataEditHelper.Instance.GetEntityDocState(_BindingSource.Current);
                if (docState != MB.Util.Model.DocState.Progress) {
                    MB.WinBase.MessageBoxEx.Show("当前单据已在提交状态,不能进行删除操作！");
                    return 0;
                }
            }
            //add by aifang 增加逻辑：如果数据未提交保存，直接进行删除 begin
            if (MB.WinBase.UIDataEditHelper.Instance.CheckExistsEntityState(_BindingSource.Current)) {
                var entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(_BindingSource.Current);
                if (entityState == Util.Model.EntityState.New)
                {
                    MB.WinBase.UIDataEditHelper.Instance.SetEntityState(_BindingSource.Current, Util.Model.EntityState.Deleted);
                    this.GetViewDataNavigator().RemoveFocusedRow(_BindingSource);
                    _BindingSource.EndEdit();
                    return 0;
                }
            }
            //end

            DialogResult dre = MB.WinBase.MessageBoxEx.Question("删除操作不可逆,是否继续？");

            if (dre != DialogResult.Yes) return 0;

            int re = 0;
            System.ServiceModel.ICommunicationObject commObject = _ClientRuleObject.CreateServerCommunicationObject();
            try {
                _ClientRuleObject.AddToCache(commObject, (int)_ClientRuleObject.MainDataTypeInDoc, _BindingSource.Current, true, null);

                re = _ClientRuleObject.Flush(commObject);

                commObject.Close();
            }
            catch (Exception ex) {
                try {
                    commObject.Close();
                }
                catch { }

                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex, "数据删除出错,请重试！");
            }
            if (re > 0) {
               // _BindingSource.RemoveCurrent();
                this.GetViewDataNavigator().RemoveFocusedRow(_BindingSource);
                _BindingSource.EndEdit();
            }
            return 0;
        }
        /// <summary>
        /// 调用过滤查询窗口。
        /// </summary>
        /// <returns></returns>
        public virtual int Query() {
            showQueryParamterInput();

            return 1;
        }
        /// <summary>
        /// 刷新浏览界面
        /// </summary>
        /// <returns></returns>
        public virtual int Refresh() {
            if (_CurrentQueryParameters != null && _CurrentQueryParameters.Length > 0)
                LoadObjectData(_CurrentQueryParameters);
            else
                throw new MB.Util.APPException("查询条件不能为空,可能会产生大量数据流,在存在查询条件的情况下才能进行刷新操作！", MB.Util.APPMessageType.DisplayToUser);

            return 1;
        }
        /// <summary>
        /// 复制新增加处理。
        /// </summary>
        /// <returns></returns>
        public virtual int CopyAsNew() {
            MB.WinBase.MessageBoxEx.Show("目前系统还没有实现复制新增的功能!");

            return 0;
        }
        /// <summary>
        /// 数据导入处理。
        /// </summary>
        /// <returns></returns>
        public virtual int DataImport() {
            bool b = _ClientRuleObject.ShowDataImport(this, _BindingSource);
            if (!b) {
                MB.WinClientDefault.DataImport.DocDataImportHelper import = new MB.WinClientDefault.DataImport.DocDataImportHelper();
                import.ShowDataImportDialog(this, _BindingSource);
            }
            return 1;
        }
        /// <summary>
        /// 数据导出处理。
        /// </summary>
        /// <returns></returns>
        public virtual int DataExport() {
            if (_ClientRuleObject != null) {
                IImportTempletExportProvider templetExport = _ClientRuleObject as IImportTempletExportProvider;
                if (templetExport != null)
                    return templetExport.TempletExport(this);
            }
            return 0;
        }

        /// <summary>
        /// 判断是否存在未保存的数据。
        /// </summary>
        public virtual bool ExistsUnSaveData()
        {
            return false;
        }

        public virtual int Save()
        {
            throw new MB.Util.APPException("该窗口未实现保存的功能", MB.Util.APPMessageType.SysErrInfo);
        }

        public virtual int ReloadData()
        {
            _IsReloadData = true;
            return 0;
        }
        #endregion

        #region IForm 成员

        public IClientRuleQueryBase ClientRuleObject {
            get {
                return _ClientRuleObject;
            }
            set {
                _ClientRuleObject = value as IClientRule;
                
            }
        }

        #endregion

        #region 内部函数处理相关...
        //显示对象窗口
        protected virtual void ShowObjectEditForm(ObjectEditType editType) {
            if (editType != ObjectEditType.AddNew && _BindingSource.Current == null) return;


            MB.Util.Model.ModuleCommandInfo commandInfo = this._ClientRuleObject.ModuleTreeNodeInfo.Commands.Find
                                (o => string.Compare(o.CommandID, MB.BaseFrame.SOD.MODULE_COMMAND_ADD, true) == 0);

            if (editType == ObjectEditType.OpenEdit) {
                var cInfo = this._ClientRuleObject.ModuleTreeNodeInfo.Commands.Find
                                (o => string.Compare(o.CommandID, MB.BaseFrame.SOD.MODULE_COMMAND_EDIT, true) == 0);
                //如果不存在就去新增加的默认配置
                if (cInfo != null)
                    commandInfo = cInfo;
            }
            if (commandInfo == null) {
                MB.WinBase.MessageBoxEx.Show(string.Format("模块{0} 的编辑窗口没有配置！", this._ClientRuleObject.ModuleTreeNodeInfo.Name));
                return;
            }
            if (_BindingSource == null)
                throw new MB.Util.APPException(" 目前框架只支持绑定到数据实体的集合，对于DataSet 目前不支持显示对象窗口的编辑。", MB.Util.APPMessageType.SysFileInfo);

            //
            IExtenderEditForm baseEditForm = null;
            try {
                MB.WinBase.IFace.IForm editForm = MB.WinClientDefault.UICommand.UICreateHelper.Instance.CreateObjectEditForm(
                                                            commandInfo, ClientRuleObject as IClientRule, editType, _BindingSource);

                if (editForm == null)
                    throw new MB.Util.APPException(" 以DefaultViewForm 做为浏览窗口的编辑窗口必须 实现  MB.WinBase.IFace.IForm  ");
                Form frm = editForm as Form;

                baseEditForm = editForm as IExtenderEditForm;
                if (baseEditForm != null)
                    baseEditForm.MainBindingGridView = this.GetViewDataNavigator();

                frm.ShowDialog();
                //如果动态列，则关闭编辑页面以后自动刷新查询窗口
                //if (_ClientRuleObject.ClientLayoutAttribute.LoadType == ClientDataLoadType.ReLoad)
                //    Refresh();
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (baseEditForm != null) {
                    baseEditForm.DisposeBindingEvent(); 
                }
            }
        }
        //显示对象数据查询窗口。
        protected MB.WinBase.IFace.IQueryFilterForm _QueryFilterForm;
        private void showQueryParamterInput() {
            MB.Util.Model.ModuleCommandInfo commandInfo = this._ClientRuleObject.ModuleTreeNodeInfo.Commands.Find
                               (o => string.Compare(o.CommandID, MB.BaseFrame.SOD.MODULE_COMMAND_QUERY, true) == 0);

            Form frm = null;
            

            if (_QueryFilterForm == null) {
                if (commandInfo == null)
                    _QueryFilterForm = new MB.WinClientDefault.QueryFilter.FrmQueryFilterInput();
                else {
                    _QueryFilterForm = MB.WinClientDefault.UICommand.UICreateHelper.Instance.CreateQueryFilterForm(commandInfo);
                }
                _QueryFilterForm.ClientRuleObject = ClientRuleObject;
                _QueryFilterForm.ViewGridForm = this;
                _QueryFilterForm.IniCreateFilterElements();
                _QueryFilterForm.AfterInputQueryParameter += new QueryFilterInputEventHandle(filterForm_AfterInputQueryParameter);
            }

            #region 如果是动态聚组的查询,先要判断是否设置了动态聚组条件，如果没有，强制设置
            if (this._IsDynamicGroupIsActive) {
                if (_DynamicGroupSettings == null) {
                    if (_DynamicGroupSettingUI == null)
                        _DynamicGroupSettingUI = new FrmDynamicGroupSetting(this, _ClientRuleObject);
                    _DynamicGroupSettings = _DynamicGroupSettingUI.GetDynamicGroupSetting();
                }
                Util.Model.DynamicGroupSetting dySetting = _DynamicGroupSettings;
                if (dySetting == null || dySetting.DataAreaFields == null || dySetting.DataAreaFields.Count <= 0) {
                    MB.WinBase.MessageBoxEx.Show("没有设置聚组列和条件，请设置");
                    if (_DynamicGroupSettingUI == null)
                        _DynamicGroupSettingUI = new FrmDynamicGroupSetting(this, _ClientRuleObject, _QueryFilterForm);
                    if (_DynamicGroupSettingUI.ShowDialog() != System.Windows.Forms.DialogResult.OK) {
                        return;
                    }
                }
            }
            #endregion

            frm = _QueryFilterForm as Form;
            frm.Text = this.Text;
            frm.ShowDialog();
        }

        void filterForm_AfterInputQueryParameter(object sender, QueryFilterInputEventArgs arg) {
            if (arg.QueryParamters != null && arg.QueryParamters.Length > 0) {
                _CurrentQueryParameters = arg.QueryParamters;
                this.ClientRuleObject.CurrentQueryBehavior.PageIndex = 0;
                LoadObjectData(arg.QueryParamters) ;

                this._ClientRuleObject.CurrentFilterParams = arg.QueryParamters;
            }
        }
        #endregion 内部函数处理相关...

        /// <summary>
        /// 获取当前编辑的主浏览控件。
        /// </summary>
        /// <param name="mustEditGrid"></param>
        /// <returns></returns>
        public virtual object GetCurrentMainGridView(bool mustEditGrid) {
            throw new MB.Util.APPException("继承的窗口必须实现的方法 GetCurrentMainGridView", MB.Util.APPMessageType.SysErrInfo);

        }
        /// <summary>
        /// 当前窗口的编辑类型
        /// </summary>
        public virtual MB.WinBase.Common.ClientUIType ActiveUIType {
            get {
                throw new MB.Util.APPException("继承的窗口必须实现的方法 ActiveUIType", MB.Util.APPMessageType.SysErrInfo);
            }
        }
        protected virtual void LoadObjectData(MB.Util.Model.QueryParameterInfo[] queryParams) {
            throw new MB.Util.APPException("继承的窗口必须实现的方法 LoadObjectData", MB.Util.APPMessageType.SysErrInfo); 
        }
        /// <summary>
        /// 继承的子类必须继承的方法
        /// </summary>
        /// <returns></returns>
        protected virtual MB.WinClientDefault.Common.MainViewDataNavigator GetViewDataNavigator() {
            throw new MB.Util.APPException("继承的窗口必须实现的方法 GetViewDataNavigator", MB.Util.APPMessageType.SysErrInfo);  

        }


        #region IViewDynamicGroupGridForm Members
        /// <summary>
        /// 表示动态聚组的模式是否开启
        /// </summary>
        public bool IsDynamicGroupIsActive {
            get {
                return _IsDynamicGroupIsActive;
            }
            set {
                _IsDynamicGroupIsActive = value;
            }
        }

        /// <summary>
        /// 动态聚组用于查询的条件
        /// 在打开DynamicSettingForm后点击"确定"按钮后会赋值
        /// </summary>
        public Util.Model.DynamicGroupSetting DynamicGroupSettingForQuery {
            get {
                return _DynamicGroupSettings;
            }
            set {
                _DynamicGroupSettings = value;
            }
        }

        /// <summary>
        /// 打开动态聚组设定的Form
        /// </summary>
        public void OpenDynamicSettingForm() {
            if (_DynamicGroupSettingUI　== null)
                _DynamicGroupSettingUI = new FrmDynamicGroupSetting(this, _ClientRuleObject);
            _DynamicGroupSettingUI.ShowDialog();
        }
        #endregion


    }
}
