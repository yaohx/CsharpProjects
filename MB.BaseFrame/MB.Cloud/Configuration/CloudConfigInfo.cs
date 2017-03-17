using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Cloud.Configuration
{
    /// <summary>
    /// 云计算平台配置信息
    /// </summary>
    public class CloudConfigInfo
    {
        public CloudConfigInfo() {
            this.CloudDbCode = "core";
        }
        public List<CloudDBTemplate> CloudDBTemplates { get; set; }
        public CloudDbManager CloudDbManager { get; set; }
        public string CloudDbCode { get; set; }
    }

    public class CloudDbManager
    {
        public string Agent { get; set; }
        public string DatabaseService{get;set;}
        public string MonitorService { get; set; }
        public string GroupService { get; set; }
        public string GroupName { get; set; }

    }

    public class CloudDBTemplate
    {
        public string Provider { get; set; }
        public string ConnectionTemplate { get; set; }
    }
}
