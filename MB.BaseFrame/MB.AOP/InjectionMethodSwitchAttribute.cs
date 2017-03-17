//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved.
// Author		:   chendc
// Create date	:	2008-08-25
// Description	:	用于决定一个被InjectionManagerAttribute修饰的class的某个特定方法是否启用截获 。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------

using System;

namespace MB.Aop{
    /// <summary>
    /// 用于决定一个被InjectionManagerAttribute修饰的class的某个特定方法是否启用截获 。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [System.Diagnostics.DebuggerStepThrough()]
    public class InjectionMethodSwitchAttribute : Attribute {

        private bool _AspectManaged;

        #region 构造函数...
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="aspectManaged"></param>
        public InjectionMethodSwitchAttribute(bool aspectManaged) {

            _AspectManaged = aspectManaged;
        }
        #endregion 构造函数...

        #region public 属性...
        /// <summary>
        /// 获取判断是否启动运行截获的控制。
        /// </summary>
        public bool AspectManaged {
            get {
                return _AspectManaged;
            }
        }
        #endregion public 属性...

    }
}
