using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.Exceptions {
    /// <summary>
    /// 客户端异常配置出错！
    /// </summary>
    public class RuleClientConfigException : MB.Util.APPException {

        public RuleClientConfigException(IFace.IClientRuleConfig clientRule)
            : base(string.Format("对于每一个客户端业务类都需要配置RUleClientLayoutAttribute 属性。请检查:" + clientRule.GetType().FullName + " 是否已经配置！", MB.Util.APPMessageType.SysErrInfo)) {
        }
    }
}
