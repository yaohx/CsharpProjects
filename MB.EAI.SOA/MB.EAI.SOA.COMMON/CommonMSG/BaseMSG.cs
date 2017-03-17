using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.EAI.SOA.COMMON.CommonMSG
{
    [DataContract]
    public class BaseMSG
    {
        private string _HeaderFlag;
        [DataMember]
        public string HeaderFlag
        {
            get { return _HeaderFlag; }
            set { _HeaderFlag = value; }
        }
        private string _HeaderMSG;
        [DataMember]
        public string HeaderMSG
        {
            get { return _HeaderMSG; }
            set { _HeaderMSG = value; }
        }
        public BaseMSG()
        {
            HeaderFlag = "0";
            HeaderMSG = "Not Find Orders";
        }
    }
}
