using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel; 

namespace MB.RuleBase.IFace {
    /// <summary>
    /// 异步查询获取数据需要实现的接口。
    /// 主要针对DataSet 的解决方案。
    /// 报表查询分析默认的处理方法。
    /// </summary>
    [ServiceContract]
    public interface IAsynQueryRule : IBaseQueryRule {
        /// <summary>
        /// 开始调用方法之前。
        /// </summary>
        void BeginRunWorker(int dataInDocType, string xmlFilterParams);
        /// <summary>
        /// 获取本次需要下载的数据块总数。
        /// </summary>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))]
        [OperationContract]
        int GetBufferCount();
        /// <summary>
        /// 根据Index 获取数据块。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))]
        [OperationContract]
        byte[] GetBufferByIndex(int index);
 
    }
}
