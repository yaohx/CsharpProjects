using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.Util.Model {
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class SysUserPrivInfo{
        /// <summary>
        /// 
        /// </summary>
        public SysUserPrivInfo() {

        }
        /// <summary>
        /// 在新的解决方案中存储的是int类型
        /// </summary>
        private string _UNIT_ID;
        [DataMember]
        public string UNIT_ID {
            get { return _UNIT_ID; }
            set { _UNIT_ID = value; }
        }
        /// <summary>
        /// 在新的解决方案中存储的是int类型
        /// </summary>
        private string _PRIV_ID;
        [DataMember]
        public string PRIV_ID {
            get { return _PRIV_ID; }
            set { _PRIV_ID = value; }
        }
    } 
}
