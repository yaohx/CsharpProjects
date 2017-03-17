using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MB.Cloud.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    internal static  class CloudConfigManager
    {
        /// <summary>
        /// 获取云计算数据库的配置信息
        /// </summary>
        /// <returns></returns>
        public static CloudConfigInfo GetConfigInfo() {
            return (CloudConfigInfo)System.Configuration.ConfigurationManager.GetSection("MBCloudConfiguratio");
        }
    }
}
