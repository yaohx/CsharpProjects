////---------------------------------------------------------------- 
//// Copyright (C) 2008-2009 www.metersbonwe.com
//// All rights reserved. 
//// Author		:	chendc
//// Create date	:	2009-03-15
//// Description	:	为了修正DynamicPropertyAccessor 不能释放的问题而增加 
//// Modify date	:			By:					Why: 
////----------------------------------------------------------------
//using System;
//using System.Reflection;
//using System.Reflection.Emit;
//using System.Threading;
//using System.Collections;

//namespace MB.Util.Emit {
//    /// <summary>
//    /// 通过指定类型,动态获取对象的属性信息。
//    /// </summary>
//    public class DynamicPropertyAccessor : IDynamicPropertyAccessor {
//        /// <summary>
//        /// 创建一个新的属性获取器.
//        /// </summary>
//        /// <param name="targetType">目标对象类型</param>
//        /// <param name="property">属性名称</param>
//        public DynamicPropertyAccessor(Type targetType, string property) : this(targetType, targetType.GetProperty(property)) {
         
//        }
//        /// <summary>
//        /// 创建一个新的属性获取器.
//        /// </summary>
//        /// <param name="targetType">目标对象类型.</param>
//        /// <param name="propertyInfo">属性.</param>
//        public DynamicPropertyAccessor(Type targetType, PropertyInfo propertyInfo) {
//            this.mTargetType = targetType;
//            this.mProperty = propertyInfo.Name ;
//            //
//            // 判断属性是否存在
//            //
//            if (propertyInfo == null) {
//                throw new DynamicPropertyAccessorException(
//                    string.Format("属性 \"{0}\" 在当前类型 "
//                    + "{1} 中不存在", mProperty, targetType));
//            }
//            else {
//                this.mCanRead = propertyInfo.CanRead;
//                this.mCanWrite = propertyInfo.CanWrite;
//                this.mPropertyType = propertyInfo.PropertyType;
//            }
//        }

//        /// <summary>
//        /// 从目标中获取属性的值
//        /// </summary>
//        /// <param name="target">目标对象.</param>
//        /// <returns>属性值</returns>
//        public object Get(object target) {
//            if (mCanRead) {
//                if (this.mEmittedPropertyAccessor == null) {
//                    this.Init();
//                }

//                return this.mEmittedPropertyAccessor.Get(target);
//            }
//            else {
//                throw new DynamicPropertyAccessorException(
//                    string.Format("属性 \"{0}\" 不存在一个 GET 方法.",
//                    mProperty));
//            }
//        }

//        /// <summary>
//        /// 设置目标对象的属性值
//        /// </summary>
//        /// <param name="target">目标对象</param>
//        /// <param name="value">需要设置的值</param>
//        public void Set(object target, object value) {
//            if (mCanWrite) {
//                if (this.mEmittedPropertyAccessor == null) {
//                    this.Init();
//                }
//                //先把值转换为对应的数据类型
//                object cVal = MB.Util.MyReflection.Instance.ConvertValueType(mPropertyType, value);
//                this.mEmittedPropertyAccessor.Set(target, cVal);
//            }
//            else {
//                throw new DynamicPropertyAccessorException(
//                    string.Format("属性 \"{0}\" 不存在一个SET 方法.",
//                    mProperty));
//            }
//        }

//        /// <summary>
//        /// 判断属性是否支持读
//        /// </summary>
//        public bool CanRead {
//            get {
//                return this.mCanRead;
//            }
//        }

//        /// <summary>
//        /// 判断属性是否允许写
//        /// </summary>
//        public bool CanWrite {
//            get {
//                return this.mCanWrite;
//            }
//        }

//        /// <summary>
//        /// 属性所在的目标类型
//        /// </summary>
//        public Type TargetType {
//            get {
//                return this.mTargetType;
//            }
//        }

//        /// <summary>
//        /// 属性的类型
//        /// </summary>
//        public Type PropertyType {
//            get {
//                return this.mPropertyType;
//            }
//        }

//        private Type mTargetType;
//        private string mProperty;
//        private Type mPropertyType;
//        private IDynamicPropertyAccessor mEmittedPropertyAccessor;
//        private Hashtable mTypeHash;
//        private bool mCanRead;
//        private bool mCanWrite;

//        /// <summary>
//        /// 产生包含动态类型信息的新assembly
//        /// </summary>
//        private void Init() {
//            this.InitTypes();

//            //创建一个新的assembly并实例化动态属性获取器
//            Assembly assembly = EmitAssembly();

//            mEmittedPropertyAccessor = assembly.CreateInstance("Property") as IDynamicPropertyAccessor;

//            if (mEmittedPropertyAccessor == null) {
//                throw new Exception("不能创建一个属性的获取器.");
//            }
//        }

//        /// <summary>
//        /// 建立一个哈稀表存储Msil 语法和类型之间的关系
//        /// </summary>
//        private void InitTypes() {
//            mTypeHash = new Hashtable();
//            mTypeHash[typeof(sbyte)] = OpCodes.Ldind_I1;
//            mTypeHash[typeof(byte)] = OpCodes.Ldind_U1;
//            mTypeHash[typeof(char)] = OpCodes.Ldind_U2;
//            mTypeHash[typeof(short)] = OpCodes.Ldind_I2;
//            mTypeHash[typeof(ushort)] = OpCodes.Ldind_U2;
//            mTypeHash[typeof(int)] = OpCodes.Ldind_I4;
//            mTypeHash[typeof(uint)] = OpCodes.Ldind_U4;
//            mTypeHash[typeof(long)] = OpCodes.Ldind_I8;
//            mTypeHash[typeof(ulong)] = OpCodes.Ldind_I8;
//            mTypeHash[typeof(bool)] = OpCodes.Ldind_I1;
//            mTypeHash[typeof(double)] = OpCodes.Ldind_R8;
//            mTypeHash[typeof(float)] = OpCodes.Ldind_R4;
//        }

//        /// <summary>
//        /// 创建一个提供get 和 set 方法的assembly。
//        /// </summary>
//        private Assembly EmitAssembly() {
//            // 创建一个配件的名称。
//            AssemblyName assemblyName = new AssemblyName();
//            assemblyName.Name = "DynamicPropertyAccessorAssembly";

//            // 创建新assembly并包含一个模块。
//            AssemblyBuilder newAssembly = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
//            ModuleBuilder newModule = newAssembly.DefineDynamicModule("Module");

//            //在assembly中定义public 属性			
//            TypeBuilder myType =
//                newModule.DefineType("Property", TypeAttributes.Public);

//            //
//            //让这个类实现 IPropertyAccessor 接口
//            myType.AddInterfaceImplementation(typeof(IDynamicPropertyAccessor));

//            // 增加一个构造函数
//            ConstructorBuilder constructor =
//                myType.DefineDefaultConstructor(MethodAttributes.Public);

//            //
//            // 定义一个get 操作的方法
//            Type[] getParamTypes = new Type[] { typeof(object) };
//            Type getReturnType = typeof(object);
//            MethodBuilder getMethod =
//                myType.DefineMethod("Get",
//                MethodAttributes.Public | MethodAttributes.Virtual,
//                getReturnType,
//                getParamTypes);

//            //从这个方法中得到一个ILGenerator ，通过它我们可以通过emit 对IL 进行操作
//            ILGenerator getIL = getMethod.GetILGenerator();


//            // 通过emit 对IL进行操作。
//            MethodInfo targetGetMethod = this.mTargetType.GetMethod("get_" + this.mProperty);

//            if (targetGetMethod != null) {

//                getIL.DeclareLocal(typeof(object));
//                getIL.Emit(OpCodes.Ldarg_1);								//加载第一个参数
//                //(target object)

//                getIL.Emit(OpCodes.Castclass, this.mTargetType);			//转换数据类型

//                getIL.EmitCall(OpCodes.Call, targetGetMethod, null);		//获取属性的值

//                if (targetGetMethod.ReturnType.IsValueType) {
//                    getIL.Emit(OpCodes.Box, targetGetMethod.ReturnType);	//如果是值类型就进行装箱
//                }
//                getIL.Emit(OpCodes.Stloc_0);								//存储起来

//                getIL.Emit(OpCodes.Ldloc_0);
//            }
//            else {
//                getIL.ThrowException(typeof(MissingMethodException));
//            }

//            getIL.Emit(OpCodes.Ret);


//            // 定义一个set 操作的方法
//            Type[] setParamTypes = new Type[] { typeof(object), typeof(object) };
//            Type setReturnType = null;
//            MethodBuilder setMethod =
//                myType.DefineMethod("Set",
//                MethodAttributes.Public | MethodAttributes.Virtual,
//                setReturnType,
//                setParamTypes);

//            //从这个方法中得到一个ILGenerator ，通过它我们可以通过emit 对IL 进行操作
//            ILGenerator setIL = setMethod.GetILGenerator();
//            //
//            //通过emit 对IL进行操作。
//            //

//            MethodInfo targetSetMethod = this.mTargetType.GetMethod("set_" + this.mProperty);
//            if (targetSetMethod != null) {
//                Type paramType = targetSetMethod.GetParameters()[0].ParameterType;

//                setIL.DeclareLocal(paramType);
//                setIL.Emit(OpCodes.Ldarg_1);						//加载第一个参数
//                //目标对象

//                setIL.Emit(OpCodes.Castclass, this.mTargetType);	//转换数据类型

//                setIL.Emit(OpCodes.Ldarg_2);						//加载第二个参数
//                //值对象

//                if (paramType.IsValueType) {
//                    setIL.Emit(OpCodes.Unbox, paramType);			//如果是值类型进行拆箱	
//                    if (mTypeHash[paramType] != null)					//结束加载
//                    {
//                        OpCode load = (OpCode)mTypeHash[paramType];
//                        setIL.Emit(load);
//                    }
//                    else {
//                        setIL.Emit(OpCodes.Ldobj, paramType);
//                    }
//                }
//                else {
//                    setIL.Emit(OpCodes.Castclass, paramType);		//类型转换
//                }

//                setIL.EmitCall(OpCodes.Callvirt,
//                    targetSetMethod, null);							//设置属性的值。
//            }
//            else {
//                setIL.ThrowException(typeof(MissingMethodException));
//            }

//            setIL.Emit(OpCodes.Ret);

//            // 加载类型。
//            myType.CreateType();

//            return newAssembly;
//        }
//    }
//}
