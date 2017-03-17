using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MB.WinBase.Common
{
    [XmlRoot("Entity")]
    public class EntityUIConfigInfo
    {
        [XmlArray("Columns")]
        [XmlArrayItem("Column")]
        public List<ColumnPropertyInfo> Columns { get; set; }

        [XmlArray("EditUI")]
        [XmlArrayItem("Column")]
        public List<ColumnEditCfgInfo> EditUIs { get; set; }

        [XmlArray("DataFilter")]
        [XmlArrayItem("Elements")]
        public List<FilterElementCfgs> DataFilter { get; set; }
    }
}
