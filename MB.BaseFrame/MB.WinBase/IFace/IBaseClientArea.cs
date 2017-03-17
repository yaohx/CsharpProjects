using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.WinBase.Common;
namespace MB.WinBase.IFace {
    /// <summary>
    /// 自定义客户端UI 必须要实现的接口。
    /// </summary>
    public class IBaseClientArea {
        ClientUIType ClientUIType { get; set; }
    }
}
