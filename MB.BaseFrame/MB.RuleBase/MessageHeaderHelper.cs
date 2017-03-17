using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using MB.Util.Model;
using MB.Util.Serializer;
using MB.BaseFrame;
using System.ServiceModel.Channels;
namespace MB.WcfService
{
    /// <summary>
    /// 获取消息头信息相关。
    /// </summary>
    public class MessageHeaderHelper
    {
        /// <summary>
        /// 获取当前登录的用户信息。
        /// </summary>
        /// <returns></returns>
        public static SysLoginUserInfo GetCurrentLoginUser()
        {
            string ctxt = string.Empty;
            if (OperationContext.Current != null)
            {
                try
                {
                    ctxt = GetMessageHeaderContext(SOD.CURRENT_USER_IDENTITY);
                    if (!string.IsNullOrEmpty(ctxt))
                    {
                        EntityXmlSerializer<SysLoginUserInfo> ser = new EntityXmlSerializer<SysLoginUserInfo>();
                        return ser.SingleDeSerializer(ctxt, string.Empty);
                    }

                }
                catch (Exception ex)
                {
                    MB.Util.TraceEx.Write("在获取当前登录用户信息时出错！" + ex.Message, Util.APPMessageType.SysWarning);
                }
            }
            return null;
        }
        /// <summary>
        /// 获取当前查询的行为信息
        /// </summary>
        /// <returns></returns>
        public static QueryBehavior GetQueryBehavior()
        {
            string ctxt = string.Empty;
            if (OperationContext.Current != null)
            {
                try
                {
                    ctxt = GetMessageHeaderContext(SOD.QUERY_BEHAVIOR_MESSAGE_HEADER);
                    if (!string.IsNullOrEmpty(ctxt))
                    {
                        EntityXmlSerializer<QueryBehavior> ser = new EntityXmlSerializer<QueryBehavior>();
                        return ser.SingleDeSerializer(ctxt, string.Empty);
                    }
                }
                catch (Exception ex)
                {
                    MB.Util.TraceEx.Write("在获取当前用户的查询QueryBehavior 出错" + ex.Message, Util.APPMessageType.SysWarning);
                }
            }
            //没有找到query behavior则返回默认的query behavior
            //发现很多项目在使用过程中，需要直接查出大于2W条的记录，所以更新回原来不做限制的处理
            return null;
        }
        /// <summary>
        /// 获取当前查询对象的行为信息,这个信息永远都是从WCF消息头里面加载出来的
        /// add by aifang 2012-08-13 获取动态列头信息 
        /// </summary>
        /// <returns></returns>
        public static QueryBehavior GetQueryBehavior(string messageHeaderKey)
        {
            string ctxt = string.Empty;
            if (OperationContext.Current != null)
            {
                try
                {
                    string header = SOD.QUERY_BEHAVIOR_MESSAGE_HEADER;
                    if (!string.IsNullOrEmpty(messageHeaderKey)) header += "_" + messageHeaderKey;
                    ctxt = GetMessageHeaderContext(header);
                    if (!string.IsNullOrEmpty(ctxt))
                    {
                        EntityXmlSerializer<QueryBehavior> ser = new EntityXmlSerializer<QueryBehavior>();
                        return ser.SingleDeSerializer(ctxt, string.Empty);
                    }
                }
                catch (Exception ex)
                {
                    MB.Util.TraceEx.Write("在获取当前用户的查询QueryBehavior 出错" + ex.Message, Util.APPMessageType.SysWarning);
                }
            }
            return null;
        }


        /// <summary>
        /// 由于动态列的情况下，某些列并不是从数据库取出来的，
        /// 需要中间层开发人员自己定义哪些列一定要在SQL中而哪些列不能包括在SQL中
        /// 该方法用于重新设定当前WCF上下文中InComingMessage中的消息头QueryBehavior的Columns中的值
        /// </summary>
        /// <returns></returns>
        public static void SetQueryBehaviorColoums(string messageHeaderKey, string columns)
        {
            if (OperationContext.Current == null ||
                    OperationContext.Current.IncomingMessageVersion == MessageVersion.None ||
                    OperationContext.Current.IncomingMessageVersion.Envelope == EnvelopeVersion.None)
                return;

            string headerKey = SOD.QUERY_BEHAVIOR_MESSAGE_HEADER;
            if (!string.IsNullOrEmpty(messageHeaderKey)) headerKey += "_" + messageHeaderKey;

            QueryBehavior queryBehavior = GetQueryBehavior(messageHeaderKey);
            if (queryBehavior != null)
            {
                queryBehavior.Columns = columns;
                EntityXmlSerializer<QueryBehavior> se = new EntityXmlSerializer<QueryBehavior>();
                string dstr = se.SingleSerializer(queryBehavior, string.Empty);

                MessageHeader<string> mh = new MessageHeader<string>(dstr);
                MessageHeader header = mh.GetUntypedHeader(headerKey, SOD.MESSAGE_HEADER_NAME_SPACE);

                int re = OperationContext.Current.IncomingMessageHeaders.FindHeader(headerKey, SOD.MESSAGE_HEADER_NAME_SPACE);
                if (re < 0)
                    OperationContext.Current.IncomingMessageHeaders.Add(header);
                else
                {
                    OperationContext.Current.IncomingMessageHeaders.RemoveAt(re);
                    OperationContext.Current.IncomingMessageHeaders.Add(header);
                }
            }

        }




        /// <summary>
        /// 得到服务端加载的ResponseHeaderInfo
        /// </summary>
        /// <returns></returns>
        public static ResponseHeaderInfo GetResponseHeaderInfo()
        {
            if (OperationContext.Current == null) return null;

            try
            {
                string header = SOD.QUERY_RESPONSE_INFO;
                string ctxt = string.Empty;
                var re = OperationContext.Current.OutgoingMessageHeaders.FindHeader(header, SOD.MESSAGE_HEADER_NAME_SPACE);
                if (re >= 0)
                {
                    ctxt = OperationContext.Current.OutgoingMessageHeaders.GetHeader<string>(header, SOD.MESSAGE_HEADER_NAME_SPACE);
                }
                
                if (!string.IsNullOrEmpty(ctxt))
                {
                    EntityXmlSerializer<ResponseHeaderInfo> ser = new EntityXmlSerializer<ResponseHeaderInfo>();
                    return ser.SingleDeSerializer(ctxt, string.Empty);
                }
            }
            catch (Exception ex)
            {
                MB.Util.TraceEx.Write("在获取当前用户的查询QueryBehavior 出错" + ex.Message, Util.APPMessageType.SysWarning);
            }
            return null;

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



        #region 设定回复的消息头信息

        /// <summary>
        /// 设定查询的总记录数，从消息头
        /// </summary>
        /// <param name="info"></param>
        public static void AppendMessageHeaderResponse(ResponseHeaderInfo info)
        {
            AppendMessageHeaderResponse(info, false);
        }


        /// <summary>
        /// 设定查询的总记录数，从消息头
        /// </summary>
        /// <param name="info"></param>
        /// <param name="isOverwrite"></param>
        public static void AppendMessageHeaderResponse(ResponseHeaderInfo info, bool isOverwrite)
        {
            EntityXmlSerializer<ResponseHeaderInfo> se = new EntityXmlSerializer<ResponseHeaderInfo>();
            string dstr = se.SingleSerializer(info, string.Empty);
            AppendMessageHeader(SOD.QUERY_RESPONSE_INFO, dstr, isOverwrite);
        }

        /// <summary>
        /// 在回复的通道中设定需要回复的消息头信息
        /// </summary>
        /// <param name="headerKey"></param>
        /// <param name="content"></param>
        public static void AppendMessageHeader(string headerKey, string content)
        {
            AppendMessageHeader(headerKey, content, false);
        }

        /// <summary>
        /// 在回复的通道中设定需要回复的消息头信息
        /// </summary>
        /// <param name="headerKey"></param>
        /// <param name="content"></param>
        /// <param name="isOverwrite"></param>
        public static void AppendMessageHeader(string headerKey, string content, bool isOverwrite)
        {
            if (OperationContext.Current == null ||
                OperationContext.Current.IncomingMessageVersion == MessageVersion.None ||
                OperationContext.Current.IncomingMessageVersion.Envelope == EnvelopeVersion.None ||
                OperationContext.Current.OutgoingMessageHeaders.MessageVersion == MessageVersion.None ||
                OperationContext.Current.OutgoingMessageHeaders.MessageVersion.Envelope == EnvelopeVersion.None)
                return;

            MessageHeader<string> mh = new MessageHeader<string>(content);
            MessageHeader header = mh.GetUntypedHeader(headerKey, SOD.MESSAGE_HEADER_NAME_SPACE);

            
            int re = OperationContext.Current.OutgoingMessageHeaders.FindHeader(headerKey, SOD.MESSAGE_HEADER_NAME_SPACE);
            if (re < 0)
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
            else
            {
                if (isOverwrite)
                {
                    OperationContext.Current.OutgoingMessageHeaders.RemoveAt(re);
                    OperationContext.Current.OutgoingMessageHeaders.Add(header);
                }
            }
        }

        
        #endregion
    }
}