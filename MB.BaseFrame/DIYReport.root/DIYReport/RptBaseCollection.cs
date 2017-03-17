//---------------------------------------------------------------- 
// Copyright (C) 2004-2004 nick chen (nickchen77@gmail.com) 
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2004-12-15
// Description	:	 
// 备注描述：  
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections ;

namespace DIYReport{
	/// <summary>
	/// Up2BaseCollection通过继承ArrayList创建一个共享的基类.
	/// </summary>
	[Serializable]
	public abstract class Hashlist : IDictionary, IEnumerable {
		/// <summary>
		/// 存储键值的数组
		/// </summary>
		protected ArrayList mKeys = new ArrayList();
		/// <summary>
		///  存储值的HashTable 
		/// </summary>
		protected Hashtable mValues = new Hashtable();		

		#region ICollection implementation
		//ICollection implementation
		/// <summary>
		/// 对象的总数
		/// </summary>
		public int Count {
			get{return mValues.Count;}
		}
		/// <summary>
		/// 是否同步对集合的访问
		/// </summary>
		public bool IsSynchronized {
			get{return mValues.IsSynchronized;}
		}
		/// <summary>
		/// 获取可同步对HashTable的访问对象
		/// </summary>
		public object SyncRoot {
			get{return mValues.SyncRoot;}
		}
		/// <summary>
		/// 把hashTable中的对象复制一维数组中指定的位置
		/// </summary>
		/// <param name="oArray"></param>
		/// <param name="iArrayIndex"></param>
		public void CopyTo(System.Array oArray, int iArrayIndex) {
			mValues.CopyTo(oArray, iArrayIndex);
		}
		#endregion

		#region IDictionary implementation

		/// <summary>
		///  在指定的位置插入对象
		/// </summary>
		/// <param name="pIndex"></param>
		/// <param name="pKey"></param>
		/// <param name="pValue"></param>
		public void Insert(int pIndex ,object pKey , object pValue){
			mKeys.Insert(pIndex,pKey);  
			mValues.Add(pKey,pValue);
		}
		//IDictionary implementation
		/// <summary>
		///  增加对象到集合中
		/// </summary>
		/// <param name="oKey"></param>
		/// <param name="oValue"></param>
		public void Add(object oKey, object oValue) {
			mKeys.Add(oKey);
			mValues.Add(oKey, oValue);
		}
		/// <summary>
		/// 设置键值的固定大小
		/// </summary>
		public bool IsFixedSize {
			get{return mKeys.IsFixedSize;}
		}
		/// <summary>
		/// 得到一个值，该值指示 ArrayList是否为只读
		/// </summary>
		public bool IsReadOnly {
			get{return mKeys.IsReadOnly;}
		}
		/// <summary>
		/// 得到集合中的所有键值
		/// </summary>
		public ICollection Keys {
			get{return mValues.Keys;}
		}
		/// <summary>
		/// 清除集合中的所有值以及键
		/// </summary>
		public void Clear() {
			mValues.Clear();
			mKeys.Clear();
		}
		/// <summary>
		/// 判断指定的键是否在集合中
		/// </summary>
		/// <param name="oKey"></param>
		/// <returns></returns>
		public bool Contains(object oKey) {
			return mValues.Contains(oKey);
		}
		/// <summary>
		/// 判断指定的键是否在集合中
		/// </summary>
		/// <param name="oKey"></param>
		/// <returns></returns>
		public bool ContainsKey(object oKey) {
			return mValues.ContainsKey(oKey);
		}
		/// <summary>
		/// 返回可循环访问的接口
		/// </summary>
		/// <returns></returns>
		public IDictionaryEnumerator GetEnumerator() {
			return mValues.GetEnumerator();
		}	
		/// <summary>
		/// 移除带有指定键的元素
		/// </summary>
		/// <param name="oKey"></param>
		public  void Remove(object oKey) {
			mValues.Remove(oKey);
			mKeys.Remove(oKey);
		}
		/// <summary>
		/// 根据键得到指定的元素
		/// </summary>
		public object this[object oKey] {
			get{return mValues[oKey];}
			set{mValues[oKey] = value;}
		}
		/// <summary>
		/// 得到所有值的集合
		/// </summary>
		public ICollection Values {
			get{return mValues.Values;}
		}
		#endregion

		#region IEnumerable implementation
		IEnumerator IEnumerable.GetEnumerator() {
			return mValues.GetEnumerator();
		}
		#endregion
		
		#region Hashlist specialized implementation
		//specialized indexer routines
		/// <summary>
		/// 根据建得到指定的元素
		/// </summary>
		public object this[string Key] {
			get{return mValues[Key];}
		}
		/// <summary>
		///  根据Index得到指定的元素
		/// </summary>
		public object this[int Index] {
			get{return mValues[mKeys[Index]];}
		}
		#endregion
	
	}
}
