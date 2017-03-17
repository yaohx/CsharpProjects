using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util.Validated
{
    /// <summary>
    /// 数据验证处理相关.
    /// </summary>
    public class ValidatedHelper
    {
        
        #region 变量定义...
        private static readonly string MSG_CHECKED_LENGTH = "{0}:输入太长;请输入<{1}个英文字母或者<{2}中文汉字.";
        private static readonly string MSG_CHECKED_FIT = "{0}:输入的字符长度只能等于{1}个字符!";
        private static readonly string MSG_MUST_INPUT = "{0}需要输入{1}";
        private static readonly string MSG_NOTNULL = "{0}:不能为空";
        private static readonly string MSG_MIN_MAX_VALUE = "{0} 的输入必须在{1} 和 {2} 之间。";
        private static readonly string MSG_MAX_PLACES = "{0}:输入的最大精度为{1} 位,请重新输入";

        private static readonly string[] NUMBER_DATA_TYPE = new string[] { "INT16", "INT32", "DECIMAL", "FLOAT", "DOUBLE", "INT64", "INT" };
        #endregion

        /// <summary>
        /// 实体对象验证。
        /// </summary>
        /// <returns></returns>
        public static bool EntityDataValidated<T>(PropertyValidates<T> validatesContainer, T entity) {
            if (validatesContainer.QuickDataAccess == null)
                throw new MB.Util.APPException("创建PropertyValidates<T> 时需要启动快速访问", Util.APPMessageType.SysErrInfo);

            foreach (var key in validatesContainer.Keys) {
                var validateInfo = validatesContainer[key];
                var ac = validatesContainer.QuickDataAccess[key];
                object val = ac.Get(entity);

                DataValidated(validateInfo, val);
            }
            return true;
        }
        /// <summary>
        /// 根据指定的ColumnPropertyInfo 判断输入的值是否符合要求。
        /// </summary>
        /// <param name="colPropertyInfo"></param>
        /// <param name="inputValue"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool DataValidated(PropertyValidatedInfo colPropertyInfo, object inputValue) {
            bool b;
            //得到要检验的这个列
            //如果是字符窜类型需要检验长度
            //如果值为空不需要检验数据类型
            if (colPropertyInfo.IsNull == true && (inputValue == null || inputValue.ToString().Length == 0)) { return true; }
            if (colPropertyInfo.IsNull != true && (inputValue == null || inputValue.ToString().Length == 0)) {
                string nameDesc = (colPropertyInfo.Description != null && colPropertyInfo.Description.Length > 0) ? colPropertyInfo.Description : colPropertyInfo.Name;
                throw new MB.Util.APPException(string.Format(MSG_NOTNULL,nameDesc), APPMessageType.DataInvalid);

            }
            Type sType = colPropertyInfo.DataType;

            b = MB.Util.DataValidated.Instance.ValidatedDataType(sType, inputValue);

            if (b == true) {
                if (string.Compare(sType.Name, "String", true) == 0) {
                    b = validatedString(colPropertyInfo, inputValue);
                } else if (Array.IndexOf(NUMBER_DATA_TYPE, sType.Name.ToUpper()) >= 0) {
                    b = validatedNumber(colPropertyInfo, inputValue);
                } else {
                    //其它类型先不做处理  后期根据实际需要再加上
                }
            } else {
                throw new MB.Util.APPException(Message(MSG_MUST_INPUT, new string[] { colPropertyInfo.Description,
                    MB.Util.DataValidated.Instance.GetTypeValidatedErrMsg(sType) }), APPMessageType.DataInvalid);

            }
            return b;
        }

        #region 内部函数处理...

        //验证字符窜处理相关...
        private static bool validatedString(PropertyValidatedInfo colPropertyInfo, object inputValue) {
            bool b = true;
            if (colPropertyInfo.MaxLength > 0) {
                b = MB.Util.DataValidated.Instance.ValidatedLength(colPropertyInfo.MaxLength, inputValue.ToString());
                if (b == false) {
                    //如果检验不通过输出错误的信息
                    int leng = colPropertyInfo.MaxLength / 2;
                    var errMsg = Message(MSG_CHECKED_LENGTH, new string[]{colPropertyInfo.Description,
																			  colPropertyInfo.MaxLength.ToString(),leng.ToString()});

                    throw new MB.Util.APPException(errMsg, APPMessageType.DataInvalid);
                }
            }
            if (colPropertyInfo.FitLength > 0) {
                b = MB.Util.DataValidated.Instance.ValidatedFitLength(colPropertyInfo.FitLength, inputValue.ToString());
                if (!b) {
                    var errMsg = Message(MSG_CHECKED_FIT, new string[] { colPropertyInfo.Description, colPropertyInfo.FitLength.ToString() });
                    throw new MB.Util.APPException(errMsg, APPMessageType.DataInvalid);
                }
            }
            return b;
        }
        //验证数字
        private static bool validatedNumber(PropertyValidatedInfo colPropertyInfo, object inputValue) {
            double val = MB.Util.MyConvert.Instance.ToDouble(inputValue);
            //判断最大最小值
            if (val < colPropertyInfo.MinValue || val > colPropertyInfo.MaxValue) {
                var errMsg = Message(MSG_MIN_MAX_VALUE, colPropertyInfo.Description, colPropertyInfo.MinValue.ToString(), colPropertyInfo.MaxValue.ToString());
                throw new MB.Util.APPException(errMsg, APPMessageType.DataInvalid);
            }
            //判断小数点位数
            if (colPropertyInfo.MaxDecimalPlaces >= 0) {
                string sv = val.ToString();
                if (sv.IndexOf('.') >= 0) {
                    int position = sv.Length - sv.LastIndexOf('.') - 1;
                    if (position > colPropertyInfo.MaxDecimalPlaces) {
                        var errMsg = Message(MSG_MAX_PLACES, colPropertyInfo.Description, colPropertyInfo.MaxDecimalPlaces.ToString());
                        throw new MB.Util.APPException(errMsg, APPMessageType.DataInvalid);
                    }
                }
            }
            return true;
        }

        private static string Message(string msg, params string[] pars) {
            return string.Format(msg, pars);
        }
        #endregion

    }
}
