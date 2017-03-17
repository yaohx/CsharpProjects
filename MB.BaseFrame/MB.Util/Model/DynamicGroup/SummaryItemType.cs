using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace MB.Util.Model
{
    /// <summary>
    /// 动态聚组，聚合的条件
    /// </summary>
    public enum SummaryItemType : int
    {
        [Description("空")]
        None,
        [Description("求平均")]
        Average,
        [Description("求总数")]
        Count,
        [Description("求最大")]
        Max,
        [Description("求最小")]
        Min,
        [Description("求和")]
        Sum
    }
}
