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

namespace DIYReport.Express
{
	/// <summary>
	/// ExpressValues 设置或者得到XML报表配置文件中的表达式。
	/// 默认属性是 Value
	/// [DefaultMemberAttribute("Value")]
	/// </summary>
	public class ExpressValue {
		#region 变量定义...
		private string _Name;
		private string _Value =  "0" ;
		private string _FieldName ;
		private DIYReport.ReportModel.RptSingleObj   _RptObj;
		#endregion 变量定义...

		#region 构造函数...
		public ExpressValue(DIYReport.ReportModel.RptSingleObj   pObj) {
			_Name = pObj.Name;
			_RptObj = pObj;
			if(RptObj!=null){
				_FieldName = RptObj.FieldName ;
			}
			else{
				_FieldName = "";
			}
		}
		#endregion 构造函数...

		#region 操作符重载...
		public static ExpressValue operator+( ExpressValue pValue,object pObj) {
			double val = PublicFun.ToDouble(pValue.Value);
			double addVal = PublicFun.ToDouble(pObj); 
			if(pValue.RptObj !=null){
				string exName = pValue.RptObj.DataSource;

				val += PublicFun.ToDouble(pObj); 
				//pValue.Value = val.ToString("F"); 
				pValue.Value = val.ToString(); 
			}
			return pValue;
		}
		#endregion 操作符重载...

		#region Public 属性...
		public DIYReport.ReportModel.RptObj.RptExpressBox    RptObj{
			get{
				return _RptObj as DIYReport.ReportModel.RptObj.RptExpressBox ;
			}
		}
		public string FieldName{
			get{
				return _FieldName;
			}
		}
		public string Name {
			get {
				return _Name;
			}
			set {
				_Name = value;
			}
		}
		public  string  Value {
			get {
				return _Value;
			}
			set {
				_Value = value;
			}
		}

		#endregion Public 属性...





	}
}
