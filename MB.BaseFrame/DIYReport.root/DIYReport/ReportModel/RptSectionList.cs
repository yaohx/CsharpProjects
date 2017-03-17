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
using System.Diagnostics; 
using System.Windows.Forms;

 
namespace DIYReport.ReportModel {
	//	#region 根据section 的类型定义 比较器...
	//	public class RptSectionSortComparer : IComparer  {
	//		// 升比较器
	//		int IComparer.Compare( Object x, Object y )  {
	//			RptSection   xInfo = x as RptSection;
	//			RptSection yInfo = y as RptSection;
	//				 
	//			return( (new CaseInsensitiveComparer()).Compare((int)xInfo.SectionType ,(int)yInfo.SectionType ) );
	//		}
	//	
	//	}
	//	#endregion 比较器...

	/// <summary>
	///  事件委托申明
	///  备注：除非明确的知道需求，最好不要这样设计事件的参赛 
	/// </summary>
	public delegate void RptSectionEventHandler(object sender,RptSection  e);

	/// <summary>
	/// RptSectionList 打印和浏览Section 集合。
	/// </summary>
	public class RptSectionList : ArrayList  {
		private RptReport _Report;
		private int _CurrReportWidth = 0;

		#region Public 属性...
		public RptReport Report{
			get{
				return _Report;
			}
			set{
				_Report = value;
			}
		}
		public int CurrReportWidth{
			get{
				return _CurrReportWidth;
			}
			set{
				_CurrReportWidth = value;
			}
		}
		#endregion public 属性...

		#region 自定义事件...
		private  System.EventHandler _AfterCreateNewSection; 
		private RptSectionEventHandler _BeforeRemoveSection;
		public event RptSectionEventHandler BeforeRemoveSection{
			add{
				_BeforeRemoveSection +=value;
			}
			remove{
				_BeforeRemoveSection -=value;
			}
		}
		private void OnBeforeRemoveSection(RptSection arg){
			if(_BeforeRemoveSection!=null){
				_BeforeRemoveSection(this,arg);
			}

		}
		public event System.EventHandler _fterCreateNewSection{
			add{
				_AfterCreateNewSection +=value;
			}
			remove{
				_AfterCreateNewSection -=value;
			}
		}
		private void onAfterCreateNewSection(System.EventArgs  arg){
			if(_AfterCreateNewSection!=null){
				_AfterCreateNewSection(this,arg);
			}
		}
		#endregion 自定义事件...

		#region 扩展的Public 方法...
		/// <summary>
		/// 创建section 
		/// </summary>
		public void AddbySectionType(DIYReport.SectionType sectionType){
			//检查对应的section 是否已经创建
			foreach(RptSection section in this){
				if(section.SectionType == sectionType){
					MessageBox.Show("需要增加的Section 已经创建,每个Section 只能创建一次。","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);    
					return;
				}
			}
			if(sectionType == DIYReport.SectionType.GroupHead || sectionType == DIYReport.SectionType.GroupFooter)
				Debug.Assert(false,"创建分组的section 请使用CreateGroupSection");
			RptSection newSection = new RptSection(sectionType);
			this.Add(newSection);

			onAfterCreateNewSection(null);
		}
		/// <summary>
		/// 删除指定的section.
		/// </summary>
		/// <param name="section"></param>
		public void RemoveSection(RptSection section){
			if(this.Count == 1){
				MessageBox.Show("最后一个Section 不能进行删除。","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);    
				return;
			}
			if(section.SectionType == SectionType.GroupHead || section.SectionType == SectionType.GroupFooter){
				MessageBox.Show("分组的 Section 不能进行删除。你可以通过拖动它的高度来达到打印不显示的效果。","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);    
				return;
			}
			if(base.Contains(section)){
				OnBeforeRemoveSection(section);
				base.Remove(section); 
			}

		}
		/// <summary>
		/// 创建分组的Data Section 
		/// </summary>
		/// <param name="pGroupField"> 需要进行分组处理的字段信息 </param>
		/// <returns></returns>
		public bool CreateGroupSection(){
			ArrayList  rptFields = DIYReport.UserDIY.DesignEnviroment.CurrentReport.DesignField as ArrayList;
			//分组排序
			rptFields.Sort(new DIYReport.GroupAndSort.FieldSortComparer());
			//int width = (this[0] as RptSection).Width ;
			//先删除不需要的分组
			foreach(DIYReport.GroupAndSort.RptFieldInfo info in rptFields){ 
				if(info.IsGroup)
					continue;
				//判断在分组是否存在，并把已经不作为分组的Data Section删除掉
				bool b = deleteGroupSection(info);
				if(b){
					//在删除组页脚
					deleteGroupSection(info);
				}
			}
			foreach(DIYReport.GroupAndSort.RptFieldInfo info in rptFields){ 
				if(!info.IsGroup)
					continue;
				bool hasCreate = false;
				foreach(RptSection section in this){
					if(section.GroupField!=null &&  section.GroupField.Equals(info)){
						hasCreate = true;
						break;
					}
				}
				if(hasCreate){continue;}
				//先创建分组的标题
				RptSection newSection = new RptSection(DIYReport.SectionType.GroupHead,info);
				this.Add(newSection);
				newSection = new RptSection(DIYReport.SectionType.GroupFooter,info);
				//newSection.Width = width;
				//	newSection.Height = 60;
				this.Add(newSection);
			}
			onAfterCreateNewSection(null);
			return true;
		}
		private bool deleteGroupSection(DIYReport.GroupAndSort.RptFieldInfo pFieldinfo){
			RptSection groupSection = null;
			foreach(RptSection section in this){
				if(section.GroupField!=null &&  section.GroupField.Equals(pFieldinfo)){
					groupSection = section;
					break;
				}
			}
			if(groupSection!=null){
				OnBeforeRemoveSection(groupSection);
				this.Remove(groupSection);
				return true;
			}
			return false;
		}

		#endregion 扩展的Public 方法...

		#region Public 方法...
		/// <summary>
		/// 根据分组的字段名称得到分组Section 信息
		/// </summary>
		/// <param name="pGroupFieldName"></param>
		/// <param name="pIsGroupHead"></param>
		/// <returns></returns>
		public RptSection GetByGroupField(string pGroupFieldName,bool pIsGroupHead){
			foreach(RptSection section in this){
				DIYReport.SectionType findType = pIsGroupHead?DIYReport.SectionType.GroupHead:DIYReport.SectionType.GroupFooter;
				if(section.SectionType == findType){
					DIYReport.GroupAndSort.RptFieldInfo field = section.GroupField;
					if(field==null){
						throw new Exception("分组Section " + section.Name + "的 GroupField 为空。");
					}
					if(field.FieldName == pGroupFieldName){
						return section;
					}
				}
			}
			return null;
		}
		/// <summary>
		/// 得到出Detail外的其它所有Section 相加的高度的总和
		/// </summary>
		/// <returns></returns>
		public int GetExceptDetailHeight(){
			int height = 0;
			foreach(RptSection section in this){
				if(section.SectionType !=  DIYReport.SectionType.Detail && section.SectionType !=  DIYReport.SectionType.GroupHead
					&& section.SectionType !=  DIYReport.SectionType.GroupFooter){
					height +=section.Height ;
				}
			}
			return height;
		}
		/// <summary>
		/// 获取页面Margin 的高度。
		/// </summary>
		/// <returns></returns>
		public int GetMarginHeight(){
			int height = 0;
			foreach(RptSection section in this){
				if(section.SectionType ==  DIYReport.SectionType.TopMargin || section.SectionType ==  DIYReport.SectionType.BottomMargin)
					 {
					height +=section.Height ;
				}
			}
			return height;
		}
		/// <summary>
		/// 通过Section Type 得到 指定的Section的高度 
		/// </summary>
		/// <param name="pType"></param>
		/// <returns></returns>
		public int GetSectionHeightByType(DIYReport.SectionType pType){
			RptSection section =  GetSectionByType(pType);
			if(section!=null && section.Visibled){
				return section.Height ;
			}
			else{
				return 0;
			}
		}
		/// <summary>
		/// 通过Section Type 得到 指定的Section
		/// </summary>
		/// <param name="pType"></param>
		public RptSection GetSectionByType(DIYReport.SectionType pType){
			foreach(RptSection section in this){
				if(section.SectionType == pType){
					return section;
				}
			}
			return null;
		}
		/// <summary>
		/// 重新调整Section 的宽度
		/// </summary>
		public void ReSizeByPaperSize(int pWidth){
			foreach(RptSection section in this){
				section.Width = pWidth;
			}
		}
		/// <summary>
		/// ADD
		/// </summary>
		/// <param name="pParam"></param>
		/// <returns></returns>
		public RptSection Add(RptSection pParam) {
			insertNewSection(pParam);
			return pParam;
		}
		/// <summary>
		/// 在指定的位置插入Section 
		/// </summary>
		/// <param name="pIndex"></param>
		/// <param name="pParam"></param>
		/// <returns></returns>
		[Obsolete("过期的方法，请使用 ADD 代替")] 
		public RptSection Insert(int pIndex , RptSection pParam) {
			base.Insert(pIndex,pParam);
			return pParam;
		}
		#endregion Public 方法...

		#region 内部处理方法...
		//判断并插入新的section....
		private void insertNewSection(RptSection section){
			section.Report = _Report;
			int position = -1;
			for(int i  = 0; i < this.Count ; i++){
				int oldIndex = (int)(this[i] as RptSection).SectionType;
				if( oldIndex == (int)section.SectionType){//如果相等说明是分组的情况。
					if(section.SectionType == SectionType.GroupHead){
						position = i;
					}
					else{
						position = i + 1;
					}
					break;
				}
				if( oldIndex > (int)section.SectionType){
					position = i;
					break;
				}
			}
			if(position == -1 || position == this.Count){
				base.Add(section);
			}
			else{
				base.Insert(position,section);
			}

		}
		//得到 Detail Section 在当前集合中的Index
		private int getDetailSectionIndex(){
			int count = this.Count ;
			for(int i =0 ; i<count;i++){
				if((this[i] as RptSection).SectionType == DIYReport.SectionType.Detail){
					return i;
				}
			}
			return -1;
		}

		#endregion 内部处理方法...
	}
}
