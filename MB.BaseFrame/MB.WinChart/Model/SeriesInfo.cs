using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinChart.Model
{
	/// <summary>
	/// 分类信息.
	/// </summary>
	public class SeriesInfo
	{
		private string _Name;

		/// <summary>
		/// 名称.
		/// </summary>
		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
			}
		}

		private string _Description;

		/// <summary>
		/// 描述.
		/// </summary>
		public string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				_Description = value;
			}
		}

		private List<PointInfo> _Points;

		/// <summary>
		/// 坐标信息集合.
		/// </summary>
		public List<PointInfo> Points
		{
			get
			{
				return _Points;
			}
			set
			{
				_Points = value;
			}
		}
	}
}
