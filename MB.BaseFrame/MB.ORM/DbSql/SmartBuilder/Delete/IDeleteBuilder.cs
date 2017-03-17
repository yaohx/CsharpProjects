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

namespace MB.Orm.DbSql.SmartBuilder.Delete
{
    /// <summary>
    /// IDeleteBuilder
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDeleteBuilder<TEntity> : IDeleteBuilder
    {
        /// <summary>
        /// 删除的条件
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        IDeleteBuilder<TEntity> Where(Expression<Func<TEntity, Object>> columnName);
        /// <summary>
        /// 删除的条件
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">对应的值</param>
        /// <returns></returns>
        IDeleteBuilder<TEntity> Where(Expression<Func<TEntity, Object>> columnName, object value);

    }
    /// <summary>
    /// IDeleteBuilder
    /// </summary>
    public interface IDeleteBuilder
    {
        /// <summary>
        /// 删除的条件
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IDeleteBuilder Where(string columnName, object value);
        int Execute();
    }
}
