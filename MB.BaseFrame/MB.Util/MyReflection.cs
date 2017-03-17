//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2008-01-05
// Description	:	MyReflection 通过反射调用对象的属性和方法。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;

using MB.Util.XmlConfig;
namespace MB.Util
{
    /// <summary>
    /// MyReflection 通过反射调用对象的属性和方法。
    /// </summary>
    public class MyReflection
    {
        private static readonly string T1 = "True";
        private static readonly string T2 = "true";
        private static readonly string T3 = "TRUE";

        #region Instance...
        private static Object _Obj = new object();
        private static MyReflection _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected MyReflection() {

        }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static MyReflection Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new MyReflection();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        #region 实体对象模型处理相关...
        /// <summary>
        ///  通过DataRow 创建一个实体对象。
        /// </summary>
        /// <param name="drData"></param>
        /// <param name="targetObjType"></param>
        /// <returns></returns>
        public T CreateModelObject<T>(DataRow drData) {
            bool hashNullDeaultCst = false;
            System.Type targetObjType = typeof(T);
            System.Reflection.ConstructorInfo[] cons = targetObjType.GetConstructors();
            foreach (System.Reflection.ConstructorInfo cInfo in cons) {
                if (cInfo.GetParameters().Length == 0) {
                    hashNullDeaultCst = true;
                    break;
                }
            }

            if (!hashNullDeaultCst) {
                throw new MB.Util.APPException("通过DataRow 自动创建必须要有一个默认空的构造函数，请检查该类型是否已经定义。");
            }

            T targetObj = (T)System.Activator.CreateInstance(targetObjType);

            FillModelObject(targetObj, drData);
            return targetObj;
        }
        /// <summary>
        /// 把一个实体对象的值赋给另外一个对象。
        /// 这里只是 Memberwise 值设置。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="orgEntity"></param>
        /// <param name="destEntity"></param>
        /// <param name="excludePros"></param>
        /// <returns></returns>
        public T FillModelObjectNoCreate<T>(T orgEntity, T destEntity, params string[] excludePros) {
            if (orgEntity == null || destEntity == null) return default(T);

            Type targetObjType = typeof(T);
            var proList = targetObjType.GetProperties();

            foreach (var p in proList) {
                if (!p.CanWrite || !p.CanRead || p.IsSpecialName) continue;

                if (excludePros != null && Array.IndexOf<string>(excludePros, p.Name) >= 0) continue;

                p.SetValue(destEntity, p.GetValue(orgEntity,null),null);
            }
            return destEntity;
        }
        /// <summary>
        /// 把一个实体对象的值赋给另外一个对象。
        /// 这里只是 Memberwise 值设置。
        /// </summary>
        /// <param name="orgEntity"></param>
        /// <param name="destEntity"></param>
        /// <param name="excludePros"></param>
        /// <returns></returns>
        public object FillModelObjectNoCreate(object orgEntity, object destEntity, params string[] excludePros) {
            if (orgEntity == null || destEntity == null) return null;

            Type targetObjType = destEntity.GetType();
            var proList = targetObjType.GetProperties();

            foreach (var p in proList) {
                if (!p.CanWrite || !p.CanRead || p.IsSpecialName) continue;

                if (excludePros != null && Array.IndexOf<string>(excludePros, p.Name) >= 0) continue;

                p.SetValue(destEntity, p.GetValue(orgEntity, null), null);
            }
            return destEntity;
        }
        /// <summary>
        /// 把行值转换为实体对象值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityModel"></param>
        /// <param name="drData"></param>
        /// <returns></returns>
        public T FillModelObject<T>(T entityModel, DataRow drData) {
            Type targetObjType = entityModel.GetType();
            var proList = targetObjType.GetProperties();

            DataTable dtData = drData.Table;
            foreach (System.Reflection.PropertyInfo proInfo in proList) {
                if (!proInfo.CanWrite) continue;

                if (!dtData.Columns.Contains(proInfo.Name)) continue;

                if (drData[proInfo.Name] != System.DBNull.Value) {
                    object val = drData[proInfo.Name];
                     
                    proInfo.SetValue(entityModel, ConvertValueType(proInfo.PropertyType, val), null);
                }
            }
            return entityModel;
        }
        /// <summary>
        /// 把一个实体的值转换为新的实体类型。（值复制并创建一个新的实体）
        /// 2010-08-16 edit by chendc
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T FillModelObject<T>(object entity) {
            Type targetObjType = typeof(T);
            T targetObj = (T)DllFactory.Instance.CreateInstance(targetObjType);
            var proList = targetObjType.GetProperties();
            foreach (System.Reflection.PropertyInfo proInfo in proList) {
                if (!proInfo.CanWrite) continue;
                if (!CheckObjectExistsProperty(entity, proInfo.Name)) continue;
                object val = InvokePropertyForGet(entity, proInfo.Name);

                proInfo.SetValue(targetObj, ConvertValueType(proInfo.PropertyType, val), null);

            }
            return targetObj;
        }

        #endregion 实体对象模型处理相关...

        /// <summary>
        /// 通过名称来调用方法，不考虑方法的重载情况。
        /// 该方法主要通过 方法的名称来查找入口点，不区分参数，
        /// 
        /// </summary>
        /// <param name="rObj"></param>
        /// <param name="methodName"></param>
        /// <param name="paramVals"></param>
        /// <returns></returns>
        public object InvokeMethodByName(object rObj, string methodName, params object[] paramVals) {
            if (rObj == null) {
                throw new MB.Util.APPException(string.Format("反射调用方法 {0}({1}) 的方法载体为NULL", methodName, getParamsValuesMsg(paramVals)), APPMessageType.SysErrInfo);
            }
            Type myType = rObj.GetType();
            MethodInfo modInfo = myType.GetMethod(methodName);
            if (modInfo == null) {
                throw new MB.Util.APPException(string.Format("对象{0}中不包含该方法{1}", rObj.GetType().FullName, methodName), APPMessageType.SysErrInfo);
            }
            try {

                return modInfo.Invoke(rObj, paramVals);
            }
            catch (Exception e) {
                throw e;
            }
        }
        /// <summary>
        /// 通过反射调用指定的方法。
        /// </summary>
        /// <param name="rObj"></param>
        /// <param name="methodName"></param>
        /// <param name="paramVals"></param>
        public object InvokeMethod(object rObj, string methodName, params object[] paramVals) {
            if (rObj == null) {
                throw new MB.Util.APPException(string.Format("反射调用方法 {0}({1}) 的方法载体为NULL", methodName, getParamsValuesMsg(paramVals)), APPMessageType.SysErrInfo);
            }
            Type myType = rObj.GetType();
            System.Type[] types = getTypesFromParams(paramVals);

            MethodInfo modInfo = null;
            if (types != null && types.Length > 0)
                modInfo = myType.GetMethod(methodName, types);
            else
                modInfo = myType.GetMethod(methodName);

            int parCount = 0;
            if (types != null)
                parCount = types.Length;

            if (modInfo == null) {
                throw new MB.Util.APPException(string.Format("对象{0}中不包含该方法{1}（{2} 个参数）", rObj.GetType().FullName, methodName, parCount), APPMessageType.SysErrInfo);
            }
            try {
                return modInfo.Invoke(rObj, paramVals);
            }
            catch (Exception e) {
                throw e;
            }
        }
        #region Property 相关...
        /// <summary>
        /// 检查指定的类型是否包含指定的属性。
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="proName"></param>
        /// <returns></returns>
        public bool CheckTypeExistsProperty(Type dataType, string proName) {
            Type myType = dataType;
            PropertyInfo proInfo = myType.GetProperty(proName);
            return proInfo != null;
        }
        /// <summary>
        /// 判断对象中是否存在指定的属性。
        /// </summary>
        /// <param name="rObj"></param>
        /// <param name="proName"></param>
        /// <returns></returns>
        public bool CheckObjectExistsProperty(object rObj, string proName) {
            if (rObj == null) {
                throw new MB.Util.APPException(string.Format("反射调用方法 {0} 的方法载体为NULL", proName), MB.Util.APPMessageType.SysErrInfo);
            }

            Type myType = rObj.GetType();
            PropertyInfo proInfo = myType.GetProperty(proName);
            return proInfo != null;
        }
        /// <summary>
        /// 检查对象中对应的属性值是否为空。
        /// </summary>
        /// <param name="rObj"></param>
        /// <param name="proName"></param>
        /// <returns></returns>
        public bool CheckPropertyValueIsNull(object rObj, string proName) {
            object val = InvokePropertyForGet(rObj, proName);
            return val == null || val.ToString().Length == 0;
        }
        /// <summary>
        /// 通过反射设置属性。
        /// </summary>
        /// <param name="rObj"></param>
        /// <param name="proName"></param>
        /// <param name="val"></param>
        public void InvokePropertyForSet(object rObj, string proName, object val) {
            if (rObj == null) {
                MB.Util.TraceEx.Write(string.Format("反射调用设置属性 {0} 的值{1} 的载体为NULL", proName, val != null ? val.ToString() : ""), MB.Util.APPMessageType.SysErrInfo);
                return;
            }
            Type myType = rObj.GetType();
            PropertyInfo proInfo = myType.GetProperty(proName);

            if (proInfo == null) {
                MB.Util.TraceEx.Write(string.Format("对象 {0} 中不包含该属性 {1}", rObj.GetType().FullName, proName), MB.Util.APPMessageType.SysErrInfo);
                return;
            }
            InvokePropertyForSet(rObj, proInfo, val);
        }
        /// <summary>
        /// 通过反射设置属性。
        /// </summary>
        /// <param name="rObj"></param>
        /// <param name="proInfo"></param>
        /// <param name="val"></param>
        public void InvokePropertyForSet(object rObj, PropertyInfo proInfo, object val) {
            if (rObj == null) {
                MB.Util.TraceEx.Write(string.Format("反射调用设置属性 {0} 的值{1} 的载体为NULL", proInfo.Name, val != null ? val.ToString() : ""), MB.Util.APPMessageType.SysErrInfo);
                return;
            }

            try {
                object proVal = ConvertValueType(proInfo.PropertyType, val);
                proInfo.SetValue(rObj, proVal, null);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// 通过反射获取属性的值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rObj"></param>
        /// <param name="proName"></param>
        /// <returns></returns>
        public T InvokePropertyForGet<T>(object rObj, string proName) {
            if (rObj == null) {
                MB.Util.TraceEx.Write(string.Format("反射获取对象属性{0}的值的载体为NULL", proName), MB.Util.APPMessageType.SysErrInfo);
                //需要修改为直接抛出异常
                return default(T);
            }
            Type myType = rObj.GetType();
            PropertyInfo proInfo = myType.GetProperty(proName);
            if (proInfo == null) {
                MB.Util.TraceEx.Write(string.Format("对象 {0} 中不包含该属性 {1}", rObj.GetType().FullName, proName), MB.Util.APPMessageType.SysErrInfo);
                //需要修改为直接抛出异常
                return default(T);
            }
            return InvokePropertyForGet<T>(rObj, proInfo);
        }
        /// <summary>
        ///  通过反射获取属性的值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rObj"></param>
        /// <param name="proInfo"></param>
        /// <returns></returns>
        public T InvokePropertyForGet<T>(object rObj, PropertyInfo proInfo) {
            object val = InvokePropertyForGet(rObj, proInfo);
            if (val == null) return default(T);
            object cV = Convert.ChangeType(val, typeof(T));
            return (T)cV;
        }
        /// <summary>
        /// 通过反射获取属性的值。
        /// </summary>
        /// <param name="rObj"></param>
        /// <param name="proName"></param>
        /// <returns></returns>
        public object InvokePropertyForGet(object rObj, string proName) {
            if (rObj == null) {
                MB.Util.TraceEx.Write(string.Format("反射获取对象属性{0}的值的载体为NULL", proName), MB.Util.APPMessageType.SysErrInfo);
                //需要修改为直接抛出异常
                return null;
            }
            Type myType = rObj.GetType();
            PropertyInfo proInfo = myType.GetProperty(proName);
            if (proInfo == null) {
                MB.Util.TraceEx.Write(string.Format("对象 {0} 中不包含该属性 {1}", rObj.GetType().FullName, proName), MB.Util.APPMessageType.SysErrInfo);
                //需要修改为直接抛出异常
                return null;
            }
            return InvokePropertyForGet(rObj, proInfo);
        }
        /// <summary>
        /// 通过反射获取属性的值。
        /// </summary>
        /// <param name="rObj"></param>
        /// <param name="proName"></param>
        /// <returns></returns>
        public object InvokePropertyForGet(object rObj, PropertyInfo proInfo) {

            try {
                return proInfo.GetValue(rObj, null);
            }
            catch (Exception e) {
                throw e;
            }
        }
        /// <summary>
        /// 获取对象的所有属性值。（特殊值除外）
        /// 所有的值都必须能写 而且能读
        /// </summary>
        /// <param name="rObj"></param>
        /// <returns></returns>
        public Dictionary<string, object> ObjectToPropertyValues(object rObj) {
            if (rObj == null) {
                MB.Util.TraceEx.Write("反射调用方法的方法载体为NULL", MB.Util.APPMessageType.SysErrInfo);
                //需要修改为直接抛出异常
                return null;
            }
            Dictionary<string, object> nameValues = new Dictionary<string, object>();
            Type myType = rObj.GetType();
            PropertyInfo[] proInfos = myType.GetProperties();
            foreach (PropertyInfo info in proInfos) {
                if (info.IsSpecialName || !info.CanRead || !info.CanWrite) continue;
                nameValues.Add(info.Name, info.GetValue(rObj, null));
            }
            return nameValues;
        }
        /// <summary>
        /// 反射设置对象值的特殊应用。
        /// 特殊说明：不是通用的行为。
        /// 批量设置对象的属性值。
        /// 不管是值类型还是引用类型都会根据新对象的属性类型来创建复制或者创建一个新值。
        /// </summary>
        /// <param name="rObj"></param>
        /// <param name="proValues"></param>
        public void SetByPropertyValues(object rObj, Dictionary<string, object> proValues) {
            if (rObj == null) {
                MB.Util.TraceEx.Write("反射调用方法的方法载体为NULL", MB.Util.APPMessageType.SysErrInfo);
                //需要修改为直接抛出异常
                return;
            }

            Type myType = rObj.GetType();
            PropertyInfo[] proInfos = myType.GetProperties();
            foreach (PropertyInfo info in proInfos) {
                if (info.IsSpecialName || !info.CanRead || !info.CanWrite) continue;
                if (!proValues.ContainsKey(info.Name)) continue;
                object val = info.GetValue(rObj, null);

                if (info.PropertyType.IsGenericType && info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) {

                    info.SetValue(rObj, proValues[info.Name], null);
                }
                else if (info.PropertyType.IsValueType)
                {
                    if (info.Equals(proValues[info.Name])) continue;
                    info.SetValue(rObj, Convert.ChangeType(proValues[info.Name], info.PropertyType), null);
                }
                else if (info.PropertyType.Equals(typeof(string)))
                {
                    //比较值是否相等
                    if (val == null && proValues[info.Name] == null) continue;
                    else
                    {
                        if (val != null && proValues[info.Name] != null)
                        {
                            if (string.Compare(val.ToString(), proValues[info.Name].ToString()) == 0) continue;
                        }
                        info.SetValue(rObj, Convert.ChangeType(proValues[info.Name], info.PropertyType), null);
                    }
                }
                else if (info.PropertyType.Name.Equals("Byte[]"))
                {
                    info.SetValue(rObj, proValues[info.Name], null);
                }
                else if (info.PropertyType.GetInterface("IList", true) != null)
                {
                    //throw new Exception();
                    IList proVals = proValues[info.Name] as IList;

                    if (proVals == null) continue;

                    IList newVals = DllFactory.Instance.CreateInstance(info.PropertyType) as IList;
                    PropertyXmlConfigAttribute att = Attribute.GetCustomAttribute(info, typeof(PropertyXmlConfigAttribute)) as PropertyXmlConfigAttribute;
                    if (att == null || att.ReferenceModelType == null) continue;

                    foreach (object item in proVals)
                    {
                        object childObj = DllFactory.Instance.CreateInstance(att.ReferenceModelType);
                        var childPros = ObjectToPropertyValues(item);
                        SetByPropertyValues(childObj, childPros);
                        newVals.Add(childObj);
                    }
                    info.SetValue(rObj, newVals, null);
                }
                else
                {
                    if (info.PropertyType.GetConstructor(new System.Type[] { }) == null) continue;

                    object proVal = DllFactory.Instance.CreateInstance(info.PropertyType);
                    var childPros = ObjectToPropertyValues(proValues[info.Name]);
                    SetByPropertyValues(proVal, childPros);

                    info.SetValue(rObj, proVal, null);
                }

            }
        }

        /// <summary>
        ///  创建当前 Object 对象 的浅表副本（只复制属性的值）。
        /// </summary>
        /// <param name="rObj"></param>
        /// <returns></returns>
        public object PropertyMemberwiseClone(object rObj) {
            if (rObj == null) return null;
            Type t = rObj.GetType();
            object newObject = t.Assembly.CreateInstance(t.FullName);
            Dictionary<string, object> vals = ObjectToPropertyValues(rObj);
            SetByPropertyValues(newObject, vals);
            return newObject;
        }
        #region 强制将值转换为指定类型
        /// <summary>
        /// 强制将值转换为指定类型
        /// </summary>
        /// <param name="propertyType">目标类型</param>
        /// <param name="valueType">值的类型</param>
        /// <param name="value">值</param>
        public object ConvertValueType(Type convertType, object value) {
            //如果值的类型与目标类型相同则直接返回,否则进行转换
            Type valueType = null;
            if (value != null && value != System.DBNull.Value)
                valueType = value.GetType();

            if (valueType != null && convertType.Equals(valueType)) {
                return value;
            }
            else {
                if (convertType.IsGenericType) {
                    if (convertType.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                        if (value == null || value == System.DBNull.Value)
                            return null;
                        else if (valueType.Equals(typeof(string)) && (string)value == string.Empty)
                            return null;
                    }
                    convertType = GetPropertyType(convertType);
                }

                if (valueType != null && convertType.IsEnum && (valueType.Equals(typeof(string)) || valueType.IsEnum))
                    return Enum.Parse(convertType, value.ToString());

                if (valueType != null && convertType.IsPrimitive && valueType.Equals(typeof(string)) && string.IsNullOrEmpty((string)value))
                    value = 0;

                if (value == null || value == System.DBNull.Value) {
                    if (convertType.IsValueType)
                        return getValueTypeDefaultValue(convertType);
                    else
                        return null;
                }
                //默认情况下添加对Bool 类型的特殊处理
                if (convertType.Equals(typeof(bool))) {
                    string sVal = value.ToString();  //从性能和兼容性上考虑 这里不使用string.Compare(sVal,"True",true)==0,在一千万次计算中要快N倍
                    return sVal == "1" || sVal.Equals(T1) || sVal.Equals(T2) || sVal.Equals(T3);
                }
                try {
                    return Convert.ChangeType(value, GetPropertyType(convertType));
                }
                catch (Exception ex) {
                    TypeConverter cnv = TypeDescriptor.GetConverter(GetPropertyType(convertType));
                    if (cnv != null && cnv.CanConvertFrom(value.GetType()))
                        return cnv.ConvertFrom(value);
                    else
                        throw ex;
                }
            }
        }
        #endregion


        /// <summary>
        /// 获取类型,如果类型为Nullable(of T)，则返回Nullable(of T)的基础类型
        /// </summary>
        /// <param name="propertyType">需要转换的类型</param>
        public static Type GetPropertyTypeWithoutNullable(Type propertyType)
        {
            Type type = propertyType;
            if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>)))
                return Nullable.GetUnderlyingType(type);
            return type;
        }

        #region 获取类型,如果类型为Nullable<>，则返回Nullable<>的基础类型

        /// <summary>
        /// 获取某个对象的属性类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="proName"></param>
        /// <returns></returns>
        public Type GetPropertyType(object entity, string proName)
        {
            if (entity == null)
                throw new MB.Util.APPException(string.Format("反射调用方法 {0} 的方法载体为NULL", proName), MB.Util.APPMessageType.SysErrInfo);

            PropertyInfo proInfo = entity.GetType().GetProperty(proName);
            return proInfo.PropertyType;
        }

        /// <summary>
        /// 获取类型,如果类型为Nullable(of T)，则返回Nullable(of T)的基础类型
        /// </summary>
        /// <param name="propertyType">需要转换的类型</param>
        public Type GetPropertyType(Type propertyType) {
            Type type = propertyType;
            if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>)))
                return Nullable.GetUnderlyingType(type);
            return type;
        }
        #endregion
        #endregion Property 相关...

        #region 内部函数处理...
        //设置Boolean 值。
        private void setBooleanPropertyValue(object rObj, PropertyInfo proInfo, object val) {
            bool bVal = false;
            if (val != null && val != System.DBNull.Value) {
                bVal = string.Compare(val.ToString(), "1") == 0 || string.Compare(val.ToString(), "TRUE", true) == 0;
            }
            proInfo.SetValue(rObj, bVal, null);
        }

        //获取值类型默认的值   (自定义的结构目前先不考虑)
        private object getValueTypeDefaultValue(Type dataType) {
            if (dataType.Equals(typeof(DateTime)))
                return default(DateTime);
            else if (dataType.Equals(typeof(bool)))
                return default(bool);
            else if (dataType.Equals(typeof(decimal)))
                return default(decimal);
            else if (dataType.Equals(typeof(double)))
                return default(double);
            else if (dataType.Equals(typeof(int)))
                return default(int);
            else if (dataType.Equals(typeof(Int64)))
                return default(Int64);
            else if (dataType.Equals(typeof(uint)))
                return default(uint);
            else if (dataType.Equals(typeof(Int16)))
                return default(Int16);
            else
                return null;
        }
        //从参数中获取参数的类型
        private System.Type[] getTypesFromParams(params object[] paramsVals) {
            if (paramsVals == null || paramsVals.Length == 0)
                return new System.Type[0];
            System.Type[] types = new Type[paramsVals.Length];
            for (int i = 0; i < paramsVals.Length; i++) {
                if (paramsVals[i] == null)
                    return new Type[0] { }; //如果存在空值的参数，那么就不能根据参数来查找方法。

                types[i] = paramsVals[i].GetType();
            }
            return types;
        }
        //获取参数转换后的字符窜
        private string getParamsValuesMsg(object[] paramsVals) {
            if (paramsVals == null || paramsVals.Length == 0)
                return string.Empty;

            string msg = string.Empty;
            foreach (object p in paramsVals) {
                msg += p != null ? p.ToString() : "";
                msg += ";";
            }
            return msg;
        }
        #endregion 内部函数处理...
    }
    /// <summary>
    /// 类字段类型。
    /// </summary>
    [Flags]
    public enum ClassFieldType : int
    {
        IsPrivate = 0x0001,
        IsPublic = 0x0002,
        IsStatic = 0x0004,
        IsSpecialName = 0x0008,
        IsInitOnly = 0x0010
    }
    /// <summary>
    /// 成员类型。
    /// </summary>
    [Flags]
    public enum PropertyType : int
    {
        CanWrite = 0x0001,
        CanRead = 0x0002,
        IsSpecialName = 0x0004,
    }
}
