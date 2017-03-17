using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace MB.Util
{
     /// <summary>
    /// 提供组件之间松耦合互相调用的方法，不要项目相互引用
    /// </summary>
    public static class DllContainer
    {
        private static Dictionary<NamedTypeBuildKey, string> _InstanceCfgs;
        private static object _LockObj = new object();
        static DllContainer()
        {
            lock (_LockObj)
            {
                if (_InstanceCfgs == null)
                {
                    _InstanceCfgs = new Dictionary<NamedTypeBuildKey, string>();
                }
            }
        }

        /// <summary>
        /// 注册业务对象
        /// </summary>
        /// <param name="interfaceType">父类或者接口类型</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="typeFullName">类型名称</param>
        /// <param name="aliasName">类型别名</param>
        public static void Registry(Type interfaceType, string assemblyName, string typeFullName, string aliasName)
        {
            var key = new NamedTypeBuildKey(interfaceType, aliasName);

            if (!_InstanceCfgs.ContainsKey(key))
            {
                _InstanceCfgs.Add(key, string.Format("{0}, {1}", assemblyName, typeFullName));
            }
            else
            {
                _InstanceCfgs[key] = string.Format("{0}, {1}", assemblyName, typeFullName);
            }
        }

        /// <summary>
        /// 注册业务对象
        /// </summary>
        /// <param name="interfaceType">父类或者接口类型</param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="typeFullName">类型名称</param>
        public static void Registry(Type interfaceType, string assemblyName, string typeFullName)
        {
            Registry(interfaceType, assemblyName, typeFullName, "Default");
        }

        /// <summary>
        /// 泛型注册业务对象方法
        /// </summary>
        /// <typeparam name="T">父类或者接口类型</typeparam>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="typeFullName">类型名称</param>
        public static void Registry<T>(string assemblyName, string typeFullName)
        {
            Registry(typeof(T), assemblyName, typeFullName, "Default");
        }

        /// <summary>
        /// 泛型注册业务对象方法
        /// </summary>
        /// <typeparam name="T">父类或者接口类型</typeparam>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="typeFullName">类型名称</param>
        /// <param name="aliasName">类型别名</param>
        public static void Registry<T>(string assemblyName, string typeFullName, string aliasName)
        {
            Registry(typeof(T), assemblyName, typeFullName, aliasName);
        }
         
        /// <summary>
        /// 创建调用对象
        /// </summary>
        /// <typeparam name="T">服务业务父类或者接口类型</typeparam>
        /// <param name="typeAliasName">类型名称</param>
        /// <returns>服务业务父类或者接口类型</returns>
        public static T CreateInstance<T>(string typeAliasName)
        {
            var key = new NamedTypeBuildKey(typeof(T), typeAliasName);

            if (!_InstanceCfgs.ContainsKey(key))
                throw new APPException(string.Format("接口{0} 还没有在{1}中进行注册",
                    typeof(T).FullName, "MB.Util.DllContainer"), APPMessageType.SysErrInfo);
            string[] dlls = _InstanceCfgs[key].Split(',');

            return (T)DllFactory.Instance.LoadObject(dlls[1],dlls[0]);
        }

        /// <summary>
        /// 创建调用对象
        /// </summary>
        /// <typeparam name="T">服务业务父类或者接口类型</typeparam>
        /// <returns>服务业务父类或者接口类型</returns>
        public static T CreateInstance<T>()
        {
            return CreateInstance<T>("Default");
        }
    }


    /// <summary>
    /// Build key used to combine a type object with a string name. Used by
    /// ObjectBuilder to indicate exactly what is being built.
    /// </summary>
    public class NamedTypeBuildKey
    {
        private readonly Type type;
        private readonly string name;

        /// <summary>
        /// Create a new <see cref="NamedTypeBuildKey"/> instance with the given
        /// type and name.
        /// </summary>
        /// <param name="type"><see cref="Type"/> to build.</param>
        /// <param name="name">Key to use to look up type mappings and singletons.</param>
        public NamedTypeBuildKey(Type type, string name)
        {
            this.type = type;
            this.name = !string.IsNullOrEmpty(name) ? name : null;
        }

        /// <summary>
        /// Create a new <see cref="NamedTypeBuildKey"/> instance for the default
        /// buildup of the given type.
        /// </summary>
        /// <param name="type"><see cref="Type"/> to build.</param>
        public NamedTypeBuildKey(Type type)
            : this(type, null)
        {

        }

        /// <summary>
        /// This helper method creates a new <see cref="NamedTypeBuildKey"/> instance. It is
        /// initialized for the default key for the given type.
        /// </summary>
        /// <typeparam name="T">Type to build.</typeparam>
        /// <returns>A new <see cref="NamedTypeBuildKey"/> instance.</returns>
        public static NamedTypeBuildKey Make<T>()
        {
            return new NamedTypeBuildKey(typeof(T));
        }

        /// <summary>
        /// This helper method creates a new <see cref="NamedTypeBuildKey"/> instance for
        /// the given type and key.
        /// </summary>
        /// <typeparam name="T">Type to build</typeparam>
        /// <param name="name">Key to use to look up type mappings and singletons.</param>
        /// <returns>A new <see cref="NamedTypeBuildKey"/> instance initialized with the given type and name.</returns>
        public static NamedTypeBuildKey Make<T>(string name)
        {
            return new NamedTypeBuildKey(typeof(T), name);
        }

        /// <summary>
        /// Return the <see cref="Type"/> stored in this build key.
        /// </summary>
        /// <value>The type to build.</value>
        public Type Type
        {
            get { return type; }
        }

        /// <summary>
        /// Returns the name stored in this build key.
        /// </summary>
        /// <remarks>The name to use when building.</remarks>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Compare two <see cref="NamedTypeBuildKey"/> instances.
        /// </summary>
        /// <remarks>Two <see cref="NamedTypeBuildKey"/> instances compare equal
        /// if they contain the same name and the same type. Also, comparing
        /// against a different type will also return false.</remarks>
        /// <param name="obj">Object to compare to.</param>
        /// <returns>True if the two keys are equal, false if not.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as NamedTypeBuildKey;
            if (other == null)
            {
                return false;
            }
            return this == other;
        }

        /// <summary>
        /// Calculate a hash code for this instance.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            int typeHash = type == null ? 0 : type.GetHashCode();
            int nameHash = name == null ? 0 : name.GetHashCode();
            return (typeHash + 37) ^ (nameHash + 17);
        }

        /// <summary>
        /// Compare two <see cref="NamedTypeBuildKey"/> instances for equality.
        /// </summary>
        /// <remarks>Two <see cref="NamedTypeBuildKey"/> instances compare equal
        /// if they contain the same name and the same type.</remarks>
        /// <param name="left">First of the two keys to compare.</param>
        /// <param name="right">Second of the two keys to compare.</param>
        /// <returns>True if the values of the keys are the same, else false.</returns>
        public static bool operator ==(NamedTypeBuildKey left, NamedTypeBuildKey right)
        {
            var leftIsNull = ReferenceEquals(left, null);
            var rightIsNull = ReferenceEquals(right, null);
            if (leftIsNull && rightIsNull)
            {
                return true;
            }
            if (leftIsNull || rightIsNull)
            {
                return false;
            }

            return left.type == right.type &&
                   string.Compare(left.name, right.name, StringComparison.Ordinal) == 0;

        }

        /// <summary>
        /// Compare two <see cref="NamedTypeBuildKey"/> instances for inequality.
        /// </summary>
        /// <remarks>Two <see cref="NamedTypeBuildKey"/> instances compare equal
        /// if they contain the same name and the same type. If either field differs
        /// the keys are not equal.</remarks>
        /// <param name="left">First of the two keys to compare.</param>
        /// <param name="right">Second of the two keys to compare.</param>
        /// <returns>false if the values of the keys are the same, else true.</returns>
        public static bool operator !=(NamedTypeBuildKey left, NamedTypeBuildKey right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Formats the build key as a string (primarily for debugging).
        /// </summary>
        /// <returns>A readable string representation of the build key.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Build Key[{0}, {1}]", type, name ?? "null");
        }
    }
}
