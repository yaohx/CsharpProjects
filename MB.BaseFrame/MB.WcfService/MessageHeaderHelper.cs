//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ServiceModel;

//using MB.Util.Model;
//namespace MB.WcfService {
//    /// <summary>
//    /// 获取消息头信息相关。
//    /// </summary>
//    public class MessageHeaderHelper {
//        /// <summary>
//        /// 获取当前登录的用户信息。
//        /// </summary>
//        /// <returns></returns>
//        public static SysLoginUserInfo GetCurrentLoginUser() {
//             string ctxt = string.Empty;
//             if (OperationContext.Current != null) {
//                 ctxt = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(MB.BaseFrame.SOD.CURRENT_USER_IDENTITY, "http://www.metersbonwe.com/");

//                 MB.Util.Serializer.EntityXmlSerializer<SysLoginUserInfo> ser = new MB.Util.Serializer.EntityXmlSerializer<SysLoginUserInfo>();
//                 if(!string.IsNullOrEmpty(ctxt))

//                 return ser.SingleDeSerializer(ctxt,string.Empty);
//             }
//             throw new MB.Util.APPException("在获取当前登录用户信息时出错！", MB.Util.APPMessageType.SysErrInfo); 


//        }
//        /// <summary>
//        /// 在当前通道中获取指定消息头的信息
//        /// </summary>
//        /// <param name="headerKey"></param>
//        /// <returns></returns>
//        public static string GetMessageHeaderContext(string headerKey) {
//            if (OperationContext.Current == null) return null;
//            string ctxt = string.Empty;
//            ctxt = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(headerKey, "http://www.metersbonwe.com/");
//            return ctxt;
      
//        }
//    }
//}
