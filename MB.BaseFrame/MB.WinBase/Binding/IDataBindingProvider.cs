using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinBase.Binding {
    /// <summary>
    /// 设计时数据绑定提供接口。
    /// </summary>
    public interface IDataBindingProvider {
        /// <summary>
        /// 已经提供的数据绑定集合。
        /// </summary>
        Dictionary<Control, DesignColumnXmlCfgInfo> DataBindings { get; }
        /// <summary>
        /// 实体对象的XML 列配置信息。
        /// </summary>
        Dictionary<string, DesignColumnXmlCfgInfo> ColumnXmlCfgs { get; }  
    }
}
