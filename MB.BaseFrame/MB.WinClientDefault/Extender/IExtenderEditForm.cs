using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinClientDefault.Extender {
    /// <summary>
    /// 扩展的对象编辑接口。
    /// </summary>
    public  interface IExtenderEditForm {
        MB.WinClientDefault.Common.MainViewDataNavigator MainBindingGridView { get; set; }

        void DisposeBindingEvent();
    }
}
