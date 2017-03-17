using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using MB.WinDxChart.IFace;
using MB.WinBase.Common;
using MB.Util.Model.Chart;
using MB.WinDxChart.Chart;

namespace MB.WinDxChart.Modules
{
    public partial class ucTemplateChartModule : DefaultChartModuleBase
    {

        public ucTemplateChartModule()
        {
            InitializeComponent();
        }

        public override ChartControl ChartControl
        {
            get
            {
                return chartControl;
            }
        }

        protected override void SetChartControlStyle()
        {
            if (Template != null && Template.TEMPLATE_FILE != null)
            {
                System.IO.Stream stream = new System.IO.MemoryStream(Template.TEMPLATE_FILE);
                chartControl.LoadFromStream(stream);
            }
        }
    }
}
