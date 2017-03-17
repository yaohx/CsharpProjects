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
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MB.Orm.DB;
using MB.Orm.Persistence;
using MB.Util;

namespace MB.Orm.DbSql.SmartBuilder.Query
{
    internal class QueryBuilder<TEntity> : QueryBuilder, IQueryBuilder<TEntity>
    {
        public QueryBuilder()
            : base(typeof(TEntity)) {
        }
        public IQueryBuilder<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, object>> columnName) {
            Actions.WhereAction<TEntity>(columnName);
            return this;
        }
        public IQueryBuilder<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, object>> columnName, object value) {
            Actions.WhereAction<TEntity>(columnName, value);
            return this;
        }

        public IQueryBuilder<TEntity> Select(System.Linq.Expressions.Expression<Func<TEntity, object>> columnName) {
            var propertyName = MB.Util.Expressions.ExpressionHelper.GetPropertyName(columnName);
            Select(propertyName);
            return this;
        }
    }

    internal class QueryBuilder : AbstractBaseBuilder, IQueryBuilder
    {
        public QueryBuilder(Type entityType)
            : base(entityType) {
        }

        public IQueryBuilder Select(string columnName) {
            Actions.ColumnValueAction(columnName, true);
            return this;
        }

        public IQueryBuilder Where(string columnName, object value) {
            Actions.WhereAction(columnName, value);
            return this;
        }
        protected override ISmartSqlGenerator CreateSqlGenerator() {
            return new SmartQuerySqlGenerator(BuilderData);
        }
        public TReturn ExecuteScalar<TReturn>() {
            Type valType = typeof(TReturn);
            if (isRequeryType(valType)) {
                var data = ExecuteReturnValue<TReturn>((db, cmd) => {
                    var val = db.ExecuteScalar(cmd);
                    if (val == null || val == System.DBNull.Value)
                        return default(TReturn);
                    else {
                        return (TReturn)MB.Util.MyConvert.Instance.ChangeType(val, valType);
                    }
                });
                return data;
            }
            else {
                throw new MB.Util.APPException(string.Format("Smart ExecuteScalar 返回值暂时不支持类型{0}", valType.FullName), APPMessageType.SysErrInfo);
            }
        }
        public List<T> Execute<T>() {
            Type valType = typeof(T);
            if (isRequeryType(valType)) {
                var data = ExecuteReturnValue<List<T>>((db, cmd) => {
                    using (var reader = db.ExecuteReader(cmd)) {
                        List<T> lstData = new List<T>();
                        while (reader.Read()) {
                            lstData.Add((T)MB.Util.MyConvert.Instance.ChangeType(reader[0], valType));
                        }
                        return lstData;
                    }
                });
                return data;
            }
            else {
                throw new MB.Util.APPException(string.Format("Smart Execute 返回值暂时不支持类型{0}", valType.FullName), APPMessageType.SysErrInfo);
            }
        }
        public dynamic ExecuteDynamic() {
            var data = ExecuteReturnValue<dynamic>((db, cmd) => {
                using (var reader = db.ExecuteReader(cmd)) {
                    if (!reader.Read())
                        return null;
                    else
                        return createDynamicObject(reader);
                }
            });
            return data;

        }
        public List<dynamic> ExecuteDynamicList() {
            var data = ExecuteReturnValue<List<dynamic>>((db, cmd) => {
                using (var reader = db.ExecuteReader(cmd)) {
                    var lstData = new List<dynamic>();
                    while (reader.Read()) {
                        var ls = createDynamicObject(reader);
                        lstData.Add(ls);
                    }
                    return lstData;
                }
            });
            return data;
        }
        private ExpandoObject createDynamicObject(IDataReader reader) {
            var ls = new System.Dynamic.ExpandoObject();
            var itemDictionary = (IDictionary<string, object>)ls;
            foreach (var col in this.BuilderData.Columns) {
                var val = reader[col.ColumnName];
                itemDictionary.Add(col.ColumnName, val);
            }
            return ls;
        }
        private bool isRequeryType(Type valueType) {
            return valueType.IsValueType || valueType.Equals(typeof(string));
        }
    }
}
