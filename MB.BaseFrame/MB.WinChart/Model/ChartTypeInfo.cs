using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dundas.Charting.WinControl;

namespace MB.WinChart.Model
{

	public enum ChartType
	{
		Line = SeriesChartType.Line,
		Column = SeriesChartType.Column,
		Pie = SeriesChartType.Pie
	}

	public class ChartTypeInfo
	{
		private ChartType _DefaultChartType;

		/// <summary>
		/// 报表类型.
		/// <remarks>仅支持Line | Pie | Column</remarks>
		/// </summary>
		public ChartType DefaultChartType
		{
			get { return _DefaultChartType; }
			set { _DefaultChartType = value; }
		}

		private string _XAxis;

		public string XAxis
		{
			get { return _XAxis; }
			set { _XAxis = value; }
		}

		private string _YAxis;

		public string YAxis
		{
			get { return _YAxis; }
			set { _YAxis = value; }
		}

		private string _ZAxis;

		public string ZAxis
		{
			get { return _ZAxis; }
			set { _ZAxis = value; }
		}
	}
}
