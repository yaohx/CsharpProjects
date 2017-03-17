using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.WinBase.Common;
using MB.WinBase.IFace;
using System.Data;
using MB.Util;
using System.ComponentModel;
using MB.WinBase.Binding;
using System.Drawing.Design;

namespace MB.WinBase.Extend.Base
{
    public abstract class ucComboxRegionBase : UserControl
    {
        private IBaseEditForm _BaseDataBindingEdit;
        protected virtual void UserControl_Load(object sender, EventArgs e)
        {
            this.cbCountry.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbProvince.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbCity.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbDistrict.DropDownStyle = ComboBoxStyle.DropDownList;

            this.cbCountry.DropDown += new EventHandler(cbCountry_DropDown);
            this.cbProvince.DropDown += new EventHandler(cbProvince_DropDown);
            this.cbCity.DropDown += new EventHandler(cbCity_DropDown);
            this.cbDistrict.DropDown += new EventHandler(cbDistrict_DropDown);

            this.cbCountry.SelectionChangeCommitted += new EventHandler(cbCountry_SelectionChangeCommitted);
            this.cbProvince.SelectionChangeCommitted += new EventHandler(cbProvince_SelectionChangeCommitted);
            this.cbCity.SelectionChangeCommitted += new EventHandler(cbCity_SelectionChangeCommitted);
            this.cbDistrict.SelectionChangeCommitted += new EventHandler(cbDistrict_SelectionChangeCommitted);

            this.cbCountry.SelectedValueChanged += new EventHandler(cbCountry_SelectedValueChanged);
            this.cbProvince.SelectedValueChanged += new EventHandler(cbProvince_SelectedValueChanged);
            this.cbCity.SelectedValueChanged += new EventHandler(cbCity_SelectedValueChanged);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        #region Set Values...
        void cbDistrict_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (_BaseDataBindingEdit != null)
            {
                object currentEditEntity = _BaseDataBindingEdit.CurrentEditEntity;

                MyReflection.Instance.InvokePropertyForSet(currentEditEntity, District.ColumnName, this.cbDistrict.SelectedValue);
            }
        }

        void cbCity_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (_BaseDataBindingEdit != null)
            {
                object currentEditEntity = _BaseDataBindingEdit.CurrentEditEntity;

                MyReflection.Instance.InvokePropertyForSet(currentEditEntity, City.ColumnName, this.cbCity.SelectedValue);
                _BaseDataBindingEdit.BindingSource.RaiseListChangedEvents = true;
            }
        }

        void cbProvince_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (_BaseDataBindingEdit != null)
            {
                object currentEditEntity = _BaseDataBindingEdit.CurrentEditEntity;

                MyReflection.Instance.InvokePropertyForSet(currentEditEntity, Province.ColumnName, this.cbProvince.SelectedValue);
            }
        }

        void cbCountry_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (_BaseDataBindingEdit != null)
            {
                object currentEditEntity = _BaseDataBindingEdit.CurrentEditEntity;

                MyReflection.Instance.InvokePropertyForSet(currentEditEntity, Country.ColumnName, this.cbCountry.SelectedValue);
            }
        }
        #endregion

        #region init control data...
        public void InitControlBinding()
        {
            IBaseEditForm parentForm = this.getDirectnessBaseEdit(this);
            if (parentForm == null)
            {
                parentForm = base.ParentForm as IBaseEditForm;
            }
            _BaseDataBindingEdit = parentForm;

            if (Country == null || Province == null || City == null || District == null)
            {
                MB.Util.TraceEx.Write("控件的[DesignColumnXmlCfgInfo]数据绑定为空，请检查UI控件属性!");
                throw new MB.Util.APPException("控件的[DesignColumnXmlCfgInfo]数据绑定为空，请检查UI控件属性!", APPMessageType.DisplayToUser);
            }

            if (_BaseDataBindingEdit != null)
            {
                //控件初始化值
                SetControlData();

                //设置控件的初始化状态
                SetCtrlReadOnly();
            }

            this.lbCountry.Text = Country.ColumnDescription + ":";
            this.lbCity.Text = City.ColumnDescription + ":";
            this.lbProvince.Text = Province.ColumnDescription + ":";
            this.lbDistrict.Text = District.ColumnDescription + ":";
        }

        private void SetControlData()
        {
            object currentEditEntity = _BaseDataBindingEdit.CurrentEditEntity;

            this.cbCountry_DropDown(cbCountry, null);
            object country = MyReflection.Instance.InvokePropertyForGet(currentEditEntity, Country.ColumnName);
            if (country != null) this.cbCountry.SelectedValue = country;
            else this.cbCountry.SelectedIndex = -1;

            this.cbProvince_DropDown(cbProvince, null);
            object province = MyReflection.Instance.InvokePropertyForGet(currentEditEntity, Province.ColumnName);
            if (province != null) this.cbProvince.SelectedValue = province;
            else this.cbProvince.SelectedIndex = -1;

            this.cbCity_DropDown(cbCity, null);
            object city = MyReflection.Instance.InvokePropertyForGet(currentEditEntity, City.ColumnName);
            if (city != null) this.cbCity.SelectedValue = city;
            else this.cbCity.SelectedIndex = -1;

            this.cbDistrict_DropDown(cbDistrict, null);
            object district = MyReflection.Instance.InvokePropertyForGet(currentEditEntity, District.ColumnName);
            if (district != null) this.cbDistrict.SelectedValue = district;
            else this.cbDistrict.SelectedIndex = -1;
        }
        private IBaseEditForm getDirectnessBaseEdit(Control ctl)
        {
            if (ctl.Parent == null)
            {
                return null;
            }
            IBaseEditForm parent = ctl.Parent as IBaseEditForm;
            if (parent != null)
            {
                return parent;
            }
            return this.getDirectnessBaseEdit(ctl.Parent);
        }

        private void SetCtrlReadOnly()
        {
            if (MB.Util.General.IsInDesignMode() || _BaseDataBindingEdit.CurrentEditType == ObjectEditType.DesignDemo) return;

            //判断是否存在单据状态属性
            bool exists = MB.WinBase.UIDataEditHelper.Instance.CheckExistsDocState(_BaseDataBindingEdit.CurrentEditEntity);
            if (exists)
            {
                MB.Util.Model.DocState docState = MB.WinBase.UIDataEditHelper.Instance.GetEntityDocState(_BaseDataBindingEdit.CurrentEditEntity);

                this.ReadOnly = (docState != MB.Util.Model.DocState.Progress);
            }
            if (_BaseDataBindingEdit.CurrentEditType == ObjectEditType.OpenReadOnly) {
                this.ReadOnly = true;
            }
        }
        #endregion

        #region private Event
        void cbCity_SelectedValueChanged(object sender, EventArgs e)
        {
            this.cbDistrict.SelectedIndex = -1;
            cbDistrict.DataSource = null;
        }

        void cbProvince_SelectedValueChanged(object sender, EventArgs e)
        {
            this.cbCity.SelectedIndex = -1;
            cbCity.DataSource = null;
        }

        void cbCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            this.cbProvince.SelectedIndex = -1;
            cbProvince.DataSource = null;
        }

        void cbCountry_DropDown(object sender, EventArgs e)
        {
            if (cbCountry.DataSource == null)
            {
                SetComBoxBinding((ComboBox)sender, CountrySource);
            }
        }

        void cbDistrict_DropDown(object sender, EventArgs e)
        {
            if (cbDistrict.DataSource == null && cbCity.SelectedValue != null)
            {
                SetComBoxBinding((ComboBox)sender, DistrictSource);
            }
        }

        void cbCity_DropDown(object sender, EventArgs e)
        {
            if (cbCity.DataSource == null && cbProvince.SelectedValue != null)
            {
                SetComBoxBinding((ComboBox)sender, CitySource);
            }
        }

        void cbProvince_DropDown(object sender, EventArgs e)
        {
            if (cbProvince.DataSource == null && cbCountry.SelectedValue != null)
            {
                SetComBoxBinding((ComboBox)sender, ProvinceSource);
            }
        }

        private void SetComBoxBinding(ComboBox combBox, InvokeDataSourceDescInfo invokeDataSourceDescInfo)
        {
            string[] arrType = invokeDataSourceDescInfo.Type.Split(new char[] { ',' });
            string[] arrMethod = invokeDataSourceDescInfo.Method.Split(new char[] { ',' });
            string[] arrParams = null;
            if (arrMethod.Length > 1) arrParams = arrMethod[1].Split(new char[] { ';' });
            IClientRuleQueryBase clientRule = (IClientRuleQueryBase)MB.Util.DllFactory.Instance.LoadObject(arrType[0], arrType[1]);
            var data = MyReflection.Instance.InvokeMethod(clientRule, arrMethod[0], arrParams);
            DataSet ds = ((DataSet)data).Copy();
            combBox.ValueMember = "CODE";
            combBox.DisplayMember = "NAME";
            combBox.DataSource = ds.Tables[0].DefaultView;
        }

        #endregion

        #region Public Property...
        [Description("获取或者设置控件是否允许编辑")]
        public bool ReadOnly
        {
            get
            {
                return !(this.cbCountry.Enabled && this.cbProvince.Enabled && this.cbCity.Enabled && this.cbDistrict.Enabled);
            }
            set
            {
                this.cbCountry.Enabled = !value;
                this.cbProvince.Enabled = !value;
                this.cbCity.Enabled = !value;
                this.cbDistrict.Enabled = !value;
            }
        }

        [Category("数据绑定")]
        [Editor(typeof(MyDataBindingProvider.DataBindingProviderDesign), typeof(UITypeEditor))]
        [Description("设置控件绑定的国家。")]
        public DesignColumnXmlCfgInfo Country { get; set; }

        [Category("数据绑定")]
        [Editor(typeof(MyDataBindingProvider.DataBindingProviderDesign), typeof(UITypeEditor))]
        [Description("设置控件绑定的城市。")]
        public DesignColumnXmlCfgInfo City { get; set; }

        [Category("数据绑定")]
        [Editor(typeof(MyDataBindingProvider.DataBindingProviderDesign), typeof(UITypeEditor))]
        [Description("设置控件绑定的省份。")]
        public DesignColumnXmlCfgInfo Province { get; set; }

        [Category("数据绑定")]
        [Editor(typeof(MyDataBindingProvider.DataBindingProviderDesign), typeof(UITypeEditor))]
        [Description("设置控件绑定的区域。")]
        public DesignColumnXmlCfgInfo District { get; set; }
        #endregion

        #region private property
        private InvokeDataSourceDescInfo _countrySource = null;
        private InvokeDataSourceDescInfo CountrySource
        {
            get
            {
                if (_countrySource == null)
                {
                    InvokeDataSourceDescInfo s = new InvokeDataSourceDescInfo();
                    s.Type = "MB.ERP.BaseLibrary.CSysSetting.UIRule.BfRegionUIRule,MB.ERP.BaseLibrary.CSysSetting.dll";
                    s.Method = "GetRegionInfos,-1;1";
                    _countrySource = s;
                }

                return _countrySource;
            }
        }

        private InvokeDataSourceDescInfo _provinceSource = null;
        private InvokeDataSourceDescInfo ProvinceSource
        {
            get
            {
                if (this.cbCountry.SelectedItem == null) throw new MB.Util.APPException("请先选择国家!", APPMessageType.DisplayToUser);
                if (_provinceSource == null)
                {
                    InvokeDataSourceDescInfo s = new InvokeDataSourceDescInfo();
                    s.Type = "MB.ERP.BaseLibrary.CSysSetting.UIRule.BfRegionUIRule,MB.ERP.BaseLibrary.CSysSetting.dll";

                    s.Method = "GetRegionInfos," + ((DataRowView)this.cbCountry.SelectedItem)["ID"].ToString() + ";2";
                    _provinceSource = s;
                }
                else
                {
                    _provinceSource.Method = "GetRegionInfos," + ((DataRowView)this.cbCountry.SelectedItem)["ID"].ToString() + ";2";
                }
                return _provinceSource;
            }
        }

        private InvokeDataSourceDescInfo _citySource = null;
        private InvokeDataSourceDescInfo CitySource
        {
            get
            {
                if (this.cbProvince.SelectedItem == null) throw new MB.Util.APPException("请先选择省份!", APPMessageType.DisplayToUser);
                if (_citySource == null)
                {
                    InvokeDataSourceDescInfo s = new InvokeDataSourceDescInfo();
                    s.Type = "MB.ERP.BaseLibrary.CSysSetting.UIRule.BfRegionUIRule,MB.ERP.BaseLibrary.CSysSetting.dll";
                    s.Method = "GetRegionInfos," + ((DataRowView)this.cbProvince.SelectedItem)["ID"].ToString() + ";3";
                    _citySource = s;
                }
                else
                {
                    _citySource.Method = "GetRegionInfos," + ((DataRowView)this.cbProvince.SelectedItem)["ID"].ToString() + ";3";
                }

                return _citySource;
            }
        }

        private InvokeDataSourceDescInfo _districtSource = null;

        private InvokeDataSourceDescInfo DistrictSource
        {
            get
            {
                if (this.cbCity.SelectedItem == null) throw new MB.Util.APPException("请先选择城市!", APPMessageType.DisplayToUser);

                if (_districtSource == null)
                {
                    InvokeDataSourceDescInfo s = new InvokeDataSourceDescInfo();
                    s.Type = "MB.ERP.BaseLibrary.CSysSetting.UIRule.BfRegionUIRule,MB.ERP.BaseLibrary.CSysSetting.dll";
                    s.Method = "GetRegionInfos," + ((DataRowView)this.cbCity.SelectedItem)["ID"].ToString() + ";4";
                    _districtSource = s;
                }
                else
                {
                    _districtSource.Method = "GetRegionInfos," + ((DataRowView)this.cbCity.SelectedItem)["ID"].ToString() + ";4";
                }

                return _districtSource;
            }
        }
        #endregion

        #region private components define...
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        protected System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        protected virtual void InitializeComponent()
        {

        }

        #endregion

        protected Label lbCountry;
        protected System.Windows.Forms.Label lbProvince;
        protected System.Windows.Forms.Label lbCity;
        protected System.Windows.Forms.Label lbDistrict;
        protected MB.WinBase.Design.MyTableLayoutPanelProvider myTableLayoutPanelProvider1;
        protected MB.WinBase.Binding.MyDataBindingProvider myDataBindingProvider1;
        protected System.Windows.Forms.ComboBox cbCountry;
        protected System.Windows.Forms.ComboBox cbProvince;
        protected System.Windows.Forms.ComboBox cbCity;
        protected System.Windows.Forms.ComboBox cbDistrict;
        #endregion
    }
}
