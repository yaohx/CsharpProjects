//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-03
// Description	:	所有业务类必须要继承的抽象基类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using MB.RuleBase.Common;
using MB.RuleBase.Atts;
using MB.RuleBase.IFace;
using MB.Util.Model;
using MB.Orm.DB;
using MB.WcfService;

namespace MB.RuleBase {
    /// <summary>
    /// 所有插销分析业务类必须要继承的抽象基类。
    /// 该类数据库操作获取的是默认配置的数据库连接字符窜，
    /// 如果需要调用不同的数据库，需要覆盖相应的方法并调用DatabaseConfigurationScope。
    /// </summary>
    public abstract class BaseQueryRule : System.ContextBoundObject,IBaseQueryRule {
        private MB.Orm.Mapping.QueryParameterMappings _QueryParamMapping;
        private Type _ObjectDataDocType;

        private string _MessageHeaderKey;


        /// <summary>
        /// constructer..
        /// </summary>
        /// <param name="objectDataDocType"></param>
        public BaseQueryRule(Type objectDataDocType) {
            _ObjectDataDocType = objectDataDocType;
        }


        /// <summary>
        /// 对象数据类型。
        /// </summary>
        public Type ObjectDataDocType {
            get {
                return _ObjectDataDocType;
            }
            set {
                _ObjectDataDocType = value;
            }
        }
 
        /// <summary>
        /// 获取查询字段的映射信息。
        /// 除了主表以外如果还有其它的映射信息，可以在子类中覆盖该方法后继续添加。
        /// </summary>
        public virtual MB.Orm.Mapping.QueryParameterMappings QueryParamMapping {
            get {
                return _QueryParamMapping;
            }
            set {
                _QueryParamMapping = value;
            }
        }

        /// <summary>
        /// 动态列加载对应的消息头键值
        /// </summary>
        public virtual string MessageHeaderKey {
            get {
                return _MessageHeaderKey;
            }
            set {
                _MessageHeaderKey = value;
            }
        }

        /// <summary>
        /// 动态聚组查询获取数据
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="xmlFilterParams"></param>
        /// <returns></returns>
        public virtual System.Data.DataSet GetDynamicGroupQueryData(MB.Util.Model.DynamicGroupSetting setting, string xmlFilterParams) {
            using (MB.Orm.DB.OperationDatabaseScope scope = new OperationDatabaseScope(true)) {
                QueryParameterInfo[] filters = new MB.Util.Serializer.QueryParameterXmlSerializer().DeSerializer(xmlFilterParams);
                return new ObjectEditHelper().GetDynamicGroupQueryData(setting, filters);
            }
        }

        /// <summary>
        /// 以DataSet 的类型返回需要获取的数据。
        /// </summary>
        /// <param name="dataInDocType">需要进行检查的数据类型</param>
        /// <param name="xmlFilterParams">QueryParameterInfo[] 系列化后的字符窜</param>
        /// <returns>以System.Data.DataSet 返回获取到的结果 </returns>  //[OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public virtual System.Data.DataSet GetObjectAsDataSet(int dataInDocType, string xmlFilterParams) {
             using (MB.Orm.DB.OperationDatabaseScope scope = new OperationDatabaseScope(true)) {
                QueryParameterInfo[] filters = new MB.Util.Serializer.QueryParameterXmlSerializer().DeSerializer(xmlFilterParams);
                var qh = MessageHeaderHelper.GetQueryBehavior(_MessageHeaderKey); //modify by aifang 动态列设置  var qh = MessageHeaderHelper.GetQueryBehavior();
                return new ObjectEditHelper(qh).GetObjectAsDataSet(this, ConvertDataInDocType(dataInDocType), filters);
            }
        }


        /// <summary>
        /// 根据过滤的条件获取指定类型实体对象的集合。
        /// </summary>
        /// <param name="xmlFilterParams">QueryParameterInfo[] 系列化后的字符窜</param>
        /// <returns>得到的实体集合</returns>  // [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public virtual IList GetObjects(int dataInDocType, string xmlFilterParams) {
             using (MB.Orm.DB.OperationDatabaseScope scope = new OperationDatabaseScope(true)) {
                QueryParameterInfo[] filters = new MB.Util.Serializer.QueryParameterXmlSerializer().DeSerializer(xmlFilterParams);
                var qh = MessageHeaderHelper.GetQueryBehavior(_MessageHeaderKey); //modify by aifang 动态列设置  var qh = MessageHeaderHelper.GetQueryBehavior();
                return new ObjectEditHelper(qh).GetObjects<System.Object>(this, ConvertDataInDocType(dataInDocType), filters);
            }
        }

        /// <summary>
        /// 得到自定义汇总列显示的内容信息
        /// 这个方法在基类中不会有具体实现，需要业务派生类去实现该方法
        /// </summary>
        /// <param name="colsToGetValue">客户端传过来需要自定义汇总的列名</param>
        /// <param name="queryParams">自定义汇总时客户端传入的条件</param>
        /// <returns>返回自定义汇总的列</returns>
        public virtual Dictionary<string, object> GetCustomSummaryColValues(List<string> colsToGetValue, MB.Util.Model.QueryParameterInfo[] queryParams) {
            throw new NotImplementedException("GetCustomSummaryColValues自定义汇总列显示的内容信息,需要在业务派生类中实现");
        }

        #region 内部函数处理...
        //把数字转换为单据数据类型值(系统需要从类型值进行相应的配置信息的获取，这点是必须的)
        protected object ConvertDataInDocType(int value) {
            return System.Convert.ChangeType(Enum.Parse(_ObjectDataDocType, value.ToString()), _ObjectDataDocType);
        }
        #endregion 内部函数处理...
 
    }
}
