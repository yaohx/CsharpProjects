using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.WinDxChart.IFace;
using MB.Util.Model.Chart;
using System.Windows.Forms;
using MB.WinBase.IFace;
using System.ComponentModel;
using System.Data;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Native;

namespace MB.WinDxChart.Chart
{
    public class ChartTemplateMenu
    {
        ChartTemplateHelper _TemplateHelper;
        IChartTemplateClient _TemplateClient;
        private System.Windows.Forms.ContextMenu _TemplateMenu;
        private DevExpress.XtraGrid.GridControl _XtraGrid;
        private MB.WinBase.IFace.IForm _ContainerForm;

        public ChartTemplateMenu(DevExpress.XtraGrid.GridControl xtraGrid)
        {
            _XtraGrid = xtraGrid;
            _TemplateMenu = new System.Windows.Forms.ContextMenu();
            if (xtraGrid.Parent != null)
            {
                _ContainerForm = MB.WinBase.ShareLib.Instance.GetControlParentForm(xtraGrid) as MB.WinBase.IFace.IForm;
            }

            _TemplateHelper = new ChartTemplateHelper();
            _TemplateClient = _TemplateHelper.CreateTemplateClient();

            var list = getTemplateList(xtraGrid);
            CreateMenuItems(list);
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ContextMenu ChartContextMenu
        {
            get
            {
                return _TemplateMenu;
            }
        }
        private List<ChartTemplateInfo> getTemplateList(DevExpress.XtraGrid.GridControl xtraGrid)
        {
            if (_ContainerForm != null && _ContainerForm.ClientRuleObject != null)
            {
                string gridName = _ContainerForm.GetType().FullName + "~" + xtraGrid.Name + "~" + _ContainerForm.GetType().Assembly.GetName().Name;
                string ruleName = _ContainerForm.ClientRuleObject.GetType().FullName + "~" + _ContainerForm.ClientRuleObject.GetType().Assembly.GetName().Name;

                List<MB.Util.Model.QueryParameterInfo> filterParams = new List<Util.Model.QueryParameterInfo>();
                filterParams.Add(new Util.Model.QueryParameterInfo("RULE_PATH", ruleName, Util.DataFilterConditions.Equal));
                filterParams.Add(new Util.Model.QueryParameterInfo("GRID_NAME", gridName, Util.DataFilterConditions.Equal));

                return _TemplateClient.GetObjects(filterParams.ToArray());
            }
            
            return null;
        }

        public virtual void CreateMenuItems(List<ChartTemplateInfo> templateList)
        {
            if (_ContainerForm != null && _ContainerForm.ClientRuleObject != null)
            {
                string gridName = _ContainerForm.GetType().FullName + "~" + _XtraGrid.Name + "~" + _ContainerForm.GetType().Assembly.GetName().Name;
                string ruleName = _ContainerForm.ClientRuleObject.GetType().FullName + "~" + _ContainerForm.ClientRuleObject.GetType().Assembly.GetName().Name;

                ChartTemplateInfo templateInfo = new ChartTemplateInfo();
                templateInfo.RULE_PATH = ruleName;
                templateInfo.GRID_NAME = gridName;
                templateInfo.TEMPLATE_TYPE = "PUBLIC";
                templateInfo.CREATE_USER = MB.WinBase.AppEnvironmentSetting.Instance.CurrentLoginUserInfo.USER_CODE;

                ChartMenu addMenu = new ChartMenu("默认图表", new EventHandler(menuItemClick), "ChartModule", templateInfo);
                _TemplateMenu.MenuItems.Add(addMenu);
            }

            if (templateList == null || templateList.Count == 0) return;
            foreach (var template in templateList)
            {
                ChartMenu menu = new ChartMenu(template.NAME, new EventHandler(menuItemClick), "TemplateChartModule", template);
                _TemplateMenu.MenuItems.Add(menu);
            }
        }

        private void menuItemClick(object sender, System.EventArgs e)
        {
            try
            {
                ChartMenu menu = sender as ChartMenu;
                if (menu == null)
                    return;

                ChartTemplateInfo template = menu.Template;
                showChart(_XtraGrid, menu.ItemName, ref template);

                menu.Template = template;
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        public void showChart(DevExpress.XtraGrid.GridControl xtraGrid,string itemName,ref ChartTemplateInfo template)
        {
            try
            {
                object datasource = xtraGrid.DataSource;
                DataSet ds = null;
                if (_ContainerForm.ClientRuleObject.ClientLayoutAttribute.CommunicationDataType == WinBase.Common.CommunicationDataType.DataSet)
                {
                    ds = datasource as DataSet;
                }
                else
                {
                    MB.WinBase.Binding.BindingSourceEx bindingSource = datasource as MB.WinBase.Binding.BindingSourceEx;
                    Type T = bindingSource.Current.GetType();
                    ds = MB.Util.MyConvert.Instance.ConvertEntityToDataSet(T, bindingSource.List, null);
                }

                MB.WinDxChart.FrmEditChart frmChart = new WinDxChart.FrmEditChart(itemName, _ContainerForm.ClientRuleObject, template,ds);
                frmChart.ShowDialog();

                template = frmChart.CurTemplate;
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write(ex.Message);
            }
        }
    }

    public class ChartMenu:System.Windows.Forms.MenuItem
    {
        private ChartTemplateInfo _Template;
        private string _ItemName;
        public ChartMenu(string text, EventHandler click, string itemName,ChartTemplateInfo templateInfo)
            : base(text, click)
        {
            _ItemName = itemName;
            _Template = templateInfo;
        }

        public string ItemName
        {
            get { return _ItemName; }
        }

        public ChartTemplateInfo Template
        {
            get
            {
                return _Template;
            }
            set
            {
                _Template = value;
            }
        }
    }
}
