//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	 2009-08-24 10:39 
// Description	:	模块使用情况评论。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using MB.Util.Model;

namespace MB.WcfService.IFace {
    /// <summary>
    /// 模块使用情况评论。
    /// </summary>
    [ServiceContract]
    public interface IBfModuleComment{
        /// <summary> 
        /// 获取主表数据。 
        /// </summary> 
        /// <param name="xmlFilterParams"></param> 
        /// <returns></returns> 
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))]
        [OperationContract]
        List<BfModuleCommentInfo> GetObjects(string xmlFilterParams);
        /// <summary> 
        /// 增加数据到Cache 中。 
        /// </summary> 
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的数据。</param> 
        /// <param name="entity">需要增加的实体。</param> 
        /// <param name="propertys">需要增加的该实体的指定属性。</param> 
        /// <returns></returns> 
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))]
        [OperationContract]
        int AddObjectImmediate(MB.Util.Model.BfModuleCommentInfo entity);

        /// <summary>
        /// 直接删除评语信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))]
        [OperationContract]
        int DeletedImmediate(int id);
        /// <summary>
        /// 清除指定模块的所有评语
        /// </summary>
        /// <param name="applicationIdentity"></param>
        /// <param name="moduleIdentity"></param>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))]
        [OperationContract]
        int ClearImmediate(string applicationIdentity, string moduleIdentity);
    } 

}
