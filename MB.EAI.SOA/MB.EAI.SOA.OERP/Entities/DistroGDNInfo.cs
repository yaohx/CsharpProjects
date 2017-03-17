using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace MB.EAI.SOA.OERP.Entities
{
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("INPT_DISTRO", "wmosInptDistro", new string[] { "ID" })]
    [KnownType(typeof(DistroGDNInfo))]
    public class DistroGDNInfo : MB.Orm.Common.BaseModel
    {
        #region ctor
        public DistroGDNInfo()
        {
        }
        #endregion

        #region propties
        private int _INPT_STORE_DISTRO_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("INPT_STORE_DISTRO_ID", System.Data.DbType.Int32)]
        public int INPT_STORE_DISTRO_ID
        {
            get { return _INPT_STORE_DISTRO_ID; }
            set { _INPT_STORE_DISTRO_ID = value; }
        }
        private string _WHSE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("WHSE", System.Data.DbType.String)]
        public string WHSE
        {
            get { return _WHSE; }
            set { _WHSE = value; }
        }
        private string _DISTRO_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DISTRO_NBR", System.Data.DbType.String)]
        public string DISTRO_NBR
        {
            get { return _DISTRO_NBR; }
            set { _DISTRO_NBR = value; }
        }
        private Int32 _SEQ_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SEQ_NBR", System.Data.DbType.Int32)]
        public Int32 SEQ_NBR
        {
            get { return _SEQ_NBR; }
            set { _SEQ_NBR = value; }
        }
        private string _DISTRO_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DISTRO_TYPE", System.Data.DbType.String)]
        public string DISTRO_TYPE
        {
            get { return _DISTRO_TYPE; }
            set { _DISTRO_TYPE = value; }
        }
        private string _PO_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PO_NBR", System.Data.DbType.String)]
        public string PO_NBR
        {
            get { return _PO_NBR; }
            set { _PO_NBR = value; }
        }
        private string _STORE_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STORE_NBR", System.Data.DbType.String)]
        public string STORE_NBR
        {
            get { return _STORE_NBR; }
            set { _STORE_NBR = value; }
        }
        private string _CO;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CO", System.Data.DbType.String)]
        public string CO
        {
            get { return _CO; }
            set { _CO = value; }
        }
        private string _DIV;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DIV", System.Data.DbType.String)]
        public string DIV
        {
            get { return _DIV; }
            set { _DIV = value; }
        }
        private string _SEASON;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SEASON", System.Data.DbType.String)]
        public string SEASON
        {
            get { return _SEASON; }
            set { _SEASON = value; }
        }
        private string _STYLE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STYLE", System.Data.DbType.String)]
        public string STYLE
        {
            get { return _STYLE; }
            set { _STYLE = value; }
        }
        private string _COLOR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("COLOR", System.Data.DbType.String)]
        public string COLOR
        {
            get { return _COLOR; }
            set { _COLOR = value; }
        }
        private string _SEC_DIM;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SEC_DIM", System.Data.DbType.String)]
        public string SEC_DIM
        {
            get { return _SEC_DIM; }
            set { _SEC_DIM = value; }
        }
        private string _SIZE_DESC;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SIZE_DESC", System.Data.DbType.String)]
        public string SIZE_DESC
        {
            get { return _SIZE_DESC; }
            set { _SIZE_DESC = value; }
        }
        private string _INVN_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("INVN_TYPE", System.Data.DbType.String)]
        public string INVN_TYPE
        {
            get { return _INVN_TYPE; }
            set { _INVN_TYPE = value; }
        }
        private string _SHPMT_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SHPMT_NBR", System.Data.DbType.String)]
        public string SHPMT_NBR
        {
            get { return _SHPMT_NBR; }
            set { _SHPMT_NBR = value; }
        }
        private Int16 _STAT_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STAT_CODE", System.Data.DbType.Int16)]
        public Int16 STAT_CODE
        {
            get { return _STAT_CODE; }
            set { _STAT_CODE = value; }
        }
        private string _MERCH_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MERCH_TYPE", System.Data.DbType.String)]
        public string MERCH_TYPE
        {
            get { return _MERCH_TYPE; }
            set { _MERCH_TYPE = value; }
        }
        private string _MERCH_GROUP;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MERCH_GROUP", System.Data.DbType.String)]
        public string MERCH_GROUP
        {
            get { return _MERCH_GROUP; }
            set { _MERCH_GROUP = value; }
        }
        private string _STORE_DEPT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STORE_DEPT", System.Data.DbType.String)]
        public string STORE_DEPT
        {
            get { return _STORE_DEPT; }
            set { _STORE_DEPT = value; }
        }
        private double _REQD_QTY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("REQD_QTY", System.Data.DbType.String)]
        public double REQD_QTY
        {
            get { return _REQD_QTY; }
            set { _REQD_QTY = value; }
        }
        private string _ASSORT_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ASSORT_NBR", System.Data.DbType.String)]
        public string ASSORT_NBR
        {
            get { return _ASSORT_NBR; }
            set { _ASSORT_NBR = value; }
        }
        private string _EVENT_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("EVENT_CODE", System.Data.DbType.String)]
        public string EVENT_CODE
        {
            get { return _EVENT_CODE; }
            set { _EVENT_CODE = value; }
        }
        private DateTime _IN_STORE_DATE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("IN_STORE_DATE", System.Data.DbType.DateTime)]
        public DateTime IN_STORE_DATE
        {
            get { return _IN_STORE_DATE; }
            set { _IN_STORE_DATE = value; }
        }
        private Int16 _WAVE_PROC_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("WAVE_PROC_TYPE", System.Data.DbType.Int16)]
        public Int16 WAVE_PROC_TYPE
        {
            get { return _WAVE_PROC_TYPE; }
            set { _WAVE_PROC_TYPE = value; }
        }
        private string _ORD_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ORD_TYPE", System.Data.DbType.String)]
        public string ORD_TYPE
        {
            get { return _ORD_TYPE; }
            set { _ORD_TYPE = value; }
        }
        private string _RTE_NAME;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RTE_NAME", System.Data.DbType.String)]
        public string RTE_NAME
        {
            get { return _RTE_NAME; }
            set { _RTE_NAME = value; }
        }
        private Int32 _ERROR_SEQ_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ERROR_SEQ_NBR", System.Data.DbType.Int32)]
        public Int32 ERROR_SEQ_NBR
        {
            get { return _ERROR_SEQ_NBR; }
            set { _ERROR_SEQ_NBR = value; }
        }
        private Int16 _PROC_STAT_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROC_STAT_CODE", System.Data.DbType.Int16)]
        public Int16 PROC_STAT_CODE
        {
            get { return _PROC_STAT_CODE; }
            set { _PROC_STAT_CODE = value; }
        }
        private string _CARTON_LABEL_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CARTON_LABEL_TYPE", System.Data.DbType.String)]
        public string CARTON_LABEL_TYPE
        {
            get { return _CARTON_LABEL_TYPE; }
            set { _CARTON_LABEL_TYPE = value; }
        }
        private Int16 _CARTON_CUBNG_INDIC;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CARTON_CUBNG_INDIC", System.Data.DbType.Int16)]
        public Int16 CARTON_CUBNG_INDIC
        {
            get { return _CARTON_CUBNG_INDIC; }
            set { _CARTON_CUBNG_INDIC = value; }
        }
        private string _REF_FIELD_1;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("REF_FIELD_1", System.Data.DbType.String)]
        public string REF_FIELD_1
        {
            get { return _REF_FIELD_1; }
            set { _REF_FIELD_1 = value; }
        }
        private string _REF_FIELD_2;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("REF_FIELD_2", System.Data.DbType.String)]
        public string REF_FIELD_2
        {
            get { return _REF_FIELD_2; }
            set { _REF_FIELD_2 = value; }
        }
        private string _REF_FIELD_3;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("REF_FIELD_3", System.Data.DbType.String)]
        public string REF_FIELD_3
        {
            get { return _REF_FIELD_3; }
            set { _REF_FIELD_3 = value; }
        }
        private DateTime _START_SHIP_DATE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("START_SHIP_DATE", System.Data.DbType.DateTime)]
        public DateTime START_SHIP_DATE
        {
            get { return _START_SHIP_DATE; }
            set { _START_SHIP_DATE = value; }
        }
        private string _UN_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UN_NBR", System.Data.DbType.String)]
        public string UN_NBR
        {
            get { return _UN_NBR; }
            set { _UN_NBR = value; }
        }
        private DateTime _MOD_DATE_TIME;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MOD_DATE_TIME", System.Data.DbType.DateTime)]
        public DateTime MOD_DATE_TIME
        {
            get { return _MOD_DATE_TIME; }
            set { _MOD_DATE_TIME = value; }
        }
        private string _USER_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("USER_ID", System.Data.DbType.String)]
        public string USER_ID
        {
            get { return _USER_ID; }
            set { _USER_ID = value; }
        }
        #endregion

    }
}
