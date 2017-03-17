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

namespace MB.Orm.DbSql.SmartBuilder.Update
{
    public interface IUpdateBuilder<TEntity> : IUpdateBuilder
    {
        IUpdateBuilder<TEntity> Where(Expression<Func<TEntity, object>> columnName, object value);
        IUpdateBuilder<TEntity> Where(Expression<Func<TEntity, object>> columnName);
        IUpdateBuilder<TEntity> Set(Expression<Func<TEntity, object>> columnName);
        IUpdateBuilder<TEntity> Set(Expression<Func<TEntity, object>> columnName, object value);
    }
    public interface IUpdateBuilder
    {
        int Execute();
        IUpdateBuilder Set(string columnName, object value);
        IUpdateBuilder Where(string columnName, object value);
    }
}
