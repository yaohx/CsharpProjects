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
using System.Linq.Expressions;
using System.Text;

namespace MB.Orm.DbSql.SmartBuilder.Query
{
    public interface IQueryBuilder<TEntity> : IQueryBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        IQueryBuilder<TEntity> Where(Expression<Func<TEntity, object>> columnName);
        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IQueryBuilder<TEntity> Where(Expression<Func<TEntity, object>> columnName, object value);
        /// <summary>
        /// 查询返回的字段。
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        IQueryBuilder<TEntity> Select(Expression<Func<TEntity, object>> columnName);
    }
    public interface IQueryBuilder
    {
        /// <summary>
        /// 目前只支持返回值类型或字符窜
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        TReturn ExecuteScalar<TReturn>();
        /// <summary>
        /// T 目前只支持返回值类型或字符窜
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> Execute<T>();
        /// <summary>
        /// 返回动态对象
        /// </summary>
        /// <returns></returns>
        dynamic ExecuteDynamic();
        /// <summary>
        /// 返回动态对象集合
        /// </summary>
        /// <returns></returns>
        List<dynamic> ExecuteDynamicList();
        /// <summary>
        /// 选择需要返回的字段。
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        IQueryBuilder Select(string columnName);
        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IQueryBuilder Where(string columnName, object value);
    }
}
