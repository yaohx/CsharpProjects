using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.IFace {
    /// <summary>
    /// 自定义查询控件必须要实现的接口
    /// </summary>
    public interface IFilterConditionCtl
    {
        /// <summary>
        ///  获取输入的查询条件。
        /// </summary>
        /// <returns></returns>
        MB.Util.Model.QueryParameterInfo[] GetQueryParameters();

        /// <summary>
        /// 判断是否可以查询所有数据。
        /// </summary>
        bool AllowEmptyFilter { get; }
    }

    public interface IPreFilterConditionCtl
    {
        /// <summary>
        ///  获取已输入的查询条件。
        /// </summary>
        /// <returns></returns>
        MB.Util.Model.QueryParameterInfo[] GetPreQueryParameters();
    }
}
