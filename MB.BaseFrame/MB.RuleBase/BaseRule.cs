//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-03
// Description	:	所有业务类必须要继承的抽象基类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using MB.RuleBase.Common;
using MB.RuleBase.Atts;
using MB.RuleBase.IFace;
using MB.Util.Model;
namespace MB.RuleBase
{
    /// <summary>
    /// 所有业务类必须要继承的抽象基类。
    /// 该类数据库操作获取的是默认配置的数据库连接字符窜，
    /// 如果需要调用不同的数据库，需要覆盖相应的方法并调用DatabaseConfigurationScope。
    /// </summary>
    public abstract class BaseRule : BaseQueryRule, MB.RuleBase.IFace.IBaseRule
    {
        private MB.Orm.Mapping.QueryParameterMappings _QueryParamMapping;
        //存储在当前线程中的编辑数据。
        //如果考虑到断线非连接的原因，可以考虑把通过 CacheProxy 把数据缓存起来。
        private ObjectDataList _ObjectDataList;

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="objectDataDocType">单据数据定义的数据类型</param>
        public BaseRule(Type objectDataDocType)
            : base(objectDataDocType) {

        }

        #region IBaseRule 成员 实现...

        #region 对象编辑操作方法...
        /// <summary>
        /// 获取指定类型新创建对象的自增列ID。
        /// </summary>
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的数据。</param>
        /// <param name="count">获取ID 的个数</param>
        /// <returns>返回第一个对象的ID,其它的加1 就可以，但最大值不能超过返回值 + count -1</returns>
        public virtual int GetCreateNewEntityIds(int dataInDocType, int count) {
            ObjectDataMappingAttribute mappingAtt = AttributeConfigHelper.Instance.GetObjectDataMappingAttribute(ConvertDataInDocType(dataInDocType));
            if (mappingAtt == null)
                throw new MB.RuleBase.Exceptions.RequireConfigDataMappingException(ConvertDataInDocType(dataInDocType));

            //不是自增加列 不需要返回自增列的ID
            if (!mappingAtt.KeyIsSelfAdd)
                return 0;

            return MB.Orm.Persistence.EntityIdentityHelper.NewInstance.GetEntityIdentity(mappingAtt.MappingTableName, count);
        }
        /// <summary>
        /// 根据指定的类型批量创建实体对象。
        /// </summary>
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的数据。</param>
        /// <param name="createCount">新需要创建的实体对象个数</param>
        /// <returns></returns>
        public virtual IList CreateNewEntityBatch(int dataInDocType, int createCount) {
            return new ObjectEditHelper().CreateNewEntityBatch(this, ConvertDataInDocType(dataInDocType), createCount);
        }

        /// <summary>
        /// 根据指定的数据类型创建一个新的实体对象。
        /// </summary>
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的数据。</param>
        /// <returns>实体对象</returns>
        public virtual object CreateNewEntity(int dataInDocType) {
            return new ObjectEditHelper().CreateNewEntity(this, ConvertDataInDocType(dataInDocType));
        }
        /// <summary>
        /// 重新刷新数据实体对象。
        /// </summary>
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的数据。</param>
        /// <param name="entity">需要刷新的实体对象</param>
        /// <returns>刷新后的实体对象</returns>
        public virtual object RefreshEntity(int dataInDocType, object entity) {
            return new ObjectEditHelper().RefreshEntity(this, ConvertDataInDocType(dataInDocType), entity);
        }

        /// <summary>
        /// 执行业务对象保存的永久化操作。
        /// </summary>
        /// <returns>返回收影响的实体个数，-1 表示不成功</returns>
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public virtual int Flush() {
            if (_ObjectDataList == null)
                return 0;

            int count = new ObjectEditHelper().SaveObjectDataList(this, _ObjectDataList);
            if (count > 0)
                _ObjectDataList.Clear(); //清空连接线程存储的编辑数据 （）

            return count;
        }
        /// <summary>
        /// 增加数据到Cache 中。
        /// </summary>
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的数据。</param>
        /// <param name="entity">需要增加的实体。</param>
        /// <param name="propertys">需要增加的该实体的指定属性。</param>
        /// <returns>1 表示成功，-1 表示不成功</returns>
        [MB.Aop.InjectionMethodSwitch(false)]
        public virtual int AddToCache(int dataInDocType, object entity, bool isDelete, string[] propertys) {
            if (_ObjectDataList == null)
                _ObjectDataList = new ObjectDataList();
            ObjectDataInfo dataInfo = new ObjectDataInfo(ConvertDataInDocType(dataInDocType), entity);

            dataInfo.SavePropertys = propertys;
            //dataInfo.DataState = ObjectDataState.Added;
            if (isDelete) {
                dataInfo.DataState = ObjectDataState.Deleted;

                if (entity.GetType().IsSubclassOf(typeof(MB.Orm.Common.BaseModel))) {
                    (entity as MB.Orm.Common.BaseModel).EntityState = EntityState.Deleted;
                }
            }

            _ObjectDataList.Add(dataInfo);
            return 1;
        }
        /// <summary>
        /// 直接删除数据。
        /// </summary>
        /// <param name="dataInDocType">数据在业务类中的数据类型。</param>
        /// <param name="key">需要进行删除的键值。</param>
        /// <returns>返回受影响的行。</returns>
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public virtual int DeletedImmediate(int dataInDocType, object key) {
            return new ObjectEditHelper().DeletedImmediate(this, ConvertDataInDocType(dataInDocType), key);
        }
        /// <summary>
        /// 对象数据提交或者撤消提交。
        /// </summary>
        /// <param name="entity">需要提交的实体对象</param>
        /// <param name="cancelSubmit">判断是否为取消提交</param>
        /// <returns>-1 表示不成功</returns>
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public virtual int Submit(object entity, bool cancelSubmit) {
            if (cancelSubmit) {
                MB.RuleBase.Common.ObjectSubmitHelper.NewInstance.ObjectCancelSubmit(this, entity);
            }
            else {
                MB.RuleBase.Common.ObjectSubmitHelper.NewInstance.ObjectSubmit(this, entity);
            }
            return 1;
        }

        /// <summary>
        /// 直接存储DataSet 数据类型。
        /// </summary>
        /// <param name="dsData">DataSet 类型数据。</param>
        /// <param name="dataInDocType">数据在业务类中的类型。</param>
        /// <returns>返回受影响的行。</returns>
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public virtual int SaveDataSetImmediate(System.Data.DataSet dsData, int dataInDocType) {
            return new ObjectEditHelper().SaveDataSetImmediate(this, ConvertDataInDocType(dataInDocType), dsData);
        }
        #endregion 对象编辑操作方法...

        /// <summary>
        /// 通过对象的主键获取对象下指定类型的关联子对象数据。
        /// 如果存在大数据 不能在客户端直接调用,需要通过大数据调用载体来进行。
        /// (未来可以通过配置的方式来解决)
        /// </summary>
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的键值。</param>
        /// <param name="mainKey">主表键值。</param>
        /// <returns></returns>
        public virtual object GetObjectByKey(int dataInDocType, object keyValue) {
            return new ObjectEditHelper().GetObjectByKey<System.Object>(this, ConvertDataInDocType(dataInDocType), keyValue);
        }

        #endregion IBaseRule 成员 实现...

        #region IBaseRule 成员
        /// <summary>
        /// 根据数据类型检查指定的值在数据库中是否已经存在
        /// </summary>
        /// <param name="dataInDocType">需要进行检查的数据类型</param>
        /// <param name="entity">需要检查的实体对象</param>
        /// <param name="checkPropertys">需要检查的属性名称</param>
        /// <returns>true 表示存在,false 表示不存在 </returns>
        public virtual bool CheckValueIsExists(int dataInDocType, object entity, string[] checkPropertys) {
            return new ObjectDataValidatedHelper().CheckValueIsExists(ConvertDataInDocType(dataInDocType), entity, checkPropertys);
        }

        #endregion

        /// <summary>
        /// 当前准备进行处理的待处理集合。
        /// </summary>
        protected ObjectDataList CurrentDataList {
            get {
                return _ObjectDataList;
            }
        }
        /// <summary>
        /// 批量刷新待存储的实体对象的主键，如果是主表的话要注意子表是否有关联。
        /// 特殊说明：不建议刷新主表的主键。
        /// 主键名称只支持名称为 ID;     if (entity.GetType().IsSubclassOf(typeof(MB.Orm.Common.BaseModel))) {
        /// </summary>
        /// <param name="dataInDocType">需要刷新主键的实体类型。</param>
        protected virtual void BatchSettingNewEntityID(int dataInDocType) {
            var entitys = from item in _ObjectDataList.Values
                          where (item.ObjectData as MB.Orm.Common.BaseModel) != null &&
                                ((MB.Orm.Common.BaseModel)item.ObjectData).EntityState == EntityState.New &&
                                (int)item.DataInDocType == dataInDocType
                          select item.ObjectData;
            int count = entitys.Count();
            if (count > 0) {
                int ids = this.GetCreateNewEntityIds(dataInDocType, count);
                MB.Util.Emit.DynamicPropertyAccessor ac = new Util.Emit.DynamicPropertyAccessor(entitys.First().GetType(), "ID");

                foreach (var entity in entitys) {
                    ac.Set(entity, ids++);
                }
            }
        }
    }
}
