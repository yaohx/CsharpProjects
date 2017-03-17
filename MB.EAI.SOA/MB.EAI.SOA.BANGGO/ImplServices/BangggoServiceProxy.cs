using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.EAI.SOA.BANGGO.IFace;
using MB.EAI.SOA.BANGGO.Entities;
using MB.EAI.SOA.BANGGO.Common;

namespace MB.EAI.SOA.BANGGO.ImplServices
{
    public class BangggoServiceProxy:IBanggoServiceProxy
    {

        public ReturnMSG SaveProduct(Entities.ProductInfo prod)
        {
            Log.Current.WriteLog("Begin");
            ReturnMSG response =UtilEx.TryCatch<ReturnMSG>(()=>{
                                      return BangGoServerProxy.SaveProduct(prod);
                                 });
            
            return response;
        }
    }
}
