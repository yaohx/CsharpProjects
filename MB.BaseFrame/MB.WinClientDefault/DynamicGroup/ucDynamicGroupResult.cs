using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MB.WinBase;

namespace MB.WinClientDefault.DynamicGroup {
    /// <summary>
    /// 动态聚组
    /// </summary>
    public partial class ucDynamicGroupResult : UserControl {
        public ucDynamicGroupResult() {
            InitializeComponent();
        }

        private void ucDynamicGroupResult_Load(object sender, EventArgs e) {

            ////订阅动态聚组条件的刷新，当重新设置了动态聚组的条件
            //MB.WinBase.AppMessenger.DefaultMessenger.Subscribe<MB.Util.Model.DynamicGroupSetting>(DynamicGroupCommon.DYNAMIC_CONDITION_SETTING_REFRESH, setting => {
            //    bindDynamicGroupSetting(setting);
            //});
        }

        /// <summary>
        /// 绑定动态聚组查询结果到Grid上
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="uiXmlFile"></param>
        public void BindDynamicResultQueryResult(DataSet dataSource, string uiXmlFile) {
            MB.XWinLib.XtraGrid.XtraGridHelper.Instance.BindingToXtraGridForDynamicGroup(gridResult, dataSource, uiXmlFile);
        }

        #region 内部处理函数
        ///// <summary>
        ///// 绑定动态聚组的分组列，汇总列，汇总条件的设置
        ///// </summary>
        ///// <param name="setting"></param>
        //private void bindDynamicGroupSetting(MB.Util.Model.DynamicGroupSetting setting) {

        //    #region 绑定汇总列到GIRD上

        //    List<DynamicGroupCondition> conditions = DynamicGroupCommon.ConvertDynamicGroupSettingToDataAreaConditions(setting);
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.LoadXml(DynamicGroupCommon.DYNAMIC_CONDITION_UI_XML_CONTENT);
        //    LayoutXmlConfigHelper uiXmlHelper = LayoutXmlConfigHelper.Instance;
        //    var cols = uiXmlHelper.GetColumnPropertys(xmlDoc);
        //    var bindingList = new BindingList<DynamicGroupCondition>(conditions);
        //    MB.XWinLib.XtraGrid.XtraGridHelper.Instance.BindingToXtraGrid(gridConditions, bindingList, cols, null, string.Empty, false);

        //    #endregion

        //    //绑定分组列到label上
        //    StringBuilder builder = null;
        //    foreach (KeyValuePair<string, List<string>> groupByCols in setting.GroupFields) {
        //        if (builder == null) {
        //            builder = new StringBuilder();
        //        }
        //        else {
        //            builder.AppendLine();
        //        }

        //        builder.Append("对象：");
        //        builder.Append(groupByCols.Key);
        //        builder.Append("|");
        //        builder.Append("列：");
        //        for (int i = 0; i < groupByCols.Value.Count; i++) {
        //            builder.Append(groupByCols.Value[i]);
        //            if (i < groupByCols.Value.Count - 1)
        //                builder.Append(",");
        //        }
        //    }
        //    this.lblGroupByColsValue.Text = builder.ToString();
        //}

        

        #endregion
    }
}
