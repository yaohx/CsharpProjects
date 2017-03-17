using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.ServiceModel;

namespace MB.RuleBase.IFace {
    /// <summary>
    /// 查询分析接口。
    /// </summary>
    [ServiceContract]
   public interface IBaseQueryRule {
       /// <summary>
       /// 对象数据
       /// </summary>
       Type ObjectDataDocType { get; set; }

        /// <summary>
        /// 获取查询参数的映射列表。
        /// </summary>
        MB.Orm.Mapping.QueryParameterMappings QueryParamMapping { get; set; }

        /// <summary>
        /// 根据过滤的条件以 DataSet 获取需要的数据填充动态聚组查询。
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="xmlFilterParams"></param>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [OperationContract]
        DataSet GetDynamicGroupQueryData(MB.Util.Model.DynamicGroupSetting setting,
            string xmlFilterParams);

        /// <summary>
        /// 根据过滤的条件以 DataSet 获取需要的数据。
        /// 如果存在大数据 不能在客户端直接调用,需要通过大数据调用载体来进行。
        /// (未来可以通过配置的方式来解决)
        /// </summary>
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的键值。</param>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))]
        [TransactionFlow(TransactionFlowOption.Allowed)]  
        [OperationContract]
        DataSet GetObjectAsDataSet(int dataInDocType, string xmlFilterParams);
        /// <summary>
        /// 根据过滤的条件以IList的格式 获取需要的数据。
        /// 如果存在大数据 不能在客户端直接调用,需要通过大数据调用载体来进行。
        /// (未来可以通过配置的方式来解决)
        /// </summary>
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的键值。</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))]
        [TransactionFlow(TransactionFlowOption.Allowed)]  
        [OperationContract]
        IList GetObjects(int dataInDocType, string xmlFilterParams);
        /// <summary>
        /// 得到自定义汇总列显示的内容信息
        /// 这个方法在基类中不会有具体实现，需要业务派生类去实现该方法
        /// </summary>
        /// <param name="colsToGetValue">客户端传过来需要自定义汇总的列名</param>
        /// <param name="queryParams">自定义汇总时客户端传入的条件</param>
        /// <returns>返回自定义汇总的列</returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))]
        [OperationContract]
        Dictionary<string, object> GetCustomSummaryColValues(List<string> colsToGetValue, MB.Util.Model.QueryParameterInfo[] queryParams);
    }
}
