
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace MB.WinBase.Ctls {
    /// <summary>
    /// 日期选择编辑公共处理控件。
    /// </summary>
    [ToolboxItem(true)]
    public partial class ucEditDateFilter : UserControl
    {
        private frmEditDateFilter _CurrentEditFilter;
        private DateFilterEditType _FilterType;
        private MB.Util.Model.DateFilterStruct _DateFilterValue;
        private static readonly string OTHER_DATE_DESCRIPTION = "{0}至{1}";
        private string _Formate;  //add by aifang 2012-03-28 根据xml配置设置日期显示的格式
        #region construct function...
        /// <summary>
        ///construct function...
        /// </summary>
        public ucEditDateFilter()
        {
            InitializeComponent();

            this.buttonEdit1.Properties.ReadOnly = true;
            this.buttonEdit1.BackColor = Color.White;
            this.buttonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(buttonEdit1_ButtonClick);
        }


        #endregion construct function...


        #region 扩展的public 成员...
        /// <summary>
        /// 
        /// </summary>
        public MB.Util.Model.DateFilterStruct CurrentSettingValue
        {
            get
            {
                DayOfWeek t = DateTime.Now.DayOfWeek;

                switch (_FilterType)
                {
                    case DateFilterEditType.Other: {
                        doCurrentEditFilterValidate();
                            return _DateFilterValue;
                        }
                    case DateFilterEditType.Today:
                        return new MB.Util.Model.DateFilterStruct(DateTime.Now, DateTime.Now);
                    case DateFilterEditType.LastDay:
                        return new MB.Util.Model.DateFilterStruct(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1));
                    case DateFilterEditType.Week:
                        return new MB.Util.Model.DateFilterStruct(DateTime.Now.AddDays(0 - (int)DateTime.Now.DayOfWeek), DateTime.Now.AddDays(7 - (int)DateTime.Now.DayOfWeek));
                    case DateFilterEditType.Month:
                        return new MB.Util.Model.DateFilterStruct(DateTime.Now.AddDays(1 - DateTime.Now.Day), DateTime.Now.AddDays(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - DateTime.Now.Day));
                    case DateFilterEditType.Year:
                        int dayOfYear = DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;
                        return new MB.Util.Model.DateFilterStruct(DateTime.Now.AddDays(1 - DateTime.Now.DayOfYear), DateTime.Now.AddDays(dayOfYear - DateTime.Now.DayOfYear));
                    default:
                        return _DateFilterValue;
                }
            }

        }
        /// <summary>
        /// 获取数据过滤的值。
        /// </summary>
        public MB.Util.Model.DateFilterStruct DateFilterValue
        {
            get
            {
                return _DateFilterValue;
            }
            set
            {
                _DateFilterValue = value;
                convertDescription();
            }
        }
        /// <summary>
        ///  设置的条件。
        /// </summary>
        public DateFilterEditType DateFilterType
        {
            get
            {
                return _FilterType;
            }
            set
            {
                _FilterType = value;
                convertDescription();
            }
        }

        /// <summary>
        /// 根据xml配置设置日期显示的格式
        /// </summary>
        public string Formate
        {
            get
            {
                return _Formate;
            }
            set
            {
                _Formate = value;
            }
        }

        /// <summary>
        /// 在某些情况下，DateEdit由于没有lose focus，所以改变的值备有被验证，也不能反映到查询条件中。
        /// 显示调用DoValidate方法，使的改变的值生效。
        /// </summary>
        private void doCurrentEditFilterValidate() {
            if (_CurrentEditFilter != null) {
                _CurrentEditFilter.dTimeBegin.DoValidate();
                _CurrentEditFilter.dTimeEnd.DoValidate();
            }
        }
        #endregion 扩展的public 成员...

        #region 内部函数处理...
        private void convertDescription()
        {
            if (_FilterType != DateFilterEditType.Other)
            {
                this.buttonEdit1.Text = MB.Util.MyCustomAttributeLib.Instance.GetFieldDesc(typeof(DateFilterEditType), _FilterType.ToString(), false);
            }
            else
            {
                buttonEdit1.Text = string.Format(OTHER_DATE_DESCRIPTION,
                    _DateFilterValue.BeginDate.ToString(_Formate),
                    _DateFilterValue.EndDate.ToString(_Formate));

            }
        }
        #endregion 内部函数处理...
        void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (_CurrentEditFilter == null)
            {
                _CurrentEditFilter = new frmEditDateFilter(this);
                _CurrentEditFilter.VisibleChanged += new EventHandler(_CurrentEditFilter_VisibleChanged);
            }
            if (_CurrentEditFilter.Visible)
            {
                _CurrentEditFilter.Visible = false;

            }
            else
            {
                Rectangle rect = this.RectangleToScreen(this.ClientRectangle);
                Point f = new Point(rect.X, rect.Y + this.Height);
                _CurrentEditFilter.Location = f;

                _CurrentEditFilter.ShowFilterForm(this.ParentForm);
                _CurrentEditFilter.BringToFront();


            }
        }
        void _CurrentEditFilter_VisibleChanged(object sender, EventArgs e)
        {
            convertDescription();

            if (this.ParentForm != null)
                this.ParentForm.BringToFront();

        }


        #region DateFilterEditType...
        /// <summary>
        /// 
        /// </summary>
        public enum DateFilterEditType
        {
            [Description("不记得")]
            None,
            [Description("今天")]
            Today,
            [Description("本周")]
            Week,
            [Description("本月")]
            Month,
            [Description("本年度")]
            Year,
            [Description("其它")]
            Other,
            [Description("昨天")]
            LastDay
        }
        #endregion DateFilterEditType...


    }
}

