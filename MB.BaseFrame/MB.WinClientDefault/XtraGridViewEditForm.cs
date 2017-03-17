//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-22-17
// Description	:	XtraGridViewEditForm: 基于MDI 子窗口的网格编辑窗口。
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

using MB.WinBase.Common;
using MB.WinClientDefault.Extender;
using MB.Util;
using MB.WcfServiceLocator;
using DevExpress.XtraGrid.Columns; 
namespace MB.WinClientDefault {
    /// <summary>
    /// 基于MDI 子窗口的网格编辑窗口。
    /// </summary>
    public partial class XtraGridViewEditForm : AbstractListViewForm {
        private List<object> _UnSaveEditEntitys;
        private ToolTip _ToolTip;

        /// <summary>
        /// 创建基于MDI 子窗口的网格编辑窗口
        /// </summary>
        public XtraGridViewEditForm() {
            InitializeComponent();

            this.Load += new EventHandler(XtraGridViewEditForm_Load);

            _ToolTip = new ToolTip();

            grdCtlMain.Dock = DockStyle.Fill;
            _UnSaveEditEntitys = new List<object>();

            grdCtlMain.BeforeContextMenuClick += new MB.XWinLib.XtraGrid.GridControlExMenuEventHandle(grdCtlMain_BeforeContextMenuClick);
        }

        void grdCtlMain_BeforeContextMenuClick(object sender, MB.XWinLib.XtraGrid.GridControlExMenuEventArg arg) {
            try {
                if (arg.MenuType == MB.XWinLib.XtraGrid.XtraContextMenuType.Delete) {
                    this.Delete();
                    arg.Handled = true;
                }
                else if(arg.MenuType == MB.XWinLib.XtraGrid.XtraContextMenuType.DataImport) {
                    this.DataImport();
                    arg.Handled = true;
                }
                                  
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        void XtraGridViewEditForm_Load(object sender, EventArgs e) {

            if (MB.Util.General.IsInDesignMode()) return;


            //获取业务类默认设置的过滤条件参数
            _CurrentQueryParameters = _ClientRuleObject.GetDefaultFilterParams();

            LoadObjectData(_CurrentQueryParameters);

            grdCtlMain.ValidedDeleteKeyDown = true;
            if (_ClientRuleObject != null) {
                string titleMag = string.Empty;
                if (string.IsNullOrEmpty(_ClientRuleObject.DefaultFilterMessage))
                    titleMag = GetDefaultFilterMessage(_ClientRuleObject.ClientLayoutAttribute.DefaultFilter);
                else
                    titleMag = _ClientRuleObject.DefaultFilterMessage;

                panTitle.Visible = true;
                _ToolTip.SetToolTip(panTitle, titleMag);
            }

            lnkNextPage.Click += new EventHandler(lnkNextPage_Click);
            lnkPreviousPage.Click += new EventHandler(lnkPreviousPage_Click);
        }

        void lnkPreviousPage_Click(object sender, EventArgs e) {
            _ClientRuleObject.CurrentQueryBehavior.PageIndex -= 1;
            Refresh();
        }

        void lnkNextPage_Click(object sender, EventArgs e) {
            _ClientRuleObject.CurrentQueryBehavior.PageIndex += 1;
            Refresh();
        }


       #region 覆盖基类的方法...
        protected override void LoadObjectData(MB.Util.Model.QueryParameterInfo[] queryParams) {

            if (_ClientRuleObject == null)
                throw new MB.Util.APPException("在加载浏览窗口<DefaultViewForm>时 需要配置对应的ClientRule 类！");

            if (_ClientRuleObject.ClientLayoutAttribute == null)
                throw new MB.Util.APPException(string.Format("对于客户段逻辑类 {0} ,需要配置 RuleClientLayoutAttribute.", _ClientRuleObject.GetType().FullName));



            


            try {

                //添加右键菜单扩展
                if (_ClientRuleObject.ReSetContextMenu != null) {
                    grdCtlMain.ContextMenu = _ClientRuleObject.ReSetContextMenu;
                    //else {
                    //    grdCtlMain.ContextMenu.MenuItems.Add(new MenuItem("-"));
                    //    foreach (MenuItem item in _ClientRuleObject.ReSetContextMenu.MenuItems) {
                    //        grdCtlMain.ContextMenu.MenuItems.Add(item);
                    //    }
                    //}

                }

                string messageHeaderKey = string.Empty;

                if (_ClientRuleObject.ClientLayoutAttribute.LoadType == ClientDataLoadType.ReLoad) {
                    messageHeaderKey = _ClientRuleObject.ClientLayoutAttribute.MessageHeaderKey;
                }


                gridViewMain.RowHeight = MB.WinBase.LayoutXmlConfigHelper.Instance.GetMainGridViewRowHeight(_ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile);
                using (MB.WinBase.WaitCursor cursor = new MB.WinBase.WaitCursor(this)) {
                    using (MethodTraceWithTime timeTrack = new MethodTraceWithTime(null)) {
                        //特殊说明 2009-02-20 在这里需要增加 RefreshLookupDataSource 以便在加载数据ID 时能得到对应的描述信息。
                        if (_ClientRuleObject.ClientLayoutAttribute.CommunicationDataType == CommunicationDataType.DataSet) {
                            throw new MB.Util.APPException("当前不支持基于DataSet 的网格编辑", MB.Util.APPMessageType.SysErrInfo);
                        }
                        else {

                            IList lstDatas = null;
                            try {
                                using (QueryBehaviorScope scope = new QueryBehaviorScope(_ClientRuleObject.CurrentQueryBehavior, messageHeaderKey)) {
                                    lstDatas = _ClientRuleObject.GetObjects((int)_ClientRuleObject.MainDataTypeInDoc, queryParams);
                                }
                            }
                            catch (Exception ex) {
                                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, " 在加载编辑网格窗口获取数据时执行业务类的 GetObjects 出错");
                            }

                            //IGridViewEditRule gridViewEditRule = _ClientRuleObject as IGridViewEditRule;
                            //if (gridViewEditRule == null)
                            //    throw new MB.Util.APPException("所有基于Mid 子窗口网格编辑的业务类必须实现IGridViewEditRule 接口", MB.Util.APPMessageType.SysErrInfo);

                            if (_BindingSource == null) {
                                IBindingList bl = _ClientRuleObject.CreateMainBindList(lstDatas);
                                _BindingSource = new MB.WinBase.Binding.BindingSourceEx();
                                _BindingSource.ListChanged += new ListChangedEventHandler(_BindingSource_ListChanged);
                                _BindingSource.AddingNew += new AddingNewEventHandler(_BindingSource_AddingNew);
                                _BindingSource.DataSource = bl;

                                MB.WinBase.Common.GridViewLayoutInfo gridViewLayoutInfo = MB.WinBase.LayoutXmlConfigHelper.Instance.GetGridColumnLayoutInfo(_ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile, string.Empty);
                                var detailBindingParams = new MB.XWinLib.GridDataBindingParam(grdCtlMain, _BindingSource, false);
                                MB.XWinLib.XtraGrid.XtraGridEditHelper.Instance.CreateEditXtraGrid(detailBindingParams, _ClientRuleObject.UIRuleXmlConfigInfo.GetDefaultColumns(), _ClientRuleObject.UIRuleXmlConfigInfo.ColumnsCfgEdit, gridViewLayoutInfo);

                            }
                            else {
                                MB.XWinLib.XtraGrid.XtraGridHelper.Instance.RefreshDataGrid(grdCtlMain, lstDatas);

                            }
                            #region 刷新status strip

                            int rowCount = (lstDatas == null ? 0 : lstDatas.Count);
                            var msg = string.Format("查询花费:{0} 毫秒,返回 {1} 记录,查询时间:{2}", timeTrack.GetExecutedTimes(), rowCount, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            string totalPageandCurrentPage = GetTotalPageAndCurrentpage(rowCount);

                            //刷新查询消息
                            labTitleMsg.Text = msg;

                            //刷新上一页/下一页
                            lnkNextPage.Enabled = rowCount >= _ClientRuleObject.CurrentQueryBehavior.PageSize;
                            lnkPreviousPage.Enabled = _ClientRuleObject.CurrentQueryBehavior.PageIndex > 0;

                            //刷新总页数
                            labTotalPageNumber.Visible = IsTotalPageDisplayed;
                            labCurrentPageNumber.Visible = IsTotalPageDisplayed;
                            if (this.IsTotalPageDisplayed) {
                                string[] totalPage_currentPage = totalPageandCurrentPage.Split(',');
                                labTotalPageNumber.Text = string.Format("共{0}页", totalPage_currentPage[0]);
                                labCurrentPageNumber.Text = string.Format("第{0}页", totalPage_currentPage[1]);

                            }


                            #endregion
                            _ClientRuleObject.BindingSource = _BindingSource;
                        }
                        panTitle.Visible = false;
                    }
                }

                //在GRIDVIEW创建完成以后
                //根据UI RULE来判断GRID的编辑状态，是否允许新增，修改，删除
                DevExpress.XtraGrid.Views.Grid.GridView mainView = grdCtlMain.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                if (_ClientRuleObject != null) {
                    var rejectCfg = MB.WinBase.Atts.AttributeConfigHelper.Instance.GetModuleRejectCommands(_ClientRuleObject.GetType());
                    if (rejectCfg != null) {
                        if ((rejectCfg.RejectCommands & UICommandType.AddNew) != 0) {

                            mainView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
                            mainView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                        }
                        else {
                            mainView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
                            mainView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
                        }

                        if ((rejectCfg.RejectCommands & UICommandType.Edit) != 0) {
                            mainView.OptionsBehavior.ReadOnly = true;
                            mainView.OptionsBehavior.Editable = false;
                        }
                        else {
                            mainView.OptionsBehavior.ReadOnly = false;
                            mainView.OptionsBehavior.Editable = true;
                        }

                        if ((rejectCfg.RejectCommands & UICommandType.Delete) != 0) {
                            mainView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
                        }
                        else {
                            mainView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
                        }
                    }
                }

                
            }
            catch (MB.Util.APPException aex) {
                throw aex;
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, "加载网格浏览编辑窗口Form_Load 时出错");
            }

        }

        void _BindingSource_AddingNew(object sender, AddingNewEventArgs e) {
            object newObj = _ClientRuleObject.CreateNewEntity((int)_ClientRuleObject.MainDataTypeInDoc);
            e.NewObject = newObj;
            _UnSaveEditEntitys.Add(newObj);

            MB.WinBase.AppMessenger.DefaultMessenger.Publish(XtraRibbonMdiMainForm.MSG_REQUIRE_REFRESH_BUTTONS);
        }

        void _BindingSource_ListChanged(object sender, ListChangedEventArgs e) {
            if (e.ListChangedType == ListChangedType.ItemChanged) {
                object currentEntity = _BindingSource[e.NewIndex];

                MB.Util.Model.EntityState entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(currentEntity);
                if (entityState == MB.Util.Model.EntityState.Persistent) {
                    MB.WinBase.UIDataEditHelper.Instance.SetEntityState(currentEntity, MB.Util.Model.EntityState.Modified);
                   // _UnSaveEditEntitys.Add(currentEntity);
                }
                if (!_UnSaveEditEntitys.Contains(currentEntity)) {
                    _UnSaveEditEntitys.Add(currentEntity);
                    MB.WinBase.AppMessenger.DefaultMessenger.Publish(XtraRibbonMdiMainForm.MSG_REQUIRE_REFRESH_BUTTONS);
                }
            }
            else {

            }
        }
        public override int Save() {
            if (!this.ExistsUnSaveData()) return 0;
            //先进行数据检查
            bool check = MB.WinBase.UIDataInputValidated.DefaultInstance.DetailGridDataValidated(_ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile, grdCtlMain.DataSource, _ClientRuleObject.ModuleTreeNodeInfo.Name);
            if (!check) return 0;

            int re = 0;
            System.ServiceModel.ICommunicationObject commObject = _ClientRuleObject.CreateServerCommunicationObject();
            try {
                foreach (object editEntity in _UnSaveEditEntitys) {
                    //判断并追加登录用户的相关信息( 实体数据的登录用户操作信息一般只在主表中存在 )
                    MB.WinBase.UIDataEditHelper.Instance.AppendLoginUserInfo(editEntity);

                    //增加主表实体对象
                    _ClientRuleObject.AddToCache(commObject, (int)_ClientRuleObject.MainDataTypeInDoc, editEntity, false, (string[])null);
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
                _UnSaveEditEntitys.Clear();
                this.Refresh();
                MB.WinBase.AppMessenger.DefaultMessenger.Publish(XtraRibbonMdiMainForm.MSG_REQUIRE_REFRESH_BUTTONS);
                MB.WinBase.MessageBoxEx.Show("数据保存成功");
            }
            return re;
        }
        public override bool ExistsUnSaveData() {

                return _UnSaveEditEntitys.Count > 0;
 
        }

        /// <summary>
        /// 清除没有保存的数据
        /// </summary>
        public void ClearUnSaveData() {
            _UnSaveEditEntitys.Clear();
        }

        public override MB.WinBase.Common.ClientUIType ActiveUIType {
            get {
                return MB.WinBase.Common.ClientUIType.GridViewEditForm;
            }
        }
        public override object GetCurrentMainGridView(bool mustEditGrid) {
                return grdCtlMain;
        }
        /// <summary>
        /// 继承的子类必须继承的方法
        /// </summary>
        /// <returns></returns>
        protected override MB.WinClientDefault.Common.MainViewDataNavigator GetViewDataNavigator() {
            return new MB.WinClientDefault.Common.MainViewDataNavigator(gridViewMain);

        }
        #endregion 覆盖基类的方法...

        private void gridViewMain_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e) {
            MB.WinClientDefault.Extender.IViewFormStyleProvider provider = _ClientRuleObject as MB.WinClientDefault.Extender.IViewFormStyleProvider;
            if (provider != null)
                provider.RowCellStyle(this, gridViewMain, e);
            else {
                MB.WinClientDefault.Extender.ViewGridExtenderHelper.CustomDrawRowStyleByDocState(this, gridViewMain, e);
            }
        }

        /// <summary>
        /// 得到 （总共页，当前页）格式以逗号隔开
        /// </summary>
        /// <param name="queryRowCount">当前数据源查出来的数据条数</param>
        /// <returns></returns>
        private string GetTotalPageAndCurrentpage(int queryRowCount) {
            int currentPageIndex = this._ClientRuleObject.CurrentQueryBehavior.PageIndex;
            int pageSize = this._ClientRuleObject.CurrentQueryBehavior.PageSize;
            int totalPageCount = 0;
            if (queryRowCount > 0) {
                if (QueryBehaviorScope.ResponseInfo != null) {
                    totalPageCount = QueryBehaviorScope.ResponseInfo.TotalRecordCount / pageSize;
                    if (QueryBehaviorScope.ResponseInfo.TotalRecordCount % pageSize != 0) {
                        totalPageCount += 1;
                    }
                }
            }
            string queryRefreshTotalPage = string.Join(",",
                new string[2] { totalPageCount.ToString(), 
                                            (queryRowCount > 0 ? (currentPageIndex + 1).ToString() : "0")});
            return queryRefreshTotalPage;
        }
    }
}
