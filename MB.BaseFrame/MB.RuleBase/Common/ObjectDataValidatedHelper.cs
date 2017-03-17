//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-07-14
// Description	:	对象数据验证处理相关。
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
    /// 对象数据验证处理相关。
    /// </summary>
    public class ObjectDataValidatedHelper {

        #region 实体数据检验处理相关...
        /// <summary>
        /// 根据数据类型检查指定的值在数据库中是否已经存在
        /// </summary>
        /// <param name="dataInDocType">需要进行检查的数据类型</param>
        /// <param name="entity">需要检查的实体对象</param>
        /// <param name="checkPropertys">需要检查的属性名称</param>
        /// <returns>true 或 false</returns>
        public bool CheckValueIsExists(object dataInDocType, object entity, string[] checkPropertys) {
            
            ObjectDataMappingAttribute mappingAtt = AttributeConfigHelper.Instance.GetObjectDataMappingAttribute(dataInDocType);
            if (mappingAtt == null)
                throw new MB.RuleBase.Exceptions.RequireConfigDataMappingException(dataInDocType);

            string tableName = mappingAtt.MappingTableName;
            object keyValue = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, mappingAtt.KeyName);
            if (keyValue == null)
                keyValue = string.Empty;

            string sql = string.Format("SELECT 1 FROM {0} WHERE ROWNUM <=1 AND {1}<>'{2}' AND ", tableName, mappingAtt.KeyName, keyValue.ToString().Replace("'", "''"));
            string where = string.Empty;
            MB.Orm.Enums.DatabaseType dbaseType = MB.Orm.Persistence.DatabaseHelper.CreateDatabaseType(); 
            foreach (string name in checkPropertys) {
                if (!string.IsNullOrEmpty(where))
                    where += " AND ";
                object checkVal = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, name);
                Type type = MB.Util.MyReflection.Instance.GetPropertyType(entity, name);

                if (type == typeof(string)) //如果是string
                {
                    string checkValStr = checkVal as string;
                    if (string.IsNullOrEmpty(checkValStr))
                        where += string.Format(" ({0} = '' OR {0} IS NULL) ", name);
                    else
                        where += string.Format("{0} = '{1}'", name, checkVal.ToString().Replace("'", "''"));
                }
                else if (type == typeof(DateTime)) {
                    //如果是日期类型，日期类型只检查到
                    DateTime checkValueUnBoxed = (DateTime)checkVal;
                    if (dbaseType == DatabaseType.Oracle) {
                        where += string.Format("TO_CHAR({0}, 'YYYY-MM-DD') = '{1}'", name, checkValueUnBoxed.ToString("yyyy-MM-dd").Replace("'", "''"));
                    }
                    else {
                        where += string.Format("{0} = '{1}'", name, checkValueUnBoxed.ToString("yyyy-MM-dd").Replace("'", "''"));
                    }
                }
                else if (type == typeof(DateTime?)) {
                    if (checkVal == null) {
                        where += string.Format(" {0} IS NULL ", name);
                    }
                    else {
                        DateTime checkValueUnBoxed = (DateTime)checkVal;
                        if (dbaseType == DatabaseType.Oracle) {
                            //如果是日期类型，日期类型只检查到
                            where += string.Format("TO_CHAR({0}, 'YYYY-MM-DD') = '{1}'", name, checkValueUnBoxed.ToString("yyyy-MM-dd").Replace("'", "''"));
                        }
                        else {
                            where += string.Format("{0} = '{1}'", name, checkValueUnBoxed.ToString("yyyy-MM-dd").Replace("'", "''"));
                        }
                    }
                }
                else {
                    if (checkVal == null)
                        where += string.Format(" {0} IS NULL ", name);
                    else
                        where += string.Format("{0} = '{1}'", name, checkVal.ToString().Replace("'", "''"));
                }
            }
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            
            sql += where;

            DbCommand dbCmd = db.GetSqlStringCommand(sql);
            object val = DatabaseExecuteHelper.NewInstance.ExecuteScalar(db, dbCmd);
            if (val != null)
                return MB.Util.MyConvert.Instance.ToBool(val);
            else
                return false;

        }
        #endregion 实体数据检验处理相关...

        /// <summary>
        /// 移除数据库中不存在的数据。
        /// 检查的参数数组采取 IN 的处理方法，如果在拼接IN 语句时，长度超过8000个字符，方法自动会进行拆分处理，
        /// 外部没必要特殊考虑。
        /// </summary>
        /// <param name="tableName">需要检查指定的表名称</param>
        /// <param name="fieldName">检查对应表的中的名称</param>
        /// <param name="checkValues">需要检查的值</param>
        /// <returns>返回数据库中存在（或者满足指定的某种条件）的值</returns>
        public string[] RemoveNotExistsData(string tableName, string fieldName, string[] checkValues) {
            //SQL 拆分目前还未实现
            string org_sql = "SELECT DISTINCT {0} FROM {1} WHERE {2} IN ({3})";
            string[] wheres = MB.Orm.DbSql.SqlShareHelper.Instance.SplitInSqlStringBySqlMaxLength<string>(checkValues);
            List<string> existsValues = new List<string>();
            Database db = MB.Orm.Persistence.DatabaseHelper.CreateDatabase();
            var exeDb = DatabaseExecuteHelper.NewInstance;
            foreach (string sWhere in wheres) {
                string sql = string.Format(org_sql, fieldName, tableName, fieldName, sWhere);
                DbCommand dbCmd = db.GetSqlStringCommand(sql);
                using (IDataReader reader = exeDb.ExecuteReader(db, dbCmd)) {
                    try {

                        while (reader.Read()) {
                            if (reader[0] == System.DBNull.Value) continue;
                            existsValues.Add(reader[0].ToString());
                        }
                    }
                    catch (Exception ex) {
                        throw new MB.RuleBase.Exceptions.DatabaseExecuteException("在执行 RemoveNotExistsData 读取数据时出错", ex);
                    }
                    finally {
                        //if (!reader.IsClosed)
                        //    reader.Close();
                    }
                }
            }
            return existsValues.ToArray();
        }
        /// <summary>
        /// 移除数据库中不存在的数据。
        /// 检查的参数数组采取 IN 的处理方法，如果在拼接IN 语句时，长度超过8000个字符，方法自动会进行拆分处理，
        /// 外部没必要特殊考虑。
        /// </summary>
        /// <param name="xmlFileName">SQL 语句配置所在的XML 文件名称</param>
        /// <param name="sqlName">xml 文件中配置的SQL 语句名称（对应的SQL 语句至少包含@Where 参数 同时必须是最后一个,SQL 语句的SELECT 第一个字段为检查的值）</param>
        /// <param name="fieldName">需要检查的字段名称(注意 必须是包含别名的完整的可直接拼接SQL 查询语句的字段名称)</param>
        /// <param name="checkValues">需要检查的值</param>
        /// <param name="parValues">除了@Where 参数外其它SQL 语句中配置的参数值</param>
        /// <returns></returns>
        public string[] RemoveNotExistsData(string xmlFileName, string sqlName, string fieldName, string[] checkValues, params object[] parValues) {
            string org_sql = "{0} IN ({1})";
            string[] wheres = MB.Orm.DbSql.SqlShareHelper.Instance.SplitInSqlStringBySqlMaxLength<string>(checkValues);
            List<string> existsValues = new List<string>();
            var dbExec = DatabaseExcuteByXmlHelper.NewInstance;
            foreach (string sWhere in wheres) {
                string sql = string.Format(org_sql, fieldName, sWhere);
                List<object> vals = new List<object>();
                if (parValues != null && parValues.Length > 0) {
                    foreach (object o in parValues)
                        vals.Add(o);
                }
                vals.Add(sql);
                DataSet dsData = dbExec.GetDataSetByXml(xmlFileName, sqlName, vals.ToArray());
                if (dsData.Tables[0].Rows.Count > 0) {
                    DataRow[] drs = dsData.Tables[0].Select();
                    foreach (DataRow dr in drs) {
                        existsValues.Add(dr[0].ToString());
                    }
                }
            }
            return existsValues.ToArray();
        }

    }
}
