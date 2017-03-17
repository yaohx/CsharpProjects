using System;
using System.ComponentModel; 
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.XWinLib.XtraGrid {
    /// <summary>
    /// XtraContextMenuType XtraGrid Context 菜单类型描述。
    /// </summary>
    public enum XtraContextMenuType : int {
        /// <summary>
        /// 新增加。
        /// </summary>
        [Description("增加")]
        Add = 0x00000001,
        /// <summary>
        /// 批量增加。
        /// </summary>
        [Description("批量增加")]
        BatchAdd = 0x00000002,
        /// <summary>
        /// 删除。
        /// </summary>
        [Description("删除")]
        Delete = 0x00000004,
        /// <summary>
        /// 聚组设置。
        /// </summary>
        [Description("聚组设置")]
        Aggregation = 0x00000008,
        /// <summary>
        /// 图表分析。
        /// </summary>
        [Description("图表分析")]
        Chart = 0x00000010,
        /// <summary>
        /// 打印。
        /// </summary>
        [Description("打印")]
        Print = 0x00000020,
        /// <summary>
        /// 显示控制。
        /// </summary>
        [Description("显示控制")]
        ViewControl = 0x00000040,
        /// <summary>
        /// 导出。
        /// </summary>
        [Description("导出")]
        Export = 0x00000080,
        /// <summary>
        /// 模板输出。
        /// </summary>
        [Description("模板输出")]
        ExportAsTemplet = 0x00000100,
        /// <summary>
        /// 导入数据。
        /// </summary>
        [Description("导入数据")]
        DataImport = 0x00000200,
        /// <summary>
        /// 保存Grid状态。
        /// </summary>
        [Description("保存Grid状态")]
        SaveGridState = 0x00000400,

        /// <summary>
        /// 允许列排序。
        /// </summary>
        [Description("允许列排序")]
        ColumnsAllowSort = 0x00000800,
        /// <summary>
        /// 快速填充
        /// </summary>
        [Description("快速填充")]
        QuickInput = 0x00001000,
        /// <summary>
        /// 复制
        /// </summary>
        [Description("复制")]
        Copy = 0x00002000,
        /// <summary>
        /// 粘贴
        /// </summary>
        [Description("粘贴")]
        Past = 0x00004000,
        /// <summary>
        /// 使子节点根据显示的内容排序
        /// </summary>
        [Description("子节点排序")]
        SortChildNode = 0x00008000,
        /// <summary>
        /// 设计图表。
        /// </summary>
        [Description("设计图表")]
        ChartDesign = 0x00010000,
        /// <summary>
        /// Excel编辑
        /// </summary>
        [Description("Excel编辑")]
        ExcelEdit = 0x00020000,
    }
}
