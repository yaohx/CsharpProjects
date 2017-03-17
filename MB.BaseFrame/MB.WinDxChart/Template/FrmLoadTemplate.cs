using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.Util.Model.Chart;

namespace MB.WinDxChart.Template
{
    public partial class FrmLoadTemplate : Form
    {
        private TemplateLoadType _TemplateType;
        private object _CurrentTemplate;
        public FrmLoadTemplate(TemplateLoadType templateType)
        {
            InitializeComponent();

            _TemplateType = templateType;
        }

        private void InitForm()
        {
            if (_TemplateType.Equals(TemplateLoadType.ChartTemplate))
            {
                bindingChartTemplate();
            }
            else if (_TemplateType.Equals(TemplateLoadType.LayoutTemplate))
            {
                bindingLayoutTemplate();
            }
        }

        private void bindingChartTemplate()
        {
            string userCode = MB.WinBase.AppEnvironmentSetting.Instance.CurrentLoginUserInfo.USER_CODE;
            List<ChartTemplateInfo> chartTemplates = MB.WinDxChart.Chart.DxChartControlHelper.Instance.GetChartTemplateByUser(userCode);

            setGridStyle();

            this.grdMain.DataSource = chartTemplates.ToArray();
        }

        private void setGridStyle()
        {
            this.gvTemplate.Columns.Clear();
            DevExpress.XtraGrid.Columns.GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn.FieldName = "NAME";
            gridColumn.Caption = "名称";
            gridColumn.OptionsColumn.AllowEdit = false;
            gridColumn.OptionsColumn.ReadOnly = true;
            gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gridColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvTemplate.Columns.Add(gridColumn);
        }

        private void bindingLayoutTemplate()
        {
            string userCode = MB.WinBase.AppEnvironmentSetting.Instance.CurrentLoginUserInfo.USER_CODE;
            List<ChartLayoutTemplateInfo> layoutTemplates = MB.WinDxChart.Chart.DxChartControlHelper.Instance.GetLayoutTemplateByUser(userCode);

            setGridStyle();
            this.grdMain.DataSource = layoutTemplates;
        }

        /// <summary>
        /// 获取加载模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (this.gvTemplate.FocusedRowHandle < 0) return;

                _CurrentTemplate = this.gvTemplate.GetFocusedRow();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public object CurrentTemplate
        {
            get { return _CurrentTemplate; }
        }
    }

    public enum TemplateLoadType
    {
        /// <summary>
        /// 图表模板
        /// </summary>
        ChartTemplate,
        /// <summary>
        /// 布局模板
        /// </summary>
        LayoutTemplate
    }
}
