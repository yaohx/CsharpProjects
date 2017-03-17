using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MB.SOA.Client;
using System.Configuration;

namespace MB.EAI.SOA.WCFHost
{
    [DataContract]
    public class OrgBasicInfo
    {
        [DataMember]
        public string Org_Code { get; set; }
        [DataMember]
        public string Org_Name { get; set; }
        [DataMember]
        public string Parent_Code { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Org_Type { get; set; }
        [DataMember]
        public string Centralized_Flag { get; set; }
        [DataMember]
        public string Center_Flag { get; set; }

    }
    [DataContract]
    public class ResponseMsg
    {
        [DataMember]
        public string isSuccess { get; set; }
        [DataMember]
        public string message { get; set; }
    }
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“MB”。
    public class YouFan : IYouFan
    {
        public void OrgBasicInfoUpdate(List<OrgBasicInfo> lsOrgs)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            try
            {
                CallCenterProxy client = CallCenterProxy.GetInstance(ConfigurationManager.AppSettings["mbsoa"]);
             
                var tmp = new { OrgBasicInfoList = lsOrgs };
                string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(tmp);
                string res = client.Invoke("WXSC_Collocation", "OrgBasicInfoUpdate", jsonStr);

                responseMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseMsg>(res);
                MB.Util.TraceEx.Write(res);
            }
            catch (Exception ex)
            {
                responseMsg = new ResponseMsg() { isSuccess = "", message = ex.Message };
            }
            return ;
        }


    }
}
