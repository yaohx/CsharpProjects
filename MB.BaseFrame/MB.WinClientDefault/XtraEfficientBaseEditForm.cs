//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-04-20。
// Description	:	XtraEfficientBaseEditForm: 所有编辑窗口必须要实现的接口,
//                 主要处理不使用Grid缓存、区分列表和编辑的处理模式(主要应用在单据主信息列比较多得情况)。
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
using MB.BaseFrame;
using MB.WinBase;
using MB.WinBase.Binding;
using MB.WinBase.Common;
using MB.WinBase.IFace;
using MB.WinClientDefault.Extender;

namespace MB.WinClientDefault
{
    public partial class XtraEfficientBaseEditForm : AbstractBaseForm, IBaseEditForm, IExtenderEditForm
    {
        #region 变量定义...
        /// <summary>
        /// UI 层客户操作业务类。
        /// </summary>
        private IClientRule _ClientRuleObject;
        /// <summary>
        /// 当前业务对象的编辑类型， 当前编辑类型是根据用户的操作而发生变化的。
        /// </summary>
        private ObjectEditType _CurrentEditType;
        /// <summary>
        /// 当前编辑窗口数据绑定 bindingSource;
        /// </summary>
        private BindingSourceEx _MainGridBindingSource;
        private BindingSourceEx _EditBindingSource;
        /// <summary>
        /// XML 配置列和编辑控件的绑定关系
        /// </summary>
        private List<MB.WinBase.Binding.ColumnBindingInfo> _EditColumnCtlBinding;
        /// <summary>
        /// 当前窗口正在编辑的单据对象明细数据
        /// </summary>
        private MB.WinBase.UIEditEntityList _BeforeSaveDetailEntityCache;
        private MB.WinBase.UIDataInputValidated _DataValidated;
        private List<System.ComponentModel.INotifyPropertyChanged> _HasAddMainEntityEvent;

        private ContextMenuStrip _ExtendToolStripButtonMenu;
        private MB.WinClientDefault.Common.MainViewDataNavigator _MainBindingGridView;
        private bool _OldActiveFilterEnabled;
        private BusinessOperateTrace _BusinessOperateTrace;
        private bool _CurrentIsCancelAddNew;
        private Dictionary<string, DevExpress.XtraBars.BarButtonItem> _GeneralCommands;
        private bool _IsInClosing;
        #endregion 变量定义...

        #region 构造函数...
        /// <summary>
        /// 构造函数...
        /// </summary>
        public XtraEfficientBaseEditForm()
            : this(null, ObjectEditType.DesignDemo, null) {

        }
        /// <summary>
        /// 创建基于Xtra工具栏的默认标准编辑窗口.
        /// </summary>
        /// <param name="clientRuleObject"></param>
        /// <param name="editType"></param>
        /// <param name="mainViewGrid"></param>
        public XtraEfficientBaseEditForm(IClientRule clientRuleObject, ObjectEditType editType, MB.WinBase.Binding.BindingSourceEx bindingSource) {
            InitializeComponent();

            if (!MB.Util.General.IsInDesignMode() && editType != ObjectEditType.DesignDemo) {

                _DataValidated = new MB.WinBase.UIDataInputValidated(this);

                _MainGridBindingSource = bindingSource;
                _EditBindingSource = new BindingSourceEx();
                _ClientRuleObject = clientRuleObject;

                _BusinessOperateTrace = new BusinessOperateTrace(_ClientRuleObject);
                _BusinessOperateTrace.CommandItemClick += new EventHandler<MB.WinClientDefault.Common.EditDocStateTraceEventArgs>(_BusinessOperateTrace_CommandItemClick);

                _ExtendToolStripButtonMenu = _BusinessOperateTrace.CommandMenus;
 
                _CurrentEditType = editType;

                this.Load += new EventHandler(AbstractEditBaseForm_Load);
                
       
                _BeforeSaveDetailEntityCache = new MB.WinBase.UIEditEntityList();
                _BeforeSaveDetailEntityCache.ListChanged += new ListChangedEventHandler(_DetailEditEntitys_ListChanged);

                _MainGridBindingSource.ListChanged += new ListChangedEventHandler(_BindingSource_ListChanged);
                _MainGridBindingSource.PositionChanged += new EventHandler(_BindingSource_PositionChanged);

                _HasAddMainEntityEvent = new List<INotifyPropertyChanged>();
            }
            _GeneralCommands = new Dictionary<string, DevExpress.XtraBars.BarButtonItem>();
            _GeneralCommands.Add(MB.BaseFrame.SOD.MODULE_COMMAND_ADD, bntAddNewItem);
            _GeneralCommands.Add(MB.BaseFrame.SOD.MODULE_COMMAND_DELETE, bntDeleteItem);
            _GeneralCommands.Add(MB.BaseFrame.SOD.MODULE_COMMAND_SAVE, bntSaveItem);
            _GeneralCommands.Add(MB.BaseFrame.SOD.MODULE_COMMAND_SUBMIT, bntSubmitItem);
            _GeneralCommands.Add(MB.BaseFrame.SOD.MODULE_COMMAND_CANCEL_SUBMIT, bntCancelSubmitItem);
        }
        #endregion 构造函数...

        #region override base method...
        protected override void OnClosing(CancelEventArgs e) {
            try
            {
                _IsInClosing = true;
                if (_CurrentEditType != ObjectEditType.DesignDemo && _EditBindingSource != null)
                {
                    if (_EditBindingSource.CheckExistsCurrentItem())
                    {
                        ObjectState objectState = MB.WinBase.UIDataEditHelper.Instance.GetObjectState(_EditBindingSource.Current);
                        bool b = checkCanAllowSave();

                        if (!b)
                        {
                            if (objectState == ObjectState.Modified)
                                _EditBindingSource.CurrentItemRejectChanges();
                            else if (objectState == ObjectState.New)
                            {
                                if (_EditBindingSource.Count == 1)
                                    MB.WinBase.Binding.BindingSourceHelper.Instance.ReleaseDataBinding(_EditColumnCtlBinding);

                                _CurrentIsCancelAddNew = true;
                                _EditBindingSource.CancelEdit();
                                _CurrentIsCancelAddNew = false;
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                }
                base.OnClosing(e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _IsInClosing = false;
            }
        }
        public void DisposeBindingEvent() {
            try {
                _MainGridBindingSource.ListChanged -= new ListChangedEventHandler(_BindingSource_ListChanged);
                _MainGridBindingSource.PositionChanged -= new EventHandler(_BindingSource_PositionChanged);
               // _BindingSource.AddingNew -= new AddingNewEventHandler(_BindingSource_AddingNew);

                foreach (INotifyPropertyChanged item in _HasAddMainEntityEvent)
                    item.PropertyChanged -= new PropertyChangedEventHandler(OnCurrentMainEditEntity_PropertyChanged);
            }
            catch { }

            _MainBindingGridView.ActiveFilterEnabled = _OldActiveFilterEnabled;

            MB.WinBase.Binding.BindingSourceHelper.Instance.ReleaseDataBinding(_EditColumnCtlBinding); 
        }
        #endregion override base method...

        #region 对象事件处理...
        private void bntItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                try {
                    if (e.Item.Equals(bntMoveFirstItem)) {
                        moveFocusPosition(GridDataRowMoveType.First);
                        reloadEditBindingSource();
                    }
                    else if (e.Item.Equals(bntMovePreviousItem)) {
                        moveFocusPosition(GridDataRowMoveType.Prev);
                        reloadEditBindingSource();
                    }
                    else if (e.Item.Equals(bntMoveNextItem)) {
                        moveFocusPosition(GridDataRowMoveType.Next);
                        reloadEditBindingSource();
                    }
                    else if (e.Item.Equals(bntMoveLastItem)) {
                        moveFocusPosition(GridDataRowMoveType.Last);
                        reloadEditBindingSource();
                    }
                    else if (e.Item.Equals(bntAddNewItem)) {
                        AddNew();
                    }
                    else if (e.Item.Equals(bntSaveItem)) {
                        Save();
                    }
                    else if (e.Item.Equals(bntDeleteItem)) {
                        Delete();
                        reloadEditBindingSource();
                    }
                    else if (e.Item.Equals(bntSubmitItem)) {
                        if (_EditBindingSource.Current == null) return;
                        MB.Util.Model.EntityState entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(_EditBindingSource.Current);
                        if (entityState == MB.Util.Model.EntityState.Modified) {
                            throw MB.Util.APPExceptionHandlerHelper.CreateDisplayToUser("单据数据已经发生改变,请先保存");
                        }
                        Submit();
                    }
                    else if (e.Item.Equals(bntCancelSubmitItem)) {
                        CancelSubmit();
                    }
                    else if (e.Item.Equals(butExtendItem)) {
                        if (_ExtendToolStripButtonMenu != null) {
                            Point p = default(Point);
                            
                            foreach (DevExpress.XtraBars.BarItemLink item in barTools.ItemLinks) {
                                if (item.Item.Equals(butExtendItem)) {
                                    p.X = item.Bounds.X; 
                                }
                            }
                            _ExtendToolStripButtonMenu.Show(this, new Point(p.X, butExtendItem.Height + 6));//6为偏移量
                            return;
                        }
                    }
                    else if (e.Item.Equals(tsButQuit)) {
                        this.Close();
                        return;
                    }
                    else {

                    }
                    OnToolsButtonValidate();
                }
                catch (Exception ex) {
                    MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                }
            }
        }
        /// <summary>
        /// 扩展功能菜单中通用业务操作时相应的事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       protected virtual void _BusinessOperateTrace_CommandItemClick(object sender, MB.WinClientDefault.Common.EditDocStateTraceEventArgs e) {
            using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                int re = 0;
                try {
                    re = _ClientRuleObject.BusinessFlowSubmit(_EditBindingSource.Current, e.DocOperateType, e.Remark);
                }
                catch (Exception ex) {
                    MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                }
                if (re > 0) {
                    try {
                        ////刷新当前编辑的对象。
                        AcceptDataChanged(true);
                        MB.WinBase.MessageBoxEx.Show("操作成功");
                    }
                    catch (Exception x) {
                        MB.WinBase.MessageBoxEx.Show("对象数据库保存已经成功,但本地化更新时出错,请关闭窗口后重新刷新！");
                        MB.Util.TraceEx.Write("对象数据库保存已经成功,但本地化更新时出错." + x.Message);
                    }
                }
            }
        }
        void _BindingSource_AddingNew(object sender, AddingNewEventArgs e) {
            
        }
        //绑定数据源的Focuse Row 发生改变时产生
        void _BindingSource_PositionChanged(object sender, EventArgs e) {
            if (MB.Util.General.IsInDesignMode()) return;
            if (_CurrentIsCancelAddNew) return;
            if (!_EditBindingSource.CheckExistsCurrentItem()) return;
            _DataValidated.ClearErrorMessage(_EditColumnCtlBinding);
            OnBindingSourcePositionChanged();
            System.ComponentModel.INotifyPropertyChanged currentMainEditEntity = _EditBindingSource.Current as System.ComponentModel.INotifyPropertyChanged;
            if (currentMainEditEntity != null) {
                foreach (INotifyPropertyChanged item in _HasAddMainEntityEvent) {
                    item.PropertyChanged -= new PropertyChangedEventHandler(OnCurrentMainEditEntity_PropertyChanged);
                }
                _HasAddMainEntityEvent.Clear();
                _HasAddMainEntityEvent.Add(currentMainEditEntity);
                currentMainEditEntity.PropertyChanged += new PropertyChangedEventHandler(OnCurrentMainEditEntity_PropertyChanged);
            }
        }
        //主表对象发生改变时发生。
       protected virtual void _BindingSource_ListChanged(object sender, ListChangedEventArgs e) {
           if (MB.Util.General.IsInDesignMode()) return;

            if (e.NewIndex < 0) return;
            if (e.ListChangedType == ListChangedType.ItemChanged) {
                setCurrentEditItemState();
            }
            if(e.ListChangedType!= ListChangedType.ItemAdded &&  e.ListChangedType != ListChangedType.Reset)
                OnToolsButtonValidate();
        }
        //明细表数据发生改变时产生。
        void _DetailEditEntitys_ListChanged(object sender, ListChangedEventArgs e) {
            if (e.ListChangedType != ListChangedType.Reset) {

                setCurrentEditItemState();

                OnToolsButtonValidate();
            }
        }
        void bntPositionItem_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Enter || !string.IsNullOrEmpty(bntPositionItem.Caption)) return;

            int index = MB.Util.MyConvert.Instance.ToInt(bntPositionItem.Caption);
            if (index > 0 && index < _MainGridBindingSource.Count) {
                _MainBindingGridView.MoveBy(index); 
            }

        }
        void AbstractEditBaseForm_Load(object sender, EventArgs e) {
            if (MB.Util.General.IsInDesignMode() || _CurrentEditType == ObjectEditType.DesignDemo) return;
            DataBindingOptions options = DataBindingOptions.None;
            switch (_CurrentEditType) {
                case ObjectEditType.AddNew:
                    AddNew();
                    options = DataBindingOptions.Edit;
                    break;
                case ObjectEditType.OpenEdit:
                    var id = UIDataEditHelper.Instance.GetEntityID(_MainGridBindingSource.Current);
                    reloadEditBindingSource();
                    options = DataBindingOptions.Edit;
                    break;
                case ObjectEditType.OpenReadOnly:
                    reloadEditBindingSource();
                    options = DataBindingOptions.ReadOnly;
                    break;
                default:
                    throw new MB.Util.APPException(string.Format("对象类型{0} 在基类中还没有进行，请在子类中覆盖基类的方法进行相应的处理！", _CurrentEditType.ToString()), MB.Util.APPMessageType.SysErrInfo);
            }

            _EditColumnCtlBinding = MB.WinBase.Binding.BindingSourceHelper.Instance.CreateDataBinding(_ClientRuleObject, _EditBindingSource, _DataBindingProvider, options);

            OnToolsButtonValidate();
            _BindingSource_PositionChanged(this, null);
        }

        /// <summary>
        /// 由于数据源分离，重新加载编辑的数据源
        /// </summary>
        private void reloadEditBindingSource()
        {
            //获取主键值
            string[] keys = _ClientRuleObject.ClientLayoutAttribute.EntityKeys;

            if (_ClientRuleObject.ClientLayoutAttribute.CommunicationDataType == CommunicationDataType.ModelEntity && !(_MainGridBindingSource.DataSource is DataSet))
            {
                if (keys.Contains(SOD.OBJECT_PROPERTY_ID))
                {
                    int id = UIDataEditHelper.Instance.GetEntityID(_MainGridBindingSource.Current);

                    _EditBindingSource.DataSource = _ClientRuleObject.GetEditObjectByID(id);
                }
                else
                {
                    List<object> keyValues = new List<object>();
                    foreach (var key in keys)
                    {
                        object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(_MainGridBindingSource.Current, key);
                        keyValues.Add(val);
                    }

                    _EditBindingSource.DataSource = _ClientRuleObject.GetEditObjectByKey(keyValues.ToArray());
                }
            }
            else if (_ClientRuleObject.ClientLayoutAttribute.CommunicationDataType == CommunicationDataType.DataSet || (_MainGridBindingSource.DataSource is DataSet))
            {
                DataRowView row = _MainGridBindingSource.Current as DataRowView;
                if (keys == null || keys.Length <= 0 || keys.Contains(SOD.OBJECT_PROPERTY_ID))
                {
                    int id = Convert.ToInt32(row[SOD.OBJECT_PROPERTY_ID]);
                    _EditBindingSource.DataSource = _ClientRuleObject.GetEditObjectByID(id);
                }
                else
                {
                    List<object> keyValues = new List<object>();
                    foreach (var key in keys)
                    {
                        object val = row[key];
                        keyValues.Add(val);
                    }
                    _EditBindingSource.DataSource = _ClientRuleObject.GetEditObjectByKey(keyValues.ToArray());
                }
            }

        }

        //根据修改的信息设置实体对象的编辑状态
        private void setCurrentEditItemState() {
            object entity = _EditBindingSource.Current;
            MB.Util.Model.EntityState entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(entity);
            if (entityState == MB.Util.Model.EntityState.Persistent) {
                //如果当前单据包含单据状态 那么如果单据已经确认了也不能进行修改
                bool b = MB.WinBase.UIDataEditHelper.Instance.CheckExistsDocState(entity);
                if (b) {
                    var docState = MB.WinBase.UIDataEditHelper.Instance.GetEntityDocState(entity);
                    if (docState != Util.Model.DocState.Progress) return;

                }
                _EditBindingSource.RaiseListChangedEvents = false;
                MB.WinBase.UIDataEditHelper.Instance.SetEntityState(entity, MB.Util.Model.EntityState.Modified);
                _EditBindingSource.RaiseListChangedEvents = true;
            }
        }
        //判断是否需要保存
        private bool checkCanAllowSave() {
            bool isInEditting = MB.WinBase.UIDataEditHelper.Instance.CheckCurrentEntityIsInEditting(_EditBindingSource.Current);
            if (isInEditting) {
                DialogResult re = MB.WinBase.MessageBoxEx.Question("当前编辑的单据已经发生改变是否放弃更改？");
                if (re != DialogResult.Yes) {
                    return true;
                }
            }
            return false;
        }
        //在新增加之前需要处理的事项
        private void beforeAddNew() {
            //1,提示并清除当前浏览主窗口控件的本地查询条件
        }
        #endregion 对象事件处理...

        #region 继承的子类可以覆盖处理的方法...
        /// <summary>
        /// 根据模块信息刷新工具栏菜单项。
        /// </summary>
        protected virtual void OnAfterRefreshButtonItem() {
            if (_ClientRuleObject.ModuleTreeNodeInfo == null || _IsInClosing)
                return;

            if (!string.IsNullOrEmpty(_ClientRuleObject.ModuleTreeNodeInfo.RejectCommands)) {
                string[] rejects = _ClientRuleObject.ModuleTreeNodeInfo.RejectCommands.Split(',');
                if (Array.IndexOf(rejects, MB.BaseFrame.SOD.MODULE_COMMAND_ADD) >= 0)
                    bntAddNewItem.Enabled = false;

                if (Array.IndexOf(rejects, MB.BaseFrame.SOD.MODULE_COMMAND_DELETE) >= 0)
                    bntDeleteItem.Enabled = false;
                if (Array.IndexOf(rejects, MB.BaseFrame.SOD.MODULE_COMMAND_SAVE) >= 0)
                    bntSaveItem.Enabled = false;
                if (Array.IndexOf(rejects, MB.BaseFrame.SOD.MODULE_COMMAND_SUBMIT) >= 0)
                    bntSubmitItem.Enabled = false;
                if (Array.IndexOf(rejects, MB.BaseFrame.SOD.MODULE_COMMAND_CANCEL_SUBMIT) >= 0)
                    bntCancelSubmitItem.Enabled = false;
            }
            //增加调用主窗口的刷新函数以方便权限调用设置
            if (MB.WinClientDefault.XtraRibbonMdiMainForm.Active_Mdi_Form != null) {
                    MB.WinClientDefault.XtraRibbonMdiMainForm.Active_Mdi_Form.ValidatedEditForm(this); 
            }
        }
        /// <summary>
        /// 获取当前窗口对象创建的所有绑定控件.
        /// </summary>
        public List<MB.WinBase.Binding.ColumnBindingInfo> EditColumnCtlBinding {
            get {
                return _EditColumnCtlBinding;
            }
        }
        /// <summary>
        /// 获取当前窗口的单据常规操作菜单项(增加\删除\保存\重做\提交)
        /// </summary>
        public  Dictionary<string, DevExpress.XtraBars.BarButtonItem> GeneralCommands {
            get {
                return _GeneralCommands;
            }
        }
        /// <summary>
        /// 单据业务状态改变操作对象。
        /// </summary>
        public BusinessOperateTrace BusinessOperateTrace {
            get {
                return _BusinessOperateTrace;
            }
        }
        /// <summary>
        /// 设置扩展菜单的弹出窗口。
        /// 一定要在继承的子类窗口 Form_Load 事件中调用。
        /// </summary>
        /// <param name="cMenu"></param>
        public virtual void SetExtendToolStripButtonMenu(ContextMenuStrip cMenu) {
            //////通过这种变态的方法来完成菜单集合的所属移动。
            List<ToolStripItem> list = new List<ToolStripItem>();
            foreach (ToolStripItem item in cMenu.Items) {
                list.Add(item);
            }
            foreach (ToolStripItem item in list) {
                if (!_ExtendToolStripButtonMenu.Items.Contains(item))
                    _ExtendToolStripButtonMenu.Items.Add(item);
            }
        }
        /// <summary>
        /// 在主编辑对象属性值发生改变时产生。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnCurrentMainEditEntity_PropertyChanged(object sender, PropertyChangedEventArgs e) {
              
        }
        /// <summary>
        ///  在绑定的数据源的焦点行 发生改变后产生。
        /// </summary>
        protected virtual void OnBindingSourcePositionChanged() {
        }
        /// <summary>
        /// 当前编辑的单据明细数据集合。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MB.WinBase.UIEditEntityList BeforeSaveDetailEntityCache {
            get {
                if (MB.Util.General.IsInDesignMode())
                    return null;
                else
                    return _BeforeSaveDetailEntityCache;
            }
        }
        /// <summary>
        /// 根据当前实体的状态控制工具拦的状态。
        /// </summary>
        protected virtual void OnToolsButtonValidate() {
            if (!_MainGridBindingSource.CheckExistsCurrentItem()) return;

            object entity = _EditBindingSource.Current;
            if (entity is DataRow)
                throw new MB.Util.APPException("目前框架的实现还不支持DataRow 的绑定编辑处理。");

            bntMoveFirstItem.Enabled = _MainBindingGridView!=null && !_MainBindingGridView.IsFirstRow;
            bntMovePreviousItem.Enabled = _MainBindingGridView != null && !_MainBindingGridView.IsFirstRow;
            bntMoveLastItem.Enabled = _MainBindingGridView != null && !_MainBindingGridView.IsLastRow;
            bntMoveNextItem.Enabled = _MainBindingGridView != null && !_MainBindingGridView.IsLastRow;
            if(_MainBindingGridView!=null)
                bntPositionItem.Caption = string.Format("{0}/{1}", _MainBindingGridView.FocusedRowHandle + 1,_MainBindingGridView.RowCount); 

            MB.WinBase.Common.ObjectState objectState = MB.WinBase.UIDataEditHelper.Instance.GetObjectState(entity);
            bntAddNewItem.Enabled = true && this.CurrentEditType != ObjectEditType.OpenReadOnly;
            //判断是否为扩展的操作类型
            bool isExtendDocState = objectState == ObjectState.OverDocState;
            bntDeleteItem.Enabled = !isExtendDocState && (objectState == ObjectState.Modified || objectState == ObjectState.Unchanged) &&
                                                          _MainGridBindingSource.Count > 1 && this.CurrentEditType != ObjectEditType.OpenReadOnly;
            bntSaveItem.Enabled = !isExtendDocState && (objectState == ObjectState.New || objectState == ObjectState.Modified);
            bntSubmitItem.Enabled = !isExtendDocState && (objectState == ObjectState.Unchanged);
            bntCancelSubmitItem.Enabled = !isExtendDocState && objectState == ObjectState.Validated;

            _BusinessOperateTrace.ResetDocEntity(entity); 

            //根据实体对象的状态变化设置相应的信息. 
            MB.Util.Model.EntityState entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(entity);
           
            //判断是否存在单据状态属性
            bool exists = MB.WinBase.UIDataEditHelper.Instance.CheckExistsDocState(entity);
            if (exists) {
                MB.Util.Model.DocState docState = MB.WinBase.UIDataEditHelper.Instance.GetEntityDocState(entity);
                if (bntDeleteItem.Enabled) {
                    //只有在录入中的数据才可以进行删除
                    bntDeleteItem.Enabled = docState == MB.Util.Model.DocState.Progress;
                }
                MB.WinBase.Binding.BindingSourceHelper.Instance.SetCtlReadOnly(_EditColumnCtlBinding, docState != MB.Util.Model.DocState.Progress);

                OnDocStateChanged(_EditBindingSource.Current, docState);
            }
            else {
                bntSubmitItem.Enabled = false;
                bntCancelSubmitItem.Enabled = false;
            }
            MB.WinBase.Binding.BindingSourceHelper.Instance.SetCtlByAllowEditStates(_EditColumnCtlBinding, entityState);
            //设置当前编辑Form 为只读属性
            if (_CurrentEditType == ObjectEditType.OpenReadOnly) {
                MB.WinBase.Binding.BindingSourceHelper.Instance.SetCtlReadOnly(_EditColumnCtlBinding, true);
            }
            //根据模块的需要重新初始化操作菜单项。
            OnAfterRefreshButtonItem();
        }
        /// <summary>
        ///  在单据状态发生时产生。
        /// </summary>
        /// <param name="mainEntity"></param>
        /// <param name="docState"></param>
        protected virtual void OnDocStateChanged(object mainEntity, MB.Util.Model.DocState newDocState) {
            _ClientRuleObject.RaiseAfterDocStateChanged(mainEntity);
        }
        /// <summary>
        ///  对象数据保存。
        /// </summary>
        /// <param name="editEntity"></param>
        /// <param name="propertys"></param>
        /// <returns></returns>
        protected virtual int ObjectDataSave(object editEntity, string[] propertys) {
            int re = 0;
            System.ServiceModel.ICommunicationObject commObject = _ClientRuleObject.CreateServerCommunicationObject();
            try {
                if (!_DataValidated.DataValidated(this.ClientRuleObject as IClientRule, _EditColumnCtlBinding, _EditBindingSource.Current)) {
                    throw new MB.Util.APPException("单据主体数据检验不成功,请检查", Util.APPMessageType.DisplayToUser);
                }
                _EditBindingSource.EndEditNoRaiseEvent();
                _BeforeSaveDetailEntityCache.RaiseBeforeDataSave();

                //判断并追加登录用户的相关信息( 实体数据的登录用户操作信息一般只在主表中存在 )
                if (propertys == null || propertys.Length == 0) {
                    MB.WinBase.UIDataEditHelper.Instance.AppendLoginUserInfo(editEntity);
                }
                //增加主表实体对象
                _ClientRuleObject.AddToCache(commObject, (int)_ClientRuleObject.MainDataTypeInDoc, editEntity, false, propertys);
                //增加明细数据
                foreach (KeyValuePair<int, object> detailInfo in _BeforeSaveDetailEntityCache) {
                    bool isDelete = false;
                    if (MB.WinBase.UIDataEditHelper.Instance.CheckExistsEntityState(detailInfo.Value)) {
                        if (MB.WinBase.UIDataEditHelper.Instance.GetEntityState(detailInfo.Value) == MB.Util.Model.EntityState.Deleted)
                            isDelete = true;
                    }
                    _ClientRuleObject.AddToCache(commObject, detailInfo.Key, detailInfo.Value, isDelete, null);
                }
                re = _ClientRuleObject.Flush(commObject);
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
            finally {
                try {
                    commObject.Close();
                }
                catch { }
            }
            if (re > 0) {
                try {
                    ////刷新当前编辑的对象。
                    AcceptDataChanged(true);
                }
                catch (Exception x) {
                    MB.WinBase.MessageBoxEx.Show("对象数据库保存已经成功,但本地化更新时出错,请关闭窗口后重新刷新！");
                    MB.Util.TraceEx.Write("对象数据库保存已经成功,但本地化更新时出错." + x.Message);
                    return 0;
                }
            }
            else {
                MB.WinBase.MessageBoxEx.Show("对象数据库保存不成功,请重试");
            }
            return re;
        }
        /// <summary>
        /// 接受数据改变
        /// </summary>
        /// <param name="refreshMainEntity">判断是否刷新主体数据</param>
        protected void AcceptDataChanged(bool refreshMainEntity) {
            if (refreshMainEntity)
                _ClientRuleObject.RefreshEntity((int)_ClientRuleObject.MainDataTypeInDoc, _EditBindingSource.Current);

            _BeforeSaveDetailEntityCache.RaiseListChangedEvents = false;
            _BeforeSaveDetailEntityCache.AcceptChanges();
            _BeforeSaveDetailEntityCache.Clear();
            _BeforeSaveDetailEntityCache.RaiseListChangedEvents = true;

            _EditBindingSource.RaiseListChangedEvents = false;
            MB.WinBase.UIDataEditHelper.Instance.SetEntityState(_EditBindingSource.Current, MB.Util.Model.EntityState.Persistent);
            _EditBindingSource.EndEditNoRaiseEvent();
            _EditBindingSource.RaiseListChangedEvents = true;

            OnToolsButtonValidate();
        }
        /// <summary>
        /// 对象提交处理。
        /// </summary>
        /// <param name="isSubmit"></param>
        /// <returns></returns>
        protected virtual int ObjectSubmit(bool isSubmit) {
            object mainEntity = _EditBindingSource.Current;
            int re = _ClientRuleObject.Submit(mainEntity, !isSubmit);
            if (re >= 0) {
                //刷新当前编辑的对象。
                _ClientRuleObject.RefreshEntity((int)_ClientRuleObject.MainDataTypeInDoc, _EditBindingSource.Current);
                _EditBindingSource.RaiseListChangedEvents = false;
                MB.WinBase.UIDataEditHelper.Instance.SetEntityState(mainEntity, MB.Util.Model.EntityState.Persistent);
                MB.WinBase.Binding.BindingSourceHelper.Instance.SetCtlReadOnly(_EditColumnCtlBinding, isSubmit);
                _EditBindingSource.EndEditNoRaiseEvent();
                _EditBindingSource.RaiseListChangedEvents = true;

                MB.Util.Model.DocState docState = MB.WinBase.UIDataEditHelper.Instance.GetEntityDocState(mainEntity);
                 OnDocStateChanged(mainEntity, docState);
            }
            return re;
        }
        #endregion 继承的子类可以覆盖处理的方法...
        /// <summary>
        /// 浏览窗口编辑主控件。
        /// </summary>
        public MB.WinClientDefault.Common.MainViewDataNavigator MainBindingGridView {
            get {
                return _MainBindingGridView;
            }
            set {
                _MainBindingGridView = value;
                _OldActiveFilterEnabled = _MainBindingGridView.ActiveFilterEnabled;
            }
        }
        #region IBaseEditForm 成员
        /// <summary>
        /// 创建一个新的实体对象。
        /// 如果需要覆盖，别忘了添加base.AddNew();
        /// </summary>
        /// <returns></returns>
        public virtual int AddNew() {
            if (!string.IsNullOrEmpty(_MainBindingGridView.ActiveFilterString)) {
                _MainBindingGridView.ActiveFilterEnabled = false;   
            }
            //chendc 2009-02-20 特别说明 ：  需要修改为通过实体对象的编辑状态来确认当前实体对象是否已经发生变化。
            if (_EditBindingSource.Current != null) {
                if (checkCanAllowSave()) return 0;
                ObjectState objectState = MB.WinBase.UIDataEditHelper.Instance.GetObjectState(_EditBindingSource.Current);
                if (objectState == ObjectState.Modified)
                    _EditBindingSource.CurrentItemRejectChanges();
                else
                    _EditBindingSource.CancelEdit();
            }
            _EditBindingSource.DataSource = _ClientRuleObject.CreateNewEntity((int)_ClientRuleObject.MainDataTypeInDoc); 

            return 0;
        }
        /// <summary>
        /// 撤消新增。
        ///  如果存在明细表，继承的子类需要覆盖该方法，进行相应的其它处理后再调用 base.Cancel()
        /// </summary>
        public virtual int Cancel() {
            ObjectState objectState = MB.WinBase.UIDataEditHelper.Instance.GetObjectState(_EditBindingSource.Current);
            DialogResult re = MB.WinBase.MessageBoxEx.Question("本次撤消操作不可逆,是否继续？");
            if (re != DialogResult.Yes) return 0;

            if (objectState == ObjectState.Modified) {

                _EditBindingSource.CurrentItemRejectChanges();
            }
            else {
                _EditBindingSource.CancelEdit();
            }
            if (_EditBindingSource.Count > 0)
                _EditBindingSource.ResetBindings(false);

            if (!_EditBindingSource.CheckExistsCurrentItem())
                this.Close();

            return 1;
        }
        /// <summary>
        /// 把实体对象保存在中间层缓存中。
        /// 如果存在明细表，继承的子类需要覆盖该方法，进行相应的其它处理后再调用 base.Save()
        /// </summary>
        /// <returns></returns>
        public virtual int Save() {
            return ObjectDataSave(_EditBindingSource.Current, null);
        }
        /// <summary>
        /// 从中间层缓存中删除实体对象。
        /// </summary>
        /// <returns></returns>
        public virtual int Delete() {
            bool deleteFirstPositionData = false;
            if (!_EditBindingSource.CheckExistsCurrentItem()) return 0;

            if (!_MainBindingGridView.CheckIsLeafDataRow()) {
                DialogResult dre = MB.WinBase.MessageBoxEx.Question("存在下级节点,删除当前节点将把下级节点一起删除,是否继续");
                if (dre != DialogResult.Yes) return 0;
            }
            DialogResult ddre = MB.WinBase.MessageBoxEx.Question("数据删除操作不可逆,是否继续？");
            if (ddre != DialogResult.Yes) return 0;
            deleteFirstPositionData = _MainGridBindingSource.Position == 0;
            int re = 0;
            System.ServiceModel.ICommunicationObject commObject = _ClientRuleObject.CreateServerCommunicationObject();
            try {
                _ClientRuleObject.AddToCache(commObject, (int)_ClientRuleObject.MainDataTypeInDoc, _EditBindingSource.Current, true, null);

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
                if (_MainGridBindingSource.Count == 1) {
                    this.Close();
                    _MainBindingGridView.RemoveFocusedRow(_MainGridBindingSource);
                    return 1;
                }
                _BeforeSaveDetailEntityCache.RaiseListChangedEvents = false;
                _BeforeSaveDetailEntityCache.Clear();
                _BeforeSaveDetailEntityCache.RaiseListChangedEvents = true;
                _MainBindingGridView.RemoveFocusedRow(_MainGridBindingSource);

                _MainGridBindingSource.EndEdit();
                if (deleteFirstPositionData) {
                    OnBindingSourcePositionChanged();
                    OnToolsButtonValidate();
                }
            }
            return 0;
        }
        /// <summary>
        /// 数据提交。
        /// </summary>
        /// <returns></returns>
        public virtual int Submit() {
            return ObjectSubmit(true);
        }
        /// <summary>
        /// 取消提交。
        /// </summary>
        /// <returns></returns>
        public virtual int CancelSubmit() {
            return ObjectSubmit(false);
        }
        /// <summary>
        /// 数据永久性保存。(保留方法)
        /// </summary>
        /// <returns></returns>
        public virtual int Flush() {
            throw new MB.Util.APPException("为保留方法,目前还没有实现", MB.Util.APPMessageType.DisplayToUser);  
        }
        /// <summary>
        /// 当前操作类型。
        /// </summary>
        [Browsable(false)]
        public ObjectEditType CurrentEditType {
            get {
                return _CurrentEditType;
            }
            set {
                _CurrentEditType = value;
            }
        }
        /// <summary>
        /// 当前编辑的主实体对象。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object CurrentEditEntity {
            get {
                return _EditBindingSource.Current;
            }

        }
        /// <summary>
        /// 数据绑定。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MB.WinBase.Binding.BindingSourceEx BindingSource {
            get {
                return _EditBindingSource;
            }
            set {
                _EditBindingSource = value;
            }
        }
        private IDataBindingProvider _DataBindingProvider;
        /// <summary>
        /// 数据源绑定
        /// </summary>
        [EditorAttribute(typeof(MB.WinBase.Binding.DataBindingProviderDesign), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("数据源绑定")]
        public IDataBindingProvider DataBindingProvider {
            get {
                MB.WinBase.Binding.DataBindingProviderDesign.SetParentContainer(this.Container);
                return _DataBindingProvider;
            }
            set {
                _DataBindingProvider = value;
            }
        }
        #endregion

        #region IForm 成员
        /// <summary>
        /// 客户端业务类。
        /// </summary>
        [Browsable(false)]
        public virtual IClientRuleQueryBase ClientRuleObject {
            get {
                return _ClientRuleObject;
            }
            set {
                _ClientRuleObject = value as IClientRule;
            }
        }
        /// <summary>
        /// 实体对象编辑窗口。
        /// </summary>
        [Browsable(false)]
        public virtual ClientUIType ActiveUIType {
            get { return ClientUIType.ObjectEditForm; }
        }
        #endregion

        #region 内部函数处理...
        /// <summary>
        /// 在绑定的数据源的焦点行 发生改变前时产生。
        /// </summary>
        private  void moveFocusPosition(GridDataRowMoveType moveType) {

            ObjectState objectState = MB.WinBase.UIDataEditHelper.Instance.GetObjectState(_EditBindingSource.Current);
            if (checkCanAllowSave()) {
                return;
            }
            if (objectState == ObjectState.Modified)
                _EditBindingSource.CurrentItemRejectChanges();
            else
                _EditBindingSource.CancelEdit();
            switch (moveType) {
                case GridDataRowMoveType.First:
                    _MainBindingGridView.MoveFirst(); 
                    break;
                case GridDataRowMoveType.Next:
                    _MainBindingGridView.MoveNext();
                    break;
                case GridDataRowMoveType.Prev:
                    _MainBindingGridView.MovePrev();
                    break;
                case GridDataRowMoveType.Last:
                    _MainBindingGridView.MoveLast();
                    break;
                default :
                    break;
            }
        }
        #endregion 内部函数处理...
    }
}
