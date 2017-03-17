using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.EntitySetCache
{
    /// <summary>
    /// 关注于Filter本身的过滤条件
    /// </summary>
    public enum FilterCondition
    {
        None,
        Equal,
        Like,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
        Different
    }

    /// <summary>
    /// 过滤条件，在查询缓存中数据的时候需要指定过滤条件
    /// </summary>
    public class FilterParameter
    {
        /// <summary>
        /// 需要与被过滤的数据源中的某个属性名字一致
        /// </summary>
        public string FilterName { get; private set; }

        public object FilterValue { get; private set; }

        public FilterCondition FilterCondition { get; private set; }


        private FilterParameter(string filterName, object filterValue)
        {
            this.FilterName = filterName;
            this.FilterValue = filterValue;
            this.FilterCondition = FilterCondition.Equal;
        }

        private FilterParameter(string filterName, object filterValue, FilterCondition condition)
            : this(filterName, filterValue)
        {
            if (condition == FilterCondition.None)
                this.FilterCondition = FilterCondition.Equal;
            else
                this.FilterCondition = condition;
        }

        /// <summary>
        /// 创建缓存过滤的参数，默认情况下过滤条件都是 == 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        public static FilterParameter CreateFilterParamater<T>(string propertyName, object propertyValue)
        {
            return CreateFilterParamater<T>(propertyName, propertyValue, FilterCondition.None);
        }

        /// <summary>
        /// 创建缓存过滤的参数，并且自己指定过滤条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static FilterParameter CreateFilterParamater<T>(string propertyName, object propertyValue, FilterCondition condition)
        {
            Type type = typeof(T);
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo == null)
                throw new MB.Util.APPException(string.Format("穿入的属性{0}并不存在类型{1}中", propertyInfo.Name, type.ToString()),
                    Util.APPMessageType.SysErrInfo);

            if (propertyInfo.PropertyType != typeof(string) && condition == FilterCondition.Like)
                throw new MB.Util.APPException(string.Format("非string类型的属性{0},{1}不支持Like过滤条件", propertyInfo.Name, type.ToString()),
                    Util.APPMessageType.SysErrInfo);

            FilterParameter filterParamater = new FilterParameter(propertyName, propertyValue, condition);
            return filterParamater;
        }
    }
}
