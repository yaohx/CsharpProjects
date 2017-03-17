using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.WinDxChart.Modules;
using MB.WinBase.Common;
using MB.Util.Model.Chart;
using MB.WinBase.IFace;

namespace MB.WinDxChart.Common
{
    public class RegisterTutorials
    {
        public static void Register()
        {
            ChartModulesInfo.Add("ChartModule", typeof(ucChartModule), "图表分析");
            ChartModulesInfo.Add("TemplateChartModule", typeof(ucTemplateChartModule), "图表模板展示");
        }
    }
}
