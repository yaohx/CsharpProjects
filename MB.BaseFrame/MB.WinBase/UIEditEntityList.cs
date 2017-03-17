//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-23
// Description	:	UI 层 实体对象编辑明细处理。。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MB.WinBase {
    /// <summary>
    /// UI 层 实体对象编辑明细处理。
    /// </summary>
    public class UIEditEntityList: BindingList<KeyValuePair<int, object>> {

        #region 自定义事件处理相关...
        private System.EventHandler _BeforeDataSave;
        /// <summary>
        /// 在数据保存之前产生。
        /// </summary>
        public event System.EventHandler BeforeDataSave {
            add {
                _BeforeDataSave += value; 
            }
            remove {
                _BeforeDataSave -= value;
            }
        }
        protected virtual void OnBeforeDataSave(System.EventArgs arg) {
            if (_BeforeDataSave != null)
                _BeforeDataSave(this, arg);
        }
        #endregion 自定义事件处理相关...

        #region 扩展的Public 成员...
        /// <summary>
        /// 抛出数据保存之前的事件。
        /// </summary>
        public void RaiseBeforeDataSave() {
            OnBeforeDataSave(new EventArgs());
        }

        /// <summary>
        /// 以相同的状态改变数据实体
        /// </summary>
        public void AcceptChanges() {
            this.RaiseListChangedEvents = false;
            foreach (KeyValuePair<int, object> info in this) {
                MB.Util.Model.EntityState entityState = MB.WinBase.UIDataEditHelper.Instance.GetEntityState(info.Value);
                if (entityState == MB.Util.Model.EntityState.New || entityState == MB.Util.Model.EntityState.Modified) {
                    MB.WinBase.UIDataEditHelper.Instance.SetEntityState(info.Value, MB.Util.Model.EntityState.Persistent);
                }

                //需要处理删除的情况
            }
            this.RaiseListChangedEvents = true;
        }
        /// <summary>
        /// 在新增的时候如果存在相同的那么就把已存在的删除掉。
        /// </summary>
        /// <param name="item"></param>
        public void AddAndDeleteEquals(KeyValuePair<int, object> item) {
            var lst = this.Where(o => o.Key == item.Key && object.Equals(o.Value, item.Value));
            if (lst.Count<KeyValuePair<int, object>>() > 0) {
                List<KeyValuePair<int, object>> dels = new List<KeyValuePair<int, object>>(lst);
                foreach (var l in dels)
                    this.Remove(l);
            }

            this.Add(item);
        }

        /// <summary>
        /// 根据数据类型删除集合中存储的数据。
        /// edit by chendc 2010-08-19
        /// </summary>
        /// <param name="classType"></param>
        public void ClearByClassType(int classType) {
            var lst = this.Where(o => o.Key == classType);
            foreach (var l in lst)
                this.Remove(l);

        }
        #endregion 扩展的Public 成员...
    }


}
