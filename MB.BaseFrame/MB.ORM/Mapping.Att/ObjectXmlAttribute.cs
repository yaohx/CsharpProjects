//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-05
// Description	:	
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Mapping.Att {
  
    /// <summary>
    /// 指示该实体类的映射元数据信息由XML文件来进行描述。
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ObjectXmlAttribute : Attribute {
        private string _XmlFileName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlFileName"></param>
        public ObjectXmlAttribute(string xmlFileName) {
            this._XmlFileName = xmlFileName;
        }


        public string XmlFileName {
            get { return _XmlFileName; }
            set { _XmlFileName = value; }
        }
    }

}
