//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-03-19
// Description	:	单据实体对象的分布式控制处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MB.Orm.Mapping;
using MB.Orm.Mapping.Att;
namespace MB.Orm.Persistence {
    /// <summary>
    /// 单据实体对象的分布式控制处理相关。
    /// </summary>
    [System.EnterpriseServices.Transaction( System.EnterpriseServices.TransactionOption.Supported)] 
    public class EntityDistributedHelper {

        //#region Instance...
        //private static Object _Obj = new object();
        //private static EntityDistributedHelper _Instance;

        ///// <summary>
        ///// 定义一个protected 的构造函数以阻止外部直接创建。
        ///// </summary>
        //protected EntityDistributedHelper() { }

        ///// <summary>
        ///// 多线程安全的单实例模式。
        ///// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        ///// </summary>
        public static EntityDistributedHelper NewInstance {
            get {
                return new EntityDistributedHelper();
            }
        }
        //#endregion Instance...

        private static readonly string LAST_MODIFIED_DATE_PROPERTY = "LAST_MODIFIED_DATE";

        /// <summary>
        /// 判断是否存在最后修改时间。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void CheckEntityCanSave(object entity) {
            if (entity == null) return;

            if (entity is DataRow) return;

            bool exists = MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, LAST_MODIFIED_DATE_PROPERTY);

            if (!exists) return;

            DateTime currentEditDate = getCurrentEditDate(entity);
            DateTime docDate = getEntityLastModifiedDate(entity);

           bool re = DateTime.Compare(currentEditDate, docDate) >= 0;
           if (!re)
               throw new MB.Util.APPException("当前单据可能已被其它用户修改,本次操作不成功！", MB.Util.APPMessageType.DisplayToUser);

        }
        /// <summary>
        /// 获取执行更新实体最后修改时间的DbCommand。
        /// </summary>
        /// <param name="db"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DbCommand GetSaveLastModifiedDateCommand(Database db, object entity) {
            bool exists = MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, LAST_MODIFIED_DATE_PROPERTY);

            if (!exists) return null;

            Type entityType = entity.GetType();
            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);
            if (string.IsNullOrEmpty(mappingInfo.MapTable))
                throw new MB.Util.APPException(string.Format("数据实体{0} 配置有误，没有指定映射到的表。", entityType.FullName), MB.Util.APPMessageType.SysErrInfo);

            if (mappingInfo.PrimaryKeys == null || mappingInfo.PrimaryKeys.Count != 1)
                throw new MB.Util.APPException(string.Format("数据实体{0} 配置有误，没有指定主键或者不是单一主键配置。", entityType.FullName), MB.Util.APPMessageType.SysErrInfo);

            FieldPropertyInfo keyInfo = mappingInfo.PrimaryKeys.Values.First<FieldPropertyInfo>();
            
            var dbType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            string sysDate = string.Empty;
            switch (dbType) {
                case MB.Orm.Enums.DatabaseType.Oracle:
                    sysDate = "SysDate";
                    break;
                case MB.Orm.Enums.DatabaseType.Sqlite:
                    sysDate = "datetime(CURRENT_TIMESTAMP,'localtime')";
                    break;
                case MB.Orm.Enums.DatabaseType.MSSQLServer:
                    sysDate = "GetDate()";
                    break;
                case Enums.DatabaseType.MySql:
                    sysDate = "Now()";
                    break;

                default:
                    throw new MB.Util.APPException(string.Format("在获取数据库系统时间时,数据库类型{0} 还没有进行相应的处理",dbType.ToString()), Util.APPMessageType.SysErrInfo);

            } 
            object key = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, keyInfo.PropertyName);
            string sqlStr = string.Format("UPDATE {0} SET {1}= {2} WHERE {3}='{4}'", mappingInfo.MapTable, LAST_MODIFIED_DATE_PROPERTY,
                                          sysDate, keyInfo.FieldName, key.ToString());

            return db.GetSqlStringCommand(sqlStr);
        }
        #region 内部处理函数...
        //获取数据实体当前编辑的时间
        private DateTime getCurrentEditDate(object entity) {
            object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, LAST_MODIFIED_DATE_PROPERTY);
            if (val == null) return DateTime.MinValue;
            return System.Convert.ToDateTime(val);
        }
        //获取数据库中当前实体的最后修改时间。
        private DateTime getEntityLastModifiedDate(object entity) {
            Type entityType = entity.GetType();
            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entityType);
            if (string.IsNullOrEmpty(mappingInfo.MapTable))
                throw new MB.Util.APPException(string.Format("数据实体{0} 配置有误，没有指定映射到的表。",entityType.FullName),MB.Util.APPMessageType.SysErrInfo);

            if(mappingInfo.PrimaryKeys==null || mappingInfo.PrimaryKeys.Count!=1)
                throw new MB.Util.APPException(string.Format("数据实体{0} 配置有误，没有指定主键或者不是单一主键配置。", entityType.FullName), MB.Util.APPMessageType.SysErrInfo);

            FieldPropertyInfo keyInfo = mappingInfo.PrimaryKeys.Values.First<FieldPropertyInfo>();
              
            object key = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, keyInfo.PropertyName);
            if (key == null) return DateTime.MinValue; 

            string sqlStr = string.Format("SELECT {0} FROM {1} WHERE {2}='{3}'",LAST_MODIFIED_DATE_PROPERTY,mappingInfo.MapTable,keyInfo.FieldName,key.ToString());

            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();  
            int lastID = 1;
            DbCommand cmdSelect = db.GetSqlStringCommand(sqlStr);
            object val = db.ExecuteScalar(cmdSelect);
            if (val == null || val == System.DBNull.Value) return DateTime.MinValue;

            cmdSelect.Dispose();

            return System.Convert.ToDateTime(val);
        }
        #endregion 内部处理函数...

    }
}
