//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-17
// Description	:	连接客户端各界面之间的客户端规则类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

using MB.WinBase.IFace;
using MB.WinBase.Atts;
using MB.Util.Common;
using MB.WinBase.WcfClient;
using System.ServiceModel;
using MB.Util;
using MB.WcfServiceLocator;
using System.Windows.Forms;
using MB.WinBase.Common;
namespace MB.WinBase{
    /// <summary>
    /// 连接客户端各界面之间的客户端规则类。
    /// </summary>
    public abstract class AbstractClientRule<T, TChannel> : AbstractClientRuleQuery,IClientRule<T, TChannel> where TChannel : class, IDisposable {
        private string _Description;

        private T _CurrentEditObject;

        #region IDisposable 成员
        private bool _Disposed = false;
        /// <summary>
        /// 外部用户可直接调用
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
       
        private void Dispose(bool disposing) {
            if (!this._Disposed) {
                if (disposing) {

                }
                //try {
                //    _ServerRuleProxy.Close();
                //}
                //catch { }

                _Disposed = true;

            }
        }
        /// <summary>
        /// 
        /// </summary>
        ~AbstractClientRule()
        {
            Dispose(false);
        }
        #endregion IDisposable 成员

        #region 覆盖基类的成员...
        /// <summary>
        /// 获取默认设置的
        /// </summary>
        /// <returns></returns>
        public override MB.Util.Model.QueryParameterInfo[] GetDefaultFilterParams() {
            List<MB.Util.Model.QueryParameterInfo> filterParam = new List<MB.Util.Model.QueryParameterInfo>();
           
            var style = this.ClientLayoutAttribute.DefaultFilter;
            switch (style) {
                case DefaultDataFilter.None:
                    return base.GetDefaultFilterParams();
                case DefaultDataFilter.All:
                    filterParam.Add(new MB.Util.Model.QueryParameterInfo("1", "1", MB.Util.DataFilterConditions.Special));
                    break;
                case DefaultDataFilter.Process:
                    appendProcessFilter(filterParam);
                    break;
                case DefaultDataFilter.Today:
                    appendDateFilter(filterParam, DateFilterType.Today);
                    break;
                //case DefaultDataFilter.ProcessAndMonth:
                //    appendProcessFilter(filterParam);
                //    appendDateFilter(filterParam, DateFilterType.Month);
                //    break;
                //case DefaultDataFilter.ProcessAndWeek:
                //    appendProcessFilter(filterParam);
                //    appendDateFilter(filterParam, DateFilterType.Week);
                //    break;
                case DefaultDataFilter.WithinOfWeek:
                    appendDateFilter(filterParam, DateFilterType.Week);
                    break;
                case DefaultDataFilter.WithinOfMonth:
                    appendDateFilter(filterParam, DateFilterType.Month);
                    break;
                default :
                    throw new MB.Util.APPException(string.Format("默认过滤条件设置{0} 还没有进行相应的处理。",style.ToString()));
            }

            return filterParam.ToArray();
        }
        #endregion 覆盖基类的成员...

        #region 内部处理函数...
        //追加正在处理中的单据查询条件
        private void appendProcessFilter(List<MB.Util.Model.QueryParameterInfo> filterParam) {
            if (!UIDataEditHelper.Instance.CheckTypeExistsDocState(typeof(T)))
                throw new MB.Util.APPException("只有存在单据状态的业务类模块才能配置默认查询条件为 (正在处理中)", MB.Util.APPMessageType.SysErrInfo);

            filterParam.Add(new MB.Util.Model.QueryParameterInfo(UIDataEditHelper.ENTITY_DOC_STATE,
                            (int)MB.Util.Model.DocState.Progress, MB.Util.DataFilterConditions.Equal));
        }
        //增加时间的过滤条件设置
        private void appendDateFilter(List<MB.Util.Model.QueryParameterInfo> filterParam, MB.Util.Common.DateFilterType dateFilterType) {
            bool b = MB.Util.MyReflection.Instance.CheckTypeExistsProperty(typeof(T), MB.BaseFrame.SOD.ENTITY_LAST_MODIFIED_DATE); 
            if(!b)
                throw new MB.Util.APPException("当前单据不包含最后修改日期的字段,无法进行单据修改日期的配置设置", MB.Util.APPMessageType.SysErrInfo);

             QueryParameterHelper paramHelper = new QueryParameterHelper();
             var date = paramHelper.ToDateStruct(dateFilterType);
            DateTime beginDate = date.BeginDate ;
            DateTime endDate = date.EndDate;
            var para = new MB.Util.Model.QueryParameterInfo(MB.BaseFrame.SOD.ENTITY_LAST_MODIFIED_DATE,beginDate, MB.Util.DataFilterConditions.Between);
            para.DataType = "DateTime";
            para.Value2 = endDate;
            filterParam.Add(para);
        }
        #endregion 内部处理函数...
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="mainDataTypeInDoc"></param>
        public AbstractClientRule(object mainDataTypeInDoc) {
            MainDataTypeInDoc = mainDataTypeInDoc;
        }

        #region IClientRule 成员
        /// <summary>
        /// 单据描述。
        /// </summary>
        public string Description {
            get {
                return _Description;
            }
            set {
                _Description = value;
            }
        }
        /// <summary>
        /// WCF 服务客户端代理对象。
        /// </summary>
        /// <returns></returns>
        public System.ServiceModel.ICommunicationObject CreateServerCommunicationObject() {
            return CreateServerRuleProxy() as System.ServiceModel.ICommunicationObject; 
        }
        /// <summary>
        /// WCF 服务客户端代理对象。
        /// </summary>
        /// <returns></returns>
        public TChannel CreateServerRuleProxy() {
           // TChannel proxy = MB.Util.MyNetworkCredential.CreateWcfClientWithCredential<TChannel>();// (TChannel)MB.Util.DllFactory.Instance.CreateInstance(typeof(TChannel)); 
            TChannel proxy = MB.WcfClient.WcfClientFactory.CreateWcfClient<TChannel>(this);   
            return proxy;
        }
        /// <summary>
        /// 批量获取新创建的实体对象ID.
        /// </summary>
        /// <param name="dataInDocType"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int GetCreateNewEntityIds(int dataInDocType, int count) {
            try {
                using (TChannel serverRuleProxy = CreateServerRuleProxy()) {
                    object val = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(serverRuleProxy, "GetCreateNewEntityIds", dataInDocType,count);
                    return MB.Util.MyConvert.Instance.ToInt(val);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// 根据类型批量创建实体对象。
        /// </summary>
        /// <param name="dataInDocType"></param>
        /// <param name="createCount"></param>
        /// <returns></returns>
        public virtual IList CreateNewEntityBatch(int dataInDocType, int createCount) {
            try {
                using (TChannel serverRuleProxy = CreateServerRuleProxy()) {
                    object lstEntity = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(serverRuleProxy, "CreateNewEntityBatch",
                                            dataInDocType, createCount);
                    if (lstEntity == null)
                        return null;
                    else
                        return lstEntity as System.Collections.IList;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// 根据类型创建一个新的实体对象。
        /// </summary>
        /// <param name="dataInDocType"></param>
        /// <returns></returns>
        public virtual object CreateNewEntity(int dataInDocType) {
            try {
                using (TChannel serverRuleProxy = CreateServerRuleProxy()) {
                    object newEntity = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(serverRuleProxy, "CreateNewEntity", dataInDocType);
                    return newEntity;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// 根据数据类型检查指定的值在数据库中是否已经存在
        /// </summary>
        /// <param name="dataInDocType">需要进行检查的数据类型</param>
        /// <param name="entity">需要检查的实体对象</param>
        /// <param name="checkPropertys">需要检查的属性名称</param>
        /// <returns></returns>
        public virtual bool CheckValueIsExists(int dataInDocType, object entity, string[] checkPropertys) {
            try {
                using (TChannel serverRuleProxy = CreateServerRuleProxy()) {
                    bool b = (bool)MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(serverRuleProxy, "CheckValueIsExists", dataInDocType, entity, checkPropertys);
                    if (b) {
                        var cols = this.UIRuleXmlConfigInfo.GetDefaultColumns();
                        string desc = ShareLib.Instance.GetPropertyDescription(checkPropertys, cols);
                        string valDesc = ShareLib.Instance.GetMultiEntityValueDescription(checkPropertys, entity); 
                        string msg = "需要存储的对象属性(" + desc + ") 的值(" + valDesc + ") 在库中已经存在,请重新输入";
                        throw new MB.Util.APPException(msg, MB.Util.APPMessageType.DisplayToUser);
                    }
                    return false;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        /// 获取动态聚组的数据
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        public virtual System.Data.DataSet GetDynamicGroupQueryData(MB.Util.Model.DynamicGroupSetting setting,
            MB.Util.Model.QueryParameterInfo[] filterParams)
        {
            string xmlFilterParams = MB.Util.Serializer.QueryParameterXmlSerializer.DefaultInstance.Serializer(filterParams);
            using (TChannel serverRuleProxy = CreateServerRuleProxy())
            {
                object[] pars = new object[] {setting, xmlFilterParams };
                object re = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(serverRuleProxy, "GetDynamicGroupQueryData", pars);
                if (re == null)
                    return null;
                else
                    return re as DataSet;
            }
        }

        /// <summary>
        /// 以DataSet 的格式获取选择指定类型的数据。
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public virtual System.Data.DataSet GetObjectAsDataSet(int dataInDocType, MB.Util.Model.QueryParameterInfo[] filterParams) {
            string xmlFilterParams = MB.Util.Serializer.QueryParameterXmlSerializer.DefaultInstance.Serializer(filterParams);
            using (TChannel serverRuleProxy = CreateServerRuleProxy()) {
                object[] pars = new object[] { dataInDocType, xmlFilterParams };
                object re = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(serverRuleProxy, "GetObjectAsDataSet", pars);
                if (re == null)
                    return null;
                else
                    return re as DataSet;
            }
        }
        /// <summary>
        /// 以数据实体类集合的方式获取需要的数据。
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public virtual System.Collections.IList GetObjects(int dataInDocType, MB.Util.Model.QueryParameterInfo[] filterParams) {
            string xmlFilterParams = MB.Util.Serializer.QueryParameterXmlSerializer.DefaultInstance.Serializer(filterParams);
            object[] pars = new object[] { dataInDocType, xmlFilterParams };
            using (TChannel serverRuleProxy = CreateServerRuleProxy()) {
                object re = null;
                IClientChannel channel = MyReflection.Instance.InvokePropertyForGet(serverRuleProxy, "InnerChannel") as IClientChannel;

                //由于该方法是在独立的线程中运行，所以在其他线程的QueryBehaviorScope中是取不到相对应的QueryBehavior信息的
                //所以需要独立记载QueryBehavior的信息

                string messageHeaderKey = string.Empty;
                if (this.ClientLayoutAttribute.LoadType == ClientDataLoadType.ReLoad)
                {
                    messageHeaderKey = this.ClientLayoutAttribute.MessageHeaderKey;
                }

                using (OperationContextScope scope = new OperationContextScope(channel)) {
                    //增加消息表头信息
                    MessageHeaderHelper.AppendUserLoginInfo();
                    //增加查询行为
                    MessageHeaderHelper.AppendQueryBehavior(this.CurrentQueryBehavior, messageHeaderKey);
                    
                    re = WcfClientHelper.Instance.InvokeServerMethod(serverRuleProxy, "GetObjects", pars);
                }
                if (re == null)
                    return null;
                else
                    return re as System.Collections.IList;
            }
        }
        /// <summary>
        /// 把数据增加数据中间层的Cache 中。
        /// 根据实体的状态决定操作的方式。
        /// </summary>
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的数据。</param>
        /// <param name="entity">需要增加的实体。</param>
        /// <param name="propertys">需要增加的该实体的指定属性。</param>
        /// <returns></returns>
        public virtual int AddToCache(System.ServiceModel.ICommunicationObject serverRuleProxy, int dataInDocType, object entity, bool isDelete, string[] propertys) {
            IClientChannel channel = MyReflection.Instance.InvokePropertyForGet(serverRuleProxy, "InnerChannel") as IClientChannel;
            using (OperationContextScope scope = new OperationContextScope(channel)) {
                //增加消息表头信息
                MessageHeaderHelper.AppendUserLoginInfo();

                object[] pars = new object[] { dataInDocType, entity, isDelete, propertys };
                object re = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(serverRuleProxy, "AddToCache", pars);
                return MB.Util.MyConvert.Instance.ToInt(re);
            }
        }
        /// <summary>
        /// 提示中间层服务期端执行永久化的操作并清空缓存中的数据。
        /// </summary>
        public virtual int Flush(System.ServiceModel.ICommunicationObject serverRuleProxy) {
            object re = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(serverRuleProxy, "Flush");
            return MB.Util.MyConvert.Instance.ToInt(re);
        }

        /// <summary>
        /// 往自定义汇总列中填写汇总值，并且返回填好值的集合
        /// </summary>
        /// <param name="colsToGetValue">需要填写值的集合</param>
        /// <param name="queryParams">自定义汇总时客户端传入的条件</param>
        /// <returns>汇总完以后的结果列的集合</returns>
        public virtual Dictionary<string, object> GetCustomSummaryColValues(string[] colsToGetValue, MB.Util.Model.QueryParameterInfo[] queryParams) {
            using (TChannel serverRuleProxy = CreateServerRuleProxy()) {
                object[] pars = new object[] { colsToGetValue, queryParams };
                object re = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(
                    serverRuleProxy, "GetCustomSummaryColValues", pars);
                if (re == null)
                    return null;
                else
                    return re as Dictionary<string, object>;
            }
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public T CurrentEditObject {
            get { return _CurrentEditObject; }
            set { _CurrentEditObject = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public System.ComponentModel.IBindingList CreateMainBindList(IList dataList) {
          System.ComponentModel.BindingList<T> bindingList = MB.WinBase.Binding.BindingSourceHelper.Instance.CreateBindingList<T>(dataList);
          return bindingList;
        }
        /// <summary>
        /// 通过ID获取可编辑的对象。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object GetEditObjectByID(int id) {
            using (TChannel serverRuleProxy = CreateServerRuleProxy()) {
                return (T)MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(serverRuleProxy, "GetObjectByKey", 0,id);
 
            }
        }

        /// <summary>
        /// 根据Key值获取对象
        /// 动态加载数据 Grid列表加载与UI编辑界面数据源分离
        /// add by aifang 2012-08-01
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public object GetEditObjectByKey(object keyValue) {
            using (TChannel serverRuleProxy = CreateServerRuleProxy())
            {
                return (T)MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(serverRuleProxy, "GetObjectByKey",0, keyValue);
            }     
        }

        #region IClientRule 成员

        /// <summary>
        ///  在实体对象保存后可能需要进行一些属性信息的更新，通过该方法来完成。
        /// </summary>
        /// <param name="dataInDocType"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int RefreshEntity(int dataInDocType, object entity) {
            object[] pars = new object[] { dataInDocType, entity };
            object val = null;
            using (TChannel serverRuleProxy = CreateServerRuleProxy()) {
               val = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(serverRuleProxy, "RefreshEntity", pars);

               if (val == null) return -1;
            }

            Dictionary<string,object> proValues = MB.Util.MyReflection.Instance.ObjectToPropertyValues(val);
            MB.Util.MyReflection.Instance.SetByPropertyValues(entity, proValues);
            return 1;
        }
        /// <summary>
        /// 对象数据提交。
        /// </summary>
        /// <param name="serverRuleProxy"></param>
        /// <param name="entity"></param>
        /// <param name="cancelSubmit"></param>
        /// <returns></returns>
        public virtual int Submit(object entity, bool cancelSubmit) {
            object[] pars = new object[] { entity, cancelSubmit };
            object val = null;
            using (TChannel serverRuleProxy = CreateServerRuleProxy()) {
                  System.ServiceModel.IClientChannel channel = MB.Util.MyReflection.Instance.InvokePropertyForGet(serverRuleProxy, "InnerChannel") as System.ServiceModel.IClientChannel;
                  using (System.ServiceModel.OperationContextScope scope = new System.ServiceModel.OperationContextScope(channel)) {
                      //增加消息表头信息
                      MB.WinBase.WcfClient.MessageHeaderHelper.AppendUserLoginInfo();
                      val = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(serverRuleProxy, "Submit", pars);
                  }
                if (val == null || (int)val < 0 ) return -1;
            }
            return 1;
        }
        /// <summary>
        /// 提交该单据对象相应的业务操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operateState"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public virtual int BusinessFlowSubmit(object entity, MB.Util.Model.DocOperateType operateState, string remark) {
            object[] pars = new object[] { entity, operateState, remark };
            object val = null;
            using (TChannel serverRuleProxy = CreateServerRuleProxy()) {
                System.ServiceModel.IClientChannel channel = MB.Util.MyReflection.Instance.InvokePropertyForGet(serverRuleProxy, "InnerChannel") as System.ServiceModel.IClientChannel;
                using (System.ServiceModel.OperationContextScope scope = new System.ServiceModel.OperationContextScope(channel)) {
                    //增加消息表头信息
                    MB.WinBase.WcfClient.MessageHeaderHelper.AppendUserLoginInfo();
                    val = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(serverRuleProxy, "BusinessFlowSubmit", pars);
                }
                if (val == null || (int)val < 0) return -1;
            }
            return 1;
        }

        #endregion
        
        /// <summary>
        ///  显示数据导入处理窗口。
        /// </summary>
        /// <returns></returns>
        public virtual bool ShowDataImport(IViewGridForm viewGridForm, MB.WinBase.Binding.BindingSourceEx bindingSource) {
            return false;
        }

        //add by aifang 2012-07-20 自定义扩展功能 begin
        public MB.WinBase.Binding.BindingSourceEx BindingSource { get; set; }
        //add by aifang 2012-07-20 自定义扩展功能 end

        #region 自定义事件处理相关...

        private UIClientRuleDataEventHandle _AfterDocStateChanged;
        public event UIClientRuleDataEventHandle AfterDocStateChanged {
            add {
                _AfterDocStateChanged += value; 
            }
            remove {
                _AfterDocStateChanged -= value; 
            }
        }
        public virtual void RaiseAfterDocStateChanged(object entity) {
            if (_AfterDocStateChanged != null)
                _AfterDocStateChanged(this, new UIClientRuleDataEventArgs(entity));
        }
        #endregion 自定义事件处理相关...

    }
}
