using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinClientDefault.QueryFilter
{
    public partial class ucCombineFilterControl : UserControl
    {
        private ucFilterCondition _SimpleFilterEdit;
        private ucAdvanceFilterControl _AdvanceFilter;
        private DevExpress.XtraGrid.GridControl _MainGridCtl;

        private string _FilterCfgName;
        private MB.WinBase.IFace.IClientRuleQueryBase _ClientRule;

        public ucCombineFilterControl(MB.WinBase.IFace.IClientRuleQueryBase clientRule, DevExpress.XtraGrid.GridControl mainGridCtl, string filterCfgName)
        {
            InitializeComponent();

            _ClientRule = clientRule;
            _FilterCfgName = filterCfgName;
            _MainGridCtl = mainGridCtl;
        }

        private void tabCtlFilterMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e) {

        }
        public void IniCreateFilterElements() {
            if (_ClientRule != null) {
                _SimpleFilterEdit = new ucFilterCondition(_ClientRule, _FilterCfgName);
                panQuickFilter.Controls.Add(_SimpleFilterEdit);
                _SimpleFilterEdit.Dock = DockStyle.Fill;
                
            }
        }

        private void showAdvanceFilter() {
            try {
                if (_AdvanceFilter == null) {
                    _AdvanceFilter = new ucAdvanceFilterControl();
                    _AdvanceFilter.IniLoadFilterControl(_ClientRule,_MainGridCtl);
                    _AdvanceFilter.Dock = DockStyle.Fill;
                    panAdvanceFilter.Controls.Add(_AdvanceFilter);
                }
                _AdvanceFilter.BringToFront();

            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }
    }
}
