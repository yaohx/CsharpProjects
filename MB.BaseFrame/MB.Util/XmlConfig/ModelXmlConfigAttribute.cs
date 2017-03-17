//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-18
// Description	:	表示某一实体类可以进行XML 文档配置来获取值。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util.XmlConfig {
    /// <summary>
    /// 表示某一实体类可以进行XML 文档配置来获取值。
    /// </summary>
   public class ModelXmlConfigAttribute  : Attribute {
        private string _NodePath;
        private bool _ByXmlNodeAttribute;

       /// <summary>
        /// 表示某一实体类可以进行XML 文档配置来获取值。
       /// </summary>
        public ModelXmlConfigAttribute() {
            _ByXmlNodeAttribute = false;
        }
       /// <summary>
       /// 表示某一实体类可以进行XML 文档配置来获取值。
       /// </summary>
        /// <param name="nodePath">对应配置节点在XML 文档总的XML 节点路径。</param>
        /// <param name="byXmlNodeAttribute">判断该字段是否可以通过XML 配置来得到。</param>
        public ModelXmlConfigAttribute(string nodePath,bool byXmlNodeAttribute) {
            _NodePath = nodePath;
            _ByXmlNodeAttribute = byXmlNodeAttribute;
        }
       /// <summary>
       /// 对应配置节点在XML 文档总的XML 节点路径。
       /// </summary>
        public string NodePath {
            get {
                return _NodePath;
            }
            set {
                _NodePath = value;
            }
        }
        /// <summary>
        /// 判断该字段是否可以通过XML 配置来得到。
        /// </summary>
        public bool ByXmlNodeAttribute {
            get {
                return _ByXmlNodeAttribute;
            }
            set {
                _ByXmlNodeAttribute = value;
            }
        }
    }
}
