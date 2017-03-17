//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-18
// Description	:	GridDataBindParam 主要针对XtraGrid 数据源绑定的参数设置项。
// Modify date	:			By:					Why: 
//---------------------------------------------------------------- 
using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;

namespace MB.XWinLib {
    /// <summary>
    /// GridDataBindParam 主要针对XtraGrid 数据源绑定的参数设置项。
    /// </summary>
    public class GridDataBindingParam {
        #region 变量定义...
        private DevExpress.XtraGrid.GridControl _XtraGrid;
        private object _DataSource;


        private bool _GroupByUISetting;
        #endregion 变量定义...

        #region 构造函数...
        /// <summary>
        /// 构造函数。
        /// </summary>
        public GridDataBindingParam() {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="xtraGrid"></param>
        /// <param name="dataSource"></param>
        public GridDataBindingParam(DevExpress.XtraGrid.GridControl xtraGrid, object dataSource) :
            this(xtraGrid, dataSource, false) {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="pDg"></param>
        /// <param name="pDataSource"></param>
        /// <param name="pPropertys"></param>
        /// <param name="imageViewUsePic"></param>
        /// <param name="editObj"></param>
        public GridDataBindingParam(DevExpress.XtraGrid.GridControl xtraGrid, object dataSource, bool groupByUISetting) {
            _XtraGrid = xtraGrid;
            _DataSource = dataSource;
            _GroupByUISetting = groupByUISetting;

        }
        #endregion 构造函数...

        #region Public 属性...
        /// <summary>
        /// 准备绑定数据源的XtraGrid。
        /// </summary>
        public DevExpress.XtraGrid.GridControl XtraGrid {
            get {
                return _XtraGrid;
            }
            set {
                _XtraGrid = value;
            }
        }
        /// <summary>
        /// 准备绑定到控件的数据源。
        /// </summary>
        public object DataSource {
            get {
                return _DataSource;
            }
            set {
                _DataSource = value;
            }
        }
        /// <summary>
        /// 判断控件数据的绑定是否使用UI 操作的分组方式。
        /// </summary>
        public bool GroupByUISetting {
            get {
                return _GroupByUISetting;
            }
            set {
                _GroupByUISetting = value;
            }
        }
        #endregion Public 属性...
    }
    ///// <summary>
    ///// 主要针对XtraGrid 数据源绑定的参数设置项。
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //public class GridDataBindingParam<T> : GridDataBindingParam, IDisposable {
    //    private BindingList<T> _BindingList;
    //    private MB.WinBase.IFace.IClientBaseRule _ClientRule;
    //    private int _DataInDocType;
    //    private bool _CreateNewEntityFromServer;
    //    private MB.WinBase.UIEditEntityList _BeforeSaveDetailEntityCache;

    //    #region 自定义事件处理相关...
    //    private GridDataBindingParamEventHandle<T> _AfterCreateNewEntity;
    //    /// <summary>
    //    /// 在创建新项后产生
    //    /// </summary>
    //    public event GridDataBindingParamEventHandle<T> AfterCreateNewEntity {
    //        add {
    //            _AfterCreateNewEntity += value;
    //        }
    //        remove {
    //            _AfterCreateNewEntity -= value;
    //        }
    //    }
    //    private void onAfterCreateNewEntity(GridDataBindingParamEventArgs<T> arg) {
    //        if (_AfterCreateNewEntity != null)
    //            _AfterCreateNewEntity(this, arg);
    //    }
    //    #endregion 自定义事件处理相关...



    //    /// <summary>
    //    /// 构造函数。
    //    /// </summary>
    //    /// <param name="xtraGrid"></param>
    //    /// <param name="dataArray"></param>
    //    /// <param name="clientRule"></param>
    //    /// <param name="dataInDocType"></param>
    //    /// <param name="editEntitysCache"></param>
    //    public GridDataBindingParam(DevExpress.XtraGrid.GridControl xtraGrid, T[] dataArray,
    //         MB.WinBase.IFace.IClientBaseRule clientRule, int dataInDocType,
    //        MB.WinBase.UIEditEntityList editEntitysCache) {

    //        _BindingList = new BindingList<T>();

    //        convertArrayToBindingList(_BindingList, dataArray);

    //        base.XtraGrid = xtraGrid;

    //        base.DataSource = _BindingList;
    //        base.GroupByUISetting = false;

    //        _BeforeSaveDetailEntityCache = editEntitysCache;
    //        _ClientRule = clientRule;
    //        _DataInDocType = dataInDocType;

    //        _BindingList.AddingNew += new AddingNewEventHandler(_BindingList_AddingNew);
    //        _BindingList.ListChanged += new ListChangedEventHandler(_BindingList_ListChanged);
    //        _BeforeSaveDetailEntityCache.BeforeDataSave += new EventHandler(_EditEntitys_BeforeDataSave);

    //        _ClientRule.AfterDocStateChanged += new MB.WinBase.IFace.UIClientRuleDataEventHandle(_ClientRule_AfterDocStateChanged);
    //        XtraGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(xtraGrid_KeyDown);
             
    //    }

    //    void xtraGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {

    //        MB.XWinLib.XtraGrid.GridControlEx grdCtlEx = XtraGrid as MB.XWinLib.XtraGrid.GridControlEx;
    //        if (grdCtlEx != null) {
    //            if (e.KeyCode == System.Windows.Forms.Keys.Delete && grdCtlEx.ValidedDeleteKeyDown) {
    //                DevExpress.XtraGrid.Views.Grid.GridView gridView = XtraGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
    //                int[] handles = gridView.GetSelectedRows();
    //                foreach (int handle in handles) {
    //                    T entity = (T)gridView.GetRow(handle);
    //                    if(entity!=null)
    //                        deleteListItem((T)entity);
    //                }
    //                gridView.DeleteSelectedRows(); 
    //            }
    //            return;
    //        }
    //    }

    //    void _ClientRule_AfterDocStateChanged(object sender, MB.WinBase.IFace.UIClientRuleDataEventArgs arg) {
    //        SetStateByMainDocState(arg.MainEntity);

    //    }

    //    void _EditEntitys_BeforeDataSave(object sender, EventArgs e) {
    //        //在保存之前调用
    //    }

    //    #region IDispose...
    //    private bool disposed = false;

    //    public void Dispose() {
    //        Dispose(true);

    //        GC.SuppressFinalize(this);
    //    }
    //    private void Dispose(bool disposing) {
    //        // Check to see if Dispose has already been called.
    //        if (!this.disposed) {
    //            // If disposing equals true, dispose all managed
    //            // and unmanaged resources.
    //            if (disposing) {
    //                try {
    //                    _ClientRule.AfterDocStateChanged -= new MB.WinBase.IFace.UIClientRuleDataEventHandle(_ClientRule_AfterDocStateChanged);
    //                    XtraGrid.KeyDown -= new System.Windows.Forms.KeyEventHandler(xtraGrid_KeyDown);
    //                }
    //                catch { }
    //            }
    //            disposed = true;

    //        }
    //    }
    //    ~GridDataBindingParam() {
    //        Dispose(false);
    //    }
    //    #endregion IDispose...

    //    /// <summary>
    //    /// 设置明细数据的编辑状态。
    //    /// </summary>
    //    /// <param name="mainEntity"></param>
    //    public void SetStateByMainDocState(object mainEntity) {
    //        bool exists = MB.WinBase.UIDataEditHelper.Instance.CheckExistsDocState(mainEntity);
    //        if (!exists) return;

    //        MB.Util.Model.DocState docState = MB.WinBase.UIDataEditHelper.Instance.GetEntityDocState(mainEntity);
    //        bool isReadonly = docState != MB.Util.Model.DocState.Progress;
    //        _BindingList.AllowEdit = !isReadonly;
    //        _BindingList.AllowNew = !isReadonly;
    //        _BindingList.AllowRemove = !isReadonly;
    //        if (isReadonly) {
    //            (XtraGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView).OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
    //        }
    //        else {
    //            (XtraGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView).OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
    //        }
    //        MB.XWinLib.XtraGrid.GridControlEx grdCtlEx = XtraGrid as MB.XWinLib.XtraGrid.GridControlEx;
    //        if (grdCtlEx != null)
    //            grdCtlEx.ValidedDeleteKeyDown = !isReadonly;
    //    }
    //    /// <summary>
    //    /// 在列相同的情况下，重新设置绑定数据源。
    //    /// </summary>
    //    /// <param name="dataArray"></param>
    //    public void ResetBindingList(T[] dataArray) {
    //        _BindingList.RaiseListChangedEvents = false;
    //        _BindingList.Clear();
    //        convertArrayToBindingList(_BindingList, dataArray);
    //        _BindingList.RaiseListChangedEvents = true;
    //        _BindingList.ResetBindings();

    //    }
    //    //把实体数组转换为绑定的集合列表对象
    //    private void convertArrayToBindingList(BindingList<T> bindingList, T[] dataArray) {
    //        if (dataArray != null && dataArray.Length > 0)
    //            foreach (T info in dataArray) {
    //                bindingList.Add(info);
    //            }
    //    }
    //    //删除已经在集合编辑中存在的实体对象
    //    private void deleteExists(object entity) {
    //        List<KeyValuePair<int, object>> deletes = new List<KeyValuePair<int, object>>();
    //        foreach (KeyValuePair<int, object> info in _BeforeSaveDetailEntityCache) {
    //            if (info.Key == _DataInDocType && object.Equals(entity, info.Value)) {
    //                deletes.Add(info);
    //            }
    //        }
    //        if (deletes.Count > 0) {
    //            _BindingList.RaiseListChangedEvents = false;
    //            foreach (KeyValuePair<int, object> del in deletes) {
    //                _BeforeSaveDetailEntityCache.Remove(del);
    //            }
    //            _BindingList.RaiseListChangedEvents = true;
    //        }
    //    }
    //    //删除当前选择的行
    //    private void deleteListItem(T currentEntity) {
    //        MB.Util.Model.EntityState entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(currentEntity);

    //        MB.WinBase.UIDataEditHelper.Instance.SetEntityState(currentEntity, MB.Util.Model.EntityState.Deleted);
    //        if (_BeforeSaveDetailEntityCache != null) {
    //            deleteExists(currentEntity);
    //            if (entityState != MB.Util.Model.EntityState.New)
    //                _BeforeSaveDetailEntityCache.AddAndDeleteEquals(new KeyValuePair<int, object>(_DataInDocType, currentEntity));
    //        }

    //    }
    //    void _BindingList_ListChanged(object sender, ListChangedEventArgs e) {
    //        //判断如果是在 _EditEntitys 的修改 明显禁止的事件就不做处理。
    //        if (_BeforeSaveDetailEntityCache.RaiseListChangedEvents == false) return;

    //        if (e.ListChangedType == ListChangedType.Reset) return;

    //        if (e.ListChangedType == ListChangedType.ItemDeleted) return; 

    //        T currentEntity = (T)_BindingList[e.NewIndex];

    //        MB.Util.Model.EntityState entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(currentEntity);
    //        if (e.ListChangedType == ListChangedType.ItemAdded) {
    //            if (_BeforeSaveDetailEntityCache != null) {
    //                _BeforeSaveDetailEntityCache.AddAndDeleteEquals(new KeyValuePair<int, object>(_DataInDocType, currentEntity));
    //            }
    //        }
    //        else if (e.ListChangedType == ListChangedType.ItemChanged) {
    //            if (entityState == MB.Util.Model.EntityState.Persistent) {
    //                MB.WinBase.UIDataEditHelper.Instance.SetEntityState(currentEntity, MB.Util.Model.EntityState.Modified);
    //                if (_BeforeSaveDetailEntityCache != null) {
    //                    deleteExists(_BindingList[e.NewIndex]);

    //                    _BeforeSaveDetailEntityCache.AddAndDeleteEquals(new KeyValuePair<int, object>(_DataInDocType, currentEntity));
    //                }
    //            }
    //        }
    //        else {

    //        }
    //    }

    //    void _BindingList_AddingNew(object sender, AddingNewEventArgs e) {
    //       // throw new Exception();

    //        if (!_CreateNewEntityFromServer) return;

    //        try {
    //            T newEntity = (T)_ClientRule.CreateNewEntity(_DataInDocType);

    //            onAfterCreateNewEntity(new GridDataBindingParamEventArgs<T>(newEntity));

    //            e.NewObject = newEntity;
    //        }
    //        catch (Exception ex) {
    //            throw new MB.Util.APPException( "从服务中创建实体对象有误！", MB.Util.APPMessageType.DisplayToUser,ex);
    //        }
    //    }
    //    /// <summary>
    //    /// 正在编辑的实体对象。
    //    /// </summary>
    //    public MB.WinBase.UIEditEntityList BeforeSaveDetailEntityCache {
    //        set {
    //            _BeforeSaveDetailEntityCache = value;
    //        }
    //    }
    //    /// <summary>
    //    /// 绑定的对象集合。
    //    /// </summary>
    //    public BindingList<T> BindingList {
    //        get {
    //            return _BindingList;
    //        }
    //        set {
    //            _BindingList = value;
    //        }
    //    }
    //    /// <summary>
    //    /// 获取或者设置是否从服务器创建新实体对象。
    //    /// </summary>
    //    public bool CreateNewEntityFromServer {
    //        get {
    //            return _CreateNewEntityFromServer;
    //        }
    //        set {
    //            _CreateNewEntityFromServer = value;
    //        }
    //    }

    //}
    //#region 自定义事件类型定义...
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="sender"></param>
    ///// <param name="arg"></param>
    //public delegate void GridDataBindingParamEventHandle<T>(object sender, GridDataBindingParamEventArgs<T> arg);
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //public class GridDataBindingParamEventArgs<T> {
    //    private T _CurrentEditItem;
    //    public GridDataBindingParamEventArgs(T currentEditItem) {
    //        _CurrentEditItem = currentEditItem;
    //    }
    //    /// <summary>
    //    /// 当前编辑的项。
    //    /// </summary>
    //    public T CurrentEditItem {
    //        get {
    //            return _CurrentEditItem;
    //        }
    //        set {
    //            _CurrentEditItem = value;
    //        }
    //    }
    //}
    //#endregion 自定义事件类型定义...
}

