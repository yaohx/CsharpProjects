using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.Util.Model;
namespace MB.WinBase.IFace {
    /// <summary>
    /// 模块评论客户端需要实现的接口。
    /// </summary>
    public interface IBfModuleCommentClient {
        /// <summary> 
        /// 获取主表数据。 
        /// </summary> 
        /// <param name="xmlFilterParams"></param> 
        /// <returns></returns> 
        List<BfModuleCommentInfo> GetObjects(MB.Util.Model.QueryParameterInfo[] filterParams);
        /// <summary> 
        /// 增加数据到Cache 中。 
        /// </summary> 
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的数据。</param> 
        /// <param name="entity">需要增加的实体。</param> 
        /// <param name="propertys">需要增加的该实体的指定属性。</param> 
        /// <returns></returns> 
        int AddObject(MB.Util.Model.BfModuleCommentInfo entity);

        /// <summary>
        /// 直接删除评语。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteObject(int id);

        /// <summary>
        /// 清除指定模块的所有评语。
        /// </summary>
        /// <param name="applicationIdentity"></param>
        /// <param name="moduleIdentity"></param>
        /// <returns></returns>
        int ClearObject(string applicationIdentity, string moduleIdentity);
    }
}
