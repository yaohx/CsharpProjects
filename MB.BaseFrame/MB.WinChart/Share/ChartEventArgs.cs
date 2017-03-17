using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Dundas.Charting.WinControl;

namespace MB.WinChart.Share
{
	/// <summary>
	/// 报表事件数据.
	/// </summary>
	public class ChartEventArgs : EventArgs
	{
		private string _AnalyseData;

		/// <summary>
		/// 分类类型.
		/// </summary>
		public string AnalyseData
		{
			get { return _AnalyseData; }
			set { _AnalyseData = value; }
		}

		private DataColumn _XAxis;

		/// <summary>
		/// X轴信息.
		/// </summary>
		public DataColumn XAxis
		{
			get { return _XAxis; }
			set { _XAxis = value; }
		}

		private DataColumn _YAxis;

		/// <summary>
		/// Y轴信息.
		/// </summary>
		public DataColumn YAxis
		{
			get { return _YAxis; }
			set { _YAxis = value; }
		}

		private DataColumn _ZAxis;

		/// <summary>
		/// Z轴信息.
		/// </summary>
		public DataColumn ZAxis
		{
			get { return _ZAxis; }
			set { _ZAxis = value; }
		}

		private SeriesChartType _ChartType;

		/// <summary>
		/// 图表类型.
		/// </summary>
		public SeriesChartType ChartType
		{
			get { return _ChartType; }
			set { _ChartType = value; }
		}

		private string _ChartTemplate;

		/// <summary>
		/// 模板信息.
		/// </summary>
		public string ChartTemplate
		{
			get { return _ChartTemplate; }
			set { _ChartTemplate = value; }
		}

		private bool _NeedSortXAxis;

		/// <summary>
		/// 是否需要对X轴排序.
		/// </summary>
		public bool NeedSortXAxis
		{
			get { return _NeedSortXAxis; }
			set { _NeedSortXAxis = value; }
		}

		private bool _IsPercent;

		public bool IsPercent
		{
			get { return _IsPercent; }
			set { _IsPercent = value; }
		}
	}
}
