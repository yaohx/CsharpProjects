//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-05
// Description	:	字段 和 对象属性的Mapping 信息。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MB.Orm.Mapping {
    /// <summary>
    /// 字段 和 对象属性的Mapping 信息。
    /// </summary>
    public class FieldPropertyInfo {
        private string _PropertyName;
        private string _FieldName;
        private DbType _DbType;
        private object _DefaultValue;
        private bool _IsReferenceObject = false;
        private bool _IsSubObject = false;
        private bool _AutoIncrease;
        private int _Step = 1;
        private Type _DeclaringType;
        private PropertyInfo _PropertyInfo;

        #region public 属性...
        /// <summary>
        /// 属性名称。
        /// </summary>
        public string PropertyName {
            get {return _PropertyName; }
            set { _PropertyName = value; }
        }
        /// <summary>
        /// 字段名称。
        /// </summary>
        public string FieldName {
            get { return _FieldName; }
            set { _FieldName = value; }
        }
        /// <summary>
        /// 对应数据库类型。
        /// </summary>
        public DbType DbType {
            get { return _DbType; }
            set { _DbType = value; }
        }
        /// <summary>
        /// 缺省的值。
        /// </summary>
        public object DefaultValue {
            get { return _DefaultValue; }
            set { _DefaultValue = value; }
        }
        /// <summary>
        /// 是否引用对象。
        /// </summary>
        public bool IsReferenceObject {
            get { return _IsReferenceObject; }
            set { _IsReferenceObject = value; }
        }
        /// <summary>
        /// 是否为子对象。
        /// </summary>
        public bool IsSubObject {
            get { return _IsSubObject; }
            set { _IsSubObject = value; }
        }

        /// <summary>
        /// 是否为自增列。
        /// </summary>
        public bool AutoIncrease {
            get { return _AutoIncrease; }
            set { _AutoIncrease = value; }
        }
        /// <summary>
        /// 自增列的步长。
        /// </summary>
        public int Step {
            get { return _Step; }
            set { _Step = value; }
        }
        /// <summary>
        /// 对象属性定义的类型。
        /// </summary>
        public Type DeclaringType {
            get { return _DeclaringType; }
            set { _DeclaringType = value; }
        }
        /// <summary>
        /// 数据对象属性描述信息。
        /// </summary>
        public PropertyInfo PropertyInfo {
            get { return _PropertyInfo; }
            set { _PropertyInfo = value; }
        }

        #endregion public 属性...
    }

}
