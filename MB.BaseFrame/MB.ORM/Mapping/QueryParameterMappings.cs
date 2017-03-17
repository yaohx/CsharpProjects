using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.Orm.Mapping {
    /// <summary>
    /// 查询参数的映射列表。
    /// </summary>
    public class QueryParameterMappings : Dictionary<string,QueryParameterMappingInfo>{
        private string _DefaultTableAlias;

        #region construct function...
        /// <summary>
        ///  查询参数的映射列表
        /// </summary>
        public QueryParameterMappings() {
        }
        /// <summary>
        /// 查询参数的映射列表
        /// </summary>
        /// <param name="defaultTableAlias">默认缺省的表别名</param>
        /// <param name="paramterMappingInfo">映射描述，属性和字段之间用逗号分开。</param>
        public QueryParameterMappings(string defaultTableAlias, params string[] paramterMappingInfo) {
            _DefaultTableAlias = defaultTableAlias;
            if (paramterMappingInfo != null && paramterMappingInfo.Length > 0) {
                foreach (string info in paramterMappingInfo) {
                    string[] ms = info.Split(',');
                    if (ms.Length != 2) continue; //只能有一个逗号
                    this.Add(ms[0], new QueryParameterMappingInfo(ms[0], ms[1]));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mappingInfo"></param>
        /// <returns></returns>
        public QueryParameterMappingInfo Add(QueryParameterMappingInfo mappingInfo) {
            if (this.ContainsKey(mappingInfo.PropertyName))
                throw new MB.Util.APPException(string.Format("在获取QueryParamMappings的时候存在重复的配置名称：{0}",mappingInfo.PropertyName), MB.Util.APPMessageType.SysErrInfo);
 
            base.Add(mappingInfo.PropertyName, mappingInfo);
            return mappingInfo;
        }
        #endregion construct function...

        /// <summary>
        /// 默认缺省的表别名。
        /// </summary>
        public string DefaultTableAlias {
            get {
                return _DefaultTableAlias;
            }
            set {
                _DefaultTableAlias = value;
            }
        }
    }
}
