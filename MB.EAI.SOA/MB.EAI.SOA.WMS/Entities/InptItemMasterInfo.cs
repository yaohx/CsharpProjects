using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.EAI.SOA.WMS.Entities
{
    // 需要引用的命名空间  using System.Runtime.Serialization;  

    /// <summary> 
    /// 文件生成时间 2014-04-02 03:42 
    /// </summary> 
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("INPT_ITEM_MASTER", "InptItemMaster", new string[] { "ID" })]
    [KnownType(typeof(InptItemMasterInfo))]
    public class InptItemMasterInfo : MB.Orm.Common.BaseModel
    {

        public InptItemMasterInfo()
        {

        }
        private int _INPT_ITEM_MASTER_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("INPT_ITEM_MASTER_ID", System.Data.DbType.Int32)]
        public int INPT_ITEM_MASTER_ID
        {
            get { return _INPT_ITEM_MASTER_ID; }
            set { _INPT_ITEM_MASTER_ID = value; }
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
        private string _SEASON_YR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SEASON_YR", System.Data.DbType.String)]
        public string SEASON_YR
        {
            get { return _SEASON_YR; }
            set { _SEASON_YR = value; }
        }
        private string _STYLE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STYLE", System.Data.DbType.String)]
        public string STYLE
        {
            get { return _STYLE; }
            set { _STYLE = value; }
        }
        private string _STYLE_SFX;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STYLE_SFX", System.Data.DbType.String)]
        public string STYLE_SFX
        {
            get { return _STYLE_SFX; }
            set { _STYLE_SFX = value; }
        }
        private string _COLOR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("COLOR", System.Data.DbType.String)]
        public string COLOR
        {
            get { return _COLOR; }
            set { _COLOR = value; }
        }
        private string _COLOR_SFX;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("COLOR_SFX", System.Data.DbType.String)]
        public string COLOR_SFX
        {
            get { return _COLOR_SFX; }
            set { _COLOR_SFX = value; }
        }
        private string _SEC_DIM;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SEC_DIM", System.Data.DbType.String)]
        public string SEC_DIM
        {
            get { return _SEC_DIM; }
            set { _SEC_DIM = value; }
        }
        private string _QUAL;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("QUAL", System.Data.DbType.String)]
        public string QUAL
        {
            get { return _QUAL; }
            set { _QUAL = value; }
        }
        private string _SIZE_RANGE_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SIZE_RANGE_CODE", System.Data.DbType.String)]
        public string SIZE_RANGE_CODE
        {
            get { return _SIZE_RANGE_CODE; }
            set { _SIZE_RANGE_CODE = value; }
        }
        private string _SIZE_REL_POSN_IN_TABLE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SIZE_REL_POSN_IN_TABLE", System.Data.DbType.String)]
        public string SIZE_REL_POSN_IN_TABLE
        {
            get { return _SIZE_REL_POSN_IN_TABLE; }
            set { _SIZE_REL_POSN_IN_TABLE = value; }
        }
        private string _SIZE_DESC;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SIZE_DESC", System.Data.DbType.String)]
        public string SIZE_DESC
        {
            get { return _SIZE_DESC; }
            set { _SIZE_DESC = value; }
        }
        private string _SKU_ATTR_REQD;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SKU_ATTR_REQD", System.Data.DbType.String)]
        public string SKU_ATTR_REQD
        {
            get { return _SKU_ATTR_REQD; }
            set { _SKU_ATTR_REQD = value; }
        }
        private string _BATCH_REQD;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BATCH_REQD", System.Data.DbType.String)]
        public string BATCH_REQD
        {
            get { return _BATCH_REQD; }
            set { _BATCH_REQD = value; }
        }
        private string _PROD_STAT_REQD;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROD_STAT_REQD", System.Data.DbType.String)]
        public string PROD_STAT_REQD
        {
            get { return _PROD_STAT_REQD; }
            set { _PROD_STAT_REQD = value; }
        }
        private string _CNTRY_OF_ORGN_REQD;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CNTRY_OF_ORGN_REQD", System.Data.DbType.String)]
        public string CNTRY_OF_ORGN_REQD
        {
            get { return _CNTRY_OF_ORGN_REQD; }
            set { _CNTRY_OF_ORGN_REQD = value; }
        }
        private Int32 _COORD_1;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("COORD_1", System.Data.DbType.Int32)]
        public Int32 COORD_1
        {
            get { return _COORD_1; }
            set { _COORD_1 = value; }
        }
        private Int32 _COORD_2;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("COORD_2", System.Data.DbType.Int32)]
        public Int32 COORD_2
        {
            get { return _COORD_2; }
            set { _COORD_2 = value; }
        }
        private string _VOLTY_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VOLTY_CODE", System.Data.DbType.String)]
        public string VOLTY_CODE
        {
            get { return _VOLTY_CODE; }
            set { _VOLTY_CODE = value; }
        }
        private string _PKG_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PKG_TYPE", System.Data.DbType.String)]
        public string PKG_TYPE
        {
            get { return _PKG_TYPE; }
            set { _PKG_TYPE = value; }
        }
        private string _PROD_GROUP;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROD_GROUP", System.Data.DbType.String)]
        public string PROD_GROUP
        {
            get { return _PROD_GROUP; }
            set { _PROD_GROUP = value; }
        }
        private string _PROD_SUB_GRP;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROD_SUB_GRP", System.Data.DbType.String)]
        public string PROD_SUB_GRP
        {
            get { return _PROD_SUB_GRP; }
            set { _PROD_SUB_GRP = value; }
        }
        private string _PROD_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROD_TYPE", System.Data.DbType.String)]
        public string PROD_TYPE
        {
            get { return _PROD_TYPE; }
            set { _PROD_TYPE = value; }
        }
        private string _PROD_LINE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROD_LINE", System.Data.DbType.String)]
        public string PROD_LINE
        {
            get { return _PROD_LINE; }
            set { _PROD_LINE = value; }
        }
        private string _SALE_GRP;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SALE_GRP", System.Data.DbType.String)]
        public string SALE_GRP
        {
            get { return _SALE_GRP; }
            set { _SALE_GRP = value; }
        }
        private string _DB_QTY_UOM;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DB_QTY_UOM", System.Data.DbType.String)]
        public string DB_QTY_UOM
        {
            get { return _DB_QTY_UOM; }
            set { _DB_QTY_UOM = value; }
        }
        private string _DSP_QTY_UOM;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DSP_QTY_UOM", System.Data.DbType.String)]
        public string DSP_QTY_UOM
        {
            get { return _DSP_QTY_UOM; }
            set { _DSP_QTY_UOM = value; }
        }
        private string _SKU_DESC;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SKU_DESC", System.Data.DbType.String)]
        public string SKU_DESC
        {
            get { return _SKU_DESC; }
            set { _SKU_DESC = value; }
        }
        private string _COLOR_DESC;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("COLOR_DESC", System.Data.DbType.String)]
        public string COLOR_DESC
        {
            get { return _COLOR_DESC; }
            set { _COLOR_DESC = value; }
        }
        private string _UPC_PRE_DIGIT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UPC_PRE_DIGIT", System.Data.DbType.String)]
        public string UPC_PRE_DIGIT
        {
            get { return _UPC_PRE_DIGIT; }
            set { _UPC_PRE_DIGIT = value; }
        }
        private string _UPC_VENDOR_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UPC_VENDOR_CODE", System.Data.DbType.String)]
        public string UPC_VENDOR_CODE
        {
            get { return _UPC_VENDOR_CODE; }
            set { _UPC_VENDOR_CODE = value; }
        }
        private string _UPC_SRL_PROD_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UPC_SRL_PROD_NBR", System.Data.DbType.String)]
        public string UPC_SRL_PROD_NBR
        {
            get { return _UPC_SRL_PROD_NBR; }
            set { _UPC_SRL_PROD_NBR = value; }
        }
        private string _UPC_POST_DIGIT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UPC_POST_DIGIT", System.Data.DbType.String)]
        public string UPC_POST_DIGIT
        {
            get { return _UPC_POST_DIGIT; }
            set { _UPC_POST_DIGIT = value; }
        }
        private string _CARTON_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CARTON_TYPE", System.Data.DbType.String)]
        public string CARTON_TYPE
        {
            get { return _CARTON_TYPE; }
            set { _CARTON_TYPE = value; }
        }
        private double _UNIT_PRICE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UNIT_PRICE", System.Data.DbType.String)]
        public double UNIT_PRICE
        {
            get { return _UNIT_PRICE; }
            set { _UNIT_PRICE = value; }
        }
        private double _RETAIL_PRICE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RETAIL_PRICE", System.Data.DbType.String)]
        public double RETAIL_PRICE
        {
            get { return _RETAIL_PRICE; }
            set { _RETAIL_PRICE = value; }
        }
        private string _OPER_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("OPER_CODE", System.Data.DbType.String)]
        public string OPER_CODE
        {
            get { return _OPER_CODE; }
            set { _OPER_CODE = value; }
        }
        private double _STD_PACK_QTY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_PACK_QTY", System.Data.DbType.String)]
        public double STD_PACK_QTY
        {
            get { return _STD_PACK_QTY; }
            set { _STD_PACK_QTY = value; }
        }
        private double _STD_BUNDL_QTY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_BUNDL_QTY", System.Data.DbType.String)]
        public double STD_BUNDL_QTY
        {
            get { return _STD_BUNDL_QTY; }
            set { _STD_BUNDL_QTY = value; }
        }
        private double _STD_CASE_QTY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_CASE_QTY", System.Data.DbType.String)]
        public double STD_CASE_QTY
        {
            get { return _STD_CASE_QTY; }
            set { _STD_CASE_QTY = value; }
        }
        private double _MAX_CASE_QTY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MAX_CASE_QTY", System.Data.DbType.String)]
        public double MAX_CASE_QTY
        {
            get { return _MAX_CASE_QTY; }
            set { _MAX_CASE_QTY = value; }
        }
        private Single _STD_CASE_LEN;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_CASE_LEN", System.Data.DbType.String)]
        public Single STD_CASE_LEN
        {
            get { return _STD_CASE_LEN; }
            set { _STD_CASE_LEN = value; }
        }
        private Single _STD_CASE_WIDTH;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_CASE_WIDTH", System.Data.DbType.String)]
        public Single STD_CASE_WIDTH
        {
            get { return _STD_CASE_WIDTH; }
            set { _STD_CASE_WIDTH = value; }
        }
        private Single _STD_CASE_HT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_CASE_HT", System.Data.DbType.String)]
        public Single STD_CASE_HT
        {
            get { return _STD_CASE_HT; }
            set { _STD_CASE_HT = value; }
        }
        private double _STD_TIER_QTY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_TIER_QTY", System.Data.DbType.String)]
        public double STD_TIER_QTY
        {
            get { return _STD_TIER_QTY; }
            set { _STD_TIER_QTY = value; }
        }
        private double _STD_PLT_QTY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_PLT_QTY", System.Data.DbType.String)]
        public double STD_PLT_QTY
        {
            get { return _STD_PLT_QTY; }
            set { _STD_PLT_QTY = value; }
        }
        private double _CUBE_MULT_QTY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CUBE_MULT_QTY", System.Data.DbType.String)]
        public double CUBE_MULT_QTY
        {
            get { return _CUBE_MULT_QTY; }
            set { _CUBE_MULT_QTY = value; }
        }
        private double _UNIT_WT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UNIT_WT", System.Data.DbType.String)]
        public double UNIT_WT
        {
            get { return _UNIT_WT; }
            set { _UNIT_WT = value; }
        }
        private double _UNIT_VOL;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UNIT_VOL", System.Data.DbType.String)]
        public double UNIT_VOL
        {
            get { return _UNIT_VOL; }
            set { _UNIT_VOL = value; }
        }
        private double _STD_PACK_WT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_PACK_WT", System.Data.DbType.String)]
        public double STD_PACK_WT
        {
            get { return _STD_PACK_WT; }
            set { _STD_PACK_WT = value; }
        }
        private double _STD_PACK_VOL;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_PACK_VOL", System.Data.DbType.String)]
        public double STD_PACK_VOL
        {
            get { return _STD_PACK_VOL; }
            set { _STD_PACK_VOL = value; }
        }
        private double _STD_CASE_WT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_CASE_WT", System.Data.DbType.String)]
        public double STD_CASE_WT
        {
            get { return _STD_CASE_WT; }
            set { _STD_CASE_WT = value; }
        }
        private double _STD_CASE_VOL;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_CASE_VOL", System.Data.DbType.String)]
        public double STD_CASE_VOL
        {
            get { return _STD_CASE_VOL; }
            set { _STD_CASE_VOL = value; }
        }
        private double _NEST_VOL;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("NEST_VOL", System.Data.DbType.String)]
        public double NEST_VOL
        {
            get { return _NEST_VOL; }
            set { _NEST_VOL = value; }
        }
        private Int32 _NEST_CNT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("NEST_CNT", System.Data.DbType.Int32)]
        public Int32 NEST_CNT
        {
            get { return _NEST_CNT; }
            set { _NEST_CNT = value; }
        }
        private Int32 _PROD_LIFE_IN_DAY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROD_LIFE_IN_DAY", System.Data.DbType.Int32)]
        public Int32 PROD_LIFE_IN_DAY
        {
            get { return _PROD_LIFE_IN_DAY; }
            set { _PROD_LIFE_IN_DAY = value; }
        }
        private string _ALLOW_RCPT_OLDER_SKU;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ALLOW_RCPT_OLDER_SKU", System.Data.DbType.String)]
        public string ALLOW_RCPT_OLDER_SKU
        {
            get { return _ALLOW_RCPT_OLDER_SKU; }
            set { _ALLOW_RCPT_OLDER_SKU = value; }
        }
        private Int16 _MHE_WT_TOL_AMNT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MHE_WT_TOL_AMNT", System.Data.DbType.Int16)]
        public Int16 MHE_WT_TOL_AMNT
        {
            get { return _MHE_WT_TOL_AMNT; }
            set { _MHE_WT_TOL_AMNT = value; }
        }
        private string _MHE_WT_TOL_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MHE_WT_TOL_TYPE", System.Data.DbType.String)]
        public string MHE_WT_TOL_TYPE
        {
            get { return _MHE_WT_TOL_TYPE; }
            set { _MHE_WT_TOL_TYPE = value; }
        }
        private Int16 _PICK_WT_TOL_AMNT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PICK_WT_TOL_AMNT", System.Data.DbType.Int16)]
        public Int16 PICK_WT_TOL_AMNT
        {
            get { return _PICK_WT_TOL_AMNT; }
            set { _PICK_WT_TOL_AMNT = value; }
        }
        private string _PICK_WT_TOL_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PICK_WT_TOL_TYPE", System.Data.DbType.String)]
        public string PICK_WT_TOL_TYPE
        {
            get { return _PICK_WT_TOL_TYPE; }
            set { _PICK_WT_TOL_TYPE = value; }
        }
        private string _TRLR_TEMP_ZONE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("TRLR_TEMP_ZONE", System.Data.DbType.String)]
        public string TRLR_TEMP_ZONE
        {
            get { return _TRLR_TEMP_ZONE; }
            set { _TRLR_TEMP_ZONE = value; }
        }
        private DateTime _ACTVTN_DATE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ACTVTN_DATE", System.Data.DbType.DateTime)]
        public DateTime ACTVTN_DATE
        {
            get { return _ACTVTN_DATE; }
            set { _ACTVTN_DATE = value; }
        }
        private Int32 _MAX_RECV_TO_XPIRE_DAYS;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MAX_RECV_TO_XPIRE_DAYS", System.Data.DbType.Int32)]
        public Int32 MAX_RECV_TO_XPIRE_DAYS
        {
            get { return _MAX_RECV_TO_XPIRE_DAYS; }
            set { _MAX_RECV_TO_XPIRE_DAYS = value; }
        }
        private Single _CRITCL_DIM_1;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CRITCL_DIM_1", System.Data.DbType.String)]
        public Single CRITCL_DIM_1
        {
            get { return _CRITCL_DIM_1; }
            set { _CRITCL_DIM_1 = value; }
        }
        private Single _CRITCL_DIM_2;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CRITCL_DIM_2", System.Data.DbType.String)]
        public Single CRITCL_DIM_2
        {
            get { return _CRITCL_DIM_2; }
            set { _CRITCL_DIM_2 = value; }
        }
        private Single _CRITCL_DIM_3;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CRITCL_DIM_3", System.Data.DbType.String)]
        public Single CRITCL_DIM_3
        {
            get { return _CRITCL_DIM_3; }
            set { _CRITCL_DIM_3 = value; }
        }
        private string _MFG_DATE_REQD;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MFG_DATE_REQD", System.Data.DbType.String)]
        public string MFG_DATE_REQD
        {
            get { return _MFG_DATE_REQD; }
            set { _MFG_DATE_REQD = value; }
        }
        private string _XPIRE_DATE_REQD;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("XPIRE_DATE_REQD", System.Data.DbType.String)]
        public string XPIRE_DATE_REQD
        {
            get { return _XPIRE_DATE_REQD; }
            set { _XPIRE_DATE_REQD = value; }
        }
        private string _SHIP_BY_DATE_REQD;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SHIP_BY_DATE_REQD", System.Data.DbType.String)]
        public string SHIP_BY_DATE_REQD
        {
            get { return _SHIP_BY_DATE_REQD; }
            set { _SHIP_BY_DATE_REQD = value; }
        }
        private Int16 _STAT_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STAT_CODE", System.Data.DbType.Int16)]
        public Int16 STAT_CODE
        {
            get { return _STAT_CODE; }
            set { _STAT_CODE = value; }
        }
        private string _SKU_BRCD;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SKU_BRCD", System.Data.DbType.String)]
        public string SKU_BRCD
        {
            get { return _SKU_BRCD; }
            set { _SKU_BRCD = value; }
        }
        private double _STD_SUB_PACK_QTY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_SUB_PACK_QTY", System.Data.DbType.String)]
        public double STD_SUB_PACK_QTY
        {
            get { return _STD_SUB_PACK_QTY; }
            set { _STD_SUB_PACK_QTY = value; }
        }
        private string _CATCH_WT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CATCH_WT", System.Data.DbType.String)]
        public string CATCH_WT
        {
            get { return _CATCH_WT; }
            set { _CATCH_WT = value; }
        }
        private Int16 _CONS_PRTY_DATE_WINDOW_INCR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CONS_PRTY_DATE_WINDOW_INCR", System.Data.DbType.Int16)]
        public Int16 CONS_PRTY_DATE_WINDOW_INCR
        {
            get { return _CONS_PRTY_DATE_WINDOW_INCR; }
            set { _CONS_PRTY_DATE_WINDOW_INCR = value; }
        }
        private string _LOAD_ATTR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("LOAD_ATTR", System.Data.DbType.String)]
        public string LOAD_ATTR
        {
            get { return _LOAD_ATTR; }
            set { _LOAD_ATTR = value; }
        }
        private Single _WT_TOL_PCNT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("WT_TOL_PCNT", System.Data.DbType.String)]
        public Single WT_TOL_PCNT
        {
            get { return _WT_TOL_PCNT; }
            set { _WT_TOL_PCNT = value; }
        }
        private string _CONS_PRTY_DATE_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CONS_PRTY_DATE_CODE", System.Data.DbType.String)]
        public string CONS_PRTY_DATE_CODE
        {
            get { return _CONS_PRTY_DATE_CODE; }
            set { _CONS_PRTY_DATE_CODE = value; }
        }
        private string _HAZMAT_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("HAZMAT_CODE", System.Data.DbType.String)]
        public string HAZMAT_CODE
        {
            get { return _HAZMAT_CODE; }
            set { _HAZMAT_CODE = value; }
        }
        private string _CONS_PRTY_DATE_WINDOW;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CONS_PRTY_DATE_WINDOW", System.Data.DbType.String)]
        public string CONS_PRTY_DATE_WINDOW
        {
            get { return _CONS_PRTY_DATE_WINDOW; }
            set { _CONS_PRTY_DATE_WINDOW = value; }
        }
        private double _AVG_DLY_DMND;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("AVG_DLY_DMND", System.Data.DbType.String)]
        public double AVG_DLY_DMND
        {
            get { return _AVG_DLY_DMND; }
            set { _AVG_DLY_DMND = value; }
        }
        private string _TEMP_ZONE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("TEMP_ZONE", System.Data.DbType.String)]
        public string TEMP_ZONE
        {
            get { return _TEMP_ZONE; }
            set { _TEMP_ZONE = value; }
        }
        private string _SKU_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SKU_ID", System.Data.DbType.String)]
        public string SKU_ID
        {
            get { return _SKU_ID; }
            set { _SKU_ID = value; }
        }
        private string _VENDOR_ITEM_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VENDOR_ITEM_NBR", System.Data.DbType.String)]
        public string VENDOR_ITEM_NBR
        {
            get { return _VENDOR_ITEM_NBR; }
            set { _VENDOR_ITEM_NBR = value; }
        }
        private double _UNITS_PER_PICK_ACTIVE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UNITS_PER_PICK_ACTIVE", System.Data.DbType.String)]
        public double UNITS_PER_PICK_ACTIVE
        {
            get { return _UNITS_PER_PICK_ACTIVE; }
            set { _UNITS_PER_PICK_ACTIVE = value; }
        }
        private string _HNDL_ATTR_ACTIVE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("HNDL_ATTR_ACTIVE", System.Data.DbType.String)]
        public string HNDL_ATTR_ACTIVE
        {
            get { return _HNDL_ATTR_ACTIVE; }
            set { _HNDL_ATTR_ACTIVE = value; }
        }
        private double _UNITS_PER_PICK_CASE_PICK;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UNITS_PER_PICK_CASE_PICK", System.Data.DbType.String)]
        public double UNITS_PER_PICK_CASE_PICK
        {
            get { return _UNITS_PER_PICK_CASE_PICK; }
            set { _UNITS_PER_PICK_CASE_PICK = value; }
        }
        private string _HNDL_ATTR_CASE_PICK;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("HNDL_ATTR_CASE_PICK", System.Data.DbType.String)]
        public string HNDL_ATTR_CASE_PICK
        {
            get { return _HNDL_ATTR_CASE_PICK; }
            set { _HNDL_ATTR_CASE_PICK = value; }
        }
        private double _UNITS_PER_PICK_RESV;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UNITS_PER_PICK_RESV", System.Data.DbType.String)]
        public double UNITS_PER_PICK_RESV
        {
            get { return _UNITS_PER_PICK_RESV; }
            set { _UNITS_PER_PICK_RESV = value; }
        }
        private string _HNDL_ATTR_RESV;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("HNDL_ATTR_RESV", System.Data.DbType.String)]
        public string HNDL_ATTR_RESV
        {
            get { return _HNDL_ATTR_RESV; }
            set { _HNDL_ATTR_RESV = value; }
        }
        private Int16 _SRL_NBR_REQD;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SRL_NBR_REQD", System.Data.DbType.Int16)]
        public Int16 SRL_NBR_REQD
        {
            get { return _SRL_NBR_REQD; }
            set { _SRL_NBR_REQD = value; }
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
        private string _MERCH_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MERCH_TYPE", System.Data.DbType.String)]
        public string MERCH_TYPE
        {
            get { return _MERCH_TYPE; }
            set { _MERCH_TYPE = value; }
        }
        private string _STORE_DEPT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STORE_DEPT", System.Data.DbType.String)]
        public string STORE_DEPT
        {
            get { return _STORE_DEPT; }
            set { _STORE_DEPT = value; }
        }
        private string _CRUSH_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CRUSH_CODE", System.Data.DbType.String)]
        public string CRUSH_CODE
        {
            get { return _CRUSH_CODE; }
            set { _CRUSH_CODE = value; }
        }
        private Single _STD_PACK_WIDTH;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_PACK_WIDTH", System.Data.DbType.String)]
        public Single STD_PACK_WIDTH
        {
            get { return _STD_PACK_WIDTH; }
            set { _STD_PACK_WIDTH = value; }
        }
        private Single _STD_PACK_LEN;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_PACK_LEN", System.Data.DbType.String)]
        public Single STD_PACK_LEN
        {
            get { return _STD_PACK_LEN; }
            set { _STD_PACK_LEN = value; }
        }
        private Single _STD_PACK_HT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_PACK_HT", System.Data.DbType.String)]
        public Single STD_PACK_HT
        {
            get { return _STD_PACK_HT; }
            set { _STD_PACK_HT = value; }
        }
        private Single _UNIT_WIDTH;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UNIT_WIDTH", System.Data.DbType.String)]
        public Single UNIT_WIDTH
        {
            get { return _UNIT_WIDTH; }
            set { _UNIT_WIDTH = value; }
        }
        private Single _UNIT_LEN;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UNIT_LEN", System.Data.DbType.String)]
        public Single UNIT_LEN
        {
            get { return _UNIT_LEN; }
            set { _UNIT_LEN = value; }
        }
        private Single _UNIT_HT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UNIT_HT", System.Data.DbType.String)]
        public Single UNIT_HT
        {
            get { return _UNIT_HT; }
            set { _UNIT_HT = value; }
        }
        private string _CONVEY_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CONVEY_FLAG", System.Data.DbType.String)]
        public string CONVEY_FLAG
        {
            get { return _CONVEY_FLAG; }
            set { _CONVEY_FLAG = value; }
        }
        private string _PKT_CONSOL_ATTR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PKT_CONSOL_ATTR", System.Data.DbType.String)]
        public string PKT_CONSOL_ATTR
        {
            get { return _PKT_CONSOL_ATTR; }
            set { _PKT_CONSOL_ATTR = value; }
        }
        private string _BUYER_DISP_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BUYER_DISP_CODE", System.Data.DbType.String)]
        public string BUYER_DISP_CODE
        {
            get { return _BUYER_DISP_CODE; }
            set { _BUYER_DISP_CODE = value; }
        }
        private string _MERCH_GROUP;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MERCH_GROUP", System.Data.DbType.String)]
        public string MERCH_GROUP
        {
            get { return _MERCH_GROUP; }
            set { _MERCH_GROUP = value; }
        }
        private string _SPL_INSTR_CODE_1;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SPL_INSTR_CODE_1", System.Data.DbType.String)]
        public string SPL_INSTR_CODE_1
        {
            get { return _SPL_INSTR_CODE_1; }
            set { _SPL_INSTR_CODE_1 = value; }
        }
        private string _SPL_INSTR_CODE_2;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SPL_INSTR_CODE_2", System.Data.DbType.String)]
        public string SPL_INSTR_CODE_2
        {
            get { return _SPL_INSTR_CODE_2; }
            set { _SPL_INSTR_CODE_2 = value; }
        }
        private string _SPL_INSTR_CODE_3;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SPL_INSTR_CODE_3", System.Data.DbType.String)]
        public string SPL_INSTR_CODE_3
        {
            get { return _SPL_INSTR_CODE_3; }
            set { _SPL_INSTR_CODE_3 = value; }
        }
        private string _SPL_INSTR_CODE_4;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SPL_INSTR_CODE_4", System.Data.DbType.String)]
        public string SPL_INSTR_CODE_4
        {
            get { return _SPL_INSTR_CODE_4; }
            set { _SPL_INSTR_CODE_4 = value; }
        }
        private string _SPL_INSTR_CODE_5;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SPL_INSTR_CODE_5", System.Data.DbType.String)]
        public string SPL_INSTR_CODE_5
        {
            get { return _SPL_INSTR_CODE_5; }
            set { _SPL_INSTR_CODE_5 = value; }
        }
        private string _SPL_INSTR_CODE_6;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SPL_INSTR_CODE_6", System.Data.DbType.String)]
        public string SPL_INSTR_CODE_6
        {
            get { return _SPL_INSTR_CODE_6; }
            set { _SPL_INSTR_CODE_6 = value; }
        }
        private string _SPL_INSTR_CODE_7;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SPL_INSTR_CODE_7", System.Data.DbType.String)]
        public string SPL_INSTR_CODE_7
        {
            get { return _SPL_INSTR_CODE_7; }
            set { _SPL_INSTR_CODE_7 = value; }
        }
        private string _SPL_INSTR_CODE_8;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SPL_INSTR_CODE_8", System.Data.DbType.String)]
        public string SPL_INSTR_CODE_8
        {
            get { return _SPL_INSTR_CODE_8; }
            set { _SPL_INSTR_CODE_8 = value; }
        }
        private string _SPL_INSTR_CODE_9;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SPL_INSTR_CODE_9", System.Data.DbType.String)]
        public string SPL_INSTR_CODE_9
        {
            get { return _SPL_INSTR_CODE_9; }
            set { _SPL_INSTR_CODE_9 = value; }
        }
        private string _SPL_INSTR_CODE_10;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SPL_INSTR_CODE_10", System.Data.DbType.String)]
        public string SPL_INSTR_CODE_10
        {
            get { return _SPL_INSTR_CODE_10; }
            set { _SPL_INSTR_CODE_10 = value; }
        }
        private string _SPL_INSTR_1;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SPL_INSTR_1", System.Data.DbType.String)]
        public string SPL_INSTR_1
        {
            get { return _SPL_INSTR_1; }
            set { _SPL_INSTR_1 = value; }
        }
        private string _SPL_INSTR_2;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SPL_INSTR_2", System.Data.DbType.String)]
        public string SPL_INSTR_2
        {
            get { return _SPL_INSTR_2; }
            set { _SPL_INSTR_2 = value; }
        }
        private string _SKU_PROFILE_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SKU_PROFILE_ID", System.Data.DbType.String)]
        public string SKU_PROFILE_ID
        {
            get { return _SKU_PROFILE_ID; }
            set { _SKU_PROFILE_ID = value; }
        }
        private Int16 _DFLT_BATCH_STAT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DFLT_BATCH_STAT", System.Data.DbType.Int16)]
        public Int16 DFLT_BATCH_STAT
        {
            get { return _DFLT_BATCH_STAT; }
            set { _DFLT_BATCH_STAT = value; }
        }
        private string _ECCN_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ECCN_NBR", System.Data.DbType.String)]
        public string ECCN_NBR
        {
            get { return _ECCN_NBR; }
            set { _ECCN_NBR = value; }
        }
        private string _EXP_LICN_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("EXP_LICN_NBR", System.Data.DbType.String)]
        public string EXP_LICN_NBR
        {
            get { return _EXP_LICN_NBR; }
            set { _EXP_LICN_NBR = value; }
        }
        private DateTime _EXP_LICN_XP_DATE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("EXP_LICN_XP_DATE", System.Data.DbType.DateTime)]
        public DateTime EXP_LICN_XP_DATE
        {
            get { return _EXP_LICN_XP_DATE; }
            set { _EXP_LICN_XP_DATE = value; }
        }
        private string _EXP_LICN_SYMBOL;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("EXP_LICN_SYMBOL", System.Data.DbType.String)]
        public string EXP_LICN_SYMBOL
        {
            get { return _EXP_LICN_SYMBOL; }
            set { _EXP_LICN_SYMBOL = value; }
        }
        private string _ORGN_CERT_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ORGN_CERT_CODE", System.Data.DbType.String)]
        public string ORGN_CERT_CODE
        {
            get { return _ORGN_CERT_CODE; }
            set { _ORGN_CERT_CODE = value; }
        }
        private string _COMMODITY_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("COMMODITY_CODE", System.Data.DbType.String)]
        public string COMMODITY_CODE
        {
            get { return _COMMODITY_CODE; }
            set { _COMMODITY_CODE = value; }
        }
        private string _COMMODITY_LEVEL_DESC;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("COMMODITY_LEVEL_DESC", System.Data.DbType.String)]
        public string COMMODITY_LEVEL_DESC
        {
            get { return _COMMODITY_LEVEL_DESC; }
            set { _COMMODITY_LEVEL_DESC = value; }
        }
        private string _FRT_CLASS;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("FRT_CLASS", System.Data.DbType.String)]
        public string FRT_CLASS
        {
            get { return _FRT_CLASS; }
            set { _FRT_CLASS = value; }
        }
        private string _NMFC_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("NMFC_CODE", System.Data.DbType.String)]
        public string NMFC_CODE
        {
            get { return _NMFC_CODE; }
            set { _NMFC_CODE = value; }
        }
        private Int16 _INCUB_DAYS;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("INCUB_DAYS", System.Data.DbType.Int16)]
        public Int16 INCUB_DAYS
        {
            get { return _INCUB_DAYS; }
            set { _INCUB_DAYS = value; }
        }
        private Single _INCUB_HOURS;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("INCUB_HOURS", System.Data.DbType.String)]
        public Single INCUB_HOURS
        {
            get { return _INCUB_HOURS; }
            set { _INCUB_HOURS = value; }
        }
        private string _DFLT_INCUB_LOCK;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DFLT_INCUB_LOCK", System.Data.DbType.String)]
        public string DFLT_INCUB_LOCK
        {
            get { return _DFLT_INCUB_LOCK; }
            set { _DFLT_INCUB_LOCK = value; }
        }
        private string _BASE_INCUB_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BASE_INCUB_FLAG", System.Data.DbType.String)]
        public string BASE_INCUB_FLAG
        {
            get { return _BASE_INCUB_FLAG; }
            set { _BASE_INCUB_FLAG = value; }
        }
        private double _MAX_RCPT_QTY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MAX_RCPT_QTY", System.Data.DbType.String)]
        public double MAX_RCPT_QTY
        {
            get { return _MAX_RCPT_QTY; }
            set { _MAX_RCPT_QTY = value; }
        }
        private string _PROMPT_PACK_QTY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROMPT_PACK_QTY", System.Data.DbType.String)]
        public string PROMPT_PACK_QTY
        {
            get { return _PROMPT_PACK_QTY; }
            set { _PROMPT_PACK_QTY = value; }
        }
        private string _HNDL_ATTR_UOM_ACT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("HNDL_ATTR_UOM_ACT", System.Data.DbType.String)]
        public string HNDL_ATTR_UOM_ACT
        {
            get { return _HNDL_ATTR_UOM_ACT; }
            set { _HNDL_ATTR_UOM_ACT = value; }
        }
        private string _HNDL_ATTR_UOM_CP;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("HNDL_ATTR_UOM_CP", System.Data.DbType.String)]
        public string HNDL_ATTR_UOM_CP
        {
            get { return _HNDL_ATTR_UOM_CP; }
            set { _HNDL_ATTR_UOM_CP = value; }
        }
        private string _SRL_NBR_BRCD_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SRL_NBR_BRCD_TYPE", System.Data.DbType.String)]
        public string SRL_NBR_BRCD_TYPE
        {
            get { return _SRL_NBR_BRCD_TYPE; }
            set { _SRL_NBR_BRCD_TYPE = value; }
        }
        private Int16 _DUP_SRL_NBR_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DUP_SRL_NBR_FLAG", System.Data.DbType.Int16)]
        public Int16 DUP_SRL_NBR_FLAG
        {
            get { return _DUP_SRL_NBR_FLAG; }
            set { _DUP_SRL_NBR_FLAG = value; }
        }
        private Int16 _MINOR_SRL_NBR_REQ;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MINOR_SRL_NBR_REQ", System.Data.DbType.Int16)]
        public Int16 MINOR_SRL_NBR_REQ
        {
            get { return _MINOR_SRL_NBR_REQ; }
            set { _MINOR_SRL_NBR_REQ = value; }
        }
        private string _FTZ_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("FTZ_FLAG", System.Data.DbType.String)]
        public string FTZ_FLAG
        {
            get { return _FTZ_FLAG; }
            set { _FTZ_FLAG = value; }
        }
        private string _HTS_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("HTS_NBR", System.Data.DbType.String)]
        public string HTS_NBR
        {
            get { return _HTS_NBR; }
            set { _HTS_NBR = value; }
        }
        private Single _STD_SUB_PACK_HT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_SUB_PACK_HT", System.Data.DbType.String)]
        public Single STD_SUB_PACK_HT
        {
            get { return _STD_SUB_PACK_HT; }
            set { _STD_SUB_PACK_HT = value; }
        }
        private Single _STD_SUB_PACK_LEN;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_SUB_PACK_LEN", System.Data.DbType.String)]
        public Single STD_SUB_PACK_LEN
        {
            get { return _STD_SUB_PACK_LEN; }
            set { _STD_SUB_PACK_LEN = value; }
        }
        private Single _STD_SUB_PACK_WIDTH;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_SUB_PACK_WIDTH", System.Data.DbType.String)]
        public Single STD_SUB_PACK_WIDTH
        {
            get { return _STD_SUB_PACK_WIDTH; }
            set { _STD_SUB_PACK_WIDTH = value; }
        }
        private double _STD_SUB_PACK_VOL;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_SUB_PACK_VOL", System.Data.DbType.String)]
        public double STD_SUB_PACK_VOL
        {
            get { return _STD_SUB_PACK_VOL; }
            set { _STD_SUB_PACK_VOL = value; }
        }
        private double _STD_SUB_PACK_WT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STD_SUB_PACK_WT", System.Data.DbType.String)]
        public double STD_SUB_PACK_WT
        {
            get { return _STD_SUB_PACK_WT; }
            set { _STD_SUB_PACK_WT = value; }
        }
        private string _PROMPT_FOR_VENDOR_ITEM_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROMPT_FOR_VENDOR_ITEM_NBR", System.Data.DbType.String)]
        public string PROMPT_FOR_VENDOR_ITEM_NBR
        {
            get { return _PROMPT_FOR_VENDOR_ITEM_NBR; }
            set { _PROMPT_FOR_VENDOR_ITEM_NBR = value; }
        }
        private string _CC_TOLER_UOM;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CC_TOLER_UOM", System.Data.DbType.String)]
        public string CC_TOLER_UOM
        {
            get { return _CC_TOLER_UOM; }
            set { _CC_TOLER_UOM = value; }
        }
        private Int64 _CC_VAR_TOLER_VALUE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CC_VAR_TOLER_VALUE", System.Data.DbType.Int64)]
        public Int64 CC_VAR_TOLER_VALUE
        {
            get { return _CC_VAR_TOLER_VALUE; }
            set { _CC_VAR_TOLER_VALUE = value; }
        }
        private double _VOCOLLECT_BASE_WT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VOCOLLECT_BASE_WT", System.Data.DbType.String)]
        public double VOCOLLECT_BASE_WT
        {
            get { return _VOCOLLECT_BASE_WT; }
            set { _VOCOLLECT_BASE_WT = value; }
        }
        private double _VOCOLLECT_BASE_QTY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VOCOLLECT_BASE_QTY", System.Data.DbType.String)]
        public double VOCOLLECT_BASE_QTY
        {
            get { return _VOCOLLECT_BASE_QTY; }
            set { _VOCOLLECT_BASE_QTY = value; }
        }
        private string _VOCOLLECT_BASE_ITEM;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VOCOLLECT_BASE_ITEM", System.Data.DbType.String)]
        public string VOCOLLECT_BASE_ITEM
        {
            get { return _VOCOLLECT_BASE_ITEM; }
            set { _VOCOLLECT_BASE_ITEM = value; }
        }
        private Int16 _PICK_WT_TOL_AMNT_ERROR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PICK_WT_TOL_AMNT_ERROR", System.Data.DbType.Int16)]
        public Int16 PICK_WT_TOL_AMNT_ERROR
        {
            get { return _PICK_WT_TOL_AMNT_ERROR; }
            set { _PICK_WT_TOL_AMNT_ERROR = value; }
        }
        private string _PRICE_TKT_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PRICE_TKT_TYPE", System.Data.DbType.String)]
        public string PRICE_TKT_TYPE
        {
            get { return _PRICE_TKT_TYPE; }
            set { _PRICE_TKT_TYPE = value; }
        }
        private string _SKU_IMAGE_FILENAME;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SKU_IMAGE_FILENAME", System.Data.DbType.String)]
        public string SKU_IMAGE_FILENAME
        {
            get { return _SKU_IMAGE_FILENAME; }
            set { _SKU_IMAGE_FILENAME = value; }
        }
        private double _MONETARY_VALUE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MONETARY_VALUE", System.Data.DbType.String)]
        public double MONETARY_VALUE
        {
            get { return _MONETARY_VALUE; }
            set { _MONETARY_VALUE = value; }
        }
        private string _MV_CURRENCY_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MV_CURRENCY_CODE", System.Data.DbType.String)]
        public string MV_CURRENCY_CODE
        {
            get { return _MV_CURRENCY_CODE; }
            set { _MV_CURRENCY_CODE = value; }
        }
        private string _MV_SIZE_UOM;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MV_SIZE_UOM", System.Data.DbType.String)]
        public string MV_SIZE_UOM
        {
            get { return _MV_SIZE_UOM; }
            set { _MV_SIZE_UOM = value; }
        }
        private string _UN_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("UN_NBR", System.Data.DbType.String)]
        public string UN_NBR
        {
            get { return _UN_NBR; }
            set { _UN_NBR = value; }
        }
        private string _CODE_DATE_PROMPT_METHOD_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CODE_DATE_PROMPT_METHOD_FLAG", System.Data.DbType.String)]
        public string CODE_DATE_PROMPT_METHOD_FLAG
        {
            get { return _CODE_DATE_PROMPT_METHOD_FLAG; }
            set { _CODE_DATE_PROMPT_METHOD_FLAG = value; }
        }
        private Int32 _MIN_RECV_TO_XPIRE_DAYS;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MIN_RECV_TO_XPIRE_DAYS", System.Data.DbType.Int32)]
        public Int32 MIN_RECV_TO_XPIRE_DAYS
        {
            get { return _MIN_RECV_TO_XPIRE_DAYS; }
            set { _MIN_RECV_TO_XPIRE_DAYS = value; }
        }
        private Int16 _MIN_PCNT_FOR_LPN_SPLIT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MIN_PCNT_FOR_LPN_SPLIT", System.Data.DbType.Int16)]
        public Int16 MIN_PCNT_FOR_LPN_SPLIT
        {
            get { return _MIN_PCNT_FOR_LPN_SPLIT; }
            set { _MIN_PCNT_FOR_LPN_SPLIT = value; }
        }
        private double _MIN_LPN_QTY_FOR_SPLIT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MIN_LPN_QTY_FOR_SPLIT", System.Data.DbType.String)]
        public double MIN_LPN_QTY_FOR_SPLIT
        {
            get { return _MIN_LPN_QTY_FOR_SPLIT; }
            set { _MIN_LPN_QTY_FOR_SPLIT = value; }
        }
        private string _PROD_CATGRY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROD_CATGRY", System.Data.DbType.String)]
        public string PROD_CATGRY
        {
            get { return _PROD_CATGRY; }
            set { _PROD_CATGRY = value; }
        }
        private string _PRICE_TIX_AVAIL_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PRICE_TIX_AVAIL_FLAG", System.Data.DbType.String)]
        public string PRICE_TIX_AVAIL_FLAG
        {
            get { return _PRICE_TIX_AVAIL_FLAG; }
            set { _PRICE_TIX_AVAIL_FLAG = value; }
        }
        private string _PREF_CRIT_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PREF_CRIT_FLAG", System.Data.DbType.String)]
        public string PREF_CRIT_FLAG
        {
            get { return _PREF_CRIT_FLAG; }
            set { _PREF_CRIT_FLAG = value; }
        }
        private string _PRODUCER_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PRODUCER_FLAG", System.Data.DbType.String)]
        public string PRODUCER_FLAG
        {
            get { return _PRODUCER_FLAG; }
            set { _PRODUCER_FLAG = value; }
        }
        private string _NET_COST_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("NET_COST_FLAG", System.Data.DbType.String)]
        public string NET_COST_FLAG
        {
            get { return _NET_COST_FLAG; }
            set { _NET_COST_FLAG = value; }
        }
        private string _STAB_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STAB_CODE", System.Data.DbType.String)]
        public string STAB_CODE
        {
            get { return _STAB_CODE; }
            set { _STAB_CODE = value; }
        }
        private string _ITEM_ORIENTATION;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ITEM_ORIENTATION", System.Data.DbType.String)]
        public string ITEM_ORIENTATION
        {
            get { return _ITEM_ORIENTATION; }
            set { _ITEM_ORIENTATION = value; }
        }
        private string _PROTN_FACTOR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROTN_FACTOR", System.Data.DbType.String)]
        public string PROTN_FACTOR
        {
            get { return _PROTN_FACTOR; }
            set { _PROTN_FACTOR = value; }
        }
        private Single _CAVITY_LEN;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CAVITY_LEN", System.Data.DbType.String)]
        public Single CAVITY_LEN
        {
            get { return _CAVITY_LEN; }
            set { _CAVITY_LEN = value; }
        }
        private Single _CAVITY_WD;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CAVITY_WD", System.Data.DbType.String)]
        public Single CAVITY_WD
        {
            get { return _CAVITY_WD; }
            set { _CAVITY_WD = value; }
        }
        private Single _CAVITY_HT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CAVITY_HT", System.Data.DbType.String)]
        public Single CAVITY_HT
        {
            get { return _CAVITY_HT; }
            set { _CAVITY_HT = value; }
        }
        private Single _INCREMENTAL_LEN;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("INCREMENTAL_LEN", System.Data.DbType.String)]
        public Single INCREMENTAL_LEN
        {
            get { return _INCREMENTAL_LEN; }
            set { _INCREMENTAL_LEN = value; }
        }
        private Single _INCREMENTAL_WD;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("INCREMENTAL_WD", System.Data.DbType.String)]
        public Single INCREMENTAL_WD
        {
            get { return _INCREMENTAL_WD; }
            set { _INCREMENTAL_WD = value; }
        }
        private Single _INCREMENTAL_HT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("INCREMENTAL_HT", System.Data.DbType.String)]
        public Single INCREMENTAL_HT
        {
            get { return _INCREMENTAL_HT; }
            set { _INCREMENTAL_HT = value; }
        }
        private string _STACKABLE_ITEM;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STACKABLE_ITEM", System.Data.DbType.String)]
        public string STACKABLE_ITEM
        {
            get { return _STACKABLE_ITEM; }
            set { _STACKABLE_ITEM = value; }
        }
        private Int16 _MAX_NEST_NUMBER;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MAX_NEST_NUMBER", System.Data.DbType.Int16)]
        public Int16 MAX_NEST_NUMBER
        {
            get { return _MAX_NEST_NUMBER; }
            set { _MAX_NEST_NUMBER = value; }
        }
        private DateTime _CREATE_DATE_TIME;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CREATE_DATE_TIME", System.Data.DbType.DateTime)]
        public DateTime CREATE_DATE_TIME
        {
            get { return _CREATE_DATE_TIME; }
            set { _CREATE_DATE_TIME = value; }
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
        private string _MARKS_NBRS;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MARKS_NBRS", System.Data.DbType.String)]
        public string MARKS_NBRS
        {
            get { return _MARKS_NBRS; }
            set { _MARKS_NBRS = value; }
        }
    } 

}
