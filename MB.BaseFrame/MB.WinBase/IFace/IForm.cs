using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.IFace {
    /// <summary>
    /// 创建的窗口必须要实现的接口。
    /// </summary>
    public interface IForm {
        /// <summary>
        /// 客户端关联处理类，连接浏览，编辑，查询的 桥梁。
        /// </summary>
        IClientRuleQueryBase ClientRuleObject { get; set; } 
        /// <summary>
        /// 当前窗口的类型。
        /// </summary>
        Common.ClientUIType ActiveUIType { get; }
        /// <summary>
        /// 关闭当前窗口。
        /// </summary>
        void Close();
    }
}
