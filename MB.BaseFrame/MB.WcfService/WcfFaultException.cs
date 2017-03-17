//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ServiceModel;
//using System.Runtime.Serialization;

//using MB.Util.Model; 
//namespace MB.WcfService {
//    /// <summary>
//    /// WCF 服务需要抛出的SAOP 异常。
//    /// </summary>
//    public class WcfFaultException : System.ServiceModel.FaultException<WcfFaultMessage> {
//        /// <summary>
//        /// 构造函数...
//        /// </summary>
//        /// <param name="innerException"></param>
//        public WcfFaultException(Exception innerException)
//            : this(string.Empty, innerException) {
//        }
//        /// <summary>
//        /// 构造函数。
//        /// </summary>
//        /// <param name="stMessage"></param>
//        /// <param name="messageType"></param>
//        public WcfFaultException(string stMessage,MB.Util.APPMessageType messageType )
//            : base(new WcfFaultMessage(stMessage, messageType)) {

//           // MB.Util.TraceEx.Write(stMessage, messageType);
//        }
//        /// <summary>
//        /// 构造函数。
//        /// </summary>
//        /// <param name="stMessage"></param>
//        /// <param name="messageType"></param>
//        public WcfFaultException(string stMessage, Exception innerException)
//            : base(new WcfFaultMessage(stMessage, MB.Util.APPMessageType.SysErrInfo)) {

//            if ((innerException as System.ServiceModel.FaultException<WcfFaultMessage>) == null) {
//                //判断是否为内部抛出的自定义异常类型。
//                MB.Util.APPException appException = innerException as MB.Util.APPException;

//                if (appException != null) {
//                    base.Detail.MessageType = appException.MsgLever;
//                    base.Detail.Message = stMessage + appException.Message;
//                }
//                else {
//                    string msg = stMessage;
//                    try {
//                        msg += "  Source:" + innerException.Source;
//                        msg += "  StackTrace:" + innerException.StackTrace;
//                    }
//                    catch { }
//                    MB.Util.TraceEx.Write(msg, base.Detail.MessageType);
//                }
//            }
//        }
//       /// <summary>
//       /// 创建显示给用户的异常信息。
//       /// </summary>
//       /// <param name="message"></param>
//       /// <returns></returns>
//        public static System.ServiceModel.FaultException<WcfFaultMessage> CreateDisplayToUserMessageException(string message) {
//            WcfFaultMessage fMsg = new WcfFaultMessage(message,MB.Util.APPMessageType.DisplayToUser);
//            // FaultCode code = new FaultCode("DisplayToUser");
//            System.ServiceModel.FaultException<WcfFaultMessage> exception = new FaultException<WcfFaultMessage>(fMsg, message);//, message, code, "服务错误");
//            return exception;
//        }
//       /// <summary>
//       /// 
//       /// </summary>
//       /// <param name="innerException"></param>
//       /// <returns></returns>
//        public static System.ServiceModel.FaultException<WcfFaultMessage> CreateException(Exception ex) {
//            if ((ex as System.ServiceModel.FaultException<WcfFaultMessage>) == null) {
//                //判断是否为内部抛出的自定义异常类型。
//                MB.Util.APPException appException = ex as MB.Util.APPException;
//                string msg = string.Empty;
//                MB.Util.APPMessageType msgType = MB.Util.APPMessageType.SysErrInfo;
//                if (appException != null) {
//                    msgType = appException.MsgLever;
//                    msg = appException.Message;
//                }
//                else {
//                    getErrMessage(ex, ref msg);
//                    MB.Util.TraceEx.Write(msg, msgType);
//                }
//                WcfFaultMessage fMsg = new WcfFaultMessage(msg, msgType);
//              //  FaultCode code = new FaultCode(msgType.ToString());
//                System.ServiceModel.FaultException<WcfFaultMessage> exception = new FaultException<WcfFaultMessage>(fMsg, msg);//, msg, code, "服务错误");

//                return exception;
//            }
//            else {
//                return ex as System.ServiceModel.FaultException<WcfFaultMessage>;
//            }
//        }

//        private static void getErrMessage(Exception ex, ref string errMsg) {
//            errMsg += " " + ex.Message;
//            if (ex.InnerException != null)
//                getErrMessage(ex.InnerException, ref errMsg);
//        }
//    }
//}
