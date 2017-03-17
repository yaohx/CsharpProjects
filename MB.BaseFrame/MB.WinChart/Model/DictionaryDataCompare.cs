using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MB.WinChart.Model
{
	/// <summary>
	/// 定义类型为比较两个<c>Dictionary</c>而实现的方法。
	/// </summary>
	public class DictionaryDataCompare : IComparer<Dictionary<string, ArrayList>>
	{
		#region IComparer<Dictionary<string,ArrayList>> 成员

		/// <summary>
		/// <see cref="System.Collections.Generic.IComparer.Compare"/>
		/// </summary>
		public int Compare(Dictionary<string, ArrayList> x, Dictionary<string, ArrayList> y)
		{
			return x.Keys.ElementAt(0).CompareTo(y.Keys.ElementAt(0));
		}

		#endregion
	}
}
