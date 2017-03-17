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

namespace MB.Orm.DbSql.SmartBuilder.Insert
{
    internal class InsertBuilder<TEntity> : InsertBuilder, IInsertBuilder<TEntity>
    {
        public InsertBuilder()
            : base(typeof(TEntity)) {
        }
        public InsertBuilder(TEntity entity)
            : base(typeof(TEntity)) {
            BuilderData.Item = entity;
        }


        public IInsertBuilder<TEntity> Column(System.Linq.Expressions.Expression<Func<TEntity, object>> columnName) {
            Actions.ColumnValueAction<TEntity>(columnName);
            return this;
        }

        public IInsertBuilder<TEntity> Column(System.Linq.Expressions.Expression<Func<TEntity, object>> columnName, object value) {
            Actions.ColumnValueAction<TEntity>(columnName, value);
            return this;
        }


    }

    internal class InsertBuilder : AbstractBaseBuilder, IInsertBuilder
    {
        public InsertBuilder(Type entityType)
            : base(entityType) {
        }


        public IInsertBuilder Column(string columnName, object value) {
            Actions.ColumnValueAction(columnName, value);
            return this;
        }

        protected override ISmartSqlGenerator CreateSqlGenerator() {
            return new SmartInsertSqlGenerator(BuilderData);
        }
    }
}
