using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MB.EAI.SOA.WMS.Entities
{
    // 需要引用的命名空间  using System.Runtime.Serialization;  

    /// <summary> 
    /// 文件生成时间 2014-04-03 09:53 
    /// </summary> 
    [DataContract(Name="Product")]
    [MB.Orm.Mapping.Att.ModelMap("INPT_ITEM_WHSE_MASTER", "InptItemWhseMaster", new string[] { "ID" })]
    [KnownType(typeof(InptItemWhseMasterInfo))]
    public class InptItemWhseMasterInfo : MB.Orm.Common.BaseModel
    {
        public InptItemWhseMasterInfo()
        {

        }
        private int _INPT_ITEM_WHSE_MASTER_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("INPT_ITEM_WHSE_MASTER_ID", System.Data.DbType.Int32)]
        public int INPT_ITEM_WHSE_MASTER_ID
        {
            get { return _INPT_ITEM_WHSE_MASTER_ID; }
            set { _INPT_ITEM_WHSE_MASTER_ID = value; }
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
        private string _SKU_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SKU_ID", System.Data.DbType.String)]
        public string SKU_ID
        {
            get { return _SKU_ID; }
            set { _SKU_ID = value; }
        }
        private string _WHSE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("WHSE", System.Data.DbType.String)]
        public string WHSE
        {
            get { return _WHSE; }
            set { _WHSE = value; }
        }
        private Int32 _LPN_PER_TIER;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("LPN_PER_TIER", System.Data.DbType.Int32)]
        public Int32 LPN_PER_TIER
        {
            get { return _LPN_PER_TIER; }
            set { _LPN_PER_TIER = value; }
        }
        private Int32 _TIER_PER_PLT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("TIER_PER_PLT", System.Data.DbType.Int32)]
        public Int32 TIER_PER_PLT
        {
            get { return _TIER_PER_PLT; }
            set { _TIER_PER_PLT = value; }
        }
        private string _CASE_SIZE_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CASE_SIZE_TYPE", System.Data.DbType.String)]
        public string CASE_SIZE_TYPE
        {
            get { return _CASE_SIZE_TYPE; }
            set { _CASE_SIZE_TYPE = value; }
        }
        private double _PICK_RATE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PICK_RATE", System.Data.DbType.String)]
        public double PICK_RATE
        {
            get { return _PICK_RATE; }
            set { _PICK_RATE = value; }
        }
        private double _WAGE_VALUE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("WAGE_VALUE", System.Data.DbType.String)]
        public double WAGE_VALUE
        {
            get { return _WAGE_VALUE; }
            set { _WAGE_VALUE = value; }
        }
        private double _PACK_RATE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PACK_RATE", System.Data.DbType.String)]
        public double PACK_RATE
        {
            get { return _PACK_RATE; }
            set { _PACK_RATE = value; }
        }
        private double _SPL_PROC_RATE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SPL_PROC_RATE", System.Data.DbType.String)]
        public double SPL_PROC_RATE
        {
            get { return _SPL_PROC_RATE; }
            set { _SPL_PROC_RATE = value; }
        }
        private string _AUTO_SUB_CASE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("AUTO_SUB_CASE", System.Data.DbType.String)]
        public string AUTO_SUB_CASE
        {
            get { return _AUTO_SUB_CASE; }
            set { _AUTO_SUB_CASE = value; }
        }
        private string _ASSIGN_DYNAMIC_ACTV_PICK_SITE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ASSIGN_DYNAMIC_ACTV_PICK_SITE", System.Data.DbType.String)]
        public string ASSIGN_DYNAMIC_ACTV_PICK_SITE
        {
            get { return _ASSIGN_DYNAMIC_ACTV_PICK_SITE; }
            set { _ASSIGN_DYNAMIC_ACTV_PICK_SITE = value; }
        }
        private string _ASSIGN_DYNAMIC_CASE_PICK_SITE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ASSIGN_DYNAMIC_CASE_PICK_SITE", System.Data.DbType.String)]
        public string ASSIGN_DYNAMIC_CASE_PICK_SITE
        {
            get { return _ASSIGN_DYNAMIC_CASE_PICK_SITE; }
            set { _ASSIGN_DYNAMIC_CASE_PICK_SITE = value; }
        }
        private string _PICK_LOCN_ASSIGN_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PICK_LOCN_ASSIGN_TYPE", System.Data.DbType.String)]
        public string PICK_LOCN_ASSIGN_TYPE
        {
            get { return _PICK_LOCN_ASSIGN_TYPE; }
            set { _PICK_LOCN_ASSIGN_TYPE = value; }
        }
        private string _PUTWY_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PUTWY_TYPE", System.Data.DbType.String)]
        public string PUTWY_TYPE
        {
            get { return _PUTWY_TYPE; }
            set { _PUTWY_TYPE = value; }
        }
        private Int32 _LET_UP_PRTY;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("LET_UP_PRTY", System.Data.DbType.Int32)]
        public Int32 LET_UP_PRTY
        {
            get { return _LET_UP_PRTY; }
            set { _LET_UP_PRTY = value; }
        }
        private Int16 _DFLT_WAVE_PROC_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DFLT_WAVE_PROC_TYPE", System.Data.DbType.Int16)]
        public Int16 DFLT_WAVE_PROC_TYPE
        {
            get { return _DFLT_WAVE_PROC_TYPE; }
            set { _DFLT_WAVE_PROC_TYPE = value; }
        }
        private Int16 _XCESS_WAVE_NEED_PROC_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("XCESS_WAVE_NEED_PROC_TYPE", System.Data.DbType.Int16)]
        public Int16 XCESS_WAVE_NEED_PROC_TYPE
        {
            get { return _XCESS_WAVE_NEED_PROC_TYPE; }
            set { _XCESS_WAVE_NEED_PROC_TYPE = value; }
        }
        private string _ALLOC_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ALLOC_TYPE", System.Data.DbType.String)]
        public string ALLOC_TYPE
        {
            get { return _ALLOC_TYPE; }
            set { _ALLOC_TYPE = value; }
        }
        private string _VIOLATE_FIFO_ALLOC_QTY_MATCH;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VIOLATE_FIFO_ALLOC_QTY_MATCH", System.Data.DbType.String)]
        public string VIOLATE_FIFO_ALLOC_QTY_MATCH
        {
            get { return _VIOLATE_FIFO_ALLOC_QTY_MATCH; }
            set { _VIOLATE_FIFO_ALLOC_QTY_MATCH = value; }
        }
        private string _QV_ITEM_GRP;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("QV_ITEM_GRP", System.Data.DbType.String)]
        public string QV_ITEM_GRP
        {
            get { return _QV_ITEM_GRP; }
            set { _QV_ITEM_GRP = value; }
        }
        private string _QUAL_INSPCT_ITEM_GRP;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("QUAL_INSPCT_ITEM_GRP", System.Data.DbType.String)]
        public string QUAL_INSPCT_ITEM_GRP
        {
            get { return _QUAL_INSPCT_ITEM_GRP; }
            set { _QUAL_INSPCT_ITEM_GRP = value; }
        }
        private double _MAX_UNITS_IN_DYNAMIC_ACTV;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MAX_UNITS_IN_DYNAMIC_ACTV", System.Data.DbType.String)]
        public double MAX_UNITS_IN_DYNAMIC_ACTV
        {
            get { return _MAX_UNITS_IN_DYNAMIC_ACTV; }
            set { _MAX_UNITS_IN_DYNAMIC_ACTV = value; }
        }
        private Int32 _MAX_CASES_IN_DYNAMIC_ACTV;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MAX_CASES_IN_DYNAMIC_ACTV", System.Data.DbType.Int32)]
        public Int32 MAX_CASES_IN_DYNAMIC_ACTV
        {
            get { return _MAX_CASES_IN_DYNAMIC_ACTV; }
            set { _MAX_CASES_IN_DYNAMIC_ACTV = value; }
        }
        private double _MAX_UNITS_IN_DYNAMIC_CASE_PICK;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MAX_UNITS_IN_DYNAMIC_CASE_PICK", System.Data.DbType.String)]
        public double MAX_UNITS_IN_DYNAMIC_CASE_PICK
        {
            get { return _MAX_UNITS_IN_DYNAMIC_CASE_PICK; }
            set { _MAX_UNITS_IN_DYNAMIC_CASE_PICK = value; }
        }
        private Int32 _MAX_CASES_IN_DYNAMIC_CASE_PICK;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MAX_CASES_IN_DYNAMIC_CASE_PICK", System.Data.DbType.Int32)]
        public Int32 MAX_CASES_IN_DYNAMIC_CASE_PICK
        {
            get { return _MAX_CASES_IN_DYNAMIC_CASE_PICK; }
            set { _MAX_CASES_IN_DYNAMIC_CASE_PICK = value; }
        }
        private Int16 _STAT_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("STAT_CODE", System.Data.DbType.Int16)]
        public Int16 STAT_CODE
        {
            get { return _STAT_CODE; }
            set { _STAT_CODE = value; }
        }
        private Int32 _SHELF_DAYS;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SHELF_DAYS", System.Data.DbType.Int32)]
        public Int32 SHELF_DAYS
        {
            get { return _SHELF_DAYS; }
            set { _SHELF_DAYS = value; }
        }
        private string _VENDOR_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VENDOR_ID", System.Data.DbType.String)]
        public string VENDOR_ID
        {
            get { return _VENDOR_ID; }
            set { _VENDOR_ID = value; }
        }
        private Int32 _VENDOR_CARTON_PER_TIER;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VENDOR_CARTON_PER_TIER", System.Data.DbType.Int32)]
        public Int32 VENDOR_CARTON_PER_TIER
        {
            get { return _VENDOR_CARTON_PER_TIER; }
            set { _VENDOR_CARTON_PER_TIER = value; }
        }
        private Int32 _VENDOR_TIER_PER_PLT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VENDOR_TIER_PER_PLT", System.Data.DbType.Int32)]
        public Int32 VENDOR_TIER_PER_PLT
        {
            get { return _VENDOR_TIER_PER_PLT; }
            set { _VENDOR_TIER_PER_PLT = value; }
        }
        private Int32 _ORD_CARTON_PER_TIER;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ORD_CARTON_PER_TIER", System.Data.DbType.Int32)]
        public Int32 ORD_CARTON_PER_TIER
        {
            get { return _ORD_CARTON_PER_TIER; }
            set { _ORD_CARTON_PER_TIER = value; }
        }
        private Int32 _ORD_TIER_PER_PLT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ORD_TIER_PER_PLT", System.Data.DbType.Int32)]
        public Int32 ORD_TIER_PER_PLT
        {
            get { return _ORD_TIER_PER_PLT; }
            set { _ORD_TIER_PER_PLT = value; }
        }
        private string _DFLT_CATCH_WT_METHOD;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DFLT_CATCH_WT_METHOD", System.Data.DbType.String)]
        public string DFLT_CATCH_WT_METHOD
        {
            get { return _DFLT_CATCH_WT_METHOD; }
            set { _DFLT_CATCH_WT_METHOD = value; }
        }
        private string _DFLT_DATE_MASK;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DFLT_DATE_MASK", System.Data.DbType.String)]
        public string DFLT_DATE_MASK
        {
            get { return _DFLT_DATE_MASK; }
            set { _DFLT_DATE_MASK = value; }
        }
        private string _CARTON_BREAK_ATTR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CARTON_BREAK_ATTR", System.Data.DbType.String)]
        public string CARTON_BREAK_ATTR
        {
            get { return _CARTON_BREAK_ATTR; }
            set { _CARTON_BREAK_ATTR = value; }
        }
        private Int16 _PROC_STAT_CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PROC_STAT_CODE", System.Data.DbType.Int16)]
        public Int16 PROC_STAT_CODE
        {
            get { return _PROC_STAT_CODE; }
            set { _PROC_STAT_CODE = value; }
        }
        private Int32 _ERROR_SEQ_NBR;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ERROR_SEQ_NBR", System.Data.DbType.Int32)]
        public Int32 ERROR_SEQ_NBR
        {
            get { return _ERROR_SEQ_NBR; }
            set { _ERROR_SEQ_NBR = value; }
        }
        private string _MISC_SHORT_ALPHA_1;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MISC_SHORT_ALPHA_1", System.Data.DbType.String)]
        public string MISC_SHORT_ALPHA_1
        {
            get { return _MISC_SHORT_ALPHA_1; }
            set { _MISC_SHORT_ALPHA_1 = value; }
        }
        private string _MISC_SHORT_ALPHA_2;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MISC_SHORT_ALPHA_2", System.Data.DbType.String)]
        public string MISC_SHORT_ALPHA_2
        {
            get { return _MISC_SHORT_ALPHA_2; }
            set { _MISC_SHORT_ALPHA_2 = value; }
        }
        private string _MISC_ALPHA_1;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MISC_ALPHA_1", System.Data.DbType.String)]
        public string MISC_ALPHA_1
        {
            get { return _MISC_ALPHA_1; }
            set { _MISC_ALPHA_1 = value; }
        }
        private string _MISC_ALPHA_2;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MISC_ALPHA_2", System.Data.DbType.String)]
        public string MISC_ALPHA_2
        {
            get { return _MISC_ALPHA_2; }
            set { _MISC_ALPHA_2 = value; }
        }
        private string _MISC_ALPHA_3;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MISC_ALPHA_3", System.Data.DbType.String)]
        public string MISC_ALPHA_3
        {
            get { return _MISC_ALPHA_3; }
            set { _MISC_ALPHA_3 = value; }
        }
        private double _MISC_NUMERIC_1;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MISC_NUMERIC_1", System.Data.DbType.String)]
        public double MISC_NUMERIC_1
        {
            get { return _MISC_NUMERIC_1; }
            set { _MISC_NUMERIC_1 = value; }
        }
        private double _MISC_NUMERIC_2;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MISC_NUMERIC_2", System.Data.DbType.String)]
        public double MISC_NUMERIC_2
        {
            get { return _MISC_NUMERIC_2; }
            set { _MISC_NUMERIC_2 = value; }
        }
        private double _MISC_NUMERIC_3;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MISC_NUMERIC_3", System.Data.DbType.String)]
        public double MISC_NUMERIC_3
        {
            get { return _MISC_NUMERIC_3; }
            set { _MISC_NUMERIC_3 = value; }
        }
        private DateTime _MISC_DATE_1;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MISC_DATE_1", System.Data.DbType.DateTime)]
        public DateTime MISC_DATE_1
        {
            get { return _MISC_DATE_1; }
            set { _MISC_DATE_1 = value; }
        }
        private DateTime _MISC_DATE_2;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MISC_DATE_2", System.Data.DbType.DateTime)]
        public DateTime MISC_DATE_2
        {
            get { return _MISC_DATE_2; }
            set { _MISC_DATE_2 = value; }
        }
        private string _SKU_PROFILE_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SKU_PROFILE_ID", System.Data.DbType.String)]
        public string SKU_PROFILE_ID
        {
            get { return _SKU_PROFILE_ID; }
            set { _SKU_PROFILE_ID = value; }
        }
        private string _CHUTE_ASSIGN_TYPE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CHUTE_ASSIGN_TYPE", System.Data.DbType.String)]
        public string CHUTE_ASSIGN_TYPE
        {
            get { return _CHUTE_ASSIGN_TYPE; }
            set { _CHUTE_ASSIGN_TYPE = value; }
        }
        private Int16 _FIFO_RANGE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("FIFO_RANGE", System.Data.DbType.Int16)]
        public Int16 FIFO_RANGE
        {
            get { return _FIFO_RANGE; }
            set { _FIFO_RANGE = value; }
        }
        private string _ACTV_REPL_ORGN;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ACTV_REPL_ORGN", System.Data.DbType.String)]
        public string ACTV_REPL_ORGN
        {
            get { return _ACTV_REPL_ORGN; }
            set { _ACTV_REPL_ORGN = value; }
        }
        private double _PRTL_CASE_ALLOC_THRESH_UNITS;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PRTL_CASE_ALLOC_THRESH_UNITS", System.Data.DbType.String)]
        public double PRTL_CASE_ALLOC_THRESH_UNITS
        {
            get { return _PRTL_CASE_ALLOC_THRESH_UNITS; }
            set { _PRTL_CASE_ALLOC_THRESH_UNITS = value; }
        }
        private double _PRTL_CASE_PUTWY_THRESH_UNITS;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PRTL_CASE_PUTWY_THRESH_UNITS", System.Data.DbType.String)]
        public double PRTL_CASE_PUTWY_THRESH_UNITS
        {
            get { return _PRTL_CASE_PUTWY_THRESH_UNITS; }
            set { _PRTL_CASE_PUTWY_THRESH_UNITS = value; }
        }
        private string _VENDOR_TAGGED_EPC_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VENDOR_TAGGED_EPC_FLAG", System.Data.DbType.String)]
        public string VENDOR_TAGGED_EPC_FLAG
        {
            get { return _VENDOR_TAGGED_EPC_FLAG; }
            set { _VENDOR_TAGGED_EPC_FLAG = value; }
        }
        private string _DFLT_MIN_FROM_PREV_LOCN_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DFLT_MIN_FROM_PREV_LOCN_FLAG", System.Data.DbType.String)]
        public string DFLT_MIN_FROM_PREV_LOCN_FLAG
        {
            get { return _DFLT_MIN_FROM_PREV_LOCN_FLAG; }
            set { _DFLT_MIN_FROM_PREV_LOCN_FLAG = value; }
        }
        private string _SLOT_MISC_1;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SLOT_MISC_1", System.Data.DbType.String)]
        public string SLOT_MISC_1
        {
            get { return _SLOT_MISC_1; }
            set { _SLOT_MISC_1 = value; }
        }
        private string _SLOT_MISC_2;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SLOT_MISC_2", System.Data.DbType.String)]
        public string SLOT_MISC_2
        {
            get { return _SLOT_MISC_2; }
            set { _SLOT_MISC_2 = value; }
        }
        private string _SLOT_MISC_3;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SLOT_MISC_3", System.Data.DbType.String)]
        public string SLOT_MISC_3
        {
            get { return _SLOT_MISC_3; }
            set { _SLOT_MISC_3 = value; }
        }
        private string _SLOT_MISC_4;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SLOT_MISC_4", System.Data.DbType.String)]
        public string SLOT_MISC_4
        {
            get { return _SLOT_MISC_4; }
            set { _SLOT_MISC_4 = value; }
        }
        private string _SLOT_MISC_5;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SLOT_MISC_5", System.Data.DbType.String)]
        public string SLOT_MISC_5
        {
            get { return _SLOT_MISC_5; }
            set { _SLOT_MISC_5 = value; }
        }
        private string _SLOT_MISC_6;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SLOT_MISC_6", System.Data.DbType.String)]
        public string SLOT_MISC_6
        {
            get { return _SLOT_MISC_6; }
            set { _SLOT_MISC_6 = value; }
        }
        private string _SLOT_ROTATE_EACHES_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SLOT_ROTATE_EACHES_FLAG", System.Data.DbType.String)]
        public string SLOT_ROTATE_EACHES_FLAG
        {
            get { return _SLOT_ROTATE_EACHES_FLAG; }
            set { _SLOT_ROTATE_EACHES_FLAG = value; }
        }
        private string _SLOT_ROTATE_INNERS_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SLOT_ROTATE_INNERS_FLAG", System.Data.DbType.String)]
        public string SLOT_ROTATE_INNERS_FLAG
        {
            get { return _SLOT_ROTATE_INNERS_FLAG; }
            set { _SLOT_ROTATE_INNERS_FLAG = value; }
        }
        private string _SLOT_ROTATE_BINS_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SLOT_ROTATE_BINS_FLAG", System.Data.DbType.String)]
        public string SLOT_ROTATE_BINS_FLAG
        {
            get { return _SLOT_ROTATE_BINS_FLAG; }
            set { _SLOT_ROTATE_BINS_FLAG = value; }
        }
        private string _SLOT_ROTATE_CASES_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SLOT_ROTATE_CASES_FLAG", System.Data.DbType.String)]
        public string SLOT_ROTATE_CASES_FLAG
        {
            get { return _SLOT_ROTATE_CASES_FLAG; }
            set { _SLOT_ROTATE_CASES_FLAG = value; }
        }
        private string _SLOT_3D_SLOTTING_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SLOT_3D_SLOTTING_FLAG", System.Data.DbType.String)]
        public string SLOT_3D_SLOTTING_FLAG
        {
            get { return _SLOT_3D_SLOTTING_FLAG; }
            set { _SLOT_3D_SLOTTING_FLAG = value; }
        }
        private string _SLOT_NEST_EACHES_FLAG;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SLOT_NEST_EACHES_FLAG", System.Data.DbType.String)]
        public string SLOT_NEST_EACHES_FLAG
        {
            get { return _SLOT_NEST_EACHES_FLAG; }
            set { _SLOT_NEST_EACHES_FLAG = value; }
        }
        private Single _SLOT_INCR_HT;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SLOT_INCR_HT", System.Data.DbType.String)]
        public Single SLOT_INCR_HT
        {
            get { return _SLOT_INCR_HT; }
            set { _SLOT_INCR_HT = value; }
        }
        private Single _SLOT_INCR_LEN;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SLOT_INCR_LEN", System.Data.DbType.String)]
        public Single SLOT_INCR_LEN
        {
            get { return _SLOT_INCR_LEN; }
            set { _SLOT_INCR_LEN = value; }
        }
        private Single _SLOT_INCR_WIDTH;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SLOT_INCR_WIDTH", System.Data.DbType.String)]
        public Single SLOT_INCR_WIDTH
        {
            get { return _SLOT_INCR_WIDTH; }
            set { _SLOT_INCR_WIDTH = value; }
        }
        private Int16 _NBR_OF_DYN_ACTV_PICK_PER_SKU;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("NBR_OF_DYN_ACTV_PICK_PER_SKU", System.Data.DbType.Int16)]
        public Int16 NBR_OF_DYN_ACTV_PICK_PER_SKU
        {
            get { return _NBR_OF_DYN_ACTV_PICK_PER_SKU; }
            set { _NBR_OF_DYN_ACTV_PICK_PER_SKU = value; }
        }
        private Int16 _NBR_OF_DYN_CASE_PICK_PER_SKU;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("NBR_OF_DYN_CASE_PICK_PER_SKU", System.Data.DbType.Int16)]
        public Int16 NBR_OF_DYN_CASE_PICK_PER_SKU
        {
            get { return _NBR_OF_DYN_CASE_PICK_PER_SKU; }
            set { _NBR_OF_DYN_CASE_PICK_PER_SKU = value; }
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
    } 

}
