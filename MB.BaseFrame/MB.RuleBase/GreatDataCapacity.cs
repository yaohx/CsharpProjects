//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2010-02-20
// Description	:	默认的大数据量数据获取解决方案。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;

namespace MB.RuleBase {
   
    /// <summary>
    /// 缺省大数据量数据获取解决方案。
    /// 大数据量获取都必须采取
    /// </summary>
    public class DefaultGreatDataCapacity{
        private const int SINGLE_BUFFER_COUNT = 500; //单次获取的数据量
        private int _SingleSegmentCapacity;  //单次获取的数据量
        private IList _BufferData;
        /// <summary>
        /// 构造当前session 需要进行大数据量处理的容器。
        /// 默认情况下单的数据块的大小为500
        /// </summary>
        /// <param name="listData">初始化需要进行处理的数据</param>
        public DefaultGreatDataCapacity(IList listData) {
            _SingleSegmentCapacity = SINGLE_BUFFER_COUNT;
            _BufferData = listData;
        }
        /// <summary>
        /// 构造当前session 需要进行大数据量处理的容器。
        /// </summary>
        /// <param name="listData">初始化需要进行处理的数据</param>
        /// <param name="singleSegmentCapacity">单的数据块的大小</param>
        public DefaultGreatDataCapacity(IList listData,int singleSegmentCapacity) {
            _SingleSegmentCapacity = singleSegmentCapacity;
            _BufferData = listData;
        }
        /// <summary>
        /// 获取数据块的个数。
        /// </summary>
        /// <returns></returns>
        public int MaxSegment() {
            int c = 0;
            int re = Math.DivRem(_BufferData.Count,  _SingleSegmentCapacity, out c);
           return  System.Convert.ToInt32(c > 0 ? re + 1 : re);
        }

        /// <summary>
        /// 根据Index 获取数据。
        /// </summary>
        /// <param name="index">顺序号</param>
        /// <returns>List</returns>
        public IList GetDataByIndex(int index) {
           
            ArrayList reData = new ArrayList();
            int endPosition = (index + 1) * _SingleSegmentCapacity < _BufferData.Count ?  (index + 1) * _SingleSegmentCapacity : _BufferData.Count;

            for (int i = index * _SingleSegmentCapacity; i < endPosition; i++) {
                reData.Add(_BufferData[i]);
            }
            return reData;
        }
   
    }
    
}
