using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.EAI.SOA.BANGGO.Entities;

namespace MB.EAI.SOA.BANGGO.Common
{
    public class UtilEx
    {
        public static ReturnMSG TryCatch<T>(Func<T> fuc) where T:ReturnMSG,new()
        {
            ReturnMSG response = new ReturnMSG();
            try
            {
                response = fuc();

            }
            catch (Exception ex)
            {
                response.isok = false;
                response.Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
            }
            return response;
        }
    }
}
