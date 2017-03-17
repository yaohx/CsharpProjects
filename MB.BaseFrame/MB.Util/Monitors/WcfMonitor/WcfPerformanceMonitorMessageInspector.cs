using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using MB.BaseFrame;
using System.ServiceModel.Channels;

namespace MB.Util.Monitors
{
    /// <summary>
    /// 消息检测器用来记录一些性能指标
    /// </summary>
    public class WcfPerformanceMonitorMessageInspector : IClientMessageInspector, IDispatchMessageInspector
    {
        #region IClientMessageInspector
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            WcfRequestMonitorInfo monitorInfo = (WcfRequestMonitorInfo)correlationState;
            if (monitorInfo != null)
            {
                
                var re = reply.Headers.FindHeader(SOD.PERFORMANCE_MONITOR_RESPONSE_MESSAGE_HEADER, SOD.MESSAGE_HEADER_NAME_SPACE);
                if (re >= 0)
                {
                    WcfRequestMonitorInfo monitorInfoFromServer = reply.Headers.GetHeader<WcfRequestMonitorInfo>(SOD.PERFORMANCE_MONITOR_RESPONSE_MESSAGE_HEADER, SOD.MESSAGE_HEADER_NAME_SPACE);
                    monitorInfo.WcfProcessTimeOnServer = monitorInfoFromServer.WcfProcessTimeOnServer;
                    monitorInfo.DBProcessMonitorInfos = monitorInfoFromServer.DBProcessMonitorInfos;
                    monitorInfo.DBRequestCount = monitorInfoFromServer.DBRequestCount;
                }


                monitorInfo.WcfRequestWatch.Stop();
                monitorInfo.WcfProcessTime = monitorInfo.WcfRequestWatch.Elapsed;
            }
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            if (WcfPerformaceMonitorContext.Current != null)
            {
                MessageHeader<bool> mhMonitorSwitch = new MessageHeader<bool>(true);
                string messageHeaderName = SOD.PERFORMANCE_MONITOR_SWITCH_MESSAGE_HEADER;
                MessageHeader header = mhMonitorSwitch.GetUntypedHeader(messageHeaderName, SOD.MESSAGE_HEADER_NAME_SPACE);
                int ret = request.Headers.FindHeader(messageHeaderName, SOD.MESSAGE_HEADER_NAME_SPACE);
                if (ret < 0) request.Headers.Add(header);

                WcfRequestMonitorInfo monitorInfo = new WcfRequestMonitorInfo();
                monitorInfo.RequestURI = channel.Via.ToString();
                monitorInfo.RequestAction = request.Headers.Action;
                monitorInfo.WcfRequestWatch = new System.Diagnostics.Stopwatch();
                monitorInfo.WcfRequestWatch.Start();
                WcfPerformaceMonitorContext.Current.WcfProcessMonitorInfos.Add(monitorInfo);

                return monitorInfo;
            }
            else
                return null;
        }
        #endregion

        #region IDispatchMessageInspector

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            bool isPerformanceMonitored = false;
            var re = request.Headers.FindHeader(SOD.PERFORMANCE_MONITOR_SWITCH_MESSAGE_HEADER, SOD.MESSAGE_HEADER_NAME_SPACE);
            if (re >= 0)
            {
                isPerformanceMonitored = request.Headers.GetHeader<bool>(SOD.PERFORMANCE_MONITOR_SWITCH_MESSAGE_HEADER, SOD.MESSAGE_HEADER_NAME_SPACE);
            }

            if (isPerformanceMonitored)
            {
                WcfPerformanceMonitorScope pmScope = new WcfPerformanceMonitorScope();
                WcfRequestMonitorInfo monitorInfo = new WcfRequestMonitorInfo();
                WcfPerformaceMonitorContext.Current.WcfProcessMonitorInfos.Add(new WcfRequestMonitorInfo());
            }
            DateTime dtWcfProcessBegin = DateTime.Now;
            return dtWcfProcessBegin;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            if (WcfPerformaceMonitorContext.Current != null)
            {
                DateTime dtWcfProcessBegin = (DateTime)correlationState;
                DateTime dtWcfProcessEnd = DateTime.Now;
                WcfPerformaceMonitorContext.Current.WcfProcessMonitorInfos[0].WcfProcessTimeOnServer = dtWcfProcessEnd.Subtract(dtWcfProcessBegin);
                WcfRequestMonitorInfo monitorInfo = WcfPerformaceMonitorContext.Current.WcfProcessMonitorInfos[0];

                MessageHeader<WcfRequestMonitorInfo> response = new MessageHeader<WcfRequestMonitorInfo>(monitorInfo);
                string messageHeaderName = SOD.PERFORMANCE_MONITOR_RESPONSE_MESSAGE_HEADER;
                MessageHeader header = response.GetUntypedHeader(messageHeaderName, SOD.MESSAGE_HEADER_NAME_SPACE);
                int ret = reply.Headers.FindHeader(messageHeaderName, SOD.MESSAGE_HEADER_NAME_SPACE);
                if (ret < 0) reply.Headers.Add(header);

                //释放服务器中的当前WCF监测的对象
                while (WcfPerformanceMonitorScope.CurrentScope != null)
                {
                    WcfPerformanceMonitorScope.CurrentScope.Dispose();
                }

            }

            
        }

        #endregion
    }
}
