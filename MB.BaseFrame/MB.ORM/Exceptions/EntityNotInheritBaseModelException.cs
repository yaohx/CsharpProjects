using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Exceptions {
    /// <summary>
    /// Model 实体必须继承MB.Orm.Common.BaseModel 基类。
    /// </summary>
    public class EntityNotInheritBaseModelException : MB.Util.APPException {
        public EntityNotInheritBaseModelException(Type entityType) :
            base(string.Format("实体类型{0}必须继承MB.Orm.Common.BaseModel!",entityType.FullName)) { }
    }
}
