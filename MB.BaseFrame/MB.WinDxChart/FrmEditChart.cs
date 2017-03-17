using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using MB.WinDxChart.Chart;
using MB.WinBase.Common;
using DevExpress.Utils;
using MB.WinBase.IFace;
using System.IO;
using System.Drawing.Imaging;
using MB.WinDxChart.Template;
using MB.Util.Model.Chart;
using System.Collections;
using DevExpress.XtraBars;
using DevExpress.DXperience.Demos;
using MB.WinDxChart.Common;
using MB.WinDxChart.Modules;
using DevExpress.XtraCharts.Native;

namespace MB.WinDxChart
{
    public partial class FrmEditChart : FrmChartMain
    {
        #region 自定义变量...
        private IClientRuleQueryBase _ClientRule;
        private object _DataSource;
        private ChartTemplateInfo _CurrentChartTemplate;
        private string _ItemName;
        #endregion

        #region 构造函数...
        public FrmEditChart(string itemName,IClientRuleQueryBase clientRule,ChartTemplateInfo template,object dataSource)
        {
            InitializeComponent();

            _ItemName = itemName;
            _ClientRule = clientRule;
            _DataSource = dataSource;
            _CurrentChartTemplate = template;
        }
        #endregion

        public override string ItemName
        {
            get
            {
                return _ItemName;
            }
        }

        private BarSubItem _ExtendItem;
        public override BarSubItem ResetExtendBarSubItemMenu
        {
            get
            {
                _ExtendItem = new BarSubItem();

                BarItem template = new ButtonBarItem(this.manager, "保存模板", new ItemClickEventHandler(miItemSave_Click));
                _ExtendItem.ItemLinks.Add(template);
                BarItem templateManager = new ButtonBarItem(this.manager, "模板管理", new ItemClickEventHandler(miItemManager_Click));
                _ExtendItem.ItemLinks.Add(templateManager);

                return _ExtendItem;
            }
            set
            {
                _ExtendItem = value;
            }
        }

        void miItemSave_Click(object sender, ItemClickEventArgs e)
        {
            ChartModulesInfo.SaveChartTemplate(_CurrentChartTemplate);
        }

        void miItemManager_Click(object sender, ItemClickEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                FrmTemplateManager frm = new FrmTemplateManager(_ClientRule, _CurrentChartTemplate.GRID_NAME, TemplateLoadType.ChartTemplate);
                frm.InitForm();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        protected override void RegisterTutorials()
        {
            MB.WinDxChart.Common.RegisterTutorials.Register();    
        }

        protected override void ShowModule(string name, DevExpress.XtraEditors.PanelControl panel, DevExpress.LookAndFeel.DefaultLookAndFeel lookAndFeel)
        {
            ChartModulesInfo.ShowModule(name, panel, (ChartAppearanceMenu)this.appearanceMenu, manager, null);          
        }

        protected override void SetFormParam()
        {
            base.SetFormParam();
            ChartModulesInfo.InitChartControl(_ClientRule, _DataSource,_CurrentChartTemplate);
        }

        public ChartTemplateInfo CurTemplate
        {
            get { return ChartModulesInfo.GetChartTemplate(); }
        }
    }
}
