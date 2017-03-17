//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	Aspect 拦截注入管理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MB.Aop {
    /// <summary>
    /// Aspect 拦截注入管理。
    /// </summary>
    [System.Diagnostics.DebuggerStepThrough()]
    public class InjectionManager {
        public static readonly string ASSEMBLY_PATH = MB.Util.General.GeApplicationDirectory();  

        // 所有的Aspect集合
        private  IList<AspectInfo> _Aspects = null;
        // Aspect和被拦截类的映射表
        private  System.Collections.Generic.Dictionary<string,bool> _AspectsMatch = new Dictionary<string,bool>();

        /// <summary>
        /// 所有的Aspect集合
        /// </summary>
        internal  IList<AspectInfo>  Aspects {
            get { return _Aspects; }
        }
        #region Instance...
        private static Object _Obj = new object();
        private static InjectionManager _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected InjectionManager() {
            _Aspects = (IList<AspectInfo>)ConfigurationSettings.GetConfig("MBBaseFrameAspects");
            if (_Aspects == null)
                _Aspects = new List<AspectInfo>();
           // _Aspects = cfg.Aspects;
        }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static InjectionManager Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new InjectionManager();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        /// <summary>
        /// 获取相关的Aspects
        /// </summary>
        /// <param name="msg">IMessage，包含有关方法调用的信息。</param>
        /// <param name="position">拦截的位置</param>
        /// <returns>Aspect数组</returns>
        public IInjection[] GetAspect(IMessage msg, AspectActionPosition position) {

            IMethodMessage mmsg = msg as IMethodMessage;
            List<IInjection> al = new List<IInjection>();
            string fullMethodInfo = mmsg.MethodBase.ReflectedType.FullName + "--" + mmsg.MethodName;

            for (int i = 0; i < _Aspects.Count; i++) {
                string key = GetMatchHashKey(mmsg, _Aspects[i], position);
                if (!_AspectsMatch.ContainsKey(key)) {
                    lock (_AspectsMatch) {
                        if (!_AspectsMatch.ContainsKey(key))
                        {
                            AddAspectMatch(mmsg, _Aspects[i], position);
                        }
                    }
                }
                if (_AspectsMatch[key]) {
                    IInjection asp;
                    if (_Aspects[i].DeployModell.Equals("Singleton")) {
                        asp = _Aspects[i].SingletonAspect;
                    }
                    else {
                        asp = Activator.CreateInstance(_Aspects[i].AssemblyName, _Aspects[i].ClassName).Unwrap() as IInjection;
                    }
                    al.Add(asp);
                }
            }
            return (IInjection[])al.ToArray();
        }

        //添加Aspect和被拦截类的映射。缓存该信息已获得好的性能
        private void AddAspectMatch(IMethodMessage mmsg, AspectInfo aspectInfo, AspectActionPosition position) {
            bool match = (aspectInfo.ActionPosition.Equals("Both") || position.ToString().Equals(aspectInfo.ActionPosition));
            if (match) {
                //首先校验是否Match类
                if (aspectInfo.MatchClass.IndexOf("*") > -1) {
                    string clssName = aspectInfo.MatchClass;
                    if (clssName.StartsWith("*")) {
                        clssName = clssName.Substring(1,clssName.Length - 1);
                    }
                    match = Regex.IsMatch(mmsg.MethodBase.ReflectedType.FullName,clssName);
                }
                else {
                    match = (mmsg.MethodBase.ReflectedType.FullName.Equals(aspectInfo.MatchClass)
                        || mmsg.MethodBase.ReflectedType.Name.Equals(aspectInfo.MatchClass));

                }
                //校验是否Match方法
                if (match) {
                    match = mmsg.MethodBase.IsConstructor;
                    if (match) {
                        match = (aspectInfo.PointCutType.IndexOf("Construction") > -1);
                    }
                    else {
                        match = ((aspectInfo.PointCutType.IndexOf("Method") > -1) || (aspectInfo.PointCutType.IndexOf("Property") > -1));
                        if (match) {
                            if (aspectInfo.MatchMethod.IndexOf("*") > -1) {
                                string methodName = aspectInfo.MatchMethod;
                                if (methodName.StartsWith("*")) {
                                    methodName = methodName.Substring(1, methodName.Length - 1);
                                }
                                match = Regex.IsMatch(mmsg.MethodName, methodName);
                            }
                            else {
                                match = (mmsg.MethodName.Equals(aspectInfo.MatchMethod));
                            }
                        }
                    }
                }
            }
            _AspectsMatch[GetMatchHashKey(mmsg, aspectInfo, position)] =  match;
        }



        // 获取Aspect和被拦截类的映射的HashKey
        private string GetMatchHashKey(IMethodMessage mmsg, AspectInfo aspectInfo, AspectActionPosition position) {

            return mmsg.MethodBase.ReflectedType.FullName + "--" + mmsg.MethodName + aspectInfo.MatchPattern + position.ToString();

        }
    }
}
