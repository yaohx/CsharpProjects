using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using MB.WcfServiceLocator.ClientChannel;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace MB.WcfServiceLocator.Test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string serviceCredential = System.Configuration.ConfigurationManager.AppSettings["WcfServers"];
            bool hasSubSystem = serviceCredential.IndexOf("nosubsystem") < 0;
            if (hasSubSystem)
            {
                ChannelFactoryTestor test = new ChannelFactoryTestor();
                test.InvokeWcfViaProxy();
                test.InvokeWcfAction();
                test.InvokeWcfFunction();

                TestMB001Rule rule = new TestMB001Rule();
                BfEmployeeClient client = rule.InvokeWcf();
                Debug.Assert(client.Endpoint.Address.Uri.ToString() == @"http://192.168.0.1:1/MB.ERP.BaseLibrary.MPrivilege.IFace/IBfEmployee.svc");
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.Domain == "MB001");
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.UserName == "administrator1");
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.Password == "password1");

                TestMB002Rule rule002 = new TestMB002Rule();
                client = rule002.InvokeWcf();
                Debug.Assert(client.Endpoint.Address.Uri.ToString() == @"http://192.168.0.1:2/MB.ERP.BaseLibrary.MPrivilege.IFace/IBfEmployee.svc");
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.Domain == "MB002");
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.UserName == "administrator2");
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.Password == "password2");


                TestMainRule mainRule = new TestMainRule();
                client = mainRule.InvokeWcf();
                Debug.Assert(client.Endpoint.Address.Uri.ToString() == @"http://192.168.0.1:18051/MB.ERP.BaseLibrary.MPrivilege.IFace/IBfEmployee.svc");
                Debug.Assert(string.IsNullOrEmpty(client.ClientCredentials.Windows.ClientCredential.Domain));
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.UserName == "administrator");
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.Password == "password");

                TestMainRuleWithoutAtt mainRuleWithoutAtt = new TestMainRuleWithoutAtt();
                client = mainRuleWithoutAtt.InvokeWcf();
                Debug.Assert(client.Endpoint.Address.Uri.ToString() == @"http://192.168.0.1:18051/Service1.svc");
                Debug.Assert(string.IsNullOrEmpty(client.ClientCredentials.Windows.ClientCredential.Domain));
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.UserName == "administrator");
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.Password == "password");

                TestCreateWcfClientWithoutAtt noAtt = new TestCreateWcfClientWithoutAtt();
                client = noAtt.InvokeWcf();
                Debug.Assert(client.Endpoint.Address.Uri.ToString() == @"http://192.168.0.1:18051/IBfEmployee.svc");
                Debug.Assert(string.IsNullOrEmpty(client.ClientCredentials.Windows.ClientCredential.Domain));
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.UserName == "administrator");
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.Password == "password");



            }
            else
            {
                BfEmployeeClient client = null;
                if (serviceCredential.IndexOf("mb001") > 0)
                {
                    TestMB001Rule rule = new TestMB001Rule();
                    client = rule.InvokeWcf();
                    Debug.Assert(client.Endpoint.Address.Uri.ToString() == @"http://192.168.0.1:1/MB.ERP.BaseLibrary.MPrivilege.IFace/IBfEmployee.svc");
                    Debug.Assert(client.ClientCredentials.Windows.ClientCredential.Domain == "MB001");
                    Debug.Assert(client.ClientCredentials.Windows.ClientCredential.UserName == "administrator1");
                    Debug.Assert(client.ClientCredentials.Windows.ClientCredential.Password == "password1");
                }
                else
                {
                    TestMB001Rule rule = new TestMB001Rule();
                    client = rule.InvokeWcf();
                    Debug.Assert(client.Endpoint.Address.Uri.ToString() == @"http://192.168.0.1:18051/MB.ERP.BaseLibrary.MPrivilege.IFace/IBfEmployee.svc");
                    Debug.Assert(string.IsNullOrEmpty(client.ClientCredentials.Windows.ClientCredential.Domain));
                    Debug.Assert(client.ClientCredentials.Windows.ClientCredential.UserName == "administrator");
                    Debug.Assert(client.ClientCredentials.Windows.ClientCredential.Password == "password");
                }

                TestMB002Rule rule002 = new TestMB002Rule();
                client = rule002.InvokeWcf();
                Debug.Assert(client.Endpoint.Address.Uri.ToString() == @"http://192.168.0.1:18051/MB.ERP.BaseLibrary.MPrivilege.IFace/IBfEmployee.svc");
                Debug.Assert(string.IsNullOrEmpty(client.ClientCredentials.Windows.ClientCredential.Domain));
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.UserName == "administrator");
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.Password == "password");


                TestMainRule mainRule = new TestMainRule();
                client = mainRule.InvokeWcf();
                Debug.Assert(client.Endpoint.Address.Uri.ToString() == @"http://192.168.0.1:18051/MB.ERP.BaseLibrary.MPrivilege.IFace/IBfEmployee.svc");
                Debug.Assert(string.IsNullOrEmpty(client.ClientCredentials.Windows.ClientCredential.Domain));
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.UserName == "administrator");
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.Password == "password");

                TestMainRuleWithoutAtt mainRuleWithoutAtt = new TestMainRuleWithoutAtt();
                client = mainRuleWithoutAtt.InvokeWcf();
                Debug.Assert(client.Endpoint.Address.Uri.ToString() == @"http://192.168.0.1:18051/Service1.svc");
                Debug.Assert(string.IsNullOrEmpty(client.ClientCredentials.Windows.ClientCredential.Domain));
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.UserName == "administrator");
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.Password == "password");

                TestCreateWcfClientWithoutAtt noAtt = new TestCreateWcfClientWithoutAtt();
                client = noAtt.InvokeWcf();
                Debug.Assert(client.Endpoint.Address.Uri.ToString() == @"http://192.168.0.1:18051/IBfEmployee.svc");
                Debug.Assert(string.IsNullOrEmpty(client.ClientCredentials.Windows.ClientCredential.Domain));
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.UserName == "administrator");
                Debug.Assert(client.ClientCredentials.Windows.ClientCredential.Password == "password");
            }

            BfEmployeeClient clientBinding = null;
            TestIndicateBindingConfigWithOutReplaceLastDot bindingTest = new TestIndicateBindingConfigWithOutReplaceLastDot();
            clientBinding = bindingTest.InvokeWcf();
            Debug.Assert(clientBinding.Endpoint.Address.Uri.ToString() == @"http://192.168.0.1:18051/MB.InfoCloud.Common.Hosts/OnlineSales.11.22.33/IBillDayCheckout.svc");
            Debug.Assert(string.IsNullOrEmpty(clientBinding.ClientCredentials.Windows.ClientCredential.Domain));
            Debug.Assert(clientBinding.ClientCredentials.Windows.ClientCredential.UserName == "administrator");
            Debug.Assert(clientBinding.ClientCredentials.Windows.ClientCredential.Password == "password");
            Debug.Assert(clientBinding.Endpoint.Binding.CloseTimeout == new TimeSpan(0,15,0));
            
            Console.WriteLine("OK");
            Console.ReadLine();
        }
    }




    [MB.WcfClient.WcfClientInvoke(typeof(BfEmployeeClient), "MB.ERP.BaseLibrary.MPrivilege.IFace.IBfEmployee")]
    public class TestMainRule
    {
        public BfEmployeeClient InvokeWcf()
        {
            BfEmployeeClient proxy = MB.WcfClient.WcfClientFactory.CreateWcfClient<BfEmployeeClient>(this);
            return proxy;
        }
    }

    #region 测试生成客户端
    [MB.WcfClient.WcfClientInvoke(typeof(BfEmployeeClient), "MB.ERP.BaseLibrary.MPrivilege.IFace.IBfEmployee", MB.WcfClient.SystemCode.MB001)]
    public class TestMB001Rule
    {
        public BfEmployeeClient InvokeWcf()
        {
            BfEmployeeClient proxy = MB.WcfClient.WcfClientFactory.CreateWcfClient<BfEmployeeClient>(this);
            return proxy;
        }
    }

    [MB.WcfClient.WcfClientInvoke(typeof(BfEmployeeClient), "MB.ERP.BaseLibrary.MPrivilege.IFace.IBfEmployee", MB.WcfClient.SystemCode.MB002)]
    public class TestMB002Rule
    {
        public BfEmployeeClient InvokeWcf()
        {
            BfEmployeeClient proxy = MB.WcfClient.WcfClientFactory.CreateWcfClient<BfEmployeeClient>(this);
            return proxy;
        }
    }


    public class TestMainRuleWithoutAtt
    {
        public BfEmployeeClient InvokeWcf()
        {
            BfEmployeeClient client = new BfEmployeeClient();

            bool hasSubSystem = System.Configuration.ConfigurationManager.AppSettings["WcfServers"].IndexOf("nosubsystem") < 0;
            string certificateName = "mb.credential";
            if (!hasSubSystem)
                certificateName = "mb.credential.nosubsystem";

            MB.Util.MyNetworkCredential.CurrentSelectedServerInfo = new MB.Util.Model.ServerConfigInfo("Server", certificateName);
            BfEmployeeClient proxy = MB.WcfClient.WcfClientFactory.CreateWcfClient<BfEmployeeClient>(this);
            return proxy;
        }
        
    }

    public class TestCreateWcfClientWithoutAtt
    {
        public BfEmployeeClient InvokeWcf()
        {
            //MB.ERP.BaseLibrary.MPrivilege.IFace.IBfEmployee
            BfEmployeeClient proxy = MB.WcfClient.WcfClientFactory.CreateWcfClient<BfEmployeeClient>(
                "IBfEmployee", MB.WcfClient.SystemCode.Main);
            return proxy;
        }

    }


    public class TestIndicateBindingConfigWithOutReplaceLastDot
    {
        public BfEmployeeClient InvokeWcf()
        {
            BfEmployeeClient proxy = MB.WcfClient.WcfClientFactory.CreateWcfClient<BfEmployeeClient>(
                "MB.InfoCloud.Common.Hosts/OnlineSales.11.22.33/IBillDayCheckout", MB.WcfClient.SystemCode.Main, "MyBinding");
            return proxy;
        }

    }


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "BfEmployeeServer.IBfEmployee")]
    public interface IBfEmployee
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IBaseRule/Flush", ReplyAction = "http://tempuri.org/IBaseRule/FlushResponse")]
        int Flush();
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBfEmployeeChannel : IBfEmployee, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BfEmployeeClient : System.ServiceModel.ClientBase<IBfEmployee>, IBfEmployee
    {

        public BfEmployeeClient()
        {
        }

        public BfEmployeeClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public BfEmployeeClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public BfEmployeeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public BfEmployeeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

       

        public int Flush()
        {
            return base.Channel.Flush();
        }

        
    }
#endregion

    #region 测试channel factory生成WCF客户端

    public class ChannelFactoryTestor {

        public void InvokeWcfViaProxy() {
            try {
                using (WcfClientProxyScope<IService1> scope =
                    WcfClientChannelFactory.CreateWcfClientProxy<IService1>()) {
                    IService1 proxy = scope.ClientProxy; ;
                    Console.WriteLine(proxy.GetData(1));
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        public void InvokeWcfAction() {
            WcfClientChannelFactory.InvokeWcfMethod<IService1>(t => {
                Console.WriteLine(t.GetData(2));
            });
        }

        public void InvokeWcfFunction() {
            var result = WcfClientChannelFactory.InvokeWcfMethod<IService1, CompositeType>(t => {
                CompositeType ct = new CompositeType();
                ct.BoolValue = true;
                ct.StringValue = "Hello World";
                return t.GetDataUsingDataContract(ct);
            });
            Console.WriteLine(result.StringValue);

        }


    }

    [ServiceContract]
    [WcfService("Service1", "localhost55442.credential")]
    public interface IService1 {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
    }

    [System.Runtime.Serialization.DataContractAttribute(Name = "CompositeType", Namespace = "http://schemas.datacontract.org/2004/07/MyTestWCFServices")]
    public class CompositeType {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    #endregion
}

