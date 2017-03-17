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
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MB.Orm.DbSql.SmartBuilder
{
    /// <summary>
    /// ActionsHandler
    /// </summary>
    internal class ActionsHandler
    {
        private readonly BuilderData _data;

        internal ActionsHandler(BuilderData data) {
            _data = data;
        }

        internal void ColumnValueAction(string columnName, object value = null, bool isSelect = false) {
            _data.Columns.Add(new SmartTableColumnInfo(columnName, value));
            if (!isSelect)
                ParameterAction(columnName, value, value == null ? typeof(object) : value.GetType(), ParameterDirection.Input);
        }
        internal void ColumnValueAction<T>(Expression<Func<T, object>> expression, bool isSelect = false) {
            ColumnValueAction<T>(expression, null);
        }

        internal void ColumnValueAction<T>(Expression<Func<T, object>> expression, object value, bool isSelect = false) {
            var propertyName = MB.Util.Expressions.ExpressionHelper.GetPropertyName(expression);
            ColumnValueAction(propertyName, getPropertyValue(propertyName, value));
        }

        internal void ParameterAction(string name, object value, Type dataType, ParameterDirection direction, int size = 0) {
            var parameter = new MB.Orm.DbSql.SqlParamInfo();
            parameter.Name = name;
            parameter.DbType = MB.Orm.Common.DbShare.Instance.SystemTypeNameToDbType(dataType.Name);
            parameter.Value = value;
            parameter.Direction = direction;
            parameter.Length = size;

            _data.Parameters.Add(parameter);
        }

        internal void ParameterOutputAction(string name, Type dataType, int size) {
            ParameterAction(name, null, dataType, ParameterDirection.Output, size);
        }

        internal void WhereAction(string columnName, object value) {
            _data.Where.Add(new SmartTableColumnInfo(columnName, value, columnName));
            ParameterAction(columnName, value, value == null ? typeof(object) : value.GetType(), ParameterDirection.Input);
        }

        internal void WhereAction<T>(Expression<Func<T, object>> expression) {
            WhereAction<T>(expression, null);
        }
        internal void WhereAction<T>(Expression<Func<T, object>> expression, object value) {
            var propertyName = MB.Util.Expressions.ExpressionHelper.GetPropertyName(expression);
            WhereAction(propertyName, getPropertyValue(propertyName, value));
        }


        private object getPropertyValue(string propertyName, object value) {
            var proInfo = _data.EntityMapping.FieldPropertys[propertyName];
            if (value == null) {
                if (_data.Item == null) {
                    throw new MB.Util.APPException("SmartDAL Execute 时通过属性{0} 获取对象值时，发现实体对象为空，请检查", Util.APPMessageType.SysErrInfo);
                }
                return proInfo.PropertyInfo.GetValue(_data.Item,null);
            }
            else {
                return value;
            }
        }
    }
}
