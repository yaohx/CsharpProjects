//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-02
// Description	:	ObjectDataList  在数据存储处理中涉及到数据库操作的数据对象自定义集合。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.RuleBase.Common {
    /// <summary>
    /// ObjectDataList  在数据存储处理中涉及到数据库操作的数据对象自定义集合。
    /// </summary>
    [Serializable]
    public class ObjectDataList : System.Collections.Concurrent.ConcurrentDictionary<string,ObjectDataInfo>{
        private static readonly string ACCEPT_CHANGES_METHOD = "AcceptChanges";
        private static readonly string REJECT_CHANGES_METHOD = "RejectChanges";
        private string[] _ExcludeDataStateChanged = new string[] {"DataRow"};

        private static readonly string SPE_CHAR = " _~!_M_ ";
        /// <summary>
        /// 构造函数...
        /// </summary>
        public ObjectDataList() {
            
        }

        #region public 方法...
        /// <summary>
        /// 增加当前需要编辑的数据对象。
        /// </summary>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public ObjectDataInfo Add(ObjectDataInfo dataInfo) {
            dataInfo.SaveToCacheDateTime = System.DateTime.Now;
            string keyString = toSaveKeyString(dataInfo);

            this[keyString] = dataInfo;
 
            return dataInfo;
        }
      
        /// <summary>
        /// 提交对ObjectDataInfo 中数据的修改。
        /// </summary>
        public void AcceptDataChanges() {
            IEnumerable<ObjectDataInfo> vals = getCanChanges();
            foreach (ObjectDataInfo info in vals) {
                info.DataState = ObjectDataState.Unchanged;

                setObjectAcceptChanges(info.ObjectData);
            }
        }
        private void setObjectAcceptChanges(object data) {
            MB.Orm.Common.BaseModel temp = data as MB.Orm.Common.BaseModel;
            if (temp != null)
                temp.EntityState = MB.Util.Model.EntityState.Persistent;
            else {
                IList lst = data as IList;
                if (lst != null) {
                    foreach (object t in lst)
                        setObjectAcceptChanges(t);
                }
                else {
                    MB.Util.MyReflection.Instance.InvokeMethod(data, ACCEPT_CHANGES_METHOD);
                }
            }
        }
        /// <summary>
        /// 回滚自上次提交以来对ObjectDataInfo 中 数据的修改。
        /// </summary>
        public void RejectDataChanges() {
            IEnumerable<ObjectDataInfo> vals = getCanChanges();
            foreach (ObjectDataInfo info in vals)
                MB.Util.MyReflection.Instance.InvokeMethod(info.ObjectData, REJECT_CHANGES_METHOD);
        }
        /// <summary>
        /// 按存储的先后顺序进行排序。
        /// </summary>
        public IEnumerable<ObjectDataInfo> OrderBySaveIndex() {
            IEnumerable<ObjectDataInfo> wvals = this.Values.Where(cc => cc != null);
            IEnumerable<ObjectDataInfo> evals = wvals.OrderBy(cc => cc.SaveOrderIndex);
            return evals;
        }
        /// <summary>
        /// 获取可以进行数据持久化操作的对象并按SaveOrderIndex 和 进入集合的顺序进行排序
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ObjectDataInfo> GetCanSaveAndOrder() {
            IEnumerable<ObjectDataInfo> wvals = this.Values.Where(cc => cc.DataState != ObjectDataState.Detached &&
                                                                    cc.DataState != ObjectDataState.Unchanged);
            //先按照进入集合的时间进行排序。
            IEnumerable<ObjectDataInfo> tvals = wvals.OrderBy(cc => cc.SaveToCacheDateTime);
            //再按照存储的先后顺序进行排序。
            IEnumerable<ObjectDataInfo> evals = tvals.OrderBy(cc => cc.SaveOrderIndex);
            return evals;
        }
        #endregion public 方法...

        #region 内部函数处理...
        //对象的基本信息有些时候是以Datarow 的格式存储, 对于无法直接进行AcceptChanges 的 要排除掉。
        private IEnumerable<ObjectDataInfo> getCanChanges() {
            //对象的基本信息有些时候是以Datarow 的格式存储
            return this.Values.Where<ObjectDataInfo>
                (cc => cc.ObjectData != null && Array.IndexOf<string>(_ExcludeDataStateChanged, 
                 cc.ObjectData.GetType().Name) >= 0);
        }
        //获取DataInfo 存储的key 值字符窜。
        private string toSaveKeyString(ObjectDataInfo dataInfo) {
            string keyString = dataInfo.GetHashCode().ToString() ;
            if (dataInfo.DataInDocType != null)
                keyString += " " + dataInfo.DataInDocType.ToString();
            System.TimeSpan t1 = dataInfo.SaveToCacheDateTime.Subtract(System.DateTime.MinValue);
            keyString += SPE_CHAR + dataInfo.DataState.ToString() + SPE_CHAR + t1.TotalMilliseconds;
            return keyString;
        }
        #endregion 内部函数处理...
    }
}
