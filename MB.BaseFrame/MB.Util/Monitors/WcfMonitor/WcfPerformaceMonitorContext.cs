using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util.Monitors
{
    /// <summary>
    /// Wcf性能检测的数据
    /// </summary>
    public class WcfPerformaceMonitorContext
    {
        [ThreadStatic]
        private static WcfPerformaceMonitorContext _Current;

        /// <summary>
        /// 当前的性能检测的数据
        /// </summary>
        public static WcfPerformaceMonitorContext Current
        {
            get {
                return _Current;
            }
            set {
                _Current = value;
            }
        }

        private List<WcfRequestMonitorInfo> _WcfProcessMonitorInfos;

        /// <summary>
        /// 返回当前正在发生的WCF的监测对象
        /// </summary>
        public WcfRequestMonitorInfo CurrentWcfProcessMonitorInfo
        {
            get 
            { 
                if (_WcfProcessMonitorInfos.Count > 0)
                    return _WcfProcessMonitorInfos[_WcfProcessMonitorInfos.Count - 1]; 
                else
                    return null;
            }
        }
        

        /// <summary>
        /// WCF处理的性能指标的集合，因为一个操作可能包含错个WCF操作
        /// </summary>
        public List<WcfRequestMonitorInfo> WcfProcessMonitorInfos
        {
            get { return _WcfProcessMonitorInfos; }
            set { _WcfProcessMonitorInfos = value; }
        }


        #region constructer...
        /// <summary>
        /// constructer...
        /// </summary>
        public WcfPerformaceMonitorContext()
        {
            _WcfProcessMonitorInfos = new List<WcfRequestMonitorInfo>();
        }

        #endregion
        /// <summary>
        /// 重写ToString方法，打印出检测性能指标的数据
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            
            StringBuilder msgBuilder = new StringBuilder();
            if (_WcfProcessMonitorInfos.Count > 0)
            {
                try
                {
                    msgBuilder.AppendLine("性能检测指标");
                    msgBuilder.AppendLine("=====================性能监测范围内的指标数据开始==========================");
                    msgBuilder.AppendLine(string.Format("WCF请求次数:{0}次", _WcfProcessMonitorInfos.Count));
                    foreach (WcfRequestMonitorInfo wcfMonitorInfo in _WcfProcessMonitorInfos)
                    {
                        msgBuilder.AppendLine(string.Format("WCF请求地址:{0}；方法:{1}", wcfMonitorInfo.RequestURI, wcfMonitorInfo.RequestAction));
                        msgBuilder.AppendLine(string.Format("WCF总处理时间:{0}毫秒；中间层处理时间:{1}毫秒；传输时间:{2}毫秒", wcfMonitorInfo.WcfProcessTime.TotalMilliseconds, wcfMonitorInfo.WcfProcessTimeOnServer.TotalMilliseconds, wcfMonitorInfo.WcfProcessTime.Subtract(wcfMonitorInfo.WcfProcessTimeOnServer).TotalMilliseconds));

                        msgBuilder.AppendLine(string.Format("WCF请求大小:{0}字节；压缩后请求大小:{1}字节；压缩比:{2}", wcfMonitorInfo.RequestSize, wcfMonitorInfo.CompressedRequestSize, CalculateCompressionRate(wcfMonitorInfo.RequestSize, wcfMonitorInfo.CompressedRequestSize)));
                        msgBuilder.AppendLine(string.Format("WCF响应大小:{0}字节；压缩后响应大小:{1}字节；压缩比:{2}", wcfMonitorInfo.ResponseSize, wcfMonitorInfo.CompressedResponseSize, CalculateCompressionRate(wcfMonitorInfo.ResponseSize, wcfMonitorInfo.CompressedResponseSize)));

                        if (wcfMonitorInfo.DBProcessMonitorInfos != null && wcfMonitorInfo.DBProcessMonitorInfos.Count > 0)
                        {
                            double totalDBProcessTime = 0;
                            wcfMonitorInfo.DBProcessMonitorInfos.ForEach(db => db.DBCommandProcessMonitorInfos.ForEach(cmd => totalDBProcessTime += cmd.DBCommandProcessTime.TotalMilliseconds));
                            msgBuilder.AppendLine(string.Format("中间层数据库请求次数:{0}次；请求总时间:{1}毫秒", wcfMonitorInfo.DBRequestCount, totalDBProcessTime));
                        }
                    }
                    msgBuilder.AppendLine("=====================性能监测范围内的指标数据结束==========================");
                    return msgBuilder.ToString();
                }
                catch (Exception ex)
                {
                    return "性能检测指标出错" + ex.ToString();
                }
            }
            
            else
                return base.ToString();
        }

        /// <summary>
        /// 计算压缩比
        /// </summary>
        /// <param name="size"></param>
        /// <param name="compressedSize"></param>
        /// <returns></returns>
        private double CalculateCompressionRate(double size, double compressedSize)
        {
            double result = 0;
            if (size > 0)
            {
                result = compressedSize / size;
            }
            return result;
        }

    }
}
