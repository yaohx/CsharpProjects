using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Dundas.Charting.WinControl;

using MB.WinChart.IFace;
using MB.WinChart.Model;
using MB.WinChart.Share;
using MB.Util;

namespace MB.WinChart.DundasChart
{
	/// <summary>
	/// 饼状图表.
	/// </summary>
	public class PieChart : IChart 
	{
		#region Private variables

		private Chart _Chart;
		private LabelStyle _LabelStyle;
		private bool _Is3D;
		private bool _IsPercent;
		private List<ChartAreaInfo> _AreaDataList;
		private bool disposed = false;

		#endregion

		#region Construct

		/// <summary>
		/// 构造函数.
		/// </summary>
		/// <param name="chart">需要渲染的图表控件.</param>
		public PieChart(Chart chart)
		{
			_Chart = chart;
			_Chart.MouseDown += new MouseEventHandler(Chart_MouseDown);
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

		#region Events

		private void Chart_MouseDown(object sender, MouseEventArgs e)
		{
			Chart chart = sender as Chart;
			if (null != chart)
			{
				HitTestResult result = chart.HitTest(e.X, e.Y);

				if (result.PointIndex < 0)
				{
					return;
				}

				if (null == result.Series)
				{
					return;
				}

				bool exploded =
					(result.Series.Points[result.PointIndex].CustomAttributes == ChartViewStatic.EXPLODED_TRUE) ? true : false;

				foreach (Series series in chart.Series)
				{
					foreach (DataPoint dataPoint in series.Points)
					{
						dataPoint.CustomAttributes = string.Empty;
					}
				}

				if (exploded)
				{
					return;
				}

				if (result.ChartElementType == ChartElementType.DataPoint || result.ChartElementType == ChartElementType.DataPointLabel)
				{
					foreach (Series series in chart.Series)
					{
						// Set Attribute
						if (series == result.Series)
						{
							DataPoint dataPoint = series.Points[result.PointIndex];
							dataPoint.CustomAttributes = ChartViewStatic.EXPLODED_TRUE;
						}
					}
				}

				if (result.ChartElementType == ChartElementType.LegendItem)
				{
					foreach (Series series in chart.Series)
					{
						// Set Attribute
						if (series == result.Series)
						{
							DataPoint dataPoint = series.Points[result.PointIndex];
							dataPoint.CustomAttributes = ChartViewStatic.EXPLODED_TRUE;
						}
					}
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

		private void createChart()
		{
			_Chart.Location = new System.Drawing.Point(ChartViewStatic.CHART_POINT_X, ChartViewStatic.CHART_POINT_Y);
			_Chart.Size = new System.Drawing.Size(ChartViewStatic.CHART_SIZE_X, ChartViewStatic.CHART_SIZE_Y);
			_Chart.ChartAreas.Clear();
			_Chart.Legends.Clear();
			_Chart.Series.Clear();

			foreach (ChartAreaInfo chartAreaInfo in AreaDataList)
			{
				ChartArea chartArea = new ChartArea();
				chartArea.Name = chartAreaInfo.Name;
				chartArea.Area3DStyle.Enable3D = _Is3D;
				_Chart.ChartAreas.Add(chartArea);

				int index = 0;
				int pointIndex = 0;
				Color[] colors = generateColors();

				foreach (SeriesInfo seriesInfo in chartAreaInfo.Series)
				{
					Legend legend = new Legend(seriesInfo.Name);

					if (AreaDataList.Count != 1)
					{
						legend.Title = seriesInfo.Name;
					}

					legend.BorderColor = Color.Gray;
					legend.DockInsideChartArea = false;
					legend.DockToChartArea = chartArea.Name;

					Series series = new Series();
					series.Name = seriesInfo.Name;
					series.Type = SeriesChartType.Pie;
					series.CustomAttributes = ChartViewStatic.SERIES_DEFAULT_CUSTOMATTRIBUTES;
					series.ShadowColor = Color.Transparent;

					series.Legend = legend.Name;
					series.ChartArea = chartArea.Name;

					series.Color = colors[index % ChartViewStatic.COLOR_COUNTS];
					index++;

					if (_IsPercent)
					{
						setSeriesPercentValue(series, seriesInfo, legend, ref pointIndex, colors);
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

								if (!yValue.Equals(0))
								{
									series.Points.AddXY(pointInfo.XValue, yValue);
								}
								else
								{
									LegendItem legendItem = new LegendItem(pointInfo.XValue.ToString(), colors[pointIndex % ChartViewStatic.COLOR_COUNTS], string.Empty);
									legend.CustomItems.Add(legendItem);

									pointIndex++;
								}
							}
						}
					}

					_Chart.Legends.Add(legend);
					_Chart.Series.Add(series);
				}
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

		private void setSeriesPercentValue(Series series, SeriesInfo seriesInfo, Legend legend, ref int pointIndex, Color[] colors)
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

					if (!yValue.Equals(0))
					{
						series.Points.AddXY(pointInfo.XValue, yValue);
					}
					else
					{
						LegendItem legendItem = new LegendItem(pointInfo.XValue.ToString(), colors[pointIndex % ChartViewStatic.COLOR_COUNTS], string.Empty);
						legend.CustomItems.Add(legendItem);

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
					_Chart.MouseDown -= new MouseEventHandler(Chart_MouseDown);
				}

				// Note disposing has been done.
				disposed = true;

			}
		}

		~PieChart()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }


		#endregion
	}
}
