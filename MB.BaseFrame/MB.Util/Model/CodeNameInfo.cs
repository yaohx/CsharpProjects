using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using MB.Util.XmlConfig; 
namespace MB.Util.Model {
    /// <summary>
    /// 代码名称描述。
    /// </summary>
    [MB.Util.XmlConfig.ModelXmlConfig(ByXmlNodeAttribute = true)]
    [DataContract]
    public class CodeNameInfo {
        private int _ID;
        private string _CODE;
        private string _NAME;
        public CodeNameInfo() {
        }
        public CodeNameInfo(string code, string name) {
            _CODE = code;
            _NAME = name;
        }
        public CodeNameInfo(int id,string code,string name) {
            _ID = id;
            _CODE = code;
            _NAME = name;
        }
        public override string ToString() {
            return _NAME;
        }
        /// <summary>
        /// ID
        /// </summary>
        [PropertyXmlConfig]
        [DataMember]
        public int ID {
            get {
                return _ID;
            }
            set {
                _ID = value;
            }
        }
        /// <summary>
        /// 编码。
        /// </summary>
        [PropertyXmlConfig]
        [DataMember]
        public string CODE {
            get {
                return _CODE;
            }
            set {
                _CODE = value;
            }
        }
        /// <summary>
        /// 名称。
        /// </summary>
        [PropertyXmlConfig]
        [DataMember]
        public string NAME {
            get {
                return _NAME;
            }
            set {
                _NAME = value;
            }
        }
    }
}
