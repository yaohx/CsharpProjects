//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2006-09-20.
// Description	:	MyCalculate 表达式计算。
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data;
using System.Collections;

namespace DIYReport.Express
{
	/// <summary>
	/// MyCalculate 表达式计算。
	/// </summary>
	public class MyCalculate {
		
		#region private construct function...
		/// <summary>
		/// private construct function。
		/// </summary>
		private  MyCalculate() {

		}
		#endregion private construct function...
		
		#region public 函数...
		//中序转换成后序表达式再计算
		// 如：23+56/(102-100)*((36-24)/(8-6))
		// 转换成：23|56|102|100|-|/|*|36|24|-|8|6|-|/|*|+"
		//以便利用栈的方式都进行计算。
		public static string  CalculateParenthesesExpression(string expression) {
			ArrayList operatorList = new ArrayList();
			string operator1;
			string expressionString = "";
			string operand3;
			expression = expression.Replace(" ","");
			while(expression.Length > 0) {
				operand3 = "";
				//取数字处理
				if(Char.IsNumber(expression[0])) {
					while(Char.IsNumber(expression[0]) || expression[0].Equals('.')) {
						operand3 += expression[0].ToString() ;
						expression = expression.Substring(1);
						if(expression == "")break;
					}
					expressionString += operand3 + "|";
				}

				//取“C”处理
				if(expression.Length >0 && expression[0].ToString() == "(") {
					operatorList.Add("("); 
					expression = expression.Substring(1);
				}

				//取“)”处理
				operand3 = "";
				if(expression.Length >0 && expression[0].ToString() == ")") {
					do {
      
						if(operatorList[operatorList.Count -1].ToString() != "(") {
							operand3 += operatorList[operatorList.Count -1].ToString() + "|" ;
							operatorList.RemoveAt(operatorList.Count - 1) ;
						}
						else {
							operatorList.RemoveAt(operatorList.Count - 1) ;
							break;
						}
      
					}while(true);
					expressionString += operand3;
					expression = expression.Substring(1);
				}

				//取运算符号处理
				operand3 = "";
				if(expression.Length ==0)break;
				if(expression[0].ToString() == "*" || expression[0].ToString() == "/" || expression[0].ToString() == "+" || expression[0].ToString() == "-"){
					operator1 = expression[0].ToString();
					if(operatorList.Count>0) {
       
						if(operatorList[operatorList.Count -1].ToString() == "(" || verifyOperatorPriority(operator1,operatorList[operatorList.Count - 1].ToString())) {
							operatorList.Add(operator1);
						}
						else {
							operand3 += operatorList[operatorList.Count - 1].ToString() + "|";
							operatorList.RemoveAt(operatorList.Count - 1);
							operatorList.Add(operator1);
							expressionString += operand3 ;
       
						}
      
					}
					else {
						operatorList.Add(operator1);
					}
					expression = expression.Substring(1);
				}
				else if(!Char.IsNumber(expression[0]) && !expression[0].Equals('.') && !expression[0].Equals('-') ){
					throw new Exception("不是有效的表达式!"); 
				}
			}

			operand3 = "";
			while(operatorList.Count != 0) {
				operand3 += operatorList[operatorList.Count -1].ToString () + "|";
				operatorList.RemoveAt(operatorList.Count -1);
			} 

			expressionString += operand3.Substring(0, operand3.Length -1);  ;
   

			return CalculateParenthesesExpressionEx(expressionString);

		}
		#endregion public 函数...

		#region 内部函数处理...
		// 第二步:把转换成后序表达的式子计算
		//23|56|102|100|-|/|*|36|24|-|8|6|-|/|*|+"
		private static string  CalculateParenthesesExpressionEx(string expression) {
			//定义两个栈
			ArrayList operandList =new ArrayList();
			float operand1;
			float operand2;
			string[] operand3;
  
			expression = expression.Replace(" ","");
			operand3 = expression.Split(Convert.ToChar("|"));
 
			for(int i = 0;i < operand3.Length;i++) {
				if(Char.IsNumber(operand3[i],0)) {
					operandList.Add( operand3[i].ToString());
				}
				else {
					//两个操作数退栈和一个操作符退栈计算
					operand2 =(float)Convert.ToDouble(operandList[operandList.Count-1]);
					operandList.RemoveAt(operandList.Count-1); 
					operand1 =(float)Convert.ToDouble(operandList[operandList.Count-1]);
					operandList.RemoveAt(operandList.Count-1);
					operandList.Add(calculate(operand1,operand2,operand3[i]).ToString()) ;
				}
    
			}


			return operandList[0].ToString();
		}

 

		//判断两个运算符优先级别
		private static bool verifyOperatorPriority(string Operator1,string Operator2) {
   
			if(Operator1=="*" && Operator2 =="+")
				return true;
			else if(Operator1=="*" && Operator2 =="-")
				return true;
			else if(Operator1=="/" && Operator2 =="+")
				return true;
			else if(Operator1=="/" && Operator2 =="-")
				return true;
			else
				return false;
		}

		//计算
		private static  float calculate(float operand1, float operand2,string operator2) {
			switch(operator2) {
				case "*":
					operand1 *=  operand2;
					break;
				case "/":
					operand1 /=  operand2;
					break;
				case "+":
					operand1 +=  operand2;
					break;
				case "-":
					operand1 -=  operand2;
					break;
				default:
					break;
			}
			return operand1;
		}
		#endregion 内部函数处理...

	}
	/// <summary>
	/// 包含字段的表达式计算。
	/// </summary>
	public class MyCalculateDataRow{
		private static readonly string CONST_OPERATE_STR = @"+-/* ()";
		private static readonly string[] Can_Process_Type_Array = new string[]{"Int16","Int32","Int64","Decimal","Double","Single"};
		
		#region private construct function...
		/// <summary>
		/// private construct function...
		/// </summary>
		private MyCalculateDataRow(){

		}
		#endregion private construct function...

		#region public 静态方法...
		/// <summary>
		/// 计算包含字段的表达式。
		/// </summary>
		/// <param name="dr"></param>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static string CalculateExpression(DataRow dr,string expression){
			expression = replaceFieldNameAsValue(dr,expression);
			//Console.WriteLine("值转换后的表达式为:" + expression);
			return MyCalculate.CalculateParenthesesExpression(expression); 
		}
		/// <summary>
		/// 把字段的Caption 转换为字段的形式。
		/// </summary>
		/// <param name="dcs"></param>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static string ReplaceCaptionAsFieldName(DataColumnCollection dcs,string expression){
			IList descs = getExpression(expression);
			foreach(string desc in descs){
				for(int i  = 0; i < dcs.Count ; i++){
					DataColumn dc = dcs[i];
					if(desc.Equals("@" + dc.Caption)){
						expression = expression.Replace (desc,"@" + dc.ColumnName); 
						break;
					}
//					if(i == dcs.Count -1)
//						throw new Exception("对应的字段在数据源中找不到！"); 
				}
			}
			return expression;

		}
		#endregion public 静态方法...

		#region 内部函数处理...
		private static string replaceFieldNameAsValue(DataRow dr,string expression){
			IList fields = getExpression(expression);
			DataColumnCollection dcs = dr.Table.Columns;
			foreach(string fieldName in fields){
				string name = fieldName.Remove(0,1);
				if(!dcs.Contains(name))  
					return "Expression Error";
					//throw new Exception("对应的字段在数据源中找不到！"); 
				DataColumn dc = dcs[name];
				string typeName = dc.DataType.Name;
				//目前先处理 类型
				if(Array.IndexOf(Can_Process_Type_Array,typeName) < 0)
					throw new Exception("目前表达式对应列的数据类型只支持Int16,Int32,Int64,Decimal,Double,Single,对应的字段类型出错。"); 
				string val = dr[name].ToString();
				if(val!=null && val.Length > 2){
					if(val[0].Equals('-'))//判断是否为负数，然后转换为0 - 正数的格式。
						val = "(0 - " + val.Remove(0,1) + ")"; 

				}
				expression = expression.Replace (fieldName,val); 
			}
			return expression;

		}
		private  static IList getExpression(string str){
			ArrayList lst = new ArrayList();
			int begin = -1;
			int end = -1;
			for(int i = 0; i< str.Length ; i++){
				if(str[i].Equals('@')){
					begin = i;
				}
				if(begin == -1) continue;
				if(CONST_OPERATE_STR.IndexOf(str[i]) > -1){
					end = i;
				}
				if(end == -1){
					if(i==str.Length-1) 
						end = str.Length ;
					else
						continue;
				}

				string temp = str.Substring(begin,end - begin);
				lst.Add(temp);
				begin = -1;
				end = -1;

			}
			return lst;
		}
		#endregion 内部函数处理...

	}

}
