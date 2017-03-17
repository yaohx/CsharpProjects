using System;
using System.Data;

namespace MB.WinEIDrive
{
	/// <summary>
	/// DataHelpers 数据处理。
	/// </summary>
	public class DataHelpers
	{
		#region private construct function...
		/// <summary>
		/// 
		/// </summary>
		private DataHelpers(){
		}
		#endregion private construct function...

		/// <summary>
		/// 把任意类型转换为DataView 的格式。
		/// </summary>
		/// <param name="pObj"></param>
		/// <returns></returns>
		public static  DataView  ToDataView(object objData){
			string name = objData.GetType().Name ;
			DataView dv = null;
			switch(name){
				case "DataSet":
					DataSet ds = objData as DataSet ;
					dv = ds.Tables[0].DefaultView;
					break;
				case "DataTable":
					dv = (objData as DataTable).DefaultView  ;
					break;
				case "DataView":
					dv =  objData as DataView ;
					break;
				default:
					throw new Exception("数据源目前不支持"+ name +"类型的数据.");
			}
			return dv;
		}
	}
}
