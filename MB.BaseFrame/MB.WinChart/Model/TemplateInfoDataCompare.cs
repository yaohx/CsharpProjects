using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinChart.Model
{
	/// <summary>
	/// 定义类型为比较两个<c>TemplateInfo</c>而实现的方法。
	/// </summary>
	public class TemplateInfoDataCompare : IComparer<TemplateInfo>
	{
		#region IComparer<TemplateInfo> 成员

		/// <summary>
		/// <see cref="System.Collections.Generic.IComparer.Compare"/>
		/// </summary>
		public int Compare(TemplateInfo x, TemplateInfo y)
		{
			return x.UpdateTime.CompareTo(y.UpdateTime);
		}

		#endregion
	}
}
