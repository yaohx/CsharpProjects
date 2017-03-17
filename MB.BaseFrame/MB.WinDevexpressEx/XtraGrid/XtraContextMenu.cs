//---------------------------------------------------------------- 
// Author		:	checndc
// Create date	:	2009-02-17
// Description	:	XtraContextMenu XtraGrid ContextMenu。。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;

using MB.BaseFrame;
using System.Collections;
using MB.WinDxChart.Chart;
namespace MB.XWinLib.XtraGrid
{
	/// <summary>
	/// XtraContextMenu XtraGrid ContextMenu。
	/// </summary>
	public class XtraContextMenu
	{
		private System.Windows.Forms.ContextMenu _GridMenu;
		private GridControlEx _XtraGrid;
		private MB.WinBase.IFace.IForm _ContainerForm;
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xtraGrid"></param>
        /// <param name="menuTypes"></param>
		public XtraContextMenu(GridControlEx xtraGrid ,XtraContextMenuType menuTypes)
		{
			_XtraGrid = xtraGrid;
          
			_GridMenu = new System.Windows.Forms.ContextMenu();
			if(xtraGrid.ParentForm!=null)
			{
				_ContainerForm = xtraGrid.ParentForm as MB.WinBase.IFace.IForm;
			}

			CreateMenuItems(menuTypes);

		}
		/// <summary>
		/// 
		/// </summary>
		public System.Windows.Forms.ContextMenu  GridContextMenu
		{
			get
			{
				return _GridMenu;
			}
		}
		/// <summary>
		/// 创建XtarGrid 的Context Menu 菜单项。
		/// </summary>
		public virtual void CreateMenuItems(XtraContextMenuType menuTypes)
		{
			//if(_ContainerForm==null)
			//	 return;
			
			if((menuTypes & XtraContextMenuType.Add)!=0)
				createMenuByType(XtraContextMenuType.Add);
            if ((menuTypes & XtraContextMenuType.BatchAdd) != 0) {
                createMenuByType(XtraContextMenuType.BatchAdd);
            }
			if((menuTypes & XtraContextMenuType.Delete)!=0)
				createMenuByType(XtraContextMenuType.Delete);

            if ( (menuTypes & XtraContextMenuType.QuickInput) != 0) {
                createMenuByType(XtraContextMenuType.QuickInput);
            }
            if ((menuTypes & XtraContextMenuType.Copy) != 0) {
                createMenuByType(XtraContextMenuType.Copy);
            }
            if ((menuTypes & XtraContextMenuType.Past) != 0) {
                createMenuByType(XtraContextMenuType.Past);
            }

			if((menuTypes & XtraContextMenuType.Aggregation)!=0)
			{
				if(_GridMenu.MenuItems.Count > 0)
					_GridMenu.MenuItems.Add("-");
				createMenuByType(XtraContextMenuType.Aggregation);
			}

            //WangHui:图表分析注释

            if ((menuTypes & XtraContextMenuType.Chart) != 0)
            {
                if (_ContainerForm != null && _ContainerForm.ClientRuleObject != null 
                    && _ContainerForm.ClientRuleObject.UIRuleXmlConfigInfo != null) {
                    var cfgs = _ContainerForm.ClientRuleObject.UIRuleXmlConfigInfo.GetDefaultChartViewCfg();
                    if (cfgs != null && cfgs.Count > 0)
                        createMenuByType(XtraContextMenuType.Chart);
                }
            }

            ////添加图表设计
            //if ((menuTypes & XtraContextMenuType.ChartDesign) != 0)
            //{
            //    createMenuByType(XtraContextMenuType.ChartDesign);
            //}
			
			if((menuTypes & XtraContextMenuType.ViewControl)!=0)
				createMenuByType(XtraContextMenuType.ViewControl);
			if((menuTypes & XtraContextMenuType.Print)!=0)
				createMenuByType(XtraContextMenuType.Print);
            if ((menuTypes & XtraContextMenuType.Export) != 0) {
                if (_GridMenu.MenuItems.Count > 0)
                    _GridMenu.MenuItems.Add("-");
                createMenuByType(XtraContextMenuType.Export);
            }
			
			if((menuTypes & XtraContextMenuType.ExportAsTemplet)!=0)
				createMenuByType(XtraContextMenuType.ExportAsTemplet);

			if((menuTypes & XtraContextMenuType.DataImport)!=0)
				createMenuByType(XtraContextMenuType.DataImport);
			
			if((menuTypes & XtraContextMenuType.SaveGridState)!=0)
			{
				if(_GridMenu.MenuItems.Count > 0)
					_GridMenu.MenuItems.Add("-");
				createMenuByType(XtraContextMenuType.SaveGridState);
			}
			if((menuTypes & XtraContextMenuType.ColumnsAllowSort)!=0) {
				if(_GridMenu.MenuItems.Count > 0)
					_GridMenu.MenuItems.Add("-");
				createMenuByType(XtraContextMenuType.ColumnsAllowSort);
			}
            if ((menuTypes & XtraContextMenuType.ExcelEdit) != 0) {
                if (_GridMenu.MenuItems.Count > 0)
                    _GridMenu.MenuItems.Add("-");
                createMenuByType(XtraContextMenuType.ExcelEdit);
            }

		}

		private System.Windows.Forms.MenuItem createMenuByType(XtraContextMenuType menuType)
		{
			Type enumType = typeof(XtraContextMenuType);
			string str = MB.Util.MyCustomAttributeLib.Instance.GetFieldDesc(enumType,menuType.ToString(), false); 
			str = CLL.Convert(str);
            XtraMenu menu = new XtraMenu(str, new System.EventHandler(menuItemClick), menuType);
            _GridMenu.MenuItems.Add(menu);
            if (menuType == XtraContextMenuType.ColumnsAllowSort)
                menu.Checked = true;
            if (menuType == XtraContextMenuType.Chart)
            {
                var templateMenu = new WinDxChart.Chart.ChartTemplateMenu(_XtraGrid);
                System.Windows.Forms.ContextMenu contextMenu = templateMenu.ChartContextMenu;
                int count = contextMenu.MenuItems.Count;
                for (int i = 0; i < count; i++)
                {
                    menu.MenuItems.Add(contextMenu.MenuItems[0]);
                }
            }

			return menu;
		}
		private void menuItemClick(object sender,System.EventArgs e)
		{
            try {
                XtraMenu menu = sender as XtraMenu;
                if (menu == null)
                    return;
                DevExpress.XtraGrid.Views.Grid.GridView gridView = _XtraGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
                GridControlExMenuEventArg arg = new GridControlExMenuEventArg(menu.MenuType);
                arg.Column = gridView.FocusedColumn; 
                _XtraGrid.OnBeforeContextMenuClick(arg);

                if (!arg.Handled) {
                    if (menu.MenuType == XtraContextMenuType.ColumnsAllowSort) {
                        clickColumnSort(menu);
                    }
                    else {
                        XtraGridHelper.Instance.HandleClickXtraContextMenu(_ContainerForm, _XtraGrid, menu.MenuType);
                    }
                }
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);    
            }
		}

		private void clickColumnSort(XtraMenu menu){
			menu.Checked = !menu.Checked;
			DevExpress.XtraGrid.Views.Grid.GridView gridView =  _XtraGrid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
			foreach(DevExpress.XtraGrid.Columns.GridColumn col in gridView.Columns){
				DevExpress.Utils.DefaultBoolean sort = menu.Checked?DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
				col.OptionsColumn.AllowSort = sort;
			}
		}
	}
	
	 
	
}
