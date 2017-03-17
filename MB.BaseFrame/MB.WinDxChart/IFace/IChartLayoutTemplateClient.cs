using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Util.Model.Chart;

namespace MB.WinDxChart.IFace
{
    public interface IChartLayoutTemplateClient
    {
        /// <summary>
        /// 获取主表数据
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        List<ChartLayoutTemplateInfo> GetObjects(MB.Util.Model.QueryParameterInfo[] queryParams);

        /// <summary> 
        /// 获取明细数据。 
        /// </summary> 
        /// <param name="xmlFilterParams"></param> 
        /// <returns></returns> 
        List<ChartLayoutItemInfo> GetObjectDetail(int id);

        /// <summary> 
        /// 增加数据到Cache 中。 
        /// </summary> 
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的数据。</param> 
        /// <param name="entity">需要增加的实体。</param> 
        /// <param name="propertys">需要增加的该实体的指定属性。</param> 
        /// <returns></returns> 
        int AddObject(ChartLayoutTemplateInfo entity, List<ChartLayoutItemInfo> items);

        /// <summary>
        /// 获取当前用户可用模板
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        List<ChartLayoutTemplateInfo> GetObjectByUserCode(string userCode);

        /// <summary>
        /// 直接删除布局
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteObject(int id);
    }
}
