using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinChart.Model
{
	/// <summary>
	/// 报表数据类型.
	/// </summary>
	public enum ChartDataType
	{
		None,

		/// <summary>
		/// 常规类型.
		/// </summary>
		String,

		/// <summary>
		/// 时间类型.
		/// </summary>
		Date,

		/// <summary>
		/// 数值类型.
		/// </summary>
		Number
	}
}
