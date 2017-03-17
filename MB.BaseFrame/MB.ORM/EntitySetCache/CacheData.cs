using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using MB.Orm.Mapping;

namespace MB.Orm.EntitySetCache {
    internal abstract class CacheData {

        //protected ICacheLoader _CacheLoader; //加载缓存的加载器
        ///// <summary>
        ///// 缓存加载器
        ///// </summary>
        //public ICacheLoader CacheLoader { get { return _CacheLoader; } }

        protected EntityCfg _EntityCfg;
        /// <summary>
        /// 缓存配置信息
        /// </summary>
        public EntityCfg EntityCfg { get { return _EntityCfg; } }


    }

    /// <summary>
    /// 某一种缓存数据，以类型来区分
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class CacheData<T> : CacheData {
        private ConcurrentDictionary<string, T> _Datas;

        public CacheData() {
            _Datas = new ConcurrentDictionary<string, T>();
        }


        public CacheData(ConcurrentDictionary<string, T> datas) {
            _Datas = new ConcurrentDictionary<string, T>(datas);
        }


        /// <summary>
        /// 加载缓存数据
        /// </summary>
        /// <param name="cacheData"></param>
        public void InitCacheData(List<T> cacheData, EntityCfg entityCfg) {
            foreach (T data in cacheData) {
                string key = BuildKey(data);
                if (!_Datas.ContainsKey(key))
                    add(key, data);
            }
            _EntityCfg = entityCfg;
            
        }

        /// <summary>
        /// 将增量信息保存进已有的缓存中
        /// </summary>
        /// <param name="incrementalDatas">incrementalDatas 中的EntityState决定了缓存数据的删除，修改和新增</param>
        public void MegerIncrementalData(List<T> incrementalDatas) {
            foreach (T data in incrementalDatas) {
                string key = BuildKey(data);

                //从增量中读取增量数据的操作行为，增-删-改
                MB.Util.Model.EntityState entityState = (MB.Util.Model.EntityState)MB.Util.MyReflection.Instance.InvokePropertyForGet(data, "EntityState");

                if (this._Datas.ContainsKey(key)) {
                    if (entityState == Util.Model.EntityState.Deleted) {
                        remove(key);
                    }
                    else
                        MB.Util.MyReflection.Instance.FillModelObjectNoCreate<T>(data, this._Datas[key]);
                }
                else
                    add(key, data);
            }
        }
        

        /// <summary>
        /// 构造当前对象的主键，用来通过字典的形式存储
        /// 主键的值作为键存储
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string BuildKey(object entity) {
            Type t = entity.GetType();
            AttMappingManager mapManager = AttMappingManager.Instance;
            object[] keyalues;
            if (!mapManager.CheckExistsModelMapping(t)) {
                string key = "ID";
                object keyValue = t.GetProperty(key).GetValue(entity, null);
                keyalues = new object[1];
                keyalues[0] = keyValue;
            }
            else {
                var keys = mapManager.GetModelMappingInfo(t).PrimaryKeys;
                keyalues = new object[keys.Count];
                int i = 0;
                foreach (string fd in keys.Keys) {
                    keyalues[i] = t.GetProperty(fd).GetValue(entity, null);
                    i++;
                }
            }

            return buildKey(keyalues);
        }

        private string buildKey(object[] keys) {
            //StringBuilder s = new StringBuilder(t.FullName).Append("@");
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < keys.Length; i++) {
                s.Append(keys[i].ToString()).Append("#");
            }
            string result = s.ToString().TrimEnd('#');
            return result;
        }

        /// <summary>
        /// 得到过滤的表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        private Expression getFilterExpression(IQueryable<T> data, FilterParameter para) {

            ParameterExpression paramExp = Expression.Parameter(typeof(T), typeof(T).Name);
            Expression left = Expression.Property(paramExp, para.FilterName);
            Expression right = Expression.Constant(para.FilterValue);

            Expression filter = null;
            Expression pred = null;
            switch (para.FilterCondition) {
                case FilterCondition.Equal:
                    filter = Expression.Equal(left, right);
                    pred = Expression.Lambda(filter, paramExp);
                    break;
                case FilterCondition.Like:
                    string filterValue = para.FilterValue as string;
                    pred = getContainsExpression(para.FilterName, filterValue);
                    break;
                case FilterCondition.GreaterThan:
                    filter = Expression.GreaterThan(left, right);
                    pred = Expression.Lambda(filter, paramExp);
                    break;
                case FilterCondition.GreaterThanOrEqual:
                    filter = Expression.GreaterThanOrEqual(left, right);
                    pred = Expression.Lambda(filter, paramExp);
                    break;
                case FilterCondition.LessThan:
                    filter = Expression.LessThan(left, right);
                    pred = Expression.Lambda(filter, paramExp);
                    break;
                case FilterCondition.LessThanOrEqual:
                    filter = Expression.LessThanOrEqual(left, right);
                    pred = Expression.Lambda(filter, paramExp);
                    break;
                case FilterCondition.Different:
                    filter = Expression.NotEqual(left, right);
                    pred = Expression.Lambda(filter, paramExp);
                    break;
                default:
                    filter = Expression.Equal(left, right);
                    pred = Expression.Lambda(filter, paramExp);
                    break;

            }

            Expression expr = Expression.Call(typeof(Queryable), "Where",
                    new Type[] { typeof(T) },
                    Expression.Constant(data), pred);
            return expr;

        }

        /// <summary>
        /// 得到String.Contain的Expression用于缓存数据类型为String的过滤条件
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        private Expression<Func<T, bool>> getContainsExpression(string propertyName, string propertyValue) {
            var parameterExp = Expression.Parameter(typeof(T), "type");
            var propertyExp = Expression.Property(parameterExp, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var someValue = Expression.Constant(propertyValue, typeof(string));
            var containsMethodExp = Expression.Call(propertyExp, method, someValue);
            return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
        }

        #region ConcurrentDictionary 原子操作
        private void add(T value) {
            string key = BuildKey(value);
            add(key, value);
        }

        private void add(string key, T value) {
            Func<string, T, T> func = new Func<string, T, T>((k, v) => {
                _Datas[k] = v;
                return v;
            });
            this._Datas.AddOrUpdate(key, value, func);
        }

        private void remove(string key) {
            Type t = typeof(T);
            int i = 0;
            T removedValue;
            bool isRemoved = false;
            while (!isRemoved && i < 10) {
                //如果不能删除，则重试10次,每次休息10毫秒
                i++;
                isRemoved = this._Datas.TryRemove(key, out removedValue);
                System.Threading.Thread.Sleep(10);
            }
            if (!isRemoved) {
                MB.Util.TraceEx.Write(string.Format("未能删除建值为{0}:{1}的对象", t.FullName, key));
            }
        }
        #endregion

        #region 从缓存中获取数据的API
        /// <summary>
        /// 获取该缓存的全部实例
        /// </summary>
        /// <returns></returns>
        public List<T> GetObjects() {
            List<T> result = new List<T>();
            foreach (T value in _Datas.Values) {
                result.Add(value);
            }
            return result;
        }

        /// <summary>
        /// 根据过滤器查询缓存数据，返回过滤后的结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filters"></param>
        /// <returns></returns> 
        public List<T> GetObjectsWithFilters(params FilterParameter[] filters) {
            IQueryable<T> data = (this._Datas.Values).AsQueryable() as IQueryable<T>;

            IQueryable<T> query = null;

            if (filters != null) {
                for (int i = 1; i <= filters.Count(); i++) {
                    Expression expr = getFilterExpression(data, filters[i - 1]);

                    query = from item in data.Provider.CreateQuery<T>(expr)
                            select item;
                }
            }

            var array = query.ToArray();

            return array.ToList<T>();
        }

        /// <summary>
        /// 通过键值获取某个实体
        /// </summary>
        /// <param name="key">键值,键可以用过BuildKey来获得</param>
        /// <returns></returns>
        public T GetObjectsWithKey(string key) {
            if (this._Datas.ContainsKey(key))
                return this._Datas[key];
            else
                return default(T);
        }

        #endregion


    }
}
