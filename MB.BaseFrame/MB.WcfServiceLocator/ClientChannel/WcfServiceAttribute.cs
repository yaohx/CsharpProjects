using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Util.Model;

namespace MB.WcfServiceLocator.ClientChannel {
    /// <summary>
    /// 在WCF的接口定义访问接口服务的一些信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
    public class WcfServiceAttribute : System.Attribute {

        private string _RelativePath;
        private string _CredentialFileName;

        /// <summary>
        /// WCF服务路径配置
        /// </summary>
        /// <param name="relativePath">WCF 服务访问的相对路径</param>
        /// <param name="credentialFileName">服务访问证书名称</param>
        public WcfServiceAttribute(string relativePath): this(relativePath, string.Empty) {
        }


        /// <summary>
        /// WCF服务路径配置
        /// </summary>
        /// <param name="relativePath">WCF 服务访问的相对路径</param>
        /// <param name="credentialFileName">服务访问证书名称</param>
        public WcfServiceAttribute(string relativePath, string credentialFileName) {
            _RelativePath = relativePath;
            _CredentialFileName = credentialFileName;
        }


        /// <summary>
        /// WCF 服务调用的相对相对路径，默认情况下为WCF 接口的完整路径名称。
        /// </summary>
        public string RelativePath {
            get { return _RelativePath; }
            set { _RelativePath = value; }
        }
        /// <summary>
        /// 服务调用证书
        /// </summary>
        public string CredentialFileName {
            get {
                return _CredentialFileName;
            }
            set {
                _CredentialFileName = value;
            }
        }

    }


    /// <summary>
    /// Wcf客户端调用时的配置信息
    /// 可以从接口属性，或者在编码时注入
    /// </summary>
    public class WcfClientInvokeCfgInfo { 

        private string _RelativePath;
        private string _CredentialFileNameOrServerName;

        /// <summary>
        /// WCF服务路径配置
        /// </summary>
        /// <param name="relativePath">WCF 服务访问的相对路径</param>
        /// <param name="credentialFileName">服务访问证书名称</param>
        public WcfClientInvokeCfgInfo(string relativePath): this(relativePath, string.Empty) {
        }


        /// <summary>
        /// WCF服务路径配置
        /// </summary>
        /// <param name="relativePath">WCF 服务访问的相对路径</param>
        /// <param name="credentialFileNameOrServerName">服务访问证书名称或者是服务的域名</param>
        public WcfClientInvokeCfgInfo(string relativePath, string credentialFileNameOrServerName) {
            _RelativePath = relativePath;
            _CredentialFileNameOrServerName = credentialFileNameOrServerName;
        }


        /// <summary>
        /// WCF 服务调用的相对相对路径，默认情况下为WCF 接口的完整路径名称。
        /// </summary>
        public string RelativePath {
            get { return _RelativePath; }
            set { _RelativePath = value; }
        }
        /// <summary>
        /// 服务访问证书名称或者是服务的域名
        /// </summary>
        public string CredentialFileNameOrServerName {
            get {
                return _CredentialFileNameOrServerName;
            }
            set {
                _CredentialFileNameOrServerName = value;
            }
        }

    }
    





}
