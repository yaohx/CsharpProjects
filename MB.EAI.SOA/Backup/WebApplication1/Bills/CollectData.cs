using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MB.SOA.Web.Entities;
using MB.SOA.Web.BtsService;

namespace MB.SOA.Web.Bills
{
    public class CollectData
    {
        public static List<OrdersOrder> GetERPGdnFromMaPkt(InvoiceCollection lspkt)
        {
            //BtsService.MA_PKTRequest ma_PKTRequest = new BtsService.MA_PKTRequest();
            List<OrdersOrder> lsOrdersOrder = new List<OrdersOrder>();
            #region Get list ordersOrder
            foreach (var one in lspkt.lsInvoice)
            {
                string temppkt_ctrl_nbr = "";
                OrdersOrderHeader header = new OrdersOrderHeader();
                if (one.LsOrderDtl != null && one.LsOrderDtl.Count > 0)
                {
                    if (string.IsNullOrEmpty(one.LsOrderDtl[0].Distro_Nbr))
                    {
                        header.Wif_Num = one.OrderHeader.Pkt_Ctrl_Nbr;
                    }
                    else
                    {
                        lsOrdersOrder.AddRange(GetERPGdnFromMaDistro(one));
                        continue;
                    }
                }
                temppkt_ctrl_nbr = one.OrderHeader.Pkt_Ctrl_Nbr;
                header.Ship_Via = one.OrderHeader.Ship_Via;
                header.Ship_DateTime = one.OrderHeader.Ship_Date_Time;
                header.User_ID = one.OrderHeader.User_Id;

                List<OrdersOrderDetails> lsOrderdtl = new List<OrdersOrderDetails>();

                one.LsOrderDtl.Where(a => a.Pkt_Ctrl_Nbr == temppkt_ctrl_nbr).ToList().ForEach(o =>
                {
                    lsOrderdtl.Add(new OrdersOrderDetails()
                    {
                        Prod_ID = o.Season + o.Style + o.Color + o.Sec_Dim + o.Size_Desc,
                        Allow_Qty = o.Pkt_Qty.ToString(),
                        Delivered_Qty = o.Shpd_Qty.ToString(),
                        Cancel_Qty = o.Cancel_Qty.ToString(),
                        Order_Qty = o.Orig_Pkt_Qty.ToString()
                    });
                });

                List<OrdersOrderBoxHeaders> lsBoxHeader = new List<OrdersOrderBoxHeaders>();

                foreach (var a in one.LsBoxHeader)
                {
                    if (a.Pkt_Ctrl_Nbr == temppkt_ctrl_nbr)
                    {
                        var boxheader = new OrdersOrderBoxHeaders()
                        {
                            Box_Num = a.Carton_Nbr,
                            Box_Size = a.Carton_Size,
                            Box_Type = a.Carton_Type,
                            Box_Vol = a.Carton_Vol.ToString(),
                            Box_WT = a.Actl_Wt.ToString(),
                            Total_Qty = a.Total_Qty.ToString(),
                            Start_Time = a.Create_Date_Time.ToString(),
                            Close_Time = a.Mod_Date_Time.ToString()
                        };
                        List<OrdersOrderBoxHeadersBoxDetails> lsBoxDtl = new List<OrdersOrderBoxHeadersBoxDetails>();
                        one.LsBoxDtl.Where(b => b.Pkt_Ctrl_Nbr == temppkt_ctrl_nbr && b.Carton_Nbr == a.Carton_Nbr).ToList().ForEach(c =>
                        {
                            lsBoxDtl.Add(new OrdersOrderBoxHeadersBoxDetails()
                            {
                                Prod_ID = c.Season + c.Style + c.Color + c.Sec_Dim + c.Size_Desc,
                                Act_Qty = c.Units_Pakd.ToString()
                            });
                        });

                        boxheader.BoxDetails = lsBoxDtl.ToArray();
                        lsBoxHeader.Add(boxheader);
                    }
                    else
                    {
                        continue;
                    }

                }
                lsOrdersOrder.Add(new OrdersOrder()
                {
                    Header = header,
                    Details = lsOrderdtl.ToArray(),
                    BoxHeaders = lsBoxHeader.ToArray()
                });


            }
            #endregion

            //ma_PKTRequest.Orders = lsOrdersOrder.ToArray();

            return lsOrdersOrder;
        }

        public static List<OrdersOrder> GetERPGdnFromMaDistro(InvoiceInfo one)
        {
            List<OrdersOrder> lsOrdersOrder = new List<OrdersOrder>();
            #region Get list ordersOrder By Distro
            var lsWifNums = one.LsOrderDtl.GroupBy(o => o.Distro_Nbr).ToList();
            foreach (var wifnum in lsWifNums)
            {
                OrdersOrderHeader header = new OrdersOrderHeader();
                header.Wif_Num = wifnum.Key;
                header.User_ID = one.OrderHeader.User_Id;
                header.Ship_Via = one.OrderHeader.Ship_Via;
                header.Ship_DateTime = one.OrderHeader.Ship_Date_Time;

                List<OrdersOrderDetails> lsOrderdtl = new List<OrdersOrderDetails>();

                var lsMaDtl=one.LsOrderDtl.Where(a => a.Distro_Nbr == header.Wif_Num).Select(o=>new {o.Distro_Nbr,o.Pkt_Ctrl_Nbr,o.Pkt_Seq_Nbr,
                    o.Pkt_Qty,o.Shpd_Qty,o.Orig_Pkt_Qty,o.Cancel_Qty,Prod_ID=o.Season + o.Style + o.Color + o.Sec_Dim + o.Size_Desc});
                
                lsMaDtl.ToList().ForEach(o =>
                {
                    lsOrderdtl.Add(new OrdersOrderDetails()
                    {
                        Prod_ID = o.Prod_ID,
                        Allow_Qty = o.Pkt_Qty.ToString(),
                        Delivered_Qty = o.Shpd_Qty.ToString(),
                        Cancel_Qty = o.Cancel_Qty.ToString(),
                        Order_Qty = o.Orig_Pkt_Qty.ToString()
                    });
                });
                //Get Prod_ID
                var lsMaBoxDtlFirstVariant = one.LsBoxDtl.Select(o => new
                { 
                    Pkt_Ctrl_Nbr=o.Pkt_Ctrl_Nbr,
                    Pkt_Seq_Nbr=o.Pkt_Seq_Nbr,
                    Box_Num = o.Carton_Nbr,
                    Prod_ID = o.Season + o.Style + o.Color + o.Sec_Dim + o.Size_Desc,    
                    Act_Qty = o.Units_Pakd
                });

                //Get Distro box Details
                var lsMaBoxDtlSecondVarian= lsMaBoxDtlFirstVariant.Where(o => lsMaDtl.Any(a => a.Pkt_Ctrl_Nbr==o.Pkt_Ctrl_Nbr&&a.Pkt_Seq_Nbr==o.Pkt_Seq_Nbr&&a.Prod_ID == o.Prod_ID));
                
                 
                List<OrdersOrderBoxHeaders> lsBoxHeader = new List<OrdersOrderBoxHeaders>();
                //Get Distro box Headers
                var lsMaboxHeaders=one.LsBoxHeader.Where(a=>lsMaBoxDtlSecondVarian.Any(b=>b.Box_Num==a.Carton_Nbr));
               
                lsMaboxHeaders.ToList().ForEach
                (c=>{
                    var boxheader = new OrdersOrderBoxHeaders()
                        {
                            Box_Num = c.Carton_Nbr,
                            Box_Size = c.Carton_Size,
                            Box_Type = c.Carton_Type,
                            Box_Vol = c.Carton_Vol.ToString(),
                            Box_WT = c.Actl_Wt.ToString(),
                            Total_Qty = c.Total_Qty.ToString(),
                            Start_Time = c.Create_Date_Time.ToString(),
                            Close_Time = c.Mod_Date_Time.ToString()
                        };
                    List<OrdersOrderBoxHeadersBoxDetails> lsBoxDtl = new List<OrdersOrderBoxHeadersBoxDetails>();
                    
                    lsMaBoxDtlSecondVarian.Where(a=>a.Box_Num==c.Carton_Nbr).ToList().ForEach(b=>
                        lsBoxDtl.Add(new OrdersOrderBoxHeadersBoxDetails(){Prod_ID=b.Prod_ID,
                            Act_Qty=b.Act_Qty.ToString()}));

                    boxheader.BoxDetails=lsBoxDtl.ToArray();

                    boxheader.Total_Qty=boxheader.BoxDetails.Sum(o=>double.Parse(o.Act_Qty)).ToString();
                    lsBoxHeader.Add(boxheader);
                });

                
                lsOrdersOrder.Add(new OrdersOrder() { Header = header, Details = lsOrderdtl.ToArray(), BoxHeaders = lsBoxHeader.ToArray() });
            }
            #endregion
            return lsOrdersOrder;

        }
    }
}