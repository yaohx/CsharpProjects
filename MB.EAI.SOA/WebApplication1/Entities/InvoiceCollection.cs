using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.SOA.Web.Entities;
using System.Xml.Serialization;

namespace MB.SOA.Web.Entities
{
    [XmlRoot("invoice_1_0")]
    public class InvoiceCollection
    {
        [XmlElement("invoice")]
        public List<InvoiceInfo> lsInvoice;
    }
}
