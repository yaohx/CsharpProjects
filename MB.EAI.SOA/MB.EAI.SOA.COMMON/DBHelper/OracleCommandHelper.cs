using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using MB.Orm.DbSql;
using MB.Orm.Mapping.Xml;
using MB.Util;
using System.Data;

namespace MB.EAI.SOA.COMMON.DBHelper
{
    public class OracleCommandHelper
    {
        public OracleCommand[] CreateDbCommandByXml(string xmlFileName, string sqlName, params object[] parValues)
        {
            SqlString[] sqlString = SqlConfigHelper.Instance.GetSqlString(xmlFileName, sqlName);
            if ((sqlString == null) || (sqlString.Length == 0))
            {
                throw new MB.Orm.Exceptions.XmlSqlConfigNotExistsException(xmlFileName, sqlName);
            }
            List<OracleCommand> list = new List<OracleCommand>();
            foreach (SqlString str in sqlString)
            {
                IList<SqlParamInfo> paramFields = str.ParamFields;
                if (((paramFields != null) && (paramFields.Count > 0)) && ((parValues == null) || (parValues.Length != paramFields.Count)))
                {
                    throw new APPException("调用XML文件:" + xmlFileName + " 下的SQL" + sqlName + " 传入的参数和配置的参数不一致！");
                }
                OracleCommand sqlStringCommand = GetSqlStringCommand(str.SqlStr);
                if ((paramFields != null) && (paramFields.Count > 0))
                {
                    for (int i = 0; i < paramFields.Count; i++)
                    {
                        SqlParamInfo parInfo = paramFields[i];
                        AddParamInfoToDbCommand(sqlStringCommand, parInfo, parValues[i]);
                    }
                }
                list.Add(sqlStringCommand);
            }
            return list.ToArray();
        }
        public OracleCommand[] CreateDbCommandByXml<T>(string xmlFileName, string sqlName, T entity) where T:class
        {
            SqlString[] sqlString = SqlConfigHelper.Instance.GetSqlString(xmlFileName, sqlName);
            if ((sqlString == null) || (sqlString.Length == 0))
            {
                throw new MB.Orm.Exceptions.XmlSqlConfigNotExistsException(xmlFileName, sqlName);
            }
            List<OracleCommand> list = new List<OracleCommand>();
            foreach (SqlString str in sqlString)
            {
                IList<SqlParamInfo> paramFields = str.ParamFields;
                
                OracleCommand sqlStringCommand = GetSqlStringCommand(str.SqlStr);
                if ((paramFields != null) && (paramFields.Count > 0))
                {
                    for (int i = 0; i < paramFields.Count; i++)
                    {
                        SqlParamInfo parInfo = paramFields[i];
                        var t=entity.GetType().GetProperty(parInfo.MappingName).GetValue(entity,null);
                        AddParamInfoToDbCommand(sqlStringCommand, parInfo,t );
                    }
                }
                list.Add(sqlStringCommand);
            }
            return list.ToArray();
        }
        public OracleCommand[] CreateDbCommandByXml<T>(string xmlFileName, string sqlName, List<T> entities) where T : class
        {
            List<OracleCommand> list = new List<OracleCommand>();
            if (entities != null && entities.Count > 1)
                foreach (var one in entities)
                    list.AddRange(CreateDbCommandByXml<T>(xmlFileName, sqlName, one));
            return list.ToArray();

        }


        private OracleCommand GetSqlStringCommand(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException("query is null", "query");
            }

            OracleCommand command = (OracleCommand)OracleClientFactory.Instance.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = ReplacePrefix(query);
            return command;
        }
        private string ReplacePrefix(string query)
        {
            return ReplaceIllegalChar(query).Replace("@", ":");

        }
        private string ReplaceIllegalChar(string query)
        {
            return query.Replace(";", "");

        }

        private OracleDbType FromDbType(DbType type)
        {
            switch (type)
            {
                case DbType.String:
                case DbType.AnsiString:
                    return OracleDbType.Varchar2;

                case DbType.AnsiStringFixedLength:
                case DbType.StringFixedLength:
                    return OracleDbType.Char; ;

                case DbType.Boolean:
                    return OracleDbType.Byte;

                case DbType.Int16:
                    return OracleDbType.Int16;
                case DbType.Int32:
                    return OracleDbType.Int32;
                case DbType.Int64:
                    return OracleDbType.Int64;

                case DbType.Decimal:
                    return OracleDbType.Decimal;

                case DbType.Binary:
                    return OracleDbType.Blob;

                case DbType.DateTime:
                    return OracleDbType.Date;

                default:
                    return OracleDbType.Varchar2;
            }
        }
        private void AddParamInfoToDbCommand(OracleCommand command, SqlParamInfo parInfo, object value)
        {
            OracleParameter parameter = new OracleParameter(parInfo.Name, FromDbType(parInfo.DbType));
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }
}
