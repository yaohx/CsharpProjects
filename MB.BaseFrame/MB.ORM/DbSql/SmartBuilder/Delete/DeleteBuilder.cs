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
    /// DeleteBuilder<TEntity>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    internal class DeleteBuilder<TEntity> : DeleteBuilder, IDeleteBuilder<TEntity>
    {
        public DeleteBuilder(TEntity item)
            : base(typeof(TEntity)) {

            BuilderData.Item = item;
        }
        public DeleteBuilder()
            : base(typeof(TEntity)) {

        }
        public IDeleteBuilder<TEntity> Where(Expression<Func<TEntity, object>> columnName) {
            Actions.WhereAction<TEntity>(columnName);
            return this;
        }
        public IDeleteBuilder<TEntity> Where(Expression<Func<TEntity, object>> columnName, object value) {
            Actions.WhereAction<TEntity>(columnName, value);
            return this;
        }

    }
    /// <summary>
    /// DeleteBuilder
    /// </summary>
    internal class DeleteBuilder : AbstractBaseBuilder, IDeleteBuilder
    {
        public DeleteBuilder(Type entityType)
            : base(entityType) {
        }

        public IDeleteBuilder Where(string columnName, object value) {
            Actions.WhereAction(columnName, value);
            return this;
        }

        protected override ISmartSqlGenerator CreateSqlGenerator() {
            return new SmartDeleteSqlGenerator(BuilderData);
        }
    }
}
