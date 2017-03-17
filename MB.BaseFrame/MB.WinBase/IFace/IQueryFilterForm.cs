using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.IFace {
    /// <summary>
    /// 过滤查询窗口必须实现的接口
    /// </summary>
    public interface IQueryFilterForm : IForm {
        /// <summary>
        /// 初始化创建
        /// </summary>
        void IniCreateFilterElements();
        /// <summary>
        ///  DataFilterElements 配置名称。
        /// </summary>
        string DataFilterElementsName { get; set; }
        /// <summary>
        /// 主浏览窗口。
        /// </summary>
        IViewGridForm ViewGridForm { get; set; }
        /// <summary>
        /// 条件编辑确认后响应的事件。
        /// </summary>
        event QueryFilterInputEventHandle AfterInputQueryParameter;
    }
    #region 自定义事件处理相关...
    public delegate void QueryFilterInputEventHandle(object sender, QueryFilterInputEventArgs arg);
    public class QueryFilterInputEventArgs {
        private MB.Util.Model.QueryParameterInfo[] _QueryParamters;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_QueryParamters"></param>
        public QueryFilterInputEventArgs(MB.Util.Model.QueryParameterInfo[] queryParamters) {
            _QueryParamters = queryParamters;
        }
        /// <summary>
        /// 当前选择的数据。
        /// </summary>
        public MB.Util.Model.QueryParameterInfo[] QueryParamters {
            get {
                return _QueryParamters;
            }
            set {
                _QueryParamters = value;
            }
        }

    }
    #endregion 自定义事件处理相关...
}
