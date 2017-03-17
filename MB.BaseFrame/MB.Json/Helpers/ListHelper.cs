using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace MB.Json.Helpers
{
    /// <summary>
    /// 集合处理
    /// </summary>
    internal static class ListHelper
    {
        private static readonly Type _IReadonlyGenericType = typeof(ReadOnlyCollection<>);
        /// <summary>
        /// 获取集合项的类型.
        /// </summary>
        /// <param name="enumerableType"></param>
        /// <returns></returns>
        public static Type GetListItemType(Type enumerableType)
        {
            if (enumerableType.IsArray)
            {
                return enumerableType.GetElementType();
            }
            if (enumerableType.IsGenericType)
            {
                return enumerableType.GetGenericArguments()[0];
            }
            return typeof(object);
        }
        /// <summary>
        /// 集合转换为数组.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public static Array ToArray(List<object> container, Type itemType)
        {
            Array array = Array.CreateInstance(itemType, container.Count);
            Array.Copy(container.ToArray(), 0, array, 0, container.Count);
            return array;
        }
        /// <summary>
        /// 创建集合类型的容器.
        /// </summary>
        /// <param name="listType"></param>
        /// <param name="listItemType"></param>
        /// <param name="isReadOnly"></param>
        /// <returns></returns>
        public static IList CreateContainer(Type listType, Type listItemType, out bool isReadOnly)
        {
            isReadOnly = false;
            if (listType.IsArray)
            {
                return new List<object>();
            }
            if (listType.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[0], null) != null)
            {
                return (IList)Activator.CreateInstance(listType);
            }
            if (_IReadonlyGenericType.IsAssignableFrom(listType.GetGenericTypeDefinition()))
            {
                isReadOnly = true;
                return (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(listItemType));
            }
            return new List<object>();
        }
    }
}
