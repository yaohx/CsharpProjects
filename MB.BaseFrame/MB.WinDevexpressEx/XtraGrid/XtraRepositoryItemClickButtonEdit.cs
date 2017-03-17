using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
using MB.Util;
using MB.WinBase.IFace;
using System.Data;

namespace MB.XWinLib.XtraGrid {
    /// <summary>
    /// 
    /// </summary>
    [UserRepositoryItem("Register")]
    public class XtraRepositoryItemClickButtonEdit : RepositoryItemButtonEdit {
        internal const string EditorName = "XtraClickButtonEdit";
        private MB.WinBase.Common.ColumnEditCfgInfo _ColumnEditCfgInfo;

        static XtraRepositoryItemClickButtonEdit() {
            Register();
        }
        public XtraRepositoryItemClickButtonEdit() {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnEditCfgInfo"></param>
        public XtraRepositoryItemClickButtonEdit(MB.WinBase.Common.ColumnEditCfgInfo columnEditCfgInfo) {
            _ColumnEditCfgInfo = columnEditCfgInfo;
        }

        public static void Register() {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(XtraClickButtonEdit),
               null, typeof(DevExpress.XtraEditors.ViewInfo.ButtonEditViewInfo),
               new DevExpress.XtraEditors.Drawing.ButtonEditPainter(), true, null));
        }
        public override string EditorTypeName {
            get {
                return EditorName;
            }
        }

        public override BaseEdit CreateEditor() {
            BaseEdit edit = base.CreateEditor();
            (edit as XtraClickButtonEdit).ColumnEditCfgInfo = _ColumnEditCfgInfo;
            return edit;
        }
        //主要通过反射的方式创建一个新的 RepositoryItem（如 在该控件对应的查询窗口中）
        //通过覆盖它避免反射创建不成功 以及需要的参数为空
        protected override RepositoryItem CreateRepositoryItem() {
            //try {
            //    return base.CreateRepositoryItem();
            //}
            //catch {
                return new XtraRepositoryItemClickButtonEdit(_ColumnEditCfgInfo);
           // }
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)] 
    public class XtraClickButtonEdit : DevExpress.XtraEditors.ButtonEdit {
        private Dictionary<int, string> _HasLoadData; 
        private MB.WinBase.Common.ColumnEditCfgInfo _ColumnEditCfgInfo;
        private MB.WinBase.IFace.IDataAssistant _FrmDataAssistant;
        private MB.WinBase.IFace.IClientRuleQueryBase _ClientQueryRule;
        private IBindingList _BindingList; 
        private object _AddedMainEntity;//临时被新增的对象，如果用户关闭窗口的话，这个对象要删除

        public XtraClickButtonEdit() {
            _HasLoadData = new Dictionary<int, string>();
            
        }

        public MB.WinBase.Common.ColumnEditCfgInfo ColumnEditCfgInfo {
            get {
                return _ColumnEditCfgInfo;
            }
            set {
                _ColumnEditCfgInfo = value;
            }
        }
        public MB.WinBase.IFace.IClientRuleQueryBase ClientQueryRule {
            get {
                return _ClientQueryRule;
            }
            set {
                _ClientQueryRule = value;
            }
        }
        protected override void OnClickButton(DevExpress.XtraEditors.Drawing.EditorButtonObjectInfoArgs buttonInfo) {
            //throw new Exception();
            Control ctl = this.Parent;
            DevExpress.XtraGrid.GridControl grdCtl = ctl as DevExpress.XtraGrid.GridControl;
            if (grdCtl != null) {
                IBindingList bindingList = grdCtl.DataSource as IBindingList;
                _BindingList = bindingList;
                if (bindingList != null && !bindingList.AllowEdit) {
                    return;
                }
            }
            ShowPopupForm();
            base.OnClickButton(buttonInfo);
        }
        protected override void OnLeave(EventArgs e) {
            try {
                checkAndFilterData();
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                return;
            }

            base.OnLeave(e);
        }
        protected override void OnKeyDown(KeyEventArgs e) {
            //Keys allowFilterKeys = Keys.Enter | Keys.Down | Keys.Right | Keys.Up | Keys.Left;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down || e.KeyCode == Keys.Up
                || e.KeyCode == Keys.Right || e.KeyCode == Keys.Left || e.KeyCode == Keys.Tab) {
                try {
                    checkAndFilterData();
                }
                catch (Exception ex) {
                    MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
                    return;
                }
            }
            base.OnKeyDown(e);
        }
 
        protected override void OnValidated(EventArgs e) {
            Control ctl = this.Parent;
            DevExpress.XtraGrid.GridControl grdCtl = ctl as DevExpress.XtraGrid.GridControl;
            //因为在客户自定义查询界面上对应的父控件不是  DevExpress.XtraGrid.GridControl
            if (grdCtl != null) {
                DevExpress.XtraGrid.Views.Grid.GridView gridView = grdCtl.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
                object focusedRow = gridView.GetRow(gridView.FocusedRowHandle);
                int rowHasCode = focusedRow.GetHashCode();

                if (!_HasLoadData.ContainsKey(rowHasCode)) {
                    this.Text = string.Empty;
                }
                else {
                    if (!string.IsNullOrEmpty(this.Text) && string.Compare(this.Text, _HasLoadData[rowHasCode], true) != 0)
                        this.Text = _HasLoadData[rowHasCode];

                }
            }
            base.OnValidated(e);
        }

        protected virtual void ShowPopupForm() {
            showFilterDialog();
        }

        private int checkAndFilterData() {
            Control ctl = this.Parent;
            DevExpress.XtraGrid.GridControl grdCtl = ctl as DevExpress.XtraGrid.GridControl;

            if (grdCtl != null) {
                IBindingList bindingList = grdCtl.DataSource as IBindingList;
                _BindingList = bindingList;
                if (bindingList != null && !bindingList.AllowEdit) {
                    return 1;
                }


                if (_ColumnEditCfgInfo != null && !string.IsNullOrEmpty(this.Text)) {
                    if (_ColumnEditCfgInfo.CharacterCasing == CharacterCasing.Upper) {
                        this.Text = this.Text.ToUpper();
                    }
                    else if (_ColumnEditCfgInfo.CharacterCasing == CharacterCasing.Lower) {
                        this.Text = this.Text.ToLower();
                    }
                }
                DevExpress.XtraGrid.Views.Grid.GridView gridView = grdCtl.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;

                object mainEntity = gridView.GetRow(gridView.FocusedRowHandle);
                if (mainEntity != null) {
                    int entityHasCode = mainEntity.GetHashCode();
                    if (string.IsNullOrEmpty(this.Text)) {
                        setMappingColumnNullValue();
                    }
                    else {
                        //MessageBox.Show("查找功能目前还没有实现！");
                        if (!_HasLoadData.ContainsKey(entityHasCode) || string.Compare(this.Text, _HasLoadData[entityHasCode], true) != 0) {
                            List<MB.Util.Model.QueryParameterInfo> lst = new List<MB.Util.Model.QueryParameterInfo>();
                            lst.Add(new MB.Util.Model.QueryParameterInfo(_ColumnEditCfgInfo.TextFieldName, this.Text, MB.Util.DataFilterConditions.Equal));

                            filterObjectsByInput(lst.ToArray());

                        }
                    }
                }
            }
            return 1;
        }

        private void showFilterDialog() {
            try
            {
                if (createFilterObject()) {
                    DialogResult result = (_FrmDataAssistant as Form).ShowDialog();
                    if (result != DialogResult.OK) {
                        if (_AddedMainEntity != null) {
                            _BindingList.Remove(_AddedMainEntity);
                            bool exists = MB.WinBase.UIDataEditHelper.Instance.CheckExistsEntityState(_AddedMainEntity);
                            if (exists) MB.WinBase.UIDataEditHelper.Instance.SetEntityState(_AddedMainEntity, Util.Model.EntityState.Deleted);
                            _AddedMainEntity = null;
                        }
                    }
                        
                }
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        void assistant_AfterGetObjectData(object sender, MB.WinBase.IFace.GetObjectDataAssistantEventArgs arg) {
            if (arg.SelectedRows == null && arg.SelectedRows.Length == 0) return;

            try {
                setSelectValue(arg.SelectedRows,false);
            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write("获取数据后设置控件值出错，请检查对应的EditCtlDataMappings 配置是否有误！" + ex.Message);
                MB.WinBase.MessageBoxEx.Show("获取数据后设置控件值出错，请检查对应的EditCtlDataMappings 配置是否有误！");
            }

        }
        protected override void EndUpdate() {
            base.EndUpdate();

        }
        private bool  createFilterObject() {
            System.Windows.Forms.Control parentHoster = MB.WinBase.ShareLib.Instance.GetInvokeDataHosterControl(this.Parent);
            if (parentHoster != null)
            {
                // System.Windows.Forms.Form parentForm = MB.WinBase.ShareLib.Instance.GetControlParentForm(this.Parent);
                _FrmDataAssistant = MB.WinBase.ObjectDataFilterAssistantHelper.Instance.CreateDataAssistantObject(this, _ColumnEditCfgInfo, parentHoster, ref _ClientQueryRule);
            }
            else
            {
                Control ctl = this.Parent;
                DevExpress.XtraGrid.GridControl grdCtl = ctl as DevExpress.XtraGrid.GridControl;
                object mainEntity = null;
                if (grdCtl != null)
                {
                    DevExpress.XtraGrid.Views.Grid.GridView gridView = grdCtl.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;

                    mainEntity = gridView.GetRow(gridView.FocusedRowHandle);
                    if (mainEntity == null) {
                        IBindingList bindingList = grdCtl.DataSource as IBindingList;
                        mainEntity = bindingList.AddNew();
                        _AddedMainEntity = mainEntity;
                    }

                    //if (gridView.FocusedRowHandle < 0)
                    //{
                    //    //通过这种变态的方法自动增加一个空行，同时保证不重复增加
                    //    this.Text = "0";
                    //    this.Text = string.Empty;
                    //}
                    //mainEntity = gridView.GetFocusedRow();
                }

                parentHoster = MB.WinBase.ShareLib.Instance.GetControlParentForm(this.Parent);
                IForm frm = parentHoster as IForm;
                if (frm != null)
                {
                    _ClientQueryRule = frm.ClientRuleObject;

                    _FrmDataAssistant = MB.WinBase.ObjectDataFilterAssistantHelper.Instance.CreateDataAssistantObject(this, mainEntity, _ColumnEditCfgInfo, _ClientQueryRule);
                }
            }
            if (_FrmDataAssistant != null)
            {
                _FrmDataAssistant.MultiSelect = false;
                _FrmDataAssistant.MaxSelectRows = _ColumnEditCfgInfo.MaxSelectRows;
                _FrmDataAssistant.AfterGetObjectData += new MB.WinBase.IFace.GetObjectDataAssistantEventHandle(assistant_AfterGetObjectData);
            }
            return _FrmDataAssistant != null;          
        }

        private   bool filterObjectsByInput(MB.Util.Model.QueryParameterInfo[] filterParameters) {
            if (createFilterObject()) {
                int docType = _ClientQueryRule.MainDataTypeInDoc != null ? (int)_ClientQueryRule.MainDataTypeInDoc : 0;
                List<MB.Util.Model.QueryParameterInfo> filterParamaters = new List<MB.Util.Model.QueryParameterInfo>(filterParameters);
                var lstData = _FrmDataAssistant.GetFilterObjects(docType, filterParamaters);
                if (lstData != null && lstData.Count > 0) {
                    if (lstData.Count == 1)
                        return setSelectValue(new object[] { lstData[0] }, true);
                    else {
                        _FrmDataAssistant.HideFilterPane = true;
                        _FrmDataAssistant.FilterParametersIfNoFiterPanel = filterParamaters;
                        (_FrmDataAssistant as Form).ShowDialog();
                    }
                }
                else {
                    
                    if (_ColumnEditCfgInfo.ClickButtonShowForm != null &&
                        _ColumnEditCfgInfo.ClickButtonShowForm.ShowMessageOnValidated) {
                        throw new MB.Util.APPException("输入有误,请重新输入!", Util.APPMessageType.DisplayToUser);
                    }
                }
            }
            return false;
        }
        private string getSourceNameByTextField(string textFieldName) {
            foreach (var info in _ColumnEditCfgInfo.EditCtlDataMappings) {
                if (info.ColumnName == textFieldName) {
                    return info.SourceColumnName;
                }
            }
            return textFieldName;
        }
        private bool setSelectValue(object[] rows,bool isKeyDown) {
            Control ctl = this.Parent;

            if (ctl == null || rows.Length ==0)  return false;

            DevExpress.XtraGrid.GridControl grdCtl = ctl as DevExpress.XtraGrid.GridControl;
            if (grdCtl == null) {
                string sourceName = getSourceNameByTextField(_ColumnEditCfgInfo.TextFieldName);
                if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(rows[0], sourceName))
                    throw new MB.Util.APPException(string.Format("在赋值时类型 {0} 中不包含属性 {1} ,请检查对应的配置 {2} 是否正确!", rows[0].GetType().Name, sourceName, _ColumnEditCfgInfo.Name), Util.APPMessageType.SysErrInfo);
                object stxt = MB.Util.MyReflection.Instance.InvokePropertyForGet(rows[0], sourceName);

                this.Text = stxt.ToString();
                return false;
            }

            DevExpress.XtraGrid.Views.Grid.GridView gridView = grdCtl.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;

            if (gridView.FocusedRowHandle < 0) {
                if (!isKeyDown) {
                    //通过这种变态的方法自动增加一个空行，同时保证不重复增加
                    this.Text = "0";
                    this.Text = string.Empty;
                }
            }

            object mainEntity = gridView.GetRow(gridView.FocusedRowHandle);
            if (mainEntity == null)
                throw new MB.Util.APPException("创建新对象为空,请检查 该绑定集合的Addnew是否设置为False", APPMessageType.SysErrInfo);

            if (string.Compare(mainEntity.GetType().Name, "DataRowView", true) == 0) {
                System.Data.DataRowView drRow = mainEntity as System.Data.DataRowView;
                foreach (MB.WinBase.Common.EditCtlDataMappingInfo info in _ColumnEditCfgInfo.EditCtlDataMappings) {
                    object dataObject = rows[0];
                    if (!drRow.Row.Table.Columns.Contains(info.ColumnName)) {
                        MB.Util.TraceEx.Write(string.Format("在根据 XML 配置文件中 EditCtlDataMappings 数值行值时,发现配置值{0} 在指定的数据源中不存在",info.ColumnName));
                        continue;
                    }
  
                    object val = null;
                    if(!string.IsNullOrEmpty(info.SourceColumnName))
                        val = MB.Util.MyReflection.Instance.InvokePropertyForGet(rows[0], info.SourceColumnName);
                    if (val != null) {
                        if (drRow[info.ColumnName] == null || drRow[info.ColumnName] == System.DBNull.Value || string.Compare(val.ToString(), drRow[info.ColumnName].ToString(), true) != 0)
                            drRow[info.ColumnName] = val;
                    }
                    else {
                        drRow[info.ColumnName] = System.DBNull.Value;
                    }
                }
                gridView.RefreshData(); 
            }
            else {
                foreach (MB.WinBase.Common.EditCtlDataMappingInfo info in _ColumnEditCfgInfo.EditCtlDataMappings) {
                    object dataObject = rows[0];

                    if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(rows[0], info.SourceColumnName)) {
                        MB.Util.TraceEx.Write(string.Format("源数据中不包含属性{0}",info.SourceColumnName)); 
                        continue; 
                    }
                    object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(rows[0], info.SourceColumnName);
                    if (val != null) {
                        MB.Util.MyReflection.Instance.InvokePropertyForSet(mainEntity, info.ColumnName, val.ToString());
                    }
                    else
                        MB.Util.MyReflection.Instance.InvokePropertyForSet(mainEntity, info.ColumnName, null);

                }
       
            }
            object txt = MB.Util.MyReflection.Instance.InvokePropertyForGet(rows[0], _ColumnEditCfgInfo.TextFieldName);

            this.Text = txt.ToString();
            int entityHasCode = mainEntity.GetHashCode();
            if (!_HasLoadData.ContainsKey(entityHasCode))
                _HasLoadData.Add(entityHasCode, this.Text);
            else
                _HasLoadData[entityHasCode] = this.Text;

            return true;
        }

        //设置映射例的空值
        private void setMappingColumnNullValue() {

            Control ctl = this.Parent;
            DevExpress.XtraGrid.GridControl grdCtl = ctl as DevExpress.XtraGrid.GridControl;
            if (grdCtl == null) return;
            DevExpress.XtraGrid.Views.Grid.GridView gridView = grdCtl.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;

            if (gridView.FocusedRowHandle < 0) return;

            object mainEntity = gridView.GetRow(gridView.FocusedRowHandle);

            bool isEntity = string.Compare(mainEntity.GetType().Name, "DataRowView", true) != 0;

            foreach (MB.WinBase.Common.EditCtlDataMappingInfo info in _ColumnEditCfgInfo.EditCtlDataMappings) {
                if (isEntity) {

                    if (MB.Util.MyReflection.Instance.CheckObjectExistsProperty(mainEntity, info.ColumnName))
                        MB.Util.MyReflection.Instance.InvokePropertyForSet(mainEntity, info.ColumnName, null);
                }
                else {
                    System.Data.DataRowView drRow = mainEntity as System.Data.DataRowView;
                    DataColumn dc = drRow.DataView.Table.Columns[info.ColumnName];
                    if (dc != null && dc.AllowDBNull) {
                        drRow[info.ColumnName] = System.DBNull.Value;
                    }
                }
            }
            this.Text = string.Empty;
        }
    }
}
