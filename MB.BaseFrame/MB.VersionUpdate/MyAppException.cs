using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.VersionUpdate {
    public class MyAppException : ApplicationException {
        public MyAppException(string message)
            : base(message) {
        }
    }
}
