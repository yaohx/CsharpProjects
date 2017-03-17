using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using MB.Util.XmlConfig;

namespace MB.Util.Model
{
    /// <summary>
    /// 动态聚组查询对象信息，可以是表或者视图，支持两层（主从关系）
    /// </summary>
    [Serializable]
    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    [XmlRoot("DynamicGroupEntityInfos")]
    [DataContract]
    public class DynamicGroupEntityInfos
    {
        private DynamicGroupEntityInfo _MainEntity;
        private DynamicGroupEntityInfo _DetailEntity;

        [DataMember]
        public DynamicGroupEntityInfo MainEntity
        {
            get { return _MainEntity; }
            set { _MainEntity = value; }
        }

        [DataMember]
        public DynamicGroupEntityInfo DetailEntity
        {
            get { return _DetailEntity; }
            set { _DetailEntity = value; }
        }

    }


    [Serializable]
    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    [XmlRoot("DynamicGroupEntityInfo")]
    [DataContract]
    public class DynamicGroupEntityInfo
    {
        private string _Name;//名字,并且是数据库中表的名字

        private string _Description;//对于实体的描述
        private string _Alias;//别名，类似于Select * from [TableName] AS ???

         
        [DataMember]
        [PropertyXmlConfig]
        [XmlAttribute]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        [DataMember]
        [PropertyXmlConfig]
        [XmlAttribute]
        public string Description {
            get { return _Description; }
            set { _Description = value; }
        }

        [DataMember]
        [PropertyXmlConfig]
        [XmlAttribute]
        public string Alias
        {
            get { return _Alias; }
            set { _Alias = value; }
        }
    }
}
