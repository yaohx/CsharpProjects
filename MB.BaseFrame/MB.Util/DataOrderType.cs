using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MB.Util {
    public enum DataOrderType {
        /// <summary>
        /// 升序。
        /// </summary>
        [Description("升序")]
        Ascending,
        /// <summary>
        /// 降序。
        /// </summary>
        [Description("降序")]
		Descending,
        /// <summary>
        /// None.
        /// </summary>
        [Description("None")]
		None
    }
}
