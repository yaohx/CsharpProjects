using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinTestProject {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MB.XWinLib.Localization.DevexpressLib.LocalSetting();
            Application.Run(new frmTestMain());
        }
    }
}
