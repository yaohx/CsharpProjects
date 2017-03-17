//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	TraceEx 程序代码跟踪
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Threading;
using MB.Platform.Common;
using MB.Platform.Logging;

namespace MB.Util {
    /// <summary>
    /// Trace 中相应的功能先不增加
    /// </summary>
    public class TraceEx
    {
        public const int SINGLE_LOG_FILE_MAX_SIZE = 1024 * 1024; //1M 日记文件的最大值，如果大于它就分文件记录
        private const int MAX_COLUMN_COUNT = 128;//在碰到大数据量时需要过滤的单行最大长度
        private static readonly string LOG_CFG_FLAG_NAME = "SaveCodeRunInfo";
        private static readonly string MUST_LOG_SWITCH_NAME_IPS = "RunInfoAllowListenerIps";
        private static System.Collections.Generic.Stack<string> _LogsStack = new Stack<string>();
        private static LogsConsume _LogsConsume;

        private static ILogger s_logger = null;

        static TraceEx()
        {
            _LogsConsume = new LogsConsume();
            _LogsConsume.IniTimerIfNull();

            s_logger = LoggerManager.GetLoggerForType(typeof(TraceEx));
        }

        #region Write ...
        /// <summary>
        /// 把运行的代码过程记录下来
        /// </summary>
        /// <param name="msgStr"></param>
        /// <param name="formatters"></param>
        public static void Write(string msgStr, params string[] paras)
        {
            if (paras.Length > 0) {
                try {
                    msgStr = string.Format(msgStr, paras);
                }
                catch (System.FormatException) {
                    //如果Format参数个数不正确，不做format
                }
            } 
                        
            Write(msgStr, APPMessageType.CodeRunInfo);
        }

        /// <summary>
        /// 把运行的代码过程记录下来
        /// </summary>
        /// <param name="msgStr"></param>
        public static void Write(string msgStr)
        {
            Write(msgStr, APPMessageType.CodeRunInfo);
        }
        /// <summary>
        /// 把运行的代码过程记录下来
        /// </summary>
        /// <param name="msgStr"></param>
        /// <param name="msgLevel"></param>
        public static void Write(string msgStr, APPMessageType msgLevel)
        {
            WriteIf(false, msgStr, msgLevel);
        }
        #endregion Write ...

        #region WriteIf...
        /// <summary>
        ///  根据条件把运行的代码过程记录下来
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="msgStr"></param>
        public static void WriteIf(bool condition, string msgStr)
        {
            WriteIf(condition, msgStr, APPMessageType.CodeRunInfo);
        }
        /// <summary>
        /// 根据条件把运行的代码过程记录下来
        /// </summary>
        /// <param name="condition">condition ：如果要禁止显示消息，那么为true,否则为false</param>
        /// <param name="msgStr"></param>
        /// <param name="msgLevel"></param>
        public static void WriteIf(bool condition, string msgStr, APPMessageType msgLevel)
        {
            SaveIf(condition, msgStr, msgLevel);
        }
        #region 显示断言...
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition">condition ：如果要禁止显示消息，那么为true,否则为false  </param>
        /// <param name="msg"></param>
        public static void Assert(bool condition, string msg)
        {
            Assert(condition, msg, "");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition">condition ：如果要禁止显示消息，那么为true,否则为false</param>
        /// <param name="msg"></param>
        /// <param name="detailMsg"></param>
        public static void Assert(bool condition, string msg, string detailMsg)
        {
            Debug.Assert(condition, msg, detailMsg);
            SaveIf(condition, msg + detailMsg, APPMessageType.CodeRunInfo);
        }
        #endregion 显示断言...
        #endregion WriteIf...
        /// <summary>
        /// SaveIf
        /// </summary>
        /// <param name="condition"> condition ：如果要禁止写入消息，那么为true,否则为false</param>
        /// <param name="msgStr"></param>
        /// <param name="msgLevel"></param>
        /// <returns></returns>
        public static bool SaveIf(bool condition, string msgStr, APPMessageType msgLevel)
        {
            if (condition) return true;

            LogLevel level = LogLevel.None;
            switch (msgLevel)
            {
                case APPMessageType.CodeRunInfo:
                    level = LogLevel.Debug;
                    break;

                case APPMessageType.DataInvalid:
                    level = LogLevel.Warn;
                    break;

                case APPMessageType.DisplayToUser:
                    level = LogLevel.Info;
                    break;

                case APPMessageType.OtherSysInfo:
                    level = LogLevel.Info;
                    break;

                case APPMessageType.SysDatabaseInfo:
                    level = LogLevel.Info;
                    break;

                case APPMessageType.SysErrInfo:
                    level = LogLevel.Error;
                    break;

                case APPMessageType.SysFileInfo:
                    level = LogLevel.Info;
                    break;

                case APPMessageType.SysWarning:
                    level = LogLevel.Warn;
                    break;

                default:
                    level = LogLevel.None;
                    break;
            }

            if (string.IsNullOrEmpty(msgStr)) return true;
            if (msgStr.Length > MB.BaseFrame.SOD.L_SINGLE_MAX_LOG_LENGTH)
            {
                msgStr = msgStr.Substring(0, MB.BaseFrame.SOD.L_SINGLE_MAX_LOG_LENGTH) + " [[单行日志超长,部分字符已经被截取掉]]";
            }
            //显示给用户看的消息不需要存储
            if (msgLevel == APPMessageType.DisplayToUser)
            {
                return true;
            }
            //判断是否为记录代码运行的信息，如果是，将根据应用程序的配置来决定是否存储
            if (msgLevel == APPMessageType.CodeRunInfo)
            {
                var allow = checkAllowWriteRunLog();
                if (!allow) return true;
            }

            string errTypeStr = GetErrorMessageByType(msgLevel) + "-->";
            string requestIP = MB.Util.General.GetRequestIP();
            if (!string.IsNullOrEmpty(requestIP))
                requestIP = "(" + requestIP + ")";
            string strLine = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":" + requestIP + errTypeStr + msgStr;

            lock (_LogsStack)
            {
                _LogsStack.Push(strLine);                
            }

            s_logger.LogAll(level, null, msgStr);

            return true;
        }
        //判断是否允许记录代码轨迹
        private static bool checkAllowWriteRunLog()
        {
            string str = System.Configuration.ConfigurationManager.AppSettings[LOG_CFG_FLAG_NAME];
            bool allowLog = !string.IsNullOrEmpty(str) && string.Compare(str, "True", true) == 0;
            if (!allowLog)
            {
                string ipsCfg = System.Configuration.ConfigurationManager.AppSettings[MUST_LOG_SWITCH_NAME_IPS];
                if (!string.IsNullOrEmpty(ipsCfg))
                {
                    string ipstr = MB.Util.General.GetRequestIP();
                    string[] ips = ipsCfg.Split(',');
                    allowLog = Array.IndexOf<string>(ips, ipstr) >= 0;
                }
            }

            return allowLog;
        }

        /// <summary>
        /// 根据错误的类型获取对应的错误描述信息。
        /// </summary>
        /// <param name="msgLevel"></param>
        /// <returns></returns>
        public static string GetErrorMessageByType(APPMessageType msgLevel)
        {
            switch (msgLevel)
            {
                case APPMessageType.CodeRunInfo:
                    return "代码执行轨迹";
                case APPMessageType.SysDatabaseInfo:
                    return "数据库操作失败";
                case APPMessageType.SysErrInfo:
                    return "系统错误";
                case APPMessageType.SysFileInfo:
                    return "硬盘文件操作失败";
                case APPMessageType.OtherSysInfo:
                    return "代码执行失败";
                default:
                    return string.Empty;

            }

        }
        /// <summary>
        /// 应用程序日志消费处理
        /// </summary>
        class LogsConsume
        {
            private static System.Threading.Timer _Timer;
            private static object _obj = new object();
            private int _Finished = 0;
            private string _APP_TEMP_PATH;
            private System.Collections.Generic.Dictionary<string, int> _LogFileIndex;
            private static string LOG_FILE_TEMPLET = @"{0}\({1})mbAppLog-{2}#.txt";
            public LogsConsume()
            {
                _LogFileIndex = new Dictionary<string, int>();
                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                if (appPath.LastIndexOf(@"\") < appPath.Length - 1)
                    appPath += @"\";

                _APP_TEMP_PATH = appPath + @"temp";
                if (!System.IO.Directory.Exists(_APP_TEMP_PATH))
                    System.IO.Directory.CreateDirectory(_APP_TEMP_PATH);

            }
            /// <summary>
            /// 初始化内存日志处理后台任务。
            /// </summary>
            public void IniTimerIfNull()
            {
                if (_Timer == null)
                {
                    lock (_obj)
                    {
                        if (_Timer == null)
                        {
                            _Timer = new System.Threading.Timer(new System.Threading.TimerCallback(consumeStackLog), null, 0, 1000);

                        }
                    }
                }
            }
            private void consumeStackLog(object state)
            {
                if (Interlocked.Exchange(ref _Finished, 1) == 0)
                {
                    int index = getCurrentLogFileIndex();
                    string name = getValidLogFileName(DateTime.Now.ToString(MB.BaseFrame.SOD.DATE_WITHOUT_TIME_FORMATE), ref index);
                    string tfullPath = string.Format(LOG_FILE_TEMPLET, _APP_TEMP_PATH, DateTime.Now.ToString(MB.BaseFrame.SOD.DATE_WITHOUT_TIME_FORMATE), index);
                    string[] vals = null;
                    if (_LogsStack.Count > 0)
                    {
                        lock (_LogsStack)
                        {
                         
                            vals = new string[_LogsStack.Count];
                            int i = 0;
                            while (_LogsStack.Count > 0)
                            {
                                vals[i++] = _LogsStack.Pop();
                            }
                          
                        }
                        try
                        {
                            using (StreamWriter swFile = new StreamWriter(tfullPath, true))
                            {
                                int length = vals.Length;
                                for (int i = 0; i < length; i++)
                                {
                                    swFile.WriteLine(vals[length - i - 1]);
                                }
                            }
                        }
                        catch { }
                    }
                    Interlocked.Exchange(ref _Finished, 0);
                }

            }
            private int getCurrentLogFileIndex()
            {
                string key = DateTime.Now.ToString(MB.BaseFrame.SOD.DATE_WITHOUT_TIME_FORMATE);
                if (_LogFileIndex.ContainsKey(key))
                    return _LogFileIndex[key];
                else
                    return 0;
            }
            private void writeCurrentLogFileIndex(int index)
            {
                string key = DateTime.Now.ToString(MB.BaseFrame.SOD.DATE_WITHOUT_TIME_FORMATE);
                _LogFileIndex[key] = index;
            }
            private string getValidLogFileName(string dateString, ref int index)
            {
                string logFullName = string.Format(LOG_FILE_TEMPLET, _APP_TEMP_PATH, dateString, index);
                if (!System.IO.File.Exists(logFullName))
                {
                    writeCurrentLogFileIndex(index);
                    return logFullName;
                }
                FileInfo fInfo = new FileInfo(logFullName);

                if (fInfo.Length < TraceEx.SINGLE_LOG_FILE_MAX_SIZE)
                {
                    writeCurrentLogFileIndex(index);
                    return logFullName;
                }
                else
                {
                    index++;
                    return getValidLogFileName(dateString, ref index);
                }

            }
        }
    }
}
