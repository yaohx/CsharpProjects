//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-18
// Description	:	表示类中的某一属性或者字段是否可以通过XML 配置来得到。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util.XmlConfig {
    /// <summary>
    /// 表示类中的某一属性或者字段是否可以通过XML 配置来得到。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class PropertyXmlConfigAttribute : Attribute {
        private bool _Switch;
        private Type _ReferenceModelType;
        private string _MappingName;
        private bool _NotExistsGroupNode;
        /// <summary>
        /// 表示类中的某一属性或者字段是否可以通过XML 配置来得到
        /// </summary>
        public PropertyXmlConfigAttribute() {
            _Switch = true;
        }
        /// <summary>
        /// 表示类中的某一属性或者字段是否可以通过XML 配置来得到
        /// </summary>
        /// <param name="referenceModelType"></param>
        public PropertyXmlConfigAttribute(Type referenceModelType) {
            _Switch = true;
            _ReferenceModelType = referenceModelType;
        }
        /// <summary>
        /// 判断该字段是否可以通过XML 配置来得到。
        /// </summary>
        public bool Switch {
            get {
                return _Switch;
            }
            set {
                _Switch = false;
            }
        }
        /// <summary>
        /// 该配置的属性引用的其它实体类的类型。
        /// </summary>
        public Type ReferenceModelType {
            get {
                return _ReferenceModelType;
            }
            set {
                _ReferenceModelType = value;
            }
        }
        /// <summary>
        /// 映射到XML 文件Node 上对应的名称。
        /// </summary>
        public string MappingName {
            get {
                return _MappingName;
            }
            set {
                _MappingName = value;
            }
        }
        /// <summary>
        /// 判断是否存在分组的节点，如果存在，该组下的所有节点都是该属性的子项。
        /// 以决定是否与集合的方式来存储。
        /// </summary>
        public bool NotExistsGroupNode {
            get {
                return _NotExistsGroupNode;
            }
            set {
                _NotExistsGroupNode = value;
            }
        }
    }
}
