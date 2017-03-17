using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.IFace {
    /// <summary>
    /// 获取数据帮助窗口中调用浏览数据列表的控件必须实现的接口。
    /// </summary>
    public interface IDataAssistantListControl {
        /// <summary>
        /// 设置列表数据源。
        /// </summary>
        /// <param name="clientRule"></param>
        /// <param name="dataSource"></param>
        void SetDataSource(MB.WinBase.IFace.IClientRuleQueryBase clientRule, object dataSource);
        /// <summary>
        /// 双击选择列表数据源后产生的事件。
        /// </summary>
        event GetObjectDataAssistantEventHandle AfterSelectData;
        /// <summary>
        /// 判断是否为多选。
        /// </summary>
        bool MultiSelect { get; set; }

        /// <summary>
        /// 选择当前列表中的数据。
        /// </summary>
        /// <param name="checkAll"></param>
        void CheckListViewItem(bool checkAll);
        void CheckListViewItems(IEnumerable<int> selectedIds);
        /// <summary>
        /// 获取选择的数据。
        /// </summary>
        /// <returns></returns>
        object[] GetSelectRows();

        /// <summary>
        /// 获取选择的数据。
        /// </summary>
        /// <returns></returns>
        IDictionary<int, object> GetSelectRowsWithIndex();

        /// <summary>
        /// XML 配置信息。
        /// </summary>
        MB.WinBase.Common.ColumnEditCfgInfo ColumnEditCfgInfo { get; set; }
    }
}
