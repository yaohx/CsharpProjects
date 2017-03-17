//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-05-18
// Description	:	对象提交封装处理。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Orm.DbSql.SmartBuilder.Delete;
using MB.Orm.DbSql.SmartBuilder.Exists;
using MB.Orm.DbSql.SmartBuilder.Insert;
using MB.Orm.DbSql.SmartBuilder.Query;
using MB.Orm.DbSql.SmartBuilder.Update;

namespace MB.RuleBase.Common
{
    /// <summary>
    /// UFSmartDAL
    /// </summary>
    public class SmartDAL
    {
        #region Delete...
        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IDeleteBuilder<TEntity> Delete<TEntity>() {
            return MB.Orm.DbSql.SmartBuilder.SmartBuilderFactory.CreateDeleteBuilder<TEntity>();
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IDeleteBuilder<TEntity> Delete<TEntity>(TEntity entity) {
            return MB.Orm.DbSql.SmartBuilder.SmartBuilderFactory.CreateDeleteBuilder<TEntity>(entity);
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static IDeleteBuilder Delete(Type entityType) {
            return MB.Orm.DbSql.SmartBuilder.SmartBuilderFactory.CreateDeleteBuilder(entityType);
        }
        #endregion

        #region Update...
        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IUpdateBuilder<TEntity> Update<TEntity>() {
            return MB.Orm.DbSql.SmartBuilder.SmartBuilderFactory.CreateUpdateBuilder<TEntity>();
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IUpdateBuilder<TEntity> Update<TEntity>(TEntity entity) {
            return MB.Orm.DbSql.SmartBuilder.SmartBuilderFactory.CreateUpdateBuilder<TEntity>(entity);
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static IUpdateBuilder Update(Type entityType) {
            return MB.Orm.DbSql.SmartBuilder.SmartBuilderFactory.CreateUpdateBuilder(entityType);
        }
        #endregion

        #region Insert...
        /// <summary>
        /// Insert
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IInsertBuilder<TEntity> Insert<TEntity>() {
            return MB.Orm.DbSql.SmartBuilder.SmartBuilderFactory.CreateInsertBuilder<TEntity>();
        }
        /// <summary>
        /// Insert
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IInsertBuilder<TEntity> Insert<TEntity>(TEntity entity) {
            return MB.Orm.DbSql.SmartBuilder.SmartBuilderFactory.CreateInsertBuilder<TEntity>(entity);
        }
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static IInsertBuilder Insert(Type entityType) {
            return MB.Orm.DbSql.SmartBuilder.SmartBuilderFactory.CreateInsertBuilder(entityType);
        }
        #endregion

        #region Query...
        /// <summary>
        /// Query
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IQueryBuilder<TEntity> Query<TEntity>() {
            return MB.Orm.DbSql.SmartBuilder.SmartBuilderFactory.CreateQueryBuilder<TEntity>();
        }
        /// <summary>
        /// Query
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static IQueryBuilder Query(Type entityType) {
            return MB.Orm.DbSql.SmartBuilder.SmartBuilderFactory.CreateQueryBuilder(entityType);
        }
        #endregion

        #region Exists...
        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static IExistsBuilder Exists(Type entityType) {
            return MB.Orm.DbSql.SmartBuilder.SmartBuilderFactory.CreateExistsBuilder(entityType);
        }
        /// <summary>
        /// Exists
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IExistsBuilder<TEntity> Exists<TEntity>() {
            return MB.Orm.DbSql.SmartBuilder.SmartBuilderFactory.CreateExistsBuilder<TEntity>();
        }
        #endregion
    }
}
