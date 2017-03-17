//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-21
// Description	:	 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data ;
using System.ComponentModel;
using System.Collections ;
using System.Drawing.Design;
using System.Windows.Forms.Design;
namespace DIYReport.Express
{
	/// <summary>
	/// ExStatistical 统计字段
	/// </summary>
	public class ExStatistical
	{
		private static int _BeginRowPoint ;
		private static  int _EndRowPoint ;
		private static string _FieldName;
		private static DataRow[] _DRows;
		#region 静态的方法...
		/// <summary>
		/// 获取表达式的值
		/// </summary>
		/// <param name="pBeginRowPoint"></param>
		/// <param name="pEndRowPoint"></param>
		/// <param name="pExpressName"></param>
		/// <param name="pDRows"></param>
		/// <returns></returns>
		/// 
		[Browsable(false)]
		public static string GetStatisticalValue(int pBeginRowPoint , int pEndRowPoint ,string pDataSource,DataRow[] pDRows){
			string fieldName = "";
			string expressName = getStatisticalName(pDataSource,out fieldName);
			return GetStatisticalValue(pBeginRowPoint,pEndRowPoint,expressName,fieldName,pDRows);
		}
		/// <summary>
		/// 获取表达式的值
		/// </summary>
		/// <param name="pBeginRowPoint"></param>
		/// <param name="pEndRowPoint"></param>
		/// <param name="pFieldName"></param>
		/// <param name="pDRows"></param>
		[Browsable(false)]
		public static string GetStatisticalValue(int pBeginRowPoint , int pEndRowPoint ,string pExpressName,string pFieldName,DataRow[] pDRows){

			_BeginRowPoint = pBeginRowPoint;
			_EndRowPoint = pEndRowPoint;
			_DRows = pDRows;
			_FieldName = pFieldName;
			bool b = checkAndGetFieldName();
			if(!b){
				return string.Empty ;//"表达式对应的字段不存在或者类型不正确(目前只支持数字类型的字段).";
			}
			switch(pExpressName){
				case "Avg":
					return Avg().ToString();
				case "Count":
					return Count().ToString();
				case "Max":
					return Max().ToString();
				case "Min":
					return Min().ToString();
				case "Sum":
					return Sum().ToString();
				default:
					return string.Empty;//"不支持的表达式";
			}
		} //
		
		[Description("平均值")]
		public static double Avg(){
			int rowCount = _EndRowPoint - _BeginRowPoint + 1;
			if(rowCount==0){
				return 0;
			}
			double totalVal = 0.00;
			for(int i = _BeginRowPoint ; i < _EndRowPoint + 1; i++){
				DataRow dr = _DRows[i];
				totalVal +=PublicFun.ToDouble(dr[_FieldName]); 
			}
			return  totalVal / rowCount;
		}
		[Description("总记录数")]
		public static int Count(){
			return _EndRowPoint - _BeginRowPoint + 1;
		}
		[Description("最大值")]
		public static double Max(){
			double maxVal = 0.00;
			for(int i = _BeginRowPoint ; i < _EndRowPoint + 1; i++){
				DataRow dr = _DRows[i];
				double val = PublicFun.ToDouble(dr[_FieldName]);
				maxVal = maxVal > val?maxVal:val; 
			}
			return maxVal;
		}
		[Description("最小值")]
		public static double Min(){
			double minVal = 0.00;
			for(int i = _BeginRowPoint ; i < _EndRowPoint + 1; i++){
				DataRow dr = _DRows[i];
				double val = PublicFun.ToDouble(dr[_FieldName]);
				minVal = minVal < val?minVal:val; 
			}
			return minVal;
		}
		[Description("汇总")]
		public static double Sum(){
			double sumVal = 0.00;
			for(int i = _BeginRowPoint ; i < _EndRowPoint + 1; i++){
				DataRow dr = _DRows[i];
				sumVal +=PublicFun.ToDouble(dr[_FieldName]); 
			}
			return sumVal;
		} // 
		#endregion 静态的方法...

		#region 内部处理函数...
		// 通过报表设计绑定的名称得到统计的函数名称  
		private static string getStatisticalName(string pBingDataSource,out string pFieldName){
			int index = pBingDataSource.IndexOf('(');
			pFieldName = pBingDataSource.Substring(index,pBingDataSource.Length - index - 1);
			return pBingDataSource.Substring(1,index - 2);
		}
		//判断分析的字段是否存在或者统计的字段类型是否合法
		private static bool  checkAndGetFieldName(){
			if(_DRows.Length == 0){
				return true;
			}
			bool b = _DRows[0].Table.Columns.Contains(_FieldName);
			if(b){
				string fieldName = PublicFun.GetFieldNameByDesc( _DRows[0].Table, _FieldName);
				if(fieldName==null || fieldName.Length ==0)
					return false;
				_FieldName = fieldName;
				System.Type dataType = _DRows[0].Table.Columns[_FieldName].DataType;
				string typeName = dataType.Name;
				//日期型的数据类型先不处理
				if(typeName == "Int16" || typeName == "Int32" || typeName == "Decimal"){
					return true;
				}
			}
			return false;

		}

		#endregion 内部处理函数...

	}
}
