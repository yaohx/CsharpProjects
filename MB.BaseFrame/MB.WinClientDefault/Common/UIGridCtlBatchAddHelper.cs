//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-17
// Description	:	UI 层 网格批量增加处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using MB.WinBase.IFace;
using MB.WinBase.Common;
using System.Windows.Controls;
using System.Collections;
namespace MB.WinClientDefault.Common
{
    

    /// <summary>
    /// UI 层 网格批量增加处理相关。
    /// </summary>
    public class UIGridCtlBatchAddHelper<T> where T : class
    {
        private MB.WinBase.IFace.IForm _EditForm;
        private Dictionary<string, ColumnEditCfgInfo> _EditCols;
        private BindingList<T> _DetailEntitys;
        private int _dataInDocType;
        private ColumnEditCfgInfo _ColumnEditCfgInfo;
        private const int SHOW_MSG_COUNT = 5000;//判断是否显示消息的最大数量
        private bool _IsEntityBatchCreatedFromServer; //批量新增时, 是不是批量从服务端创建对象

        /// <summary>
        /// 批量新增时, 是不是批量从服务端创建对象
        /// </summary>
        public bool IsEntityBatchCreatedFromServer {
            get { return _IsEntityBatchCreatedFromServer; }
            set { _IsEntityBatchCreatedFromServer = value; }
        }

        #region 当批量新增从数据小助手返回数据时触发
        private System.EventHandler<AfterGetDataFromDataAssistanceEventArgs<T>> _AfterGetDataFromDataAssistance;
        /// <summary>
        /// 当entity从服务端批量新增时从数据小助手返回数据时触发
        /// </summary>
        public event System.EventHandler<AfterGetDataFromDataAssistanceEventArgs<T>> AfterGetDataFromDataAssistance {
            add {
                _AfterGetDataFromDataAssistance += value;
            }
            remove {
                _AfterGetDataFromDataAssistance -= value;
            }
        }
        private void onAfterGetDataFromDataAssistance(AfterGetDataFromDataAssistanceEventArgs<T> arg) {
            if (_AfterGetDataFromDataAssistance != null)
                _AfterGetDataFromDataAssistance(this, arg);
        }
        #endregion


        /// <summary>
        /// 构造批量新增
        /// </summary>
        /// <param name="editForm">当前编辑的form实例</param>
        /// <param name="editCols">当前的编辑列的配置信息</param>
        /// <param name="detailEntitys">当前明细的绑定</param>
        /// <param name="dataInDocType">当前明细所对应的DocType值</param>
        public UIGridCtlBatchAddHelper(MB.WinBase.IFace.IForm editForm, Dictionary<string, ColumnEditCfgInfo> editCols, BindingList<T> detailEntitys, int dataInDocType) {
            _EditForm = editForm;
            _EditCols = editCols;
            _DetailEntitys = detailEntitys;
            _dataInDocType = dataInDocType;
            _IsEntityBatchCreatedFromServer = false;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="editForm"></param>
        /// <param name="editCols"></param>
        /// <param name="detailEntitys"></param>
        /// <param name="dataInDocType"></param>
        public void ShowBatchAppendDataToGrid() {
            if (_EditCols == null || _EditCols.Values.Count == 0) return;
            _ColumnEditCfgInfo = _EditCols.Values.FirstOrDefault(o => o.DefaultBatchAdd == true);
            if (_ColumnEditCfgInfo == null) {
                throw new MB.Util.APPException("在批量增加时，在对应的XML 配置文件中没发现配置DefaultBatchAdd 为True 的配置项！", MB.Util.APPMessageType.DisplayToUser);
            }

            System.Windows.Forms.Form parentForm = _EditForm as System.Windows.Forms.Form;
            System.Windows.Forms.Control parentHoster = MB.WinBase.ShareLib.Instance.GetInvokeDataHosterControl(parentForm);
            MB.WinBase.IFace.IDataAssistant dataAssistant = null;
            object mainEntity = null;
            if (parentHoster != null)
            {
                dataAssistant = MB.WinBase.ObjectDataFilterAssistantHelper.Instance.CreateDataAssistantObject(this, _ColumnEditCfgInfo, parentForm);
            }
            else {
                mainEntity = _DetailEntitys.AddNew();

                dataAssistant = MB.WinBase.ObjectDataFilterAssistantHelper.Instance.CreateDataAssistantObject(this, mainEntity, _ColumnEditCfgInfo, _EditForm.ClientRuleObject);             
            }
            
            if (dataAssistant != null) {
                dataAssistant.MultiSelect = true;
                dataAssistant.MaxSelectRows = _ColumnEditCfgInfo.MaxSelectRows;
                dataAssistant.AfterGetObjectData += new GetObjectDataAssistantEventHandle(dataAssistant_AfterGetObjectData);
                (dataAssistant as System.Windows.Forms.Form).ShowDialog();
            }

            if (mainEntity != null)
            {
                bool exists = MB.WinBase.UIDataEditHelper.Instance.CheckExistsEntityState(mainEntity);
                if (exists) MB.WinBase.UIDataEditHelper.Instance.SetEntityState(mainEntity, Util.Model.EntityState.Deleted);

                _DetailEntitys.Remove((T)mainEntity);
            }
        }

        void dataAssistant_AfterGetObjectData(object sender, GetObjectDataAssistantEventArgs arg) {
            if (arg.SelectedRows == null || arg.SelectedRows.Length == 0) return;
            if (arg.SelectedRows.Length > SHOW_MSG_COUNT) {
                var re = MB.WinBase.MessageBoxEx.Question(string.Format("当前需要批量增加的数据量有{0}行,请分批导入,继续将花费几十秒的时间.是否继续?", arg.SelectedRows.Length));
                if (re != System.Windows.Forms.DialogResult.Yes) return;

            }
            try {
                //edit by chendc 2010-07-08 性能优化调整
                Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> orgAcs = new Dictionary<string, Util.Emit.DynamicPropertyAccessor>();
                Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> desAcs = new Dictionary<string, Util.Emit.DynamicPropertyAccessor>();
                
                object orgEntity = arg.SelectedRows[0];

                foreach (MB.WinBase.Common.EditCtlDataMappingInfo info in _ColumnEditCfgInfo.EditCtlDataMappings) {
                    if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(orgEntity, info.SourceColumnName)) {
                        MB.Util.TraceEx.Write(string.Format("源数据中不包含属性{0}", info.SourceColumnName));
                        continue;
                    }
                    orgAcs.Add(info.ColumnName, new Util.Emit.DynamicPropertyAccessor(orgEntity.GetType(), info.SourceColumnName));
                    desAcs.Add(info.ColumnName, new Util.Emit.DynamicPropertyAccessor(typeof(T), info.ColumnName));
                }

                List<T> listFromDataAssistance = new List<T>(); //从数据小助手获取的数据列表。
                IClientRule uiRule = _EditForm.ClientRuleObject as IClientRule;
                IList newList = null;
                if (_IsEntityBatchCreatedFromServer)
                    newList = uiRule.CreateNewEntityBatch(_dataInDocType, arg.SelectedRows.Length); //从服务端批量新增对象, 为了获取服务端赋予对象的默认值
                int newListIndex = 0;
                foreach (object row in arg.SelectedRows) {
                    
                    //这个AddNew 主要是产生一个AddNew的事件， 通过这个事件从服务器端返回一个新的对象并把
                    //该集合中创建的对象替换掉，在真正使用中 如果存在性能的问题，可以修改为从本地创建。
                    //T newEntity = _DetailEntitys.AddNew();
                    T newEntity = null;
                    if (_IsEntityBatchCreatedFromServer) {
                        newEntity = (T)newList[newListIndex];
                        listFromDataAssistance.Add(newEntity);
                    }
                    else
                        newEntity = _DetailEntitys.AddNew();

                    foreach (var acKey in orgAcs.Keys) {
                        object val = orgAcs[acKey].Get(row);
                        if (val == null) continue;

                        desAcs[acKey].Set(newEntity, val);
                    }
                    //foreach (MB.WinBase.Common.EditCtlDataMappingInfo info in _ColumnEditCfgInfo.EditCtlDataMappings) {
                    //    object dataObject = row;

                    //    if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(dataObject, info.SourceColumnName)) {
                    //        MB.Util.TraceEx.Write(string.Format("源数据中不包含属性{0}", info.SourceColumnName));
                    //        continue;
                    //    }
                    //    object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(dataObject, info.SourceColumnName);
                    //    if (val == null) continue;

                    //    MB.Util.MyReflection.Instance.InvokePropertyForSet(newEntity, info.ColumnName, val.ToString());

                    //},
                    newListIndex++;
                }
                
                if (_IsEntityBatchCreatedFromServer)
                    onAfterGetDataFromDataAssistance(new AfterGetDataFromDataAssistanceEventArgs<T>(listFromDataAssistance.ToArray()));


            }
            catch (Exception ex) {
                throw new MB.Util.APPException("批量增加数据有误,请重试！", MB.Util.APPMessageType.DisplayToUser, ex);
            }
        }


    }

    /// <summary>
    /// 从数据小助手批量选择数据以后的参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AfterGetDataFromDataAssistanceEventArgs<T> : System.EventArgs {
        private T[] _SelectedItems;

        public AfterGetDataFromDataAssistanceEventArgs(T[] selectedItems) {
            _SelectedItems = selectedItems;
        }
        /// <summary>
        /// 批量选择的数据条目
        /// </summary>
        public T[] SelectedItems {
            get {
                return _SelectedItems;
            }
            set {
                _SelectedItems = value;
            }
        }
    }
}
