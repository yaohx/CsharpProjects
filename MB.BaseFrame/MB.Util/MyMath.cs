//---------------------------------------------------------------- 
// Copyright (C) 2008-2009 www.metersbonwe.com
// All rights reserved. 
// 
// Author		:	Nick
// Create date	:	2008-01-05
// Description	:	常用公共计算方法.
// Modify date	:			By:					Why: 
//----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Util {
    /// <summary>
    /// 常用公共计算方法.
    /// </summary>
    public sealed class MyMath : System.ContextBoundObject {

        #region Instance...
        private static Object _Obj = new object();
        private static MyMath _Instance;

        /// <summary>
        /// 定义一个protected 的构造函数以阻止外部直接创建。
        /// </summary>
        protected MyMath() { }

        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static MyMath Instance {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new MyMath();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...

        #region 精度计算...
        /// <summary>
        /// 由于1111111.525 * 100.0时得到的结果是1111111.52499999999999
        /// Math.Round出现问题，所以用自己计算过的四舍五入。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="DesLength"></param>
        /// <returns></returns>
        public string SetRound(string strData, int desLength) {
            
            if (strData == null || strData.Length == 0)
                return "0";
            double d = Convert.ToDouble(strData);
            double dp = Math.Pow(10, desLength);
            double l = Math.Floor(dp * d + 0.5);
            return Convert.ToString(Math.Round(l / dp, desLength));
        }
        /// <summary>
        /// 四舍五入。
        /// </summary>
        /// <param name="pData"></param>
        /// <param name="pDesLength"></param>
        /// <returns></returns>
        public string SetRound(object strData, int desLength) {
            if (strData == null || strData == System.DBNull.Value || strData == System.DBNull.Value) {
                return "0";
            }
            return SetRound(strData.ToString(), desLength);
        }

        #endregion 精度计算...

        #region 字符窜处理...
        /// <summary>
        /// 格式化字符窜
        /// </summary>
        /// <param name="pStr"></param>
        /// <param name="pLen"></param>
        /// <param name="pFormatChar"></param>
        /// <returns></returns>
        public string FormatStr(string orgStr, int fLen, char fFormatChar) {
            string s = orgStr == null ? "" : orgStr;
            if (s.Length > fLen) {
                return orgStr.Substring(0, fLen);
            }
            else if (s == "") {
                for (int i = 0; i < fLen; i++) {
                    s += fFormatChar.ToString();
                }
                return s;
            }
            else if (s.Length < fLen) {
                int l = fLen - s.Length;
                for (int i = 0; i < l; i++) {
                    s += fFormatChar.ToString();
                }
                return s;
            }
            return s;
        }
        /// <summary>
        /// 格式化字符窜 (把附加的字符加在前面)
        /// </summary>
        /// <param name="pStr"></param>
        /// <param name="pLen"></param>
        /// <param name="pFormatChar"></param>
        /// <returns></returns>
        public string FormatStrLeft(string orgStr, int fLen, char fFormatChar) {
            string s = orgStr == null ? "" : orgStr;
            if (s.Length > fLen) {
                return orgStr.Substring(0, fLen);
            }
            else if (s == "") {
                for (int i = 0; i < fLen; i++) {
                    s = fFormatChar.ToString() + s;
                }
                return s;
            }
            else if (s.Length < fLen) {
                int l = fLen - s.Length;
                for (int i = 0; i < l; i++) {
                    s = fFormatChar.ToString() + s;
                }
                return s;
            }
            return s;
        }
        #endregion 字符窜处理...

        #region 时间描述处理...
        /// <summary>
        ///  周的描述从英文转换为中文。
        /// </summary>
        /// <param name="pDate"></param>
        /// <returns></returns>
        public string EnglishWeekToChinese(System.DateTime engDate) {
            string weekName = engDate.DayOfWeek.ToString();
            switch (weekName) {
                case "Monday":
                    return "周一";
                case "Tuesday":
                    return "周二";
                case "Wednesday":
                    return "周三";
                case "Thursday":
                    return "周四";
                case "Friday":
                    return "周五";
                case "Saturday":
                    return "周六";
                case "Sunday":
                    return "周日";
                default:
                    return weekName + "不能转换成中文的名称";
            }
        }

        #endregion 时间描述处理...

        #region 除法计算...
        /// <summary>
        /// 两个数相除  eliminates
        /// </summary>
        /// <param name="pDividend"></param>
        /// <param name="pDivisor"></param>
        /// <returns></returns>
        public decimal Dividend(object dividend, object divisor) {
            if (dividend == null || dividend == System.DBNull.Value
                || divisor == null || divisor == System.DBNull.Value) {
                return decimal.Parse("0.00");
            }
            if (double.Parse(divisor.ToString()) == 0) {
                return decimal.Parse("0.00");
            }
            decimal re = decimal.Parse(dividend.ToString()) / decimal.Parse(divisor.ToString());
            return re;
        }
        /// <summary>
        /// 两个数相除
        /// </summary>
        /// <param name="pDividend"></param>
        /// <param name="pDivisor"></param>
        /// <param name="pDesLength"></param>
        /// <returns></returns>
        public decimal Dividend(object dividend, object divisor, int desLength) {
            if (dividend == null || dividend == System.DBNull.Value
                || divisor == null || divisor == System.DBNull.Value) {

                return decimal.Parse("0.00");
            }
            if (double.Parse(divisor.ToString()) == 0) {
                return decimal.Parse("0.00");
            }
            decimal re = decimal.Parse(dividend.ToString()) / decimal.Parse(divisor.ToString());
            return decimal.Parse(SetRound(re, desLength));
        }
 
        /// <summary>
        /// 两个数相除 
        /// </summary>
        /// <param name="pDividend"></param>
        /// <param name="pDivisor"></param>
        /// <returns></returns>
        public float DividendToFloat(object dividend, object divisor) {
            if (dividend == null || dividend == System.DBNull.Value
                || divisor == null || divisor == System.DBNull.Value) {

                return float.Parse("0.00");
            }
            if (double.Parse(divisor.ToString()) == 0) {
                return float.Parse("0.00");
            }
            float re = float.Parse(dividend.ToString()) / float.Parse(divisor.ToString());
            return re;
        }
        /// <summary>
        /// 两个数相除
        /// </summary>
        /// <param name="pDividend"></param>
        /// <param name="pDivisor"></param>
        /// <param name="pDesLength"></param>
        /// <returns></returns>
        public float DividendToFloat(object dividend, object divisor, int desLength) {
            if (dividend == null || dividend == System.DBNull.Value
                || divisor == null || divisor == System.DBNull.Value) {

                return float.Parse("0.00");
            }
            if (double.Parse(divisor.ToString()) == 0) {
                return float.Parse("0.00");
            }
            float re = float.Parse(dividend.ToString()) / float.Parse(divisor.ToString());
            return float.Parse(SetRound(re, desLength));
        }
        #endregion 除法计算...

        #region 求和计算...
        /// <summary>
        /// 求两数的和
        /// </summary>
        /// <param name="pData1"></param>
        /// <param name="pData2"></param>
        /// <returns></returns>
        public int SumToInt32(object sData1, object sData2) {
            int data1 = MyConvert.Instance.ToInt(sData1);
            int data2 = MyConvert.Instance.ToInt(sData2);
            return data1 + data2;

        }
        /// <summary>
        /// 求两数的和
        /// </summary>
        /// <param name="pData1"></param>
        /// <param name="pData2"></param>
        /// <returns></returns>
        public decimal SumToDecimal(object pData1, object pData2) {
            decimal data1 = MyConvert.Instance.ToDecimal(pData1, 6);
            decimal data2 = MyConvert.Instance.ToDecimal(pData2, 6);
            return data1 + data2;

        }
        #endregion 求和计算...
    }
}
