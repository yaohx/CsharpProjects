//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	应用程序级别异常处理。 
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace MB.Util {
    /// <summary>
    /// APPException 应用程序异常时产生的错误。
    /// </summary>
    [System.Diagnostics.DebuggerStepThrough()]
    [Serializable]
    public class APPException : ApplicationException {

        #region 变量定义...
        private APPMessageType _MsgLever = APPMessageType.OtherSysInfo;
        private string _ErrorCode;

        #endregion 变量定义...

        #region 构造函数...
        public APPException() {
        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="innerException"></param>
        public APPException(Exception innerException) : this(string.Empty,APPMessageType.SysErrInfo,innerException){

        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="pMsg">抛出异常的信息</param>
        public APPException(string clientMsg)
            : this(clientMsg, APPMessageType.SysErrInfo) {
        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="pMsg"></param>
        /// <param name="pMsgLever"></param>
        public APPException(string clientMsg, APPMessageType msgLever)
            : this(clientMsg, msgLever, "-1") {
        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="pMsg">抛出异常的信息</param>
        /// <param name="pMsgLever">异常信息的类型等级</param>
        public APPException(string clientMsg, APPMessageType msgLever, string errorCode)
            : base(clientMsg) {

            _MsgLever = msgLever;
            _ErrorCode = errorCode;
            TraceEx.Write(clientMsg, msgLever);

        }
        /// <summary>
        ///  构造函数...
        /// </summary>
        /// <param name="clientMsg"></param>
        /// <param name="msgLever"></param>
        /// <param name="innerException"></param>
        public APPException(string clientMsg, APPMessageType msgLever, Exception innerException)
            : base(clientMsg, innerException) {

            _MsgLever = msgLever;
            string msg = clientMsg;

            if (innerException != null) {
                string innerMsg = string.Empty;
                if (innerException is APPException) {
                    APPException appEx = innerException as   APPException;
                    _MsgLever = appEx.MsgLever;
                    innerMsg = appEx.Message;
                }
                else {
                    try {
                        getErrMessage(innerException, ref innerMsg);
                    }
                    catch { }
                }
                TraceEx.Write(innerMsg, APPMessageType.SysErrInfo);
                if (!string.IsNullOrEmpty(innerException.StackTrace))
                    TraceEx.Write(innerException.StackTrace);
            }
            TraceEx.Write(msg, msgLever);
        }
        #endregion 构造函数...
       
        #region Public 属性...
        /// <summary>
        /// 异常消息的类型
        /// </summary>
        public APPMessageType MsgLever {
            get {
                return _MsgLever;
            }
            set {
                _MsgLever = value;
            }
        }
        /// <summary>
        /// 产生异常的错误编码数值。
        /// </summary>
        public string ErrorCode {
            get {
                return _ErrorCode;
            }
        }
        #endregion Public 属性...

        private void getErrMessage(Exception ex, ref string errMsg) {
            errMsg += " " + ex.Message;
            if (ex.InnerException != null)
                getErrMessage(ex.InnerException, ref errMsg);
        }
    }
   /// <summary>
   /// 异常提升方法处理类。
   /// </summary>
    public class APPExceptionHandlerHelper {
        /// <summary>
        /// 创建异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="msgLever"></param>
        /// <returns></returns>
        public static APPException PromoteException(Exception ex,string appendErrorMessage) {
            if (ex is APPException) {
                return ex as APPException;
            }
            else
                return new APPException(appendErrorMessage, APPMessageType.SysErrInfo, ex);
        }
        /// <summary>
        /// 创建异常。
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="msgLever"></param>
        /// <returns></returns>
        public static APPException PromoteException(Exception ex, APPMessageType defaultMsgLever) {
            if (ex is APPException) {
                return ex as APPException;
            }
            else {
                APPException aex = new APPException(ex.Message, defaultMsgLever, ex);

                return aex;
            }
        }
        /// <summary>
        /// 创建显示给用户的消息。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public static APPException CreateDisplayToUser(string message, params string[] pars) {
            return new APPException(string.Format(message, pars), APPMessageType.DisplayToUser);
        }
    }
}
