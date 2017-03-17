using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Enums {
    public enum SqlGeneratorType {
        /// <summary>
        /// 通过属性配置的自动产生。
        /// </summary>
        AutoCreate,//
        /// <summary>
        /// 通过XML 配置文件
        /// </summary>
        XmlConfig
    }
}
