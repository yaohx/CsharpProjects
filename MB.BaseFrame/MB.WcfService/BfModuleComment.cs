//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	 2009-08-24 10:39 
// Description	:	模块使用情况评论。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Data;
using System.Data.Common;

using MB.WcfService.IFace;

using MB.Util.Model;

namespace MB.WcfService {
    /// <summary> 
    /// 文件生成时间 2009-08-24 10:39 
    /// 模块使用情况评论
    /// </summary> 
    [MB.Aop.InjectionManager]
    [MB.WcfService.WcfFaultBehavior(typeof(MB.WcfService.WcfFaultExceptionHandler))]
    public class BfModuleComment :  IBfModuleComment {
 
        /// <summary> 
        /// 构造函数。 
        /// </summary> 
        public BfModuleComment() {
        }

        #region IBfModuleComment 成员
        /// <summary>
        /// 获取模块评论的信息。
        /// </summary>
        /// <param name="xmlFilterParams"></param>
        /// <returns></returns>
        public List<MB.Util.Model.BfModuleCommentInfo> GetObjects(string xmlFilterParams) {
            string sql = MB.WcfService.Properties.Resources.BfModuleComment_Sql_SelectObject;
            List<MB.Util.Model.BfModuleCommentInfo> lstData = new List<BfModuleCommentInfo>();
            var pars = MB.Util.Serializer.QueryParameterXmlSerializer.DefaultInstance.DeSerializer(xmlFilterParams);
            string sqlFilter = MB.Orm.DbSql.SqlShareHelper.Instance.QueryParametersToSqlString(null, pars);
            sql = string.Format(sql, sqlFilter);
            using (IDataReader reader = MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteReader(sql)) {
                while (reader.Read()) {
                    MB.Util.Model.BfModuleCommentInfo newInfo = new BfModuleCommentInfo();
                    newInfo.ID = int.Parse(reader["ID"].ToString());
                    newInfo.APPLICATION_IDENTITY = reader["APPLICATION_IDENTITY"].ToString();
                    newInfo.MODULE_IDENTITY = reader["MODULE_IDENTITY"].ToString();
                    newInfo.COMMENT_TYPE = reader["COMMENT_TYPE"].ToString();
                    newInfo.COMMENT_CONTENT = (byte[])reader["COMMENT_CONTENT"];
                    newInfo.USER_ID = reader["USER_ID"].ToString();
                    newInfo.CREATE_DATE = (DateTime)reader["CREATE_DATE"];

                    lstData.Add(newInfo); 
                }
            }
            return lstData; 

        }
        /// <summary>
        /// 模块评论信息保存。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        public int AddObjectImmediate(MB.Util.Model.BfModuleCommentInfo entity) {
            string sql = MB.WcfService.Properties.Resources.BfModuleComment_Sql_AddObject;
            List<MB.Orm.DbSql.SqlParamInfo> pars = new List<MB.Orm.DbSql.SqlParamInfo>();
            entity.ID = MB.Orm.Persistence.EntityIdentityHelper.NewInstance.GetEntityIdentity("BF_MODULE_COMMENT");     
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("ID", entity.ID, DbType.Int32));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("APPLICATION_IDENTITY", entity.APPLICATION_IDENTITY, DbType.String));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("MODULE_IDENTITY", entity.MODULE_IDENTITY, DbType.String));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("COMMENT_TYPE", entity.COMMENT_TYPE, DbType.String));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("COMMENT_CONTENT", entity.COMMENT_CONTENT, DbType.Binary));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("USER_ID", entity.USER_ID, DbType.String));
            return MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteNonQuery(sql, pars); 
        }

        #endregion

        #region IBfModuleComment 成员

       /// <summary>
       /// 直接删除指定的评语信息。
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public int DeletedImmediate(int id) {
            string sql = MB.WcfService.Properties.Resources.BfModuleComment_Sql_DeleteObject;
            List<MB.Orm.DbSql.SqlParamInfo> pars = new List<MB.Orm.DbSql.SqlParamInfo>();
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("ID", id, DbType.Int32));
            return MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteNonQuery(sql, pars); 
        }
        /// <summary>
        /// 清除指定模块的所有评语
        /// </summary>
        /// <param name="applicationIdentity"></param>
        /// <param name="moduleIdentity"></param>
        /// <returns></returns>
        public int ClearImmediate(string applicationIdentity, string moduleIdentity) {
            string sql = MB.WcfService.Properties.Resources.BfModuleComment_Sql_ClearObject;
            List<MB.Orm.DbSql.SqlParamInfo> pars = new List<MB.Orm.DbSql.SqlParamInfo>();
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("APPLICATION_IDENTITY", applicationIdentity, DbType.String));
            pars.Add(new MB.Orm.DbSql.SqlParamInfo("MODULE_IDENTITY", moduleIdentity, DbType.String));
            return MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteNonQuery(sql, pars); 
        }
        #endregion
    } 

}
