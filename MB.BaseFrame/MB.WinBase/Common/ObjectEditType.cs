using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Common {
    /// <summary>
    /// 对象编辑操作类型。
    /// </summary>
    public enum ObjectEditType {
        /// <summary>
        /// 新增。
        /// </summary>
        AddNew,
        /// <summary>
        /// 复制新增。
        /// </summary>
        CopyAsNew,
        /// <summary>
        /// 打开编辑。
        /// </summary>
        OpenEdit,
        /// <summary>
        /// 以只读方式打开对象。
        /// </summary>
        OpenReadOnly,
        /// <summary>
        /// 删除当前选择的数据。
        /// </summary>
        Delete,
        /// <summary>
        /// 保存。
        /// </summary>
        Save,
        /// <summary>
        /// 提交。
        /// </summary>
        Submit,
        /// <summary>
        /// 取消提交。
        /// </summary>
        CancelSubmit,
        /// <summary>
        /// 设计演示
        /// </summary>
        DesignDemo,
        /// <summary>
        /// 其它。
        /// </summary>
        Other
    }
}
