using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Exceptions {
    /// <summary>
    /// 
    /// </summary>
    public class NotManagedBySelfException : DatabaseNonsupportException {
        public NotManagedBySelfException() : base("This entity is not managed by this PersistenceManager!") { }
        public NotManagedBySelfException(string msg) : base(msg) { }
        public NotManagedBySelfException(string msg, Exception exception) : base(msg, exception) { }
    }
}
