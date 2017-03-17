using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.RuleBase.Exceptions {
    /// <summary>
    /// 子类必须覆盖基类的方法。
    /// </summary>
    public class DatabaseExecuteException : MB.Util.APPException  {
        public DatabaseExecuteException() : base("执行数据库操作有误！") { }
        public DatabaseExecuteException(string msg) : base(msg) { }
        public DatabaseExecuteException(string msg, Exception exception) : base(msg, MB.Util.APPMessageType.SysDatabaseInfo, exception) { }
    }
}
