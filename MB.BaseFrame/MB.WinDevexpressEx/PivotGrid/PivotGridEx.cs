//---------------------------------------------------------------- 
// Author		:	Nick
// Create date	:	2009-02-13
// Description	:	PivotGridEx 扩展DevExpress.XtraPivotGrid.PivotGridControl  控件，做特殊的处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace MB.XWinLib.PivotGrid {
	/// <summary>
	/// PivotGridEx 扩展DevExpress.XtraPivotGrid.PivotGridControl  控件，做特殊的处理。
	/// </summary>
	public class PivotGridEx : DevExpress.XtraPivotGrid.PivotGridControl {
		private DevExpress.XtraPivotGrid.PivotGridField _SummarySortField;
		private bool _CreateOtherMenuByUser;
		/// <summary>
		/// 构造函数...
		/// </summary>
		public PivotGridEx() {
			this.FieldAreaChanged +=new DevExpress.XtraPivotGrid.PivotFieldEventHandler(PivotGridEx_FieldAreaChanged);

            bool design = MB.Util.General.IsInDesignMode();
            if (!design) {
                //在运行阶段设置本地化语言应用
               // DevExpress.XtraPivotGrid.Localization.PivotGridResLocalizer.Active = MB.XWibLib.Localization.PivotGridLocal.Create();
            }
		}

		#region 覆盖基类的方法...
		protected override void OnLoaded() {
			base.OnLoaded ();
			if(!base.DesignMode ){
				if(base.ContextMenu==null && !_CreateOtherMenuByUser){
					createMenuItems(XtraContextMenuType.Sort | XtraContextMenuType.Style | XtraContextMenuType.SaveDefaultStyle); 
				}
			}
		
		}
		protected override void OnHandleDestroyed(EventArgs e) {
			try{
				this.FieldAreaChanged -=new DevExpress.XtraPivotGrid.PivotFieldEventHandler(PivotGridEx_FieldAreaChanged);
			}
			catch{}
			base.OnHandleDestroyed (e);
		}

		#endregion 覆盖基类的方法...

		#region 扩展的Public 成员...
		/// <summary>
		/// 获取或设置弹出菜单是否由用户自己创建
		/// </summary>
		[Description("获取或设置弹出菜单是否由用户自己创建") ]
		public bool CreateOtherMenuByUser{
			get{
				return _CreateOtherMenuByUser;
			}
			set{
				_CreateOtherMenuByUser = value;
			}
		}
		/// <summary>
		/// 获取控件所在的窗口。
		/// </summary>
		[Browsable(false)]
		public virtual System.Windows.Forms.Form ParentForm{
			get{
                if (!MB.Util.General.IsInDesignMode()) {
					return getCtlParentForm(this);
				}
				else
					return null;
			}
		}
		/// <summary>
		/// 重新设置弹出窗口的菜单项。
		/// </summary>
		/// <param name="createMenus"></param>
		public virtual void ReSetContextMenu(XtraContextMenuType   createMenus){
			createMenuItems(createMenus);
		}
		#endregion 扩展的Public 成员...

		#region 创建自定义菜单处理相关...
		/// <summary>
		/// 创建XtarGrid 的Context Menu 菜单项。
		/// </summary>
		private  void createMenuItems(XtraContextMenuType menuTypes){
			System.Windows.Forms.ContextMenu cMenu = new ContextMenu();
			this.ContextMenu = cMenu;
			if((menuTypes & XtraContextMenuType.Sort)!=0){
				createMenuByType(XtraContextMenuType.Sort);
				
				createSummarySortItem();
			}

			if((menuTypes & XtraContextMenuType.SaveDefaultStyle)!=0){
				if(cMenu.MenuItems.Count > 0)
					cMenu.MenuItems.Add("-");
				createMenuByType(XtraContextMenuType.SaveDefaultStyle);
			}
		}

		private System.Windows.Forms.MenuItem createMenuByType(XtraContextMenuType menuType){
			Type enumType = typeof(XtraContextMenuType);
			string str = MB.Util.MyCustomAttributeLib.Instance.GetFieldDesc(enumType,menuType.ToString(), false); 
			XtraPivotMenu menu = null;
			if(menuType!=XtraContextMenuType.Sort) 
				menu = new XtraPivotMenu(str,new System.EventHandler(menuItemClick),menuType); 
			else
				menu = new XtraPivotMenu(str,XtraContextMenuType.Sort);
			this.ContextMenu.MenuItems.Add(menu);
			return menu;
		}
		private void menuItemClick(object sender,System.EventArgs e){
			XtraPivotMenu menu = sender as XtraPivotMenu;
			if(menu==null)
				return;
			
			PivotGridHelper.Instance.HandleClickXtraContextMenu(this.ParentForm as MB.WinBase.IFace.IForm ,this,menu.MenuType);
		}
		//创建排序的菜单项
		private void createSummarySortItem(){
			XtraPivotMenu sortMenu = null;
			if(_CreateOtherMenuByUser || this.ContextMenu==null)
				return;
			foreach(XtraPivotMenu menu in this.ContextMenu.MenuItems){
				if(menu.MenuType == XtraContextMenuType.Sort){
					sortMenu = menu;
					break;
				}
			}
			if(sortMenu==null)
				return;

			sortMenu.MenuItems.Clear();
			foreach(DevExpress.XtraPivotGrid.PivotGridField info in this.Fields){
				if(info.Area == DevExpress.XtraPivotGrid.PivotArea.DataArea)
					sortMenu.MenuItems.Add( new XtraPivotMenu(info.Caption,new System.EventHandler(summaryFieldClick),XtraContextMenuType.Sort,info));  
			}
		}
		private void summaryFieldClick(object sender,System.EventArgs e){
			XtraPivotMenu menu = sender as XtraPivotMenu;
			if(menu==null || _SummarySortField==null)
				return;
			_SummarySortField.SortOrder = _SummarySortField.SortOrder==DevExpress.XtraPivotGrid.PivotSortOrder.Ascending? DevExpress.XtraPivotGrid.PivotSortOrder.Descending:DevExpress.XtraPivotGrid.PivotSortOrder.Ascending;   
			_SummarySortField.SortBySummaryInfo.Field = menu.SortField;
		}
		#endregion 创建自定义菜单处理相关...

		
		#region 静态方法...
		/// <summary>
		/// 静态方法。
		/// </summary>
		/// <param name="ctl"></param>
		/// <returns></returns>
		private static System.Windows.Forms.Form getCtlParentForm(System.Windows.Forms.Control ctl){
			if(ctl==null)
				return null;
			System.Windows.Forms.Form frm = ctl as System.Windows.Forms.Form;
			if(frm!=null)
				return frm;
			System.Windows.Forms.ContainerControl conCtl = ctl as System.Windows.Forms.ContainerControl;
			if(conCtl!=null)
				return conCtl.ParentForm;
			else
				return getCtlParentForm(ctl.Parent); 

		}
		#endregion 静态方法...

		private void PivotGridEx_FieldAreaChanged(object sender, DevExpress.XtraPivotGrid.PivotFieldEventArgs e) {
			createSummarySortItem();

			foreach(DevExpress.XtraPivotGrid.PivotGridField info in this.Fields){
				if(info.Area == DevExpress.XtraPivotGrid.PivotArea.RowArea)
					_SummarySortField = info;
			}
		}
	}	

	#region MenuItem 的扩展项...
	public class XtraPivotMenu : System.Windows.Forms.MenuItem{
		private XtraContextMenuType _MenuType;
		private DevExpress.XtraPivotGrid.PivotGridField _SortField;
		public XtraPivotMenu(string text,XtraContextMenuType menuType): base(text){
			_MenuType = menuType;
		}
		public XtraPivotMenu(string text,System.EventHandler click, XtraContextMenuType menuType) : base(text,click){
			_MenuType = menuType;
		}
		public XtraPivotMenu(string text,System.EventHandler click, XtraContextMenuType menuType,DevExpress.XtraPivotGrid.PivotGridField sortField) : base(text,click){
			_MenuType = menuType;
			_SortField = sortField;
		}
		public XtraContextMenuType MenuType{
			get{
				return _MenuType;
			}
		}
		public DevExpress.XtraPivotGrid.PivotGridField SortField{
			get{
				return _SortField;
			}
		}
	}
	public enum XtraContextMenuType : int {
		[Description("数据区排序")]
		Sort = 0x00000001,
		[Description("样式")]
		Style = 0x00000002,
		[Description("保存布局")]
		SaveDefaultStyle = 0x00000004
	}
	#endregion MenuItem 的扩展项...
}
