//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-03-11
// Description	:	缺省默认UI 层 数据编辑 公共处理方法。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.BaseFrame;

namespace MB.WinBase {
    /// <summary>
    /// 缺省默认UI 层 数据编辑 公共处理方法。
    /// </summary>
    public class UIDataEditHelper {
        public static readonly string ENTITY_STATE_PROPERTY = "EntityState";
        public static readonly string ENTITY_DOC_STATE = "DOC_STATE";
        public static readonly string ENTITY_CREATE_USER = "CREATE_USER";
        public static readonly string ENTITY_LAST_MODIFIED_USER = "LAST_MODIFIED_USER";


        #region Instance...
        private static Object _Obj = new object();
        private static UIDataEditHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected UIDataEditHelper() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static UIDataEditHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new UIDataEditHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...
        /// <summary>
        /// 在编辑实体中追加登录用户的信息。
        /// </summary>
        /// <param name="editEntity"></param>
        public void AppendLoginUserInfo(object editEntity) {
            bool exists = MB.Util.MyReflection.Instance.CheckObjectExistsProperty(editEntity, ENTITY_CREATE_USER);
            if (!exists) return;
            if (AppEnvironmentSetting.Instance.CurrentLoginUserInfo == null) return;
            var state = GetEntityState(editEntity);
            var userInfo = AppEnvironmentSetting.Instance.CurrentLoginUserInfo;
            string userCode = string.IsNullOrEmpty(userInfo.USER_CODE) ? userInfo.USER_ID : userInfo.USER_CODE;  
            if (state == MB.Util.Model.EntityState.New) {
                MB.Util.MyReflection.Instance.InvokePropertyForSet(editEntity, ENTITY_CREATE_USER, userCode);
                MB.Util.MyReflection.Instance.InvokePropertyForSet(editEntity, ENTITY_LAST_MODIFIED_USER, userCode);
            }
            else if (state == MB.Util.Model.EntityState.Modified) {
                MB.Util.MyReflection.Instance.InvokePropertyForSet(editEntity, ENTITY_LAST_MODIFIED_USER, userCode);
            }
        }

        /// <summary>
        /// 把数据编辑到符合数据存储的集合类中。
        /// </summary>
        /// <param name="detailEditEntitys"></param>
        /// <param name="dataInDocType"></param>
        /// <param name="lstData"></param>
        public void BuildEditEntitys(List<KeyValuePair<int, object>> detailEditEntitys, int dataInDocType, IList lstData) {
            foreach (object entity in lstData) {
                var val = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, ENTITY_STATE_PROPERTY);
                if (val == null)
                    throw new MB.Util.APPException(string.Format(" {0} 不是系统有效的主表实体对象,系统所认识的实体对象至少包含 {1} 属性！", entity.GetType().FullName, ENTITY_STATE_PROPERTY));

                MB.Util.Model.EntityState state = (MB.Util.Model.EntityState)Enum.Parse(typeof(MB.Util.Model.EntityState), val.ToString());
                if (state == MB.Util.Model.EntityState.New || state == MB.Util.Model.EntityState.Modified) {
                    detailEditEntitys.Add(new KeyValuePair<int, object>(dataInDocType, entity));
                }
            }
        }
        /// <summary>
        /// 比较两个数据实体的值。修改不同的部分。
        /// </summary>
        /// <param name="orgEntity">源数据</param>
        /// <param name="destEntity">目标数据 被更新</param>
        public void MergeChangedValue(object orgEntity, object destEntity) {
            if (orgEntity == null || destEntity == null) return;
            System.Reflection.PropertyInfo[] proInfos = destEntity.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo info in proInfos) {
                if (info.IsSpecialName || !info.CanRead || !info.CanWrite) continue;
                object destVal = info.GetValue(orgEntity, null);
                object orgVal = MB.Util.MyReflection.Instance.InvokePropertyForGet(orgEntity, info.Name);
                if (!object.Equals(orgVal, destVal)) {
                    info.SetValue(destEntity, orgVal, null); 
                }
            }

        }
        /// <summary>
        /// 判断当前的数据实体对象正在编辑状态。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool CheckCurrentEntityIsInEditting(object entity) {
             MB.WinBase.Common.ObjectState objectState = MB.WinBase.UIDataEditHelper.Instance.GetObjectState(entity);
             return (objectState == MB.WinBase.Common.ObjectState.Modified || objectState == MB.WinBase.Common.ObjectState.New);
        }
        #region 状态处理相关...
        /// <summary>
        /// 获取当前编辑单据的状态。
        /// 特殊说明： 在UI 层操作时 由于该方法要频繁进行调用，所以把它实现在UI层 而非作为服务来提供。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public MB.WinBase.Common.ObjectState GetObjectState(object entity) {
            var val = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, ENTITY_STATE_PROPERTY);
            if (val == null)
                throw new MB.Util.APPException( string.Format(" {0} 不是系统有效的主表实体对象,系统所认识的实体对象至少包含 {1} 属性！",entity.GetType().FullName,ENTITY_STATE_PROPERTY) );

            MB.Util.Model.EntityState state = (MB.Util.Model.EntityState)Enum.Parse(typeof(MB.Util.Model.EntityState), val.ToString());

            if (state == MB.Util.Model.EntityState.New)
                return MB.WinBase.Common.ObjectState.New;
            else if (state == MB.Util.Model.EntityState.Deleted)
                return MB.WinBase.Common.ObjectState.Deleted;
            else if (state == MB.Util.Model.EntityState.Modified)
                return MB.WinBase.Common.ObjectState.Modified;
            else if (state == MB.Util.Model.EntityState.Persistent) {
                bool existsDocState = MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, ENTITY_DOC_STATE);
                if (!existsDocState) {
                    return MB.WinBase.Common.ObjectState.Unchanged;
                }
                
                MB.Util.Model.DocState docState = GetEntityDocState(entity);
                if ((int)docState >= MB.BaseFrame.SOD.OVER_DOC_STATE_LIMIT)
                    return Common.ObjectState.OverDocState;

                switch (docState) {
                    case MB.Util.Model.DocState.Progress:
                        return MB.WinBase.Common.ObjectState.Unchanged;
                    case MB.Util.Model.DocState.Validated:
                        return MB.WinBase.Common.ObjectState.Validated;
                    case MB.Util.Model.DocState.Approved:
                        return MB.WinBase.Common.ObjectState.Approved;
                    case MB.Util.Model.DocState.Completed:
                        return MB.WinBase.Common.ObjectState.Completed;
                    case MB.Util.Model.DocState.Suspended :
                        return MB.WinBase.Common.ObjectState.Suspended;
                    case MB.Util.Model.DocState.Withdraw:
                        return MB.WinBase.Common.ObjectState.Withdraw;
                        
                    default:
                       throw new MB.Util.APPException(string.Format("从DocState 转换到ObjectState 时，对于DocState 还没有进行处理！", docState.ToString()));
                }
            }
            else if (state == MB.Util.Model.EntityState.Transient)
                return MB.WinBase.Common.ObjectState.None;
            else {
                throw new MB.Util.APPException("根据主表的实体对象转换为所认识的对象状态是有误，请检查EntityState 和 DocState!");
            }
        }
        /// <summary>
        /// 获取实体对象的单据状态。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public MB.Util.Model.DocState GetEntityDocState(object entity) {
            object val2 = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, ENTITY_DOC_STATE);
            MB.Util.Model.DocState docState = (MB.Util.Model.DocState)Enum.Parse(typeof(MB.Util.Model.DocState), val2.ToString());
            return docState;
        }
        /// <summary>
        /// 判断指定的类型中是否包含单据状态类型。
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public bool CheckTypeExistsDocState(Type entityType) {
            return MB.Util.MyReflection.Instance.CheckTypeExistsProperty(entityType, ENTITY_DOC_STATE); 
        }
        /// <summary>
        /// 判断实体对象的是否存在单据状态。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool CheckExistsDocState(object entity) {
            return MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, ENTITY_DOC_STATE); 
        }
        /// <summary>
        /// 判断实体对象的是否存在单据状态。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool CheckExistsEntityState(object entity) {
            return MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, ENTITY_STATE_PROPERTY);
        }
        /// <summary>
        /// 获取实体对象的ID.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int GetEntityID(object entity) {
            if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, SOD.OBJECT_PROPERTY_ID))
                throw new MB.Util.APPException(string.Format("请检查{0} 是否存在主键{1}", entity.GetType().FullName, SOD.OBJECT_PROPERTY_ID));

            return (int)MB.Util.MyReflection.Instance.InvokePropertyForGet(entity,SOD.OBJECT_PROPERTY_ID);
        }
        /// <summary>
        /// 获取实体对象的状态。
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public MB.Util.Model.EntityState GetEntityState(object entity) {
            var val = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, ENTITY_STATE_PROPERTY);
            if (val == null)
                throw new MB.Util.APPException(string.Format(" {0} 不是系统有效的主表实体对象,系统所认识的实体对象至少包含 {1} 属性！", entity.GetType().FullName, ENTITY_STATE_PROPERTY));

            MB.Util.Model.EntityState state = (MB.Util.Model.EntityState)Enum.Parse(typeof(MB.Util.Model.EntityState), val.ToString());
            return state;
        }
        /// <summary>
        /// 设置实体对象的状态。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="state"></param>
        public void SetEntityState(object entity,MB.Util.Model.EntityState state) {
            Type t = entity.GetType();
            System.Reflection.PropertyInfo proInfo = t.GetProperty(ENTITY_STATE_PROPERTY);
            if(proInfo==null)
                throw new MB.Util.APPException(string.Format(" {0} 不是系统有效的主表实体对象,系统所认识的实体对象至少包含 {1} 属性！", entity.GetType().FullName, ENTITY_STATE_PROPERTY));

            //由于WCF 客户端代理引用后生产的枚举类型不一样，需要进行转换。
            //以后可以把 MB.Util.Model.EntityState 注册为 WCF 所认识的类型，那么就不需要进行这样的转换。
            try {
                object val = Enum.Parse(proInfo.PropertyType, state.ToString());
                MB.Util.MyReflection.Instance.InvokePropertyForSet(entity, ENTITY_STATE_PROPERTY, val);
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex,"可能是MB.Util.Model.EntityState 中增加了新的类型值，但客户端没有重新刷新引用引起。");
            }
        }

        #endregion 状态处理相关...

    }
}
