//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-02
// Description	:	所有数据对象必须继承的抽象基类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

//using MB.Orm.Enums;
namespace MB.Orm.Common {
    /// <summary>
    /// 所有数据对象必须继承的抽象基类。
    /// </summary>
    [Serializable]
    [DataContract]
    public abstract class BaseModel {

        #region 变量定义...
        //实体对象状态。
        private MB.Util.Model.EntityState _EntityState;
        private bool _Selected;
        #endregion 变量定义...

        #region public 属性...
        /// <summary>
        /// 当前实体的状态。
        /// </summary>
        [DataMember]
        public MB.Util.Model.EntityState EntityState {
            get {
                return _EntityState;
            }
            set {
                _EntityState = value;
            }
        }
        /// <summary>
        /// 实体对象模型的附加属性。
        /// 判断当前实体对象是否在选中状态。
        /// </summary>
        [DataMember]
        public bool Selected {
            get {
                return _Selected;
            }
            set {
                _Selected = value;
            }
        }
        #endregion public 属性...

    }
}
