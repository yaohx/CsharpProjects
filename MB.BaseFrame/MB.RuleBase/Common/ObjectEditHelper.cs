//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-03
// Description	:	ObjectDataState 标记数据在集合中的状态。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.EnterpriseServices;
using MB.Orm.Enums;
using MB.RuleBase.Atts;
using MB.Util.Model;
using MB.RuleBase.IFace;
using MB.Orm.Persistence;
using MB.Orm.Common;
using MB.Orm.DB;
namespace MB.RuleBase.Common {
    /// <summary>
    /// 业务对象编辑处理相关。
    /// [Transaction(TransactionOption.Required)]  
    /// </summary>
    public class ObjectEditHelper {
        private QueryBehavior _QueryBehavior;

        #region 构造函数...
        /// <summary>
        /// 构造函数。
        /// </summary>
        public ObjectEditHelper() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queryBehavior"></param>
        public ObjectEditHelper(QueryBehavior queryBehavior)
        {
            _QueryBehavior = queryBehavior;
        }
        #endregion

        /// <summary>
        /// 默认安全的单实例模式。
        /// 由于该类中的方法都需要占用大量的时间,建议外部直接通过New 的方式来进行调用。
        /// </summary>
        public static ObjectEditHelper DefalutInstance {
            get {
                return MB.Util.SingletonProvider<ObjectEditHelper>.Instance;
            }
        }
        /// <summary>
        /// 重新刷新实体对象。
        /// 1)应该先找对应配置的XML 文件中是否已经配置了 RefreshEntity 
        /// 2)如果用户没有配置再根据 GetObjects 查找
        /// </summary>
        /// <param name="baseRule"></param>
        /// <param name="dataInDocType"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public object RefreshEntity(IBaseRule baseRule, object dataInDocType, object entity,params object[] parValues) {
            string keyPropertyName = string.Empty;
            object key = MB.Orm.Persistence.EntityDataHelper.Instance.GetEntityKeyValue(entity, out keyPropertyName);
            MB.Util.Model.QueryParameterInfo[] pars = new QueryParameterInfo[1];
            pars[0] = new QueryParameterInfo(keyPropertyName, key, MB.Util.DataFilterConditions.Equal);
            List<object> lstData = GetObjects<object>(baseRule, dataInDocType, pars, parValues);
            if (lstData == null || lstData.Count == 0)
                return null;
            else
                return lstData[0];
        }

        #region CreateNewEntity...

        /// <summary>
        /// 根据类型创建一个新的实体对象。    CreateNewEntityBatch
        /// </summary>
        /// <param name="baseRule">指定操作的业务类。</param>
        /// <param name="dataInDocType">指定的数据类型。</param>
        /// <returns>数据实体</returns>
        public object CreateNewEntity(IBaseRule baseRule, object dataInDocType) {
            ObjectDataMappingAttribute mappingAtt = AttributeConfigHelper.Instance.GetObjectDataMappingAttribute(dataInDocType);
            if (mappingAtt == null)
                throw new MB.RuleBase.Exceptions.RequireConfigDataMappingException(dataInDocType);
            Type entityType = mappingAtt.EntityType;
            if (string.Compare(entityType.FullName, "System.Object", true) == 0) {
                if (mappingAtt.EntityType == null)
                    throw new MB.Util.APPException("业务类:" + baseRule.GetType().FullName + " 中的:" +
                                                    dataInDocType.GetType() + " 的数据值:" + dataInDocType.ToString() +
                                                    " 在配置ObjectDataMapping时需要配置 EntityType", MB.Util.APPMessageType.SysErrInfo);
            }
            object entity = MB.Util.DllFactory.Instance.CreateInstance(entityType);
            //判断是否为自增列
            if (mappingAtt.KeyIsSelfAdd) {
                int id = MB.Orm.Persistence.EntityIdentityHelper.NewInstance.GetEntityIdentity(mappingAtt.MappingTableName);
                MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, mappingAtt.KeyName, id);
            }
            if (entity.GetType().IsSubclassOf(typeof(MB.Orm.Common.BaseModel))) {
                (entity as MB.Orm.Common.BaseModel).EntityState = EntityState.New;
            }
            //设置默认值
            MB.Orm.Mapping.ModelMappingInfo mappingInfo = MB.Orm.Mapping.AttMappingManager.Instance.GetModelMappingInfo(entityType);
            foreach (MB.Orm.Mapping.FieldPropertyInfo colProperty in mappingInfo.FieldPropertys.Values) {
                if (colProperty.DefaultValue == null) continue;

                System.Reflection.PropertyInfo proInfo = entityType.GetProperty(colProperty.PropertyName);
                proInfo.SetValue(entity, colProperty.DefaultValue, null);
            }

            return entity;
        }
        /// <summary>
        /// 批量创建实体对象。
        /// </summary>
        /// <param name="baseRule"></param>
        /// <param name="dataInDocType"></param>
        /// <param name="createCount"></param>
        /// <returns></returns>
        public IList CreateNewEntityBatch(IBaseRule baseRule, object dataInDocType,int createCount) {
            ObjectDataMappingAttribute mappingAtt = AttributeConfigHelper.Instance.GetObjectDataMappingAttribute(dataInDocType);
            if (mappingAtt == null)
                throw new MB.RuleBase.Exceptions.RequireConfigDataMappingException(dataInDocType);
            Type entityType = mappingAtt.EntityType;
            if (string.Compare(entityType.FullName, "System.Object", true) == 0) {
                if (mappingAtt.EntityType == null)
                    throw new MB.Util.APPException("业务类:" + baseRule.GetType().FullName + " 中的:" +
                                                    dataInDocType.GetType() + " 的数据值:" + dataInDocType.ToString() +
                                                    " 在配置ObjectDataMapping时需要配置 EntityType", MB.Util.APPMessageType.SysErrInfo);
            }
            List<object> lstData = new List<object>();
            int id = 0;
           //判断是否为自增列
            if (mappingAtt.KeyIsSelfAdd) {
                id = MB.Orm.Persistence.EntityIdentityHelper.NewInstance.GetEntityIdentity(mappingAtt.MappingTableName, createCount);
            }
            for (int i = 0; i < createCount; i++) {
                object entity = MB.Util.DllFactory.Instance.CreateInstance(entityType);
                //判断是否为自增列
                if (id > 0) {
                    MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, mappingAtt.KeyName, id++);
                }
                if (entity.GetType().IsSubclassOf(typeof(MB.Orm.Common.BaseModel))) {
                    (entity as MB.Orm.Common.BaseModel).EntityState = EntityState.New;
                }
                //设置默认值
                MB.Orm.Mapping.ModelMappingInfo mappingInfo = MB.Orm.Mapping.AttMappingManager.Instance.GetModelMappingInfo(entityType);
                foreach (MB.Orm.Mapping.FieldPropertyInfo colProperty in mappingInfo.FieldPropertys.Values) {
                    if (colProperty.DefaultValue == null) continue;

                    System.Reflection.PropertyInfo proInfo = entityType.GetProperty(colProperty.PropertyName);
                    proInfo.SetValue(entity, colProperty.DefaultValue, null);
                }
                lstData.Add(entity);
            }
            return lstData;
        }
        #endregion CreateNewEntity...

        #region 对象永久化存储处理相关...
        /// <summary>
        /// 把保存在ObjectDataList 持久化到数据库中。
        /// 特殊说明： 暂时先在这里进行服务的提交处理，以后需要进行修改。
        /// </summary>
        /// <param name="baseRule">当前进行持久化操作的业务类。</param>
        /// <param name="dataList">存储进行数据库操作的ObjectDataList。</param>
        /// <returns>返回受影响的行。</returns>
        public int SaveObjectDataList(IBaseRule baseRule, ObjectDataList dataList) {
            if (dataList == null || dataList.Count == 0)
                return 0;

            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase(); 

            IList<EntityDbCommandInfo> cmds = CreateCmdsFromObjectDataList(baseRule, db, dataList);
            if (cmds.Count == 0)
                return 0;
            bool succeed = false;
            int count = 0;
            List<DbCommand> tempCmds = new List<DbCommand>();
            //using (DbConnection connection = db.CreateConnection()) {
            //    connection.Open();
            try {
                foreach (EntityDbCommandInfo cmd in cmds) {
                    try {
                        if (cmd.IsMainEntity) {
                            
                            //判断该实体对象是否允许保存。
                            MB.Orm.Persistence.EntityDistributedHelper.NewInstance.CheckEntityCanSave(cmd.DataEntity);
                            if (baseRule != null) {
                                //判断引用的上级对象是否还在提交状态
                                MB.RuleBase.Common.ObjectSubmitHelper.NewInstance.CheckParentHasSubmit(db, baseRule, cmd.DataEntity);
                                //如果要删除主题单据的话，需要进行下级数据的引用判断。
                                if (cmd.OperationType == OperationType.Delete) {
                                    //检查是否已存在下级引用对象。
                                    ObjectSubmitHelper.NewInstance.ObjectOwnerless(db, baseRule, cmd.DataEntity, LinkRestrictOption.CancelSubmit);
                                }
                            }
                        }
                        string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmd.DbCommand);
                        MB.Util.TraceEx.Write("正在执行:" + cmdMsg);
                        int re = 0;
                        using (new Util.MethodTraceWithTime(true, cmdMsg))
                        {
                            re = db.ExecuteNonQuery(cmd.DbCommand);
                        }
                        //判断是否为部分属性修改，如果是的话，那么需要修改最后修改时间。
                        if (re > 0 && cmd.IsPartPropertyUpdate) {
                            DbCommand tmpCmd = MB.Orm.Persistence.EntityDistributedHelper.NewInstance.GetSaveLastModifiedDateCommand(db, cmd.DataEntity);
                            if (tmpCmd != null) {
                                db.ExecuteNonQuery(tmpCmd);
                                tempCmds.Add(tmpCmd);
                            }
                        }
                        count += re;

                    }
                    catch (Exception ex) {
                        //throw new MB.RuleBase.Exceptions.DatabaseExecuteException("ExecuteNonQuery 出错！", ex);
                        throw;
                    }
                }
                succeed = true;
            }
            catch (MB.RuleBase.Exceptions.DatabaseExecuteException dex) {
                throw;
            }
            catch (Exception ex) {
                //throw new MB.RuleBase.Exceptions.DatabaseExecuteException("SaveObjectDataList 出错！", ex);
                throw;
            }
            finally {
                //if (connection.State == System.Data.ConnectionState.Open)
                //    connection.Close();

                try {
                    foreach (EntityDbCommandInfo cmd in cmds)
                        cmd.DbCommand.Dispose();
                    foreach (DbCommand m in tempCmds)
                        m.Dispose();
                }
                catch { }
            }
            //if(succeed)
            //    dataList.AcceptDataChanges();

            return count;
            //  }
        }

        /// <summary>
        /// 根据键值直接执行业务类中指定数据类型的数据。
        ///  特殊说明： 暂时先在这里进行服务的提交处理，以后需要进行修改。
        /// </summary>
        /// <param name="baseRule">指定操作的业务类。</param>
        /// <param name="dataInDocType">数据在业务类的中数据类型。</param>
        /// <param name="key">指定需要删除的键值。</param>
        /// <returns>返回受影响的行。</returns>
        public int DeletedImmediate(IBaseRule baseRule, object dataInDocType, object key) {
            ObjectDataMappingAttribute mappingAtt = AttributeConfigHelper.Instance.GetObjectDataMappingAttribute(dataInDocType);
            if (mappingAtt == null)
                throw new MB.RuleBase.Exceptions.RequireConfigDataMappingException(dataInDocType);

            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase(); 

            System.Data.Common.DbCommand[] cmds = MB.Orm.Persistence.PersistenceManagerHelper.NewInstance.CreateDbCommandByXml(db,
                                               mappingAtt.MappingXmlFileName, MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_DELETE_OBJECT, key);


            int count = 0;
            //using (DbConnection connection = db.CreateConnection()) {
            //    connection.Open();
            try {

                foreach (System.Data.Common.DbCommand cmd in cmds) {
                    string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmd);
                    MB.Util.TraceEx.Write("正在执行:" + cmdMsg);
                    using (new Util.MethodTraceWithTime(true, cmdMsg))
                    {
                        count = db.ExecuteNonQuery(cmd);
                    }
                }

            }
            catch (Exception ex) {
                //throw new MB.RuleBase.Exceptions.DatabaseExecuteException("DeletedImmediate 出错！", ex);
                throw;
            }
            finally {
                //if (connection.State == System.Data.ConnectionState.Open)
                //    connection.Close();

                try {
                    foreach (System.Data.Common.DbCommand cmd in cmds)
                        cmd.Dispose();
                }
                catch { }
            }
            return count;
            //  }
        }
        /// <summary>
        /// 通过DataSet 直接存储到数据库,排除已经删除的行,对于需要删除的行请直接通过 DeletedImmediate 来进行。
        ///  特殊说明： 暂时先在这里进行服务的提交处理，以后需要进行修改。
        /// </summary>
        /// <param name="baseRule">当前需要操作的业务类。</param>
        /// <param name="dataInDocType">数据在业务类的中数据类型。</param>
        /// <param name="dsData">DataSet 类型数据。</param>
        /// <returns>返回受影响的行。</returns>
        public int SaveDataSetImmediate(IBaseRule baseRule, object dataInDocType, System.Data.DataSet dsData) {
            ObjectDataMappingAttribute mappingAtt = AttributeConfigHelper.Instance.GetObjectDataMappingAttribute(dataInDocType);
            if (mappingAtt == null)
                throw new MB.RuleBase.Exceptions.RequireConfigDataMappingException(dataInDocType);
            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase(); 

            IList<EntityDbCommandInfo> cmds = CreateCmdsFromDataSet(db, baseRule, dsData, null, dataInDocType);
            if (cmds.Count == 0)
                return 0;

            int count = 0;
            //using (DbConnection connection = db.CreateConnection()) {
            //    connection.Open();
            try {
                foreach (EntityDbCommandInfo cmd in cmds) {
                    try {
                        string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmd.DbCommand);
                        MB.Util.TraceEx.Write("正在执行:" + cmdMsg);
                        using (new Util.MethodTraceWithTime(true, cmdMsg))
                        {
                            count += db.ExecuteNonQuery(cmd.DbCommand);
                        }
                    }
                    catch (Exception ex) {
                        throw new MB.RuleBase.Exceptions.DatabaseExecuteException("SaveDataSetImmediate 出错！", ex);
                    }
                }
            }
            catch (MB.RuleBase.Exceptions.DatabaseExecuteException dx) {
                throw;
            }
            catch (Exception exx) {
                throw;
                //throw new MB.RuleBase.Exceptions.DatabaseExecuteException("SaveDataSetImmediate 出错！", exx);
            }
            finally {
                //if (connection.State == System.Data.ConnectionState.Open)
                //    connection.Close();

                try {
                    foreach (EntityDbCommandInfo cmd in cmds)
                        cmd.DbCommand.Dispose();
                }
                catch { }
            }
            return count;
            //  }

        }

        #endregion 对象永久化存储处理相关...

        #region Create DbCommand 处理相关...
        /// <summary>
        /// 从ObjectDataList 创建可进行数据库操作的DbCommand。
        /// </summary>
        /// <param name="baseRule">指定操作的业务类(为空将根据集合中的实体类型中的配置信息来自动创建DBCommand)。</param>
        /// <param name="db">数据库操作DB.</param>
        /// <param name="dataList">数据</param>
        /// <returns></returns>
        public IList<EntityDbCommandInfo> CreateCmdsFromObjectDataList(IBaseRule baseRule, Database db, ObjectDataList dataList) {
            RuleSettingAttribute mappingAtt = null;
            if(baseRule!=null)
                mappingAtt = AttributeConfigHelper.Instance.GetRuleSettingAtt(baseRule);
            if (mappingAtt!=null && mappingAtt.BaseDataType == null)
                throw new MB.Util.APPException("业务类:" + baseRule.GetType().FullName + " 必须要先配置主表数据在单据中的类型值。", MB.Util.APPMessageType.SysErrInfo);

            //获取存储变化的数据对象。
            IEnumerable<ObjectDataInfo> vals = dataList.GetCanSaveAndOrder(); //必须要进行排序以获取主表的键值
            List<EntityDbCommandInfo> cmds = new List<EntityDbCommandInfo>();
            object mainKeyValue = null;
            bool currentIsMain = false;
            MB.Orm.Persistence.PersistenceManagerHelper persistenceManager = MB.Orm.Persistence.PersistenceManagerHelper.NewInstance;
            foreach (ObjectDataInfo dataInfo in vals) {
                if (mappingAtt!=null && (int)dataInfo.DataInDocType == (int)mappingAtt.BaseDataType) {//获取基本对象的主键值。
                    if (mappingAtt.GenerateKeyModel == GenerateKeyModel.OnDataSave) {
                        //设置对象的主键
                        MB.Orm.Persistence.EntityIdentityHelper.NewInstance.FillEntityIdentity(dataInfo.ObjectData);
                    }

                    mainKeyValue = GetObjectKeyValue(baseRule, dataInfo);
                    currentIsMain = true;
                }
                else {
                    currentIsMain = false;
                }

                object data = dataInfo.ObjectData;

                //MB.Orm.Enums.OperationType operationType = ConvertDataStateToOperationType(dataInfo.DataState);
                //if (operationType == MB.Orm.Enums.OperationType.None)
                //    continue;

                if (data == null)
                    continue;

                if (data.GetType().IsSubclassOf(typeof(MB.Orm.Common.BaseModel))) {

                    DbCommand[] acmd = null;
                    if (string.IsNullOrEmpty(dataInfo.ExecuteXmlCfgSqlName)) {
                        MB.Orm.Enums.OperationType operationType = ConvertEntityStateToOperationType((data as MB.Orm.Common.BaseModel).EntityState);
                        if (operationType == MB.Orm.Enums.OperationType.None)
                            continue;
                        acmd = persistenceManager.GetDbCommand(db, data as MB.Orm.Common.BaseModel,
                                                                                 operationType, dataInfo.SavePropertys);
                    }
                    else
                        acmd = persistenceManager.GetDbCommand(db, data as MB.Orm.Common.BaseModel,
                                                                                 dataInfo.ExecuteXmlCfgSqlName);
                    //如果存在多个Command 上只需要记录一次主体对象就可以,避免多次进行引用约束判断
                    bool hasRegister = false;
                    foreach (DbCommand d in acmd) {
                        EntityDbCommandInfo ecmd = new EntityDbCommandInfo(data, d);
                        if (!hasRegister) {
                            ecmd.IsMainEntity = currentIsMain;
                            hasRegister = true;
                        }
                        //目前只对主体数据进行删除的约束标记。
                        if (ecmd.IsMainEntity && dataInfo.DataState == ObjectDataState.Deleted) {
                            ecmd.OperationType = OperationType.Delete;
                        }

                        ecmd.IsPartPropertyUpdate = (dataInfo.SavePropertys != null && dataInfo.SavePropertys.Length > 0);
                        cmds.Add(ecmd);
                    }

                }
                else if (data.GetType().IsSubclassOf(typeof(System.Data.DataSet))) {
                    if (mainKeyValue == null)
                        throw new MB.Util.APPException("在批量存储对象明细前必须先获取主表键值！", MB.Util.APPMessageType.SysErrInfo);
                    if (baseRule == null)
                        throw new MB.Util.APPException("通过CreateCmdsFromObjectDataList 执行DataSet时,业务对象不能为空", Util.APPMessageType.SysErrInfo);

                    IList<EntityDbCommandInfo> tempCmds = CreateCmdsFromDataSet(db, baseRule, data as DataSet, mainKeyValue, dataInfo.DataInDocType);
                   
                    foreach (EntityDbCommandInfo tCmd in tempCmds)
                        cmds.Add(tCmd);
                }
                else {
                    throw new MB.RuleBase.Exceptions.DataTypeUnSupportException(data.GetType().FullName);
                }
            }
            return cmds;
        }

        /// <summary>
        /// 从DataSet 中创建可以进行数据库操作的DBCommand。
        /// </summary>
        /// <param name="db"></param>
        /// <param name="baseRule"></param>
        /// <param name="dsData"></param>
        /// <param name="dataInDocType"></param>
        /// <returns></returns>
        public IList<EntityDbCommandInfo> CreateCmdsFromDataSet(Database db, IBaseRule baseRule, DataSet dsData, object foreingKeyValue, object dataInDocType) {
            List<EntityDbCommandInfo> cmds = new List<EntityDbCommandInfo>();
            ObjectDataMappingAttribute mappingAtt = AttributeConfigHelper.Instance.GetObjectDataMappingAttribute(dataInDocType);

            MB.Orm.Enums.OperationType opts = MB.Orm.Enums.OperationType.Insert | MB.Orm.Enums.OperationType.Update;
            if (mappingAtt.DeleteNotInIds)
                opts = opts | MB.Orm.Enums.OperationType.DeleteNotIn;
            else
                opts = opts | MB.Orm.Enums.OperationType.Delete;

            MB.Orm.Persistence.PersistenceManagerHelper persistenceManager = MB.Orm.Persistence.PersistenceManagerHelper.NewInstance;
            Dictionary<MB.Orm.Enums.OperationType, MB.Orm.DbSql.SqlString[]> sqlLib =
                                     MB.Orm.Mapping.Xml.SqlConfigHelper.Instance.GetObjectStandardEditSql(mappingAtt.MappingXmlFileName, opts);

            DataRow[] drsData = dsData.Tables[0].Select();
            if (sqlLib.ContainsKey(OperationType.DeleteNotIn) && (foreingKeyValue != null && foreingKeyValue != System.DBNull.Value)) {
                EntityDbCommandInfo[] cmdDeletes = persistenceManager.CreateDeleteNotInDbCommand(db,
                                    sqlLib[OperationType.DeleteNotIn], drsData, mappingAtt.KeyName, foreingKeyValue);

                cmds.AddRange(cmdDeletes);
            }
           
            StringBuilder sqlB = new StringBuilder();
            foreach (DataRow dr in drsData) {
                //空行跳过不进行处理
                bool b = MB.Orm.Common.ValueValidated.Instance.RowIsNull(dr);
                if (b)
                    continue;
                switch (dr.RowState) {
                    case System.Data.DataRowState.Added: //增加
                        if (!sqlLib.ContainsKey(OperationType.Insert))
                            continue;

                        if (!string.IsNullOrEmpty(mappingAtt.ForeingKeyName) && foreingKeyValue != null &&
                                        dr.Table.Columns.Contains(mappingAtt.ForeingKeyName) &&
                                        (dr[mappingAtt.ForeingKeyName] == System.DBNull.Value)) {
                            dr[mappingAtt.ForeingKeyName] = foreingKeyValue;
                        }
                        DbCommand[] cmdAdds = persistenceManager.CreateDbCommandByDataRow(db, sqlLib[OperationType.Insert], dr);
                        foreach (DbCommand ad in cmdAdds)
                            cmds.Add(new EntityDbCommandInfo(dr, ad));
                        // cmds.AddRange(cmdAdds);

                        break;
                    case System.Data.DataRowState.Modified://修改
                        if (!sqlLib.ContainsKey(OperationType.Update))
                            continue;
                        DbCommand[] cmdUpdates = persistenceManager.CreateDbCommandByDataRow(db, sqlLib[OperationType.Update], dr);
                        foreach (DbCommand ud in cmdUpdates)
                            cmds.Add(new EntityDbCommandInfo(dr, ud));
                        // cmds.AddRange(cmdUpdates);
                        break;
                    case System.Data.DataRowState.Deleted: //删除为直接删除，所以这里不进行处理。
                        break;
                    case System.Data.DataRowState.Detached:
                    default:
                        break;
                }
            }
            return cmds;
        }

        #endregion Create DbCommand 处理相关...

        #region 获取对象数据处理相关...
        /// <summary>
        /// 通过主键获取对应明细的数据。
        /// </summary>
        /// <param name="baseRule">指定操作的业务类</param>
        /// <param name="dataInDocType">指定的数据类型</param>
        /// <param name="mainKeyValue">主键</param>
        /// <returns></returns>
        public System.Data.DataSet GetObjectsByForeingKeyAsDs(IBaseRule baseRule, object dataInDocType, object mainKeyValue) {
            ObjectDataMappingAttribute mappingAtt = AttributeConfigHelper.Instance.GetObjectDataMappingAttribute(dataInDocType);
            if (mappingAtt == null)
                throw new MB.RuleBase.Exceptions.RequireConfigDataMappingException(dataInDocType);

            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase(); 

            System.Data.Common.DbCommand[] cmds = MB.Orm.Persistence.PersistenceManagerHelper.NewInstance.CreateDbCommandByXml(db,
                                               mappingAtt.MappingXmlFileName, MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_GET_BY_FOREING_KEY,
                                               mainKeyValue);

            if (cmds.Length != 1) {
                throw new MB.RuleBase.Exceptions.SelectSqlXmlConfigException(mappingAtt.MappingXmlFileName, MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_GET_BY_FOREING_KEY);
            }
            System.Data.Common.DbCommand cmd = cmds[0];
            DataSet ds = null;
            try {
                string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmd);
                MB.Util.TraceEx.Write("正在执行:" + cmdMsg);
                ds = new DatabaseExecuteHelper(_QueryBehavior).ExecuteDataSet(db, cmd);
            }
            catch (Exception ex) {
                throw new MB.RuleBase.Exceptions.DatabaseExecuteException("GetObjectsByForeingKey 出错！", ex);
            }
            return ds;
        }
        /// <summary>
        /// 通过主键获取对应明细的数据。
        /// </summary>
        /// <typeparam name="T">从MB.Orm.Common.BaseModel 中继承的数据对象类型。</typeparam>
        /// <param name="baseRule">指定操作的业务类</param>
        /// <param name="dataInDocType">指定的数据类型</param>
        /// <param name="mainKeyValue">主键</param>
        /// <returns></returns>
        public List<T> GetObjectsByForeingKey<T>(IBaseRule baseRule, object dataInDocType, object mainKeyValue) {
            ObjectDataMappingAttribute mappingAtt = AttributeConfigHelper.Instance.GetObjectDataMappingAttribute(dataInDocType);
            if (mappingAtt == null)
                throw new MB.RuleBase.Exceptions.RequireConfigDataMappingException(dataInDocType);

            Type entityType = typeof(T);
            if (string.Compare(entityType.FullName, "System.Object", true) == 0) {
                if (mappingAtt.EntityType == null)
                    throw new MB.Util.APPException("业务类:" + baseRule.GetType().FullName + " 中的:" +
                                            dataInDocType.GetType() + " 的数据值:" + dataInDocType.ToString() +
                                            " 在配置ObjectDataMapping时需要配置 EntityType", MB.Util.APPMessageType.SysErrInfo);
                entityType = mappingAtt.EntityType;
            }
            return DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<T>(entityType, mappingAtt.MappingXmlFileName, MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_GET_BY_FOREING_KEY, mainKeyValue);
        }

        /// <summary>
        /// 动态聚组查询获取数据
        /// </summary>
        /// <param name="baseRule"></param>
        /// <param name="setting"></param>
        /// <param name="parInfos"></param>
        /// <param name="parValues"></param>
        /// <returns></returns>
        public System.Data.DataSet GetDynamicGroupQueryData(MB.Util.Model.DynamicGroupSetting setting, QueryParameterInfo[] parInfos, params object[] parValues)
        {
            MB.Orm.Mapping.QueryParameterMappings queryMappings = null;
            if (!string.IsNullOrEmpty(setting.EntityInfos.MainEntity.Alias))
                queryMappings = new Orm.Mapping.QueryParameterMappings(setting.EntityInfos.MainEntity.Alias);
            else
                queryMappings = new Orm.Mapping.QueryParameterMappings(setting.EntityInfos.MainEntity.Name);

            string sqlFilter = MB.Orm.DbSql.SqlShareHelper.Instance.QueryParametersToSqlString(queryMappings, parInfos);
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();

            MB.Orm.Persistence.PersistenceManagerHelper persitence = MB.Orm.Persistence.PersistenceManagerHelper.NewInstance;
            var sql = MB.Orm.DbSql.DynamicGroupBuilder.DynamicGroupBuilderFactory.CreateQueryBuilder(setting).BuildDynamicQuery(sqlFilter);
            DbCommand dbCmd = persitence.GetSqlStringCommand(db, sql);


            DataSet ds = null;
            try
            {
                MB.Util.TraceEx.Write("正在执行:" + MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, dbCmd));

                ds = new DatabaseExecuteHelper(_QueryBehavior).ExecuteDataSet(db, dbCmd);
            }
            catch (Exception ex)
            {
                throw new MB.RuleBase.Exceptions.DatabaseExecuteException("GetDynamicGroupQueryData 出错！", ex);
            }
            return ds;
        }

        /// <summary>
        /// 根据数据类型和过滤条件返回需要的数据。
        /// </summary>
        /// <param name="baseRule">指定操作的业务类。</param>
        /// <param name="dataInDocType">指定的数据类型。</param>
        /// <param name="whereFilters">过滤条件</param>
        /// <param name="otherParamValues">其它附加的过滤条件</param>
        /// <returns>满足条件的DataSet 类型数据</returns>
        public System.Data.DataSet GetObjectAsDataSet(IBaseQueryRule baseRule, object dataInDocType, QueryParameterInfo[] parInfos, params object[] parValues) {
            ObjectDataMappingAttribute mappingAtt = AttributeConfigHelper.Instance.GetObjectDataMappingAttribute(dataInDocType);
            if (mappingAtt == null)
                throw new MB.RuleBase.Exceptions.RequireConfigDataMappingException(dataInDocType);

            string sqlName = string.IsNullOrEmpty(mappingAtt.XmlCfgSelectSqlName) ? MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_SELECT_OBJECT : mappingAtt.XmlCfgSelectSqlName;

            MB.Orm.Mapping.QueryParameterMappings queryMappings = baseRule.QueryParamMapping;
            if (queryMappings == null)
                queryMappings = MB.Orm.Mapping.Xml.SqlConfigHelper.Instance.GetSqlQueryParamMappings(mappingAtt.MappingXmlFileName, sqlName);

            string sqlFilter = MB.Orm.DbSql.SqlShareHelper.Instance.QueryParametersToSqlString(queryMappings, parInfos);

            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();

            ArrayList paramValues = new ArrayList();
            if (parValues != null && parValues.Length > 0) {
                foreach (object v in parValues)
                    paramValues.Add(v);
            }
            paramValues.Add(sqlFilter);
            System.Data.Common.DbCommand[] cmds = MB.Orm.Persistence.PersistenceManagerHelper.NewInstance.CreateDbCommandByXml(db,
                                               mappingAtt.MappingXmlFileName, sqlName, paramValues.ToArray());

            if (cmds.Length != 1)
                throw new MB.RuleBase.Exceptions.SelectSqlXmlConfigException(mappingAtt.MappingXmlFileName, sqlName);

            System.Data.Common.DbCommand cmd = cmds[0];

            DataSet ds = null;
            try {
                MB.Util.TraceEx.Write("正在执行:" + MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmd));

                ds = new DatabaseExecuteHelper(_QueryBehavior).ExecuteDataSet(db, cmd);  
            }
            catch (Exception ex) {
                throw new MB.RuleBase.Exceptions.DatabaseExecuteException("GetObjectAsDataSet 出错！", ex);
            }
            return ds;
        }
        /// <summary>
        /// 根据指定类型获取满足条件的实体对象。
        /// </summary>
        /// <typeparam name="T">从MB.Orm.Common.BaseModel 中继承的数据对象类型。</typeparam>
        /// <param name="baseRule">获取实体对象集合的业务类。</param>
        /// <param name="dataInDocType">在业务类中的数据类型。</param>
        /// <param name="filter">过滤条件。</param>
        /// <returns>指定类型的集合类。</returns>
        public List<T> GetObjects<T>(IBaseQueryRule baseRule, object dataInDocType, QueryParameterInfo[] parInfos, params object[] parValues) {
            ObjectDataMappingAttribute mappingAtt = AttributeConfigHelper.Instance.GetObjectDataMappingAttribute(dataInDocType);
            if (mappingAtt == null)
                throw new MB.RuleBase.Exceptions.RequireConfigDataMappingException(dataInDocType);

            string sqlName = string.IsNullOrEmpty(mappingAtt.XmlCfgSelectSqlName) ? MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_SELECT_OBJECT : mappingAtt.XmlCfgSelectSqlName;

            MB.Orm.Mapping.QueryParameterMappings queryMappings = baseRule.QueryParamMapping;
            if (queryMappings == null)
                queryMappings = MB.Orm.Mapping.Xml.SqlConfigHelper.Instance.GetSqlQueryParamMappings(mappingAtt.MappingXmlFileName, sqlName);

            string sqlFilter = MB.Orm.DbSql.SqlShareHelper.Instance.QueryParametersToSqlString(queryMappings, parInfos);
            Type entityType = typeof(T);
            if (string.Compare(entityType.FullName, "System.Object", true) == 0) {
                if (mappingAtt.EntityType == null)
                    throw new MB.Util.APPException("业务类:" + baseRule.GetType().FullName + " 中的:" +
                                            dataInDocType.GetType() + " 的数据值:" + dataInDocType.ToString() +
                                            " 在配置ObjectDataMapping时需要配置 EntityType", MB.Util.APPMessageType.SysErrInfo);
                entityType = mappingAtt.EntityType;
            }

            List<object> pars = new List<object>();
            if (parValues != null && parValues.Length > 0) {
                foreach (object p in parValues)
                    pars.Add(p);
            }
            pars.Add(sqlFilter);
            //以后需要增加是否通过XML 配置还是根据实体自动生成。
            return new DatabaseExcuteByXmlHelper(_QueryBehavior).GetObjectsByXml<T>(entityType, mappingAtt.MappingXmlFileName, sqlName, pars.ToArray());
        }

        /// <summary>
        ///  获取指定对象数据的值。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="sqlName"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public object ExecuteScalar(string xmlFileName, string sqlName, params object[] parValues) {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase(); 

            System.Data.Common.DbCommand[] cmds = MB.Orm.Persistence.PersistenceManagerHelper.NewInstance.CreateDbCommandByXml(db,
                                               xmlFileName, sqlName, parValues);

            if (cmds.Length != 1)
                throw new MB.RuleBase.Exceptions.SelectSqlXmlConfigException(xmlFileName, sqlName);

            object val = db.ExecuteScalar(cmds[0]);
            return val;
        }
        /// <summary>
        /// 根据键值获取包含值的实体对象。
        /// </summary>
        /// <typeparam name="T">从MB.Orm.Common.BaseModel 中继承的数据对象类型。</typeparam>
        /// <param name="baseRule">获取实体对象集合的业务类。</param>
        /// <param name="dataInDocType">在业务类中的数据类型。</param>
        /// <param name="keyValue">需要获取实体键值。</param>
        /// <returns>返回指定类型的实体对象。</returns>
        public T GetObjectByKey<T>(IBaseRule baseRule, object dataInDocType, object keyValue) {
            ObjectDataMappingAttribute mappingAtt = AttributeConfigHelper.Instance.GetObjectDataMappingAttribute(dataInDocType);
            if (mappingAtt == null)
                throw new MB.RuleBase.Exceptions.RequireConfigDataMappingException(dataInDocType);

            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase(); 

            System.Data.Common.DbCommand[] cmds = MB.Orm.Persistence.PersistenceManagerHelper.NewInstance.CreateDbCommandByXml(db,
                                               mappingAtt.MappingXmlFileName, MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_SELECT_BY_KEY, keyValue);

            if (cmds.Length != 1)
                throw new MB.RuleBase.Exceptions.SelectSqlXmlConfigException(mappingAtt.MappingXmlFileName, MB.Orm.Mapping.Xml.XmlSqlMappingInfo.SQL_SELECT_BY_KEY);

            System.Data.Common.DbCommand cmd = cmds[0];
            Type entityType = typeof(T);
            if (string.Compare(entityType.FullName, "System.Object", true) == 0) {
                if (mappingAtt.EntityType == null)
                    throw new MB.Util.APPException("业务类:" + baseRule.GetType().FullName + " 中的:" +
                                            dataInDocType.GetType() + " 的数据值:" + dataInDocType.ToString() +
                                            " 在配置ObjectDataMapping时需要配置 EntityType", MB.Util.APPMessageType.SysErrInfo);
                entityType = mappingAtt.EntityType;
            }
            bool isBaseModelSub = entityType.IsSubclassOf(typeof(BaseModel));
            MB.Orm.Mapping.ModelMappingInfo modelMapping = MB.Orm.Mapping.AttMappingManager.Instance.GetModelMappingInfo(entityType);
            object entity = MB.Util.DllFactory.Instance.CreateInstance(entityType);
            string cmdMsg = MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmd);
            MB.Util.TraceEx.Write("正在执行:" + cmdMsg);
            using (new Util.MethodTraceWithTime(cmdMsg))
            {
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    try
                    {
                        DataTable dtReader = reader.GetSchemaTable();
                        while (reader.Read())
                        {

                            foreach (MB.Orm.Mapping.FieldPropertyInfo proInfo in modelMapping.FieldPropertys.Values)
                            {
                                if (dtReader.Select(string.Format("ColumnName='{0}'", proInfo.FieldName)).Length <= 0)
                                    continue;
                                if (reader[proInfo.FieldName] != System.DBNull.Value)
                                    MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, proInfo.PropertyName, reader[proInfo.FieldName]);
                            }
                            break;
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new MB.RuleBase.Exceptions.DatabaseExecuteException("执行" + baseRule.GetType().FullName + "GetObjectByKey<T> 出错！", ex);
                    }
                    finally
                    {
                    }
                }
            }
            if (isBaseModelSub) {
                MB.Orm.Common.BaseModel temp = entity as MB.Orm.Common.BaseModel;
                //if (temp == null)
                //    throw new MB.Util.APPException(string.Format("所有的数据实体对象都必须从MB.Orm.Common.BaseModel 继承,请检查{0}", entity.GetType()));
                temp.EntityState = EntityState.Persistent;
            }
            return (T)entity;
        }

        /// <summary>
        /// 获取数据对象的主键。
        /// </summary>
        /// <param name="baseRule"></param>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public object GetObjectKeyValue(IBaseRule baseRule, ObjectDataInfo dataInfo) {
            string keyName = null;
            if (dataInfo.DataInDocType != null) {
                ObjectDataMappingAttribute mappingAtt = AttributeConfigHelper.Instance.GetObjectDataMappingAttribute(dataInfo.DataInDocType);
                if (mappingAtt != null)
                    keyName = mappingAtt.KeyName;
            }
            bool isEntity = dataInfo.ObjectData.GetType().IsSubclassOf(typeof(MB.Orm.Common.BaseModel));
            if (string.IsNullOrEmpty(keyName) && isEntity) {
                MB.Orm.Mapping.Att.ModelMapAttribute tattr = Attribute.GetCustomAttribute(dataInfo.ObjectData.GetType(), typeof(MB.Orm.Mapping.Att.ModelMapAttribute)) as MB.Orm.Mapping.Att.ModelMapAttribute;
                string[] keys = tattr.PrimaryKeys;

                if (keys != null && keys.Length > 0)
                    MB.Util.TraceEx.Assert(keys.Length > 1, "业务类：" + baseRule.GetType().FullName + " 主表对象的键值配置不能存在联合主键！");
                keyName = keys[0];
            }
            if (string.IsNullOrEmpty(keyName))
                throw new MB.Util.APPException("业务类：" + baseRule.GetType().FullName + " 的主表对象的主键信息没有配置！", MB.Util.APPMessageType.SysErrInfo);

            if (isEntity)
                return MB.Util.MyReflection.Instance.InvokePropertyForGet(dataInfo.ObjectData, keyName);
            else if ((dataInfo.ObjectData as DataRow) != null)
                return (dataInfo.ObjectData as DataRow)[keyName];
            else
                throw new MB.Util.APPException("获取业务类：" + baseRule.GetType().FullName + " 的主表对象的键值有误！", MB.Util.APPMessageType.SysErrInfo);

        }
        #endregion 获取对象数据处理相关...

        #region 其它public 函数...
        /// <summary>
        /// 转换对象数据的状态为需要进行数据库操作的操作类型。
        /// </summary>
        /// <param name="dataState"></param>
        /// <returns></returns>
        public MB.Orm.Enums.OperationType ConvertEntityStateToOperationType(EntityState dataState) {
            switch (dataState) {
                case EntityState.New:
                    return MB.Orm.Enums.OperationType.Insert;
                case EntityState.Modified:
                    return MB.Orm.Enums.OperationType.Update;
                case EntityState.Deleted:
                    return MB.Orm.Enums.OperationType.Delete;
                default:
                    return MB.Orm.Enums.OperationType.None;
            }
        }
        #endregion 其它public 函数...

    }
}
