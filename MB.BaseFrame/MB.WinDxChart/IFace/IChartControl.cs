using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.WinBase.IFace;
using System.Data;
using MB.WinBase.Common;
using MB.Util.Model.Chart;

namespace MB.WinDxChart.IFace
{
    public interface IChartControl
    {
        void InitChartControl(IClientRuleQueryBase clientRule);

        void BindingChartControl(object datasource);

        void SaveChartControl(ChartTemplateInfo template);
    }
}
