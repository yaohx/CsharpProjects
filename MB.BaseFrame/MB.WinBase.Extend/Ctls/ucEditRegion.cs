using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.WinBase.IFace;
using MB.WinBase.Common;
using MB.WinBase.Binding;
using System.Drawing.Design;
using MB.Util;

namespace MB.WinBase.Extend.Ctls
{
    [ToolboxItem(true)]
    public partial class ucEditRegion : UserControl
    {
        private const int BEGIN_LEVEL = 1; //起始级别
        private FrmEditRegion _CurrentEditRegion;
        private RegionEditInfo _CurRegionEditInfo;

        public ucEditRegion()
        {
            InitializeComponent();

            this.bEditRegion.Properties.ReadOnly = true;
            this.bEditRegion.BackColor = Color.White;
            _SelectRegionLevel = BEGIN_LEVEL;
            _CurRegionEditInfo = new RegionEditInfo();
            this.bEditRegion.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(bEditRegion_ButtonClick);
        }

        #region 内部函数处理...
        private void convertDescription()
        {
            if (_CurRegionEditInfo != null)
            {
                if (!string.IsNullOrEmpty(_CurRegionEditInfo.Country)) this.bEditRegion.Text = _CurRegionEditInfo.Country;
                if (!string.IsNullOrEmpty(_CurRegionEditInfo.Province)) this.bEditRegion.Text += " " + _CurRegionEditInfo.Province;
                if (!string.IsNullOrEmpty(_CurRegionEditInfo.City)) this.bEditRegion.Text += " " + _CurRegionEditInfo.City;
                if (!string.IsNullOrEmpty(_CurRegionEditInfo.District)) this.bEditRegion.Text += " " + _CurRegionEditInfo.District;

                if (_BaseDataBindingEdit != null)
                {
                    object currentEditEntity = _BaseDataBindingEdit.CurrentEditEntity;
                    if(Country != null) MyReflection.Instance.InvokePropertyForSet(currentEditEntity, Country.ColumnName, _CurRegionEditInfo.Country);
                    if (Province != null) MyReflection.Instance.InvokePropertyForSet(currentEditEntity, Province.ColumnName, _CurRegionEditInfo.Province);
                    if (City != null) MyReflection.Instance.InvokePropertyForSet(currentEditEntity, City.ColumnName, _CurRegionEditInfo.City);
                    if (District != null) MyReflection.Instance.InvokePropertyForSet(currentEditEntity, District.ColumnName, _CurRegionEditInfo.District);
                }
            }
            else this.bEditRegion.Text = string.Empty;
        }

        #endregion 内部函数处理...

        private IBaseEditForm _BaseDataBindingEdit;
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

        public void InitControlData()
        {
            _BaseDataBindingEdit = getDirectnessBaseEdit(this);

            if (_BaseDataBindingEdit != null)
            {
                RegionEditInfo regionEditInfo = this.CurRegionEditInfo;
                object currentEditEntity = _BaseDataBindingEdit.CurrentEditEntity;

                object country = MyReflection.Instance.InvokePropertyForGet(currentEditEntity, Country.ColumnName);
                if (country != null) regionEditInfo.Country = country.ToString();
                else regionEditInfo.Country = null;

                object province = MyReflection.Instance.InvokePropertyForGet(currentEditEntity, Province.ColumnName);
                if (province != null) regionEditInfo.Province = province.ToString();
                else regionEditInfo.Province = null;

                object city = MyReflection.Instance.InvokePropertyForGet(currentEditEntity, City.ColumnName);
                if (city != null) regionEditInfo.City = city.ToString();
                else regionEditInfo.City = null;

                object district = MyReflection.Instance.InvokePropertyForGet(currentEditEntity, District.ColumnName);
                if (district != null) regionEditInfo.District = district.ToString();
                else regionEditInfo.District = null;

                this.CurRegionEditInfo = regionEditInfo;
            }

            SetCtrlReadOnly();
        }

        private void SetCtrlReadOnly()
        {
            if (MB.Util.General.IsInDesignMode() || _BaseDataBindingEdit.CurrentEditType == ObjectEditType.DesignDemo) return;

            //判断是否存在单据状态属性
            bool exists = MB.WinBase.UIDataEditHelper.Instance.CheckExistsDocState(_BaseDataBindingEdit.CurrentEditEntity);
            if (exists)
            {
                MB.Util.Model.DocState docState = MB.WinBase.UIDataEditHelper.Instance.GetEntityDocState(_BaseDataBindingEdit.CurrentEditEntity);

                this.bEditRegion.Properties.Buttons[0].Enabled = (docState == MB.Util.Model.DocState.Progress);
            }
            if (_BaseDataBindingEdit.CurrentEditType == ObjectEditType.OpenReadOnly)
            {
                this.bEditRegion.Properties.ReadOnly = true;
                this.bEditRegion.Properties.Buttons[0].Enabled = false;
            }
        }
        void bEditRegion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (_CurrentEditRegion == null)
            {
                _CurrentEditRegion = new FrmEditRegion(this);
                _CurrentEditRegion.VisibleChanged += new EventHandler(_CurrentEditFilter_VisibleChanged);
            }
            if (_CurrentEditRegion.Visible)
            {
                _CurrentEditRegion.Visible = false;

            }
            else
            {
                Rectangle rect = this.RectangleToScreen(this.ClientRectangle);
                Point f = new Point(rect.X, rect.Y + this.Height);
                _CurrentEditRegion.Location = f;

                _CurrentEditRegion.ShowFilterForm(this.ParentForm);
                _CurrentEditRegion.BringToFront();


            }
        }
        void _CurrentEditFilter_VisibleChanged(object sender, EventArgs e)
        {
            convertDescription();

            if (this.ParentForm != null)
                this.ParentForm.BringToFront();

        }

        public bool ReadOnly {
            get { return !this.bEditRegion.Enabled; }
            set { this.bEditRegion.Enabled = !value; }
        }

        public string Text
        {
            get { return this.bEditRegion.Text; }
            set { this.bEditRegion.Text = value; }
        }

        public RegionEditInfo CurRegionEditInfo {
            get { return _CurRegionEditInfo; }
            set {
                _CurRegionEditInfo = value;
                convertDescription();
            }
        }

        private int _SelectRegionLevel;
        [Category("RegionProperty")]
        [Description("获取或设置区域选择的级别")]
        public int SelectRegionLevel
        {
            get { return _SelectRegionLevel; }
            set { _SelectRegionLevel = value; }
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
    }

    #region RegionEditType...
    /// <summary>
    /// 
    /// </summary>
    public class RegionEditInfo
    {
        private string _Country;
        private string _Province;
        private string _City;
        private string _District;

        public RegionEditInfo()
        { }
        public RegionEditInfo(string country, string province, string city, string district)
        {
            _Country = country;
            _Province = province;
            _City = city;
            _District = district;
        }

        [Description("国家")]
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        [Description("省份")]
        public string Province
        {
            get { return _Province; }
            set { _Province = value; }
        }

        [Description("城市")]
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        [Description("地区")]
        public string District
        {
            get { return _District; }
            set { _District = value; }
        }
    }

    #endregion RegionEditType...
}
