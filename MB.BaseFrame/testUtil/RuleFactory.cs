using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using MB.Util;

namespace testUtil
{
    /// <summary>
    /// RuleFactory （放在公共的接口类中）
    /// </summary>
    public class RuleFactory
    {
        private static ConcurrentDictionary<Type, Type> _Mappings = new ConcurrentDictionary<Type, Type>();
        /// <summary>
        /// 注册业务对象
        /// </summary>
        public static void Registry<T,TRule>() 
        where TRule : T{
            _Mappings[typeof(T)] = typeof(TRule);
        }
        /// <summary>
        /// 创建服务端互调用对象
        /// </summary>
        /// <typeparam name="T">服务业务接口类型</typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>() {
            if (!_Mappings.ContainsKey(typeof(T)))
                throw new APPException(string.Format("接口{0} 还没有在{1}中进行注册",
                    typeof(T).FullName, "RuleFactory"), APPMessageType.SysErrInfo);

            return (T)DllFactory.Instance.CreateInstance(_Mappings[typeof(T)]);
        }
        /// <summary>
        /// InvokeMethod
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="method"></param>
        /// <returns></returns>
        public static TResult InvokeMethod<TInterface, TResult>(Func<TInterface, TResult> method) {
            TInterface instance = CreateInstance<TInterface>();
            return method(instance);
        }
    }
    //如果是服务端在 Global.asax 中进行注册
    //RuleFactory.Registry<接口>（服务类型）

    //如果是客户端可以直接在 MB.ERP.exe 中进行注册就可以

    public interface MyInterface
    {
        int GetData(int count);
    }

    public class MyRule : MyInterface
    {

        public int GetData(int count) {
            throw new NotImplementedException();
        }
    }

    public class MyOtherUele
    {
        public void MyProcess() {
           // int re = RuleFactory.InvokeMethod<MyInterface, int>(o => o.GetData(10));
            MyInterface instance = RuleFactory.CreateInstance<MyInterface>();
            var t = instance.GetData(10);
        }
    }
}
