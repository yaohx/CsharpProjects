//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	自定义的Aspect代理特性，在这里，创建受Aspect管理的类的代理类。任何需要接受Aspect管理的类，都必须加上这个特性
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Runtime.Remoting.Proxies;
using System.Security.Permissions;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Reflection;
using System.Configuration;

namespace MB.Aop {

    /// <summary>
    /// 自定义的Aspect代理特性，在这里，创建受Aspect管理的类的代理类。
    /// 任何需要接受Aspect管理的类，都必须加上这个特性	
    /// <br /><br />
    /// <b>示例</b><br />
    /// <code>
    /// [InjectionManagerAttribute]<br />
    /// public class BusinessClass{}
    /// </code>
    /// </summary>
    /// <example>
    /// <code>
    /// [InjectionManagerAttribute]
    /// public class BusinessClass{}
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class)]
    [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
    [System.Diagnostics.DebuggerStepThrough()]
    public class InjectionManagerAttribute : ProxyAttribute {

        private bool _AspectManaged;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public InjectionManagerAttribute() {
            var aop = System.Configuration.ConfigurationManager.AppSettings["NotStartAopManaged"];
            if (string.IsNullOrEmpty(aop))
                _AspectManaged = true;
            else
                _AspectManaged = aop == "False" || aop == "false";

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="AspectManaged">指明该类是否接受Aspect管理</param>
        public InjectionManagerAttribute(bool aspectManaged) {

            _AspectManaged =aspectManaged;

        }

        /// <summary>
        /// 创建受Aspect管理的类的代理类
        /// </summary>
        /// <param name="serverType">要创建的类的类型</param>
        /// <returns>受Aspect管理的类的代理类</returns>
        public override MarshalByRefObject CreateInstance(Type serverType) {

            MarshalByRefObject mobj = base.CreateInstance(serverType);
            if (_AspectManaged) {
                RealProxy realProxy = new InjectionProxy(serverType, mobj);
                MarshalByRefObject retobj = realProxy.GetTransparentProxy() as MarshalByRefObject;
                return retobj;
            }
            else {
                return mobj;
            }
        }

    }
}

