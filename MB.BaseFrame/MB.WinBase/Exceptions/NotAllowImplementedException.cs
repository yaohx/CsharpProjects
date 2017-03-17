using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Exceptions {
    /// <summary>
    /// 当前对象不能执行该项操作，可能该操作未实现或不能执行
    /// </summary>
    public class NotAllowImplementedException : MB.Util.APPException {
           public NotAllowImplementedException()
            : base("当前对象不能执行该项操作，可能该操作未实现或不能执行", MB.Util.APPMessageType.DisplayToUser) {
        }
           public NotAllowImplementedException(string message)
            : base(message, MB.Util.APPMessageType.SysErrInfo) {
        }
    }
}
