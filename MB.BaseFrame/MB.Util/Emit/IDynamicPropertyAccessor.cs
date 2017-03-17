using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util.Emit {
    /// <summary>
    ///定义一个动态获取属性的接口
    /// </summary>
    public interface IDynamicPropertyAccessor {
        /// <summary>
        /// 从目标对象中获取属性的值。
        /// </summary>
        /// <param name="target">属性所在的目标对象</param>
        /// <returns>属性值.</returns>
        object Get(object target);

        /// <summary>
        /// 为目标对象设置属性值。
        /// </summary>
        /// <param name="target">属性所在的目标对象</param>
        /// <param name="value">属性值</param>
        void Set(object target, object value);
    }
}
