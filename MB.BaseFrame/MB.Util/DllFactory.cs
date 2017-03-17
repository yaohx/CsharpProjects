//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2008-01-05
// Description	:	DllFactory 动态创建业务处理操作对象
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Reflection;
using System.Diagnostics;
using MB.Platform.Common;
using MB.Platform.Logging;

namespace MB.Util {
    /// <summary>
    /// DllFactory 动态创建业务处理操作对象。
    /// </summary>
    public class DllFactory : System.ContextBoundObject {
        private static readonly string ASSEMBLY_PATH = MB.Util.General.GeApplicationDirectory();

        #region Instance...
        private static object _Object = new object();
        private static DllFactory _Instance;

        private static ILogger s_logger = null;

        static DllFactory()
        {            
            s_logger = LoggerManager.GetLoggerForType(typeof(TraceEx));
        }

        protected DllFactory() { }

        /// <summary>
        /// Instance
        /// </summary>
        public static DllFactory Instance {
            get {
                if (_Instance == null) {
                    lock (_Object) {
                        if (_Instance == null)
                            _Instance = new DllFactory();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        #region Public 方法...
        /// <summary>
        /// 根据类型创建对象实例。
        /// </summary>
        /// <param name="objType"></param>
        /// <returns></returns>
        public object CreateInstance(Type objType) {
            if (objType == null) return null;
            object obj = null;
            try {
                obj = objType.Assembly.CreateInstance(objType.FullName, true);
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(string.Format("根据类型:{0}创建实例有误！",objType), APPMessageType.SysErrInfo, ex);
            }
            if(obj==null)
                throw new MB.Util.APPException(string.Format("根据类型:{0} 创建实例有误！",objType));
            return obj;
        }
        /// <summary>
        /// 通过反射实例化一个对象
        /// </summary>
        /// <param name="pName">类的名称</param>
        /// <param name="pAssemblyName">对应配件的名称</param>
        /// <returns></returns>
        public object LoadObject(string objName, string objAssemblyName) {
            return LoadObject(objName, null, objAssemblyName);
        }

        /// <summary>
        /// 通过反射实例化一个对象
        /// </summary>
        /// <param name="pName">类的名称</param>
        /// <param name="pPars">加载类需要的参数描述信息</param>
        /// <param name="pAssemblyName">对应配件的名称</param>
        /// <returns></returns>
        public object LoadObject(string objName, object[] objPars, string objAssemblyName)
        {
            string filePath = checkAndRetuanAssemblyFullName(objAssemblyName);
            object obj = null;

            //有可能根据相对路径来进行LoadWithPartialName
            //if (System.IO.File.Exists(filePath)) {
            try
            {
                Assembly DLL = Assembly.LoadFrom(filePath);
                if (DLL == null)
                {
                    s_logger.ErrorFormat("加载程序集{0}失败，文件路径{1}.", objAssemblyName, filePath);
                }

                System.Type t = DLL.GetType();

                try
                {
                    obj = DLL.CreateInstance(objName, true, BindingFlags.Default, null, objPars, null, null);
                }
                catch (Exception ex)
                {
                    s_logger.Fatal(ex);
                    return null;
                }

                if (obj == null)
                {
                    s_logger.ErrorFormat("创建对象{0}失败，文件路径{1}.", objName, filePath);
                }
                
                MB.Util.TraceEx.WriteIf(obj != null, string.Format("加载对象 {0} 出错,文件{1} 可能找不到", objName, filePath), MB.Util.APPMessageType.SysErrInfo);
            }
            catch (Exception ex)
            {
                throw new MB.Util.APPException(ex);
            }

            return obj;
        }

        /// <summary>
        /// 根据配件的名称从当前目录或者goal assembly 中查找并创建对象。
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="objPars"></param>
        /// <param name="objAssemblyName">配件名称，不需要后缀名</param>
        /// <returns></returns>
        public object LoadWithPartialName(string objName, object[] objPars, string objAssemblyName) {
            object obj = null;
            try {
                Assembly DLL = Assembly.LoadWithPartialName(objAssemblyName);
                System.Type t = DLL.GetType();

                obj = DLL.CreateInstance(objName, true, BindingFlags.Default, null, objPars, null, null);
                MB.Util.TraceEx.WriteIf(obj != null, string.Format("加载对象 {0} 出错,文件{1} 可能找不到", objName, objAssemblyName), MB.Util.APPMessageType.SysErrInfo);
            }
            catch (Exception ex) {
               
                throw new MB.Util.APPException(ex);
            }

            return obj;
        }
        /// <summary>
        /// 在指定的配件中获取对象类型。
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="objAssemblyName"></param>
        /// <returns></returns>
        public Type GetObjectType(string objName, string objAssemblyName) {
            string filePath = checkAndRetuanAssemblyFullName(objAssemblyName);
            Type objType = null;
            if (System.IO.File.Exists(filePath)) {
                Assembly DLL = Assembly.LoadFrom(filePath);
                System.Type t = DLL.GetType();
                objType = DLL.GetType(objName,false, true); 
            }
            MB.Util.TraceEx.WriteIf(objType != null, string.Format("在{0} 中获取对象类型{1}出错,文件{2} 可能找不到！", objAssemblyName, objName,filePath), MB.Util.APPMessageType.SysErrInfo);
            return objType;
        }
        #endregion Public 方法...
        //检查并返回配件的完整路径
        private string checkAndRetuanAssemblyFullName(string objAssemblyName) {
            //主要在当前目录进行查找
            string filePath = ASSEMBLY_PATH + objAssemblyName.Trim();
            if (!System.IO.File.Exists(filePath)) {
                string dFile = filePath + ".dll";
                if (!System.IO.File.Exists(dFile)) {
                    string eFile = filePath + ".exe";
                    if (System.IO.File.Exists(eFile)) {
                        filePath = eFile;
                    }
                }
                else {
                    filePath = dFile;
                }
            }
            if (!System.IO.File.Exists(filePath)) {
                filePath = objAssemblyName;
                if (filePath.ToLower().LastIndexOf(".dll")!=filePath.Length - 4) {
                    filePath += ".dll";
                }
            }
            return filePath;
        }
    }
}
