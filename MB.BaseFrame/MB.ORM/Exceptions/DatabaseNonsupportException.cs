
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Exceptions {
    /// <summary>
    /// 数据库类型不能支持。
    /// </summary>
    public class DatabaseNonsupportException : MB.Util.APPException  {
        public DatabaseNonsupportException() : base("DatabaseNonsupportException") { }
        public DatabaseNonsupportException(string msg) : base(msg) { }
        public DatabaseNonsupportException(string msg, Exception exception) : base(msg, MB.Util.APPMessageType.SysErrInfo, exception) { }
    }
}
