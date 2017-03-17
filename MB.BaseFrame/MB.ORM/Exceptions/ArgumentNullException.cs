using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Exceptions {
    /// <summary>
    /// 参数传入不能为空异常。
    /// </summary>
    public class ArgumentNullException : MB.Util.APPException  {
        public ArgumentNullException() : base("参数传入不能为空") { }
        public ArgumentNullException(string msg) : base(string.Format( "参数:{0}的传入不能为空！",msg)) { }
        public ArgumentNullException(string msg, Exception exception) : base(msg, MB.Util.APPMessageType.SysErrInfo, exception) { }
    }
}
