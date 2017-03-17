//---------------------------------------------------------------- 
// Author		:	Nick
// Create date	:	2009-02-13
// Description	:	DevexpressLib 在应用程序的启动方法中调用，实现组件的本地化。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;

namespace MB.XWinLib.Localization {
    /// <summary>
    /// DevexpressLib 在应用程序的启动方法中调用，实现组件的本地化。
    /// </summary>
    public class DevexpressLib {
        #region private construct function...
        /// <summary>
        /// private construct function...
        /// </summary>
        private DevexpressLib() {

        }
        #endregion private construct function...


        /// <summary>
        /// 开始设置资源的信息。
        /// </summary>
        public static void LocalSetting() {
            //本地化XtraGrid 组件
            DevExpress.XtraGrid.Localization.GridLocalizer.Active = GridLocal.Create();
            //本地化XtraPrinting 组件
            DevExpress.XtraPrinting.Localization.PreviewLocalizer.Active = new PrintLocal();

            DevExpress.XtraPivotGrid.Localization.PivotGridLocalizer.Active = new PivotGridLocal();

            DevExpress.XtraEditors.Controls.Localizer.Active = new EditorsLocal();

            //WangHui:图表分析注释
            //本地化图表组件
            //MB.WinDxChart.Localization.DevexpressLib.LocalSetting();
        }
    }
}
