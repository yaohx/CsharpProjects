using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Util.Expressions;
using System.Linq.Expressions;

namespace MB.Orm.Common
{
    /// <summary>
    /// 默认的数据存储容器
    /// </summary>
    public class DefaltDataRelationContainer : DataRelationContainer<MB.Orm.Common.BaseModel>
    {
        public DefaltDataRelationContainer(MB.Orm.Common.BaseModel rootEntity)
            : base(rootEntity) {

        }
    }

    /// <summary>
    /// 关系数据容器。
    /// </summary>
    public class DataRelationContainer<TBaseModel> : IDataRelationContainer<TBaseModel>
    {
        private static Dictionary<Type, EntityRelationInfo[]> RELATION_DATA_TYPE = new Dictionary<Type, EntityRelationInfo[]>();
        private static Dictionary<Type, Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor>> _DynamicAccess =
                                        new Dictionary<Type, Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor>>();
        private Dictionary<TBaseModel, Dictionary<Type, List<TBaseModel>>> _Childs;
        //默认的键值名称
        private static readonly string DEFAULT_KEY_NAME = "ID";
        /// <summary>
        /// 实例化一个容器对象。
        /// </summary>
        public DataRelationContainer(TBaseModel rootEntity) {
            _Childs = new Dictionary<TBaseModel, Dictionary<Type, List<TBaseModel>>>();

            RootEntity = rootEntity;
        }
        /// <summary>
        /// 获取根对象.
        /// </summary>
        public TBaseModel RootEntity { get; private set; }

        /// <summary>
        /// 增加关联子项到容器中。
        /// </summary>
        /// <param name="val"></param>
        public void AddItem(DataRelationValue<TBaseModel> val) {
            //验证是否允许增加
            checkAllowAdd(val);

            Dictionary<Type, List<TBaseModel>> childsData = null;
            if (_Childs.ContainsKey(val.Parent)) {
                childsData = _Childs[val.Parent];
            } else {
                childsData = new Dictionary<Type, List<TBaseModel>>();
                _Childs[val.Parent] = childsData;
            }
            Type childType = val.Child.GetType();
            List<TBaseModel> datas = null;
            if (childsData.ContainsKey(childType)) {
                datas = childsData[childType];
            } else {
                datas = new List<TBaseModel>();
                childsData.Add(childType, datas);
            }
            datas.Add(val.Child);
        }
        /// <summary>
        /// 在关系容器中获取指定的子对象。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TDetail"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public TDetail[] GetChilds<T, TDetail>(T parent)
            where T : TBaseModel
            where TDetail : TBaseModel {
            if (!_Childs.ContainsKey(parent)) return new TDetail[] { };

            var allTypeChild = _Childs[parent];
            if (!allTypeChild.ContainsKey(typeof(TDetail))) return new TDetail[] { };

            return allTypeChild[typeof(TDetail)].ConvertAll(new Converter<TBaseModel, TDetail>((o) => { return (TDetail)o; })).ToArray();
        }
        /// <summary>
        /// 获取指定类型的所有对象。
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public TDetail[] GetAllChilds<TDetail>() where TDetail : TBaseModel {
            List<TDetail> lstData = new List<TDetail>();
            var dataType = typeof(TDetail);
            foreach (var d in _Childs.Values) {
                if (!d.ContainsKey(dataType)) continue;

                d[dataType].ForEach(o => lstData.Add((TDetail)o));
            }
            return lstData.ToArray();
        }
        /// <summary>
        /// 获取指定类型所有子对象的数量。
        /// </summary>
        /// <typeparam name="TDetail"></typeparam>
        /// <returns></returns>
        public int GetAllChildsCount<TDetail>() where TDetail : TBaseModel {
            int count = 0;
            var dataType = typeof(TDetail);
            foreach (var d in _Childs.Values) {
                if (!d.ContainsKey(dataType)) continue;

                count += d[dataType].Count;
            }
            return count;
        }

        /// <summary>
        /// 设置指定类型所有实体对象的键值。
        /// </summary>
        /// <typeparam name="TDetail"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public void SetAllChildsID<TDetail>(int id) where TDetail : TBaseModel {
            var dataType = typeof(TDetail);
            var keyname = _DynamicAccess[dataType][DEFAULT_KEY_NAME];

            foreach (var d in _Childs.Values) {
                if (!d.ContainsKey(dataType)) continue;

                d[dataType].ForEach(o => keyname.Set(o, id++));
            }
        }
        /// <summary>
        /// 重新设置所有实体对象的外键值。
        /// </summary>
        public void ResetForeingKeyValue() {
            resetChildForingKeyValue(RootEntity);
        }
        //设置所有外键的值。
        private void resetChildForingKeyValue(TBaseModel entity) {
            Type entityType = entity.GetType();
            int key = (int)_DynamicAccess[entityType][DEFAULT_KEY_NAME].Get(entity);
            if (!_Childs.ContainsKey(entity)) return;

            foreach (var childType in _Childs[entity].Keys) {
                var relation = RELATION_DATA_TYPE[entityType].FirstOrDefault(o => o.EntityType == childType);
                var ac = _DynamicAccess[childType][relation.RelationMap.ForeingKeyName];
                foreach (var child in _Childs[entity][childType]) {
                    ac.Set(child, key);

                    resetChildForingKeyValue(child);
                }
            }
        }
        /// <summary>
        /// 登记主从明细表的关系信息。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TDetail"></typeparam>
        /// <param name="foreingKeyNameExpress"></param>
        public static void RegisterRelation<T, TDetail>(Expression<Func<TDetail, object>> foreingKeyNameExpress)
            where T : TBaseModel
            where TDetail : TBaseModel {
            string fname = ExpressionHelper.GetPropertyName(foreingKeyNameExpress);

            RegisterRelation<T, TDetail>(new EntityRelationMapInfo(fname, DEFAULT_KEY_NAME));
        }
        /// <summary>
        /// 登记主从明细表的关系信息。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TDetail"></typeparam>
        /// <param name="foreingKeyName"></param>
        public static void RegisterRelation<T, TDetail>(string foreingKeyName)
            where T : TBaseModel
            where TDetail : TBaseModel {
            RegisterRelation<T, TDetail>(new EntityRelationMapInfo(foreingKeyName, DEFAULT_KEY_NAME));
        }
        /// <summary>
        /// 登记对象之间的关系。
        /// </summary>
        /// <typeparam name="T">父对象</typeparam>
        /// <typeparam name="TDetail">子对象</typeparam>
        private static void RegisterRelation<T, TDetail>(EntityRelationMapInfo relation)
            where T : TBaseModel
            where TDetail : TBaseModel {
            Type parentType = typeof(T);
            Type childType = typeof(TDetail);
            if (!RELATION_DATA_TYPE.ContainsKey(parentType))
                RELATION_DATA_TYPE[parentType] = new EntityRelationInfo[] { new EntityRelationInfo(childType, relation) };
            else {
                List<EntityRelationInfo> childsType = new List<EntityRelationInfo>(RELATION_DATA_TYPE[parentType]);
                childsType.Add(new EntityRelationInfo(childType, relation));
                RELATION_DATA_TYPE[parentType] = childsType.ToArray();
            }
            iniTypeDynamicAccess(parentType, childType, relation);
        }

        #region  内部处理函数...
        //创建类型的快速访问。
        private static void iniTypeDynamicAccess(Type parentType, Type childType, EntityRelationMapInfo relation) {
            iniDataTypeProAccess(parentType, DEFAULT_KEY_NAME);
            iniDataTypeProAccess(childType, DEFAULT_KEY_NAME);

            if (relation != null) {
                iniDataTypeProAccess(parentType, relation.KeyName);
                iniDataTypeProAccess(childType, relation.ForeingKeyName);
            }

        }
        private static void iniDataTypeProAccess(Type dataType, string proName) {
            Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> existsFields = null;
            if (_DynamicAccess.ContainsKey(dataType)) {
                existsFields = _DynamicAccess[dataType];
            } else {
                existsFields = new Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor>();
                _DynamicAccess[dataType] = existsFields;
            }
            if (!existsFields.ContainsKey(proName)) {
                var pro = dataType.GetProperty(proName);
                if (pro == null)
                    throw new MB.Util.APPException(string.Format("实体类型{0} 中不包含键名称 {1}", dataType.FullName, proName), MB.Util.APPMessageType.SysErrInfo);
                MB.Util.Emit.DynamicPropertyAccessor propertyAccess = new MB.Util.Emit.DynamicPropertyAccessor(dataType, pro);
                existsFields[pro.Name] = propertyAccess;
            }
        }
        //检查是否允许增加
        private int checkAllowAdd(DataRelationValue<TBaseModel> val) {
            if (val.Parent == null)
                throw new MB.Util.APPException("父对象输入不能为空");
            if (val.Child == null)
                throw new MB.Util.APPException("添加的子对象不能为空");

            var parentType = val.Parent.GetType();
            if (!RELATION_DATA_TYPE.ContainsKey(parentType))
                throw new MB.Util.APPException(string.Format("父对象类型 {0} 还没有进行注册", parentType.FullName));
            var childs = RELATION_DATA_TYPE[parentType];

            var childType = val.Child.GetType();
            var map = childs.FirstOrDefault(o => o.EntityType == childType);
            if (map == null)
                throw new MB.Util.APPException(string.Format("父对象类型{0} 下的子类型{1} 还没有进行注册", parentType.FullName, childType.FullName));

            return 1;
        }
        #endregion
    }
}
