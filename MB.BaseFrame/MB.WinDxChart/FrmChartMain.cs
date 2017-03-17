using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.DXperience.Demos;
using MB.WinDxChart.Common;
using MB.WinDxChart.Modules;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.Skins;
using System.Diagnostics;
using DevExpress.XtraEditors;
using DevExpress.Utils.Frames;

namespace MB.WinDxChart
{
    public partial class FrmChartMain : Form//DevExpress.DXperience.Demos.frmMain
    {
        protected ChartAppearanceMenu appearanceMenu;

        protected DefaultLookAndFeel defaultLookAndFeel1;
        public FrmChartMain()
        {
            InitializeComponent();

            SkinManager.EnableFormSkins();
            LookAndFeelHelper.ForceDefaultLookAndFeelChanged();

            this.Load += new EventHandler(OnLoad);
        }

        protected virtual void CreateMenu()
        {
            this.appearanceMenu = new ChartAppearanceMenu(manager, defaultLookAndFeel1, "图表分析");
            this.appearanceMenu.BeginSkinChanging += new EventHandler(appearanceMenu_BeginSkinChanging);
            this.appearanceMenu.EndSkinChanging += new EventHandler(appearanceMenu_EndSkinChanging);

            //设置扩展菜单
            this.appearanceMenu.ResetExtendMenu(ResetExtendBarSubItemMenu);
        }

        /// <summary>
        /// 供业务类重载的扩展菜单
        /// </summary>
        public virtual BarSubItem ResetExtendBarSubItemMenu
        {
            get;
            set;
        }
        

        void appearanceMenu_BeginSkinChanging(object sender, EventArgs e)
        {
            ChartModuleBase currentDemo = ChartModulesInfo.CurrentModule as ChartModuleBase;
            if (currentDemo != null)
                currentDemo.BeginSkinChanging();
        }
        void appearanceMenu_EndSkinChanging(object sender, EventArgs e)
        {
            ChartModuleBase currentDemo = ChartModulesInfo.CurrentModule as ChartModuleBase;
            if (currentDemo != null)
                currentDemo.EndSkinChanging();
        }

        public virtual string ItemName { get { return string.Empty; } }
        
        protected virtual void OnLoad(object sender, EventArgs e)
        {
            LookAndFeelMenu.RegisterDefaultBonusSkin();
            this.CreateMenu();
            this.RegisterTutorials();
            ModuleInfo item = ModulesInfo.GetItem(ItemName);
            if (item != null)
            {
                this.ShowModule(item.Name);
            }
        }

        protected virtual void RegisterTutorials()
        {
        }

        protected virtual void SetFormParam()
        {
        }
        public void ShowModule(string name)
        {
            this.pnlMain.Parent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.ShowModule(name, this.pnlMain, this.defaultLookAndFeel1);
            this.SetFormParam();

            TutorialControlBase tModule = ModulesInfo.Instance.CurrentModuleBase.TModule as TutorialControlBase;
            if (tModule != null)
            {
                tModule.RunActiveDemo();
            }
        }

        protected virtual void ShowModule(string name, DevExpress.XtraEditors.PanelControl panel, DevExpress.LookAndFeel.DefaultLookAndFeel lookAndFeel)
        {
 
        }

    }
}
