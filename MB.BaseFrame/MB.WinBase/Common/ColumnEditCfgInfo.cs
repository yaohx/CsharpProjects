 //---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-02-18
// Description	:	ColumnEditCfgInfo 列编辑的描述信息。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using MB.Util.XmlConfig;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.ComponentModel;
namespace MB.WinBase.Common {
    /// <summary>
    /// ColumnEditCfgInfo 列编辑的描述信息。
    /// </summary>
    [ModelXmlConfig(ByXmlNodeAttribute = false)]
    [XmlRoot("Column")]
    public class ColumnEditCfgInfo {

        #region 变量定义...
        private string _Name;
        private string _EditControlType;
        private object _DataSource;
        private string _TextFieldName;
        private string _ValueFieldName;
        private InvokeDataSourceDescInfo _InvokeDataSourceDesc;
        private ClickButtonShowFormCfgInfo _ClickButtonShowForm;
        private bool _SaveLocalCache;
        private List<LookUpColumnInfo> _LookUpColumns;
        private List<EditCtlDataMappingInfo> _EditCtlDataMappings;
        private bool _InsertNullItem;
        private string _FilterCfgName;
        private bool _DefaultBatchAdd;
        private bool _HideFilterPane;

        private bool _OrderByValueField;
        private CharacterCasing _CharacterCasing; //设置输入和显示的大小写
        private Func<object, MB.WinBase.IFace.IDataAssistant> _CreateDataAssistant;

        private bool _NeedCreate;  //是否需要每次创建查询助手对象  add by aifang 2012-3-27

        private int _MaxSelectRows;  //多选选择时设置最大行数  add by aifang 2012-5-22

        private bool _HideProperty;
        private InvokeDataPropertyDescInfo _InvokeDataPropertyDesc;

        private int _Level;  //设置区域控件选择级别

        private DataAssistantContextMenuInfo _DataAssistantContextMenu;  //配置自定义扩展右键菜单项 add by aifang 2012-08-22
        private bool _HideContextMenu;  //是否隐藏右键菜单项  add by aifang 2012-08-22
        private bool _IsValidateInputFromDataSource = true; //是不是需要从DataSource中去验证 EditControlType.ComboCheckedListBox,EditControlType.LookUpEdit中的值



        #endregion 变量定义...

        #region 构造函数...
        public ColumnEditCfgInfo() : this(null) {

        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="colName"></param>
        public ColumnEditCfgInfo(string colName)
            : this(colName, null, null) {
        }
        /// <summary>
        /// 构造函数...
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="editControlType"></param>
        /// <param name="dataSource"></param>
        public ColumnEditCfgInfo(string colName, string editControlType, string dataSource) {
            _Name = colName;
            _EditControlType = editControlType;
            _DataSource = dataSource;

            _ValueFieldName = "ID";
            _TextFieldName = "NAME";
            _OrderByValueField = true;

            _HideProperty = true; //add by aifang

            _LookUpColumns = new List<LookUpColumnInfo>();
            _EditCtlDataMappings = new List<EditCtlDataMappingInfo>();

            _HideContextMenu = true;
        }
        #endregion 构造函数...

        #region Public 属性...

        /// <summary>
        ///  需要设置的列的名称。
        /// </summary>
        [PropertyXmlConfig]  
        [XmlAttribute]
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }
        /// <summary>
        /// 绑定编辑的控件类型。
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement]
        public string EditControlType {
            get {
                return _EditControlType;
            }
            set {
                _EditControlType = value;
            }
        }
        /// <summary>
        /// 数据源。
        /// 通过下拉框来进行选择的目前需要进行约束，只能接受DataSet 的数据集。
        /// 该数据源的获取可以通过配置UI 层客户端的业务类来得到。
        /// </summary>
        [XmlIgnore]
        public object DataSource {
            get {
                return _DataSource;
            }
            set {
                _DataSource = value;
            }
        }
        /// <summary>
        /// 文本字段。
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement]
        public string TextFieldName {
            get {
                return _TextFieldName;
            }
            set {
                _TextFieldName = value;
            }
        }
        /// <summary>
        /// 需要进行关联存储的字段名称。
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement]
        public string ValueFieldName {
            get {
                return _ValueFieldName;
            }
            set {
                _ValueFieldName = value;
            }
        }
        /// <summary>
        /// 获取数据源的调用描述信息。
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement("InvokeDataSourceDesc")]
        public InvokeDataSourceDescInfo InvokeDataSourceDesc {
            get {
                return _InvokeDataSourceDesc;
            }
            set {
                _InvokeDataSourceDesc = value;
            }
        }
        /// <summary>
        /// Click Button 点击后弹窗口的配置信息。
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement]
        public ClickButtonShowFormCfgInfo ClickButtonShowForm {
            get {
                return _ClickButtonShowForm;
            }
            set {
                _ClickButtonShowForm = value;
            }
        }
        /// <summary>
        /// 编辑控件查询需要调用的过滤带名称。
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement]
        public string FilterCfgName {
            get {
                return _FilterCfgName;
            }
            set {
                _FilterCfgName = value;
            }
        }
        /// <summary>
        /// 判断获取到的列表值是否存储在本地 Cache中。 
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement]
        [DefaultValue(false)]
        public bool SaveLocalCache {
            get {
                return _SaveLocalCache;
            }
            set {
                _SaveLocalCache = value;
            }
        }
        /// <summary>
        /// 判断是否插入空的选择行。
        /// 
        /// </summary>
        [PropertyXmlConfig] 
        public bool InsertNullItem {
            get {
                return _InsertNullItem;
            }
            set {
                _InsertNullItem = value;
            }
        }
        /// <summary>
        /// 需要查找列的集合。
        /// </summary>
        [PropertyXmlConfig(typeof(LookUpColumnInfo))]  
        public List<LookUpColumnInfo> LookUpColumns {
            get {
                return _LookUpColumns;
            }
            set {
                _LookUpColumns = value;
            }
        }
        /// <summary>
        /// 编辑控件的数据对应项绑定信息。
        /// </summary>
        [PropertyXmlConfig(typeof(EditCtlDataMappingInfo))]  
        [XmlArray("EditCtlDataMappings")]
        [XmlArrayItem("EditCtlDataMappingInfo")]
        public List<EditCtlDataMappingInfo> EditCtlDataMappings {
            get {
                return _EditCtlDataMappings;
            }
            set {
                _EditCtlDataMappings = value;
            }
        }
        /// <summary>
        /// 获取或者设置是否为批量增加的配置项。
        /// </summary>
        [PropertyXmlConfig] 
        [XmlElement]
        [DefaultValue(false)]
        public bool DefaultBatchAdd {
            get {
                return _DefaultBatchAdd;
            }
            set {
                _DefaultBatchAdd = value;
            }
        }
        /// <summary>
        /// 判断是否隐掉查询过滤界面。
        /// </summary>
        [PropertyXmlConfig] 
        [XmlElement]
        [DefaultValue(false)]
        public bool HideFilterPane {
            get {
                return _HideFilterPane;
            }
            set {
                _HideFilterPane = value;
            }
        }
        /// <summary>
        /// 判断是否根据值字段进行排序。
        /// 默认情况下为True。
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement]
        [DefaultValue(true)]
        public bool OrderByValueField {
            get {
                return _OrderByValueField;
            }
            set {
                _OrderByValueField = value;
            }
        }
        /// <summary>
        /// 设置和显示的输入框的大小写。
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement]
        public CharacterCasing CharacterCasing {
            get {
                return _CharacterCasing;
            }
            set {
                _CharacterCasing = value;
            }
        }

        /// <summary>
        /// 是否需要每次创建查询助手对象  add by aifang 2012-3-27
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement]
        [DefaultValue(false)]
        public bool NeedCreate
        {
            get {
                return _NeedCreate;
            }
            set {
                _NeedCreate = value;
            }
        }

        /// <summary>
        /// 多选选择时配置的最大行数 add by aifang 2012-5-22
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement]
        public int MaxSelectRows
        {
            get
            {
                return _MaxSelectRows;
            }
            set
            {
                _MaxSelectRows = value;
            }
        }

        /// <summary>
        /// 设置是否隐藏掉属性按钮显示
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement]
        [DefaultValue(false)]
        public bool HideProperty
        {
            get { return _HideProperty; }
            set { _HideProperty = value; }
        }

        /// <summary>
        /// 获取数据属性的调用描述信息。 add by aifang
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement("InvokeDataPropertyDesc")]
        public InvokeDataPropertyDescInfo InvokeDataPropertyDesc
        {
            get
            {
                return _InvokeDataPropertyDesc;
            }
            set
            {
                _InvokeDataPropertyDesc = value;
            }
        }

        /// <summary>
        /// 设置区域控件级别
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement]
        [DefaultValue(4)]
        public int Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        /// <summary>
        /// 获取数据属性的调用描述信息。 add by aifang 2012-08-22
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement("DataAssistantContextMenu")]
        public DataAssistantContextMenuInfo DataAssistantContextMenu
        {
            get
            {
                return _DataAssistantContextMenu;
            }
            set
            {
                _DataAssistantContextMenu = value;
            }
        }
        
        /// <summary>
        /// 是否隐藏右键菜单项  add by aifang 2012-08-22
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement]
        [DefaultValue(true)]
        public bool HideContextMenu
        {
            get { return _HideContextMenu; }
            set { _HideContextMenu = value; }
        }

        /// <summary>
        /// 是不是需要从DataSource中去验证 EditControlType.ComboCheckedListBox,EditControlType.LookUpEdit中的值
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement]
        [DefaultValue(true)]
        public bool IsValidateInputFromDataSource {
            get { return _IsValidateInputFromDataSource; }
            set { _IsValidateInputFromDataSource = value; }
        }


        #endregion Public 属性...

        /// <summary>
        /// 创建数据助手的函数。
        /// </summary>
        [XmlIgnore]
        public Func<object, MB.WinBase.IFace.IDataAssistant> CreateDataAssistant
        {
            get
            {
                return _CreateDataAssistant;
            }
            set
            {
                _CreateDataAssistant = value;
            }
        }
    }



    /// <summary>
    /// 调用数据的描述信息。
    /// </summary>
    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    [XmlRoot("InvokeDataSourceDesc")]
    public class InvokeDataSourceDescInfo {
        private string _Type;
        private string _Method;
        private string _TypeConstructParams;
        private bool _ExistsFilterParams;

        public InvokeDataSourceDescInfo() {

            _ExistsFilterParams = true;
        }
        /// <summary>
        /// 调用数据的描述信息
        /// </summary>
        /// <param name="invokeType"></param>
        /// <param name="invokeMethod"></param>
        /// <param name="constructPar"></param>
        public InvokeDataSourceDescInfo(string invokeType,string invokeMethod,string constructPar) {

            _ExistsFilterParams = true;
            _Type = invokeType;
            _Method = invokeMethod;
            _TypeConstructParams = constructPar; 
        }

        /// <summary>
        /// 类型配置。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string Type {
            get {
                return _Type;
            }
            set {
                _Type = value;
            }
        }
        /// <summary>
        /// 调用方法配置。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string Method {
            get {
                return _Method;
            }
            set {
                _Method = value;
            }
        }
        /// <summary>
        /// 实例化类型需要的构函数参数。
        /// 在配置的时候最好都配置成字符类型的参数。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string TypeConstructParams {
            get {
                return _TypeConstructParams;
            }
            set {
                _TypeConstructParams = value;
            }
        }
        /// <summary>
        /// 判断调用的方法是否存在查询的参数。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        [DefaultValue(false)]
        public bool ExistsFilterParams {
            get {
                return _ExistsFilterParams;
            }
            set {
                _ExistsFilterParams = value;
            }
        }

    }

    /// <summary>
    /// 单击button click 按钮时调用的窗口选择的数据配置信息。
    /// </summary>
    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    public class ClickButtonShowFormCfgInfo {
        private string _Type;//自定义弹出窗口类型。
        private string _TypeConstructParams; //弹出窗口构造函数参数配置
        private bool _ShowMessageOnValidated;//
        /// <summary>
        /// 调用的数据查询窗口。 
        /// </summary>
        [PropertyXmlConfig]
        public string Type {
            get { return _Type; }
            set { _Type = value; }
        }
        /// <summary>
        /// 实例化类型需要的构函数参数。
        /// 在配置的时候最好都配置成字符类型的参数。
        /// </summary>
        [PropertyXmlConfig]
        public string TypeConstructParams {
            get { return _TypeConstructParams; }
            set { _TypeConstructParams = value; }
        }
        /// <summary>
        /// 判断ButtonInput 焦点离开数据验证后是否显示Message.
        /// </summary>
        [PropertyXmlConfig]
        public bool ShowMessageOnValidated {
            get { return _ShowMessageOnValidated; }
            set { _ShowMessageOnValidated = value; }
        }


    }

    #region LookUpColumnInfo...
    /// <summary>
    /// LookUpColumnInfo 弹出列表框的描述信息。
    /// </summary>
    [ModelXmlConfig(ByXmlNodeAttribute = false)]
    public class LookUpColumnInfo {
        private string _FieldName;
        private string _Description;
        private int _ShowWidth;

        public LookUpColumnInfo() {
        }

        [PropertyXmlConfig]
        public string FieldName {
            get {
                return _FieldName;
            }
            set {
                _FieldName = value;
            }
        }
        [PropertyXmlConfig]
        public string Description {
            get {
                return _Description;
            }
            set {
                _Description = value;
            }
        }
        [PropertyXmlConfig]
        public int ShowWidth {
            get {
                return _ShowWidth;
            }
            set {
                _ShowWidth = value;
            }
        }
    }
    #endregion LookUpColumnInfo...

    #region CtlEditMappingInfo...
    /// <summary>
    /// 编辑控件列相关绑定信息
    /// </summary>
    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    public class EditCtlDataMappingInfo {
        private string _ColumnName;
        private string _SourceColumnName;

        public EditCtlDataMappingInfo() {
        }
        /// <summary>
        /// 控件绑定列的名称
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string ColumnName {
            get {
                return _ColumnName;
            }
            set {
                _ColumnName = value;
            }
        }
        /// <summary>
        ///  数据源的名称...
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string SourceColumnName {
            get {
                return _SourceColumnName;
            }
            set {
                _SourceColumnName = value;
            }
        }
    }
    #endregion CtlEditMappingInfo...

    #region InvokeDataPropertyDescInfo...
    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    public class InvokeDataPropertyDescInfo
    {
        private string _Type;
        private string _Method;
        private string _TypeConstructParams;

        public InvokeDataPropertyDescInfo()
        {
        }
        /// <summary>
        /// 调用数据的描述信息
        /// </summary>
        /// <param name="invokeType"></param>
        /// <param name="invokeMethod"></param>
        /// <param name="constructPar"></param>
        public InvokeDataPropertyDescInfo(string invokeType, string invokeMethod, string typeConstructParams)
        {
            _Type = invokeType;
            _Method = invokeMethod;
            _TypeConstructParams = typeConstructParams;
        }

        /// <summary>
        /// 类型配置。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }
        /// <summary>
        /// 调用方法配置。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string Method
        {
            get
            {
                return _Method;
            }
            set
            {
                _Method = value;
            }
        }
        /// <summary>
        /// 实例化类型需要的构函数参数。
        /// 在配置的时候最好都配置成字符类型的参数。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string TypeConstructParams
        {
            get
            {
                return _TypeConstructParams;
            }
            set
            {
                _TypeConstructParams = value;
            }
        }
    }
    #endregion InvokeDataPropertyDescInfo...

    /// <summary>
    /// 单击button click 按钮时调用的窗口选择的数据配置信息。
    /// </summary>
    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    public class DataAssistantContextMenuInfo
    {
        private string _Type;//自定义弹出窗口类型。
        private string _TypeConstructParams; //弹出窗口构造函数参数配置
        /// <summary>
        /// 调用的数据查询窗口。 
        /// </summary>
        [PropertyXmlConfig]
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        /// <summary>
        /// 实例化类型需要的构函数参数。
        /// 在配置的时候最好都配置成字符类型的参数。
        /// </summary>
        [PropertyXmlConfig]
        public string TypeConstructParams
        {
            get { return _TypeConstructParams; }
            set { _TypeConstructParams = value; }
        }
    }
}
