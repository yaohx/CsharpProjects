using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel; 
namespace MB.WcfServiceLocator {
    /// <summary>
    /// 提供WCF 客户端需要的Public 函数。
    /// </summary>
   public class WcfClientHelper {

        #region Instance...
        private static Object _Obj = new object();
        private static WcfClientHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected WcfClientHelper() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static WcfClientHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new WcfClientHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        /// <summary>
        /// 通过反射公共调用WCF 服务端的方法。
        /// </summary>
        /// <param name="serverRule"></param>
        /// <param name="methodName"></param>
        /// <param name="paramVals"></param>
        /// <returns></returns>
        public object  InvokeServerMethod(object serverRule, string methodName, params object[] paramVals) {
            try {
                //WCF 接口的方法没有重载，只要根据名称来进行调用就可以。
                return MB.Util.MyReflection.Instance.InvokeMethodByName(serverRule, methodName, paramVals);
            }
            catch (FaultException<MB.Util.Model.WcfFaultMessage> e) { //先判断是否为WCF 产生SOAP 消息的异常
                throw new MB.Util.APPException(e.Detail.Message, e.Detail.MessageType, e.Detail.ErrorCode);
            }
            catch (FaultException fe) {
                throw new MB.Util.APPException("接收到来自服务端返回的异常错误,本次操作不成功", MB.Util.APPMessageType.DisplayToUser,fe);
            }
            catch (MB.Util.APPException ex) { //如果是应用程序可控的异常就直接抛出。
                throw ex;
            }
            catch (Exception exx) { //
                FaultException<MB.Util.Model.WcfFaultMessage> innEx = exx.InnerException as FaultException<MB.Util.Model.WcfFaultMessage>;
                if (innEx != null)
                    throw new MB.Util.APPException(innEx.Detail.Message, innEx.Detail.MessageType);
                else {
                    FaultException fexx = exx.InnerException as FaultException;
                    if(fexx!=null)
                        throw new MB.Util.APPException("接收到来自服务端返回的异常错误,本次操作不成功", MB.Util.APPMessageType.DisplayToUser, fexx);
                    else
                        throw new MB.Util.APPException(exx);
                }
            }
 
        }

 
    }
}
