using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Enums {
    /// <summary>
    /// 数据对象操作类型。
    /// </summary>
    [Flags] 
    public enum OperationType : int { 
        /// <summary>
        /// 未知状态，,根据其它的条件来决定具体的操作。
        /// </summary>
        None = 0x0000,
        /// <summary>
        /// 获取数据。
        /// </summary>
        Select = 0x0001, 
        /// <summary>
        /// 通过键值获取数据。
        /// </summary>
        SelectByKey = 0x0002, 
        /// <summary>
        /// 插入数据。
        /// </summary>
        Insert = 0x0004, 
        /// <summary>
        /// 修改数据
        /// </summary>
        Update = 0x0008, 
        /// <summary>
        /// 删除数据。
        /// </summary>
        Delete = 0x0010, 
        /// <summary>
        /// 通过delete not in 的方式来删除数据。
        /// </summary>
        DeleteNotIn = 0x0012,
        /// <summary>
        /// 其它。
        /// </summary>
        Others = 0x0014
    }
}
