using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace MB.Util {
    /// <summary>
    /// 提供
    /// </summary>
    //[MB.Aop.InjectionManager(true)]
    public class DataFilterHelper : System.ContextBoundObject {


        #region Instance...
        private static object _Object = new object();
        private static DataFilterHelper _Instance;

        protected DataFilterHelper() { }

        /// <summary>
        /// Instance
        /// </summary>
        public static DataFilterHelper Instance {
            get {
                if (_Instance == null) {
                    lock (_Object) {
                        if (_Instance == null)
                            _Instance = new DataFilterHelper();
                    }
                }
                return _Instance;
            }
        }
        #endregion Instance...


        /// <summary>
        /// 值之间的条件比较。
        /// </summary>
        /// <param name="srcValue"></param>
        /// <param name="desValue"></param>
        /// <param name="desValue2">在between 操作时才有效果</param>
        /// <param name="queryCondition"></param>
        /// <returns></returns>
        public bool ValueCompare(object srcValue, object desValue, object desValue2, DataFilterConditions queryCondition) {
            if (srcValue == null || srcValue == System.DBNull.Value)
                srcValue = string.Empty;
            if (desValue == null || desValue == System.DBNull.Value)
                desValue = string.Empty;

             
            int re = (new CaseInsensitiveComparer()).Compare(srcValue, desValue);

            switch (queryCondition) {
                case DataFilterConditions.Equal:
                    return re == 0;
                case DataFilterConditions.NotEqual:
                    return re != 0;
                case DataFilterConditions.GreaterOrEqual:
                    return re > 0 || re == 0;
                case DataFilterConditions.Greater:
                    return re > 0;
                case DataFilterConditions.Include:
                    return srcValue.ToString().IndexOf(desValue.ToString()) > -1;
                case DataFilterConditions.NotInclude:
                    return srcValue.ToString().IndexOf(desValue.ToString()) < 0;
                case DataFilterConditions.LessOrEqual:
                    return re < 0 || re == 0;
                case DataFilterConditions.Less:
                    return re < 0;
                case DataFilterConditions.Between:
                    int re2 = (new CaseInsensitiveComparer()).Compare(srcValue, desValue2);
                    return (re > 0 || re == 0) && (re2 < 0 || re2 == 0);
                default:
                    return false;
            }
        }
        /// <summary>
        /// 根据数据查询的条件标记获取该标记的描述。
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string GetConditionDescByVal(DataFilterConditions queryCondition) {
            switch (queryCondition) {
                case DataFilterConditions.Equal:
                    return "等于";
                case DataFilterConditions.NotEqual:
                    return "不等于";
                case DataFilterConditions.GreaterOrEqual:
                    return "大于或等于";
                case DataFilterConditions.Greater:
                    return "大于";
                case DataFilterConditions.Include:
                    return "包含";
                case DataFilterConditions.NotInclude:
                    return "不包含";
                case DataFilterConditions.LessOrEqual:
                    return "小于或等于";
                case DataFilterConditions.Less:
                    return "小于";
                case DataFilterConditions.Between:
                    return "介于";
                default:
                    return null;
            }
        }
        /// <summary>
        /// 根据字段类型获取可用于操作的条件集合。
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        public IList<DataFilterConditions> GetConditionsByType(string typeFullName) {
            List<DataFilterConditions> condis = new List<DataFilterConditions>();
            switch (typeFullName) {
                case "System.Int16":
                case "System.Int32":
                case "System.Decimal":

                    condis.Add(DataFilterConditions.Equal);
                    condis.Add(DataFilterConditions.NotEqual);
                    condis.Add(DataFilterConditions.Greater);
                    condis.Add(DataFilterConditions.GreaterOrEqual);
                    condis.Add(DataFilterConditions.Less);
                    condis.Add(DataFilterConditions.LessOrEqual);
                    break;
                case "System.Boolean":
                    //condis.Add(DataFilterConditions.Equal);
                    break;
                case "System.String":
                    condis.Add(DataFilterConditions.Equal);
                    condis.Add(DataFilterConditions.NotEqual);
                    condis.Add(DataFilterConditions.Include);
                    condis.Add(DataFilterConditions.NotInclude);
                    break;
                case "System.DateTime":
                    condis.Add(DataFilterConditions.Between);
                    condis.Add(DataFilterConditions.Equal);
                    condis.Add(DataFilterConditions.NotEqual);
                    condis.Add(DataFilterConditions.Greater);
                    condis.Add(DataFilterConditions.GreaterOrEqual);
                    condis.Add(DataFilterConditions.Less);
                    condis.Add(DataFilterConditions.LessOrEqual);
                    break;
                default:
                    condis.Add(DataFilterConditions.Equal);
                    condis.Add(DataFilterConditions.NotEqual);
                    condis.Add(DataFilterConditions.Include);
                    condis.Add(DataFilterConditions.NotInclude);
                    break;
            }
            return condis;
        }
    }

    /// <summary>
    /// DataFilterConditions 字段查询的连接条件。
    /// </summary>
    public enum DataFilterConditions {
        /// <summary>
        /// None
        /// </summary>
        [Description("None")]
        None,    //什么都没有选择
        /// <summary>
        /// 小于
        /// </summary>
        [Description("小于")]
        Less,	//小于
        /// <summary>
        /// 大于
        /// </summary>
        [Description("大于")]
        Greater,	//大于
        /// <summary>
        /// 等于
        /// </summary>
        [Description("等于")]
        Equal,	//等于
        /// <summary>
        /// 不等于
        /// </summary>
        [Description("不等于")]
        NotEqual,//不等于
        /// <summary>
        /// 包含
        /// </summary>
        [Description("包含")]
        Include,	//包含
        /// <summary>
        /// 不包含
        /// </summary>
        [Description("不包含")]
        NotInclude,	//不包含
        /// <summary>
        /// 以什么开始
        /// </summary>
        [Description("以什么开始")]
        BenginsWith,
        /// <summary>
        /// 以什么结束
        /// </summary>
        EndsWith,
        /// <summary>
        /// 介于什么之间
        /// </summary>
        [Description("介于什么之间")]
        Between,//介于什么之间
        /// <summary>
        /// 不介于什么之间
        /// </summary>
        [Description("不介于什么之间")]
        NotBetween,
        /// <summary>
        /// 大于或者等于
        /// </summary>
        [Description("大于或者等于")]
        GreaterOrEqual,//大于或者等于
        /// <summary>
        /// 小于或者等于
        /// </summary>
        [Description("小于或者等于")]
        LessOrEqual,//小于或者等于
        /// <summary>
        /// 为空
        /// </summary>
        [Description("为空")]
        IsNull,
        /// <summary>
        /// 不为空
        /// </summary>
        [Description("为空")]
        IsNotNull,
        /// <summary>
        /// 在指定范围内
        /// </summary>
        [Description("在指定范围内")]
        In,
        /// <summary>
        /// 不在在指定范围内
        /// </summary>
        [Description("不在指定范围内")]
        NotIn,
        /// <summary>
        /// 相似
        /// </summary>
        [Description("相似")]
        Like,
        /// <summary>
        /// 相似(非)
        /// </summary>
        [Description("相似(非)")]
        NotLike,
        /// <summary>
        /// 特殊条件处理(特殊需要)
        /// </summary>
        [Description("特殊条件处理")]
        Special,
        /// <summary>
        /// SQL 语句追加 
        /// </summary>
        [Description("SQL 语句追加")]
        SqlAppend
    }
}
