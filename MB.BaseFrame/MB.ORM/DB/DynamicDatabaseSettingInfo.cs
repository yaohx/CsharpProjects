using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.DB
{
    /// <summary>
    /// 云计算
    /// </summary>
    public  class DynamicDatabaseSettingInfo
    {
        public DynamicDatabaseSettingInfo(){}


        public DynamicDatabaseSettingInfo(string provider,string connectionString) {
            Provider = provider;
            ConnectionString = connectionString;
        }
        /// <summary>
        /// 数据库连接Provider
        /// </summary>
        public string Provider { get; set; }
        /// <summary>
        /// ConnectionString.
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
