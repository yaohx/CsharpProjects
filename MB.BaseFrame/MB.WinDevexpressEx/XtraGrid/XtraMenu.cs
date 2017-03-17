using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.XWinLib.XtraGrid {
    /// <summary>
    /// MenuItem 的扩展项...
    /// </summary>
    public class XtraMenu : System.Windows.Forms.MenuItem {
        private XtraContextMenuType _MenuType;

        public XtraMenu(string text, System.EventHandler click, XtraContextMenuType menuType)
            : base(text, click) {
            _MenuType = menuType;
        }
        public XtraContextMenuType MenuType {
            get {
                return _MenuType;
            }
        }
    }
}
