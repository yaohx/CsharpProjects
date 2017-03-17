using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util.Model {
    /// <summary>
    /// 服务项配置信息
    /// </summary>
    public class ServerConfigInfo {
        private string _ServerName;
        private string _Credential;
        public ServerConfigInfo(string serverName) {
            _ServerName = serverName;
        }
        public ServerConfigInfo(string serverName, string credential) {
            _ServerName = serverName;
            _Credential = credential;
        }
        public override string ToString() {
            return _ServerName;
        }
        /// <summary>
        /// 服务IP地址
        /// </summary>
        public string ServerIP {
            get {
                return _ServerName;
            }
            set {
                _ServerName = value;
            }
        }
        /// <summary>
        /// 证书。
        /// </summary>
        public string Credential {
            get {
                return _Credential;
            }
            set {
                _Credential = value;
            }
        }
    }
}
