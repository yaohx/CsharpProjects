///<summary>--------------------------------------------------- 
/// Copyright (C) 2008-2009 www.metersbonwe.com
/// All rights reerved. 
/// 
/// Author		:	Nick
/// Create date	:	2009-01-05
/// Description	:	MyDataCache 采取先进先出的方式存储数据并且限制数据存储的大小.
/// Modify date	:			By:					Why: 
///</summary>-----------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace MB.Util {
    /// <summary>
    /// MyDataCache 采取先进先出的方式存储数据并且限制数据存储的大小.
    /// 备注：该类不适合在多线程下使用。
    /// </summary>
    public class MyDataCache<TKey,TValue> {
        #region 变量定义...
        private Dictionary<TKey, TValue> _DataObj;
        private Queue<TKey> _ObjKeys;

        private int _MaxCapacity;
        private int _RemoveCountOnFull;
        #endregion 变量定义...

        #region 构造函数...
        /// <summary>
        ///  构造函数. ,默认情况下存储的最大值是100 个,每次移除的个数是20 个。
        /// </summary>
        public MyDataCache()
            : this(20, 100) {
        }
        /// <summary>
        ///  构造函数.
        /// </summary>
        /// <param name="remove"></param>
        /// <param name="maxCapacity"></param>
        public MyDataCache(int remove, int maxCapacity) {
            _MaxCapacity = maxCapacity;
            _RemoveCountOnFull = remove;
            _DataObj = new Dictionary<TKey, TValue>();
            _ObjKeys = new Queue<TKey>();
        }
        #endregion 构造函数...

        #region this...
        /// <summary>
        ///  this 操作方法....
        /// </summary>
        public TValue this[TKey keyName] {
            get {
                if (_DataObj.ContainsKey(keyName)) {
                    return _DataObj[keyName];
                }
                return default(TValue);
            }
        }
        #endregion this...

        #region Public 方法...
        /// <summary>
        /// 判断键是否存在。
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey keyName) {
            return _DataObj.ContainsKey(keyName);
        }
        /// <summary>
        ///  往数据存储桶中增加一个数据。
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="data"></param>
        public void Add(TKey keyName, TValue data) {
            if (_DataObj.Count > _MaxCapacity - 1) {
                int count = _RemoveCountOnFull < _ObjKeys.Count ? _RemoveCountOnFull : _ObjKeys.Count;
                for (int i = 0; i < count; i++) {
                    TKey key = _ObjKeys.Dequeue();
                    _DataObj.Remove(key);
                }
            }
            _ObjKeys.Enqueue(keyName);
            _DataObj[keyName] = data;
        }
        #endregion Public 方法...

        #region Public 属性...
        /// <summary>
        /// 
        /// </summary>
        public int MaxCapacity {
            get {
                return _MaxCapacity;
            }
            set {
                _MaxCapacity = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int RemoveCountOnFull {
            get {
                return _RemoveCountOnFull;
            }
            set {
                _RemoveCountOnFull = value;
            }
        }
        #endregion Public 属性...
    }
}
