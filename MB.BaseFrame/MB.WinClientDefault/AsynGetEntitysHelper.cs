//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2009-06-08
// Description	:	数据分析处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using MB.Util.Model;

namespace MB.WinClientDefault {
    /// <summary>
    /// 异步加载数据，主要针对Entitys 集合类的解决方案。
    ///  TChannel 对应的WCF 服务端业务类必须实现接口 MB.RuleBase.IFace.IAsynGetEntitysInvoke 
    /// </summary>
    public class AsynGetEntitysHelper<TChannel> where TChannel : class, IDisposable {
        private static readonly string ASYN_INVOKE_METHOD = "ReceiveGreatDataInvoke";
        /// <summary>
        /// 异步调用服务端方法获取对应的数据。
        /// </summary>
        /// <param name="clientProxy">clientProxy 对应的WCF 服务端业务类必须实现接口MB.RuleBase.IFace.IAsynGetEntitysInvoke</param>
        /// <param name="dataInDocType">获取的数据在单据中的数据类型，如果没有就输入0</param>
        /// <param name="paramValues">参数值,根据服务端对应方法决定(可以为空)</param>
        /// <param name="filterParams">动态查询参数,根据服务端对应方法决定 (可以为空)</param>
        /// <returns>实体集合类</returns>
        public IList InvokeGetObjectData(TChannel clientProxy, int dataInDocType,string[] paramValues,MB.Util.Model.QueryParameterInfo[] filterParams) {

            using (MB.Util.MethodTraceWithTime trace = new MB.Util.MethodTraceWithTime(string.Format("AsynGetEntitysHelper<{0}> 开始执行方法{1} ",
                                                                                       typeof(TChannel).FullName, ASYN_INVOKE_METHOD))) {
                //先调用参数为 index 为 -1 进行初始化
                try {
                    string xmlFilterParams = string.Empty;
                    if(filterParams!=null && filterParams.Length > 0)
                        xmlFilterParams = MB.Util.Serializer.QueryParameterXmlSerializer.DefaultInstance.Serializer(filterParams);
                    GreatCapacityInvokeParamInfo iniParam = new GreatCapacityInvokeParamInfo(dataInDocType, xmlFilterParams);
                    iniParam.ParamValues = paramValues;
                    GreatCapacityResult reVal = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(clientProxy, 
                                                                                     ASYN_INVOKE_METHOD, iniParam) as GreatCapacityResult;
                    if (reVal == null)
                        throw new MB.Util.APPException(string.Format("请WCF 客户端{0} 对应的服务业务类是否已经实现了接口{1}",
                                                       clientProxy.GetType().FullName,"MB.RuleBase.IFace.IAsynGetEntitysInvoke")
                                                        , MB.Util.APPMessageType.SysErrInfo);

                    int count = reVal.MaxSegment;

                    IAsyncResult[] res = new IAsyncResult[count];
                    System.Collections.Generic.SortedList<int, IList> dataList = new SortedList<int, IList>();

                    for (int i = 0; i < count; i++) {
                        GreatCapacityInvokeParamInfo bparam = new GreatCapacityInvokeParamInfo(i);
                        object re = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(clientProxy, 
                                                                        "Begin" + ASYN_INVOKE_METHOD, bparam,null,0);
                        res[i] = (IAsyncResult)re;
                    }
                    for (int i = 0; i < count; i++) {
                        if (res[i].IsCompleted) {
                            GreatCapacityResult reData = MB.WcfServiceLocator.WcfClientHelper.Instance.InvokeServerMethod(clientProxy,
                                                         "End" + ASYN_INVOKE_METHOD, res[i]) as GreatCapacityResult;
                            if (reData == null || reData.SegmentData == null)
                                throw new MB.Util.APPException("服务端业务类要根据传入参数的不同进行相应的处理,当获取数据时返回值SegmentData 为空", 
                                                                MB.Util.APPMessageType.SysErrInfo);

                            dataList.Add(i, reData.SegmentData);
                        }
                        else {
                            System.Threading.Thread.Sleep(100);
                            //通过这种变态的方式 等待直到接收到数据为止。
                            i--;
                        }
                    }

                    ArrayList relst = new ArrayList();
                    foreach (var v in dataList.Values) {
                        relst.AddRange(v);
                    }
                    return relst;
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
        }
    }
}
