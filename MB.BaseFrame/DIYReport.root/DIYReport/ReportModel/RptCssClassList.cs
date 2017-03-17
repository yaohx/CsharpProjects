//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-15
// Description	:	 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.Xml;
using System.Windows.Forms;

 
namespace DIYReport.ReportModel
{
	/// <summary>
	/// RptCssClassList 的摘要说明。
	/// </summary>
	public class RptCssClassList :  Hashlist   {
		public RptCssClassList(){
		}
		public RptCssClassList(XmlNodeList pNodeList) {
			//string pName,int pSize,Color pFontColor,bool pFontBold,Color pBackGround ,
			//bool pWordWrap,bool pShowFrame,string pAlignment
			foreach(XmlNode node in pNodeList) {
				RptCssClass css = new RptCssClass(node); 
				this.Add(css);
			}
		}
		#region this...
		public new RptCssClass this[string key] {
			get {
				if(base[key]!=null){
					return  base[key] as RptCssClass;
				}
				else{
					return null;
				}
			}
		}
		#endregion this...

		#region Public 方法...
		public RptCssClass GetCssByName(string pName){
			foreach(RptCssClass css in this.Values ){
				if(css.Name == pName){
					return css;
				}
			}
			return null;
		}
		/// <summary>
		/// 得到新增加的cssClass 的名称
		/// </summary>
		/// <returns></returns>
		public string GetNewCssName(){
			string sName = "";
			int i =1;
			while(i>0){
				sName = "cssClass" + i.ToString();
				if(this[sName]==null)
					break;
				i++;
			}
			return sName;
		}
		public RptCssClass Add(RptCssClass pClass) {
			this.Add(pClass.Name,pClass);
			return pClass;
		}
		#endregion Public 方法...
	}
}
