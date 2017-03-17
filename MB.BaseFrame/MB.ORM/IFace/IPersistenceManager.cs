//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-01-08
// Description	:	永久化操作需要处理需要实现的接口。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections; 
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.Orm.Enums;

namespace MB.Orm.IFace {
    /// <summary>
    /// 永久化操作需要处理需要实现的接口。
    /// </summary>
    public interface IPersistenceManager : IDisposable {
        /// <summary>
        /// 关闭一个PersistenceManager。当一个PersistenceManager被关闭的时候，需要做如下的工作
        ///1、释放它所管理的实体类，以及相关的状态。如果不释放，可能造成内存泄露
        ///2、如果Flush方法没有被执行，则执行Flush方法。
        ///3、释放数据库联接等资源。
        ///4、从PersistenManagerfactroy中清除自己
        /// </summary>
        void Close();

        bool IsClosed { get; }

        /// <summary>
        /// 获取PersistenceManager当前所处的事务
        /// </summary>
        Transaction CurrentTransaction { get; }
        
        /// <summary>
        /// 指示在默认的情况下，所有的操作是否忽略缓存。如果忽略缓存，那么，有的时候会存在一些不一致的情况，
        /// 除非系统被禁止了缓存的使用。
        /// </summary>
        bool IgnoreCache { get; set; }

        /// <summary>
        /// 将一个新的实体对象转换成可持续对象，这个对象在事务结束的时候，会被Insert到数据库中
        /// 调用这个方法后，该对象的状态为EntityState.New
        /// 如果一个对象的状态为EntityState.Persistent，则本方法将抛出一个EntityIsPersistentException异常
        /// </summary>
        /// <param name="entity"></param>
        void PersistNew(object entity);
        void PersistNew(object entity, PersistOptions options);

        /// <summary>
        /// 将一个新的实体对象转换成可持续对象,并且指明要保存的属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        void PersistNew(object entity, params string[] properties);
        void PersistNew(object entity, PersistOptions options, params string[] properties);

        /// <summary>
        /// 将一个实体对象保存到数据库中。
        ///如果一个对象是Trasient的，则将其转换为EntityState.New状态。在事务结束的时候，会被Insert到数据库中
        ///否则，其状态就是EntityState.Persist，就更新到数据库中。
        ///如果一个Trasient对象实际上已经存在于数据库中，由于Persist方法并不检查实际的数据库，因此，
        ///调用这个方法，将会抛出异常。这个时候，应该使用先使用Attach方法，然后调用Persist。
        ///Persist方法主要用于已受管的对象的更新
        /// </summary>
        /// <param name="entity"></param>
        void Persist(object entity);
        void Persist(object entity, PersistOptions options);

        /// <summary>
        /// 将一个实体对象保存到数据库中,并且指明要保存的属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        void Persist(object entity, params string[] properties);
        void Persist(object entity, PersistOptions options, params string[] properties);

        /// <summary>
        /// 强制将一个实体更新到数据库中。
        /// 执行这个方法，将把对象的状态强制变为EntityState.Persist。
        /// 由于Update方法并不检查实际的数据库，因此如果一个对象实际上不存在于数据库中，那么，这个方法实际上
        /// 不会对数据库造成变化。
        /// 如果不能确认对象已经存在于数据库中，那么，应该使用先使用Attach方法。
        ///</summary>
        ///<param name="entity"></param>
        void Update(object entity);
        void Update(object entity, PersistOptions options);

        /// <summary>
        ///  将一个实体对象更新到数据库中,并且指明要保存的属性 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        void Update(object entity, params string[] properties);
        void Update(object entity, PersistOptions options, params string[] properties);

        /// <summary>
        /// 删除一个对象。
        /// 一个对象被删除后，其状态变成EntityState.Deleted，在事务结束的时候，会被从数据库中删除。
        /// 如果一个对象不是持久的，那么，这个方法将抛出异常。
        /// </summary>
        /// <param name="entity"></param>
        void Delete(object entity);
        void Delete(object entity, PersistOptions options);

       /// <summary>
       /// 将一个对象标记为可持续的。如果这个对象已经存在于实际的数据库中，那么，这个对象的状态就是
       /// EntityState.Persistent，否则，这个对象的状态就是EntityState.New。
       /// 注意：这个方法不会导致对数据库实际数据的更改
       /// </summary>
       /// <param name="entity"></param>
        void Attach(object entity);
        void Attach(object entity, PersistOptions options);

        /// <summary>
        /// 重新从数据库中载入这个对象，这意味着重新给对象的各个属性赋值。 
        /// </summary>
        /// <param name="entity"></param>
        void Reload(ref object entity);
        void Reload(ref object entity, PersistOptions options);

        /// <summary>
        /// 从缓存中把某个对象移除。 
        /// </summary>
        /// <param name="entity"></param>
        void Evict(object entity);
        void EvictAll(object[] pcs);
        void EvictAll(IEnumerable pcs);
        void EvictAll();

        /// <summary>
        /// 根据主键查找某个对象，如果主键是多个字段的，顺序必须同TableMapAttribute中的顺序相同,否则将有不可预测的事情发生 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        object FindByPrimaryKey(Type entityType, params object[] id);
        object FindByPrimaryKey(Type entityType, PersistOptions options, params object[] id);
        T FindByPrimaryKey<T>(params object[] id);
        T FindByPrimaryKey<T>(PersistOptions options, params object[] id);

        /// <summary>
        /// 获取某个对象的状态。这个对象必须是受该PersistenceManager管理的，否则，抛出异常
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        MB.Util.Model.EntityState GetState(object entity);
        ICollection GetManagedEntities();

        /// <summary>
        /// 本操作使对实体对象所作的更改立即生效。
        ///如果本操作实效，应该做如下操作：
        ///从缓存中清除该对象
        /// </summary>
        /// <returns></returns>
        bool Flush();

        /// <summary>
        /// 该方法撤销前面所做的所有操作。
        /// </summary>
        void Cancel();

        /// <summary>
        /// 开始一个新的查询。
        /// </summary>
        /// <returns></returns>
        IQuery NewQuery();
        IQuery NewQuery(Type entityType);
        IQuery NewQuery(Type entityType, string filter);
        //IQuery NewQuery(Type entityType, string filter, QueryParameterCollection paramColletion);

        IQuery<T> NewQuery<T>();
        IQuery<T> NewQuery<T>(string filter);
        //IQuery<T> NewQuery<T>(string filter, QueryParameterCollection paramColletion);
    }
}
