//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-20。
// Description	:	AbstractEditBaseForm: 所有编辑窗口必须要实现的接口。
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
using System.ComponentModel.Design;


using MB.WinBase.IFace;
using MB.WinBase.Atts;
using MB.WinBase.Common;
using MB.WinBase.Binding;
using MB.WinClientDefault.Extender;
namespace MB.WinClientDefault {
    /// <summary>
    /// AbstractEditBaseForm: 所有编辑窗口必须要实现的接口。
    /// </summary>
    public partial class AbstractEditBaseForm : AbstractBaseForm, IBaseEditForm, IExtenderEditForm {
        #region 变量定义...
        /// <summary>
        /// UI 层客户操作业务类。
        /// </summary>
        private IClientRule _ClientRuleObject;
        /// <summary>
        /// UI 层业务类的配置信息。
        /// </summary>
        private RuleClientLayoutAttribute _ClientLayoutAttribute;
        /// <summary>
        /// 当前业务对象的编辑类型， 当前编辑类型是根据用户的操作而发生变化的。
        /// </summary>
        private ObjectEditType _CurrentEditType;
        /// <summary>
        /// 当前编辑窗口数据绑定 bindingSource;
        /// </summary>
        private BindingSourceEx _BindingSource;
        /// <summary>
        /// XML 配置列和编辑控件的绑定关系
        /// </summary>
        private List<MB.WinBase.Binding.ColumnBindingInfo> _EditColumnCtlBinding;
        /// <summary>
        /// 当前窗口正在编辑的单据对象明细数据
        /// </summary>
        private MB.WinBase.UIEditEntityList _BeforeSaveDetailEntityCache;
        private MB.WinBase.UIDataInputValidated _DataValidated;

      // private Dictionary<ToolStripButton, CommandID> _ExtendUICommands;

        private List<System.ComponentModel.INotifyPropertyChanged> _HasAddMainEntityEvent;

       private ContextMenuStrip _ExtendToolStripButtonMenu;
        private MB.WinClientDefault.Common.MainViewDataNavigator _MainBindingGridView;
        private bool _OldActiveFilterEnabled;
        private UICommandType _RejectCommands = UICommandType.None;
        #endregion 变量定义...

        #region 构造函数...
        /// <summary>
        /// 构造函数...
        /// </summary>
        public AbstractEditBaseForm()
            : this(null, ObjectEditType.DesignDemo, null) {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientRuleObject"></param>
        /// <param name="editType"></param>
        /// <param name="mainViewGrid"></param>
        public AbstractEditBaseForm(IClientRule clientRuleObject, ObjectEditType editType, MB.WinBase.Binding.BindingSourceEx bindingSource) {
            InitializeComponent();

            if (!MB.Util.General.IsInDesignMode() && editType != ObjectEditType.DesignDemo) {

                _DataValidated = new MB.WinBase.UIDataInputValidated(this);

                _BindingSource = bindingSource;

                _ClientRuleObject = clientRuleObject;
                 //根据业务类上的配置项来进行约束操作菜单项。
                var rejectCfg = MB.WinBase.Atts.AttributeConfigHelper.Instance.GetModuleRejectCommands(_ClientRuleObject.GetType());
                if (rejectCfg != null) {
                    _RejectCommands = rejectCfg.RejectCommands; 
                }
                //_RejectCommands
                _CurrentEditType = editType;

                // _MainViewGridControl = mainViewGrid;

                this.Load += new EventHandler(AbstractEditBaseForm_Load);
       
                _BeforeSaveDetailEntityCache = new MB.WinBase.UIEditEntityList();
                _BeforeSaveDetailEntityCache.ListChanged += new ListChangedEventHandler(_DetailEditEntitys_ListChanged);

                bindingNavMain.ItemClicked += new ToolStripItemClickedEventHandler(bindingNavMain_ItemClicked);

                _BindingSource.ListChanged += new ListChangedEventHandler(_BindingSource_ListChanged);
                _BindingSource.PositionChanged += new EventHandler(_BindingSource_PositionChanged);
                _BindingSource.AddingNew += new AddingNewEventHandler(_BindingSource_AddingNew);
              //  bntPositionItem.KeyDown += new KeyEventHandler(bntPositionItem_KeyDown);

                //_HasLoadDetailData = new Dictionary<KeyValuePair<object, int>, object>();
             // _ExtendUICommands = new Dictionary<ToolStripButton, CommandID>();

                _HasAddMainEntityEvent = new List<INotifyPropertyChanged>();


            }
        }

        #endregion 构造函数...

        #region override base method...
        protected override void OnClosing(CancelEventArgs e) {
            if (_CurrentEditType != ObjectEditType.DesignDemo && _BindingSource != null) {
                if (_BindingSource.CheckExistsCurrentItem()) {
                    ObjectState objectState = MB.WinBase.UIDataEditHelper.Instance.GetObjectState(_BindingSource.Current);
                    bool b = checkCanAllowSave();

                    if (!b) {
                        if (objectState == ObjectState.Modified)
                            _BindingSource.CurrentItemRejectChanges();
                        else if (objectState == ObjectState.New) {
                            if (_BindingSource.Count == 1)
                                MB.WinBase.Binding.BindingSourceHelper.Instance.ReleaseDataBinding(_EditColumnCtlBinding);

                            _BindingSource.CancelEdit();

                        }
                    }
                    else {
                        e.Cancel = true;
                    }
                }

            }


            base.OnClosing(e);

             
        }
        public void DisposeBindingEvent() {
            try {
                _BindingSource.ListChanged -= new ListChangedEventHandler(_BindingSource_ListChanged);
                _BindingSource.PositionChanged -= new EventHandler(_BindingSource_PositionChanged);
                _BindingSource.AddingNew -= new AddingNewEventHandler(_BindingSource_AddingNew);

                foreach (INotifyPropertyChanged item in _HasAddMainEntityEvent)
                    item.PropertyChanged -= new PropertyChangedEventHandler(OnCurrentMainEditEntity_PropertyChanged);
            }
            catch { }

            _MainBindingGridView.ActiveFilterEnabled = _OldActiveFilterEnabled;

            MB.WinBase.Binding.BindingSourceHelper.Instance.ReleaseDataBinding(_EditColumnCtlBinding); 
        }
        #endregion override base method...

        #region 对象事件处理...
        void bindingNavMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                tsbHideTextBox.Focus();
                try {
                    if (e.ClickedItem.Equals(bntMoveFirstItem)) {
                        moveFocusPosition(GridDataRowMoveType.First);

                        //OnBindingSourcePositionChanging(0);
                    }
                    else if (e.ClickedItem.Equals(bntMovePreviousItem)) {
                        moveFocusPosition(GridDataRowMoveType.Prev);
                       // OnBindingSourcePositionChanging(_BindingSource.Position);
                    }
                    else if (e.ClickedItem.Equals(bntMoveNextItem)) {
                        moveFocusPosition(GridDataRowMoveType.Next);
                       // OnBindingSourcePositionChanging(_BindingSource.Position + 1);
                    }
                    else if (e.ClickedItem.Equals(bntMoveLastItem)) {
                        moveFocusPosition(GridDataRowMoveType.Last);
                        //OnBindingSourcePositionChanging(_BindingSource.Count - 1);
                    }
                    else if (e.ClickedItem.Equals(bntAddNewItem)) {
                        AddNew();
                    }
                    else if (e.ClickedItem.Equals(bntCancelItem)) {
                        Cancel();
                    }
                    else if (e.ClickedItem.Equals(bntSaveItem)) {
                        Save();
                    }
                    else if (e.ClickedItem.Equals(bntDeleteItem)) {
                        Delete();
                    }
                    else if (e.ClickedItem.Equals(bntSubmitItem)) {
                        if (_BindingSource.Current == null) return;
                        MB.Util.Model.EntityState entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(_BindingSource.Current);
                        if (entityState == MB.Util.Model.EntityState.Modified) {
                            throw MB.Util.APPExceptionHandlerHelper.CreateDisplayToUser("单据数据已经发生改变,请先保存");
                        }
                        Submit();
                    }
                    else if (e.ClickedItem.Equals(bntCancelSubmitItem)) {
                        CancelSubmit();
                    }
                    else if (e.ClickedItem.Equals(butExtendItem)) {
                        if (_ExtendToolStripButtonMenu != null) {
                            _ExtendToolStripButtonMenu.Show(this, new Point(butExtendItem.Bounds.X, butExtendItem.Height));
                        }
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

        void _BindingSource_AddingNew(object sender, AddingNewEventArgs e) {
            e.NewObject = _ClientRuleObject.CreateNewEntity((int)_ClientRuleObject.MainDataTypeInDoc);  //_ClientRuleObject.CreateNewEntity((int)_ClientRuleObject.MainDataTypeInDoc);

        }
        //绑定数据源的Focuse Row 发生改变时产生
        void _BindingSource_PositionChanged(object sender, EventArgs e) {
            if (MB.Util.General.IsInDesignMode()) return;

            if (!_BindingSource.CheckExistsCurrentItem()) return;


            _DataValidated.ClearErrorMessage(_EditColumnCtlBinding);


            OnBindingSourcePositionChanged();

            System.ComponentModel.INotifyPropertyChanged currentMainEditEntity = _BindingSource.Current as System.ComponentModel.INotifyPropertyChanged;
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
            if (e.KeyCode != Keys.Enter || !string.IsNullOrEmpty(bntPositionItem.Text)) return;

            int index = MB.Util.MyConvert.Instance.ToInt(bntPositionItem.Text);
            if (index > 0 && index < _BindingSource.Count) {
                //_BindingSource.Position = index;
                _MainBindingGridView.MoveBy(index); 
            }

        }
        void AbstractEditBaseForm_Load(object sender, EventArgs e) {
            if (MB.Util.General.IsInDesignMode() || _CurrentEditType == ObjectEditType.DesignDemo) return;

            bindingNavMain.BindingSource = _BindingSource;
            DataBindingOptions options = DataBindingOptions.None;
            switch (_CurrentEditType) {
                case ObjectEditType.AddNew:
                    AddNew();
                    options = DataBindingOptions.Edit;
                    break;
                case ObjectEditType.OpenEdit:
                    options = DataBindingOptions.Edit;
                    break;
                case ObjectEditType.OpenReadOnly:
                    options = DataBindingOptions.ReadOnly;
                    break;
                default:
                    throw new MB.Util.APPException(string.Format("对象类型{0} 在基类中还没有进行，请在子类中覆盖基类的方法进行相应的处理！", _CurrentEditType.ToString()), MB.Util.APPMessageType.SysErrInfo);
            }

            _EditColumnCtlBinding = MB.WinBase.Binding.BindingSourceHelper.Instance.CreateDataBinding(_ClientRuleObject, _BindingSource, _DataBindingProvider, options);

            OnToolsButtonValidate();
            OnBindingSourcePositionChanged();

            refreshFormatButtonItem();

            _BindingSource_PositionChanged(this, null);
        }
        //根据模块信息刷新工具栏菜单项。
        private void refreshFormatButtonItem() {
            if (_ClientRuleObject.ModuleTreeNodeInfo == null || string.IsNullOrEmpty(_ClientRuleObject.ModuleTreeNodeInfo.RejectCommands))
                return;

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
        //根据修改的信息设置实体对象的编辑状态
        private void setCurrentEditItemState() {
            object entity = _BindingSource.Current;
            MB.Util.Model.EntityState entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(entity);
            if (entityState == MB.Util.Model.EntityState.Persistent) {
                _BindingSource.RaiseListChangedEvents = false;
                MB.WinBase.UIDataEditHelper.Instance.SetEntityState(entity, MB.Util.Model.EntityState.Modified);
                _BindingSource.RaiseListChangedEvents = true;
            }
        }
        //判断是否需要保存
        private bool checkCanAllowSave() {
            bool isInEditting = MB.WinBase.UIDataEditHelper.Instance.CheckCurrentEntityIsInEditting(_BindingSource.Current);
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
        /// 设置扩展菜单的弹出窗口。
        /// 一定要在继承的子类窗口 Form_Load 事件中调用。
        /// </summary>
        /// <param name="cMenu"></param>
        protected virtual void SetExtendToolStripButtonMenu(ContextMenuStrip cMenu) {
            butExtendItem.DropDownItems.Clear();
            //通过这种变态的方法来完成菜单集合的所属移动。
            List<ToolStripItem> list = new List<ToolStripItem>();
            foreach (ToolStripItem item in cMenu.Items)
                list.Add(item);

            foreach (ToolStripItem item in list) {
                butExtendItem.DropDownItems.Add(item);
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
            if (!_BindingSource.CheckExistsCurrentItem()) return;

            object entity = _BindingSource.Current;
            if (entity is DataRow)
                throw new MB.Util.APPException("目前框架的实现还不支持DataRow 的绑定编辑处理。");

            bntMoveFirstItem.Enabled = !_MainBindingGridView.IsFirstRow;// _BindingSource.Position > 0;
            bntMovePreviousItem.Enabled = !_MainBindingGridView.IsFirstRow;// _BindingSource.Position > 0;
            bntMoveLastItem.Enabled = !_MainBindingGridView.IsLastRow;// _BindingSource.Position < _BindingSource.Count - 1;
            bntMoveNextItem.Enabled = !_MainBindingGridView.IsLastRow;// _BindingSource.Position < _BindingSource.Count - 1;
           // bntPositionItem.Text = _MainBindingGridView.FocusedRowHandle.ToString();// (_BindingSource.Position + 1).ToString();
            bntPositionItem.Text = string.Format("{0}/{1}", _MainBindingGridView.FocusedRowHandle + 1,_MainBindingGridView.RowCount); 

            MB.WinBase.Common.ObjectState objectState = MB.WinBase.UIDataEditHelper.Instance.GetObjectState(entity);
            bntAddNewItem.Enabled = notRejectCommand(UICommandType.AddNew) && true && this.CurrentEditType != ObjectEditType.OpenReadOnly;
            bntCancelItem.Enabled =  ((objectState == ObjectState.New && _BindingSource.Count > 1) || objectState == ObjectState.Modified);
            bntDeleteItem.Enabled = notRejectCommand(UICommandType.Delete) && (objectState == ObjectState.Modified || objectState == ObjectState.Unchanged) && _BindingSource.Count > 1 && this.CurrentEditType != ObjectEditType.OpenReadOnly;
            bntSaveItem.Enabled = notRejectCommand(UICommandType.Save) && (objectState == ObjectState.New || objectState == ObjectState.Modified);
            bntSubmitItem.Enabled = notRejectCommand(UICommandType.Submit) && (objectState == ObjectState.Unchanged);
            bntCancelSubmitItem.Enabled = notRejectCommand(UICommandType.CancelSubmit) && objectState == ObjectState.Validated;

            //根据实体对象的状态变化设置相应的信息.
            MB.Util.Model.EntityState entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(entity);
           
            //判断是否存在单据状态属性
            bool exists = MB.WinBase.UIDataEditHelper.Instance.CheckExistsDocState(entity);
            if (exists) {
                MB.Util.Model.DocState docState = MB.WinBase.UIDataEditHelper.Instance.GetEntityDocState(entity);
                MB.WinBase.Binding.BindingSourceHelper.Instance.SetCtlReadOnly(_EditColumnCtlBinding, docState != MB.Util.Model.DocState.Progress);
              
                OnDocStateChanged(_BindingSource.Current, docState);
            }
            else {
                bntSubmitItem.Enabled = false;
                bntCancelSubmitItem.Enabled = false;
            }
            MB.WinBase.Binding.BindingSourceHelper.Instance.SetCtlByAllowEditStates(_EditColumnCtlBinding, entityState);

            //根据模块的需要重新初始化操作菜单项。
            refreshFormatButtonItem();
        }
        //判断是否已经Reject.
        private bool notRejectCommand(UICommandType cmdType) {
            return (_RejectCommands & cmdType) == 0;
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
                if (!_DataValidated.DataValidated(this.ClientRuleObject as IClientRule, _EditColumnCtlBinding, _BindingSource.Current)) {
                    MB.WinBase.MessageBoxEx.Show("单据主体数据检验不成功,请检查！");
                    return 0;
                }

                _BindingSource.EndEditNoRaiseEvent();
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
                    //刷新当前编辑的对象。
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
            if(refreshMainEntity)
                _ClientRuleObject.RefreshEntity((int)_ClientRuleObject.MainDataTypeInDoc, _BindingSource.Current);


            _BeforeSaveDetailEntityCache.RaiseListChangedEvents = false;
            _BeforeSaveDetailEntityCache.AcceptChanges();
            _BeforeSaveDetailEntityCache.Clear();
            _BeforeSaveDetailEntityCache.RaiseListChangedEvents = true;

            _BindingSource.RaiseListChangedEvents = false;
            MB.WinBase.UIDataEditHelper.Instance.SetEntityState(_BindingSource.Current, MB.Util.Model.EntityState.Persistent);
            _BindingSource.EndEditNoRaiseEvent();
            _BindingSource.RaiseListChangedEvents = true;

            OnToolsButtonValidate();
        }
        /// <summary>
        /// 对象提交处理。
        /// </summary>
        /// <param name="isSubmit"></param>
        /// <returns></returns>
        protected virtual int ObjectSubmit(bool isSubmit) {
            //string[] updateProperty = new string[] { MB.WinBase.UIDataEditHelper.ENTITY_DOC_STATE };
            //object wiseObject = MB.Util.MyReflection.Instance.PropertyMemberwiseClone(_BindingSource.Current);
            //if(isSubmit)
            //    MB.Util.MyReflection.Instance.InvokePropertyForSet(wiseObject, MB.WinBase.UIDataEditHelper.ENTITY_DOC_STATE, MB.Util.Model.DocState.Submit);
            //else
            //    MB.Util.MyReflection.Instance.InvokePropertyForSet(wiseObject, MB.WinBase.UIDataEditHelper.ENTITY_DOC_STATE, MB.Util.Model.DocState.Progress);

            //MB.WinBase.UIDataEditHelper.Instance.SetEntityState(wiseObject, MB.Util.Model.EntityState.Modified);
            //int re = ObjectDataSave(wiseObject, updateProperty);
            //if (re > 0) {
            //    MB.WinBase.Binding.BindingSourceHelper.Instance.SetCtlReadOnly(_EditColumnCtlBinding, isSubmit);
            //    _ClientRuleObject.RaiseAfterDocStateChanged(_BindingSource.Current);

            //}
            object mainEntity = _BindingSource.Current;
            int re = _ClientRuleObject.Submit(mainEntity, !isSubmit);
            if (re >= 0) {

                //if (isSubmit)
                //    MB.Util.MyReflection.Instance.InvokePropertyForSet(mainEntity, MB.WinBase.UIDataEditHelper.ENTITY_DOC_STATE, MB.Util.Model.DocState.Submit);
                //else
                //    MB.Util.MyReflection.Instance.InvokePropertyForSet(mainEntity, MB.WinBase.UIDataEditHelper.ENTITY_DOC_STATE, MB.Util.Model.DocState.Progress);

                //刷新当前编辑的对象。

                _ClientRuleObject.RefreshEntity((int)_ClientRuleObject.MainDataTypeInDoc, _BindingSource.Current);

                _BindingSource.RaiseListChangedEvents = false;
                MB.WinBase.UIDataEditHelper.Instance.SetEntityState(mainEntity, MB.Util.Model.EntityState.Persistent);
                MB.WinBase.Binding.BindingSourceHelper.Instance.SetCtlReadOnly(_EditColumnCtlBinding, isSubmit);
                _BindingSource.EndEditNoRaiseEvent();
                _BindingSource.RaiseListChangedEvents = true;

                MB.Util.Model.DocState docState = MB.WinBase.UIDataEditHelper.Instance.GetEntityDocState(mainEntity);
                 OnDocStateChanged(mainEntity, docState);
                //_ClientRuleObject.RaiseAfterDocStateChanged(mainEntity);
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
               // var dre = MB.WinBase.MessageBoxEx.Question("");
                _MainBindingGridView.ActiveFilterEnabled = false;   
            }

            //chendc 2009-02-20 特别说明 ：  需要修改为通过实体对象的编辑状态来确认当前实体对象是否已经发生变化。
            if (_BindingSource.Current != null) {
                if (checkCanAllowSave()) return 0;
                ObjectState objectState = MB.WinBase.UIDataEditHelper.Instance.GetObjectState(_BindingSource.Current);
                if (objectState == ObjectState.Modified)
                    _BindingSource.CurrentItemRejectChanges();
                else
                    _BindingSource.CancelEdit();
            }

            _BindingSource.AddNew();

            // _BindingSource.Position = _BindingSource.Count - 1;

            // OnToolsButtonValidate();
            return 0;
        }
        /// <summary>
        /// 撤消新增。
        ///  如果存在明细表，继承的子类需要覆盖该方法，进行相应的其它处理后再调用 base.Cancel()
        /// </summary>
        public virtual int Cancel() {
            ObjectState objectState = MB.WinBase.UIDataEditHelper.Instance.GetObjectState(_BindingSource.Current);
            DialogResult re = MB.WinBase.MessageBoxEx.Question("本次撤消操作不可逆,是否继续？");
            if (re != DialogResult.Yes) return 0;

            if (objectState == ObjectState.Modified) {

                _BindingSource.CurrentItemRejectChanges();
            }
            else {
                _BindingSource.CancelEdit();
            }
            if (_BindingSource.Count > 0)
                _BindingSource.ResetBindings(false);

            if (!_BindingSource.CheckExistsCurrentItem())
                this.Close();

            return 1;
        }
        /// <summary>
        /// 把实体对象保存在中间层缓存中。
        /// 如果存在明细表，继承的子类需要覆盖该方法，进行相应的其它处理后再调用 base.Save()
        /// </summary>
        /// <returns></returns>
        public virtual int Save() {
            return ObjectDataSave(_BindingSource.Current, null);
        }

        /// <summary>
        /// 从中间层缓存中删除实体对象。
        /// </summary>
        /// <returns></returns>
        public virtual int Delete() {
            bool deleteFirstPositionData = false;
            if (!_BindingSource.CheckExistsCurrentItem()) return 0;

            if (!_MainBindingGridView.CheckIsLeafDataRow()) {
                DialogResult dre = MB.WinBase.MessageBoxEx.Question("存在下级节点,删除当前节点将把下级节点一起删除,是否继续");
                if (dre != DialogResult.Yes) return 0;
               // throw new MB.Util.APPException("存在下级节点,请先删除下级节点", MB.Util.APPMessageType.DisplayToUser);
            }

            DialogResult ddre = MB.WinBase.MessageBoxEx.Question("数据删除操作不可逆,是否继续？");

            if (ddre != DialogResult.Yes) return 0;

            deleteFirstPositionData = _BindingSource.Position == 0;
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
                if (_BindingSource.Count == 1) {
                    this.Close();
                    //_BindingSource.RemoveCurrent();
                    _MainBindingGridView.RemoveFocusedRow(_BindingSource);
                    return 1;
                }

                _BeforeSaveDetailEntityCache.RaiseListChangedEvents = false;
                _BeforeSaveDetailEntityCache.Clear();
                _BeforeSaveDetailEntityCache.RaiseListChangedEvents = true;

                //_BindingSource.RemoveCurrent();
                _MainBindingGridView.RemoveFocusedRow(_BindingSource);

                _BindingSource.EndEdit();
                if (deleteFirstPositionData) {
                    OnBindingSourcePositionChanged();
                    OnToolsButtonValidate();
                }


            }


            return 0;
        }

        /// <summary>
        /// 
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
        /// 数据永久性保存。
        /// </summary>
        /// <returns></returns>
        public int Flush() {
            //try {
            //    _ClientRuleObject.Flush();
            //}
            //catch (Exception ex) {
            //    if (MB.WinBase.ShareLib.Instance.ExceptionMessageCommand(ex) != 0) {
            //        MB.WinBase.MessageBoxEx.Show("数据保存出错,请重试！");
            //    }
            //}
            return 0;
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
                return _BindingSource.Current;
            }

        }
        /// <summary>
        /// 数据绑定。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MB.WinBase.Binding.BindingSourceEx BindingSource {
            get {
                return _BindingSource;
            }
            set {
                _BindingSource = value;
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

            ObjectState objectState = MB.WinBase.UIDataEditHelper.Instance.GetObjectState(_BindingSource.Current);
            if (checkCanAllowSave()) {
                return;
            }

            if (objectState == ObjectState.Modified)
                _BindingSource.CurrentItemRejectChanges();
            else
                _BindingSource.CancelEdit();
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
            //_BindingSource.Position = newIndex;
        }
        #endregion 内部函数处理...

        private void tsButQuit_Click(object sender, EventArgs e) {
            this.Close();
        }

    }

    /// <summary>
    /// 网格数据焦点移动的类型
    /// </summary>
    enum GridDataRowMoveType {
        First,
        Next,
        Prev,
        Last
    }
}
