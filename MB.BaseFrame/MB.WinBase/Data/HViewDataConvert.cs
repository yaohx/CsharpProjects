//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-09-03
// Description	:	行横向转换处理相关。
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;


using MB.WinBase.Common;

namespace MB.WinBase.Data {
    /// <summary>
    /// 数据集合横向转换处理相关。
    /// </summary>
    public class HViewDataConvert<T> : HViewDataConvert {
        #region 变量定义...
        private static readonly string DEFAULT_MAPPING_DATA_TYPE = "System.Int32";
        private Dictionary<DataRow, List<DynamicColumnValueMappingInfo<T>>> _DynamicColumnValueMappings;//动态编辑列的数据
        private List<DynamicColumnInfo> _DynamicColumns;//
        //public static readonly string ACTIVE_LEFT_NAME = MB.BaseFrame.SOD.DYNAMIC_COLUMN_NAME;
        private IList _IniEntitys;
        private DataTable _DtEditData;
        private int _DynamicColumnCount;
        private HViewConvertCfgParam _ConvertCfgParam;

        #endregion 变量定义...

        
        /// <summary>
        ///  数据集合横向转换处理相关。
        /// </summary>
        public HViewDataConvert(HViewConvertCfgParam convertCfgParam) {
            _DynamicColumnValueMappings = new Dictionary<DataRow, List<DynamicColumnValueMappingInfo<T>>>();
            _DynamicColumns = new List<DynamicColumnInfo>();

            _ConvertCfgParam = convertCfgParam;
            if (_ConvertCfgParam.RowAreaColumns == null || _ConvertCfgParam.RowAreaColumns.Length == 0)
                throw new MB.Util.APPException("动态列数据集转换RowAreaColumns配置不能为空");
            if (_ConvertCfgParam.ColumnAreaCfgInfo == null)
                throw new MB.Util.APPException("动态列数据集转换ColumnAreaCfgInfo配置不能为空");
            if (string.IsNullOrEmpty(_ConvertCfgParam.ColumnAreaCfgInfo.ValueColumnName))
                throw new MB.Util.APPException("动态列数据集转换ColumnAreaCfgInfo.ValueColumnName配置不能为空");
            if (_ConvertCfgParam.ColumnAreaCfgInfo.MappingColumnName == null || _ConvertCfgParam.ColumnAreaCfgInfo.MappingColumnName.Length == 0)
                throw new MB.Util.APPException("动态列数据集转换ColumnAreaCfgInfo.MappingColumnName配置不能为空");

            //add by aifang 2012-04-18 判断可编辑列是否为转换键值中的列
            if (_ConvertCfgParam.EditableColumns != null)
            {
                var intersect = _ConvertCfgParam.ConvertKeyColumns.Intersect(_ConvertCfgParam.EditableColumns);
                if (intersect != null && intersect.Count() > 0)
                    throw new MB.Util.APPException("可编辑列不能为转换键值中的列，请检查!");
            }

        }

        /// <summary>
        /// 数据横向转换。
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="colPropertys"></param>
        /// <returns></returns>
        public DataTable Convert(List<T> dataSource, Dictionary<string, ColumnPropertyInfo> colPropertys) {
            _DynamicColumnValueMappings.Clear();
          //  if (_ConvertCfgParam.DynamicColumnCaption) {
                return convertToDynamicCaption(dataSource, colPropertys);
          //  }
          //  else {
          //     return convertToUnDynamicCaption(dataSource, colPropertys);
          //  }
        }

        //数据集合横向转换(带有动态Caption 的动态列)。
        private DataTable convertToDynamicCaption(List<T> dataSource, Dictionary<string, ColumnPropertyInfo> colPropertys) {
            try {
                _IniEntitys = dataSource;

                _DtEditData = createNewTable(dataSource, colPropertys);
                if (_DtEditData == null) return null;

                string oldKeyValue = string.Empty;
                DataRow currEditRow = null;
 
                int hColIndex = 0;
                if(_ConvertCfgParam.DynamicColumnCaption)
                    _DynamicColumnCount = 0;

                //根据款式编码 、颜色 和 规格编码进行排序
             //  dataSource.Sort(new Comparison<T>(hViewDataCompareSort));
               SortedList<string, T> sortDataSource = convertToSortList(dataSource);
               var lstEntitys = sortDataSource.OrderBy(o => o.Key);
               Type tp = typeof(T);
               PropertyInfo proVal = tp.GetProperty(_ConvertCfgParam.ColumnAreaCfgInfo.ValueColumnName);
               PropertyInfo proTxt = tp.GetProperty(_ConvertCfgParam.ColumnAreaCfgInfo.CaptionColumnName);
               PropertyInfo proSort = tp.GetProperty(_ConvertCfgParam.ColumnAreaCfgInfo.OrderColumnName);

               List<PropertyInfo> rowAreaPros = new List<PropertyInfo>(); 
               foreach (string colName in _ConvertCfgParam.RowAreaColumns) {
                   rowAreaPros.Add(tp.GetProperty(colName));
               }
               List<PropertyInfo> mappingPros = new List<PropertyInfo>();
               foreach (string cName in _ConvertCfgParam.ColumnAreaCfgInfo.MappingColumnName) {
                   mappingPros.Add(tp.GetProperty(cName));
               }
               //add by aifang 2012-4-19 增加可编辑列及键值列可编辑控制
               if (_ConvertCfgParam.EditableColumns != null)
               {
                   foreach (string eName in _ConvertCfgParam.EditableColumns)
                   {
                       if (!colPropertys.ContainsKey(eName)) continue;
                       var colPropertyInfo = colPropertys[eName];
                       colPropertyInfo.CanEdit = true;
                   }
                   foreach (string kName in _ConvertCfgParam.ConvertKeyColumns)
                   {
                       if (!colPropertys.ContainsKey(kName)) continue;
                       var colPropertyInfo = colPropertys[kName];
                       colPropertyInfo.CanEdit = false;
                   }
               }
               //add by aifang 2012-4-19 增加可编辑列及键值列可编辑控制

               foreach (var v in lstEntitys) {
                    T entity = v.Value; 
                    string keyValue = getkeyStr(entity);
                    if (_ConvertCfgParam.DynamicColumnCaption) {
                        if (hColIndex > _DynamicColumnCount - 1) {
                            //创建动态列
                            _DynamicColumnCount = hColIndex + 1;
                            createDynamicColumn(_DtEditData, hColIndex);
                        }
                    }

                    if (string.Compare(oldKeyValue, keyValue, true) != 0) {
                        hColIndex = 0;
                        oldKeyValue = keyValue;
                        currEditRow = _DtEditData.NewRow();
                        _DynamicColumnValueMappings.Add(currEditRow, new List<DynamicColumnValueMappingInfo<T>>());

                        //判断是否可以设置纵向列的值
                        try {
                            foreach (var colName in rowAreaPros) {
                                object colValue = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, colName);
                                if (colValue != null && colValue != System.DBNull.Value)
                                    currEditRow[colName.Name] = colValue;
                            }
                        }
                        catch (Exception lex) {
                            throw lex;
                        }

                        _DtEditData.Rows.Add(currEditRow);
                    }
                    string val = MB.Util.MyReflection.Instance.InvokePropertyForGet<string>(entity, proVal);
                    string txt = MB.Util.MyReflection.Instance.InvokePropertyForGet<string>(entity, proTxt);
                    string sortVal = MB.Util.MyReflection.Instance.InvokePropertyForGet<string>(entity, proSort);
                    //如果是非动态Caption 列，这个HColIndex 需要根据 Val 值从  _DynamicColumns 获取Index
                    int colIndex = _ConvertCfgParam.DynamicColumnCaption ? hColIndex : getUnDynamicoCaptionColumnIndex(val);
                    _DynamicColumnValueMappings[currEditRow].Add(new DynamicColumnValueMappingInfo<T>(colIndex, entity, val, txt, sortVal));
                    try {

                        foreach (var cName in mappingPros) {
                            object mappingVal = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, cName);

                            string colName = CreateDynamicColumnFieldName(cName.Name, colIndex);
                            if (mappingVal != null && mappingVal != System.DBNull.Value)
                                currEditRow[colName] = mappingVal;
                        }
                    }
                    catch (Exception aex) {
                        throw new MB.Util.APPException("执行 convertToDynamicCaption 时, 设置Mapping 列的值出错" + aex.Message);
                    }

                    hColIndex++;
                }
                return _DtEditData;
            }
            catch (Exception ex) {
                throw MB.Util.APPExceptionHandlerHelper.PromoteException(ex, MB.Util.APPMessageType.SysErrInfo);  
                //throw new MB.Util.APPException("动态列转换出错!" + ex.Message);
            }
        }
       //数据横向转换，转换以后列的Caption 是固定的，不会随着行的 Index 不同而发生改变。
        //private DataTable convertToUnDynamicCaption(List<T> dataSource, Dictionary<string, ColumnPropertyInfo> colPropertys) {
        //    try {
        //        _IniEntitys = dataSource;

        //        _DtEditData = createNewTable(dataSource, colPropertys);
        //        if (_DtEditData == null) return null;

        //        string oldKeyValue = string.Empty;
        //        DataRow currEditRow = null;
        //        //根据款式编码 、颜色 和 规格编码进行排序
        //        dataSource.Sort(new Comparison<T>(hViewDataCompareSort));
        //        foreach (T entity in dataSource) {
        //            string keyValue = getkeyStr(entity);

        //            if (string.Compare(oldKeyValue, keyValue, true) != 0) {
        //                oldKeyValue = keyValue;
        //                currEditRow = _DtEditData.NewRow();
        //                //判断是否可以设置纵向列的值
        //                try {
        //                    foreach (string colName in _ConvertCfgParam.RowAreaColumns) {

        //                        currEditRow[colName] = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, colName);
        //                    }
        //                }
        //                catch (Exception lex) {
        //                    throw lex;
        //                }

        //                _DtEditData.Rows.Add(currEditRow);
        //            }

        //            try {
        //                string columnCodeValue = MB.Util.MyReflection.Instance.InvokePropertyForGet<string>(entity, _ConvertCfgParam.ColumnAreaCfgInfo.ValueColumnName );
        //                foreach (string aName in _ConvertCfgParam.ColumnAreaCfgInfo.MappingColumnName) {
        //                    string mappingVal = MB.Util.MyReflection.Instance.InvokePropertyForGet<string>(entity, aName);
        //                    string colName = string.Format(MB.BaseFrame.SOD.DYNAMIC_COLUMN_NAME, aName, columnCodeValue);
        //                    currEditRow[colName] = mappingVal;
        //                }
        //            }
        //            catch (Exception aex) {
        //                throw new MB.Util.APPException("在转换横向数据源时,设置Mapping 列的值出错" + aex.Message) ;
        //            }
        //        }
        //        return _DtEditData;
        //    }
        //    catch (Exception ex) {
        //        throw  MB.Util.APPExceptionHandlerHelper.PromoteException(ex, MB.Util.APPMessageType.SysErrInfo);
        //    }
        //}
        #region 内部函数处理相关...
        //动态列排序
        private int hViewDataCompareSort(T entity1, T entity2) {
            try {
                string[] sorFields = _ConvertCfgParam.ConvertKeyColumns; 
                List<string> xOrders = new List<string>();
                foreach (string xOrder in _ConvertCfgParam.ConvertKeyColumns) {
                    string x = MB.Util.MyReflection.Instance.InvokePropertyForGet<string>(entity1, xOrder);
                    xOrders.Add(string.IsNullOrEmpty(x)?"": x.ToString());
                }
                //增加横向列的转换键值
                string xHView = MB.Util.MyReflection.Instance.InvokePropertyForGet<string>(entity1, _ConvertCfgParam.ColumnAreaCfgInfo.OrderColumnName);
                xOrders.Add(xHView);

                List<string> yOrders = new List<string>();
                foreach (string yOrder in _ConvertCfgParam.ConvertKeyColumns) {
                    string y = MB.Util.MyReflection.Instance.InvokePropertyForGet<string>(entity2, yOrder);
                    yOrders.Add(string.IsNullOrEmpty(y)?"":y.ToString());
                }
                //增加横向列的转换键值
                string yHView = MB.Util.MyReflection.Instance.InvokePropertyForGet<string>(entity2, _ConvertCfgParam.ColumnAreaCfgInfo.OrderColumnName);
                yOrders.Add(yHView);
                return ((new CaseInsensitiveComparer()).Compare(string.Join(" ", xOrders.ToArray()), string.Join(" ", yOrders.ToArray())));
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(string.Format("在进行动态列转换时进行实体对象排序出错,可能是排序的列名称{0} 配置有误！", _ConvertCfgParam.ColumnAreaCfgInfo.OrderColumnName) + ex.Message);
            }
        }
        //转换为可以进行排序的集合
        private SortedList<string, T> convertToSortList(List<T> dataSource) {
            SortedList<string, T> sortDataSource = new SortedList<string, T>();
            string[] sortFields = _ConvertCfgParam.ConvertKeyColumns;
            Type tp = typeof(T);
            PropertyInfo proSort = tp.GetProperty(_ConvertCfgParam.ColumnAreaCfgInfo.OrderColumnName);
            List<PropertyInfo> convertPros = new List<PropertyInfo>(); 
            foreach (string xOrder in _ConvertCfgParam.ConvertKeyColumns) {
                convertPros.Add(tp.GetProperty(xOrder));
            }
            foreach (T entity in dataSource) {
                List<string> orders = new List<string>();
                foreach (var xOrder in convertPros) {
                    string x = MB.Util.MyReflection.Instance.InvokePropertyForGet<string>(entity, xOrder);
                    orders.Add(x.ToString());
                }
                //增加横向列的转换键值
                string hViewValue = MB.Util.MyReflection.Instance.InvokePropertyForGet<string>(entity, proSort);
                orders.Add(hViewValue);

                sortDataSource.Add(string.Join(" ", orders.ToArray()), entity);

            }
            return sortDataSource;
        }
        //根据keys的字段得到该行中关键字段组成的字符窜...
        private string getkeyStr(T entity) {
            try {
                if (_ConvertCfgParam.ConvertKeyColumns.Length == 1) {
                    string keyName = _ConvertCfgParam.ConvertKeyColumns[0];
                    object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, keyName);
                    return val == null ? " " : val.ToString();
                }
                else {
                    List<string> keys = new List<string>();
                    foreach (string keyName in _ConvertCfgParam.ConvertKeyColumns) {
                        object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, keyName);
                        if (val == null)
                            keys.Add(" ");
                        else
                            keys.Add(val.ToString());
                    }

                    //根据款式编码和颜色编码组成键值；
                    return string.Join(" ", keys.ToArray());
               }
            }
            catch (Exception ex) {
                throw new MB.Util.APPException("在进行动态列转换时获取实体对象的比较键值出错！" + ex.Message);
            }
        }

        //增加相应的横向转换的列并构造DataTable
        private DataTable createNewTable(List<T> dataSource, Dictionary<string, ColumnPropertyInfo> colPropertys) {
                DataTable newDt = new DataTable();
                //如果存在XML 配置的信息那么通过XML 列配置创建动态编辑列表。
                if (colPropertys != null && colPropertys.Count > 0) {
                    foreach (ColumnPropertyInfo colInfo in colPropertys.Values) {
                        if (Array.IndexOf<string>(_ConvertCfgParam.RowAreaColumns, colInfo.Name) < 0) continue;

                        newDt.Columns.Add(colInfo.Name, System.Type.GetType(colInfo.DataType));
                    }
                }
                else {//通过实体字段的原数据类型来创建
                    Type entityType = typeof(T);
                    var pros = entityType.GetProperties();
                    foreach (var pro in pros) {
                        if (Array.IndexOf<string>(_ConvertCfgParam.RowAreaColumns, pro.Name) < 0) continue;

                        newDt.Columns.Add(pro.Name, System.Type.GetType(pro.PropertyType.FullName));
                    }
                }

                //判断是否为动态固定Caption 列
                if (!_ConvertCfgParam.DynamicColumnCaption) {
                    List<DynamicColumnInfo> hViewValues = getAllHViewValue(dataSource);
                    _DynamicColumns = hViewValues;
                    if (hViewValues != null && hViewValues.Count > 0) {
                        try {
                            string[] mapCols = _ConvertCfgParam.ColumnAreaCfgInfo.MappingColumnName;
                            string[] mapTypes = _ConvertCfgParam.ColumnAreaCfgInfo.MappingColumnType;

                            for (int index = 0; index < hViewValues.Count; index ++ ) {
                                for (int i = 0; i < mapCols.Length; i++) {
                                    string colName = CreateDynamicColumnFieldName(mapCols[i], index);
                                    Type dataType = typeof(System.Int32);
                                    if (mapTypes != null && mapTypes.Length > i)
                                        dataType = System.Type.GetType(mapTypes[i]);
                                    newDt.Columns.Add(colName, dataType);


                                }
                            }
                        }
                        catch (Exception ex) {
                            throw new MB.Util.APPException("横向转换数据时,创建动态列出错" + ex.Message);
                        }
                    }
                    _DynamicColumnCount = _DynamicColumns.Count; 
                }
                return newDt;
       
        }
        //根据动态列的值获取动态列的Index
        private int getUnDynamicoCaptionColumnIndex(string columnValue) {
            int index = _DynamicColumns.FindIndex(o => o.ColumnValueCode == columnValue);
            if (index < 0)
                throw new MB.Util.APPException(string.Format("根据动态列的编码 {0} 获取动态列的Index 有误",columnValue));
            return index;
        }
        //得到记录集中的所有HView 列的值 的记录
        private List<DynamicColumnInfo> getAllHViewValue(List<T> dataSource) {
           
            Dictionary<string, DynamicColumnInfo> drcDatas = new Dictionary<string, DynamicColumnInfo>();
            if (dataSource == null || dataSource.Count ==0) {
                throw new MB.Util.APPException("动态获取横向字段的值出错,有可能是传入的数据源为空");
            }
            try {
                Type tp = typeof(T);
                PropertyInfo proVal = tp.GetProperty(_ConvertCfgParam.ColumnAreaCfgInfo.ValueColumnName);
                PropertyInfo proTxt = tp.GetProperty(_ConvertCfgParam.ColumnAreaCfgInfo.CaptionColumnName);
                PropertyInfo proSort = tp.GetProperty(_ConvertCfgParam.ColumnAreaCfgInfo.OrderColumnName);

                foreach (T entity in dataSource) {
                    string val = MB.Util.MyReflection.Instance.InvokePropertyForGet<string>(entity, proVal);
                    string txt = MB.Util.MyReflection.Instance.InvokePropertyForGet<string>(entity, proTxt);
                    string sortVal = MB.Util.MyReflection.Instance.InvokePropertyForGet<string>(entity, proSort);
                    //if (valueList.FirstOrDefault(o => o.ColumnValueCode==val) != null)
                    //    continue;
                    if (drcDatas.ContainsKey(val)) continue;
                    drcDatas.Add(val, new DynamicColumnInfo(val, txt, sortVal));
                   // valueList.Add();
                }
                SortedList<string, DynamicColumnInfo> sortLst = new SortedList<string, DynamicColumnInfo>();
                foreach (var info in drcDatas.Values) {
                    sortLst.Add(info.SortValue, info); 
                }
                //List<DynamicColumnInfo> valueList = new List<DynamicColumnInfo>();
                //var orderDatas = valueList.OrderBy(o => o.SortValue);
                return sortLst.Values.ToList<DynamicColumnInfo>();  
                //List<DynamicColumnInfo> orderLst = new List<DynamicColumnInfo>();
                //foreach (var data in orderDatas)
                //    orderLst.Add(data);
                //return orderLst;
            }
            catch (Exception ex) {
                throw new MB.Util.APPException("横向转换数据源时,getAllHViewValue(List<T> dataSource)  出错" + ex.Message);
            }
        }

        //创建动态列
        private void createDynamicColumn(DataTable dtData, int index) {
            try {
                int count = _ConvertCfgParam.ColumnAreaCfgInfo.MappingColumnName.Length;
                for (int i = 0; i < count; i++) {
                    string typeName = DEFAULT_MAPPING_DATA_TYPE;
                    if (_ConvertCfgParam.ColumnAreaCfgInfo.MappingColumnType != null && _ConvertCfgParam.ColumnAreaCfgInfo.MappingColumnType.Length >= i + 1)
                        typeName = _ConvertCfgParam.ColumnAreaCfgInfo.MappingColumnType[i];

                    Type dataType = System.Type.GetType(typeName);
                    string colName = CreateDynamicColumnFieldName(_ConvertCfgParam.ColumnAreaCfgInfo.MappingColumnName[i], index);
                    dtData.Columns.Add(colName, dataType);
                }
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(string.Format("在进行动态列转换时获取数据类型出错,可能是类型{0}配置有误！", _ConvertCfgParam.ColumnAreaCfgInfo.MappingColumnType) + ex.Message);
            }
        }
        #endregion 内部函数处理相关...

        #region Public 属性...
        /// <summary>
        /// 转换过后的动态列信息。
        /// </summary>
        public List<DynamicColumnInfo> DynamicColumns {
            get {
                return _DynamicColumns; 
            }
        }
        /// <summary>
        /// 获取动态的横向转换的列并带有列的动态描述信息。
        /// </summary>
        public Dictionary<DataRow, List<DynamicColumnValueMappingInfo<T>>> DynamicColumnValueMappings {
            get {
                return _DynamicColumnValueMappings;
            }
        }
        /// <summary>
        /// 获取当前编辑绑定的数据集。
        /// </summary>
        public DataTable CurrentEditData {
            get {
                return _DtEditData;
            }
            set {
                _DtEditData = value;
            }
        }
        /// <summary>
        /// 最大列的数量。
        /// </summary>
        public int DynamicColumnCount {
            get {
                return _DynamicColumnCount;
            }
        }
        /// <summary>
        /// 转换配置的信息。
        /// </summary>
        public HViewConvertCfgParam ConvertCfgParam {
            get {
                return _ConvertCfgParam;
            }
        }
        #endregion Public 属性...
    }

    #region 动态列转换实体模型定义相关...
    /// <summary>
    /// 数据横向转换需要的参数设置。
    /// </summary>
    [MB.Util.XmlConfig.ModelXmlConfig(ByXmlNodeAttribute = false)]
    public class HViewConvertCfgParam {
        public HViewConvertCfgParam() {
            _ColumnAreaCfgInfo = new DynamicColumnCfgInfo();
        }
        private bool _DynamicColumnCaption;
        /// <summary>
        /// 判断动态列的Caption 是否为随着行的不同而发生变化
        /// </summary>
        [DataMember]
        [MB.Util.XmlConfig.PropertyXmlConfig]
        public bool DynamicColumnCaption {
            get {
                return _DynamicColumnCaption;
            }
            set {
                _DynamicColumnCaption = value;
            }
        }

        private string[] _ConvertKeyColumns;
        /// <summary>
        /// 需要转换为动态列的键名称。
        /// </summary>
        [DataMember]
        [MB.Util.XmlConfig.PropertyXmlConfig]
        public string[] ConvertKeyColumns {
            get { return _ConvertKeyColumns; }
            set { _ConvertKeyColumns = value; }
        }

        private DynamicColumnCfgInfo _ColumnAreaCfgInfo;
        /// <summary>
        /// 动态列的配置区域。
        /// </summary>
        [DataMember]
        [MB.Util.XmlConfig.PropertyXmlConfig(typeof(DynamicColumnCfgInfo))]
        public DynamicColumnCfgInfo ColumnAreaCfgInfo {
            get { return _ColumnAreaCfgInfo; }
            set { _ColumnAreaCfgInfo = value; }
        }
        private string[] _RowAreaColumns;
        /// <summary>
        /// 行列的配置区域。
        /// </summary>
        [DataMember]
        [MB.Util.XmlConfig.PropertyXmlConfig]
        public string[] RowAreaColumns {
            get { return _RowAreaColumns; }
            set { _RowAreaColumns = value; }
        }

        private string[] _EditableColumns;
        /// <summary>
        /// 行列的配置区域可编辑的列。
        /// </summary>
        [DataMember]
        [MB.Util.XmlConfig.PropertyXmlConfig]
        public string[] EditableColumns
        {
            get { return _EditableColumns; }
            set { _EditableColumns = value; }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class HViewDataConvert {
        /// <summary>
        /// 根据Mapping 的列名称和 Index 构建动态列的字段名称。
        /// </summary>
        /// <param name="mappingName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string CreateDynamicColumnFieldName(string mappingName, int index) {
            return string.Format(MB.BaseFrame.SOD.DYNAMIC_COLUMN_NAME, mappingName, index);
        }
        /// <summary>
        /// 根据列的名称获取对应的Index。
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static int GetIndexByColumnFieldName(string columnName) {
            int s = columnName.LastIndexOf(MB.BaseFrame.SOD.DYNAMC_SEPARATOR_CHAR);
            try {
                return int.Parse(columnName.Substring(s + 1, columnName.Length - s -1));
            }
            catch {
                return -1;
            }
        }
        /// <summary>
        /// 根据动态列名获取对应Mapping 的列
        /// </summary>
        /// <param name="dynamicFieldName"></param>
        /// <returns></returns>
        public static string GetMappingFieldName(string dynamicFieldName) {
            try {
                int s = dynamicFieldName.LastIndexOf(MB.BaseFrame.SOD.DYNAMC_SEPARATOR_CHAR);
                string L = dynamicFieldName.Substring(0, s);
                s = L.LastIndexOf(MB.BaseFrame.SOD.DYNAMC_SEPARATOR_CHAR);
                string name = L.Substring(s + 1, L.Length - s - 1);
                return name;
            }
            catch (Exception ex) {
                throw new MB.Util.APPException(string.Format("根据动态列名{0} 获取对应Mapping 的列有误 ",dynamicFieldName) + ex.Message, MB.Util.APPMessageType.SysErrInfo); 
            }
        }
    }
    /// <summary>
    /// 动态列的配置信息。
    /// </summary>
    [MB.Util.XmlConfig.ModelXmlConfig(ByXmlNodeAttribute = false)]
    public class DynamicColumnCfgInfo {
        /// <summary>
        ///  动态列的配置信息。
        /// </summary>
        public DynamicColumnCfgInfo() {
            //_MappingColumnType = "System.Int32";
        }
        /// <summary>
        ///  动态列的配置信息。
        /// </summary>
        /// <param name="valueColumnName"></param>
        public DynamicColumnCfgInfo(string valueColumnName) {
            _ValueColumnName = valueColumnName;
            _CaptionColumnName = valueColumnName;
            _OrderColumnName =  valueColumnName ;
            // _MappingColumnType = "System.Int32";
        }
        private string _ValueColumnName;  //值列的名称
        /// <summary>
        /// 值列的名称
        /// </summary>
        [DataMember]
        [MB.Util.XmlConfig.PropertyXmlConfig]
        public string ValueColumnName {
            get { return _ValueColumnName; }
            set { _ValueColumnName = value; }
        }
        private string _CaptionColumnName;//描述列的名称
        /// <summary>
        /// 描述列的名称
        /// </summary>
        [DataMember]
        [MB.Util.XmlConfig.PropertyXmlConfig]
        public string CaptionColumnName {
            get {
                if (string.IsNullOrEmpty(_CaptionColumnName))
                    return _ValueColumnName;
                return _CaptionColumnName;
            }
            set { _CaptionColumnName = value; }
        }
        private string _OrderColumnName;  //排序列的名称
        /// <summary>
        /// 排序列的名称
        /// </summary>
        [DataMember]
        [MB.Util.XmlConfig.PropertyXmlConfig]
        public string OrderColumnName {
            get {
                if (string.IsNullOrEmpty(_OrderColumnName))
                    return  _ValueColumnName ;
                return _OrderColumnName;
            }
            set { _OrderColumnName = value; }
        }
        private string[] _MappingColumnName;
        /// <summary>
        /// 横向转换后映射到列的名称。
        /// </summary>
        [DataMember]
        [MB.Util.XmlConfig.PropertyXmlConfig]
        public string[] MappingColumnName {
            get { return _MappingColumnName; }
            set { _MappingColumnName = value; }
        }

        private string[] _MappingColumnType; //对应映射列的数据类型
        /// <summary>
        /// 对应映射列的数据类型
        /// </summary>
        [DataMember]
        [MB.Util.XmlConfig.PropertyXmlConfig]
        public string[] MappingColumnType {
            get { return _MappingColumnType; }
            set { _MappingColumnType = value; }
        }

    }

    /// <summary>
    /// 动态列并且是动态列Caption的列描述信息。
    /// </summary>
    public class DynamicColumnValueMappingInfo<T> : DynamicColumnInfo {
        private int _Index;

        private T _DetailEntity;

        /// <summary>
        /// 动态Caption 需要实例化的类。
        /// </summary>
        public DynamicColumnValueMappingInfo(int index, T detailEntity,string columnValueCode, string caption,string sortValue) : 
                                    base(columnValueCode,caption,sortValue) {
            _Index = index;
            _DetailEntity = detailEntity;

        }

        #region public 属性...
        /// <summary>
        /// 在动态列中的位置。
        /// </summary>
        public int Index {
            get {
                return _Index;
            }
            set {
                _Index = value;
            }
        }

        /// <summary>
        /// 描述。
        /// </summary>
        public T DetailEntity {
            get {
                return _DetailEntity;
            }
            set {
                _DetailEntity = value;
            }
        }

        #endregion public 属性...
    }
    /// <summary>
    /// 动态列描述信息
    /// </summary>
    public class DynamicColumnInfo {
        private string _ColumnValueCode;
        private string _Caption;
        private string _SortValue;
 
        /// <summary>
        /// 动态列但是固定Caption 需要
        /// </summary>
        /// <param name="columnValueCode"></param>
        /// <param name="caption"></param>
        public DynamicColumnInfo(string columnValueCode, string caption,string sortValue) {
            _ColumnValueCode = columnValueCode;
            _Caption = caption;
            _SortValue = sortValue;
        }

        /// <summary>
        /// 列对应的值。
        /// </summary>
        public string ColumnValueCode {
            get {
                return _ColumnValueCode;
            }
            set {
                _ColumnValueCode = value;
            }
        }
        /// <summary>
        /// 动态列对应的描述。
        /// </summary>
        public string Caption {
            get {
                return _Caption;
            }
            set {
                _Caption = value;
            }
        }
        /// <summary>
        /// 动态列对应的描述。
        /// </summary>
        public string SortValue {
            get {
                return _SortValue;
            }
            set {
                _SortValue = value;
            }
        }
    }
    #endregion 动态列转换实体模型定义相关...
}
