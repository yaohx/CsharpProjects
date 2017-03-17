using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;

namespace MB.WinBase.Ctls {
    [ToolboxItem(true)]
    public class MyGroupPanel : DevExpress.XtraEditors.GroupControl {
        public MyGroupPanel() {
            this.Text = "基本信息";
        }
    }
}
