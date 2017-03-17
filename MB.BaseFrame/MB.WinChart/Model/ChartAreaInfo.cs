using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinChart.Model
{
	/// <summary>
	/// 报表数据源.
	/// </summary>
	public class ChartAreaInfo
	{
		private string _Name;

		/// <summary>
		/// 名称.
		/// </summary>
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		private string _Description;

		/// <summary>
		/// 描述.
		/// </summary>
		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}

		private List<SeriesInfo> _Series;

		/// <summary>
		/// 数据分类集合.
		/// </summary>
		public List<SeriesInfo> Series
		{
			get { return _Series; }
			set { _Series = value; }
		}
	}
}
