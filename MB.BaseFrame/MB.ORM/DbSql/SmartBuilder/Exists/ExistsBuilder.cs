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

namespace MB.Orm.DbSql.SmartBuilder.Exists
{
    internal class ExistsBuilder<TEntity> : ExistsBuilder, IExistsBuilder<TEntity>
    {
        public ExistsBuilder()
            : base(typeof(TEntity)) {
        }
        public ExistsBuilder(TEntity entity)
            : base(typeof(TEntity)) {
            BuilderData.Item = entity;
        }
        public IExistsBuilder<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, object>> columnName) {
            Actions.WhereAction<TEntity>(columnName);
            return this;
        }

        public IExistsBuilder<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, object>> columnName, object value) {
            Actions.WhereAction<TEntity>(columnName, value);
            return this;
        }
    }
    internal class ExistsBuilder : AbstractBaseBuilder, IExistsBuilder
    {
        public ExistsBuilder(Type entityType)
            : base(entityType) {
        }
        public new bool Execute() {
            var data = ExecuteReturnValue<bool>((db, cmd) => {
                var val = db.ExecuteScalar(cmd);
                if (val == null || val == System.DBNull.Value)
                    return false;
                else {
                    return true;
                }
            });
            return data;
        }

        public IExistsBuilder Where(string columnName, object value) {
            Actions.WhereAction(columnName, value);
            return this;
        }

        protected override ISmartSqlGenerator CreateSqlGenerator() {
            return new SmartExistsSqlGenerator(BuilderData);
        }
    }
}
