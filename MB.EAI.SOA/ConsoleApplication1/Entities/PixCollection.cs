using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApplication1.Entities
{
    [XmlRoot("PIX_1_0")]
    public class PixCollection
    {
        [XmlElement("PIX_TRAN")]
        public List<PixInfo> lsPixies;
    }
}
