//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-05
// Description	:	对象Mapping 的描述。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.Orm.Enums; 
namespace MB.Orm.Mapping {
    /// <summary>
    /// 对象Mapping 的描述。
    /// </summary>
    public class ModelMappingInfo {
        #region private declare...
        private string _ClassName;
        private Type _EntityType;
        private string _MapTable;
        private bool _IsMultPrimaryKey;
        private Dictionary<string, FieldPropertyInfo> _PrimaryKeys;
        private bool _HasAutoIncreasePorperty;
        private string _AutoIncreasePorperty;
        private Dictionary<string, FieldPropertyInfo> _FieldPropertys;
        private string _XmlConfigFileName;
        private ModelConfigOptions _ConfigOptions;
        #endregion private declare...

        #region public 属性...
        /// <summary>
        /// 数据对象的名字。
        /// </summary>
        public string ClassName {
            get { return _ClassName; }
            set { _ClassName = value; }
        }
        /// <summary>
        /// 实体的类型。
        /// </summary>
        public Type EntityType {
            get { return _EntityType; }
            set { _EntityType = value; }
        }
        /// <summary>
        /// 映射的表。
        /// </summary>
        public string MapTable {
            get { return _MapTable; }
            set { _MapTable = value; }
        }
        /// <summary>
        /// 是否为联合主键。
        /// </summary>
        public bool IsMultiPrimaryKey {
            get { return _IsMultPrimaryKey; }
            set { _IsMultPrimaryKey = value; }
        }
        /// <summary>
        /// 主键。
        /// </summary>
        public Dictionary<string, FieldPropertyInfo> PrimaryKeys {
            get { return _PrimaryKeys; }
            set { _PrimaryKeys = value; }
        }
        /// <summary>
        /// 是否存在自增列属性。
        /// </summary>
        public bool HasAutoIncreasePorperty {
            get { return _HasAutoIncreasePorperty; }
            set { _HasAutoIncreasePorperty = value; }
        }
        /// <summary>
        /// 自增列属性。
        /// </summary>
        public string AutoIncreasePorperty {
            get { return _AutoIncreasePorperty; }
            set { _AutoIncreasePorperty = value; }
        }

        /// <summary>
        /// 字段属性的映射信息。
        /// </summary>
        public Dictionary<string, FieldPropertyInfo> FieldPropertys {
            get { return _FieldPropertys; }
            set { _FieldPropertys = value; }
        }
        /// <summary>
        /// XML 文件配置的名称。
        /// </summary>
        public string XmlConfigFileName {
            get {
                return _XmlConfigFileName;
            }
            set {
                _XmlConfigFileName = value;
            }
        }
        /// <summary>
        /// 对象的配置选项信息。
        /// </summary>
        public ModelConfigOptions ConfigOptions {
            get {
                return _ConfigOptions;
            }
            set {
                _ConfigOptions = value;
            }
        }
        #endregion public 属性...

    }
}
