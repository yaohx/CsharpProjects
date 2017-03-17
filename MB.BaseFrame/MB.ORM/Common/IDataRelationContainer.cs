using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Common
{
    /// <summary>
    /// 关系数据容器必须要实现的接口。
    /// </summary>
    public interface IDataRelationContainer<TBaseModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        void AddItem(DataRelationValue<TBaseModel> val);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TDetail"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        TDetail[] GetChilds<T, TDetail>(T parent)
            where TDetail : TBaseModel
            where T : TBaseModel;
    }
}
