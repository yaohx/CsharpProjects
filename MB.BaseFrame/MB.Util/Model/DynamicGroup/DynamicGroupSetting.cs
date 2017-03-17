using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.Util.Model
{
    /// <summary>
    /// 动态聚组分组列的信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class DynamicGroupSetting
    {
        protected Dictionary<string, List<DataAreaField>> _DataAreaFields;//数据区域字段，求和，平均，数量，最大，最小等，在SQL中的聚合函数, 键是指定Field是从哪个对象来的

        protected Dictionary<string, List<string>> _GroupFields; //用于分组的字段，包括了行分组和列分组，在SQL中是Group By. 键是指定Field是从哪个对象来的

        protected DynamicGroupEntityInfos _EntityInfos; //用于指定数据库表或视图的名字，并且支持别名。并且能通过Name能从上面的字典中找到对应的字段

        protected List<DynamicGroupRelationInfo> _RelationInfos; //指定主表与详细表的关系 


        [DataMember]
        public Dictionary<string, List<DataAreaField>> DataAreaFields
        {
            get { return _DataAreaFields; }
            set { _DataAreaFields = value; }
        }

        [DataMember]
        public Dictionary<string, List<string>> GroupFields
        {
            get { return _GroupFields; }
            set { _GroupFields = value; }
        }

        [DataMember]
        public DynamicGroupEntityInfos EntityInfos
        {
            get { return _EntityInfos; }
            set { _EntityInfos = value; }
        }

        [DataMember]
        public List<DynamicGroupRelationInfo> RelationInfos
        {
            get { return _RelationInfos; }
            set { _RelationInfos = value; }
        }

        public DynamicGroupSetting()
        {
            _DataAreaFields = new Dictionary<string, List<DataAreaField>>();
            _GroupFields = new Dictionary<string,List<string>>();
            _RelationInfos = new List<DynamicGroupRelationInfo>();
            _EntityInfos = new DynamicGroupEntityInfos();
        }
    }

    
}
