using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Exceptions {
    /// <summary>
    /// 不能持久化临时对象而产生的异常。
    /// </summary>
    public class EntityIsTransientException : MB.Util.APPException {
        public EntityIsTransientException() : base("This operation can't be performed because the entity is transient !") { }
        public EntityIsTransientException(string msg) : base(msg) { }
        public EntityIsTransientException(string msg, Exception exception) : base(msg, MB.Util.APPMessageType.SysErrInfo, exception) { }
    }
}
