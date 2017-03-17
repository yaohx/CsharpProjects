using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util {
    /// <summary>
    /// 方法调用中带有时间的日记跟踪
    /// </summary>
   public class MethodTraceWithTime : IDisposable {
       private string _MethodName;
       private string _ArgsString;
       private DateTime _BeginTime;
       private string _CmdText;
       private bool _IsDBCmdMonitor;

       /// <summary>
       /// 方法调用中带有时间的日记跟踪
       /// </summary>
       /// <param name="isDBCmdMonitor">表示是监控DB Cmd</param>
       /// <param name="cmdText">sql语句</param>
       public MethodTraceWithTime(bool isDBCmdMonitor, string cmdText)
       {
           _BeginTime = System.DateTime.Now;
           _IsDBCmdMonitor = isDBCmdMonitor;
           if (isDBCmdMonitor)
               _CmdText = cmdText;
           else
               _MethodName = cmdText;
       }

       /// <summary>
       /// 方法调用中带有时间的日记跟踪
       /// </summary>
       /// <param name="methodName">调用方法的名称</param>
       /// <param name="parValues">方法调用的参数</param>
       public MethodTraceWithTime(string methodName, params object[] parValues) {
           _BeginTime = System.DateTime.Now;
           if (!string.IsNullOrEmpty(methodName))
           {
               _MethodName = methodName;
               _ArgsString = parValuesToString(parValues);
               MB.Util.TraceEx.Write(string.Format("开始执行方法:{0}({1})", methodName, _ArgsString));
           }
       }

       private string parValuesToString(params object[] parValues) {
           if (parValues == null || parValues.Length == 0)
              return string.Empty;
           List<string> pars = new List<string>();
           foreach (object p in parValues) {
               if (p != null) {
                   string temp = p.ToString();
                   if (temp.Length > MB.BaseFrame.SOD.LOG_PARAMTER_VALUE_MAX_LENGTH)
                       temp = temp.Substring(0, MB.BaseFrame.SOD.LOG_PARAMTER_VALUE_MAX_LENGTH) + "......";
                   pars.Add(temp);
               }
               else
                   pars.Add(" ");

           }
           return string.Join(",", pars.ToArray());
       }


       /// <summary>
       /// 获取已经执行的时间
       /// </summary>
       /// <returns></returns>
       public double GetExecutedTimes() {
           DateTime endTime = System.DateTime.Now;
           TimeSpan elapsedTime = endTime.Subtract(_BeginTime);
           return elapsedTime.TotalMilliseconds;
       }
       #region IDisposable 成员
      /// <summary>
      /// 
      /// </summary>
       public void Dispose() {

           if (_IsDBCmdMonitor)
           {
               DateTime endTime = System.DateTime.Now;
               TimeSpan elapsedTime = endTime.Subtract(_BeginTime);

               if (MB.Util.Monitors.WcfPerformaceMonitorContext.Current != null &&
                MB.Util.Monitors.WcfPerformaceMonitorContext.Current.CurrentWcfProcessMonitorInfo != null &&
                MB.Util.Monitors.WcfPerformaceMonitorContext.Current.CurrentWcfProcessMonitorInfo.CurrentDBProcessMonitorInfo != null)
               {
                   MB.Util.Monitors.DBCommandProcessMonitorInfo cmdMonitor = new Util.Monitors.DBCommandProcessMonitorInfo();
                   cmdMonitor.DBCommandProcessTime = elapsedTime;
                   cmdMonitor.CommandText = _CmdText;
                   MB.Util.Monitors.WcfPerformaceMonitorContext.Current.CurrentWcfProcessMonitorInfo.CurrentDBProcessMonitorInfo.DBCommandProcessMonitorInfos.Add(cmdMonitor);
               }
           }
           else
           {
               if (!string.IsNullOrEmpty(_MethodName))
               {
                   DateTime endTime = System.DateTime.Now;
                   TimeSpan elapsedTime = endTime.Subtract(_BeginTime);

                   string msg1 = string.Format("完成:{0}({1}) {2} {3}", _MethodName, _ArgsString, endTime.ToLongTimeString(), endTime.Millisecond.ToString());
                   MB.Util.TraceEx.Write(msg1);
                   string msg2 = "方法:" + _MethodName;
                   msg2 += "总共花费(毫秒)" + elapsedTime.TotalMilliseconds.ToString();
                   MB.Util.TraceEx.Write(msg2);
               }
           }
       }

       #endregion
   }
}
