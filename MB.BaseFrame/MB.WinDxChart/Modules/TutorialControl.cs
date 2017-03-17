using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.DXperience.Demos;
using DevExpress.Utils.Menu;

namespace MB.WinDxChart.Modules
{
    public class TutorialControl : TutorialControlBase
    {
        IDXMenuManager menuManager;

        public IDXMenuManager MenuManager
        {
            get { return menuManager; }
            set { menuManager = value; }
        }
    }
}
