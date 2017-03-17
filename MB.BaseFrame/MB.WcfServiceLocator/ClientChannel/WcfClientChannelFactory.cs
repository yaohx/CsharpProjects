using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using MB.Util;
using MB.Util.Model;
using MB.WcfClient;

namespace MB.WcfServiceLocator.ClientChannel {

    /// <summary>
    /// WCF客户端代理访问生成工厂类，不生成客户端代理，直接通过接口定义访问服务端
    /// </summary>
    public class WcfClientChannelFactory {

        private static readonly string NET_TCP_HEADER = "net.tcp://";
        private static readonly string HTTP_HEADER = "http://";
        private static readonly string HTTP_BINDING_NAME = "WcfWsHttpBindingName";
        private static readonly string TCP_BINDING_NAME = "WcfNetTcpBindingName";
        private static readonly string SERVICE_DOMAIN_NAME = "ServiceDomainName";
        private static readonly string END_POINT_ADD_FORMATE_STRING = "EndpointFormatString";
        private static readonly string REPLACE_PATH_LAST_DOT = "ReplaceRelativePathLastDot";
        private static readonly string ENABLE_GZIP_MESSAGE = "EnableGZipMessageEncoding";

        private static object _SynLock = new object();
        private const string CUSTOM_WCF_MSG_INSPECTORS_CONFIG = "CustomWcfMessageInspectors"; //在appsetting中可以定义message inspector, {类型},{DLL};{类型},{DLL}的格式
        private static MB.Util.MyDataCache<string, string> _MyInvokeCache; //把每次接口的调用，放入这个缓存，避免每次调用都生成日志，造成日志过多
        private static List<IClientMessageInspector> _ClientMessageInspectors; //自定义的MessageInspector
        private static Dictionary<Type, WcfClientInvokeCfgInfo> _WcfInvokeContainer;

        /// <summary>
        /// 自定义的Client Message Inspectors, 继承自IClientMessageInspector
        /// </summary>
        public static List<IClientMessageInspector> ClientMessageInspectors {
            get { return WcfClientChannelFactory._ClientMessageInspectors; }
        }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static WcfClientChannelFactory() {
            if (_MyInvokeCache == null)
                _MyInvokeCache = new MB.Util.MyDataCache<string, string>();

            string customInspectorCfg = System.Configuration.ConfigurationManager.AppSettings[CUSTOM_WCF_MSG_INSPECTORS_CONFIG];
            _ClientMessageInspectors = createMessageInspectors(customInspectorCfg);

            if (_WcfInvokeContainer == null)
                _WcfInvokeContainer = new Dictionary<Type, WcfClientInvokeCfgInfo>();
        }

        #region 注册接口

        /// <summary>
        /// 根据WCF接口类型上的属性，注册wcf调用配置信息
        /// </summary>
        /// <param name="iServiceType">需要注册的类型</param>
        public static void RegisterTypeIfMissing(Type iServiceType) {
            WcfServiceAttribute cfgAtt = getWcfServiceAttribute(iServiceType);
            WcfClientInvokeCfgInfo cfgInfo = new WcfClientInvokeCfgInfo(cfgAtt.RelativePath, cfgAtt.CredentialFileName);
            RegisterTypeIfMissing(iServiceType, cfgInfo);
        }

        /// <summary>
        /// 只注册wcf配置调用信息中的相对路径
        /// </summary>
        /// <param name="iServiceType">要注册的Wcf接口的类型</param>
        /// <param name="relativePath">相对路径</param>
        public static void RegisterTypeIfMissing(Type iServiceType, string relativePath) {
            WcfClientInvokeCfgInfo cfgInfo = new WcfClientInvokeCfgInfo(relativePath);
            RegisterTypeIfMissing(iServiceType, cfgInfo);
        }

        /// <summary>
        /// 注册wcf配置调用信息
        /// </summary>
        /// <param name="iServiceType">要注册的Wcf接口的类型</param>
        /// <param name="invokeCfg">wcf客户端调用配置信息</param>
        public static void RegisterTypeIfMissing(Type iServiceType, WcfClientInvokeCfgInfo invokeCfg) {
            if (!_WcfInvokeContainer.ContainsKey(iServiceType)) {
                lock (_SynLock) {
                    if (!_WcfInvokeContainer.ContainsKey(iServiceType)) {
                        _WcfInvokeContainer.Add(iServiceType, invokeCfg);
                    }
                }
            }
        }

        #endregion

        #region 根据接口类型创建客户端访问Proxy
        /// <summary>
        /// 根据指定的系统编号，直接寻找WCF客户端地址
        /// </summary>
        /// <typeparam name="T">客户端代理实例</typeparam>
        /// <param name="code">系统编码</param>
        /// <param name="bindingCfgName">自己指定绑定的名称,如果绑定为空，则根据绑定类型和配置项来决定绑定配置</param>
        /// <returns>WCF接口调用类型</returns>
        public static WcfClientProxyScope<T> CreateWcfClientProxy<T>() {
            return CreateWcfClientProxy<T>(string.Empty);
        }

        /// <summary>
        /// 根据指定的系统编号，直接寻找WCF客户端地址
        /// </summary>
        /// <typeparam name="T">客户端代理实例</typeparam>
        /// <param name="code">系统编码</param>
        /// <param name="bindingCfgName">自己指定绑定的名称,如果绑定为空，则根据绑定类型和配置项来决定绑定配置</param>
        /// <returns>WCF接口调用类型</returns>
        public static WcfClientProxyScope<T> CreateWcfClientProxy<T>(string bindingCfgName) {
            try {
         
                Type type = typeof(T);
                RegisterTypeIfMissing(type);
                WcfClientInvokeCfgInfo wcfClientCfgInfo = _WcfInvokeContainer[type];
                if (string.IsNullOrEmpty(wcfClientCfgInfo.CredentialFileNameOrServerName)) {
                    if (MB.Util.MyNetworkCredential.CurrentSelectedServerInfo == null) {
                        throw new MB.Util.APPException("没有设置服务的配置信息,请检查节点AppSetting/WcfServers！");
                    }
                    wcfClientCfgInfo.CredentialFileNameOrServerName = MB.Util.MyNetworkCredential.CurrentSelectedServerInfo.Credential;
                }

                MB.Util.Model.WcfCredentialInfo wcfCredentialInfo = createWcfCredentialByCfgAttribute(wcfClientCfgInfo);
                string gzip = System.Configuration.ConfigurationManager.AppSettings[ENABLE_GZIP_MESSAGE];
                bool isGZipCustomBinding = !string.IsNullOrEmpty(gzip) && string.Compare(gzip, "True", true) == 0;
                System.ServiceModel.Channels.Binding binding = createBinding(wcfCredentialInfo, isGZipCustomBinding, string.Empty);
                string uri = buildEndpointAddress(wcfCredentialInfo, wcfClientCfgInfo);
                System.ServiceModel.EndpointAddress address = new EndpointAddress(uri);
                NetworkCredential credentialInfo = null;
                if (!string.IsNullOrEmpty(wcfCredentialInfo.UserName) && !string.IsNullOrEmpty(wcfCredentialInfo.Password))
                      credentialInfo = new NetworkCredential(wcfCredentialInfo.UserName, wcfCredentialInfo.Password, wcfCredentialInfo.Domain);
                T proxy = (T)createProxyInstance<T>(binding, address, credentialInfo);

                //如果每个调用都需要记录的话为产生很多日志，这里特处理一下。
                if (!_MyInvokeCache.ContainsKey(uri)) {
                    _MyInvokeCache.Add(uri, uri);
                    MB.Util.TraceEx.Write(string.Format("开始根据地址{0} 创建WCF 客户端Channel Proxy{1}", uri, typeof(T).FullName));
                }
                WcfClientProxyScope<T> proxyScope = new WcfClientProxyScope<T>(proxy);

                return proxyScope;
            }
            catch (MB.Util.APPException aex) {
                throw aex;
            }
            catch (Exception ex) {
                throw new MB.Util.APPException("创建WCF 客户端代理出错！", APPMessageType.SysErrInfo, ex);
            }
        }


        #region 内部处理函数
        //通过配置文件创建全局message isnpector
        private static List<IClientMessageInspector> createMessageInspectors(string customMessageInspectorCfg) {
            if (string.IsNullOrEmpty(customMessageInspectorCfg)) return null;

            string[] inspectorCfgs = customMessageInspectorCfg.Split(';');
            if (inspectorCfgs != null && inspectorCfgs.Length > 0) {
                List<IClientMessageInspector> results = new List<IClientMessageInspector>();
                foreach (string inspectorCfgStr in inspectorCfgs) {
                    string[] inspectorCfg = inspectorCfgStr.Split(',');
                    if (inspectorCfg == null || inspectorCfg.Length != 2) continue;
                    object obj = MB.Util.DllFactory.Instance.LoadObject(inspectorCfg[0], inspectorCfg[1]);
                    results.Add(obj as IClientMessageInspector);
                }

                return results;
            }
            return null;
        }

        /// <summary>
        /// 自动注册
        /// </summary>
        private static WcfClientInvokeCfgInfo checkAndRegisterWcfInvokeCfgInfo(Type type) {
            if (_WcfInvokeContainer.ContainsKey(type)) {
                return _WcfInvokeContainer[type];
            }
            else {
                RegisterTypeIfMissing(type);
                return _WcfInvokeContainer[type];
            }
        }

        /// <summary>
        /// 得到WcfServiceAttribute
        /// </summary>
        /// <param name="iServiceType">WcfServiceAttribute使用的类型</param>
        /// <returns>WcfServiceAttribute</returns>
        private static WcfServiceAttribute getWcfServiceAttribute(Type iServiceType) {
            object[] desc = iServiceType.GetCustomAttributes(typeof(WcfServiceAttribute), false);
            if (desc == null || desc.Length <= 0)
                throw new MB.Util.APPException("没有设置调用服务的配置信息WcfServiceAttribute！");

            WcfServiceAttribute cfgAtt = desc[0] as WcfServiceAttribute;
            return cfgAtt;
        }

        /// <summary>
        /// 根据接口类型的配置，返回wcf的访问信息和安全信息
        /// </summary>
        /// <param name="wcfClientCfgInfo">wcf类型的配置信息</param>
        /// <returns>wcf的访问信息和安全信息</returns>
        private static MB.Util.Model.WcfCredentialInfo createWcfCredentialByCfgAttribute(WcfClientInvokeCfgInfo wcfClientCfgInfo) {
            if (wcfClientCfgInfo.CredentialFileNameOrServerName.IndexOf(':') >= 0) {
                MB.Util.Model.WcfCredentialInfo newInfo = new MB.Util.Model.WcfCredentialInfo();
                newInfo.BaseAddress = wcfClientCfgInfo.CredentialFileNameOrServerName;
                newInfo.HostType = MB.Util.Model.WcfServiceHostType.DEVELOPER;
                return newInfo;
            }
            else {
                string appPath = MB.Util.General.GeApplicationDirectory();
                string fileFullName = appPath + wcfClientCfgInfo.CredentialFileNameOrServerName;
                if (!System.IO.File.Exists(fileFullName)) {
                    MB.Util.TraceEx.Write(string.Format("证书文件{0} 找不到", fileFullName));
                    throw new MB.Util.APPException("对应选择的服务器还没有注册相应的安全访问证书,请联系系统管理员", APPMessageType.DisplayToUser);
                }
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

        #region 创建绑定
        //创建指定的Wcf 绑定类型。
        private static System.ServiceModel.Channels.Binding createBinding(MB.Util.Model.WcfCredentialInfo credentialInfo, bool isGZipCustomBinding) {
            return createBinding(credentialInfo, isGZipCustomBinding, string.Empty);

        }

        private static System.ServiceModel.Channels.Binding createBinding(MB.Util.Model.WcfCredentialInfo credentialInfo,
                bool isGZipCustomBinding, string usedbindingCfgName) {
            System.ServiceModel.Channels.Binding binding = null;
            var cfgInfo = credentialInfo;
            WcfServiceBindingType bindingType = getBindingType(credentialInfo);

            if (bindingType == WcfServiceBindingType.wsHttp) {
                string bindingCfgName;
                if (string.IsNullOrEmpty(usedbindingCfgName))
                    bindingCfgName = System.Configuration.ConfigurationManager.AppSettings[HTTP_BINDING_NAME];
                else
                    bindingCfgName = usedbindingCfgName;

                if (string.IsNullOrEmpty(bindingCfgName))
                    throw new MB.Util.APPException(string.Format("wsHttp绑定需要配置{0}", HTTP_BINDING_NAME), MB.Util.APPMessageType.SysErrInfo);

                //string gzip = System.Configuration.ConfigurationManager.AppSettings[ENABLE_GZIP_MESSAGE]; 

                if (!isGZipCustomBinding)
                    binding = new WSHttpBinding(bindingCfgName);
                else
                    binding = new CustomBinding(bindingCfgName);
            }
            else if (bindingType == WcfServiceBindingType.netTcp) {
                string cfgName = System.Configuration.ConfigurationManager.AppSettings[TCP_BINDING_NAME];
                if (string.IsNullOrEmpty(cfgName))
                    throw new MB.Util.APPException(string.Format("netTcp 绑定需要配置{0}", TCP_BINDING_NAME), MB.Util.APPMessageType.SysErrInfo);

                binding = new System.ServiceModel.NetTcpBinding(cfgName);
            }
            else {
                throw new MB.Util.APPException(string.Format("Wcf 客户端绑定目前不支持{0}", bindingType.ToString()), MB.Util.APPMessageType.SysErrInfo);
            }
            return binding;
        }

        //根据配置的信息获取绑定的类型。
        private static WcfServiceBindingType getBindingType(MB.Util.Model.WcfCredentialInfo credentialInfo) {
            WcfServiceBindingType bindingType = (credentialInfo.HostType == MB.Util.Model.WcfServiceHostType.IIS ||
                credentialInfo.HostType == MB.Util.Model.WcfServiceHostType.DEVELOPER) ? WcfServiceBindingType.wsHttp : WcfServiceBindingType.netTcp;
            //如果是开发环境下的话，那么根据用户配置的基地址来决定
            if (credentialInfo.HostType == MB.Util.Model.WcfServiceHostType.DEVELOPER) {
                if (credentialInfo.BaseAddress.Length > NET_TCP_HEADER.Length) {
                    if (string.Compare(credentialInfo.BaseAddress.Substring(0, NET_TCP_HEADER.Length), NET_TCP_HEADER, true) == 0) {
                        bindingType = WcfServiceBindingType.netTcp;
                    }
                }
            }
            return bindingType;
        }
        #endregion

        #region 拼接对应的终结点的地址

        //拼接对应的终结点的地址
        private static string buildEndpointAddress(MB.Util.Model.WcfCredentialInfo credentialInfo, WcfClientInvokeCfgInfo wcfClientCfgInfo) {

            string relativePath = wcfClientCfgInfo.RelativePath;
            string replaceDot = System.Configuration.ConfigurationManager.AppSettings[REPLACE_PATH_LAST_DOT];
            //判断是否修改相对路径的地址
            if ((!string.IsNullOrEmpty(replaceDot) && string.Compare(replaceDot, "True", true) == 0) || credentialInfo.ReplaceRelativePathLastDot)
                relativePath = replaceRelativePath(relativePath);

            if (!string.IsNullOrEmpty(credentialInfo.EndpointFormatString)) {
                var v = System.Text.RegularExpressions.Regex.Matches(credentialInfo.EndpointFormatString, @"\{\d\}");
                if (v.Count != 1)
                    throw new MB.Util.APPException(string.Format(@"证书 的 EndpointFormatString:{0} 设置有误,只能设置一个参数", credentialInfo.EndpointFormatString), APPMessageType.SysErrInfo);

                return string.Format(credentialInfo.EndpointFormatString, relativePath);
            }
            else {
                string formateString = System.Configuration.ConfigurationManager.AppSettings[END_POINT_ADD_FORMATE_STRING];
                if (!string.IsNullOrEmpty(formateString)) {
                    return string.Format(formateString, getAbsoluteBassAddress(credentialInfo.BaseAddress), relativePath);
                }
                else {
                    WcfServiceBindingType bindingType = getBindingType(credentialInfo);
                    string header = bindingType == WcfServiceBindingType.wsHttp ? HTTP_HEADER : NET_TCP_HEADER;

                    string path = System.Configuration.ConfigurationManager.AppSettings[SERVICE_DOMAIN_NAME];
                    if (string.IsNullOrEmpty(path))
                        throw new MB.Util.APPException(string.Format("app.config 需要配置程序域名称 {0}", SERVICE_DOMAIN_NAME), MB.Util.APPMessageType.SysErrInfo);

                    return string.Format("{0}{1}/{2}/{3}", header, getAbsoluteBassAddress(credentialInfo.BaseAddress), path, relativePath);
                }
            }
        }

        //修改相对路径的地址表现形式
        private static string replaceRelativePath(string relativePath) {
            string path = relativePath;
            int p1 = path.LastIndexOf('/');
            int p = path.LastIndexOf('.');
            //特殊处理
            if (p1 > p) return path;
            if (p <= 0) return path;
            return path.Substring(0, p) + '/' + path.Substring(p + 1, path.Length - p - 1);
        }

        //获取基地址
        private static string getAbsoluteBassAddress(string baseAddress) {
            int index = baseAddress.IndexOf("//");
            if (index < 0) {
                return baseAddress;
            }
            else {
                return baseAddress.Substring(index + 2, baseAddress.Length - index - 2);
            }
        }

        #endregion

        //创建Wcf 服务端代理
        private static T createProxyInstance<T>(Binding binding, EndpointAddress address, NetworkCredential credential) {
            Type objType = typeof(T);
            if (objType == null) return default(T);
            try {
                object[] pars = new object[] { binding, address };
                var channelFactory = new System.ServiceModel.ChannelFactory<T>(binding, address);
                if (credential != null) {
                    channelFactory.Credentials.Windows.ClientCredential = credential;
                }

                System.ServiceModel.Description.ServiceEndpoint endPoint =
                          (System.ServiceModel.Description.ServiceEndpoint)MB.Util.MyReflection.Instance.InvokePropertyForGet(channelFactory, "Endpoint");


                object obj = typeof(System.ServiceModel.ServiceHost).Assembly.CreateInstance("System.ServiceModel.Dispatcher.DataContractSerializerServiceBehavior",
                             true, BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { false, Int32.MaxValue }, null, null);

                IEndpointBehavior dataSerializerBehavior = obj as IEndpointBehavior;
                endPoint.Behaviors.Add(dataSerializerBehavior);
                endPoint.Behaviors.Add(new EndpointMessageBehavior());
               
                var clientProxy = channelFactory.CreateChannel();
                if (clientProxy == null)
                    throw new APPException(string.Format("根据类型:{0} 创建实例有误！", typeof(T).FullName));

                return clientProxy;

            }
            catch (APPException) {
                throw;
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(string.Format("根据类型:{0}创建实例有误！", objType), APPMessageType.SysErrInfo, ex);
            }

        }
        #endregion

        #endregion

        #region 直接调用wcf接口

        /// <summary>
        /// 调用WCF接口方法
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="execute">执行委托</param>
        public static void InvokeWcfMethod<T>(Action<T> execute) {
            using (WcfClientProxyScope<T> proxy = CreateWcfClientProxy<T>()) {
                execute(proxy.ClientProxy);
            }
        }

        /// <summary>
        /// 调用WCF接口方法
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="execute">执行委托</param>
        /// <returns>返回值</returns>
        public static TResult InvokeWcfMethod<T, TResult>(Func<T, TResult> execute) {
            using (WcfClientProxyScope<T> proxy = CreateWcfClientProxy<T>()) {
                return execute(proxy.ClientProxy);
            }
        }

        #endregion
       

        

    }


    
}
