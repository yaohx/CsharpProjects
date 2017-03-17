using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Enums {
    /// <summary>
    /// 字段Mapping 的类型。
    /// </summary>
    public enum FieldMappingType {
        /// <summary>
        /// 通过对象的属性配置来映射。
        /// </summary>
        Attribute,
        /// <summary>
        /// 通过XML 配置 属性来进行映射。
        /// </summary>
        Xml
    }
}
