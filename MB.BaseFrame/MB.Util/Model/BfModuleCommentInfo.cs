//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	 2009-08-24 10:39 
// Description	:	模块使用情况评论。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace MB.Util.Model {
    /// <summary> 
    /// 文件生成时间 2009-08-24 10:37 
    /// 模块评论描述信息。
    /// </summary> 
    [DataContract]
    public class BfModuleCommentInfo {

        /// <summary>
        /// 模块评论描述信息。
        /// </summary>
        public BfModuleCommentInfo() {

        }
        private int _ID;
        /// <summary>
        /// 评论ID。
        /// </summary>
        [DataMember]
        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _APPLICATION_IDENTITY;
        /// <summary>
        /// 应用程序标识。
        /// </summary>
        [DataMember]
        public string APPLICATION_IDENTITY {
            get { return _APPLICATION_IDENTITY; }
            set { _APPLICATION_IDENTITY = value; }
        }
        private string _MODULE_IDENTITY;
        /// <summary>
        /// 模块标识。
        /// </summary>
        [DataMember]
        public string MODULE_IDENTITY {
            get { return _MODULE_IDENTITY; }
            set { _MODULE_IDENTITY = value; }
        }
        /// <summary>
        /// 评语类型。
        /// </summary>
        private string _COMMENT_TYPE;
        [DataMember]
        public string COMMENT_TYPE {
            get {
                return _COMMENT_TYPE;
            }
            set {
                _COMMENT_TYPE = value;
            }
        }
        private byte[] _COMMENT_CONTENT;
        /// <summary>
        /// 评论内容。
        /// </summary>
        [DataMember]
        public byte[] COMMENT_CONTENT {
            get { return _COMMENT_CONTENT; }
            set { _COMMENT_CONTENT = value; }
        }
        private string _USER_ID;
        /// <summary>
        /// 用户ID 。
        /// </summary>
        [DataMember]
        public string USER_ID {
            get { return _USER_ID; }
            set { _USER_ID = value; }
        }
        private DateTime _CREATE_DATE;
        /// <summary>
        /// 发表评论日期。
        /// </summary>
        [DataMember]
        public DateTime CREATE_DATE {
            get { return _CREATE_DATE; }
            set { _CREATE_DATE = value; }
        }
        private DateTime _LAST_MODIFIED_DATE;
        /// <summary>
        /// 最后修改日期。
        /// </summary>
        [DataMember]
        public DateTime LAST_MODIFIED_DATE {
            get { return _LAST_MODIFIED_DATE; }
            set { _LAST_MODIFIED_DATE = value; }
        }
    } 

}
