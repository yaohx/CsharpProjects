using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util.WcfCompress
{
    /// <summary>
    /// 需要进行压缩处理的接口注册相关。
    /// </summary>
    public static class WcfCompressManager
    {
        private static HashSet<string> _GeneralCompressMethodName;

        static WcfCompressManager() {
            _GeneralCompressMethodName = new HashSet<string>();
            _GeneralCompressMethodName.Add("");

        }
    }
}
