using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.WinBase.Common;
using System.Collections;

namespace MB.XWinLib.Share
{
    /// <summary>
    /// 动态列设置
    /// </summary>
    public partial class FrmDynamicColumnSetting : Form
    {
        #region 自定义变量...
        private MB.WinBase.IFace.IClientRuleQueryBase _ClientRuleObject;

        private static readonly string GRID_FILE_PATH = MB.Util.General.GeApplicationDirectory() + @"GridColumnSetting\";
        private static readonly string GRID_COLUMN_SETTING_FULLNAME = GRID_FILE_PATH + "GridDynamicColumnSetting.xml";

        private DataSet _DsSource = null;
        private DataSet _DsDynamic = null;

        private List<GridDynamicColumnSettingInfo> _Settings;
        private List<string> _DeleteItems = new List<string>();
        #endregion

        public FrmDynamicColumnSetting(MB.WinBase.IFace.IClientRuleQueryBase clientRuleObject)
        {
            InitializeComponent();

            _ClientRuleObject = clientRuleObject;

            bindingEvent();
        }

        private void bindingEvent()
        {
            this.Load += new EventHandler(FrmDynamicColumnSetting_Load);
            this.cmbDynamicTemplate.SelectedIndexChanged += new EventHandler(cmbDynamicTemplate_SelectedIndexChanged);

            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnImport.Click += new EventHandler(btnImport_Click);
            this.lnkDelete.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkDelete_LinkClicked);
            this.lnkNew.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkNew_LinkClicked);
        }

        void lnkNew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.cmbDynamicTemplate.Text))
                {
                    MessageBox.Show("请输入模板名称!");
                    this.cmbDynamicTemplate.Focus();
                    return;
                }
                if (this.cmbDynamicTemplate.Properties.Items.Contains(this.cmbDynamicTemplate.Text))
                {
                    MessageBox.Show("模板名称已存在!");
                    this.cmbDynamicTemplate.Focus();
                    return;
                }

                this.cmbDynamicTemplate.Properties.Items.Add(this.cmbDynamicTemplate.Text);

                resetDynamicColumn();
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        void lnkDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.cmbDynamicTemplate.SelectedIndex < 0)
                {
                    MessageBox.Show("请选择模板!");
                    return;
                }
                if (_Settings.Exists(o => o.Name.Equals(this.cmbDynamicTemplate.Text)))
                {
                    var data = _Settings.Find(o => o.Name.Equals(this.cmbDynamicTemplate.Text));
                    _Settings.Remove(data);
                }
                _DeleteItems.Add(this.cmbDynamicTemplate.Text);

                string delTemplateName = this.cmbDynamicTemplate.Text;
                this.cmbDynamicTemplate.Text = string.Empty;
                this.cmbDynamicTemplate.SelectedIndex = -1;

                this.cmbDynamicTemplate.Properties.Items.Remove(delTemplateName);
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        void FrmDynamicColumnSetting_Load(object sender, EventArgs e)
        {

            //绑定模板信息
            bindDynamicTemplate();

            //初始化数据
            bindingSourceColumns();

        }

        #region private method...
        private void bindingSourceColumns()
        {
            try
            {
                var list = _ClientRuleObject.UIRuleXmlConfigInfo.GetDefaultColumns();

                //过滤出可见的列
                var cols = list.Values.ToList().Where(o => o.Visibled).ToList();
                _DsSource = MB.Util.MyConvert.Instance.ConvertEntityToDataSet<ColumnPropertyInfo>(cols, new string[] { "IsDynamic", "Name", "Description" });

                if (_DsSource == null || _DsSource.Tables.Count == 0) return;

                createGridColumns(_DsSource, gvColumn);

                //loadDynamicColumns();

                mergeColumnsWithDynamic();

                this.gridColumn.DataSource = _DsSource.Tables[0];
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        private void mergeColumnsWithDynamic()
        {
            if (_DsSource == null || _DsSource.Tables.Count == 0 || _DsSource.Tables[0].Rows.Count == 0) return;
            if (_DsDynamic == null || _DsDynamic.Tables.Count == 0 || _DsDynamic.Tables[0].Rows.Count == 0) return;

            foreach (DataRow dr in _DsSource.Tables[0].Rows)
            {
                DataRow[] drs = _DsDynamic.Tables[0].Select("Name = '" + dr["Name"].ToString() + "'");
                if (drs.Length > 0) dr["IsDynamic"] = true;
                else dr["IsDynamic"] = false;
            }
        }

        private void createGridColumns(DataSet ds,DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            gridView.Columns.Clear();
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();
                col.Name = "Xtra" + gridView.Name + dc.ColumnName;
                col.FieldName = dc.ColumnName;
                if (dc.ColumnName.Equals("Name"))
                {
                    col.UnboundType = DevExpress.Data.UnboundColumnType.String;
                    col.Caption = "列名";
                    col.VisibleIndex = 1;
                    col.OptionsColumn.ReadOnly = true;
                }
                else if (dc.ColumnName.Equals("Description"))
                {
                    col.UnboundType = DevExpress.Data.UnboundColumnType.String;
                    col.Caption = "描述";
                    col.VisibleIndex = 2;
                    col.OptionsColumn.ReadOnly = true;
                }
                else if (dc.ColumnName.Equals("IsDynamic"))
                {
                    col.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
                    col.Caption = "是否动态列";
                    col.VisibleIndex = 0;
                    col.OptionsColumn.ReadOnly = false;
                }

                gridView.Columns.Add(col);
            }
        }

        private void bindDynamicTemplate()
        {
            try
            {
                cmbDynamicTemplate.Properties.Items.Clear();

                IList list = DynamicColumnSettingHelper.GetDynamicColumnSettings(_ClientRuleObject);

                _Settings = list as List<GridDynamicColumnSettingInfo>;
                if (_Settings == null || _Settings.Count == 0)
                {
                    _Settings = new List<GridDynamicColumnSettingInfo>();
                    return;
                }

                var orderSettings = _Settings.OrderByDescending(o => o.LastModifyDate);
                foreach (var col in orderSettings)
                {
                    cmbDynamicTemplate.Properties.Items.Add(col.Name);
                }

                cmbDynamicTemplate.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        private void loadDynamicColumns()
        {
            if (string.IsNullOrEmpty(cmbDynamicTemplate.Text)) return;

            //根据模块名称加载动态列数据
            var dynamicColumns = DynamicColumnSettingHelper.GetDynamicColumnByTemplateName(_ClientRuleObject, cmbDynamicTemplate.Text);
            if (dynamicColumns == null)
                throw new MB.Util.APPException(string.Format("获取模板 {0} 动态列失败!", cmbDynamicTemplate.Text), Util.APPMessageType.DisplayToUser);

            _DsDynamic = MB.Util.MyConvert.Instance.ConvertEntityToDataSet<DynamicColumnInfo>(dynamicColumns, new string[] { "Name", "Description" });

            if (_DsDynamic == null || _DsDynamic.Tables.Count == 0)
                throw new MB.Util.APPException(string.Format("动态列实体对象转换失败!"), Util.APPMessageType.DisplayToUser);
        }

        private void resetDynamicColumn()
        {
            foreach (DataRow dr in _DsSource.Tables[0].Rows)
                dr["IsDynamic"] = false;

            this.gridColumn.RefreshDataSource();
        }
        #endregion private mehtod

        #region private event...
        void cmbDynamicTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbDynamicTemplate.Text) && cmbDynamicTemplate.Properties.Items.Contains(cmbDynamicTemplate.Text))
            {
                //根据当前模板加载动态列数据
                loadDynamicColumns();
                mergeColumnsWithDynamic();
            }
            else resetDynamicColumn();
        }

        void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "XML文件 (*.xml)| *.xml|All files (*.*)|*.*";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fullName = dialog.FileName;
                    var data = new MB.Util.Serializer.DataContractFileSerializer<List<DynamicColumnInfo>>(fullName);
                    List<DynamicColumnInfo> list = data.Read();
                    if (list == null || list.Count == 0) throw new MB.Util.APPException("读取文件失败!",Util.APPMessageType.DisplayToUser);

                    _DsDynamic = MB.Util.MyConvert.Instance.ConvertEntityToDataSet<DynamicColumnInfo>(list, new string[] { "Name", "Description" });
                    if (_DsDynamic == null || _DsDynamic.Tables.Count == 0)
                        throw new MB.Util.APPException(string.Format("动态列实体对象转换失败!"), Util.APPMessageType.DisplayToUser);

                    mergeColumnsWithDynamic();

                    this.gridColumn.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //保存当前设置值
                List<DynamicColumnInfo> list = new List<DynamicColumnInfo>();
                if (!string.IsNullOrEmpty(this.cmbDynamicTemplate.Text))
                {
                    DataRow[] drs = _DsSource.Tables[0].Select("IsDynamic = true");
                    if (drs.Length == 0) throw new MB.Util.APPException("请选择动态显示列，动态列数据不能为空！", Util.APPMessageType.DisplayToUser);


                    foreach (DataRow dr in drs)
                    {
                        DynamicColumnInfo col = new DynamicColumnInfo();
                        col = MB.Util.MyReflection.Instance.FillModelObject<DynamicColumnInfo>(col, dr);

                        list.Add(col);
                    }
                }

                string sectionName = _ClientRuleObject.GetType().FullName + " " + _ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile;
                List<GridColumnSettingInfo> gridColumnSettings = new MB.Util.Serializer.DataContractFileSerializer<List<GridColumnSettingInfo>>(GRID_COLUMN_SETTING_FULLNAME).Read();
                if (!string.IsNullOrEmpty(this.cmbDynamicTemplate.Text))
                {
                    GridColumnSettingInfo setting = null; GridDynamicColumnSettingInfo dynSetting = null;
                    if (gridColumnSettings == null)
                    {
                        gridColumnSettings = new List<GridColumnSettingInfo>();
                    }
                    else
                    {
                        setting = gridColumnSettings.Find(o => o.Name.Equals(sectionName));
                        dynSetting = _Settings.Find(o => o.Name.Equals(this.cmbDynamicTemplate.Text.Trim()));
                    }
                    if (dynSetting == null)
                    {
                        dynSetting = new GridDynamicColumnSettingInfo();
                        dynSetting.Name = this.cmbDynamicTemplate.Text.Trim();
                        dynSetting.LastModifyDate = DateTime.Now;

                        _Settings.Add(dynSetting);
                    }
                    else
                    {
                        dynSetting.LastModifyDate = DateTime.Now;
                    }
                    if (setting == null)
                    {
                        setting = new GridColumnSettingInfo();
                        setting.Name = sectionName;

                        gridColumnSettings.Add(setting);
                    }
                    setting.DynamicColumns = _Settings;
                }

                foreach (string name in _DeleteItems)
                {
                    var item = gridColumnSettings.Find(o => o.Name.Equals(sectionName));
                    if (item != null)
                    {
                        var dynamicCol = item.DynamicColumns.Find(o => o.Name.Equals(name));
                        if (dynamicCol != null) item.DynamicColumns.Remove(dynamicCol);
                        if (item.DynamicColumns.Count == 0) gridColumnSettings.Remove(item);
                    }

                    DynamicColumnSettingHelper.DeleteDynamicColumnSetting(_ClientRuleObject, name);
                }

                DynamicColumnSettingHelper.SaveDynamicColumnSettings(_ClientRuleObject, this.cmbDynamicTemplate.Text, list, gridColumnSettings);

                MB.WinBase.MessageBoxEx.Show("保存成功!");

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion private event
    }
}
