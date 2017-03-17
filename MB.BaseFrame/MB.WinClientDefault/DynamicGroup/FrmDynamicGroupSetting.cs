using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using MB.Util.Model;
using MB.WinBase;
using MB.WinBase.Common.DynamicGroup;
using MB.WinBase.IFace;
using MB.WinClientDefault.DynamicGroup;
using MB.WinClientDefault.QueryFilter;

namespace MB.WinClientDefault.DynamicGroup {

    /// <summary>
    /// 动态聚组，汇总列，分组列，汇总条件设定
    /// </summary>
    public partial class FrmDynamicGroupSetting : DevExpress.XtraEditors.XtraForm {

        private IClientRuleQueryBase _ClientRule;//客户端业务类,在构造函数中构造
        private IViewDynamicGroupGridForm _DynamicGroupGridHost; //需要显示动态聚组结果的Form
        private IQueryFilterForm _QueryFilterFrm; //过滤Form，表示当前窗口是从过滤窗口打开
        private DynamicGroupUIHelper _DynamicGroupUIHelper;//帮助UI处理动态聚组


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dynamicGroupViewHost">打开动态聚组窗口的HOST窗口</param>
        /// <param name="clientRule">客户端UI RULE</param>
        public FrmDynamicGroupSetting(IViewDynamicGroupGridForm dynamicGroupViewHost,
            IClientRuleQueryBase clientRule)
            : this(dynamicGroupViewHost, clientRule, null) {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dynamicGroupViewHost">打开动态聚组窗口的HOST窗口</param>
        /// <param name="clientRule">客户端UI RULE</param>
        /// <param name="filterForm">数据筛选窗口,证明在查询时打开动态聚组窗口</param>
        public FrmDynamicGroupSetting(IViewDynamicGroupGridForm dynamicGroupViewHost,
            IClientRuleQueryBase clientRule, IQueryFilterForm filterForm) {
            InitializeComponent();

            try {
                if (clientRule == null)
                    throw new MB.Util.APPException("clientRule不能为空");

                if (dynamicGroupViewHost == null)
                    throw new MB.Util.APPException("dynamicGroupViewForm不能为空");

                _DynamicGroupGridHost = dynamicGroupViewHost;
                _ClientRule = clientRule;
                _QueryFilterFrm = filterForm;
                _DynamicGroupUIHelper = new DynamicGroupUIHelper(_ClientRule, gridControl);


                _DynamicGroupUIHelper.BindDynamicGroupColumnsToGrid();
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex, string.Format("动态聚组设定构造函数出错:{0}", ex.ToString()));
            }

        }

        #region 界面事件
        /// <summary>
        /// 窗口Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XtraFrmDynamicGroupSetting_Load(object sender, EventArgs e) {
            try {

            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }

        }

        /// <summary>
        /// 确定分组列，汇总列，汇总条件的设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e) {
            try {
                List<DynamicGroupUIColumns> dyUIColumns = new List<DynamicGroupUIColumns>();
                DynamicGroupSetting setting = _DynamicGroupUIHelper.GetDynamicGroupSetting(ref dyUIColumns);

                if (setting == null || setting.DataAreaFields == null ||
                    setting.DataAreaFields.Count <= 0) {
                    MB.WinBase.MessageBoxEx.Show("请设置聚组条件");
                }
                else {
                    _DynamicGroupGridHost.DynamicGroupSettingForQuery = setting;
                    _DynamicGroupUIHelper.SaveDynamicGroupSettings(dyUIColumns);
                    MB.WinBase.MessageBoxEx.Show("动态聚组的条件保存成功");
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write(string.Format("动态聚组设定汇总列出错:{0}", ex.ToString()));
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }

        }

        /// <summary>
        /// 清空分组列，包括缓存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e) {
            try {
                _DynamicGroupUIHelper.ClearDynamicGroupSetting();
                _DynamicGroupUIHelper.BindDynamicGroupColumnsToGrid();
            }
            catch (Exception ex) {
                MB.Util.TraceEx.Write(string.Format("动态聚组清空汇总列出错:{0}", ex.ToString()));
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 当Form被关闭的时候触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmDynamicGroupSetting_FormClosing(object sender, FormClosingEventArgs e) {
            if (_QueryFilterFrm != null && this.DialogResult == System.Windows.Forms.DialogResult.OK) {
                var dySetting = _DynamicGroupGridHost.DynamicGroupSettingForQuery;
                if (dySetting == null || dySetting.DataAreaFields == null ||
                    dySetting.DataAreaFields.Count <= 0) {
                    MB.WinBase.MessageBoxEx.Show("请设置聚组条件");
                    e.Cancel = true;
                }
            }

        }

        /// <summary>
        /// 当AGGType为空时，也就是当该列是分组列时，只允许被选中，
        /// 其他字段不能被编辑
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">CancelEventArgs</param>
        private void gridView_ShowingEditor(object sender, CancelEventArgs e) {
            GridView view = sender as GridView;
            int rowHandler = gridView.FocusedRowHandle;
            GridColumn column = gridView.Columns[DynamicGroupUIHelper.DYMANIC_GROUP_AGG_TYPE];
            object aggType = view.GetRowCellValue(rowHandler, column);
            string currentColumnName = gridView.FocusedColumn.FieldName;
            if (aggType != null &&
                string.Compare(aggType.ToString(), string.Empty) == 0 &&
                string.Compare(currentColumnName, DynamicGroupUIHelper.DYMANIC_GROUP_COL_SELECTED) != 0) {
                e.Cancel = true;
            }
        }




        #endregion

        /// <summary>
        /// 根据Grid得到动态聚组的最终结果，用来传递到服务端
        /// </summary>
        /// <returns></returns>
        public MB.Util.Model.DynamicGroupSetting GetDynamicGroupSetting() {
            return _DynamicGroupUIHelper.GetDynamicGroupSetting();
        }


    }





}


   
