//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-17
// Description	:	连接客户端各界面之间的查询分析客户端规则类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.ComponentModel;

using MB.WinBase.IFace;
using MB.WinBase.Atts;
using MB.Util.Model;
using MB.BaseFrame;
using System.Windows.Forms;
using MB.WinBase.Common;
namespace MB.WinBase {
    /// <summary>
    /// 客户端查询分析对象。
    /// </summary>
    public abstract class AbstractClientRuleQuery : IClientRuleQueryBase {
        private MB.Util.Model.ModuleTreeNodeInfo _ModuleTreeNodeInfo;
        private MB.WinBase.Atts.RuleClientLayoutAttribute _ClientLayoutAttribute;
        private MB.WinBase.Common.UIRuleXmlConfigInfo _UIRuleXmlConfigInfo;
        private QueryBehavior _QueryBehavior ;
        private object _MainDataTypeInDoc;
        private string _RuleInstanceName;
        private Func<object, List<MB.Util.Model.QueryParameterInfo>> _FilterParams;
        private MB.Util.Model.QueryParameterInfo[] _CurrentFilterParams;
        private ModuleOpenState _OpennedState;

        /// <summary>
        /// 客户端查询RULE的抽象
        /// </summary>
        public AbstractClientRuleQuery() {
            _QueryBehavior = new QueryBehavior(0,SOD.DEFAULT_MAX_SHOT_COUNT);
        }
        #region IClientRuleQuery 成员
        /// <summary>
        /// 获取默认设置的缺省参数。  
        /// 对于查询分析对象默认情况下都不显示数据。
        /// </summary>
        /// <returns></returns>
        public virtual MB.Util.Model.QueryParameterInfo[] GetDefaultFilterParams() {
            MB.Util.TraceEx.Assert(ClientLayoutAttribute.DefaultFilter == DefaultDataFilter.None, "对于查询分析对象来说,默认情况下不能显示任何数,请重新设置 DefaultFilter");

            List<MB.Util.Model.QueryParameterInfo> filters = new List<MB.Util.Model.QueryParameterInfo>();
            filters.Add(new MB.Util.Model.QueryParameterInfo("1", "0", MB.Util.DataFilterConditions.Special));
            return filters.ToArray() ;
        }

        /// <summary>
        /// 如果是从TASK列表打开菜单的，可以设定默认的查询条件以默认加载条件
        /// </summary>
        /// <param name="state">打开时注入的参数，参数类型由提供方和调用方自行约定</param>
        /// <returns></returns>
        public virtual MB.Util.Model.QueryParameterInfo[] GetFilterParamsIfOpenFromTask(object state) {
            List<MB.Util.Model.QueryParameterInfo> filters = new List<MB.Util.Model.QueryParameterInfo>();
            filters.Add(new MB.Util.Model.QueryParameterInfo("1", "0", MB.Util.DataFilterConditions.Special));
            return filters.ToArray();
        }


        /// <summary>
        /// 主表对象在单据中的数据类型。
        /// </summary>
        public object MainDataTypeInDoc {
            get {
                return _MainDataTypeInDoc;
            }
            set {
                _MainDataTypeInDoc = value; ;
            }
        }
        private string _DefaultFilterMessage;
        /// <summary>
        /// 获取默认数值的查询提示信息。
        /// </summary>
        public string DefaultFilterMessage {
            get {
                return _DefaultFilterMessage;
            }
            set {
                _DefaultFilterMessage = value;
            }
        }

        public virtual DataSet GetDynamicGroupQueryData(MB.Util.Model.DynamicGroupSetting setting, MB.Util.Model.QueryParameterInfo[] filterParams)
        {
            throw new NotImplementedException();
        }

        public virtual DataSet GetObjectAsDataSet(int dataInDocType, MB.Util.Model.QueryParameterInfo[] filterParams) {
            throw new NotImplementedException();
        }

        public virtual IList GetObjects(int dataInDocType, MB.Util.Model.QueryParameterInfo[] filterParams) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 当前查询行为
        /// </summary>
        public QueryBehavior CurrentQueryBehavior {
            get {
                return _QueryBehavior;
            }
        }

        public Func<object, List<MB.Util.Model.QueryParameterInfo>> FilterParams {
            get {
                return _FilterParams;
            }
            set {
                _FilterParams = value;
            }
        }

        /// <summary>
        /// 往自定义汇总列中填写汇总值，并且返回填好值的集合
        /// </summary>
        /// <param name="colsToGetValue">需要填写值的集合</param>
        /// <param name="queryParams">自定义汇总时客户端传入的条件</param>
        /// <returns>汇总完以后的结果列的集合</returns>
        public virtual Dictionary<string, object> GetCustomSummaryColValues(string[] colsToGetValue, MB.Util.Model.QueryParameterInfo[] queryParams) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 重新绑定右键菜单项
        /// </summary>
        public virtual ContextMenu ReSetContextMenu {
            get;
            set;
        }
        #endregion

        #region IClientRuleConfig 成员

        /// <summary>
        /// 客户端布局配置属性
        /// </summary>
        public MB.WinBase.Atts.RuleClientLayoutAttribute ClientLayoutAttribute {
            get {
                if (_ClientLayoutAttribute == null)
                    _ClientLayoutAttribute = AttributeConfigHelper.Instance.GetClientRuleSettingAtt(this);
                return _ClientLayoutAttribute;
            }
        }

        /// <summary>
        /// 模块对应节点信息。
        /// </summary>
        public MB.Util.Model.ModuleTreeNodeInfo ModuleTreeNodeInfo {
            get {
                return _ModuleTreeNodeInfo;
            }
            set {
                _ModuleTreeNodeInfo = value;
            }
        }

        /// <summary>
        /// 对应XML 文件的配置信息。
        /// </summary>
        public MB.WinBase.Common.UIRuleXmlConfigInfo UIRuleXmlConfigInfo {
            get {
                if (_UIRuleXmlConfigInfo == null)
                    _UIRuleXmlConfigInfo = new MB.WinBase.Common.UIRuleXmlConfigInfo(this);

                return _UIRuleXmlConfigInfo;
            }
            set {
                _UIRuleXmlConfigInfo = value;
            }
        }

        /// <summary>
        /// 当前业务类实例的名称。
        /// </summary>
        public string RuleInstanceName {
            get {
                return _RuleInstanceName;
            }
            set {
                _RuleInstanceName = value;
            }
        }

        public ModuleOpenState OpennedState {
            get {
                return _OpennedState;
            }
            set {
                _OpennedState = value;
            }
        }

        #endregion


        public QueryParameterInfo[] CurrentFilterParams
        {
            get
            {
                return _CurrentFilterParams;
            }
            set
            {
                _CurrentFilterParams = value;
            }
        }
    }


    /// <summary>
    /// 客户端查询分析对象，带上客户端代理,以满足动态聚组异步查询的需求
    /// </summary>
    /// <typeparam name="TChannel"></typeparam>
    public abstract class AbstractClientRuleQuery<TChannel> : AbstractClientRuleQuery where TChannel : class, IDisposable {

        public override DataSet GetDynamicGroupQueryData(MB.Util.Model.DynamicGroupSetting setting, MB.Util.Model.QueryParameterInfo[] filterParams) {
            string xmlFilterParams = MB.Util.Serializer.QueryParameterXmlSerializer.DefaultInstance.Serializer(filterParams);
            using (TChannel serverRuleProxy = CreateServerRuleProxy()) {
                object[] pars = new object[] { setting, xmlFilterParams };
                object re = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(serverRuleProxy, "GetDynamicGroupQueryData", pars);
                if (re == null)
                    return null;
                else
                    return re as DataSet;
            }
        }

        /// <summary>
        /// WCF 服务客户端代理对象。
        /// </summary>
        /// <returns></returns>
        public TChannel CreateServerRuleProxy() {

            TChannel proxy = MB.WcfClient.WcfClientFactory.CreateWcfClient<TChannel>(this);
            return proxy;
        }

    }

}
