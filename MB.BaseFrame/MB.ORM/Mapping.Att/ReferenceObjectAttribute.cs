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
    ///ReferenceObjectAttribute指示该属性是引用的另外一个对象，
    ///因此，在执行持久化操作的时候，需要根据参数进行额外的处理。
    ///默认情况下，当持久化实体对象的时候，ReferenceObjectAttribute
    ///指示的属性，不进行操作。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ReferenceObjectAttribute : Attribute {
        private Type referenceType;
        private string primaryKey;
        private string foreignKey;
        public ReferenceObjectAttribute(Type referenceType, string primaryKey, string foreignKey) {
            this.referenceType = referenceType;
            this.primaryKey = primaryKey;
            this.foreignKey = foreignKey;
        }

        public ReferenceObjectAttribute() { }

        public Type ReferenceType {
            get { return referenceType; }
            set { referenceType = value; }
        }

        public string PrimaryKey {
            get { return primaryKey; }
            set { primaryKey = value; }
        }

        public string ForeignKey {
            get { return foreignKey; }
            set { foreignKey = value; }
        }
    }
}
