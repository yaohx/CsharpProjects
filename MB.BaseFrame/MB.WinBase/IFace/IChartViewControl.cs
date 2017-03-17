using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MB.WinBase.IFace {
    /// <summary>
    ///  图表自定义控件必须要实现的接口
    /// </summary>
    public interface IChartViewControl {
        /// <summary>
        /// 将数据源绑定到 MB.WinChart.ChartView 控件.
        /// </summary>
        /// <param name="clientRule">当前分析的业务对象</param>
        /// <param name="dsData">数据源</param>
        void CreateDataBinding(MB.WinBase.IFace.IClientRuleConfig clientRule, DataSet dsData);

        /// <summary>
        /// 刷新控件.
        /// </summary>
        void RefreshLayout();
    }
}
