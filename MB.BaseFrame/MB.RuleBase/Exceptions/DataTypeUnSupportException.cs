using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.RuleBase.Exceptions {
    /// <summary>
    /// 不能进行统一处理的数据类型。
    /// </summary>
    public class DataTypeUnSupportException : MB.Util.APPException  {
        public DataTypeUnSupportException() : base("目前在进行ObjectDataList存储还未对该类型进行相应操作处理。") { }
        public DataTypeUnSupportException(string dataType) : base(string.Format("目前在进行ObjectDataList存储还未对该类型 {0} 进行相应操作处理。",dataType)) { }
        public DataTypeUnSupportException(string message, Exception exception) : base(message, MB.Util.APPMessageType.SysErrInfo, exception) { }
    }
}
