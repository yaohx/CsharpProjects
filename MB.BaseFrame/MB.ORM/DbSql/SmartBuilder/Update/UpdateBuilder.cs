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

namespace MB.Orm.DbSql.SmartBuilder.Update
{
    internal class UpdateBuilder<TEntity> : UpdateBuilder, IUpdateBuilder<TEntity>
    {
        public UpdateBuilder()
            : base(typeof(TEntity)) {
        }
        public UpdateBuilder(TEntity entity)
            : base(typeof(TEntity)) {

            BuilderData.Item = entity;
        }
        public IUpdateBuilder<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, object>> columnName, object value) {
            Actions.WhereAction<TEntity>(columnName, value);
            return this;
        }

        public IUpdateBuilder<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, object>> columnName) {
            Actions.WhereAction<TEntity>(columnName);
            return this;
        }


        public IUpdateBuilder<TEntity> Set(System.Linq.Expressions.Expression<Func<TEntity, object>> columnName) {
            Actions.ColumnValueAction<TEntity>(columnName);
            return this;
        }

        public IUpdateBuilder<TEntity> Set(System.Linq.Expressions.Expression<Func<TEntity, object>> columnName, object value) {
            Actions.ColumnValueAction<TEntity>(columnName, value);
            return this;
        }
    }
    internal class UpdateBuilder : AbstractBaseBuilder, IUpdateBuilder
    {
        public UpdateBuilder(Type entityType)
            : base(entityType) {
        }
        public IUpdateBuilder Set(string columnName, object value) {
            Actions.ColumnValueAction(columnName, value);
            return this;
        }

        public IUpdateBuilder Where(string columnName, object value) {
            Actions.WhereAction(columnName, value);
            return this;
        }
        protected override ISmartSqlGenerator CreateSqlGenerator() {
            return new SmartUpdateSqlGenerator(BuilderData);
        }
    }
}
