using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;

namespace MB.WinBase.Common {
    /// <summary>
    /// UI Command 操作类型。
    /// </summary>
    [Flags]
    public enum UICommandType : int {
        /// <summary>
        /// Nono
        /// </summary>
        [Description("None")]
        None = 0x0000001,
        /// <summary>
        /// 编辑
        /// </summary>
        [Description("编辑")]
        AddNew = 0x0000002,
        /// <summary>
        /// 浏览
        /// </summary>
        [Description("浏览")]
        Open = 0x0000004,
        /// <summary>
        /// 保存
        /// </summary>
        [Description("保存")]
        Save = 0x0000008,
        /// <summary>
        /// 复制
        /// </summary>
        [Description("复制")]
        Copy = 0x0000010,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 0x0000020,
        /// <summary>
        /// 提交
        /// </summary>
        [Description("提交")]
        Submit = 0x0000040,
        /// <summary>
        /// 取消提交
        /// </summary>
        [Description("取消提交")]
        CancelSubmit = 0x0000080,
        /// <summary>
        /// 打印
        /// </summary>
        [Description("打印")]
        Print = 0x0000100,
        /// <summary>
        ///打印浏览 
        /// </summary>
        [Description("打印预览")]
        PrintPreview = 0x0000200,
        /// <summary>
        /// 退出
        /// </summary>
        [Description("退出")]
        Exit = 0x0000400,
        /// <summary>
        /// 系统设置
        /// </summary>
        [Description("系统设置")]
        SysSetting = 0x0000800,
        /// <summary>
        /// 个性化设置
        /// </summary>
        [Description("个性化设置")]
        Individuality = 0x0001000,  //个性化设置
        /// <summary>
        /// 帮助
        /// </summary>
        [Description("帮助")]
        HelpList = 0x0002000, //帮助
        /// <summary>
        /// 关于
        /// </summary>
        [Description("关于")]
        About = 0x0004000, //关于
        /// <summary>
        /// 在线消息
        /// </summary>
        [Description("在线消息")]
        OnlineMessage = 0x0008000, //在线消息
        /// <summary>
        /// 显示计算器
        /// </summary>
        [Description("显示计算器")]
        Calculator = 0x0010000, //显示计算器
        /// <summary>
        ///显示功能模块树 
        /// </summary>
        [Description("显示功能模块树")]
        FunctionTree = 0x0020000, //显示功能模块树
        /// <summary>
        /// 快速查询
        /// </summary>
        [Description("快速查询")]
        Query = 0x0040000, //快速查询
        /// <summary>
        /// 查询摸板
        /// </summary>
        [Description("查询摸板")]
        SearchTemplet = 0x0080000, //查询摸板
        /// <summary>
        /// 刷新
        /// </summary>
        [Description("刷新")]
        Refresh = 0x0100000, //刷新
        /// <summary>
        /// 聚组
        /// </summary>
        [Description("聚组")]
        Aggregation = 0x0200000,  //聚组
        /// <summary>
        /// 导出
        /// </summary>
        [Description("导出")]
        DataExport = 0x0400000,  //导出
        /// <summary>
        /// 导入
        /// </summary>
        [Description("导入")]
        DataImport = 0x0800000,  //导入
        /// <summary>
        /// 保存布局
        /// </summary>
        [Description("保存布局")]
        MdiSaveLayout = 0x1000000,  //保存布局
        /// <summary>
        /// 对象修改
        /// </summary>
        [Description("对象修改")]
        Edit = 0x2000000,
        /// <summary>
        /// 动态聚组
        /// </summary>
        [Description("动态聚组")]
        DynamicAggregation = 0x0400000,  //动态聚组
    }
}
