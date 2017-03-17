using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util {
    /// <summary>
    /// 扩展的哈希存储表。
    ///  继承Dictionary 是因为 Dictionary 类是作为一个哈希表来实现的 所以它的查询速度还是非常快的。
    /// </summary>
    public class MyHashtable<TKey,TValue> : Dictionary<TKey,TValue>{
        /// <summary>
        /// 扩展的哈希存储表
        /// </summary>
        public MyHashtable(){
             
        }
        public MyHashtable(IEqualityComparer<TKey> comparer) : base(comparer) {
        }
        /// <summary>
        /// 扩展的哈希存储表
        /// </summary>
        /// <param name="dictionary">包含的初始化元素</param>
        /// <param name="comparer">比较键值时需要</param>
        public MyHashtable(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
            : base(dictionary, comparer) {
        }

        
    }

   
}
