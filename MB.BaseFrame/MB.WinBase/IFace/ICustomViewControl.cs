using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MB.WinBase.IFace {
    /// <summary>
    /// 自定义查询控件需要实现的接口
    /// </summary>
    public interface ICustomViewControl {
        /// <summary>
        /// 将数据源绑定到自定义控件.
        /// </summary>
        /// <param name="clientRule">当前分析的业务对象</param>
        /// <param name="dataSource">数据源</param>
        void CreateDataBinding(MB.WinBase.IFace.IClientRuleQueryBase clientRule, object dataSource);

        /// <summary>
        /// 导出数据
        /// </summary>
        void Export();
    }
}
