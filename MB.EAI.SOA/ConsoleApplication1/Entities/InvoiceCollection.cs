using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication1.Entities;
using System.Xml.Serialization;

namespace ConsoleApplication1.Entities
{
    [XmlRoot("invoice_1_0")]
    public class InvoiceCollection
    {
        [XmlElement("invoice")]
        public List<InvoiceInfo> lsInvoice;
    }
}
