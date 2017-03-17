using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebApplication1.Entities
{
    public class OutPKTHdr
    {
        #region Fields
        private int _Invc_Batch_Nbr;
        private string _Pkt_Ctrl_Nbr;
        private string _Whse;
        private string _Co;
        private string _Div;
        private string _Pkt_Nbr;
        private string _Pkt_Sfx;
        private string _Ord_Nbr;
        private string _Ord_Sfx;
        private string _Ord_Type;
        private string _Shipto;
        private string _Soldto;
        private string _Store_Nbr;
        private string _Dc_Ctr_Nbr;
        private string _Merch_Class;
        private string _Merch_Div;
        private string _Orig_Ship_Via;
        private string _Ship_Via;
        private string _Shpmt_Nbr;
        private string _Pnh_Ctrl_Nbr;
        private string _Pkt_Genrtn_Date;
        private string _Pkt_Prt_Date;
        private string _Back_Ord_Flag;
        private string _Sched_Dlvry_Date;
        private string _Ship_Date_Time;
        private string _Cust_Po_Nbr;
        private string _Pro_Nbr;
        private string _Appt_Nbr;
        private string _Acct_Rcvbl_Acct_Nbr;
        private string _Addr_Code;
        private double _Total_Wt;
        private int _Total_Nbr_Of_Carton;
        private string _Bol;
        private string _Manif_Nbr;
        private string _Ship_W_Ctrl_Nbr;
        private double _Prod_Value;
        private double _Shpng_Chrg;
        private double _Hndl_Chrg;
        private double _Insur_Chrg;
        private double _Tax_Chrg;
        private double _Misc_Chrg;
        private string _Spl_Instr_Code_1;
        private string _Spl_Instr_Code_2;
        private string _Spl_Instr_Code_3;
        private string _Spl_Instr_Code_4;
        private string _Spl_Instr_Code_5;
        private string _Spl_Instr_Code_6;
        private string _Spl_Instr_Code_7;
        private string _Spl_Instr_Code_8;
        private string _Spl_Instr_Code_9;
        private string _Spl_Instr_Code_10;
        private string _Batch_Ctrl_Nbr;
        private string _Rec_Xpans_Field;
        private string _Chkr;
        private string _Proc_Date_Time;
        private int _Proc_Stat_Code;
        private string _Appt_Made_By_Id;
        private string _Appt_Date;
        private string _Cust_Rte;
        private string _Disc_Date;
        private string _Cust_Dept;
        private string _Mang_Terr;
        private string _Merch_Code;
        private int _Nbr_Of_Hngr;
        private int _Nbr_Of_Times_Appt_Chgd;
        private string _Ord_Date;
        private int _Stat_Code;
        private string _Pkt_Type;
        private string _Prty_Code;
        private string _Prty_Sfx;
        private string _Shipto_Whse;
        private string _Slsprsn_Nbr;
        private string _Start_Ship_Date;
        private string _Stop_Ship_Date;
        private int _Total_Nbr_Of_Plt;
        private double _Total_Nbr_Of_Units;
        private int _Trlr_Stop_Seq_Nbr;
        private string _Vendor_Nbr;
        private string _Xfer_Whse;
        private string _Whse_Xfer_Flag;
        private string _Plan_Shpmt_Nbr;
        private string _Plan_Load_Nbr;
        private string _Rte_Id;
        private int _Rte_Stop_Seq;
        private string _Bol_Break_Attr;
        private string _Freight_Terms;
        private string _Plan_Bol;
        private string _Plan_Master_Bol;
        private string _Major_Pkt_Ctrl_Nbr;
        private int _Rte_Load_Seq;
        private int _Addr_Only_Pkt;
        private string _Zone_Skip;
        private string _Init_Ship_Via;
        private int _Pre_Pack_Flag;
        private string _Global_Locn_Nbr;
        private string _Create_Date_Time;
        private string _Mod_Date_Time;
        private string _User_Id;

        #endregion

        #region Properties
        [XmlElement("invc_batch_nbr")]
        public int Invc_Batch_Nbr { get { return _Invc_Batch_Nbr; } set { _Invc_Batch_Nbr = value; } }
        [XmlElement("pkt_ctrl_nbr")]
        public string Pkt_Ctrl_Nbr { get { return _Pkt_Ctrl_Nbr; } set { _Pkt_Ctrl_Nbr = value; } }
        [XmlElement("whse")]
        public string Whse { get { return _Whse; } set { _Whse = value; } }
        [XmlElement("co")]
        public string Co { get { return _Co; } set { _Co = value; } }
        [XmlElement("div")]
        public string Div { get { return _Div; } set { _Div = value; } }
        [XmlElement("pkt_nbr")]
        public string Pkt_Nbr { get { return _Pkt_Nbr; } set { _Pkt_Nbr = value; } }
        [XmlElement("pkt_sfx")]
        public string Pkt_Sfx { get { return _Pkt_Sfx; } set { _Pkt_Sfx = value; } }
        [XmlElement("ord_nbr")]
        public string Ord_Nbr { get { return _Ord_Nbr; } set { _Ord_Nbr = value; } }
        [XmlElement("ord_sfx")]
        public string Ord_Sfx { get { return _Ord_Sfx; } set { _Ord_Sfx = value; } }
        [XmlElement("ord_type")]
        public string Ord_Type { get { return _Ord_Type; } set { _Ord_Type = value; } }
        [XmlElement("shipto")]
        public string Shipto { get { return _Shipto; } set { _Shipto = value; } }
        [XmlElement("soldto")]
        public string Soldto { get { return _Soldto; } set { _Soldto = value; } }
        [XmlElement("store_nbr")]
        public string Store_Nbr { get { return _Store_Nbr; } set { _Store_Nbr = value; } }
        [XmlElement("dc_ctr_nbr")]
        public string Dc_Ctr_Nbr { get { return _Dc_Ctr_Nbr; } set { _Dc_Ctr_Nbr = value; } }
        [XmlElement("merch_class")]
        public string Merch_Class { get { return _Merch_Class; } set { _Merch_Class = value; } }
        [XmlElement("merch_div")]
        public string Merch_Div { get { return _Merch_Div; } set { _Merch_Div = value; } }
        [XmlElement("orig_ship_via")]
        public string Orig_Ship_Via { get { return _Orig_Ship_Via; } set { _Orig_Ship_Via = value; } }
        [XmlElement("ship_via")]
        public string Ship_Via { get { return _Ship_Via; } set { _Ship_Via = value; } }
        [XmlElement("shpmt_nbr")]
        public string Shpmt_Nbr { get { return _Shpmt_Nbr; } set { _Shpmt_Nbr = value; } }
        [XmlElement("pnh_ctrl_nbr")]
        public string Pnh_Ctrl_Nbr { get { return _Pnh_Ctrl_Nbr; } set { _Pnh_Ctrl_Nbr = value; } }
        [XmlElement("pkt_genrtn_date")]
        public string Pkt_Genrtn_Date { get { return _Pkt_Genrtn_Date; } set { _Pkt_Genrtn_Date = value; } }
        [XmlElement("pkt_prt_date")]
        public string Pkt_Prt_Date { get { return _Pkt_Prt_Date; } set { _Pkt_Prt_Date = value; } }
        [XmlElement("back_ord_flag")]
        public string Back_Ord_Flag { get { return _Back_Ord_Flag; } set { _Back_Ord_Flag = value; } }
        [XmlElement("sched_dlvry_date")]
        public string Sched_Dlvry_Date { get { return _Sched_Dlvry_Date; } set { _Sched_Dlvry_Date = value; } }
        [XmlElement("ship_date_time")]
        public string Ship_Date_Time { get { return _Ship_Date_Time; } set { _Ship_Date_Time = value; } }
        [XmlElement("cust_po_nbr")]
        public string Cust_Po_Nbr { get { return _Cust_Po_Nbr; } set { _Cust_Po_Nbr = value; } }
        [XmlElement("pro_nbr")]
        public string Pro_Nbr { get { return _Pro_Nbr; } set { _Pro_Nbr = value; } }
        [XmlElement("appt_nbr")]
        public string Appt_Nbr { get { return _Appt_Nbr; } set { _Appt_Nbr = value; } }
        [XmlElement("acct_rcvbl_acct_nbr")]
        public string Acct_Rcvbl_Acct_Nbr { get { return _Acct_Rcvbl_Acct_Nbr; } set { _Acct_Rcvbl_Acct_Nbr = value; } }
        [XmlElement("addr_code")]
        public string Addr_Code { get { return _Addr_Code; } set { _Addr_Code = value; } }
        [XmlElement("total_wt")]
        public double Total_Wt { get { return _Total_Wt; } set { _Total_Wt = value; } }
        [XmlElement("total_nbr_of_carton")]
        public int Total_Nbr_Of_Carton { get { return _Total_Nbr_Of_Carton; } set { _Total_Nbr_Of_Carton = value; } }
        [XmlElement("bol")]
        public string Bol { get { return _Bol; } set { _Bol = value; } }
        [XmlElement("manif_nbr")]
        public string Manif_Nbr { get { return _Manif_Nbr; } set { _Manif_Nbr = value; } }
        [XmlElement("ship_w_ctrl_nbr")]
        public string Ship_W_Ctrl_Nbr { get { return _Ship_W_Ctrl_Nbr; } set { _Ship_W_Ctrl_Nbr = value; } }
        [XmlElement("prod_value")]
        public double Prod_Value { get { return _Prod_Value; } set { _Prod_Value = value; } }
        [XmlElement("shpng_chrg")]
        public double Shpng_Chrg { get { return _Shpng_Chrg; } set { _Shpng_Chrg = value; } }
        [XmlElement("hndl_chrg")]
        public double Hndl_Chrg { get { return _Hndl_Chrg; } set { _Hndl_Chrg = value; } }
        [XmlElement("insur_chrg")]
        public double Insur_Chrg { get { return _Insur_Chrg; } set { _Insur_Chrg = value; } }
        [XmlElement("tax_chrg")]
        public double Tax_Chrg { get { return _Tax_Chrg; } set { _Tax_Chrg = value; } }
        [XmlElement("misc_chrg")]
        public double Misc_Chrg { get { return _Misc_Chrg; } set { _Misc_Chrg = value; } }
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
        [XmlElement("spl_instr_code_6")]
        public string Spl_Instr_Code_6 { get { return _Spl_Instr_Code_6; } set { _Spl_Instr_Code_6 = value; } }
        [XmlElement("spl_instr_code_7")]
        public string Spl_Instr_Code_7 { get { return _Spl_Instr_Code_7; } set { _Spl_Instr_Code_7 = value; } }
        [XmlElement("spl_instr_code_8")]
        public string Spl_Instr_Code_8 { get { return _Spl_Instr_Code_8; } set { _Spl_Instr_Code_8 = value; } }
        [XmlElement("spl_instr_code_9")]
        public string Spl_Instr_Code_9 { get { return _Spl_Instr_Code_9; } set { _Spl_Instr_Code_9 = value; } }
        [XmlElement("spl_instr_code_10")]
        public string Spl_Instr_Code_10 { get { return _Spl_Instr_Code_10; } set { _Spl_Instr_Code_10 = value; } }
        [XmlElement("batch_ctrl_nbr")]
        public string Batch_Ctrl_Nbr { get { return _Batch_Ctrl_Nbr; } set { _Batch_Ctrl_Nbr = value; } }
        [XmlElement("rec_xpans_field")]
        public string Rec_Xpans_Field { get { return _Rec_Xpans_Field; } set { _Rec_Xpans_Field = value; } }
        [XmlElement("chkr")]
        public string Chkr { get { return _Chkr; } set { _Chkr = value; } }
        [XmlElement("proc_date_time")]
        public string Proc_Date_Time { get { return _Proc_Date_Time; } set { _Proc_Date_Time = value; } }
        [XmlElement("proc_stat_code")]
        public int Proc_Stat_Code { get { return _Proc_Stat_Code; } set { _Proc_Stat_Code = value; } }
        [XmlElement("appt_made_by_id")]
        public string Appt_Made_By_Id { get { return _Appt_Made_By_Id; } set { _Appt_Made_By_Id = value; } }
        [XmlElement("appt_date")]
        public string Appt_Date { get { return _Appt_Date; } set { _Appt_Date = value; } }
        [XmlElement("cust_rte")]
        public string Cust_Rte { get { return _Cust_Rte; } set { _Cust_Rte = value; } }
        [XmlElement("disc_date")]
        public string Disc_Date { get { return _Disc_Date; } set { _Disc_Date = value; } }
        [XmlElement("cust_dept")]
        public string Cust_Dept { get { return _Cust_Dept; } set { _Cust_Dept = value; } }
        [XmlElement("mang_terr")]
        public string Mang_Terr { get { return _Mang_Terr; } set { _Mang_Terr = value; } }
        [XmlElement("merch_code")]
        public string Merch_Code { get { return _Merch_Code; } set { _Merch_Code = value; } }
        [XmlElement("nbr_of_hngr")]
        public int Nbr_Of_Hngr { get { return _Nbr_Of_Hngr; } set { _Nbr_Of_Hngr = value; } }
        [XmlElement("nbr_of_times_appt_chgd")]
        public int Nbr_Of_Times_Appt_Chgd { get { return _Nbr_Of_Times_Appt_Chgd; } set { _Nbr_Of_Times_Appt_Chgd = value; } }
        [XmlElement("ord_date")]
        public string Ord_Date { get { return _Ord_Date; } set { _Ord_Date = value; } }
        [XmlElement("stat_code")]
        public int Stat_Code { get { return _Stat_Code; } set { _Stat_Code = value; } }
        [XmlElement("pkt_type")]
        public string Pkt_Type { get { return _Pkt_Type; } set { _Pkt_Type = value; } }
        [XmlElement("prty_code")]
        public string Prty_Code { get { return _Prty_Code; } set { _Prty_Code = value; } }
        [XmlElement("prty_sfx")]
        public string Prty_Sfx { get { return _Prty_Sfx; } set { _Prty_Sfx = value; } }
        [XmlElement("shipto_whse")]
        public string Shipto_Whse { get { return _Shipto_Whse; } set { _Shipto_Whse = value; } }
        [XmlElement("slsprsn_nbr")]
        public string Slsprsn_Nbr { get { return _Slsprsn_Nbr; } set { _Slsprsn_Nbr = value; } }
        [XmlElement("start_ship_date")]
        public string Start_Ship_Date { get { return _Start_Ship_Date; } set { _Start_Ship_Date = value; } }
        [XmlElement("stop_ship_date")]
        public string Stop_Ship_Date { get { return _Stop_Ship_Date; } set { _Stop_Ship_Date = value; } }
        [XmlElement("total_nbr_of_plt")]
        public int Total_Nbr_Of_Plt { get { return _Total_Nbr_Of_Plt; } set { _Total_Nbr_Of_Plt = value; } }
        [XmlElement("total_nbr_of_units")]
        public double Total_Nbr_Of_Units { get { return _Total_Nbr_Of_Units; } set { _Total_Nbr_Of_Units = value; } }
        [XmlElement("trlr_stop_seq_nbr")]
        public int Trlr_Stop_Seq_Nbr { get { return _Trlr_Stop_Seq_Nbr; } set { _Trlr_Stop_Seq_Nbr = value; } }
        [XmlElement("vendor_nbr")]
        public string Vendor_Nbr { get { return _Vendor_Nbr; } set { _Vendor_Nbr = value; } }
        [XmlElement("xfer_whse")]
        public string Xfer_Whse { get { return _Xfer_Whse; } set { _Xfer_Whse = value; } }
        [XmlElement("whse_xfer_flag")]
        public string Whse_Xfer_Flag { get { return _Whse_Xfer_Flag; } set { _Whse_Xfer_Flag = value; } }
        [XmlElement("plan_shpmt_nbr")]
        public string Plan_Shpmt_Nbr { get { return _Plan_Shpmt_Nbr; } set { _Plan_Shpmt_Nbr = value; } }
        [XmlElement("plan_load_nbr")]
        public string Plan_Load_Nbr { get { return _Plan_Load_Nbr; } set { _Plan_Load_Nbr = value; } }
        [XmlElement("rte_id")]
        public string Rte_Id { get { return _Rte_Id; } set { _Rte_Id = value; } }
        [XmlElement("rte_stop_seq")]
        public int Rte_Stop_Seq { get { return _Rte_Stop_Seq; } set { _Rte_Stop_Seq = value; } }
        [XmlElement("bol_break_attr")]
        public string Bol_Break_Attr { get { return _Bol_Break_Attr; } set { _Bol_Break_Attr = value; } }
        [XmlElement("freight_terms")]
        public string Freight_Terms { get { return _Freight_Terms; } set { _Freight_Terms = value; } }
        [XmlElement("plan_bol")]
        public string Plan_Bol { get { return _Plan_Bol; } set { _Plan_Bol = value; } }
        [XmlElement("plan_master_bol")]
        public string Plan_Master_Bol { get { return _Plan_Master_Bol; } set { _Plan_Master_Bol = value; } }
        [XmlElement("major_pkt_ctrl_nbr")]
        public string Major_Pkt_Ctrl_Nbr { get { return _Major_Pkt_Ctrl_Nbr; } set { _Major_Pkt_Ctrl_Nbr = value; } }
        [XmlElement("rte_load_seq")]
        public int Rte_Load_Seq { get { return _Rte_Load_Seq; } set { _Rte_Load_Seq = value; } }
        [XmlElement("addr_only_pkt")]
        public int Addr_Only_Pkt { get { return _Addr_Only_Pkt; } set { _Addr_Only_Pkt = value; } }
        [XmlElement("zone_skip")]
        public string Zone_Skip { get { return _Zone_Skip; } set { _Zone_Skip = value; } }
        [XmlElement("init_ship_via")]
        public string Init_Ship_Via { get { return _Init_Ship_Via; } set { _Init_Ship_Via = value; } }
        [XmlElement("pre_pack_flag")]
        public int Pre_Pack_Flag { get { return _Pre_Pack_Flag; } set { _Pre_Pack_Flag = value; } }
        [XmlElement("global_locn_nbr")]
        public string Global_Locn_Nbr { get { return _Global_Locn_Nbr; } set { _Global_Locn_Nbr = value; } }
        [XmlElement("create_date_time")]
        public string Create_Date_Time { get { return _Create_Date_Time; } set { _Create_Date_Time = value; } }
        [XmlElement("mod_date_time")]
        public string Mod_Date_Time { get { return _Mod_Date_Time; } set { _Mod_Date_Time = value; } }
        [XmlElement("user_id")]
        public string User_Id { get { return _User_Id; } set { _User_Id = value; } }

        #endregion
    }
}