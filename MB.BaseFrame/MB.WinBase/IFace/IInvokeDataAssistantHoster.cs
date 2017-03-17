using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.WinBase.Common;
using MB.Util.Model;

namespace MB.WinBase.IFace {
    /// <summary>
    /// 数据过滤助手在执行获取数据的过程中需要调用的方法, 需要在承载的主窗口中实现该接口。
    /// </summary>
    public interface IInvokeDataAssistantHoster {
        /// <summary>
        /// 在显示查询窗口前发生。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void BeforeShowDataAssistant(object sender, InvokeDataAssistantHosterEventArgs args);
        /// <summary>
        /// 在根据用户输入的过滤条件获取数据前发生。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void BeforeGetFilterData(object sender, InvokeDataAssistantHosterEventArgs args);


    }

    public class InvokeDataAssistantHosterEventArgs {
        private  IClientRuleQueryBase _ClientRule;
        private List<MB.Util.Model.QueryParameterInfo> _FilterParameters;
        private MB.WinBase.Common.ColumnEditCfgInfo _ClumnEditCfgInfo;
        private bool _Cancel;

        private object _CurrentEditObject;
        private MB.Util.Model.QueryParameterInfo[] _PreFilterParameters;

        public InvokeDataAssistantHosterEventArgs(MB.WinBase.Common.ColumnEditCfgInfo clumnEditCfgInfo) {
            _ClumnEditCfgInfo = clumnEditCfgInfo;
        }
        public InvokeDataAssistantHosterEventArgs(IClientRuleQueryBase clientRule, ColumnEditCfgInfo clumnEditCfgInfo, List<QueryParameterInfo> filterParameters)
        {
            _ClientRule = clientRule;
            _FilterParameters = filterParameters;
            _ClumnEditCfgInfo = clumnEditCfgInfo;
        }
        public InvokeDataAssistantHosterEventArgs(IClientRuleQueryBase clientRule,ColumnEditCfgInfo clumnEditCfgInfo,object currentEditObject, List<QueryParameterInfo> filterParameters) {
            _ClientRule = clientRule;
            _FilterParameters = filterParameters;
            _ClumnEditCfgInfo = clumnEditCfgInfo;
            _CurrentEditObject = currentEditObject;
        }

        public IClientRuleQueryBase ClientRule {
            get {
                return _ClientRule;
            }
            set {
                _ClientRule = value;
            }
        }
        public List<MB.Util.Model.QueryParameterInfo> FilterParameters {
            get {
                return _FilterParameters;
            }
            set {
                _FilterParameters = value;
            }
        }
        public bool Cancel {
            get {
                return _Cancel;
            }
            set {
                _Cancel = value;
            }
        }
        public MB.WinBase.Common.ColumnEditCfgInfo ClumnEditCfgInfo {
            get {
                return _ClumnEditCfgInfo;
            }
            set {
                _ClumnEditCfgInfo = value;
            }
        }

        public object CurrentEditObject {
            get {
                return _CurrentEditObject;
            }
            set {
                _CurrentEditObject = value;
            }
        }

        public MB.Util.Model.QueryParameterInfo[] PreFilterParameters
        {
            get
            {
                return _PreFilterParameters;
            }
            set
            {
                _PreFilterParameters = value;
            }
        }
    }
}
