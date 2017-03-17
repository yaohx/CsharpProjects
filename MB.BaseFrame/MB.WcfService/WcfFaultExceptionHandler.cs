using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

using MB.Util.Model;
namespace MB.WcfService {
    /// <summary>
    /// 异常提升。
    /// </summary>
    public class WcfFaultExceptionHandler : IErrorHandler {
        #region IErrorHandler 成员

        public bool HandleError(Exception error) {
            //throw new Exception();

            if ((error as MB.Util.APPException)!=null) {
            }
            else if (error is FaultException<WcfFaultMessage>) {
            }
            else{
                string errMsg = string.Empty;
                getErrMessage(error, ref errMsg);
                MB.Util.TraceEx.Write(errMsg);  
            }

            return true;
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault) {
            if ((error as MB.Util.APPException)!=null) {
                MB.Util.APPException appEx = error as MB.Util.APPException;
                var myerror = new FaultException<WcfFaultMessage>(new WcfFaultMessage(appEx.Message, appEx.ErrorCode, appEx.MsgLever));
                MessageFault myFault = myerror.CreateMessageFault();
                fault = Message.CreateMessage(version, myFault, myerror.Action);
            }
            else if (error is FaultException<WcfFaultMessage>) {
                //不需要进行处理。
            }
            else {
                var myerror = new FaultException<WcfFaultMessage>(new WcfFaultMessage(error.Message, MB.Util.APPMessageType.SysErrInfo));
                MessageFault myFault = myerror.CreateMessageFault();
                fault = Message.CreateMessage(version, myFault, myerror.Action);
            }
        }

        #endregion

        private void getErrMessage(Exception ex, ref string errMsg) {
            errMsg += " " + ex.Message;
            if (ex.InnerException != null)
                getErrMessage(ex.InnerException, ref errMsg);
        }
    }
}
