using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MB.WinBase.Binding;  
namespace MB.WinClientDefault.Ctls {
    [ToolboxItem(true)]
    public partial class ucEntityEditBaseData : UserControl {
        private static readonly string CREATE_USER = "CREATE_USER";
        private static readonly string CREATE_DATE = "CREATE_DATE";
        private static readonly string LAST_MODIFIED_USER = "LAST_MODIFIED_USER";
        private static readonly string LAST_MODIFIED_DATE = "LAST_MODIFIED_DATE";
        public ucEntityEditBaseData() {
            InitializeComponent();

            txtCREATE_USER.Text = "测试员";
            txtLAST_MODIFIED_USER.Text = "测试员"; 
        }

        private IDataBindingProvider _DataBindingProvider;
        [EditorAttribute(typeof(MB.WinBase.Binding.DataBindingProviderDesign), typeof(System.Drawing.Design.UITypeEditor))] 
        [Description("数据源绑定")]
        public IDataBindingProvider DataBindingProvider {
            get {
               MB.WinBase.Binding.DataBindingProviderDesign.SetParentContainer(this.ParentForm.Container);
                return _DataBindingProvider;
            }
            set {
                _DataBindingProvider = value;
             
                databindingCtl();
            }
        }

        #region 内部函数处理...
        private void databindingCtl() {
            if (_DataBindingProvider == null || _DataBindingProvider.ColumnXmlCfgs == null 
                                    || _DataBindingProvider.DataBindings == null) return;

            if (_DataBindingProvider.ColumnXmlCfgs.ContainsKey(CREATE_USER)) {
                if( _DataBindingProvider.DataBindings.ContainsKey(txtCREATE_USER)) 
                    _DataBindingProvider.DataBindings.Add(txtCREATE_USER, new DesignColumnXmlCfgInfo(CREATE_USER, "创建用户"));
            }
            if (_DataBindingProvider.ColumnXmlCfgs.ContainsKey(CREATE_DATE)) {
                if (_DataBindingProvider.DataBindings.ContainsKey(dtpCREATE_DATE)) 
                    _DataBindingProvider.DataBindings.Add(dtpCREATE_DATE, new DesignColumnXmlCfgInfo(CREATE_DATE, "创建日期"));
            }
            if (_DataBindingProvider.ColumnXmlCfgs.ContainsKey(LAST_MODIFIED_USER)) {
                if (_DataBindingProvider.DataBindings.ContainsKey(txtLAST_MODIFIED_USER)) 
                    _DataBindingProvider.DataBindings.Add(txtLAST_MODIFIED_USER, new DesignColumnXmlCfgInfo(LAST_MODIFIED_USER, "最后修改人"));
            }
            if (_DataBindingProvider.ColumnXmlCfgs.ContainsKey(LAST_MODIFIED_DATE)) {
                if (_DataBindingProvider.DataBindings.ContainsKey(dtpLAST_MODIFIED_DATE)) 
                    _DataBindingProvider.DataBindings.Add(dtpLAST_MODIFIED_DATE, new DesignColumnXmlCfgInfo(LAST_MODIFIED_DATE, "日期"));
            }
            
        }
        #endregion 内部函数处理...
    }
}
