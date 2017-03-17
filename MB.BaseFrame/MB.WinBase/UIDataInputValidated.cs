//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// Author		:	chendc
// Create date	:	2009-03-16
// Description	:	UIDataInputValidated: 数据输入验证处理相关...
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms; 

using MB.BaseFrame;
using MB.WinBase.Common;
namespace MB.WinBase {
    /// <summary>
    /// 数据输入验证处理相关...
    /// </summary>
    public class UIDataInputValidated {
        #region 变量定义...
        private static readonly string MSG_CHECKED_LENGTH = "{0}:输入太长;请输入<{1}个英文字母或者<{2}中文汉字.";
        private static readonly string MSG_CHECKED_FIT = "{0}:输入的字符长度只能等于{1}个字符!";
        private static readonly string MSG_MUST_INPUT = "{0}需要输入{1}";
        private static readonly string MSG_NOTNULL = "{0}:不能为空";
        private static readonly string MSG_MIN_MAX_VALUE = "{0} 的输入必须在{1} 和 {2} 之间。";
        private static readonly string MSG_MAX_PLACES = "{0}:输入的最大精度为{1} 位,请重新输入";
        //private static readonly string MSG_SAVE_KEY_CHECKED = "{0}的值:{1}在表中已经存在，请重新输入!";

        #endregion 变量定义...

        private System.Windows.Forms.ErrorProvider _ErrorProvider;
        private static readonly string[] NUMBER_DATA_TYPE = new string[] { "INT16", "INT32", "DECIMAL", "FLOAT", "DOUBLE", "INT64", "INT" };
        private static readonly string[] DATETIME_DATA_TYPE = new string[] { "DATETIME" };

        #region 构造函授数...
        public UIDataInputValidated() {
        }
        /// <summary>
        /// 构造函授数...
        /// </summary>
        public UIDataInputValidated(System.Windows.Forms.ContainerControl containerControl) {
            _ErrorProvider = new System.Windows.Forms.ErrorProvider();
            _ErrorProvider.ContainerControl = containerControl;
            // _ErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

        }
        #endregion 构造函授数...
        /// <summary>
        /// 默认 主要针对明细数据的检验
        /// </summary>
        public static UIDataInputValidated DefaultInstance {
            get {
                return MB.Util.SingletonProvider<UIDataInputValidated>.Instance; 
            }
        }

        /// <summary>
        /// 清空显示的错误提示信息。
        /// </summary>
        /// <param name="editColumnCtls"></param>
        public void ClearErrorMessage(List<MB.WinBase.Binding.ColumnBindingInfo> editColumnCtls) {
            if (editColumnCtls == null || editColumnCtls.Count == 0) return;

            foreach (MB.WinBase.Binding.ColumnBindingInfo colCfg in editColumnCtls) {
                _ErrorProvider.SetError(colCfg.BindingControl, string.Empty);
            }
        }

        #region public DataValidated...
        /// <summary>
        /// 根据控件的输入值进行判断。
        /// </summary>
        /// <param name="editColumnCtls"></param>
        /// <returns></returns>
        public bool Validated(List<MB.WinBase.Binding.ColumnBindingInfo> editColumnCtls) {
            if (editColumnCtls == null || editColumnCtls.Count == 0) return true;
            bool re = false;
            return re;
        }

        /// <summary>
        /// 针对实体类的数据验证。
        /// </summary>
        /// <param name="clientRule"></param>
        /// <param name="editColumnCtls"></param>
        /// <param name="dataEntity"></param>
        /// <returns></returns>
        public bool DataValidated(MB.WinBase.IFace.IClientRule clientRule,List<MB.WinBase.Binding.ColumnBindingInfo> editColumnCtls, object dataEntity) {
            if (editColumnCtls == null || editColumnCtls.Count == 0) return true;
            bool re = true;
            Control firstErrorCtl = null;
            foreach (MB.WinBase.Binding.ColumnBindingInfo colCfg in editColumnCtls) {
                _ErrorProvider.SetError(colCfg.BindingControl, string.Empty);

                if (dataEntity == null) {
                    continue;
                }
                bool exists = MB.Util.MyReflection.Instance.CheckObjectExistsProperty(dataEntity, colCfg.ColumnName);
                if (!exists) {
                   // _ErrorProvider.SetError(colCfg.BindingControl, string.Empty);
                    if (firstErrorCtl == null)
                        firstErrorCtl = colCfg.BindingControl;
                    continue;
                }

                object val = MB.Util.MyReflection.Instance.InvokePropertyForGet(dataEntity, colCfg.ColumnName);
                string errMsg = string.Empty;
                bool check = validated(colCfg.ColumnPropertyInfo, val, ref errMsg);
                if (!check || errMsg.Length > 0)
                    re = false;

                if (!string.IsNullOrEmpty(errMsg)) {
                    _ErrorProvider.SetError(colCfg.BindingControl, errMsg);
                    if (firstErrorCtl == null)
                        firstErrorCtl = colCfg.BindingControl;
                }
            }

            if (firstErrorCtl != null) {
                firstErrorCtl.Focus();
                MB.WinBase.ShareLib.Instance.ShowFocusControl(firstErrorCtl); 
            }
            if (re) {
                //在数据检验通过后进行逻辑主键的判断
                string[] logicKeys = MB.WinBase.LayoutXmlConfigHelper.Instance.GetLogicKeys(clientRule.ClientLayoutAttribute.UIXmlConfigFile);
                if (logicKeys != null && logicKeys.Length > 0 && clientRule.MainDataTypeInDoc!=null) {
                    clientRule.CheckValueIsExists((int)clientRule.MainDataTypeInDoc, dataEntity, logicKeys);
                }
            }
            return re;
        }

        /// <summary>
        /// 检查DataRow 的数据是否满足XML 配置信息中的需求。
        /// </summary>
        /// <param name="colPropertys"></param>
        /// <param name="drData"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool DataRowValidated(Dictionary<string,MB.WinBase.Common.ColumnPropertyInfo> colPropertys,DataRow drData,ref string errMsg) {
            if (drData == null) return true;
            DataColumnCollection dcs = drData.Table.Columns;
            foreach (DataColumn dc in dcs) {
                if (!colPropertys.ContainsKey(dc.ColumnName)) continue;

                object val = drData[dc.ColumnName];
                bool check = validated(colPropertys[dc.ColumnName], val, ref errMsg);
                if (!check || errMsg.Length > 0)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 根据XML 配置的单个列进行验证
        /// </summary>
        /// <param name="colPropertyInfo"></param>
        /// <param name="inputValue"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool SingleDataValidated(MB.WinBase.Common.ColumnPropertyInfo colPropertyInfo, object inputValue, ref string errMsg) {
            return validated(colPropertyInfo, inputValue, ref errMsg);
        }
        #endregion public DataValidated...

        #region 明细编辑处理相关...

        /// <summary>
        /// 根据配置XML文件验证输入的明细是否合法。
        /// 主要针对网格数据的检验。
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="detailData"></param>
        /// <returns></returns>
        public bool DetailGridDataValidated(string xmlFileName, object detailData, string detailDescription) {
            if (detailData == null) return true;
            Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> fieldPropertys = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnPropertys(xmlFileName);
            Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> colEditPropertyInfos = MB.WinBase.LayoutXmlConfigHelper.Instance.GetColumnEdits(fieldPropertys, xmlFileName);
            DataTable dtData = MB.Util.MyConvert.Instance.ToDataTable(detailData, string.Empty);
            if (dtData != null) {
                 return validatedDataTable(dtData, fieldPropertys, colEditPropertyInfos, detailDescription);   
            }
            IList lstData = detailData as IList;
            if (lstData == null) {
                throw new MB.Util.APPException("明细数据检验只对集合类 和能转换为DataTable 的数据进行检验");
            }
            if (lstData.Count == 0) return true;

            //先删除没有任何数据的空行。 (同时如果所有显示的列的值都为空，那么就判断该行为空)
            deleteNULLDataRow(lstData,fieldPropertys);

            foreach (MB.WinBase.Common.ColumnPropertyInfo colInfo in fieldPropertys.Values) {
                if (string.Compare(colInfo.Name, "ID", true) == 0 || string.Compare(colInfo.Name, "UID", true) == 0)
                    continue;

                if (!colInfo.Visibled || !colInfo.CanEdit) continue;

                foreach (object entity in lstData) {
                    string errMsg = string.Empty;
                    if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, colInfo.Name))
                        continue;

                    object valData = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, colInfo.Name);
                    bool b = validated(colInfo, valData, ref errMsg);
                    if (!b) {
                       // MB.WinBase.MessageBoxEx.Show(string.Format("对象：{0}编辑中 {1}!", detailDescription, errMsg));
                        throw new MB.Util.APPException(string.Format("对象：{0}编辑中 {1}!", detailDescription, errMsg), Util.APPMessageType.DisplayToUser);
                       // return false;
                    }
                }
                if (colInfo.IsKey) {
                    bool checkKey = checkDetailLogicKey(lstData, colInfo.Name, detailDescription);
                    if (!checkKey) {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion 明细编辑处理相关...

        #region 内部函数处理相关...
        private bool validatedDataTable(DataTable dtData, Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> fieldPropertys, Dictionary<string, MB.WinBase.Common.ColumnEditCfgInfo> colEditPropertyInfos, string detailDescription) {
            DataRow[] drs = dtData.Select();
            DataColumnCollection cols = dtData.Columns;
            string errMsg = string.Empty;
            foreach (DataRow dr in drs) {
                foreach(DataColumn dc in cols){
                    string colName = dc.ColumnName;
                    if (!fieldPropertys.ContainsKey(colName)) continue;
                    if (string.Compare(colName, "ID", true) == 0 || string.Compare(colName, "UID", true) == 0)
                        continue;
                    var fieldInfo = fieldPropertys[colName];
                    if (!fieldInfo.Visibled || !fieldInfo.CanEdit) continue;
                    ColumnEditCfgInfo colEditProInfo = null;
                    if (colEditPropertyInfos != null && colEditPropertyInfos.ContainsKey(colName))
                      colEditProInfo = colEditPropertyInfos[colName];

                    object valData = dr[colName];
                    bool b = validated(fieldInfo, colEditProInfo, valData, ref errMsg);
                    if (!b) {
                        MB.WinBase.MessageBoxEx.Show(string.Format("对象：{0}编辑中 {1}!", detailDescription, errMsg));
                        return false;
                    }
                    if (fieldInfo.IsKey) {
                        bool checkKey = checkDetailLogicKeyOnDataTable(drs, colName, detailDescription);
                        if (!checkKey) {
                            return false;
                        }
                    }

                    
                }
            }
            return true;
        }
        //检验指定的列是否存在重复的值。
        private bool checkDetailLogicKey(IList lstData, string fieldName, string detailDescription) {
            //对象的键值都转换为string 来进行比较
            string dataValue = string.Empty;
            System.Collections.Generic.HashSet<string> existsVal = new HashSet<string>();
            foreach (object  entity in lstData) {
                object  tempVal = MB.Util.MyReflection.Instance.InvokePropertyForGet(entity, fieldName);
                if (tempVal == null) continue;

                if(existsVal.Contains(tempVal.ToString())){
               // if (string.Compare(tempVal.ToString(), dataValue) == 0) {
                    throw new MB.Util.APPException(string.Format("{0} 明细数据编辑中,存在重复的数据[{1}]。", detailDescription, tempVal), Util.APPMessageType.DisplayToUser);
                   // return false;
                }
                existsVal.Add(tempVal.ToString());
            }
            return true;
        }
        private bool checkDetailLogicKeyOnDataTable(DataRow[] drs, string fieldName, string detailDescription) {
            //对象的键值都转换为string 来进行比较
            System.Collections.Generic.HashSet<string> existsVal = new HashSet<string>();
            foreach (DataRow dr in drs) {
                object tempVal = dr[fieldName];
                if (tempVal == null) continue;
                if(existsVal.Contains(tempVal.ToString())){
               // if (string.Compare(tempVal.ToString(), dataValue) == 0) {
                    throw new MB.Util.APPException(string.Format("{0} 明细数据编辑中,存在重复的数据[{1}]。", detailDescription, tempVal), Util.APPMessageType.DisplayToUser);
                    return false;
                }
                existsVal.Add(tempVal.ToString());
            }
            return true;
        }

        //删除明细中的空行
        private void deleteNULLDataRow(IList lstData, Dictionary<string, MB.WinBase.Common.ColumnPropertyInfo> fieldPropertys) {
            ArrayList deleteds = new ArrayList();
            foreach (object entity in lstData) {
                bool isNULL = true;
                foreach (MB.WinBase.Common.ColumnPropertyInfo colInfo in fieldPropertys.Values) {
                    if (!MB.Util.MyReflection.Instance.CheckObjectExistsProperty(entity, colInfo.Name))
                        continue;
                    if (!colInfo.Visibled) continue;

                    if (!MB.Util.MyReflection.Instance.CheckPropertyValueIsNull(entity, colInfo.Name)) {
                        isNULL = false;
                        break;
                    }
                }
                if (isNULL)
                    deleteds.Add(entity);
            }
            if (deleteds.Count > 0) {
                foreach (object delEntity in deleteds)
                    lstData.Remove(delEntity);  
            }

        }

        //为了兼容以前的验证规则
        private bool validated(MB.WinBase.Common.ColumnPropertyInfo colPropertyInfo, object inputValue, ref string errMsg) {
            return validated(colPropertyInfo, null, inputValue, ref errMsg);
        }
        //根据指定的ColumnPropertyInfo 判断输入的值是否符合要求。
        private bool validated(MB.WinBase.Common.ColumnPropertyInfo colPropertyInfo, MB.WinBase.Common.ColumnEditCfgInfo colEditProInfo, object inputValue, ref string errMsg) {
            bool b;
            StringBuilder msgStr = new StringBuilder();
            //得到要检验的这个列
            //如果是字符窜类型需要检验长度
            //如果值为空不需要检验数据类型
            if (colPropertyInfo.IsNull == true && (inputValue == null || inputValue.ToString().Length == 0)) { return true; }
            if (colPropertyInfo.IsNull != true && (inputValue == null || inputValue.ToString().Length == 0)) {
                string nameDesc = (colPropertyInfo.Description != null && colPropertyInfo.Description.Length > 0) ? colPropertyInfo.Description : colPropertyInfo.Name;
                msgStr.Append(nameDesc + ":不能为空");
                //如果检验不通过输出错误的信息
                errMsg = CLL.Message(MSG_NOTNULL, new string[] { nameDesc });
                return false;
            }
            Type sType = MB.Util.General.CreateSystemType(colPropertyInfo.DataType,false);
            if (sType == null)
                throw new MB.Util.APPException(string.Format("XML 文件中列{0} 的类型配置信息有误,请检查XML 文件配置", colPropertyInfo.Description));

            b = MB.Util.DataValidated.Instance.ValidatedDataType(sType, inputValue);

            if (b == true) {
                if (string.Compare(sType.Name, "String", true) == 0) {
                    b = validatedString(colPropertyInfo, inputValue, ref errMsg);
                }
                else if (Array.IndexOf(NUMBER_DATA_TYPE, sType.Name.ToUpper()) >= 0) {
                    b = validatedNumber(colPropertyInfo, inputValue, ref errMsg);
                }
                else if (Array.IndexOf(DATETIME_DATA_TYPE, sType.Name.ToUpper()) >= 0) {
                    b = validatedDateTime(colPropertyInfo, inputValue, ref errMsg);
                }
                else {
                    //其它类型先不做处理  后期根据实际需要再加上
                }
            }
            else {
                errMsg = CLL.Message(MSG_MUST_INPUT, new string[] { colPropertyInfo.Description, MB.Util.DataValidated.Instance.GetTypeValidatedErrMsg(sType) });
                return false;
            }

            if (colEditProInfo != null && colEditProInfo.IsValidateInputFromDataSource) {
                b = validateColValueInLookUpSource(colPropertyInfo, colEditProInfo, inputValue, ref errMsg);
            }

            return b;
        }
        //验证字符窜处理相关...
        private bool validatedString(MB.WinBase.Common.ColumnPropertyInfo colPropertyInfo, object inputValue, ref string errMsg) {
            bool b = true;
            if (colPropertyInfo.MaxLength > 0) {
                b = MB.Util.DataValidated.Instance.ValidatedLength(colPropertyInfo.MaxLength, inputValue.ToString());
                if (b == false) {
                    //如果检验不通过输出错误的信息
                    int leng = colPropertyInfo.MaxLength / 2;
                    errMsg = CLL.Message(MSG_CHECKED_LENGTH, new string[]{colPropertyInfo.Description,
																			  colPropertyInfo.MaxLength.ToString(),leng.ToString()});
                    return false;
                }
            }
            if (colPropertyInfo.FitLength > 0) {
                b = MB.Util.DataValidated.Instance.ValidatedFitLength(colPropertyInfo.FitLength, inputValue.ToString());
                if (!b) {
                    errMsg = CLL.Message(MSG_CHECKED_FIT, new string[] { colPropertyInfo.Description, colPropertyInfo.FitLength.ToString() });
                }
            }
            return b;
        }
        //验证数字
        private bool validatedNumber(MB.WinBase.Common.ColumnPropertyInfo colPropertyInfo, object inputValue, ref string errMsg) {
            double val = MB.Util.MyConvert.Instance.ToDouble(inputValue);
            //判断最大最小值
            if (val < colPropertyInfo.MinValue || val > colPropertyInfo.MaxValue) {
                errMsg = CLL.Message(MSG_MIN_MAX_VALUE, colPropertyInfo.Description, colPropertyInfo.MinValue.ToString(), colPropertyInfo.MaxValue.ToString());
                return false;
            }
            //判断小数点位数
            if (colPropertyInfo.MaxDecimalPlaces >= 0) {
                string sv = val.ToString();
                if (sv.IndexOf('.') >= 0) {
                    int position = sv.Length - sv.LastIndexOf('.') - 1;
                    if (position > colPropertyInfo.MaxDecimalPlaces) {
                        errMsg = CLL.Message(MSG_MAX_PLACES, colPropertyInfo.Description, colPropertyInfo.MaxDecimalPlaces.ToString());
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 验证日期格式的字段
        /// </summary>
        /// <param name="colPropertyInfo">对该字段列的客户端配置属性</param>
        /// <param name="inputValue">该列的值</param>
        /// <param name="errMsg">错误信息，ref参数，在方法中定义错误信息并传出</param>
        /// <returns>验证字段值是否正确，true表示验证正确，false表示验证错误</returns>
        private bool validatedDateTime(MB.WinBase.Common.ColumnPropertyInfo colPropertyInfo, object inputValue, ref string errMsg) {
            DateTime val = Convert.ToDateTime(inputValue);
            //判断是不是DateTime初始值,如果是初始值，则认为是空
            if (!colPropertyInfo.IsNull && val == DateTime.MinValue) {
                string nameDesc = (colPropertyInfo.Description != null && colPropertyInfo.Description.Length > 0) ? colPropertyInfo.Description : colPropertyInfo.Name;
                errMsg = CLL.Message(MSG_NOTNULL, nameDesc);
                return false;
            }
            return true;
        }
        //检查edit column中的值，是否与LookUp类似的控件中的data source中的，TextField或者ValueField相互匹配
        private bool validateColValueInLookUpSource(ColumnPropertyInfo colPropertyInfo, ColumnEditCfgInfo colEditProInfo, object value, ref string errMsg) {
            if (colEditProInfo == null || colEditProInfo.DataSource == null)
                return true;

            if (!colEditProInfo.IsValidateInputFromDataSource)
                return true;

            DataTable dtData = MB.Util.MyConvert.Instance.ToDataTable(colEditProInfo.DataSource, string.Empty);
            //如果返回的数据源不是data table则直接返回不做验证
            if (dtData == null || dtData.Rows.Count <= 0)
                return true;

            EditControlType controlType = (EditControlType)Enum.Parse(typeof(EditControlType), colEditProInfo.EditControlType);
            if (controlType == EditControlType.ComboCheckedListBox ||
                    controlType == EditControlType.LookUpEdit) {
                if (value == null) {
                    errMsg = string.Format("XML 文件中列 {0} 的值不能为空,请检查导入的值", colPropertyInfo.Description);
                }
                DataTable newData = dtData.Copy();
                foreach (DataRow dr in newData.Rows) {
                    bool isEqualToTextField = dr[colEditProInfo.TextFieldName] == null || (value.ToString() == dr[colEditProInfo.TextFieldName].ToString());
                    bool isEqualToValueField = dr[colEditProInfo.ValueFieldName] == null || (value.ToString() == dr[colEditProInfo.ValueFieldName].ToString());
                    if (isEqualToTextField || isEqualToValueField)
                        return true;
                }
                errMsg = string.Format("XML 文件中列 {0} 的值不能与下拉框中的数据源相匹配,请修改当前导入的值 {1}", colPropertyInfo.Description, value);
                return false;

            }
            return true;
            

        }

        #endregion 内部函数处理相关...

    }
}
