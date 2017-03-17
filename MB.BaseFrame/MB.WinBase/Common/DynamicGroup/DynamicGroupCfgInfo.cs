using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Util.Model;

namespace MB.WinBase.Common.DynamicGroup
{
    /// <summary>
    /// 动态聚组配置信息
    /// </summary>
    public class DynamicGroupCfgInfo
    {

        private Dictionary<string, DynamicGroupColumnPropertyInfo> _MainEntityColInfo;
        private Dictionary<string, DynamicGroupColumnPropertyInfo> _DetailEntityColInfo;
        private Dictionary<string, MB.Util.Model.DynamicGroupRelationInfo> _RelationInfo;
        private DynamicGroupEntityInfo _MainEntityInfo;
        private DynamicGroupEntityInfo _DetailEntityInfo;

        //public bool SameFieldInOneGroup { get; set; }
        //public string FieldGroups { get; set; }

        /// <summary>
        /// UI主查询对象中动态聚合列的配置
        /// </summary>
        public Dictionary<string, DynamicGroupColumnPropertyInfo> MainEntityColInfo
        {
            get { return _MainEntityColInfo; }
            set { _MainEntityColInfo = value; }
        }

        /// <summary>
        /// 从对象中的动态聚组列的配置信息
        /// 从对象可以是个表。如果是多个表，可以先做成一个视图，然后再配置动态聚组
        /// </summary>
        public Dictionary<string, DynamicGroupColumnPropertyInfo> DetailEntityColInfo
        {
            get { return _DetailEntityColInfo; }
            set { _DetailEntityColInfo = value; }
        }

        /// <summary>
        /// UI主查询对象与从对象之间的关联关系配置
        /// </summary>
        public Dictionary<string, MB.Util.Model.DynamicGroupRelationInfo> RelationInfo
        {
            get { return _RelationInfo; }
            set { _RelationInfo = value; }
        }

        /// <summary>
        /// 主对象的配置信息
        /// </summary>
        public DynamicGroupEntityInfo MainEntityInfo
        {
            get { return _MainEntityInfo; }
            set { _MainEntityInfo = value; }
        }

        /// <summary>
        /// 详细对象的配置信息
        /// </summary>
        public DynamicGroupEntityInfo DetailEntityInfo
        {
            get { return _DetailEntityInfo; }
            set { _DetailEntityInfo = value; }
        }

        /// <summary>
        /// 根据字段的名称获取配置对应的信息
        /// </summary>
        /// <param name="name">列名称</param>
        /// <param name="isMain">列是主对象还是从对象的</param>
        /// <returns>列配置信息</returns>
        public DynamicGroupColumnPropertyInfo GetColumnInfoByName(string name, bool isMain)
        {
            Dictionary<string, DynamicGroupColumnPropertyInfo> source = null;
            if (isMain)
                source = _MainEntityColInfo;
            else
                source = _DetailEntityColInfo;

            DynamicGroupColumnPropertyInfo colInfo = source[name];
            return colInfo;
        }
    }
}
