using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.EAI.SOA.OERP.IFace;
using MB.EAI.SOA.OERP.BasicData;
using Newtonsoft.Json;

namespace MB.EAI.SOA.OERP.Impl
{
    public class OrganizSerivce : IOrganizeService
    {
         
        public string GetShopsInfo()
        {
            var lswaperShops = ShopService.Instance.GetShopsInfo();
            string result = JsonConvert.SerializeObject(lswaperShops.Responseparams[0]);
            return result;
        }
    }
}
