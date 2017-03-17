//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-12。
// Description	:	XML 配置对象SQL 操作处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;

using MB.Util; 
using MB.Orm.DbSql;
using MB.Orm.Exceptions; 
namespace MB.Orm.Mapping.Xml {
    /// <summary>
    /// XML 配置对象SQL 操作处理相关,对于涉及到多表操作都通过这种方式来进行处理。
    /// </summary>
    public sealed class SqlConfigHelper
    {
        #region 定义...

        public static string XML_FILE_PATH = null;
        private static string[] SQL_SPEC_STRING = SqlShareHelper.SQL_SPEC_STRING;
        private static readonly string SQL_PARAMS_PATH = "Entity/Sqls/Sql";

        private static readonly string XML_SQL_STRING_NAME = "SqlString";
        private static readonly string XML_SQL_STRING_TEXT_NAME = "String";
        private static readonly string XML_PARAM_MAPPINGS = "QueryParamMappings";
        //默认的参数长度
        private const int DEFAULT_PARAM_LENGTH = 128;
        #endregion 定义...

        #region 线程安全的单实例实现...
        private static Object _Obj = new object();
        private static SqlConfigHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        public SqlConfigHelper() {
            XML_FILE_PATH = MB.Util.General.GeApplicationDirectory();

            XML_FILE_PATH += ConfigurationManager.AppSettings["XmlConfigPath"];
        }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static SqlConfigHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new SqlConfigHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion 线程安全的单实例实现...

        #region public 成员...
        /// <summary>
        /// 获取指定的SQL 字符窜。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="sqlName"></param>
        /// <returns></returns>
        public  MB.Orm.DbSql.SqlString[]  GetSqlString(string xmlFileName, string sqlName) {
           // string xmlFileFullName = XML_FILE_PATH + xmlFileName + ".xml";
            //统一在方法 getSqlNode 进行判断
            //if (!System.IO.File.Exists(xmlFileFullName))
            //    throw new MB.Util.APPException(string.Format("XML配置文件{0},不存在!", xmlFileFullName), MB.Util.APPMessageType.SysErrInfo);

            System.Xml.XmlNode sqlNode = getSqlNode(xmlFileName, sqlName);
            if (sqlNode == null || sqlNode.ChildNodes.Count == 0) {
                TraceEx.Write("在XML 文件：" + xmlFileName + " 中获取相应SQL 语句：" + sqlName + "出错，请检查资源文件或者对应SQL 语句是否存在！");
                return null;
            }
            List<MB.Orm.DbSql.SqlString> sqlStrings = new List<SqlString>();
            foreach (System.Xml.XmlNode sqlStringNode in sqlNode.ChildNodes) {
                if (sqlStringNode.NodeType != System.Xml.XmlNodeType.Element) continue;
                if (string.Compare(sqlStringNode.Name, XML_SQL_STRING_NAME , true) != 0) continue;

                string str = string.Empty;
                foreach (System.Xml.XmlNode node in sqlStringNode.ChildNodes) {
                    if (node.NodeType != System.Xml.XmlNodeType.Element) continue;
                    if (string.Compare(node.Name, XML_SQL_STRING_TEXT_NAME , true) != 0) continue;
                    TraceEx.Write("成功发现SQL 语句：" + sqlName);
                    str = formatSqlString(node.InnerText.Trim());
                    break;
                }
                List<SqlParamInfo> pars = getSqlParams(sqlStringNode);
                MB.Orm.DbSql.SqlString sqlStr = new MB.Orm.DbSql.SqlString(str, pars);
                sqlStrings.Add(sqlStr);
            }
            return sqlStrings.ToArray(); 
        }

        /// <summary>
        /// 从XML 文件中获取该对象标准SQL 操作语句。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <returns></returns>
        public Dictionary<MB.Orm.Enums.OperationType, MB.Orm.DbSql.SqlString[]> GetObjectStandardEditSql(string xmlFileName,MB.Orm.Enums.OperationType optTypes) {
            Dictionary<MB.Orm.Enums.OperationType, MB.Orm.DbSql.SqlString[]> sqlTable = new Dictionary<MB.Orm.Enums.OperationType, SqlString[]>();

            if ((optTypes & MB.Orm.Enums.OperationType.Insert) != 0) {
                SqlString[] addOrgSqlStr = GetSqlString(xmlFileName, XmlSqlMappingInfo.SQL_ADD_OBJECT);
                if (addOrgSqlStr == null || addOrgSqlStr.Length == 0)
                    throw new XmlSqlConfigNotExistsException(xmlFileName, XmlSqlMappingInfo.SQL_ADD_OBJECT);

                sqlTable[MB.Orm.Enums.OperationType.Insert] = addOrgSqlStr;
            }
            if ((optTypes & MB.Orm.Enums.OperationType.Update) != 0) {
                SqlString[] updateOrgSqlStr = GetSqlString(xmlFileName, XmlSqlMappingInfo.SQL_UPDATE_OBJECT);
                if (updateOrgSqlStr == null || updateOrgSqlStr.Length == 0)
                    throw new XmlSqlConfigNotExistsException(xmlFileName, XmlSqlMappingInfo.SQL_UPDATE_OBJECT);

                sqlTable[MB.Orm.Enums.OperationType.Update] = updateOrgSqlStr;
            }
            if ((optTypes & MB.Orm.Enums.OperationType.DeleteNotIn) != 0) {
                SqlString[] deleteNoInSqlStr = GetSqlString(xmlFileName, XmlSqlMappingInfo.SQL_DELETE_NOT_IN_IDS);
                if (deleteNoInSqlStr == null || deleteNoInSqlStr.Length ==0)
                    throw new XmlSqlConfigNotExistsException(xmlFileName, XmlSqlMappingInfo.SQL_DELETE_NOT_IN_IDS);

                sqlTable[MB.Orm.Enums.OperationType.DeleteNotIn] = deleteNoInSqlStr;
            }
            return sqlTable;
        }
        /// <summary>
        /// 根据指定的 XML 和 SQL 名称获取参数的Mapping 配置信息。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="sqlName"></param>
        /// <returns></returns>
        public MB.Orm.Mapping.QueryParameterMappings GetSqlQueryParamMappings(string xmlFileName, string sqlName) {
            

            System.Xml.XmlNode sqlNode = getSqlNode(xmlFileName, sqlName);
            if (sqlNode == null || sqlNode.ChildNodes.Count == 0) {
                TraceEx.Write("在XML 文件：" + xmlFileName + " 中获取相应SQL 语句：" + sqlName + "出错，请检查资源文件或者对应SQL 语句是否存在！");
                return null;
            }
            MB.Orm.Mapping.QueryParameterMappings mappings = null; 
            foreach (System.Xml.XmlNode paramMappingNode in sqlNode.ChildNodes) {
                if (paramMappingNode.NodeType != System.Xml.XmlNodeType.Element) continue;
                if (string.Compare(paramMappingNode.Name, XML_PARAM_MAPPINGS, true) != 0) continue;

                mappings = new QueryParameterMappings();
                if (paramMappingNode.Attributes["DefaultTableAlias"] != null)
                    mappings.DefaultTableAlias = paramMappingNode.Attributes["DefaultTableAlias"].Value;

                foreach (System.Xml.XmlNode node in paramMappingNode.ChildNodes) {
                    if (node.NodeType != System.Xml.XmlNodeType.Element) continue;
                    if (string.Compare(node.Name, "Mapping", true) != 0) continue;
                    if (node.Attributes["Name"] == null || node.Attributes["DbFieldName"] == null)
                        throw new MB.Util.APPException(string.Format("XML配置文件{0},配置QueryParamMappings 是可能没有配置 Name 或者 DbFieldName!", xmlFileName));

                    mappings.Add(new QueryParameterMappingInfo(node.Attributes["Name"].Value,node.Attributes["DbFieldName"].Value));

                }
                break;
            }
            return mappings;
        }
        #endregion public 成员...

        #region 内部函数处理...

        //根据文件名称创建XML 文档。
        private System.Xml.XmlDocument createXmlDocument(string xmlFileName) {
           return (new XmlResourceHelper()).CreateXmlDocument(xmlFileName);
        }
        //获取指定的SQL NODE 节点。
        private System.Xml.XmlNode getSqlNode(string xmlFileName, string sqlName) {
            //string xmlFileFullName = xmlFileName + ".xml";

            System.Xml.XmlDocument xDoc = createXmlDocument(xmlFileName);

            System.Xml.XmlNodeList nodes = xDoc.SelectNodes(SQL_PARAMS_PATH);
            foreach (System.Xml.XmlNode node in nodes) {
                if (node.NodeType != System.Xml.XmlNodeType.Element) continue;

                if (node.Attributes["Name"] == null) continue;

                if (string.Compare(node.Attributes["Name"].Value.ToString(), sqlName, true) != 0) continue;

                return node;
            }
            return null;
        }
        //获取SQL 节点的所有参数信息。
        private List<SqlParamInfo> getSqlParams(System.Xml.XmlNode sqlNode) {
            List<SqlParamInfo> pars = new List<SqlParamInfo>();
            if (sqlNode == null || sqlNode.ChildNodes.Count == 0) return pars;

            foreach (System.Xml.XmlNode node in sqlNode.ChildNodes) {
                if (node.NodeType != System.Xml.XmlNodeType.Element) continue;
                if (string.Compare(node.Name, "Param", true) != 0) continue;

                SqlParamInfo sqlParam = new SqlParamInfo();
                string parName = getNodeAttValue(node, "Name");
                parName = MB.Orm.DbSql.SqlShareHelper.SQL_XML_CFG_PARAM_PREFIX + parName.Substring(1, parName.Length - 1);
                sqlParam.Name = parName;  
                string mapppingColumn = getNodeAttValue(node, "Column");
                if (!string.IsNullOrEmpty(mapppingColumn))
                    sqlParam.MappingName = mapppingColumn;

                string description = getNodeAttValue(node, "Description");
                if (string.IsNullOrEmpty(description))
                    sqlParam.Description = sqlParam.Name;
                else
                    sqlParam.Description = description;

                string typeName = getNodeAttValue(node, "TypeName");
                if (string.IsNullOrEmpty(typeName))
                    sqlParam.DbType = MB.Orm.Common.DbShare.Instance.SystemTypeNameToDbType("System.String");
                else
                    sqlParam.DbType = MB.Orm.Common.DbShare.Instance.SystemTypeNameToDbType(typeName);

                string length = getNodeAttValue(node, "Length");
                if (string.IsNullOrEmpty(length))
                    sqlParam.Length = DEFAULT_PARAM_LENGTH;
                else
                    sqlParam.Length = MB.Util.MyConvert.Instance.ToInt(length);

                string overcast = getNodeAttValue(node, "Overcast");
                if (!string.IsNullOrEmpty(overcast))
                    sqlParam.Overcast = MB.Util.MyConvert.Instance.ToBool(overcast);    

                string direction = getNodeAttValue(node, "Direction");
                if (string.IsNullOrEmpty(direction))
                    sqlParam.Direction = System.Data.ParameterDirection.Input;
                else
                    sqlParam.Direction = (System.Data.ParameterDirection)Enum.Parse(typeof(System.Data.ParameterDirection), direction);

                //如果是特殊字段的话,那么现阶段只能采取覆盖的方式来进行处理
                if (MB.Orm.DbSql.SqlShareHelper.HS_SQL_SPEC_STRING.ContainsKey(parName.ToUpper()))
                    sqlParam.Overcast = true;
 
                pars.Add(sqlParam);
            }
            return pars;
        }
        //获取节点属性的配置值。
        private string getNodeAttValue(System.Xml.XmlNode xmlNode, string attName) {
            return xmlNode.Attributes[attName] != null ? xmlNode.Attributes[attName].Value.ToString() : string.Empty;
        }
        //格式化SQL 语句。
        private string formatSqlString(string strSql) {
            if (string.IsNullOrEmpty(strSql)) return string.Empty;

            string strTemp = strSql;

            //去掉回车和换行符
            strTemp = Regex.Replace(strTemp, @"[\r\n]+", " ", RegexOptions.IgnoreCase);
            //去掉换行符
            strTemp = Regex.Replace(strTemp, @"[\t]+", " ", RegexOptions.IgnoreCase);
            //去掉多余空格
            strTemp = Regex.Replace(strTemp, @"[ ]+", " ", RegexOptions.IgnoreCase);

            strTemp = strTemp.Replace("@",MB.Orm.DbSql.SqlShareHelper.SQL_XML_CFG_PARAM_PREFIX);
            strTemp = strTemp.Replace(":", MB.Orm.DbSql.SqlShareHelper.SQL_XML_CFG_PARAM_PREFIX);  
            return strTemp;
        }
 
        #endregion 内部函数处理...
    }


}
