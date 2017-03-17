using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MB.Tools {
    class ShareLib {
        public static readonly string PARAM_WHILE = "#WHILE#";
        public static readonly string PARAM_END = "#END#";
        public static readonly string PARAM_SYSTEM_TIME = "#SystemDateTime#";

        private static readonly string[] TO_INT_COLUMN = new string[] { "ID", "DOC_STATE" };

        public static ShareLib Instance = new ShareLib();
       /// <summary>
       /// 
       /// </summary>
       /// <param name="columnName"></param>
       /// <param name="typeName"></param>
       /// <returns></returns>
        public string SystemTypeNameToDbType(string columnName, string typeName) {
            if (Array.IndexOf<string>(TO_INT_COLUMN,columnName) >= 0)
                return "System.Data.DbType.Int32";
            else if (columnName.LastIndexOf("_ID")==columnName.Length - 3 ) {
                if (string.Compare(typeName, "System.String", true) != 0)
                    return "System.Data.DbType.Int32";
            }


            switch (typeName) {
                case "System.String":
                    return "System.Data.DbType.String";
                case "System.Boolean":
                    return "System.Data.DbType.Boolean";
                case "System.Int16":
                    return "System.Data.DbType.Int16";
                case "System.Int32":
                    return "System.Data.DbType.Int32";
                case "System.Int64":
                    return "System.Data.DbType.Int64";
                case "System.Decimal":
                    return "System.Data.DbType.Decimal";
                case "System.Byte[]":
                    return "System.Data.DbType.Binary";
                case "System.DateTime":
                    return "System.Data.DbType.DateTime";
                default:
                    return "System.Data.DbType.String";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public string ConvertDataType(DataColumn dc, Type dataType) {
            if (!dc.AllowDBNull) {
                return convertDataType(dc.ColumnName, dataType);
            }
            else {
                string notNullType = convertDataType(dc.ColumnName, dataType);
                if (!dataType.IsPrimitive 
                    && dataType != typeof(DateTime)
                    && dataType != typeof(Decimal))
                    return notNullType;
                else {
                    string nullType = notNullType +  "?";
                    return nullType;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private string convertDataType(string columnName, Type dataType) {
            if (Array.IndexOf<string>(TO_INT_COLUMN, columnName) >= 0)
                return "int";
            else if (columnName.LastIndexOf("_ID") == columnName.Length - 3) {
                if(string.Compare(dataType.Name,"string",true)!=0)
                    return "int";
            }

            string name = dataType.Name;
            if (string.Compare(name, "DateTime",true)==0)
                return name;
            else if (string.Compare(name, "Boolean", true) == 0) {
                return "bool";
            }
            else if (string.Compare(name, "Int16", true) == 0) {
                return "short";
            }
            else if (string.Compare(name, "Int32", true) == 0) {
                return "int";
            }
            else if (string.Compare(name, "Int64", true) == 0) {
                return "long";
            }
            else
                return dataType.Name.ToLower();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public string ConvertDataTypeFullName(string columnName, Type dataType) {
            if (Array.IndexOf<string>(TO_INT_COLUMN, columnName) >= 0)
                return  "System.Int32";
            else if (columnName.LastIndexOf("_ID") == columnName.Length - 3) {
                if (string.Compare(dataType.Name, "string", true) != 0)
                    return "System.Int32";
            }

            return dataType.FullName;
        }
        /// <summary>
        /// 读取模板。
        /// </summary>
        /// <param name="templeteName"></param>
        /// <param name="whileLines"></param>
        /// <returns></returns>
        public List<string> ReadRfTemplete(string templeteName, ref List<string> whileLines) {
            List<string> alist = new List<string>();
            System.IO.Stream fs = null;
            System.IO.StreamReader sr = null;
            try {
                fs = this.GetType().Assembly.GetManifestResourceStream(templeteName);
                if (fs == null)
                    throw new System.Exception("需要分析的模板文件不存在！");

                sr = new System.IO.StreamReader(fs, System.Text.Encoding.Default);
                string line = string.Empty;
                bool begWhile = false;
                while ((line = sr.ReadLine()) != null) {
                    if (string.Compare(line.Trim(), PARAM_WHILE, true) == 0) {
                        begWhile = true;
                        alist.Add(line);
                        continue;
                    }
                    if (string.Compare(line.Trim(), PARAM_END, true) == 0) {
                        begWhile = false;
                        continue;
                    }
                    if (begWhile) {
                        whileLines.Add(line);
                    }
                    else {
                        alist.Add(line);
                    }
                }
            }
            catch (System.Exception ex) {
                throw ex;
            }
            finally {
                if (sr != null) {
                    sr.Close();
                }
                fs = null;
                sr = null;
            }
            return alist;
        }

    }
}
