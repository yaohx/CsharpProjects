using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

using Dundas.Charting.WinControl;

using MB.WinChart.IFace;
using MB.WinChart.Model;
using MB.WinChart.Share;
using MB.Util;

namespace MB.WinChart.DundasChart
{
	/// <summary>
	/// 柱状图表.
	/// </summary>
	public class ColumnChart : IChart
	{
		#region Private variables

		private Chart _Chart;
		private LabelStyle _LabelStyle;
		private bool _Is3D;
		private bool _IsPercent;
		private bool _NeedSortXAxis;
		private bool _IsMultiChartArea;
		private List<ChartAreaInfo> _AreaDataList;
		private bool disposed = false;

		#endregion

		#region Construct

		/// <summary>
		/// 构造函数.
		/// </summary>
		/// <param name="chart">需要渲染的图表控件.</param>
		/// <param name="needSortXAxis">是否需要对X轴进行排序.</param>
		/// <param name="isMultiChartArea">是否需要呈现多<c>ChartArea</c>.</param>
		public ColumnChart(Chart chart, bool needSortXAxis, bool isMultiChartArea)
		{
			_Chart = chart;
			_NeedSortXAxis = needSortXAxis;
			_IsMultiChartArea = isMultiChartArea;
		}

		#endregion

		#region IChart 成员

		/// <summary>
		/// <see cref="MB.WinChart.IFace.IChart.LabelStyle"/>
		/// </summary>
		public LabelStyle LabelStyle
		{
			get
			{
				return _LabelStyle;
			}
			set
			{
				_LabelStyle = value;
			}
		}

		/// <summary>
		/// <see cref="MB.WinChart.IFace.IChart.Is3D"/>
		/// </summary>
		public bool Is3D
		{
			get
			{
				return _Is3D;
			}
			set
			{
				_Is3D = value;
			}
		}

		/// <summary>
		/// <see cref="MB.WinChart.IFace.IChart.IsPercent"/>
		/// </summary>
		public bool IsPercent
		{
			get { return _IsPercent; }
			set { _IsPercent = value; }
		}

		/// <summary>
		/// <see cref="MB.WinChart.IFace.IChart.AreaDataList"/>
		/// </summary>
		public List<ChartAreaInfo> AreaDataList
		{
			get
			{
				return _AreaDataList;
			}
			set
			{
				_AreaDataList = value;

				try
				{
					createChart();
				}
				catch (Exception ex)
				{
					string clientMsg = getClientMsg(MethodBase.GetCurrentMethod().Name, ex);

					throw new APPException(clientMsg, APPMessageType.DisplayToUser, ex);
				}
			}
		}

		#endregion

		#region Private methods

		private Color[] generateColors()
		{
			ChartViewHelper chartViewHelper = new ChartViewHelper();

			Color[] colors = chartViewHelper.GenerateColors();

			return colors;
		}

		private void createSingleChartAreaChart()
		{
			if (null != AreaDataList)
			{
				ChartArea chartArea = new ChartArea();
				chartArea.Name = AreaDataList[0].Name;
				chartArea.Area3DStyle.Enable3D = _Is3D;
				chartArea.AxisX.Interval = 1;
				_Chart.ChartAreas.Add(chartArea);

				Legend legend = new Legend(chartArea.Name);
				legend.Title = string.Empty;

				int index = 0;
				Color[] colors = generateColors();

				foreach (SeriesInfo seriesInfo in AreaDataList[0].Series)
				{
					Series series = new Series();
					series.Name = seriesInfo.Name;
					series.Type = SeriesChartType.Column;
					series.CustomAttributes = ChartViewStatic.SERIES_DEFAULT_CUSTOMATTRIBUTES;
					series.ShadowColor = Color.Transparent;
					series.ChartArea = chartArea.Name;
					series.Legend = legend.Name;

					series.Color = colors[index % ChartViewStatic.COLOR_COUNTS];
					index++;

					int pointIndex = 0;

					if (1 == AreaDataList[0].Series.Count)
					{
						legend.CustomItems.Clear();
						series.ShowInLegend = false;
					}

					if (_IsPercent)
					{
						//if (1 == AreaDataList[0].Series.Count)
						//{
							setSeriesPercentValue(series, seriesInfo,AreaDataList[0], ref pointIndex, legend, colors);
						//}
					}
					else
					{
						foreach (PointInfo pointInfo in seriesInfo.Points)
						{
							IList ilist = pointInfo.YValue as IList;

							if (null != ilist)
							{
								double yValue = 0;

								for (int i = 0; i < ilist.Count; i++)
								{
									yValue += Convert.ToDouble(ilist[i]);
								}

								series.Points.AddXY((object)pointInfo.XValue, new object[] { yValue });
							}

							if (1 == AreaDataList[0].Series.Count)
							{
								series.Points[pointIndex].Color = colors[pointIndex % ChartViewStatic.COLOR_COUNTS];
								LegendItem legendItem = new LegendItem(pointInfo.XValue.ToString(), colors[pointIndex % ChartViewStatic.COLOR_COUNTS], string.Empty);
								legend.CustomItems.Add(legendItem);
								pointIndex++;
							}
						}
					}

					if (_NeedSortXAxis)
					{
						series.Sort(PointsSortOrder.Ascending, ChartViewStatic.SORT_BY_AXISLABEL);
					}

					_Chart.Series.Add(series);
				}

				legend.BorderColor = Color.Gray;
				legend.DockInsideChartArea = false;
				legend.DockToChartArea = chartArea.Name;
				_Chart.Legends.Add(legend);
			}
		}

		private void createMultiChartAreaChart()
		{
			foreach (ChartAreaInfo chartAreaInfo in AreaDataList)
			{
				ChartArea chartArea = new ChartArea();
				chartArea.Name = chartAreaInfo.Name;
				chartArea.Area3DStyle.Enable3D = _Is3D;
				chartArea.AxisX.Interval = 1;
				_Chart.ChartAreas.Add(chartArea);

				int index = 0;
				Color[] colors = generateColors();

				foreach (SeriesInfo seriesInfo in chartAreaInfo.Series)
				{
					Legend legend = new Legend(seriesInfo.Name);
					legend.Title = seriesInfo.Name;
					legend.BorderColor = Color.Gray;
					legend.DockInsideChartArea = false;
					legend.DockToChartArea = chartArea.Name;

					Series series = new Series();
					series.Name = seriesInfo.Name;
					series.Type = SeriesChartType.Column;
					series.CustomAttributes = ChartViewStatic.SERIES_DEFAULT_CUSTOMATTRIBUTES;
					series.ShadowColor = Color.Transparent;
					series.ChartArea = chartArea.Name;
					series.Legend = legend.Name;

					series.Color = colors[index % ChartViewStatic.COLOR_COUNTS];
					index++;

					int pointIndex = 0;

					if (1 == chartAreaInfo.Series.Count)
					{
						legend.CustomItems.Clear();
						series.ShowInLegend = false;
					}

					if (_IsPercent)
					{
						setSeriesPercentValue(series, seriesInfo, chartAreaInfo, ref pointIndex, legend, colors);
					}
					else
					{
						foreach (PointInfo pointInfo in seriesInfo.Points)
						{
							double yValue = 0;
							IList ilist = pointInfo.YValue as IList;

							if (null != ilist)
							{
								for (int i = 0; i < ilist.Count; i++)
								{
									yValue += Convert.ToDouble(ilist[i]);
								}

								series.Points.AddXY((object)pointInfo.XValue, new object[] { yValue });
							}

							if (1 == chartAreaInfo.Series.Count)
							{
								LegendItem legendItem = null;

								if (0 != yValue)
								{
									series.Points[pointIndex].Color = colors[pointIndex % ChartViewStatic.COLOR_COUNTS];
									legendItem = new LegendItem(pointInfo.XValue.ToString(), series.Points[pointIndex].Color, string.Empty);
									legend.CustomItems.Add(legendItem);
								}

								pointIndex++;
							}
						}
					}

					if (_NeedSortXAxis)
					{
						series.Sort(PointsSortOrder.Ascending, ChartViewStatic.SORT_BY_AXISLABEL);
					}

					_Chart.Legends.Add(legend);
					_Chart.Series.Add(series);
				}
			}
		}

		private void createChart()
		{
			_Chart.Location = new System.Drawing.Point(ChartViewStatic.CHART_POINT_X, ChartViewStatic.CHART_POINT_Y);
			_Chart.Size = new System.Drawing.Size(ChartViewStatic.CHART_SIZE_X, ChartViewStatic.CHART_SIZE_Y);
			_Chart.ChartAreas.Clear();
			_Chart.Legends.Clear();
			_Chart.Series.Clear();

			if (_IsMultiChartArea)
			{
				createMultiChartAreaChart();
			}
			else
			{
				createSingleChartAreaChart();
			}
		}

		private string getClientMsg(string methodName, Exception e)
		{
			string clientMsg = string.Empty;

			StringBuilder stringBuilder = new StringBuilder();

			clientMsg = stringBuilder.Append(GetType().Name)
				.Append(ChartViewStatic.DOT)
				.Append(methodName)
				.Append(ChartViewStatic.COLON)
				.Append(e.Message).ToString();

			return clientMsg;
		}

		private void setSeriesPercentValue(Series series, SeriesInfo seriesInfo,
			ChartAreaInfo chartAreaInfo, ref int pointIndex, Legend legend, Color[] colors)
		{
			double yValueCount = 0;

			foreach (PointInfo pointInfo in seriesInfo.Points)
			{
				IList ilist = pointInfo.YValue as IList;

				if (null != ilist)
				{
					for (int i = 0; i < ilist.Count; i++)
					{
						yValueCount += Convert.ToDouble(ilist[i]);
					}
				}
			}

			foreach (PointInfo pointInfo in seriesInfo.Points)
			{
				IList ilist = pointInfo.YValue as IList;

				if (null != ilist)
				{
					double yValue = 0;

					for (int i = 0; i < ilist.Count; i++)
					{
						yValue += Convert.ToDouble(ilist[i]);
					}

					yValue = Math.Round(yValue / yValueCount, ChartViewStatic.FLOAT_PRECISION) * ChartViewStatic.CONVERT_PERCENT;

					series.Points.AddXY(pointInfo.XValue, yValue);

					if (1 == chartAreaInfo.Series.Count)
					{
						LegendItem legendItem = null;

						if (0 != yValue)
						{
							series.Points[pointIndex].Color = colors[pointIndex % ChartViewStatic.COLOR_COUNTS];
							legendItem = new LegendItem(pointInfo.XValue.ToString(), series.Points[pointIndex].Color, string.Empty);
							legend.CustomItems.Add(legendItem);
						}

						pointIndex++;
					}
				}
			}
		}

		#endregion

		#region IDisposable 成员

		/// <summary>
		/// <see cref="System.IDisposable.Dispose"/>
		/// </summary>
		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if (!this.disposed)
			{
				// If disposing equals true, dispose all managed
				// and unmanaged resources.
				if (disposing)
				{
					// Dispose managed resources.
				}

				// Note disposing has been done.
				disposed = true;
			}
		}

		~ColumnChart()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

		#endregion
	}
}
