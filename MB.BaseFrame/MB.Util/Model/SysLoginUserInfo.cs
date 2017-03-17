using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using MB.Util.XmlConfig;
namespace MB.Util.Model {
    /// <summary> 
    /// 文件生成时间 2009-05-12 10:50 
    /// 当前登录用户的描述信息。
    /// </summary> 
    [MB.Util.XmlConfig.ModelXmlConfig(ByXmlNodeAttribute = false)]
    [DataContract]
    public class SysLoginUserInfo{
       /// <summary>
        ///  当前登录用户的描述信息。
       /// </summary>
        public SysLoginUserInfo() {

        }
        /// <summary>
        /// 为了兼容老ERP 而定义为 string 类型。
        /// 在新的解决方案中存储的是int类型
        /// </summary>
        private string _USER_ID;
        [PropertyXmlConfig]
        [DataMember]
        public string USER_ID {
            get { return _USER_ID; }
            set { _USER_ID = value; }
        }
        private string _USER_CODE;
        [PropertyXmlConfig]
        [DataMember]
        public string USER_CODE {
            get { return _USER_CODE; }
            set { _USER_CODE = value; }
        }

        private string _USER_NAME;
        [PropertyXmlConfig]
        [DataMember]
        public string USER_NAME {
            get { return _USER_NAME; }
            set { _USER_NAME = value; }
        }
        private string _USER_PSWD;
        [PropertyXmlConfig]
        [DataMember]
        public string USER_PSWD {
            get { return _USER_PSWD; }
            set { _USER_PSWD = value; }
        }
        /// <summary>
        /// 在新的解决方案中存储的是int类型。
        /// </summary>
        private string _DFLT_TREE_ID;
        [PropertyXmlConfig]
        [DataMember]
        public string DFLT_TREE_ID {
            get { return _DFLT_TREE_ID; }
            set { _DFLT_TREE_ID = value; }
        }
        /// <summary>
        /// 在新的解决方案中存储的是int类型
        /// </summary>
        private string _AU_UNIT_ID;
        [PropertyXmlConfig]
        [DataMember]
        public string AU_UNIT_ID {
            get { return _AU_UNIT_ID; }
            set { _AU_UNIT_ID = value; }
        }
        private string _STATUS;
        [PropertyXmlConfig]
        [DataMember]
        public string STATUS {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
        /// <summary>
        /// 在新的解决方案中存储的是int类型
        /// </summary>
        private string _OWNER_ID;
        [PropertyXmlConfig]
        [DataMember]
        public string OWNER_ID {
            get { return _OWNER_ID; }
            set { _OWNER_ID = value; }
        }
        private string _OWNER_CODE;
        [PropertyXmlConfig]
        [DataMember]
        public string OWNER_CODE {
            get { return _OWNER_CODE; }
            set { _OWNER_CODE = value; }
        }

        private string _DISP_NAME;
        [PropertyXmlConfig]
        [DataMember]
        public string DISP_NAME {
            get { return _DISP_NAME; }
            set { _DISP_NAME = value; }
        }
        private string _OWNER_NAME;
        [PropertyXmlConfig]
        [DataMember]
        public string OWNER_NAME {
            get { return _OWNER_NAME; }
            set { _OWNER_NAME = value; }
        }
        /// <summary>
        /// 在新的解决方案中存储的是int类型
        /// </summary>
        private string _SUPERIOR_ID;
        [PropertyXmlConfig]
        [DataMember]
        public string SUPERIOR_ID {
            get { return _SUPERIOR_ID; }
            set { _SUPERIOR_ID = value; }
        }
    } 

}
