//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-26
// Description	:	功能模块树型节点描述信息。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.Util.Model {
    /// <summary>
    /// 模块相应操作的描述信息。
    /// </summary>
    [DataContract]
    public class ModuleCommandInfo {
        private string _CommandID;
        private string _ClientRule;
        private string _UIView;
        private string _RuleCreateParams;
        private string _UICreateParams;

        /// <summary>
        /// 构造函数...
        /// </summary>
        public ModuleCommandInfo() {
           
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="commandID">命令操作的标记</param>
        /// <param name="clientRule">客户端的业务类</param>
        /// <param name="uiView">客户端操作界面</param>
        public ModuleCommandInfo(string commandID, string clientRule, string uiView) {
            _CommandID = commandID;
            _ClientRule = clientRule;
            _UIView = uiView;
        }
        #region public 属性...
        /// <summary>
        /// 命令操作的标记。
        /// </summary>
        [DataMember]
        public string CommandID {
            get {
                return _CommandID;
            }
            set {
                _CommandID = value;
            }
        }
        /// <summary>
        /// 客户端的业务类。
        /// 完整的类型配置信息，类型，配件名称之间用分号隔开。
        /// </summary>
        [DataMember]
        public string ClientRule {
            get {
                return _ClientRule;
            }
            set {
                _ClientRule = value;
            }
        }
        /// <summary>
        /// 客户端操作界面。
        /// 完整的类型配置信息，类型，配件名称之间用分号隔开。
        /// </summary>
        [DataMember]
        public string UIView {
            get {
                return _UIView;
            }
            set {
                _UIView = value;
            }
        }
        /// <summary>
        /// 业务类创建需要的参数说明。
        /// </summary>
        [DataMember]
        public string RuleCreateParams {
            get {
                return _RuleCreateParams;
            }
            set {
                _RuleCreateParams = value;
            }
        }
        /// <summary>
        /// UI 创建需要的参数说明。
        /// </summary>
        [DataMember]
        public string UICreateParams {
            get {
                return _UICreateParams;
            }
            set {
                _UICreateParams = value;
            }
        }

        #endregion public 属性...
    }

  
}
