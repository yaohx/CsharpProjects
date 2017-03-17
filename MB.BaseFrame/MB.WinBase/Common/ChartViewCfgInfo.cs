using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Util.XmlConfig;
using System.Xml.Serialization;

namespace MB.WinBase.Common
{
    [ModelXmlConfig(ByXmlNodeAttribute = false)]
    [XmlRoot("Chart")]
    public class ChartViewCfgInfo
    {
        private string _Name;
        private string _Title;
        private string _ViewType;
        private string _MappingType;
        private ChartMappingInfo _ChartMapping;

        /// <summary>
        ///  需要创建的图表名称。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        /// <summary>
        ///  需要创建的图表类型。
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string ViewType
        {
            get
            {
                return _ViewType;
            }
            set
            {
                _ViewType = value;
            }
        }

        /// <summary>
        /// 映射的数据绑定类型
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string MappingType
        {
            get { return _MappingType; }
            set { _MappingType = value; }
        }

        [PropertyXmlConfig]
        [XmlElement("ChartMapping")]
        public ChartMappingInfo ChartMapping
        {
            get { return _ChartMapping; }
            set { _ChartMapping = value; }
        }
    }
}
