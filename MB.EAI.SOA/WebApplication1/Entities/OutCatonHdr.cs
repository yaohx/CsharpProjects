using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace MB.SOA.Web.Entities
{
    public class OutCatonHdr
    {
        #region Fields
        private int _Invc_Batch_Nbr;
        private string _Carton_Nbr;
        private string _Whse;

        private string _Co;
        private string _Div;
        private string _Pkt_Ctrl_Nbr;

        private string _Pkt_Nbr;
        private string _Pkt_Sfx;
        private string _Ord_Nbr;
        private string _Ord_Sfx;
        private string _Sold_To;
        private string _Ship_To;
        private string _Cust_Po_Nbr;
        private string _Trkg_Nbr;
        private string _Carton_Type;
        private string _Carton_Size;
        private double _Carton_Vol;
        private double _Est_Wt;
        private double _Actl_Wt;
        private double _Total_Qty;
        private double _Frt_Chrg;
        private int _Carton_Nbr_X_Of_Y;
        private string _Plt_Id;
        private string _Shpmt_Nbr;
        private string _Bol;
        private string _Manif_Nbr;
        private string _Trlr_Nbr;
        private string _Ship_Via;
        private string _Appt_Nbr;
        private int _Nbr_Of_Times_Appt_Chgd;
        private string _Appt_Date;
        private string _Appt_Made_By;
        private int _Load_Seq_Nbr;
        private string _Audtr;
        private string _Pikr;
        private string _Pakr;
        private double _Misc_Num_1;
        private double _Misc_Num_2;
        private string _Misc_Instr_Code_1;
        private string _Misc_Instr_Code_2;
        private string _Misc_Instr_Code_3;
        private string _Misc_Instr_Code_4;
        private string _Misc_Instr_Code_5;
        private string _Proc_Date_Time;
        private int _Proc_Stat_Code;
        private string _Load_Nbr;
        private string _Rte_Id;
        private string _Load_Date_Time;
        private string _Master_Bol;
        private string _Parcl_Shpmt_Nbr;
        private string _Store_Nbr;
        private int _Misc_Carton;
        private double _Base_Chrg;
        private double _Addnl_Optn_Chrg;
        private double _Insur_Chrg;
        private double _Actl_Chrg_Amt;
        private double _Pre_Bulk_Base_Chrg;
        private double _Pre_Bulk_Addnl_Optn_Chrg;
        private string _Plt_Type;
        private string _Plt_Size;
        private double _Dist_Chrg;
        private string _Ship_Date_Time;
        private string _Epc_Match_Flag;
        private string _Carton_Epc;
        private string _Plt_Epc;
        private string _Loaded_Posn;
        private int _Plt_Master_Carton_Flag;
        private string _Create_Date_Time;
        private string _Mod_Date_Time;
        private string _User_Id;

        #endregion

        #region Properties
        [XmlElement("invc_batch_nbr")]
        public int Invc_Batch_Nbr { get { return _Invc_Batch_Nbr; } set { _Invc_Batch_Nbr = value; } }
        [XmlElement("carton_nbr")]
        public string Carton_Nbr { get { return _Carton_Nbr; } set { _Carton_Nbr = value; } }
        [XmlElement("whse")]
        public string Whse { get { return _Whse; } set { _Whse = value; } }

        [XmlElement("co")]
        public string Co { get { return _Co; } set { _Co = value; } }
        [XmlElement("div")]
        public string Div { get { return _Div; } set { _Div = value; } }
        [XmlElement("pkt_ctrl_nbr")]
        public string Pkt_Ctrl_Nbr { get { return _Pkt_Ctrl_Nbr; } set { _Pkt_Ctrl_Nbr = value; } }

        [XmlElement("pkt_nbr")]
        public string Pkt_Nbr { get { return _Pkt_Nbr; } set { _Pkt_Nbr = value; } }
        [XmlElement("pkt_sfx")]
        public string Pkt_Sfx { get { return _Pkt_Sfx; } set { _Pkt_Sfx = value; } }
        [XmlElement("ord_nbr")]
        public string Ord_Nbr { get { return _Ord_Nbr; } set { _Ord_Nbr = value; } }
        [XmlElement("ord_sfx")]
        public string Ord_Sfx { get { return _Ord_Sfx; } set { _Ord_Sfx = value; } }
        [XmlElement("sold_to")]
        public string Sold_To { get { return _Sold_To; } set { _Sold_To = value; } }
        [XmlElement("ship_to")]
        public string Ship_To { get { return _Ship_To; } set { _Ship_To = value; } }
        [XmlElement("cust_po_nbr")]
        public string Cust_Po_Nbr { get { return _Cust_Po_Nbr; } set { _Cust_Po_Nbr = value; } }
        [XmlElement("trkg_nbr")]
        public string Trkg_Nbr { get { return _Trkg_Nbr; } set { _Trkg_Nbr = value; } }
        [XmlElement("carton_type")]
        public string Carton_Type { get { return _Carton_Type; } set { _Carton_Type = value; } }
        [XmlElement("carton_size")]
        public string Carton_Size { get { return _Carton_Size; } set { _Carton_Size = value; } }
        [XmlElement("carton_vol")]
        public double Carton_Vol { get { return _Carton_Vol; } set { _Carton_Vol = value; } }
        [XmlElement("est_wt")]
        public double Est_Wt { get { return _Est_Wt; } set { _Est_Wt = value; } }
        [XmlElement("actl_wt")]
        public double Actl_Wt { get { return _Actl_Wt; } set { _Actl_Wt = value; } }
        [XmlElement("total_qty")]
        public double Total_Qty { get { return _Total_Qty; } set { _Total_Qty = value; } }
        [XmlElement("frt_chrg")]
        public double Frt_Chrg { get { return _Frt_Chrg; } set { _Frt_Chrg = value; } }
        [XmlElement("carton_nbr_x_of_y")]
        public int Carton_Nbr_X_Of_Y { get { return _Carton_Nbr_X_Of_Y; } set { _Carton_Nbr_X_Of_Y = value; } }
        [XmlElement("plt_id")]
        public string Plt_Id { get { return _Plt_Id; } set { _Plt_Id = value; } }
        [XmlElement("shpmt_nbr")]
        public string Shpmt_Nbr { get { return _Shpmt_Nbr; } set { _Shpmt_Nbr = value; } }
        [XmlElement("bol")]
        public string Bol { get { return _Bol; } set { _Bol = value; } }
        [XmlElement("manif_nbr")]
        public string Manif_Nbr { get { return _Manif_Nbr; } set { _Manif_Nbr = value; } }
        [XmlElement("trlr_nbr")]
        public string Trlr_Nbr { get { return _Trlr_Nbr; } set { _Trlr_Nbr = value; } }
        [XmlElement("ship_via")]
        public string Ship_Via { get { return _Ship_Via; } set { _Ship_Via = value; } }
        [XmlElement("appt_nbr")]
        public string Appt_Nbr { get { return _Appt_Nbr; } set { _Appt_Nbr = value; } }
        [XmlElement("nbr_of_times_appt_chgd")]
        public int Nbr_Of_Times_Appt_Chgd { get { return _Nbr_Of_Times_Appt_Chgd; } set { _Nbr_Of_Times_Appt_Chgd = value; } }
        [XmlElement("appt_date")]
        public string Appt_Date { get { return _Appt_Date; } set { _Appt_Date = value; } }
        [XmlElement("appt_made_by")]
        public string Appt_Made_By { get { return _Appt_Made_By; } set { _Appt_Made_By = value; } }
        [XmlElement("load_seq_nbr")]
        public int Load_Seq_Nbr { get { return _Load_Seq_Nbr; } set { _Load_Seq_Nbr = value; } }
        [XmlElement("audtr")]
        public string Audtr { get { return _Audtr; } set { _Audtr = value; } }
        [XmlElement("pikr")]
        public string Pikr { get { return _Pikr; } set { _Pikr = value; } }
        [XmlElement("pakr")]
        public string Pakr { get { return _Pakr; } set { _Pakr = value; } }
        [XmlElement("misc_num_1")]
        public double Misc_Num_1 { get { return _Misc_Num_1; } set { _Misc_Num_1 = value; } }
        [XmlElement("misc_num_2")]
        public double Misc_Num_2 { get { return _Misc_Num_2; } set { _Misc_Num_2 = value; } }
        [XmlElement("misc_instr_code_1")]
        public string Misc_Instr_Code_1 { get { return _Misc_Instr_Code_1; } set { _Misc_Instr_Code_1 = value; } }
        [XmlElement("misc_instr_code_2")]
        public string Misc_Instr_Code_2 { get { return _Misc_Instr_Code_2; } set { _Misc_Instr_Code_2 = value; } }
        [XmlElement("misc_instr_code_3")]
        public string Misc_Instr_Code_3 { get { return _Misc_Instr_Code_3; } set { _Misc_Instr_Code_3 = value; } }
        [XmlElement("misc_instr_code_4")]
        public string Misc_Instr_Code_4 { get { return _Misc_Instr_Code_4; } set { _Misc_Instr_Code_4 = value; } }
        [XmlElement("misc_instr_code_5")]
        public string Misc_Instr_Code_5 { get { return _Misc_Instr_Code_5; } set { _Misc_Instr_Code_5 = value; } }
        [XmlElement("proc_date_time")]
        public string Proc_Date_Time { get { return _Proc_Date_Time; } set { _Proc_Date_Time = value; } }
        [XmlElement("proc_stat_code")]
        public int Proc_Stat_Code { get { return _Proc_Stat_Code; } set { _Proc_Stat_Code = value; } }
        [XmlElement("load_nbr")]
        public string Load_Nbr { get { return _Load_Nbr; } set { _Load_Nbr = value; } }
        [XmlElement("rte_id")]
        public string Rte_Id { get { return _Rte_Id; } set { _Rte_Id = value; } }
        [XmlElement("load_date_time")]
        public string Load_Date_Time { get { return _Load_Date_Time; } set { _Load_Date_Time = value; } }
        [XmlElement("master_bol")]
        public string Master_Bol { get { return _Master_Bol; } set { _Master_Bol = value; } }
        [XmlElement("parcl_shpmt_nbr")]
        public string Parcl_Shpmt_Nbr { get { return _Parcl_Shpmt_Nbr; } set { _Parcl_Shpmt_Nbr = value; } }
        [XmlElement("store_nbr")]
        public string Store_Nbr { get { return _Store_Nbr; } set { _Store_Nbr = value; } }
        [XmlElement("misc_carton")]
        public int Misc_Carton { get { return _Misc_Carton; } set { _Misc_Carton = value; } }
        [XmlElement("base_chrg")]
        public double Base_Chrg { get { return _Base_Chrg; } set { _Base_Chrg = value; } }
        [XmlElement("addnl_optn_chrg")]
        public double Addnl_Optn_Chrg { get { return _Addnl_Optn_Chrg; } set { _Addnl_Optn_Chrg = value; } }
        [XmlElement("insur_chrg")]
        public double Insur_Chrg { get { return _Insur_Chrg; } set { _Insur_Chrg = value; } }
        [XmlElement("actl_chrg_amt")]
        public double Actl_Chrg_Amt { get { return _Actl_Chrg_Amt; } set { _Actl_Chrg_Amt = value; } }
        [XmlElement("pre_bulk _base_chrg")]
        public double Pre_Bulk_Base_Chrg { get { return _Pre_Bulk_Base_Chrg; } set { _Pre_Bulk_Base_Chrg = value; } }
        [XmlElement("pre_bulk _addnl_optn_chrg")]
        public double Pre_Bulk_Addnl_Optn_Chrg { get { return _Pre_Bulk_Addnl_Optn_Chrg; } set { _Pre_Bulk_Addnl_Optn_Chrg = value; } }
        [XmlElement("plt_type")]
        public string Plt_Type { get { return _Plt_Type; } set { _Plt_Type = value; } }
        [XmlElement("plt_size")]
        public string Plt_Size { get { return _Plt_Size; } set { _Plt_Size = value; } }
        [XmlElement("dist_chrg")]
        public double Dist_Chrg { get { return _Dist_Chrg; } set { _Dist_Chrg = value; } }
        [XmlElement("ship_date_time")]
        public string Ship_Date_Time { get { return _Ship_Date_Time; } set { _Ship_Date_Time = value; } }
        [XmlElement("epc_match_flag")]
        public string Epc_Match_Flag { get { return _Epc_Match_Flag; } set { _Epc_Match_Flag = value; } }
        [XmlElement("carton_epc")]
        public string Carton_Epc { get { return _Carton_Epc; } set { _Carton_Epc = value; } }
        [XmlElement("plt_epc")]
        public string Plt_Epc { get { return _Plt_Epc; } set { _Plt_Epc = value; } }
        [XmlElement("loaded_posn")]
        public string Loaded_Posn { get { return _Loaded_Posn; } set { _Loaded_Posn = value; } }
        [XmlElement("plt_master_carton_flag")]
        public int Plt_Master_Carton_Flag { get { return _Plt_Master_Carton_Flag; } set { _Plt_Master_Carton_Flag = value; } }
        [XmlElement("create_date_time")]
        public string Create_Date_Time { get { return _Create_Date_Time; } set { _Create_Date_Time = value; } }
        [XmlElement("mod_date_time")]
        public string Mod_Date_Time { get { return _Mod_Date_Time; } set { _Mod_Date_Time = value; } }
        [XmlElement("user_id")]
        public string User_Id { get { return _User_Id; } set { _User_Id = value; } }

        #endregion
    }
}