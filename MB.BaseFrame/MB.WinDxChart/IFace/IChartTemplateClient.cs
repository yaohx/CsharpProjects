using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Util.Model.Chart;

namespace MB.WinDxChart.IFace
{
    public interface IChartTemplateClient
    {
        /// <summary>
        /// 获取主表数据
        /// </summary>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        List<ChartTemplateInfo> GetObjects(MB.Util.Model.QueryParameterInfo[] filterParams);

        /// <summary>
        /// 获取指定用户的模板信息
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        List<ChartTemplateInfo> GetObjectByUser(string userCode);

        /// <summary> 
        /// 增加数据 
        /// </summary> 
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的数据。</param> 
        /// <param name="entity">需要增加的实体。</param> 
        /// <param name="propertys">需要增加的该实体的指定属性。</param> 
        /// <returns></returns> 
        int AddObject(ChartTemplateInfo entity);

        /// <summary>
        /// 直接删除模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteObject(int id);
    }
}
