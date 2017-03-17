using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Common {
    /// <summary>
    /// 模块在通过OpenModuleById时
    /// 需要注入的打开状态
    /// </summary>
    public class ModuleOpenState {

        private  ModuleOpennedFrom _OpennedFrom;

        /// <summary>
        /// 模块是哪个位置打开的
        /// </summary>
        public ModuleOpennedFrom OpennedFrom {
            get { return _OpennedFrom; }
            set { _OpennedFrom = value; }
        }


        private object _OpenState;
        /// <summary>
        /// 模块打开时需要给入的参数，这个参数需要由提供方和调用方自行约定类型
        /// </summary>
        public object OpenState {
            get { return _OpenState; }
            set { _OpenState = value; }
        }
        
    }
}
