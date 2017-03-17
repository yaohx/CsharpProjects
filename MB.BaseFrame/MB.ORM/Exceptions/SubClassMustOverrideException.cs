using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Exceptions {
    /// <summary>
    /// 子类必须覆盖基类的方法。
    /// </summary>
    public class SubClassMustOverrideException : DatabaseNonsupportException {
        public SubClassMustOverrideException() : base("sub class must override this method!") { }
        public SubClassMustOverrideException(string msg) : base(string.Format("继承的子类需要实现方法:",msg)) { }
        public SubClassMustOverrideException(string msg, Exception exception) : base(msg, exception) { }
    }
}
