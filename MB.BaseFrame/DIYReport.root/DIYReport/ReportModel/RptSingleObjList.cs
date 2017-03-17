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
using System.Drawing ;
using System.Xml ;
using System.Collections ;
using System.Diagnostics ;

using DIYReport.Interface;
namespace DIYReport.ReportModel
{
	#region 比较器...
	class RptObjSortComparer : IComparer  {

		//  
		int IComparer.Compare( Object x, Object y )  {
			RptSingleObj xObj = x as RptSingleObj;
			RptSingleObj yObj = y as RptSingleObj;
			return( (new CaseInsensitiveComparer()).Compare( xObj.Location.X ,yObj.Location.X ) );
		}

	}
	class RptObjSortByTopComparer : IComparer  {
		int IComparer.Compare( Object x, Object y )  {
			RptSingleObj xObj = x as RptSingleObj;
			RptSingleObj yObj = y as RptSingleObj;
			return( (new CaseInsensitiveComparer()).Compare( xObj.Location.Y ,yObj.Location.Y ) );
		}

	}
	#endregion 比较器...
	/// <summary>
	/// RptSingleObjList  描述每一个Section 中的
	/// </summary>
	public class RptSingleObjList : ArrayList{
		private float mHeight;
		private RptSection _Section;

		#region 构造函数...
		public RptSingleObjList(){
		}
		public  RptSingleObjList( RptSection pSection){
			_Section = pSection;
		}

		#endregion 构造函数...

		#region this...
		public new IRptSingleObj this[int pIndex] {
			get {
				return  base[pIndex] as IRptSingleObj;
			}
		}
		#endregion this...

		#region Public 方法(位置大小调整相关)...		
		/// <summary>
		/// 在指定的位置插入一个报表
		/// </summary>
		/// <param name="pRptObj"></param>
		/// <param name="pLocation"></param>
		/// <returns></returns>
		public RptSingleObj InsertByLocation(RptSingleObj pRptObj,Point pLocation){
			foreach(RptSingleObj obj in this){
				if(obj.Location.X >=pLocation.X){
					//在控件插入的指定位置的左边不需要调整，右边调整各个控件的位置
					obj.Location = new Point(obj.Location.X + pRptObj.Size.Width,obj.Location.Y);

				}
			}
			this.Add(pRptObj);
			return pRptObj;
		}

		/// <summary>
		/// 自动调整Section 中各控件(主要针对网格控件打印的情况，而且是单表头的情况，其它时候不需要使用）
		/// 如果打印内容的最右边小于报表的宽度，那么不需要调整。 
		/// </summary>
		public void AutoFitSizeWidth(){
			int maxRight = GetMaxCtlRight();
			int sectionWidth = _Section.Width;
			int ctlCount = this.Count ;
			//先让控件从左到右排列
			this.Sort(new RptObjSortComparer());
			if(maxRight > sectionWidth){
				int setWidth = maxRight - sectionWidth;
				int moveSize = 0;
				for(int i = 0;i <ctlCount;i++){
					RptSingleObj obj = this[i] as RptSingleObj;
					obj.Location = new Point(obj.Location.X - moveSize,obj.Location.Y);
					int sm = System.Convert.ToInt32(obj.Size.Width * setWidth / maxRight)   ;
					moveSize += sm;
					obj.Size = new Size(obj.Size.Width - sm,obj.Size.Height);
				}
			}
		}
		/// <summary>
		/// 通过控件的Top坐标的顺序，排列控件
		/// </summary>
		public void SortByTopOrder(){
			this.Sort(new RptObjSortByTopComparer()); 
		}
		#endregion Public 方法(位置大小调整相关)...

		#region Public 方法(获取信息相关)...
		/// <summary>
		/// 得到控件在Section 中最右边的Right,根据它来控制报表的宽度
		/// </summary>
		/// <returns></returns>
		public int GetMaxCtlRight(){
			int right = 0;
			foreach(RptSingleObj obj in this){
				int newbo = obj.Rect.Right;
				right = right > newbo ? right: newbo ;
			}
			return right;
		}
		/// <summary>
		/// 得到控件在Section 中最底下的Bottom,根据它来控制Section 的最小高度
		/// </summary>
		/// <returns></returns>
		public int GetMaxCtlBottom(){
			int bottom = 0;
			foreach(RptSingleObj obj in this){
				int newbo = obj.Rect.Bottom ;
				bottom = bottom > newbo ? bottom: newbo ;
			}
			return bottom;
		}
		#endregion Public 方法(获取信息相关)...

		#region Public 方法(编辑相关)...
		/// <summary>
		/// 通过指定的类型增加一个
		/// </summary>
		/// <param name="pType"></param>
		/// <returns></returns>
		public IRptSingleObj AddByType( DIYReport.ReportModel.RptObjType pType,DIYReport.ReportModel.RptSection pSection){
			return AddByType(pType,null,pSection);
		}
		public IRptSingleObj AddByType( DIYReport.ReportModel.RptObjType pType,string pDispText,DIYReport.ReportModel.RptSection pSection){
			DIYReport.Interface.IRptSingleObj obj=  DIYReport.RptObjectHelper.CreateObj(pType,pDispText);
			if(obj==null)
				return null;

			obj.Name = getObjNewName(obj.GetType().Name); 
			obj.Section = pSection;
			base.Add(obj);
			return obj;
		}
		public IRptSingleObj Add(IRptSingleObj pClass) {
			base.Add(pClass);
			 
			return pClass;
		}
		#endregion Public 方法(编辑相关)...

		#region Public 属性...

		public float Height {
			get {
				return mHeight;
			}
			set {
				mHeight = value;
			}
		}
		#endregion Public 属性...

		#region 内部处理函数...
		private string getObjNewName(string pBegName){
			string newName = pBegName + "0";
			int i = 1;
			while(i<=this.Count){
				newName = pBegName + i.ToString();
				if(getObjByName(newName)==null){
					break;
				}
				i++;
			}
			return newName;
		}
		private DIYReport.Interface.IRptSingleObj getObjByName(string pName){
			foreach(DIYReport.Interface.IRptSingleObj obj in this){
				if(System.String.Compare( obj.Name ,pName ,true)==0){
					return obj;
				}
			}
			return null;
		}
		#endregion 内部处理函数...
	}
 
}
