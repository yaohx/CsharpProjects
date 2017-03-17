//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-13
// Description	:	客户端逻辑操作控制类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MB.Util.Model;
using System.Windows.Forms;
using MB.WinBase.Common;

namespace MB.WinBase.IFace {
    /// <summary>
    /// 编辑业务类。
    /// </summary>
    public interface IClientRuleEditBase {
        /// <summary>
        /// 批量获取新实体对象的ID。
        /// </summary>
        /// <param name="dataInDocType"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        int GetCreateNewEntityIds(int dataInDocType, int count);
        /// <summary>
        /// 根据类型批量创建实体对象。
        /// </summary>
        /// <param name="dataInDocType"></param>
        /// <param name="createCount"></param>
        /// <returns></returns>
        IList CreateNewEntityBatch(int dataInDocType, int createCount);
        /// <summary>
        /// 创建一个新的数据实体。
        /// </summary>
        /// <returns></returns>
        object CreateNewEntity(int dataInDocType);
        /// <summary>
        /// 把数据增加数据中间层的Cache 中。
        /// 根据实体的状态决定操作的方式。
        /// </summary>
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的数据。</param>
        /// <param name="entity">需要增加的实体。</param>
        /// <param name="propertys">需要增加的该实体的指定属性。</param>
        /// <returns></returns>
        int AddToCache(System.ServiceModel.ICommunicationObject serverRuleProxy, int dataInDocType, object entity, bool isDelete, string[] propertys);
        /// <summary>
        /// 提示中间层服务期端执行永久化的操作并清空缓存中的数据。
        /// 特殊说明：在具体的业务需求中需要修改为在保存后返回主表实体对象
        /// </summary>
        /// <returns></returns>
        int Flush(System.ServiceModel.ICommunicationObject serverRuleProxy);
        /// <summary>
        /// 对象数据提交。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancelSubmit"></param>
        /// <returns></returns>
        int Submit(object entity, bool cancelSubmit);
        /// <summary>
        /// 业务操作提交
        /// Submit 提交只是数据进行提交。相应的业务操作提交要执行 BusinessOperateSubmit 操作。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operateState"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        int BusinessFlowSubmit(object entity, MB.Util.Model.DocOperateType operateState, string remark);
         /// <summary>
        /// 根据数据类型检查指定的值在数据库中是否已经存在
        /// </summary>
        /// <param name="dataInDocType">需要进行检查的数据类型</param>
        /// <param name="entity">需要检查的实体对象</param>
        /// <param name="checkPropertys">需要检查的属性名称</param>
        /// <returns></returns>
        bool CheckValueIsExists(int dataInDocType, object entity, string[] checkPropertys);
    }
    /// <summary>
    /// UI 层界面对应配置处理相关。
    /// </summary>
    public interface IClientRuleConfig {
        /// <summary>
        /// 当前业务类实体的名称。
        /// </summary>
        string RuleInstanceName { get; set; }
        /// <summary>
        /// 业务类配置信息。
        /// </summary>
        MB.WinBase.Atts.RuleClientLayoutAttribute ClientLayoutAttribute { get; }
        /// <summary>
        /// 模块配置树节点信息。
        /// </summary>
        MB.Util.Model.ModuleTreeNodeInfo ModuleTreeNodeInfo { get; set; }
        /// <summary>
        /// UI配置信息
        /// </summary>
        MB.WinBase.Common.UIRuleXmlConfigInfo UIRuleXmlConfigInfo { get; set; }
        /// <summary>
        /// 模块打开时需要的一些参数
        /// </summary>
        ModuleOpenState OpennedState { get; set; }

    }
    /// <summary>
    /// 查询分析必须要实现的接口。
    /// 主要征对带有编辑对象的数据浏览窗口
    /// </summary>
    public interface IClientRuleQueryBase : IClientRuleConfig {
        /// <summary>
        /// 主表数据在单据中的数据类型。
        /// </summary>
        object MainDataTypeInDoc { get; set; }
        /// <summary>
        /// 默认显示的查询提示信息。
        /// </summary>
        string DefaultFilterMessage { get; set; }
        /// <summary>
        /// 获取设置的默认条件。
        /// </summary>
        /// <returns></returns>
        MB.Util.Model.QueryParameterInfo[] GetDefaultFilterParams();
        /// <summary>
        /// 获取动态聚组的
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        DataSet GetDynamicGroupQueryData(MB.Util.Model.DynamicGroupSetting setting, MB.Util.Model.QueryParameterInfo[] filterParams);
        /// <summary>
        /// 以DataSet 的格式获取数据。
        /// </summary>
        /// <returns></returns>
        DataSet GetObjectAsDataSet(int dataInDocType, MB.Util.Model.QueryParameterInfo[] filterParams);
        /// <summary>
        /// 以集合的方式获取数据。
        /// </summary>
        /// <returns></returns>
        IList GetObjects(int dataInDocType, MB.Util.Model.QueryParameterInfo[] filterParams);
        /// <summary>
        /// 当前数据查询行为
        /// </summary>
        QueryBehavior CurrentQueryBehavior { get; }
        /// <summary>
        /// 对过滤条件进行二次过滤
        /// </summary>
        Func<object, List<MB.Util.Model.QueryParameterInfo>> FilterParams{ get; set; }
        /// <summary>
        /// 自定义扩展UI RULE关联网格的右键菜单
        /// 当这个属性被赋值以后，基类设置的网格菜单会清空
        /// </summary>
        ContextMenu ReSetContextMenu { get; set; }
        /// <summary>
        /// 往自定义汇总列中填写汇总值，并且返回填好值的集合
        /// </summary>
        /// <param name="colsToGetValue">需要填写值的集合</param>
        /// <param name="queryParams">自定义汇总时客户端传入的条件</param>
        /// <returns>最终结果</returns>
        Dictionary<string, object> GetCustomSummaryColValues(string[] colsToGetValue, 
            MB.Util.Model.QueryParameterInfo[] queryParams);
        /// <summary>
        /// 当前查询条件
        /// </summary>
        MB.Util.Model.QueryParameterInfo[] CurrentFilterParams { get; set; }
        /// <summary>
        /// 如果是从TASK列表打开菜单的，可以设定默认的查询条件以默认加载条件
        /// </summary>
        /// <param name="state">打开时注入的参数，参数类型由提供方和调用方自行约定</param>
        /// <returns></returns>
        MB.Util.Model.QueryParameterInfo[] GetFilterParamsIfOpenFromTask(object state);

    }
    /// <summary>
    /// 客户端逻辑操作控制类。
    /// 没有业务类对应客户端都必须实现该接口，以满足公共框架知道应该调用什么服务类。
    /// 以及对应客户端UI 处理的的策略。（包括 显示绑定的XML 文件，需要客户验证的说明 ）
    /// </summary>
    public interface IClientRule : IClientRuleEditBase, IClientRuleQueryBase, IDisposable {
        #region 自定义事件处理相关...
        /// <summary>
        /// 单据状态发生改变时产生。
        /// </summary>
        event UIClientRuleDataEventHandle AfterDocStateChanged;
        /// <summary>
        /// 产生单据状态发生改变的事件。
        /// </summary>
        /// <param name="entity"></param>
        void RaiseAfterDocStateChanged(object entity);
        #endregion 自定义事件处理相关...

        /// <summary>
        /// 单据描述。
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 创建WCF 客户端代理。
        /// </summary>
        /// <returns></returns>
        System.ServiceModel.ICommunicationObject CreateServerCommunicationObject();
        /// <summary>
        /// 在实体对象保存后可能需要进行一些属性信息的更新，通过该方法来完成。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int RefreshEntity(int dataInDocType,object entity);
        /// <summary>
        /// 创建客户端绑定列表。
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        System.ComponentModel.IBindingList CreateMainBindList(IList dataList);

        /// <summary>
        /// 显示数据导入处理窗口。
        /// </summary>
        /// <returns></returns>
        bool ShowDataImport(IViewGridForm viewGridForm, MB.WinBase.Binding.BindingSourceEx bindingSource);
        /// <summary>
        /// 通过ID获取可编辑的对象
        /// </summary>
        /// <returns></returns>
        object  GetEditObjectByID(int id);

        /// <summary>
        /// 自定义扩展绑定数据源
        /// </summary>
        MB.WinBase.Binding.BindingSourceEx BindingSource { get; set; }

        /// <summary>
        /// 通过Key值获取可编辑的对象
        /// </summary>
        /// <returns></returns>
        object GetEditObjectByKey(object keyValue);

        
    }
    /// <summary>
    /// 客户端逻辑操作控制类。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IClientRule<T, TChannel> : IClientRule where TChannel : class,IDisposable {
        /// <summary>
        /// 创建WCF 客户端代理。
        /// </summary>
        /// <returns></returns>
        TChannel CreateServerRuleProxy();
        /// <summary>
        /// 当前编辑对象
        /// </summary>
        T CurrentEditObject { get; set; }
    }

    #region 自定义事件类型...
    public delegate void UIClientRuleDataEventHandle(object sender,UIClientRuleDataEventArgs arg);
    public class UIClientRuleDataEventArgs {
        private object _MainEntity;
        public UIClientRuleDataEventArgs(object mainEntity) {
            _MainEntity = mainEntity;
        }
        public object MainEntity {
            get {
                return _MainEntity;
            }
            set {
                _MainEntity = value;
            }
        }
    }
    #endregion 自定义事件类型...
}
