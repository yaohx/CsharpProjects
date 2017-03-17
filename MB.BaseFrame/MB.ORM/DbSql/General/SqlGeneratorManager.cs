//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-08-02
// Description	:	 
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Orm.Enums;

namespace MB.Orm.DbSql
{
    /// <summary>
    /// SqlGeneratorManager
    /// </summary>
    public static class SqlGeneratorManager
    {
        private static XmlConfigSqlGenerator _XmlSqlConfigObject;
        private static AutoMappingSqlGenerator _AutoSqlCreate;

        /// <summary>
        /// 获取生成SQL 的操作对象
        /// </summary>
        /// <param name="cfgOptions"></param>
        /// <returns></returns>
        public static BaseSqlGenerator GetSqlGenerator(ModelConfigOptions cfgOptions) {
            return GetSqlGenerator(cfgOptions, null);
        }
        /// <summary>
        /// 获取生成SQL 的操作对象。
        /// </summary>
        /// <param name="cfgOptions"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static BaseSqlGenerator GetSqlGenerator(ModelConfigOptions cfgOptions, string[] properties) {
            BaseSqlGenerator sqlGenerator = null;
            if ((cfgOptions & ModelConfigOptions.CreateSqlByXmlCfg) != 0) {
                if (_XmlSqlConfigObject == null)
                    _XmlSqlConfigObject = new XmlConfigSqlGenerator();

                sqlGenerator = _XmlSqlConfigObject;
            }
            else {
                if (_AutoSqlCreate == null)
                    _AutoSqlCreate = new AutoMappingSqlGenerator();

                sqlGenerator = _AutoSqlCreate;
            }
            //如果存在个性化的属性，那么只能通过自动生成SQL 的方式。
            if (properties != null && properties.Length > 0) {
                if (_AutoSqlCreate == null)
                    _AutoSqlCreate = new AutoMappingSqlGenerator();

                return _AutoSqlCreate;
            }
            else
                return sqlGenerator;
        }
    }
}
