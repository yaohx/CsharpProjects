//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-06
// Description	:	ObjectDataMappingAttribute 单据数据关联配置自定义属性。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.RuleBase.Atts {
    /// <summary>
    /// ObjectDataMappingAttribute 单据数据关联配置自定义属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
    public class ObjectDataMappingAttribute : System.Attribute {
        private static readonly string DEFAULT_KEY_NAME = "ID";

        private string _MappingXmlFileName; //该类型对应的XML文件。
        private string _MappingTableName;  //该类型对应的表名称。
        private string _KeyName; //通过这种方式来进行配置的对象表主键只能有一个；
        private bool _ExecBySqlCmd; //判断是否通过Command 的方式来执行。，默认情况下是；
        private bool _KeyIsSelfAdd; //Key 值是否为自增加列（自增列的值通过内部的解决方案来进行）默认所有的对象都包含自增列。   
        private bool _DeleteNotInIds;
        private string _ForeingKeyName;
        private bool _IncludeSubmitField = true;//判断是否包含提交的字段
        private bool _ExecByRule;//
        private bool _ReadOnly;
        private string _Description;
        private bool _CheckReturnValue; 
        private string _TableAlias;//主表别名
        private Type _EntityType;//数据实体类型。
        private string _XmlCfgSelectSqlName;


        /// <summary>
        /// 构造函数...
        /// </summary>
        public ObjectDataMappingAttribute(string mappingXmlFileName)
            : this(mappingXmlFileName, false, true) {
        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="mappingXmlFileName"></param>
        /// <param name="execBySqlCmd">判断是否通过SQL Command 来执行数据库操作</param>
        /// <param name="mappingInfo"></param>
        public ObjectDataMappingAttribute(string mappingXmlFileName, bool execBySqlCmd, bool keyIsSelfAdd) {
            _ExecBySqlCmd = execBySqlCmd;
            _MappingXmlFileName = mappingXmlFileName;
            _MappingTableName = mappingXmlFileName;
            _KeyIsSelfAdd = keyIsSelfAdd;

        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="mappingXmlFileName"></param>
        /// <param name="selectSql"></param>
        public ObjectDataMappingAttribute(string mappingXmlFileName, string selectSql) {
            _ExecBySqlCmd = false;
            _MappingXmlFileName = mappingXmlFileName;
            _MappingTableName = mappingXmlFileName;
            _KeyIsSelfAdd = true;

        }
        /// <summary>
        /// 关联对应的XML文件名称。
        /// </summary>
        public string MappingXmlFileName {
            get {
                return _MappingXmlFileName;
            }
            set {
                _MappingXmlFileName = value;
            }
        }
        /// <summary>
        /// 关联对应的对象表的名称。
        /// </summary>
        public string MappingTableName {
            get {
                return _MappingTableName;
            }
            set {
                _MappingTableName = value;
            }
        }
        /// <summary>
        /// 当前
        /// </summary>
        public string KeyName {
            get {
                if (string.IsNullOrEmpty(_KeyName))
                    return DEFAULT_KEY_NAME;
                else
                    return _KeyName;
            }
            set {
                _KeyName = value;
            }
        }
        /// <summary>
        /// 判断是否通过SQL Command 来执行。
        /// </summary>
        public bool ExecBySqlCmd {
            get {
                return _ExecBySqlCmd;
            }
            set {
                _ExecBySqlCmd = value;
            }
        }
        /// <summary>
        /// 判断对应的表是否以自增列作为主键。
        /// </summary>
        public bool KeyIsSelfAdd {
            get {
                return _KeyIsSelfAdd;
            }
            set {
                _KeyIsSelfAdd = value;
            }
        }
        /// <summary>
        /// 判断是否启动DeleteNotInIDS的方法在批量处理中删除数据。
        /// </summary>
        public bool DeleteNotInIds {
            get {
                return _DeleteNotInIds;
            }
            set {
                _DeleteNotInIds = value;
            }
        }
        /// <summary>
        /// 外键名称。
        /// </summary>
        public string ForeingKeyName {
            get {
                return _ForeingKeyName;
            }
            set {
                _ForeingKeyName = value;
            }
        }
        /// <summary>
        /// 判断是否包含提交功能。
        /// </summary>
        public bool IncludeSubmit {
            get {
                return _IncludeSubmitField;
            }
            set {
                _IncludeSubmitField = value;
            }
        }
        /// <summary>
        /// 判断是否自动进行存储处理还是特定的业务类来完成。
        /// </summary>
        public bool ExecByRule {
            get {
                return _ExecByRule;
            }
            set {
                _ExecByRule = value;
            }
        }
        /// <summary>
        /// 判断该对象数据是否为只读。
        /// </summary>
        public bool ReadOnly {
            get {
                return _ReadOnly;
            }
            set {
                _ReadOnly = value;
            }
        }
        /// <summary>
        /// 对象描述。
        /// </summary>
        public string Description {
            get {
                return _Description;
            }
            set {
                _Description = value;
            }
        }
        /// <summary>
        /// 获取或者设置判断是否检查执行返回的值。
        /// </summary>
        public bool CheckReturnValue {
            get {
                return _CheckReturnValue;
            }
            set {
                _CheckReturnValue = value;
            }
        }
        /// <summary>
        /// 表别名。
        /// 需要的地方再进行配置。
        /// </summary>
        public string TableAlias {
            get {
                return _TableAlias;
            }
            set {
                _TableAlias = value;
            }
        }
        /// <summary>
        /// 数据实体类型。
        /// </summary>
        public Type EntityType {
            get {
                return _EntityType;
            }
            set {
                _EntityType = value;
            }
        }
        /// <summary>
        /// xml 文件配置的selecte 语句的SQL 名称。
        /// </summary>
        public string XmlCfgSelectSqlName {
            get {
                return _XmlCfgSelectSqlName;
            }
            set {
                _XmlCfgSelectSqlName = value;
            }
        }

    }
}
