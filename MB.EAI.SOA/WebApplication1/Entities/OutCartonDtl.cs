using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace MB.SOA.Web.Entities
{
    public class OutCartonDtl
    {
        #region Fields
        private int _Invc_Batch_Nbr;
        private string _Carton_Nbr;
        private int _Carton_Seq_Nbr;
        private string _Co;
        private string _Div;
        private string _Pkt_Ctrl_Nbr;

        private int _Pkt_Seq_Nbr;

        private string _Season;
        private string _Season_Yr;
        private string _Style;
        private string _Style_Sfx;
        private string _Color;
        private string _Color_Sfx;
        private string _Sec_Dim;
        private string _Qual;
        private string _Size_Range_Code;
        private string _Size_Rel_Posn_In_Table;
        private string _Size_Desc;
        private string _Invn_Type;
        private string _Cust_Sku;
        private string _Prod_Stat;
        private string _Batch_Nbr;
        private string _Sku_Attr_1;
        private string _Sku_Attr_2;
        private string _Sku_Attr_3;
        private string _Sku_Attr_4;
        private string _Sku_Attr_5;
        private string _Cntry_Of_Orgn;
        private string _Upc_Pre_Digit;
        private string _Upc_Vendor_Code;
        private string _Upc_Srl_Prod_Nbr;
        private string _Upc_Post_Digit;
        private double _Units_Pakd;
        private int _Bundles_Pakd;
        private string _Assort_Nbr;
        private string _Rec_Proc_Indic;
        private string _Proc_Date_Time;
        private int _Proc_Stat_Code;
        private string _Vendor_Id;
        private string _Vendor_Item_Nbr;
        private string _Cons_Prty_Date;
        private string _Ord_Type;
        private string _Sku_Id;
        private string _In_Store_Date;
        private string _Minor_Pkt_Ctrl_Nbr;
        private string _Minor_Po_Nbr;
        private string _Create_Date_Time;
        private string _Mod_Date_Time;
        private string _User_Id;


        #endregion

        #region Properties
        [XmlElement("invc_batch_nbr")]
        public int Invc_Batch_Nbr { get { return _Invc_Batch_Nbr; } set { _Invc_Batch_Nbr = value; } }
        [XmlElement("carton_nbr")]
        public string Carton_Nbr { get { return _Carton_Nbr; } set { _Carton_Nbr = value; } }
        [XmlElement("carton_seq_nbr")]
        public int Carton_Seq_Nbr { get { return _Carton_Seq_Nbr; } set { _Carton_Seq_Nbr = value; } }
        [XmlElement("co")]
        public string Co { get { return _Co; } set { _Co = value; } }
        [XmlElement("div")]
        public string Div { get { return _Div; } set { _Div = value; } }
        [XmlElement("pkt_ctrl_nbr")]
        public string Pkt_Ctrl_Nbr { get { return _Pkt_Ctrl_Nbr; } set { _Pkt_Ctrl_Nbr = value; } }

        [XmlElement("pkt_seq_nbr")]
        public int Pkt_Seq_Nbr { get { return _Pkt_Seq_Nbr; } set { _Pkt_Seq_Nbr = value; } }

        [XmlElement("season")]
        public string Season { get { return _Season; } set { _Season = value; } }
        [XmlElement("season_yr")]
        public string Season_Yr { get { return _Season_Yr; } set { _Season_Yr = value; } }
        [XmlElement("style")]
        public string Style { get { return _Style; } set { _Style = value; } }
        [XmlElement("style_sfx")]
        public string Style_Sfx { get { return _Style_Sfx; } set { _Style_Sfx = value; } }
        [XmlElement("color")]
        public string Color { get { return _Color; } set { _Color = value; } }
        [XmlElement("color_sfx")]
        public string Color_Sfx { get { return _Color_Sfx; } set { _Color_Sfx = value; } }
        [XmlElement("sec_dim")]
        public string Sec_Dim { get { return _Sec_Dim; } set { _Sec_Dim = value; } }
        [XmlElement("qual")]
        public string Qual { get { return _Qual; } set { _Qual = value; } }
        [XmlElement("size_range_code")]
        public string Size_Range_Code { get { return _Size_Range_Code; } set { _Size_Range_Code = value; } }
        [XmlElement("size_rel_posn_in_table")]
        public string Size_Rel_Posn_In_Table { get { return _Size_Rel_Posn_In_Table; } set { _Size_Rel_Posn_In_Table = value; } }
        [XmlElement("size_desc")]
        public string Size_Desc { get { return _Size_Desc; } set { _Size_Desc = value; } }
        [XmlElement("invn_type")]
        public string Invn_Type { get { return _Invn_Type; } set { _Invn_Type = value; } }
        [XmlElement("cust_sku")]
        public string Cust_Sku { get { return _Cust_Sku; } set { _Cust_Sku = value; } }
        [XmlElement("prod_stat")]
        public string Prod_Stat { get { return _Prod_Stat; } set { _Prod_Stat = value; } }
        [XmlElement("batch_nbr")]
        public string Batch_Nbr { get { return _Batch_Nbr; } set { _Batch_Nbr = value; } }
        [XmlElement("sku_attr_1")]
        public string Sku_Attr_1 { get { return _Sku_Attr_1; } set { _Sku_Attr_1 = value; } }
        [XmlElement("sku_attr_2")]
        public string Sku_Attr_2 { get { return _Sku_Attr_2; } set { _Sku_Attr_2 = value; } }
        [XmlElement("sku_attr_3")]
        public string Sku_Attr_3 { get { return _Sku_Attr_3; } set { _Sku_Attr_3 = value; } }
        [XmlElement("sku_attr_4")]
        public string Sku_Attr_4 { get { return _Sku_Attr_4; } set { _Sku_Attr_4 = value; } }
        [XmlElement("sku_attr_5")]
        public string Sku_Attr_5 { get { return _Sku_Attr_5; } set { _Sku_Attr_5 = value; } }
        [XmlElement("cntry_of_orgn")]
        public string Cntry_Of_Orgn { get { return _Cntry_Of_Orgn; } set { _Cntry_Of_Orgn = value; } }
        [XmlElement("upc_pre_digit")]
        public string Upc_Pre_Digit { get { return _Upc_Pre_Digit; } set { _Upc_Pre_Digit = value; } }
        [XmlElement("upc_vendor_code")]
        public string Upc_Vendor_Code { get { return _Upc_Vendor_Code; } set { _Upc_Vendor_Code = value; } }
        [XmlElement("upc_srl_prod_nbr")]
        public string Upc_Srl_Prod_Nbr { get { return _Upc_Srl_Prod_Nbr; } set { _Upc_Srl_Prod_Nbr = value; } }
        [XmlElement("upc_post_digit")]
        public string Upc_Post_Digit { get { return _Upc_Post_Digit; } set { _Upc_Post_Digit = value; } }
        [XmlElement("units_pakd")]
        public double Units_Pakd { get { return _Units_Pakd; } set { _Units_Pakd = value; } }
        [XmlElement("bundles_pakd")]
        public int Bundles_Pakd { get { return _Bundles_Pakd; } set { _Bundles_Pakd = value; } }
        [XmlElement("assort_nbr")]
        public string Assort_Nbr { get { return _Assort_Nbr; } set { _Assort_Nbr = value; } }
        [XmlElement("rec_proc_indic")]
        public string Rec_Proc_Indic { get { return _Rec_Proc_Indic; } set { _Rec_Proc_Indic = value; } }
        [XmlElement("proc_date_time")]
        public string Proc_Date_Time { get { return _Proc_Date_Time; } set { _Proc_Date_Time = value; } }
        [XmlElement("proc_stat_code")]
        public int Proc_Stat_Code { get { return _Proc_Stat_Code; } set { _Proc_Stat_Code = value; } }
        [XmlElement("vendor_id")]
        public string Vendor_Id { get { return _Vendor_Id; } set { _Vendor_Id = value; } }
        [XmlElement("vendor_item_nbr")]
        public string Vendor_Item_Nbr { get { return _Vendor_Item_Nbr; } set { _Vendor_Item_Nbr = value; } }
        [XmlElement("cons_prty_date")]
        public string Cons_Prty_Date { get { return _Cons_Prty_Date; } set { _Cons_Prty_Date = value; } }
        [XmlElement("ord_type")]
        public string Ord_Type { get { return _Ord_Type; } set { _Ord_Type = value; } }
        [XmlElement("sku_id")]
        public string Sku_Id { get { return _Sku_Id; } set { _Sku_Id = value; } }
        [XmlElement("in_store_date")]
        public string In_Store_Date { get { return _In_Store_Date; } set { _In_Store_Date = value; } }
        [XmlElement("minor_pkt_ctrl_nbr")]
        public string Minor_Pkt_Ctrl_Nbr { get { return _Minor_Pkt_Ctrl_Nbr; } set { _Minor_Pkt_Ctrl_Nbr = value; } }
        [XmlElement("minor_po_nbr")]
        public string Minor_Po_Nbr { get { return _Minor_Po_Nbr; } set { _Minor_Po_Nbr = value; } }
        [XmlElement("create_date_time")]
        public string Create_Date_Time { get { return _Create_Date_Time; } set { _Create_Date_Time = value; } }
        [XmlElement("mod_date_time")]
        public string Mod_Date_Time { get { return _Mod_Date_Time; } set { _Mod_Date_Time = value; } }
        [XmlElement("user_id")]
        public string User_Id { get { return _User_Id; } set { _User_Id = value; } }

        #endregion
    }
}