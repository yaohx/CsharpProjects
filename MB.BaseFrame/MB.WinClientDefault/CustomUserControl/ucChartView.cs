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
using MB.WinDxChart.Chart;

namespace MB.WinClientDefault.CustomUserControl {
    public partial class ucChartView : UserControl, ICustomViewControl {
        private readonly static string CHART_VIEW_CFG = "ChartView";
        private ucChartControl _Chart;
        public ucChartView() {
            InitializeComponent();
        }


        #region ICustomViewControl Members

        public void CreateDataBinding(IClientRuleQueryBase clientRule, object dataSource) {
            DataSet dsData = dataSource as DataSet;

            if (_Chart == null) {
                _Chart = new ucChartControl();
                ChartViewCfgInfo chartViewCfg;
                string viewType = string.Empty;
                var cfgs = clientRule.UIRuleXmlConfigInfo.GetDefaultChartViewCfg();
                if (cfgs != null && cfgs.Count > 0) {
                    chartViewCfg = cfgs[CHART_VIEW_CFG];
                    viewType = chartViewCfg.ViewType;

                    _Chart.ArgumentType = chartViewCfg.ChartMapping.ArgumentType;
                    _Chart.ChartMapping = chartViewCfg.ChartMapping;
                    _Chart.SeriesColumn = chartViewCfg.ChartMapping.SeriesColumn;
                    _Chart.Title = chartViewCfg.Title;
                    _Chart.ViewType = chartViewCfg.ViewType;
                    _Chart.XColumn = chartViewCfg.ChartMapping.XColumn;
                    _Chart.YColumn = chartViewCfg.ChartMapping.YColumn;

                    _Chart.Dock = DockStyle.Fill;
                    this.panel.Controls.Add(_Chart);
                }
                else
                    throw new MB.Util.APPException("请检查是否在XML配置了节点 Entity/Charts/Chart");

            }
            _Chart.RefreshBindingData(dsData);


        }

        public void Export() {
            _Chart.ExportToXls();
        }

        #endregion

    }
}
