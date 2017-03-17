//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2005-04-19
// Description	:	在分组处理中对于指定的类型（根据什么方式来排序和分组）。
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;

namespace DIYReport.GroupAndSort
{
	/// <summary>
	/// GroupDivide 在分组处理中对于指定的类型（根据什么方式来排序和分组）。
	/// </summary>
	public class GroupDivide
	{
		/// <summary>
		/// 得到分组间隔的描述信息
		/// </summary>
		/// <param name="pType"></param>
		/// <returns></returns>
		public static string[] GetDivideTextByType(string pType){
			string[] vals = null;
			switch(pType){
				case "String":
					vals = new string[]{"普通","第一个字母","两个首写字母","三个首写字母","四个首写字母","五个首写字母"};
					break;
				case "DateTime":
					vals = new string[]{"普通","按年","按季","按月","按周","按天","按小时","按分钟"};
					break;
				case "Decimal":
				case "Int16":
				case "Int32":
				case "Int64":
					vals = new string[]{"普通","10s","50s","100s","500s","1000s","5000s","10000s"};
					break;
				default:
					vals = new string[]{"普通","第一个字母","两个首写字母","三个首写字母","四个首写字母","五个首写字母"};
					break;
			}
			return vals;
		}

		
	}
}
