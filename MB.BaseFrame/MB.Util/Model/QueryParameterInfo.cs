//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-23
// Description	:	数据查询过滤属性定义。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.Util.Model
{
    /// <summary>
    /// 数据查询过滤属性定义。
    /// 该实体类 的系列化有专门的方法来处理 。
    /// </summary>
    [DataContract]
    public class QueryParameterInfo
    {
        #region 定义...
        private string _PropertyName;
        private string _Description;
        private object _Value;
        private DataFilterConditions _Condition;
        private object _Value2;
        private string _DataTypeName;
        private int _OrderIndex;
        private bool _Limited;
        private bool _IsGroupNode; //判断是否为分组节点
        private QueryGroupLinkType _GroupNodeLinkType;
        private List<QueryParameterInfo> _Childs;
        private bool _MultiValue; //判断Value 是否为多个值，不同值之间用逗号分开
        #endregion 定义...

        #region 构造函数...
        /// <summary>
        /// 创建需要进行过滤查询的字段描述信息。
        /// </summary>
        public QueryParameterInfo()
        {
        }
        /// <summary>
        /// 新创建一个分组节点。
        /// </summary>
        /// <param name="groupNodeLinkType">分组节点类型</param>
        public QueryParameterInfo(QueryGroupLinkType groupNodeLinkType)
        {
            _Childs = new List<QueryParameterInfo>();
            _IsGroupNode = true;
            _GroupNodeLinkType = groupNodeLinkType;
        }
        /// <summary>
        /// 创建需要进行过滤查询的字段描述信息。
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="queryValue"></param>
        /// <param name="filterCondition"></param>
        public QueryParameterInfo(string propertyName, object queryValue, DataFilterConditions filterCondition)
            : this(propertyName, queryValue, filterCondition, false)
        {

        }
        /// <summary>
        /// 创建需要进行过滤查询的字段描述信息。
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="queryValue"></param>
        /// <param name="filterCondition">过滤条件</param>
        /// <param name="limited">是否为限制列</param>
        public QueryParameterInfo(string propertyName, object queryValue, DataFilterConditions filterCondition, bool limited)
        {
            _PropertyName = propertyName;
            _Value = queryValue;
            _Condition = filterCondition;
            _Limited = limited;
        }
        #endregion 构造函数...

        #region Public 属性...
        /// <summary>
        /// 属性的完整名称包含类名称和属性名称。
        /// </summary>
        [DataMember]
        public string PropertyName
        {
            get
            {
                return _PropertyName;
            }
            set
            {
                _PropertyName = value;
            }
        }
        /// <summary>
        /// 查询对应字段或者属性的描述。
        /// </summary>
        [DataMember]
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }
        /// <summary>
        /// 需要进行查询比较的值。
        /// </summary>
        [DataMember]
        public object Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }
        /// <summary>
        /// 需要查询进行比较的值。 
        /// 只在查询在什么和什么之间的时候有用。
        /// </summary>
        [DataMember]
        public object Value2
        {
            get
            {
                return _Value2;
            }
            set
            {
                _Value2 = value;
            }
        }
        /// <summary>
        /// 查询条件。
        /// </summary>
        [DataMember]
        public DataFilterConditions Condition
        {
            get
            {
                return _Condition;
            }
            set
            {
                _Condition = value;
            }
        }
        /// <summary>
        /// 查询值的类型。
        /// </summary>
        [DataMember]
        public string DataType
        {
            get
            {
                return _DataTypeName;
            }
            set
            {
                _DataTypeName = value;
            }
        }
        /// <summary>
        /// 排序顺序。
        /// </summary>
        [DataMember]
        public int OrderIndex
        {
            get
            {
                return _OrderIndex;
            }
            set
            {
                _OrderIndex = value;
            }
        }
        /// <summary>
        /// 判断该参数是否做为限定的参数，限制的参数将不发送给数据库服务器。
        /// </summary>
        [DataMember]
        public bool Limited
        {
            get
            {
                return _Limited;
            }
            set
            {
                _Limited = value;
            }
        }
        /// <summary>
        /// 判断是否为分组节电。
        /// </summary>
        [DataMember]
        public bool IsGroupNode
        {
            get
            {
                return _IsGroupNode;
            }
            set
            {
                _IsGroupNode = value;
            }
        }
        /// <summary>
        /// 判断Value 是否为多个值的组合，不同值之间用逗号分开。
        /// </summary>
        [DataMember]
        public bool MultiValue
        {
            get
            {
                return _MultiValue;
            }
            set
            {
                _MultiValue = value;
            }
        }
        /// <summary>
        /// 分组内节点连接类型。
        /// </summary>
        [DataMember]
        public QueryGroupLinkType GroupNodeLinkType
        {
            get
            {
                return _GroupNodeLinkType;
            }
            set
            {
                _GroupNodeLinkType = value;
            }
        }
        /// <summary>
        /// 分组内子节点。
        /// </summary>
        [DataMember]
        public List<QueryParameterInfo> Childs
        {
            get
            {
                return _Childs;
            }
            set
            {
                _Childs = value;
            }
        }

        #endregion Public 属性...
    }

    /// <summary>
    /// 分组节点类型。
    /// </summary>
    public enum QueryGroupLinkType
    {
        /// <summary>
        /// 组内的所有节点以 AND 关联。
        /// </summary>
        AND,
        /// <summary>
        /// 组内的所有节点以 OR 进行关联。
        /// </summary>
        OR,
        /// <summary>
        /// 组内的所有节点以 Not AND 关联。
        /// </summary>
        AndNot,
        /// <summary>
        /// 组内的所有节点以 Not OR 进行关联。
        /// </summary>
        OrNot
    }
}
