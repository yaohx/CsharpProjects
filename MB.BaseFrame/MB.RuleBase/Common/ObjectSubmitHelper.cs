//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-05-18
// Description	:	对象提交封装处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.EnterpriseServices;

using MB.Orm.Enums;
using MB.RuleBase.Atts;
using MB.Util.Model;
using MB.RuleBase.IFace;
using MB.Orm.Persistence;
namespace MB.RuleBase.Common {
    /// <summary>
    /// 对象提交封装处理。
    /// </summary>
    public class ObjectSubmitHelper {
        public static readonly string ENTITY_STATE_PROPERTY = "EntityState";
        public static readonly string ENTITY_DOC_STATE = "DOC_STATE";
        public static readonly string ENTITY_CREATE_USER = "CREATE_USER";
        public static readonly string ENTITY_LAST_MODIFIED_USER = "LAST_MODIFIED_USER";
        public static readonly string ENTITY_LAST_MODIFIED_DATE = "LAST_MODIFIED_DATE";

        private DatabaseExecuteHelper _DbExexuteHelper;
        /// <summary>
        /// 对象提交封装处理。
        /// </summary>
        public ObjectSubmitHelper() {
            _DbExexuteHelper = new DatabaseExecuteHelper();
        }

        /// <summary>
        /// 返回新的对象实例。
        /// </summary>
        public static ObjectSubmitHelper NewInstance {
            get {
                return new ObjectSubmitHelper();
            }
        }

        #region CheckParentHasSubmit...
        /// <summary>
        /// 判断该业务对象对应的键值是否已经进行了提交处理。
        /// 区别于CheckHasSubmit 最主要的部分是增加和是否审核通过的判断。
        /// </summary>
        /// <param name="dbCmd"></param>
        /// <param name="objectName"></param>
        /// <param name="keyField"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool CheckParentHasSubmit(Database db, string tableName, string keyField, int[] ids) {
            string filter = MB.Orm.DbSql.SqlShareHelper.Instance.BuildQueryInSql(ids, keyField);
            string sql = string.Format("SELECT {0} FROM {1} WHERE {2} IN('1','2') AND {3})", ENTITY_DOC_STATE, tableName, ENTITY_DOC_STATE, filter);
            DbCommand dbCmd = db.GetSqlStringCommand(sql);
            DataSet ds = _DbExexuteHelper.ExecuteDataSet(db,dbCmd);
            if (ds.Tables[0].Rows.Count != ids.Length)//如果不相等，表示有部分数据已经被删除
                throw new MB.Util.APPException("该对象使用的数据已经不存在或者不在提交状态。", MB.Util.APPMessageType.DisplayToUser);
            foreach (DataRow dr in ds.Tables[0].Rows) {
                if (MB.Util.MyConvert.Instance.ToInt(dr[ENTITY_DOC_STATE]) != 1)
                    throw new MB.Util.APPException("该对象使用的数据已经不存在或者不在提交状态。", MB.Util.APPMessageType.DisplayToUser);
            }
            return true;
        }
        /// <summary>
        /// 判断该业务对象对应的键值是否已经进行了提交处理。
        /// 区别于CheckHasSubmit 最主要的部分是增加和是否审核通过的判断。
        /// </summary>
        /// <param name="dbCmd"></param>
        /// <param name="tableName"></param>
        /// <param name="keyField"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool CheckParentHasSubmit(Database db, string tableName, string keyField, string keyValue) {
            string sql = string.Format("SELECT {0} FROM {1} WHERE {2} IN('1','2') AND {3} ='{4}'", ENTITY_DOC_STATE, tableName, ENTITY_DOC_STATE, keyField, keyValue);
            DbCommand dbCmd = db.GetSqlStringCommand(sql);
            object val = _DbExexuteHelper.ExecuteScalar(db,dbCmd);
            if (val == null || val == System.DBNull.Value)
                throw new MB.Util.APPException("该对象使用的数据已经不存在或者不在提交状态。", MB.Util.APPMessageType.DisplayToUser);
            int re = MB.Util.MyConvert.Instance.ToInt(val);
            if (re < 1) {
                throw new MB.Util.APPException("该对象使用的数据已经不存在或者不在提交状态。", MB.Util.APPMessageType.DisplayToUser);
            }
            return true;
        }

        /// <summary>
        /// 判断该业务对象对应的键值是否已经进行了提交处理。
        /// 区别于CheckHasSubmit 最主要的部分是增加和是否审核通过的判断。
        /// </summary>
        /// <param name="dbCmd"></param>
        /// <param name="busObj"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool CheckParentHasSubmit(Database db, MB.RuleBase.IFace.IBaseRule baseRule, object mainEntity) {
            RuleSettingAttribute ruleSettAtt = MB.RuleBase.Atts.AttributeConfigHelper.Instance.GetRuleSettingAtt(baseRule);
            //如果用户没有配置那么表示不需要上级引用的约束控制
            if (ruleSettAtt == null || ruleSettAtt.BaseDataType == null) return true;
            //providerAtt.BaseDataType 
            ParentProviderAttribute providerAtt = MB.RuleBase.Atts.AttributeConfigHelper.Instance.GetParentProviderAttByType(ruleSettAtt.BaseDataType);
            if (providerAtt == null || providerAtt.ProviderTableName == null || providerAtt.ProviderTableName.Length == 0)
                return true;
            if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(mainEntity, providerAtt.ForeingKeyField))
                throw new MB.Util.APPException(string.Format("在检查引用上级对象时 属性{0}在对象{1}中不存在。请检查配置信息！", providerAtt.ForeingKeyField, mainEntity.GetType().FullName), MB.Util.APPMessageType.DisplayToUser);

            object refrenceKeyValue = MB.Util.MyReflection.Instance.InvokePropertyForGet(mainEntity, providerAtt.ForeingKeyField);
            return CheckParentHasSubmit(db, providerAtt.ProviderTableName, providerAtt.ProviderKeyName, refrenceKeyValue.ToString());
        }
        #endregion CheckParentHasSubmit...

        #region ObjectOwnerless...
 
        /// <summary>
        /// 检查当前对象是否已被其它对象引用。
        /// </summary>
        /// <param name="db"></param>
        /// <param name="baseRule"></param>
        /// <param name="mainEntity"></param>
        /// <param name="restrictOption"></param>
        /// <returns></returns>
        public bool ObjectOwnerless(Database db, MB.RuleBase.IFace.IBaseRule baseRule, object mainEntity,LinkRestrictOption restrictOption) {
            if (restrictOption == LinkRestrictOption.CancelSubmit) {
                if (!CheckExistsDocState(mainEntity)) return true;

                var docState = GetEntityDocState(mainEntity);

                if (docState == DocState.Progress) return true;
 
            }
            RuleSettingAttribute ruleSettAtt = MB.RuleBase.Atts.AttributeConfigHelper.Instance.GetRuleSettingAtt(baseRule);
            //如果用户没有配置那么表示不需要上级引用的约束控制
            if (ruleSettAtt == null || ruleSettAtt.BaseDataType == null) return true;

            NextOwnAttribute[] atts = MB.RuleBase.Atts.AttributeConfigHelper.Instance.GetNextOwnAttByType(ruleSettAtt.BaseDataType);
            if (atts == null || atts.Length == 0)
                return true;


            List<DbCommand> cmds = new List<DbCommand>();
            string objDescription = string.Empty;
            MB.Orm.Mapping.ModelMappingInfo mappingInfo = null;
            string keyName = MB.Orm.Mapping.AttMappingManager.Instance.GetPrimaryKey(mainEntity, ref mappingInfo);

            object keyValue = MB.Util.MyReflection.Instance.InvokePropertyForGet(mainEntity, keyName);

            foreach (NextOwnAttribute att in atts) {
                //只有配置成 阻止撤消提交的，才进行这样的处理
                if (att.RestrictOption != restrictOption) continue;
 
                if (string.IsNullOrEmpty(att.CfgXmlSqlName)) {
                    string sql = string.Format("SELECT 1 FROM {0} WHERE {1}='{2}'", att.OwnTableName, att.OwnFieldName, keyValue.ToString());
                    if (!string.IsNullOrEmpty(att.OwnFilter)) {
                        sql += string.Format(att.OwnFilter, keyValue.ToString());
                    }
                    DbCommand tempCmd = db.GetSqlStringCommand(sql);
                    cmds.Add(tempCmd);
                }
                else {

                    System.Data.Common.DbCommand[] tempCmds = MB.Orm.Persistence.PersistenceManagerHelper.NewInstance.CreateDbCommandByXml(db,
                                                                                   mappingInfo.XmlConfigFileName, att.CfgXmlSqlName, keyValue);

                    if (tempCmds.Length != 1)
                        throw new MB.RuleBase.Exceptions.SelectSqlXmlConfigException(mappingInfo.XmlConfigFileName, att.CfgXmlSqlName);

                    cmds.AddRange(tempCmds);
                }

                if (objDescription.Length == 0)
                    objDescription = "<<" + att.OwnDescription + ">>";
                else
                    objDescription += "或 <<" + att.OwnDescription + ">>";
            }
            foreach (DbCommand dbCmd in cmds) {
                object val = _DbExexuteHelper.ExecuteScalar(db,dbCmd);
                if (val != null && val != System.DBNull.Value)
                    throw new MB.Util.APPException(string.Format("由于该对象已生成{0}，不能再进行重做。", objDescription), 
                                                                                    MB.Util.APPMessageType.DisplayToUser);

            }

            return true;
        }
        #endregion ObjectOwnerless...

        /// <summary>
        /// 提交指定的业务处理数据。
        /// </summary>
        /// <param name="baseRule"></param>
        /// <param name="mainEntity"></param>
        /// <returns></returns>
        public int ObjectSubmit(MB.RuleBase.IFace.IBaseRule baseRule, object mainEntity) {
            MB.Orm.Mapping.ModelMappingInfo mappingInfo = null;
            string keyName = MB.Orm.Mapping.AttMappingManager.Instance.GetPrimaryKey(mainEntity, ref mappingInfo);


            object keyValue = MB.Util.MyReflection.Instance.InvokePropertyForGet(mainEntity, keyName);
            string sql = string.Format("UPDATE  {0} SET {1} = '1',{2} = SYSTIMESTAMP WHERE {3} = '{4}' ", mappingInfo.MapTable, ENTITY_DOC_STATE, ENTITY_LAST_MODIFIED_DATE, keyName, keyValue.ToString());

            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase(); 

            //在提交之前先检查服务器日期
            MB.Orm.Persistence.EntityDistributedHelper.NewInstance.CheckEntityCanSave(mainEntity);

            DbCommand cmd = db.GetSqlStringCommand(sql);

            _DbExexuteHelper.ExecuteNonQuery(db, cmd); 

            return 1;
        }
        /// <summary>
        /// 撤消对象的提交处理相关...
        /// </summary>
        /// <param name="baseRule"></param>
        /// <param name="mainEntity"></param>
        /// <returns></returns>
        public int ObjectCancelSubmit(MB.RuleBase.IFace.IBaseRule baseRule, object mainEntity) {
            MB.Orm.Mapping.ModelMappingInfo mappingInfo = null;
            string keyName = MB.Orm.Mapping.AttMappingManager.Instance.GetPrimaryKey(mainEntity, ref mappingInfo);

            object keyValue = MB.Util.MyReflection.Instance.InvokePropertyForGet(mainEntity, keyName);
            string sql = string.Format("UPDATE  {0} SET {1} = '0',{2} = SYSTIMESTAMP WHERE {3} = '{4}' ", mappingInfo.MapTable, ENTITY_DOC_STATE, ENTITY_LAST_MODIFIED_DATE, keyName, keyValue.ToString());

            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase(); 

            //在提交之前先检查服务器日期
            MB.Orm.Persistence.EntityDistributedHelper.NewInstance.CheckEntityCanSave(mainEntity);

            //检查是否已存在下级引用对象。
            ObjectOwnerless(db, baseRule, mainEntity, LinkRestrictOption.CancelSubmit);

            DbCommand cmd = db.GetSqlStringCommand(sql);

            _DbExexuteHelper.ExecuteNonQuery(db, cmd); 
            return 1;
        }
        /// <summary>
        /// 获取实体对象的单据状态。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public MB.Util.Model.DocState GetEntityDocState(object entity) {
            object val2 = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, ENTITY_DOC_STATE);
            MB.Util.Model.DocState docState = (MB.Util.Model.DocState)Enum.Parse(typeof(MB.Util.Model.DocState), val2.ToString());
            return docState;
        }
        /// <summary>
        /// 判断指定的类型中是否包含单据状态类型。
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public bool CheckTypeExistsDocState(Type entityType) {
            return MB.Util.MyReflection.Instance.CheckTypeExistsProperty(entityType, ENTITY_DOC_STATE);
        }
        /// <summary>
        /// 判断实体对象的是否存在单据状态。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool CheckExistsDocState(object entity) {
            return MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, ENTITY_DOC_STATE);
        }
    }
}
