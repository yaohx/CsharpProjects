//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-15
// Description	:	报表的数据结构和绘制信息描述
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Drawing ;
using System.Data;
using System.Collections; 
using System.ComponentModel ;
using System.Drawing.Printing;
using System.Drawing.Design;
using System.Windows.Forms.Design;
 

namespace DIYReport.ReportModel
{
	/// <summary>
	/// RptReport 报表结构描述。
	/// </summary>
	public class RptReport
	{
		public const int DEFAULT_SELCTION_HEIGHT = 68;
		private System.Guid _ID;
		private  string _Name;
		private string _Text;
		private PaperSize  _PaperSize;
		private string _PaperName;
		private System.Drawing.Printing.PaperKind _PaperKind;

		private Margins _Margins;
		private RptSectionList _SectionList;

		private bool _IsEndUpdate  ;
		
		private string _PrintName;
		private PrintDocument _PrintDocument;
		private bool _FillNULLRow ;//判断是否要填充空行。
		private bool _IsLandscape;
		private string _RptFilePath;

		//add by Nick 2005-01-27 
		private GroupLayoutType _GroupLayoutType;//分组布局显示的方式
		private string _DetailOrderString ;//Detail 数据区显示的排列方式
		// add by nick 2006-04-03 (非存储的字段,根据用户对应的功能模块来获取)
		private object _DataSource;//报表对应的数据源，可以是多个table 的形式
		private IList _DesignField;
		private DIYReport.ReportModel.RptParamList _UserParamList ;  
		
		private int _ReportDataWidth;//报表数据打印或者浏览真正的宽度
		private int _Width;//报表的宽
		private int _Height;//报表高

		private Hashtable _SubReports;
		private DIYReport.Interface.ISubReportCommand _SubReportCommand;
		private string _Tag;
		private System.Guid _IDEX;//由于之前的ID存储重复的值
		#region 自定义事件...
		private RptEventHandler _AfterValueChanged;
		public event RptEventHandler AfterValueChanged{
			add{
				_AfterValueChanged +=value;
			}
			remove{
				_AfterValueChanged -=value;
			}
		}
		protected void OnAfterValueChanged(RptEventArgs arg){
			if(_AfterValueChanged!=null){
				_AfterValueChanged(this,arg);
			}

		}
		#endregion 自定义事件...

		#region 构造函数...
		public RptReport()
		{    
			_PrintDocument = new PrintDocument();

			_Margins = _PrintDocument.DefaultPageSettings.Margins ;
			_PaperSize = _PrintDocument.DefaultPageSettings.PaperSize ; 

			if((_PaperSize.Width < _Margins.Left + _Margins.Right) || (_PaperSize.Height < _Margins.Top + _Margins.Bottom)) {
				_PaperSize =  DIYReport.Print.RptPageSetting.GetPaperSizeByName(_PrintDocument,null,"A4"); 
			}
			//_ReportDataWidth = _PaperSize.Width - _Margins.Left - _Margins.Right ;

			_IsLandscape = _PrintDocument.DefaultPageSettings.Landscape ;

			_IsEndUpdate = true;
			_FillNULLRow = false;
			_SectionList  = new RptSectionList();
			_SectionList.Report = this;

			_SubReports = new Hashtable();
		}
		#endregion 构造函数...

		#region 扩展的Public 方法...

		public void BeginUpdate(){
			_IsEndUpdate = false;
		}
		public void EndUpdate(){
			_IsEndUpdate = true;
			OnAfterValueChanged(new RptEventArgs());
		}
		/// <summary>
		/// 根据section 中所有控件的top 坐标排列绘制的先后顺序
		/// </summary>
		public void SortCtlByTop(){
			if(_SectionList==null)
				return;
			foreach(RptSection section in _SectionList){
				section.RptObjList.SortByTopOrder();  
			}
		}
		#endregion 扩展的Public 方法...

		#region Public 方法...
		/// <summary>
		/// 新建一张新的报表
		/// </summary>
		public static RptReport NewReport(){
			return NewReport(null,System.Guid.NewGuid());
		}
		public static RptReport NewReport(string pReportName,System.Guid pRptID){
			RptReport  report = new RptReport();
			report.ID = pRptID;
			if(pReportName!=null && pReportName!=""){
				report.Name = pReportName;
			}
			else{
				report.Name = "Bank-Report" + System.DateTime.Now.ToShortDateString() + System.DateTime.Now.Millisecond.ToString() ;
			}
			//PageSettings ps = DIYReport.Print.RptPageSetting.GetDefaultPageSetting();
			int paperWidth = report.IsLandscape?report.PaperSize.Height:report.PaperSize.Width ;
			int reportDetailWidth = paperWidth- report.Margins.Left - report.Margins.Right ;
		 
			DIYReport.ReportModel.RptSection section = new DIYReport.ReportModel.RptSection(DIYReport.SectionType.Detail);
			section.Width = reportDetailWidth;    
			section.Report = report;
			section.Height = 3 * DEFAULT_SELCTION_HEIGHT;
			report.SectionList.Add(section);
            report.Width = report.PaperSize.Width;
            report.Height = report.PaperSize.Height;
			return report;
		}
		/// <summary>
		/// 构造一个新的报表对象。
		/// </summary>
		/// <param name="dataSource">数据需要的数据源。</param>
		/// <param name="userParamList">报表参数列表，可以为空</param>
		/// <param name="reportData">系列化为XML文档的报表数据，为空的话那么将创建只有一个detail section 的空报表。</param>
		public static RptReport CreateReport(DataSet dataSource,DIYReport.ReportModel.RptParamList userParamList,byte[] reportXmlData){

			string reportStr = string.Empty ;
			if(reportXmlData!=null && reportXmlData.Length > 0)
				reportStr = System.Text.Encoding.UTF8.GetString(reportXmlData); 
			 
			return CreateReport(dataSource,userParamList,reportStr);

		}
		/// <summary>
		/// 构造一个新的报表对象。
		/// </summary>
		/// <param name="dataSource">数据需要的数据源。</param>
		/// <param name="userParamList">报表参数列表，可以为空</param>
		/// <param name="reportData">系列化为XML文档的报表数据，为空的话那么将创建只有一个detail section 的空报表。</param>
		public static RptReport CreateReport(DataSet dataSource,DIYReport.ReportModel.RptParamList userParamList,string reportXmlData){
			RptReport  report = null;
			if(reportXmlData==null || reportXmlData.Length ==0){
				report = NewReport();
			}
			else{
				report = DIYReport.ReportReader.Instance().ReadFromXmlString(reportXmlData);   
			}
			report.DataSource  = dataSource;
			report.UserParamList = userParamList;
			if(report.ReportDataWidth==0)
				report.ReportDataWidth = report.PaperSize.Width - report.Margins.Left - report.Margins.Right ;
			return report;
		}
		#endregion Public 方法...

		#region Public 属性...
		/// <summary>
		/// 设置或者获取子报表操作的控制对象。(不永久性存储)
		/// </summary>
		[Browsable(false)]
		public DIYReport.Interface.ISubReportCommand SubReportCommand{
			get{
				return _SubReportCommand;
			}
			set{
				_SubReportCommand = value;
			}
		}
		/// <summary>
		/// 报表对应的子报表。(不永久性存储)
		/// </summary>
		[Browsable(false)]
		public Hashtable  SubReports{
			get{
				return _SubReports;
			}
			set{
				_SubReports = value;
			}
		}
		[Browsable(false)]
		public string PaperName{
			get{
				return _PaperName;
			}
			set{
				_PaperName = value;
			}
		}
		[Browsable(false)]
		public System.Drawing.Printing.PaperKind PaperKind{
			get{
				return _PaperKind;
			}
			set{
				_PaperKind = value;
			}
		}

		[Browsable(false)]
		public string DetailOrderString{
			get{
				return _DetailOrderString;
			}
			set{
				_DetailOrderString = value;
			}
		}
		[Browsable(false)]
		public GroupLayoutType GroupLayoutType{
			get{
				return _GroupLayoutType;
			}
			set{
				_GroupLayoutType = value;
			}
		}
		public bool IsLandscape{
			get{
				return _IsLandscape;
			}
			set{
				_IsLandscape = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		public bool FillNULLRow{
			get{
				return _FillNULLRow;
			}
			set{
				_FillNULLRow = value;
			}
		}
		/// <summary>
		/// 得到打印的文档对象
		/// </summary>
    //	[Obsolete("该属性已不再使用。")]
		public PrintDocument PrintDocument{
			get{
				if(_PrintDocument == null){
					_PrintDocument = new PrintDocument();
				}
				return _PrintDocument;
			}
			set{
				_PrintDocument = value;
			}
		}
		[Browsable(false)]
		public string RptFilePath{
			get{
				return _RptFilePath;
			}
			set{
				_RptFilePath = value;
			}
		}
		[Browsable(false)]
		public RptSectionList SectionList{
			get{
				return _SectionList;
			}
			set{
				_SectionList = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[ReadOnly(true),Description("报表页面设置"),Category("外观")]
		public PaperSize PaperSize{
			get{ 
				return _PaperSize;
			}
			set{
				_PaperSize = value;
				_PaperKind = PaperSize.Kind ;
				_Width = _PaperSize.Width;
				_Height = _PaperSize.Height;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Browsable(false)]
		public int Width{
			get{ 
				return _Width;
			}
			set{
				_Width = value;
			}
		}
		[Browsable(false)]
		public int Height{
			get{ 
				return _Height;
			}
			set{
				_Height = value;
			}
		}
		[ReadOnly(true),Description("报表数据绘制的最大宽度，超过部分将不绘制。"),Category("外观")]
		public int ReportDataWidth{
			get{
				return _ReportDataWidth;
			}
			set{
				_ReportDataWidth = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
        [Description("设置或者需要指定的打印机名称"), Category("打印"), Editor(typeof(DIYReport.Design.EnumPrinterListEditor), typeof(UITypeEditor))]
		public string PrintName{
			get{
				return _PrintName;
			}
			set{
				_PrintName = value;
			}
		}
		[ReadOnly(true),Description("得到或者设置报表的边距"),Category("外观")]
		public Margins Margins{
			get{ 
				return _Margins;
			}
			set{
				_Margins = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Browsable(true),Description("报表名称-->该名称作为显示打印和浏览该报表的标志。"),Category("描述")]
		public string Name{
			get{
				return _Name;
			}
			set{
				_Name = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Browsable(true),Description("报表描述-->描述该报表体现的功能和作用。"),Category("描述")]
		public string Text{
			get{
				return _Text;
			}
			set{
				_Text = value;
				if(_IsEndUpdate){OnAfterValueChanged(new RptEventArgs());}
			}
		}
		[Browsable(false)]
		public System.Guid ID{
			get{
				if(_ID == System.Guid.Empty){
					_ID = System.Guid.NewGuid();
				}
				return _ID;
			}
			set{
				_ID = value;

			}
		}
		[Browsable(true)]
		public System.Guid IDEX{
			get{
				if(_IDEX == System.Guid.Empty){
					_IDEX = System.Guid.NewGuid();
				}
				return _IDEX;
			}
			set{
				_IDEX = value;

			}
		}
		[Browsable(false)]
		public object DataSource{
			get{
				return _DataSource;
			}
			set{
				_DataSource = value;
				_DesignField = DIYReport.PublicFun.ToDesignField(_DataSource);   
			}
		}
		[Browsable(false)]
		public IList DesignField{
			get{
				if(_DesignField==null)
					_DesignField = DIYReport.PublicFun.ToDesignField(_DataSource); 
				return _DesignField;
			}
			set{
				_DesignField = value;
			}
		}
		[Browsable(false)]
		public DIYReport.ReportModel.RptParamList UserParamList{
			get{
				return _UserParamList;
			}
			set{
				_UserParamList = value;
			}
		}
		[Browsable(false)]
		public string Tag{
			get{
				return _Tag;
			}
			set{
				_Tag = value;
			}
		}
		#endregion Public 属性...
	}
}
