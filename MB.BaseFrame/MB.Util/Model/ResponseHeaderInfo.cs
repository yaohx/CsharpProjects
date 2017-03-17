using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using MB.Util.XmlConfig;
using MB.BaseFrame;

namespace MB.Util.Model
{
    /// <summary>
    /// 服务返回的一些信息
    /// </summary>
    [ModelXmlConfig(ByXmlNodeAttribute = false)]
    [DataContract]
    public class ResponseHeaderInfo
    {
        private static ResponseHeaderInfo _defaultResponseInfo;
        private static object _lock = new object();

        /// <summary>
        /// 缺省默认返回值
        /// </summary>
        public static ResponseHeaderInfo DefaultResponseInfo
        {
            get
            {
                if (_defaultResponseInfo == null)
                {
                    lock (_lock)
                    {
                        if (_defaultResponseInfo == null)
                            _defaultResponseInfo = new ResponseHeaderInfo(-1);
                    }
                }
                return _defaultResponseInfo;
            }
        }

         #region 构造函数...
        /// <summary>
        /// 
        /// </summary>
        public ResponseHeaderInfo()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalRecordCount"></param>
        public ResponseHeaderInfo(int totalRecordCount)
        {
            TotalRecordCount = totalRecordCount;
        }
        #endregion

        /// <summary>
        /// 返回的总记录数
        /// </summary>
        [PropertyXmlConfig]
        [DataMember]
        public int TotalRecordCount { get; set; }
    }
}
