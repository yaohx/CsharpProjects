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

using MB.Util.XmlConfig;

namespace MB.Util.Model{
    /// <summary>
    /// 功能模块树型节点描述信息。
    /// </summary>
    [DataContract(Namespace = "MB.Util.Model.ModuleTreeNodeInfo")]
    public class ModuleTreeNodeInfo {
        private int _ID;
        private string _Code;
        private string _Name;
        private int _PrevID;
        private int _LevelNum;
        private int _Index;
        private int _ImageIndex;
        private string _PriID;
        private string _RejectCommands;
        private bool _IsGroupNode;

        private List<ModuleCommandInfo> _Commands;

        public ModuleTreeNodeInfo() {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public ModuleTreeNodeInfo(int id, string name) {
            _ID = id;
            _Name = name;
        }
        #region public 属性...
        /// <summary>
        /// 模块ID.
        /// </summary>
        [DataMember]
        public int ID {
            get {
                return _ID;
            }
            set {
                _ID = value;
            }
        }
        /// <summary>
        /// 编码
        /// </summary>
        [DataMember]
        public string Code {
            get {
                return _Code;
            }
            set {
                _Code = value;
            }
        }
        /// <summary>
        /// 模块名称。
        /// </summary>
        [DataMember]
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }
        /// <summary>
        /// 上级节点。
        /// </summary>
        [DataMember]
        public int PrevID {
            get {
                return _PrevID;
            }
            set {
                _PrevID = value;
            }
        }
        /// <summary>
        /// 所在层的编号。
        /// </summary>
        [DataMember]
        public int LevelNum {
            get {
                return _LevelNum;
            }
            set {
                _LevelNum = value;
            }
        }
        /// <summary>
        /// 节点在所在层的Index.
        /// </summary>
        [DataMember]
        public int Index {
            get {
                return _Index;
            }
            set {
                _Index = value;
            }
        }
        /// <summary>
        /// 模块图标。
        /// </summary>
        [DataMember]
        public int ImageIndex {
            get {
                return _ImageIndex;
            }
            set {
                _ImageIndex = value;
            }
        }
        /// <summary>
        /// 配置的权限Key。
        /// </summary>
        [DataMember]
        public string PriID {
            get {
                return _PriID;
            }
            set {
                _PriID = value;
            }
        }
        /// <summary>
        /// 否决的操作Commands。
        /// </summary>
        [DataMember]
        public string RejectCommands {
            get {
                return _RejectCommands;
            }
            set {
                _RejectCommands = value;
            }
        }
        /// <summary>
        /// 判断是否为分组节点。
        /// </summary>
        [DataMember]
        public bool IsGroupNode {
            get {
                return _IsGroupNode;
            }
            set {
                _IsGroupNode = value;
            }
        }
        /// <summary>
        /// 客户端业务类。
        /// </summary>
        [DataMember]
        [PropertyXmlConfig(typeof(ModuleCommandInfo))]  
        public List<ModuleCommandInfo> Commands {
            get {
                return _Commands;
            }
            set {
                _Commands = value;
            }
        }
        #endregion public 属性...
    }
}
