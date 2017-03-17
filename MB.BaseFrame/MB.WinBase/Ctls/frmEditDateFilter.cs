using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;

namespace MB.WinBase.Ctls
{
    public partial class frmEditDateFilter : DevExpress.XtraEditors.XtraForm
    {
        private ucEditDateFilter _UcDate;
        private Dictionary<RadioButton, ucEditDateFilter.DateFilterEditType> _BindingRadAndType;
        private Dictionary<int, ucEditDateFilter.DateFilterEditType> _BindingFixedValue;
        private int _CurIndex;

        private int _DeactivateCount;
        public frmEditDateFilter()
        {
            InitializeComponent();
        }
        public frmEditDateFilter(ucEditDateFilter ucDate)
        {
            InitializeComponent();

            _UcDate = ucDate;
            _BindingRadAndType = new Dictionary<RadioButton, ucEditDateFilter.DateFilterEditType>();
            _BindingRadAndType.Add(radNone, ucEditDateFilter.DateFilterEditType.None);
            _BindingRadAndType.Add(radValue, ucEditDateFilter.DateFilterEditType.Today);
            _BindingRadAndType.Add(radOther, ucEditDateFilter.DateFilterEditType.Other);

            _BindingFixedValue = new Dictionary<int, ucEditDateFilter.DateFilterEditType>();
            _BindingFixedValue.Add(0, ucEditDateFilter.DateFilterEditType.LastDay);
            _BindingFixedValue.Add(1, ucEditDateFilter.DateFilterEditType.Today);
            _BindingFixedValue.Add(2, ucEditDateFilter.DateFilterEditType.Week);
            _BindingFixedValue.Add(3, ucEditDateFilter.DateFilterEditType.Month);
            _BindingFixedValue.Add(4, ucEditDateFilter.DateFilterEditType.Year);

            dTimeBegin.DateTime = DateTime.Now;
            dTimeEnd.DateTime = DateTime.Now;

            this.Load += new EventHandler(frmEditDateFilter_Load);
        }
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="parent"></param>
        public void ShowFilterForm(IWin32Window parent)
        {
            this.Show(parent);

        }
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            timer1.Enabled = true;

        }

        void frmEditDateFilter_Load(object sender, EventArgs e)
        {
            if (MB.Util.General.IsInDesignMode()) return;

            // refreshUserSetting();

            bool exist = false;
            foreach (RadioButton button in _BindingRadAndType.Keys)
            {
                if (_BindingRadAndType[button] == _UcDate.DateFilterType)
                {
                    button.Checked = true;
                    exist = true;
                    break;
                }
            }

            if (!exist || _UcDate.DateFilterType == ucEditDateFilter.DateFilterEditType.Today)
            {
                getDescription(_UcDate.DateFilterType);
                radValue.Checked = true;

                foreach (int index in _BindingFixedValue.Keys)
                {
                    if (_BindingFixedValue[index] == _UcDate.DateFilterType)
                    {
                        _CurIndex = index;
                        break;
                    }
                }
            }

            //add by aifang 2012-03-28 begin
            dTimeBegin.Properties.DisplayFormat.FormatString = _UcDate.Formate;
            dTimeBegin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            dTimeBegin.Properties.EditMask = _UcDate.Formate;
            dTimeBegin.Properties.EditFormat.FormatString = _UcDate.Formate;
            dTimeBegin.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;

            dTimeEnd.Properties.DisplayFormat.FormatString = _UcDate.Formate;
            dTimeEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            dTimeEnd.Properties.EditMask = _UcDate.Formate;
            dTimeEnd.Properties.EditFormat.FormatString = _UcDate.Formate;
            dTimeEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //end

            if (_UcDate.DateFilterType == ucEditDateFilter.DateFilterEditType.Other)
            {

                dTimeBegin.DateTime = _UcDate.DateFilterValue.BeginDate;
                dTimeEnd.DateTime = _UcDate.DateFilterValue.EndDate;
            }
            radNone_CheckedChanged(null, null);
        }

        private void getDescription(ucEditDateFilter.DateFilterEditType dateFilterType)
        {
            Type type = dateFilterType.GetType();
            FieldInfo fi = type.GetField(Enum.GetName(type, dateFilterType));
            DescriptionAttribute dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));

            this.buttonEdit1.Text = dna.Description;
        }

        private void refreshUserSetting()
        {
            if (_UcDate == null || ActiveForm == null) return;
            if (!ActiveForm.Equals(this))
            {
                this.Hide();
                return;
            }
            if (radValue.Checked)
            {
                _UcDate.DateFilterType = _BindingFixedValue[_CurIndex];
            }
            else
            {
                foreach (RadioButton button in _BindingRadAndType.Keys)
                {
                    if (button.Checked)
                    {
                        _UcDate.DateFilterType = _BindingRadAndType[button];
                        break;
                    }
                }
                if (_UcDate.DateFilterType == ucEditDateFilter.DateFilterEditType.Other)
                {
                    _UcDate.DateFilterValue = new MB.Util.Model.DateFilterStruct(dTimeBegin.DateTime, dTimeEnd.DateTime);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_DeactivateCount > 0) {//变态的处理方法
                if (radOther.Checked && _UcDate != null)
                    _UcDate.DateFilterValue = new MB.Util.Model.DateFilterStruct(dTimeBegin.DateTime, dTimeEnd.DateTime);
                this.Hide();
            }
            timer1.Enabled = false;

            _DeactivateCount += 1;
        }

        private void radNone_CheckedChanged(object sender, EventArgs e)
        {
            refreshUserSetting();

            this.buttonEdit1.Enabled = radValue.Checked;
            dTimeBegin.Enabled = radOther.Checked;
            dTimeEnd.Enabled = radOther.Checked;
            if (radNone.Checked)
            {
                timer1.Enabled = true;
            }
        }

        private void dTimeBegin_ValueChanged(object sender, EventArgs e)
        {
            if (radOther.Checked)
                refreshUserSetting();
        }

        private void dTimeEnd_ValueChanged(object sender, EventArgs e)
        {
            if (radOther.Checked)
                refreshUserSetting();
        }

        private void frmEditDateFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                timer1.Enabled = true;
            }
        }

        private void dTimeBegin_DateTimeChanged(object sender, EventArgs e)
        {
            dTimeBegin_ValueChanged(null, null);
        }

        private void dTimeEnd_DateTimeChanged(object sender, EventArgs e)
        {
            dTimeEnd_ValueChanged(null, null);
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (!radValue.Checked) return;
            if (e.Button.IsLeft)
            {
                if (_CurIndex == 0) return;
                _CurIndex--;
            }
            else
            {
                if (_CurIndex == _BindingFixedValue.Count - 1) return;
                _CurIndex++;
            }
            ucEditDateFilter.DateFilterEditType dateFilterType = _BindingFixedValue[_CurIndex];
            getDescription(dateFilterType);

            refreshUserSetting();
        }

        private void dTimeBegin_Validated(object sender, EventArgs e) {
            if (radOther.Checked && _UcDate != null)
                _UcDate.DateFilterValue = new MB.Util.Model.DateFilterStruct(dTimeBegin.DateTime, dTimeEnd.DateTime);
        }

        private void dTimeEnd_Validated(object sender, EventArgs e) {
            if (radOther.Checked && _UcDate != null)
                _UcDate.DateFilterValue = new MB.Util.Model.DateFilterStruct(dTimeBegin.DateTime, dTimeEnd.DateTime);
        }
    }
}
