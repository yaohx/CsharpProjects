using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Util.XmlConfig;
using System.Xml.Serialization;

namespace MB.WinBase.Common
{
    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    [XmlRoot("ChartMapping")]
    public class ChartMappingInfo
    {
        private string _XValueColumn;
        private string _YValueColumn;
        private string _SeriesColumn;
        private string _ArgumentType;
        public ChartMappingInfo()
        {

        }
        public ChartMappingInfo(string xValueColumn, string yValueColumn, string seriesColumn, string argumentType)
        {
            _XValueColumn = xValueColumn;
            _YValueColumn = yValueColumn;
            _SeriesColumn = seriesColumn;
            _ArgumentType = argumentType;
        }
        /// <summary>
        /// X轴值的列名
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string XColumn
        {
            get { return _XValueColumn; }
            set { _XValueColumn = value; }
        }

        /// <summary>
        /// Y轴值得列名
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string YColumn
        {
            get { return _YValueColumn; }
            set { _YValueColumn = value; }
        }

        /// <summary>
        /// 系列值列名
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string SeriesColumn
        {
            get { return _SeriesColumn; }
            set { _SeriesColumn = value; }
        }

        /// <summary>
        /// 图表值类型
        /// </summary>
        [PropertyXmlConfig]
        [XmlAttribute]
        public string ArgumentType
        {
            get { return _ArgumentType; }
            set { _ArgumentType = value; }
        }
    }

    public enum ChartArgumentType
    {
        /// <summary>
        /// 求和
        /// </summary>
        Sum = 0x00001,

        /// <summary>
        /// 求数量
        /// </summary>
        Count = 0x00002,

        /// <summary>
        /// 最小值
        /// </summary>
        MinValue = 0x00003,

        /// <summary>
        /// 最大值
        /// </summary>
        MaxValue = 0x00004,

        /// <summary>
        /// 扩展值
        /// </summary>
        Extend = 0x00005,

        /// <summary>
        /// 平均值
        /// </summary>
        Average = 0x00010
    }
}
