using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.WinChart.Share;

namespace MB.WinChart.Model
{
	/// <summary>
	/// 翻页设置.
	/// </summary>
	public class PagerSettings
	{
		private int _PageIndex;

		/// <summary>
		/// 页面坐标.
		/// </summary>
		public int PageIndex
		{
			get { return _PageIndex; }
			set { _PageIndex = value; }
		}

		private int _PageSize = ChartViewStatic.DEFAULT_XCOUNTS;

		/// <summary>
		/// 页面容量.
		/// </summary>
		public int PageSize
		{
			get { return _PageSize; }
			set { _PageSize = value; }
		}

		private int _PageCount;

		/// <summary>
		/// 总页数.
		/// </summary>
		public int PageCount
		{
			get { return _PageCount; }
			set { _PageCount = value; }
		}

		private int _ShowPageIndex;

		public int ShowPageIndex
		{
			get 
			{
				_ShowPageIndex = _PageIndex + 1;
				return _ShowPageIndex; 
			}
		}

	}
}
