using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.Util.Model
{
    /// <summary>
    /// 短消息实体
    /// </summary>
    [DataContract]
    public class SMSEntity
    {
        public SMSEntity()
        {
            MobileNumbers = new List<string>();
        }

        /// <summary>
        /// 发送者名称
        /// </summary>
        [DataMember]
        public string SendUserName { get; set; }

        /// <summary>
        /// 短信分组信息，例如广告，促销
        /// </summary>
        [DataMember]
        public string MsgGroup { get; set; }

        /// <summary>
        /// 批量的移动号码
        /// </summary>
        [DataMember]
        public List<string> MobileNumbers { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [DataMember]
        public string MessageEntity { get; set; }
    }
}
