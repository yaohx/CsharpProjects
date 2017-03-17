//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-03-16
// Description	:	 常用数据验证处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MB.Util {
    /// <summary>
    /// 常用数据验证处理相关。
    /// </summary>
    public class DataValidated {

        #region 常量定义...
        private static readonly string MSG_STRING = "字符";
        private static readonly string MSG_INT16 = "<32767的数字";
        private static readonly string MSG_INT32 = "<2,147,483,647的数字";
        private static readonly string MSG_DATETIME = "正确的日期类型";
        private static readonly string MSG_DECIMAL = "数字类型";
        private static readonly string MSG_BOOLEAN = "0 或者1";
        private static readonly string MSG_BYTE = "Byte[]";
        private static readonly string MSG_GUID = "全局唯一标识符 (GUID)结构";
        private static readonly string MSG_DEFAULT = "正确的数据类型";
        #endregion 常量定义...

       #region Instance...
        private static Object _Obj = new object();
        private static DataValidated _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected DataValidated() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static DataValidated Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new DataValidated();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        #region public 成员...
        /// <summary>
        /// 检验输入是否为合法的数据类型。
        /// </summary>
        /// <param name="sType"></param>
        /// <param name="dataValue"></param>
        /// <returns></returns>
        public  bool ValidatedDataType(Type sType,object dataValue)
		{
			if (dataValue==null)
			{
				return true;
			}
			try
			{
				switch (sType.Name)
				{
					case "String":
						System.Convert.ToString( dataValue );
						return true;
					case "Int16" :
                        Int16 re16 = 0;
						return System.Int16.TryParse(dataValue.ToString(),out re16);
					case "Int32" :
                        int re32 = 0;
						return System.Int32.TryParse( dataValue.ToString(),out re32 );
                    case "Int64":
                        Int64 re64 = 0;
                        return System.Int64.TryParse(dataValue.ToString(), out re64);
                    case "UInt16":
                        UInt16 reU16 = 0;
                        return System.UInt16.TryParse(dataValue.ToString(), out reU16);
                    case "UInt32":
                        UInt32 reU32 = 0;
                        return System.UInt32.TryParse(dataValue.ToString(), out reU32);
                    case "UInt64":
                        UInt64 reU64 = 0;
                        return System.UInt64.TryParse(dataValue.ToString(), out reU64);
                    case "Single":
                        Single reS = Single.MinValue;
                        return System.Single.TryParse(dataValue.ToString(), out reS);    
                    case "Double":
                        Double reD = Double.MinValue;
                        return System.Double.TryParse(dataValue.ToString(), out reD);
                    case "Decimal":
                        decimal reDc = decimal.MinValue;
                        return System.Decimal.TryParse(dataValue.ToString(), out reDc);
					case "DateTime" :
                        DateTime reDt = DateTime.MinValue;
                        return System.DateTime.TryParse(dataValue.ToString(),out reDt);
					case "Boolean":
                        Boolean reB;
                        return System.Boolean.TryParse(dataValue.ToString(), out reB);  
					case "Byte[]":
						Byte[] t = (Byte[])dataValue ;
                        return true;
					case "Guid":
						System.Guid gud = new Guid(dataValue.ToString());
						return true;
					default:
						System.Convert.ToString( dataValue );
						return true;
				}
			}
			catch(Exception e)
			{
				//MB.Util.TraceEx.Write("类型检验出错!" +  e.Message);  
				return false;
			}
		}
        /// <summary>
        /// 检验输入的数据长度。
        /// </summary>
        /// <param name="limitLen"></param>
        /// <param name="dataValue"></param>
        /// <returns></returns>
        public bool ValidatedLength(int limitLen, string dataValue)
		{
			//中文算两个字节，英文算一个字节
			return limitLen >= System.Text.Encoding.Default.GetByteCount(dataValue);
		}
        /// <summary>
        /// 检查字段的输入值最适合的长度。不能长也不能短。
        /// </summary>
        /// <param name="limitLen"></param>
        /// <param name="dataValue"></param>
        /// <returns></returns>
        public bool ValidatedFitLength(int limitLen, string dataValue) {
			bool b = true ;
			if(limitLen!=-1 && limitLen !=0){
				b = limitLen == System.Text.Encoding.Default.GetByteCount(dataValue);
			}
			return b;
		}
        /// <summary>
        /// 获取类型的中文描述信息。
        /// </summary>
        /// <param name="sType"></param>
        /// <returns></returns>
        public string GetTypeValidatedErrMsg(Type sType)
		{
			switch (sType.Name)
			{
				case "String":
					return MSG_STRING;
				case "Int16" :
					return MSG_INT16;
				case "Int32" :
					return MSG_INT32;
				case "DateTime" :
					return MSG_DATETIME;
				case "Decimal" :
					return MSG_DECIMAL;
				case "Boolean":
					return MSG_BOOLEAN;
				case "Byte[]" :
					return MSG_BYTE;
				case "Guid":
					return MSG_GUID;
				default:
					return MSG_DEFAULT;
			}
        }
        #endregion public 成员...


        /// <summary>
        /// 检查指定的值在集合中是否已存在。
        /// </summary>
        /// <param name="lstEntitys"></param>
        /// <param name="logicPropertys"></param>
        /// <param name="checkValues"></param>
        /// <returns></returns>
        public bool CheckEntityDataExists(IList lstEntitys, string[] logicPropertys, string[] checkValues,out object findEntity) {
            string checkKeyString = string.Join(" ", checkValues);
            int count = logicPropertys.Length;
            foreach (object entity in lstEntitys) {
                List<string> vals = new List<string>();
                for (int i = 0; i < count; i++) {
                    bool b = MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, logicPropertys[i]);
                    if (!b)
                        throw new MB.Util.APPException(string.Format("在进行数据本地逻辑主键 检验时, 数据结构中不包含 属性 {0},请检查XML 文件中LogicKeys 配置是否正确" , logicPropertys[i]));
                    object v = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, logicPropertys[i]);
                    if (v == null)
                        vals.Add(string.Empty);
                    else
                        vals.Add(v.ToString());
                }
                string cKey = string.Join(" ", vals.ToArray());
                if (string.Compare(checkKeyString, cKey, true) == 0) {
                    findEntity = entity;
                    return true;
                }
            }
            findEntity = null;
            return false;
        }
        /// <summary>
        /// 检查指定的值在集合中是否已存在。
        /// </summary>
        /// <param name="drsData"></param>
        /// <param name="logicPropertys"></param>
        /// <param name="findDataRow"></param>
        /// <returns></returns>
        public bool CheckExistsDataRow(DataRow[] drsData, string[] logicPropertys, string[] checkValues, out DataRow findDataRow) {
            string checkKeyString = string.Join(" ", checkValues);
            int count = logicPropertys.Length;
            foreach (DataRow dr in drsData) {
                List<string> vals = new List<string>();
                for (int i = 0; i < count; i++) {
                    bool b = dr.Table.Columns.Contains(logicPropertys[i]);
                    if (!b)
                        throw new MB.Util.APPException(string.Format("在进行数据本地逻辑主键 检验时, 数据结构中不包含 属性 {0},请检查XML 文件中LogicKeys 配置是否正确", logicPropertys[i]));
                    object v = dr[logicPropertys[i]];
                    if (v == null)
                        vals.Add(string.Empty);
                    else
                        vals.Add(v.ToString());
                }
                string cKey = string.Join(" ", vals.ToArray());
                if (string.Compare(checkKeyString, cKey, true) == 0) {
                    findDataRow = dr;
                    return true;
                }
            }
            findDataRow = null;
            return false;
        }

        #region DataSet 相关处理...
        /// <summary>
        /// 根据指定的字段判断值在集合中是否已经存在。
        /// </summary>
        /// <param name="drsData"></param>
        /// <param name="logicFields"></param>
        /// <param name="checkValues"></param>
        /// <returns></returns>
        public bool CheckDataExists(DataRow[] drsData, string[] logicFields, string[] checkValues) {
            string checkKeyString = string.Join(" ", checkValues);
            int count = logicFields.Length;
            foreach (DataRow dr in drsData) {
                List<string> vals = new List<string>();
                for (int i = 0; i < count; i++) {
                    if (!dr.Table.Columns.Contains(logicFields[i]))
                        throw new MB.Util.APPException(string.Format("在进行数据本地逻辑主键 检验时, 数据结构中不包含 属性 {0},请检查XML 文件中LogicKeys 配置是否正确" ,logicFields[i]));
                    if (dr[logicFields[i]] == System.DBNull.Value)
                        vals.Add(string.Empty);
                    else
                        vals.Add(dr[logicFields[i]].ToString());
                }
                string cKey = string.Join(" ", vals.ToArray() );
                if (string.Compare(checkKeyString, cKey, true) == 0) return true;
            }
            return false;
        }
        /// <summary>
        ///  移除空行数据。
        /// </summary>
        /// <param name="dsData"></param>
        public void RemoveNULLRowData(DataSet dsData) {
            DataTable dt = dsData.Tables[0];
            if (dt == null) return;
            DataRow[] drs = dt.Select();
            foreach (DataRow dr in drs) {
                bool b = RowIsNull(dr);
                if (b)
                    dt.Rows.Remove(dr);
            }
        }

        /// <summary>
        /// 判断整行的数据是否都为空。在新增的时候，如果是空行，将忽略不进行处理。
        /// </summary>
        /// <param name="drData"></param>
        /// <returns></returns>
        public bool RowIsNull(DataRow drData) {
            object[] vals = drData.ItemArray;
            foreach (object objVal in vals) {
                if (objVal != System.DBNull.Value && objVal != null) {
                    return false;
                }
            }
            return true;
        }
        #endregion DataSet 相关处理...
    }
}
