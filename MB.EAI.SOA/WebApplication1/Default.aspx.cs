using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.ServiceModel;
using MB.SOA.Web.Entities;
using MB.SOA.Web.Bills;
using MB.SOA.Web.BtsService;


namespace MB.SOA.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //StreamReader sr = new StreamReader(Request.InputStream);
            //StreamWriter sw = new StreamWriter(string.Format("d:\\Request\\{0}.txt", DateTime.Now.ToString("yyyy-MM-ddHH-mm-ss-fff")));

            //sw.WriteLine(sr.ReadToEnd());
            //sw.Close();
            //sr.Close();
            StreamHelper streamHelper = new StreamHelper();
            List<OrdersOrder> lsOrders = new List<OrdersOrder>();
            try
            {
                if (Request.InputStream == null||Request.InputStream.Length==0)
                {
                    MB.Util.TraceEx.Write("MA没有传送任何信息");
                }
                else
                {
                    InvoiceCollection lsmaInvoice = streamHelper.DealFor<InvoiceCollection>(Request.InputStream);
                    lsOrders = CollectData.GetERPGdnFromMaPkt(lsmaInvoice);

                    BtsService.BtsMAServerClient client = new BtsService.BtsMAServerClient();
                    MB.Util.TraceEx.Write("YHX Test1");
                    client.MA_PKT(lsOrders.ToArray());

                }

            }
            catch (Exception ex)
            {
               MB.Util.TraceEx.Write(ex.Message);
                

            }
            finally
            {
                using (StreamWriter sw = new StreamWriter(Response.OutputStream))
                {
                    sw.WriteLine("This is My Response");
                    sw.Flush();
                    sw.Close();
                }
            }
        }
    }
}
