using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MB.WinBase.Common;
using MB.WinBase.IFace;
using System.Reflection;
using MB.Util.Emit;
using System.Runtime.Serialization;
using MB.WinBase;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using MB.WinEIDrive.Excel;
using System.Drawing;
using System.Collections;
using MB.WinEIDrive;

namespace MB.WinClientDefault.OfficeAutomation {
    /// <summary>
    /// 在winform中提供的Excel编辑器
    /// </summary>
    /// <typeparam name="T">需要被编辑的实体</typeparam>
    public class WinformExcelEditor<T> : IExcelEditor where T : class{

        private static readonly string COL_DEFAULT_KEY = "ID"; //缺省的键名称
        private static readonly string COL_ENTITY_STATE_PROPERTY = "EntityState";
        private static readonly string SHEET_NAME = "WorkingSheet";

        private int _DocType;
        private WinBase.IFace.IClientRule _ClientRule;
        private Dictionary<string, T> _OrignalDataSource; //传进来需要被编辑的数据源
        private DataSet _ConvertedDataSource;  //把_OrignalDataSource数据源转换成DataSet
        private string[] _MainKeys; //支持逗号隔开的组合键值,构造函数通过,形式传入

        private string _ExcelFilePath = null;
        private string _XmlFile = null;
        private Dictionary<string, ColumnPropertyInfo> _Columns;
        private Dictionary<string, ColumnEditCfgInfo> _EditColumns;
        private Dictionary<string, T> _Values;         //缓存的初始化值
        private Dictionary<string, T> _SubmittedValue; //缓存编辑后的值
        private List<T> _SubmittedNewValue;         //新提交的数据
        private Dictionary<string, string> _HashedValues;   //把值HASH，进行比较
        private Dictionary<string, string> _SubmittedHashedValues; //哈希提交值，与原值做比较
        private List<string> _NonEditableColumns; //指定了那些列是不可被编辑的，等EXCEL提交以后从原来的ENTITY中再赋值回来
        private Dictionary<string, Dictionary<string, object>> _LookUpDataValueKey;
        private Dictionary<string, Dictionary<string, object>> _LookUpDataTextKey;
        private Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> _DAccs;

        #region 当提交excel数据的时候，转换clickInputButton的值的委托
        private System.Func<object, GetClickInputButtonSourceAfterSubmitEventArgs, DataSet> _GetClickInputButtonSourceAfterSubmit;
        /// <summary>
        /// 当提交excel数据后，在对ClickInputButton进行赋值的时候触发。
        /// 注册的事件主要提供clickInputButton的数据源
        /// 数据源中的字段是根据配置文件中的EditCtlDataMappingInfo中的SourceColumnName字段来决定的
        /// 需要返回的DataSet中。需要包括全部的SourceColumnName字段
        /// </summary>
        public event System.Func<object, GetClickInputButtonSourceAfterSubmitEventArgs, DataSet> GetClickInputButtonSourceAfterSubmit {
            add {
                _GetClickInputButtonSourceAfterSubmit += value;
            }
            remove {
                _GetClickInputButtonSourceAfterSubmit -= value;
            }
        }
        private DataSet onGetClickInputButtonSourceAfterSubmit(object sender, GetClickInputButtonSourceAfterSubmitEventArgs arg) {
            if (_GetClickInputButtonSourceAfterSubmit != null)
                return _GetClickInputButtonSourceAfterSubmit(this, arg);
            else
                return null;
        }
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数,使用默认主键：“ID”
        /// </summary>
        /// <param name="clientRule">客户端Rule</param>
        /// <param name="dataSource">客户端数据源</param>
        /// <param name="xmlFile">客户端UI Xml</param>
        /// <param name="docType">当前编辑的对象类型的值</param>
        public WinformExcelEditor(IClientRule clientRule, List<T> dataSource, string xmlFile,
            int docType) :
            this(clientRule, dataSource, xmlFile, docType, new string[] {COL_DEFAULT_KEY}) {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clientRule">客户端Rule</param>
        /// <param name="dataSource">客户端数据源</param>
        /// <param name="xmlFile">客户端UI Xml</param>
        /// <param name="docType">当前编辑的对象类型的值</param>
        /// <param name="mainKeys">主键的名字</param>
        /// <param name="mainKeyValues">主键的值</param>
        public WinformExcelEditor(IClientRule clientRule, 
            List<T> dataSource, string xmlFile, int docType, string[] mainKeys) {
            _ClientRule = clientRule;
            _XmlFile = xmlFile;
            _DocType = docType;
            _MainKeys = mainKeys;
            _DAccs = createDynamicProperyAccess(typeof(T));
            _Columns = LayoutXmlConfigHelper.Instance.GetColumnPropertys(_XmlFile);
            _EditColumns = LayoutXmlConfigHelper.Instance.GetColumnEdits(_Columns, _XmlFile);
            _OrignalDataSource = saveOrignialDataSourceToHashTable(dataSource);
            _ConvertedDataSource = convertListToDataSet(dataSource);
            if (_EditColumns != null && _EditColumns.Count > 0) {
                _LookUpDataValueKey = convertToHasTable(_EditColumns, false);
                _LookUpDataTextKey = convertToHasTable(_EditColumns, true);
            }
            _NonEditableColumns = new List<string>(); //在formatDataSourceToExcel被赋值
            _Values = new Dictionary<string, T>();
            _SubmittedValue = new Dictionary<string, T>();
            _SubmittedNewValue = new List<T>();
            _HashedValues = new Dictionary<string, string>();
            _SubmittedHashedValues = new Dictionary<string, string>();
        }

        #endregion


        #region IExcelEditor Members
        /// <summary>
        /// 打开Excel编辑
        /// </summary>
        /// <returns></returns>
        public string OpenExcel() {
            //在打开Excel前，先要把正在运行的Excel进程给杀掉
            //killExcelProcess();

            string filePath = generateTempXlsFilePath();
            formatDataSourceToExcel(_ConvertedDataSource.Tables[0]);
            List<T> dataSource = new List<T>();
            MB.Util.MyConvert.ConvertDataSetToEntity<T>(_ConvertedDataSource, dataSource, null);
            hashDataSource(dataSource);
            if (_EditColumns != null && _EditColumns.Count > 0)
                convertLookUpEditValueToText(_ConvertedDataSource.Tables[0]);

            sortByColumnSetting(_ConvertedDataSource.Tables[0]);

            ExcelEditHelper excelEditHelper = new ExcelEditHelper(filePath);
            excelEditHelper.LoadDataAsExcel(_MainKeys, _ConvertedDataSource.Tables[0]);
            _ExcelFilePath = filePath;
            return filePath;            
        }

        /// <summary>
        /// 提交excel中编辑的数据
        /// </summary>
        /// <returns>编辑数据的集合</returns>
        public IList Submit() {
            getSubmittedDataFromExcel();

            List<T> result = new List<T>();

            //处理被删除的情况，有一些列在原始缓存中存在，但提交的时候却没有了，标记为删除
            foreach (string key in _HashedValues.Keys) {
                if (!_SubmittedHashedValues.ContainsKey(key)) {
                    copyNonEditPropToSubmittedEntity(_OrignalDataSource[key], _Values[key]);
                    _DAccs[COL_ENTITY_STATE_PROPERTY].Set(_Values[key], MB.Util.Model.EntityState.Deleted);
                    result.Add(_Values[key]);
                }
            }

            foreach (string key in _SubmittedHashedValues.Keys) {
                bool isDifferent = _SubmittedHashedValues[key].CompareTo(_HashedValues[key]) != 0;
                if (isDifferent) {
                    copyNonEditPropToSubmittedEntity(_OrignalDataSource[key], _SubmittedValue[key]);
                    _DAccs[COL_ENTITY_STATE_PROPERTY].Set(_SubmittedValue[key], MB.Util.Model.EntityState.Modified);
                    result.Add(_SubmittedValue[key]);
                }
            }


            if (_SubmittedNewValue.Count > 0) {
                _SubmittedNewValue.ForEach(t => {
                    //注意返回的对象中需要设置主键，或者外键
                    _DAccs[COL_ENTITY_STATE_PROPERTY].Set(t, MB.Util.Model.EntityState.New);
                    result.Add(t);
                });
            }

            return result;

        }

        #endregion

        
        #region 内部函数

        #region 工具函数
        /// <summary>
        /// 创建动态对象的属性访问器，以便快速的动态访问
        /// </summary>
        /// <param name="typeObject">对象的类型</param>
        /// <returns>属性访问器，以字段存储</returns>
        private Dictionary<string, DynamicPropertyAccessor> createDynamicProperyAccess(Type typeObject) {
            PropertyInfo[] infos = typeObject.GetProperties();
            Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor> dAccs = new Dictionary<string, MB.Util.Emit.DynamicPropertyAccessor>();
            foreach (PropertyInfo info in infos) {
                object[] atts = info.GetCustomAttributes(typeof(DataMemberAttribute), true);
                if (atts == null || atts.Length == 0) continue;

                dAccs.Add(info.Name, new MB.Util.Emit.DynamicPropertyAccessor(typeObject, info));
            }
            return dAccs;
        }

        /// <summary>
        /// 把List的每一行都进行hash，用于将来比较该行的值是不是被修改了
        /// </summary>
        private Dictionary<string, T> saveOrignialDataSourceToHashTable(List<T> objs) {
            Dictionary<string, T> result = new Dictionary<string, T>();
            if (objs.Count > 0) {
                foreach (T obj in objs) {
                    string keyValue = generateKeyValueString(obj);

                    if (!result.ContainsKey(keyValue))
                        result.Add(keyValue, obj);
                    else
                        result[keyValue] = obj;
                }
            }
            return result;
        }

        /// <summary>
        /// 把客户端传入的List转换成DataSet
        /// </summary>
        /// <param name="dataSource">客户端传入的DataList</param>
        /// <returns>返回转换的DataSet</returns>
        private DataSet convertListToDataSet(List<T> dataSource) {
            DataSet ds = MB.Util.MyConvert.Instance.ConvertEntityToDataSet<T>(dataSource, null);
            if (ds.Tables.Count > 0) {
                foreach (DataColumn dc in ds.Tables[0].Columns) {
                    if (_Columns.ContainsKey(dc.ColumnName)) {
                        dc.Caption = _Columns[dc.ColumnName].Description;
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// 取出数据源中的主键的值,如果主键值为空，则抛出异常
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <returns>主键的值</returns>
        private string generateKeyValueString(object dataSource) {
            return generateKeyValueString(dataSource, true);
        }


        /// <summary>
        /// 取出数据源中的主键的值
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="raiseExceptionIfMainKeyIsNull">是否要抛出异常</param>
        /// <returns>主键值</returns>
        private string generateKeyValueString(object dataSource, bool raiseExceptionIfMainKeyIsNull) {
            string keyValue = string.Empty;
            if (dataSource is DataRow) {
                DataRow dr = dataSource as DataRow;
                if (dr != null) {
                    foreach (string mainKeyColumn in _MainKeys) {
                        bool raiseException = (dr[mainKeyColumn] == null || dr[mainKeyColumn] == System.DBNull.Value) 
                            && raiseExceptionIfMainKeyIsNull;
                        if (raiseException)
                            throw new MB.Util.APPException(string.Format("主键{0}的值不存在", mainKeyColumn));
                        
                        keyValue += dr[mainKeyColumn].ToString() + "|";
                        
                    }
                    keyValue = keyValue.TrimEnd('|');
                }
            }
            else if (dataSource is T) {
                T t = dataSource as T;
                if (t != null) {
                    foreach (string mainKeyColumn in _MainKeys) {
                        object tmpValue = _DAccs[mainKeyColumn].Get(t);
                        if (tmpValue == null && raiseExceptionIfMainKeyIsNull)
                            throw new MB.Util.APPException(string.Format("主键{0}的值不存在", mainKeyColumn));

                        keyValue += tmpValue.ToString() + "|";
                        
                    }
                    keyValue = keyValue.TrimEnd('|');
                }
            }
            else
                throw new MB.Util.APPException("该数据源类型不支持");

            return keyValue;
        }

        /// <summary>
        /// 在打开excel前，先要把其他excel进程杀尽
        /// </summary>
        private void killExcelProcess() {
            try {
                Process[] ps = Process.GetProcessesByName("EXCEL");
                if (ps != null && ps.Length > 0) {
                    for (int i = 0; i < ps.Length; i++) {
                        ps[i].Kill();
                    }
                }
            }
            catch { }
        }

        #region 对于lookupedit列的处理
        /// <summary>
        /// 通过键值查找LOOK UP EDIT中的值
        /// </summary>
        /// <param name="key">KEY是TEXT FIELD的值</param>
        /// <param name="fieldName">编辑列的名称</param>
        /// <returns></returns>
        private object findLookUpDataValueByKey(object key, string fieldName,
            Dictionary<string, Dictionary<string, object>> lookUpData) {
            if (key == null || key == System.DBNull.Value)
                return key;
            if (_EditColumns == null || _EditColumns.Count == 0)
                return key;

            if (!lookUpData.ContainsKey(fieldName)) return key;

            var temp = lookUpData[fieldName];
            if (temp.ContainsKey(key.ToString()))
                return temp[key.ToString()];
            else
                return key;
        }



        /// <summary>
        /// 把LookUpEdit查询出来的结果转换成HashTable便于查询,第一层的字典键是EditColumn的列名
        /// 第二层的字典键是：
        /// 如果把数据源变成EXCEL，需要把ID转换成TEXT则，VALUE是KEY，TEXT是值
        /// 如果是EXCEL变成数据源，需要把TEXT转换成ID，则TEXT是KEY, VALUE是值
        /// </summary>
        /// <param name="editColumns">编辑列的配置信息</param>
        /// <param name="textMemberIsKey">标志位，指定了在hashTable中到底什么是KEY，什么是值。</param>
        /// <returns></returns>
        private Dictionary<string, Dictionary<string, object>> convertToHasTable(Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> editColumns, bool textMemberIsKey) {
            Dictionary<string, Dictionary<string, object>> datas = new Dictionary<string, Dictionary<string, object>>();
            if (editColumns == null) return datas;
            foreach (MB.WinBase.Common.ColumnEditCfgInfo info in editColumns.Values) {
                if (info.DataSource == null) continue;
                System.Data.DataTable dtData = MB.Util.MyConvert.Instance.ToDataTable(info.DataSource, string.Empty);
                if (dtData == null) continue;

                DataRow[] drs = dtData.Select();
                if (drs.Length == 0) continue;
                if (!drs[0].Table.Columns.Contains(info.TextFieldName))
                    throw new MB.Util.APPException(string.Format("列 {0} 的下拉框配置中描述字段{1} 不属于对应的数据源", info.Name, info.TextFieldName));

                if (!drs[0].Table.Columns.Contains(info.ValueFieldName))
                    throw new MB.Util.APPException(string.Format("列 {0} 的下拉框配置中值字段{1} 不属于对应的数据源", info.Name, info.ValueFieldName));


                Dictionary<string, object> hasTable = new Dictionary<string, object>();
                foreach (DataRow dr in drs) {
                    if (textMemberIsKey) {
                        if (dr[info.TextFieldName] == System.DBNull.Value) continue;

                        string key = dr[info.TextFieldName].ToString();
                        if (hasTable.ContainsKey(key)) continue;

                        hasTable.Add(key, dr[info.ValueFieldName]);
                    }
                    else {
                        if (dr[info.ValueFieldName] == System.DBNull.Value) continue;

                        string key = dr[info.ValueFieldName].ToString();
                        if (hasTable.ContainsKey(key)) continue;

                        hasTable.Add(key, dr[info.TextFieldName]);
                    }
                }
                datas.Add(info.Name, hasTable);
            }
            return datas;
        }
        #endregion

        #region 计算哈希
        /// <summary>
        /// 把List的每一行都进行hash，用于将来比较该行的值是不是被修改了
        /// </summary>
        private void hashDataSource(List<T> objs) {
            if (objs.Count > 0) {
                foreach (T obj in objs) {
                    string keyValue = generateKeyValueString(obj);

                    if (!_Values.ContainsKey(keyValue))
                        _Values.Add(keyValue, obj);
                    else
                        _Values[keyValue] = obj;


                    string value = computeHash(obj);
                    if (!_HashedValues.ContainsKey(keyValue))
                        _HashedValues.Add(keyValue, value);
                    else
                        _HashedValues[keyValue] = value;


                }
            }
        }

        /// <summary>
        /// 计算每个对象的哈希值
        /// 用于比较对象是否被修改
        /// </summary>
        /// <param name="obj">需要计算哈希值的对象</param>
        /// <returns>计算完的哈希值</returns>
        private string computeHash(object obj) {
            if (obj == null)
                return string.Empty;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream()) {
                bf.Serialize(ms, obj);
                var md5 = MD5.Create();
                var bytes = md5.ComputeHash(ms.ToArray());
                string hashValue = BitConverter.ToString(bytes).Replace("-", "").ToLower();
                return hashValue;
            }

        }
        #endregion


        #endregion

        #region 在数据源通过excel打开前所做的一些准备工作
        /// <summary>
        /// 生成excel的临时文件
        /// </summary>
        /// <returns>临时文件的路径,包括文件名</returns>
        private string generateTempXlsFilePath() {

            string appDir = MB.Util.General.GeApplicationDirectory();
            string excelTempDir = appDir + @"ExcelAuto\";
            if (!System.IO.Directory.Exists(excelTempDir)) {
                Directory.CreateDirectory(excelTempDir);
            }
            string fileName = _ClientRule.GetType().FullName + "~" + _DocType.ToString() + ".xls";
            string filePath = excelTempDir + fileName;
            return filePath;
        }

        /// <summary>
        /// 处理要变成excel的数据源
        /// - 验证主键，把主键列排在列最前
        /// - 清除在配置中没有的列
        /// - 清除在配置中不显示的列
        /// - 把不可见与不能编辑的列记录在缓存中,并且把不可见的列从数据源中清除
        /// </summary>
        private void formatDataSourceToExcel(System.Data.DataTable dtData) {
            
            #region 验证主键是否存在数据源中存在

            string msgColNotExist = null;
            foreach (string mainKeyName in _MainKeys) {
                if (!dtData.Columns.Contains(mainKeyName)) {
                    msgColNotExist += mainKeyName;
                    msgColNotExist += ",";
                }
            }
            
            if (!string.IsNullOrEmpty(msgColNotExist)) {
                throw new MB.Util.APPException(string.Format("数据源中没有包括的主键列：{0}", msgColNotExist.TrimEnd(',')));
            }

            #endregion


            //首先处理那些在配置中不存在的列
            for (int i = 0; i < dtData.Columns.Count; i++) {
                if (!_Columns.ContainsKey(dtData.Columns[i].ColumnName)) {
                    dtData.Columns.Remove(dtData.Columns[i].ColumnName);
                    i--;
                }
            }

            //把不可见与不能编辑的列记录在缓存中,并且把不可见的列从数据源中清除
            foreach (var col in _Columns.Values) {
                if (_MainKeys.Contains(col.Name)) continue;

                if (!col.Visibled) {
                    dtData.Columns.Remove(col.Name);
                    _NonEditableColumns.Add(col.Name);
                }

                if (!col.CanEdit) {
                    _NonEditableColumns.Add(col.Name);
                }
            }
        }

        /// <summary>
        /// 把数据源中lookUpEdit的value值转换成text
        /// </summary>
        /// <param name="dtData">DataTable数据源</param>
        private void convertLookUpEditValueToText(System.Data.DataTable dtData) {
            foreach (var col in _Columns.Values) {
                //处理lookUpEdit的值，把value转换成text
                if (_LookUpDataValueKey.ContainsKey(col.Name)) {
                    Dictionary<object, object> tempLookUpTextWithEntityKey = new Dictionary<object, object>();
                    foreach (DataRow dr in _ConvertedDataSource.Tables[0].Rows) {
                        object lookUpText = findLookUpDataValueByKey(dr[col.Name], col.Name, _LookUpDataValueKey);
                        string mainKeyValue = generateKeyValueString(dr);
                        tempLookUpTextWithEntityKey.Add(mainKeyValue, lookUpText);
                    }

                    int index = dtData.Columns.IndexOf(col.Name);
                    dtData.Columns.Remove(col.Name);
                    dtData.Columns.Add(col.Name, typeof(object));
                    dtData.Columns[col.Name].Caption = col.Description;

                    foreach (DataRow dr in _ConvertedDataSource.Tables[0].Rows) {
                        string mainKeyValue = generateKeyValueString(dr);
                        dr[col.Name] = tempLookUpTextWithEntityKey[mainKeyValue];
                    }
                }
            }
        }

        /// <summary>
        /// 根据配置进行列排序
        /// </summary>
        /// <param name="dtData"></param>
        private void sortByColumnSetting(System.Data.DataTable dtData) {
            //根据配置进行排序
            int orderIndex = 0;
            foreach (var col in _Columns.Values) {
                if (dtData.Columns.Contains(col.Name))
                    dtData.Columns[col.Name].SetOrdinal(orderIndex++);
            }

            int mainKeyIndex = 0;
            foreach (string mainKeyName in _MainKeys) {
                dtData.Columns[mainKeyName].SetOrdinal(mainKeyIndex++);
            }
        }

        #endregion

        #region 在提交excel数据源时，所做的工作

        /// <summary>
        /// 得到要提交的excel数据的哈希
        /// 对于新的数据，直接放在新的集合对象中
        /// 其他数据，放在hash表中与编辑前的数据做对比 
        /// </summary>
        private void getSubmittedDataFromExcel() {
            DataSet dsData = MB.WinBase.LayoutXmlConfigHelper.Instance.CreateNULLDataByFieldPropertys(_Columns);
            if (dsData.Tables != null && dsData.Tables.Count > 0) {
                //如果数据源结构中没有主键，则把主键添加进去
                int keyColumnIndex = 0;
                foreach (string mainKeyColumn in _MainKeys) {
                    if (!dsData.Tables[0].Columns.Contains(mainKeyColumn)) {
                        var colInfo = _Columns[mainKeyColumn];
                        DataColumn newCol = new DataColumn(mainKeyColumn, MB.Util.General.CreateSystemType(colInfo.DataType, false));
                        newCol.Caption = colInfo.Description;
                        dsData.Tables[0].Columns.Add(newCol);
                        dsData.Tables[0].Columns[COL_DEFAULT_KEY].SetOrdinal(keyColumnIndex++);
                    }
                }

            }
            ExcelEditHelper excelEditHelper = new ExcelEditHelper(_ExcelFilePath);
            excelEditHelper.DataProgress += new WinEIDrive.Import.ImportProgressEventHandler(xlsImport_DataProgress);
            excelEditHelper.CommitExcel(dsData);
            //MB.WinEIDrive.Import.XlsImport xlsImport = new MB.WinEIDrive.Import.XlsImport(dsData, _ExcelFilePath);
            //xlsImport.DataProgress += new WinEIDrive.Import.ImportProgressEventHandler(xlsImport_DataProgress);
            //xlsImport.Commit();
            MB.Util.DataValidated.Instance.RemoveNULLRowData(dsData);            //移除空行数据

            bool hasData = dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0;
            if (hasData) {
                if (_EditColumns != null && _EditColumns.Count > 0) {
                    Dictionary<string, List<object>> clickButtonInputColsWithTexts = generateClickButtonWithTextDic(dsData);
                    Dictionary<string, Dictionary<object, DataRow>> clickButtonInputColsWithValues = covertClickButtonInputTextToValue(clickButtonInputColsWithTexts);
                    fillClickInputButtonValueBackTo(dsData, clickButtonInputColsWithValues);
                }

                //对数据源进行分类，对有主键的记录进行哈希
                List<T> sumitted = new List<T>();
                MB.Util.MyConvert.ConvertDataSetToEntity<T>(dsData, sumitted, null);

                foreach (T obj in sumitted) {
                    string mainKeyValue = generateKeyValueString(obj);
                    if (string.IsNullOrEmpty(mainKeyValue) || !_Values.Keys.Contains(mainKeyValue)) {
                        _SubmittedNewValue.Add(obj);
                    }
                    else {
                        if (!_SubmittedValue.ContainsKey(mainKeyValue))
                            _SubmittedValue.Add(mainKeyValue, obj);
                        else
                            _SubmittedValue[mainKeyValue] = obj;
                        string value = computeHash(obj);
                        if (!_SubmittedHashedValues.ContainsKey(mainKeyValue))
                            _SubmittedHashedValues.Add(mainKeyValue, value);
                        else
                            _SubmittedHashedValues[mainKeyValue] = value;
                    }
                }
            }

        }

        /// <summary>
        /// 在excel导入时，对每个Cell做处理的事件订阅
        /// </summary>
        /// <param name="sender">xlsImport</param>
        /// <param name="e"></param>
        private void xlsImport_DataProgress(object sender, WinEIDrive.Import.ImportProgressEventArgs e) {
            if (string.Compare(e.FieldName, COL_DEFAULT_KEY, true) == 0) {
                return;
            }
            e.NewValue = findLookUpDataValueByKey(e.DataValue, e.FieldName, _LookUpDataTextKey);
        }

        /// <summary>
        /// 根据配置，从数据源中得到所有ClickButton的列，并把这些列的text值与列名做成字典，方便后面转换
        /// </summary>
        /// <param name="dsData">从excel中获得的数据源</param>
        /// <returns>字典，键是ClickButtonInput的名字，值是所有text的集合</returns>
        private Dictionary<string, List<object>> generateClickButtonWithTextDic(DataSet dsData) {
            
            //对ClickButtonInput的导入数据进行赋值，因为在excel中，导入的是这个字段的Text Value
            IEnumerable<ColumnEditCfgInfo> clickInputButtonCols = _EditColumns.Values.Where(editCol => {
                MB.WinBase.Common.EditControlType controlType =
                    (MB.WinBase.Common.EditControlType)Enum.Parse(typeof(MB.WinBase.Common.EditControlType), editCol.EditControlType);
                return (controlType == EditControlType.ClickButtonInput);
            });

            Dictionary<string, List<object>> clickInputButtonColsWithTexts = new Dictionary<string, List<object>>();
            foreach (DataRow dr in dsData.Tables[0].Rows) {
                foreach (var clickButtonInputCol in clickInputButtonCols) {

                    if (!dsData.Tables[0].Columns.Contains(clickButtonInputCol.Name)) continue;

                    if (clickInputButtonColsWithTexts.ContainsKey(clickButtonInputCol.Name)) {
                        if (dr[clickButtonInputCol.Name] != null &&
                            dr[clickButtonInputCol.Name] != System.DBNull.Value &&
                            !clickInputButtonColsWithTexts[clickButtonInputCol.Name].Contains(dr[clickButtonInputCol.Name]))
                            clickInputButtonColsWithTexts[clickButtonInputCol.Name].Add(dr[clickButtonInputCol.Name]);
                    }
                    else {
                        List<object> textValues = new List<object>();
                        textValues.Add(dr[clickButtonInputCol.Name]);
                        clickInputButtonColsWithTexts.Add(clickButtonInputCol.Name, textValues);
                    }
                }
            }
            return clickInputButtonColsWithTexts;
        }

        /// <summary>
        /// 从注册GetClickInputButtonSourceAfterSubmit事件中获取的值
        /// </summary>
        /// <param name="clickInputButtonColsWithTexts">整理过的clickButtonText字典,键值列名，值是所有text集合</param>
        /// <returns>根据texts返回的value</returns>
        private Dictionary<string, Dictionary<object, DataRow>> covertClickButtonInputTextToValue(Dictionary<string, List<object>> clickInputButtonColsWithTexts) {
            //通过客户端注册是事件，拿到clickButtonInput的Source,保存结果到内存，以便等下一次处理
            Dictionary<string, Dictionary<object, DataRow>> clickInputButtonColsWithValues = new Dictionary<string, Dictionary<object, DataRow>>();
            foreach (var oneColTexts in clickInputButtonColsWithTexts) {
                GetClickInputButtonSourceAfterSubmitEventArgs args = new GetClickInputButtonSourceAfterSubmitEventArgs(oneColTexts.Key, oneColTexts.Value.ToArray());
                DataSet dsValues = onGetClickInputButtonSourceAfterSubmit(this, args);

                bool hasValues = dsValues != null && dsValues.Tables.Count > 0 && dsValues.Tables[0].Rows.Count > 0;
                if (hasValues) {
                    ColumnEditCfgInfo editColumn = _EditColumns[oneColTexts.Key];
                    Dictionary<object, DataRow> values = new Dictionary<object, DataRow>();
                    foreach (DataRow dr in dsValues.Tables[0].Rows) {
                        object valueKey = dr[editColumn.TextFieldName];
                        if (!values.ContainsKey(valueKey))
                            values.Add(valueKey, dr);
                    }
                    clickInputButtonColsWithValues.Add(oneColTexts.Key, values);
                }
            }
            return clickInputButtonColsWithValues;
        }

        /// <summary>
        /// 把从注册事件中获取的值，回填到excel导入的数据源中
        /// </summary>
        /// <param name="dsData"></param>
        /// <param name="clickButtonInputColsWithValues"></param>
        private void fillClickInputButtonValueBackTo(DataSet dsData, Dictionary<string, Dictionary<object, DataRow>> clickButtonInputColsWithValues) {
            foreach (DataRow dr in dsData.Tables[0].Rows) {
                foreach (var oneColValue in clickButtonInputColsWithValues) {
                    ColumnEditCfgInfo editColumn = _EditColumns[oneColValue.Key];
                    object valueInExcel = dr[oneColValue.Key];
                    if (oneColValue.Value.ContainsKey(valueInExcel)) {
                        foreach (MB.WinBase.Common.EditCtlDataMappingInfo info in editColumn.EditCtlDataMappings) {
                            if (info.SourceColumnName.CompareTo(editColumn.TextFieldName) == 0) continue;
                            dr[info.ColumnName] = oneColValue.Value[valueInExcel][info.SourceColumnName];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 把那些不能编辑的列
        /// </summary>
        /// <param name="orignalDataSource">原始数据源</param>
        /// <param name="submittedDataSource">EXCEL编辑以后提交的数据源</param>
        private void copyNonEditPropToSubmittedEntity(T orignalDataSource, T submittedDataSource) {
            foreach (var da in _DAccs) {
                if (_NonEditableColumns.Contains(da.Key)) {
                    object value = da.Value.Get(orignalDataSource);
                    if (value != null)
                        da.Value.Set(submittedDataSource, value);
                }
                else continue;
            }
        }

        #endregion

        #endregion


        #region IDisposable Members
        private bool disposed = false;

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    //托管代码
                }

                System.Threading.Thread.Sleep(500);
                disposed = true;
            }
        }

        ~WinformExcelEditor() {
            Dispose(false);
        }

        #endregion
    }


    /// <summary>
    /// 提交以后，根据ClickInputButton的TextValue得到其他值的事件参数
    /// </summary>
    public class GetClickInputButtonSourceAfterSubmitEventArgs : System.EventArgs {

        private string _ColName;
        private object[] _TextValues;

        /// <summary>
        ///构造函数,
        /// </summary>
        /// <param name="colName">ClickInputButton的列名</param>
        /// <param name="textValues">该列所有text的值的集合</param>
        public GetClickInputButtonSourceAfterSubmitEventArgs(string colName, object[] textValues) {
        }
        /// <summary>
        /// ClickInputButton的列名
        /// </summary>
        public string ColName {
            get {
                return _ColName;
            }
            set {
                _ColName = value;
            }
        }

        /// <summary>
        /// ClickInputButton配置的TextFieldName的值集合
        /// </summary>
        public object[] TextValues {
            get {
                return _TextValues;
            }
            set {
                _TextValues = value;
            }
        }
    }
}
