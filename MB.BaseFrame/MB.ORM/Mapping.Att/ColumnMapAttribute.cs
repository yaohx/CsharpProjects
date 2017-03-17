//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-05
// Description	:	
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Mapping.Att {
   /// <summary>
   /// ColumnMapAttribute指明某个实体类属性所映射的数据库字段
   /// 目前的版本，在DbType部分，对大数据对象的支持有问题，需要
   /// 进一步改进
   /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnMapAttribute : Attribute {
        private string columnName;
        private DbType dbtype;
        private object defaultValue;
        private string _Description;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="dbtype"></param>
        public ColumnMapAttribute(string columnName, DbType dbtype) {
            this.columnName = columnName;
            this.dbtype = dbtype;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="description"></param>
        /// <param name="dbtype"></param>
        public ColumnMapAttribute(string columnName,string description, DbType dbtype) {
            this.columnName = columnName;
            this.dbtype = dbtype;
            _Description = description;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="dbtype"></param>
        /// <param name="defaultValue"></param>
        public ColumnMapAttribute(string columnName, DbType dbtype, object defaultValue) {
            this.columnName = columnName;
            this.dbtype = dbtype;
            this.defaultValue = defaultValue;
        }

        public string ColumnName {
            get { return columnName; }
            set { columnName = value; }
        }

        public DbType DbType {
            get { return dbtype; }
            set { dbtype = value; }
        }

        public object DefaultValue {
            get { return defaultValue; }
            set { defaultValue = value; }
        }
        /// <summary>
        /// 描述。
        /// </summary>
        public string Description {
            get {
                return _Description;
            }
            set {
                _Description = value;
            }
        }
    }
}
