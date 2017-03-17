//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-05-18
// Description	:	公共跟踪处理单据状态。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MB.Orm.DbSql;
using System.Data.Common;
using MB.Util.Model;  

namespace MB.RuleBase.Common {
    /// <summary>
    /// 公共处理单据状态改变跟踪记录。
    /// </summary>
    public class BusinessOperateTracHelper
    {
        #region 变量定义..
        //增加到单据状态更改的
        private static readonly string SQL_INSERT_DOC_TRACE = @"INSERT INTO SYS_DOC_STATE_CHANGED_TRACE(ID,DOC_TYPE,DOC_ID,DOC_ORG_SATE,OP_USER_CODE,OP_DATE,OP_STATE,REMARK) 
                                                                VALUES(@ID,@DOC_TYPE,@DOC_ID,@DOC_ORG_SATE,@OP_USER_CODE,SYSTIMESTAMP,@OP_STATE,@REMARK)";
        private static readonly string SQL_UPDATE_DOC_STATE = @"UPDATE  {0} SET DOC_STATE = @DOC_STATE,LAST_MODIFIED_DATE = SYSTIMESTAMP WHERE ID = @DOC_ID";

        private static readonly string SQL_GET_ORG_DOC_STATE = @"SELECT DOC_ORG_SATE 
                                                                 FROM SYS_DOC_STATE_CHANGED_TRACE C
                                                                 WHERE C.OP_DATE = (SELECT MAX(OP_DATE) FROM SYS_DOC_STATE_CHANGED_TRACE WHERE
                                                                                    DOC_TYPE = @DOC_TYPE AND DOC_ID = @DOC_ID AND OP_STATE = @OP_STATE)
                                                                       AND DOC_TYPE = @DOC_TYPE2 AND DOC_ID = @DOC_ID2 AND OP_STATE = @OP_STATE2";
        private static readonly string SQL_GET_LAST_DOC_STATE = @"SELECT OP_STATE 
                                                                  FROM SYS_DOC_STATE_CHANGED_TRACE C
                                                                  WHERE C.OP_DATE = (SELECT MAX(OP_DATE) FROM SYS_DOC_STATE_CHANGED_TRACE WHERE
                                                                  DOC_TYPE = @DOC_TYPE AND DOC_ID = @DOC_ID )
                                                                  AND DOC_TYPE = @DOC_TYPE2 AND DOC_ID = @DOC_ID2";
                                        

        #endregion 变量定义..
        /// <summary>
        /// 返回新的对象实例。
        /// </summary>
        public static BusinessOperateTracHelper NewInstance {
            get {
                return new BusinessOperateTracHelper();
            }
        }
        #region public 方法...
        /// <summary>
        /// 提供扩展的操作单据状态
        /// </summary>
        /// <param name="docType"></param>
        /// <param name="docID"></param>
        /// <param name="remark"></param>
        /// <param name="operateType"></param>
        /// <returns></returns>
        public int SaveExtendDocStateTrack(string docType, int docID, string remark, int operateType) {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            string user = MB.WcfService.MessageHeaderHelper.GetCurrentLoginUser().USER_CODE;
            int id = MB.Orm.Persistence.EntityIdentityHelper.NewInstance.GetEntityIdentity("SYS_DOC_STATE_CHANGED_TRACE");
            var databaseType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            string sql = databaseType == MB.Orm.Enums.DatabaseType.Oracle ? SQL_INSERT_DOC_TRACE.Replace('@', ':') : SQL_INSERT_DOC_TRACE;
            DbCommand dbCmd = db.GetSqlStringCommand(sql);
            int orgDocState = getLastDocState(db, docType, docID);

            db.AddInParameter(dbCmd, "ID", System.Data.DbType.Int32, id);
            db.AddInParameter(dbCmd, "DOC_TYPE", System.Data.DbType.String, docType);
            db.AddInParameter(dbCmd, "DOC_ID", System.Data.DbType.Int32, docID);
            db.AddInParameter(dbCmd, "DOC_ORG_SATE", System.Data.DbType.Int32, orgDocState);
            db.AddInParameter(dbCmd, "OP_STATE", System.Data.DbType.Int32, operateType);
            db.AddInParameter(dbCmd, "OP_USER_CODE", System.Data.DbType.String, user);
            db.AddInParameter(dbCmd, "REMARK", System.Data.DbType.String, remark);
            return MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteNonQuery(db, new DbCommand[] { dbCmd });     
        }
        /// <summary>
        ///  存储单据操作的状态改变操作记录。
        ///  这里有很多特殊的约束：要求主表必须有一个ID、DOC_STATE和LAST_MODIFIED_DATE，同时键值必须是Int32类型。
        /// </summary>
        /// <param name="stateInfo"></param>
        /// <returns></returns>
        public int SaveDocState(string docType, MB.Orm.Common.BaseModel docEntity, string remark, DocOperateType operateType) {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            return SaveDocState(db, docType, docEntity, remark, operateType);
        }
        /// <summary>
        /// 存储单据操作的状态改变操作记录。
        /// 这里有很多特殊的约束：要求主表必须有一个ID、DOC_STATE和LAST_MODIFIED_DATE，同时键值必须是Int32类型。
        /// </summary>
        /// <param name="db">连接的数据库</param>
        /// <param name="stateInfo">状态改变的记录信息</param>
        /// <returns>1表示成功，-1表示不成功</returns>
        public int SaveDocState(Database db, string docType, MB.Orm.Common.BaseModel docEntity, string remark, DocOperateType operateType) {
            DocStateTraceInfo stateInfo = new DocStateTraceInfo(docType, docEntity, remark, operateType);

            var databaseType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            //在提交之前先检查服务器日期
            MB.Orm.Persistence.EntityDistributedHelper.NewInstance.CheckEntityCanSave(docEntity);

            string sql = databaseType == MB.Orm.Enums.DatabaseType.Oracle ? SQL_INSERT_DOC_TRACE.Replace('@', ':') : SQL_INSERT_DOC_TRACE;
            DbCommand dbCmd = db.GetSqlStringCommand(sql);
            if (stateInfo.ID <= 0)
                stateInfo.ID = MB.Orm.Persistence.EntityIdentityHelper.NewInstance.GetEntityIdentity("SYS_DOC_STATE_CHANGED_TRACE");

            //@ID,@DOC_TYPE,@DOC_ID,@DOC_ORG_SATE,@OP_USER_CODE,@OP_DATE,@OP_STATE,@REMARK
            db.AddInParameter(dbCmd, "ID", System.Data.DbType.Int32, stateInfo.ID);
            db.AddInParameter(dbCmd, "DOC_TYPE", System.Data.DbType.String, stateInfo.DOC_TYPE);
            db.AddInParameter(dbCmd, "DOC_ID", System.Data.DbType.Int32, stateInfo.DOC_ID);
            db.AddInParameter(dbCmd, "DOC_ORG_SATE", System.Data.DbType.Int32, (int)stateInfo.DOC_ORG_SATE);
            db.AddInParameter(dbCmd, "OP_STATE", System.Data.DbType.Int32, (int)stateInfo.OP_STATE);
            db.AddInParameter(dbCmd, "OP_USER_CODE", System.Data.DbType.String, stateInfo.OP_USER_CODE);
            db.AddInParameter(dbCmd, "REMARK", System.Data.DbType.String, stateInfo.REMARK);
            //更改原表的单据状态
            string orgObjectSql = string.Format(SQL_UPDATE_DOC_STATE, stateInfo.OBJECT_TABLE_NAME);
            orgObjectSql = databaseType == MB.Orm.Enums.DatabaseType.Oracle ? orgObjectSql.Replace('@', ':') : orgObjectSql;
 
            DbCommand orgCmd = db.GetSqlStringCommand(orgObjectSql);
            db.AddInParameter(orgCmd, "DOC_STATE", System.Data.DbType.Int32, convertToDocState(db, stateInfo));
            db.AddInParameter(orgCmd, "DOC_ID", System.Data.DbType.Int32, stateInfo.DOC_ID);

            return MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteNonQuery(db, new DbCommand[] { dbCmd, orgCmd });     
        }

        #endregion public 方法...

        #region 内部函数处理...
        //获取挂起时的单据状态
        private int getSuspendDocState(Database db, string docType, int docID) {
            var databaseType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            string sql = databaseType == MB.Orm.Enums.DatabaseType.Oracle ? SQL_GET_ORG_DOC_STATE.Replace('@', ':') : SQL_GET_ORG_DOC_STATE;
            DbCommand dbCmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCmd, "DOC_TYPE", System.Data.DbType.String, docType);
            db.AddInParameter(dbCmd, "DOC_ID", System.Data.DbType.Int32, docID);
            db.AddInParameter(dbCmd, "OP_STATE", System.Data.DbType.Int32, (int)DocOperateType.Suspended);
            db.AddInParameter(dbCmd, "DOC_TYPE2", System.Data.DbType.String, docType);
            db.AddInParameter(dbCmd, "DOC_ID2", System.Data.DbType.Int32, docID);
            db.AddInParameter(dbCmd, "OP_STATE2", System.Data.DbType.Int32, (int)DocOperateType.Suspended);

            object re = db.ExecuteScalar(dbCmd);
            if (re == null || re == System.DBNull.Value)
                return (int)DocState.Progress;
            else
                return  MB.Util.MyConvert.Instance.ToInt(re);
        }
        //获取最后一次操作时的单据状态
        private int getLastDocState(Database db, string docType, int docID) {
            var databaseType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            string sql = databaseType == MB.Orm.Enums.DatabaseType.Oracle ? SQL_GET_LAST_DOC_STATE.Replace('@', ':') : SQL_GET_LAST_DOC_STATE;
            DbCommand dbCmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCmd, "DOC_TYPE", System.Data.DbType.String, docType);
            db.AddInParameter(dbCmd, "DOC_ID", System.Data.DbType.Int32, docID);
            db.AddInParameter(dbCmd, "DOC_TYPE2", System.Data.DbType.String, docType);
            db.AddInParameter(dbCmd, "DOC_ID2", System.Data.DbType.Int32, docID);

            object re = db.ExecuteScalar(dbCmd);
            if (re == null || re == System.DBNull.Value)
                return (int)DocState.Progress;
            else
                return MB.Util.MyConvert.Instance.ToInt(re);
        }

        //把操作的状态改变的操作后单据的状态。
        private int convertToDocState(Database db, DocStateTraceInfo stateInfo) {
            MB.Util.Model.DocOperateType operateType = stateInfo.OP_STATE;
            switch (operateType) {
                case DocOperateType.Approved:
                    return (int)MB.Util.Model.DocState.Approved;
                case DocOperateType.Completed:
                    return (int)MB.Util.Model.DocState.Completed; 
                case DocOperateType.Withdraw:
                    return (int)MB.Util.Model.DocState.Withdraw;  
                case DocOperateType.Suspended:
                    return (int)MB.Util.Model.DocState.Suspended;  
                case DocOperateType.CancelSuspended:
                    int orgDocState = getSuspendDocState(db, stateInfo.DOC_TYPE, stateInfo.DOC_ID);
                    return orgDocState;
                default:
                    throw new MB.Util.APPException(string.Format("当前单据的操作类型{0} 还没有进行相应的处理,请检查",operateType.ToString()), MB.Util.APPMessageType.SysErrInfo);
            }
        }
        #endregion 内部函数处理...
    }

    #region DocStateTraceInfo...
    /// <summary>
    /// 单据状态跟踪描述信息。
    /// </summary>
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("SYS_DOC_STATE_CHANGED_TRACE", new string[] { "ID" })]
    [KnownType(typeof(DocStateTraceInfo))]
    public class DocStateTraceInfo : MB.Orm.Common.BaseModel {
        #region 变量定义...
        private int _ID;
        private string _DOC_TYPE;
        private int _DOC_ID;
        private int _DOC_ORG_SATE;
        private string _OP_USER_CODE;
        private DateTime _OP_DATE;
        private DocOperateType _OP_STATE;
        private string _REMARK;
        private string _OBJECT_TABLE_NAME;

        #endregion 变量定义...

        #region 构造函数...
        /// <summary>
        /// 单据状态跟踪描述信息。
        /// </summary>
        public DocStateTraceInfo() {

        }
        /// <summary>
        /// 单据状态跟踪描述信息。
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="docType"></param>
        /// <param name="docID"></param>
        /// <param name="userCode"></param>
        /// <param name="remark"></param>
        /// <param name="operateType"></param>
        public DocStateTraceInfo(string tableName, string docType, int docID, string userCode,string remark, DocOperateType operateType) {
            _OBJECT_TABLE_NAME = tableName; 
            _DOC_TYPE = docType;
            _DOC_ID = docID;
            _OP_USER_CODE = userCode;
            _OP_STATE = operateType;
            _REMARK = remark;
        }
        /// <summary>
        /// 单据状态跟踪描述信息。
        /// </summary>
        /// <param name="docType"></param>
        /// <param name="docEntityInfo">单据对应的实体对象</param>
        /// <param name="remark"></param>
        /// <param name="operateType"></param>
        public DocStateTraceInfo(string docType, MB.Orm.Common.BaseModel docEntityInfo, string remark, DocOperateType operateType) {
            _DOC_TYPE = docType;
            _OP_STATE = operateType;
            _REMARK = remark;

            MB.Orm.Mapping.Att.ModelMapAttribute att = Attribute.GetCustomAttribute(docEntityInfo.GetType(), typeof(MB.Orm.Mapping.Att.ModelMapAttribute)) as MB.Orm.Mapping.Att.ModelMapAttribute;
            if (att == null)
                throw new MB.Util.APPException("在进行DocStateTrace 时对应的实体对象需要配置 MB.Orm.Mapping.Att.ModelMapAttribute", MB.Util.APPMessageType.SysErrInfo);

            _OBJECT_TABLE_NAME = att.TableName;
            _DOC_ID = MB.Util.MyReflection.Instance.InvokePropertyForGet<int>(docEntityInfo, "ID");
            _DOC_ORG_SATE =  MB.Util.MyReflection.Instance.InvokePropertyForGet<int>(docEntityInfo, "DOC_STATE");

            _OP_USER_CODE = MB.WcfService.MessageHeaderHelper.GetCurrentLoginUser().USER_CODE;
   
        }
        #endregion 构造函数...

        #region public 属性...
        /// <summary>
        /// 键值ID
        /// </summary>
        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }
        /// <summary>
        /// 单据类型。
        /// </summary>
        public string DOC_TYPE {
            get { return _DOC_TYPE; }
            set { _DOC_TYPE = value; }
        }
        /// <summary>
        /// 单据对应的键值ID
        /// </summary>
        public int DOC_ID {
            get { return _DOC_ID; }
            set { _DOC_ID = value; }
        }
        /// <summary>
        /// 操作时单据的原始状态
        /// </summary>
        public int DOC_ORG_SATE {
            get { return _DOC_ORG_SATE; }
            set { _DOC_ORG_SATE = value; }
        }
        /// <summary>
        /// 操作用户编码
        /// </summary>
        public string OP_USER_CODE {
            get { return _OP_USER_CODE; }
            set { _OP_USER_CODE = value; }
        }
        /// <summary>
        /// 操作日期（包含时间）
        /// </summary>
        public DateTime OP_DATE {
            get { return _OP_DATE; }
            set { _OP_DATE = value; }
        }
        /// <summary>
        /// 当前操作后的状态
        /// </summary>
        public DocOperateType OP_STATE {
            get { return _OP_STATE; }
            set { _OP_STATE = value; }
        }
        /// <summary>
        /// 备注。
        /// </summary>
        public string REMARK {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        /// <summary>
        /// 对象表名称。
        /// </summary>
        public string OBJECT_TABLE_NAME {
            get { return _OBJECT_TABLE_NAME; }
            set { _OBJECT_TABLE_NAME = value; }
        }

        #endregion public 属性...
    }
    #endregion DocStateTraceInfo...
}
