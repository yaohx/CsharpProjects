using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Enums {
    /// <summary>
    /// 这个枚举指明当操作]一个实体对象的时候，应该采用何种策略
    /// </summary>
    public enum PersistOptions {
        /// <summary>
        /// 只包含自己的属性
        /// </summary>
        SelfOnly, 
        /// <summary>
        /// 包含自己和所有的子对象
        /// </summary>
        IncludeChildren, 
        /// <summary>
        /// 包含自己和父对象
        /// </summary>
        IncludeReference, 
        /// <summary>
        /// 同时还要更新引用对象的数据
        /// </summary>
        Full 
    }
}
