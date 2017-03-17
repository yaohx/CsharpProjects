//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	TraceEx 程序代码跟踪
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MB.Aop {
    /// <summary>
    ///  描述Aspect配置信息的类。
    /// </summary>
    [System.Diagnostics.DebuggerStepThrough()]
    internal class AspectInfo {
        private string _AssemblyName;
        private string _ClassName;
        private string _DeployModell;
        private string _PointCutType;
        private string _ActionPosition;
        private string _MatchClass;
        private string _MatchMethod;
        private string _MatchPattern;
        private IInjection _SingletonAspect;

        public AspectInfo(string assemblyName, string className, string deployModell, string pointCutType,
                            string actionPosition, string matchClass, string matchMethod, string matchPattern) {

            _AssemblyName = assemblyName;
            _ClassName = className;
            _DeployModell = deployModell;
            _PointCutType = pointCutType;
            _ActionPosition = actionPosition;
            _MatchClass = matchClass;
            _MatchMethod = matchMethod;
            _MatchPattern = matchPattern;
            if (DeployModell.Equals("Singleton")) {
                string filePath = InjectionManager.ASSEMBLY_PATH;
                if (filePath.LastIndexOf(@"\") < filePath.Length - 1)
                    filePath += @"\";

                filePath +=assemblyName;
                if (System.IO.File.Exists(filePath)) {
                    System.Reflection.Assembly DLL = System.Reflection.Assembly.LoadFrom(filePath);
                    System.Type t = DLL.GetType();
                    SingletonAspect = DLL.CreateInstance(className, true, BindingFlags.Default, null, null, null, null) as IInjection;
                }
                else {
                    throw new Exception(string.Format("Injecttion 配置有误，配件{0}找不到！",filePath));
                }
            }

        }


        /// <summary>
        /// Aspect所在的程序集名称
        /// </summary>
        public string AssemblyName {
            get { return _AssemblyName; }
            set { _AssemblyName = value; }
        }
        /// <summary>
        /// Aspect的类名
        /// </summary>
        public string ClassName {
            get { return _ClassName; }
            set { _ClassName = value; }
        }

        /// Aspect部署模式，有Singleton和MultiInstance两种模式
        /// </summary>
        public string DeployModell {
            get { return _DeployModell; }
            set { _DeployModell = value; }
        }

        /// <summary>
        /// 拦截的类型，方法(Method)、构造函数(Construction)和属性(Property)，不过目前对Property的拦截等同于Method
        /// </summary>
        public string PointCutType {
            get { return _PointCutType; }
            set { _PointCutType = value; }
        }

        /// <summary>
        /// 拦截位置的类型，有Before,After和Both
        /// </summary>
        public string ActionPosition {
            get { return _ActionPosition; }
            set { _ActionPosition = value; }
        }

        /// <summary>
        /// 被拦截类的表达式
        /// </summary>
        public string MatchClass {
            get { return _MatchClass; }
            set { _MatchClass = value; }
        }

        /// <summary>
        /// 被拦截方法的表达式
        /// </summary>
        public string MatchMethod {
            get { return _MatchMethod; }
            set { _MatchMethod = value; }
        }

        /// <summary>
        /// 拦截的表达式
        /// </summary>
        public string MatchPattern {
            get { return _MatchPattern; }
            set { _MatchPattern = value; }
        }

        /// <summary>
        /// 当该Aspect是Singleton的时候，表示该Aspect对象
        /// </summary>
        public IInjection SingletonAspect {
            get { return _SingletonAspect; }
            set { _SingletonAspect = value; }
        }

    }


}
