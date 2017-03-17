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

using MB.Orm.Enums; 
namespace MB.Orm.Mapping.Att {
    /// <summary>
    ///TableMapAttribute指示实体类所映射的表。
    ///其中PrimaryKeys指明关键字段的名称。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ModelMapAttribute : Attribute {
        private string _TableName;
        private string[] _PrimaryKeys;
        private ModelConfigOptions _ConfigOptions;
        private string _XmlFileName;
        private string[] _UniqueKeys;



        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKeys"></param>
        public ModelMapAttribute(string tableName, params string[] primaryKeys)
            : this(tableName, tableName, primaryKeys) {

        }
        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="xmlFileName"></param>
        /// <param name="primaryKeys"></param>
        public ModelMapAttribute(string tableName,string xmlFileName, params string[] primaryKeys)  :
            this(tableName, xmlFileName, ModelConfigOptions.ColumnCfgByAttribute | ModelConfigOptions.CreateSqlByXmlCfg, primaryKeys) {
        }
        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="xmlFileName"></param>
        /// <param name="configOptions"></param>
        /// <param name="primaryKeys"></param>
        public ModelMapAttribute(string tableName, string xmlFileName,ModelConfigOptions configOptions,params string[] primaryKeys) {
            _TableName = tableName;
            _XmlFileName = xmlFileName;
            _PrimaryKeys = primaryKeys;
            _ConfigOptions = configOptions;
        }

        /// <summary>
        /// 表名称。
        /// </summary>
        public string TableName {
            get { return _TableName; }
            set { _TableName = value; }
        }
        /// <summary>
        /// 主键。
        /// </summary>
        public string[] PrimaryKeys {
            get { return _PrimaryKeys; }
            set { _PrimaryKeys = value; }
        }
        /// <summary>
        /// XML文件配置的名称。
        /// </summary>
        public string XmlFileName {
            get {
                return _XmlFileName;
            }
            set {
                _XmlFileName = value;
            }
        }
        /// <summary>
        /// 数据实体的数据库操作方式配置。
        /// 默认情况下：实体属性的映射配置通过Attribute 来完成，
        /// SQL 的创建通过XML 配置来完成。
        /// </summary>
        public ModelConfigOptions ConfigOptions {
            get {
                return _ConfigOptions;
            }
            set {
                _ConfigOptions = value;
            }
        }
        /// <summary>
        /// 唯一键
        /// </summary>
        public string[] UniqueKeys {
            get { return _UniqueKeys; }
            set { _UniqueKeys = value; }
        }
    }
}
