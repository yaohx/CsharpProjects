using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Native;
using MB.WinBase.Common;

namespace MB.WinDxChart.Chart
{
    [ToolboxItem(true)]
    public partial class ucChartControl : UserControl
    {
        private string _ViewType;

        public ucChartControl()
        {
            InitializeComponent();

            this.Resize += new EventHandler(ucChartControl_Resize);
            this.Load += new EventHandler(ucChartControl_Load);
        }

        void ucChartControl_Load(object sender, EventArgs e)
        {

        }

        
        void ucChartControl_Resize(object sender, EventArgs e)
        {
            this.Height = chartControl.Height;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        public void RefreshBindingData(DataSet ds)
        {
            try
            {
                //参数校验
                if (string.IsNullOrEmpty(XColumn)) throw new MB.Util.APPException("XColumn未设置，初始化控件失败!", Util.APPMessageType.DisplayToUser);
                if (string.IsNullOrEmpty(YColumn)) throw new MB.Util.APPException("YColumn未设置，初始化控件失败!", Util.APPMessageType.DisplayToUser);
                if (string.IsNullOrEmpty(SeriesColumn)) throw new MB.Util.APPException("SeriesColumn未设置，初始化控件失败!", Util.APPMessageType.DisplayToUser);
                if (string.IsNullOrEmpty(ArgumentType)) throw new MB.Util.APPException("ArgumentType未设置，初始化控件失败!", Util.APPMessageType.DisplayToUser);
                if (string.IsNullOrEmpty(ViewType)) throw new MB.Util.APPException("ViewType未设置，初始化控件失败!", Util.APPMessageType.DisplayToUser);

                ChartMapping = new ChartMappingInfo(_XColumn, _YColumn, _SeriesColumn, _ArgumentType);

                chartControl.Titles.Clear();
                chartControl.Titles.Add(new ChartTitle() { Text = Title, Font = new Font("宋体", 24, System.Drawing.FontStyle.Bold) });
                chartControl.Legend.Visible = true;

                chartControl.Series.Clear();
                chartControl.DataSource = ds;

                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    return;

                ChartMappingHelper helper = new ChartMappingHelper(null);
                List<ChartSeriesInfo> series = helper.ConvertToChartSeries(ds.Tables[0], _ChartMapping, _ViewType);
                if (series == null || series.Count == 0)
                    throw new MB.Util.APPException("ChartSeries转换失败，请检查数据源与配置的Mapping项是否匹配!", Util.APPMessageType.DisplayToUser);

                DxChartControlHelper.Instance.BindingDxChartControl(chartControl, series);
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        public void ExportToXls() {
            string fileName = showSaveFileDialog();
            if (!string.IsNullOrEmpty(fileName)) {
                this.chartControl.ExportToXls(fileName);
            }
        }

        private string showSaveFileDialog() {
            SaveFileDialog dlg = new SaveFileDialog();
            string name = _Title;
            int n = name.LastIndexOf(".") + 1;
            if (n > 0) name = name.Substring(n, name.Length - n);
            dlg.Title = "XLS Document";
            dlg.FileName = name;
            dlg.Filter = "XLS Documents (*.xls)|*.xls";
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return string.Empty;
        }

        #region property
        private string _Title;
        /// <summary>
        /// 获取或者设置控件是否允许编辑。
        /// </summary>
        [Description("获取或者设置图表的标题。")]
        public string Title
        {
            get { return _Title; }
            set 
            {
                _Title = value;
            }
        }

        [Description("获取或设置图表的类型")]
        public string ViewType
        {
            get
            {
                return _ViewType;
            }
            set 
            {
                _ViewType = value;
            }
        }

        private ChartMappingInfo _ChartMapping;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ChartMappingInfo ChartMapping
        {
            get { return _ChartMapping; }
            set { _ChartMapping = value; }
        }

        private string _XColumn;
        [Bindable(true)]
        [Category("ChartMapping")]
        [Description("获取或设置绑定的X轴数据源")]
        public string XColumn
        {
            get { return _XColumn; }
            set { _XColumn = value; }
        }

        private string _YColumn;
        [Bindable(true)]
        [Category("ChartMapping")]
        [Description("获取或设置绑定的Y轴数据源")]
        public string YColumn
        {
            get { return _YColumn; }
            set { _YColumn = value; }
        }

        private string _SeriesColumn;
        [Bindable(true)]
        [Category("ChartMapping")]
        [Description("获取或设置绑定的维度数据源")]
        public string SeriesColumn
        {
            get { return _SeriesColumn; }
            set { _SeriesColumn = value; }
        }

        private string _ArgumentType;
        [Category("ChartMapping")]
        [Description("获取或设置Y轴值计算类型，目前支持Sum、Count、MaxValue、MinValue、Average、Extend")]
        public string ArgumentType
        {
            get { return _ArgumentType; }
            set { _ArgumentType = value; }
        }

        private object _DataSource;
        [Description("获取或设置显示的数据源")]
        public object DataSource
        {
            get 
            {
                if (_DataSource == null) _DataSource = chartControl.DataSource;
                return _DataSource;
            }
            set { _DataSource = value; }
        }
        #endregion
    }
}
