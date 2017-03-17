//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-23
// Description	:	WcfFaultMessage: WCF SOAP 消息体定义。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.Util.Model {
    /// <summary>
    /// WCF SOAP 消息体定义。
    /// </summary>
    [DataContract]
    public class WcfFaultMessage {
        private MB.Util.APPMessageType _MessageType;
        private string _ErrorCode;
        private string _Message;

        #region 构造函数...
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="strMessage"></param>
        public WcfFaultMessage(string strMessage) : this(strMessage,MB.Util.APPMessageType.SysErrInfo){
            _Message = strMessage;
        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="messageType"></param>
        public WcfFaultMessage(string strMessage, MB.Util.APPMessageType messageType) {
            _Message = strMessage;
            _MessageType = messageType;
        }

        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="errorCode"></param>
        /// <param name="messageType"></param>
        public WcfFaultMessage(string strMessage, string errorCode, MB.Util.APPMessageType messageType) {
            _Message = strMessage;
            _ErrorCode = errorCode;
            _MessageType = messageType;
        }
        #endregion 构造函数...

        #region public 属性...
        /// <summary> 
        /// 异常消息文本 
        /// </summary> 
        [DataMember]
        public string Message {
            get { return _Message; }
            set { _Message = value; }
        }
        /// <summary> 
        /// 异常编码 
        /// </summary> 
        [DataMember]
        public string ErrorCode {
            get {
                return _ErrorCode;
            }
            set {
                _ErrorCode = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public MB.Util.APPMessageType MessageType { 
            get{
                return _MessageType;
            }
            set {
                _MessageType = value;
            } 
        }

        #endregion public 属性...
    }
}
