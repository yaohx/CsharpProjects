using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.EAI.SOA.WMS.Entities;

namespace MB.EAI.SOA.WMS.IFace
{
    [System.ServiceModel.ServiceContract]
    public interface IBaseData
    {
        [System.ServiceModel.OperationContract]
        bool GenProd(List<InptItemWhseMasterInfo> lsProds);
    }
}
