//---------------------------------------------------------------- 
// Copyright (C)2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	TraceEx 程序代码跟踪
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting.Channels;

namespace MB.Aop {
    /// <summary>
    /// 注入真实代理，在这个类里面，实现对方法的拦截。
    /// </summary>
    // class InjectionProxy {
    [System.Diagnostics.DebuggerStepThrough()]
    public class InjectionProxy : RealProxy {
        MarshalByRefObject _Target;

        #region 构造函数...
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public InjectionProxy()
            : base() {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="myType">被代理的类的类型</param>
        public InjectionProxy(Type myType)
            : base(myType) {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="myType">被代理的类的类型</param>
        /// <param name="obj">被代理的对象</param>
        public InjectionProxy(Type myType, MarshalByRefObject obj)
            : base(myType) {

            _Target = obj;
        }
        #endregion 构造函数...


        /// <summary>
        /// 当在派生类中重写时，在当前实例所表示的远程对象上调用在所提供的 IMessage 中指定的方法。<br />
        /// WebsharpAspect在这里执行对方法执行的拦截处理
        /// </summary>
        /// <param name="msg">IMessage，包含有关方法调用的信息。</param>
        /// <returns>调用的方法所返回的消息，包含返回值和所有 out 或 ref 参数。</returns>
        public override IMessage Invoke(IMessage msg) {
            bool canInjection = checkCanInjection(msg);
            DateTime beginExecute = System.DateTime.Now;
            if(canInjection)
                BeginProcess(msg);

            if (msg is IConstructionCallMessage) {
                IConstructionCallMessage ccm = (IConstructionCallMessage)msg;
                RemotingServices.GetRealProxy(_Target).InitializeServerObject(ccm);
                ObjRef oRef = RemotingServices.Marshal(_Target);
                RemotingServices.Unmarshal(oRef);
                IMessage retMsg = EnterpriseServicesHelper.CreateConstructionReturnMessage(ccm, (MarshalByRefObject)this.GetTransparentProxy());
                if (canInjection)
                    EndProcess(beginExecute, retMsg);

                return retMsg;
            }
            else {
                IMethodReturnMessage returnmsg = RemotingServices.ExecuteMessage(_Target, (IMethodCallMessage)msg);

                if (canInjection)
                    EndProcess(beginExecute,returnmsg);

                return returnmsg;
            }
        }

        #region private function...
        //判断是否配置了方面的拦截
        private bool checkCanInjection(IMessage msg) {
            System.Reflection.MethodBase method = null;
            if (msg is IConstructionCallMessage) {
                IConstructionCallMessage ccm = (IConstructionCallMessage)msg;
                method = ccm.MethodBase;
            }
            else {
                IMethodCallMessage callmsg = (IMethodCallMessage)msg;
                method = callmsg.MethodBase;
            }
            object[] pros = method.GetCustomAttributes(typeof(InjectionMethodSwitchAttribute),false);
            InjectionMethodSwitchAttribute att = null;
            if (pros != null && pros.Length > 0)
                att = pros[0] as InjectionMethodSwitchAttribute;

            if (att == null) 
                return true;
            else 
                return att.AspectManaged;
        }
        // 被拦截方法执行前进行处理
        private void  BeginProcess(IMessage msg) {
            IInjection[] aspectBefore = InjectionManager.Instance.GetAspect(msg, AspectActionPosition.Before);
            if ((aspectBefore != null) && (aspectBefore.Length > 0)) {
                for (int i = 0; i < aspectBefore.Length; i++) {
                    aspectBefore[i].BeginProcess(msg);
                }
            }
        }
        // 被拦截方法执行后进行处理
        private void EndProcess(DateTime beginExecute, IMessage msg) {
            IInjection[] aspectAfter = InjectionManager.Instance.GetAspect(msg, AspectActionPosition.After);
            if ((aspectAfter != null) && (aspectAfter.Length > 0)) {
                for (int i = 0; i < aspectAfter.Length; i++) {
                    aspectAfter[i].EndProcess(beginExecute,msg);
                }
            }
        }
        #endregion private function...
    }
}
