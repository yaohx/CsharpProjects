using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinChart.Model
{
	/// <summary>
	/// 配制项允许的坐标轴.
	/// </summary>
	[Flags]
	public enum AllowChartAxes
	{
		None = 0X0000,

		/// <summary>
		/// X轴.
		/// </summary>
		X = 0X0001,

		/// <summary>
		/// Y轴.
		/// </summary>
		Y = 0X0002,

		/// <summary>
		/// Z轴.
		/// </summary>
		Z = 0X0004
	}
}
