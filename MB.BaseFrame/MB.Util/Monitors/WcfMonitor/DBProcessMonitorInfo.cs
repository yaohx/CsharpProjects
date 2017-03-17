using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.Util.Monitors
{
    [DataContract]
    public class DBProcessMonitorInfo
    {
        #region 字段与属性定义
        private List<DBCommandProcessMonitorInfo> _DBCommandProcessMonitorInfos;

        #endregion

        /// <summary>
        /// 当前正在处理的DB Command
        /// </summary>
        public DBCommandProcessMonitorInfo CurrentDBCommandProcessMonitorInfo
        {
            get
            {
                if (_DBCommandProcessMonitorInfos.Count > 0)
                    return _DBCommandProcessMonitorInfos[_DBCommandProcessMonitorInfos.Count - 1];
                else
                    return null;
            }
        }

        /// <summary>
        /// 在一个数据库连接中处理的所有commands
        /// </summary>
        [DataMember]
        public List<DBCommandProcessMonitorInfo> DBCommandProcessMonitorInfos
        {
            get { return _DBCommandProcessMonitorInfos; }
            set { _DBCommandProcessMonitorInfos = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DBProcessMonitorInfo()
        {
            _DBCommandProcessMonitorInfos = new List<DBCommandProcessMonitorInfo>();
        }
    }
}
