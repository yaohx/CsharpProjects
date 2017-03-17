using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Configuration;

namespace MB.Util.Monitors
{
    /// <summary>
    /// WCF性能指标监控的行为配置节点，主要用来配置服务端的Service behavior
    /// </summary>
    public class WcfPerformanceMonitorServiceBehaviorElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(WcfPerformanceMonitorBehavior); }
        }
        protected override object CreateBehavior()
        {
            return new WcfPerformanceMonitorBehavior();
        }

    }
}
