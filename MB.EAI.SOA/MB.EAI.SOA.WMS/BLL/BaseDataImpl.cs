using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.EAI.SOA.WMS.IFace;

namespace MB.EAI.SOA.WMS.BLL
{
    public class BaseDataImpl:IBaseData
    {
        bool IBaseData.GenProd(List<Entities.InptItemWhseMasterInfo> lsProds)
        {
            throw new NotImplementedException();
        }
    }
}
