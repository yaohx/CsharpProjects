using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.RuleBase.Exceptions {
    public class   SelectSqlXmlConfigException : MB.Util.APPException  {
        public SelectSqlXmlConfigException(string xmlFileName,string sqlName) : base("在XML文件{0} 中的 {1} 配置Select 语句有误，Select 只能配置一个！"
            , MB.Util.APPMessageType.SysFileInfo) { }
    }
}
