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
    /// 支持的关系定义，现在只支持两层关系，用left join链接
    /// </summary>
    [Serializable]
    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    [XmlRoot("Relations")]
    [DataContract]
    public class DynamicGroupRelationInfo
    {
        #region 变量定义...
        private string _RelationWith;//与那张表关联，这里用的是实体的名字
        private string _Column;//主Entity中的关系列
        private string _WithColumn;//Detail Entity中的列的

        
        #endregion

        [DataMember]
        [PropertyXmlConfig]
        [XmlAttribute]
        public string RelationWith
        {
            get { return _RelationWith; }
            set { _RelationWith = value; }
        }

        [DataMember]
        [PropertyXmlConfig]
        [XmlAttribute]
        public string Column
        {
            get { return _Column; }
            set { _Column = value; }
        }

        [DataMember]
        [PropertyXmlConfig]
        [XmlAttribute]
        public string WithColumn
        {
            get { return _WithColumn; }
            set { _WithColumn = value; }
        }
    }
}
