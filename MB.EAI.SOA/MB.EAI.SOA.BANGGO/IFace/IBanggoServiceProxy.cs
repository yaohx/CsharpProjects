using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MB.EAI.SOA.BANGGO.Entities;

namespace MB.EAI.SOA.BANGGO.IFace
{
     [ServiceContract(Namespace = "http://MB.EAI.SOA.BANGGO/IBanggoServiceProxy")]
    public interface IBanggoServiceProxy
    {
         [OperationContract]
         ReturnMSG SaveProduct(ProductInfo prod);
    }
}
