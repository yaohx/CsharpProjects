//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-05
// Description	:	数据库类型。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Enums {
    /// <summary>
    /// 数据库类型。
    /// </summary>
    public enum DatabaseType { 
        /// <summary>
        /// MSSQLServer
        /// </summary>
        MSSQLServer, 
        /// <summary>
        /// Oracle
        /// </summary>
        Oracle, 
        /// <summary>
        /// OleDBSupported
        /// </summary>
        OleDBSupported,
        /// <summary>
        /// Sqlite
        /// </summary>
        Sqlite,
        /// <summary>
        /// MySql
        /// </summary>
        MySql
    }
}
