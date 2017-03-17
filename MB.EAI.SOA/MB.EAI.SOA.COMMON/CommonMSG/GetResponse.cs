using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.EAI.SOA.COMMON.CommonMSG
{
    [DataContract]
    public  class GetResponse<T>:BaseMSG 
    {
       
        private T _Responseparams;
        [DataMember]
        public T Responseparams
        {
            get { return _Responseparams; }
            set { _Responseparams = value; }
        }
        public GetResponse()
        {
             
        }
    }
}
