using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApplication1.Entities
{
    public class PixInfo
    {
        #region Fields
        private int _Wmos_Pix_Seq_Id;
        private string _Tran_Type;
        private string _Tran_Code;
        private int _Tran_Nbr;
        private int _Pix_Seq_Nbr;
        private int _Proc_Stat_Code;
        private string _Whse;
        private string _Co;
        private string _Div;
        private string _Case_Nbr;
        private string _Season;
        private string _Style;
        private string _Color;
        private string _Sec_Dim;
        private string _Size_Desc;
        private double _Invn_Adjmt_Qty;
        private string _Invn_Adjmt_Type;
        private string _Uom;
        private string _Rsn_Code;
        private string _Rcpt_Vari;
        private int _Cases_Shpd;
        private double _Units_Shpd;
        private int _Cases_Rcvd;
        private double _Units_Rcvd;
        private string _Actn_Code;
        private string _Date_Proc;
        private string _Sys_User_Id;
        private string _Error_Cmnt;
        private string _Ref_Code_Id_1;
        private string _Ref_Field_1;
        private string _Ref_Code_Id_2;
        private string _Ref_Field_2;
        private string _Ref_Code_Id_3;
        private string _Ref_Field_3;
        private string _Ref_Code_Id_4;
        private string _Ref_Field_4;
        private string _Ref_Code_Id_5;
        private string _Ref_Field_5;
        private string _Ref_Code_Id_6;
        private string _Ref_Field_6;
        private string _Ref_Code_Id_7;
        private string _Ref_Field_7;
        private string _Ref_Code_Id_8;
        private string _Ref_Field_8;
        private string _Ref_Code_Id_9;
        private string _Ref_Field_9;
        private string _Ref_Code_Id_10;
        private string _Ref_Field_10;
        private string _Create_Date_Time;
        private string _Mod_Date_Time;
        private string _User_Id;

        #endregion

        #region Properties
        [XmlElement("WMOS_PIX_SEQ_ID")]
        public int Wmos_Pix_Seq_Id { get { return _Wmos_Pix_Seq_Id; } set { _Wmos_Pix_Seq_Id = value; } }
        [XmlElement("TRAN_TYPE")]
        public string Tran_Type { get { return _Tran_Type; } set { _Tran_Type = value; } }
        [XmlElement("TRAN_CODE")]
        public string Tran_Code { get { return _Tran_Code; } set { _Tran_Code = value; } }
        [XmlElement("TRAN_NBR")]
        public int Tran_Nbr { get { return _Tran_Nbr; } set { _Tran_Nbr = value; } }
        [XmlElement("PIX_SEQ_NBR")]
        public int Pix_Seq_Nbr { get { return _Pix_Seq_Nbr; } set { _Pix_Seq_Nbr = value; } }
        [XmlElement("PROC_STAT_CODE")]
        public int Proc_Stat_Code { get { return _Proc_Stat_Code; } set { _Proc_Stat_Code = value; } }
        [XmlElement("WHSE")]
        public string Whse { get { return _Whse; } set { _Whse = value; } }
        [XmlElement("CO")]
        public string Co { get { return _Co; } set { _Co = value; } }
        [XmlElement("DIV")]
        public string Div { get { return _Div; } set { _Div = value; } }
        [XmlElement("CASE_NBR")]
        public string Case_Nbr { get { return _Case_Nbr; } set { _Case_Nbr = value; } }
        [XmlElement("SEASON")]
        public string Season { get { return _Season; } set { _Season = value; } }
        [XmlElement("STYLE")]
        public string Style { get { return _Style; } set { _Style = value; } }
        [XmlElement("COLOR")]
        public string Color { get { return _Color; } set { _Color = value; } }
        [XmlElement("SEC_DIM")]
        public string Sec_Dim { get { return _Sec_Dim; } set { _Sec_Dim = value; } }
        [XmlElement("SIZE_DESC")]
        public string Size_Desc { get { return _Size_Desc; } set { _Size_Desc = value; } }
        [XmlElement("INVN_ADJMT_QTY")]
        public double Invn_Adjmt_Qty { get { return _Invn_Adjmt_Qty; } set { _Invn_Adjmt_Qty = value; } }
        [XmlElement("INVN_ADJMT_ TYPE")]
        public string Invn_Adjmt_Type { get { return _Invn_Adjmt_Type; } set { _Invn_Adjmt_Type = value; } }
        [XmlElement("UOM")]
        public string Uom { get { return _Uom; } set { _Uom = value; } }
        [XmlElement("RSN_CODE")]
        public string Rsn_Code { get { return _Rsn_Code; } set { _Rsn_Code = value; } }
        [XmlElement("RCPT_VARI")]
        public string Rcpt_Vari { get { return _Rcpt_Vari; } set { _Rcpt_Vari = value; } }
        [XmlElement("CASES_SHPD")]
        public int Cases_Shpd { get { return _Cases_Shpd; } set { _Cases_Shpd = value; } }
        [XmlElement("UNITS_SHPD")]
        public double Units_Shpd { get { return _Units_Shpd; } set { _Units_Shpd = value; } }
        [XmlElement("CASES_RCVD")]
        public int Cases_Rcvd { get { return _Cases_Rcvd; } set { _Cases_Rcvd = value; } }
        [XmlElement("UNITS_RCVD")]
        public double Units_Rcvd { get { return _Units_Rcvd; } set { _Units_Rcvd = value; } }
        [XmlElement("ACTN_CODE")]
        public string Actn_Code { get { return _Actn_Code; } set { _Actn_Code = value; } }
        [XmlElement("DATE_PROC")]
        public string Date_Proc { get { return _Date_Proc; } set { _Date_Proc = value; } }
        [XmlElement("SYS_USER_ID")]
        public string Sys_User_Id { get { return _Sys_User_Id; } set { _Sys_User_Id = value; } }
        [XmlElement("ERROR_CMNT")]
        public string Error_Cmnt { get { return _Error_Cmnt; } set { _Error_Cmnt = value; } }
        [XmlElement("REF_CODE_ID_1")]
        public string Ref_Code_Id_1 { get { return _Ref_Code_Id_1; } set { _Ref_Code_Id_1 = value; } }
        [XmlElement("REF_FIELD_1")]
        public string Ref_Field_1 { get { return _Ref_Field_1; } set { _Ref_Field_1 = value; } }
        [XmlElement("REF_CODE_ID_2")]
        public string Ref_Code_Id_2 { get { return _Ref_Code_Id_2; } set { _Ref_Code_Id_2 = value; } }
        [XmlElement("REF_FIELD_2")]
        public string Ref_Field_2 { get { return _Ref_Field_2; } set { _Ref_Field_2 = value; } }
        [XmlElement("REF_CODE_ID_3")]
        public string Ref_Code_Id_3 { get { return _Ref_Code_Id_3; } set { _Ref_Code_Id_3 = value; } }
        [XmlElement("REF_FIELD_3")]
        public string Ref_Field_3 { get { return _Ref_Field_3; } set { _Ref_Field_3 = value; } }
        [XmlElement("REF_CODE_ID_4")]
        public string Ref_Code_Id_4 { get { return _Ref_Code_Id_4; } set { _Ref_Code_Id_4 = value; } }
        [XmlElement("REF_FIELD_4")]
        public string Ref_Field_4 { get { return _Ref_Field_4; } set { _Ref_Field_4 = value; } }
        [XmlElement("REF_CODE_ID_5")]
        public string Ref_Code_Id_5 { get { return _Ref_Code_Id_5; } set { _Ref_Code_Id_5 = value; } }
        [XmlElement("REF_FIELD_5")]
        public string Ref_Field_5 { get { return _Ref_Field_5; } set { _Ref_Field_5 = value; } }
        [XmlElement("REF_CODE_ID_6")]
        public string Ref_Code_Id_6 { get { return _Ref_Code_Id_6; } set { _Ref_Code_Id_6 = value; } }
        [XmlElement("REF_FIELD_6")]
        public string Ref_Field_6 { get { return _Ref_Field_6; } set { _Ref_Field_6 = value; } }
        [XmlElement("REF_CODE_ID_7")]
        public string Ref_Code_Id_7 { get { return _Ref_Code_Id_7; } set { _Ref_Code_Id_7 = value; } }
        [XmlElement("REF_FIELD_7")]
        public string Ref_Field_7 { get { return _Ref_Field_7; } set { _Ref_Field_7 = value; } }
        [XmlElement("REF_CODE_ID_8")]
        public string Ref_Code_Id_8 { get { return _Ref_Code_Id_8; } set { _Ref_Code_Id_8 = value; } }
        [XmlElement("REF_FIELD_8")]
        public string Ref_Field_8 { get { return _Ref_Field_8; } set { _Ref_Field_8 = value; } }
        [XmlElement("REF_CODE_ID_9")]
        public string Ref_Code_Id_9 { get { return _Ref_Code_Id_9; } set { _Ref_Code_Id_9 = value; } }
        [XmlElement("REF_FIELD_9")]
        public string Ref_Field_9 { get { return _Ref_Field_9; } set { _Ref_Field_9 = value; } }
        [XmlElement("REF_CODE_ID_10")]
        public string Ref_Code_Id_10 { get { return _Ref_Code_Id_10; } set { _Ref_Code_Id_10 = value; } }
        [XmlElement("REF_FIELD_10")]
        public string Ref_Field_10 { get { return _Ref_Field_10; } set { _Ref_Field_10 = value; } }
        [XmlElement("CREATE_DATE_ TIME")]
        public string Create_Date_Time { get { return _Create_Date_Time; } set { _Create_Date_Time = value; } }
        [XmlElement("MOD_DATE_TIME")]
        public string Mod_Date_Time { get { return _Mod_Date_Time; } set { _Mod_Date_Time = value; } }
        [XmlElement("USER_ID")]
        public string User_Id { get { return _User_Id; } set { _User_Id = value; } }


        #endregion
    }
}
