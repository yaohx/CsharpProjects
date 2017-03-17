using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinChart.Model
{
	/// <summary>
	/// <c>DataPoint</c> 数值单位.
	/// </summary>
	public enum LabelStyle
	{
		/// <summary>
		/// 货币.
		/// </summary>
		C,

		/// <summary>
		/// 整型货币.
		/// </summary>
		C0,

		/// <summary>
		/// 科学计数.
		/// </summary>
		E,

		/// <summary>
		/// 整型科学技术.
		/// </summary>
		E0,

		/// <summary>
		/// 百分比.
		/// </summary>
		P
	}
}
