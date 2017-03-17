//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-12-17
// Description	:	AbstractGridEditRule: 扩展的实现基于Mdi 子窗口网格编辑界面的业务实现类。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.WinClientDefault.Extender {
    /// <summary>
    /// 扩展的实现基于Mdi 子窗口网格编辑界面的业务实现类。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TChannel"></typeparam>
    public abstract class AbstractGridEditRule<T, TChannel> : MB.WinBase.AbstractClientRule<T, TChannel>, IGridViewEditRule
                                        where TChannel : class, IDisposable {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainDataTypeInDoc"></param>
        public AbstractGridEditRule(object mainDataTypeInDoc)
                                : base(mainDataTypeInDoc) {


        }




        #region IGridViewEditRule 成员

        public void CreateDataBinding(DevExpress.XtraGrid.GridControl grdCtl, IList dataSource) {
             
        }

        #endregion
    }

    /// <summary>
    /// 网格业务类必须要实现的接口。
    /// </summary>
    public interface IGridViewEditRule {
        /// <summary>
        /// 创建数据绑订
        /// </summary>
        /// <param name="grdCtl"></param>
        /// <param name="dataSource"></param>
        void CreateDataBinding(DevExpress.XtraGrid.GridControl grdCtl, IList dataSource);
    }
}
