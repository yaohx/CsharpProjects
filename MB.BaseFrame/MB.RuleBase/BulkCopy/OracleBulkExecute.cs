//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2010-03-21
// Description	:	针对Oracle 的数据批量处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
//using Oracle.DataAccess.Client;
using MB.Orm.DbSql;
using MB.Orm.Persistence;
using System.Collections;
using System.Reflection;
using System.Transactions;
using System.Data.SqlClient;
using System.Data;

namespace MB.RuleBase.BulkCopy
{
    /// <summary>
    /// 针对Oracle 的数据批量处理。
    /// 需要使用ODP.NET Oracle.DataAccess ,10.2之前不能使用TransactionScope ,只能ODP 事务。
    /// </summary>
    public class OracleBulkExecute : AbstractBaseBulk
    {
        private PersistenceManagerHelper _PersistenceManagerHelper;
        
        // private object _DbCn;
        /// <summary>
        /// 需要使用 ODP.NET Oracle.DataAccess.
        /// 10.2之前不能使用TransactionScope ,只能ODP 事务。
        /// </summary>
        public OracleBulkExecute(params string[] commandTextFormatValues)
            : this(null, commandTextFormatValues) {

        }
        /// <summary>
        ///   需要使用 ODP.NET Oracle.DataAccess.
        ///    10.2之前不能使用TransactionScope ,只能ODP 事务。
        /// </summary>
        /// <param name="dbTransaction"></param>
        public OracleBulkExecute(System.Data.IDbTransaction dbTransaction, params string[] commandTextFormatValues)
            : base(dbTransaction, commandTextFormatValues) {

        }
        /// <summary>
        /// 通过db.ConnectionString 返回Oracle 的数据库配置字符窜。
        /// </summary>
        /// <param name="cnStr"></param>
        /// <returns></returns>
        public string CreateOracleCnString(string cnStr) {

            return new SimulatedOracleHelper().ToOracleConnString(cnStr);
        }
        #region IDbBulkExecute 成员

        /// <summary>
        /// 目前Oracle 的批量处理只能在独立的事务中进行处理,使用的是Oracle.DataAccess.
        /// 以后Oracle 升级到11G 后再使用TransactionScope.
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="sqlName"></param>
        /// <param name="lstData">实体集合类或者DataRow[]数组</param>
        public override void WriteToServer(string xmlFileName, string sqlName, System.Collections.IList lstData) {
            MB.Util.TraceEx.Write("准备进行Oracle批量处理!");
            if (lstData == null || lstData.Count == 0) return;
            if (this.DbTransaction != null) {
                writeToServer(xmlFileName, sqlName, lstData, this.DbTransaction.Connection as System.Data.Common.DbConnection);
                return;
            }

            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            var oracleHelper = new SimulatedOracleHelper();
            var cnStr = oracleHelper.GetOracleDataAccessConnectionString(db);
            if (!oracleHelper.CheckProviderIsOracleDataAccess(db))
                throw new Exception(string.Format("请配置providerName类型为Oracle.DataAccess.Client，其它类型暂不支持!"));

            using (var cn = db.GetOpenConnection())
            {
            //System.Data.Common.DbConnection cn = null;
                //if (oracleHelper.CheckProviderIsOracleDataAccess(db))
                //    cn = db.GetOpenConnection(); //db.CreateConnection();
                //else
                //    cn = oracleHelper.CreateOracleConnection(cnStr);// new OracleConnection(cnStr);

                try
                {

                    writeToServer(xmlFileName, sqlName, lstData, cn.Connection);
                }
                catch (Exception ex)
                {
                    // tran.Rollback();
                    throw new MB.Util.APPException("Oracle数据库批量处理有误" + ex.Message, MB.Util.APPMessageType.SysDatabaseInfo);
                }
                finally
                {
                    try
                    {
                        //if (tran != null) {
                        //    tran.Dispose();
                        //}

                        //if (cn != null) {
                        //    cn.Close();
                        //    cn.Dispose();
                        //}
                    }
                    catch (Exception ex)
                    {
                        MB.Util.TraceEx.Write("资源释放出错！" + ex.Message);
                    }
                }
            }
            MB.Util.TraceEx.Write("结束进行Oracle批量处理!");
        }

        // 使用的是Oracle.DataAccess.Client
        private void writeToServer(string xmlFileName, string sqlName, IList lstData, System.Data.Common.DbConnection cn) {
            if (lstData == null || lstData.Count == 0) return;

            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            int count = lstData.Count;
            int SINGLE_L = this.BatchSize < 1 ? lstData.Count : this.BatchSize;
            int c;
            int re = Math.DivRem(count, SINGLE_L, out c);

            int all = System.Convert.ToInt32(c > 0 ? re + 1 : re);
            ArrayList lst = new ArrayList(lstData);
            for (int i = 0; i < all; i++) {
                int l = (i + 1) * SINGLE_L > count ? count - i * SINGLE_L : SINGLE_L;
                ArrayList childLst = lst.GetRange(i * SINGLE_L, l);
                var cmd = createDbCommandByXml(db, xmlFileName, sqlName, childLst);
                if (cn.State != ConnectionState.Open)
                    cn.Open();
                cmd.Connection = cn; 
                MB.Util.TraceEx.Write("开始执行Oracle批量插入语句!:" + MB.Orm.Persistence.DbCommandExecuteTrack.Instance.CommandToTrackMessage(db, cmd));
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region 内部函数处理...

        //创建DBCommand 对象.
        public System.Data.Common.DbCommand createDbCommandByXml(Database db, string xmlFileName, string sqlName, IList lstData) {
            SimulatedOracleHelper oh = new SimulatedOracleHelper();
            MB.Orm.Persistence.PersistenceManagerHelper pm = new PersistenceManagerHelper();
            var dbType = MB.Orm.Persistence.DatabaseHelper.GetDatabaseType(db);
            List<System.Data.Common.DbCommand> dbCmds = new List<System.Data.Common.DbCommand>();
            MB.Orm.DbSql.SqlString sqlStr = this.GetXmlSqlString(db, xmlFileName, sqlName);

            IList<SqlParamInfo> tPars = sqlStr.ParamFields;
            if (_PersistenceManagerHelper == null)
                _PersistenceManagerHelper = new PersistenceManagerHelper();

            var dbCmd = oh.CreateOracleCommand(_PersistenceManagerHelper.ReplaceSqlParamsByDatabaseType(db, sqlStr.SqlStr));// new OracleCommand(_PersistenceManagerHelper.ReplaceSqlParamsByDatabaseType(db, sqlStr.SqlStr));
            //dbCmd.ArrayBindCount = lstData.Count;
             List<SqlParamInfo> overcastPars = new List<SqlParamInfo>();

             oh.SetCommandArrayBindCount(dbCmd, lstData.Count);
          
            Type oracleDbType = oh.GetOracleDbType();
            Dictionary<string, ArrayList> pValues = paramValues(db, tPars, lstData);
            for (int i = 0; i < tPars.Count; i++) {
                SqlParamInfo parInfo = tPars[i];
                if (parInfo.Overcast) {
                    dbCmd.CommandText = pm.ReaplaceSpecString(dbCmd.CommandText, MB.Orm.DbSql.SqlShareHelper.ORACLE_PARAM_PREFIX + pm.CreateParName(db, parInfo.Name), pValues[parInfo.Name][0] == null ? null : pValues[parInfo.Name][0].ToString());
                }
                else {
                    var oraInfo = oh.CreateOracleParameter(parInfo.Name, convertToOracleType(oh, oracleDbType, parInfo.DbType));// new OracleParameter(parInfo.Name, convertToOracleType(parInfo.DbType));
                    oraInfo.Direction = System.Data.ParameterDirection.Input;
                    oraInfo.Value = pValues[parInfo.Name].ToArray();
                    dbCmd.Parameters.Add(oraInfo);
                }
            }

            return dbCmd;
        }
    
        //根据参数和实体对象构造出参数值集合。
        private Dictionary<string, ArrayList> paramValues(Database db, IList<SqlParamInfo> tPars, IList lstData) {
            Dictionary<string, ArrayList> values = new Dictionary<string, ArrayList>();
            object obj = lstData[0];
            Type objType = obj.GetType();
            //edit by cdc 2011-3-13
            if (objType.IsValueType || string.Compare(objType.Name, "String", true) == 0) {
                if (tPars.Count != 1)
                    throw new MB.Util.APPException(string.Format("批量处理中,值只有一个而参数有{0}个",tPars.Count), Util.APPMessageType.SysErrInfo); 
                ArrayList lst = new ArrayList();
                var sp = tPars[0];
                foreach (var d in lstData) {
                    lst.Add(convertToRealDbValue(d, sp.DbType));
                }
                values.Add(sp.Name, lst);
            }
            else if ((obj as DataRow) != null) {  //判断传入的是否为DataRow.
                foreach (SqlParamInfo info in tPars) {
                    ArrayList lst = new ArrayList();
                    foreach (DataRow dr in lstData) {
                        object val = dr[info.MappingName];
                        lst.Add(convertToRealDbValue(val, info.DbType));
                    }
                    values.Add(info.Name, lst);
                }
            }
            else {
                foreach (SqlParamInfo info in tPars) {
                    MB.Util.Emit.DynamicPropertyAccessor acc = new MB.Util.Emit.DynamicPropertyAccessor(objType, info.MappingName);
                    if (acc == null)
                        throw new MB.Util.APPException(string.Format("配置的属性{0} 在类型{1} 中不存在", info.MappingName, objType.FullName), Util.APPMessageType.SysErrInfo);
                    ArrayList lst = new ArrayList();
                    foreach (object entity in lstData) {
                        object val = acc.Get(entity);
                       
                        lst.Add(convertToRealDbValue(val, info.DbType));
                    }
                    values.Add(info.Name, lst);
                }
            }
            return values;
        }


        private object convertToRealDbValue(object val, System.Data.DbType dbType) {
            //由于Oracle 数据用 CHAR(1) 代表 Boolean 所以需要进行特殊处理
            if (val != null && string.Compare(val.GetType().Name, "Boolean", true) == 0)
                return (Boolean)val ? "1" : "0";
            else
                return val;

        }
        //转换成oracle 相对应的类型.
        private object convertToOracleType(SimulatedOracleHelper oracleHelper, Type oracleDbType, System.Data.DbType dbType) {
            switch (dbType) {
                case System.Data.DbType.String:
                    return oracleHelper.GetOracleDbTypeValue(oracleDbType, "Varchar2");
                //return OracleDbType.Varchar2;
                case System.Data.DbType.Decimal:
                    return oracleHelper.GetOracleDbTypeValue(oracleDbType, "Decimal");
                //return OracleDbType.Decimal;
                case System.Data.DbType.Int16:
                    return oracleHelper.GetOracleDbTypeValue(oracleDbType, "Int16");
                //return OracleDbType.Int16;
                case System.Data.DbType.Int32:
                    return oracleHelper.GetOracleDbTypeValue(oracleDbType, "Int32");
                // return OracleDbType.Int32;
                case System.Data.DbType.Int64:
                    return oracleHelper.GetOracleDbTypeValue(oracleDbType, "Int64");
                //return OracleDbType.Int64;
                case System.Data.DbType.Double:
                    return oracleHelper.GetOracleDbTypeValue(oracleDbType, "Double");
                // return OracleDbType.Double;
                case System.Data.DbType.DateTime:
                    return oracleHelper.GetOracleDbTypeValue(oracleDbType, "Date");
                //return OracleDbType.Date;
                case System.Data.DbType.Boolean:
                    return oracleHelper.GetOracleDbTypeValue(oracleDbType, "Char");
                //return OracleDbType.Char;
                case System.Data.DbType.Binary:
                    return oracleHelper.GetOracleDbTypeValue(oracleDbType, "Blob");
                //return OracleDbType.Blob;
                case System.Data.DbType.Byte:
                    return oracleHelper.GetOracleDbTypeValue(oracleDbType, "Byte");
                // return OracleDbType.Byte;
                default:
                    throw new MB.Util.APPException(string.Format("把DbType{0} 转换为OravleDbType 时出错,请检查相应的数据类型是否已经处理",
                                                    dbType.ToString()), MB.Util.APPMessageType.SysDatabaseInfo);
            }
            //return null;
        }
       
        #endregion 内部函数处理...

        #region IDisposable 成员
        private bool _Disposed = false;
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing) {
            if (!this._Disposed) {
                _Disposed = true;

            }
        }
        ~OracleBulkExecute() {

            Dispose(false);
        }
        #endregion


    }

    #region SimulatedOracleHelper...
    /// <summary>
    /// 临时解决方案，解决Oracle.DataAccess 编译版本和运行版本不一致时会出现异常的问题。
    /// </summary>
    public class SimulatedOracleHelper
    {
        public static readonly string ORACLE_ASSEMBLY = @"Oracle.DataAccess";
        public static readonly string ORACLE_CONNECTION = "Oracle.DataAccess.Client.OracleConnection";
        public static readonly string ORACLE_COMMAND = "Oracle.DataAccess.Client.OracleCommand";
        public static readonly string ORACLE_PARAMTER = "Oracle.DataAccess.Client.OracleParameter";
        public static readonly string ORACLE_FACTORY_PROVIDER = "Oracle.DataAccess.Client.OracleClientFactory";
        //这样处理是有些Oracle 连接字符串配置参数在批量处理中不支持
        private string[] _ORACLE_CONNECTION_STRING_PRO = new string[] { "Data Source", "Persist Security Info", 
            "User ID","Password","Pooling","Min Pool Size","Max Pool Size","Connection Lifetime","Connection Timeout",
            "Incr Pool Size","Decr Pool Size","Enlist" };

        private Assembly ORACLE_DLL;
        public SimulatedOracleHelper() {
            ORACLE_DLL = Assembly.LoadWithPartialName(ORACLE_ASSEMBLY);
            if (ORACLE_DLL == null)
                throw new MB.Util.APPException("加载对象Oracle.DataAccess 出错,请检查该组件是否存在", Util.APPMessageType.SysErrInfo);
        }
        /// <summary>
        /// 获取数据库连接字符窜
        /// </summary>
        /// <returns></returns>
        public string GetOracleDataAccessConnectionString() {
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            return GetOracleDataAccessConnectionString(db);
        }
        /// <summary>
        /// 获取数据库连接字符窜
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public string GetOracleDataAccessConnectionString(Database db) {
          
            string cnStr = ToOracleConnString(db.ConnectionString);
            if (cnStr.IndexOf("Connection Timeout") < 0)
                cnStr = cnStr + ";Connection Timeout=1800";

            return cnStr;
        }
        /// <summary>
        /// 判断是否为Oracle.DataAccess.Client
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool CheckProviderIsOracleDataAccess(Database db) {
            return db.DbProviderFactory!=null && db.DbProviderFactory.GetType().FullName == "Oracle.DataAccess.Client.OracleClientFactory";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="objPars"></param>
        /// <returns></returns>
        public object CreateInstance(string objectName, object[] objPars) {
            return ORACLE_DLL.CreateInstance(objectName, true, BindingFlags.Default, null, objPars, null, null);
        }
        /// <summary>
        /// CreateOracleConnection
        /// 以数据库配置的信息创建一个Oracle.DataAccess.Client
        /// </summary>
        /// <returns></returns>
        public System.Data.Common.DbConnection CreateOracleConnection() {
            var cnStr = GetOracleDataAccessConnectionString();
            return CreateOracleConnection(cnStr);
        }
        /// <summary>
        /// CreateOracleConnection
        /// </summary>
        /// <param name="cn"></param>
        /// <returns></returns>
        public System.Data.Common.DbConnection CreateOracleConnection(string cn) {
            object instance = CreateInstance(ORACLE_CONNECTION, new object[] { cn });

            var dbCn = instance as System.Data.Common.DbConnection;
            if (dbCn == null)
                throw new MB.Util.APPException("MB.BaseFrame 必须升级Oracle Client 为11G及以上版本的情况下才支持批量处理", Util.APPMessageType.SysErrInfo);

            return dbCn;
        }
        /// <summary>
        ///  SetCommandConnection
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="oracleCommand"></param>
        public void SetCommandConnection(System.Data.Common.DbConnection cn, System.Data.Common.DbCommand oracleCommand) {
            MB.Util.MyReflection.Instance.InvokePropertyForSet(oracleCommand, "Connection", cn);
        }
        /// <summary>
        /// CreateOracleCommand
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public System.Data.Common.DbCommand CreateOracleCommand(string sql) {
            object instance = CreateInstance(ORACLE_COMMAND, new object[] { sql });
            if (instance == null)
                throw new MB.Util.APPException(string.Format("根据类型{0},{1} 创建实例出错", ORACLE_COMMAND, ORACLE_ASSEMBLY), MB.Util.APPMessageType.SysErrInfo);

            var dbCmd = instance as System.Data.Common.DbCommand;
            if (dbCmd == null)
                throw new MB.Util.APPException("MB.BaseFrame 必须升级Oracle Client 为11G及以上版本的情况下才支持批量处理", Util.APPMessageType.SysErrInfo);

            return dbCmd;
        }
        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="oracleCommand"></param>
        /// <returns></returns>
        public void ExecuteNonQuery(System.Data.Common.DbCommand oracleCommand) {
            MB.Util.MyReflection.Instance.InvokeMethodByName(oracleCommand, "ExecuteNonQuery");
        }

        /// <summary>
        /// AddOracleParamter
        /// </summary>
        /// <param name="oracleCommand"></param>
        /// <param name="paramter"></param>
        public void AddOracleParamter(System.Data.Common.DbCommand oracleCommand, System.Data.Common.DbParameter paramter) {
            Type cmdType = oracleCommand.GetType();
            object paramters = cmdType.GetProperty("Parameters");
            if (paramters == null)
                throw new MB.Util.APPException(string.Format("在DbCommand 类型{0} 中不包含属性{1}", cmdType.FullName, "Parameters"), Util.APPMessageType.SysErrInfo);

            MB.Util.MyReflection.Instance.InvokeMethodByName(paramters, "Add", paramter);

        }
        /// <summary>
        /// SetCommandArrayBindCount
        /// </summary>
        /// <param name="oracleCommand"></param>
        /// <param name="arrayBindCount"></param>
        public void SetCommandArrayBindCount(System.Data.Common.DbCommand oracleCommand, int arrayBindCount) {
            MB.Util.MyReflection.Instance.InvokePropertyForSet(oracleCommand, "ArrayBindCount", arrayBindCount);
        }
        #region 参数相关...
        /// <summary>
        /// CreateOracleParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="oracleDataType"></param>
        /// <returns></returns>
        public System.Data.Common.DbParameter CreateOracleParameter(string name, object oracleDataType) {
            object instance = CreateInstance(ORACLE_PARAMTER, new object[] { name, oracleDataType });
            return instance as System.Data.Common.DbParameter;

        }
        /// <summary>
        /// 获取OracleDbType 类型。
        /// </summary>
        /// <returns></returns>
        public Type GetOracleDbType() {
            var dbType = ORACLE_DLL.GetType("Oracle.DataAccess.Client.OracleDbType");
            return dbType;
        }
        /// <summary>
        /// 获取OracleDbType Value.
        /// </summary>
        /// <param name="oracleDbType"></param>
        /// <param name="dbTypeName"></param>
        /// <returns></returns>
        public object GetOracleDbTypeValue(Type oracleDbType, string dbTypeName) {
            var field = oracleDbType.GetField(dbTypeName);
            if (field == null)
                throw new MB.Util.APPException(string.Format("字段{0} 在枚举类型{1}中不存在", dbTypeName, oracleDbType.FullName), Util.APPMessageType.SysErrInfo);
            var val = field.GetValue(null);
            return val;
        }
        #endregion 参数相关...
        /// <summary>
        /// 转换成Oracle.DataAccess.Client 的数据库连接字符窜
        /// </summary>
        /// <param name="cnStr"></param>
        /// <returns></returns>
        public string ToOracleConnString(string cnStr) {
            string[] arr = cnStr.Split(';');
            string newStr = string.Empty;
            foreach (string s in arr) {
                string[] temp = s.Split('=');
                
                if (_ORACLE_CONNECTION_STRING_PRO.Contains(temp[0], StringComparer.Create(System.Globalization.CultureInfo.CurrentCulture, true))) {
                    newStr += s;
                    newStr += ";";
                }

            }
            return newStr;
        }
    }

    #endregion SimulatedOracleHelper...
}

