//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-04
// Description	:	SingletonProvider： 一个泛型来实现的单例模式重用。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util {
    /// <summary>
    /// 一个泛型来实现的单例模式重用。
    /// 对于提供给其他人使用的单例模式 不能使用该方法，需要通过硬编码的方式来进行约束。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonProvider<T> where T : new() {

        SingletonProvider() {
        }

        public static T Instance {
            get {
                return SingletonCreator.instance;
            }
        }

        class SingletonCreator {
            static SingletonCreator() {
            }
            internal static readonly T instance = new T();
        }
    }
}
