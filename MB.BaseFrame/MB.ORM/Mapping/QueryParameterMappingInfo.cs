using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Mapping {
    /// <summary>
    /// 查询参数的属性映射描述。
    /// </summary>
    public class QueryParameterMappingInfo {
        private string _PropertyName;
        private string _FieldName;

        /// <summary>
        /// 构造函数。
        /// </summary>
        public QueryParameterMappingInfo() {
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="fieldName"></param>
        public QueryParameterMappingInfo(string propertyName, string fieldName) {
            _PropertyName = propertyName;
            _FieldName = fieldName;
        }

        #region public 属性...
        /// <summary>
        /// 属性名称。
        /// </summary>
        public string PropertyName {
            get {
                return _PropertyName;
            }
            set {
                _PropertyName = value;
            }
        }
        /// <summary>
        /// 映射的字段名称。
        /// </summary>
        public string FieldName {
            get {
                return _FieldName;
            }
            set {
                _FieldName = value;
            }
        }
        #endregion public 属性...
    }
}
