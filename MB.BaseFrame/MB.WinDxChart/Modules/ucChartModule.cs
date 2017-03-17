using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using MB.WinBase.Common;
using MB.Util.Model.Chart;
using MB.WinDxChart.Chart;
using MB.WinDxChart.Template;
using System.IO;
using MB.WinDxChart.Common;
using DevExpress.XtraCharts.Native;
using MB.WinDxChart.IFace;

namespace MB.WinDxChart.Modules
{
    public partial class ucChartModule : DefaultChartModuleBase, IChartControl
    {
        public override ChartControl ChartControl
        {
            get
            {
                return chartControl;
            }
        }

        private ChartTemplateInfo _CurrentChartTemplate;

        public ucChartModule() 
        {
            InitializeComponent();
           
        }

        protected override void SetChartControlStyle()
        {
            //根据配置信息设置Chart的基本信息
            if (_ChartViewCfg != null)
            {
                string title = _ChartViewCfg.Title;
                if (string.IsNullOrEmpty(title)) title = _ClientRule.ModuleTreeNodeInfo.Name;
                chartControl.Titles.Clear();
                chartControl.Titles.Add(new ChartTitle() { Text = title, Font = new Font("宋体", 24, System.Drawing.FontStyle.Bold) });
                chartControl.Legend.Visible = true;
            }
        }
    }
}
