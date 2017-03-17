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
using System.Collections ;
using System.Drawing ;

using DIYReport;
namespace DIYReport.UserDIY
{
	/// <summary>
	/// DesignSectionList 定义报表的Section
	/// </summary>
	public class DesignSectionList : ArrayList 
	{
		#region 变量定义...
		private DIYReport.Interface.IDesignPanel   _Report;
		private DIYReport.ReportModel.RptSectionList  _DataObj; 
		#endregion 变量定义...

		#region 自定义事件...
		private DesignSectionEventHandler _BeforeRemoveSection;
		private DesignSectionEventHandler _AfterInsertSection;
		private DesignSectionEventHandler _AfterRefreshLayout;
		public event DesignSectionEventHandler BeforeRemoveSection{
			add{
				_BeforeRemoveSection +=value;
			}
			remove{
				_BeforeRemoveSection -=value;
			}
		}
		public event DesignSectionEventHandler AfterInsertSection{
			add{
				_AfterInsertSection +=value;
			}
			remove{
				_AfterInsertSection-=value;
			}
		}
		public event DesignSectionEventHandler AfterRefreshLayout{
			add{
				_AfterRefreshLayout +=value;
			}
			remove{
				_AfterRefreshLayout-=value;
			}
		}
		private void OnBeforeRemoveSection(DesignSectionEventArgs arg){
			if(_BeforeRemoveSection!=null){
				_BeforeRemoveSection(this,arg);
			}

		}
		private void OnAfterInsertSection(DesignSectionEventArgs arg){
			if(_AfterInsertSection!=null){
				_AfterInsertSection(this,arg);
			}

		}
		private void OnAfterRefreshLayout(DesignSectionEventArgs arg){
			if(_AfterRefreshLayout!=null){
				_AfterRefreshLayout(this,arg);
			}

		}
		#endregion 自定义事件...

		#region 构造函数...
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pReport"></param>
		public DesignSectionList(DIYReport.Interface.IDesignPanel  pReport){
			_Report = pReport;
		}
		#endregion 构造函数...
	
		#region Public 属性...
		public DIYReport.Interface.IDesignPanel   Report{
			get{
				return _Report;
			}
			set{
				_Report = value;
			}
		}
		public DIYReport.ReportModel.RptSectionList DataObj{
			get{
				return _DataObj; 
			}
		}
		#endregion Public 属性...

		#region public 方法...
		/// <summary>
		/// 创建一个新的Section List
		/// </summary>
		public void CreateNewSectionList(){
			_DataObj = _Report.DataObj.SectionList ; 

			foreach(DIYReport.ReportModel.RptSection dataSection in  _DataObj){
				DesignSection section = new DesignSection(dataSection); 
				section.IsDisplay = true;
				SectionCaption caption = new SectionCaption();
				section.CaptionCtl = caption;
				this.Add(section);
			}
			RefreshDesignLayout();
			_DataObj.BeforeRemoveSection +=new DIYReport.ReportModel.RptSectionEventHandler(_DataObj_BeforeRemoveSection);
			_DataObj._fterCreateNewSection+=new System.EventHandler(_DataObj_AfterCreateNewSection);
		}

		#region 重新刷新报表设计中Section 的显示...
//		/// <summary>
//		/// section 的大小和位置调整
//		/// </summary>
//		/// <param name="pSection"></param>
//		public void ReizeSectionsLocation(DesignSection pSection){
//			int height = 0;
//			int captionHeight = SectionCaption.CAPTION_HEIGHT;
//			foreach(DesignSection section in this ){
//				if(pSection==null || (int)section.SectionType > (int)pSection.SectionType){
//					section.Location = new Point(0,height + captionHeight);
//					section.CaptionCtl.Location = new Point(0,height );
//				}
//				height += section.Height + captionHeight ;
//			}
//		}
		/// <summary>
		/// 得到所有Design Section 的总体高度
		/// </summary>
		/// <returns></returns>
		public int GetDesignHeight(){
			int height = 0;
			foreach(DesignSection section in this){
				if(section.IsDisplay){
					height +=section.Height +  SectionCaption.CAPTION_HEIGHT  ;
				}//
			}
			return height;
		}
		// 重新布置Design Section 的显示
		public void RefreshDesignLayout(){
			int height = 0;
			int drawY = 0;
			int paperWidth = _Report.DataObj.IsLandscape?_Report.DataObj.PaperSize.Height:_Report.DataObj.PaperSize.Width ;
			int width = paperWidth - _Report.DataObj.Margins.Left - _Report.DataObj.Margins.Right ;
			
			foreach(DIYReport.ReportModel.RptSection rptSection in this.DataObj){
				DesignSection section = getDesignSectionByDataSection(rptSection);
				if(section==null)
					continue;
				//DIYReport.TrackEx.Write(section!=null,"在调整DesignLayout是时候出现根据rptsection 获取 designsection 为空的情况。"); 
				bool b = section.IsDisplay;
				section.Visible = b;
				section.CaptionCtl.Visible = b;
				section.DataObj.DrawLocationY = drawY;
				if(b){
					section.BringToFront();
					section.CaptionCtl.BringToFront();
					section.CaptionCtl.Location = new Point(0,height);
					section.Location = new Point(0,height +  SectionCaption.CAPTION_HEIGHT );
					section.Width = width;
					section.CaptionCtl.Width =  width;
					height +=section.Height +  SectionCaption.CAPTION_HEIGHT;
					drawY +=section.Height;
				}//
				
			}
			OnAfterRefreshLayout((DesignSectionEventArgs)null); 
		}
		private DesignSection getDesignSectionByDataSection(DIYReport.ReportModel.RptSection rptSection){
			foreach(DesignSection section in this){
				if(section.DataObj.Equals(rptSection))
					return section;
			}
			return null;
		}
		#endregion 重新刷新报表设计中Section 的显示...
		/// <summary>
		/// 得到当前正在活动状态的Section 
		/// </summary>
		/// <returns></returns>
		public DesignSection GetActiveSection(){
			foreach(DesignSection section in this ){
				if(section.IsActive){
					return section;
				}
			}
			return null;
		}
		/// <summary>
		/// 设置Section 是否在选择的状态
		/// </summary>
		/// <param name="pSection"></param>
		public void SetActiveSection(DesignSection pSection){
			foreach(DesignSection section in this ){
				if((int)section.SectionType == (int)pSection.SectionType){
					if(pSection.SectionType == DIYReport.SectionType.GroupFooter || pSection.SectionType == DIYReport.SectionType.GroupHead){
						if(pSection.DataObj.GroupField == section.DataObj.GroupField){
							section.IsActive = true;
							continue;
						}
					}
					else{
						section.IsActive = true;
						continue;
					}
				}

				if(section.IsActive){
					section.DesignControls.SetAllNotSelected(); 
				}
				section.IsActive = false;

			}
		}
		/// <summary>
		/// 让其它Section 的所有控件在不选择状态
		/// </summary>
		public void SetOtherSectionAllNotSelected(DesignSection pSection){
			foreach(DesignSection section in this ){
				if((int)section.SectionType != (int)pSection.SectionType){
					section.DesignControls.SetAllNotSelected(); 
				}
			}
		}
		/// <summary>
		/// 同时设置所有section 的设计控件
		/// </summary>
		/// <param name="pShow"></param>
		public void ShowFocusHandle(bool pShow){
			foreach(DesignSection section in this ){
				section.DesignControls.ShowFocusHandle(pShow); 
			}
		}
		/// <summary>
		/// 通过Data Section List 设置 Design Section List 的数据信息
		/// </summary>
		/// <param name="pDataSectionList"></param>
		public void SetDataSectionList(DIYReport.ReportModel.RptSectionList pDataSectionList ){
			foreach(DIYReport.ReportModel.RptSection  section in pDataSectionList){
				DesignSection dse = GetSectionByType(section.SectionType);
				if(dse!=null){
					dse.SetDataSection(section);
				}
			}
			//ReizeSectionsLocation( (DesignSection)null );
			RefreshDesignLayout(); 
		}
		/// <summary>
		/// 根据报表设计的Section 得到
		/// </summary>
		/// <returns></returns>
		public DIYReport.ReportModel.RptSectionList GetDataSectionList(){
//			DIYReport.ReportModel.RptSectionList sList = new DIYReport.ReportModel.RptSectionList();
//			foreach(DesignSection section in this){
//				DIYReport.ReportModel.RptSection rpt = new DIYReport.ReportModel.RptSection();
//				rpt.Height = section.Height  ;
//				rpt.Width = section.Width ;
//				rpt.SectionType = section.SectionType ;
//				rpt.Visibled = section.IsDisplay ;
//				sList.Add(rpt);
//			}
			return _DataObj ;
		}
		
		public DesignSection GetSectionByType(DIYReport.SectionType pType){
			foreach(DesignSection section in this){
				if(section.SectionType == pType){
					return section;
				}
			}
			return null;
		}
 
		/// <summary>
		/// Add 
		/// </summary>
		/// <param name="pSection"></param>
		/// <returns></returns>
		public DesignSection Add(DesignSection pSection){
			base.Add(pSection);
			pSection.SectionList = this;
			return pSection;
		}
		#endregion public 方法...

		#region Data SectionList 事件...

		private void _DataObj_BeforeRemoveSection(object sender, DIYReport.ReportModel.RptSection e) {
			DesignSection deSection = null;
			foreach(DesignSection section in this){
				if(section.DataObj.Equals(e)){
					deSection = section;
					break;
				}
			}
			if(deSection!=null){
				OnBeforeRemoveSection( new DesignSectionEventArgs(-1,deSection));
				this.Remove(deSection); 
			}
			//在删除Section 后重新布置
			RefreshDesignLayout(); 
		}
		#endregion Data SectionList 事件...

		#region 内部处理函数...
		// 重新刷新报表设计中所有Section 的显示
		private void createDesignSection(){
			//检查 DataSection 在DesignSectionList 中是否已经创建，如果不存在，就创建它
			int i = 0;
			foreach(DIYReport.ReportModel.RptSection dataSection in  _DataObj){
				if(!dataSection.HasCreateViewDesign){
					DesignSection section = new DesignSection(dataSection); 
					SectionCaption caption = new SectionCaption();
					section.IsDisplay = true;
					section.CaptionCtl = caption;
					section.SectionList = this;
					this.Add(section);
					OnAfterInsertSection(new DesignSectionEventArgs(i,section)); 
				}
				i++;
			}
			RefreshDesignLayout();
		}
//		
//		//根据Section 在集合中的index 得到location
//		private Point getSectionPosition(int pIndex,out int pCaptionY){
//			int height = 0;
//			for(int i = 0;i < pIndex;i++){
//				 DesignSection section = this[i] as DesignSection ;
//				if(section.IsDisplay){
//					height +=section.Height +  SectionCaption.CAPTION_HEIGHT ;
//				}//
//			}
//			pCaptionY =  height;
//			return new Point(0, height + SectionCaption.CAPTION_HEIGHT);
//		}
	#endregion 内部处理函数...

		private void _DataObj_AfterCreateNewSection(object sender, System.EventArgs  e) {
			createDesignSection();
		}
	}
}
