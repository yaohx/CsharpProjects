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

namespace MB.Orm.DbSql.SmartBuilder.Insert
{
    public interface IInsertBuilder<TEntity> : IInsertBuilder
    {
        IInsertBuilder<TEntity> Column(Expression<Func<TEntity, Object>> columnName);
        IInsertBuilder<TEntity> Column(Expression<Func<TEntity, Object>> columnName, object value);
    }
    public interface IInsertBuilder
    {
        IInsertBuilder Column(string columnName, object value);
        int Execute();
    }
}
