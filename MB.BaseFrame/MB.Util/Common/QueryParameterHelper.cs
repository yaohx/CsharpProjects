using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using MB.Util.Model;
namespace MB.Util.Common {
    /// <summary>
    /// 提供对象数组转换为集合类的处理。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryParameterHelper {
        private List<QueryParameterInfo> _DataList;
        private MB.Util.Serializer.QueryParameterXmlSerializer _XmlSerializer;
        #region 构造函数...
        /// <summary>
        /// 提供对象数组转换为集合类的处理。
        /// </summary>
        public QueryParameterHelper() {
            _DataList = new List<QueryParameterInfo>();
        }
        /// <summary>
        /// 提供对象数组转换为集合类的处理。
        /// </summary>
        /// <param name="xmlFilterParams">查询参数系列化字符窜</param>
        public QueryParameterHelper(string xmlFilterParams) {
            if(_XmlSerializer==null)
                _XmlSerializer = new MB.Util.Serializer.QueryParameterXmlSerializer();
            var filters = _XmlSerializer.DeSerializer(xmlFilterParams);
            _DataList = new List<QueryParameterInfo>(filters);
        }
        /// <summary>
        /// 提供对象数组转换为集合类的处理。
        /// </summary>
        /// <param name="dataArray">查询参数数组</param>
        public QueryParameterHelper(QueryParameterInfo[] dataArray) {
            if(dataArray!=null && dataArray.Length > 0)
                _DataList = new List<QueryParameterInfo>(dataArray);
            else
                _DataList = new List<QueryParameterInfo>();
        }
        #endregion 构造函数...

        /// <summary>
        /// 根据参数名称
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public QueryParameterInfo GetParameterInfo(string paramName) {
            return _DataList.FirstOrDefault(o => string.Compare(o.PropertyName, paramName, true) == 0);
        }
        
        /// <summary>
        /// 追加参数。
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public QueryParameterInfo AddParameterInfo(string paramName,object value,DataFilterConditions condition) {
            QueryParameterInfo newParm = new QueryParameterInfo(paramName, value, condition);
            _DataList.Add(newParm);
            return newParm;
        }
        /// <summary>
        /// 追加参数。
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        /// <param name="condition"></param>
        /// <param name="dataType">System 下对应的数据类型。</param>
        /// <returns></returns>
        public QueryParameterInfo AddParameterInfo(string paramName, object value, DataFilterConditions condition,Type dataType) {
            QueryParameterInfo newParm = new QueryParameterInfo(paramName, value, condition);
            newParm.DataType = dataType.Name; 
            _DataList.Add(newParm);

            return newParm;
        }
        /// <summary>
        /// 追加分组节点。
        /// </summary>
        /// <param name="linkType"></param>
        /// <returns></returns>
        public QueryParameterInfo AddParameterGroup(MB.Util.Model.QueryGroupLinkType linkType) {
            QueryParameterInfo newParm = new QueryParameterInfo(linkType);
            _DataList.Add(newParm);
            return newParm;
        }

        /// <summary>
        /// 删除参数。
        /// </summary>
        /// <param name="paramName"></param>
        public void RemoveParameterInfo(string paramName) {
           QueryParameterInfo info =  _DataList.Find(o => string.Compare(o.PropertyName, paramName, true) == 0);
           if (info != null)
               _DataList.Remove(info); 
        }

        /// <summary>
        /// 以数组形式返回。
        /// </summary>
        /// <returns></returns>
        public QueryParameterInfo[] ToArray() {
            return _DataList.ToArray(); 
        }
        /// <summary>
        /// 系列为XML 字符窜。
        /// </summary>
        /// <returns></returns>
        public string SerializerToXmlString() {
            if (_XmlSerializer == null)
                _XmlSerializer = new MB.Util.Serializer.QueryParameterXmlSerializer();

            return _XmlSerializer.Serializer(_DataList.ToArray());   
        }
        /// <summary>
        /// 日期类型转换为具体的时间段。
        /// </summary>
        /// <param name="filterType"></param>
        /// <returns></returns>
        public MB.Util.Model.DateFilterStruct ToDateStruct(DateFilterType filterType) {
           
                DayOfWeek t = DateTime.Now.DayOfWeek;
                switch (filterType) {
                    case DateFilterType.Today:

                        return new MB.Util.Model.DateFilterStruct(DateTime.Now, DateTime.Now);
                    case DateFilterType.Week:
                        return new MB.Util.Model.DateFilterStruct(DateTime.Now.AddDays(1 - (int)DateTime.Now.DayOfWeek), DateTime.Now);
                    case DateFilterType.Month:
                        return new MB.Util.Model.DateFilterStruct(DateTime.Now.AddDays(1 - DateTime.Now.Day), DateTime.Now);
                    case DateFilterType.Year:
                        return new MB.Util.Model.DateFilterStruct(DateTime.Now.AddDays(1 - DateTime.Now.DayOfYear), DateTime.Now);
                    default:
                        throw new MB.Util.APPException(string.Format("当前过滤的日期类型 {0} 不支持转换！",filterType));
                }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum DateFilterType {
        [Description("不记得")]
        None,
        [Description("今天")]
        Today,
        [Description("本周")]
        Week,
        [Description("本月")]
        Month,
        [Description("本年度")]
        Year,
        [Description("其它")]
        Other
    }
}
