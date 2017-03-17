using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using MB.Util.Model;
using System.ServiceModel;
using MB.BaseFrame;
using MB.Util.Serializer;
using System.ServiceModel.Channels;

namespace MB.WcfServiceLocator
{
    public class QueryBehaviorMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            string ctxt = string.Empty;
            var re = reply.Headers.FindHeader(SOD.QUERY_RESPONSE_INFO, SOD.MESSAGE_HEADER_NAME_SPACE);
            if (re >= 0)
            {
                ctxt = reply.Headers.GetHeader<string>(SOD.QUERY_RESPONSE_INFO, SOD.MESSAGE_HEADER_NAME_SPACE);
                EntityXmlSerializer<ResponseHeaderInfo> se = new EntityXmlSerializer<ResponseHeaderInfo>();
                ResponseHeaderInfo responseHeaderInfo = se.SingleDeSerializer(ctxt, string.Empty);
                QueryBehaviorScope.ResponseInfo = responseHeaderInfo;
            }
            else
                QueryBehaviorScope.ResponseInfo = null;
            
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            QueryBehavior queryBehavior = QueryBehaviorScope.CurQueryBehavior;
            if (queryBehavior != null)
            {
                EntityXmlSerializer<QueryBehavior> se = new EntityXmlSerializer<QueryBehavior>();
                string dstr = se.SingleSerializer(queryBehavior, string.Empty);
                MessageHeader<string> mh = new MessageHeader<string>(dstr);
                string name = SOD.QUERY_BEHAVIOR_MESSAGE_HEADER;
                if (!string.IsNullOrEmpty(QueryBehaviorScope.MessageHeaderKey)) name += "_" + QueryBehaviorScope.MessageHeaderKey;
                MessageHeader header = mh.GetUntypedHeader(name, SOD.MESSAGE_HEADER_NAME_SPACE);

                int ret = request.Headers.FindHeader(name, SOD.MESSAGE_HEADER_NAME_SPACE);
                if(ret < 0) request.Headers.Add(header);
            }
            return null;
        }
    }
}
