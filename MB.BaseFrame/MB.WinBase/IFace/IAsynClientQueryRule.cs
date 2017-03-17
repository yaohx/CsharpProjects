using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel; 

namespace MB.WinBase.IFace {
    /// <summary>
    /// 异步查询分析需要实现的接口。
    /// 默认的查询分析Client 端业务类必须实现的接口。
    /// 主要针对返回值是dataSet 的解决方案。
    /// </summary>
    public interface IAsynClientQueryRule : IClientRuleQueryBase {
        /// <summary>
        /// 开始调用方法之前。
        /// </summary>
        void BeginRunWorker(MB.Util.Model.QueryParameterInfo[] filterParams);
        /// <summary>
        /// 获取本次需要下载的数据块总数。
        /// </summary>
        /// <returns></returns>
        int GetBufferCount();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        /// <returns></returns>
        System.IAsyncResult BeginGetBufferByIndex(int index, System.AsyncCallback callback, object asyncState);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        byte[] EndGetBufferByIndex(System.IAsyncResult result);
        /// <summary>
        /// 
        /// </summary>
        event System.EventHandler<MB.WinBase.Common .GetBufferByIndexCompletedEventArgs> GetBufferByIndexCompleted;
        
        /// <summary>
        /// 在调用结束后调用的方法。
        /// </summary>
        void WorkerCompleted();

       
    }


    
  
}
