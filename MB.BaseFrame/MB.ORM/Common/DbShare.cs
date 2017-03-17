//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-02
// Description	:	 数据库操作公共处理相关函数。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.EnterpriseLibrary.Data;
namespace MB.Orm.Common {
    /// <summary>
    /// 数据库操作公共处理相关函数。
    /// </summary>
    internal class DbShare {
       // private static MB.Orm.Enums.DatabaseType _DatabaseType = MB.Orm.Enums.DatabaseType.Oracle;//后期需要修改为从配置信息中获取
        /// <summary>
        /// Instance.
        /// </summary>
        public static DbShare Instance {
            get {
                return MB.Util.SingletonProvider<DbShare>.Instance; 
            }
        }
        ///// <summary>
        ///// 获取当前数据库的类型。
        ///// </summary>
        //public MB.Orm.Enums.DatabaseType GetCurrentDatabaseType {
        //    get {
        //        //Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase(        
        //        return _DatabaseType;
        //    }
        //}
        ///// <summary>
        ///// 得到对应数据库参数需要的前缀。
        ///// </summary>
        ///// <param name="databaseType"></param>
        ///// <returns></returns>
        //public string GetPramPrefixByDatabaseType(MB.Orm.Enums.DatabaseType databaseType) {
        //    switch (databaseType) {
        //        case MB.Orm.Enums.DatabaseType.MSSQLServer:
        //            return "@";
        //        case MB.Orm.Enums.DatabaseType.Oracle:
        //            return ":";
        //        default:
        //            throw new Exceptions.DatabaseNonsupportException();
        //    }
        //}
        /// <summary>
        /// 转换SystemType
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public  System.Data.DbType SystemTypeNameToDbType(string typeName) {
            switch (typeName) {
                case "System.String":
                    return System.Data.DbType.String;
                case "System.Boolean": 
                        return System.Data.DbType.Boolean;
                case "System.Int16":
                    return System.Data.DbType.Int16;
                case "System.Int32":
                    return System.Data.DbType.Int32;
                case "System.Int64":
                    return System.Data.DbType.Int64;
                case "System.Decimal":
                    return System.Data.DbType.Decimal;
                case "System.Byte[]":
                    return System.Data.DbType.Binary;
                case "System.DateTime":
                    return System.Data.DbType.DateTime;
                default:
                    return System.Data.DbType.String;
            }
        }
    }
}
