//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-18
// Description	:	CustomAttributeLib 类客户化属性操作类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Reflection;
using System.ComponentModel;

namespace MB.Util {
    /// <summary>
    /// CustomAttributeLib 类客户化属性操作类。
    /// </summary>
    public class MyCustomAttributeLib : System.ContextBoundObject {

        #region Instance...
        private static object _Object = new object();
        private static MyCustomAttributeLib _Instance;

        protected MyCustomAttributeLib() { }

        /// <summary>
        /// Instance
        /// </summary>
        public static MyCustomAttributeLib Instance {
            get {
                if (_Instance == null) {
                    lock (_Object) {
                        if (_Instance == null)
                            _Instance = new MyCustomAttributeLib();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...
 

        #region Public static function...
        /// <summary>
        /// 得到枚举类型的描述
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public string GetEnumValueDesc(object enumValue, Type enumType) {
            if (!enumType.IsEnum)
                throw new MB.Util.APPException("传入的不是枚举类型");

            string name = Enum.GetName(enumType, enumValue);
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            FieldInfo fieldInfo = enumType.GetField(name);
            return GetFieldDesc(fieldInfo, false);

        }

        #region 获取描述相关...
        /// <summary>
        /// 根据fieldInfo 获取它对应的描述。
        /// </summary>
        /// <param name="field"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public string GetFieldDesc(FieldInfo field, bool inherit) {
            object[] desc = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), inherit);
            if (desc == null || desc.Length == 0)
                return string.Empty;
            System.ComponentModel.DescriptionAttribute a = desc[0] as System.ComponentModel.DescriptionAttribute;
            if (a != null) {
                return a.Description;
            }
            return string.Empty;
        }
        /// <summary>
        /// 根据对象的类型和类字段的名称获取它对应的描述。
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="fieldName"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public string GetFieldDesc(Type objType, string fieldName, bool inherit) {
            FieldInfo[] infos = objType.GetFields();
            ArrayList aList = new ArrayList();
            foreach (FieldInfo info in infos) {
                if (string.Compare(info.Name, fieldName, true) == 0) {
                    object[] desc = info.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), inherit);
                    if (desc == null || desc.Length == 0)
                        continue;
                    System.ComponentModel.DescriptionAttribute a = desc[0] as System.ComponentModel.DescriptionAttribute;
                    if (a != null) {
                        return a.Description;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 根据对象的type 获取它的所有field 的描述
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="fTypes"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public string[] GetFieldsDesc(Type objType, ClassFieldType fTypes, bool inherit) {
            FieldInfo[] infos = objType.GetFields();
            ArrayList aList = new ArrayList();
            foreach (FieldInfo info in infos) {
                
                if ((fTypes & ClassFieldType.IsPrivate) == ClassFieldType.IsPrivate)
                    if (!info.IsPrivate)
                        continue;
                if ((fTypes & ClassFieldType.IsPublic) == ClassFieldType.IsPublic)
                    if (!info.IsPublic)
                        continue;
                if ((fTypes & ClassFieldType.IsStatic) == ClassFieldType.IsStatic)
                    if (!info.IsStatic)
                        continue;
                if ((fTypes & ClassFieldType.IsSpecialName) == ClassFieldType.IsSpecialName)
                    if (!info.IsSpecialName)
                        continue;
                if ((fTypes & ClassFieldType.IsInitOnly) == ClassFieldType.IsInitOnly)
                    if (!info.IsInitOnly)
                        continue;

                object[] desc = info.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), inherit);
                if (desc == null || desc.Length == 0)
                    continue;
                System.ComponentModel.DescriptionAttribute a = desc[0] as System.ComponentModel.DescriptionAttribute;
                if (a != null) {
                    aList.Add(a.Description);
                }
            }
            string[] des = new string[aList.Count];
            for (int i = 0; i < aList.Count; i++) {
                des[i] = aList[i].ToString();
            }
            return des;
        }
        /// <summary>
        /// 获取指定PropertyInfo的描述。
        /// </summary>
        /// <param name="property"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public string GetPropertyDesc(PropertyInfo property, bool inherit) {
            object[] desc = property.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), inherit);
            if (desc == null || desc.Length == 0)
                return string.Empty;
            System.ComponentModel.DescriptionAttribute a = desc[0] as System.ComponentModel.DescriptionAttribute;
            if (a != null) {
                return a.Description;
            }
            return string.Empty;
        }
        /// <summary>
        /// 根据对象的类型和类属性的名称获取它对应的描述。
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="propertyName"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public string GetPropertyDesc(Type objType, string propertyName, bool inherit) {
            PropertyInfo[] infos = objType.GetProperties();
            ArrayList aList = new ArrayList();
            foreach (PropertyInfo info in infos) {
                if (string.Compare(info.Name, propertyName, true) == 0) {
                    object[] desc = info.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), inherit);
                    if (desc == null || desc.Length == 0)
                        continue;
                    System.ComponentModel.DescriptionAttribute a = desc[0] as System.ComponentModel.DescriptionAttribute;
                    if (a != null) {
                        return a.Description;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 根据对的象type 获取该对象的所有 propertys 描述。
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="fTypes"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public string[] GetPropertysDesc(Type objType, PropertyType fTypes, bool inherit) {
            PropertyInfo[] infos = objType.GetProperties();
            ArrayList aList = new ArrayList();
            foreach (PropertyInfo info in infos) {
                if ((fTypes & PropertyType.CanRead) == PropertyType.CanRead)
                    if (!info.CanRead)
                        continue;
                if ((fTypes & PropertyType.CanWrite) == PropertyType.CanWrite)
                    if (!info.CanWrite)
                        continue;
                if ((fTypes & PropertyType.IsSpecialName) == PropertyType.IsSpecialName)
                    if (!info.IsSpecialName)
                        continue;

                object[] desc = info.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), inherit);
                if (desc == null || desc.Length == 0)
                    continue;
                System.ComponentModel.DescriptionAttribute a = desc[0] as System.ComponentModel.DescriptionAttribute;
                if (a != null) {
                    aList.Add(a.Description);
                }
            }
            string[] des = new string[aList.Count];
            for (int i = 0; i < aList.Count; i++) {
                des[i] = aList[i].ToString();
            }
            return des;
        }
        #endregion 获取描述相关...

        #endregion Public static function...

    }
 
}
