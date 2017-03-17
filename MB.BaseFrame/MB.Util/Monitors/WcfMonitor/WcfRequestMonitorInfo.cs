using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.Util.Monitors
{
    [DataContract]
    public class WcfRequestMonitorInfo
    {
        #region 字段与属性定义
        private string _RequestURI;
        private string _RequestAction;
        private int _RequestSize;
        private int _CompressedRequestSize;
        private int _CompressedResponseSize;
        private int _ResponseSize;
        private System.Diagnostics.Stopwatch _WcfRequestWatch;
        private TimeSpan _WcfProcessTime;

        //以下字段是从服务端进行统计的
        private int _DBRequestCount;
        private TimeSpan _WcfProcessTimeOnServer;
        private List<DBProcessMonitorInfo> _DBProcessMonitorInfos;




        /// <summary>
        /// 请求的资源地址
        /// </summary>
        public string RequestURI
        {
            get { return _RequestURI; }
            set { _RequestURI = value; }

        }

        /// <summary>
        /// SOAP协议的Action
        /// </summary>
        public string RequestAction
        {
            get { return _RequestAction; }
            set { _RequestAction = value; }
        }

        /// <summary>
        /// 请求的大小
        /// </summary>
        public int RequestSize
        {
            get { return _RequestSize; }
            set { _RequestSize = value; }
        }

        /// <summary>
        /// 压缩后的请求的大小
        /// </summary>
        public int CompressedRequestSize
        {
            get { return _CompressedRequestSize; }
            set { _CompressedRequestSize = value; }
        }

        /// <summary>
        /// 压缩响应的大小
        /// </summary>
        public int CompressedResponseSize
        {
            get { return _CompressedResponseSize; }
            set { _CompressedResponseSize = value; }
        }

        /// <summary>
        /// 解压后响应的大小
        /// </summary>
        public int ResponseSize
        {
            get { return _ResponseSize; }
            set { _ResponseSize = value; }
        }

        /// <summary>
        /// 观察整个请求的监视器
        /// </summary>
        public System.Diagnostics.Stopwatch WcfRequestWatch
        {
            get { return _WcfRequestWatch; }
            set { _WcfRequestWatch = value; }
        }



        /// <summary>
        /// Wcf整个处理时间
        /// </summary>       
        public TimeSpan WcfProcessTime
        {
            get { return _WcfProcessTime; }
            set { _WcfProcessTime = value; }
        }

        /// <summary>
        /// 数据库请求的次数
        /// </summary>
        [DataMember]
        public int DBRequestCount
        {
            get { return _DBRequestCount; }
            set { _DBRequestCount = value; }
        }

        /// <summary>
        /// 中间层完成wcf请求的时间跨度
        /// </summary>
        [DataMember]
        public TimeSpan WcfProcessTimeOnServer
        {
            get { return _WcfProcessTimeOnServer; }
            set { _WcfProcessTimeOnServer = value; }
        }

        /// <summary>
        /// 在一个wcf请求中，所有数据库的请求
        /// </summary>
        [DataMember]
        public List<DBProcessMonitorInfo> DBProcessMonitorInfos
        {
            get { return _DBProcessMonitorInfos; }
            set { _DBProcessMonitorInfos = value; }
        }

        /// <summary>
        /// 返回当前正在发生的WCF的监测对象
        /// </summary>
        public DBProcessMonitorInfo CurrentDBProcessMonitorInfo
        {
            get
            {
                if (_DBProcessMonitorInfos.Count > 0)
                    return _DBProcessMonitorInfos[_DBProcessMonitorInfos.Count - 1];
                else
                    return null;
            }
        }

        #endregion


        public WcfRequestMonitorInfo()
        {
            _DBProcessMonitorInfos = new List<DBProcessMonitorInfo>();
        }

    }
}
