using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.Util.Model.Chart;
using MB.WinBase.IFace;

namespace MB.WinDxChart.Template
{
    public partial class FrmTemplateManager : Form
    {
        private TemplateLoadType _TemplateType;
        private object _CurrentTemplate;
        private IClientRuleQueryBase _ClientRule;
        private string _GridName;
        private List<ChartTemplateInfo> _TemplateList;
        Dictionary<int, string> _dicTemplate = new Dictionary<int, string>();
        public FrmTemplateManager(IClientRuleQueryBase clientRule,string gridName, TemplateLoadType templateType)
        {
            InitializeComponent();

            _ClientRule = clientRule;
            _GridName = gridName;
            _TemplateType = templateType;

            setGridMenu();
        }

        public void InitForm()
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
            try
            {
                string userCode = MB.WinBase.AppEnvironmentSetting.Instance.CurrentLoginUserInfo.USER_CODE;
                string ruleName = _ClientRule.GetType().FullName + "~" + _ClientRule.GetType().Assembly.GetName().Name;

                List<MB.Util.Model.QueryParameterInfo> filterParams = new List<Util.Model.QueryParameterInfo>();
                filterParams.Add(new Util.Model.QueryParameterInfo("RULE_PATH", ruleName, Util.DataFilterConditions.Equal));
                filterParams.Add(new Util.Model.QueryParameterInfo("GRID_NAME", _GridName, Util.DataFilterConditions.Equal));
                filterParams.Add(new Util.Model.QueryParameterInfo("CREATE_USER", userCode, Util.DataFilterConditions.Equal));

                _TemplateList = WinDxChart.Chart.DxChartControlHelper.Instance.GetChartTemplateList(filterParams.ToArray());
                var data = (from a in _TemplateList select new { a.ID, a.NAME });
                foreach (var key in data)
                {
                    _dicTemplate.Add(key.ID, key.NAME);
                }

                setGridStyle();

                this.grdMain.DataSource = _TemplateList.ToArray();
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        private void setGridMenu()
        {
            ContextMenu context = new System.Windows.Forms.ContextMenu();
            grdMain.ContextMenu = context;


            grdMain.ContextMenu.MenuItems.Add(new MenuItem("删除", itemDelete_Click));
            grdMain.ContextMenu.MenuItems.Add(new MenuItem("刷新", itemRefresh_Click));
        }

        void itemDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除当前行吗？", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                ChartTemplateInfo item = gvTemplate.GetFocusedRow() as ChartTemplateInfo;
                if(item != null) _TemplateList.Remove(item);

                this.grdMain.DataSource = _TemplateList.ToArray();
            }
        }

        void itemRefresh_Click(object sender, EventArgs e)
        {
            InitForm();
        }

        private void setGridStyle()
        {
            this.gvTemplate.Columns.Clear();

            DevExpress.XtraEditors.Repository.RepositoryItemTextEdit item = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            item.ReadOnly = false;
            this.grdMain.RepositoryItems.Add(item);

            DevExpress.XtraGrid.Columns.GridColumn gridColumn = gvTemplate.Columns.Add();
            gridColumn.FieldName = "NAME";
            gridColumn.Caption = "名称";
            gridColumn.OptionsColumn.AllowEdit = true;
            gridColumn.OptionsColumn.ReadOnly = false;
            gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gridColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridColumn.Visible = true;

            gridColumn.ColumnEdit = item;

            DevExpress.XtraEditors.Repository.RepositoryItemDateEdit date = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            date.ReadOnly = true;
            date.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            date.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            
            gridColumn = gvTemplate.Columns.Add();
            gridColumn.FieldName = "CREATE_DATE";
            gridColumn.Caption = "创建日期";
            gridColumn.OptionsColumn.AllowEdit = false;
            gridColumn.OptionsColumn.ReadOnly = true;
            gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gridColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridColumn.Visible = true;
            gridColumn.ColumnEdit = date;

            this.gvTemplate.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvTemplate.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvTemplate.OptionsBehavior.Editable = true;
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
                ChartTemplateInfo[] data = gvTemplate.DataSource as ChartTemplateInfo[];
                foreach (var template in _dicTemplate)
                {
                    var curTemplate = data.Where(o => o.ID.Equals(template.Key)).FirstOrDefault();
                    if (curTemplate == null)
                    {
                        //删除
                        WinDxChart.Chart.DxChartControlHelper.Instance.DeleteTemplate(template.Key);
                    }
                    else
                    {
                        if (curTemplate.NAME.Equals(template.Value)) continue;
                        //修改
                        WinDxChart.Chart.DxChartControlHelper.Instance.SaveChartTemplate(curTemplate);
                    }
                }

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
