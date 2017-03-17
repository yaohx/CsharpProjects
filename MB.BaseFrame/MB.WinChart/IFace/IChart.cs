using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.WinChart.Model;

namespace MB.WinChart.IFace
{
	/// <summary>
	/// 定义报表共有属性.
	/// </summary>
	public interface IChart : IDisposable
	{
		/// <summary>
		/// DataPoint 样式.
		/// </summary>
		LabelStyle LabelStyle { get; set; }

		/// <summary>
		/// 是否将数据转化为百分比.
		/// </summary>
		bool IsPercent { get; set; }

		/// <summary>
		/// 是否显示3D.
		/// </summary>
		bool Is3D{get;set;}

		/// <summary>
		/// 配制过的数据源.
		/// </summary>
		List<ChartAreaInfo> AreaDataList { get; set; }
	}
}
