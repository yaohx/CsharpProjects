using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.WinBase.Common;
using System.Data;
using DevExpress.XtraCharts;
using MB.WinBase.IFace;
using MB.Util;

namespace MB.WinDxChart.Chart
{
    public class ChartMappingHelper
    {
        private static readonly string CHART_VIEW_CFG = "ChartView";
        private IClientRuleQueryBase _ClientRule;

        public ChartMappingHelper(IClientRuleQueryBase clientRule)
        {
            _ClientRule = clientRule;
        }

        /// <summary>
        /// 根据Datatable转换成ChartSeriesInfo数组,用于在图表中显示
        /// </summary>
        /// <param name="table">数据源</param>
        /// <param name="mapping">DataTable与Chart的映射信息</param>
        /// <param name="viewType">图表类型</param>
        /// <returns></returns>
        public List<ChartSeriesInfo> ConvertToChartSeries(DataTable table,ChartMappingInfo mapping,string viewType)
        {
            if (!table.Columns.Contains(mapping.XColumn))
            {
                throw new ArgumentException(string.Format("X轴字段名{0}在数据源中不存在", mapping.XColumn));
            }

            if (!table.Columns.Contains(mapping.YColumn))
            {
                throw new ArgumentException(string.Format("Y轴字段名{0}在数据源中不存在", mapping.YColumn));
            }

            if (!table.Columns.Contains(mapping.SeriesColumn))
            {
                throw new ArgumentException(string.Format("系列字段名{0}在数据源中不存在", mapping.SeriesColumn));
            }

            List<ChartSeriesInfo> chartSeries = new List<ChartSeriesInfo>();
            ChartArgumentType type = (ChartArgumentType)Enum.Parse(typeof(ChartArgumentType), mapping.ArgumentType);
            DataColumn dc = table.Columns[mapping.YColumn];
            Type yType = dc.DataType;
            //MB.WinBase.MessageBoxEx.Show(yType.FullName);
            if (type != ChartArgumentType.Extend)
            {
                //判断传入的YColumn是否为数值类型
                if (!yType.FullName.Equals("System.Decimal") &&
                   !yType.FullName.Equals("System.Double") &&
                   !yType.FullName.Equals("System.Int32"))
                    throw new MB.Util.APPException("配置的参数YColumn类型不正确，请配置数值类型！", APPMessageType.DisplayToUser);
            }
            if (type == ChartArgumentType.Extend)
            {
                chartSeries = createExtendChart(table, mapping, viewType);
            }
            else if (type == ChartArgumentType.Sum)
            {
                chartSeries = createSumChart(table, mapping, viewType);
            }
            else if (type == ChartArgumentType.Count)
            {
                chartSeries = createCountChart(table, mapping, viewType);
            }
            else if (type == ChartArgumentType.MaxValue)
            {
                chartSeries = createMaxValueChart(table, mapping, viewType);
            }
            else if (type == ChartArgumentType.MinValue)
            {
                chartSeries = createMinValueChart(table, mapping, viewType);
            }
            else if (type == ChartArgumentType.Average)
            {
                chartSeries = createAverageChart(table, mapping, viewType);
            }
            else throw new MB.Util.APPException(string.Format("暂不支持 {0} 值类型计算", mapping.ArgumentType), APPMessageType.DisplayToUser);

            return chartSeries;
        }

        private List<ChartSeriesInfo> createSumChart(DataTable table, ChartMappingInfo mapping, string viewType)
        {
            List<ChartSeriesInfo> chartSeries = new List<ChartSeriesInfo>();
            IEnumerable<IGrouping<string, DataRow>> result = table.Rows.Cast<DataRow>().GroupBy<DataRow, string>(dr => dr[mapping.SeriesColumn].ToString());
            foreach (IGrouping<string, DataRow> series in result)
            {
                var seriesInfo = new ChartSeriesInfo(series.Key, viewType.ToString());
                var xValues = series.Cast<DataRow>().GroupBy<DataRow, string>(dr => dr[mapping.XColumn].ToString());
                foreach (IGrouping<string, DataRow> points in xValues)
                {
                    seriesInfo.SeriesPoints.Add(new ChartSeriesPointInfo(points.Key, points.Sum<DataRow>(o => decimal.Parse(o[mapping.YColumn].ToString()))));
                }
                chartSeries.Add(seriesInfo);
            }
            return chartSeries;
        }

        private List<ChartSeriesInfo> createCountChart(DataTable table, ChartMappingInfo mapping, string viewType)
        {

            List<ChartSeriesInfo> chartSeries = new List<ChartSeriesInfo>();
            IEnumerable<IGrouping<string, DataRow>> result = table.Rows.Cast<DataRow>().GroupBy<DataRow, string>(dr => dr[mapping.SeriesColumn].ToString());
            foreach (IGrouping<string, DataRow> series in result)
            {
                var seriesInfo = new ChartSeriesInfo(series.Key, viewType);
                var xValues = series.Cast<DataRow>().GroupBy<DataRow, string>(dr => dr[mapping.XColumn].ToString());
                foreach (IGrouping<string, DataRow> points in xValues)
                {
                    seriesInfo.SeriesPoints.Add(new ChartSeriesPointInfo(points.Key, points.Count()));
                }
                chartSeries.Add(seriesInfo);
            }
            return chartSeries;
        }

        private List<ChartSeriesInfo> createMaxValueChart(DataTable table, ChartMappingInfo mapping, string viewType)
        {

            List<ChartSeriesInfo> chartSeries = new List<ChartSeriesInfo>();
            IEnumerable<IGrouping<string, DataRow>> result = table.Rows.Cast<DataRow>().GroupBy<DataRow, string>(dr => dr[mapping.SeriesColumn].ToString());
            foreach (IGrouping<string, DataRow> series in result)
            {
                var seriesInfo = new ChartSeriesInfo(series.Key, viewType);
                var xValues = series.Cast<DataRow>().GroupBy<DataRow, string>(dr => dr[mapping.XColumn].ToString());
                foreach (IGrouping<string, DataRow> points in xValues)
                {
                    seriesInfo.SeriesPoints.Add(new ChartSeriesPointInfo(points.Key, points.Max<DataRow>(dr => decimal.Parse(dr[mapping.YColumn].ToString()))));
                }
                chartSeries.Add(seriesInfo);
            }

            return chartSeries;
        }

        private List<ChartSeriesInfo> createMinValueChart(DataTable table, ChartMappingInfo mapping, string viewType)
        {

            List<ChartSeriesInfo> chartSeries = new List<ChartSeriesInfo>();
            IEnumerable<IGrouping<string, DataRow>> result = table.Rows.Cast<DataRow>().GroupBy<DataRow, string>(dr => dr[mapping.SeriesColumn].ToString());
            foreach (IGrouping<string, DataRow> series in result)
            {
                var seriesInfo = new ChartSeriesInfo(series.Key, viewType);
                var xValues = series.Cast<DataRow>().GroupBy<DataRow, string>(dr => dr[mapping.XColumn].ToString());
                foreach (IGrouping<string, DataRow> points in xValues)
                {
                    seriesInfo.SeriesPoints.Add(new ChartSeriesPointInfo(points.Key, points.Min<DataRow>(dr => decimal.Parse(dr[mapping.YColumn].ToString()))));
                }
                chartSeries.Add(seriesInfo);
            }

            return chartSeries;
        }

        private List<ChartSeriesInfo> createAverageChart(DataTable table, ChartMappingInfo mapping, string viewType)
        {

            List<ChartSeriesInfo> chartSeries = new List<ChartSeriesInfo>();
            IEnumerable<IGrouping<string, DataRow>> result = table.Rows.Cast<DataRow>().GroupBy<DataRow, string>(dr => dr[mapping.SeriesColumn].ToString());
            foreach (IGrouping<string, DataRow> series in result)
            {
                var seriesInfo = new ChartSeriesInfo(series.Key, viewType);
                var xValues = series.Cast<DataRow>().GroupBy<DataRow, string>(dr => dr[mapping.XColumn].ToString());
                foreach (IGrouping<string, DataRow> points in xValues)
                {
                    seriesInfo.SeriesPoints.Add(new ChartSeriesPointInfo(points.Key, points.Average<DataRow>(dr => decimal.Parse(dr[mapping.YColumn].ToString()))));
                }
                chartSeries.Add(seriesInfo);
            }

            return chartSeries;
        }

        private List<ChartSeriesInfo> createExtendChart(DataTable table, ChartMappingInfo mapping, string viewType)
        {

            List<ChartSeriesInfo> chartSeries = new List<ChartSeriesInfo>();
            foreach (DataRow row in table.Rows)
            {
                string xValue = row[mapping.XColumn].ToString();
                string seriesName = row[mapping.SeriesColumn].ToString();

                decimal yValue = 0;

                if (row[mapping.YColumn] != null)
                    yValue = decimal.Parse(row[mapping.YColumn].ToString());

                var chart = chartSeries.Find(c => c.Name == seriesName);

                if (chart == null)
                {
                    chart = new ChartSeriesInfo(seriesName, viewType);
                    chartSeries.Add(chart);
                }

                chart.SeriesPoints.Add(new ChartSeriesPointInfo(xValue, yValue));

            }

            return chartSeries;
        }
    }
}
