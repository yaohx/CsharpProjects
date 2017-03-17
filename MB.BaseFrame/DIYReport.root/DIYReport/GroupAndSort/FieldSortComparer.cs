using System;
using System.Collections ;

namespace DIYReport.GroupAndSort
{
	/// <summary>
	/// FieldSortComparer 的摘要说明。
	/// </summary>
	public class FieldSortComparer : IComparer  {
		// 升序较器
		int IComparer.Compare( Object x, Object y )  {
			RptFieldInfo xObj = x as RptFieldInfo;
			RptFieldInfo yObj = y as RptFieldInfo;
			if(xObj!=null && yObj!=null){
				return( (new CaseInsensitiveComparer()).Compare( xObj.OrderIndex  ,yObj.OrderIndex) );
			}
			else{
				return 0;
			}
		}
	}
}
