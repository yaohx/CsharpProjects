using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.XWinLib.XtraGrid; 
namespace MB.XWinLib.XtraTreeList {
    /// <summary>
    /// TreeList 控件Context菜单。
    /// </summary>
    public class TreeListContextMenu {
        private System.Windows.Forms.ContextMenu _GridMenu;
        private TreeListEx _TreList;
        private MB.WinBase.IFace.IForm _ContainerForm;

        /// <summary>
        /// TreeList 控件Context菜单。
        /// </summary>
        /// <param name="xtraGrid"></param>
        /// <param name="menuTypes"></param>
        public TreeListContextMenu(TreeListEx treList, XtraContextMenuType menuTypes) {
            _TreList = treList;

            _GridMenu = new System.Windows.Forms.ContextMenu();

            CreateMenuItems(menuTypes);

        }
        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ContextMenu GridContextMenu {
            get {
                return _GridMenu;
            }
        }
        /// <summary>
        /// 创建XtarGrid 的Context Menu 菜单项。
        /// </summary>
        public virtual void CreateMenuItems(XtraContextMenuType menuTypes) {
            //if(_ContainerForm==null)
            //	 return;

            if ((menuTypes & XtraContextMenuType.Add) != 0)
                createMenuByType(XtraContextMenuType.Add);
            if ((menuTypes & XtraContextMenuType.BatchAdd) != 0) {
                createMenuByType(XtraContextMenuType.BatchAdd);
            }
            if ((menuTypes & XtraContextMenuType.Delete) != 0)
                createMenuByType(XtraContextMenuType.Delete);

            if ((menuTypes & XtraContextMenuType.QuickInput) != 0) {
                createMenuByType(XtraContextMenuType.QuickInput);
            }
            if ((menuTypes & XtraContextMenuType.Copy) != 0) {
                createMenuByType(XtraContextMenuType.Copy);
            }
            if ((menuTypes & XtraContextMenuType.Past) != 0) {
                createMenuByType(XtraContextMenuType.Past);
            }

            if ((menuTypes & XtraContextMenuType.Aggregation) != 0) {
                if (_GridMenu.MenuItems.Count > 0)
                    _GridMenu.MenuItems.Add("-");
                createMenuByType(XtraContextMenuType.Aggregation);
            }
            if ((menuTypes & XtraContextMenuType.Chart) != 0)
                createMenuByType(XtraContextMenuType.Chart);

            if ((menuTypes & XtraContextMenuType.ViewControl) != 0)
                createMenuByType(XtraContextMenuType.ViewControl);
            if ((menuTypes & XtraContextMenuType.Print) != 0)
                createMenuByType(XtraContextMenuType.Print);
            if ((menuTypes & XtraContextMenuType.Export) != 0) {
                if (_GridMenu.MenuItems.Count > 0)
                    _GridMenu.MenuItems.Add("-");
                createMenuByType(XtraContextMenuType.Export);
            }

            if ((menuTypes & XtraContextMenuType.ExportAsTemplet) != 0)
                createMenuByType(XtraContextMenuType.ExportAsTemplet);

            if ((menuTypes & XtraContextMenuType.DataImport) != 0)
                createMenuByType(XtraContextMenuType.DataImport);

            if ((menuTypes & XtraContextMenuType.SaveGridState) != 0) {
                if (_GridMenu.MenuItems.Count > 0)
                    _GridMenu.MenuItems.Add("-");
                createMenuByType(XtraContextMenuType.SaveGridState);
            }
            if ((menuTypes & XtraContextMenuType.ColumnsAllowSort) != 0) {
                if (_GridMenu.MenuItems.Count > 0)
                    _GridMenu.MenuItems.Add("-");
                createMenuByType(XtraContextMenuType.ColumnsAllowSort);
            }
            if ((menuTypes & XtraContextMenuType.SortChildNode) != 0) {
                if (_GridMenu.MenuItems.Count > 0)
                    _GridMenu.MenuItems.Add("-");
                createMenuByType(XtraContextMenuType.SortChildNode);
            }

        }

        private System.Windows.Forms.MenuItem createMenuByType(XtraContextMenuType menuType) {
            Type enumType = typeof(XtraContextMenuType);
            string str = MB.Util.MyCustomAttributeLib.Instance.GetFieldDesc(enumType, menuType.ToString(), false);
            str = MB.BaseFrame.CLL.Convert(str);
            XtraMenu menu = new XtraMenu(str, new System.EventHandler(menuItemClick), menuType);
            _GridMenu.MenuItems.Add(menu);
            if (menuType == XtraContextMenuType.ColumnsAllowSort)
                menu.Checked = true;

            return menu;
        }
        private void menuItemClick(object sender, System.EventArgs e) {
            try {
                XtraMenu menu = sender as XtraMenu;
                if (menu == null)
                    return;

                TreeListExMenuEventArg arg = new TreeListExMenuEventArg(menu.MenuType);
                arg.Column = _TreList.FocusedColumn;
                _TreList.RaiseBeforeContextMenuClick(arg);
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        
    }
}
