using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using MB.Util.XmlConfig;
using MB.BaseFrame;

namespace MB.Util.Model
{
    /// <summary>
    /// 数据库查询分析的行为描述信息。
    /// </summary>
    [ModelXmlConfig(ByXmlNodeAttribute = false)]
    [DataContract]
    public class QueryBehavior
    {
        private static QueryBehavior _DefaultBehavior;
        private static object _lock = new object();

        /// <summary>
        /// 缺省默认查询
        /// </summary>
        public static QueryBehavior DefaultBehavior
        {
            get
            {
                if (_DefaultBehavior == null)
                {
                    lock (_lock)
                    {
                        if (_DefaultBehavior == null)
                            _DefaultBehavior = new QueryBehavior(0, SOD.DEFAULT_MAX_SHOT_COUNT);
                    }
                }
                return _DefaultBehavior;
            }
        }
        #region 构造函数...
        /// <summary>
        /// 
        /// </summary>
        public QueryBehavior()
        {
            IsTotalPageDisplayed = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public QueryBehavior(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            IsTotalPageDisplayed = false;
        }
        #endregion


        /// <summary>
        /// 当前页
        /// </summary>
        [PropertyXmlConfig]
        [DataMember]
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页数量
        /// </summary>
        [PropertyXmlConfig]
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// 需要查找的字段
        /// 多个字段之间用逗号分开
        /// </summary>
        [PropertyXmlConfig]
        [DataMember]
        public string Columns { get; set; }
        /// <summary>
        /// 分处理的字段
        /// 多个字段之间用逗号分开
        /// </summary>
        [PropertyXmlConfig]
        [DataMember]
        public string GroupColumns { get; set; }

        /// <summary>
        /// 显示最大页数
        /// </summary>
        [PropertyXmlConfig]
        [DataMember]
        public bool IsTotalPageDisplayed { get; set; }


        /// <summary>
        /// 查询全部记录，不分页
        /// </summary>
        [PropertyXmlConfig]
        [DataMember]
        public bool IsQueryAll { get; set; }
    }
}
