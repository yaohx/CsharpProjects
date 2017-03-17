//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	chendc
// Create date	:	2009-01-05
// Description	:	应用程序数据转换需要的公共处理函数。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using MB.Util.Emit;

namespace MB.Util {
    /// <summary>
    /// MBConvert 应用程序数据转换需要的公共处理函数。
    /// </summary>
    public sealed class MyConvert{
        private static readonly DateTime SQL_MIN_DATE = DateTime.Parse("1756-01-01");
        private static readonly string[] CONVERT_TABLE_EXCLUDE_PROPERTY = new string[] { "ExtensionData" };
        #region Instance...
        private static Object _Obj = new object();
        private static MyConvert _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected MyConvert() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static MyConvert Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new MyConvert();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        /// <summary>
        /// 根据属性值进行匹配，转换为新的对象。
        /// 该方法主要解决服务器组件到本地组件的转换的问题。（临时解决方案，以后要修改为通用的XmlSerializer 方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="srcObject"></param>
        /// <returns></returns>
        public T ConvertToNewTypeObject<T>(object srcObject) {
            var pros = MB.Util.MyReflection.Instance.ObjectToPropertyValues(srcObject);
            T newObject = (T)MB.Util.DllFactory.Instance.CreateInstance(typeof(T));
            MB.Util.MyReflection.Instance.SetByPropertyValues(newObject, pros);
            return newObject;
        }

        #region dataSet \ dataTable \ DataView ...
        /// <summary>
        /// 把数据实体集合类转换为 客户可分析DataSet 的格式。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <param name="convertPropertysName"></param>
        /// <returns></returns>
        public DataSet ConvertEntityToDataSet<T>(IList<T> entitys, string[] convertPropertysName)
        {
            return ConvertEntityToDataSet(typeof(T), entitys, convertPropertysName);
        }
        /// <summary>
        /// 把数据实体集合类转换为 客户可分析DataSet 的格式。
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="convertPropertysName">需要转换的属性名称 (为空 将转换所有可读的属性)</param>
        /// <returns></returns>
        public DataSet ConvertEntityToDataSet(Type entityType, IEnumerable entitys, string[] convertPropertysName)
        {
            var pros = entityType.GetProperties();
            DataTable dtData = new DataTable();
            Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> entityExistsPros = new Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor>();
            foreach (var p in pros) {
                if (p.IsSpecialName || !p.CanRead || Array.IndexOf(CONVERT_TABLE_EXCLUDE_PROPERTY,p.Name)>=0) continue;

                if (convertPropertysName != null && convertPropertysName.Length > 0 && !convertPropertysName.Contains(p.Name)) continue;

                Type pType = p.PropertyType;
                if (pType.IsGenericType) {
                    pType = pType.GetGenericArguments()[0];
                }
                Type t = MB.Util.General.CreateSystemType(pType.FullName, false);

                if (t == null) throw new MB.Util.APPException(string.Format("根据类型名称 {0} 获取类型出错", p.PropertyType.FullName),
                                                                Util.APPMessageType.SysErrInfo);
                dtData.Columns.Add(p.Name, t);
                entityExistsPros[p.Name] = new Util.Emit.DynamicPropertyAccessor(entityType, p);
            }
            foreach (object entity in entitys) {
                DataRow drNew = dtData.NewRow();
                dtData.Rows.Add(drNew);
                foreach (string columnName in entityExistsPros.Keys) {
                    object val = entityExistsPros[columnName].Get(entity);// MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, columnName);
                    if (val == null || val == System.DBNull.Value) continue;
                    drNew[columnName] = MB.Util.MyConvert.Instance.ChangeType(val, dtData.Columns[columnName].DataType);
                }
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtData);
            return ds;
        }
        /// <summary>
        /// 根据DataSet 转换为实体集合.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dsSource"></param>
        /// <param name="toEntitys"></param>
        /// <param name="excludeColumns"></param>
        public static void ConvertDataSetToEntity<T>(DataSet dsSource, IList<T> toEntitys, string[] excludeColumns)
        {
            Type entityType = typeof(T);
            var pros = entityType.GetProperties();
            DataTable dt = dsSource.Tables[0];
            Dictionary<string, DynamicPropertyAccessor> entityExistsPros = new Dictionary<string, DynamicPropertyAccessor>();
            foreach (var p in pros)
            {
                if (!dt.Columns.Contains(p.Name)) continue;
                if (p.IsSpecialName || !p.CanRead) continue;

                if (excludeColumns != null && excludeColumns.Length > 0 && excludeColumns.Contains(p.Name)) continue;

                if (Array.IndexOf(CONVERT_TABLE_EXCLUDE_PROPERTY, p.Name) >= 0) continue;

                Type t = MyReflection.GetPropertyTypeWithoutNullable(p.PropertyType);// UF.BaseFrame.Util.General.CreateSystemType(p.PropertyType.FullName, true);
                if (t == null) throw new MB.Util.APPException(string.Format("根据类型名称 {0} 获取类型出错", p.PropertyType.FullName),
                                                                Util.APPMessageType.SysErrInfo);
                entityExistsPros[p.Name] = new DynamicPropertyAccessor(entityType, p);
            }
            DataRow[] drs = dsSource.Tables[0].Select();
            foreach (var dr in drs)
            {
                T newEntity = (T)DllFactory.Instance.CreateInstance(entityType);
                foreach (var da in entityExistsPros.Keys)
                {
                    entityExistsPros[da].Set(newEntity, dr[da]);
                }
                toEntitys.Add(newEntity);
            }
        }
        /// <summary>
        /// 把任意的数据类型转换成 DataTable 的格式
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="dataMember"></param>
        /// <returns></returns>
        public DataTable ToDataTable(object dataSource,string dataMember) {
            string name = dataSource.GetType().Name;
            DataTable dt = null;
            switch (name) {
                case "DataSet":
                    DataSet ds = dataSource as DataSet;
                    if (!string.IsNullOrEmpty(dataMember))
                        dt = ds.Tables[dataMember];
                    else
                        dt = ds.Tables[0];
                    break;
                case "DataTable":
                    dt = dataSource as DataTable;
                    break;
                case "DataView":
                    dt = (dataSource as DataView).Table;
                    break;
                default:
                    break;
            }
            return dt;
        }
        /// <summary>
        /// 把任意的数据类型转换成 DataTable 的格式
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="dataMember"></param>
        /// <returns></returns>
        public DataView ToDataView(object dataSource, string dataMember) {
            if (dataSource == null) return null; 

            string name = dataSource.GetType().Name;
            DataView dv = null;
            switch (name) {
                case "DataSet":
                    DataSet ds = dataSource as DataSet;
                    if (!string.IsNullOrEmpty(dataMember))
                        dv = ds.Tables[dataMember].DefaultView;
                    else
                        dv = ds.Tables[0].DefaultView;
                    break;
                case "DataTable":
                    dv = (dataSource as DataTable).DefaultView;
                    break;
                case "DataView":
                    dv = dataSource as DataView;
                    break;
                default:
                    break;
            }
            return dv;
        }
        /// <summary>
        /// 转换为网格浏览需要的视图。
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public object ToGridViewSource(object dataSource) {
            if (dataSource == null) return null;
            string name = dataSource.GetType().Name;
            switch (name) {
                case "DataSet":
                    DataSet ds = dataSource as DataSet;
                     return  ds.Tables[0].DefaultView;
                case "DataTable":
                    return (dataSource as DataTable).DefaultView;
                case "DataView":
                default:
                    return dataSource;
            }
            return dataSource;
        }
        #endregion dataSet \ dataTable \ DataView ...

        #region 常用类型转换...
        /// <summary>
        /// 返回指定类型的对象值。
        /// </summary>
        /// <param name="val"></param>
        /// <param name="convertType"></param>
        /// <returns></returns>
        public object ChangeType(object value, Type convertType) {
            return MB.Util.MyReflection.Instance.ConvertValueType(convertType, value); 
        }
        /// <summary>
        /// 得到双精度的数据
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public double ToDouble(object orgData) {
            if (orgData == null || orgData == System.DBNull.Value || string.IsNullOrEmpty(orgData.ToString().Trim()) )
                return 0;

            double re = 0;

            bool b = double.TryParse(orgData.ToString(), out re);
            if (b) 
                return re;
            else
                return 0;
        }
        /// <summary>
        /// 得到整形的数据
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public int ToInt(object orgData) {
            string val = MyMath.Instance.SetRound(orgData, 0);
            return int.Parse(val);
        }
        /// <summary>
        /// 得到整形的数据
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public Int64 ToInt64(object orgData) {
            string val = MyMath.Instance.SetRound(orgData, 0);
            return Int64.Parse(val);
        }
        /// <summary>
        ///  把数据转换成 Decimal
        /// </summary>
        /// <param name="pData"></param>
        /// <param name="pDesLength"></param>
        /// <returns></returns>
        public decimal ToDecimal(object orgData, int desLength) {
            string val = MyMath.Instance.SetRound(orgData, desLength);
            return decimal.Parse(val);
        }
        /// <summary>
        /// 把数据转换成 Decimal ,并转换为控制的指定长度
        /// </summary>
        /// <param name="pData"></param>
        /// <param name="pDesLength"></param>
        /// <returns></returns>
        public decimal ToDecimal(object orgData, int desLength, bool desIsFix) {
            string val = MyMath.Instance.SetRound(orgData, desLength);
            if (desIsFix) {
                int index = val.LastIndexOf('.');
                if (index < 0) {
                    val += ".";
                    for (int i = 0; i < desLength; i++) {
                        val += "0";
                    }
                }
                else {
                    string des = val.Substring(index + 1, val.Length - index - 1);
                    for (int i = 0; i < desLength - des.Length; i++) {
                        val += "0";
                    }
                }
            }
            return decimal.Parse(val);
        }

        /// <summary>
        /// 得到float的数据
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public float ToFloat(object orgData, int desLength) {
            string val = MyMath.Instance.SetRound(orgData, desLength);
            return float.Parse(val);
        }
        /// <summary>
        /// 转换为bool 的数据
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public bool ToBool(object orgData) {
            if (orgData == null || orgData == System.DBNull.Value || string.IsNullOrEmpty(orgData.ToString())) {
                return false;
            }
            string val = orgData.ToString().Trim();
            if (val == "1" || string.Compare(val, "TRUE", true) == 0) {
                return true;
            }
            return false;
        }
        #endregion 常用类型转换...

        #region 2进制、8进制、10进制、16进制 之间的数据转换...
        /// <summary>
        /// 2进制、8进制、10进制、16进制 之间的数据转换。
        /// </summary>
        /// <param name="valStr">需要转换的字符窜。</param>
        /// <param name="fromBase">它必须是2,8,10或者16</param>
        /// <param name="toBase">它必须是2,8,10或者16</param>
        /// <returns></returns>
        public string BODHConvert(string valStr, int fromBase, int toBase) {
            int intValue = Convert.ToInt32(valStr, fromBase);

            return Convert.ToString(intValue, toBase);
        }
        /// <summary>
        /// 2进制 转换为10进制。
        /// </summary>
        /// <param name="binStr"></param>
        /// <returns></returns>
        public int BinToDec(string binStr) {
            return int.Parse(BODHConvert(binStr, 2, 10));
        }
        /// <summary>
        /// 16进制 转换为10进制。
        /// </summary>
        /// <param name="binStr"></param>
        /// <returns></returns>
        public int HexToDec(string hexStr) {
            return int.Parse(BODHConvert(hexStr, 16, 10));
        }
        /// <summary>
        /// 8进制 转换为10进制。
        /// </summary>
        /// <param name="binStr"></param>
        /// <returns></returns>
        public int OcxToDec(string ocxStr) {
            return int.Parse(BODHConvert(ocxStr, 8, 10));
        }
        #endregion 2进制、8进制、10进制、16进制 之间的数据转换...

        #region Image 转换处理相关...
        /// <summary>
        /// 把图像文件转换成Base64String 类型
        /// </summary>
        /// <param name="pImage"></param>
        /// <returns></returns>
        public string ImageToBase64String(System.Drawing.Image imageData) {
            byte[] bts = ImageToByte(imageData);
            return Convert.ToBase64String(bts);
        }
        /// <summary>
        /// 把图像文件转换成Byte[] 类型
        /// </summary>
        /// <param name="pImage"></param>
        /// <returns></returns>
        public Byte[] ImageToByte(System.Drawing.Image imageData) {
            if (imageData == null) {
                return null;
            }
            FileStream st = null;
            Byte[] buffer = null;
            try {
                string cPath = Environment.CurrentDirectory + "TempJpg.Temp";
                imageData.Save(cPath);
                st = new FileStream(cPath, FileMode.Open, FileAccess.Read);

                BinaryReader mbr = new BinaryReader(st);
                buffer = new Byte[st.Length];
                mbr.Read(buffer, 0, int.Parse(st.Length.ToString()));

            }
            catch {
               MB.Util.TraceEx.Write("图象转换为Byte[] 类型出错。");
                return null;
            }
            finally {
                if (st != null) {
                    st.Close();
                }
            }
            return buffer;
        }
        /// <summary>
        ///  把硬盘文件转换成Byte[] 类型格式
        /// </summary>
        /// <param name="pFileFullPath"> 报表文件完整的路径</param>
        /// <returns></returns>
        public Byte[] FileToByte(string fileFullPath) {
            bool b = System.IO.File.Exists(fileFullPath);
            if (!b) {
                return null;
            }
            FileStream st = null;
            Byte[] buffer = null;
            try {
                st = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read);
                BinaryReader mbr = new BinaryReader(st);
                buffer = new Byte[st.Length];
                mbr.Read(buffer, 0, int.Parse(st.Length.ToString()));

            }
            catch {
               MB.Util.TraceEx.Write("得到图象文件" + fileFullPath + "转换为Byte[] 类型出错。");
                return null;
            }
            finally {
                if (st != null) {
                    st.Close();
                }
            }
            return buffer;
        }
        /// <summary>
        /// 通过string 转换成Image文件格式的形式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public System.Drawing.Image Base64StringToImage(string data) {
            byte[] bts = Convert.FromBase64String(data);
            //byte[] bts = System.Text.Encoding.UTF8.GetBytes(data); 
            return ByteToImage(bts);
        }
        /// <summary>
        /// 通过Byte[] 字节流转换成Image文件格式的形式
        /// </summary>
        /// <param name="pByte"></param>
        /// <returns></returns>
        public System.Drawing.Image ByteToImage(Byte[] pByte) {
            if (pByte != null && pByte.Length > 0) {
                try {
                    MemoryStream stream = new MemoryStream(pByte, true);
                    stream.Write(pByte, 0, pByte.Length);
                    System.Drawing.Image img = Image.FromStream(stream);
                    return img;
                }
                catch (Exception e) {
                    throw new MB.Util.APPException("从数据库中得到文件信息有误" + e.Message);
                }
            }
            else {
                return null;
            }
        }
        /// <summary>
        /// 把Byte数组转换成文件格式并存储到本地硬盘中
        /// </summary>
        /// <param name="pFileFullPath">文件的完整路径</param>
        /// <param name="pByte">文件的字节流</param>
        /// <returns></returns>
        public bool ByteToFile(string pFileFullPath, Byte[] pByte) {
            if (pByte != null && pByte.Length > 0) {
                FileStream st = new FileStream(pFileFullPath, FileMode.Create, FileAccess.Write);
                st.Write(pByte, 0, pByte.Length);
                try {
                    st.Close();
                }
                catch (Exception e) {
                    MB.Util.TraceEx.Write("Byte[] 转换为文件出错。" + e.Message);
                }
                return true;
            }
            return false;
        }
        #endregion Image 转换处理相关...

        #region 颜色转换...
        /// <summary>
        /// 根据颜色的名称或者Arg 转换为指定的颜色。
        /// </summary>
        /// <param name="colorNameOrArg">对于Arg来说，不同颜色之间用逗号分开。例如：202,203,102</param>
        /// <returns></returns>
        public Color ToColor(string colorNameOrArg) {
            try {
                if (colorNameOrArg.IndexOf(',') >= 0) {
                    string[] vals = colorNameOrArg.Split(',');
                    if (vals.Length != 3)
                        return Color.Empty;

                    return Color.FromArgb(ToInt(vals[0]), ToInt(vals[1]), ToInt(vals[2]));
                }
                else {
                    return Color.FromName(colorNameOrArg);
                }
                return Color.Empty;
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(string.Format("颜色转换出错,请检查{0} 是否为有效的颜色值:" ,colorNameOrArg), APPMessageType.SysErrInfo); 
            }
        }
        #endregion 颜色转换...


        /// <summary>
        /// 转换为SQL SERVER 允许的 日期格式。
        /// </summary>
        /// <param name="dTime"></param>
        /// <returns></returns>
        public DateTime ToSqlServerDateTime(DateTime dTime) {
            if (dTime < SQL_MIN_DATE)
                return SQL_MIN_DATE;
            else
                return dTime;
        }


        #region 时间戳和日期之间的转换
        /// <summary>
        /// 时间戳转为C#日期格式
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public DateTime ConvertTsToDateTime(string timeStamp) {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }


        /// <summary>
        /// c#日期转换成时间戳
        /// </summary>
        /// <returns></returns>
        public string ConvertDtToTimpStamp(DateTime dt) {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toDt = dt.Subtract(dtStart);
            string timeStamp = toDt.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
            return timeStamp;
        }

        #endregion

    }
}
