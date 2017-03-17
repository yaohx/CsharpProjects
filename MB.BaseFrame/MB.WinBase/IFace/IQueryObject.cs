using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinBase.IFace {
    /// <summary>
    /// 获取数据对象需要实现的接口。
    /// </summary>
   public interface IQueryObject {
       /// <summary>
       ///  获取对象数据。
       /// </summary>
       /// <param name="dataInDocType"></param>
       /// <param name="filterParameters"></param>
       /// <returns></returns>
       IList GetFilterObjects(MB.Util.Model.QueryParameterInfo[] filterParameters); 
    }
}
