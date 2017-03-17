using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.Util.Model {
    /// <summary>
    /// 服务项配置信息。
    /// </summary>
    [MB.Util.XmlConfig.ModelXmlConfig(ByXmlNodeAttribute = false)]
    [DataContract]
    public class WcfCredentialInfo {
        private string _BaseAddress;
        private string _Domain;
        private string _UserName;
        private string _Password;
        private bool _StartWindowsCredential = true;
        private WcfServiceHostType _HostType = WcfServiceHostType.IIS;
        private string _EndpointFormatString;
        private bool _ReplaceRelativePathLastDot;
        private string _AppendDetails;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverInfo"></param>
        public WcfCredentialInfo() {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="start"></param>
        public WcfCredentialInfo(string baseAddress,string userName,string pwd,bool start) {
            _BaseAddress = baseAddress;
            _UserName = userName;
            _Password = pwd;
            _StartWindowsCredential = start;
        }
        /// <summary>
        /// WCF 基准地址。
        /// </summary>
        [MB.Util.XmlConfig.PropertyXmlConfig]
        [DataMember]
        public string BaseAddress {
            get {
                return _BaseAddress;
            }
            set {
                _BaseAddress = value;
            }
        }
        /// <summary>
        /// 用户名称。
        /// </summary>
        [MB.Util.XmlConfig.PropertyXmlConfig]
        [DataMember]
        public string UserName {
            get {
                return _UserName;
            }
            set {
                _UserName = value;
            }
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        [MB.Util.XmlConfig.PropertyXmlConfig]
        [DataMember]
        public string Password {
            get {
                return _Password;
            }
            set {
                _Password = value;
            }
        }
        /// <summary>
        /// 域
        /// </summary>
        [MB.Util.XmlConfig.PropertyXmlConfig]
        [DataMember]
        public string Domain {
            get {
                return _Domain;
            }
            set {
                _Domain = value;
            }
        }
        /// <summary>
        /// 只有本地调式的服务才不需要，远程都需要启动安全证书，直到用户反复抱怨为止。
        /// </summary>
        [MB.Util.XmlConfig.PropertyXmlConfig]
        [DataMember]
        public bool StartWindowsCredential {
            get {
                return _StartWindowsCredential;
            }
            set {
                _StartWindowsCredential = value;
            }
        }
        /// <summary>
        /// 服务承载类型
        /// </summary>
        [MB.Util.XmlConfig.PropertyXmlConfig]
        [DataMember]
        public WcfServiceHostType HostType {
            get {
                return _HostType;
            }
            set {
                _HostType = value;
            }
        }
        /// <summary>
        /// WCF 终节点的格式化字符窜。 
        /// 例如： http://192.168.125.70:8089/MyServices/{0}.svc
        /// </summary>
        [MB.Util.XmlConfig.PropertyXmlConfig]
        [DataMember]
        public string EndpointFormatString {
            get {
                return _EndpointFormatString;
            }
            set {
                _EndpointFormatString = value;
            }
        }
        /// <summary>
        /// 是否需要 修改终节点的相对路径
        /// </summary>
        [MB.Util.XmlConfig.PropertyXmlConfig]
        [DataMember]
        public bool ReplaceRelativePathLastDot {
            get {
                return _ReplaceRelativePathLastDot;
            }
            set {
                _ReplaceRelativePathLastDot = value;
            }
        }
        [MB.Util.XmlConfig.PropertyXmlConfig]
        [DataMember]
        public string AppendDetails {
            get {
                return _AppendDetails;
            }
            set {
                _AppendDetails = value;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum WcfServiceHostType {
        /// <summary>
        /// 自承载     developer
        /// </summary>
        SELF,
        /// <summary>
        /// 开发过程VS.NET 自启动服务。
        /// </summary>
        DEVELOPER,
        /// <summary>
        /// Windows Server   
        /// </summary>
        WS,
        /// <summary>
        /// Windows 击活服务
        /// </summary>
        WAS,
        /// <summary>
        /// Web IIS
        /// </summary>
        IIS
    }
    /// <summary>
    /// 服务绑定的类型。 
    /// </summary>
    public enum WcfServiceBindingType {
        /// <summary>
        /// WsHttp
        /// </summary>
        wsHttp,
        /// <summary>
        /// NetTcp
        /// </summary>
        netTcp
    }
}
