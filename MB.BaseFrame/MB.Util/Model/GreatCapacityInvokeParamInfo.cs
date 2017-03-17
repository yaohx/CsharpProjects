//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	 2010-02-20
// Description	:	大数据调用的参数描述信息。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.Util.Model {
    /// <summary>
    /// 大数据调用的参数描述信息。
    /// </summary>
    [DataContract]
    public class GreatCapacityInvokeParamInfo {
        /// <summary>
        /// 在获取数据时传入的参数。
        /// 默认情况下IsBeginWorking = false;
        /// </summary>
        /// <param name="dataIndex">数据块Index</param>
        public GreatCapacityInvokeParamInfo(int dataIndex) {
            _DataIndex = dataIndex;
            _IsBeginWorking = false;
        }
        /// <summary>
        /// 异步调用输入的参数。
        /// 初始化执行时调用。
        /// 默认情况下IsBeginWorking = true;
        /// </summary>
        /// <param name="dataInDocType">数据在单据定义中的类型</param>
        /// <param name="xmlFilterParams">过滤的参数</param>
        public GreatCapacityInvokeParamInfo(int dataInDocType, string xmlFilterParams) {
            _IsBeginWorking = true;
            _DataInDocType = dataInDocType;
            _XmlFilterParams = xmlFilterParams;
        }
        private bool _IsBeginWorking;
        /// <summary>
        /// 判断是否为初始化执行操作。
        /// 初始化执行操作时将从数据库中获取数据并根据默认的块容量进行计算。
        /// </summary>
        [DataMember]
        public bool IsBeginWorking {
            get { return _IsBeginWorking; }
            set { _IsBeginWorking = value; }
        }
        private int _DataIndex;
        /// <summary>
        /// 执行获取数据的偏移量（dataIndex * 单个数据块大小）。
        /// </summary>
        [DataMember]
        public int DataIndex {
            get { return _DataIndex; }
            set { _DataIndex = value; }
        }
        private int _DataInDocType;
        /// <summary>
        /// 当前获取数据在单据中的数据类型(兼容单据处理默认提供的方法)。
        /// </summary>
        [DataMember]
        public int DataInDocType {
            get { return _DataInDocType; }
            set { _DataInDocType = value; }
        }
        private string[] _ParamValues;
        /// <summary>
        /// 以值的方式传递参数。
        /// </summary>
        [DataMember]
        public string[] ParamValues {
            get { return _ParamValues; }
            set { _ParamValues = value; }
        }
        private string _XmlFilterParams;
        /// <summary>
        /// MB.Util.Model.QueryParameterInfo[] 系列化后的XML 参数字符窜。
        /// </summary>
        [DataMember]
        public string XmlFilterParams {
            get { return _XmlFilterParams; }
            set { _XmlFilterParams = value; }
        }

    }

}
