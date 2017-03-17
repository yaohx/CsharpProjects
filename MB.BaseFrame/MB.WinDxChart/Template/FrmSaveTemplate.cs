using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MB.WinBase.Common;
using MB.WinBase.IFace;
using MB.Util;

namespace MB.WinDxChart.Template
{
    public partial class FrmSaveTemplate : Form
    {
        private string _TemplateType;
        private string _TemplateName;
        public FrmSaveTemplate(string templateType,string templateName)
        {
            InitializeComponent();

            _TemplateType = templateType;
            _TemplateName = templateName;

            this.Load += new EventHandler(FrmSaveTemplate_Load);
        }

        void FrmSaveTemplate_Load(object sender, EventArgs e)
        {
            //初始化模板类型
            object datasource = loadTemplateType();

            this.cmbTemplateType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTemplateType.ValueMember = "CODE";
            this.cmbTemplateType.DisplayMember = "NAME";
            this.cmbTemplateType.DataSource = datasource;

            this.cmbTemplateType.SelectedValue = _TemplateType;
            this.txtName.Text = _TemplateName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MB.WinBase.MessageBoxEx.Show("请输入模板名称!");
                    txtName.Focus();
                    return;
                }

                _TemplateType = this.cmbTemplateType.SelectedValue.ToString();
                _TemplateName = this.txtName.Text.Trim();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        public string TemplateType
        {
            get { return _TemplateType; }
            set { _TemplateType = value; }
        }

        public string TemplateName
        {
            get { return _TemplateName; }
            set { _TemplateName = value; }
        }

        private object loadTemplateType()
        {
            string[] arrType = TemplateSource.Type.Split(new char[] { ',' });
            string[] arrMethod = TemplateSource.Method.Split(new char[] { ',' });
            string[] arrParams = null;
            if (arrMethod.Length > 1) arrParams = arrMethod[1].Split(new char[] { ';' });
            IClientRuleQueryBase clientRule = (IClientRuleQueryBase)MB.Util.DllFactory.Instance.LoadObject(arrType[0], arrType[1]);
            var data = MyReflection.Instance.InvokeMethod(clientRule, arrMethod[0], arrParams);
            return data;
        }

        private InvokeDataSourceDescInfo _TemplateSource;
        private InvokeDataSourceDescInfo TemplateSource
        {
            get
            {
                if (_TemplateSource == null)
                    _TemplateSource = new InvokeDataSourceDescInfo("MB.ERP.BaseLibrary.CSysSetting.UIRule.BfCodeUIRule,MB.ERP.BaseLibrary.CSysSetting.dll", "GetSysCodeName,CHART_TEMPLATE_TYPE", "");

                return _TemplateSource;
            }
        }
    }
}
