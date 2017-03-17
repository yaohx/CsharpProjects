using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Reflection;
using System.ServiceModel.Description;
using System.Runtime.Serialization;

using MB.Util;
using MB.Util.Model;
using System.ServiceModel.Channels;
using MB.WcfServiceLocator;
using MB.Util.Serializer;
using MB.BaseFrame;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Configuration;
namespace MB.WcfClient
{
    /// <summary>
    /// 关联系统的系统编码
    /// 用来和证书中的系统编码做匹配，以获取关联系统的服务地址
    /// </summary>
    public enum SystemCode
    {
        /// <summary>
        /// 主中间层
        /// </summary>
        [Description("主中间层")]
        Main,
        /// <summary>
        /// 分销中间层
        /// </summary>
        [Description("分销中间层")]
        MB001,
        /// <summary>
        /// 物流
        /// </summary>
        [Description("物流中间层")]
        MB002,
        /// <summary>
        /// 分销的接口
        /// </summary>
        [Description("分销接口")]
        MBInterface001,
        /// <summary>
        /// 物流的接口
        /// </summary>
        [Description("物流接口")]
        MBInterface002,
        /// <summary>
        /// 通过WCF访问Queue
        /// 测试环境 http://10.100.20.102:55672/#/exchanges
        /// 生产环境 http://mb1.live.mb.com:55672/#/
        /// </summary>
        [Description("数据交换平台的队列")]
        MBQueue,
        /// <summary>
        /// 数据交换平台，没有接口给其他系统调用的
        /// 测试 http://10.100.20.100:8010/HomePage/HomeFrame.aspx
        /// 生产 http://10.100.25.40:9001/Index.aspx?ReturnUrl=%2f  
        /// </summary>
        [Description("数据交换平台")]
        MBDataExchange,
        /// <summary>
        /// POS接口
        /// </summary>
        [Description("POS接口")]
        MBPos
    }
    
    /// <summary>
    /// WCF 客户端方法调用配置。
    /// 特别说明：配置了该属性的类，那么该属性下的所有方法都使用同样的相对路径来调用WCF 服务。
    /// 原则上调用WCF 的服务必须通过本地的业务来调用。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class WcfClientInvokeAttribute : System.Attribute
    {
        /// <summary>
        /// WCF 客户端方法调用配置。
        /// </summary>
        /// <param name="wcfClientType">WCF 服务客户端代理</param>
        /// <param name="relativePath">WCF 服务访问的绝对路径</param>
        public WcfClientInvokeAttribute(Type wcfClientType, string relativePath)
        {
            _WcfClientType = wcfClientType;
            _RelativePath = relativePath;
            SystemCode = SystemCode.Main;
        }

        /// <summary>
        /// WCF 客户端方法调用配置。
        /// </summary>
        /// <param name="wcfClientType">WCF 服务客户端代理</param>
        /// <param name="relativePath">WCF 服务访问的绝对路径</param>
        /// <param name="code">关联系统代码</param>
        public WcfClientInvokeAttribute(Type wcfClientType, string relativePath, SystemCode code) 
            : this(wcfClientType, relativePath)
        {
            SystemCode = code;
        }
        private Type _WcfClientType;
        /// <summary>
        /// WCF 服务客户端代理类型。
        /// </summary>
        public Type WcfClientType {
            get { return _WcfClientType; }
            set { _WcfClientType = value; }
        }

        private string _RelativePath;
        /// <summary>
        /// WCF 服务调用的相对相对路径，默认情况下为WCF 接口的完整路径名称。
        /// </summary>
        public string RelativePath {
            get { return _RelativePath; }
            set { _RelativePath = value; }
        }

        /// <summary>
        /// WCF服务的系统编码，该编码需要与客户端证书中的系统编码相一致
        /// 请使用WcfClientInvokeAttribute中的常量来赋值
        /// </summary>
        public SystemCode SystemCode
        {
            get;
            set;
        }

    }

    


    /// <summary>
    /// WCF 服务客户端调用公共处理函数。
    /// </summary>
    public class WcfClientFactory
    {
        private static readonly string IIS_SPEC = ".svc";
        private static readonly string NET_TCP_HEADER = "net.tcp://";
        private static readonly string HTTP_HEADER = "http://";
        private static readonly string HTTP_BINDING_NAME = "WcfWsHttpBindingName";
        private static readonly string TCP_BINDING_NAME = "WcfNetTcpBindingName";
        private static readonly string SERVICE_DOMAIN_NAME = "ServiceDomainName";
        //例如： http://{0}/MyServices/{1}.svc
        private static readonly string END_POINT_ADD_FORMATE_STRING = "EndpointFormatString";
        //edit by cdc 2010-2-28 修改相对路径的表达形式
        //之前是: MB.ERP.MyserviceAPP.IFace.MyService ,修改为 MB.ERP.MyserviceAPP.IFace/MyService
        private static readonly string REPLACE_PATH_LAST_DOT = "ReplaceRelativePathLastDot";
        //
        private static readonly string ENABLE_GZIP_MESSAGE = "EnableGZipMessageEncoding";
        private static readonly string ENABLE_WCF_PERFORMANCE_MONITOR = "EableWcfPerformaceMonitor";
        private static readonly string CUSTOM_ENDPOINT_BEHAVIORS = "CustomEndPointBehavior";

        private static MB.Util.MyDataCache<string, string> _MyInvokeCache;
        static WcfClientFactory() {
            _MyInvokeCache = new MB.Util.MyDataCache<string, string>();
        }

        #region CreateWcfClient 根据VS自动生成的Client来创建Proxy

        /// <summary>
        /// 创建带有Windows 安全访问Wcf 客户端代理。 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clientRule"></param>
        /// <returns></returns>
        public static T CreateWcfClient<T>(object clientRule) where T : class {
            return CreateWcfClient<T>(clientRule, (MB.Util.Model.WcfCredentialInfo)null);
        }
        /// <summary>
        /// 创建带有Windows 安全访问Wcf 客户端代理。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clientRule"></param>
        /// <param name="serverInfo"></param>
        /// <returns></returns>
        public static T CreateWcfClient<T>(object clientRule, MB.Util.Model.ServerConfigInfo serverInfo) where T : class {
            return CreateWcfClient<T>(clientRule, createClientConfigInfo(serverInfo));
        }

        /// <summary>
        /// 创建带有Windows 安全访问Wcf 客户端代理。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clientRule"></param>
        /// <param name="credentialInfo"></param>
        /// <returns></returns>
        public static T CreateWcfClient<T>(object clientRule, MB.Util.Model.WcfCredentialInfo credentialInfo) where T : class {
            try {
                if (MB.Util.MyNetworkCredential.CurrentSelectedServerInfo == null) {
                    throw new MB.Util.APPException("没有设置服务的配置信息！");
                }
                
                WcfClientInvokeAttribute cfgAtt = getWcfInvokeAtt(clientRule.GetType(), typeof(T));
                if (cfgAtt == null) {
                    //特殊说明： 从兼容性考虑，暂时先这里处理，以后需要去掉，要独立调用
                    return MB.Util.MyNetworkCredential.CreateWcfClientWithCredential<T>();
                }
                string gzip = System.Configuration.ConfigurationManager.AppSettings[ENABLE_GZIP_MESSAGE];

                bool isGZipCustomBinding = !string.IsNullOrEmpty(gzip) && string.Compare(gzip, "True", true) == 0;

                MB.Util.Model.WcfCredentialInfo cfgInfo = null;
                if (credentialInfo == null) {
                    cfgInfo = createClientConfigInfo(MB.Util.MyNetworkCredential.CurrentSelectedServerInfo);
                }
                else {
                    cfgInfo = credentialInfo;
                }

                if (cfgAtt.SystemCode != SystemCode.Main)
                {
                    string appendDetail = cfgInfo.AppendDetails;
                    //从主服务的credetial转化到关联系统的credential
                    MB.Util.Model.WcfCredentialInfo cfgSubSystemInfo = resolveSubSystemCredentialInfo(cfgAtt.SystemCode, appendDetail, cfgInfo);
                    if (cfgSubSystemInfo != null)
                        cfgInfo = cfgSubSystemInfo;
                    else
                        cfgAtt.SystemCode = SystemCode.Main;

                }


                System.ServiceModel.Channels.Binding binding = createBinding(cfgInfo, isGZipCustomBinding);
                string uri = buildEndpointAddress(cfgInfo, cfgAtt);

                //如果每个调用都需要记录的话为产生很多日志，这里特处理一下。
                if (!_MyInvokeCache.ContainsKey(uri)) {
                    _MyInvokeCache.Add(uri, uri);
                    MB.Util.TraceEx.Write(string.Format("开始根据地址{0} 创建WCF 客户端Proxy{1}", uri, typeof(T).FullName));
                }

                System.ServiceModel.EndpointAddress address = new EndpointAddress(uri);
                T proxy = (T)createProxyInstance<T>(binding, address);

                System.ServiceModel.Description.ServiceEndpoint endPoint =
                                          (System.ServiceModel.Description.ServiceEndpoint)MB.Util.MyReflection.Instance.InvokePropertyForGet(proxy, "Endpoint");


                object obj = typeof(System.ServiceModel.ServiceHost).Assembly.CreateInstance("System.ServiceModel.Dispatcher.DataContractSerializerServiceBehavior",
                             true, BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { false, Int32.MaxValue }, null, null);

                IEndpointBehavior dataSerializerBehavior = obj as IEndpointBehavior;
                endPoint.Behaviors.Add(dataSerializerBehavior);

                //增加分页消息头
                endPoint.Behaviors.Add(new WcfDataQueryBehavior());

                //增加性能指标检测行为
                string wcfMonitorEnable = System.Configuration.ConfigurationManager.AppSettings[ENABLE_WCF_PERFORMANCE_MONITOR];
                bool isWcfMonitorEnable = string.IsNullOrEmpty(wcfMonitorEnable) || string.Compare(gzip, "True", true) == 0;
                if (isWcfMonitorEnable)
                    endPoint.Behaviors.Add(new MB.Util.Monitors.WcfPerformanceMonitorBehavior());

                #region 加载应用程序自定义的behavior

                //增加扩展的Behavior,从配置文件中读取配置的扩展Behavior   
                //在APPSetting中每个扩展由分号隔开，类型和Assmbly之间由逗号隔开
                string customerBehaviorSrc = System.Configuration.ConfigurationManager.AppSettings[CUSTOM_ENDPOINT_BEHAVIORS];
                if (!string.IsNullOrEmpty(customerBehaviorSrc)) {
                    string[] customerBehaviors = customerBehaviorSrc.Split(';');
                    foreach (string customerBehavior in customerBehaviors) {
                        try {
                            string[] beTypes = customerBehavior.Split(',');
                            object extBehavior = DllFactory.Instance.LoadObject(beTypes[0], beTypes[1]);
                            if (extBehavior != typeof(WcfDataQueryBehavior)
                                && extBehavior != typeof(MB.Util.Monitors.WcfPerformanceMonitorBehavior)) {
                                IEndpointBehavior extEndPointBehavior = extBehavior as IEndpointBehavior;
                                endPoint.Behaviors.Add(extEndPointBehavior);
                            }
                        }
                        catch (Exception ex) {
                            MB.Util.TraceEx.Write(string.Format("加载自定义behvaior出错：{0}；错误：{1}", customerBehavior, ex.ToString()));
                        }
                    }
                }
                #endregion

                if (!isGZipCustomBinding) {
                    if (cfgInfo.StartWindowsCredential && !string.IsNullOrEmpty(cfgInfo.UserName)) {
                        System.Net.NetworkCredential credential = MB.Util.MyNetworkCredential.CreateNetworkCredential(cfgInfo);
                        System.ServiceModel.Description.ClientCredentials clientCredentials =
                                                (System.ServiceModel.Description.ClientCredentials)MB.Util.MyReflection.Instance.InvokePropertyForGet(proxy, "ClientCredentials");

                        clientCredentials.Windows.ClientCredential = credential;
                    }
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

        /// <summary>
        /// 根据指定的系统编号，直接寻找WCF客户端地址
        /// </summary>
        /// <typeparam name="T">客户端代理实例</typeparam>
        /// <param name="relativePath">相对地址</param>
        /// <param name="code">系统编码</param>
        /// <returns></returns>
        public static T CreateWcfClient<T>(string relativePath, SystemCode code) where T : class
        {
            return CreateWcfClient<T>(relativePath, code, string.Empty);
        }

        /// <summary>
        /// 根据指定的系统编号，直接寻找WCF客户端地址
        /// </summary>
        /// <typeparam name="T">客户端代理实例</typeparam>
        /// <param name="relativePath">相对地址</param>
        /// <param name="code">系统编码</param>
        /// <param name="bindingCfgName">自己指定绑定的名称</param>
        /// <returns></returns>
        public static T CreateWcfClient<T>(string relativePath, SystemCode code, string bindingCfgName) where T : class
        {
            try
            {
                if (MB.Util.MyNetworkCredential.CurrentSelectedServerInfo == null)
                {
                    throw new MB.Util.APPException("没有设置服务的配置信息！");
                }
                WcfClientInvokeAttribute cfgAtt = new WcfClientInvokeAttribute(typeof(T), relativePath, code);

                string gzip = System.Configuration.ConfigurationManager.AppSettings[ENABLE_GZIP_MESSAGE];

                bool isGZipCustomBinding = !string.IsNullOrEmpty(gzip) && string.Compare(gzip, "True", true) == 0;

                MB.Util.Model.WcfCredentialInfo cfgInfo = createClientConfigInfo(MB.Util.MyNetworkCredential.CurrentSelectedServerInfo);

                if (code != SystemCode.Main)
                {
                    string appendDetail = cfgInfo.AppendDetails;
                    //从主服务的credetial转化到关联系统的credential
                    MB.Util.Model.WcfCredentialInfo cfgSubSystemInfo = resolveSubSystemCredentialInfo(code, appendDetail, cfgInfo);
                    if (cfgSubSystemInfo != null)
                        cfgInfo = cfgSubSystemInfo;
                    else
                        code = SystemCode.Main;
                }


                System.ServiceModel.Channels.Binding binding = createBinding(cfgInfo, isGZipCustomBinding, bindingCfgName);
                string uri = buildEndpointAddress(cfgInfo, cfgAtt);

                //如果每个调用都需要记录的话为产生很多日志，这里特处理一下。
                if (!_MyInvokeCache.ContainsKey(uri))
                {
                    _MyInvokeCache.Add(uri, uri);
                    MB.Util.TraceEx.Write(string.Format("开始根据地址{0} 创建WCF 客户端Proxy{1}", uri, typeof(T).FullName));
                }

                System.ServiceModel.EndpointAddress address = new EndpointAddress(uri);
                T proxy = (T)createProxyInstance<T>(binding, address);

                System.ServiceModel.Description.ServiceEndpoint endPoint =
                                          (System.ServiceModel.Description.ServiceEndpoint)MB.Util.MyReflection.Instance.InvokePropertyForGet(proxy, "Endpoint");


                object obj = typeof(System.ServiceModel.ServiceHost).Assembly.CreateInstance("System.ServiceModel.Dispatcher.DataContractSerializerServiceBehavior",
                             true, BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { false, Int32.MaxValue }, null, null);

                IEndpointBehavior dataSerializerBehavior = obj as IEndpointBehavior;
                endPoint.Behaviors.Add(dataSerializerBehavior);

                if (!isGZipCustomBinding)
                {
                    if (cfgInfo.StartWindowsCredential && !string.IsNullOrEmpty(cfgInfo.UserName))
                    {
                        System.Net.NetworkCredential credential = MB.Util.MyNetworkCredential.CreateNetworkCredential(cfgInfo);
                        System.ServiceModel.Description.ClientCredentials clientCredentials =
                                                (System.ServiceModel.Description.ClientCredentials)MB.Util.MyReflection.Instance.InvokePropertyForGet(proxy, "ClientCredentials");

                        clientCredentials.Windows.ClientCredential = credential;
                    }
                }
                return proxy;
            }
            catch (MB.Util.APPException aex)
            {
                throw aex;
            }
            catch (Exception ex)
            {
                throw new MB.Util.APPException("创建WCF 客户端代理出错！", APPMessageType.SysErrInfo, ex);
            }
        }

        #endregion

        


        #region 内部函数

        //拼接对应的类型
        private static string buildEndpointAddress(MB.Util.Model.WcfCredentialInfo credentialInfo, WcfClientInvokeAttribute cfgAtt) {
            string relativePath = cfgAtt.RelativePath;
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
        //创建指定的Wcf 绑定类型。
        private static System.ServiceModel.Channels.Binding createBinding(MB.Util.Model.WcfCredentialInfo credentialInfo, bool isGZipCustomBinding) {
            return createBinding(credentialInfo, isGZipCustomBinding, string.Empty);

        }
        private static System.ServiceModel.Channels.Binding createBinding(MB.Util.Model.WcfCredentialInfo credentialInfo,
                bool isGZipCustomBinding, string usedbindingCfgName)
        {
            System.ServiceModel.Channels.Binding binding = null;
            var cfgInfo = credentialInfo;
            WcfServiceBindingType bindingType = getBindingType(credentialInfo);

            if (bindingType == WcfServiceBindingType.wsHttp)
            {
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
            else if (bindingType == WcfServiceBindingType.netTcp)
            {
                string cfgName = System.Configuration.ConfigurationManager.AppSettings[TCP_BINDING_NAME];
                if (string.IsNullOrEmpty(cfgName))
                    throw new MB.Util.APPException(string.Format("netTcp 绑定需要配置{0}", TCP_BINDING_NAME), MB.Util.APPMessageType.SysErrInfo);

                binding = new System.ServiceModel.NetTcpBinding(cfgName);
            }
            else
            {
                throw new MB.Util.APPException(string.Format("Wcf 客户端绑定目前不支持{0}", bindingType.ToString()), MB.Util.APPMessageType.SysErrInfo);
            }
            return binding;
        }
        //获取对象配置属性
        private static WcfClientInvokeAttribute getWcfInvokeAtt(Type clientRuleType, Type wcfClientType) {
            Attribute[] atts = Attribute.GetCustomAttributes(clientRuleType, typeof(WcfClientInvokeAttribute));
            if (atts == null) {
                // throw new MB.Util.APPException(string.Format("根据客户端业务类{0} 获取调用Wcf 客户端配置信息时出错,请检查对应的业务类是否已经配置了WcfClientInvokeAttribute", clientRuleType.FullName), MB.Util.APPMessageType.SysErrInfo);
                return null;
            }
            foreach (Attribute att in atts) {
                WcfClientInvokeAttribute invokeAtt = att as WcfClientInvokeAttribute;
                if (wcfClientType.Equals(invokeAtt.WcfClientType)) {
                    return invokeAtt;
                }
            }
            return null;
        }
        //创建Wcf 服务端代理
        private static T createProxyInstance<T>( System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress address) {
            Type objType = typeof(T);
            if (objType == null) return default(T);
            object obj = null;
            try {
                object[] pars = new object[] { binding, address };
                obj = objType.Assembly.CreateInstance(objType.FullName, true, System.Reflection.BindingFlags.CreateInstance, null, pars, null, null);
                
                if (obj == null)
                    throw new MB.Util.APPException(string.Format("根据类型:{0} 创建实例有误！", objType));

                return (T)obj;
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(string.Format("根据类型:{0}创建实例有误！", objType), APPMessageType.SysErrInfo, ex);
            }
            
        }
        //得到服务配置信息
        private static MB.Util.Model.WcfCredentialInfo createClientConfigInfo(MB.Util.Model.ServerConfigInfo serverInfo) {
            if (serverInfo.Credential.IndexOf(':') >= 0) {
                MB.Util.Model.WcfCredentialInfo newInfo = new MB.Util.Model.WcfCredentialInfo();
                newInfo.BaseAddress = serverInfo.Credential;

                newInfo.HostType = MB.Util.Model.WcfServiceHostType.DEVELOPER;
                return newInfo;
            }
            else {

                string creName = serverInfo.Credential;
                string fileFullName = AppDomain.CurrentDomain.BaseDirectory + serverInfo.Credential;
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
        /// <summary>
        /// 根据证书中的appendDetail和客户端配置的WcfClientInvokeAttribute -> SystemCode
        /// 来解析出关联系统的服务连接
        /// </summary>
        /// <param name="code">客户端的配置System Code</param>
        /// <param name="appendDetail">证书中的appendDetail信息以分号分隔</param>
        /// <returns>关联系统的服务的信息</returns>
        private static WcfCredentialInfo resolveSubSystemCredentialInfo(SystemCode code, string appendDetail, WcfCredentialInfo mainCredentialInfo)
        {
            if (!string.IsNullOrEmpty(appendDetail))
            {
                string[] subServices = appendDetail.Split(';');
                if (subServices.Length > 0)
                {
                    foreach (string subService in subServices)
                    {
                        string[] subServiceAttribute = subService.Split(',');
                        if (subServiceAttribute.Length > 0 && subServiceAttribute.Length == 5)
                        {
                            string systemCode = subServiceAttribute[0].Substring(
                                subServiceAttribute[0].IndexOf("CfgName=") + "CfgName=".Length);

                            if (string.Compare(code.ToString(), systemCode, true) == 0)
                            {
                                WcfCredentialInfo subCredetialInfo = MB.Util.MyReflection.Instance.FillModelObject<WcfCredentialInfo>(mainCredentialInfo);
                                string uri = subServiceAttribute[1].Substring("URL=".Length);
                                subCredetialInfo.EndpointFormatString = uri;
                                subCredetialInfo.BaseAddress = uri.Substring(0, uri.Length - "{0}.svc".Length - 1);
                                subCredetialInfo.Domain = subServiceAttribute[2].Substring("Domain=".Length);
                                subCredetialInfo.UserName = subServiceAttribute[3].Substring("LoginName=".Length);
                                subCredetialInfo.Password = subServiceAttribute[4].Substring("LoginPassword=".Length);
                                return subCredetialInfo;
                            }
                        }
                    }
                }
            }

            return null;
        }



        #endregion

    }


}



