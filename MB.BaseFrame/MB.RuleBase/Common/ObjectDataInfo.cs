//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-02
// Description	:	ObjectDataInfo 在数据存储处理中涉及到的数据对象。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MB.Orm.Enums;
namespace MB.RuleBase.Common {
    /// <summary>
    /// ObjectDataInfo 在数据存储处理中涉及到的数据对象。
    /// </summary>
    public class ObjectDataInfo {
        private object _ObjectData;
        private ObjectDataState _DataState;//数据存储的状态
        private object _DataInDocType;
        private int _SaveOrderIndex;//数据存储的顺序号

        private string[] _SavePropertys; //需要进行本次存储的实体属性。
        private DateTime _SaveToCacheDateTime;//存储到Cache 中的时间戳

        private string _ExecuteXmlCfgSqlName;//配置的XML 执行SQL 名称
        private object _Tag;//附加存储的数据。

        #region 构造函数...
        /// <summary>
        /// 构造函数...
        /// </summary>
        public ObjectDataInfo(object dataInDocType, object objectData) {
            _DataInDocType = dataInDocType;
            _ObjectData = objectData;
            _SaveOrderIndex = 0;
        }
        #endregion 构造函数...


        #region public 属性...
        /// <summary>
        /// 该数据在单据定义的数据类型。
        /// </summary>
        public object DataInDocType {
            get {
                return _DataInDocType;
            }
            set {
                _DataInDocType = value;
            }
        }

        /// <summary>
        /// 对象数据
        /// </summary>
        public object ObjectData {
            get {
                return _ObjectData;
            }
            set {
                _ObjectData = value;
            }
        }
        /// <summary>
        /// 对象数据在集合中的状态
        /// </summary>
        public ObjectDataState DataState {
            get {
                return _DataState;
            }
            set {
                _DataState = value;
            }
        }
        /// <summary>
        /// 数据存储的顺序号
        /// </summary>
        public int SaveOrderIndex {
            get {
                return _SaveOrderIndex;
            }
            set {
                _SaveOrderIndex = value;
            }
        }
        /// <summary>
        /// 存储到Cache 中的时间戳,有时候需要对它进行排序，以决定存储的先后顺序。
        /// </summary>
        public DateTime SaveToCacheDateTime {
            get {
                return _SaveToCacheDateTime;
            }
            set {
                _SaveToCacheDateTime = value;
            }
        }
        /// <summary>
        /// 本次实体对象存储的属性信息。
        /// </summary>
        public string[] SavePropertys {
            get {
                return _SavePropertys;
            }
            set {
                _SavePropertys = value;
            }
        }
        /// <summary>
        ///  配置的XML 执行SQL 名称.
        /// </summary>
        public string ExecuteXmlCfgSqlName {
            get {
                return _ExecuteXmlCfgSqlName;
            }
            set {
                _ExecuteXmlCfgSqlName = value;
            }
        }
        /// <summary>
        /// 附加存储的数据。
        /// </summary>
        public object Tag {
            get {
                return _Tag;
            }
            set {
                _Tag = value;
            }
        }

        #endregion public 属性...
    }
}
