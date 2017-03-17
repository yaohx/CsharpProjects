using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MB.Util.Model {

    public enum DynamicGroupConditionOperator : int {
        [Description("N/A")]
        None,
        [Description("=")]
        Equal,
        //[Description("Like")]
        //Like,
        [Description(">")]
        GreaterThan,
        [Description(">=")]
        GreaterThanOrEqual,
        [Description("<")]
        LessThan,
        [Description("<=")]
        LessThanOrEqual,
        [Description("!=")]
        Different
    }
}
