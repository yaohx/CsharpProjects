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
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace DIYReport.ReportModel.RptObj
{
	/// <summary>
	/// ExpressBox 包括字段、参数和表达。
	/// </summary>
	public class RptExpressBox : DIYReport.ReportModel.RptTextObj {
		private string _DataSource;
		private ExpressType _ExpressType;
		private string _FieldName;
		//内部描述字段
		private string _BingDBFieldName;
		private string _ExpressValue;
		/// <summary>
		/// 
		/// </summary>
		public RptExpressBox() : this(null){
		}
		//private readonly string NO_BING_TAG = "[未绑定]";
		public RptExpressBox(string pName):this(pName,null){
		}
		public RptExpressBox(string pName,string pDataSource) : base(pName,DIYReport.ReportModel.RptObjType.Express) {
			if(pDataSource==null){
				_DataSource = DIYReport.Drawing.RptDrawHelper.NO_BING_TAG ; 
			}
			else{
				_DataSource = pDataSource; 
			}
	    }
		#region ICloneable Members

		public  override object Clone() {
			object info = this.MemberwiseClone() as object ;
			return info;
		}
 
		#endregion ICloneable Members

		#region public 属性...
		[Browsable(false)]
		public string BingDBFieldName{
			get{
				//为了兼容老版本的报表格式而设计成这种方式，如果性能上出现问题，再进一步修改
				string temp ;
				if(_BingDBFieldName==null || _BingDBFieldName==""){
					if(_ExpressType == ExpressType.Express){
						temp =  _FieldName ;
					}
					else{
						temp = _DataSource;
					}
				}
				else{
					if(_BingDBFieldName == _FieldName || _BingDBFieldName == _DataSource){
						if(this.Section!=null && this.Section.Report!=null){ 
							string fName = DIYReport.PublicFun.GetFieldNameByDesc(_FieldName,this.Section.Report.DesignField); 
							_BingDBFieldName = fName;
						}
					}
					temp =  _BingDBFieldName;
				}
				return temp;
			}
			set{
				_BingDBFieldName = value;
			}
		}
		[Browsable(false)]
		public string ExpressValue{
			get{
				return _ExpressValue;
			}
			set{
				_ExpressValue = value;
			}
		}
		[Description("在选择统计的时候设置或者得到统计字段的名称。"),Category("数据"),Editor(typeof(DIYReport.Design.RptFieldAttributesEditor), typeof(UITypeEditor))]
		public string FieldName{
			get{
				return _FieldName;
			}
			set{
				_FieldName = value;
				if(DIYReport.UserDIY.DesignEnviroment.IsUserDesign){ 
					//为了兼容老版本的报表格式而设计成这种方式，如果性能上出现问题，再进一步修改
					if(this.Section!=null && this.Section.Report!=null){ 
						string fName = DIYReport.PublicFun.GetFieldNameByDesc(_FieldName,this.Section.Report.DesignField); 
						_BingDBFieldName = fName;
					}
				}
				if(IsEndUpdate){base.OnAfterValueChanged(new DIYReport.ReportModel.RptEventArgs());} 
			}
		}

		[Description("设置或者得到数据源的名称。"),Category("数据"),Editor(typeof(DIYReport.Design.RptDataSourceEditor), typeof(UITypeEditor))]
		public string DataSource{
			get{
				return _DataSource;
			}
			set{
				_DataSource = value;
				if(DIYReport.UserDIY.DesignEnviroment.IsUserDesign){ 
					//为了兼容老版本的报表格式而设计成这种方式，如果性能上出现问题，再进一步修改
					if(_ExpressType== ExpressType.Field){
						if(this.Section!=null && this.Section.Report!=null){ 
							string fName = DIYReport.PublicFun.GetFieldNameByDesc(_FieldName,this.Section.Report.DesignField); 
							_BingDBFieldName = fName;
						}
					}
					if(_ExpressType!= ExpressType.Express){
						_FieldName = "";
					}
				}
				if(IsEndUpdate){base.OnAfterValueChanged(new DIYReport.ReportModel.RptEventArgs());} 
			}
		}
		[Browsable(true),Description("设置或者得到数据源的类型。"),Category("数据")]
		public ExpressType ExpressType{
			get{
				return _ExpressType;
			}
			set{
				_ExpressType = value;
				if(DIYReport.UserDIY.DesignEnviroment.IsUserDesign){ 
					_FieldName = "";
					_DataSource = DIYReport.Drawing.RptDrawHelper.NO_BING_TAG; 
				}
				if(IsEndUpdate){base.OnAfterValueChanged(new DIYReport.ReportModel.RptEventArgs());} 
				
			}
		}

		#endregion public 属性...
	}
}
