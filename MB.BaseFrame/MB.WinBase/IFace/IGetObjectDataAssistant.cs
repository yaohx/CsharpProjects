using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.IFace {
    /// <summary>
    /// 获取数据帮助窗口必须实现的接口。
    /// </summary>
    public interface IDataAssistant {
        /// <summary>
        /// 选择数据后相应的实践。
        /// </summary>
        event GetObjectDataAssistantEventHandle AfterGetObjectData;
        /// <summary>
        /// 是否可以多选。
        /// </summary>
        bool MultiSelect { get; set; }
        /// <summary>
        ///  执行获取数据的查询对象。
        /// </summary>
        IQueryObject QueryObject { get; set; }
        /// <summary>
        /// 获取或设置是否显示过滤Panel。
        /// </summary>
        bool HideFilterPane { get; set; }
        /// <summary>
        /// 当隐藏过滤的Panel的时候，可以默认注入查询过滤条件
        /// </summary>
        List<MB.Util.Model.QueryParameterInfo> FilterParametersIfNoFiterPanel { get; set; }
        /// <summary>
        ///  获取对象数据。
        /// </summary>
        /// <param name="dataInDocType"></param>
        /// <param name="filterParameters"></param>
        /// <returns></returns>
        IList GetFilterObjects(int dataInDocType, List<MB.Util.Model.QueryParameterInfo> filterParameters);

        /// <summary>
        /// 多选最大行数
        /// </summary>
        int MaxSelectRows { get; set; }
    }

    /// <summary>
    /// 获取数据助手控件需要必须实现的接口。
    /// </summary>
    public interface IGetObjectDataAssistant : IDataAssistant {

        /// <summary>
        ///  业务类。
        /// </summary>
        MB.WinBase.IFace.IClientRuleQueryBase FilterClientRule{get;set;}
        ///// <summary>
        ///// XML 配置文件名称。
        ///// </summary>
        //string FilterCfgName { get; set; }
        /// <summary>
        /// XML 配置信息。
        /// </summary>
        MB.WinBase.Common.ColumnEditCfgInfo ClumnEditCfgInfo { get; set; }

        /// <summary>
        /// 调用的描述
        /// </summary>
        MB.WinBase.Common.InvokeDataSourceDescInfo  InvokeDataSourceDesc { get; set; }


        /// <summary>
        /// 在通过数据助手获取数据时需要通知的对象。
        /// </summary>
        IInvokeDataAssistantHoster InvokeFilterParentFormHoster { get; set; }

        /// <summary>
        /// 调用它的主控件。
        /// </summary>
        object InvokeParentControl { get; set; }

        /// <summary>
        /// 当前编辑对象
        /// </summary>
        object CurrentEditObject{get;set;}
    }
    #region 自定义事件处理相关...

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="arg"></param>
    public delegate void GetObjectDataAssistantEventHandle(object sender, GetObjectDataAssistantEventArgs arg);
    public class GetObjectDataAssistantEventArgs : System.EventArgs {
        private object[] _SelecedRows;
        private bool _Handed;
        public GetObjectDataAssistantEventArgs() {
        }
        public GetObjectDataAssistantEventArgs(object[] selecedRows) {
            _SelecedRows = selecedRows;
        }

        /// <summary>
        /// 当前选择的数据。
        /// </summary>
        public object[] SelectedRows {
            get {
                return _SelecedRows;
            }
            set {
                _SelecedRows = value;
            }
        }
        /// <summary>
        /// 判断是否已经处理。
        /// </summary>
        public bool Handed {
            get {
                return _Handed;
            }
            set {
                _Handed = value;
            }
        }
    }
    #endregion 自定义事件处理相关...
}
