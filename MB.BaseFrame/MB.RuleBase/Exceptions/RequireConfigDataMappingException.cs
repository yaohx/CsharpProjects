using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.RuleBase.Exceptions {
    /// <summary>
    /// 数据类型没有进行ObjectDataMapping 的配置。
    /// </summary>
    public class RequireConfigDataMappingException : MB.Util.APPException  {
        public RequireConfigDataMappingException(object dataDocType) : base("类型:" + 
                    dataDocType.GetType().FullName + "中的值:" + dataDocType.ToString() + 
                    " 没有进行ObjectDataMappingAttribute的配置！" , MB.Util.APPMessageType.SysErrInfo) { }
    }
}
