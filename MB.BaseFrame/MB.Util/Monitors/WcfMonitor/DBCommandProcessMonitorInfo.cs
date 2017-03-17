using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.Util.Monitors
{
    [DataContract]
    public class DBCommandProcessMonitorInfo
    {
        #region 字段与属性定义

        private string _CommandText;
        private TimeSpan _DBCommandProcessTime;

        #endregion

        [DataMember]
        public TimeSpan DBCommandProcessTime
        {
            get { return _DBCommandProcessTime; }
            set { _DBCommandProcessTime = value; }
        }


        /// <summary>
        /// SQL语句
        /// </summary>
        public string CommandText
        {
            get { return _CommandText; }
            set { _CommandText = value; }
        }
    }
}
