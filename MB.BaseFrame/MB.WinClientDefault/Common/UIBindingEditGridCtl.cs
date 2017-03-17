//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-17
// Description	:	编辑界面数据和编辑网格数据绑定。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;

using MB.BaseFrame;
using MB.WinClientDefault.DataImport;
using MB.WinBase.Common;
using MB.XWinLib.XtraGrid;
using DevExpress.XtraGrid.Columns;
using MB.WinClientDefault.OfficeAutomation;
using System.Collections;
using MB.Util.Emit;
using System.Reflection;
using System.Runtime.Serialization;
namespace MB.WinClientDefault.Common
{
    /// <summary>
    ///  编辑界面数据和编辑网格数据绑定。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UIBindingEditGridCtl<T> : IDisposable where T : class
    {
        private static readonly string COL_ENTITY_STATE_PROPERTY = "EntityState";
        private static readonly string GRID_VIEW_LAYOUT_NAME = "DefaultGridView"; //明细设定默认的GIRDVIEWLAYOUT时用的是Default，现在增加DefaultGridView，并且兼容Default以保持

        #region 变量定义...
        private MB.WinBase.IFace.IBaseEditForm _EditForm;
        private MB.XWinLib.GridDataBindingParam _DetailBindingParams;
        private Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> _ColPropertys;
        private Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> _EditCols;
        //判断当前对象是否从业务类中创建
        private bool _CreateNewEntityFromRule;
        private string _ForeignPropertyName;
        private object _MainEntity;
        private object _MainKeyValue;
        private Dictionary<object, List<T>> _BindingDetailEntitys;
        private MB.XWinLib.XtraGrid.GridControlEx _XtraGrid;
        private int _DataInDocType;
        private BindingList<T> _BindingList;
        private MB.WinBase.IFace.IClientRule _ClientRule;
        private MB.WinBase.UIEditEntityList _BeforeSaveDetailEntityCache;
        private string _CurrentXmlFileName;
        private bool _IsReadonly;
        private bool _AllowDataDelete = true;
        private bool _AllowNew = true;
        //private bool _RaiseListChangedEvents;
        private ProcessScopeControlInfo _IsInBatchAdd;
        private bool _HasInitialGird = false; //表示网格已经被初始化过，不需要重复初始化
        private Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> _DAccs; //缓存当前编辑对象类型的访问，提高速度
        private bool _IsEntityBatchAddedFromServer = false;//批量新增时, 是不是批量从服务端创建对象
        private bool _IsListChangedRaisedInBatchAddedSuppressed = false; //是否抑制批量新增时的ListChanged事件

        

        #endregion 变量定义...

       
        #region 自定义事件处理相关...
        private System.EventHandler<GridBeforeDataImportEventArgs> _BeforeDataImport;
        /// <summary>
        /// 从XML 文件中获取数据并点击数据整理完成之前产生。
        /// </summary>
        public event System.EventHandler<GridBeforeDataImportEventArgs> BeforeDataImport {
            add {
                _BeforeDataImport += value;
            }
            remove {
                _BeforeDataImport -= value;
            }
        }
        private void onBeforeDataImport(GridBeforeDataImportEventArgs arg) {
            if (_BeforeDataImport != null)
                _BeforeDataImport(this, arg);
        }
        private System.EventHandler<BindingEditGridDataImportEventArgs> _DbDataExistsValidated;
        /// <summary>
        /// 数据导入之前相应。
        /// </summary>
        public event System.EventHandler<BindingEditGridDataImportEventArgs> DbDataExistsValidated {
            add {
                _DbDataExistsValidated += value;
            }
            remove {
                _DbDataExistsValidated -= value;
            }
        }
        private void onDbDataExistsValidated(BindingEditGridDataImportEventArgs arg) {
            if (_DbDataExistsValidated != null)
                _DbDataExistsValidated(this, arg);
        }

        private System.EventHandler<AfterDataItemImportEventArgs<T>> _AfterDataItemImport;
        public event System.EventHandler<AfterDataItemImportEventArgs<T>> AfterDataItemImport {
            add {
                _AfterDataItemImport += value;
            }
            remove {
                _AfterDataItemImport -= value;
            }
        }
        private void onAfterDataItemImport(AfterDataItemImportEventArgs<T> arg) {
            if (_AfterDataItemImport != null)
                _AfterDataItemImport(this, arg);
        }

        private System.EventHandler<AfterAllDataItemImportEventArgs<T>> _AfterAllDataItemImport;
        public event System.EventHandler<AfterAllDataItemImportEventArgs<T>> AfterAllDataItemImport {
            add {
                _AfterAllDataItemImport += value;
            }
            remove {
                _AfterAllDataItemImport -= value;
            }
        }
        private void onAfterAllDataItemImport(AfterAllDataItemImportEventArgs<T> arg) {
            if (_AfterAllDataItemImport != null)
                _AfterAllDataItemImport(this, arg);
        }


        #region 当批量新增以后锁触发的事件
        private System.EventHandler<AfterAllDataBatchAddEventArgs<T>> _AfterAllDataBatchAdd;
        /// <summary>
        /// 当批量新增结束以后触发，时间的参数中包含了批量新增的数据
        /// </summary>
        public event System.EventHandler<AfterAllDataBatchAddEventArgs<T>> AfterAllDataBatchAdd {
            add {
                _AfterAllDataBatchAdd += value;
            }
            remove {
                _AfterAllDataBatchAdd -= value;
            }
        }
        private void onAfterAllDataBatchAdd(AfterAllDataBatchAddEventArgs<T> arg) {
            if (_AfterAllDataBatchAdd != null)
                _AfterAllDataBatchAdd(this, arg);
        }
        #endregion


        private System.EventHandler<BindlingEditGridDataEventArgs<T>> _ListChanged;
        public event System.EventHandler<BindlingEditGridDataEventArgs<T>> ListChanged {
            add {
                _ListChanged += value;
            }
            remove {
                _ListChanged -= value;
            }
        }
        protected void onListChanged(BindlingEditGridDataEventArgs<T> arg) {
            if (_ListChanged != null)
                _ListChanged(this, arg);
        }
        private System.EventHandler<BindlingEditGridDataEventArgs<T>> _AfterAddingNew;
        public event System.EventHandler<BindlingEditGridDataEventArgs<T>> AfterAddingNew {
            add {
                _AfterAddingNew += value;
            }
            remove {
                _AfterAddingNew -= value;
            }
        }
        private void onAfterAddingNew(BindlingEditGridDataEventArgs<T> arg) {
            if (_AfterAddingNew != null)
                _AfterAddingNew(this, arg);
        }

        private Func<DataSet, DataSet> _BusinessCheck4DataImport;
        /// <summary>
        /// 当导入明细数据时做的业务检查
        /// </summary>
        public Func<DataSet, DataSet> BusinessCheck4DataImport {
            get { return _BusinessCheck4DataImport; }
            set { _BusinessCheck4DataImport = value; }
        }
        
        #endregion 自定义事件处理相关...


        #region construct function...

        /// <summary>
        /// 实例化明细绑定组件
        /// </summary>
        /// <param name="baseEditForm">当前使用明细绑定组件的编辑窗口</param>
        /// <param name="createEntityFromServer">实体对象是不是从服务端创建</param>
        /// <param name="foreignPropertyName">明细对象中用于做外键的属性名称</param>
        public UIBindingEditGridCtl(MB.WinBase.IFace.IBaseEditForm baseEditForm, bool createEntityFromServer, string foreignPropertyName) {
            _EditForm = baseEditForm;
            _CreateNewEntityFromRule = createEntityFromServer;
            _ForeignPropertyName = foreignPropertyName;

            _AllowDataDelete = true;

            _ClientRule = _EditForm.ClientRuleObject as MB.WinBase.IFace.IClientRule;

            _BindingList = new BindingList<T>();
            _IsInBatchAdd = new ProcessScopeControlInfo();

            _BindingDetailEntitys = new Dictionary<object, List<T>>();

            _BindingList.AddingNew += new AddingNewEventHandler(_BindingList_AddingNew);
            _BindingList.ListChanged += new ListChangedEventHandler(_BindingList_ListChanged);

            _BeforeSaveDetailEntityCache = baseEditForm.BeforeSaveDetailEntityCache;
            _BeforeSaveDetailEntityCache.BeforeDataSave += new EventHandler(_EditEntitys_BeforeDataSave);
            _ClientRule.AfterDocStateChanged += new MB.WinBase.IFace.UIClientRuleDataEventHandle(_ClientRule_AfterDocStateChanged);
            _DAccs = createDynamicProperyAccess(typeof(T));
            
        }

        #endregion construct function...

        #region public 成员...
        
        ///// <summary>
        ///// 数据绑定集合。
        ///// </summary>
        //public BindingList<T> CurrentBindingList {
        //    get {
        //        return _BindingList;
        //    }
        //    set {
        //        _BindingList = value;
        //    }
        //}
        /// <summary>
        /// 指定一个值 判断是否相应ListChanged 事件.
        /// </summary>
        public bool RaiseListChangedEvents {
            get {
                return _BindingList.RaiseListChangedEvents;
            }
            set {
                _BindingList.RaiseListChangedEvents = value;
            }
        }
        /// <summary>
        /// 当前加载的键值。
        /// </summary>
        public object CurrentMainKey {
            get {
                return _MainKeyValue;
            }
        }
        /// <summary>
        /// 已经绑定的明细数据。
        /// </summary>
        public Dictionary<object, List<T>> BindingDetailEntitys {
            get {
                return _BindingDetailEntitys;
            }
        }
        /// <summary>
        /// 是否允许删除。
        /// </summary>
        public bool AllowDataDelete {
            get {
                return _AllowDataDelete;
            }
            set {
                _AllowDataDelete = value;
                if (value) {
                    if (_XtraGrid != null)
                        _XtraGrid.ValidedDeleteKeyDown = true;
                }
            }
        }
        /// <summary>
        /// 判断明细数据是否允许新增加。
        /// </summary>
        public bool AllowNew {
            get {
                return _AllowNew;
            }
            set {
                _AllowNew = value;
            }
        }

        /// <summary>
        /// 批量新增时, 是不是批量从服务端创建对象
        /// </summary>
        public bool IsEntityBatchAddedFromServer {
            get { return _IsEntityBatchAddedFromServer; }
            set { _IsEntityBatchAddedFromServer = value; }
        }

        /// <summary>
        /// 是否抑制批量新增时的ListChanged事件
        /// </summary>
        public bool IsListChangedRaisedInBatchAddedSuppressed {
            get { return _IsListChangedRaisedInBatchAddedSuppressed; }
            set { _IsListChangedRaisedInBatchAddedSuppressed = value; }
        }

        /// <summary>
        /// 删除指定的数据
        /// </summary>
        /// <param name="currentEntity"></param>
        public void DeleteListItem(T currentEntity) {

            //绑定访问的集合类
            if (_MainKeyValue != null) {
                if (_BindingDetailEntitys[_MainKeyValue].Contains(currentEntity))
                    _BindingDetailEntitys[_MainKeyValue].Remove(currentEntity);
                if (_BindingList.Contains(currentEntity)) {
                    _BindingList.Remove(currentEntity);
                }
            }

            onListChanged(new BindlingEditGridDataEventArgs<T>(currentEntity, ListChangedType.ItemDeleted));

            MB.Util.Model.EntityState entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(currentEntity);

            MB.WinBase.UIDataEditHelper.Instance.SetEntityState(currentEntity, MB.Util.Model.EntityState.Deleted);
            if (_BeforeSaveDetailEntityCache != null) {
                deleteExists(currentEntity);
                if (entityState != MB.Util.Model.EntityState.New)
                    _BeforeSaveDetailEntityCache.AddAndDeleteEquals(new KeyValuePair<int, object>(_DataInDocType, currentEntity));
            }

        }
        /// <summary>
        /// 增加一个新的实体对象。
        /// 默认情况下从服务端创建新对象。
        /// </summary>
        /// <returns></returns>
        public T AddNewItem() {
            return AddNewItem(true);
        }
        /// <summary>
        /// 增加一个新的实体对象。
        /// create date : 2010-01-14 
        /// </summary>
        /// <param name="createFromServer">判断是否从服务端创建,如果是从本地创建需要给实体的ID 赋值</param>
        /// <returns></returns>
        public T AddNewItem(bool createFromServer) {
            bool old = _CreateNewEntityFromRule;
            _CreateNewEntityFromRule = createFromServer;
            T newEntity = default(T);
            try {
                if (createFromServer) {
                    newEntity = (T)_ClientRule.CreateNewEntity(_DataInDocType);
                    _BindingList.Add(newEntity);
                }
                else {
                    newEntity = _BindingList.AddNew();
                }

                if (!string.IsNullOrEmpty(_ForeignPropertyName)) {
                    if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(newEntity, _ForeignPropertyName))
                        throw new MB.Util.APPException(string.Format("在创建明细对象的外键值时出错,请检查外键 {0} 配置 是否正确?", _ForeignPropertyName));

                    MB.Util.MyReflection.Instance.InvokePropertyForSet(newEntity, _ForeignPropertyName, _MainKeyValue);
                }

                //设置对象状态
                MB.WinBase.UIDataEditHelper.Instance.SetEntityState(newEntity, MB.Util.Model.EntityState.New);

                _BeforeSaveDetailEntityCache.AddAndDeleteEquals(new KeyValuePair<int, object>(_DataInDocType, newEntity));

                onAfterAddingNew(new BindlingEditGridDataEventArgs<T>(newEntity, ListChangedType.ItemAdded));
            }
            catch (Exception ex) {
                throw new MB.Util.APPException("创建实体对象有误", MB.Util.APPMessageType.DisplayToUser, ex);
            }
            finally {
                _CreateNewEntityFromRule = old;
            }

            return newEntity;
        }

        /// <summary>
        /// 清除正在编辑的所有明细数据。
        /// </summary>
        public void ClearDetailData() {
            DevExpress.XtraGrid.Views.Grid.GridView gridView = _XtraGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
            int count = gridView.RowCount;
            List<T> forDelItems = new List<T>();
            for (int handle = 0; handle < count; handle++) {
                T entity = (T)gridView.GetRow(handle);
                if (entity != null)
                    forDelItems.Add(entity);
            }
            foreach (T item in forDelItems) {
                DeleteListItem(item);
            }
            _BindingList.Clear();
        }
        /// <summary>
        ///  刷新网格数据。
        /// </summary>
        /// <param name="mainEntity"></param>
        /// <param name="mainKeyValue"></param>
        /// <param name="detailEntitys"></param>
        /// <param name="editGrdCtl"></param>
        /// <param name="uiXmlFileName"></param>
        /// <param name="dataInDocType"></param>
        public void RefreshDataSource(object mainEntity, object mainKeyValue, T[] detailEntitys,
                                DevExpress.XtraGrid.GridControl editGrdCtl,
                                string uiXmlFileName, int dataInDocType) {

            RefreshDataSource(mainEntity, mainKeyValue, detailEntitys, editGrdCtl, uiXmlFileName, null, null, dataInDocType);
        }
        /// <summary>
        /// 刷新网格数据。
        /// </summary>
        /// <param name="mainEntity"></param>
        /// <param name="mainKeyValue"></param>
        /// <param name="detailEntitys"></param>
        /// <param name="editGrdCtl"></param>
        /// <param name="uiXmlFileName"></param>
        /// <param name="dataInDocType"></param>
        public void RefreshDataSource(object mainEntity, object mainKeyValue, T[] detailEntitys,
                                      DevExpress.XtraGrid.GridControl editGrdCtl,
                                      string uiXmlFileName, Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols, int dataInDocType) {

            RefreshDataSource(mainEntity, mainKeyValue, detailEntitys, editGrdCtl, uiXmlFileName, null, editCols, dataInDocType);
        }
        /// <summary>
        /// 刷新网格数据。
        /// </summary>
        /// <param name="mainEntity"></param>
        /// <param name="mainKeyValue"></param>
        /// <param name="detailEntitys"></param>
        /// <param name="editGrdCtl"></param>
        /// <param name="uiXmlFileName"></param>
        /// <param name="dataInDocType"></param>
        public void RefreshDataSource(object mainEntity, object mainKeyValue, T[] detailEntitys,
                                      DevExpress.XtraGrid.GridControl editGrdCtl,
                                      string uiXmlFileName, Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys,
                                      Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editCols, int dataInDocType) {

            if (_BindingList.Count > 0) {
                resetBindingList(detailEntitys);
            }
            else {
                convertArrayToBindingList(_BindingList, detailEntitys);
            }

            _CurrentXmlFileName = uiXmlFileName;
            _MainEntity = mainEntity;
            _MainKeyValue = mainKeyValue;
            _DataInDocType = dataInDocType;

            if (mainEntity == null || mainKeyValue == null) {
                _BindingList.RaiseListChangedEvents = false;
                _BindingList.Clear();
                _BindingList.RaiseListChangedEvents = true;
                _BindingList.ResetBindings();
                editGrdCtl.RefreshDataSource();
                return;
            }

            T[] entitys = detailEntitys;
            //这里存储是为了方便主体对象的访问
            if (!_BindingDetailEntitys.ContainsKey(mainKeyValue))
                _BindingDetailEntitys.Add(mainKeyValue, new List<T>(detailEntitys));
            else {
                List<T> lstDetail = _BindingDetailEntitys[mainKeyValue];
                lstDetail.Clear();
                lstDetail.AddRange(detailEntitys);
            }

            //else
            //    _BindingDetailEntitys[mainKeyValue] = _BindingList;

            if (_DetailBindingParams == null || editGrdCtl.DataSource == null) {
                //_DetailBindingParams.CreateNewEntityFromServer = _CreateEntityFromServer;
                // _DetailBindingParams.AfterCreateNewEntity += new MB.XWinLib.GridDataBindingParamEventHandle<T>(_DetailBindingParams_AfterCreateNewEntity<T>);
                if (colPropertys == null)
                    _ColPropertys = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnPropertys(uiXmlFileName);
                else
                    _ColPropertys = colPropertys;

                if (editCols == null)
                    _EditCols = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnEdits(_ColPropertys, uiXmlFileName);
                else
                    _EditCols = editCols;

                MB.WinBase.Common.GridViewLayoutInfo gridViewLayoutInfo = MB.WinBase.LayoutXmlConfigHelper.Instance.GetGridColumnLayoutInfo(uiXmlFileName, string.Empty);
                //明细GRID设定布局格式 GRID_VIEW_LAYOUT_NAME

                if (gridViewLayoutInfo == null) {
                    gridViewLayoutInfo = MB.WinBase.LayoutXmlConfigHelper.Instance.GetGridColumnLayoutInfo(uiXmlFileName, GRID_VIEW_LAYOUT_NAME);
                }

                _DetailBindingParams = new MB.XWinLib.GridDataBindingParam(editGrdCtl, _BindingList, false);
                MB.XWinLib.XtraGrid.XtraGridEditHelper.Instance.CreateEditXtraGrid(_DetailBindingParams, _ColPropertys, _EditCols, gridViewLayoutInfo);

                _XtraGrid = editGrdCtl as MB.XWinLib.XtraGrid.GridControlEx;
                if (_XtraGrid != null) {
                    _XtraGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(xtraGrid_KeyDown);
                    _XtraGrid.BeforeContextMenuClick += new MB.XWinLib.XtraGrid.GridControlExMenuEventHandle(ctlEx_BeforeContextMenuClick);

                    if (!_HasInitialGird) {
                        _HasInitialGird = true;
                        DevExpress.XtraGrid.Views.Grid.GridView gridView = _XtraGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
                        gridView.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(gridView_InvalidRowException);
                        gridView.CustomFilterDialog += new DevExpress.XtraGrid.Views.Grid.CustomFilterDialogEventHandler(_GridView_CustomFilterDialog);
                    }
                }

            }
            //在该方法的开始部分已经做了相应的处理
            //else {
            //    resetBindingList(detailEntitys);
            //}
            if (mainEntity != null)
                setStateByMainDocState(mainEntity);

            editGrdCtl.RefreshDataSource();
        }

        

        #region gridView事件

        private void gridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e) {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore;
        }

        private void _GridView_CustomFilterDialog(object sender, DevExpress.XtraGrid.Views.Grid.CustomFilterDialogEventArgs e) {
            MyXtraGridFilterDialog dialog = new MyXtraGridFilterDialog(e.Column);
            dialog.ShowDialog();
            e.FilterInfo = new ColumnFilterInfo(dialog.GetCustomFiltersCriterion());
            e.Handled = true;
        }
        #endregion

        #endregion public 成员...

        /// <summary>
        /// 当UIGridCtlBatchAddHelper批量数据从数据小助手获得以后，会触发代理，执行该方法
        /// 由于对象是从服务器批量创建的, 主键也已经被创建, 这边不用处理
        /// </summary>
        /// <param name="sender">事件触发者</param>
        /// <param name="e">触发的参数</param>
        void batchAddHelper_AfterGetDataFromDataAssistance(object sender, AfterGetDataFromDataAssistanceEventArgs<T> e) {
            #region 设置当前上下文环境
            bool oldBindingListChanged = _BindingList.RaiseListChangedEvents;
            _BindingList.RaiseListChangedEvents = !_IsListChangedRaisedInBatchAddedSuppressed; //看看是不是要抑制ListChange, 以避免批量新增时, listChange事件导致的客户端订阅循环问题
            bool oldCreateNewEntityFromRule = _CreateNewEntityFromRule;
            _CreateNewEntityFromRule = false;//不管什么情况下限制所有的实体都不从业务类创建。
            #endregion

            List<T> newItems = new List<T>();
            foreach (T entity in e.SelectedItems) {
                //批量在本地创建新对象（在具体的应用中 有些对象可能必须在服务端创建，那么可以通过扩展的方式批量返回服务器端创建好的新对象，当前也可以在客户端的业务类中创建新对象。）
                _BindingList.Add(entity);
                newItems.Add(entity);

                if (!string.IsNullOrEmpty(_ForeignPropertyName)) {
                    if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, _ForeignPropertyName))
                        throw new MB.Util.APPException(string.Format("在创建明细对象的外键值时出错,请检查外键 {0} 配置 是否正确?", _ForeignPropertyName));

                    MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, _ForeignPropertyName, _MainKeyValue);
                }
                //设置对象状态
                MB.WinBase.UIDataEditHelper.Instance.SetEntityState(entity, MB.Util.Model.EntityState.New);
                //只有当批量新增事件触发，而ListChanged事件不触发时执行
                if (_IsListChangedRaisedInBatchAddedSuppressed) {
                    _BeforeSaveDetailEntityCache.AddAndDeleteEquals(new KeyValuePair<int, object>(_DataInDocType, entity));
                    if (_MainKeyValue != null)
                        _BindingDetailEntitys[_MainKeyValue].Add((T)entity);
                }
            }

            onAfterAllDataBatchAdd(new AfterAllDataBatchAddEventArgs<T>(newItems.ToArray()));

            #region 恢复上下文环境
            _BindingList.RaiseListChangedEvents = oldBindingListChanged;
            _CreateNewEntityFromRule = oldCreateNewEntityFromRule;
            #endregion
        }

        void ctlEx_BeforeContextMenuClick(object sender, MB.XWinLib.XtraGrid.GridControlExMenuEventArg arg) {
            if (arg.Handled) return;

            try {
                if (arg.MenuType == MB.XWinLib.XtraGrid.XtraContextMenuType.Delete) {
                    arg.Handled = true;
                    deleteSelectedRows();
                }
                if (arg.MenuType == MB.XWinLib.XtraGrid.XtraContextMenuType.BatchAdd) {
                    arg.Handled = true;
                    checkMainEntityDocState();

                    using (ProcessScopeControl scope = new ProcessScopeControl(_IsInBatchAdd, _BeforeSaveDetailEntityCache)) {
                        UIGridCtlBatchAddHelper<T> batchAddHelper = new UIGridCtlBatchAddHelper<T>(_EditForm, _EditCols, _BindingList, _DataInDocType);
                        batchAddHelper.IsEntityBatchCreatedFromServer = _IsEntityBatchAddedFromServer;
                        batchAddHelper.AfterGetDataFromDataAssistance += new EventHandler<AfterGetDataFromDataAssistanceEventArgs<T>>(batchAddHelper_AfterGetDataFromDataAssistance);
                        batchAddHelper.ShowBatchAppendDataToGrid();
                        
                    }

                }
                else if (arg.MenuType == MB.XWinLib.XtraGrid.XtraContextMenuType.QuickInput) {

                    #region 快速填充
                    arg.Handled = true;
                    if (arg.Column != null) {
                        DevExpress.XtraGrid.Views.Grid.GridView gridView = _XtraGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
                        if (arg.Column.OptionsColumn.AllowEdit && gridView.FocusedRowHandle >= 0) {
                            var dre = MB.WinBase.MessageBoxEx.Question("是否决定以当前选择列的值进行快速填充");
                            if (dre != DialogResult.Yes) return;

                            object entity = gridView.GetRow(gridView.FocusedRowHandle);
                            object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, arg.Column.FieldName);

                            List<int> dataRowIndexs = new List<int>(); //保存要改变的数据源的INDEX，主要用于GRIDVIEW在过滤以后，只改变过滤后的行数
                            //其他一般的填充方式
                            if (val != null) {
                                int count = gridView.RowCount;
                                for (int i = 0; i < count; i++) {
                                    gridView.SetRowCellValue(i, arg.Column.FieldName, val);
                                    int dataRowIndex = gridView.GetDataSourceRowIndex(i);
                                    if (dataRowIndex >= 0) {
                                        dataRowIndexs.Add(dataRowIndex);
                                    }
                                }
                            }

                            #region 对于ClickButtonInput组件 关于快速填充的处理

                            if (_EditCols != null && dataRowIndexs.Count > 0) {
                                if (_EditCols.ContainsKey(arg.Column.FieldName)) {
                                    ColumnEditCfgInfo colEditCfgInfo = _EditCols[arg.Column.FieldName];
                                    if (colEditCfgInfo.EditCtlDataMappings != null && colEditCfgInfo.EditCtlDataMappings.Count > 0) {
                                        colEditCfgInfo.EditCtlDataMappings.ForEach(mappingInfo => {
                                            object mappingValue = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, mappingInfo.ColumnName);
                                            if (mappingValue != null) {
                                                foreach (int dataRowIndex in dataRowIndexs) {
                                                    object desEntity = _BindingList[dataRowIndex];
                                                    MB.Util.MyReflection.Instance.InvokePropertyForSet(desEntity, mappingInfo.ColumnName, mappingValue);
                                                }
                                                //foreach (object e in _BindingList) {
                                                //    if (e.Equals(entity)) continue;
                                                //    MB.Util.MyReflection.Instance.InvokePropertyForSet(e, mappingInfo.ColumnName, mappingValue);
                                                //}
                                               
                                            }
                                        });
                                    }
                                }
                            }

                            #endregion

                        }
                    }
                    #endregion
                }
                else if (arg.MenuType == MB.XWinLib.XtraGrid.XtraContextMenuType.DataImport) {

                    #region 导入

                    arg.Handled = true;
                    checkMainEntityDocState();

                    if (!string.IsNullOrEmpty(_CurrentXmlFileName)) {
                        var importData = DefaultDataImportDialog.ShowDataImport(_EditForm as IWin32Window,
                                                                _EditForm.ClientRuleObject as MB.WinBase.IFace.IClientRule,
                                                                _CurrentXmlFileName, false, _BusinessCheck4DataImport);

                        if (importData != null) {
                            GridBeforeDataImportEventArgs barg = new GridBeforeDataImportEventArgs(importData);
                            onBeforeDataImport(barg);

                            if (barg.Cancel) return;

                            var cfgInfo = MB.WinBase.LayoutXmlConfigHelper.Instance.GetDataImportCfgInfo(_CurrentXmlFileName, null);

                            if (cfgInfo != null) {
                                if (cfgInfo.Operate == MB.WinBase.Common.DataImportOperate.Overide)
                                    dataImportByOveride(importData.ImportData, cfgInfo, false);
                                else
                                    dataImportByOveride(importData.ImportData, cfgInfo, true);
                            }
                            else {
                                dataImportByAdd(importData.ImportData.Tables[0].Select(), true);
                            }

                            _BindingList.RaiseListChangedEvents = true;
                            _BindingList.ResetBindings();
                            _XtraGrid.RefreshDataSource();
                        }

                    }
                    else {
                        MB.WinBase.MessageBoxEx.Show("该网格控件还未绑定任何数据源,无法进行数据的导入操作！");
                    }
                    #endregion

                }
                else if (arg.MenuType == MB.XWinLib.XtraGrid.XtraContextMenuType.ExcelEdit) {
                    arg.Handled = true;
                    #region excel编辑
                    Dictionary<int, T> bindingListDic = _BindingList.ToDictionary<T, int>((t) => 
                    {
                        int id = (int)_DAccs[SOD.OBJECT_PROPERTY_ID].Get(t);
                        return id;
                    });

                    List<T> newAddedList = new List<T>();
                    List<T> modifiedList = new List<T>();
                    List<T> deletedList = new List<T>();
                    IExcelEditor excelEdit = new WinformExcelEditor<T>(_ClientRule, _BindingList.ToList<T>(), _CurrentXmlFileName, _DataInDocType);
                    frmExcelEdit excelEditFrm = new frmExcelEdit(excelEdit);
                    DialogResult excelEditFrmResult = excelEditFrm.ShowDialog();
                    if (excelEditFrmResult == DialogResult.OK) {
                        IList result = excelEditFrm.SumittedList;
                        foreach (object obj in result) {
                            if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(obj, SOD.OBJECT_PROPERTY_ID))
                                throw new MB.Util.APPException("暂时只能支持以ID为主键的对象", Util.APPMessageType.DisplayToUser);

                            MB.Util.Model.EntityState entityState = (MB.Util.Model.EntityState)_DAccs[COL_ENTITY_STATE_PROPERTY].Get(obj);
                            if (entityState == Util.Model.EntityState.New) {
                                newAddedList.Add((T)obj);
                            }
                            else if (entityState == Util.Model.EntityState.Modified) { 
                                modifiedList.Add((T)obj);
                            }
                            else if (entityState == Util.Model.EntityState.Deleted) {
                                deletedList.Add((T)obj);
                            }
                        }

                        if (newAddedList.Count > 0) {
                            int id = _ClientRule.GetCreateNewEntityIds(_DataInDocType, newAddedList.Count);
                            //如果返回的值等于 0 表示不存在自增加列
                            bool notExistsSelfIDKey = id == 0;
                            bool old = _CreateNewEntityFromRule;
                            //不管什么情况下限制所有的实体都不从业务类创建。
                            _CreateNewEntityFromRule = false;

                            newAddedList.ForEach(t => {
                                    //批量在本地创建新对象（在具体的应用中 有些对象可能必须在服务端创建，那么可以通过扩展的方式批量返回服务器端创建好的新对象，当前也可以在客户端的业务类中创建新对象。）
                                    _BindingList.Add(t);
                                    if (!notExistsSelfIDKey) {
                                        //设置对象键值
                                        _DAccs[SOD.OBJECT_PROPERTY_ID].Set(t, id);
                                        id += 1;
                                    }
                                });
                        }

                        if (modifiedList.Count > 0) {
                            modifiedList.ForEach(t => {
                                int id = (int)_DAccs[SOD.OBJECT_PROPERTY_ID].Get(t);
                                T originalEntity = bindingListDic[id];
                                foreach (var dataAcc in _DAccs.Values) {
                                    object tmpValue = dataAcc.Get(t);
                                    dataAcc.Set(originalEntity, tmpValue);
                                }
                            });
                        }

                        if (deletedList.Count > 0) { 
                            deletedList.ForEach(t => 
                                {
                                    int id = (int)_DAccs[SOD.OBJECT_PROPERTY_ID].Get(t);
                                    DeleteListItem(bindingListDic[id]);
                                });
                            
                        }
                    }
                    else if (excelEditFrmResult == DialogResult.Abort) {
                        throw new MB.Util.APPException("Excel编辑出问题了", Util.APPMessageType.DisplayToUser);
                    }
                    #endregion
                }

            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        /// <summary>
        /// 创建动态对象的属性访问器，以便快速的动态访问
        /// </summary>
        /// <param name="typeObject">对象的类型</param>
        /// <returns>属性访问器，以字段存储</returns>
        private Dictionary<string, DynamicPropertyAccessor> createDynamicProperyAccess(Type typeObject) {
            PropertyInfo[] infos = typeObject.GetProperties();
            Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> dAccs = new Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor>();
            foreach (PropertyInfo info in infos) {
                object[] atts = info.GetCustomAttributes(typeof(DataMemberAttribute), true);
                if (atts == null || atts.Length == 0) continue;

                dAccs.Add(info.Name, new MB.Util.Emit.DynamicPropertyAccessor(typeObject, info));
            }
            return dAccs;
        }

        private void checkMainEntityDocState() {
            if (_MainEntity == null) return;

            var exists = MB.WinBase.UIDataEditHelper.Instance.CheckExistsDocState(_MainEntity);
            if (!exists) return;

            var docState = MB.WinBase.UIDataEditHelper.Instance.GetEntityDocState(_MainEntity);
            if (docState != MB.Util.Model.DocState.Progress)
                throw new MB.Util.APPException("单据已经提交确认,不能进行该项操作！", MB.Util.APPMessageType.DisplayToUser);
        }
        #region 数据导入处理相关...
        //以新增的方式导入数据
        private void dataImportByAdd(DataRow[] drsData, bool checkLocalExists) {
            DataRow[] drs = drsData;
            //在导入之前要先根据下面的2步先进行数据的处理
            //1)先去掉已经重复的数据
            if (checkLocalExists)
                drs = filterExistsData(drs);
            //2)有些时候在引用数据库存在的数据时，只能导入数据库中存在的数据
            drs = filterOnlyDBExists(drs);

            _BindingList.RaiseListChangedEvents = false;
            MB.WinBase.IFace.IClientRule clientRule = _EditForm.ClientRuleObject as MB.WinBase.IFace.IClientRule;
            int id = clientRule.GetCreateNewEntityIds(_DataInDocType, drs.Length);
            //如果返回的值等于 0 表示不存在自增加列
            bool notExistsSelfIDKey = id == 0;
            bool old = _CreateNewEntityFromRule;
            //不管什么情况下限制所有的实体都不从业务类创建。
            _CreateNewEntityFromRule = false;
            List<T> importItems = new List<T>();
            foreach (DataRow dr in drs) {

                //批量在本地创建新对象（在具体的应用中 有些对象可能必须在服务端创建，那么可以通过扩展的方式批量返回服务器端创建好的新对象，当前也可以在客户端的业务类中创建新对象。）
                T newEntity = _BindingList.AddNew();
                MB.Util.MyReflection.Instance.FillModelObject(newEntity, dr);
                if (!notExistsSelfIDKey) {
                    //设置对象键值
                    MB.Util.MyReflection.Instance.InvokePropertyForSet(newEntity, SOD.OBJECT_PROPERTY_ID, id);
                    id += 1;
                }

                if (!string.IsNullOrEmpty(_ForeignPropertyName)) {
                    if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(newEntity, _ForeignPropertyName))
                        throw new MB.Util.APPException(string.Format("在创建明细对象的外键值时出错,请检查外键 {0} 配置 是否正确?", _ForeignPropertyName));

                    MB.Util.MyReflection.Instance.InvokePropertyForSet(newEntity, _ForeignPropertyName, _MainKeyValue);
                }
                //设置对象状态
                MB.WinBase.UIDataEditHelper.Instance.SetEntityState(newEntity, MB.Util.Model.EntityState.New);
                _BeforeSaveDetailEntityCache.AddAndDeleteEquals(new KeyValuePair<int, object>(_DataInDocType, newEntity));

                AfterDataItemImportEventArgs<T> arg = new AfterDataItemImportEventArgs<T>(newEntity);
                onAfterDataItemImport(arg);

                importItems.Add(newEntity);
            }
            //增加所有行都Import 后相应对应的事件

            onAfterAllDataItemImport(new AfterAllDataItemImportEventArgs<T>(importItems.ToArray()));

            _CreateNewEntityFromRule = old;

            //
            //_BindingList.RaiseListChangedEvents = true;
            //_BindingList.ResetBindings();
        }
        //以覆盖的方式进行数据导入
        private void dataImportByOveride(DataSet dsData, MB.WinBase.Common.DataImportCfgInfo importCfgInfo, bool allAddWhenNotExists) {
            DataRow[] drs = dsData.Tables[0].Select();

            string[] keys = importCfgInfo.OverideKeys.Split(',');
            string[] oFields = importCfgInfo.OverideFields.Split(',');
            if (keys == null || keys.Length == 0)
                throw new MB.Util.APPException("在数据覆盖导入是对DataImportCfgInfo 没有 配置 OverideKeys");
            if (oFields == null || oFields.Length == 0)
                throw new MB.Util.APPException("在数据覆盖导入是对DataImportCfgInfo 没有 配置 OverideFields");
            List<DataRow> notExists = new List<DataRow>();
            _BindingList.RaiseListChangedEvents = false;
            foreach (DataRow dr in drs) {
                object findEntity = null;
                string[] vals = MB.Util.DataHelper.Instance.GetMultiFieldValue(dr, keys);

                bool exits = MB.Util.DataValidated.Instance.CheckEntityDataExists(_BindingList, keys, vals.ToArray(), out findEntity);
                if (!exits) {
                    if (allAddWhenNotExists) {
                        notExists.Add(dr);
                    }
                    continue;
                }

                foreach (string field in oFields) {
                    if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(findEntity, field))
                        throw new MB.Util.APPException(string.Format("导入配置的属性{0}不属于{1}", field, findEntity.GetType().FullName));
                    if (!dr.Table.Columns.Contains(field))
                        continue;
                    MB.Util.MyReflection.Instance.InvokePropertyForSet(findEntity, field, dr[field]);
                }

                var entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(findEntity);
                if (entityState != MB.Util.Model.EntityState.New) {
                    MB.WinBase.UIDataEditHelper.Instance.SetEntityState(findEntity, MB.Util.Model.EntityState.Modified);
                    _BeforeSaveDetailEntityCache.AddAndDeleteEquals(new KeyValuePair<int, object>(_DataInDocType, findEntity));
                }
            }
            //以新增的方式导入其它数据
            dataImportByAdd(notExists.ToArray(), false);
        }


        //过滤已经存在的数据
        private DataRow[] filterExistsData(DataRow[] drsData) {
            string[] logicKeys = MB.WinBase.LayoutXmlConfigHelper.Instance.GetLogicKeys(_CurrentXmlFileName);
            if (logicKeys == null || logicKeys.Length == 0) return drsData;

            List<DataRow> allowImports = new List<DataRow>();
            foreach (DataRow dr in drsData) {
                List<string> vals = new List<string>();
                for (int i = 0; i < logicKeys.Length; i++) {
                    if (!dr.Table.Columns.Contains(logicKeys[i]))
                        throw new MB.Util.APPException(string.Format("在进行数据本地逻辑主键 检验时, 数据结构中不包含 属性 {0},请检查XML 文件中LogicKeys 配置是否正确", logicKeys[i]));
                    if (dr[logicKeys[i]] == System.DBNull.Value)
                        vals.Add(string.Empty);
                    else
                        vals.Add(dr[logicKeys[i]].ToString());
                }
                bool hasAdd = MB.Util.DataValidated.Instance.CheckDataExists(allowImports.ToArray(), logicKeys, vals.ToArray());
                if (!hasAdd) {
                    object findEntity = null;
                    bool exits = MB.Util.DataValidated.Instance.CheckEntityDataExists(_BindingList, logicKeys, vals.ToArray(), out findEntity);
                    if (!exits)
                        allowImports.Add(dr);
                }
            }
            return allowImports.ToArray();
        }
        //删除掉数据库中不存在的数据。
        private DataRow[] filterOnlyDBExists(DataRow[] drsData) {
            string[] checkFields = MB.WinBase.LayoutXmlConfigHelper.Instance.GetExistsCheckFields(_CurrentXmlFileName);
            if (checkFields == null || checkFields.Length == 0) return drsData;
            if (drsData == null || drsData.Length == 0) return drsData;
            DataRow drRoot = drsData[0];

            Dictionary<string, DataRow[]> checkDatas = new Dictionary<string, DataRow[]>();
            List<DataRow> filterData = new List<DataRow>(drsData);
            List<DataRow> delsData = new List<DataRow>();
            foreach (string name in checkFields) {
                if (!drRoot.Table.Columns.Contains(name))
                    throw new MB.Util.APPException(string.Format("在进行数据本地逻辑主键 检验时, 数据结构中不包含 属性 {0},请检查XML 文件中DBDataExistsCheck 配置是否正确", name));

                List<string> vals = new List<string>();
                foreach (DataRow dr in filterData) {
                    string tempVal = dr[name].ToString();
                    if (!vals.Contains(tempVal))
                        vals.Add(tempVal);
                }
                BindingEditGridDataImportEventArgs arg = new BindingEditGridDataImportEventArgs(vals.ToArray());

                onDbDataExistsValidated(arg);
                //如果客户端没有实现相应的事件处理,默认情况下表示不需要进行处理
                if (arg.Handed) {
                    string[] existsDatas = arg.ExistsData;
                    if (existsDatas == null || existsDatas.Length == 0)
                        return new DataRow[0] { };
                    foreach (DataRow dr in filterData) {
                        string key = dr[name].ToString();
                        if (Array.IndexOf<string>(existsDatas, key) < 0) {
                            delsData.Add(dr);
                        }
                    }
                    foreach (DataRow dr in delsData)
                        filterData.Remove(dr);
                }
            }
            return filterData.ToArray();

        }
        #endregion 数据导入处理相关...

        #region 数据绑定处理相关...
        private void xtraGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {

            if (_XtraGrid != null) {
                if (e.KeyCode == System.Windows.Forms.Keys.Delete) {
                    if (_AllowDataDelete && !_IsReadonly && _XtraGrid.ValidedDeleteKeyDown)
                        deleteSelectedRows();
                }
                return;
            }
        }
        //删除当前选择的行
        private void deleteSelectedRows() {
            if (_IsReadonly || !_AllowDataDelete) return;

            DevExpress.XtraGrid.Views.Grid.GridView gridView = _XtraGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
            int[] handles = gridView.GetSelectedRows();
            List<T> deletedItems = new List<T>();
            foreach (int handle in handles) {
                T entity = (T)gridView.GetRow(handle);
                if (entity != null) {
                    deletedItems.Add(entity);
                    
                }
            }
            foreach (T entity in deletedItems) {
                DeleteListItem((T)entity);
            }
        }
        private void _ClientRule_AfterDocStateChanged(object sender, MB.WinBase.IFace.UIClientRuleDataEventArgs arg) {
            setStateByMainDocState(arg.MainEntity);

        }

        private void _EditEntitys_BeforeDataSave(object sender, EventArgs e) {
            //在保存之前调用
        }
        //void _DetailBindingParams_AfterCreateNewEntity<T>(object sender, MB.XWinLib.GridDataBindingParamEventArgs<T> arg) {

        // 设置明细数据的编辑状态。
        private void setStateByMainDocState(object mainEntity) {
            bool exists = MB.WinBase.UIDataEditHelper.Instance.CheckExistsDocState(mainEntity);
            bool isReadonly = (_EditForm != null && _EditForm.CurrentEditType == MB.WinBase.Common.ObjectEditType.OpenReadOnly);
            if (!isReadonly && !exists) return;

            if (!isReadonly) {
                MB.Util.Model.DocState docState = MB.WinBase.UIDataEditHelper.Instance.GetEntityDocState(mainEntity);
                isReadonly = docState != MB.Util.Model.DocState.Progress;
            }
            _BindingList.AllowEdit = !isReadonly;
            _BindingList.AllowNew = (!isReadonly && _AllowNew);
            _BindingList.AllowRemove = !isReadonly;
            if (_XtraGrid == null) return;
            if (isReadonly) {
                (_XtraGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView).OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            }
            else {
                if (_AllowNew)
                    (_XtraGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView).OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            }
            _IsReadonly = isReadonly;
            //if (_XtraGrid != null)
            //    _XtraGrid.ValidedDeleteKeyDown = !isReadonly;
        }

        // 在列相同的情况下，重新设置绑定数据源。
        private void resetBindingList(T[] dataArray) {
            _BindingList.RaiseListChangedEvents = false;
            _BindingList.Clear();
            convertArrayToBindingList(_BindingList, dataArray);
            _BindingList.RaiseListChangedEvents = true;
            _BindingList.ResetBindings();
        }
        //把实体数组转换为绑定的集合列表对象
        private void convertArrayToBindingList(BindingList<T> bindingList, T[] dataArray) {
            _BindingList.RaiseListChangedEvents = false;

            if (dataArray != null && dataArray.Length > 0)
                foreach (T info in dataArray) {
                    bindingList.Add(info);
                }
            _BindingList.RaiseListChangedEvents = true;
        }
        //删除已经在集合编辑中存在的实体对象
        private void deleteExists(object entity) {
            List<KeyValuePair<int, object>> deletes = new List<KeyValuePair<int, object>>();
            foreach (KeyValuePair<int, object> info in _BeforeSaveDetailEntityCache) {
                if (info.Key == _DataInDocType && object.Equals(entity, info.Value)) {
                    deletes.Add(info);
                }
            }
            if (deletes.Count > 0) {
                _BindingList.RaiseListChangedEvents = false;
                foreach (KeyValuePair<int, object> del in deletes) {
                    _BeforeSaveDetailEntityCache.Remove(del);
                }
                _BindingList.RaiseListChangedEvents = true;
            }
        }

        private void _BindingList_ListChanged(object sender, ListChangedEventArgs e) {
            //判断如果是在 _EditEntitys 的修改 明显禁止的事件就不做处理。
            if (!_IsInBatchAdd.IsInScope && _BeforeSaveDetailEntityCache.RaiseListChangedEvents == false) return;

            if (e.ListChangedType == ListChangedType.Reset) return;

            

            if (e.ListChangedType == ListChangedType.ItemDeleted) {
                #region 暂时注释掉，如果有需要在拿出来
                //在详细绑定触发的删除操作，会显示调用DeleteListItem方法
                //但是有时数据源会触发删除操作这是需要在当前事件中调用DeleteListItem方法
                //int deletedItemIndex = e.NewIndex;
                //if (_MainKeyValue != null && e.NewIndex >= 0) {
                //    if (_BindingDetailEntitys[_MainKeyValue].Count > deletedItemIndex) {
                //        T currentDeletedEntity = _BindingDetailEntitys[_MainKeyValue][deletedItemIndex];
                //        DeleteListItem(currentDeletedEntity); 
                //    }
                //}
                #endregion
                return;
            }

            T currentEntity = (T)_BindingList[e.NewIndex];
            

            //
            if (e.ListChangedType == ListChangedType.ItemAdded) {
                if (!string.IsNullOrEmpty(_ForeignPropertyName)) {
                    if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(currentEntity, _ForeignPropertyName))
                        throw new MB.Util.APPException(string.Format("在创建明细对象的外键值时出错,请检查外键 {0} 配置 是否正确?", _ForeignPropertyName));

                    MB.Util.MyReflection.Instance.InvokePropertyForSet(currentEntity, _ForeignPropertyName, _MainKeyValue);
                }

                MB.WinBase.UIDataEditHelper.Instance.SetEntityState(currentEntity, MB.Util.Model.EntityState.New);

                if (_BeforeSaveDetailEntityCache != null) {
                    //判断是否当前的操作为批量新增加
                    if (_IsInBatchAdd.IsInScope) {

                        _BeforeSaveDetailEntityCache.Add(new KeyValuePair<int, object>(_DataInDocType, currentEntity));
                        //在一个批量新增加的操作中，只要第一个新增的对象抛出修改的事件就可以
                        _BeforeSaveDetailEntityCache.RaiseListChangedEvents = false;

                    }
                    else {
                        _BeforeSaveDetailEntityCache.AddAndDeleteEquals(new KeyValuePair<int, object>(_DataInDocType, currentEntity));

                    }


                    if (_MainKeyValue != null)
                        _BindingDetailEntitys[_MainKeyValue].Add(currentEntity);
                }

                onListChanged(new BindlingEditGridDataEventArgs<T>(currentEntity, ListChangedType.ItemAdded));
            }
            else if (e.ListChangedType == ListChangedType.ItemChanged) {
                MB.Util.Model.EntityState entityState = _IsInBatchAdd.IsInScope ? MB.Util.Model.EntityState.New : MB.WinBase.UIDataEditHelper.Instance.GetEntityState(currentEntity);
                if (entityState == MB.Util.Model.EntityState.Persistent) {
                    if (_MainEntity != null && MB.WinBase.UIDataEditHelper.Instance.CheckExistsDocState(_MainEntity)) {
                        var mainEntityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityDocState(_MainEntity);
                        if (mainEntityState != MB.Util.Model.DocState.Progress) return;
                    }
                    _BindingList.RaiseListChangedEvents = false;
                    MB.WinBase.UIDataEditHelper.Instance.SetEntityState(currentEntity, MB.Util.Model.EntityState.Modified);
                    _BindingList.RaiseListChangedEvents = true;

                    if (_BeforeSaveDetailEntityCache != null) {
                        deleteExists(_BindingList[e.NewIndex]);

                        _BeforeSaveDetailEntityCache.AddAndDeleteEquals(new KeyValuePair<int, object>(_DataInDocType, currentEntity));
                    }
                }
                onListChanged(new BindlingEditGridDataEventArgs<T>(currentEntity, ListChangedType.ItemChanged));
            }
            else {

            }
        }
        private void _BindingList_AddingNew(object sender, AddingNewEventArgs e) {
            if (!_CreateNewEntityFromRule)  return;

            try {
                T newEntity = (T)_ClientRule.CreateNewEntity(_DataInDocType);


                //onAfterCreateNewEntity(new GridDataBindingParamEventArgs<T>(newEntity));


                if (!string.IsNullOrEmpty(_ForeignPropertyName)) {
                    if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(newEntity, _ForeignPropertyName))
                        throw new MB.Util.APPException(string.Format("在创建明细对象的外键值时出错,请检查外键 {0} 配置 是否正确?", _ForeignPropertyName));

                    MB.Util.MyReflection.Instance.InvokePropertyForSet(newEntity, _ForeignPropertyName, _MainKeyValue);
                }

                e.NewObject = newEntity;

                onAfterAddingNew(new BindlingEditGridDataEventArgs<T>(newEntity, ListChangedType.ItemAdded));
            }
            catch (Exception ex) {
                throw new MB.Util.APPException("从服务中创建实体对象有误！", MB.Util.APPMessageType.DisplayToUser, ex);
            }
            
        }

        /// <summary>
        /// 正在编辑的实体对象。
        /// </summary>
        public MB.WinBase.UIEditEntityList BeforeSaveDetailEntityCache {
            set {
                _BeforeSaveDetailEntityCache = value;
            }
        }
        #endregion 数据绑定处理相关...

        #region IDispose...
        private bool disposed = false;
        public void Dispose() {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    try {
                        _ClientRule.AfterDocStateChanged -= new MB.WinBase.IFace.UIClientRuleDataEventHandle(_ClientRule_AfterDocStateChanged);
                        _XtraGrid.KeyDown -= new System.Windows.Forms.KeyEventHandler(xtraGrid_KeyDown);

                        if (_XtraGrid != null)
                            _XtraGrid.BeforeContextMenuClick -= new MB.XWinLib.XtraGrid.GridControlExMenuEventHandle(ctlEx_BeforeContextMenuClick);

                        //if (_DetailBindingParams != null) {
                        //    _DetailBindingParams.Dispose();
                        //}
                    }
                    catch { }

                }
                disposed = true;
            }
        }
        ~UIBindingEditGridCtl() {
            Dispose(false);
        }
        #endregion IDispose...

    }
    /// <summary>
    /// 绑定控件响应控件处理相关。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BindlingEditGridDataEventArgs<T> : System.EventArgs
    {
        private T _CurrentItem;
        private ListChangedType _ListChangedType;

        public ListChangedType ListChangedType {
            get { return _ListChangedType; }
            set { _ListChangedType = value; }
        }
        public BindlingEditGridDataEventArgs(T currentItem, ListChangedType listChangedType) {
            _CurrentItem = currentItem;
            _ListChangedType = listChangedType;
        }
        public T CurrentItem {
            get {
                return _CurrentItem;
            }
            set {
                _CurrentItem = value;
            }
        }
    }

    #region BindingEditGridDataImportEventArgs...
    /// <summary>
    /// 导入新项后产生的事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AfterDataItemImportEventArgs<T> : System.EventArgs
    {
        private T _CurrentImportItem;
        public AfterDataItemImportEventArgs(T currentImportItem) {
            _CurrentImportItem = currentImportItem;
        }
        public T CurrentImportItem {
            get {
                return _CurrentImportItem;
            }
            set {
                _CurrentImportItem = value;
            }
        }
    }
    /// <summary>
    /// 导入新项后产生的事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AfterAllDataItemImportEventArgs<T> : System.EventArgs
    {
        private T[] _CurrentImportItem;

        public AfterAllDataItemImportEventArgs(T[] currentImportItem) {
            _CurrentImportItem = currentImportItem;
        }
        public T[] CurrentImportItem {
            get {
                return _CurrentImportItem;
            }
            set {
                _CurrentImportItem = value;
            }
        }
    }
    /// <summary>
    /// 批量新增以后产生的新事件参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AfterAllDataBatchAddEventArgs<T> : System.EventArgs {
        private T[] _BatchAddedItems;

        public AfterAllDataBatchAddEventArgs(T[] batchAddedItems) {
            _BatchAddedItems = batchAddedItems;
        }
        /// <summary>
        /// 批量新增的条目
        /// </summary>
        public T[] BatchAddedItems {
            get {
                return _BatchAddedItems;
            }
            set {
                _BatchAddedItems = value;
            }
        }
    }
    /// <summary>
    /// 网格编辑控件相关处理事件
    /// </summary>
    public class BindingEditGridDataImportEventArgs : System.EventArgs
    {
        private string[] _CheckValues;
        private string[] _ExistsData;
        private string _PropertyName;
        private bool _Handed;
        /// <summary>
        /// 网格编辑控件相关处理事件
        /// </summary>
        /// <param name="existsData"></param>
        public BindingEditGridDataImportEventArgs(string[] checkValues) {
            _CheckValues = checkValues;
        }
        /// <summary>
        /// 需要进行数据检验的属性名称。
        /// </summary>
        public string PropertyName {
            get {
                return _PropertyName;
            }
            set {
                _PropertyName = value;
            }
        }
        /// <summary>
        /// 准备进行检查的数据。
        /// </summary>
        public string[] CheckValues {
            get {
                return _CheckValues;
            }
            set {
                _CheckValues = value;
            }
        }
        /// <summary>
        /// 数据库中存在的数据。
        /// </summary>
        public string[] ExistsData {
            get {
                return _ExistsData;
            }
            set {
                _ExistsData = value;
                _Handed = true;
            }
        }
        /// <summary>
        /// 是否已经处理。
        /// </summary>
        public bool Handed {
            get {
                return _Handed;
            }
            set {
                _Handed = value;
            }
        }
    }
    #endregion BindingEditGridDataImportEventArgs...

    /// <summary>
    /// 数据导入中从XML 文件中获取数据后
    /// </summary>
    public class GridBeforeDataImportEventArgs : System.EventArgs
    {
        /// <summary>
        ///  数据导入中从XML 文件中获取数据后
        /// </summary>
        /// <param name="dataImportInfo"></param>
        public GridBeforeDataImportEventArgs(DataImportInfo dataImportInfo) {
            _DataImportInfo = dataImportInfo;
        }

        private bool _Cancel;
        /// <summary>
        /// 判断是否取消当前导入的动作。
        /// </summary>
        public bool Cancel {
            get { return _Cancel; }
            set { _Cancel = value; }
        }
        private DataImportInfo _DataImportInfo;
        /// <summary>
        /// 需要导入的数据信息。
        /// </summary>
        public DataImportInfo DataImportInfo {
            get { return _DataImportInfo; }
            set { _DataImportInfo = value; }
        }


    }
    public class ProcessScopeControlInfo
    {
        public bool IsInScope { get; set; }

    }
    public class ProcessScopeControl : IDisposable
    {
        private ProcessScopeControlInfo _ScopeInfo;
        private MB.WinBase.UIEditEntityList _BeforeSaveDetailEntityCache;
        public ProcessScopeControl(ProcessScopeControlInfo scopeInfo, MB.WinBase.UIEditEntityList beforeSaveDetailEntityCache) {
            _ScopeInfo = scopeInfo;
            _ScopeInfo.IsInScope = true;

            _BeforeSaveDetailEntityCache = beforeSaveDetailEntityCache;
        }

        public void Dispose() {
            _ScopeInfo.IsInScope = false;
            _BeforeSaveDetailEntityCache.RaiseListChangedEvents = true;

        }
    }

}
