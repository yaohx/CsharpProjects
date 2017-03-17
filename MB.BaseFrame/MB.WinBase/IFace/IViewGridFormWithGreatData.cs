using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.IFace {
    /// <summary>
    /// 支持大数据查询的模块需要实现的接口
    /// </summary>
    public interface IViewGridFormWithGreatData {
        /// <summary>
        /// 是否查询出全部数据，不分页
        /// 这个值在客户端配置 "DataFilter/Elements -> AllowQueryAll"属性
        /// </summary>
        bool IsQueryAll { get; set; }
    }

}
