using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Exceptions {
    /// <summary>
    /// 在XML 文件的SQL语句配置中，参数配置有误。
    /// </summary>
    public class SqlArgumentXmlConfigException : MB.Util.APPException {
        public SqlArgumentXmlConfigException(string sqlString, string paramName) :
            base(string.Format( "配置SQL语句：{0} 中的S参数:{1} 出错！请检查！",sqlString,paramName)) { }
    }
}
