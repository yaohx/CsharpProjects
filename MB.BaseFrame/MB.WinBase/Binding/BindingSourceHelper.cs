//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-16
// Description	:	数据绑定公共处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MB.WinBase.Binding
{
    /// <summary>
    /// 数据绑定公共处理相关。
    /// </summary>
    public class BindingSourceHelper
    {

        #region 变量定义...
        //实体Model 固定非绑定字段名称
        private static readonly string[] _ENTITY_EXCLUSION_PROPERTYS = new string[] { "ReadOnly", "EntityState" };
        //windows form 描述或者操作控件
        private static readonly string[] _DESCRIPTION_CONTROLS = new string[] { "Label", "Button", "LinkLabel" };
        //windows form 容器控件
        private static readonly List<string> _CONTAINER_CONTROLS;

        private const int MIN_CTL_NAME_LENGTH = 4;
        private const int CTL_LEFT_PREX_LENGTH = 3;
        private static readonly string CTL_TYPE_COMBOBOX = "ComboBox";
        private static readonly string CTL_TYPE_BUTTON_CLICK = "ButtonClick";
        private Dictionary<Control, System.Windows.Forms.Binding> _HasCreateBindingCompleteEvents;
        #endregion 变量定义...

        /// <summary>
        /// 
        /// </summary>
        static BindingSourceHelper() {
            _CONTAINER_CONTROLS = new List<string>();

            _CONTAINER_CONTROLS.Add("Panel");
            _CONTAINER_CONTROLS.Add("GroupBox");
            _CONTAINER_CONTROLS.Add("TabControl");
            _CONTAINER_CONTROLS.Add("TabPage");
            _CONTAINER_CONTROLS.Add("SplitContainer");
            _CONTAINER_CONTROLS.Add("FlowLayoutPanel");
            _CONTAINER_CONTROLS.Add("TableLayoutPanel");
        }

        /// <summary>
        /// 可以进入进行遍历的容器类型名称。
        /// 在其它地方调用时先要进行判断是否存在这种类型。
        /// 在个性化非通用处理中 增加后要删除掉。
        /// </summary>
        public static List<string> CONTAINER_CONTROLS {
            get {
                return _CONTAINER_CONTROLS;
            }
        }

        #region Instance...
        private static Object _Obj = new object();
        private static BindingSourceHelper _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected BindingSourceHelper() {
            _HasCreateBindingCompleteEvents = new Dictionary<Control, System.Windows.Forms.Binding>();
        }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static BindingSourceHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new BindingSourceHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        #region public 成员...


        /// <summary>
        /// 根据泛型IList 返回数据绑定的集合类。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataArray"></param>
        /// <returns></returns>
        public System.ComponentModel.BindingList<T> CreateBindingList<T>(IList dataArray) {
            System.ComponentModel.BindingList<T> dataList = new System.ComponentModel.BindingList<T>();
            if (dataArray != null && dataArray.Count > 0) {
                foreach (T info in dataArray) {
                    dataList.Add(info);
                }
            }
            return dataList;
        }
        /// <summary>
        /// 清除输入框的值。
        /// </summary>
        /// <param name="dataBindingProvider"></param>
        public void ClearBindingEditCtl(MB.WinBase.Binding.IDataBindingProvider dataBindingProvider) {
            if (dataBindingProvider == null || dataBindingProvider.DataBindings == null) return;
            foreach (Control ctl in dataBindingProvider.DataBindings.Keys) {
                string ctlName = ctl.GetType().Name;
                if (Array.IndexOf<string>(MyDataBindingProvider.INCLUDE_TAG_TEXT_CTLS, ctlName) >= 0)
                    ctl.Text = string.Empty;
            }
        }
        /// <summary>
        /// 重新设置绑定的Lable 描述。    
        /// </summary>
        /// <param name="dataBindingProvider"></param>
        public void ResetLableCaption(MB.WinBase.Binding.IDataBindingProvider dataBindingProvider, Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys) {
            if (dataBindingProvider == null || dataBindingProvider.DataBindings == null) return;
            foreach (Control ctl in dataBindingProvider.DataBindings.Keys) {
                string ctlName = ctl.GetType().Name;
                if (Array.IndexOf<string>(_DESCRIPTION_CONTROLS, ctlName) < 0) continue;
                string propertyName = dataBindingProvider.DataBindings[ctl].ColumnName;
                if (!colPropertys.ContainsKey(propertyName)) continue;
                var colInfo = colPropertys[propertyName];
                System.Drawing.Color canEditColor = colInfo.CanEdit ? System.Drawing.Color.Blue : System.Drawing.Color.Black;

                ctl.ForeColor = colInfo.IsNull ? canEditColor : System.Drawing.Color.Red;
                if (colInfo.IsKey)
                    ctl.Font = new System.Drawing.Font(ctl.Font, System.Drawing.FontStyle.Bold);


            }
        }
        /// <summary>
        /// 编辑界面的数据绑定分3步来完成 
        /// 1,先绑定ComboBox 的数据源
        /// 2,绑定点击选择数据的数据源
        /// 3,控件编辑绑定
        /// </summary>
        /// <param name="clientBaseRule"></param>
        /// <param name="dataSource"></param>
        /// <param name="ctls"></param>
        /// <returns></returns>
        public List<ColumnBindingInfo> CreateDataBinding(IFace.IClientRule clientBaseRule, BindingSourceEx dataSource,
                                    MB.WinBase.Binding.IDataBindingProvider dataBindingProvider, DataBindingOptions bindingOptions) {
            ////在数据绑定之前先清空文本框的数据
            //ClearBindingEditCtl(dataBindingProvider);

            //List<ColumnBindingInfo> bindingDatas = new List<ColumnBindingInfo>();
            //Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colsCfg = clientBaseRule.UIRuleXmlConfigInfo.GetDefaultColumns(); 
            //string[] propertys = getDataPropertysNames(dataSource);
            //if (propertys == null || propertys.Length == 0)
            //    throw new Exceptions.DataBindingException();


            //ctlDataBingByDataBinding(colsCfg, propertys, dataSource, dataBindingProvider, bindingOptions, bindingDatas, clientBaseRule.UIRuleXmlConfigInfo.ColumnsCfgEdit);

            ////设置绑定Lable 的 描述展示信息
            //ResetLableCaption(dataBindingProvider, colsCfg);

            //return bindingDatas;
            return CreateDataBinding(clientBaseRule.UIRuleXmlConfigInfo.GetDefaultColumns(), clientBaseRule.UIRuleXmlConfigInfo.ColumnsCfgEdit, dataSource, dataBindingProvider, bindingOptions);
        }
        /// <summary>
        /// 编辑界面的数据绑定分3步来完成 
        /// 1,先绑定ComboBox 的数据源
        /// 2,绑定点击选择数据的数据源
        /// 3,控件编辑绑定
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="xmlFileName"></param>
        /// <param name="dataBindingProvider"></param>
        /// <param name="bindingOptions"></param>
        /// <returns></returns>
        public List<ColumnBindingInfo> CreateDataBinding( BindingSourceEx dataSource,string xmlFileName,
                                                       MB.WinBase.Binding.IDataBindingProvider dataBindingProvider,
                                                       DataBindingOptions bindingOptions) {
            Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys =  MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnPropertys(xmlFileName);
            Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> columnEditInfoList = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnEdits(colPropertys,xmlFileName);
            return CreateDataBinding(colPropertys, columnEditInfoList, dataSource, dataBindingProvider, bindingOptions);

        }
        /// <summary>
        /// 编辑界面的数据绑定分3步来完成 
        /// 1,先绑定ComboBox 的数据源
        /// 2,绑定点击选择数据的数据源
        /// 3,控件编辑绑定
        /// </summary>
        /// <param name="colPropertys"></param>
        /// <param name="columnEditInfoList"></param>
        /// <param name="dataSource"></param>
        /// <param name="dataBindingProvider"></param>
        /// <param name="bindingOptions"></param>
        /// <returns></returns>
        public List<ColumnBindingInfo> CreateDataBinding(Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys,
                                                         Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> columnEditInfoList,
                                                         BindingSourceEx dataSource,
                                                         MB.WinBase.Binding.IDataBindingProvider dataBindingProvider,
                                                         DataBindingOptions bindingOptions) {
            //在数据绑定之前先清空文本框的数据
            ClearBindingEditCtl(dataBindingProvider);

            List<ColumnBindingInfo> bindingDatas = new List<ColumnBindingInfo>();
            Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colsCfg = colPropertys;
            string[] fieldPropertys = getDataPropertysNames(dataSource);
            if (fieldPropertys == null || fieldPropertys.Length == 0)
                throw new Exceptions.DataBindingException();


            ctlDataBingByDataBinding(colPropertys, fieldPropertys, dataSource, dataBindingProvider, bindingOptions, bindingDatas, columnEditInfoList);

            //设置绑定Lable 的 描述展示信息
            ResetLableCaption(dataBindingProvider, colsCfg);

            return bindingDatas;
        }
        /// <summary>
        /// 释放所绑定的信息。
        /// </summary>
        /// <param name="ctls"></param>
        public void ReleaseDataBinding(List<ColumnBindingInfo> bindingDatas) {
            if (bindingDatas == null || bindingDatas.Count == 0) return;

            foreach (ColumnBindingInfo colCfg in bindingDatas) {
                colCfg.BindingControl.DataBindings.Clear();
                if (_HasCreateBindingCompleteEvents.ContainsKey(colCfg.BindingControl)) {
                    _HasCreateBindingCompleteEvents[colCfg.BindingControl].BindingComplete -= new BindingCompleteEventHandler(binding_BindingComplete);
                    _HasCreateBindingCompleteEvents.Remove(colCfg.BindingControl);
                }
            }
        }

        /// <summary>
        /// 设置当前编辑界面是否为只读状态。
        /// </summary>
        /// <param name="bindingCtlList">需要进行设置的控件集合。</param>
        /// <param name="readOnly">是否设置为只读。</param>
        /// <param name="exclusiveCtls">排除不进行处理的控件</param>
        public void SetCtlReadOnly(List<ColumnBindingInfo> bindingCtlList, bool readOnly, params Control[] exclusiveCtls) {
            if (bindingCtlList == null || bindingCtlList.Count == 0) return;

            foreach (ColumnBindingInfo ctl in bindingCtlList) {
                if (!ctl.ColumnPropertyInfo.CanEdit) continue;
                if (exclusiveCtls == null || Array.IndexOf(exclusiveCtls, ctl.BindingControl) >= 0)
                    continue;

                setEditControlReadonly(ctl.BindingControl, readOnly);
            }
        }
        /// <summary>
        /// 根据实体对象的状态改变编辑控件的编辑状态。
        /// </summary>
        /// <param name="bindingCtlList"></param>
        /// <param name="entityState"></param>
        /// <param name="exclusiveCtls"></param>
        public void SetCtlByAllowEditStates(List<ColumnBindingInfo> bindingCtlList, MB.Util.Model.EntityState entityState, params Control[] exclusiveCtls) {
            if (bindingCtlList == null || bindingCtlList.Count == 0) return;

            foreach (ColumnBindingInfo ctl in bindingCtlList) {
                if (!ctl.ColumnPropertyInfo.CanEdit) continue;
                if (string.IsNullOrEmpty(ctl.ColumnPropertyInfo.AllowEditStates)) continue;

                if (exclusiveCtls == null || Array.IndexOf(exclusiveCtls, ctl.BindingControl) >= 0)
                    continue;

                string[] states = ctl.ColumnPropertyInfo.AllowEditStates.Split(',');
                string currentState = entityState.ToString();
                bool canEdit = Array.IndexOf<string>(states, currentState) >= 0;
                setEditControlReadonly(ctl.BindingControl, !canEdit);
            }
        }
        #endregion public 成员...

        #region 控件绑定处理相关...
        /// <summary>
        /// ComboBox 控件绑定。
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="propertyName"></param>
        /// <param name="columnEditInfoList"></param>
        public void FillCombox(Control ctl, string propertyName, Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> columnEditInfoList, bool mustInsertNullRow) {
            if (ctl is ComboBox && columnEditInfoList.ContainsKey(propertyName)) {
                ComboBox cob = ctl as ComboBox;
                cob.Items.Clear();
                MB.WinBase.Common.ColumnEditCfgInfo cfgInfo = columnEditInfoList[propertyName];
                if (cfgInfo.SaveLocalCache) {
                    if (cfgInfo.DataSource == null)
                        return;

                    DataTable dtData = MB.Util.MyConvert.Instance.ToDataTable(cfgInfo.DataSource, string.Empty);
                    DataRow[] drs = dtData.Select();
                    if (mustInsertNullRow || cfgInfo.InsertNullItem)
                        cob.Items.Add(string.Empty);

                    foreach (DataRow dr in drs) {
                        cob.Items.Add(dr[cfgInfo.TextFieldName].ToString());
                    }
                }
                else {
                    //以后再处理
                    MB.Util.TraceEx.Write("ColumnEditCfgInfo 配置成 ComboBox 必须把 SaveLocalCache 设置为True,为 False 的情况目前还没有进行处理。");
                }
            }
        }
        /// <summary>
        ///  ComboBox 控件绑定(包含编码和名称的 ComboBox 绑定项)
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="propertyName"></param>
        /// <param name="columnEditInfoList"></param>
        public void FillComboxLookUp(Control ctl, string propertyName, Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> editCfgColumns, Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> columnEditInfoList, bool mustInsertNullRow) {
            if (ctl is ComboBox && columnEditInfoList.ContainsKey(propertyName)) {
                ComboBox cob = ctl as ComboBox;
                MB.WinBase.Common.ColumnPropertyInfo colCfg = null;
                if (editCfgColumns != null)
                    colCfg = editCfgColumns[propertyName];

                MB.WinBase.Common.ColumnEditCfgInfo cfgInfo = columnEditInfoList[propertyName];
                if (cfgInfo.SaveLocalCache) {
                    if (cfgInfo.DataSource == null)
                        return;

                    cob.DisplayMember = cfgInfo.TextFieldName;
                    cob.ValueMember = cfgInfo.ValueFieldName;
                    DataTable dtData = MB.Util.MyConvert.Instance.ToDataTable(cfgInfo.DataSource, string.Empty);
                    DataTable newData = dtData.Copy();
                    if (mustInsertNullRow || cfgInfo.InsertNullItem) {
                        DataRow newDr = newData.NewRow();
                        if (colCfg != null) {
                            if (string.Compare(colCfg.DataType, "System.String", true) == 0)
                                newDr[cfgInfo.ValueFieldName] = "";
                            else if (string.Compare(colCfg.DataType, "System.Int32", true) == 0 || string.Compare(colCfg.DataType, "System.Decimal", true) == 0)
                                newDr[cfgInfo.ValueFieldName] = 0;

                        }
                        newData.Rows.InsertAt(newDr, 0);
                    }

                    cob.DataSource = newData.DefaultView;

                }
                else {
                    //以后再处理
                    MB.Util.TraceEx.Write("ColumnEditCfgInfo 配置成 ComboBox 必须把 SaveLocalCache 设置为True,为 False 的情况目前还没有进行处理。");
                }
            }

        }
        /// <summary>
        /// ComboCheckListBox 数据绑定。
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="propertyName"></param>
        /// <param name="editCfgColumns"></param>
        /// <param name="columnEditInfoList"></param>
        public void FillComboCheckListBox(Control ctl, string propertyName, Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> editCfgColumns, Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> columnEditInfoList) {
            bool check = ctl is MB.WinBase.Ctls.ucComboCheckedListBox && columnEditInfoList.ContainsKey(propertyName);
            if (!check) return;

            MB.WinBase.Ctls.ucComboCheckedListBox cob = ctl as MB.WinBase.Ctls.ucComboCheckedListBox;
            MB.WinBase.Common.ColumnPropertyInfo colCfg = null;
            if (editCfgColumns != null)
                colCfg = editCfgColumns[propertyName];

            MB.WinBase.Common.ColumnEditCfgInfo cfgInfo = columnEditInfoList[propertyName];
            if (cfgInfo.SaveLocalCache) {
                if (cfgInfo.DataSource == null)
                    return;

                cob.DisplayMember = cfgInfo.TextFieldName;
                cob.ValueMember = cfgInfo.ValueFieldName;
                DataTable dtData = MB.Util.MyConvert.Instance.ToDataTable(cfgInfo.DataSource, string.Empty);


                cob.DataSource = dtData;

            }
            else {
                //以后再处理
                MB.Util.TraceEx.Write("ColumnEditCfgInfo 配置成 ucComboCheckedListBox 必须把 SaveLocalCache 设置为True,为 False 的情况目前还没有进行处理。");
            }
        }
        #endregion 控件绑定处理相关...
        //绑定ButtonClick

        #region 内部函数处理...


        //控件编辑绑定。
        private void ctlDataBingByDataBinding(Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> editCfgColumns, string[] dataPropertys,
                                 System.Windows.Forms.BindingSource bindingSource,
                                 MB.WinBase.Binding.IDataBindingProvider dataBindingProvider,
                                 DataBindingOptions bindingOptions,
                                 List<ColumnBindingInfo> bindingDatas,
                                 Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> columnEditInfoList) {

            if (dataBindingProvider == null || dataBindingProvider.DataBindings == null || dataBindingProvider.DataBindings.Count == 0) return;

            Dictionary<Control, DesignColumnXmlCfgInfo> bindingCtls = dataBindingProvider.DataBindings;
            foreach (Control ctl in bindingCtls.Keys) {

                string ctlTypeName = ctl.GetType().Name;
                if (_DESCRIPTION_CONTROLS.Contains<string>(ctlTypeName))
                    continue;

                string ctlBindingName = getCtlBindingPropertyName(ctl);
                string dataPropertyName = bindingCtls[ctl].ColumnName;
                if (Array.IndexOf<string>(dataPropertys, dataPropertyName) < 0)
                    continue;

                MB.WinBase.Common.ColumnPropertyInfo colCfg = null;
                if (editCfgColumns.ContainsKey(dataPropertyName))
                    colCfg = editCfgColumns[dataPropertyName];

                if (editCfgColumns != null && editCfgColumns.ContainsKey(dataPropertyName)) {

                    //特殊处理，绑定到特殊的控件。
                    ColumnBindingInfo ctlBinding = new ColumnBindingInfo(dataPropertyName, colCfg, ctl);
                    if (!colCfg.CanEdit)
                        setEditControlReadonly(ctl, true);

                    //如果是string 类型 并且设置MacLength 同时绑订的是TextBox 控件，那么就限制它的输入长度
                    if (string.Compare(colCfg.DataType, "System.String", true) == 0) {
                        TextBox txtBox = ctl as TextBox;
                        if (txtBox != null && colCfg.MaxLength > 0) {
                            txtBox.MaxLength = colCfg.MaxLength;
                        }
                    }
                    bindingDatas.Add(ctlBinding);

                }


                System.Windows.Forms.Binding binding = new System.Windows.Forms.Binding(ctlBindingName, bindingSource,
                                                            dataPropertyName, true, DataSourceUpdateMode.OnPropertyChanged);   //DataSourceUpdateMode.OnValidation

                if (colCfg != null) {
                    formateBindControl(ctl, binding, colCfg);
                }
                //编辑界面的数据绑定分3步来完成

                //1,先绑定ComboBox 的数据源
                if (columnEditInfoList != null && columnEditInfoList.Count > 0) {
                    FillComboxLookUp(ctl, dataPropertyName, editCfgColumns, columnEditInfoList, false);
                }

                //2,绑定点击选择数据的数据源
                if (columnEditInfoList != null && columnEditInfoList.ContainsKey(dataPropertyName)) {
                    bindingToSpecialEditCtl(ctl, columnEditInfoList[dataPropertyName]);
                }

                //3,控件编辑绑定
                ctl.DataBindings.Add(binding);

            }
        }
        private void formateBindControl(Control editCtl, System.Windows.Forms.Binding binding, MB.WinBase.Common.ColumnPropertyInfo colCfg) {
            if (string.Compare(colCfg.DataType, "System.DateTime?", true) == 0) {
                DateTimePicker date = editCtl as DateTimePicker;
                if (date != null) {
                    binding.BindingComplete += new BindingCompleteEventHandler(binding_BindingComplete);
                    date.ShowCheckBox = true;
                }
            }
            else if (string.Compare(colCfg.DataType, "System.Decimal", true) == 0 && colCfg.IsFormatControl) {
                NumericUpDown num = editCtl as NumericUpDown;
                if (num != null) {
                    num.ThousandsSeparator = colCfg.ThousandsSeperator;
                    num.DecimalPlaces = colCfg.MaxDecimalPlaces;
                }
            }
        }

        void binding_BindingComplete(object sender, BindingCompleteEventArgs e) {
            DateTimePicker pk = e.Binding.Control as DateTimePicker;
            if (pk == null) return;
            BindingSource bs = e.Binding.DataSource as BindingSource;
            object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(bs.Current, e.Binding.BindingMemberInfo.BindingField);
            if (e.BindingCompleteContext == BindingCompleteContext.ControlUpdate) {

                if (val == null || val == System.DBNull.Value)
                    pk.Checked = false;
                else
                    pk.Checked = true;
            }
            else {
                if (!pk.Checked) {
                    MB.Util.MyReflection.Instance.InvokePropertyForSet(bs.Current, e.Binding.BindingMemberInfo.BindingField, null);
                }
            }
        }
        //控件编辑绑定。(已过期)
        [Obsolete("最好使用 ctlDataBingByDataBinding 来代替")]
        private void ctlDataBingByCtlName(Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> editCfgColumns, string[] dataPropertys,
                                 System.Windows.Forms.BindingSource bindingSource,
                                 System.Windows.Forms.Control.ControlCollection ctls,
                                 DataBindingOptions bindingOptions,
                                 List<ColumnBindingInfo> bindingDatas,
                                 Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> columnEditInfoList) {

            foreach (Control ctl in ctls) {
                string ctlTypeName = ctl.GetType().Name;
                if (_DESCRIPTION_CONTROLS.Contains<string>(ctlTypeName))
                    continue;

                if (_CONTAINER_CONTROLS.Contains(ctlTypeName)) {
                    ctlDataBingByCtlName(editCfgColumns, dataPropertys, bindingSource, ctl.Controls, bindingOptions, bindingDatas, columnEditInfoList);
                    continue;
                }
                string sName = ctl.Name;

                if (sName.Length < MIN_CTL_NAME_LENGTH) {
                    MB.Util.TraceEx.Write("在执行BindingSourceEx.SetDataSource 方法时,控件" + sName + "的命名方式至少要大于" + MIN_CTL_NAME_LENGTH + "个字符。该控件以及所在的ChildControl将不进行处理，请检查。",
                      MB.Util.APPMessageType.SysWarning);
                }
                if (sName.Length <= MIN_CTL_NAME_LENGTH) {
                    ctlDataBingByCtlName(editCfgColumns, dataPropertys, bindingSource, ctl.Controls, bindingOptions, bindingDatas, columnEditInfoList);
                    continue;
                }
                //获取控件需要进行绑定的属性名称。
                string ctlBindingName = getCtlBindingPropertyName(ctl);
                string dataPropertyName = sName.Substring(CTL_LEFT_PREX_LENGTH, sName.Length - CTL_LEFT_PREX_LENGTH);
                if (Array.IndexOf<string>(dataPropertys, dataPropertyName) < 0)
                    continue;

                if (editCfgColumns != null && editCfgColumns.ContainsKey(dataPropertyName)) {
                    MB.WinBase.Common.ColumnPropertyInfo colCfg = editCfgColumns[dataPropertyName];
                    ColumnBindingInfo ctlBinding = new ColumnBindingInfo(dataPropertyName, colCfg, ctl);
                    bindingDatas.Add(ctlBinding);
                }
                System.Windows.Forms.Binding binding = new System.Windows.Forms.Binding(ctlBindingName, bindingSource,
                                                            dataPropertyName, false, DataSourceUpdateMode.OnValidation);

                //编辑界面的数据绑定分3步来完成

                //1,先绑定ComboBox 的数据源
                if (columnEditInfoList != null && columnEditInfoList.Count > 0) {
                    FillComboxLookUp(ctl, dataPropertyName, editCfgColumns, columnEditInfoList, false);
                }
                //2,绑定点击选择数据的数据源

                //3,控件编辑绑定
                ctl.DataBindings.Add(binding);

            }
        }


        //绑定特殊处理的控件
        private void bindingToSpecialEditCtl(Control ctl, MB.WinBase.Common.ColumnEditCfgInfo colEditCfg) {
            if (ctl is MB.WinBase.Ctls.ucClickButtonInput) {
                MB.WinBase.Ctls.ucClickButtonInput cbi = ctl as MB.WinBase.Ctls.ucClickButtonInput;
                cbi.ColumnEditCfgInfo = colEditCfg;
            }

            //add by aifang 增加区域编辑控件 begin
            if (ctl is MB.WinBase.Ctls.ucPopupRegionEdit) {
                MB.WinBase.Ctls.ucPopupRegionEdit regionEdit = ctl as MB.WinBase.Ctls.ucPopupRegionEdit;
                regionEdit.ColumnEditCfgInfo = colEditCfg;
            }
            //add by aifang 增加区域编辑控件 end
        }
        //设置指定的控件为只读状态。
        private void setEditControlReadonly(Control ctl, bool readOnly) {
            if (ctl is System.Windows.Forms.NumericUpDown) {
                System.Windows.Forms.NumericUpDown nBox = ctl as System.Windows.Forms.NumericUpDown;
                nBox.ReadOnly = readOnly;
                nBox.Enabled = !readOnly;
            }
            else {
                System.Reflection.PropertyInfo info = ctl.GetType().GetProperty("ReadOnly");
                if (info != null) {
                    MB.Util.MyReflection.Instance.InvokePropertyForSet(ctl, "ReadOnly", readOnly);
                    return;
                }
                System.Reflection.PropertyInfo einfo = ctl.GetType().GetProperty("Enabled");
                if (einfo != null) {
                    MB.Util.MyReflection.Instance.InvokePropertyForSet(ctl, "Enabled", !readOnly);
                }
            }

        }

        //获取绑定数据源所有字段 或者 实体数据属性集合
        private string[] getDataPropertysNames(System.Windows.Forms.BindingSource bindingSource) {
            List<string> fields = new List<string>();
            if (bindingSource.DataSource == null)
                return fields.ToArray<string>();
            DataTable dtData = null;
            //把datasource 有可能是dataser,datatable,dataView 需要统一转换为dataTable 来进行处理
            dtData = MB.Util.MyConvert.Instance.ToDataTable(bindingSource.DataSource, string.Empty);

            if (dtData != null) {
                foreach (DataColumn dc in dtData.Columns) {
                    fields.Add(dc.ColumnName);
                }
                return fields.ToArray<string>();
            }
            IList source = bindingSource.DataSource as IList;
            Type entityType = null;
            if (source != null) {
                if (source.Count == 0) {
                    throw new Exceptions.DataBindingException("在绑定自集合类时，至少在集合中存在一项,否则在目前处理中无法自动获取对应的实体类型！");
                }
                entityType = (bindingSource.DataSource as IList)[0].GetType();
            }
            else {
                entityType = bindingSource.DataSource.GetType();
            }
            System.Reflection.PropertyInfo[] pros = entityType.GetProperties();
            foreach (System.Reflection.PropertyInfo info in pros) {
                if (Array.IndexOf<string>(_ENTITY_EXCLUSION_PROPERTYS, info.Name) >= 0)
                    continue;
                fields.Add(info.Name);
            }
            return fields.ToArray<string>();
        }

        //根据Win 控件获取数据绑定的属性名称。
        private string getCtlBindingPropertyName(Control ctl) {
            if (ctl is CheckBox)
                return "Checked";
            else if (ctl is DateTimePicker)
                return "Value";
            else if (ctl is ComboBox)
                return "SelectedValue";
            else if (ctl is MB.WinBase.Ctls.ucIamgeIcoEdit)
                return "ImageData";
            else if (ctl is MB.WinBase.Ctls.ucDbPictureBox)
                 return "ImageData";
            else if (ctl is MB.WinBase.Ctls.RichTextBoxEx)
                return "RtfContent";
            else if (ctl is ListBox || ctl is CheckedListBox) {
                MB.Util.TraceEx.Write("在控件自动绑定中对控件类型 ListBox,CheckedListBox 还没有进行处理！", MB.Util.APPMessageType.SysErrInfo);
                return null;
            }
            else if (ctl is RadioButton) {
                MB.Util.TraceEx.Write("在控件自动绑定中对控件类型 RadioButton 还没有进行处理！", MB.Util.APPMessageType.SysErrInfo);
                return null;
            }
            else
                return "Text";

        }
        #endregion 内部函数处理...
    }
}
