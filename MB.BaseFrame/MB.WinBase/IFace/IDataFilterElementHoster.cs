using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MB.WinBase.Common; 
namespace MB.WinBase.IFace {
    /// <summary>
    /// 业务类可选需要实现的接口以满足个性化设置的需要。
    /// </summary>
    public interface IDataFilterElementHoster {
        /// <summary>
        /// 创建查询过滤控件后调用的方法。
        /// </summary>
        /// <param name="editCtl"></param>
        /// <param name="cfgInfo"></param>
        void AfterCreateFilterCtl(Control editCtl, DataFilterElementCfgInfo cfgInfo);
    }
}
