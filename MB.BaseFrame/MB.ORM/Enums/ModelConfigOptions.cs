//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-14
// Description	:	指定实体对象类通过那种方式来进行处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MB.Orm.Enums {
    /// <summary>
    /// 指定实体对象类通过那种方式来进行处理。
    /// </summary>
    [Flags] 
    public enum ModelConfigOptions : int {
        [Description("None")]
        None = 0x0000,
        /// <summary>
        /// 通过Attribut配置对象的列映射。
        /// </summary>
        [Description("通过Attribut配置对象的列映射。")]
        ColumnCfgByAttribute = 0x0001,
        /// <summary>
        /// 通过Xml配置对象的列映射。
        /// </summary>
        [Description("通过Xml配置对象的列映射。")]
        ColumnCfgByXml = 0x0002,
        /// <summary>
        /// 根据列的映射自动创建处理的SQL 语句。
        /// </summary>
        [Description("根据列的映射自动创建处理的SQL 语句。")]
        AutoCreateSql = 0x0004,
        /// <summary>
        /// 通过XML 文件的配置得到SQL 语句。
        /// </summary>
        [Description("通过XML 文件的配置得到SQL 语句。")]
        CreateSqlByXmlCfg = 0x0008,
        /// <summary>
        /// 对象类的永久性处理通过业务类来进行操作。
        /// </summary>
        [Description("对象类的永久性处理通过业务类来进行操作。")]
        ExecuteByRule = 0x0016

    }
}

