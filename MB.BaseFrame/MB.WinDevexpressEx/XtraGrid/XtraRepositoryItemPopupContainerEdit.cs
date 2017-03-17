using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;

namespace MB.XWinLib.XtraGrid {
    /// <summary>
    /// 自定义的网格编辑下拉数据选择框。
    /// </summary>
    public class XtraRepositoryItemPopupContainerEdit : RepositoryItemPopupContainerEdit {
        #region 自定义变量...
        private DevExpress.XtraGrid.GridControl gridPopup;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1Popup;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl;
        private System.Drawing.Point _MousePosition;
        //private System.Drawing.Point _GridMousePosition;
        private System.Data.DataView dataViewLookup;
        private Dictionary<string, string> _LookUpDatas; 

        private string _ValueFieldName;
        private string _TextFieldName;
        private bool _AllowEdit;
       // private bool _IsDoubleClickSelected;

        private static string NULL_DISP_TEXT = "  <<不选择>>  ";
        #endregion 自定义变量...

        #region 构造函数...
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="editInfo"></param>
        public XtraRepositoryItemPopupContainerEdit(MB.WinBase.Common.ColumnEditCfgInfo editInfo, bool allowEdit) {
            _AllowEdit = allowEdit;
            this.gridView1Popup = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.popupContainerControl = new DevExpress.XtraEditors.PopupContainerControl();
            this.gridPopup = new DevExpress.XtraGrid.GridControl();
            //gridPopup
            this.gridPopup.EmbeddedNavigator.Name = "";
            this.gridPopup.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
																									 this.gridView1Popup});
            this.gridPopup.MainView = this.gridView1Popup;
            this.gridPopup.BindingContextChanged += new EventHandler(gridPopup_BindingContextChanged);
            //this.gridPopup.BindingContextChanged += new System.EventHandler(this.gridPopup_BindingContextChanged);
            this.gridPopup.Dock = System.Windows.Forms.DockStyle.Fill;

            this.gridView1Popup.GridControl = this.gridPopup;
            this.gridView1Popup.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView1Popup.OptionsBehavior.Editable = false;
            this.gridView1Popup.OptionsSelection.InvertSelection = true;
            this.gridView1Popup.OptionsSelection.EnableAppearanceHideSelection = true;
            setdatasource(gridView1Popup, editInfo);

            this.gridView1Popup.MouseMove += new MouseEventHandler(gridView1Popup_MouseMove);
            this.gridView1Popup.DoubleClick += new EventHandler(gridView1Popup_DoubleClick);
            this.gridView1Popup.KeyDown += new KeyEventHandler(gridView1Popup_KeyDown);


            // popupContainerControl
            // 
            this.popupContainerControl.Controls.Add(this.gridPopup);



            this.AutoHeight = false;
            this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //			this.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            //																														  new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CloseOnOuterMouseClick = false;
            this.Name = "xtraRepositoryItemPopupContainerEdit";
            this.PopupControl = this.popupContainerControl;

            this.QueryPopUp += new CancelEventHandler(XtraRepositoryItemPopupContainerEdit_QueryPopUp);
            this.QueryCloseUp += new CancelEventHandler(XtraRepositoryItemPopupContainerEdit_QueryCloseUp);
            this.QueryDisplayText += new DevExpress.XtraEditors.Controls.QueryDisplayTextEventHandler(XtraRepositoryItemPopupContainerEdit_QueryDisplayText);
            this.QueryResultValue += new DevExpress.XtraEditors.Controls.QueryResultValueEventHandler(XtraRepositoryItemPopupContainerEdit_QueryResultValue);
        }

        private void setdatasource(DevExpress.XtraGrid.Views.Grid.GridView gridView, MB.WinBase.Common.ColumnEditCfgInfo editInfo) {
            if (editInfo.DataSource == null)
                return;
            int index = 0;
            if (editInfo.LookUpColumns != null && editInfo.LookUpColumns.Count > 0) {
                gridView1Popup.OptionsView.ShowGroupPanel = editInfo.LookUpColumns.Count > 1;

                gridView.Columns.Clear();
                for (int i = 0; i < editInfo.LookUpColumns.Count; i++) {
                    MB.WinBase.Common.LookUpColumnInfo cInfo = editInfo.LookUpColumns[i];
                    DevExpress.XtraGrid.Columns.GridColumn col = gridView.Columns.Add();
                    col.FieldName = cInfo.FieldName;
                    col.Caption = cInfo.Description;
                    col.VisibleIndex = index++;
                    col.Width = cInfo.ShowWidth;
                }
            }
            else {
                gridView1Popup.OptionsView.ShowGroupPanel = false;
                DevExpress.XtraGrid.Columns.GridColumn singleCol = gridView.Columns.Add();
                singleCol.FieldName = editInfo.TextFieldName;
                singleCol.Caption = "请选择";
                singleCol.VisibleIndex = 0;
                singleCol.Width = 100;
            }
            _ValueFieldName = editInfo.ValueFieldName;
            _TextFieldName = editInfo.TextFieldName;
            DataTable dtData = MB.Util.MyConvert.Instance.ToDataTable(editInfo.DataSource, string.Empty);
            dataViewLookup = dtData.DefaultView;
            //下面增加不选择的列(不需要增加不选择列，按空格就可以不选择)
            //createNullCheckRow(dataViewLookup);
            //通过下拉框来进行选择的目前需要进行约束，只能接受DataSet 的数据集。
            gridPopup.DataSource = dtData.DefaultView;

            _LookUpDatas = new Dictionary<string, string>();
            for (int i = 0; i < dataViewLookup.Count; i++) {
                _LookUpDatas.Add(dataViewLookup[i][_ValueFieldName].ToString(), dataViewLookup[i][_TextFieldName].ToString());
            }
        }
        #endregion 构造函数...

        #region 扩展的自定义事件相关...
        private XtraRepositoryItemEventHandle _BeforePopupQuery;
        public event XtraRepositoryItemEventHandle BeforePopupQuery {
            add {
                _BeforePopupQuery += value;
            }
            remove {
                _BeforePopupQuery -= value;
            }
        }
        private void onBeforePopupQuery(XtraRepositoryItemEventArg arg) {
            if (_BeforePopupQuery != null)
                _BeforePopupQuery(this, arg);
        }
        private XtraRepositoryItemEventHandle _AfterPopupQueryClose;
        public event XtraRepositoryItemEventHandle AfterPopupQueryClose {
            add {
                _AfterPopupQueryClose += value;
            }
            remove {
                _AfterPopupQueryClose -= value;
            }
        }
        private void onAfterPopupQueryClose(XtraRepositoryItemEventArg arg) {
            if (_AfterPopupQueryClose != null)
                _AfterPopupQueryClose(this, arg);
        }

        #endregion 扩展的自定义事件相关...

        #region 扩展的public 属性...
        /// <summary>
        /// 获取存储的字段名称。
        /// </summary>
        public string ValueFieldName {
            get {
                return _ValueFieldName;
            }
        }
        /// <summary>
        /// 获取文本显示的字段名称。
        /// </summary>
        public string TextFieldName {
            get {
                return _TextFieldName;
            }
        }
        /// <summary>
        /// 设置的数据源。
        /// </summary>
        public object DataSource {
            get {
                return gridPopup.DataSource;
            }
            set {
                gridPopup.DataSource = value;
            }
        }
        public bool AllowEdit {
            get {
                return _AllowEdit;
            }
            set {
                _AllowEdit = value;
            }
        }
        #endregion 扩展的public 属性...

        #region 内部函数处理...
        //判断并增加不选择的空列
        private void createNullCheckRow(object dvData) {
        }
        private string PopupContainerUserByID(object id) {
            try {
                //for (int i = 0; i < dataViewLookup.Count; i++) {
                //    if (id != null && id != System.DBNull.Value) {
                //        if (string.Compare(dataViewLookup[i][_ValueFieldName].ToString(), id.ToString(), true) == 0)
                //            return dataViewLookup[i][_TextFieldName].ToString();
                //    }
                //}
                if (id != null && id != System.DBNull.Value) {
                    if (_LookUpDatas.ContainsKey(id.ToString())) {
                        return _LookUpDatas[id.ToString()];
                    }
                }
            }
            catch {
                if(id!=null)
                    return id.ToString();
            }
            return string.Empty;
        }

        private void PopupContainerFindRowByEditValue() {
            if (popupContainerControl.OwnerEdit == null) return;
            DataRowView row;
            for (int i = 0; i < dataViewLookup.Count; i++) {
                row = dataViewLookup[i];
                if (row.Row[_ValueFieldName].Equals(popupContainerControl.OwnerEdit.EditValue))
                    for (int k = 0; k < gridView1Popup.DataRowCount; k++) {
                        if (gridView1Popup.GetRow(k) == row) {
                            gridView1Popup.FocusedRowHandle = k;
                            return;
                        }
                    }
            }
           // gridView1Popup.FocusedRowHandle = -1;
        }
        private void PopupContainerClosePopup() {
           // if (_IsDoubleClickSelected) {
                if (popupContainerControl.OwnerEdit != null) {
                    popupContainerControl.OwnerEdit.ClosePopup();
                    if (gridView1Popup != null)
                        gridView1Popup.UpdateCurrentRow();
                }
           // }
            //_IsDoubleClickSelected = false;
        }
        #endregion 内部函数处理...

        #region 内部对象事件...
        private void gridView1Popup_DoubleClick(object sender, EventArgs e) {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = gridView1Popup.CalcHitInfo(_MousePosition);//gridPopup.PointToClient(_MousePosition));
            if (info.InRow) {
                
                PopupContainerClosePopup();
            }
        }
        private bool _IsNULLSelected;
        private void gridView1Popup_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
               
                PopupContainerClosePopup();
            }
            else if (e.KeyCode == Keys.Space) {
                _IsNULLSelected = true;
                PopupContainerClosePopup();
                _IsNULLSelected = false;
            }
        }

        private void gridPopup_BindingContextChanged(object sender, EventArgs e) {
            PopupContainerFindRowByEditValue();
        }

        private void gridView1Popup_MouseMove(object sender, MouseEventArgs e) {
            _MousePosition = new Point(e.X, e.Y);
        }

        private void XtraRepositoryItemPopupContainerEdit_QueryPopUp(object sender, CancelEventArgs e) {
            XtraRepositoryItemEventArg arg = new XtraRepositoryItemEventArg();
            onBeforePopupQuery(arg);

            if (arg.Cancel) {
                e.Cancel = true;
                return;
            }
            if (_AllowEdit)
                PopupContainerFindRowByEditValue();
            else
                e.Cancel = true;
        }

        private void XtraRepositoryItemPopupContainerEdit_QueryDisplayText(object sender, DevExpress.XtraEditors.Controls.QueryDisplayTextEventArgs e) {
            if (e.EditValue == System.DBNull.Value)
                e.DisplayText = string.Empty;
            else
                e.DisplayText = PopupContainerUserByID(e.EditValue);
        }

        private void XtraRepositoryItemPopupContainerEdit_QueryResultValue(object sender, DevExpress.XtraEditors.Controls.QueryResultValueEventArgs e) {
            if (this.gridView1Popup.FocusedRowHandle >= 0) {
                if (_IsNULLSelected)
                    e.Value = System.DBNull.Value;
                else
                    e.Value = this.gridView1Popup.GetDataRow(this.gridView1Popup.FocusedRowHandle)[_ValueFieldName];
            }
            
        }
        #endregion 内部对象事件...

        #region 自定义事件处理相关...
        public delegate void XtraRepositoryItemEventHandle(object sender, XtraRepositoryItemEventArg arg);
        public class XtraRepositoryItemEventArg {
            private bool _Cancel;
            public XtraRepositoryItemEventArg() {
            }
            public bool Cancel {
                get {
                    return _Cancel;
                }
                set {
                    _Cancel = value;
                }
            }
        }
        #endregion 自定义事件处理相关...

        private void XtraRepositoryItemPopupContainerEdit_QueryCloseUp(object sender, CancelEventArgs e) {
            DataRow dr = gridView1Popup.GetDataRow(gridView1Popup.FocusedRowHandle);
            onAfterPopupQueryClose(new XtraRepositoryItemEventArg());

            setPopupFocusedRowHandle(dr);
        }
        //特殊处理 保证选择的行。
        private void setPopupFocusedRowHandle(DataRow drData) {
            int rowCount = gridView1Popup.RowCount;
            for (int i = 0; i < rowCount; i++) {
                DataRow dr = gridView1Popup.GetDataRow(i);
                if (dr.Equals(drData)) {
                    gridView1Popup.FocusedRowHandle = i;
                    return;
                }
            }
           
            //gridView1Popup.FocusedRowHandle = -1;
        }
    }
}
