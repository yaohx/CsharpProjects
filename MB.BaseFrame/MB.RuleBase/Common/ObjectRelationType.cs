using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.RuleBase.Common {
    /// <summary>
    /// ObjectRelationType 对象关联类型。
    /// </summary>
    public enum ObjectRelationType {
        OneToOne, //一对一
        OntToMulti,//一对多
        MultiToOne,//多对一
        MultiToMulti //多对多
    }
}
