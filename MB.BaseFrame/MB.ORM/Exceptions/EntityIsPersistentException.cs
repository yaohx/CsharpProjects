using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Exceptions {
    /// <summary>
    /// 实体持久化操作异常。
    /// </summary>
    public class EntityIsPersistentException : MB.Util.APPException {
        public EntityIsPersistentException() : base("This entity is already persistent!Perform PersistentNew operation failed!") { }
        public EntityIsPersistentException(string msg) : base(msg) { }
        public EntityIsPersistentException(string msg, Exception exception) : base(msg, MB.Util.APPMessageType.SysErrInfo, exception) { }
    }

   
  
}
