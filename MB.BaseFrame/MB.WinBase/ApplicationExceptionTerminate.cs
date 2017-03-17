using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Util;

namespace MB.WinBase {
    /// <summary>
    /// 终止系统产生的异常，并以消息的形式提示给用户。
    /// </summary>
    public class ApplicationExceptionTerminate {
        /// <summary>
        /// 默认缺省的实例。
        /// </summary>
        public static ApplicationExceptionTerminate DefaultInstance {
            get {
                return MB.Util.SingletonProvider<ApplicationExceptionTerminate>.Instance;
            }
        }

        /// <summary>
        /// 终止系统产生的异常，并以消息的形式提示给用户。
        /// </summary>
        /// <param name="ex">异常</param>
        public void ExceptionTerminate(Exception ex) {
            ExceptionTerminate(ex, string.Empty);
        }
        /// <summary>
        /// 终止系统产生的异常，并以消息的形式提示给用户。
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="expectMsgOnError">出错时期待提示的消息。</param>
        public void ExceptionTerminate(Exception ex, string expectMsgOnError) {

            MB.Util.APPException appEx = getAppException(ex);// as MB.Util.APPException;
            string reason = string.Empty;
            if (appEx == null)
                appEx = ex.InnerException as MB.Util.APPException;

            if (appEx != null) {
                if (appEx.MsgLever == MB.Util.APPMessageType.DisplayToUser) {
                    MessageBoxEx.Show(appEx.Message);

                    return;
                }
                else {
                    expectMsgOnError = MB.Util.TraceEx.GetErrorMessageByType(appEx.MsgLever) + " " + appEx.Message;
                }
            }
            else {
                System.ServiceModel.FaultException<MB.Util.Model.WcfFaultMessage> fex = ex as System.ServiceModel.FaultException<MB.Util.Model.WcfFaultMessage>;
                if (fex != null) {
                    if (fex.Detail != null) {
                        if (fex.Detail.MessageType == MB.Util.APPMessageType.DisplayToUser) {
                            MessageBoxEx.Show(fex.Detail.Message);
                            return;
                        }
                        else {
                            reason = fex.Detail.Message + " " + MB.Util.TraceEx.GetErrorMessageByType(fex.Detail.MessageType); 
                        }
                    }
                    else {
                        reason = fex.Message;
                    }
                }
                string msg = string.Empty;
                if(string.IsNullOrEmpty(reason )) 
                    getErrMessage(ex, ref msg);
               
                TraceEx.Write(msg, APPMessageType.SysErrInfo);
                if(!string.IsNullOrEmpty(ex.StackTrace))
                    TraceEx.Write(ex.StackTrace, APPMessageType.SysErrInfo);
            }

            if (string.IsNullOrEmpty(expectMsgOnError)) {
                if(string.IsNullOrEmpty(reason))
                    reason = "系统出现未知的异常，请重试！";
                MessageBoxEx.Show(reason);
            }
            else {
                MessageBoxEx.Show("系统错误：" + expectMsgOnError);
            }

        }
        private void getErrMessage(Exception ex,ref string errMsg) {
            errMsg += " " + ex.Message;
            if (ex.InnerException != null)
                getErrMessage(ex.InnerException, ref errMsg);
        }

        private MB.Util.APPException getAppException(Exception ex) {
            MB.Util.APPException appEx = ex as MB.Util.APPException;
            if (appEx != null)
                return appEx;
            else {
                if (ex.InnerException != null)
                    return getAppException(ex.InnerException);
                else
                    return null;
            }
        }
    }
}
