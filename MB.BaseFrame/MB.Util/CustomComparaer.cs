using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using MB.Util.Emit;

namespace MB.Util {
    /// <summary>
    /// 自定义的比较对象，用于泛型中需要比较对象的函数
    /// 比如Union
    /// 该类可以被继承
    /// </summary>
    /// <typeparam name="T">传入的类型，在传入的类型中，作为比较键的属性需要附上[DataMemeber]</typeparam>
    public class CustomComparaer<T> : IEqualityComparer<T> {
        Dictionary<string, DynamicPropertyAccessor> _DynamicAccs; //动态属性访问器
        string[] _PropNames;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propNames">需要用作比较键的属性名集合</param>
        public CustomComparaer(string[] propNames) {
            if (propNames == null || propNames.Length <= 0)
                throw new ArgumentException("propNames不能为空或没有值");

            _PropNames = propNames;

            PropertyInfo[] infos = typeof(T).GetProperties();
            _DynamicAccs = new Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor>();
            foreach (PropertyInfo info in infos) {
                object[] atts = info.GetCustomAttributes(typeof(DataMemberAttribute), true);
                if (atts == null || atts.Length == 0 || !_PropNames.Contains(info.Name)) continue;


                _DynamicAccs.Add(info.Name, new MB.Util.Emit.DynamicPropertyAccessor(typeof(T), info));
            }

            
        }

        /// <summary>
        ///  Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">对象1</param>
        /// <param name="y">对象2</param>
        /// <returns></returns>
        public virtual bool Equals(T x, T y) {

            if (Object.ReferenceEquals(x, y)) return true;
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            foreach (string proName in _PropNames) {
                object a = _DynamicAccs[proName].Get(x);
                object b = _DynamicAccs[proName].Get(y);

                if (Object.ReferenceEquals(a, null) || Object.ReferenceEquals(b, null))
                    return false;
                if (a.ToString().CompareTo(b.ToString()) != 0)
                    return false;

            }
            return true;
        }


        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="t">The T for which a hash code is to be returned.</param>
        /// <returns></returns>
        public virtual int GetHashCode(T t) {
            if (Object.ReferenceEquals(t, null)) return 0;

            int hashId = 0;

            foreach (string proName in _PropNames) {
                object a = _DynamicAccs[proName].Get(t);
                int tempHashId = (a == null ? 0 : a.GetHashCode());
                hashId = hashId ^ tempHashId;
            }

            return hashId;
        }
    }
}
