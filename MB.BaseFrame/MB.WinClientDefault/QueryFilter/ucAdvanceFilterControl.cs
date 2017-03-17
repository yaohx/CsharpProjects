//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-08-04
// Description	:	高级查询控件。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
                                                           
using DevExpress.XtraEditors;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraEditors.Repository;

using MB.WinBase.IFace;
namespace MB.WinClientDefault.QueryFilter {
    /// <summary>
    /// 高级查询控件。
    /// </summary>
    public partial class ucAdvanceFilterControl : UserControl {
        
        private XGridFilterControl _FilterControl;
        private List<FilterTemplateInfo> _FilterTemplates;
        private FilterTemplateEditHelper _TemplateEditHelper;
        /// <summary>
        /// ucAdvanceFilterControl
        /// </summary>
        public ucAdvanceFilterControl() {
            InitializeComponent();

            panMain.Dock = DockStyle.Fill;

        }

        #region public 成员...
        /// <summary>
        /// 创建高级查询控件。  
        /// </summary>
        /// <param name="viewGridForm"></param>
        public void IniLoadFilterControl(IViewGridForm viewGridForm) {
            IniLoadFilterControl(viewGridForm.ClientRuleObject, viewGridForm.GetCurrentMainGridView(true) as DevExpress.XtraGrid.GridControl);
        }
        /// <summary>
        /// 创建高级查询控件。     
        /// </summary>
        /// <param name="viewGridForm"></param>
        public void IniLoadFilterControl(IClientRuleQueryBase clientRuleObject, DevExpress.XtraGrid.GridControl mainGridCtl) {
            try {
                if (_FilterControl == null) {
                    DevExpress.XtraGrid.Views.Grid.GridView gridView = mainGridCtl.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
                    _FilterControl = new XGridFilterControl(clientRuleObject.UIRuleXmlConfigInfo.GetDefaultColumns());

                    DevExpress.XtraEditors.Filtering.FilterColumnCollection filterColumns = new DevExpress.XtraGrid.FilterEditor.ViewFilterColumnCollection(gridView);
                    var createColumns = createFilterColumn(clientRuleObject, filterColumns);
                    _FilterControl.SortFilterColumns = false;
                    _FilterControl.SetFilterColumnsCollection(createColumns, mainGridCtl.MenuManager);

                    _FilterControl.ShowOperandTypeIcon = gridView.OptionsFilter.UseNewCustomFilterDialog;
                    _FilterControl.Dock = DockStyle.Fill;
                    panMain.Controls.Add(_FilterControl);
                    _TemplateEditHelper = new FilterTemplateEditHelper(clientRuleObject, cobFilterTemplate);
                    //加载模板
                    _FilterTemplates = _TemplateEditHelper.LoadFilterTemplate(); 
                }
                _FilterControl.BringToFront();
            }
            catch (Exception e) {
                MB.Util.TraceEx.Write("加载高级查询出错：" + e.Message);
                throw new MB.Util.APPException("当前模块可能还没有提供相应的高级查询功能，请检查相应的配置信息!", MB.Util.APPMessageType.DisplayToUser);
            }
        }
        /// <summary>
        ///  自定义的高级查询编辑项。
        /// </summary>
        /// <returns></returns>
        public MB.Util.Model.QueryParameterInfo[] GetQueryParameters() {
            return _FilterControl.GetQueryParameters() ;
        }
        #endregion public 成员...
        private DevExpress.XtraEditors.Filtering.FilterColumnCollection createFilterColumn(IClientRuleQueryBase clientRuleObject, DevExpress.XtraEditors.Filtering.FilterColumnCollection orgFilterColumns) {
            var vals = clientRuleObject.UIRuleXmlConfigInfo.GetDefaultColumns();
            List<MB.WinBase.Common.ColumnPropertyInfo> cols = new List<MB.WinBase.Common.ColumnPropertyInfo>();
            cols.AddRange(vals.Values.ToArray());
            cols.Sort(new Comparison<MB.WinBase.Common.ColumnPropertyInfo>(colPropertyOrderByIndex));

            DevExpress.XtraEditors.Filtering.FilterColumnCollection filterColumns = new FilterColumnCollection();
            foreach (var colInfo in cols) {
                if (!colInfo.AdvanceFilter || !colInfo.Visibled) continue;

                var fCol = getFilterColumn(orgFilterColumns,colInfo.Name);
                if (fCol == null) continue;

                filterColumns.Add(fCol);
            }
            return filterColumns;
        }
        private DevExpress.XtraEditors.Filtering.FilterColumn getFilterColumn(DevExpress.XtraEditors.Filtering.FilterColumnCollection orgFilterColumns,string colName) {
            foreach (DevExpress.XtraEditors.Filtering.FilterColumn  filterCol in orgFilterColumns) {
                if (string.Compare(filterCol.FieldName, colName, true) == 0)
                    return filterCol;
            }
            return null;
        }
        private int colPropertyOrderByIndex(MB.WinBase.Common.ColumnPropertyInfo x,MB.WinBase.Common.ColumnPropertyInfo y) {
            return (new CaseInsensitiveComparer()).Compare(x.OrderIndex, y.OrderIndex);
        }

        #region 界面操作事件...
        private void sMenuTemplateSave_Click(object sender, EventArgs e) {
            try {
                if (string.IsNullOrEmpty(cobFilterTemplate.Text.Trim())) {
                    throw new MB.Util.APPException("需要存储的模板名称不能为空,请输入", MB.Util.APPMessageType.DisplayToUser);
                }
                string filter = _FilterControl.FilterString;
                if (string.IsNullOrEmpty(filter))
                    throw new MB.Util.APPException("不存在需要存储的查询条件,请先设置", MB.Util.APPMessageType.DisplayToUser);

                FilterTemplateInfo templateInfo = new FilterTemplateInfo(cobFilterTemplate.Text.Trim(), filter);
                _TemplateEditHelper.SaveFilterTemplate(_FilterTemplates, templateInfo);
            }
            catch (Exception ex) {
                MB.WinBase.ApplicationExceptionTerminate.DefaultInstance.ExceptionTerminate(ex);  
            }   
        }

        private void sMenuTemplateDelete_Click(object sender, EventArgs e) {
            _TemplateEditHelper.DeleteFilterTemplate(_FilterTemplates);
        }

        private void sMenuTemplateClear_Click(object sender, EventArgs e) {
            _TemplateEditHelper.ClearFilterTemplate(_FilterTemplates);
        }

        private void cobFilterTemplate_SelectedIndexChanged(object sender, EventArgs e) {
            if (cobFilterTemplate.SelectedIndex < 0) return;
            FilterTemplateInfo templateInfo = cobFilterTemplate.SelectedItem as FilterTemplateInfo;
            _FilterControl.FilterString = templateInfo.FilterContent;
        }

        private void lnkTemplateOperate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            cMenuTemplate.Show(lnkTemplateOperate,new Point(0,lnkTemplateOperate.Height));
        }
        
        #endregion 界面操作事件...
    }

    /// <summary>
    /// 自定义的高级查询编辑项。
    /// </summary>
    internal class XGridFilterControl : FilterControl {
        private Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> _ColPropertys;
        public XGridFilterControl(Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> colPropertys) {
            _ColPropertys = colPropertys;
        }

        /// <summary>
        /// 获取当前设置的查询参数。
        /// </summary>
        /// <returns></returns>
        public MB.Util.Model.QueryParameterInfo[] GetQueryParameters() {
            CriteriaOperator op= this.FilterCriteria;
            List<MB.Util.Model.QueryParameterInfo> lstParams = new List<MB.Util.Model.QueryParameterInfo>();
            builderQueryParams(lstParams, op,false);
            return lstParams.ToArray() ;
            
        }
        protected override void OnClick(EventArgs e) {
            try {
                base.OnClick(e);
            }
            catch { }
        }

        #region 内部函数处理...
        //构建数据库查询参数
        private void builderQueryParams(List<MB.Util.Model.QueryParameterInfo> lstParams, CriteriaOperator op,bool isNot) {
            DevExpress.Data.Filtering.GroupOperator groupOp = op as DevExpress.Data.Filtering.GroupOperator;
            if (op is DevExpress.Data.Filtering.GroupOperator) {
                MB.Util.Model.QueryParameterInfo groupPar = null;
                if (groupOp.OperatorType == GroupOperatorType.And)
                    groupPar = new MB.Util.Model.QueryParameterInfo(isNot?MB.Util.Model.QueryGroupLinkType.AndNot : MB.Util.Model.QueryGroupLinkType.AND);
                else
                    groupPar = new MB.Util.Model.QueryParameterInfo(isNot?MB.Util.Model.QueryGroupLinkType.OrNot : MB.Util.Model.QueryGroupLinkType.OR);
                lstParams.Add(groupPar);
                foreach (CriteriaOperator childOp in groupOp.Operands)
                    builderQueryParams(groupPar.Childs, childOp, isNot);
            }
            else if (op is DevExpress.Data.Filtering.UnaryOperator) {
                DevExpress.Data.Filtering.UnaryOperator uOp = op as DevExpress.Data.Filtering.UnaryOperator;
                if (uOp.OperatorType == UnaryOperatorType.IsNull) {
                    MB.Util.Model.QueryParameterInfo nullPar = new MB.Util.Model.QueryParameterInfo();
                    nullPar.PropertyName = QueryParametersEditHelper.RemoveSpecName(uOp.Operand.ToString());
                    nullPar.Condition = isNot ? MB.Util.DataFilterConditions.IsNotNull : MB.Util.DataFilterConditions.IsNull;
                    lstParams.Add(nullPar);
                }
                else if (uOp.OperatorType == UnaryOperatorType.Not) {
                    builderQueryParams(lstParams, uOp.Operand, true);
                }
            }
            else if (op is DevExpress.Data.Filtering.BetweenOperator) {
                DevExpress.Data.Filtering.BetweenOperator btOp = op as DevExpress.Data.Filtering.BetweenOperator;
                MB.Util.Model.QueryParameterInfo btPar = new MB.Util.Model.QueryParameterInfo();
                
                btPar.PropertyName = QueryParametersEditHelper.RemoveSpecName(btOp.Property.ToString());
                btPar.Condition = isNot?MB.Util.DataFilterConditions.NotBetween:MB.Util.DataFilterConditions.Between;
                btPar.Value =(btOp.BeginExpression as DevExpress.Data.Filtering.OperandValue).Value;
                btPar.Value2 = (btOp.EndExpression as DevExpress.Data.Filtering.OperandValue).Value;
                if (_ColPropertys.ContainsKey(btPar.PropertyName)) {
                    btPar.DataType = _ColPropertys[btPar.PropertyName].DataType;
                }
                lstParams.Add(btPar);
            }
            else if (op is DevExpress.Data.Filtering.BinaryOperator) {
                DevExpress.Data.Filtering.BinaryOperator binOp = op as DevExpress.Data.Filtering.BinaryOperator;
                MB.Util.Model.QueryParameterInfo binPar = new MB.Util.Model.QueryParameterInfo();
                binPar.PropertyName = QueryParametersEditHelper.RemoveSpecName(binOp.LeftOperand.ToString());
                binPar.Condition = QueryParametersEditHelper.ConvertToFilterCondition(binOp.OperatorType,isNot);
                binPar.Value = (binOp.RightOperand as DevExpress.Data.Filtering.OperandValue).Value;
                if (_ColPropertys.ContainsKey(binPar.PropertyName)) {
                    binPar.DataType = _ColPropertys[binPar.PropertyName].DataType;
                }
                lstParams.Add(binPar);
            }
            else if (op is DevExpress.Data.Filtering.InOperator) {
                DevExpress.Data.Filtering.InOperator inOp = op as DevExpress.Data.Filtering.InOperator;
                if (inOp.Operands.Count == 0) return;

                MB.Util.Model.QueryParameterInfo inPar = new MB.Util.Model.QueryParameterInfo();
                inPar.PropertyName = QueryParametersEditHelper.RemoveSpecName(inOp.LeftOperand.ToString());
                inPar.Condition = isNot ? MB.Util.DataFilterConditions.NotIn : MB.Util.DataFilterConditions.In;
                //获取In 的查询值
                List<string> vals = new List<string>();
                foreach (DevExpress.Data.Filtering.OperandValue v in inOp.Operands) {
                    if (v.Value == null) continue;

                    vals.Add(v.Value.ToString());
                }
                inPar.Value = string.Join(",", vals.ToArray());
                if (_ColPropertys.ContainsKey(inPar.PropertyName)) {
                    inPar.DataType = _ColPropertys[inPar.PropertyName].DataType;
                }
                lstParams.Add(inPar);
            }
            else {

            }
           
        }
        #endregion 内部函数处理...

    }

    /// <summary>
    /// 查询参数的编辑辅助类。
    /// </summary>
    internal class QueryParametersEditHelper {
       
        /// <summary>
        /// 设计条件转换为数据库查询参数条件
        /// </summary>
        /// <param name="operatorType"></param>
        /// <param name="isNot"></param>
        /// <returns></returns>
        public static  MB.Util.DataFilterConditions ConvertToFilterCondition(BinaryOperatorType operatorType,bool isNot) {
            switch (operatorType) {
                case BinaryOperatorType.BitwiseAnd:
                case BinaryOperatorType.BitwiseOr:
                case BinaryOperatorType.BitwiseXor:
                case BinaryOperatorType.Divide:
                    return MB.Util.DataFilterConditions.Special;
                default:
                    return (MB.Util.DataFilterConditions)Enum.Parse(typeof(MB.Util.DataFilterConditions),
                        (isNot?"Not":"")+operatorType.ToString());
            }
        }
        
        /// <summary>
        /// 删除不需要的字符。
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string ConvertToParamValue(string strValue) {
            if (string.IsNullOrEmpty(strValue)) return strValue;
            else {
                string val = strValue;//.Replace("%", "");
                val = val.Replace("'", "");
                return val;
            }
        }
        
        /// <summary>
        /// 去掉字段的 [ 和 ] 因为在Oracle 数据库查询中无效
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string RemoveSpecName(string fieldName) {
            return fieldName.Substring(1, fieldName.Length - 2);
        }

    }
    /// <summary>
    /// 查询模板存储编辑处理相关。
    /// </summary>
    internal class FilterTemplateEditHelper {
        private static readonly string TEMPLATE_XML_FILE_NAME = MB.Util.General.GeApplicationDirectory() + @"UserSetting\{0}~FilterTemplate.xml";
        private IClientRuleQueryBase _ClientRuleObject;
        private System.Windows.Forms.ComboBox _TemplateBox;

        public FilterTemplateEditHelper(IClientRuleQueryBase clientRuleObject, System.Windows.Forms.ComboBox cobBox) {
            _ClientRuleObject = clientRuleObject;
            _TemplateBox = cobBox;
        }
        /// <summary>
        /// 加载已经保存的查询模板。
        /// </summary>
        /// <returns></returns>
        public List<FilterTemplateInfo> LoadFilterTemplate() {
            string xmlFullFilName = string.Format(TEMPLATE_XML_FILE_NAME, _ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile);
            if (!System.IO.File.Exists(xmlFullFilName))
                return new List<FilterTemplateInfo>();

            var xmlDoc = MB.Util.XmlConfig.XmlConfigHelper.Instance.LoadXmlConfigFile(xmlFullFilName);
            var xmlSerializer = new MB.Util.Serializer.EntityXmlSerializer<FilterTemplateInfo>();
            var lstData = xmlSerializer.DeSerializer(xmlDoc.InnerXml);
            lstData.Sort(new Comparison<FilterTemplateInfo>(saveDateCompare));
            _TemplateBox.Items.Clear();
            foreach (var info in lstData) {
                _TemplateBox.Items.Add(info);
            }
            return lstData;
        }
        //根据存储日期进行排序
        private int saveDateCompare(FilterTemplateInfo x, FilterTemplateInfo y) {
            return (new CaseInsensitiveComparer()).Compare(y.SaveDateTime, x.SaveDateTime);
        }
        /// <summary>
        /// 模板保存。
        /// </summary>
        /// <param name="filterTemplates"></param>
        /// <param name="newTemplateInfo"></param>
        public void SaveFilterTemplate( List<FilterTemplateInfo> filterTemplates , FilterTemplateInfo newTemplateInfo) {
            string xmlFullFilName = string.Format(TEMPLATE_XML_FILE_NAME, _ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile);
            try {
                if (newTemplateInfo != null) {
                    var find = filterTemplates.FirstOrDefault(o => string.Compare(o.TemplateName, newTemplateInfo.TemplateName, true) == 0);
                    newTemplateInfo.SaveDateTime = System.DateTime.Now;
                    if (find != null) {
                        find.FilterContent = newTemplateInfo.FilterContent;
                        find.SaveDateTime = newTemplateInfo.SaveDateTime;
                    }
                    else {
                        filterTemplates.Add(newTemplateInfo);
                        _TemplateBox.Items.Insert(0, newTemplateInfo);
                    }
                }
                var xmlSerializer = new MB.Util.Serializer.EntityXmlSerializer<FilterTemplateInfo>();
                var xmlString = xmlSerializer.Serializer(filterTemplates);
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.LoadXml(xmlString);
                xmlDoc.Save(xmlFullFilName);

               // MB.WinBase.MessageBoxEx.Show("查询条件模板保存成功");
            }
            catch (Exception ex) {
                MB.WinBase.MessageBoxEx.Show("查询条件模板保存有误");
                MB.Util.TraceEx.Write(ex.Message);
            }

        }
        /// <summary>
        /// 删除选择的模板。
        /// </summary>
        /// <param name="filterTemplates"></param>
        public void DeleteFilterTemplate(List<FilterTemplateInfo> filterTemplates) {
            if (_TemplateBox.SelectedIndex < 0) return;
            FilterTemplateInfo delInfo = _TemplateBox.SelectedItem as FilterTemplateInfo;
            filterTemplates.Remove(delInfo);
            _TemplateBox.Items.Remove(_TemplateBox.SelectedItem);
            SaveFilterTemplate(filterTemplates, null);
        }
        /// <summary>
        /// 清楚该模块对应的所有查询模板。
        /// </summary>
        /// <param name="filterTemplates"></param>
        public void ClearFilterTemplate(List<FilterTemplateInfo> filterTemplates) {
            if (_TemplateBox.Items.Count == 0) return;
            DialogResult dre = MB.WinBase.MessageBoxEx.Question("是否决定清除该模块对应的所有查询模板?");
            if (dre != DialogResult.Yes) return;

            filterTemplates.Clear();
            _TemplateBox.Items.Clear();
            _TemplateBox.Text = string.Empty;
            string xmlFullFilName = string.Format(TEMPLATE_XML_FILE_NAME, _ClientRuleObject.ClientLayoutAttribute.UIXmlConfigFile);
            System.IO.File.Delete(xmlFullFilName); 
        }

    }

    /// <summary>
    /// 模板存储的描述信息。
    /// </summary>
    [MB.Util.XmlConfig.ModelXmlConfig]
    internal class FilterTemplateInfo {
        private string _TemplateName;
        private string _FilterContent;
        private DateTime _SaveDateTime;

        public FilterTemplateInfo() {
        }
        public FilterTemplateInfo(string templateName, string filterContent) {
            _TemplateName = templateName;
            _FilterContent = filterContent;
        }
        public override string ToString() {
            return _TemplateName;
        }
        /// <summary>
        /// 查询模板名称。
        /// </summary>
        [System.Runtime.Serialization.DataMember] 
        public string TemplateName {
            get {
                return _TemplateName;
            }
            set {
                _TemplateName = value;
            }
        }
        /// <summary>
        /// 查询条件内容。
        /// </summary>
        [System.Runtime.Serialization.DataMember]
        public string FilterContent {
            get {
                return _FilterContent;
            }
            set {
                _FilterContent = value;
            }
        }
        /// <summary>
        /// 保存的时间。
        /// </summary>
        [System.Runtime.Serialization.DataMember]
        public DateTime SaveDateTime {
            get {
                return _SaveDateTime;
            }
            set {
                _SaveDateTime = value;
            }
        }
    }
}
