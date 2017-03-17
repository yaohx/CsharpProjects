using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.EAI.SOA.BANGGO.Entities;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using MB.EAI.SOA.BANGGO.Common;

namespace MB.EAI.SOA.BANGGO
{
    public class BangGoServerProxy
    {
        private const string ServerUriName = "BanggoServerUrl";
        public static ReturnMSG SaveProduct(ProductInfo prod)
        {
            string serverUril=ConfigurationManager.AppSettings[ServerUriName];
            WebClient wc = new WebClient();
            wc.BaseAddress = ConfigurationManager.AppSettings[ServerUriName];
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
           

            string jsonStr=JsonConvert.SerializeObject(prod);
            Log.Current.WriteLog(jsonStr);
            byte[] postData = ASCIIEncoding.UTF8.GetBytes(string.Format("productGoodsStr={0}", jsonStr));
 
            byte[] returnData = wc.UploadData(serverUril,"POST", postData);

            string responseJsonStr = ASCIIEncoding.UTF8.GetString(returnData);
            Log.Current.WriteLog(responseJsonStr);

            return JsonConvert.DeserializeObject<ReturnMSG>(responseJsonStr);

        }

        public static void Main()
        {
            //ProductInfo prod = new ProductInfo();
            //prod.User = "ERP";
            //prod.GoodsSn = "12345678901";
            //prod.OpType = "c";

            string ss = "{\"brandCode\":\"MB\",\"goodsName\":\"男一款多花圆领短袖恤\",\"goodsSn\":\"110496\",\"goodsWeight\":20,\"marketPrice\":69,\"opType\":\"c\",\"seasonCode\":\"0010\",\"skuVos\":[{\"barcode\":\"6907303082662\",\"colorCode\":\"01\",\"colorName\":\"漂白\",\"custumCode\":\"11049601047\",\"goodsSn\":\"110496\",\"opFrom\":\"erp1\",\"opType\":\"c\",\"sizeCode\":\"11047\",\"sizeName\":\"165/95A \"},{\"barcode\":\"6907303082679\",\"colorCode\":\"01\",\"colorName\":\"漂白\",\"custumCode\":\"11049601049\",\"goodsSn\":\"110496\",\"opFrom\":\"erp1\",\"opType\":\"c\",\"sizeCode\":\"11049\",\"sizeName\":\"1170/100A\"}],\"user\":\"erp1\"}";
            var aa=JsonConvert.DeserializeObject<ProductInfo>(ss);
            var t = SaveProduct(aa);
        }
    }
}
