//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2010-03-24
// Description	:	轻量级的对象系列化过程，适合大批量的数据处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;

namespace MB.Util.Serializer {
    /// <summary>
    /// 以字符窜拼接的方式完成实体对象的系列化。
    /// 系列化后的值是压缩过的。
    /// </summary>
    public class LightweightTextSerializer {
        //每5千个实体启动一个新的线线来进行处理
        private const int SINGLE_L = 5000;
        //表示第一行为标志位,该系列化数据的说明信息。
        private static string ENTITY_LIST = "EntityList";
        private static string DATA_TABLE = "DataTable";
        private static string FLAG_WORD = "[FLAG]={0}";
        private static string SE_COLUMNS = "[COLUMNS]={0}";

        private Encoding _Encoding;


        #region 构造函数...
        /// <summary>
        ///以字符窜拼接的方式完成实体对象的系列化。
        /// 默认使用 UnicodeEncoding 进行编码,支持中文
        /// </summary>
        public LightweightTextSerializer()
            : this(new UnicodeEncoding()) {
        }
        /// <summary>
        /// 以字符窜拼接的方式完成实体对象的系列化。
        /// </summary>
        /// <param name="encoding">编码方式,如果在实体类中不包括中文，最好使用单字节的编码器。</param>
        public LightweightTextSerializer(Encoding encoding) {
            _Encoding = encoding;
        }
        #endregion 构造函数...

        #region public 函数...

        #region Serializer...
        /// <summary>
        ///  以最简单的方式系列化为Bytes 的格式。
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public byte[] Serializer(IDataReader dataReader) {
            //ColumnName 
            DataTable headerReader = dataReader.GetSchemaTable();
            //构建表头：
            DataRow[] heads = headerReader.Select();
            List<ColumnInfo> cols = new List<ColumnInfo>();
            foreach (DataRow dr in heads) {
                cols.Add(new ColumnInfo(dr["ColumnName"].ToString(), (Type)dr["DataType"]));
            }
            TextTableSchema schema = new TextTableSchema();
            string flag = schema.ColumnsToString(cols);
            string header = buildTableHeader(cols);
            using (MemoryStream stream = new MemoryStream()) {
                flag = string.Format(FLAG_WORD, DATA_TABLE) + ";" + string.Format(SE_COLUMNS, flag);
                byte[] bufferflag = _Encoding.GetBytes(flag + "\n");
                stream.Write(bufferflag, 0, bufferflag.Length);
                byte[] buffer = _Encoding.GetBytes(header + "\n");
                stream.Write(buffer, 0, buffer.Length);
                while (dataReader.Read()) {
                    string line = buildDataRow(cols, dataReader);

                    buffer = _Encoding.GetBytes(line + "\n");
                    stream.Write(buffer, 0, buffer.Length);
                }

                dataReader.Close();

                //压缩
                byte[] bytes_c = MB.Util.Compression.Instance.Zip(stream.ToArray());
                return bytes_c;
            }
        }
        /// <summary>
        /// 以最简单的方式系列化为Bytes 的格式。
        /// </summary>
        /// <param name="dtData"></param>
        /// <returns></returns>
        public byte[] Serializer(DataTable dtData) {
            List<ColumnInfo> cols = new List<ColumnInfo>();
            foreach (DataColumn col in dtData.Columns) {
                cols.Add(new ColumnInfo(col.ColumnName, col.DataType));
            }
            TextTableSchema schema = new TextTableSchema();
            string flag = schema.ColumnsToString(cols);
            string header = buildTableHeader(cols);
            DataRow[] drs = dtData.Select();
            using (MemoryStream stream = new MemoryStream()) {
                flag = string.Format(FLAG_WORD, DATA_TABLE) + ";" + string.Format(SE_COLUMNS, flag);
                byte[] bufferflag = _Encoding.GetBytes(flag + "\n");
                stream.Write(bufferflag, 0, bufferflag.Length);
                byte[] buffer = _Encoding.GetBytes(header + "\n");
                stream.Write(buffer, 0, buffer.Length);
                foreach (DataRow dr in drs) {
                    string line = buildDataRow(cols, dr);
                    buffer = _Encoding.GetBytes(line + "\n");
                    stream.Write(buffer, 0, buffer.Length);
                }
                //压缩
                byte[] bytes_c = MB.Util.Compression.Instance.Zip(stream.ToArray());
                return bytes_c;
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 以最简单的方式系列化为Bytes 的格式。
        /// </summary>
        /// <param name="entityType">需要系列化的实体对象类型</param>
        /// <param name="lstData">数据集合</param>
        /// <returns>压缩后的值</returns>
        public byte[] Serializer(Type entityType, IList lstData) {

            PropertyInfo[] infos = entityType.GetProperties();
            Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> dAccs = new Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor>();
            foreach (PropertyInfo info in infos) {
                object[] atts = info.GetCustomAttributes(typeof(DataMemberAttribute), true);
                if (atts == null || atts.Length == 0) continue;

                dAccs.Add(info.Name, new MB.Util.Emit.DynamicPropertyAccessor(entityType, info));
            }
            using (MemoryStream stream = new MemoryStream()) {
                string flag = string.Format(FLAG_WORD, ENTITY_LIST) + ";" + string.Format(SE_COLUMNS, entityType.Name);
                byte[] bufferflag = _Encoding.GetBytes(flag + "\n");
                stream.Write(bufferflag, 0, bufferflag.Length);

                string header = buildTableHeader(dAccs);
                byte[] buffer = _Encoding.GetBytes(header + "\n");
                stream.Write(buffer, 0, buffer.Length);
                foreach (object entity in lstData) {
                    string line = buildDataRow(dAccs, entity);
                    buffer = _Encoding.GetBytes(line + "\n");
                    stream.Write(buffer, 0, buffer.Length);
                }
                //压缩
                byte[] bytes_c = MB.Util.Compression.Instance.Zip(stream.ToArray());
                return bytes_c;
            }

        }
        #endregion Serializer...
        /// <summary>
        /// 以最简单的方式系列化为Bytes 的格式。
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public DataTable DeSerializer(byte[] datas) {
            TextTableSchema schema = new TextTableSchema();
            byte[] bytes_c = MB.Util.Compression.Instance.UnZip(datas);
            string dstr = _Encoding.GetString(bytes_c);
            string colString;
            string header;
            List<string> dataRows = new List<string>();
            using (StringReader reader = new StringReader(dstr)) {
                //先取Flag  第一行的标志行 为构造DataTable 
                string[] flag = reader.ReadLine().Split(';');
                if (flag.Length < 2)
                    throw new MB.Util.APPException("反系列化时出错,不是有效的系列化文件", MB.Util.APPMessageType.SysErrInfo);

                colString = flag[1].Substring(SE_COLUMNS.Length - 3, flag[1].Length - SE_COLUMNS.Length + 3);

                //先读取表头
                header = reader.ReadLine();
                string[] colFields = System.Text.RegularExpressions.Regex.Split(header, "\t");
                while (true) {
                    string dataRow = reader.ReadLine();
                    if (string.IsNullOrEmpty(dataRow)) break;
                    dataRows.Add(dataRow);
                }
            }
            int all = computeCount(dataRows.Count);
            var cols = schema.StringToColumns(colString);
            DataTable dt = schema.CreateDataTable(cols);
            List<MultiSetTableValue> lst = new List<MultiSetTableValue>();
            for (int i = 0; i < all; i++) {
                int l = (i + 1) * SINGLE_L > dataRows.Count ? dataRows.Count - i * SINGLE_L : SINGLE_L;
                List<string> childLst = dataRows.GetRange(i * SINGLE_L, l);
                lst.Add(new MultiSetTableValue(colString, childLst));
                Thread t = new Thread(new ThreadStart(lst[i].SetValue));
                t.Priority = ThreadPriority.Highest;
                t.Start();
            }
            ArrayList lstData = new ArrayList();
            for (int i = 0; i < lst.Count; i++) {
                if (lst[i].Done) {    //保证按系列化前的顺序加载到集合中。
                    dt.Merge(lst[i].CreateDataTable);
                }
                else {
                    i--;
                    Thread.Sleep(100);
                }
            }
            return dt;
        }
        /// <summary>
        /// 把Bytes 转换为实体对象集合类。
        /// </summary>
        /// <param name="entityType">需要系列化的实体对象类型</param>
        /// <param name="datas">系列化压缩后的Bytes</param>
        /// <returns></returns>
        public IList DeSerializer(Type entityType, byte[] datas) {
            byte[] bytes_c = MB.Util.Compression.Instance.UnZip(datas);
            string dstr = _Encoding.GetString(bytes_c);
            List<string> dataRows = new List<string>();
            string header;
            using (StringReader reader = new StringReader(dstr)) {
                //先取Flag  第一行的标志行 为构造DataTable 
                string f = reader.ReadLine();
                string[] flag = f.Split(';');
                //在老的版本中，第一行就是表头
                if (flag.Length == 0)
                    header = f;
                else
                    //先读取表头
                    header = reader.ReadLine();

                while (true) {
                    string dataRow = reader.ReadLine();
                    if (string.IsNullOrEmpty(dataRow)) break;
                    dataRows.Add(dataRow);
                }
            }

            int all = computeCount(dataRows.Count);
            List<MultiSetEntityValue> lst = new List<MultiSetEntityValue>();
            for (int i = 0; i < all; i++) {
                int l = (i + 1) * SINGLE_L > dataRows.Count ? dataRows.Count - i * SINGLE_L : SINGLE_L;
                List<string> childLst = dataRows.GetRange(i * SINGLE_L, l);
                lst.Add(new MultiSetEntityValue(entityType, childLst, header));
                Thread t = new Thread(new ThreadStart(lst[i].SetValue));
                t.Priority = ThreadPriority.Highest;
                t.Start();
            }
            ArrayList lstData = new ArrayList();
            for (int i = 0; i < lst.Count; i++) {
                if (lst[i].Done) {    //保证按系列化前的顺序加载到集合中。
                    lstData.AddRange(lst[i].CreateListData);
                }
                else {
                    i--;
                    Thread.Sleep(100);
                }
            }
            return lstData;
        }
        #endregion public 函数...

        #region 内部函数处理...
        private int computeCount(int count) {

            int c;
            int re = Math.DivRem(count, SINGLE_L, out c);

            return System.Convert.ToInt32(c > 0 ? re + 1 : re);

        }
        private List<MB.Util.Emit.DynamicPropertyAccessor> convertToDynaAcc(Type entityType, string header) {
            List<MB.Util.Emit.DynamicPropertyAccessor> dAccs = new List<MB.Util.Emit.DynamicPropertyAccessor>();
            string[] cols = System.Text.RegularExpressions.Regex.Split(header, "\t");
            foreach (string col in cols) {
                //判断是否为最后一列
                if (string.IsNullOrEmpty(col)) break;
                dAccs.Add(new MB.Util.Emit.DynamicPropertyAccessor(entityType, col));
            }
            return dAccs;
        }

        #region buildTableHeader...
        private string buildTableHeader(Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> dAccs) {
            StringBuilder sb = new StringBuilder();
            foreach (string key in dAccs.Keys) {
                sb.Append(key + "\t");
            }
            return sb.ToString();
        }
        private string buildTableHeader(List<ColumnInfo> cols) {
            StringBuilder sb = new StringBuilder();
            foreach (ColumnInfo col in cols) {
                sb.Append(col.Name + "\t");
            }
            return sb.ToString();
        }
        #endregion buildTableHeader...

        #region buildDataRow...
        private string buildDataRow(Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> dAccs, object entity) {
            StringBuilder sb = new StringBuilder();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-CN", false);
            foreach (string key in dAccs.Keys) {
                object val = dAccs[key].Get(entity);
                if (val != null) {
                    if (dAccs[key].PropertyType.Equals(typeof(string)))
                        sb.Append(replaceSpecChar(val));
                    else
                        sb.Append(val);
                }
                sb.Append("\t");
            }
            return sb.ToString();
        }
        private string buildDataRow(List<ColumnInfo> cols, DataRow drData) {
            StringBuilder sb = new StringBuilder();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-CN", false);
            foreach (ColumnInfo key in cols) {
                object val = drData[key.Name];
                if (val != null && val != System.DBNull.Value) {
                    if (key.DataType.Equals(typeof(string)))
                        sb.Append(replaceSpecChar(val));
                    else
                        sb.Append(val);
                }
                sb.Append("\t");
            }
            return sb.ToString();
        }
        private string buildDataRow(List<ColumnInfo> cols, IDataReader reader) {
            StringBuilder sb = new StringBuilder();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-CN", false);
            object[] vals = new object[cols.Count];
            reader.GetValues(vals);
            for (int i = 0; i < cols.Count; i++) {
                object val = vals[i];
                if (val != null && val != System.DBNull.Value) {
                    if (cols[i].DataType.Equals(typeof(string)))
                        sb.Append(replaceSpecChar(val));
                    else
                        sb.Append(val);
                }
                sb.Append("\t");
            }
            return sb.ToString();
        }

        #endregion buildDataRow...

        // 替换特殊字符
        private string replaceSpecChar(object val) {
            if (val == null || val == System.DBNull.Value) return null;
            string str = val.ToString();
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\t", string.Empty);
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\r", string.Empty);
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\n", string.Empty);

            return str;
        }
        #endregion 内部函数处理...

        #region SetEntityValue...
        /// <summary>
        /// 启动一个多线程完成赋值的操作
        /// </summary>
        class MultiSetEntityValue {
            private List<string> _LstDataRows;
            private string _DAccs;
            private Type _EntityType;
            private ArrayList _CreateListData;
            private bool _Done;
            public MultiSetEntityValue(Type entityType, List<string> lstDataRow, string dAccs) {
                _LstDataRows = lstDataRow;
                _DAccs = dAccs;
                _EntityType = entityType;
            }
            public ArrayList CreateListData {
                get {
                    return _CreateListData;
                }
            }
            public bool Done {
                get {
                    return _Done;
                }
            }
            public void SetValue() {
                _CreateListData = new ArrayList();
                var dAccs = convertToDynaAcc(_EntityType, _DAccs);
                foreach (string dataRow in _LstDataRows) {
                    object newEntity = MB.Util.DllFactory.Instance.CreateInstance(_EntityType);
                    string[] colData = System.Text.RegularExpressions.Regex.Split(dataRow, "\t");

                    for (int i = 0; i < dAccs.Count; i++) {
                        var acc = dAccs[i];
                        string val = colData[i];
                        if (!string.IsNullOrEmpty(val))
                            acc.Set(newEntity, val);
                    }
                    _CreateListData.Add(newEntity);
                }
                _Done = true;
            }

            private List<MB.Util.Emit.DynamicPropertyAccessor> convertToDynaAcc(Type entityType, string header) {
                List<MB.Util.Emit.DynamicPropertyAccessor> dAccs = new List<MB.Util.Emit.DynamicPropertyAccessor>();
                string[] cols = System.Text.RegularExpressions.Regex.Split(header, "\t");
                foreach (string col in cols) {
                    //判断是否为最后一列
                    if (string.IsNullOrEmpty(col)) break;
                    dAccs.Add(new MB.Util.Emit.DynamicPropertyAccessor(entityType, col));
                }
                return dAccs;
            }
        }
        /// <summary>
        /// 启动一个多线程完成赋值的操作
        /// </summary>
        class MultiSetTableValue {
            private List<string> _LstDataRows;
            private List<ColumnInfo> _Cols;

            private DataTable _CreateDataTable;
            private bool _Done;
            private TextTableSchema schema;
            public MultiSetTableValue(string colstring, List<string> lstDataRow) {
                schema = new TextTableSchema();
                _LstDataRows = lstDataRow;
                _Cols = schema.StringToColumns(colstring);

            }
            public DataTable CreateDataTable {
                get {
                    return _CreateDataTable;
                }
            }
            public bool Done {
                get {
                    return _Done;
                }
            }
            public void SetValue() {
                _CreateDataTable = createDataTable(_Cols);
                foreach (string dataRow in _LstDataRows) {
                    DataRow newDr = _CreateDataTable.NewRow();
                    for (int i = 0; i < _Cols.Count; i++) {

                        string[] vals = System.Text.RegularExpressions.Regex.Split(dataRow, "\t");

                        string name = _Cols[i].Name;
                        if (!string.IsNullOrEmpty(vals[i]))
                            newDr[name] = MB.Util.MyReflection.Instance.ConvertValueType(_CreateDataTable.Columns[name].DataType, vals[i]);
                    }
                    _CreateDataTable.Rows.Add(newDr);
                }
                _Done = true;
            }

            private DataTable createDataTable(List<ColumnInfo> cols) {
                DataTable dtNew = new DataTable();
                foreach (var col in cols) {
                    dtNew.Columns.Add(col.Name, col.DataType);
                }
                return dtNew;
            }
        }
        #endregion SetEntityValue...




    }

    #region 单线程处理方法...
    //   public DataTable DeSerializerEx(byte[] datas) {
    //    TextTableSchema schema = new TextTableSchema();
    //    byte[] bytes_c = MB.Util.Compression.Instance.UnZip(datas);
    //    string dstr = _Encoding.GetString(bytes_c);
    //    string header;
    //    using (StringReader reader = new StringReader(dstr)) {
    //        //先取Flag  第一行的标志行 为构造DataTable 
    //        string[] flag = reader.ReadLine().Split(';');
    //        if (flag.Length != 2)
    //            throw new MB.Util.APPException("反系列化时出错,不是有效的系列化文件", MB.Util.APPMessageType.SysErrInfo);

    //        string colString = flag[1].Substring(SE_COLUMNS.Length - 3, flag[1].Length - SE_COLUMNS.Length + 3);
    //        var cols = schema.StringToColumns(colString);
    //        DataTable dt = schema.CreateDataTable(cols);
    //        //先读取表头
    //        header = reader.ReadLine();
    //        string[] colFields = System.Text.RegularExpressions.Regex.Split(header, "\t");
    //        while (true) {
    //            string dataRow = reader.ReadLine();
    //            if (string.IsNullOrEmpty(dataRow)) break;

    //            DataRow newDr = dt.NewRow();

    //            for (int i = 0; i < colFields.Length; i++) {
    //                if (string.IsNullOrEmpty(colFields[i])) break;
    //                string[] vals = System.Text.RegularExpressions.Regex.Split(dataRow, "\t");
    //                if (colFields.Length != vals.Length)
    //                    throw new MB.Util.APPException(string.Format("反系列化第{0}时出错,列的个数{1}和值的个数{2}不一致", i, colFields.Length, vals.Length), MB.Util.APPMessageType.SysErrInfo);
    //                string name = colFields[i];
    //                newDr[name] = MB.Util.MyReflection.Instance.ConvertValueType(dt.Columns[name].DataType, vals[i]);
    //            }
    //            dt.Rows.Add(newDr);
    //        }
    //        return dt;
    //    }
    //}
    #endregion 单线程处理方法...
    /// <summary>
    /// Table 表头结构
    /// </summary>
    internal class TextTableSchema {
        string FORMATE = "{0}:{1}";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cols"></param>
        /// <returns></returns>
        public string ColumnsToString(List<ColumnInfo> cols) {
            List<string> lst = new List<string>();
            foreach (ColumnInfo col in cols) {
                lst.Add(string.Format(FORMATE, col.Name, col.DataType.FullName));
            }
            return string.Join(",", lst.ToArray());
        }
        public List<ColumnInfo> StringToColumns(string colsString) {
            string[] cols = colsString.Split(',');
            List<ColumnInfo> lst = new List<ColumnInfo>();
            foreach (string c in cols) {
                string[] fields = c.Split(':');
                lst.Add(new ColumnInfo(fields[0], Type.GetType(fields[1])));
            }
            return lst;
        }

        public DataTable CreateDataTable(List<ColumnInfo> cols) {

            DataTable dtNew = new DataTable();
            foreach (var col in cols) {
                dtNew.Columns.Add(col.Name, col.DataType);
            }
            return dtNew;
        }
    }
    #region columnInfo...
    /// <summary>
    /// 列的描述信息。
    /// </summary>
    internal class ColumnInfo {
        public ColumnInfo() {
        }
        public ColumnInfo(string name, Type type) {
            Name = name;
            DataType = type;
        }
        public string Name { get; set; }
        public Type DataType { get; set; }
        public string Caption { get; set; }
    }
    #endregion columnInfo...


    
}
