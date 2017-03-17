using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace MB.EAI.SOA.WMS.DAL
{
    public class RunningUtil
    {
        public void TryCatch(Action<string, string> action,string arg1,string arg2)
        {
            try
            {
                action(arg1, arg2);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }           
        }
    }
}
