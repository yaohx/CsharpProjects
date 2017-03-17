using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinChart.Model
{
	/// <summary>
	/// 图表坐标信息.
	/// </summary>
	public class PointInfo
	{
		private object _XValue;

		/// <summary>
		/// X轴坐标信息.
		/// </summary>
		public object XValue
		{
			get
			{
				return _XValue;
			}
			set
			{
				_XValue = value;
			}
		}

		private object _YValue;

		/// <summary>
		/// Y轴坐标信息.
		/// </summary>
		public object YValue
		{
			get
			{
				return _YValue;
			}
			set
			{
				_YValue = value;
			}
		}

		private int _XAddPercent;

		/// <summary>
		/// X轴增量.
		/// </summary>
		public int XAddPercent
		{
			get
			{
				return _XAddPercent;
			}
			set
			{
				_XAddPercent = value;
			}
		}
	}
}
