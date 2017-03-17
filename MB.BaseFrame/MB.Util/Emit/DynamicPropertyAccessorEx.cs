//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	 
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Util.Emit;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;
using System.Runtime.Serialization;

namespace MB.Util.Emit {
    /// <summary>
    /// 通过指定类型,动态获取对象的属性信息。
    /// </summary>
    public class DynamicPropertyAccessor : IDynamicPropertyAccessor {
        /// <summary>
        /// 创建动态对象的属性访问器，以便快速的动态访问
        /// </summary>
        /// <param name="typeObject">对象的类型</param>
        /// <returns>属性访问器，以字段存储</returns>
        public static Dictionary<string, DynamicPropertyAccessor> CreateDynamicProperyAccessOnObjType(Type typeObject) {
            PropertyInfo[] infos = typeObject.GetProperties();
            Dictionary<string, DynamicPropertyAccessor> dAccs = new Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor>();
            foreach (PropertyInfo info in infos) {
                object[] atts = info.GetCustomAttributes(typeof(DataMemberAttribute), true);
                if (atts == null || atts.Length == 0) continue;

                dAccs.Add(info.Name, new MB.Util.Emit.DynamicPropertyAccessor(typeObject, info));
            }
            return dAccs;
        }

        private delegate void SetPropertyDelegate(object obj, object m_Value);
        private delegate object GetPropertyDelegate(object obj);

        private Type _PropertyType;
        private Type _TargetType;
        private PropertyInfo _PropertyInfo;
        private SetPropertyDelegate _SetDelegate;
        private GetPropertyDelegate _GetDelegate;

        static DynamicPropertyAccessor() {
            InitTypes();
        }
        /// <summary>
        /// 创建一个新的属性获取器.
        /// </summary>
        /// <param name="targetType">目标对象类型</param>
        /// <param name="property">属性名称</param>
        public DynamicPropertyAccessor(Type targetType, string property)
            : this(targetType, property,targetType.GetProperty(property)) {

        }
        /// <summary>
        /// 创建一个新的属性获取器
        /// </summary>
        /// <param name="targetType">目标对象类型</param>
        /// <param name="propertyInfo">属性</param>
        public DynamicPropertyAccessor(Type targetType, PropertyInfo propertyInfo)
            : this(targetType, null, propertyInfo) {
        }
        /// <summary>
        /// 创建一个新的属性获取器.
        /// </summary>
        /// <param name="targetType">目标对象类型.</param>
        /// <param name="propertyInfo">属性.</param>
        private DynamicPropertyAccessor(Type targetType,string propertyName, PropertyInfo propertyInfo) {
            _TargetType = targetType;
            _PropertyInfo = propertyInfo;
            if (propertyName == null && propertyInfo != null)
                propertyName = propertyInfo.Name;
            //
            // 判断属性是否存在
            //
            if (_PropertyInfo == null) {
                throw new DynamicPropertyAccessorException(
                    string.Format("属性 \"{0}\" 在当前类型 "
                    + "{1} 中不存在", propertyName, targetType));
            }
            else {
                _PropertyType = propertyInfo.PropertyType;
            }
        }
        public Type PropertyType {
            get {
                return _PropertyType;
            }
        }
        /// <summary>
        /// 从目标中获取属性的值
        /// </summary>
        /// <param name="target">目标对象.</param>
        /// <returns>属性值</returns>
        public object Get(object target) {
            if (_PropertyInfo.CanRead) {
                if (_GetDelegate == null) {
                    _GetDelegate = getPropertyorValue(_PropertyInfo);
                }
                return _GetDelegate.Invoke(target);
            }
            else {
                throw new DynamicPropertyAccessorException(
                    string.Format("属性 \"{0}\" 不存在一个 GET 方法.",
                    _PropertyInfo.Name));
            }
        }

        /// <summary>
        /// 设置目标对象的属性值
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="value">需要设置的值</param>
        public void Set(object target, object value) {
            if (_PropertyInfo.CanWrite) {
                if (_SetDelegate == null) {
                    _SetDelegate = setPropertyValue(_TargetType, _PropertyInfo);
                }
                //先把值转换为对应的数据类型
                object cVal = MB.Util.MyReflection.Instance.ConvertValueType(_PropertyInfo.PropertyType, value);

                _SetDelegate.Invoke(target, cVal);
            }
            else {
                throw new DynamicPropertyAccessorException(
                    string.Format("属性 \"{0}\" 不存在一个SET 方法.",
                    _PropertyInfo.Name));
            }
        }

        #region 内部函数处理...
        //属性设置方法 
        private SetPropertyDelegate setPropertyValue(Type oType, PropertyInfo pi) {
            string methodName = pi.Name;
            DynamicMethod dm = new DynamicMethod("SET_" + methodName, typeof(void),
                new Type[] { typeof(object), typeof(object) }, pi.Module, true);
            ILGenerator il = dm.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);

            il.Emit(OpCodes.Ldarg_1);

            Type paramType = pi.PropertyType;
            if (paramType.IsValueType) {
                il.Emit(OpCodes.Unbox, paramType);			//如果是值类型进行拆箱	
                if (_TYPE_HASH[paramType] != null)					//结束加载
				{
                    OpCode load = (OpCode)_TYPE_HASH[paramType];
                    il.Emit(load);
                }
                else {
                    il.Emit(OpCodes.Ldobj, paramType);
                }
            }
            else {
                il.Emit(OpCodes.Castclass, paramType);		//类型转换
            }
            il.EmitCall(OpCodes.Callvirt, pi.GetSetMethod(true), null);
            il.Emit(OpCodes.Ret);
            return (SetPropertyDelegate)dm.CreateDelegate(typeof(SetPropertyDelegate));

        }
        //属性获取方法
        private GetPropertyDelegate getPropertyorValue(PropertyInfo pi) {
            string methodName = pi.Name;
            Module mod = pi.Module;

            DynamicMethod dm = new DynamicMethod("GET_" + methodName, typeof(object), new Type[] { typeof(object) }, mod, true);
            ILGenerator il = dm.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            if (pi != null) {
                il.EmitCall(OpCodes.Callvirt, pi.GetGetMethod(), null);
                if (pi.PropertyType.IsValueType) {
                    il.Emit(OpCodes.Box, pi.PropertyType);
                }
            }
            il.Emit(OpCodes.Ret);

            return (GetPropertyDelegate)dm.CreateDelegate(typeof(GetPropertyDelegate));


        }

        #endregion 内部函数处理...

        private static Hashtable _TYPE_HASH;
        /// <summary>
        /// 建立一个哈稀表存储Msil 语法和类型之间的关系
        /// </summary>
        private static void InitTypes() {
            _TYPE_HASH = new Hashtable();
            _TYPE_HASH[typeof(sbyte)] = OpCodes.Ldind_I1;
            _TYPE_HASH[typeof(byte)] = OpCodes.Ldind_U1;
            _TYPE_HASH[typeof(char)] = OpCodes.Ldind_U2;
            _TYPE_HASH[typeof(short)] = OpCodes.Ldind_I2;
            _TYPE_HASH[typeof(ushort)] = OpCodes.Ldind_U2;
            _TYPE_HASH[typeof(int)] = OpCodes.Ldind_I4;
            _TYPE_HASH[typeof(uint)] = OpCodes.Ldind_U4;
            _TYPE_HASH[typeof(long)] = OpCodes.Ldind_I8;
            _TYPE_HASH[typeof(ulong)] = OpCodes.Ldind_I8;
            _TYPE_HASH[typeof(bool)] = OpCodes.Ldind_I1;
            _TYPE_HASH[typeof(double)] = OpCodes.Ldind_R8;
            _TYPE_HASH[typeof(float)] = OpCodes.Ldind_R4;
        }
    }
}
