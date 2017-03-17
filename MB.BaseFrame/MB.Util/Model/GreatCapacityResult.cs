//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	 2010-02-20
// Description	:	大数据量获取返回值。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections;

namespace MB.Util.Model {
    /// <summary>
    /// 大数据量获取返回值。
    /// </summary>
    [DataContract]
    public class GreatCapacityResult {
        /// <summary>
        /// 大数据量获取返回值。
        /// </summary>
        public GreatCapacityResult() {

        }
        /// <summary>
        /// 大数据量获取返回值。
        /// 初始化时调用。
        /// </summary>
        /// <param name="maxSegment">最大的数据块</param>
        public GreatCapacityResult(int maxSegment) {
            _MaxSegment = maxSegment;
        }
        /// <summary>
        ///  大数据量获取返回值。
        ///  根据Index 得到返回值时调用。
        /// </summary>
        /// <param name="segmentData">当前得到的集合</param>
        public GreatCapacityResult(IList segmentData) {
            _SegmentData = segmentData;
        }
        private int _MaxSegment;
        /// <summary>
        /// 当前获取数据的最大数据块。
        /// </summary>
        [DataMember]
        public int MaxSegment {
            get { return _MaxSegment; }
            set { _MaxSegment = value; }
        }
        private IList _SegmentData;
        /// <summary>
        /// 根据Index 返回的数据集合。
        /// </summary>
        [DataMember]
        public IList SegmentData {
            get { return _SegmentData; }
            set { _SegmentData = value; }
        }

    }
}
