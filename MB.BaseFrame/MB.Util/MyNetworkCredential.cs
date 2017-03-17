using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Drawing;

namespace MB.Util {
    /// <summary>
    /// 提供创建Windows 访问的凭证。
    /// </summary>
    public class MyNetworkCredential {
        private static readonly string START_WINDOWS_CREDENTIAL = "StartWindowsCredential";
        private static readonly string START_WINDOWS_USER = "WindowsUserName";
        private static readonly string START_WINDOWS_PASSWORD = "WindowsUserPassword";
        private static readonly string INVOKE_WCF_SERVICE_CFG = "WcfServers";

        //  private static bool _StartWindowsCredential;
        private static MB.Util.Model.ServerConfigInfo _CurrentSelectedServerInfo;
        /// <summary>
        /// 
        /// </summary>
        public MyNetworkCredential() {


        }

        static MyNetworkCredential() {
            // string start = System.Configuration.ConfigurationManager.AppSettings[START_WINDOWS_CREDENTIAL];
            // _StartWindowsCredential = string.Compare(start, "true", true) == 0; ati
        }
        /// <summary>
        /// 当前选择的服务配置信息。
        /// </summary>
        public static MB.Util.Model.ServerConfigInfo CurrentSelectedServerInfo {
            get {
                if (_CurrentSelectedServerInfo == null) {
                    //如果得不到用户设置的WCF 服务配置项，那么取配置文件中默认配置项的第一项。
                    string servers = System.Configuration.ConfigurationManager.AppSettings[INVOKE_WCF_SERVICE_CFG];
                    if (!string.IsNullOrEmpty(servers)) {
                        string[] ss = servers.Split(';');
                        if (ss.Length > 0) {
                            string[] cfg = ss[0].Split(',');
                            if (cfg.Length == 2) {
                                _CurrentSelectedServerInfo = new Model.ServerConfigInfo(cfg[0], cfg[1]);
                            }
                        }
                    }

                }
                return _CurrentSelectedServerInfo;
            }
            set {
                _CurrentSelectedServerInfo = value;
                //设置当前服务选择的相关信息
            }
        }
        ///// <summary>
        ///// 判断是否启动Windows
        ///// </summary>
        ///// <returns></returns>
        //public static bool StartWindowsCredential{
        //    get {
        //        return _StartWindowsCredential;
        //    }
        //}
        // 
        /// <summary>
        /// 创建另外一种windows 凭证。
        /// </summary>
        /// <param name="cfgInfo"></param>
        /// <returns></returns>
        public static System.Net.NetworkCredential CreateNetworkCredential(MB.Util.Model.WcfCredentialInfo cfgInfo) {

            string user = cfgInfo.UserName;
            string name = cfgInfo.Password;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
            if (!string.IsNullOrEmpty(cfgInfo.Domain))
                credentials.Domain = cfgInfo.Domain;

            credentials.UserName = user;
            credentials.Password = name;
            return credentials;
        }
        /// <summary>
        /// 创建带有Windows 安全访问Wcf 客户端代理。
        /// </summary>
        /// <param name="wcfClient"></param>
        /// <returns></returns>
        [Obsolete("方法已经过期请使用 MB.WcfClient.WcfClientFactory.CreateWcfClient<T> 代替 并注意在对应的业务类中配置MB.WcfClient.WcfClientInvoke")]
        public static T CreateWcfClientWithCredential<T>(){
            try {
                if (_CurrentSelectedServerInfo == null) {
                    throw new MB.Util.APPException("没有设置服务的配置信息！");
                }
                MB.Util.TraceEx.Write(string.Format("开始根据 app.config 创建WCF 客户端Proxy{0}", typeof(T).FullName));

                T proxy = (T)MB.Util.DllFactory.Instance.CreateInstance(typeof(T));
                var cfgInfo = createClientConfigInfo(_CurrentSelectedServerInfo);

                ServiceEndpoint endpoint = (ServiceEndpoint)MB.Util.MyReflection.Instance.InvokePropertyForGet(proxy, "Endpoint");
                Uri u = endpoint.Address.Uri;
                //暂时处理 net.tcp 的地址由配置来决定
                if (string.Compare(u.Scheme, "net.tcp", true) != 0) {
                   // string newU = u.Scheme + "://" + cfgInfo.BaseAddress.Replace("http://", "") + u.AbsolutePath;
                    string newU = cfgInfo.BaseAddress + u.AbsolutePath;


                    if (cfgInfo.HostType == MB.Util.Model.WcfServiceHostType.IIS) {
                        string tempUrl = newU;
                        if (tempUrl.LastIndexOf('/') == tempUrl.Length - 1)
                            tempUrl = tempUrl.Remove(tempUrl.Length - 1, 1);

                        if (string.Compare(tempUrl.Substring(tempUrl.Length - 4, 4), ".svc", true) != 0)
                            newU = tempUrl + ".svc";
                    }
                    endpoint.Address = new EndpointAddress(newU);
                }

                if (cfgInfo.StartWindowsCredential && !string.IsNullOrEmpty(cfgInfo.UserName)) {
                    System.Net.NetworkCredential credential = MB.Util.MyNetworkCredential.CreateNetworkCredential(cfgInfo);
                    //System.ServiceModel.ClientBase<T> clientProxy = proxy as System.ServiceModel.ClientBase<T>;
                    //clientProxy.ClientCredentials.Windows.ClientCredential  = credential;
                    System.ServiceModel.Description.ClientCredentials clientCredentials = (System.ServiceModel.Description.ClientCredentials)MB.Util.MyReflection.Instance.InvokePropertyForGet(proxy, "ClientCredentials");
                    clientCredentials.Windows.ClientCredential = credential;

                }

                return proxy;
            }
            catch (MB.Util.APPException aex) {
                throw aex;
            }
            catch (Exception ex) {
                throw new MB.Util.APPException("创建WCF 客户端代理出错！", APPMessageType.SysErrInfo, ex);
            }
        }
        //得到服务配置信息
        private static MB.Util.Model.WcfCredentialInfo createClientConfigInfo(MB.Util.Model.ServerConfigInfo serverInfo) {

            if (serverInfo.Credential.IndexOf(':') >= 0) {
                MB.Util.Model.WcfCredentialInfo newInfo = new MB.Util.Model.WcfCredentialInfo();
                // MessageBox.Show("从中连接服务:" + serverInfo.Credential);
                newInfo.BaseAddress = serverInfo.Credential;
                return newInfo;
            }
            else {

                string creName = serverInfo.Credential;
                string fileFullName = AppDomain.CurrentDomain.BaseDirectory + serverInfo.Credential;
                if (!System.IO.File.Exists(fileFullName)) {
                    MB.Util.TraceEx.Write(string.Format("证书文件{0} 找不到", fileFullName));
                    throw new MB.Util.APPException("对应选择的服务器还没有注册相应的安全访问证书,请联系系统管理员", APPMessageType.DisplayToUser);
                }
                //throw new MB.Util.APPException(string.Format("证书文件{0} 找不到", fileFullName), APPMessageType.DisplayToUser);

                try {
                    System.IO.StreamReader r = new System.IO.StreamReader(fileFullName);
                    string txt = r.ReadToEnd();
                    r.Close();
                    r.Dispose();

                    string dStr = MB.Util.DESDataEncrypt.DecryptString(txt);
                    MB.Util.Serializer.EntityXmlSerializer<MB.Util.Model.WcfCredentialInfo> ser = new MB.Util.Serializer.EntityXmlSerializer<MB.Util.Model.WcfCredentialInfo>();
                    return ser.SingleDeSerializer(dStr, string.Empty);
                }
                catch (Exception ex) {
                    throw new MB.Util.APPException(string.Format("证书文件{0} 不是有效的证书！", fileFullName), APPMessageType.DisplayToUser, ex);
                }

            }
        }


    }
    ///// <summary>
    ///// 创建带有Windows 访问的凭证的WCF 客户端。
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //public class MyNetworkCredential<T>  where T : class{
    //    private T _WcfClient;
    //    public MyNetworkCredential() {
    //        _WcfClient = MyNetworkCredential.CreateWcfClientWithCredential<T>();
    //    }
    //    public T WcfClient {
    //        get {
    //            return _WcfClient;
    //        }
    //    }
    //}
}
