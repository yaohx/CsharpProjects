//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-08-02
// Description	:	 
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

namespace MB.Orm.DbSql.SmartBuilder
{
    /// <summary>
    /// SmartBuilderFactory
    /// </summary>
    public class SmartBuilderFactory
    {
        #region IDeleteBuilder...
        /// <summary>
        /// CreateDeleteBuilder
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IDeleteBuilder<TEntity> CreateDeleteBuilder<TEntity>() {
            return new DeleteBuilder<TEntity>();
        }
        /// <summary>
        /// CreateDeleteBuilder<TEntity>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IDeleteBuilder<TEntity> CreateDeleteBuilder<TEntity>(TEntity entity) {
            return new DeleteBuilder<TEntity>(entity);
        }
        /// <summary>
        /// CreateDeleteBuilder
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IDeleteBuilder CreateDeleteBuilder(Type entityType) {
            return new DeleteBuilder(entityType);
        }
        #endregion IDeleteBuilder...

        #region IInsertBuilder...
        /// <summary>
        /// CreateInsertBuilder
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IInsertBuilder<TEntity> CreateInsertBuilder<TEntity>() {
            return new InsertBuilder<TEntity>();
        }
        /// <summary>
        /// CreateInsertBuilder<TEntity>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IInsertBuilder<TEntity> CreateInsertBuilder<TEntity>(TEntity entity) {
            return new InsertBuilder<TEntity>(entity);
        }
        /// <summary>
        /// CreateInsertBuilder
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IInsertBuilder CreateInsertBuilder(Type entityType) {
            return new InsertBuilder(entityType);
        }
        #endregion IDeleteBuilder...

        #region IUpdateBuilder...
        /// <summary>
        /// CreateUpdateBuilder
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IUpdateBuilder<TEntity> CreateUpdateBuilder<TEntity>() {
            return new UpdateBuilder<TEntity>();
        }
        /// <summary>
        /// CreateUpdateBuilder<TEntity>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IUpdateBuilder<TEntity> CreateUpdateBuilder<TEntity>(TEntity entity) {
            return new UpdateBuilder<TEntity>(entity);
        }
        /// <summary>
        /// CreateUpdateBuilder
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IUpdateBuilder CreateUpdateBuilder(Type entityType) {
            return new UpdateBuilder(entityType);
        }
        #endregion IUpdateBuilder...

        #region IQueryBuilder...
        /// <summary>
        /// CreateQueryBuilder
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IQueryBuilder<TEntity> CreateQueryBuilder<TEntity>() {
            return new QueryBuilder<TEntity>();
        }
        /// <summary>
        /// CreateQueryBuilder
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static IQueryBuilder CreateQueryBuilder(Type entityType) {
            return new QueryBuilder(entityType);
        }
        #endregion IDeleteBuilder...

        #region CreateExistsBuilder...
        /// <summary>
        /// CreateExistsBuilder
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static IExistsBuilder CreateExistsBuilder(Type entityType) {
            return new ExistsBuilder(entityType);
        }
        /// <summary>
        /// CreateExistsBuilder
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IExistsBuilder<TEntity> CreateExistsBuilder<TEntity>() {
            return new ExistsBuilder<TEntity>();
        }
        #endregion...
    }
}
