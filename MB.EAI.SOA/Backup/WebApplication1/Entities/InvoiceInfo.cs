using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace MB.SOA.Web.Entities
{
    public class InvoiceInfo
    {
        private OutPKTHdr _OrderHeader;

        [XmlElement("outpt_pkt_hdr")]
        public OutPKTHdr OrderHeader
        {
            get { return _OrderHeader; }
            set { _OrderHeader = value; }
        }
        private List<OutPKTDtl> _lsOrderDtl;
        [XmlElement("outpt_pkt_dtl")]
        public List<OutPKTDtl> LsOrderDtl
        {
            get { return _lsOrderDtl; }
            set { _lsOrderDtl = value; }
        }
        private List<OutCatonHdr> _lsBoxHeader;
        [XmlElement("outpt_carton_hdr")]
        public List<OutCatonHdr> LsBoxHeader
        {
            get { return _lsBoxHeader; }
            set { _lsBoxHeader = value; }
        }
        private List<OutCartonDtl> _lsBoxDtl;
        [XmlElement("outpt_carton_dtl")]
        public List<OutCartonDtl> LsBoxDtl
        {
            get { return _lsBoxDtl; }
            set { _lsBoxDtl = value; }
        }



    }
}