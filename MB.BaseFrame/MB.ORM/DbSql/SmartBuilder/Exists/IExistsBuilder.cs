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

namespace MB.Orm.DbSql.SmartBuilder.Exists
{
    public interface IExistsBuilder<TEntity> : IExistsBuilder
    {
        IExistsBuilder<TEntity> Where(Expression<Func<TEntity, object>> columnName);
        IExistsBuilder<TEntity> Where(Expression<Func<TEntity, object>> columnName, object value);
    }
    public interface IExistsBuilder
    {
        bool Execute();
        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IExistsBuilder Where(string columnName, object value);
    }
}
