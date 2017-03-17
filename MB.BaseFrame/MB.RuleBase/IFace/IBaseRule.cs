//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-03
// Description	:	所有业务类必须要实现的接口。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------

using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using MB.Util.Model;
namespace MB.RuleBase.IFace {
    /// <summary>
    /// 所有业务类必须要实现的接口。
    /// </summary>
    [ServiceContract]
    public interface IBaseRule : IBaseQueryRule {

        #region 对象编辑操作方法...
        /// <summary>
        /// 执行业务对象保存的永久化操作。
        /// </summary>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))] 
        [TransactionFlow(TransactionFlowOption.Allowed)]   
        [OperationContract]
        int Flush();
        /// <summary>
        /// 重新属性实体对象。
        /// </summary>
        /// <param name="dataInDocType"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        object RefreshEntity(int dataInDocType, object entity);
        /// <summary>
        /// 增加数据到Cache 中。
        /// </summary>
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的数据。</param>
        /// <param name="entity">需要增加的实体。</param>
        /// <param name="propertys">需要增加的该实体的指定属性。</param>
        /// <returns></returns>
        int AddToCache(int dataInDocType, object entity,bool isDelete, string[] propertys);
        /// <summary>
        /// 直接删除数据（同时执行本地和数据库操作）
        /// </summary>
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的键值。</param>
        /// <param name="key">对象键值</param>.
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))] 
        [TransactionFlow(TransactionFlowOption.Allowed)]  
        [OperationContract]
        int DeletedImmediate(int dataInDocType, object key);
        /// <summary>
        /// 对象数据提交或者撤消提交。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancelSubmit"></param>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))] 
        int Submit(object entity, bool cancelSubmit);
        /// <summary>
        /// 保存主表数据。
        /// 如果存在大数据 不能在客户端直接调用,需要通过大数据调用载体来进行。
        /// (未来可以通过配置的方式来解决)
        /// </summary>
        /// <param name="dsData"></param>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))] 
        [TransactionFlow(TransactionFlowOption.Allowed)]  
        [OperationContract]
        int SaveDataSetImmediate(DataSet dsData, int dataInDocType);

        #endregion 对象编辑操作方法...

        /// <summary>
        /// 获取指定类型新创建对象的自增列ID。
        /// </summary>
        /// <param name="dataInDocType"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))] 
        [OperationContract]
        int GetCreateNewEntityIds(int dataInDocType,int count);

        /// <summary>
        /// 批量创建实体对象。
        /// </summary>
        /// <param name="dataInDocType"></param>
        /// <param name="createCount"></param>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))]
        [OperationContract]
        IList CreateNewEntityBatch(int dataInDocType, int createCount);
        /// <summary>
        /// 根据指定的数据类型创建一个新的实体对象。
        /// </summary>
        /// <param name="dataInDocType"></param>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))] 
        [OperationContract]
        object CreateNewEntity(int dataInDocType);

        /// <summary>
        /// 通过对象的主键获取对象下指定类型的关联子对象数据。
        /// 如果存在大数据 不能在客户端直接调用,需要通过大数据调用载体来进行。
        /// (未来可以通过配置的方式来解决)
        /// </summary>
        /// <param name="dataInDocType">在单据中的数据类型,默认为主表的键值。</param>
        /// <param name="mainKey">主表键值。</param>
        /// <returns></returns>
        [FaultContract(typeof(MB.Util.Model.WcfFaultMessage))] 
        [OperationContract]
        object GetObjectByKey(int dataInDocType, object keyValue);

        /// <summary>
        /// 根据数据类型检查指定的值在数据库中是否已经存在
        /// </summary>
        /// <param name="dataInDocType">需要进行检查的数据类型</param>
        /// <param name="entity">需要检查的实体对象</param>
        /// <param name="checkPropertys">需要检查的属性名称</param>
        /// <returns></returns>
        bool CheckValueIsExists(int dataInDocType, object entity, string[] checkPropertys);

        ///// <summary>
        ///// 根据过滤的条件以IList的格式 获取需要的数据。
        ///// 如果存在大数据 不能在客户端直接调用,需要通过大数据调用载体来进行。
        ///// (未来可以通过配置的方式来解决)
        ///// 备注：WCF 服务不支持范型。
        ///// </summary>
        ///// <param name="dataInDocType">在单据中的数据类型,默认为主表的键值。</param>
        ///// <param name="filter">过滤条件</param>
        ///// <returns>范型的集合类</returns>
        //IList<T> GetObjects<T>(object dataInDocType, QueryParameterInfo[] filters);

        ///// <summary>
        ///// 通过对象的主键获取对象下指定类型的关联子对象数据。
        ///// 如果存在大数据 不能在客户端直接调用,需要通过大数据调用载体来进行。
        ///// (未来可以通过配置的方式来解决)
        ///// 备注：WCF 服务不支持范型。
        ///// </summary>
        ///// <param name="dataInDocType">在单据中的数据类型,默认为主表的键值。</param>
        ///// <param name="mainKey">主表键值。</param>
        ///// <returns></returns>
        //T GetObjectByKey<T>(object dataInDocType, object keyValue);

    }

 
}
