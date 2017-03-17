using System;
using System.Xml.Serialization;
using MB.SOA.Web.Bills;
using WebApplication1.Entities;

using System.IO;
using System.Collections.Generic;
 
using System.Net;
using System.Text;
using MB.SOA.Web.Entities;
using System.Diagnostics;
using Newtonsoft.Json;
using MB.EAI.SOA.BANGGO.Entities;
using MB.EAI.SOA.BANGGO.ImplServices;
using MB.EAI.SOA.WCFHost;
 

namespace ConsoleApplication1
{ 
    public class A
        {
            public string ID;
            public string Name;
            public string Desc;
        }
    class Program
    {

       
        static void Main(string[] args)
        {



            OrgBasicInfoUpdate();


            //string ss = "{\"brandCode\":\"MB\",\"goodsName\":\"男一款多花圆领短袖恤\",\"goodsSn\":\"110496\",\"goodsWeight\":20,\"marketPrice\":69,\"opType\":\"c\",\"seasonCode\":\"0010\",\"skuVos\":[{\"barcode\":\"6907303082662\",\"colorCode\":\"01\",\"colorName\":\"漂白\",\"custumCode\":\"11049601047\",\"goodsSn\":\"110496\",\"opFrom\":\"erp1\",\"opType\":\"c\",\"sizeCode\":\"11047\",\"sizeName\":\"165/95A \"},{\"barcode\":\"6907303082679\",\"colorCode\":\"01\",\"colorName\":\"漂白\",\"custumCode\":\"11049601049\",\"goodsSn\":\"110496\",\"opFrom\":\"erp1\",\"opType\":\"c\",\"sizeCode\":\"11049\",\"sizeName\":\"1170/100A\"}],\"user\":\"erp1\"}";
            ////var aa = JsonConvert.DeserializeObject<ProductInfo>(ss);

            ////var response=new BangggoServiceProxy().SaveProduct(aa);
            //var aa = JsonConvert.DeserializeObject<ServiceReference1.ProductInfo>(ss);
            //ServiceReference1.BanggoServiceProxyClient client = new ServiceReference1.BanggoServiceProxyClient();


            //var response = client.SaveProduct(aa);

            //Console.WriteLine(Process.GetCurrentProcess().MainModule.FileName);
            //Console.WriteLine(Environment.CurrentDirectory);
            //Console.WriteLine(Directory.GetCurrentDirectory());
            //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            //Console.WriteLine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);

            
            
          //  InvoiceInfo invoice = new InvoiceInfo();
          //  invoice.OrderHeader = new OutPKTHdr();
          //  invoice.OrderHeader.Addr_Code = "abcdefg";
          //  invoice.LsOrderDtl = new List<OutPKTDtl>();
          //  invoice.LsBoxHeader = new List<OutCatonHdr>();
          //  invoice.LsBoxDtl = new List<OutCartonDtl>();


          ////  List<InvoiceInfo> lsInvoices = new List<InvoiceInfo>() { invoice };
          //  InvoiceCollection lsInvoices = new InvoiceCollection();
          //  lsInvoices.lsInvoice = new List<InvoiceInfo>();
          //  lsInvoices.lsInvoice.Add(invoice);
           // XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<InvoiceInfo>), new XmlRootAttribute("invoice_1_0"));
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(InvoiceCollection));

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            
            try
            {
                XmlSerializer xmlSerializer1 = new XmlSerializer(typeof(A));
                Stream stream = new FileStream("d:\\Invoice_real.xml", FileMode.Open);
               // xmlSerializer1.Serialize(stream, new A() { ID = "001", Name = null, Desc = "" }, ns);


                var t = xmlSerializer1.Deserialize(stream) as A;

                stream.Close();

                //invoice = xmlSerializer.Deserialize(stream) as InvoiceInfo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            try
            {
                Stream stream1 = new FileStream("d:\\Invoice.xml", FileMode.OpenOrCreate);
                var t = xmlSerializer.Deserialize(stream1) as InvoiceCollection;
                var t2=CollectData.GetERPGdnFromMaPkt(t);



                HttpWebRequest webRequest = System.Net.WebRequest.Create("http://localhost:5821/Default.aspx") as HttpWebRequest;
                webRequest.Method = "POST";
                var bytes = ASCIIEncoding.UTF8.GetBytes(new StreamReader(stream1).ReadToEnd());
                
                
                webRequest.ContentLength = bytes.Length;
                webRequest.ContentType = "text/xml";


                //webRequest.ServicePoint.Expect100Continue = false;
                //webRequest.Timeout = 1000 * 60;
                //webRequest.ContentType = "application/x-www-form-urlencoded";
                var requestWriter = webRequest.GetRequestStream();
                requestWriter.Write(bytes,0,bytes.Length);
               
                
                var responseData = webRequest.GetResponse(); 
                //WebClient client = new WebClient();
                //string str = new StreamReader(stream1).ReadToEnd();
                //byte[] bytes = ASCIIEncoding.ASCII.GetBytes(str);

                //var t3 = client.UploadData("http://localhost:5821/Default.aspx", "POST", bytes);
                
            }
            catch (Exception ex)
            {

                throw ex;
            }



            #region testcancel
            //#region HttpListener
            //using (HttpListener listerner = new HttpListener())
            //{
            //    listerner.AuthenticationSchemes = AuthenticationSchemes.Anonymous;//指定身份验证 Anonymous匿名访问
            //    listerner.Prefixes.Add("http://192.168.156.91:8080/web/");

            //    // listerner.Prefixes.Add("http:// 192.168.156.91/web/");
            //    listerner.Start();
            //    Console.WriteLine("WebServer Start Successed.......");
            //    while (true)
            //    {
            //        //等待请求连接
            //        //没有请求则GetContext处于阻塞状态
            //        HttpListenerContext ctx = listerner.GetContext();
            //        ctx.Response.StatusCode = 200;//设置返回给客服端http状态代码
            //        string name = ctx.Request.QueryString["name"];

            //        if (name != null)
            //        {
            //            Console.WriteLine(name);
            //        }

            //        //使用Writer输出http响应代码
            //        using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream))
            //        {
            //            Console.WriteLine("hello");
            //            writer.WriteLine("<html><head><title>The WebServer Test</title></head><body>");
            //            writer.WriteLine("<div style=\"height:20px;color:blue;text-align:center;\"><p> hello {0}</p></div>", name);
            //            writer.WriteLine("<ul>");

            //            foreach (string header in ctx.Request.Headers.Keys)
            //            {
            //                writer.WriteLine("<li><b>{0}:</b>{1}</li>", header, ctx.Request.Headers[header]);
            //            }
            //            writer.WriteLine("</ul>");
            //            writer.WriteLine("</body></html>");

            //            writer.Close();
            //            ctx.Response.Close();
            //        }

            //    }
            //    listerner.Stop();
            //}
            //#endregion
            //DbProviderFactory dbfactory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["MB.OldERP"].ProviderName);
            //OracleConnection cn = dbfactory.CreateConnection() as OracleConnection;
            //cn.ConnectionString = ConfigurationManager.ConnectionStrings["MB.OldERP"].ConnectionString;
            //OracleCommand cmd = new OracleCommand(
            //    //"select t.unit_id,t.owner_id,t.hierarchy from sys_unit t where unit_id=:unit_id "+
            //    "select t.unit_id,t.owner_id,t.hierarchy from sys_unit t where unit_id=:unit_id", cn);
            ////OracleCommand cmd = new OracleCommand("update sys_unit t set t.owner_id=rtrim(t.owner_id,'T') " +
            ////                        "where unit_id=:unit_id", cn);
            //List<string> lsUnit = new List<string>();
            //for(int i=0;i<10;i++)
            //{
            //    lsUnit.Add("V3009" + i);
            //}
            ////cmd.ArrayBindCount = lsUnit.Count;
            //OracleParameter param = new OracleParameter("unit_id", OracleDbType.Varchar2);
            //param.Direction = ParameterDirection.Input;
            ////param.Value = lsUnit.ToArray();
            //cmd.Parameters.Add(param);
            //cn.Open();
            //DataTable dt = new DataTable();
            //for (int i = 0; i < 1000; i++)
            //{
            //    cmd.Parameters[0].Value = "V30" + i;
            //    var datareader = cmd.ExecuteReader();
            //     var columncount = datareader.FieldCount;
            //     while (datareader.Read())
            //    {
            //        for (int j = 0; j < columncount; j++)
            //            Console.Write(datareader[j] + "|");
            //        Console.WriteLine();

            //    }
            //     datareader.Close();
            //         OracleDataAdapter da = new OracleDataAdapter(cmd);
            //         da.Fill(dt);
            //}
            //do
            //{
            //    while (datareader.Read())
            //    {
            //        for (int i = 0; i < columncount; i++)
            //            Console.Write(datareader[i] + "|");
            //        Console.WriteLine();

            //    }
            //} while (datareader.NextResult());
            //cmd.ExecuteNonQuery();


            //cn.Close();

            //List<A> lsA = new List<A>();
            //for (int i = 0; i < 10; i++)
            //{


            //    if(i>5)
            //        lsA.Add(new A() { ID = (i - 5).ToString(), Name = "N" + (i - 5), Desc = "D" + (i - 5) });
            //    else
            //        lsA.Add(new A() { ID = i.ToString(), Name = "N" + i, Desc = "D" + i });
            //}

            //var t = lsA.GroupBy(o => o.ID).Select(o => new { id = o.Key, Count = o.Count() });

            ////var t=new SFGDN().GetDistroDeliveredNote();
            //var t1 = t.ToList().FindIndex(o => o.id == "6");
            //var t2 = t.ToList().FindIndex(2,o => o.id == "1");





            //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            //client.Open();
            //int i = 0;
            //do
            //{
            //    i++;
            //    //if (i % 2 != 0)
            //    //{
            //    //    ServiceReference2.Service1Client client2 = new ServiceReference2.Service1Client();
            //    //    Console.WriteLine("You Input {0} and Get {1}",i,client2.GetData(i));
            //    //}
            //    var request = new ServiceReference1.ORDER() { ID = i.ToString(), Name = i.ToString() };
            //    client.BeginGetMSG(request, delegate(IAsyncResult ar)
            //    {

            //        var t = client.EndGetMSG(ar);
            //        Console.WriteLine("{3}- {4}\tResponse: ID:{0}Name:{1} Str:{2}", t.ID, t.Name, t.Str, ar.AsyncState, ar.IsCompleted);

            //    }, i);
            //    // var t = client.GetMSG(request);
            //    //client.GetRequest(new ServiceReference1.Root() { ID = i.ToString(), Name = i.ToString() });
            //    //   Console.Write("{0} :Call finish", i);
            //    //Console.WriteLine("Response: ID:{0}Name:{1} Str:{2}",t.ID,t.Name,t.Str);

            //} while (i < 100);

            #endregion
            Console.WriteLine("Test Over...");
            Console.ReadLine();

        }

        public static void OrgBasicInfoUpdate()
        {
            List<OrgBasicInfo> lsOrgInfo = new List<OrgBasicInfo>() { new OrgBasicInfo() { Org_Code="HQ01S001",Org_Name="TEST", Parent_Code="HQ01", Org_Type="SHOP", Status="1" } };


            new YouFan().OrgBasicInfoUpdate(lsOrgInfo);



        }
    }
}
