//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-10-13
// Description	:	表示类中的某一属性或者字段是否可以进行XML 系列化。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MB.Util.Serializer {
    /// <summary>
    /// 表示类中的某一属性或者字段是否可以进行XML 系列化。
    /// 特别说明：对于继承了IList 接口的属性类型,对该属性的设置会影响容器内子对象的设置。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ValueXmlSerializerAttribute : Attribute {
        private bool _Switch;
        private bool _GeneralStruct;
        private bool _CreateByInstanceType;
        private string _ValueType;
        /// <summary>
        ///  表示类中的某一属性或者字段是否可以进行XML 系列化。
        /// </summary>
        public ValueXmlSerializerAttribute() {
            _Switch = true;
        }
        /// <summary>
        /// 表示类中的某一属性或者字段是否可以进行XML 系列化。
        /// </summary>
        /// <param name="start"></param>
        public ValueXmlSerializerAttribute(bool start) {
            _Switch = start;
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
        /// 判断是否为结构，主要针对非常用类型的结构。（如Point 、Rectangle等类型的判断)
        /// </summary>
        public bool GeneralStruct {
            get {
                return _GeneralStruct;
            }
            set {
                _GeneralStruct = value;
            }
        }
        /// <summary>
        /// 判断是否根据存储时的实例类型来创建实例。
        /// </summary>
        public bool CreateByInstanceType {
            get {
                return _CreateByInstanceType;
            }
            set {
                _CreateByInstanceType = value;
            }
        }
        /// <summary>
        /// 值的数据类型。
        /// </summary>
        public string ValueType {
            get {
                return _ValueType;
            }
            set {
                _ValueType = value;
            }
        }
        
    }
    
}
