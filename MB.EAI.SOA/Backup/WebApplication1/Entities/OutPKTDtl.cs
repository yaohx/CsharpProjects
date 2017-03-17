using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[Serializable]
public class OutPKTDtl
{
    #region Fields
    private int _Invc_Batch_Nbr;
    private string _Pkt_Ctrl_Nbr;
    private int _Pkt_Seq_Nbr;
    private string _Co;
    private string _Div;
    private string _Size_Rel_Posn_In_Table;
    private string _Season;
    private string _Orig_Season;
    private string _Season_Yr;
    private string _Orig_Season_Yr;
    private string _Style;
    private string _Orig_Style;
    private string _Style_Sfx;
    private string _Orig_Style_Sfx;
    private string _Color;
    private string _Orig_Color;
    private string _Color_Sfx;
    private string _Orig_Color_Sfx;
    private string _Sec_Dim;
    private string _Orig_Sec_Dim;
    private string _Qual;
    private string _Orig_Qual;
    private string _Batch_Nbr;
    private string _Invn_Type;
    private string _Prod_Stat;
    private string _Sku_Attr_1;
    private string _Sku_Attr_2;
    private string _Sku_Attr_3;
    private string _Sku_Attr_4;
    private string _Sku_Attr_5;
    private string _Cntry_Of_Orgn;
    private double _Pkt_Qty;
    private double _Shpd_Qty;
    private string _Rsn_Code;
    private string _Size_Range_Code;
    private string _Size_Desc;
    private string _Orig_Size_Desc;
    private string _Upc_Pre_Digit;
    private string _Upc_Vendor_Code;
    private string _Upc_Srl_Prod_Nbr;
    private string _Upc_Post_Digit;
    private string _Area;
    private string _Zone;
    private string _Aisle;
    private string _Bay;
    private string _Lvl;
    private string _Posn;
    private string _Spl_Instr_Code_1;
    private string _Spl_Instr_Code_2;
    private string _Spl_Instr_Code_3;
    private string _Spl_Instr_Code_4;
    private string _Spl_Instr_Code_5;
    private string _Misc_Instr_10_Byte_1;
    private string _Misc_Instr_10_Byte_2;
    private string _Batch_Ctrl_Nbr;
    private string _Rec_Xpans_Field;
    private string _Rec_Proc_Flag;
    private string _Proc_Date_Time;
    private int _Proc_Stat_Code;
    private string _Assort_Nbr;
    private double _Cancel_Qty;
    private string _Line_Type;
    private int _Orig_Ord_Line_Nbr;
    private int _Orig_Pkt_Line_Nbr;
    private double _Orig_Ord_Qty;
    private double _Orig_Pkt_Qty;
    private double _Price;
    private double _Retail_Price;
    private string _Sku_Id;
    private string _Orig_Sku_Id;
    private double _Unit_Wt;
    private double _Unit_Vol;
    private string _Uom;
    private string _Temp_Zone;
    private double _Back_Ord_Qty;
    private int _Shelf_Days;
    private string _Distro_Nbr;
    private int _Distro_Seq_Nbr;
    private string _Distro_Type;
    private string _Po_Nbr;
    private string _Ppack_Grp_Code;
    private double _Ppack_Qty;
    private string _Tms_Po_Pkt;
    private string _Exp_Info_Code;
    private int _Cust_Po_Line_Nbr;
    private string _Create_Date_Time;
    private string _Mod_Date_Time;
    private string _User_Id;

    #endregion

    #region Properties
    [XmlElement("invc_batch_nbr")]
    public int Invc_Batch_Nbr { get { return _Invc_Batch_Nbr; } set { _Invc_Batch_Nbr = value; } }
    [XmlElement("pkt_ctrl_nbr")]
    public string Pkt_Ctrl_Nbr { get { return _Pkt_Ctrl_Nbr; } set { _Pkt_Ctrl_Nbr = value; } }

    [XmlElement("pkt_seq_nbr")]
    public int Pkt_Seq_Nbr { get { return _Pkt_Seq_Nbr; } set { _Pkt_Seq_Nbr = value; } }
    [XmlElement("co")]
    public string Co { get { return _Co; } set { _Co = value; } }
    [XmlElement("div")]
    public string Div { get { return _Div; } set { _Div = value; } }
    [XmlElement("size_rel_posn_in_table")]
    public string Size_Rel_Posn_In_Table { get { return _Size_Rel_Posn_In_Table; } set { _Size_Rel_Posn_In_Table = value; } }
    [XmlElement("season")]
    public string Season { get { return _Season; } set { _Season = value; } }
    [XmlElement("orig_season")]
    public string Orig_Season { get { return _Orig_Season; } set { _Orig_Season = value; } }
    [XmlElement("season_yr")]
    public string Season_Yr { get { return _Season_Yr; } set { _Season_Yr = value; } }
    [XmlElement("orig_season_yr")]
    public string Orig_Season_Yr { get { return _Orig_Season_Yr; } set { _Orig_Season_Yr = value; } }
    [XmlElement("style")]
    public string Style { get { return _Style; } set { _Style = value; } }
    [XmlElement("orig_style")]
    public string Orig_Style { get { return _Orig_Style; } set { _Orig_Style = value; } }
    [XmlElement("style_sfx")]
    public string Style_Sfx { get { return _Style_Sfx; } set { _Style_Sfx = value; } }
    [XmlElement("orig_style_sfx")]
    public string Orig_Style_Sfx { get { return _Orig_Style_Sfx; } set { _Orig_Style_Sfx = value; } }
    [XmlElement("color")]
    public string Color { get { return _Color; } set { _Color = value; } }
    [XmlElement("orig_color")]
    public string Orig_Color { get { return _Orig_Color; } set { _Orig_Color = value; } }
    [XmlElement("color_sfx")]
    public string Color_Sfx { get { return _Color_Sfx; } set { _Color_Sfx = value; } }
    [XmlElement("orig_color_sfx")]
    public string Orig_Color_Sfx { get { return _Orig_Color_Sfx; } set { _Orig_Color_Sfx = value; } }
    [XmlElement("sec_dim")]
    public string Sec_Dim { get { return _Sec_Dim; } set { _Sec_Dim = value; } }
    [XmlElement("orig_sec_dim")]
    public string Orig_Sec_Dim { get { return _Orig_Sec_Dim; } set { _Orig_Sec_Dim = value; } }
    [XmlElement("qual")]
    public string Qual { get { return _Qual; } set { _Qual = value; } }
    [XmlElement("orig_qual")]
    public string Orig_Qual { get { return _Orig_Qual; } set { _Orig_Qual = value; } }
    [XmlElement("batch_nbr")]
    public string Batch_Nbr { get { return _Batch_Nbr; } set { _Batch_Nbr = value; } }
    [XmlElement("invn_type")]
    public string Invn_Type { get { return _Invn_Type; } set { _Invn_Type = value; } }
    [XmlElement("prod_stat")]
    public string Prod_Stat { get { return _Prod_Stat; } set { _Prod_Stat = value; } }
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
    [XmlElement("pkt_qty")]
    public double Pkt_Qty { get { return _Pkt_Qty; } set { _Pkt_Qty = value; } }
    [XmlElement("shpd_qty")]
    public double Shpd_Qty { get { return _Shpd_Qty; } set { _Shpd_Qty = value; } }
    [XmlElement("rsn_code")]
    public string Rsn_Code { get { return _Rsn_Code; } set { _Rsn_Code = value; } }
    [XmlElement("size_range_code")]
    public string Size_Range_Code { get { return _Size_Range_Code; } set { _Size_Range_Code = value; } }
    [XmlElement("size_desc")]
    public string Size_Desc { get { return _Size_Desc; } set { _Size_Desc = value; } }
    [XmlElement("orig_size_desc")]
    public string Orig_Size_Desc { get { return _Orig_Size_Desc; } set { _Orig_Size_Desc = value; } }
    [XmlElement("upc_pre_digit")]
    public string Upc_Pre_Digit { get { return _Upc_Pre_Digit; } set { _Upc_Pre_Digit = value; } }
    [XmlElement("upc_vendor_code")]
    public string Upc_Vendor_Code { get { return _Upc_Vendor_Code; } set { _Upc_Vendor_Code = value; } }
    [XmlElement("upc_srl_prod_nbr")]
    public string Upc_Srl_Prod_Nbr { get { return _Upc_Srl_Prod_Nbr; } set { _Upc_Srl_Prod_Nbr = value; } }
    [XmlElement("upc_post_digit")]
    public string Upc_Post_Digit { get { return _Upc_Post_Digit; } set { _Upc_Post_Digit = value; } }
    [XmlElement("area")]
    public string Area { get { return _Area; } set { _Area = value; } }
    [XmlElement("zone")]
    public string Zone { get { return _Zone; } set { _Zone = value; } }
    [XmlElement("aisle")]
    public string Aisle { get { return _Aisle; } set { _Aisle = value; } }
    [XmlElement("bay")]
    public string Bay { get { return _Bay; } set { _Bay = value; } }
    [XmlElement("lvl")]
    public string Lvl { get { return _Lvl; } set { _Lvl = value; } }
    [XmlElement("posn")]
    public string Posn { get { return _Posn; } set { _Posn = value; } }
    [XmlElement("spl_instr_code_1")]
    public string Spl_Instr_Code_1 { get { return _Spl_Instr_Code_1; } set { _Spl_Instr_Code_1 = value; } }
    [XmlElement("spl_instr_code_2")]
    public string Spl_Instr_Code_2 { get { return _Spl_Instr_Code_2; } set { _Spl_Instr_Code_2 = value; } }
    [XmlElement("spl_instr_code_3")]
    public string Spl_Instr_Code_3 { get { return _Spl_Instr_Code_3; } set { _Spl_Instr_Code_3 = value; } }
    [XmlElement("spl_instr_code_4")]
    public string Spl_Instr_Code_4 { get { return _Spl_Instr_Code_4; } set { _Spl_Instr_Code_4 = value; } }
    [XmlElement("spl_instr_code_5")]
    public string Spl_Instr_Code_5 { get { return _Spl_Instr_Code_5; } set { _Spl_Instr_Code_5 = value; } }
    [XmlElement("misc_instr_10_byte_1")]
    public string Misc_Instr_10_Byte_1 { get { return _Misc_Instr_10_Byte_1; } set { _Misc_Instr_10_Byte_1 = value; } }
    [XmlElement("misc_instr_10_byte_2")]
    public string Misc_Instr_10_Byte_2 { get { return _Misc_Instr_10_Byte_2; } set { _Misc_Instr_10_Byte_2 = value; } }
    [XmlElement("batch_ctrl_nbr")]
    public string Batch_Ctrl_Nbr { get { return _Batch_Ctrl_Nbr; } set { _Batch_Ctrl_Nbr = value; } }
    [XmlElement("rec_xpans_field")]
    public string Rec_Xpans_Field { get { return _Rec_Xpans_Field; } set { _Rec_Xpans_Field = value; } }
    [XmlElement("rec_proc_flag")]
    public string Rec_Proc_Flag { get { return _Rec_Proc_Flag; } set { _Rec_Proc_Flag = value; } }
    [XmlElement("proc_date_time")]
    public string Proc_Date_Time { get { return _Proc_Date_Time; } set { _Proc_Date_Time = value; } }
    [XmlElement("proc_stat_code")]
    public int Proc_Stat_Code { get { return _Proc_Stat_Code; } set { _Proc_Stat_Code = value; } }
    [XmlElement("assort_nbr")]
    public string Assort_Nbr { get { return _Assort_Nbr; } set { _Assort_Nbr = value; } }
    [XmlElement("cancel_qty")]
    public double Cancel_Qty { get { return _Cancel_Qty; } set { _Cancel_Qty = value; } }
    [XmlElement("line_type")]
    public string Line_Type { get { return _Line_Type; } set { _Line_Type = value; } }
    [XmlElement("orig_ord_line_nbr")]
    public int Orig_Ord_Line_Nbr { get { return _Orig_Ord_Line_Nbr; } set { _Orig_Ord_Line_Nbr = value; } }
    [XmlElement("orig_pkt_line_nbr")]
    public int Orig_Pkt_Line_Nbr { get { return _Orig_Pkt_Line_Nbr; } set { _Orig_Pkt_Line_Nbr = value; } }
    [XmlElement("orig_ord_qty")]
    public double Orig_Ord_Qty { get { return _Orig_Ord_Qty; } set { _Orig_Ord_Qty = value; } }
    [XmlElement("orig_pkt_qty")]
    public double Orig_Pkt_Qty { get { return _Orig_Pkt_Qty; } set { _Orig_Pkt_Qty = value; } }
    [XmlElement("price")]
    public double Price { get { return _Price; } set { _Price = value; } }
    [XmlElement("retail_price")]
    public double Retail_Price { get { return _Retail_Price; } set { _Retail_Price = value; } }
    [XmlElement("sku_id")]
    public string Sku_Id { get { return _Sku_Id; } set { _Sku_Id = value; } }
    [XmlElement("orig_sku_id")]
    public string Orig_Sku_Id { get { return _Orig_Sku_Id; } set { _Orig_Sku_Id = value; } }
    [XmlElement("unit_wt")]
    public double Unit_Wt { get { return _Unit_Wt; } set { _Unit_Wt = value; } }
    [XmlElement("unit_vol")]
    public double Unit_Vol { get { return _Unit_Vol; } set { _Unit_Vol = value; } }
    [XmlElement("uom")]
    public string Uom { get { return _Uom; } set { _Uom = value; } }
    [XmlElement("temp_zone")]
    public string Temp_Zone { get { return _Temp_Zone; } set { _Temp_Zone = value; } }
    [XmlElement("back_ord_qty")]
    public double Back_Ord_Qty { get { return _Back_Ord_Qty; } set { _Back_Ord_Qty = value; } }
    [XmlElement("shelf_days")]
    public int Shelf_Days { get { return _Shelf_Days; } set { _Shelf_Days = value; } }
    [XmlElement("distro_nbr")]
    public string Distro_Nbr { get { return _Distro_Nbr; } set { _Distro_Nbr = value; } }
    [XmlElement("distro_seq_nbr")]
    public int Distro_Seq_Nbr { get { return _Distro_Seq_Nbr; } set { _Distro_Seq_Nbr = value; } }
    [XmlElement("distro_type")]
    public string Distro_Type { get { return _Distro_Type; } set { _Distro_Type = value; } }
    [XmlElement("po_nbr")]
    public string Po_Nbr { get { return _Po_Nbr; } set { _Po_Nbr = value; } }
    [XmlElement("ppack_grp_code")]
    public string Ppack_Grp_Code { get { return _Ppack_Grp_Code; } set { _Ppack_Grp_Code = value; } }
    [XmlElement("ppack_qty")]
    public double Ppack_Qty { get { return _Ppack_Qty; } set { _Ppack_Qty = value; } }
    [XmlElement("tms_po_pkt")]
    public string Tms_Po_Pkt { get { return _Tms_Po_Pkt; } set { _Tms_Po_Pkt = value; } }
    [XmlElement("exp_info_code")]
    public string Exp_Info_Code { get { return _Exp_Info_Code; } set { _Exp_Info_Code = value; } }
    [XmlElement("cust_po_line_nbr")]
    public int Cust_Po_Line_Nbr { get { return _Cust_Po_Line_Nbr; } set { _Cust_Po_Line_Nbr = value; } }
    [XmlElement("create_date_time")]
    public string Create_Date_Time { get { return _Create_Date_Time; } set { _Create_Date_Time = value; } }
    [XmlElement("mod_date_time")]
    public string Mod_Date_Time { get { return _Mod_Date_Time; } set { _Mod_Date_Time = value; } }
    [XmlElement("user_id")]
    public string User_Id { get { return _User_Id; } set { _User_Id = value; } }

    #endregion
}