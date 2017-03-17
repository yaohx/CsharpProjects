using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MB.Orm.EntitySetCache
{
    /// <summary>
    /// 缓存接口,需要业务类去实现的接口
    /// </summary>
    public interface ICacheLoader
    {
        /// <summary>
        /// 加载缓存
        /// </summary>
        /// <returns></returns>
        IList LoadCache();

        ///// <summary>
        ///// 加载增量缓存
        ///// </summary>
        //IList LoadIncrementalCache();

    }
}
