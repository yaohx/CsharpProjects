using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Exceptions {
    /// <summary>
    /// 
    /// </summary>
    public class ManagedByOtherPMException : MB.Util.APPException {
        public ManagedByOtherPMException() : base("This entity is managed by other PersistenceManager!") { }
        public ManagedByOtherPMException(string msg) : base(msg) { }
        public ManagedByOtherPMException(string msg, Exception exception) : base(msg, MB.Util.APPMessageType.SysErrInfo, exception) { }
    }
}
