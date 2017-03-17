using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using MB.XWinLib.XtraGrid;
namespace MB.XWinLib.XtraTreeList {
    /// <summary>
    /// 扩展TreeList 控件满足业务需要。
    /// </summary>
    public class TreeListEx : DevExpress.XtraTreeList.TreeList {
        private static Color ODD_ROW_BACK_COLOR = Color.FromArgb(245, 245, 250);   

        #region 扩展的自定义事件处理相关...
        private TreeListExMenuEventHandle _BeforeContextMenuClick;
        public event TreeListExMenuEventHandle BeforeContextMenuClick {
            add {
                _BeforeContextMenuClick += value;
            }
            remove {
                _BeforeContextMenuClick -= value;
            }
        }
        public virtual void RaiseBeforeContextMenuClick(TreeListExMenuEventArg arg) {
            if (_BeforeContextMenuClick != null)
                _BeforeContextMenuClick(this, arg);
        }
        #endregion 扩展的自定义事件处理相关...

        public TreeListEx() {
            this.OptionsView.EnableAppearanceEvenRow = true; 
            this.Appearance.EvenRow.BackColor = ODD_ROW_BACK_COLOR;
        }

     
        
        private Guid _InstanceIdentity;
        /// <summary>
        /// 当前实例的身份标识。
        /// </summary>
        public Guid InstanceIdentity {
            get {
                return _InstanceIdentity;
            }
            set {
                _InstanceIdentity = value;
            }
        }
        /// <summary>
        /// 重新设置弹出窗口的菜单项。
        /// </summary>
        /// <param name="createMenus"></param>
        public virtual void ReSetContextMenu(XtraContextMenuType createMenus) {
            TreeListContextMenu xMenu = new TreeListContextMenu(this, createMenus);
            base.ContextMenu = xMenu.GridContextMenu;
        }

        
       
    }
    #region 自定义事件处理相关...
    public delegate void TreeListExMenuEventHandle(object sender, TreeListExMenuEventArg arg);
    public class TreeListExMenuEventArg {
        private bool _Handled;
        private DevExpress.XtraTreeList.Columns.TreeListColumn _Column;
        private XtraContextMenuType _MenuType;
        public TreeListExMenuEventArg(XtraContextMenuType menuType) {
            _MenuType = menuType;
        }
        public bool Handled {
            get {
                return _Handled;
            }
            set {
                _Handled = value;
            }
        }
        public XtraContextMenuType MenuType {
            get {
                return _MenuType;
            }
            set {
                _MenuType = value;
            }
        }
        /// <summary>
        /// 当前响应鼠标的列
        /// </summary>
        public DevExpress.XtraTreeList.Columns.TreeListColumn Column {
            get {
                return _Column;
            }
            set {
                _Column = value;
            }
        }
    }
    #endregion 自定义事件处理相关...
}
