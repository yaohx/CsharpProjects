using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using MB.Util.Model.Chart;
using MB.WinDxChart.IFace;
using MB.WinBase.Common;
using MB.WinDxChart.Chart;
using System.IO;
using MB.WinDxChart.Template;

namespace MB.WinDxChart.Modules
{

    public partial class ChartModuleBase : TutorialControl, IChartControl
    {
        protected PanelControl panel;
        protected CheckEdit checkEditShowLabels;

        protected bool CheckEditShowLabelsVisible
        {
            get { return checkEditShowLabels.Visible; }
            set { checkEditShowLabels.Visible = value; }
        }
        protected bool ShowLabels
        {
            get { return checkEditShowLabels.Checked; }
            set { checkEditShowLabels.Checked = value; }
        }
        public virtual ChartControl ChartControl { get { return null; } }

        public string AppearanceName
        {
            get { return ChartControl == null ? String.Empty : ChartControl.AppearanceName; }
            set { SetAppearanceName(value); }
        }
        public string PaletteName
        {
            get { return ChartControl == null ? String.Empty : ChartControl.PaletteName; }
            set
            {
                if (ChartControl != null)
                    ChartControl.PaletteName = value;
            }
        }

        public ChartModuleBase()
        {
            InitializeComponent();
        }
        protected void SetNumericOptions(Series series, NumericFormat format, int precision)
        {
            series.PointOptions.ValueNumericOptions.Format = format;
            series.PointOptions.ValueNumericOptions.Precision = precision;
        }
        protected virtual void ChartDemoBase_Load(object sender, EventArgs e)
        {
            if (ChartControl != null && !DesignMode)
            {
                InitControls();
                UpdateControls();
            }
        }
        protected virtual void SetAppearanceName(string appearanceName)
        {
            if (ChartControl != null)
                ChartControl.AppearanceName = appearanceName;
        }
        protected virtual void checkEditShowLabels_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Series series in ChartControl.Series)
                series.Label.Visible = checkEditShowLabels.Checked;
            UpdateControls();
        }
        protected virtual void InitControls()
        {
        }
        public virtual void UpdateControls()
        {
        }
        public virtual void BeginSkinChanging()
        {
        }
        public virtual void EndSkinChanging()
        {
        }

        protected virtual void SetChartControlStyle()
        {
 
        }

        #region IChartControl 成员

        private readonly static string CHART_VIEW_CFG = "ChartView";
        private static string viewType = string.Empty;
        public static ChartViewCfgInfo _ChartViewCfg;
        public WinBase.IFace.IClientRuleQueryBase _ClientRule;
        public virtual void InitChartControl(WinBase.IFace.IClientRuleQueryBase clientRule)
        {
            var cfgs = clientRule.UIRuleXmlConfigInfo.GetDefaultChartViewCfg();
            if (cfgs != null && cfgs.Count > 0)
            {
                _ChartViewCfg = cfgs[CHART_VIEW_CFG];
                viewType = _ChartViewCfg.ViewType;
            }
        }

        public virtual void BindingChartControl(object datasource)
        {
            try
            {
                ChartControl.Series.Clear();

                SetChartControlStyle();

                ChartControl.DataSource = datasource;
                if (ChartControl.Series.Count > 0) return;

                DataSet ds = datasource as DataSet;
                ChartMappingHelper helper = new ChartMappingHelper(_ClientRule);
                List<ChartSeriesInfo> series = helper.ConvertToChartSeries(ds.Tables[0], _ChartViewCfg.ChartMapping, viewType);
                if (series == null || series.Count == 0)
                    throw new MB.Util.APPException("ChartSeries转换失败，请检查数据源与配置的Mapping项是否匹配!", Util.APPMessageType.DisplayToUser);

                DxChartControlHelper.Instance.BindingDxChartControl(ChartControl, series);
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        public virtual void SaveChartControl(ChartTemplateInfo template)
        {
            try
            {
                if (template.ID <= 0 || string.IsNullOrEmpty(template.NAME))
                {
                    string tempType = template.TEMPLATE_TYPE;
                    string name = template.NAME;

                    FrmSaveTemplate frm = new FrmSaveTemplate(tempType, name);
                    frm.ShowDialog();

                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {

                        template.NAME = frm.TemplateName;
                        template.TEMPLATE_TYPE = frm.TemplateType;
                    }
                    else return;
                }

                string basePath = AppDomain.CurrentDomain.BaseDirectory + "\\ChartTemplate\\";
                if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);

                string path = basePath + template.TEMPLATE_TYPE + "_" + template.NAME + ".xml";
                ChartControl.SaveToFile(path);

                byte[] file = File.ReadAllBytes(path);

                template.TEMPLATE_FILE = file;
                File.Delete(path);

                MB.WinDxChart.Chart.DxChartControlHelper.Instance.SaveChartTemplate(template);

            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }
        #endregion

        private ChartTemplateInfo _Template;
        public ChartTemplateInfo Template
        {
            get { return _Template; }
            set { _Template = value; }
        }
    }
}
