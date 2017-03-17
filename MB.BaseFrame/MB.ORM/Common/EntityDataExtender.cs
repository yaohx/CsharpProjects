using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using MB.Util.Expressions;

namespace MB.Orm.Common
{
    /// <summary>
    /// 实体对象的扩张方法。
    /// </summary>
    public static class EntityDataExtender
    {
        /// <summary>
        /// 增加一个子项。
        /// </summary>
        /// <typeparam name="TBaseModel"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="rootContainer"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        public static T AddChildItem<T>(this MB.Orm.Common.BaseModel parent, IDataRelationContainer<MB.Orm.Common.BaseModel> rootContainer, T child)
                where T : MB.Orm.Common.BaseModel {
            rootContainer.AddItem(new DataRelationValue<MB.Orm.Common.BaseModel>() { Parent = parent, Child = child });

            return child;
        }
        /// <summary>
        /// 获取所有指定类型的子项。
        /// </summary>
        /// <typeparam name="TDetail"></typeparam>
        /// <param name="parent"></param>
        /// <param name="rootContainer"></param>
        /// <returns></returns>
        public static TDetail[] GetChildItems<TDetail>(this MB.Orm.Common.BaseModel parent, IDataRelationContainer<MB.Orm.Common.BaseModel> rootContainer)
                where TDetail : MB.Orm.Common.BaseModel {
            return rootContainer.GetChilds<MB.Orm.Common.BaseModel, TDetail>(parent);
        }

        /// <summary>
        /// 获取对象属性名称。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetPropertyName<T>(this T entity, Expression<Func<T, Object>> expression) {
            return ExpressionHelper.GetPropertyName<T>(expression);
        }

    }
}
