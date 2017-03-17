using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WcfService {
    /// <summary>
    /// 存在于WCF 服务端的代码运行跟踪器。
    /// </summary>
   // [System.Diagnostics.DebuggerStepThrough()]
    public class MyCodeRunTrackInjection : MB.Aop.IInjection {

        public MyCodeRunTrackInjection() {
        }
        #region IInjection 成员

        /// <summary>
        /// 方法调用之前。
        /// </summary>
        /// <param name="msg"></param>
        public void BeginProcess(System.Runtime.Remoting.Messaging.IMessage msg) {
            System.Runtime.Remoting.Messaging.IMethodCallMessage callMsg = msg as System.Runtime.Remoting.Messaging.IMethodCallMessage;
           beginExecuting(callMsg.MethodName, callMsg);
        }
        /// <summary>
        /// 方法调用之后。
        /// </summary>
        /// <param name="msg"></param>
        public void EndProcess(DateTime beginExecute, System.Runtime.Remoting.Messaging.IMessage msg) {
            System.Runtime.Remoting.Messaging.ReturnMessage callMsg = msg as System.Runtime.Remoting.Messaging.ReturnMessage;
          
            endExecuted(msg, beginExecute);
        }

        #endregion

        #region 内部函数处理...
        //方法开始执行.
        private DateTime beginExecuting(string methodName, System.Runtime.Remoting.Messaging.IMethodCallMessage cellmsg) {
            DateTime startTime = System.DateTime.Now;

            string msg = "调用方法:" + methodName;
            msg += getExecParamsInfo(cellmsg) + "  " + startTime.ToLongTimeString() + " " + startTime.Millisecond.ToString();
            //Console.WriteLine();
            MB.Util.TraceEx.Write(msg);

            return startTime;
        }
        //方法执行的结束
        private void endExecuted(System.Runtime.Remoting.Messaging.IMessage msg,  DateTime startTime) {
            System.Runtime.Remoting.Messaging.ReturnMessage callMsg = msg as System.Runtime.Remoting.Messaging.ReturnMessage;
            string methodName = callMsg.MethodName;
            DateTime endTime = System.DateTime.Now;
            TimeSpan elapsedTime = endTime.Subtract(startTime);
            string msg1 = "完成:" + methodName + endTime.ToLongTimeString() + " " + endTime.Millisecond.ToString();
            MB.Util.TraceEx.Write(msg1);

            string msg2 = "方法:" + methodName;
            if(callMsg.ReturnValue!=null)
               msg2 +=string.Format("返回值为:{0} ", callMsg.ReturnValue.ToString());
            msg2 += "总共花费(毫秒)" + elapsedTime.TotalMilliseconds.ToString();

            MB.Util.TraceEx.Write(msg2);
        }
        //获取参数信息。
        private string getExecParamsInfo(System.Runtime.Remoting.Messaging.IMethodCallMessage cellmsg) {
            if (cellmsg.ArgCount == 0) return "()";
            object[] pars = cellmsg.Args;
            int count = pars.Length;
            string msg = "(";
            for (int i = 0; i < count; i++) {
                string parName = cellmsg.GetArgName(i);
                msg += parName + "=";
                if (pars[i] == null)
                    msg += "null";
                else {
                    //截取过长的参数值 （在方法调用中，日记记录参数只是一个参考）
                    string temp = pars[i].ToString();
                    if (temp.Length > MB.BaseFrame.SOD.LOG_PARAMTER_VALUE_MAX_LENGTH)
                        temp = temp.Substring(0, MB.BaseFrame.SOD.LOG_PARAMTER_VALUE_MAX_LENGTH) + "......";
                    msg += temp;
                }
                msg += ",";
            }
            msg = msg.Remove(msg.Length - 1, 1);
            msg += ")";
            return msg;

        }
        #endregion 内部函数处理...
    }
}
