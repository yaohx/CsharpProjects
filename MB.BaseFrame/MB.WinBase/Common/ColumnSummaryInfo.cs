using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Util.XmlConfig;
using System.Xml.Serialization;

namespace MB.WinBase.Common {

    [Serializable]
    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    [XmlRoot("Summary")]
    public class SummaryInfo {

        #region XmlIgnore Field
        private object _DataSource; //如果配置了数据源的获取方式，则从配置中获取数据源
        private Dictionary<string, ColumnSummaryInfo> _ColumnSummaryInfos; //汇总列的信息
        #endregion

        private string _Name; //名字，随便填写，不能重复
        private InvokeDataSourceDescInfo _InvokeDataSourceDesc; //获取数据源的描述信息

        /// <summary>
        /// 名字，随便填写，不能重复
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement("Name")]
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// 获取数据源的调用描述信息。
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement("InvokeDataSourceDesc")]
        public InvokeDataSourceDescInfo InvokeDataSourceDesc {
            get {
                return _InvokeDataSourceDesc;
            }
            set {
                _InvokeDataSourceDesc = value;
            }
        }

        #region XmlIgnore 属性
        /// <summary>
        /// 数据源。
        /// 通过下拉框来进行选择的目前需要进行约束，只能接受DataSet 的数据集。
        /// 该数据源的获取可以通过配置UI 层客户端的业务类来得到。
        /// </summary>
        [XmlIgnore]
        public object DataSource {
            get {
                return _DataSource;
            }
            set {
                _DataSource = value;
            }
        }

        /// <summary>
        /// 汇总列的信息
        /// </summary>
        [XmlIgnore]
        public Dictionary<string, ColumnSummaryInfo> ColumnSummaryInfos {
            get { return _ColumnSummaryInfos; }
            set { _ColumnSummaryInfos = value; }
        }

        #endregion
    }

    /// <summary>
    /// 数据汇总用的列配置信息
    /// 该配置信息需要与ColumnPropertyInfo一起用，避免重复定义属性
    /// NAME属性用来和ColumnPropertyInfo做匹配
    /// </summary>
    [Serializable]
    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    [XmlRoot("Column")]
    public class ColumnSummaryInfo {
        #region 变量定义...
        private string _Name; //汇总列的名称
        private string _Description; //汇总列的描述
        private string _ColumnName; //ColumnPropertyInfo中对应的NAME
        private bool _IsSummaryCondition = false; //是否创建汇总的列，默认是False，表示根据这个列来汇总
        private bool _IsSummaryItem = false;//是否显示汇总的信息
        private SummaryItemType _SummaryItemType = SummaryItemType.None;//汇总的类型设置。
        private string _IncludeSummaryColumns;//包含的聚组列的名称集合，以逗号隔开。"Name1,Name2"
        private List<GroupByColumn> _GroupByColumn; //对非聚组列的处理

        #endregion 变量定义...

        #region 构造函数...
        public ColumnSummaryInfo(){
            _GroupByColumn = new List<GroupByColumn>();
        }

       
        #endregion 构造函数...

        /// <summary>
        /// 列的名称
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement("Name")]
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// 列的描述
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement("Description")]
        public string Description {
            get { return _Description; }
            set { _Description = value; }
        }

        /// <summary>
        /// ColumnPropertyInfo中对应的NAME
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement("ColumnName")]
        public string ColumnName {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }

        /// <summary>
        /// 是否创建汇总的列，表示根据这个列来汇总。默认是False。
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement("IsSummaryCondition")]
        public bool IsSummaryCondition {
            get { return _IsSummaryCondition; }
            set { _IsSummaryCondition = value; }
        }

        /// <summary>
        /// 是否是汇总列
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement("IsSummaryItem")]
        public bool IsSummaryItem {
            get { return _IsSummaryItem; }
            set { _IsSummaryItem = value; }
        }

        /// <summary>
        /// 汇总类型
        /// 这个信息可以从ColumnPropertyInfo继承。如果没有设置，则继承，如果设置了，则覆盖
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement("SummaryItemType")]
        public SummaryItemType SummaryItemType {
            get { return _SummaryItemType; }
            set { _SummaryItemType = value; }
        }

        /// <summary>
        /// 包含的聚组列的名称集合，以逗号隔开。"Name1,Name2"
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement("IncludeSummaryColumns")]
        public string IncludeSummaryColumns {
            get { return _IncludeSummaryColumns; }
            set { _IncludeSummaryColumns = value; }
        }

        /// <summary>
        /// 对非聚合列的处理集合
        /// </summary>
        [PropertyXmlConfig(typeof(GroupByColumn))]
        [XmlArray("IncludeGroupByColumns")]
        [XmlArrayItem("GroupByColumn")]
        public List<GroupByColumn> IncludeGroupByColumns {
            get { return _GroupByColumn; }
            set { _GroupByColumn = value; }
        }
    }


    /// <summary>
    /// 分组列配置信息
    /// 现在提供了对分组列的截取
    /// </summary>
    [ModelXmlConfig(ByXmlNodeAttribute = true)]
    public class GroupByColumn {
        #region 变量定义...
        private string _Name; //列的名称
        private string _SubString;//对列的值进行截取，截取的参数是 “截取起始位置,截取的长度”

        /// <summary>
        /// 非汇总列的名称
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement("Name")]
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// 对列的值进行截取，截取的参数是 “截取起始位置,截取的长度”
        /// </summary>
        [PropertyXmlConfig]
        [XmlElement("SubString")]
        public string SubString {
            get { return _SubString; }
            set { _SubString = value; }
        }


        #endregion 变量定义...
    }
}
