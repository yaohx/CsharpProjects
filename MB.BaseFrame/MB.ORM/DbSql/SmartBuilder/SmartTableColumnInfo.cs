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

namespace MB.Orm.DbSql.SmartBuilder
{
    /// <summary>
    /// ActiveTableColumn
    /// </summary>
    internal class SmartTableColumnInfo
    {
        public SmartTableColumnInfo(string columnName, object value)
            : this(columnName, value, columnName) {

        }
        public SmartTableColumnInfo(string columnName, object value, string parameterName) {
            ColumnName = columnName;
            Value = value;
            ParameterName = parameterName;
        }

        public string ColumnName {
            get;
            set;
        }
        public string ParameterName {
            get;
            set;
        }
        public object Value {
            get;
            set;
        }

    }
}
