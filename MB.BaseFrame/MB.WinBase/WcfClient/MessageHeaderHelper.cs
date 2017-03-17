using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

using MB.Util.Model;
using MB.Util.Serializer;
using MB.BaseFrame;
namespace MB.WinBase.WcfClient {
    /// <summary>
    /// WCF 消息头处理相关。
    /// </summary>
    public class MessageHeaderHelper {


        /// <summary>
        /// 追加当前登录用户的信息到消息头上。
        /// </summary>
        public static void AppendUserLoginInfo() {

            if (AppEnvironmentSetting.Instance.CurrentLoginUserInfo == null) return;

            EntityXmlSerializer<SysLoginUserInfo> se = new EntityXmlSerializer<SysLoginUserInfo>();
            string dstr = se.SingleSerializer(AppEnvironmentSetting.Instance.CurrentLoginUserInfo, string.Empty);
            AppendMessageHeader(SOD.CURRENT_USER_IDENTITY, dstr);
        }

        /// <summary>
        /// 追加当前查询的行为信息
        /// </summary>
        /// <param name="queryBehavior"></param>
        public static void AppendQueryBehavior(QueryBehavior queryBehavior, string messageKey)
        {
            EntityXmlSerializer<QueryBehavior> se = new EntityXmlSerializer<QueryBehavior>();
            string dstr = se.SingleSerializer(queryBehavior, string.Empty);
            string queryBehavuorMessageHeader = SOD.QUERY_BEHAVIOR_MESSAGE_HEADER;
            if (!string.IsNullOrEmpty(messageKey))
                queryBehavuorMessageHeader = queryBehavuorMessageHeader + "_" + messageKey;

            AppendMessageHeader(queryBehavuorMessageHeader, dstr);

        }

        /// <summary>
        /// 追加当前查询的行为信息
        /// </summary>
        /// <param name="queryBehavior"></param>
        public static void AppendQueryBehavior(QueryBehavior queryBehavior) {
            AppendQueryBehavior(queryBehavior, string.Empty);
           
        }
        /// <summary>
        /// 在当前通道的消息头同追加信息
        /// </summary>
        /// <param name="headerKey"></param>
        /// <param name="context"></param>
        public static void AppendMessageHeader(string headerKey, string context) {
            MessageHeader<string> mh = new MessageHeader<string>(context);
            MessageHeader header = mh.GetUntypedHeader(headerKey, SOD.MESSAGE_HEADER_NAME_SPACE);

            int re = OperationContext.Current.OutgoingMessageHeaders.FindHeader(headerKey, SOD.MESSAGE_HEADER_NAME_SPACE);
            if (re < 0)
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
        }




        #region 从返回的消息头中取得消息头信息

        /// <summary>
        /// 获取当前查询的ResponseInfo
        /// </summary>
        /// <returns></returns>
        public static ResponseHeaderInfo GetMessageHeaderResponseInfo()
        {
            string ctxt = string.Empty;
            if (OperationContext.Current != null)
            {
                try
                {
                    ctxt = GetMessageHeaderContext(SOD.QUERY_RESPONSE_INFO);
                    if (!string.IsNullOrEmpty(ctxt))
                    {
                        EntityXmlSerializer<ResponseHeaderInfo> ser = new EntityXmlSerializer<ResponseHeaderInfo>();
                        return ser.SingleDeSerializer(ctxt, string.Empty);
                    }
                }
                catch (Exception ex)
                {
                    MB.Util.TraceEx.Write("在获取当前查询的ResponseInfo 出错" + ex.Message, Util.APPMessageType.SysWarning);
                }
            }
            return ResponseHeaderInfo.DefaultResponseInfo;
        }


        /// <summary>
        /// 在当前通道中获取指定消息头的信息
        /// </summary>
        /// <param name="headerKey"></param>
        /// <returns></returns>
        public static string GetMessageHeaderContext(string headerKey)
        {
            if (OperationContext.Current == null) return null;
            string ctxt = string.Empty;
            var re = OperationContext.Current.IncomingMessageHeaders.FindHeader(headerKey, SOD.MESSAGE_HEADER_NAME_SPACE);
            if (re >= 0)
            {
                ctxt = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(headerKey, SOD.MESSAGE_HEADER_NAME_SPACE);
            }
            return ctxt;

        }

        #endregion 
    }
}
