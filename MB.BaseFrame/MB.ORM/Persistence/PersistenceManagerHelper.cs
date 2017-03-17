//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-02
// Description	:	执行Entity 永久化操作的公共方法,整个业务对象的所有实体操作在业务类的层次中进行整合操作处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Microsoft.Practices.EnterpriseLibrary.Data;

using MB.Orm.Common;
using MB.Orm.Enums;
using MB.Orm.Mapping;
using MB.Orm.DbSql;
using MB.Util.Model;
using System.Reflection;
using MB.Util.Emit;
using System.Data.Common;
using MB.Util;
namespace MB.Orm.Persistence {
    /// <summary>
    /// 执行Entity 永久化操作的公共方法。
    /// 
    /// </summary>
    public class PersistenceManagerHelper {
        private static readonly string SQL_PREFIX = SqlShareHelper.SQL_XML_CFG_PARAM_PREFIX;
        //private static string[] SQL_SPEC_STRING = SqlShareHelper.SQL_SPEC_STRING;// new string[] { SQL_PREFIX + "WHERE", SQL_PREFIX + "ORDERBY", SQL_PREFIX + "GROUPBY" };   
        private string[] _CommandTextFormatValues;
        /// <summary>
        /// 兼容老版本二增加
        /// </summary>
        public PersistenceManagerHelper() {
        }
        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        public PersistenceManagerHelper(string[] commandTextFormatValues) {
            _CommandTextFormatValues = commandTextFormatValues;
        }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// </summary>
        public static PersistenceManagerHelper NewInstance {
            get {
                return new PersistenceManagerHelper(new string[]{});
            }
        }
        /// <summary>
        /// 根据SQL 字符窜获取执行的DBCommand.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public DbCommand GetSqlStringCommand(Database db, string sqlString) {
            var sql = ReplaceSqlParamsByDatabaseType(db, sqlString);
            return db.GetSqlStringCommand(sql);
        }
        /// <summary>
        /// 根据数据库类型获取SQL 参数的前缀.
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static string GetSqlParamsPrefix(Database db) {
            var dbType = DatabaseConfigurationScope.GetDatabaseType(db);
            if (dbType == DatabaseType.Oracle) {
                return SqlShareHelper.ORACLE_PARAM_PREFIX;
            }
            else if (dbType == DatabaseType.MSSQLServer) {
                return SqlShareHelper.SQL_SERVER_PARAM_PREFIX;
            }
            else if (dbType == DatabaseType.Sqlite) {//sqlite 和参数前缀是@也可以是:,默认情况下酒取 : (Oracle 参数前缀),
                return SqlShareHelper.ORACLE_PARAM_PREFIX;
            }
            else if (dbType == DatabaseType.MySql) {//MySql  参数前缀是@  (sqlserver 参数前缀),
                return SqlShareHelper.SQL_SERVER_PARAM_PREFIX;
            }
            else {
                throw new APPException(string.Format("当前数据库类型不支持{0}", dbType.ToString()),
                                               APPMessageType.SysDatabaseInfo);
            }
        }
        #region Public 根据XML 配置创建DbCommand 处理相关...
        /// <summary>
        /// 根据参数和XML文件获取数据库执行的DbCommand.
        /// </summary>
        /// <param name="db">当前连接库</param>
        /// <param name="xmlFileName">Mapping对应的XML 文件名称</param>
        /// <param name="sqlName">SQL 语句配置对应的名称</param>
        /// <param name="sqlParams">参数以及值</param>
        /// <returns>Command 数组,如果只配置一个SqlString 那么就只返回一个DbCommand</returns>
        public System.Data.Common.DbCommand[] CreateDbCommandBySqlParams(Database db, string xmlFileName, string sqlName, List<SqlParamInfo> sqlParams) {
            var dbType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            MB.Orm.DbSql.SqlString[] sqlStrsArray = MB.Orm.Mapping.Xml.SqlConfigHelper.Instance.GetSqlString(xmlFileName, sqlName);
            if (sqlStrsArray == null || sqlStrsArray.Length == 0)
                throw new MB.Orm.Exceptions.XmlSqlConfigNotExistsException(xmlFileName, sqlName);
            List<System.Data.Common.DbCommand> dbCmds = new List<System.Data.Common.DbCommand>();
            Dictionary<string, SqlParamInfo> parsHas = new Dictionary<string, SqlParamInfo>();
            foreach (SqlParamInfo info in sqlParams) {
                if (parsHas.ContainsKey(info.Name)) continue;
                parsHas.Add(info.Name, info);
            }
            foreach (MB.Orm.DbSql.SqlString sqlStr in sqlStrsArray) {
                sqlStr.SqlStr = formatSqlString(sqlStr.SqlStr);

                IList<SqlParamInfo> tPars = sqlStr.ParamFields;             
                System.Data.Common.DbCommand dbCmd = db.GetSqlStringCommand(sqlStr.SqlStr);
                if (tPars != null && tPars.Count > 0) {
                    for (int i = 0; i < tPars.Count; i++) {
                        SqlParamInfo parInfo = tPars[i];
                        if(!parsHas.ContainsKey(parInfo.Name))
                            throw new MB.Util.APPException(string.Format("调用XML文件:{0} 下的SQL {1} 配置的参数 {2} 在传入的参数中不存在！",xmlFileName,sqlName,parInfo.Name), MB.Util.APPMessageType.SysErrInfo);

                        SqlParamInfo valPar = parsHas[parInfo.Name];
                        AddParamInfoToDbCommand(db, dbCmd, parInfo, valPar.Value );
                    
                    }
                }


                dbCmd.CommandText = ReplaceSqlParamsByDatabaseType(db, dbCmd.CommandText);
                dbCmds.Add(dbCmd);
            }
            return dbCmds.ToArray();
        }
        /// <summary>
        /// 根据参数和XML文件获取数据库执行的DbCommand.
        /// 根据该方法获取SqlString 都具有相同的参数。
        /// <param name="db">当前连接库</param>
        /// <param name="xmlFileName">Mapping对应的XML 文件名称</param>
        /// <param name="sqlName">SQL 语句配置对应的名称</param>
        /// <param name="parValues">参数的值,根据配置的顺序来匹配</param>
        /// <returns>Command 数组,如果只配置一个SqlString 那么就只返回一个DbCommand</returns>
        public System.Data.Common.DbCommand[] CreateDbCommandByXml(Database db,string xmlFileName, string sqlName, params object[] parValues) {
            var dbType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            MB.Orm.DbSql.SqlString[] sqlStrsArray= MB.Orm.Mapping.Xml.SqlConfigHelper.Instance.GetSqlString(xmlFileName, sqlName);
            if (sqlStrsArray == null || sqlStrsArray.Length == 0)
                throw new MB.Orm.Exceptions.XmlSqlConfigNotExistsException(xmlFileName, sqlName);
            List<System.Data.Common.DbCommand> dbCmds = new List<System.Data.Common.DbCommand>();
            foreach (MB.Orm.DbSql.SqlString sqlStr in sqlStrsArray) {
 
                 sqlStr.SqlStr = formatSqlString(sqlStr.SqlStr);

                IList<SqlParamInfo> tPars = sqlStr.ParamFields;
                if (tPars != null && tPars.Count > 0) {
                    if (parValues == null || parValues.Length != tPars.Count)
                        throw new MB.Util.APPException("调用XML文件:" + xmlFileName + " 下的SQL" + sqlName + " 传入的参数和配置的参数不一致！");
                }
                System.Data.Common.DbCommand dbCmd = db.GetSqlStringCommand(sqlStr.SqlStr);
                if (tPars != null && tPars.Count > 0) {
                    for (int i = 0; i < tPars.Count; i++) {
                        SqlParamInfo parInfo = tPars[i];

                        AddParamInfoToDbCommand(db, dbCmd, parInfo, parValues[i]);
                    }
                }
               
            
                dbCmd.CommandText = ReplaceSqlParamsByDatabaseType(db,dbCmd.CommandText);
                dbCmds.Add(dbCmd);
            }
            return dbCmds.ToArray() ;
        }
        #endregion Public 根据XML 配置创建DbCommand 处理相关...
        //覆盖特殊的字段。
        public string ReaplaceSpecString(string sqlStr,string oldStr, string newStr) {
            sqlStr = sqlStr.Replace(" " + oldStr + " ", " " + newStr + " ");
            sqlStr = sqlStr.Replace("(" + oldStr + ")", "(" + newStr + ")");
            sqlStr = sqlStr.Replace(" " + oldStr + ")", " " + newStr + ")");
            sqlStr = sqlStr.Replace(" " + oldStr + ",", " " + newStr + ",");
            sqlStr = sqlStr.Replace(" " + oldStr + ";", " " + newStr + ";");
            sqlStr = sqlStr.Replace(" " + oldStr + "(", " " + newStr + "(");

            return sqlStr;
        }
        //替换SQL 语句中的参数字符。
        public string ReplaceSqlParamsByDatabaseType(Database db, string sqlString) {
            var dbType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            if (dbType == DatabaseType.Oracle) {
                sqlString = sqlString.Replace(MB.Orm.DbSql.SqlShareHelper.SQL_XML_CFG_PARAM_PREFIX, MB.Orm.DbSql.SqlShareHelper.ORACLE_PARAM_PREFIX);     
            }
            else if (dbType == DatabaseType.MSSQLServer) {
                sqlString = sqlString.Replace(MB.Orm.DbSql.SqlShareHelper.SQL_XML_CFG_PARAM_PREFIX, MB.Orm.DbSql.SqlShareHelper.SQL_SERVER_PARAM_PREFIX);
            }
            else if (dbType == DatabaseType.Sqlite) {//sqlite 和参数前缀是@也可以是:,默认情况下酒取 : (Oracle 参数前缀),
                sqlString = sqlString.Replace(MB.Orm.DbSql.SqlShareHelper.SQL_XML_CFG_PARAM_PREFIX, MB.Orm.DbSql.SqlShareHelper.ORACLE_PARAM_PREFIX);    
            }
            else if (dbType == DatabaseType.MySql) {//MySql  参数前缀是@  (sqlserver 参数前缀),
                sqlString = sqlString.Replace(MB.Orm.DbSql.SqlShareHelper.SQL_XML_CFG_PARAM_PREFIX, MB.Orm.DbSql.SqlShareHelper.SQL_SERVER_PARAM_PREFIX);
            }
            else {
                throw new MB.Util.APPException(string.Format("当前数据库类型不支持{0}", dbType.ToString()),
                                               MB.Util.APPMessageType.SysDatabaseInfo);
            }

            return RemoveSqlStringLastSemicolon(db,sqlString);
        }
        /// <summary>
        /// 判断是否为Oracle 通过是否为单个执行任务,如果是那么去掉 最后一个分号
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public string RemoveSqlStringLastSemicolon(Database db, string sqlString) {
            var dbType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            if (dbType == DatabaseType.Oracle) {
                int len = sqlString.Length;
                if (sqlString.LastIndexOf(';') == len - 1) {
                    //在这里4 等于 "END;" 的字符窜长度
                    if (len > 4) {
                        //判断是否存在块执行
                        if (string.Compare(sqlString.Substring(len - 4, 4), "END;", true) != 0)
                            sqlString = sqlString.Remove(len - 1, 1);
                    }
                }
                
            }
            return sqlString;
        }
        /// <summary>
        /// 创建Command 执行的参数
        /// </summary>
        /// <param name="db">配置的数据库</param>
        /// <param name="dbCmd">dbCommand</param>
        /// <param name="paramInfo">sql参数</param>
        public void AddParamInfoToDbCommand(Database db, System.Data.Common.DbCommand dbCmd, SqlParamInfo parInfo) {
            AddParamInfoToDbCommand(db, dbCmd, parInfo, parInfo.Value);
        }
        /// <summary>
        /// 创建Command 执行的参数
        /// </summary>
        /// <param name="db">配置的数据库</param>
        /// <param name="dbCmd">dbCommand</param>
        /// <param name="paramInfo">sql参数</param>
        /// <param name="parValue">参数值</param>
        public void AddParamInfoToDbCommand(Database db, System.Data.Common.DbCommand dbCmd, SqlParamInfo parInfo,object parValue) {
            if (parInfo.Overcast) {
                dbCmd.CommandText = ReaplaceSpecString(dbCmd.CommandText, parInfo.Name, parValue.ToString());
                return;
            }
            //判断是否为特殊的字段。
            //string tmp = Array.Find<string>(SQL_SPEC_STRING, o => string.Compare(o, parInfo.Name, true) == 0);
            //if (tmp != null && tmp.Length > 0) {
            //    dbCmd.CommandText = reaplaceSpecString(dbCmd.CommandText, parInfo.Name, parValue.ToString());
            //    return;
            //}
            if (parInfo.Direction == ParameterDirection.Output)
                db.AddOutParameter(dbCmd, CreateParName(db, parInfo.Name), parInfo.DbType, parInfo.Length);
            else {
                DbType dtype =  ConvertToRealDbType(db, parInfo.DbType);
                db.AddInParameter(dbCmd, CreateParName(db, parInfo.Name), dtype , ConvertToRealDbValue(db, parValue, parInfo.DbType));
            
            }
        }
        /// <summary>
        /// 为当前数据库创建对应的一个参数。
        /// </summary>
        /// <param name="db">当前数据库</param>
        /// <param name="name">参数名</param>
        /// <returns></returns>
        public string CreateParName(Database db, string name) {
            string parName = string.Empty;
            parName += name.IndexOf(SqlShareHelper.SQL_XML_CFG_PARAM_PREFIX) == 0 ? name.Substring(1, name.Length - 1) : name;
            return parName;
        }

        #region Public  根据Entity 创建DbCommand 处理相关...
        /// <summary>
        /// 创建一个针对一个实体永久化操作的Command。
        /// </summary>
        /// <param name="db"></param>
        /// <param name="entity">操作实体</param>
        /// <param name="sqlName">SQL 语句配置对应的名称</param>
        /// <returns>返回包含的DbCommand</returns>
        public System.Data.Common.DbCommand[] GetDbCommand(Database db, BaseModel entity,string sqlName) {
            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entity.GetType());
            SqlString[] sqlStr = MB.Orm.Mapping.Xml.SqlConfigHelper.Instance.GetSqlString(mappingInfo.XmlConfigFileName, sqlName);
            List<System.Data.Common.DbCommand> cmds = new List<System.Data.Common.DbCommand>();
            foreach (SqlString s in sqlStr) {
                System.Data.Common.DbCommand dbCmd = PrepareExecuteCommand(db, entity, s);
                cmds.Add(dbCmd);
            }
            return cmds.ToArray(); 
        }
        /// <summary>
        /// 创建一个针对一个实体永久化操作的Commands。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="entitys"></param>
        /// <param name="xmlFileName"></param>
        /// <param name="sqlName"></param>
        /// <returns></returns>
        public System.Data.Common.DbCommand[] GetDbCommand<T>(Database db, IList<T> entitys, string xmlFileName, string sqlName){
            SqlString[] sqlStr = MB.Orm.Mapping.Xml.SqlConfigHelper.Instance.GetSqlString(xmlFileName, sqlName);
            List<System.Data.Common.DbCommand> cmds = new List<System.Data.Common.DbCommand>();

            foreach (SqlString s in sqlStr) {
                Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> dycs = new Dictionary<string, Util.Emit.DynamicPropertyAccessor>();
                foreach (var p in s.ParamFields) {
                    var e = new MB.Util.Emit.DynamicPropertyAccessor(typeof(T), p.MappingName);
                    dycs.Add(p.MappingName,e);
                }
                foreach (var entity in entitys) {
                    System.Data.Common.DbCommand dbCmd = PrepareExecuteCommandByEmit(dycs,db, entity, s);
                    cmds.Add(dbCmd);
                }
            }
            return cmds.ToArray();
        }
        /// <summary>
        /// 创建一个针对一个实体永久化操作的Command。   MB.Util.Emit.DynamicPropertyAccessor
        /// </summary>
        /// <param name="db"></param>
        /// <param name="entity">操作实体</param>
        /// <param name="xmlFileName">SQL 语句配置对应XML文件</param>
        /// <param name="sqlName">SQL 语句配置对应的名称</param>
        /// <returns>返回包含的DbCommand</returns>
        public System.Data.Common.DbCommand[] GetDbCommand(Database db, BaseModel entity, string xmlFileName, string sqlName) {
            SqlString[] sqlStr = MB.Orm.Mapping.Xml.SqlConfigHelper.Instance.GetSqlString(xmlFileName, sqlName);
            List<System.Data.Common.DbCommand> cmds = new List<System.Data.Common.DbCommand>();
            foreach (SqlString s in sqlStr) {
                System.Data.Common.DbCommand dbCmd = PrepareExecuteCommand(db, entity, s);
                cmds.Add(dbCmd);
            }
            return cmds.ToArray(); 
        }
        
        /// <summary>
        ///  针对一个实体永久化操作。
        ///   整个业务对象的所有实体操作在业务类的层次中进行整合操作处理。
        /// </summary>
        /// <param name="db"></param>
        /// <param name="entity"></param>
        /// <param name="operationType"></param>
        /// <param name="propertys"></param>
        /// <returns></returns>
        public System.Data.Common.DbCommand[] GetDbCommand(Database db, BaseModel entity, OperationType operationType,string[] propertys) {
            ModelMappingInfo mappingInfo = AttMappingManager.Instance.GetModelMappingInfo(entity.GetType());
            ModelConfigOptions cfgOptions = mappingInfo.ConfigOptions;
            BaseSqlGenerator sqlGenerator = SqlGeneratorManager.GetSqlGenerator(cfgOptions, propertys);

            SqlString[] sqlStr = sqlGenerator.GenerateSql(entity.GetType(), operationType, propertys);
            List<System.Data.Common.DbCommand> cmds = new List<System.Data.Common.DbCommand>();
            foreach (SqlString s in sqlStr) {
                System.Data.Common.DbCommand dbCmd = PrepareExecuteCommand(db, entity, s);
                cmds.Add(dbCmd);
            }
            return cmds.ToArray(); 
        }
        #endregion Public  根据Entity 创建DbCommand 处理相关...

        #region Public 根据DataRow 创建DbCommand 处理相关...
        /// <summary>
        /// 根据DataRow 创建 数据库操作需要的DbCommand。
        /// </summary>
        /// <param name="db">当前连接的数据库</param>
        /// <param name="sqlStrs">SQL 字符窜</param>
        /// <param name="drData">参数值，以DataRow 中的字段名称来进行匹配</param>
        /// <returns>Command 数组,如果只有一个SqlString 那么就只返回一个DbCommand</returns>
        public System.Data.Common.DbCommand[] CreateDbCommandByDataRow(Database db, SqlString[] sqlStrs, DataRow drData) {
            List<System.Data.Common.DbCommand> dbCmds = new List<System.Data.Common.DbCommand>();
            foreach (SqlString sqlStr in sqlStrs) {
                IList<SqlParamInfo> pars = sqlStr.ParamFields;
                System.Data.Common.DbCommand dbCmd = db.GetSqlStringCommand(sqlStr.SqlStr);
               
                foreach (SqlParamInfo parInfo in pars) {
                    if (parInfo.Direction == System.Data.ParameterDirection.Input || parInfo.Direction == System.Data.ParameterDirection.InputOutput) {
                        if (!drData.Table.Columns.Contains(parInfo.MappingName))
                            throw new MB.Orm.Exceptions.SqlArgumentXmlConfigException(sqlStr.SqlStr, parInfo.MappingName);

                        //db.AddInParameter(dbCmd, parInfo.Name, parInfo.DbType, drData[parInfo.MappingName]);
                        //parInfo.Value = drData[parInfo.MappingName]; 
                        AddParamInfoToDbCommand(db, dbCmd, parInfo, drData[parInfo.MappingName]);
                        continue;
                    }
                    else
                        throw new MB.Orm.Exceptions.SqlArgumentXmlConfigException(sqlStr.SqlStr, parInfo.MappingName);
                }

                dbCmd.CommandText = ReplaceSqlParamsByDatabaseType(db, dbCmd.CommandText);
                dbCmds.Add(dbCmd); 
            }
            return dbCmds.ToArray();
        }
        /// <summary>
        /// 创建基于DataRow数组 的Delete Not In DbCommand.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sqlDeleteNotIn"></param>
        /// <param name="drsData"></param>
        /// <param name="keyName"></param>
        /// <param name="foreingKeyValue"></param>
        /// <returns></returns>
        public EntityDbCommandInfo[] CreateDeleteNotInDbCommand(Database db, SqlString[] sqlDeleteNotIn, DataRow[] drsData, string keyName, object foreingKeyValue) {
            List<EntityDbCommandInfo> dbCmds = new List<EntityDbCommandInfo>();
            foreach (SqlString sqlInfo in sqlDeleteNotIn) {
                IList<SqlParamInfo> pars = sqlInfo.ParamFields;
                System.Data.Common.DbCommand dbCmd = db.GetSqlStringCommand(sqlInfo.SqlStr);
                dbCmd.CommandText = ReplaceSqlParamsByDatabaseType(db, dbCmd.CommandText);


                MB.Util.TraceEx.Assert(pars.Count == 2, sqlInfo.SqlStr + " SQL 语句配置需要2个参数,外键 和 主键的拼接字符窜。");

                //db.AddInParameter(dbCmd, pars[0].Name, pars[0].DbType, foreingKeyValue);
                AddParamInfoToDbCommand(db, dbCmd, pars[0], foreingKeyValue);

                string ids = MB.Orm.DbSql.SqlShareHelper.Instance.GetKeyValuesToString(drsData, keyName);

                //db.AddInParameter(dbCmd, pars[1].Name, pars[1].DbType, ids);
                
                AddParamInfoToDbCommand(db, dbCmd, pars[1], ids);

                dbCmds.Add(new EntityDbCommandInfo(null,dbCmd));
            }
            return dbCmds.ToArray();
        }
        #endregion Public 根据DataRow 创建DbCommand 处理相关...


        /// <summary>
        /// 通过Emit 方式获取实体的值。
        /// </summary>
        /// <param name="dycs"></param>
        /// <param name="db"></param>
        /// <param name="entity"></param>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public System.Data.Common.DbCommand PrepareExecuteCommandByEmit(Dictionary<string,DynamicPropertyAccessor> dycs,Database db, object entity, SqlString sqlStr) {
            IList<SqlParamInfo> pars = sqlStr.ParamFields;
            System.Data.Common.DbCommand dbCmd = db.GetSqlStringCommand(sqlStr.SqlStr);
            foreach (SqlParamInfo parInfo in pars) {
                if (parInfo.Direction == System.Data.ParameterDirection.Input || parInfo.Direction == System.Data.ParameterDirection.InputOutput) {
                    var d = dycs[parInfo.MappingName];
                    if (d == null)
                        throw new MB.Util.APPException(string.Format("参数MappingName :{0} 在类型{1}中找不到",parInfo.MappingName,entity.GetType().FullName), Util.APPMessageType.SysErrInfo);

                    object val = d.Get(entity);
                   
                    AddParamInfoToDbCommand(db, dbCmd, parInfo, val);
                } else if (parInfo.Direction == System.Data.ParameterDirection.Output) {
                     
                    AddParamInfoToDbCommand(db, dbCmd, parInfo);
                } else { //默认情况下把它当作输入参数
                    var d = dycs[parInfo.MappingName];
                    if (d == null)
                        throw new MB.Util.APPException(string.Format("参数MappingName :{0} 在类型{1}中找不到", parInfo.MappingName, entity.GetType().FullName), Util.APPMessageType.SysErrInfo);

                    object val = d.Get(entity);
                   
                    AddParamInfoToDbCommand(db, dbCmd, parInfo, val);
                }
            }
            dbCmd.CommandText = RemoveSqlStringLastSemicolon(db, dbCmd.CommandText);

            dbCmd.CommandText = ReplaceSqlParamsByDatabaseType(db, dbCmd.CommandText);
            return dbCmd;
        }
        /// <summary>
        /// 得到没有返回值的数据库操作Command.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="entity"></param>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public System.Data.Common.DbCommand PrepareExecuteCommand(Database db, BaseModel entity, SqlString sqlStr) {
            IList<SqlParamInfo> pars = sqlStr.ParamFields;
            System.Data.Common.DbCommand dbCmd = db.GetSqlStringCommand(sqlStr.SqlStr);
            foreach (SqlParamInfo parInfo in pars) {
                if (parInfo.Direction == System.Data.ParameterDirection.Input || parInfo.Direction == System.Data.ParameterDirection.InputOutput) {
                    object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, parInfo.MappingName);
                    
                    AddParamInfoToDbCommand(db, dbCmd, parInfo, val);
                }
                else if (parInfo.Direction == System.Data.ParameterDirection.Output) {
                    
                    AddParamInfoToDbCommand(db, dbCmd, parInfo);
                }
                else { //默认情况下把它当作输入参数
                    object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, parInfo.MappingName);
                     
                    AddParamInfoToDbCommand(db, dbCmd, parInfo, val);
                }
            }
            dbCmd.CommandText = RemoveSqlStringLastSemicolon(db,dbCmd.CommandText);

            dbCmd.CommandText = ReplaceSqlParamsByDatabaseType(db,dbCmd.CommandText);

            dbCmd.CommandText = formatSqlString(dbCmd.CommandText);

            return dbCmd;
        }
       /// <summary>
        /// 转换为数据库能真正存储的值。
       /// </summary>
       /// <param name="db"></param>
       /// <param name="val"></param>
       /// <param name="dbType"></param>
       /// <returns></returns>
        public object ConvertToRealDbValue(Database db, object val,DbType dbType) {
            //由于Oracle 数据用 CHAR(1) 代表 Boolean 所以需要进行特殊处理
            var databaseType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            if (databaseType != DatabaseType.Oracle)
                return val; 

            if (val != null &&  string.Compare(val.GetType().Name , "Boolean",true)==0 )
                return (Boolean)val ? "1" : "0";
            else
                return val;

        }
        /// <summary>
        /// 获取真正数据库处理类型。
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public DbType ConvertToRealDbType(Database db, DbType dbType) {
            var databaseType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            if (databaseType != DatabaseType.Oracle)
                return dbType; 

            if (dbType == DbType.Boolean)
                return DbType.String;
            else if (dbType == DbType.String) {
                return DbType.AnsiString;     //特殊说明，针对 NVarChar 查询慢的问题
            }
            else
                return dbType; 
        }
        ///// <summary>
        ///// 设置Oracle DbCommand  参数绑定的模式。
        ///// </summary>
        ///// <param name="db"></param>
        ///// <param name="dbCmd"></param>
        ///// <param name="byName"></param>
        public void SetDbCmdParamterBindByName(Database db, System.Data.Common.DbCommand dbCmd, bool byName) {
            var databaseType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            if (databaseType == DatabaseType.Oracle) {
                //edit by  陈迪臣 修改Oracle 参数的顺序的问题,默认情况下通过名称来匹配
                if (dbCmd.GetType().FullName == "Oracle.DataAccess.Client.OracleCommand") {//
                    PropertyInfo proInfo = dbCmd.GetType().GetProperty("BindByName");
                    if (proInfo != null)
                        proInfo.SetValue(dbCmd, true, null);

                }
            }
        }
        //格式化执行的SQL 语句
        private string formatSqlString(string sqlString) {
            if (sqlString.IndexOf("{0}") > 0 && _CommandTextFormatValues != null && _CommandTextFormatValues.Length > 0) {
                try {
                    return string.Format(sqlString, _CommandTextFormatValues);

                }
                catch (Exception ex) {
                    throw new MB.Util.APPException(string.Format("格式化CommandText {0} 时出 错,{1}", sqlString, ex.Message), MB.Util.APPMessageType.SysErrInfo);
                }
            } else {
                return sqlString;
            }
        }
    }


}
