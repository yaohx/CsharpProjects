using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Exceptions {
    /// <summary>
    /// 获取配置SQL 语句出错！
    /// </summary>
    public class XmlSqlConfigNotExistsException : MB.Util.APPException {
        public XmlSqlConfigNotExistsException(string xmlFileName,string sqlName) : 
            base(string.Format("获取XML文件：中的SQL 语句:出错！请检查是否已经配置！",xmlFileName,sqlName)) { }
    }
}
